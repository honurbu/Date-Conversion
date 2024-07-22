using DateConversion.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateConversion.Context
{
    public class MyDbContext : DbContext
    {
        public DbSet<DateRecord> DateRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=DateRecord;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;");
        }
    }
}
