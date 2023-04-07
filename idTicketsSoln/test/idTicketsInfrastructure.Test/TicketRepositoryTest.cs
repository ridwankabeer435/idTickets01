using Bogus;
using Dapper;
using idTicketsInfrastructure.Models;
using idTicketsInfrastructure.Repository;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Moq;
using Moq.Dapper;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.ValueGeneration.Internal;
using System.Data;
using System.Diagnostics;

namespace idTicketsInfrastructure.Test
{
    public class TicketRepositoryTest
    {
        //private Mock<IDbConnection> _mockConnection;
        private Mock<IDbConnectionFactory> _dbAccess;
        private readonly Mock<IDbConnection> _mockDbConnection;
        private TicketRepository _ticketRepository;
        private readonly IDbConnectionFactory _connectionFactory;

        private readonly IDbConnection _connection;

        public TicketRepositoryTest()
        {

            // Create a connection to the test database
            _connectionFactory = new TestDbConnectionFactory();
            _connection = _connectionFactory.GetConnection();
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                // then run the test db schema
            }

            // Initialize the test database schema
            // test database
            
            var sqlCreateTables = @"
                DROP TABLE IF EXISTS tickets, users, comments;

                CREATE TABLE IF NOT EXISTS users (
                    id SERIAL PRIMARY KEY,
                    firstName VARCHAR(75) NOT NULL,
                    lastName VARCHAR(50) NOT NULL,
                    email VARCHAR(255) NOT NULL,
                    creationDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    updateDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    isITStaff bool NOT NULL DEFAULT FALSE,
                    isSupervisor bool NOT NULL DEFAULT FALSE

                );




                CREATE TABLE IF NOT EXISTS departments(
                    id SERIAL PRIMARY KEY,
                    title VARCHAR(40) NOT NULL
    
                );



                CREATE TABLE IF NOT EXISTS tickets(
                    id SERIAL PRIMARY KEY,
                    requestorId bigint not null,
                    assigneeId bigint,
                    title VARCHAR(80),
                    details VARCHAR(255),
                    status VARCHAR(10),
                    priority VARCHAR(10),
                    creationDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    updateDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
                );


                CREATE TABLE IF NOT EXISTS comments(
                    id SERIAL PRIMARY KEY,
                    userId bigint not null,
                    ticketId bigint not null,
                    textContent varchar not null,
                    creationDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP  
                );


                CREATE TABLE IF NOT EXISTS files_info(
                    id SERIAL PRIMARY KEY,
                    name varchar(70) not null,
                    typeExtension varchar(15),
                    uploaderId bigint not null,
                    ticketId bigint,
                    commentId bigint,
                    storageLocation varchar(255),
                    creationDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,  
                    updateDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
                );
                ";
            _connection.Execute(sqlCreateTables);
           

            var sqlInsertData = @"INSERT INTO tickets (requestorId, assigneeId, title, details, status, priority, creationDate, updateDate)
                VALUES
                    (1, 2, 'Need help with email', 'I can''t seem to send emails, can you help me?', 'ISSUED', 'HIGH', '2022-03-01 12:00:00', '2022-03-01 12:00:00'),
                    (3, 1, 'Printer not working', 'The printer won''t print anything, can you check it out?', 'ISSUED', 'MEDIUM', '2022-03-02 14:00:00', '2022-03-02 14:00:00'),
                    (4, 2, 'Can''t connect to VPN', 'I need to connect to the VPN but it keeps failing, can you help me troubleshoot?', 'ISSUED', 'LOW', '2022-03-02 15:00:00', '2022-03-02 15:00:00'),
                    (2, 3, 'New software request', 'I need a license for the new software we are using, can you provide one?', 'ISSUED', 'HIGH', '2022-03-03 16:00:00', '2022-03-03 16:00:00'),
                    (1, 4, 'Computer won''t start', 'My computer won''t turn on, can you fix it?', 'ISSUED', 'high', '2022-03-03 17:00:00', '2022-03-03 17:00:00'),
                    (5, 1, 'Need help with Excel', 'I need help with a formula in Excel, can you assist me?', 'ISSUED', 'MEDIUM', '2022-03-04 18:00:00', '2022-03-04 18:00:00'),
                    (6, 2, 'Need new mouse', 'My mouse is broken and I need a replacement, can you order one for me?', 'ISSUED', 'LOW', '2022-03-04 19:00:00', '2022-03-04 19:00:00'),
                    (3, 4, 'Can''t access shared drive', 'I can''t seem to access the shared drive, can you help me troubleshoot?', 'ISSUED', 'MEDIUM', '2022-03-05 20:00:00', '2022-03-05 20:00:00'),
                    (7, 1, 'Need help with PowerPoint', 'I need help with a presentation in PowerPoint, can you assist me?', 'ISSUED', 'HIGH', '2022-03-05 21:00:00', '2022-03-05 21:00:00'),
                    (5, 2, 'Need new monitor', 'My monitor is flickering and I need a replacement, can you order one for me?', 'ISSUED', 'LOW', '2022-03-06 22:00:00', '2022-03-06 22:00:00'),
                    (8, 3, 'Can''t access email', 'I can''t seem to access my email, can you help me troubleshoot?', 'ISSUED', 'HIGH', '2022-03-06 23:00:00', '2022-03-06 23:00:00');
                ";
           
            _connection.Execute(sqlInsertData);
            
            _connection.Close();
            
          


        }
  
        private void dispose()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                // then run the test db schema
            }

            string cleanUpSQL = @"DELETE FROM tickets";
            _connection.ExecuteAsync(cleanUpSQL);
            _connection.Close();
     

        }

        [Fact]
        public async void getTicketById()
        {
            // arrange
            _ticketRepository = new TicketRepository(_connectionFactory);

            // Act
            var actualTicket = await _ticketRepository.getById(1);


            // assert
            Assert.NotNull(actualTicket);
            Assert.Equal(1, actualTicket.id);
            dispose();
        }

        
        [Fact]
        public async void getAllTickets()
        {
            _ticketRepository = new TicketRepository(_connectionFactory);

            List<Ticket> actualTickets =  await _ticketRepository.getAll();
          
            Assert.NotNull(actualTickets);
            dispose();

        }
       
        [Fact]
        public async void addNewTicket()
        {
            Ticket newTicketEntry = DataGenerator.sampleExtraTicket;
            _ticketRepository = new TicketRepository(_connectionFactory);
            List<Ticket> actualTickets = await _ticketRepository.getAll();
            Debug.WriteLine(actualTickets.Count);
            bool addTicketRes = await _ticketRepository.addEntry(newTicketEntry);
            actualTickets = await _ticketRepository.getAll();
            Debug.WriteLine(actualTickets.Count);
            Assert.True(addTicketRes);
            
            var lastTicket = actualTickets.Last();
            Debug.WriteLine(lastTicket.id);

            Assert.Equal(newTicketEntry.title, lastTicket.title);
            Assert.Equal(newTicketEntry.requestorId, lastTicket.requestorId);
            Assert.Equal(newTicketEntry.assigneeId, lastTicket.assigneeId);
            Assert.Equal(newTicketEntry.status, lastTicket.status);
            Assert.Equal(newTicketEntry.creationDate.Value.Date, lastTicket.creationDate.Value.Date);

            dispose();
        }
     

        [Fact]
        public async void updateExistingTicket()
        {
            
            Ticket newTicketEntry = DataGenerator.sampleExtraTicket;
            _ticketRepository = new TicketRepository(_connectionFactory);

            List<Ticket> currentExistingTickets =  await _ticketRepository.getAll();
          
            Ticket randomExistingTicket = currentExistingTickets[new Random().Next(0, currentExistingTickets.Count)];
            
            //bool addTicketRes = await _ticketRepository.addEntry(randomExistingTicket);
            
            randomExistingTicket.details = newTicketEntry.details + "Additional Info";
            randomExistingTicket.status = Status.RESOLVED;
            bool updateTicketRes = await _ticketRepository.updateEntry(randomExistingTicket);
            Assert.True(updateTicketRes);
            Ticket updatedTicket = await _ticketRepository.getById(randomExistingTicket.id);
            Assert.Equal(updatedTicket.id, randomExistingTicket.id);
            Assert.Equal(updatedTicket.details, randomExistingTicket.details);
            Assert.Equal(updatedTicket.status, randomExistingTicket.status);

          
            dispose();


        }
   

    }

}

    