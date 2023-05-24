using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportMicroService.Models
{
    public class ReportRequestModel
    {
        public string Location { get; set; }
        public Guid ReportId { get; set; }

    }
}