using idTicketsInfrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Repository
{
    public class UsersRepository : IRepository<User>
    {
        public TicketsDbContext _ticketsDbContext { get; set; }

        public DbSet<User> _entitySet { get; set; }

        public UsersRepository(TicketsDbContext dbCtx)
        {
            this._ticketsDbContext =  (dbCtx == null) ? new TicketsDbContext() : dbCtx;
            _entitySet = _ticketsDbContext.users;
        }

        public bool addEntry(User item)
        {
            try
            {
                if (item == null)
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

        public bool deleteEntry(User item)
        {
            try
            {
                // maybe check for existence
                bool ticketExist = _entitySet.Where(t => t.userId == item.userId).Any();
                if (ticketExist == false)
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

        public List<User> getAll()
        {
            return _entitySet.ToList();

        }

        // this will have a different implementation

        // do not return a default user
        public User getById(int id)
        {
            throw new NotImplementedException();
        }

        public bool updateEntry(User item)
        {
            try
            {
                bool userExist = _entitySet.Where(t => t.userId == item.userId).Any();
                if (userExist)
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
