using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course.Data
{
    public interface IStore<TEntity> where TEntity : class
    {
        Task<int> AddNewRequestAsync(TEntity request);
        Task<IQueryable<TEntity>> GetAllRequests();
        Task RemoveRequest(string requestId);
    }
}
