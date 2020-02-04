using System;
using System.Collections.Generic;
using System.Text;

namespace TransCollectGateway.Common
{
    public enum TransFileFormat
    {
        CSV,
        XML
    }

    public enum TransStatus
    {
        Approved = 'A',
        Rejected = 'R',
        Done = 'D'
    }
}
