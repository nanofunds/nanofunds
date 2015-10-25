namespace nanofunds.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Logic;

    using Models;
    using Models.Enums;

    using NUnit.Framework;

    [TestFixture]
    public class FundsDistributorTests
    {
        private DateTime testDate = DateTime.Today;

        [Test]
        public void Should_Distribute_Funds()
        {
            var distributor = new FundsDistributor();

            var ledger = GetLedger();

            distributor.Distribute(ledger);

            var merchantAccount = ledger.Accounts.Single(x => x.Id != Guid.Empty);
            var nanoAccount = ledger.Accounts.Single(x => x.Id == Guid.Empty);

            Assert.AreEqual(merchantAccount
                .Transactions.Single(x=>x.Kind == TTransactionKind.Advance).Amount, 75);

            Assert.AreEqual(nanoAccount
                .Transactions.Single(x=>x.Kind == TTransactionKind.Advance).Amount, -75);

            var repayment = ledger.Repayments.First();
            Assert.AreEqual(repayment.ChargeOnDate, testDate.AddDays(2));
            Assert.AreEqual(repayment.Amount, 75);
            Assert.AreEqual(repayment.FromAccount.Merchant.Name, "some merchant");
            Assert.AreEqual(repayment.ToAccount.Merchant.Name, "nanofunds");
        }

        private Ledger GetLedger()
        {
            var ledger = new Ledger
            {
                Name = "main"
            };

            ledger.Accounts.Add(new Account
            {
                Balance = 0,
                Id = Guid.Empty,
                Merchant = new Merchant("nanofunds")
                {
                    Id = Guid.Empty,
                    Name = "nanofunds",
                    Balance = 0,
                    Enrolled = false
                }
            });

            ledger.Accounts.Add(new Account
            {
                Balance = 0,
                Id = Guid.NewGuid(),
                Merchant = new Merchant("some-merchant-source-id")
                {
                    Enrolled = true,
                    Id = Guid.NewGuid(),
                    Name = "some merchant",
                    Predictions = new List<PredictedReceipt>
                    {
                        new PredictedReceipt
                        {
                            Amount = 100,
                            Date = testDate,
                            Id = Guid.NewGuid()
                        }
                    }
                }
            });

            return ledger;
        }
    }
}