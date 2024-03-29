﻿using Dapper;
using idTicketsInfrastructure.Models;
using idTicketsInfrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Test
{
    [Collection("DbTestCollection")]
    public class CommentsRepositoryTest
    {
        private CommentRepository? _commentsRepository;
        private readonly TestDbFixture _fixture;

        private readonly IDbConnectionFactory _connectionFactory;

        private readonly IDbConnection _connection;

        public CommentsRepositoryTest(TestDbFixture fixture)
        {
            // Create a connection to the test database
            _fixture = fixture;

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

            string cleanUpSQL = @"DELETE FROM users, departments, tickets, comments;
                    DROP TABLE IF EXISTS tickets, comments, users, departments;";
            _connection.ExecuteAsync(cleanUpSQL);
            //_connection.Close();

        }

        [Fact]
        public async void getExistingCommentById()
        {
            // arrange     
            _commentsRepository = new CommentRepository(_connectionFactory);

            // Act
            var actualComment = await _commentsRepository.getById(4);

            Assert.NotNull(actualComment);
            Assert.Equal(4, actualComment.id);
            dispose();
        }


        [Fact]
        public async void addNewComment()
        {
            Comment newComment = _fixture.DataGenerator.sampleExtraComment;
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
            List<Comment> currentExistingComments = await _commentsRepository.getAll();
            Comment randomExistingComment = currentExistingComments[new Random().Next(0, currentExistingComments.Count)];

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

        [Fact]
        public async void getCommenterInfoIfExists()
        {
            _commentsRepository = new CommentRepository(_connectionFactory);
            // grab a random comments
            List<Comment> existingComments = await _commentsRepository.getAll();
            Comment randomExistingComment = existingComments[new Random().Next(0, existingComments.Count)];

            // act
            Comment sameCommentWithUserInfo = await _commentsRepository.getByIdWithUserInfo(randomExistingComment.id);
            
            // assert
            Assert.True(sameCommentWithUserInfo.userId > 0);
            Assert.NotNull(sameCommentWithUserInfo.userInfo);
            Assert.Equal(randomExistingComment.userId, sameCommentWithUserInfo.userInfo.id);
            

            dispose();

        }

        [Fact]

        public async void getCommentsByExistingTicketId()
        {
            // now a ticket entry may not even have any comments -- that is alright
            // so let's pick a ticket that has comments
            // arrange
            _commentsRepository = new CommentRepository(_connectionFactory);
            Comment sampleComment = await _commentsRepository.getById(10);

            Assert.NotNull(sampleComment);

            List<Comment>  commentsByExistingTicket = await _commentsRepository.getByTicketId(sampleComment.ticketId);
            Assert.NotEmpty(commentsByExistingTicket);
            // now let's pick a random comment


        }

    }
}
