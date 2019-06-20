using ACACLC.Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACACLC.Application.Models
{
    public class QuotePayment
    {
        public DateTime PaymentDay { get; set; }

        public decimal Amount { get; set; }

        public decimal? FeeAmount { get; set; }

        public decimal Total => Amount + (FeeAmount ?? 0);

        public Fees? FeeType { get; set; }
    }
}
