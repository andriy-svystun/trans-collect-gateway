using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCollectGateway.Common;

namespace TransCollectGateway.Infrastructure
{
    public class TransLogCSVParser : ITransactionParser
    {
        public string GetSupportedFileType()
        {
            return "CSV";
        }

        public IEnumerable<string> GetTRansactions(Stream transData)
        {
            IEnumerable<string> res = new List<string>();

            throw new NotImplementedException();
        }

        public Transaction ParseTransaction(string transactionData)
        {
            throw new NotImplementedException();
        }
    }
}
