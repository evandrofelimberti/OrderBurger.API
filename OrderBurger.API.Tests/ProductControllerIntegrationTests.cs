using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using OrderBurger.API.Enums;

namespace OrderBurger.API.Tests.Integration;

public class ProductControllerIntegrationTests : IClassFixture<IntegrationTestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductControllerIntegrationTests(IntegrationTestWebApplicationFactory factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });
    }

    [Fact]
    public async Task Post_Create_Product_Return_201()
    {
        // Arrange
        var payload = new
        {
            code = "XB-01",
            description = "X Burger bacon",
            name = "Hambúrguer bacon",
            price = 25.90m,
            category = (int)CategoryEnum.Sandwich
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/product", payload);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        Assert.NotNull(response.Headers.Location);

        var json = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        root.TryGetProperty("id", out var idProp).Should().BeTrue();
        idProp.GetGuid().Should().NotBeEmpty();

        root.GetProperty("code").GetString().Should().Be("XB-01");
        root.GetProperty("name").GetString().Should().Be("Hambúrguer bacon");
        root.GetProperty("description").GetString().Should().Be("X Burger bacon");
        root.GetProperty("price").GetDecimal().Should().Be(25.90m);
        root.GetProperty("category").GetInt32().Should().Be((int)CategoryEnum.Sandwich);
    }
}
