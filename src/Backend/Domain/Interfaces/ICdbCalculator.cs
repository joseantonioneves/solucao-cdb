using Domain.Models;

namespace Domain.Interfaces;

public interface ICdbCalculator
{
    /// <summary>
    /// Calcula o rendimento de um CDB com base nos parâmetros de entrada.
    /// </summary>
    /// <param name="input">Parâmetros do investimento</param>
    /// <returns>Resultado com valores brutos, imposto e líquidos</returns>
    InvestmentResult Calculate(InvestmentInput input);
}
