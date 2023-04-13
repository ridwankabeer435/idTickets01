using Dapper;
using idTicketsInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Repository
{
    public class CommentRepository : IRepository<Comment>
    {

        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CommentRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<bool> addEntry(Comment item)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                const string sql = @"INSERT INTO comments (userId, ticketId, textContent, creationDate)
                         VALUES (@userId, @ticketId, @textContent, @creationDate)
                         RETURNING id, userId, ticketId, textContent, creationDate";
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
        }

        // a user may choose to remove a comment on a ticket
        public async Task<bool> deleteEntry(Comment item)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                const string sql = @"DELETE FROM comments WHERE id = @id";
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
        }

        public async Task<List<Comment>> getAll()
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                List<Comment> commentsList = (List<Comment>)await connection.QueryAsync<Comment>(@"SELECT * FROM comments");
                //ticketItem.ticketStatus = Enum.Parse<Status>(ticketItem.ticketStatus.ToString(), true);
                return commentsList;
            }
        }

        public async Task<Comment> getById(int id)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                Comment commenttItem = await connection.QueryFirstOrDefaultAsync<Comment>(@"SELECT 
                       id, userId, ticketId, textContent, creationDate FROM comments WHERE id = @id", new { id });
                return commenttItem;
            }
        }

        public async Task<Comment> getByIdWithUserInfo(int id)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                Comment commentItem = await connection.QueryFirstOrDefaultAsync<Comment>(@"SELECT 
                       id, userId, ticketId, textContent, creationDate FROM comments WHERE id = @id", new { id });

                if(commentItem != null && commentItem.id > 0 && commentItem.userId > 0)
                {
                    commentItem.userInfo = await connection.QueryFirstOrDefaultAsync<User>(@"SELECT 
                    id, firstName, lastName, email, creationDate, updateDate, isITStaff, isSupervisor, departmentId FROM users WHERE id = @id", new { id = commentItem.userId});
                    return commentItem;
                }

                return commentItem;
            }
        }


        // needed for making edits by the user
        public async Task<bool> updateEntry(Comment item)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                const string sql = @"UPDATE comments SET textContent = @textContent
                                               WHERE id = @id"; ;
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
        }

        public async Task<List<Comment>> getByTicketId(long ticketId) {

            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                List<Comment> ticketComments = (List<Comment>) await connection.QueryAsync<Comment>("SELECT * FROM comments WHERE id = @id", new { id = ticketId });
                return ticketComments;
            }
            
        }
    
    }
}
