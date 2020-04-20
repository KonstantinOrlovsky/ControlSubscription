using System;
using System.Collections.Generic;
using Controlsubscriptions.HandlersDiscounts;

namespace Controlsubscriptions
{
        /*
         Определяем обработчика исходя из количества скидок в подписке
         Заранее сортируем список List<Discount> по убыванию скидки
         Проходим по каждому дню от начала до конца подписки
        */

    internal class BillingHelper
    {   
        internal static IEnumerable<InvoiceLine> BillSubscriptionWithDiscounts(Subscription subscription,
            List<Discount> discounts, DateTime billingEnd)
        {
            var finishHandler = new List<DiscountHandler>();
            switch (discounts.Count)
            {
                case 0:
                    finishHandler.Add(new NoneDiscount());
                    break;
                case 1:
                    finishHandler.Add(new OneDiscount());
                    break;
                case 2:
                    finishHandler.Add(new TwoDiscounts());
                    break;
                case 3:
                    finishHandler.Add(new ThreeDiscounts());
                    break;
            }

            var billList = new List<InvoiceLine>();

            IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
            {
                for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                    yield return day;
            }

            discounts.Sort(finishHandler[0]);

            foreach (DateTime day in EachDay(subscription.Start, subscription.End ?? billingEnd))
            {
                finishHandler[0].RegisteringInvoicePeriodsByDate(day, billList, discounts, subscription, billingEnd);
            }
            
            finishHandler[0].RegistrationTotalResultsForPeriod(billList);

            return billList;
        }

        
    }
}
