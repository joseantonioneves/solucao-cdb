using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

/// <summary>
/// Orquestrador da lógica de negócio, intermediando uso do CdbCalculator
/// </summary>
public class CdbService : ICdbService
{
    private readonly ICdbCalculator _calculator;

    public CdbService(ICdbCalculator calculator)
    {
        _calculator = calculator;
    }

    public InvestmentResult CalculateInvestment(InvestmentInput input)
    {
        return _calculator.Calculate(input);
    }
}
