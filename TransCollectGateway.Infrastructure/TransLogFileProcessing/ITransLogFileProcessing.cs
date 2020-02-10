using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCollectGateway.Common;

namespace TransCollectGateway.Infrastructure
{
    public interface ITransLogFileProcessing
    {
        void SetParser(ITransactionParser parser);
        Task<IEnumerable<Transaction>> ProcessTransactionFile(System.IO.Stream fileStream);
    }
}
