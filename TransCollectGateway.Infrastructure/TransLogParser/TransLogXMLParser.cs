using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCollectGateway.Common;

namespace TransCollectGateway.Infrastructure.TransLogParser
{
    public class TransLogXMLParser : ITransactionParser
    {
        public string GetSupportedFileType()
        {
            return "XML";
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
