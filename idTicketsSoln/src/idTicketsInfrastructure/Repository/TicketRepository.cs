using idTicketsInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Net.Sockets;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace idTicketsInfrastructure.Repository
{
    class TicketRepository : IRepository<Ticket>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public TicketRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        // on adding a new ticket, set the status to ISSUED
        public async Task<bool> addEntry(Ticket item)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                const string sql = @"INSERT INTO tickets (title, description, priority, requestor_id, assignee_id,status, created_at, updated_at)
                         VALUES (@title, @details, @requestor_id, @ticketStatus, @ticketPriority, @ticketIssueDate, @ticketUpdateDate)
                         RETURNING id";
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
         
        }

        public async Task<bool> deleteEntry(Ticket item)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                const string sql = @"DELETE FROM tickets WHERE id = @ticketId";
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }

            
        }

        // need to get all tickets
        public async Task<List<Ticket>> getAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Ticket> getById(int id)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                Ticket ticketItem = await connection.QueryFirstOrDefaultAsync<Ticket>("SELECT * FROM tickets WHERE id = @id", new { id });
                return ticketItem;
            }

        }

        public async Task<bool> updateEntry(Ticket item)
        {

            using (var connection = _dbConnectionFactory.GetConnection())
            {
                const string sql = @"UPDATE tickets SET title = @title, 
                        description = @details, priority = @ticketPriority, status = @ticketStatus, assignee_id = @assigneeId, 
                        updated_at = @ticketUpdateDate WHERE id = @ticketId"; ;
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }
        }
    }
}
