using System;
using System.Collections.Generic;
using System.Text;

namespace TransCollectGateway.Common
{
    public interface ITransLogUploadService
    {
        void UploadTransLog(System.IO.Stream fileData, TransFileFormat format);
    }
}
