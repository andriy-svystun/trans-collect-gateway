using System;
using System.Collections.Generic;
using System.Text;

namespace TransCollectGateway.Common
{
    public class Transaction
    {
        string TransactionId { get; set; }
        decimal Amount { get; set; }
        DateTime TransDate { get; set; }
        string CurrencyCode { get; set; }
        TransStatus Status { get; set; }
    }
}
