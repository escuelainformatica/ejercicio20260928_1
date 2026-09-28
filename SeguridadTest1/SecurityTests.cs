using System.Net;
using System.Net.Http.Json;
using Consola1;
using Libreria1;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApi1;

namespace SeguridadTest1;

public class SecurityTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public SecurityTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Api_NoExponeSecretosNiRutasInternas()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/carrocompra");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("application/json", response.Content.Headers.ContentType!.MediaType);
        Assert.DoesNotContain("password", body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("connectionstring", body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("stack trace", body, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("carro.json", body, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Consola_RechazaUrlBaseMalformada()
    {
        var app = new CarroCompraConsoleApp();

        await Assert.ThrowsAsync<UriFormatException>(() => app.EjecutarAsync("::malformed-url::"));
    }

    [Fact]
    public async Task Consola_TrataComoTextoContenidoNoConfiableYCalculaTotal()
    {
        var handler = new FakeHttpMessageHandler("[{'Id':1,'NombreProducto':'<script>alert(1)</script>','Cantidad':2,'PrecioUnitario':1000}]".Replace('\'', '"'));
        var client = new HttpClient(handler) { BaseAddress = new Uri("http://localhost/") };
        var app = new CarroCompraConsoleApp(client);

        var result = await app.EjecutarAsync(client.BaseAddress!.ToString());

        Assert.Equal(2000m, result.Total);
        Assert.Equal(2380m, result.TotalConIva);
    }

    private sealed class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _content;

        public FakeHttpMessageHandler(string content)
        {
            _content = content;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_content)
            };

            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return Task.FromResult(response);
        }
    }
}