using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransCollectGateway.Common;

namespace TransCollectGateway.Infrastructure
{
    public class TransLogUploadService : ITransLogUploadService
    {
        private readonly ITransLogFileProcessing _fileProcessing;
        private readonly IRepository<Transaction> _repository;

        public TransLogUploadService(ITransLogFileProcessing fileProcessing, IRepository<Transaction> repository)
        {
            _fileProcessing = fileProcessing;
            _repository = repository;
        }

        public void UploadTransLog(Stream fileData, TransFileFormat format)
        {
            if (fileData == null)
                throw new ArgumentNullException(nameof(fileData));

            switch(format)
            {
                case TransFileFormat.CSV:
                    _fileProcessing.SetParser(new TransLogCSVParser());
                    break;
                case TransFileFormat.XML:
                    _fileProcessing.SetParser(new TransLogXMLParser());
                    break;
            }

            var result = _fileProcessing.ProcessTransactionFile(fileData) ?? throw new TCGException("Error parsing transaction file");

            foreach(var trans in result)
            {
                _repository.Create(trans);
            }

            _repository.SaveChanges();
        }
    }
}
