using System;

namespace Controlsubscriptions
{
    internal class InvoiceLine
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal PricePerPeriod { get; set; }
        public decimal Duration { get; set; } // in periods
        public decimal Total { get; set; }
    }
}
