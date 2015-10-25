﻿namespace nanofunds.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Models;
    using Models.Enums;

    public class FundsDistributor
    {
        public void Distribute(Ledger ledger)
        {
            var nano = ledger.Accounts.SingleOrDefault(x => x.Merchant.Id == Guid.Empty);

            foreach (var account in ledger.Accounts)
            {
                if (account.Merchant.Enrolled)
                {
                    var estimatedReceipts = 0m;

                    if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
                    {
                        estimatedReceipts = account.Merchant.Predictions
                            .Where(
                                x =>
                                    x.Date == DateTime.Today || 
                                    x.Date == DateTime.Today.AddDays(1) ||
                                    x.Date == DateTime.Today.AddDays(2))
                            .Sum(x => x.Amount);
                    }
                    else if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday ||
                             DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
                    {
                        estimatedReceipts = 0;
                    }
                    else
                    {
                        estimatedReceipts = account.Merchant.Predictions
                            .Where(x => x.Date == DateTime.Today)
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

                            ledger.ScheduleRepayment(account, nano, advanceAmount, DateTime.Today.AddDays(2));
                        }
                    }
                }
            }

            var removeRepayments = new List<Guid>();

            foreach (var repayment in ledger.Repayments)
            {
                if (repayment.ChargeOnDate == DateTime.Today)
                {
                    ledger.Transfer(repayment.FromAccount, repayment.ToAccount, repayment.Amount,
                        TTransactionKind.Repayment);

                    removeRepayments.Add(repayment.Id);
                }
            }

            ledger.Repayments.RemoveAll(x => removeRepayments.Contains(x.Id));
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