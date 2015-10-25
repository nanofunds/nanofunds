namespace nanofunds.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<nanofunds>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(nanofunds context)
        {
            var merchant = new Merchant("nanofunds")
            {
                Id = Guid.Empty,
                Name = "nanofunds",
                Balance = 0,
                Enrolled = false
            };

            context.Merchants.Add(merchant);

            context.Ledgers.Add(new Ledger
            {
                Id = Guid.NewGuid(),
                Accounts = new List<Account>
                {
                    new Account
                    {
                        Balance = 0,
                        Id = Guid.Empty,
                        Merchant = merchant
                    }
                }
            });
        }
    }
}