using System;
using System.Collections.Generic;
using System.Globalization;

namespace Controlsubscriptions
{
    internal class InitializeInputData
    {
        private Subscription InitializeSubscription()
        {
            return new Subscription { Start = new DateTime(2017, 03, 03), End = null, PricePerPeriod = 10.00m };
        }

        private List<Discount> InitializeDiscount()
        {
            return new List<Discount>
            {
                new Discount { Start = new DateTime(2017, 03, 03), End = new DateTime(2017, 03, 17), PercentReduction = 50},
               // new Discount { Start = new DateTime(2017, 03, 09), End = new DateTime(2017, 03, 12), PercentReduction = 70},
               // new Discount { Start = new DateTime(2017, 02, 17, 0, 0, 0), End = new DateTime(2017, 04, 10, 12, 0, 0), PercentReduction = 20},
            };
        }

        private DateTime InitializeBillingEnd()
        {
            return new DateTime(2017, 05, 03);
        }

        private IEnumerable<InvoiceLine> GetInvoiceLines()
        {
           return  BillingHelper.BillSubscriptionWithDiscounts(InitializeSubscription(), InitializeDiscount(), InitializeBillingEnd());
        }

        internal string Display()
        {
            string finalMessage = default;
            foreach (var invoice in GetInvoiceLines())
            {
                finalMessage +=
                    $"{invoice.Start.ToString(CultureInfo.InvariantCulture).PadLeft(20)} -{invoice.End.ToString(CultureInfo.InvariantCulture).PadLeft(20)} | PricePerPeriod: " +
                    $"{invoice.PricePerPeriod.ToString("f2").PadLeft(5)} | Duration: {invoice.Duration.ToString("f1").PadLeft(5)} | " +
                    $"Total:{invoice.Total.ToString("f2").PadLeft(7)}\n";
            }

            return finalMessage;
        }
    }
}
