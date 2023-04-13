using Dapper;
using idTicketsInfrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Test
{

    [CollectionDefinition("DbTestCollection")]
    public class DbTestFixtureCollection : ICollectionFixture<TestDbFixture>
    {
    }

    /*
     * Static class to initialize the test database
     * 
     * **/
    public class TestDbFixture 
    {
        // have the  queries for database creation and sample data insertion

        public IDbConnectionFactory DbConnectionFactory { get; }
        public  DataGenerator DataGenerator { get; }
        private readonly IDbConnection _connection;


        public TestDbFixture() {

            DbConnectionFactory = new TestDbConnectionFactory();
            DataGenerator = new DataGenerator();
            _connection = DbConnectionFactory.GetConnection();
            initializeTestDb();
        }

        private void initializeTestDb()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();

            }

            // Initialize the test database schema
            // test database

            var sqlCreateTables = @"
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
                    departmentId integer
                );


                CREATE TABLE IF NOT EXISTS departments(
                    id SERIAL PRIMARY KEY,
                    title VARCHAR(40) NOT NULL
    
                );

                CREATE TABLE IF NOT EXISTS tickets(
                    id SERIAL PRIMARY KEY,
                    requestorId bigint not null,
                    assigneeId bigint,
                    title VARCHAR(255),
                    details VARCHAR(500),
                    status VARCHAR(15),
                    priority VARCHAR(15),
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

            // and then insert some dummy data
            string departmentInsert = @"INSERT INTO departments (title) 
                VALUES 
                    ('Sales'),
                    ('Marketing'),
                    ('Finance'),
                    ('IT'),
                    ('Human Resources');";

            string sampleTickeks = @"INSERT INTO tickets (title, details, requestorId, assigneeId, status, 
                                priority, creationDate, updateDate) 
                    VALUES (@title, @details, @requestorId, @assigneeId, @status, @priority, @creationDate, @updateDate)";
            string sampleUsers = @"INSERT INTO users (firstName, lastName, email, creationDate, updateDate, isITStaff, isSupervisor, departmentId)
                         VALUES (@firstname, @lastName, @email, @creationDate, @updateDate, @isITStaff, @isSupervisor, @departmentId)";
            string sampleComments = @"INSERT INTO comments (userId, ticketId, textContent, creationDate)
                         VALUES (@userId, @ticketId, @textContent, @creationDate)";

            // bulk insert
            _connection.Execute(departmentInsert);
            _connection.Execute(sampleUsers, DataGenerator.sampleUsers);
            _connection.Execute(sampleTickeks, DataGenerator.sampleTickets);
            _connection.Execute(sampleComments, DataGenerator.sampleComments);
            //_connection.Close();

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
