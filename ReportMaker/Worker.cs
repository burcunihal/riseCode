using System.Text.Json;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using ReportMaker.Models;

namespace ReportMaker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private AppDbContext _context;

    public Worker(ILogger<Worker> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string topic = "test";
        string groupId = "test_group";
        string bootstrapServers = "localhost:29092";
        var config = new ConsumerConfig
        {
            GroupId = groupId,
            BootstrapServers = bootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        while (!stoppingToken.IsCancellationRequested)
        {

            try
            {
                using (var consumerBuilder = new ConsumerBuilder
                <Ignore, string>(config).Build())
                {
                    consumerBuilder.Subscribe(topic);
                    var cancelToken = new CancellationTokenSource();

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume
                                         (cancelToken.Token);
                            var orderRequest = JsonSerializer.Deserialize
                                <ReportRequestModel>
                                    (consumer.Message.Value);
                            _logger.LogInformation(orderRequest.Location);
                            var _report = _context.Reports.Where(r => r.UUID.Equals(orderRequest.ReportId)).FirstOrDefault();
                            _report.CurrentState = Enums.ReportState.InProgress;
                            _context.SaveChanges();
                            var data = _context.People.Include(p => p.ContactInfos).Where(p => p.Location.Contains(orderRequest.Location)).ToList();
                            _logger.LogInformation(JsonSerializer.Serialize(data).ToString());
                            _report.Data = JsonSerializer.Serialize(data).ToString();
                            _context.SaveChanges();
                        }


                    }
                    catch (OperationCanceledException)
                    {
                        consumerBuilder.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            _logger.LogInformation("Worker waiting for report command: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
