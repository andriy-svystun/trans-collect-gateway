using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCollectGateway.Common;

namespace TransCollectGateway.Infrastructure.TransLogParser
{
    public interface ITransLogFileProcessing
    {
        void SetParser(ITransactionParser parser);
        IEnumerable<Transaction> ProcessTansactionFile(System.IO.Stream fileStream);
    }
}
