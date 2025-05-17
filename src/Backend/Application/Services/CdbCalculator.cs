using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Options;
using Application.Configuration;

namespace Application.Services;

/// <summary>
/// Implementação da lógica de cálculo de CDB conforme fórmula composta mensal
/// </summary>
public class CdbCalculator : ICdbCalculator
{
    private readonly decimal _cdiRate;
    private readonly decimal _tbRate;

    public CdbCalculator(IOptions<CdbSettings> settings)
    {
        _cdiRate = (decimal)settings.Value.CdiRate; // Exemplo: 0.0135 (1.35% ao mês)
        _tbRate = (decimal)settings.Value.TbRate;   // Exemplo: 1.035 (103.5%)
    }

    /* --- versão 0.0.1a -----
    private const decimal FixedCdiRate = 0.009m; // 0.9%
    private const decimal FixedTbRate = 1.08m;   // 108%
    */

    public InvestmentResult Calculate(InvestmentInput input)
    {
        //var totalMonths = ((input.EndDate.Year - input.StartDate.Year) * 12) + input.EndDate.Month - input.StartDate.Month;
        if (input.Months < 1)
            throw new ArgumentException("O prazo do investimento deve ser de pelo menos 1 mês.");

        decimal currentValue = input.InitialAmount;
        decimal monthlyRate = _cdiRate * _tbRate;

        for (int i = 0; i < input.Months; i++)
        {
            currentValue *= (1 + monthlyRate);
        }

        decimal grossAmount = Math.Round(currentValue, 2);
        decimal taxRate = GetIncomeTaxRate(input.Months);
        decimal taxAmount = Math.Round((grossAmount - input.InitialAmount) * taxRate, 2);

        return new InvestmentResult
        {
            GrossAmount = grossAmount,
            IncomeTax = taxAmount
        };
    }

    private decimal GetIncomeTaxRate(int months)
    {
        if (months <= 6) return 0.225m;
        if (months <= 12) return 0.20m;
        if (months <= 24) return 0.175m;
        return 0.15m;
    }
}
