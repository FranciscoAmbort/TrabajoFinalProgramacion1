using System;
using EmpresaService;
using EmpresaDTO;
using EmpresaData;
using EmpresaEntities;
using EmpresaFile;
namespace EmpresaTest
{
    public class CompraTest
    {
            public class Tests
            {
                CompraService _service = new CompraService();
                public Tests()
                {
                _service = new CompraService();
            }
              
                [SetUp]
            public void Setup()
            {
            }

            [Test]
            public void ComprobarCompraConUnClienteInexistente()
            {
                //ARRANGE
                CompraDTO compra = new CompraDTO();

                compra.CantidadComprada = 50;
                compra.DniCliente = 31255820;
                compra.FechaDeEntrega = DateTime.Now.AddDays(15);
                compra.CodProducto = 1;
                
                //ACT
             
                Resultado result = _service.AgregarCompra(compra);
                
                
                //ASSERT
                Assert.That((result.Message).Equals($"no se ha encontrado un cliente con el dni 31255820"));
            }
            [Test]

            public void EditarCompra()
            {
                //ARRANGE
                CompraDTO compra = new CompraDTO();

                compra.CantidadComprada = 50;
                compra.DniCliente = 46218245;
                compra.FechaDeEntrega = DateTime.Now.AddDays(15);
                compra.CodProducto = 1;
                //ACT
               Resultado resultado =  _service.ActualizarCompra(compra,100);
                //ASSERT
                Assert.That(resultado.Message.Equals("La compra que desea editar no existe"));
            }
        }
    }
}