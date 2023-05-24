using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportMicroService.Models;

namespace ReportMicroService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {

        private readonly string bootstrapServers = "localhost:29092";
        private readonly string topic = "test";
        private AppDbContext _context;
        public ReportController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("RequestReport/{location}")]
        public async Task<IActionResult> RequestReport(string locaiton)
        {
            if (!string.IsNullOrEmpty(locaiton))
            {
                Report rep = new Report();
                rep.CurrentState = enums.ReportState.Waiting;
                rep.RequestedAt = DateTime.UtcNow;
                rep.Data = "";
                _context.Reports.Add(rep);
                _context.SaveChanges();
                string message = JsonSerializer.Serialize(new ReportRequestModel() { Location = locaiton, ReportId = rep.UUID });

                SendReportRequest(topic, message);
                return Ok();
            }
            return BadRequest();

        }

        [HttpGet]
        [Route("Reports")]
        public async Task<ActionResult<List<Report>>> GetAllReports(){
            return await _context.Reports.ToListAsync();
        }
           private async Task<bool> SendReportRequest(string topic, string message)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = Dns.GetHostName()
            };
            try
            {
                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync(topic, new Message<Null, string>
                    {
                        Value = message
                    });


                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }
            return await Task.FromResult(false);
        }

    }
}