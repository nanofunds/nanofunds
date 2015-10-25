namespace nanofunds.Models
{
    using System;

    public class Repayment
    {
        public Guid Id { get; set; } 

        public Account FromAccount { get; set; }

        public Account ToAccount { get; set; }

        public decimal Amount { get; set; }

        public DateTime ChargeOnDate { get; set; }

        public Repayment(Account from, Account to, decimal amount, DateTime chargeOnDate)
        {
            Id = Guid.NewGuid();
            FromAccount = from;
            ToAccount = to;
            Amount = amount;
            ChargeOnDate = chargeOnDate;
        }
    }
}