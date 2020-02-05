using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCollectGateway.Common;

namespace TransCollectGateway.Infrastructure
{
    public class TransLogXMLParser : ITransactionParser
    {
        public string GetSupportedFileType()
        {
            return "XML";
        }

        public IEnumerable<string> GetTransactions(Stream transData)
        {
            throw new NotImplementedException();
        }

        public Transaction ParseTransaction(string transactionData)
        {
            throw new NotImplementedException();
        }
    }
}
