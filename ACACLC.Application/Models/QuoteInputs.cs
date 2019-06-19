using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace ACACLC.Application.Models
{
    public class QuoteInputs : AbstractValidator<QuoteInputs>
    {
        public List<int> AvailableDurations = new List<int>() { 1, 2, 3 };

        public decimal MinimumDeposit = 0.15M;

        public QuoteInputs()
        {
            RuleFor(x => x.CustomerName).NotNull().WithMessage("Customer must provide a name.");
            RuleFor(x => x.DeliveryDate).NotNull().WithMessage("Customer must provide a delivery date.");
            RuleFor(x => x.DeliveryDate).LessThan(DateTime.Now).WithMessage("Delivery date cannot be in the past.");
            RuleFor(x => x.NumberOfYears).GreaterThan(0).LessThanOrEqualTo(3).WithMessage($"Payment term must be between {AvailableDurations.Min()}-{AvailableDurations.Max()} years");
            RuleFor(x => x.Deposit).Must(BeWithinMinimumDeposit).GreaterThan(0).WithMessage($"Deposit £{Deposit} must be greater than 0 and within {MinimumDepositDisplay}% of the vehicle price £{VehiclePrice}.");
            RuleFor(x => x.ArrangementFee).GreaterThanOrEqualTo(0).WithMessage($"Arrangement fee £{ArrangementFee} cannot be a negative number.");
            RuleFor(x => x.SettlementFee).GreaterThanOrEqualTo(0).WithMessage($"Settlement fee £{SettlementFee} cannot be a negative number.");
            RuleFor(x => x.VehiclePrice).GreaterThanOrEqualTo(0).WithMessage($"Vehicle price £{VehiclePrice} cannot be a negative number.");
        }

        public string CustomerName { get; set; }

        public decimal? VehiclePrice { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public int NumberOfYears { get; set; }

        public decimal? Deposit { get; set; }

        public decimal ArrangementFee { get; set; } = 88.0M;

        public decimal SettlementFee { get; set; } = 20.0M;

        public decimal MinimumDepositDisplay => (MinimumDeposit * 100);

        private bool BeWithinMinimumDeposit(decimal? deposit)
        {
            var minimumDeposit = VehiclePrice * MinimumDeposit;
            return deposit < minimumDeposit;
        }
    }
}
