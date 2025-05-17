namespace Domain.Models;

public class InvestmentResult
{
    /// <summary>
    /// Valor bruto do investimento ao final do período (sem desconto de IR)
    /// </summary>
    public decimal GrossAmount { get; set; }

    /// <summary>
    /// Valor do imposto de renda descontado no resgate
    /// </summary>
    public decimal IncomeTax { get; set; }

    /// <summary>
    /// Valor líquido do investimento ao final do período (com desconto de IR)
    /// </summary>
    public decimal NetAmount => GrossAmount - IncomeTax;
}
