using Microsoft.AspNetCore.Mvc;
using Newtonsoft;
using EmpresaService;
using EmpresaDTO;

namespace EmpresaWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
         EmpresaService.ProductoService _serviceProducto = new EmpresaService.ProductoService();

        [HttpPost]
        public IActionResult AgregarProducto(ProductoDTO producto) {

            if (!ModelState.IsValid)
            { 
                return BadRequest("Complete todos los campos segun corresponda");
            }
            _serviceProducto.AgregarProducto(producto);
            return Ok("Se ha agregado con exito");
        }
        [HttpPut("{cod}")]
        public IActionResult ActualizarProducto(int cod, ProductoDTO producto)
        {
            if (!ModelState.IsValid)
            {
                return NotFound(); 
            }
            var result= _serviceProducto.ActualizarProducto(cod, producto);
            if (result.Success==false)
            {
                return BadRequest(result.Message); 
            }
            return Ok(result.Message); 
        }

        [HttpPatch("actualizar-stock")]
        public IActionResult ActualizarStock ([FromQuery] int codProducto, [FromQuery] int stockActualizado) 
        { 
            var result = _serviceProducto.ActualizarStock(codProducto, stockActualizado);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpDelete("{codProducto}")]

        public IActionResult EliminarProducto (int codProducto)
        {
            var result = _serviceProducto.EliminarProducto(codProducto);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpGet("{codProducto}")]

        public IActionResult ObtenerProductoPorCod(int codProducto)
        { 
            var producto = _serviceProducto.ObtenerProductoPorCod(codProducto);
            if (producto == null)
            {
                return NotFound($"No se encontro un producto con el codigo {codProducto}");
            }
            return Ok(producto);
        }

        [HttpGet]
        public IActionResult ObtenerProductos()
        {
            return Ok(_serviceProducto.ObtenerListadoDePoductos());
        }


    }
}
