using ACACLC.Application.Interfaces;
using ACACLC.Application.Models;
using ACACLC.Application.UseCases;
using FluentAssertions;
using Moq;
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

        [Fact]
        public void CompleteAddsCompletedDate()
        {
            // Given

            var mockStorage = new Mock<IStorage>();

            var inputs = new QuoteInputs()
            {
                CustomerName = "Michael Law",
                DeliveryDate = new DateTime(2019, 6, 22),
                NumberOfYearsStr = "1",
                ArrangementFee = 100.0M,
                SettlementFee = 10.0M,
                VehiclePrice = 12000.0M,
                Deposit = 6000.0M
            };
            var calculator = new CalculateQuote(mockStorage.Object);
            var sut = calculator.Calculate(inputs);

            sut.Complete();

            // When

            sut.CompletedDateTime.Should().NotBeNull();
        }
    }
}
