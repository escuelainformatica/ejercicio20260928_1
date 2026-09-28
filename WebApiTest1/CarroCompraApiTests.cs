using System.Net;
using System.Net.Http.Json;
using Libreria1;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi1;

namespace WebApiTest1;

public class CarroCompraApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CarroCompraApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetCarroCompra_ReturnsExpectedItems()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/carrocompra");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var carros = await response.Content.ReadFromJsonAsync<List<CarroCompra>>();

        Assert.NotNull(carros);
        Assert.Equal(3, carros!.Count);
        Assert.Equal("Pan", carros[0].NombreProducto);
    }
}