using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactMicroservice.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
            
        }
        public DbSet<Person> People {get;set;}
        public DbSet<Company> Companies { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
    }
    
}