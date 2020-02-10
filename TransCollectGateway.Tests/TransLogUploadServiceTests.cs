using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCollectGateway.Common;
using TransCollectGateway.Infrastructure;
using Moq;

namespace TransCollectGateway.Tests
{
    [TestClass]
    public class TransLogUploadServiceTests
    {
        private TransLogUploadService _uploadService = null;
        private Mock<ITransLogFileProcessing> _fileProcessingMock = null;
        private Mock<IRepository<Transaction>> _reposMock;
        private readonly Transaction _transaction;

        public TransLogUploadServiceTests()
        {
            _transaction = new Transaction() { TransactionId = "TransId001" };
            _fileProcessingMock = new Mock<ITransLogFileProcessing>();
            _fileProcessingMock.Setup(a => a.ProcessTransactionFile(It.IsAny<Stream>())).ReturnsAsync(new List<Transaction>() { _transaction } );

            _reposMock = new Mock<IRepository<Transaction>>();
            _reposMock.Setup(a => a.Create(It.IsAny<Transaction>()));
        }

        [TestMethod]
        public async void UploadTransLogCVSTest()
        {
            _fileProcessingMock.Setup(a => a.SetParser(It.IsAny<TransLogCSVParser>()));

            _uploadService = new TransLogUploadService(_fileProcessingMock.Object, _reposMock.Object);

            var data = new MemoryStream();

            await _uploadService.UploadTransLog(data, TransFileFormat.CSV);

            _fileProcessingMock.Verify(f => f.SetParser(It.IsAny<TransLogCSVParser>()), Times.Once);
            _fileProcessingMock.Verify(f => f.ProcessTransactionFile(It.Is<MemoryStream>( s => (s == data))), Times.Once);

            _reposMock.Verify(f => f.Create(It.Is<Transaction>(s => s == _transaction)), Times.Once);
        }
    }
}
