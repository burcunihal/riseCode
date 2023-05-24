using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ContactMicroservice.Models
{
    public class Company
    {
        [Key]
        public Guid UUID { get; set; }
        public string CompanyName { get; set; }
    }
}