using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TransCollectGateway.Common;

namespace TransCollectGateway.Store
{
    public class EFDBContext : DbContext
    {
        public EFDBContext() : base("TCGDatabase")
        {
            Database.SetInitializer(new MainContextInitializer());
        }
        public DbSet<Transaction> Transactions { get; set; }
    }

    class MainContextInitializer : CreateDatabaseIfNotExists<EFDBContext>
    {

    }
}
