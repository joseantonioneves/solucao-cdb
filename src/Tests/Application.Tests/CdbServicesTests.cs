using Application.Services;
using Application.Configuration;
using Domain.Interfaces;
using Domain.Models;
using Moq;
using Xunit;
using Microsoft.Extensions.Options;

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

// ===========================
//  Testes para CdbCalculator
// ===========================

public class CdbCalculatorTests
{
    [Fact]
    public void Calculate_ReturnsCorrectResult_ForValidInput()
    {
        var settings = new CdbSettings { CdiRate = 13.65, TbRate = 1.08 };
        var calculator = new CdbCalculator(Options.Create(settings));

        var input = new InvestmentInput
        {
            InitialAmount = 1000,
            Months = 12
        };

        var result = calculator.Calculate(input);

        Assert.NotNull(result);
        Assert.True(result.GrossAmount > 0);
        Assert.True(result.IncomeTax >= 0);
        Assert.Equal(result.GrossAmount - result.IncomeTax, result.NetAmount);
    }

    [Fact]
    public void Calculate_ReturnsZeroAmounts_WhenInitialAmountIsZero()
    {
        var settings = new CdbSettings { CdiRate = 13.65, TbRate = 1.08 };
        var calculator = new CdbCalculator(Options.Create(settings));

        var input = new InvestmentInput
        {
            InitialAmount = 0,
            Months = 12
        };

        var result = calculator.Calculate(input);

        Assert.Equal(0, result.GrossAmount);
        Assert.Equal(0, result.IncomeTax);
        Assert.Equal(0, result.NetAmount);
    }
    /* ---------- Não Faz Sentido para a regra de negócios -----------
    [Fact]
    public void Calculate_ReturnsInitialAmount_WhenMonthsIsZero()
    {
        var settings = new CdbSettings { CdiRate = 13.65, TbRate = 1.08 };
        var calculator = new CdbCalculator(Options.Create(settings));

        var input = new InvestmentInput
        {
            InitialAmount = 1000,
            Months = 0
        };

        var result = calculator.Calculate(input);

        Assert.Equal(1000, result.GrossAmount);
        Assert.True(result.IncomeTax >= 0);
        Assert.Equal(result.GrossAmount - result.IncomeTax, result.NetAmount);
    }
    
    [Fact]
    public void Calculate_ThrowsArgumentException_WhenMonthsIsZero()
    {
        var settings = new CdbSettings { CdiRate = 13.65, TbRate = 1.08 };
        var calculator = new CdbCalculator(Options.Create(settings));

        var input = new InvestmentInput
        {
            InitialAmount = 1000,
            Months = 0
        };

        Assert.Throws<ArgumentException>(() => calculator.Calculate(input));
    }
    */
    [Fact]
    public void CdbSettings_Getters_WorkCorrectly()
    {
        var settings = new CdbSettings { CdiRate = 12.5, TbRate = 1.1 };
        Assert.Equal(12.5, settings.CdiRate);
        Assert.Equal(1.1, settings.TbRate);
    }
}