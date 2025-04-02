using EmpresaData;
using EmpresaDTO;
using EmpresaEntities;
using EmpresaFile;
using EmpresaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaTest
{
    public class ViajeTest
    {
        public class Tests
        {
            ViajeService _service = new ViajeService();
            public Tests()
            {
                _service = new ViajeService();

            }

            [SetUp]
            public void Setup()
            {
            }


            [Test]
            public void AgregarViaje()
            {
                //ARRANGE
                ViajeDTO viajeDTO = new ViajeDTO
                {
                    FechaDeEntregaDesde = DateTime.Now.AddDays(1),
                    FechaEntregaHasta = DateTime.Now.AddDays(4)
                };
                CompraDTO compraDTO = new CompraDTO
                {
                    CantidadComprada = 6,       //Al tener mucho volumen la compra(6000cm3), esta se debe asignar al camion 3
                    CodProducto = 1,
                    DniCliente = 46218245,
                    FechaDeEntrega = DateTime.Now.AddDays(2),
                };

                //ACT
                CompraService compraService = new CompraService();
                int cont = ViajeFile.LeerViajesDesdeJson().Count();
                compraService.AgregarCompra(compraDTO);
               Resultado resultado =  _service.AgregarViaje(viajeDTO);
      
                int contCompras = CompraFile.LeerComprasDesdeJson().Count();
                //ASSERT
                Assert.AreEqual(ViajeFile.LeerViajesDesdeJson().Count(), (cont+3));
                Assert.That(ViajeFile.LeerViajesDesdeJson().LastOrDefault().FechaDeEntregaDesde.Day, Is.EqualTo(DateTime.Now.AddDays(1).Day));
            }

            [Test]
            public void EliminarViaje()
            {
                //ARRANGE
                Viaje ViajeEliminar = ViajeFile.LeerViajesDesdeJson().LastOrDefault();
                //ACT
                _service.EliminarViaje(ViajeEliminar.CodViaje);
                //ASSERT
                Assert.That(ViajeFile.LeerViajesDesdeJson().FirstOrDefault(x => x.CodViaje == ViajeEliminar.CodViaje).FechaDeEliminacion.HasValue);
            
        }

        }
    }
}
