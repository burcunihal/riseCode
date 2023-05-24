using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ReportMaker.Enums;

namespace ReportMaker.Models
{
    public class Report
    {
        public DateTime RequestedAt { get; set; }
        public ReportState CurrentState { get; set; }
        [Key]
        public Guid UUID { get; set; }
        public string Data { get; set; }
    }
}