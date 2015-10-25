namespace nanofunds.Models
{
    using System;

    public class Repayment
    {
        public Repayment()
        {
        }

        public Repayment(Ledger ledger, Account from, Account to, decimal amount, DateTime chargeOnDate)
        {
            Id = Guid.NewGuid();
            FromAccount = from;
            ToAccount = to;
            Amount = amount;
            ChargeOnDate = chargeOnDate;
            Ledger = ledger;
        }

        public decimal Amount { get; set; }

        public DateTime ChargeOnDate { get; set; }

        public Account FromAccount { get; set; }

        public Guid Id { get; set; }

        public bool IsPaid { get; set; }

        public Ledger Ledger { get; set; }

        public Account ToAccount { get; set; }
    }
}