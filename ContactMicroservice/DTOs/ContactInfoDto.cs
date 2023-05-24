using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactMicroservice.Models;

namespace ContactMicroservice.DTOs
{
    public class ContactInfoDto
    {
     public string Content { get; set; }
     public ContactType Type { get; set; }
    }
}