using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransCollectGateway.Infrastructure;
using System.Linq;
using System.Text;
using TransCollectGateway.Common;

namespace TransCollectGateway.Tests
{
    [TestClass]
    public class TransLogCSVParserTest
    {
        private const string  csvString1 = @"Invoice0000001”,”1,000.00”, “USD”, “20/02/2019 12:33:16”, “Approved";
        private const string  csvString2 = @"Invoice0000002”,”300.00”,”USD”,”21/02/2019 02:04:59”, “Failed";

        private Stream GetCSVFileStreeam(string content)
        {
            MemoryStream outputStream = new MemoryStream();
            var writer = new StreamWriter(outputStream);
            writer.Write(content);
            writer.Flush();
            outputStream.Position = 0;

            return outputStream;
        }

        [TestMethod]
        public void TestGetSupportedFileType()
        {
            TransLogCSVParser testpasrer = new TransLogCSVParser();

            Assert.AreEqual(testpasrer.GetSupportedFileType(), "CSV");
        }

        [TestMethod]
        public void GetTransactionsCount1Test()
        {
            TransLogCSVParser testpasrer = new TransLogCSVParser();

            var cvsFile = GetCSVFileStreeam(csvString1);

            IEnumerable<string> res = testpasrer.GetTransactions(cvsFile);

            Assert.IsNotNull(res);

            Assert.AreEqual(1, res.Count());
        }

        [TestMethod]
        public void GetTransactionsCount2Test()
        {
            TransLogCSVParser testpasrer = new TransLogCSVParser();

            var buf = new StringBuilder(2);
            buf.AppendLine(csvString1);
            buf.AppendLine(csvString2);

            var cvsFile = GetCSVFileStreeam(buf.ToString());

            IEnumerable<string> res = testpasrer.GetTransactions(cvsFile);

            Assert.IsNotNull(res);

            Assert.AreEqual(2, res.Count());
        }


        [TestMethod]
        public void ParseTransactionNullTest()
        {
            TransLogCSVParser testpasrer = new TransLogCSVParser();

            var res = testpasrer.ParseTransaction(csvString1);

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void ParseTransactionTransactionIdTest()
        {
            TransLogCSVParser testpasrer = new TransLogCSVParser();

            var res = testpasrer.ParseTransaction(csvString1).TransactionId;

            Assert.AreEqual("Invoice0000001", res);
        }


        [TestMethod]
        public void ParseTransactionAmountTest()
        {
            TransLogCSVParser testpasrer = new TransLogCSVParser();

            var res = testpasrer.ParseTransaction(csvString1).Amount;

            Assert.AreEqual(1000, res);
        }

        [TestMethod]
        public void ParseTransactionCurrencyCodeTest()
        {
            TransLogCSVParser testpasrer = new TransLogCSVParser();

            var res = testpasrer.ParseTransaction(csvString1).CurrencyCode;

            Assert.AreEqual("USD", res);
        }

        [TestMethod]
        public void ParseTransactionDateTest()
        {
            TransLogCSVParser testpasrer = new TransLogCSVParser();

            var res = testpasrer.ParseTransaction(csvString1).TransDate;

            Assert.AreEqual(new DateTime(2019,02,20,12,33,16,DateTimeKind.Local), res);
        }

        [TestMethod]
        public void ParseTransactionStatusTest()
        {
            TransLogCSVParser testpasrer = new TransLogCSVParser();

            var res = testpasrer.ParseTransaction(csvString1).Status;

            Assert.AreEqual(TransStatus.Approved, res);
        }


    }
}
