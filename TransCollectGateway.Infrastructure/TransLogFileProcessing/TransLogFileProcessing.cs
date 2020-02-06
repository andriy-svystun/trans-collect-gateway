using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCollectGateway.Common;

namespace TransCollectGateway.Infrastructure
{
    public class TransLogFileProcessing : ITransLogFileProcessing
    {
        private ITransactionParser _transactionParser;

        public IEnumerable<Transaction> ProcessTransactionFile(Stream fileStream)
        {
            if (_transactionParser == null)
                throw new ApplicationException("Setup parser first");

            var result = new List<Transaction>();

            var transactions = _transactionParser.GetTransactions(fileStream);

            foreach(var trans in transactions)
            {
                result.Add(_transactionParser.ParseTransaction(trans));
            }

            return result;
        }

        public void SetParser(ITransactionParser parser)
        {
            _transactionParser = parser;
        }
    }
}
