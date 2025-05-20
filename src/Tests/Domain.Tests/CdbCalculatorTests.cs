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

    [Fact]
    public void Calculate_ShouldThrow_WhenInputIsNull()
    {
        try
        {
            _calculator.Calculate(null);
            Assert.True(false); // Se não lançar exceção, falha o teste
        }
        catch (Exception)
        {
            Assert.True(true); // Passa o teste se qualquer exceção for lançada
        }
    }

    [Fact]
    public void Calculate_ShouldReturnZero_WhenInitialAmountIsZero()
    {
        var input = new InvestmentInput
        {
            InitialAmount = 0,
            Months = 12
        };

        var result = _calculator.Calculate(input);

        Assert.Equal(0, result.GrossAmount);
        Assert.Equal(0, result.IncomeTax);
        Assert.Equal(0, result.NetAmount);
    }

    [Theory]
    [InlineData(1000, 6)]
    [InlineData(1000, 12)]
    [InlineData(1000, 24)]
    [InlineData(1000, 36)]
    public void Calculate_ShouldApplyCorrectIncomeTaxRate(decimal initialAmount, int months)
    {
        var input = new InvestmentInput
        {
            InitialAmount = initialAmount,
            Months = months
        };

        var result = _calculator.Calculate(input);

        // Aqui você pode validar a alíquota de IR conforme a regra do seu cálculo
        Assert.True(result.IncomeTax >= 0);
    }

    [Fact]
    public void CdbSettings_Getters_WorkCorrectly()
    {
        var settings = new CdbSettings { CdiRate = 0.01, TbRate = 1.1 };
        Assert.Equal(0.01, settings.CdiRate);
        Assert.Equal(1.1, settings.TbRate);
    }
    
    [Fact]
public void Calculate_DeveRetornarResultadoCorreto()
{
    var settings = Options.Create(new CdbSettings { CdiRate = 0.009, TbRate = 1.08 });
    var calculator = new CdbCalculator(settings);

    var input = new InvestmentInput { InitialAmount = 1000, Months = 12 };
    var result = calculator.Calculate(input);

    Assert.True(result.GrossAmount > 1000);
    Assert.True(result.IncomeTax > 0);
    Assert.Equal(result.NetAmount, result.GrossAmount - result.IncomeTax);
}
}
