using System;
using System.Collections.Generic;

namespace Controlsubscriptions.HandlersDiscounts
{
    internal class OneDiscount : DiscountHandler
    {
        private int _index = -1;

        internal override void RegisteringInvoicePeriodsByDate(DateTime dataNow, List<InvoiceLine> list,
            List<Discount> discounts,
            Subscription subscription, DateTime finishSubscription)
        {
            if (IsInRange(dataNow, discounts[0]))
            {
                if (dataNow == subscription.Start.Date || dataNow == discounts[0].Start)
                {
                    list.Add(new InvoiceLine());
                    _index++;
                    list[_index].PricePerPeriod = GetPricePerPeriod(subscription, discounts[0].PercentReduction);
                }

                if (list.Count == 1)
                    list[_index].Start = subscription.Start;
                else
                    list[_index].Start = list[_index - 1].End;

                list[_index].End = dataNow.AddDays(1);
                if (dataNow == discounts[0].End.Date)
                {
                    list[_index].End = discounts[0].End;
                }
            }

            else if (!IsInRange(dataNow, discounts[0]))
            {
                if (dataNow == subscription.Start.Date || dataNow.AddDays(-1) == discounts[0].End.Date)
                {
                    list.Add(new InvoiceLine());
                    _index++;

                    list[_index].PricePerPeriod = list[_index].PricePerPeriod = GetPricePerPeriod(subscription);
                }

                if (list.Count == 1)
                    list[_index].Start = subscription.Start;
                else
                    list[_index].Start = list[_index - 1].End;

                list[_index].End = dataNow.AddDays(1);

                if (dataNow == finishSubscription || dataNow == subscription.End)
                {
                    list[_index].End = subscription.End ?? finishSubscription;
                }
            }
        }
    }
}
