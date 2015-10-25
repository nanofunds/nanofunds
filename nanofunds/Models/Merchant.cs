namespace nanofunds.Models
{
    using System;
    using System.Collections.Generic;

    public class Merchant
    {
        public Merchant()
        {
        }

        public Merchant(string sourceId)
        {
            this.SourceId = sourceId;
            this.Receipts = new List<ActualReceipt>();
            this.Predictions = new List<PredictedReceipt>();
        }

        public decimal Balance { get; set; }

        public bool Enrolled { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<PredictedReceipt> Predictions { get; set; }

        public List<ActualReceipt> Receipts { get; set; }

        public string SourceId { get; set; }

        public string SourceToken { get; set; }

        public void AddReceipt(decimal amount, DateTime date)
        {
            this.Receipts.Add(new ActualReceipt(this, amount, date));
        }
    }
}