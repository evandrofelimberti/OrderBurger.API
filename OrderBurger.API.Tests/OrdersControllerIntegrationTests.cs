using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using OrderBurger.API.Enums;

namespace OrderBurger.API.Tests.Integration;

public class OrdersControllerIntegrationTests : IClassFixture<IntegrationTestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OrdersControllerIntegrationTests(IntegrationTestWebApplicationFactory factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });
    }

    [Fact]
    public async Task Post_Order_DeveRetornar201_EContratoBasico()
    {
        // Arrange
        var sandwichId = await CriarProdutoAsync("XB-01", "X Burger Bacon", 30m, CategoryEnum.Sandwich);
        var sodaId = await CriarProdutoAsync("SD-01", "Coca-Cola", 10m, CategoryEnum.Soda);

        var orderPayload = new
        {
            customerName = "Cristiano Ronaldo",
            items = new[]
            {
                new { productId = sandwichId, quantity = 1m },
                new { productId = sodaId, quantity = 1m }
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/orders", orderPayload);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        Assert.NotNull(response.Headers.Location);

        var json = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        root.GetProperty("id").GetGuid().Should().NotBeEmpty();
        root.GetProperty("customerName").GetString().Should().Be("Cristiano Ronaldo");
        root.GetProperty("subTotal").GetDecimal().Should().BeGreaterThan(0);
        root.GetProperty("discount").GetDecimal().Should().BeGreaterThanOrEqualTo(0);
        root.GetProperty("total").GetDecimal().Should().BeGreaterThan(0);
        root.GetProperty("status").GetInt32().Should().Be((int)OrderStatus.None);

        var items = root.GetProperty("items");
        items.GetArrayLength().Should().Be(2);
    }

    private async Task<Guid> CriarProdutoAsync(string code, string name, decimal price, CategoryEnum category)
    {
        var payload = new
        {
            code ,
            description = $"Descrição {name}",
            name,
            price,
            category = (int)category
        };

        var response = await _client.PostAsJsonAsync("/api/v1/product", payload);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var json = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(json);
        return document.RootElement.GetProperty("id").GetGuid();
    }
}
