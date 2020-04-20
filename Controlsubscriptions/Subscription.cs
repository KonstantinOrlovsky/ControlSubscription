using System;

namespace Controlsubscriptions
{
    internal class Subscription
    {
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public decimal PricePerPeriod { get; set; }
    }
}
