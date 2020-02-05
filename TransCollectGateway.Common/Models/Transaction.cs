using System;
using System.Collections.Generic;
using System.Text;

namespace TransCollectGateway.Common
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransDate { get; set; }
        public string CurrencyCode { get; set; }
        public TransStatus Status { get; set; }
    }
}
