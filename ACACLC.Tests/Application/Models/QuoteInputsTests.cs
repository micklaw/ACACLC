using ACACLC.Application.Models;
using FluentAssertions;
using FluentValidation;
using System;
using System.Linq;
using Xunit;

namespace ACACLC.Tests
{
    public class QuoteInputsTests
    {
        [Fact]
        public void CustomerNameMustBePopulated()
        {
            var inputs = new QuoteInputs();
            var results = inputs.Validate(inputs);

            results.IsValid.Should().BeFalse();
            results.Errors.Select(i => i.ErrorMessage).Should().Contain("Customer must provide a name.");
        }

        [Fact]
        public void DeliveryDateCanNotBeInthePast()
        {
            var inputs = new QuoteInputs()
            {
                DeliveryDate = DateTime.Now.AddDays(-1)
            };

            var results = inputs.Validate(inputs);

            results.IsValid.Should().BeFalse();
            results.Errors.Select(i => i.ErrorMessage).Should().Contain("Delivery date cannot be in the past.");
        }
    }
}
