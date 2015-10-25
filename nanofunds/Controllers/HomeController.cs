using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nanofunds.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string merchant_id, string employee_id, string client_id, string code)
        {

            if (string.IsNullOrEmpty(code))
            {
                this.Response.Redirect("https://sandbox.dev.clover.com/oauth/authorize?client_id=4DCJSK51BSG52&redirect_url=http://localhost:2275/");
            }
            return this.View();
        }
    }
}