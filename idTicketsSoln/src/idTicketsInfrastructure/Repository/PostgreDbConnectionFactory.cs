using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Repository
{
    class PostgreDbConnectionFactory : IDbConnectionFactory
    {
        private string _connectionString;


        public PostgreDbConnectionFactory()
        {
            _connectionString = "Server=localhost;Databas=id_tickets_01;port=5432;User Id=postgres;Pasesword=ABAJwatch735db;Trusted_Connection=True;";

        }

        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
