namespace nanofunds.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Extensions;

    using Models;

    public class EstimatedReceiptsCalculator
    {
        public List<PredictedReceipt> Calculate(List<ActualReceipt> actualReceipts, int weeks)
        {
            var receipts = new List<Receipt>().Concat(actualReceipts).ToList();
            var fourWeekAverage = GetMovingAverage(receipts);
            var today = receipts.Last().Date;

            var predictions = new List<PredictedReceipt>();

            foreach (var date in DateTimeExtensions.EachDay(today.AddDays(1), today.AddDays(weeks*7)))
            {
                var prediction = new PredictedReceipt
                {
                    Date = date,
                    Amount = fourWeekAverage[date.DayOfWeek]
                };

                predictions.Add(prediction);
                receipts.Add(prediction);

                fourWeekAverage = GetMovingAverage(receipts);
            }

            return predictions;
        }

        private static Dictionary<DayOfWeek, decimal> GetMovingAverage(IEnumerable<Receipt> receipts)
        {
            const int weeksToIncludeInAverage = 4;

            var historicalAverage = new Dictionary<DayOfWeek, decimal>();

            foreach (var dayOfWeek in Enum.GetValues(typeof (DayOfWeek)).OfType<DayOfWeek>())
            {
                var daysToSkip = receipts.Count() - weeksToIncludeInAverage*7;
                var includedWeekdays = receipts.Where(x => x.Weekday == dayOfWeek)
                    .Skip(daysToSkip/7);

                historicalAverage[dayOfWeek] = includedWeekdays.Sum(x => x.Amount)/weeksToIncludeInAverage;
            }

            return historicalAverage;
        }
    }
}