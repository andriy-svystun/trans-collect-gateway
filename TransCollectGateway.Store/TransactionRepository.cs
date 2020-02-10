using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TransCollectGateway.Common;

namespace TransCollectGateway.Store
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private readonly EFDBContext _dbContext;

        public TransactionRepository()
        {
            _dbContext = new EFDBContext();
        }

        public void Create(Transaction item)
        {
            _dbContext.Transactions.Add(item);
        }

        public void Delete(int id)
        {
            var item = _dbContext.Transactions.Find(id);

            if (item != null)
            {
                _dbContext.Transactions.Remove(item);
            }
        }

        public Transaction Get(int id)
        {
            return _dbContext.Transactions.Find(id);
        }

        public IEnumerable<Transaction> Get(Func<Transaction, bool> predicate)
        {
            return _dbContext.Transactions.AsNoTracking().Where(predicate).ToList();
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _dbContext.Transactions.AsNoTracking().ToList();
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _dbContext.Transactions.AsNoTracking().ToListAsync();
        }

        public async Task<Transaction> GetAsync(int id)
        {
            return await _dbContext.Transactions.FindAsync(id);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Update(Transaction item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
