namespace Libreria1;

public class CarroCompraService : ICarroCompraService
{
    private readonly ICarroCompraRepository _repository;

    public CarroCompraService(ICarroCompraRepository repository)
    {
        _repository = repository;
    }

    public IReadOnlyList<CarroCompra> ObtenerCarrosCompra()
    {
        return _repository.GetAll();
    }

    public decimal ObtenerTotal()
    {
        return ObtenerCarrosCompra().Sum(c => c.Cantidad * c.PrecioUnitario);
    }

    public decimal ObtenerTotalConIva(decimal tasaIva = 1.19m)
    {
        return decimal.Round(ObtenerTotal() * tasaIva, 2);
    }
}