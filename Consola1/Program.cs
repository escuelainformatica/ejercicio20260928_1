using Consola1;

var baseUrl = args.Length > 0 ? args[0] : "http://localhost:5500";
var app = new CarroCompraConsoleApp();
var result = await app.EjecutarAsync(baseUrl);

Console.WriteLine($"Total carro de compras: {result.Total}");
Console.WriteLine($"Total con IVA: {result.TotalConIva}");
