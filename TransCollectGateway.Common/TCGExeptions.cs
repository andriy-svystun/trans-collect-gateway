using System;
using System.Collections.Generic;
using System.Text;

namespace TransCollectGateway.Common
{

    [Serializable]
    public class TCGException : ApplicationException
    {
        public TCGException() { }
        public TCGException(string message) : base(message) { }
        public TCGException(string message, Exception inner) : base(message, inner) { }
        protected TCGException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
