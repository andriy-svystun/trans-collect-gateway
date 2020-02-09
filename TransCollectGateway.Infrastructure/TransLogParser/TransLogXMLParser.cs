using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TransCollectGateway.Common;

namespace TransCollectGateway.Infrastructure
{
    public class TransLogXMLParser : ITransactionParser
    {
        public string GetSupportedFileType()
        {
            return "XML";
        }

        public async Task<IEnumerable<string>> GetTransactions(Stream transData)
        {
            if (transData == null)
                throw new ArgumentNullException(nameof(transData));

            var res = new List<string>();

            XmlDocument xmlDoc = new XmlDocument();
            await Task.Run(() => xmlDoc.Load(transData));

            var xRoot = xmlDoc.SelectNodes("//Transactions/Transaction") ?? throw new TCGException("Bad file format");

            foreach(XmlNode xNode in xRoot)
            {
                res.Add(xNode.OuterXml);             
            }

            return res;

        }

        public Transaction ParseTransaction(string transactionData)
        {
            if (transactionData == null)
                throw new ArgumentNullException(nameof(transactionData));

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(transactionData);

            var result = new Transaction();

            var xRoot = xmlDoc.DocumentElement;

            result.TransactionId = xRoot.GetAttribute("id");

            var xNode = xRoot.SelectSingleNode("//PaymentDetails/Amount") ?? throw new TCGException("Bad file format");
            result.Amount = Transaction.ParseDecimal(xNode.InnerText);

            xNode = xRoot.SelectSingleNode("//PaymentDetails/CurrencyCode") ?? throw new TCGException("Bad file format");
            result.CurrencyCode = xNode.InnerText;

            xNode = xRoot.SelectSingleNode("TransactionDate") ?? throw new TCGException("Bad file format");

            result.TransDate = DateTime.Parse(xNode.InnerText);

            xNode = xRoot.SelectSingleNode("Status") ?? throw new TCGException("Bad file format");
            switch (xNode.InnerText)
            {
                case "Approved":
                    result.Status = TransStatus.Approved;
                    break;
                case "Rejected":
                    result.Status = TransStatus.Rejected;
                    break;
                case "Done":
                    result.Status = TransStatus.Done;
                    break;

                default: throw new TCGException("Bad file format");
            }


            return result;
        }
    }
}
