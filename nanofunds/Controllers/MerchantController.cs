namespace nanofunds.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Models;

    public class MerchantController : ApiController
    {
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public List<Merchant> Get(string id)
        {
            var db = new nanofunds();

            var merchants = db.Merchants.ToList();

            return merchants;
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}