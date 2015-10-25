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
        public object Get(string id)
        {
            var db = new nanofunds();

            var merchant = db.Merchants.SingleOrDefault(x => x.SourceId == id);

            db.Entry(merchant).Collection(x=>x.Receipts).Load();
            db.Entry(merchant).Collection(x=>x.Predictions).Load();

            return new
            {
                Merchant = merchant,
                Graphs = new
                {
                    a = merchant.Receipts.OrderByDescending(x=>x.Date).Take(21),
                    b = merchant.Predictions.OrderByDescending(x => x.Date)
                }
            };
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