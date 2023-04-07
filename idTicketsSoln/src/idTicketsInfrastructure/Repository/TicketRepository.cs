using idTicketsInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Net.Sockets;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Drawing;
using System.Diagnostics;

namespace idTicketsInfrastructure.Repository
{   
    public class TicketRepository : IRepository<Ticket>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        

        public TicketRepository(IDbConnectionFactory dbConnectionFactory )
        {
            _dbConnectionFactory = dbConnectionFactory;
            
        }

        // on adding a new ticket, set the status to ISSUED
        public async Task<bool> addEntry(Ticket item)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();

                const string sql = @"INSERT INTO tickets ( requestorId, assigneeId, title, details, status, priority, creationDate, updateDate)
                         VALUES (@requestorId, @assigneeId, @title, @details, @status, @priority, @creationDate, @updateDate)
                         RETURNING id, title, details, requestorId, assigneeId, status, priority, creationDate, updateDate
                         ";
                var rowsAffected = 0;
               
               
                rowsAffected = await connection.ExecuteAsync(sql, item);

                
                return rowsAffected > 0;
            }
         
        }

        public async Task<bool> deleteEntry(Ticket item)
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                const string sql = @"DELETE FROM tickets WHERE id = @id";
                var rowsAffected = await connection.ExecuteAsync(sql, item);
                return rowsAffected > 0;
            }

            
        }

        // need to get all tickets
        public async Task<List<Ticket>> getAll()
        {
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                List<Ticket> ticketItems = (List<Ticket>)await connection.QueryAsync<Ticket>(@"SELECT * FROM tickets");
                //ticketItem.ticketStatus = Enum.Parse<Status>(ticketItem.ticketStatus.ToString(), true);
                return ticketItems;
            }
        }

        public async Task<Ticket> getById(int id)
        {
            Debug.WriteLine(id);
            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                Ticket ticketItem = await connection.QueryFirstOrDefaultAsync<Ticket>(@"SELECT id, title, details, requestorId, assigneeId, 
                                        status, priority, creationDate, updateDate FROM tickets WHERE id = @id", new { id });
                //ticketItem.ticketStatus = Enum.Parse<Status>(ticketItem.ticketStatus.ToString(), true);
                return ticketItem;
            }

        }

        public async Task<bool> updateEntry(Ticket item)
        {

            using (var connection = _dbConnectionFactory.GetConnection())
            {
                connection.Open();
                const string sql = @"UPDATE tickets SET title = @title, 
                details = @details, priority = @priority, status = @status, assigneeId = @assigneeId, 
                updateDate = @updateDate WHERE id = @id";
                var rowsAffected = await connection.ExecuteAsync(sql, new { id = item.id, title = item.title, details = item.details, priority = item.priority, status = item.status, assigneeId = item.assigneeId, updateDate = DateTime.Now });
                return rowsAffected > 0;
            }
        }
    }
}
