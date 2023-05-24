using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportMaker.Models
{
    public class ReportRequestModel
    {
        public string Location { get; set; }
        public Guid ReportId { get; set; }
    }
}