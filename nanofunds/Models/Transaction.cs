namespace nanofunds.Models
{
    using System;

    using Enums;

    public class Transaction
    {
        public decimal Amount { get; set; }

        public Guid Id { get; set; }

        public DateTime Timestamp { get; set; }

        public TTransaction Type { get; set; }
    }
}