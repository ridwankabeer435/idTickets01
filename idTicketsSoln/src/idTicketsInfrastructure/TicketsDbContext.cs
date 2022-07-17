using idTicketsInfrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure
{
    class TicketsDbContext : DbContext
    {
        // SqlConnectionBuilder builder = new SqlConnectionBuilder();

        public TicketsDbContext(DbContextOptions options) : base(options)
        {

        }

        // then set up Dbset for primary db entities

        public DbSet<User> users { get; set; }
        public DbSet<Ticket> tickets { get; set; }
        public DbSet<Comment> comments { get; set; }

    }
}
