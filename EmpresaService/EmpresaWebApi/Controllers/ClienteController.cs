using EmpresaDTO;
using EmpresaEntities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft;

namespace EmpresaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        EmpresaService.ClienteService _serviceCliente = new EmpresaService.ClienteService();

        [HttpGet]
        public IActionResult ObtenerClientes()
        {

            return Ok(_serviceCliente.ObtenerClientes());
        }

        // GET api/<ValuesController>/5
        [HttpGet("{dni}")]
        public IActionResult ObtenerClientePorDNI(int dni)
        {
            ClienteDTO clienteDTO = _serviceCliente.ObtenerClientePorDni(dni);
            if (clienteDTO == null)
            {
                return NotFound($"No se encontro un cliente con el dni: {dni}");
            }
            return Ok(clienteDTO);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult AgregarCliente([FromBody] ClienteDTO clienteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Complete todos los campos segun corresponda");
            }
               var result= _serviceCliente.AgregarCliente(clienteDTO);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult EditarCliente(int id, [FromBody] ClienteDTO clienteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Resultado result = _serviceCliente.EditarCliente(clienteDTO, id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return NotFound(result.Message);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult EliminarCliente(int id)
        {
            Resultado result = _serviceCliente.EliminarCliente(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return NotFound(result.Message);
        }
    }
}
