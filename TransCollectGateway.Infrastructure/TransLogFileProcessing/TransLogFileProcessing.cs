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

            try
            {
                var transactions = _transactionParser.GetTransactions(fileStream);

                foreach (var trans in transactions)
                {
                    result.Add(_transactionParser.ParseTransaction(trans));
                }
            }
            catch (TCGException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new TCGException($"Bad file format! {ex.Message}");
            }

            return result;
        }

        public void SetParser(ITransactionParser parser)
        {
            _transactionParser = parser;
        }
    }
}
