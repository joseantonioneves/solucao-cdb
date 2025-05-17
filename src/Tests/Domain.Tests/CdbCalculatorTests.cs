using Domain.Models;
using Application.Configuration;
using Application.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace Domain.Tests;

public class CdbCalculatorTests
{
    private readonly CdbCalculator _calculator;

    public CdbCalculatorTests()
    {
        var settings = Options.Create(new CdbSettings
        {
            CdiRate = 0.009,   // 0.9%
            TbRate = 1.08      // 108%
        });

        _calculator = new CdbCalculator(settings);
    }

    [Theory]
    [InlineData(1000, 6)]
    [InlineData(1000, 12)]
    [InlineData(1000, 24)]
    [InlineData(1000, 36)]
    public void Calculate_ShouldReturn_ValidResults(decimal initialAmount, int months)
    {
        var input = new InvestmentInput
        {
            InitialAmount = initialAmount,
            Months = months
        };

        var result = _calculator.Calculate(input);

        Assert.True(result.GrossAmount > initialAmount);
        Assert.True(result.IncomeTax > 0);
        Assert.Equal(result.NetAmount, result.GrossAmount - result.IncomeTax);
    }

    [Fact]
    public void Calculate_ShouldThrow_WhenMonthsIsZero()
    {
        var input = new InvestmentInput
        {
            InitialAmount = 1000,
            Months = 0
        };

        Assert.Throws<ArgumentException>(() => _calculator.Calculate(input));
    }
}
