using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Repository
{
    // this interface 
    public interface IRepository<T> where T : class
    {
        TicketsDbContext _ticketsDbContext { get; }
        DbSet<T> _entitySet { get; }

        public List<T> getAll();
        public T getById(int id);

        public bool addEntry(T item);

        public bool updateEntry(T item);
        public bool deleteEntry(T item);

    }
}
