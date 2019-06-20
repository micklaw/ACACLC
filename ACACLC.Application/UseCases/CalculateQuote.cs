using System;
using System.Collections.Generic;
using System.Linq;
using ACACLC.Application.Constants;
using ACACLC.Application.Enums;
using ACACLC.Application.Interfaces;
using ACACLC.Application.Models;

namespace ACACLC.Application.UseCases
{
    public class CalculateQuote
    {
        private readonly IStorage storage;

        public CalculateQuote(IStorage storage)
        {
            this.storage = storage;
        }

        public Quote Calculate(QuoteInputs inputs)
        {
            if (inputs == null)
            {
                throw new ArgumentNullException(nameof(inputs));
            }

            var quote = new Quote();

            var lastDate = inputs.DeliveryDate;

            for (var i = 1; i <= inputs.NumberOrMonths; i++)
            {
                lastDate = new DateTime(lastDate.Year, lastDate.Month, 1);
                lastDate = lastDate.AddMonths(1);

                while (lastDate.DayOfWeek != DayOfWeek.Monday)
                {
                    lastDate = lastDate.AddDays(1);
                }

                quote.Payments.Add(new QuotePayment()
                {
                    Amount = inputs.MonthlyPayment,
                    PaymentDay = lastDate,
                    FeeAmount = i == 1 ? inputs.ArrangementFee : i == inputs.NumberOrMonths ? inputs.SettlementFee : (decimal?)null,
                    FeeType = i == 1 ? Fees.Arrangement : i == inputs.NumberOrMonths ? Fees.Settlement : (Fees?)null,
                });
            }

            return quote;
        }

        public void Save(Quote quote)
        {
            if (quote == null)
            {
                throw new ArgumentNullException(nameof(quote));
            }

            var quotes = storage.Get<List<Quote>>(Keys.Quotes) ?? new List<Quote>();

            quotes.Add(quote);

            storage.Set(Keys.Quotes, quotes.OrderByDescending(i => i.CompletedDateTime).ToList());
        }
    }
}
