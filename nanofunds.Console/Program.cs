namespace nanofunds.Console
{
    using System;
    using System.Linq;

    using Extensions;

    using Logic;

    using Models;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var db = new nanofunds();

            var calculator = new EstimatedReceiptsCalculator();
            var distributor = new FundsDistributor();

            var merchants = db.Merchants.ToList();
            var ledger = db.Ledgers.Single(x => x.Name == "main");

            db.Entry(ledger).Collection(x => x.Accounts).Load();
            db.Entry(ledger).Collection(x => x.Repayments).Load();

            var today = new DateTime(2015, 10, 1);

            foreach (var merchant in merchants)
            {
                if (merchant.Id == Guid.Empty) continue;

                db.Entry(merchant).Collection(x => x.Receipts).Load();
                db.Entry(merchant).Collection(x => x.Predictions).Load();

                var predictions = calculator.Calculate(merchant.Receipts.Where(x => x.Date < today).ToList(), 1);

                foreach (var prediction in predictions)
                {
                    merchant.Predictions.Add(prediction);
                }
            }

            db.SaveChanges();

            foreach (var date in DateTimeExtensions.EachDay(today, today.AddDays(7)))
            {
                foreach (var account in ledger.Accounts)
                {
                    db.Entry(account.Merchant).Collection(x => x.Receipts).Load();
                    db.Entry(account.Merchant).Collection(x => x.Predictions).Load();
                }

                distributor.Distribute(ledger, date);

                db.SaveChanges();
            }
        }
    }
}