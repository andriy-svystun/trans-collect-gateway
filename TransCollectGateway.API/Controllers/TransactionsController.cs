using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using TransCollectGateway.Common;

namespace TransCollectGateway.API.Controllers
{
    public class TransactionsController : ApiController
    {
        private readonly IRepository<Transaction> _repository;

        public TransactionsController(IRepository<Transaction> repository)
        {
            _repository = repository;
        }

        // GET api/<controller>
        public async Task<JsonResult<IEnumerable<Transaction>>> Get()
        {
            var result = await _repository.GetAllAsync();

            return Json<IEnumerable<Transaction>>(result);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}