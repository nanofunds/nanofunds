namespace nanofunds.Logic
{
    using System;
    using System.Linq;

    using Models;
    using Models.Enums;

    public class FundsDistributor
    {
        public void Distribute(Ledger ledger, DateTime? now = null)
        {
            var timestamp = now ?? DateTime.Today;

            var nano = ledger.Accounts.SingleOrDefault(x => x.Merchant.Id == Guid.Empty);

            foreach (var account in ledger.Accounts)
            {
                if (account.Merchant.Enrolled)
                {
                    var estimatedReceipts = 0m;

                    if (timestamp.DayOfWeek == DayOfWeek.Friday)
                    {
                        estimatedReceipts = account.Merchant.Predictions
                            .Where(
                                x =>
                                    x.Date == timestamp ||
                                    x.Date == timestamp.AddDays(1) ||
                                    x.Date == timestamp.AddDays(2))
                            .Sum(x => x.Amount);
                    }
                    else if (timestamp.DayOfWeek == DayOfWeek.Saturday ||
                             timestamp.DayOfWeek == DayOfWeek.Sunday)
                    {
                        estimatedReceipts = 0;
                    }
                    else
                    {
                        estimatedReceipts = account.Merchant.Predictions
                            .Where(x => x.Date == timestamp)
                            .Sum(x => x.Amount);
                    }

                    if (estimatedReceipts > 0)
                    {
                        var interestPercentage = GetInterestPercentage(account.Merchant);
                        var advancePercentage = GetAdvancePercentage(account.Merchant);

                        var interestAmount = account.Balance*interestPercentage/100;
                        var advanceAmount = estimatedReceipts*advancePercentage;

                        if (interestAmount > 0)
                        {
                            ledger.Transfer(account, nano, interestAmount,
                                TTransactionKind.Interest);
                        }

                        if (advanceAmount > 0)
                        {
                            ledger.Transfer(nano, account, advanceAmount,
                                TTransactionKind.Advance);

                            ledger.ScheduleRepayment(account, nano, advanceAmount, timestamp.AddDays(2));
                        }
                    }
                }
            }

            foreach (var repayment in ledger.Repayments)
            {
                if (repayment.ChargeOnDate == timestamp && !repayment.IsPaid)
                {
                    ledger.Transfer(repayment.FromAccount, repayment.ToAccount, repayment.Amount,
                        TTransactionKind.Repayment);

                    repayment.IsPaid = true;
                }
            }
        }

        public decimal GetAdvancePercentage(Merchant merchant)
        {
            return 0.75m;
        }

        public decimal GetInterestPercentage(Merchant merchant)
        {
            return 0.20m;
        }
    }
}