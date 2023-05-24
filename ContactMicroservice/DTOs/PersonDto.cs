using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMicroservice.DTOs
{
    public class PersonDto
    {
        public string Name {get;set;}
        public string Surname { get; set; }
        public List<ContactInfoDto>? ContactInfoDto {get;set;}
        public string CompanyName {get;set;}
        public string Location { get; set; }
    }
}