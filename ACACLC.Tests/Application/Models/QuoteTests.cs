using ACACLC.Application.Models;
using FluentAssertions;
using System;
using Xunit;

namespace ACACLC.Tests
{
    public class QuoteTests
    {
        [Fact]
        public void ThrowsOnNullInput()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Quote(null);
            });
        }

        [Fact]
        public void MapsCorrectFieldOnValidInput()
        {
            var inputs = new QuoteInputs()
            {
                CustomerName = "Mike Law",
                Deposit = 11.11M,
                VehiclePrice = 22.22M,
                DeliveryDate = DateTime.Now
            };

            var quote = new Quote(inputs);

            quote.CustomerName.Should().Be(inputs.CustomerName);
            quote.DeliveryDate.Should().Be(inputs.DeliveryDate);
            quote.VehiclePrice.Should().Be(inputs.VehiclePrice);
            quote.Deposit.Should().Be(inputs.Deposit);
            quote.Id.Should().NotBe(Guid.Empty);
            quote.StartDateTime.Should().NotBe(DateTime.MinValue);
        }
    }
}
