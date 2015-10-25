namespace nanofunds.Tests
{
    using System;

    using Logic;

    using Models;

    using NUnit.Framework;

    [TestFixture]
    public class EstimatedReceiptsCalculatorTests
    {
        [Test]
        public void Should_Calculate_Estimated_Receipts()
        {
            var calculator = new EstimatedReceiptsCalculator();

            var now = new DateTime(2015, 10, 19);

            var merchant = new Merchant
            {
                Id = Guid.NewGuid(),
                Balance = 0,
                Name = "nanofunds"
            };

            // week 1
            merchant.AddReceipt(50, now.AddDays(0));
            merchant.AddReceipt(55, now.AddDays(1));
            merchant.AddReceipt(49, now.AddDays(2));
            merchant.AddReceipt(60, now.AddDays(3));
            merchant.AddReceipt(100, now.AddDays(4));
            merchant.AddReceipt(110, now.AddDays(5));
            merchant.AddReceipt(160, now.AddDays(6));
            // week 2
            merchant.AddReceipt(60, now.AddDays(7));
            merchant.AddReceipt(45, now.AddDays(8));
            merchant.AddReceipt(40, now.AddDays(9));
            merchant.AddReceipt(53, now.AddDays(10));
            merchant.AddReceipt(90, now.AddDays(11));
            merchant.AddReceipt(120, now.AddDays(12));
            merchant.AddReceipt(150, now.AddDays(13));
            // week 3
            merchant.AddReceipt(30, now.AddDays(14));
            merchant.AddReceipt(50, now.AddDays(15));
            merchant.AddReceipt(60, now.AddDays(16));
            merchant.AddReceipt(70, now.AddDays(17));
            merchant.AddReceipt(100, now.AddDays(18));
            merchant.AddReceipt(100, now.AddDays(19));
            merchant.AddReceipt(200, now.AddDays(20));
            // week 4
            merchant.AddReceipt(60, now.AddDays(21));
            merchant.AddReceipt(47, now.AddDays(22));
            merchant.AddReceipt(53, now.AddDays(23));
            merchant.AddReceipt(55, now.AddDays(24));
            merchant.AddReceipt(90, now.AddDays(25));
            merchant.AddReceipt(130, now.AddDays(26));
            merchant.AddReceipt(110, now.AddDays(27));
            // week 5
            merchant.AddReceipt(40, now.AddDays(28));
            merchant.AddReceipt(50, now.AddDays(29));
            merchant.AddReceipt(59, now.AddDays(30));
            merchant.AddReceipt(80, now.AddDays(31));
            merchant.AddReceipt(110, now.AddDays(32));
            merchant.AddReceipt(100, now.AddDays(33));
            merchant.AddReceipt(140, now.AddDays(34));
            // week 6
            merchant.AddReceipt(50, now.AddDays(35));
            merchant.AddReceipt(50, now.AddDays(36));
            merchant.AddReceipt(45, now.AddDays(37));
            merchant.AddReceipt(80, now.AddDays(38));
            merchant.AddReceipt(90, now.AddDays(39));
            merchant.AddReceipt(100, now.AddDays(40));
            merchant.AddReceipt(170, now.AddDays(41));

            var estimatedReceipts = calculator.Calculate(merchant, 2);

            var db = new nanofunds();

            db.Merchants.Add(merchant);

            db.SaveChanges();
        }
    }
}