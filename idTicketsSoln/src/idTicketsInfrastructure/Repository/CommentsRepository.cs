using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Repository
{
    public class CommentsRepository 
    {
        public Task<bool> addEntryPAsync<T>(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> deleteEntryAsync<T>(T item)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> getAllAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task<T> getByIdAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> updateEntryAsync<T>(T item)
        {
            throw new NotImplementedException();
        }
    }
}
