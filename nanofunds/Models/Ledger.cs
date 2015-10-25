namespace nanofunds.Models
{
    using System;
    using System.Collections.Generic;

    using Enums;

    public class Ledger
    {
        public Ledger()
        {
            Accounts = new List<Account>();
            Repayments = new List<Repayment>();
        }

        public List<Account> Accounts { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Repayment> Repayments { get; set; }

        public void ScheduleRepayment(Account from, Account to, decimal amount, DateTime chargeOnDate)
        {
            Repayments.Add(new Repayment(this, from, to, amount, chargeOnDate));
        }

        public void Transfer(Account from, Account to, decimal amount, TTransactionKind kind)
        {
            from.Transactions.Add(new Transaction
            {
                Amount = -amount,
                Id = Guid.NewGuid(),
                Kind = kind,
                Timestamp = DateTime.Now,
                Type = TTransaction.Credit
            });

            from.Balance += -amount;

            to.Transactions.Add(new Transaction
            {
                Amount = amount,
                Id = Guid.NewGuid(),
                Kind = kind,
                Timestamp = DateTime.Now,
                Type = TTransaction.Debit
            });

            to.Balance += amount;
        }
    }
}