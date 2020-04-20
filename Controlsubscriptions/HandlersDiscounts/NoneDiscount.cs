using System;
using System.Collections.Generic;

namespace Controlsubscriptions.HandlersDiscounts
{
    internal class NoneDiscount:DiscountHandler
    {
        private const int Index = 0;

        internal override void RegisteringInvoicePeriodsByDate(DateTime dataNow, List<InvoiceLine> list,
            List<Discount> discounts,
            Subscription subscription, DateTime finishSubscription)
        {
            if (dataNow == subscription.Start)
            {
                list.Add(new InvoiceLine());
                list[Index].PricePerPeriod = GetPricePerPeriod(subscription);
            }

            list[Index].Start = subscription.Start;
            list[Index].End = dataNow.AddDays(1);
        }
    }
}
