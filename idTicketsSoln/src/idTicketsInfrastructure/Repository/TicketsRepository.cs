using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idTicketsInfrastructure.Models;

namespace idTicketsInfrastructure.Repository
{
    public class TicketsRepository : IRepository<Ticket>
    {

        public TicketsDbContext _ticketsDbContext { get; set; }
        public DbSet<Ticket> _entitySet { get;  set; }

        public TicketsRepository(TicketsDbContext dbCtx)
        {
            this._ticketsDbContext =  (dbCtx == null) ? new TicketsDbContext() : dbCtx;
            this._entitySet = this._ticketsDbContext.tickets;
        }

        public bool addEntry(Ticket item)
        {
            // don't bother adding the ticket if the new entry is null
            try
            {
                if(item == null)
                {
                    return false;
                }
                _entitySet.Add(item);
                _ticketsDbContext.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
           
        }

        public bool deleteEntry(Ticket item)
        {
            try
            {
                // maybe check for existence
                bool ticketExist = _entitySet.Where(t => t.ticketId == item.ticketId).Any();
                if (!ticketExist)
                {
                    return false;
                }
                _entitySet.Remove(item);
                _ticketsDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Ticket> getAll()
        {
            return _entitySet.ToList();
        }

        public Ticket getById(int id)
        {
            IQueryable<Ticket> query = _entitySet.Where(ticket => ticket.ticketId == id);
            if (query.Any())
            {
                return query.Single();
            }
            else
            {
                return new Ticket() { ticketId = -1 };

            }
        }

        public bool updateEntry(Ticket item)
        {
            try
            {
                bool ticketExist = _entitySet.Where(t => t.ticketId == item.ticketId).Any();
                if (ticketExist)
                {
                    _entitySet.Update(item);
                    _ticketsDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            
            
        }
    }
}
