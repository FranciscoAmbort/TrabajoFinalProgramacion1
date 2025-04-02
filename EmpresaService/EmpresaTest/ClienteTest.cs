using EmpresaData;
using EmpresaDTO;
using EmpresaEntities;
using EmpresaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaTest
{
    public class ClienteTest
    {
        public class Tests
        {
            ClienteService _service = new ClienteService();
            Random random = new Random();
            public Tests()
            {
                _service = new ClienteService();
                random = new Random();
            }

            [SetUp]
            public void Setup()
            {
               
            }

            [Test]
            public void EditarClienteInexistente()
            {
                //Arrange
                ClienteDTO cliente= new ClienteDTO()
                {
                    
                    DniCliente= random.Next(40000000,50000000),
                    NombreCliente= "Matt",
                    ApellidoCliente= "Caste",
                    EmailCliente= "matt@gmail.com" ,
                    FechaNacimientoCliente=new DateTime(2004,10,10),
                    TelefonoCliente= 15672280, 
                    LatitudCliente=-20000,
                    LongitudCliente=-15000
                };
                var result = _service.EditarCliente(cliente,cliente.DniCliente ); 
                Assert.That(result.Success, Is.False);

            }

            [Test]
            public void AgregarCliente()
            {
                //Arrange
                ClienteDTO cliente = new ClienteDTO()
                {

                    DniCliente = 46954954,
                    NombreCliente = "Fran",
                    ApellidoCliente = "Caste",
                    EmailCliente = "matt@gmail.com",
                    FechaNacimientoCliente = new DateTime(2004, 10, 10),
                    TelefonoCliente = 15672280,
                    LatitudCliente = -20000,
                    LongitudCliente = -15000
                };
                int cont = ClienteFile.LeerClienteDesdeJson().Count();
                var result = _service.AgregarCliente(cliente);
                Assert.That((cont+1).Equals(ClienteFile.LeerClienteDesdeJson().Count()));

            }
            [Test]
            public void EliminarClienteInexistente()
            {
                int dni = 100;
                var result = _service.EliminarCliente(dni); 
                Assert.That(result.Success is false);
            }
            
        }
    }
}
