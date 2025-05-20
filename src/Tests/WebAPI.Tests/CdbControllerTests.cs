using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using Xunit;

namespace WebAPI.Tests;

public class CdbControllerTests
{
    [Fact]
    public void Calculate_ShouldReturnOk_WithValidResult()
    {
        var input = new InvestmentInput
        {
            InitialAmount = 1000,
            Months = 12
        };

        var expected = new InvestmentResult
        {
            GrossAmount = 1200,
            IncomeTax = 30
        };

        var mockService = new Mock<ICdbService>();
        mockService.Setup(s => s.CalculateInvestment(input)).Returns(expected);

        var controller = new CdbController(mockService.Object);

        var response = controller.Calculate(input);

        var okResult = Assert.IsType<OkObjectResult>(response);
        var result = Assert.IsType<InvestmentResult>(okResult.Value);

        Assert.Equal(expected.GrossAmount, result.GrossAmount);
        Assert.Equal(expected.IncomeTax, result.IncomeTax);
        Assert.Equal(expected.NetAmount, result.NetAmount);
    }

    [Fact]
    public void Calculate_ShouldReturnBadRequest_WhenInputIsNull()
    {
        var mockService = new Mock<ICdbService>();
        var controller = new CdbController(mockService.Object);

        var response = controller.Calculate(null);

        Assert.IsType<BadRequestResult>(response);
    }

    [Fact]
    public void Calculate_ShouldReturnBadRequest_WhenServiceThrowsArgumentException()
    {
        var input = new InvestmentInput
        {
            InitialAmount = 1000,
            Months = 0
        };

        var mockService = new Mock<ICdbService>();
        mockService.Setup(s => s.CalculateInvestment(input)).Throws<ArgumentException>();

        var controller = new CdbController(mockService.Object);

        var response = controller.Calculate(input);

        Assert.IsType<BadRequestObjectResult>(response);
    }

    [Fact]
    public void Calculate_ShouldReturnStatus500_WhenServiceThrowsUnexpectedException()
    {
        var input = new InvestmentInput
        {
            InitialAmount = 1000,
            Months = 12
        };

        var mockService = new Mock<ICdbService>();
        mockService.Setup(s => s.CalculateInvestment(input)).Throws<Exception>();

        var controller = new CdbController(mockService.Object);

        var response = controller.Calculate(input);

        var result = Assert.IsType<ObjectResult>(response);
        Assert.Equal(500, result.StatusCode);
    }
}