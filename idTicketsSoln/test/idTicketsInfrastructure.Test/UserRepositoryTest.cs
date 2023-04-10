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
    public class UserRepositoryTest
    {
        private UserRepository _userRepository;
        private readonly IDbConnectionFactory _connectionFactory;

        private readonly IDbConnection _connection;

        public UserRepositoryTest() {

            _connectionFactory = new TestDbConnectionFactory();
            _connection = _connectionFactory.GetConnection();
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                // then run the test db schema
            }

            string sqlCreateUsersTable = @"
                
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
                ";
            _connection.Execute(sqlCreateUsersTable);

            string dataInsertionQueries = @"
                INSERT INTO departments (title) 
                VALUES 
                    ('Sales'),
                    ('Marketing'),
                    ('Finance'),
                    ('IT'),
                    ('Human Resources');


                INSERT INTO users (firstName, lastName, email, creationDate, updateDate, isITStaff, isSupervisor, departmentId)
                VALUES
                    ('John', 'Doe', 'john.doe@example.com', '2022-03-01 12:00:00', '2022-03-01 12:00:00', true, false, 1),
                    ('Jane', 'Doe', 'jane.doe@example.com', '2022-03-01 13:00:00', '2022-03-01 13:00:00', false, true, 2),
                    ('Sara', 'Smith', 'sara.smith@example.com', '2022-03-02 14:00:00', '2022-03-02 14:00:00', false, false, 1),
                    ('Ahmed', 'Ali', 'ahmed.ali@example.com', '2022-03-02 15:00:00', '2022-03-02 15:00:00', false, false, 3),
                    ('Juan', 'Garcia', 'juan.garcia@example.com', '2022-03-03 16:00:00', '2022-03-03 16:00:00', false, false, 2),
                    ('Marie', 'Dupont', 'marie.dupont@example.com', '2022-03-03 17:00:00', '2022-03-03 17:00:00', false, false, 4),
                    ('Jin-Soo', 'Kim', 'jin-soo.kim@example.com', '2022-03-04 18:00:00', '2022-03-04 18:00:00', false, false, 3),
                    ('Emil', 'Andersson', 'emil.andersson@example.com', '2022-03-04 19:00:00', '2022-03-04 19:00:00', true, false, 1),
                    ('Maria', 'Gonzalez', 'maria.gonzalez@example.com', '2022-03-05 20:00:00', '2022-03-05 20:00:00', false, false, 2),
                    ('Svetlana', 'Ivanova', 'svetlana.ivanova@example.com', '2022-03-05 21:00:00', '2022-03-05 21:00:00', false, false, 4),
                    ('Pablo', 'Rodriguez', 'pablo.rodriguez@example.com', '2022-03-06 22:00:00', '2022-03-06 22:00:00', false, true, 3),
                    ('Hiroshi', 'Yamamoto', 'hiroshi.yamamoto@example.com', '2022-03-06 23:00:00', '2022-03-06 23:00:00', false, false, 1),
                    ('Li', 'Wang', 'li.wang@example.com', '2022-03-07 00:00:00', '2022-03-07 00:00:00', true, false, 2);
                
            ";


            
            _connection.Execute(dataInsertionQueries);
            _connection.Close();
        }

        private void dispose()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                // then run the test db schema
            }

            string cleanUpSQL = @"DELETE FROM users, departments";
            _connection.ExecuteAsync(cleanUpSQL);
            _connection.Close();


        }

        
        [Fact]
        public async void getValidUserById()
        {
            // arrange     
            _userRepository = new UserRepository(_connectionFactory);

            // Act
            var actualUser = await _userRepository.getById(1);

            Assert.NotNull(actualUser);
            Assert.Equal(1, actualUser.id);
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
