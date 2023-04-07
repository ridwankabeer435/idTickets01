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
                const string sql = @"INSERT INTO comments (text_content, ticket_Id, user_Id, created_at)
                         VALUES (@details, @ticketId, @userId, @postingDate)
                         RETURNING id";
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
        }

        // a user may choose to remove a comment on a ticket
        public async Task<bool> deleteEntry(Comment item)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                const string sql = @"DELETE FROM comments WHERE id = @commentId";
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
        }

        public async Task<Comment> getById(int itemId)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                Comment commenttItem = await connection.QueryFirstOrDefaultAsync<Comment>("SELECT * FROM comments WHERE id = @itemId", new { itemId });
                return commenttItem;
            }
        }

        // needed for making edits by the user
        public async Task<bool> updateEntry(Comment item)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                const string sql = @"UPDATE comments SET text_content = @details, 
                                                updated_at = @ticketUpdateDate WHERE id = @commentId"; ;
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
        }

        public async Task<List<Comment>> getCommentsByTickets(long ticketId) {

            using (var connection = _dbConnectionFactory.GetConnection())
            {
                List<Comment> ticketComments = (List<Comment>) await connection.QueryAsync<Comment>("SELECT * FROM comments WHERE id = @ticketId", new { ticketId });
                return ticketComments;
            }
            
        }
        // additional methods include obtaining comments by ticket id
    }
}
