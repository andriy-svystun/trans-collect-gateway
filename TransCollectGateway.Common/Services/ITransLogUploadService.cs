using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TransCollectGateway.Common
{
    public interface ITransLogUploadService
    {
        Task UploadTransLog(System.IO.Stream fileData, TransFileFormat format);
    }
}
