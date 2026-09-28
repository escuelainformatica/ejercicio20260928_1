using Consola1;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi1;

namespace ConsolaTest1;

public class CarroCompraConsoleAppIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CarroCompraConsoleAppIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task EjecutarAsync_ObtieneElTotalDesdeLaApi()
    {
        var client = _factory.CreateClient();
        var app = new CarroCompraConsoleApp(client);

        var result = await app.EjecutarAsync(client.BaseAddress!.ToString());

        Assert.Equal(11400m, result.Total);
        Assert.Equal(13566m, result.TotalConIva);
    }
}