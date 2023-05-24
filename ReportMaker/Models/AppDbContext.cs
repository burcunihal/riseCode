using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ReportMaker.Models
{
   public class AppDbContext:DbContext
    {
           public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
            
        }
        public DbSet<Report> Reports {get;set;}
               public DbSet<Person> People {get;set;}
        public DbSet<ContactInfo> ContactInfos { get; set; }
    }
}