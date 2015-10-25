using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace nanofunds.Controllers
{
    using System.Diagnostics;
    using System.Web.Configuration;

    using RestSharp;

    public class HomeController : Controller
    {
        public ActionResult Index(string merchant_id, string employee_id, string client_id, string code)
        {

            if (string.IsNullOrEmpty(code))
            {
                this.Response.Redirect(
                    "https://sandbox.dev.clover.com/oauth/authorize?client_id=4DCJSK51BSG52&redirect_url=http://localhost:2275/");
            }
            else
            {
                this.TestRequest(merchant_id, code);
            }
            
            return this.View();
        }

        private void TestRequest(string merchantId, string code)
        {
            var cloverApiUrl = WebConfigurationManager.AppSettings["CloverApiUrl"];
            var clientId = WebConfigurationManager.AppSettings["ClientId"];
            var secretKey = WebConfigurationManager.AppSettings["SecretKey"];

            var client = new RestClient("https://sandbox.dev.clover.com");

            var request = new RestRequest("/oauth/token?client_id={client_id}&client_secret={client_secret}&code={code}", Method.GET);

            request.AddUrlSegment("client_id", clientId);
            request.AddUrlSegment("client_secret", secretKey);
            request.AddUrlSegment("code", code);

            var response = client.Execute(request);
            var content = response.Content;
        }
    }
}