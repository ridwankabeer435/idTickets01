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
    [Collection("DbTestCollection")]
    public class TicketRepositoryTest
    {
        private TicketRepository? _ticketRepository;
        private readonly IDbConnectionFactory _connectionFactory;

        private readonly IDbConnection _connection;
        private readonly TestDbFixture _fixture;

        public TicketRepositoryTest(TestDbFixture fixture)
        {

            // Create a connection to the test database
            _fixture  = fixture;
            _connectionFactory = _fixture.DbConnectionFactory;
            _connection = _connectionFactory.GetConnection();
            
            
        }

    
        private void dispose()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                // then run the test db schema
            }

            string cleanUpSQL = @"DELETE FROM departments, users, tickets, comments;
                    DROP TABLE IF EXISTS tickets, comments, users, departments;";
            _connection.ExecuteAsync(cleanUpSQL);
            //_connection.Close();
        }
     


        [Fact]
        public async void getTicketById()
        {
            // arrange
            _ticketRepository = new TicketRepository(_fixture.DbConnectionFactory);
            // determine a random id
            
            // Act
            var actualTicket = await _ticketRepository.getById(5);


            // assert
            Assert.NotNull(actualTicket);
            Assert.Equal(5, actualTicket.id);
           
            dispose();
        }

        
        [Fact]
        public async void getAllTickets()
        {
            _ticketRepository = new TicketRepository(_connectionFactory);

            List<Ticket> actualTickets =  await _ticketRepository.getAll();
          
            Assert.NotNull(actualTickets);
            //Dispose();
            dispose();

        }
       
        [Fact]
        public async void addNewTicket()
        {
            Ticket newTicketEntry = _fixture.DataGenerator.sampleExtraTicket;
            _ticketRepository = new TicketRepository(_connectionFactory);
            List<Ticket> actualTickets = await _ticketRepository.getAll();

            bool addTicketRes = await _ticketRepository.addEntry(newTicketEntry);
            actualTickets = await _ticketRepository.getAll();
            
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
            
            Ticket newTicketEntry = _fixture.   DataGenerator.sampleExtraTicket;
            _ticketRepository = new TicketRepository(_connectionFactory);
            List<Ticket> currentExistingTickets =  await _ticketRepository.getAll();
            Ticket randomExistingTicket = currentExistingTickets[new Random().Next(0, currentExistingTickets.Count)];

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


        [Fact]
        public async void deleteExistingTicket()
        {
            _ticketRepository = new TicketRepository(_connectionFactory);
            List<Ticket> currentExistingTicktets = await _ticketRepository.getAll();
            Ticket ticketToDelete = currentExistingTicktets.First();
            // let's try removing the first entry
            bool successfulDeletion = await _ticketRepository.deleteEntry(ticketToDelete);
            Assert.True(successfulDeletion);

            Ticket phantomTicketEntry = await _ticketRepository.getById(ticketToDelete.id);
            // then try to look for the deleted item
            Assert.Null(phantomTicketEntry);
            dispose();

        }

        [Fact]
        public async void getTicketWithImportantUsersAndComments()
        {
            // arrange
            _ticketRepository = new TicketRepository(_connectionFactory);
            
            // Act
            var actualTicket = await _ticketRepository.getByIdWithComments(5);
                            
            // assert
            Assert.NotNull(actualTicket);
            Assert.NotNull(actualTicket.requestorInfo);
            if(actualTicket.assigneeId > 0)
            {
                Assert.NotNull(actualTicket.assigneeInfo);
            }
            // now comments may or may not be null
            dispose();


        }


        [Fact]
        public async void getTicketWithCommentNoRequestorOrAssigneeNeeded()
        {
            // arrange

            // add a comment with corresponding tickek id
            List<Ticket> currentExistingTickets = _fixture.DataGenerator.sampleTickets;
            int randomTicketId = 15;
            Comment commentToAdd = _fixture.DataGenerator.sampleExtraComment;
            commentToAdd.ticketId = randomTicketId;
            CommentRepository _commentsRepository = new CommentRepository(_connectionFactory);
            bool commentAddSuccess = await _commentsRepository.addEntry(commentToAdd);

            _ticketRepository = new TicketRepository(_connectionFactory);

            // act
            if (commentAddSuccess)
            {
                // get the ticket item and check if the comment list is not null
                Ticket testTicketItem = await _ticketRepository.getByIdWithComments(randomTicketId);
                Assert.NotNull(testTicketItem.comments);
                Assert.NotEmpty(testTicketItem.comments);
                Assert.True(testTicketItem.comments.Last().ticketId == commentToAdd.ticketId);
                Assert.True(testTicketItem.comments.Last().textContent == commentToAdd.textContent);

            }
            dispose();
            

        }

        /*
         public void Dispose()
         {
             // clean up all of the test tables cases
             if (_connection.State != ConnectionState.Open)
             {
                 //_connection = DbConnectionFactory.GetConnection();
                 _connection.Open();

             }
             string cleanupQuery = @"DELETE FROM departments, users, tickets, comments;
                     DROP TABLE IF EXISTS tickets, comments, users, departments;
                     ";


             _connection.ExecuteAsync(cleanupQuery);
             _connection.Close();
         }
        */
    }

}

    