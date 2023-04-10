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


        //public Task<List<T>> getAll();
        public Task<T> getById(int itemId);

        public Task<bool> addEntry(T item);

        public Task<bool> updateEntry(T item);
        public Task<bool> deleteEntry(T item);

    }
}
