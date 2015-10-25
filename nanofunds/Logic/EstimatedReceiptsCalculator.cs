namespace nanofunds.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Extensions;

    using Models;

    public class EstimatedReceiptsCalculator
    {
        public List<Receipt> Calculate(Merchant merchant, int weeks)
        {
            var receipts = new List<Receipt>().Concat(merchant.Receipts).ToList();
            var fourWeekAverage = GetMovingAverage(receipts);
            var today = receipts.Last().Date;

            var predictions = new List<Receipt>();

            foreach (var date in DateTimeExtensions.EachDay(today.AddDays(1), today.AddDays(weeks*7)))
            {
                var prediction = new Receipt
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