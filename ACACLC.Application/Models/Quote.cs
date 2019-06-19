using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace ACACLC.Application.Models
{
    public class Quote
    {
        public DateTime StarTime;

        public Quote()
        {
            Id = Guid.NewGuid();
            StarTime = DateTime.Now;
        }

        public Guid Id { get; set; }

        public QuoteInputs Inputs { get; set; }

        public DateTime CompletedTime { get; set; }

        public IList<QuoteYear> Years { get; set; } = new List<QuoteYear>();

        public void Calculate()
        {

        }
    }
}
