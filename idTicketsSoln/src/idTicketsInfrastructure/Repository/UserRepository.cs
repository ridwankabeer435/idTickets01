using Dapper;
using idTicketsInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Repository
{
   
    class UserRepository: IRepository<User>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<bool> addEntry(User item)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                const string sql = @"INSERT INTO users (first_name, last_name, email, created_at, updated_at, is_it, is_supervisor, department_id)
                         VALUES (@firstName, @lastName, @email, @dateCreated, @dateUpdated, @isITPersonnel, @isSupervisor, @departmentId)
                         RETURNING id";
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
           
        }

        public async Task<bool> deleteEntry(User item)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                const string sql = @"DELETE FROM users WHERE id = @userId";
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
        }

        public async Task<User> getById(int id)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                User userInfo = await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM user WHERE id = @id", new { id });
                return userInfo;
            }
        }

        public async Task<bool> updateEntry(User item)
        {

            using (var connection = _dbConnectionFactory.GetConnection())
            {
                const string sql = @"UPDATE users SET  first_name = @firstName, 
                        last_name = @lastName, email = @email, updated_at = @dateUpdated,
                        is_it = @isITPersonnel, is_supervisor = @isSupervisor,
                        department_id = @departmentId
                        WHERE id = @userId";
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
        }
    }
}
