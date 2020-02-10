using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using TransCollectGateway.Common;
using AutoMapper;

namespace TransCollectGateway.API.Controllers
{
    public class TransactionsController : ApiController
    {
        private readonly IRepository<Transaction> _repository;
        private readonly IMapper _mapper;

        public TransactionsController(IRepository<Transaction> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/<controller>
        public async Task<JsonResult<IEnumerable<TransactionModel>>> Get()
        {
            var data = await _repository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionModel>>(data);

            return Json<IEnumerable<TransactionModel>>(result);
        }

        [HttpGet]
        // GET api/<controller>?currency
        public JsonResult<IEnumerable<TransactionModel>> Get([FromUri] string currency)
        {
            var result = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionModel>>(_repository.Get(q => q.CurrencyCode.Equals(currency.ToUpper())));

            return Json<IEnumerable<TransactionModel>>(result);
        }

        [HttpGet]
        public JsonResult<IEnumerable<TransactionModel>> GetByStatus([FromUri] string status)
        {
            var result = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionModel>>(_repository.Get(q => q.Status == (TransStatus) Enum.Parse(typeof(TransStatus), status) ));

            return Json<IEnumerable<TransactionModel>>(result);
        }

        [HttpGet]
        public JsonResult<IEnumerable<TransactionModel>> GetByDateRange([FromUri] DateTime date1, [FromUri] DateTime date2)
        {
            var result = _mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionModel>>(_repository.Get(q => q.TransDate >= date1 && q.TransDate <= date2));

            return Json<IEnumerable<TransactionModel>>(result);
        }

    }
}