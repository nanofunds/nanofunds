using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace nanofunds.Controllers
{
    using System.Data.Entity;
    using System.Security.Cryptography.X509Certificates;

    using Models;
    using Models.Enums;

    public class TransactionController : ApiController
    {
        public object Get(string id)
        {
            var db = new nanofunds();

            var ledger = db.Ledgers.Include(x=>x.Accounts.Select(a=>a.Merchant)).SingleOrDefault(x => x.Name == "main");

            var account =
                ledger.Accounts.SingleOrDefault(x => x.Merchant.SourceId == id);

            db.Entry(account).Collection(x=>x.Transactions).Load();

            var history = account.Transactions.OrderByDescending(x => x.Timestamp).Select(x => new
            {
                Date = x.Timestamp.Date,
                Kind = Enum.GetName(typeof (TTransactionKind), x.Kind),
                Type = Enum.GetName(typeof (TTransaction), x.Type),
                Amount = x.Amount
            });

            return history;
        }
    }
}
