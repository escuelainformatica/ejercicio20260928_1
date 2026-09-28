using System.Text.Json;

namespace Libreria1;

public class CarroCompraJsonRepository : ICarroCompraRepository
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    private readonly string _filePath;

    public CarroCompraJsonRepository(string filePath)
    {
        _filePath = filePath;
    }

    public IReadOnlyList<CarroCompra> GetAll()
    {
        EnsureFileExists();

        var json = File.ReadAllText(_filePath);
        var carros = JsonSerializer.Deserialize<List<CarroCompra>>(json, JsonOptions);

        if (carros is null || carros.Count == 0)
        {
            carros = CreateDefaultItems();
            Save(carros);
        }

        return carros;
    }

    private void EnsureFileExists()
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (File.Exists(_filePath))
        {
            return;
        }

        Save(CreateDefaultItems());
    }

    private void Save(IEnumerable<CarroCompra> carros)
    {
        var json = JsonSerializer.Serialize(carros, JsonOptions);
        File.WriteAllText(_filePath, json);
    }

    private static List<CarroCompra> CreateDefaultItems()
    {
        return new List<CarroCompra>
        {
            new() { Id = 1, NombreProducto = "Pan", Cantidad = 2, PrecioUnitario = 1200m },
            new() { Id = 2, NombreProducto = "Leche", Cantidad = 3, PrecioUnitario = 1500m },
            new() { Id = 3, NombreProducto = "Cafe", Cantidad = 1, PrecioUnitario = 4500m }
        };
    }
}