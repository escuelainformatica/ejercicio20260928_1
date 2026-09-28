using System.Net.Http.Json;
using Libreria1;

namespace Consola1;

public class CarroCompraConsoleApp
{
    private readonly HttpClient _httpClient;

    public CarroCompraConsoleApp(HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? new HttpClient();
    }

    public async Task<CarroCompraConsoleResult> EjecutarAsync(string baseUrl)
    {
        var endpoint = new Uri(new Uri(baseUrl.TrimEnd('/') + "/"), "api/carrocompra");
        var carros = await _httpClient.GetFromJsonAsync<List<CarroCompra>>(endpoint) ?? new List<CarroCompra>();
        var total = carros.Sum(c => c.Cantidad * c.PrecioUnitario);
        var totalConIva = decimal.Round(total * 1.19m, 2);

        return new CarroCompraConsoleResult(total, totalConIva);
    }
}