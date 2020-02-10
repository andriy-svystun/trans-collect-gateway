using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransCollectGateway.API
{
    public class TransactionModel
    {
        public string Id { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }
    }
}