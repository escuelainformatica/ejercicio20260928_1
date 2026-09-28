namespace Libreria1;

public interface ICarroCompraRepository
{
    IReadOnlyList<CarroCompra> GetAll();
}