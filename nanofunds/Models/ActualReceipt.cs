namespace nanofunds.Models
{
    using System;

    public class ActualReceipt : Receipt
    {
        public ActualReceipt(Merchant merchant, decimal amount, DateTime date)
            : base(merchant, amount, date)
        {
        }
    }
}