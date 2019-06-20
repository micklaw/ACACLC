using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using FluentValidation.Validators;

namespace ACACLC.Application.Models
{
    public class QuoteInputs : AbstractValidator<QuoteInputs>
    {
        public List<int> AvailableDurations = new List<int>() { 1, 2, 3 };

        public decimal MinimumDeposit = 0.15M;

        public QuoteInputs()
        {
            RuleFor(x => x.CustomerName)
                .NotNull().WithMessage("Customer must provide a name.");

            RuleFor(x => x.DeliveryDate)
                .NotNull().WithMessage("Delivery date must be provided.")
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Delivery date cannot be in the past.");

            RuleFor(x => x.NumberOfYears)
                .NotNull().WithMessage($"Number Of Years must be provided.")
                .GreaterThan(0).WithMessage($"Number Of Years must be greater than 0.")
                .LessThanOrEqualTo(3).WithMessage($"Payment term must be between less than {AvailableDurations.Max()} years");

            RuleFor(x => x.VehiclePrice)
                .NotNull().WithMessage($"Vehicle price cannot be empty.")
                .GreaterThanOrEqualTo(0).WithMessage($"Vehicle price cannot be a negative number.");

            RuleFor(x => x.Deposit);

            RuleFor(x => x.Deposit)
                .NotNull().WithMessage($"Deposit must be populated.")
                .GreaterThanOrEqualTo(0).WithMessage($"Deposit cannot be a negative number.")
                .Custom(IsDepositWithinMinimumRange);

            RuleFor(x => x.ArrangementFee)
                .GreaterThanOrEqualTo(0).WithMessage($"Arrangement fee cannot be a negative number.");

            RuleFor(x => x.SettlementFee)
                .GreaterThanOrEqualTo(0).WithMessage($"Settlement fee cannot be a negative number.");
        }

        public string CustomerName { get; set; }

        public decimal VehiclePrice { get; set; }

        public DateTime DeliveryDate { get; set; } = DateTime.Now;

        public int NumberOfYears { get; set; }

        public string NumberOfYearsStr
        {
            get => this.NumberOfYears.ToString();
            set
            {
                if (int.TryParse(value, out int parsed))
                {
                    NumberOfYears = parsed;
                }
            }
        }

        public decimal NumberOrMonths => NumberOfYears * 12;

        public decimal Deposit { get; set; }

        public decimal ArrangementFee { get; set; } = 88.0M;

        public decimal SettlementFee { get; set; } = 20.0M;

        public decimal MinimumDepositDisplay => (MinimumDeposit * 100);

        public decimal MonthlyPayment => Math.Round((VehiclePrice - Deposit) / NumberOrMonths, 2);

        private void IsDepositWithinMinimumRange(decimal deposit, CustomContext context)
        {
            var inputs = (QuoteInputs)context.InstanceToValidate;

            var key = nameof(Deposit);
            var message = $"Deposit must be greater than {MinimumDepositDisplay}% of the vehicle price";

            var failed = false;

            if (inputs.Deposit <= 0)
            {
                failed = true;
            }

            if (inputs.VehiclePrice <= 0)
            {
                failed = true;
            }

            if (!failed)
            {
                var minimumDeposit = inputs.VehiclePrice * inputs.MinimumDeposit;
                failed = inputs.Deposit < minimumDeposit;
            }

            if (failed)
            {
                context.AddFailure(key, message);
            }
        }
    }
}
