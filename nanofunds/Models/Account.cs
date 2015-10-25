namespace nanofunds.Models
{
    using System;
    using System.Collections.Generic;

    public class Account
    {
        public Account()
        {
            Transactions = new List<Transaction>();
        }

        public decimal Balance { get; set; }

        public Guid Id { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}