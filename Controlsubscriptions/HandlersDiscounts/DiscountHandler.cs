using System;
using System.Collections.Generic;

namespace Controlsubscriptions
{
    internal abstract class DiscountHandler:IComparer<Discount>
    {
        internal abstract void RegisteringInvoicePeriodsByDate(DateTime dataNow, List<InvoiceLine> list, List<Discount> discounts,
            Subscription subscription, DateTime finishSubscription);
         
        internal decimal GetPricePerPeriod(Subscription subscription, decimal percentReduction = 0)
        {
            return subscription.PricePerPeriod - (subscription.PricePerPeriod * percentReduction / 100);
        }

        internal void RegistrationTotalResultsForPeriod(List<InvoiceLine> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Duration = Math.Round(Convert.ToDecimal((list[i].End - list[i].Start).TotalDays), 2);
                list[i].Total = list[i].PricePerPeriod * list[i].Duration;
            }
        }

        public int Compare(Discount o1, Discount o2)
        {
            if (o1.PercentReduction > o2.PercentReduction)
            {
                return -1;
            }

            if (o1.PercentReduction < o2.PercentReduction)
            {
                return 1;
            }

            return 0;
        }

        internal bool IsInRange(DateTime dateToCheck, Discount discount)
        {
            return dateToCheck >= discount.Start && dateToCheck <= discount.End;
        }
    }
}
