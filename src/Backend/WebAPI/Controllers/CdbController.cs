using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Endpoint de cálculo de rendimento de CDB
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CdbController : ControllerBase
{
    private readonly ICdbService _cdbService;

    public CdbController(ICdbService cdbService)
    {
        _cdbService = cdbService;
    }

    /// <summary>
    /// Calcula o rendimento bruto, imposto e líquido de um CDB
    /// </summary>
    /// <param name="input">Parâmetros do investimento</param>
    /// <returns>Resultado do cálculo</returns>
    [HttpPost("calculate")]
    [ProducesResponseType(typeof(InvestmentResult), StatusCodes.Status200OK)]
    public IActionResult Calculate([FromBody] InvestmentInput input)
    {
        var result = _cdbService.CalculateInvestment(input);
        return Ok(result);
    }
}
