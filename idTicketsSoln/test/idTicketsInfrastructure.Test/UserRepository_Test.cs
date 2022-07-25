using idTicketsInfrastructure.Models;
using idTicketsInfrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Test
{
    public class UserRepository_Test
    {
        private UsersRepository _sut;
        private List<User> expected;
        private Mock<DbSet<User>> mockSet;
        private Mock<TicketsDbContext> mockContext;

        // mock database setup -- maybe have an abstract class set up to configure the mock database
    }
}
