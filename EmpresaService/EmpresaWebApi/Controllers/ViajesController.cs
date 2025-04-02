using EmpresaDTO;
using EmpresaEntities;
using EmpresaService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmpresaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajesController : ControllerBase
    {
        private ViajeService _ServiceViaje = new ViajeService();
        public ViajesController() 
        {
            _ServiceViaje = new ViajeService(); 
        }
        [HttpPost]
        public IActionResult CrearViaje(ViajeDTO viajeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Complete todos los campos segun corresponda");
            }
            var result = _ServiceViaje.AgregarViaje(viajeDTO);

            if (result.Success == false)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);

        }
        [HttpPut("{cod}")]
        public IActionResult ActualizarViaje(int cod, ViajeDTO viaje)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Complete todos los campos segun corresponda");
            }
            var result= _ServiceViaje.ActualizarViaje(cod, viaje);
            if (result.Success == false) { 
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpDelete("{cod}")]
        public IActionResult EliminarViaje(int cod)
        {
            var result= _ServiceViaje.EliminarViaje(cod);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return NotFound(result.Message);
            }

        }
        [HttpGet("{id}")]
        public IActionResult ObtenerViajePorId(int id)
        {
            var viaje = _ServiceViaje.ObtenerViajePorId(id);
            if (viaje== null)
            {
                return NotFound($"No se encontro un viaje con el id: {id}"); 
            }
            return Ok(viaje);
        }
        [HttpGet]
        public IActionResult ObtenerListadoViajes()
        {
            var viajes= _ServiceViaje.ObtenerViajes();
            return Ok(viajes);
        } 

    }
}
