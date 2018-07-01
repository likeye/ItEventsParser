using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLibrary.DB
{
    public class ItEventsParserDbContext : DbContext
    {
        public ItEventsParserDbContext() : base("name = ItEventsParserEntity")
        {
            
        }
        public DbSet<Events> Events { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.HasDefaultSchema("dbo");
        }

    }
}
