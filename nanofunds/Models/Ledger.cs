namespace nanofunds.Models
{
    using System;
    using System.Collections.Generic;

    public class Ledger
    {
        public Ledger()
        {
            Accounts = new List<Account>();
        }

        public List<Account> Accounts { get; set; }

        public Guid Id { get; set; }
    }
}