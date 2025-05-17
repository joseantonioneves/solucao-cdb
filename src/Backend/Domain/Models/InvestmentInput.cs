namespace Domain.Models;

public class InvestmentInput
{
    /// <summary>
    /// Valor investido inicial (em R$)
    /// </summary>
    public decimal InitialAmount { get; set; }

    /// <summary>
    /// Prazo em meses para o resgate do investimento
    /// </summary>
    public int Months { get; set; }
}
