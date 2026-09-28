using Libreria1;

namespace ConsolaTest1;

public class CarroCompraServiceTests
{
    [Fact]
    public void ObtenerTotal_CalculaLaSumaCorrecta()
    {
        var repository = new FakeCarroCompraRepository(new[]
        {
            new CarroCompra { Id = 1, NombreProducto = "Pan", Cantidad = 2, PrecioUnitario = 1200m },
            new CarroCompra { Id = 2, NombreProducto = "Leche", Cantidad = 3, PrecioUnitario = 1500m }
        });

        var service = new CarroCompraService(repository);

        var total = service.ObtenerTotal();

        Assert.Equal(6900m, total);
    }

    [Fact]
    public void ObtenerTotalConIva_AplicaElFactorCorrecto()
    {
        var repository = new FakeCarroCompraRepository(new[]
        {
            new CarroCompra { Id = 1, NombreProducto = "Cafe", Cantidad = 1, PrecioUnitario = 1000m }
        });

        var service = new CarroCompraService(repository);

        var totalConIva = service.ObtenerTotalConIva();

        Assert.Equal(1190m, totalConIva);
    }

    private sealed class FakeCarroCompraRepository : ICarroCompraRepository
    {
        private readonly IReadOnlyList<CarroCompra> _carros;

        public FakeCarroCompraRepository(IEnumerable<CarroCompra> carros)
        {
            _carros = carros.ToList();
        }

        public IReadOnlyList<CarroCompra> GetAll()
        {
            return _carros;
        }
    }
}