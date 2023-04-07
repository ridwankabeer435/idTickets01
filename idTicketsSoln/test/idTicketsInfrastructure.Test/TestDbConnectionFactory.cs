using idTicketsInfrastructure.Repository;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Test
{
    internal class TestDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public TestDbConnectionFactory()
        {
            _connectionString= "Server=localhost;Database=id_tickets_01_test;port=5432;User Id=postgres;Password=ABAJwatch735db;Pooling=true;MinPoolSize=1;MaxPoolSize=20;"; ;
        }

        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
