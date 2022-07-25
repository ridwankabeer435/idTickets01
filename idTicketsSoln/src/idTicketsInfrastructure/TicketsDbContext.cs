using idTicketsInfrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure
{
    public class TicketsDbContext : DbContext
    {
        // SqlConnectionBuilder builder = new SqlConnectionBuilder();
        public TicketsDbContext()
        {

        }

        public TicketsDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void  OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                /*
                var configurationRoot = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                 .Build();
                */

                optionsBuilder.UseNpgsql("Server=localhost;Database=id_tickets_01;port=5432;User Id=postgres;Password=ABAJwatch735db;"); // the connection string will be obtained from appsettings.json
                //optionsBuilder.UseNpgsql(configurationRoot.GetConnectionString("IdTicketsDatabase")); // the connection string will be obtained from appsettings.json

            }
        }

        // then set up Dbset for primary db entities

        public DbSet<User> users { get; set; }
        public virtual DbSet<Ticket> tickets { get; set; }
        public DbSet<Comment> comments { get; set; }

    }
}
