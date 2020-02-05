﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (transactionData == null)
                throw new ArgumentNullException(nameof(transactionData));

            string pattern = ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))";
            Regex regex = new Regex(pattern, RegexOptions.Compiled);

            var values = regex.Split(transactionData);

            if (values.Length < 5)
                throw new ArgumentException(nameof(transactionData));

            for (int i =0; i< values.Length; i++)
            {
                values[i] = values[i].TrimStart(' ', '"');
                values[i] = values[i].TrimEnd('"');
            }

            var result = new Transaction();

            result.TransactionId = values[0];

            var format = new CultureInfo(CultureInfo.InvariantCulture.LCID);
            format.NumberFormat.NumberDecimalSeparator = ".";
            format.NumberFormat.NumberGroupSeparator = ",";
            NumberStyles numStyle = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
            result.Amount = decimal.Parse(values[1], numStyle, format);
            
            result.CurrencyCode =  values[2];
            result.TransDate = DateTime.Parse(values[3]);
            switch (values[4])
            {
                case "Approved": result.Status = TransStatus.Approved;
                    break;
                case "Failed": result.Status = TransStatus.Rejected;
                    break;
                case "Finished": result.Status = TransStatus.Done;
                    break;

                default: throw new ArgumentException(nameof(transactionData));
            }

            return result;
        }
    }
}
