using System;
using System.Collections.Generic;
using System.Text;

namespace TransCollectGateway.Common.Services
{
    public interface ITransViewService
    {
        IEnumerable<Transaction> GetTransByCurrency(string currency);
        IEnumerable<Transaction> GetTransByDates(DateTime startDate, DateTime endDate);
        IEnumerable<Transaction> GetTransByStatus(TransStatus status);
    }
}
