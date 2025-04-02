using EmpresaDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using EmpresaEntities;
using EmpresaData;

namespace EmpresaService
{
    public class ClienteService
    {

        public Resultado AgregarCliente(ClienteDTO clienteDTO)
        {
            Resultado result= new Resultado();
            if (ClienteFile.LeerClienteDesdeJson().Exists(x=>x.DniCliente==clienteDTO.DniCliente))
            {
                result.Success=false;
                result.Message="El cliente con ese dni ya existe"; 
                return result; 
            }
            Cliente clienteConvertido = Conversion.ConvertirDTOACliente(clienteDTO);
            clienteConvertido.FechaCreacion = DateTime.Now;
            ClienteFile.EscribirClienteAJson(clienteConvertido);
            result.Success = true;
            result.Message = "El cliente se creo con exito";
            return result;

        }

        public Resultado EditarCliente(ClienteDTO clienteDTO, int dni)
        {
            Resultado resultado = new Resultado();

           Cliente clienteActualizado = ClienteFile.LeerClienteDesdeJson().FirstOrDefault(x=>x.DniCliente==dni);
            if (clienteActualizado == null)
            {
                resultado.Message = $"No se ha encontrado el cliente";
                resultado.Success = false;
                return resultado;
            }

            clienteActualizado.LongitudCliente= clienteDTO.LongitudCliente;
            clienteActualizado.DniCliente= clienteDTO.DniCliente;
            clienteActualizado.LatitudCliente=clienteDTO.LatitudCliente;
            clienteActualizado.EmailCliente= clienteDTO.EmailCliente;
            clienteActualizado.ApellidoCliente= clienteDTO.ApellidoCliente;
            clienteActualizado.NombreCliente= clienteDTO.NombreCliente;
            clienteActualizado.FechaNacimientoCliente= clienteDTO.FechaNacimientoCliente;
            clienteActualizado.TelefonoCliente= clienteDTO.TelefonoCliente;
            clienteActualizado.FechaActualizacion = DateTime.Now;
            ClienteFile.EscribirClienteAJson(clienteActualizado);
            resultado.Message = "El cliente se edito con exito";
            resultado.Success = true;
            return resultado;
        }

        public Resultado EliminarCliente(int dniCliente)
        {
            Resultado resultado = new Resultado();
            Cliente clienteEliminar = ClienteFile.LeerClienteDesdeJson().FirstOrDefault(x=>x.DniCliente==dniCliente);
            if (clienteEliminar == null)
            {
                resultado.Success = false;
                resultado.Message = $"No se ha encontrado el cliente con el dni {dniCliente}";
                return resultado;
            }
            clienteEliminar.FechaEliminacion = DateTime.Now;
            ClienteFile.EscribirClienteAJson(clienteEliminar);
            resultado.Message = "El cliente se elimino con exito";

            resultado.Success = true;
            return resultado;
        }

        public ClienteDTO ObtenerClientePorDni(int dniCliente)
        {
            var cliente= ClienteFile.LeerClienteDesdeJson().FirstOrDefault(x=>x.DniCliente==dniCliente);
            if (cliente == null)
            {
                return null; 
            }
            var clienteConvert= Conversion.ConvertirClienteADTO(cliente);
            return clienteConvert;
        }

        public List<ClienteDTO> ObtenerClientes()
        {
            List<Cliente> clientesSinConvertir = ClienteFile.LeerClienteDesdeJson();
            List<ClienteDTO> clientesConvertidos = new List<ClienteDTO>();
            foreach ( Cliente cliente in clientesSinConvertir)
            {
               clientesConvertidos.Add(Conversion.ConvertirClienteADTO(cliente));
            }
            return clientesConvertidos;
        }
    }
}
