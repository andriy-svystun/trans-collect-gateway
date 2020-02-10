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
using Moq;

namespace TransCollectGateway.Tests
{
    [TestClass]
    public class TransLogFileProcessingTests
    {
        [TestMethod]
        public async void ProcessTransactionFileTest()
        {
            var transData = "transData";
            var transaction = new Transaction() { TransactionId = transData };

            var mock = new Mock<ITransactionParser>();
            mock.Setup(a => a.GetTransactions(It.IsAny<Stream>())).ReturnsAsync(new string[] { transData });
            mock.Setup(a => a.ParseTransaction(It.IsAny<string>())).Returns(transaction);

            var processor = new TransLogFileProcessing();
            processor.SetParser(mock.Object);
            var result = await processor.ProcessTransactionFile(new MemoryStream());

            mock.Verify(a => a.GetTransactions(It.IsAny<Stream>()), Times.Once);
            mock.Verify(a => a.ParseTransaction(It.IsAny<string>()), Times.AtLeastOnce);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreSame(transaction, result.First());
        }
    }
}
