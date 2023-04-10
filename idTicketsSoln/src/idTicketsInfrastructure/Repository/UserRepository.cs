using Dapper;
using idTicketsInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Repository
{
   
    public class UserRepository: IRepository<User>
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
                connection.Open();
                const string sql = @"INSERT INTO users (firstName, lastName, email, creationDate, updateDate, isITStaff, isSupervisor, departmentId)
                         VALUES (@firstname, @lastName, @email, @creationDate, @updateDate, @isITStaff, @isSupervisor, @departmentId)
                         RETURNING id, firstName, lastName, email, creationDate, updateDate, isITStaff, isSupervisor, departmentId";
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
           
        }

        public async Task<bool> deleteEntry(User item)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                const string sql = @"DELETE FROM users WHERE id = @id";
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
        }

        public async Task<List<User>> getAll()
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                List<User> usersItems = (List<User>)await connection.QueryAsync<User>(@"SELECT * FROM users");
                //ticketItem.ticketStatus = Enum.Parse<Status>(ticketItem.ticketStatus.ToString(), true);
                return usersItems;
            }
        }

        public async Task<User> getById(int id)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                User userInfo = await connection.QueryFirstOrDefaultAsync<User>(@"SELECT 
                    id, firstName, lastName, email, creationDate, updateDate, isITStaff, isSupervisor, departmentId FROM users WHERE id = @id", new { id });
                return userInfo;
            }
        }

        public async Task<bool> updateEntry(User item)
        {

            using (var connection = _dbConnectionFactory.GetConnection())
            {
                const string sql = @"UPDATE users SET  firstName = @firstName, 
                        lastName = @lastName, email = @email, updateDate = @updateDate,
                        isITStaff = @isITStaff, isSupervisor = @isSupervisor,
                        departmentId = @departmentId
                        WHERE id = @id";
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
        }
    }
}
