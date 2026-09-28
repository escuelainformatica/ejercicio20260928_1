using Libreria1;
using Microsoft.AspNetCore.Mvc;

namespace WebApi1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarroCompraController : ControllerBase
{
    private readonly ICarroCompraService _service;

    public CarroCompraController(ICarroCompraService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<CarroCompra>> Get()
    {
        return Ok(_service.ObtenerCarrosCompra());
    }
}