using Domain.Models;

namespace Application.Interfaces;

/// <summary>
/// Interface de orquestração do serviço de cálculo de CDB
/// </summary>
public interface ICdbService
{
    /// <summary>
    /// Executa o cálculo de investimento de CDB
    /// </summary>
    InvestmentResult CalculateInvestment(InvestmentInput input);
}
