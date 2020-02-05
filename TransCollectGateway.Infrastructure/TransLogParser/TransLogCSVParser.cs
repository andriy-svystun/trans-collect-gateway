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

        public IEnumerable<string> GetTransactions(Stream transData)
        {
            if (transData == null)
                throw new ArgumentNullException(nameof(transData));

            var res = new List<string>();

            using (var reader = new StreamReader(transData))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    res.Add(line);
                }
            }

            return res;
        }

        public Transaction ParseTransaction(string transactionData)
        {
            throw new NotImplementedException();
        }
    }
}
