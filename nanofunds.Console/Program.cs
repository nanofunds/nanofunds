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

            var today = new DateTime(2015, 10, 1);

            foreach (var merchant in merchants)
            {
                var predictions = calculator.Calculate(merchant.Receipts.Where(x => x.Date < today).ToList(), 1);
                foreach (var prediction in predictions)
                {
                    merchant.Predictions.Add(prediction);
                }
            }

            db.SaveChanges();

            foreach (var date in DateTimeExtensions.EachDay(today, today.AddDays(7)))
            {
                distributor.Distribute(ledger);

                db.SaveChanges();
            }
        }
    }
}