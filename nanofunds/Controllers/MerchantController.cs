﻿namespace nanofunds.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

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
        public string Get(int id)
        {
            return "value";
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