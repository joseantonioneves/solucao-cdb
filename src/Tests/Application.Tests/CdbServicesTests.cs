using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using Xunit;

namespace Application.Tests;

public class CdbServiceTests
{
    private readonly Mock<ICdbCalculator> _mockCalculator;
    private readonly CdbService _service;

    public CdbServiceTests()
    {
        _mockCalculator = new Mock<ICdbCalculator>();
        _service = new CdbService(_mockCalculator.Object);
    }

    [Fact]
    public void CalculateInvestment_ShouldInvokeCalculator()
    {
        var input = new InvestmentInput
        {
            InitialAmount = 1000,
            Months = 12
        };

        var expected = new InvestmentResult
        {
            GrossAmount = 1200,
            IncomeTax = 20
        };

        _mockCalculator.Setup(x => x.Calculate(input)).Returns(expected);

        var result = _service.CalculateInvestment(input);

        Assert.Equal(expected.GrossAmount, result.GrossAmount);
        Assert.Equal(expected.IncomeTax, result.IncomeTax);
        Assert.Equal(expected.NetAmount, result.NetAmount);

        _mockCalculator.Verify(x => x.Calculate(input), Times.Once);
    }
}