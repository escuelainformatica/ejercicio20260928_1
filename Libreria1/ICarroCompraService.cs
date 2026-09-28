namespace Libreria1;

public interface ICarroCompraService
{
    IReadOnlyList<CarroCompra> ObtenerCarrosCompra();

    decimal ObtenerTotal();

    decimal ObtenerTotalConIva(decimal tasaIva = 1.19m);
}