namespace nanofunds.Models
{
    using System;
    using System.Collections.Generic;

    public class Merchant
    {
        public Merchant(string sourceId)
        {
            SourceId = sourceId;
            Receipts = new List<ActualReceipt>();
            Predictions = new List<PredictedReceipt>();
        }

        public bool Enrolled { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<PredictedReceipt> Predictions { get; set; }

        public List<ActualReceipt> Receipts { get; set; }

        public decimal Balance { get; set; }

        public string SourceId { get; set; }

        public string SourceToken { get; set; }

        public void AddReceipt(decimal amount, DateTime date)
        {
            Receipts.Add(new ActualReceipt(this, amount, date));
        }
    }
}