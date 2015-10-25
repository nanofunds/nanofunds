namespace nanofunds.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using Extensions;

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

            var a = new List<decimal>();
            var b = new List<decimal>();

            foreach (var date in DateTimeExtensions.EachDay(new DateTime(2015, 9, 15), new DateTime(2015, 10, 25)))
            {
                a.Add(merchant.Receipts.Where(x => x.Date == date).Sum(x => x.Amount));
                b.Add(merchant.Predictions.Where(x => x.Date == date).Sum(x => x.Amount));
            }

            return new
            {
                Days = DateTimeExtensions.EachDay(new DateTime(2015, 9, 15), new DateTime(2015, 10, 25)).Select(x=>x.Date.ToString("d")).ToList(),
                Merchant = merchant.Name,
                Graphs = new[]
                {
                    a,
                    b
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