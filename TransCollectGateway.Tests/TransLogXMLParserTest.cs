using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TransCollectGateway.Common;
using TransCollectGateway.Infrastructure;

namespace TransCollectGateway.Tests
{
    [TestClass]
    public class TransLogXMLParserTest
    {
        private const string xmlFileContent = @"<Transactions>
                    <Transaction id=”Inv00001”>
                        <TransactionDate>2019-01-23T13:45:10</TransactionDate>
                        <PaymentDetails>
                        <Amount>200.00</Amount>
                        <CurrencyCode>USD</CurrencyCode>
                        </PaymentDetails>
                        <Status>Done</Status>
                    </Transaction>
                </Transactions>";


        private const string xmlTransaction = @"<Transaction id=”Inv00001”>
                        <TransactionDate>2019-01-23T13:45:10</TransactionDate>
                        <PaymentDetails>
                        <Amount>200.00</Amount>
                        <CurrencyCode>USD</CurrencyCode>
                        </PaymentDetails>
                        <Status>Done</Status>
                    </Transaction>
                    ";

        private Stream GetXMLFileStream(string content)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(content);

            MemoryStream stream = new MemoryStream();
            xml.Save(stream);
            stream.Flush();
            stream.Position = 0;

            return stream;
        }

        [TestMethod]
        public void TestGetSupportedFileType()
        {
            TransLogXMLParser testpasrer = new TransLogXMLParser();

            Assert.AreEqual(testpasrer.GetSupportedFileType(), "XML");
        }

        [TestMethod]
        public void GetTransactionsCount1Test()
        {
            TransLogXMLParser testpasrer = new TransLogXMLParser();

            var xmlFile = GetXMLFileStream(xmlFileContent);

            IEnumerable<string> res = testpasrer.GetTransactions(xmlFile);

            Assert.IsNotNull(res);

            Assert.AreEqual(1, res.Count());
        }

        [TestMethod]
        public void ParseTransactionNullTest()
        {
            TransLogXMLParser testpasrer = new TransLogXMLParser();

            Assert.ThrowsException<ArgumentNullException>(() => testpasrer.ParseTransaction(null), "ParseTransaction() - ArgumentNullException");
        }

        [TestMethod]
        public void ParseTransactionBadFormatExpeptionTest()
        {
            TransLogXMLParser testpasrer = new TransLogXMLParser();

            Assert.ThrowsException<TCGException>(() => testpasrer.ParseTransaction("..."), "ParseTransaction() - TCGException");
        }

        [TestMethod]
        public void ParseTransactionTransactionIdTest()
        {
            TransLogXMLParser testpasrer = new TransLogXMLParser();

            var res = testpasrer.ParseTransaction(xmlTransaction).TransactionId;

            Assert.AreEqual("Inv00001", res);
        }

        [TestMethod]
        public void ParseTransactionAmountTest()
        {
            TransLogXMLParser testpasrer = new TransLogXMLParser();

            var res = testpasrer.ParseTransaction(xmlTransaction).Amount;

            Assert.AreEqual(200, res);

        }

        [TestMethod]
        public void ParseTransactionCurrencyCodeTest()
        {
            TransLogXMLParser testpasrer = new TransLogXMLParser();

            var res = testpasrer.ParseTransaction(xmlTransaction).CurrencyCode;

            Assert.AreEqual("USD", res);
        }

        [TestMethod]
        public void ParseTransactionDateTest()
        {
            TransLogCSVParser testpasrer = new TransLogCSVParser();

            var res = testpasrer.ParseTransaction(xmlTransaction).TransDate;

            Assert.AreEqual(new DateTime(2019, 01, 23, 13, 45, 10, DateTimeKind.Local), res);
        }

        [TestMethod]
        public void ParseTransactionStatusTest()
        {
            TransLogCSVParser testpasrer = new TransLogCSVParser();

            var res = testpasrer.ParseTransaction(xmlTransaction).Status;

            Assert.AreEqual(TransStatus.Approved, res);
        }

    }
}
