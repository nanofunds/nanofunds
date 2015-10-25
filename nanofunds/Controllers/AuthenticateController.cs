using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nanofunds.Controllers
{
    using System.Web.Configuration;

    using Models;
    using Models.DTOS;

    using RestSharp;

    public class AuthenticateController : Controller
    {
        // GET: Authenticate
        public ActionResult Index(string merchant_id, string employee_id, string client_id, string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                this.Response.Redirect(
                    "https://sandbox.dev.clover.com/oauth/authorize?client_id=4DCJSK51BSG52&redirect_url=http://localhost:2275/");
            }
            else
            {
                this.GetToken(merchant_id, code);
            }

            return this.RedirectToAction("Index", "Home", new { id = merchant_id });
        }

        private void GetToken(string merchantId, string code)
        {
            var tokenEndpoint = WebConfigurationManager.AppSettings["CloverTokenEndpoint"];
            var clientId = WebConfigurationManager.AppSettings["ClientId"];
            var secretKey = WebConfigurationManager.AppSettings["SecretKey"];

            var client = new RestClient(tokenEndpoint);

            var request = new RestRequest("/oauth/token?client_id={client_id}&client_secret={client_secret}&code={code}", Method.GET);

            request.AddUrlSegment("client_id", clientId);
            request.AddUrlSegment("client_secret", secretKey);
            request.AddUrlSegment("code", code);

            var authorization = client.Execute<Authorization>(request).Data;
            var db = new nanofunds();
            var merchant = db.Merchants.SingleOrDefault(m => m.SourceId == merchantId);

            if (merchant == null)
            {
                var cloverMerchant = this.GetMerchant(merchantId, authorization.access_token);
                merchant = new Merchant(merchantId) { Id = Guid.NewGuid(), Name = cloverMerchant.Name, SourceToken = authorization.access_token };
                db.Merchants.Add(merchant);
            }
            else
            {
                merchant.SourceToken = authorization.access_token;
            }
            db.SaveChanges();
        }

        private CloverMerchant GetMerchant(string merchantId, string accessToken)
        {
            var endpoint = WebConfigurationManager.AppSettings["CloverApiUrl"];
            var client = new RestClient(endpoint);

            var request = new RestRequest("/merchants/{mId}", Method.GET);
            request.AddUrlSegment("mId", merchantId);

            request.AddHeader("Authorization", "Bearer " + accessToken);

            var merchant = client.Execute<CloverMerchant>(request).Data;

            return merchant;
        }
    }
}