using System;
using System.Collections.Generic;

namespace Controlsubscriptions
{
    internal class TwoDiscounts : DiscountHandler
    {
        private int _index = -1; 
        internal override void RegisteringInvoicePeriodsByDate(DateTime dataNow, List<InvoiceLine> list, List<Discount> discounts, 
            Subscription subscription, DateTime finishSubscription)
        {
            if (IsInRange(dataNow, discounts[0]) || IsInRange(dataNow, discounts[0]) && IsInRange(dataNow, discounts[1]))
            {
                if (dataNow == subscription.Start.Date || dataNow == discounts[0].Start)
                {
                    list.Add(new InvoiceLine());
                    _index++;

                    list[_index].PricePerPeriod = GetPricePerPeriod(subscription, discounts[0].PercentReduction);
                }

                list[_index].Start = list.Count == 1 ? subscription.Start : list[_index - 1].End;
                list[_index].End = dataNow.AddDays(1);
                if (dataNow == discounts[0].End.Date)
                {
                    list[_index].End = discounts[0].End;
                }
            }

            else if (IsInRange(dataNow, discounts[1]))
            {
                if (dataNow == subscription.Start.Date || dataNow == discounts[1].Start || dataNow.AddDays(-1) == discounts[0].End)
                {
                    list.Add(new InvoiceLine());
                    _index++;

                    list[_index].PricePerPeriod = GetPricePerPeriod(subscription, discounts[1].PercentReduction);
                }

                list[_index].Start = list.Count == 1 ? subscription.Start : list[_index - 1].End;
                list[_index].End = dataNow.AddDays(1);
                if (dataNow == discounts[1].End.Date)
                {
                    list[_index].End = discounts[1].End;
                }
            }

            else if (!IsInRange(dataNow, discounts[0]) || IsInRange(dataNow, discounts[0]) && IsInRange(dataNow, discounts[1]))
            {
                if (dataNow == subscription.Start.Date || dataNow.AddDays(-1) == discounts[1].End.Date || dataNow.AddDays(-1) == discounts[0].End.Date)
                {
                    list.Add(new InvoiceLine());
                    _index++;

                    list[_index].PricePerPeriod = list[_index].PricePerPeriod = GetPricePerPeriod(subscription);
                }

                list[_index].Start = list.Count == 1 ? subscription.Start : list[_index - 1].End;
                list[_index].End = dataNow.AddDays(1);

                if (dataNow == finishSubscription.Date || dataNow == subscription.End)
                {
                    list[_index].End = subscription.End ?? finishSubscription;
                }
            }
        }
    }
}
