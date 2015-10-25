using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace nanofunds.Controllers
{
    using Models;
    using Models.Enums;

    public class TransactionController : ApiController
    {
        public object Get(string id)
        {
            var db = new nanofunds();

            var account =
                db.Ledgers.SingleOrDefault(x => x.Name == "main").Accounts.SingleOrDefault(x => x.Merchant.SourceId == id);

            return account.Transactions.OrderByDescending(x=>x.Timestamp).Select(x => new
            {
                Date = x.Timestamp.Date,
                Kind = Enum.GetName(typeof(TTransactionKind), x.Kind),
                Type = Enum.GetName(typeof(TTransaction), x.Type),
                Amount = x.Amount
            });
        }
    }
}
