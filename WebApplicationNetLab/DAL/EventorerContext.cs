using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WebApplicationNetLab.Models;

namespace WebApplicationNetLab.DAL
{
    public class EventorerContext : DbContext
    {
        public EventorerContext() : base("EventorerContext")
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}