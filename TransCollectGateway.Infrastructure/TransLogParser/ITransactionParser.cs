using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCollectGateway.Common;

namespace TransCollectGateway.Infrastructure
{

    public interface ITransactionParser
    {
        string GetSupportedFileType();
        IEnumerable<string> GetTRansactions(System.IO.Stream transData);
        Transaction PasreTransaction(string transactionLine);
    }
}
