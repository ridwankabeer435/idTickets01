using Dapper;
using idTicketsInfrastructure.Models;
using idTicketsInfrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Test
{
    public class CommentsRepositoryTest
    {
        private CommentRepository _commentsRepository;
        private readonly IDbConnectionFactory _connectionFactory;

        private readonly IDbConnection _connection;

        public CommentsRepositoryTest()
        {
            // Create a connection to the test database
            _connectionFactory = new TestDbConnectionFactory();
            _connection = _connectionFactory.GetConnection();
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                // then run the test db schema
            }


            // then create the schema
            string commentsTablesCreationQuery = @"
                 
                DROP TABLE IF EXISTS tickets, users, comments, departments;

                CREATE TABLE IF NOT EXISTS users (
                    id SERIAL PRIMARY KEY,
                    firstName VARCHAR(75) NOT NULL,
                    lastName VARCHAR(50) NOT NULL,
                    email VARCHAR(255) NOT NULL,
                    creationDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    updateDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    isITStaff bool NOT NULL DEFAULT FALSE,
                    isSupervisor bool NOT NULL DEFAULT FALSE,
                    departmentId integer NULL
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

               
            ";
            _connection.Execute(commentsTablesCreationQuery);


            string commentsInsertion = @"
            INSERT INTO comments (userId, ticketId, textContent, creationDate) VALUES
              (1, 1, 'This is a sample comment.', '2022-03-01 12:00:00'),
              (2, 2, 'Thanks for reporting this issue. We will investigate further.', '2022-03-01 12:30:00'),
              (3, 3, 'Please provide more details about the problem you are facing.', '2022-03-01 13:00:00'),
              (4, 4, 'I think I have encountered the same issue as well. Any updates?', '2022-03-02 14:00:00'),
              (5, 5, 'I can confirm that the issue is now resolved. Thanks for your help!', '2022-03-02 15:00:00'),
              (5, 6, 'This is a sample comment.', '2022-03-03 16:00:00'),
              (6, 7, 'We have escalated this issue to our engineering team for further investigation.', '2022-03-03 17:00:00'),
              (8, 8, 'Thanks for your feedback. We will take it into consideration.', '2022-03-04 18:00:00'),
              (11, 9, 'Can you please provide more information about the error message you are seeing?', '2022-03-04 19:00:00'),
              (10, 10, 'I have experienced this issue as well. Can you please provide a workaround?', '2022-03-05 20:00:00'),
              (5, 11, 'We are currently investigating this issue and will provide an update soon.', '2022-03-05 21:00:00'),
              (1, 12, 'This is a sample comment.', '2022-03-06 22:00:00'),
              (1, 13, 'Thanks for letting us know. We will look into this and provide an update shortly.', '2022-03-06 23:00:00'),
              (2, 1, 'Glad to hear that the issue is now resolved. Thanks for your help!', '2022-03-07 00:00:00'),
              (3, 2, 'We have identified the root cause of the problem and will deploy a fix shortly.', '2022-03-08 01:00:00'),
              (3, 3, 'This is a sample comment.', '2022-03-08 02:00:00'),
              (4, 4, 'Thanks for reporting this issue. We will investigate further.', '2022-03-09 03:00:00'),
              (1, 5, 'Please provide more details about the problem you are facing.', '2022-03-09 04:00:00'),
              (2, 6, 'I think I have encountered the same issue as well. Any updates?', '2022-03-10 05:00:00'),
              (10, 7, 'I can confirm that the issue is now resolved. Thanks for your help!', '2022-03-10 06:00:00'),
              (7, 8, 'This is a sample comment.', '2022-03-11 07:00:00');
            
            ";
            _connection.Execute(commentsInsertion);
            _connection.Close();


            
        }

        private void dispose()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                // then run the test db schema
            }

            string cleanUpSQL = @"DELETE FROM users, departments, tickets, comments";
            _connection.ExecuteAsync(cleanUpSQL);
            _connection.Close();

        }

        [Fact]
        public async void getExistingCommentById()
        {
            // arrange     
            _commentsRepository = new CommentRepository(_connectionFactory);

            // Act
            var actualComment = await _commentsRepository.getById(1);

            Assert.NotNull(actualComment);
            Assert.Equal(1, actualComment.id);
            dispose();
        }


        [Fact]
        public async void addNewComment()
        {
            Comment newComment = DataGenerator.sampleExtraComment;
            _commentsRepository = new CommentRepository(_connectionFactory);

            bool insertionResult = await _commentsRepository.addEntry(newComment);
            Assert.True(insertionResult);

            // now try to get the last item of the updated list
            List<Comment> actualComment = await _commentsRepository.getAll();
            Comment currentLastComment = actualComment.Last();

            Assert.Equal(newComment.userId, currentLastComment.userId);
            Assert.Equal(newComment.ticketId, currentLastComment.ticketId);
            Assert.Equal(newComment.textContent, currentLastComment.textContent);
            Assert.Equal(newComment.creationDate.Value.Date, currentLastComment.creationDate.Value.Date);

            dispose();
        }

        [Fact]
        public async void updateExistingComment()
        {
            _commentsRepository = new CommentRepository(_connectionFactory);
            List<Comment> currentExistingUsers = await _commentsRepository.getAll();
            Comment randomExistingComment = currentExistingUsers[new Random().Next(0, currentExistingUsers.Count)];

            randomExistingComment.textContent += @" More content description being added";

            bool succesfulUpdate = await _commentsRepository.updateEntry(randomExistingComment);
            Assert.True(succesfulUpdate);

            Comment theSameRandomComment = await _commentsRepository.getById(randomExistingComment.id);
            Assert.NotNull(theSameRandomComment);
            Assert.Equal(randomExistingComment.id, theSameRandomComment.id);
            Assert.Equal(randomExistingComment.textContent, theSameRandomComment.textContent);
            dispose();

        }

        [Fact]
        public async void removeExistingComment()
        {
            _commentsRepository = new CommentRepository(_connectionFactory);
            List<Comment> currentExistingUsers = await _commentsRepository.getAll();
            Comment commentToDelete = currentExistingUsers.First();
            // let's try removing the first entry
            bool successfulDeletion = await _commentsRepository.deleteEntry(commentToDelete);
            Assert.True(successfulDeletion);

            Comment phantomCommentEntry = await _commentsRepository.getById(commentToDelete.id);
            // then try to look for the deleted item
            Assert.Null(phantomCommentEntry);
            dispose();
        }

    }
}
