using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCollectGateway.Common;

namespace TransCollectGateway.Infrastructure.TransLogParser
{
    public class TransLogCVSParser : ITransactionParser
    {
        public string GetSupportedFileType()
        {
            return "CSV";
        }

        public IEnumerable<string> GetTRansactions(Stream transData)
        {
            throw new NotImplementedException();
        }

        public Transaction PasreTransaction(string transactionData)
        {
            throw new NotImplementedException();
        }
    }
}
