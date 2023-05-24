using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ReportMaker.Models
{
    public class Person
    {
        [Key]
        public Guid UUID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual List<ContactInfo> ContactInfos {get;set;}
        public string Location { get; set; }
    }
}