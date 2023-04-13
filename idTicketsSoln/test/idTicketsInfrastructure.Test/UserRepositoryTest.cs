using Dapper;
using idTicketsInfrastructure.Models;
using idTicketsInfrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Test
{
    [Collection("DbTestCollection")]
    public class UserRepositoryTest
    {
        private UserRepository? _userRepository;
        private readonly TestDbFixture _fixture;

        private readonly IDbConnectionFactory _connectionFactory;

        private readonly IDbConnection _connection;

        public UserRepositoryTest(TestDbFixture fixture) {

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

            string cleanUpSQL = @"DELETE FROM departments, users, tickets, comments;
                    DROP TABLE IF EXISTS tickets, comments, users, departments;";
            _connection.ExecuteAsync(cleanUpSQL);
            //_connection.Close();
        }


        [Fact]
        public async void getValidUserById()
        {
            // arrange     
            _userRepository = new UserRepository(_connectionFactory);

            // Act
            var actualUser = await _userRepository.getById(5);

            Assert.NotNull(actualUser);
            Assert.Equal(5, actualUser.id);
            dispose();
        }

        [Fact]
        public async void addNewValidUser()
        {
            User newUser = DataGenerator.sampleExtraUser;
            _userRepository = new UserRepository(_connectionFactory);

            bool insertionResult =  await _userRepository.addEntry(newUser);
            Assert.True(insertionResult);

            // now try to get the last item of the updated list
            List<User> actualUsers = await _userRepository.getAll();
            User currentLastUser = actualUsers.Last();

            Assert.Equal(newUser.firstName, currentLastUser.firstName);
            Assert.Equal(newUser.lastName, newUser.lastName);
            Assert.Equal(newUser.email, newUser.email);
            Assert.Equal(newUser.creationDate.Date, newUser.creationDate.Date);
            Assert.Equal(newUser.isITStaff, newUser.isITStaff);
            Assert.Equal(newUser.isSupervisor, newUser.isSupervisor);
            Assert.Equal(newUser.departmentId, newUser.departmentId);

            dispose();

        }

        [Fact]
        public async void updateExistingUser()
        {
            _userRepository = new UserRepository(_connectionFactory);
            List<User> currentExistingUsers = await _userRepository.getAll();
            User randomExistingUser = currentExistingUsers[new Random().Next(0, currentExistingUsers.Count)];

            randomExistingUser.isSupervisor = true;
            randomExistingUser.departmentId = 4;
            randomExistingUser.firstName = "Mikhail";

            bool succesfulUpdate = await _userRepository.updateEntry(randomExistingUser);
            Assert.True(succesfulUpdate);

            User theSameRandomUser = await _userRepository.getById(randomExistingUser.id);
            Assert.NotNull(theSameRandomUser);
            Assert.Equal(randomExistingUser.id, theSameRandomUser.id);
            Assert.Equal(randomExistingUser.firstName, theSameRandomUser.firstName);
            Assert.Equal(randomExistingUser.departmentId, theSameRandomUser.departmentId);
            Assert.Equal(randomExistingUser.isSupervisor, theSameRandomUser.isSupervisor);


            dispose();

        }

        [Fact]
        public async void deleteExistingUser()
        {
            _userRepository = new UserRepository(_connectionFactory);
            List<User> currentExistingUsers = await _userRepository.getAll();
            User userToDelete = currentExistingUsers.First();
            // let's try removing the first entry
            bool successfulDeletion = await _userRepository.deleteEntry(userToDelete);
            Assert.True(successfulDeletion);

            User phantomUserEntry = await _userRepository.getById(userToDelete.id);
            // then try to look for the deleted item
            Assert.Null(phantomUserEntry);
            dispose();

        }


    }
}
