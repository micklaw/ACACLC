using ACACLC.Application.Constants;
using ACACLC.Application.Enums;
using ACACLC.Application.Interfaces;
using ACACLC.Application.Models;
using ACACLC.Application.UseCases;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ACACLC.Tests.Application.UseCases
{
    public class CalculateQuoteTests
    {
        [Fact]
        public void GeneratesCorrectQuoteGivenCorrectInputs()
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

            var sut = new CalculateQuote(mockStorage.Object);

            // When

            var outputs = sut.Calculate(inputs);

            // Then

            outputs.Payments.Count().Should().Be(12);
            outputs.Payments[0].Total.Should().Be(600.0M);
            outputs.Payments[1].Total.Should().Be(500.0M);
            outputs.Payments[2].Total.Should().Be(500.0M);
            outputs.Payments[3].Total.Should().Be(500.0M);
            outputs.Payments[4].Total.Should().Be(500.0M);
            outputs.Payments[5].Total.Should().Be(500.0M);
            outputs.Payments[6].Total.Should().Be(500.0M);
            outputs.Payments[7].Total.Should().Be(500.0M);
            outputs.Payments[8].Total.Should().Be(500.0M);
            outputs.Payments[9].Total.Should().Be(500.0M);
            outputs.Payments[10].Total.Should().Be(500.0M);
            outputs.Payments[11].Total.Should().Be(510.0M);
            outputs.Payments.Sum(i => i.Total).Should().Be(6110.0M);
        }

        [Fact]
        public void GeneratesCorrectDayOfMonthForEachPayment()
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

            var sut = new CalculateQuote(mockStorage.Object);

            // When

            var outputs = sut.Calculate(inputs);

            // Then

            outputs.Payments.Count().Should().Be(12);
            outputs.Payments[0].PaymentDay.Should().Be(DateTime.Parse("Monday, 01 July 2019"));
            outputs.Payments[1].PaymentDay.Should().Be(DateTime.Parse("Monday, 05 August 2019"));
            outputs.Payments[2].PaymentDay.Should().Be(DateTime.Parse("Monday, 02 September 2019"));
            outputs.Payments[3].PaymentDay.Should().Be(DateTime.Parse("Monday, 07 October 2019"));
            outputs.Payments[4].PaymentDay.Should().Be(DateTime.Parse("Monday, 04 November 2019"));
            outputs.Payments[5].PaymentDay.Should().Be(DateTime.Parse("Monday, 02 December 2019"));
            outputs.Payments[6].PaymentDay.Should().Be(DateTime.Parse("Monday, 06 January 2020"));
            outputs.Payments[7].PaymentDay.Should().Be(DateTime.Parse("Monday, 03 February 2020"));
            outputs.Payments[8].PaymentDay.Should().Be(DateTime.Parse("Monday, 02 March 2020"));
            outputs.Payments[9].PaymentDay.Should().Be(DateTime.Parse("Monday, 06 April 2020"));
            outputs.Payments[10].PaymentDay.Should().Be(DateTime.Parse("Monday, 04 May 2020"));
            outputs.Payments[11].PaymentDay.Should().Be(DateTime.Parse("Monday, 01 June 2020"));
        }

        [Fact]
        public void SaveCallsCorrectStorageWhennotNull()
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

            var sut = new CalculateQuote(mockStorage.Object);

            // When

            var outputs = sut.Calculate(inputs);
            sut.Save(outputs);

            // Then

            mockStorage.Verify(i => i.Get<List<Quote>>(Keys.Quotes), Times.Once);
            mockStorage.Verify(i => i.Set(Keys.Quotes, It.IsAny<List<Quote>>()), Times.Once);
        }

        [Fact]
        public void SaveCallsThrowsWhenInputsNull()
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

            var sut = new CalculateQuote(mockStorage.Object);

            // When

            Assert.Throws<ArgumentNullException>(() => sut.Save(null));
        }

        [Fact]
        public void GeneratesCorrectFees()
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

            var sut = new CalculateQuote(mockStorage.Object);

            // When

            var outputs = sut.Calculate(inputs);

            // Then

            outputs.Payments.Count().Should().Be(12);
            outputs.Payments[0].FeeType.Should().Be(Fees.Arrangement);
            outputs.Payments[0].FeeAmount.Should().Be(inputs.ArrangementFee);
            outputs.Payments[11].FeeType.Should().Be(Fees.Settlement);
            outputs.Payments[11].FeeAmount.Should().Be(inputs.SettlementFee);
        }
    }
}
