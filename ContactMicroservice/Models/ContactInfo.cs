using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ContactMicroservice.Models
{
    public class ContactInfo
    {
        [Key]
        public Guid UUID { get; set; }
        public ContactType Type { get; set; }
        public string Content { get; set; }
    }
}