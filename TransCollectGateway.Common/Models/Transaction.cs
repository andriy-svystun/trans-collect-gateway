using System;
using System.Collections.Generic;
using System.Globalization;
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

        public static decimal ParseDecimal(string text)
        {
            var format = new CultureInfo(CultureInfo.InvariantCulture.LCID);
            format.NumberFormat.NumberDecimalSeparator = ".";
            format.NumberFormat.NumberGroupSeparator = ",";
            NumberStyles numStyle = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
            return decimal.Parse(text, numStyle, format);

        }

    }
}
