namespace nanofunds.Models
{
    using System;

    public class Receipt
    {
        public Receipt(Merchant merchant, decimal amount, DateTime date)
        {
            Merchant = merchant;
            Amount = amount;
            Date = date;
            Id = Guid.NewGuid();
        }

        public Receipt()
        {
        }

        public Merchant Merchant { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public Guid Id { get; set; }

        public DayOfWeek Weekday => Date.DayOfWeek;
    }
}