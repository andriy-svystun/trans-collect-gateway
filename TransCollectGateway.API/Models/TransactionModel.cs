using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransCollectGateway.Common;

namespace TransCollectGateway.API
{
    public class TransactionModel
    {
        public string Id { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }

        public static string GetStatusCodeMapping(TransStatus transStatus)
        {
            switch (transStatus)
            {
                case TransStatus.Approved: return "A";
                case TransStatus.Rejected: return "R";
                case TransStatus.Done: return "D";
                default:
                    return "";
            }

        }
    }
}