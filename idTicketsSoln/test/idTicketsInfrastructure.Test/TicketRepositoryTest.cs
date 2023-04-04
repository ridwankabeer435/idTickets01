using Bogus;
using Dapper;
using idTicketsInfrastructure.Models;
using idTicketsInfrastructure.Repository;
using Moq;
using Moq.Dapper;
using System.Data;

namespace idTicketsInfrastructure.Test
{
    public class TicketRepositoryTest
    {
        //private Mock<IDbConnection> _mockConnection;
        private Mock<IDbConnectionFactory> _dbAccess;
        private readonly Mock<IDbConnection> _mockDbConnection;
        private TicketRepository _ticketRepository;


        public TicketRepositoryTest()
        {
            _dbAccess = new Mock<IDbConnectionFactory>();
            _mockDbConnection = new Mock<IDbConnection>();
            _ticketRepository = new TicketRepository(_dbAccess.Object);

            // arrange
            var expectedTicket = DataGenerator.sampleTickets.FirstOrDefault();
            int ticketId = expectedTicket.ticketId;
            var sql = $"SELECT * FROM tickets WHERE id = @Id";
            var parameters = new { Id = ticketId };

            _dbAccess.Setup(c => c.GetConnection()).Returns(_mockDbConnection.Object);
        }

        [Fact]
        public async void getTicketById()
        {
            // arrange
            Ticket expectedTicket = DataGenerator.sampleTickets.FirstOrDefault();
            int ticketId = expectedTicket.ticketId;
            string sql = $"SELECT * FROM tickets WHERE id = @Id";
            var parameters = new { Id = ticketId };

            _mockDbConnection.Setup(x => x.QueryFirstOrDefaultAsync<Ticket>(sql, parameters, null, null, null)).Returns(Task.FromResult(expectedTicket));

            // act

            var actualTicket = await _ticketRepository.getById(ticketId);

            // assert
            Assert.Equal(expectedTicket, actualTicket);
        }

        [Fact]
        public async void addNewTicket()
        {
            //have an existing list of tickets
        }
      
    }
}
