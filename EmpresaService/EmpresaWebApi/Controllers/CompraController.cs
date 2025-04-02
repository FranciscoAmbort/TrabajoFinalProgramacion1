using EmpresaDTO;
using EmpresaService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaWebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CompraController : Controller
    {
        private CompraService _serviceCompra;
        public CompraController()
        {
            _serviceCompra = new CompraService();
        }
        [HttpPost]

        public IActionResult AgregarCompra([FromBody] CompraDTO compraNew)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Complete todos los campos segun corresponda");
            }
            var result = _serviceCompra.AgregarCompra(compraNew);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet("{cod}")]
        public IActionResult ObtenerCompraPorCod(int cod)
        {
            var compra = _serviceCompra.ObtenerCompraPorCod(cod);
            if (compra == null)
            {
                return NotFound($"No se encontro una compra con el Código: {cod}");
            }
            else
            {
                return Ok(compra);
            }
        }
        [HttpGet]
        public IActionResult ObtenerCompras()
        {
            var compras = _serviceCompra.ObtenerCompras();
            return Ok(compras); 
        }

        [HttpDelete("{cod}")]
        public IActionResult EliminarCompra(int cod)
        {
            var result = _serviceCompra.EliminarCompra(cod);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
        [HttpPut("{cod}")]
        public IActionResult ActualizarCompra(CompraDTO compra, int cod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Complete todos los campos segun corresponda"); 
            }
            var result = _serviceCompra.ActualizarCompra(compra, cod);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return NotFound(result.Message); 
            }
        }
    }
}
