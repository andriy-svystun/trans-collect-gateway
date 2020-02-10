using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TransCollectGateway.Common
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Get(int id);
        Task<T> GetAsync(int id);
        IEnumerable<T> Get(Func<T, bool> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
