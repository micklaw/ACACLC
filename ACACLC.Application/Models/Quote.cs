using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace ACACLC.Application.Models
{
    public class Quote
    {
        public Quote()
        {
            Id = Guid.NewGuid();
            StartDateTime = DateTime.Now;
        }

        public Guid Id { get; set; }

        public DateTime StartDateTime { get; set; }

        public IList<QuotePayment> Payments { get; set; } = new List<QuotePayment>();

        public DateTime CompletedDateTime { get; set; }

        public void Complete()
        {
            CompletedDateTime = DateTime.Now;
        }
    }
}
