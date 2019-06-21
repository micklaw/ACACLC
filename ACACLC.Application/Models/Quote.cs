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
        }

        public Quote(QuoteInputs inputs)
        {
            if (inputs == null)
            {
                throw new ArgumentNullException(nameof(inputs));
            }

            Id = Guid.NewGuid();
            StartDateTime = DateTime.Now;
            CustomerName = inputs.CustomerName;
            VehiclePrice = inputs.VehiclePrice;
            Deposit = inputs.Deposit;
            DeliveryDate = inputs.DeliveryDate;
        }

        public Guid Id { get; set; }

        public DateTime StartDateTime { get; set; }

        public string CustomerName { get; set; }

        public decimal VehiclePrice { get; set; }

        public decimal Deposit { get; set; }

        public DateTime DeliveryDate { get; set; }

        public IList<QuotePayment> Payments { get; set; } = new List<QuotePayment>();

        public DateTime? CompletedDateTime { get; set; }

        public void Complete()
        {
            CompletedDateTime = DateTime.Now;
        }
    }
}
