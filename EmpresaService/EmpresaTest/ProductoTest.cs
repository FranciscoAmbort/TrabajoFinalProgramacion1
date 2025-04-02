using System;
using EmpresaService;
using EmpresaDTO;
using EmpresaData;
using EmpresaEntities;
using EmpresaFile;
namespace EmpresaTest
{
    public class ProductoTest
    {
            public class Tests
            {
                ProductoService _service = new ProductoService();
            public Tests()
            {
                _service = new ProductoService ();
            }
              
                [SetUp]
            public void Setup()
            {
            }

            [Test]
            public void ComprobarAgregarUnNuevoProducto()
            {
                //ARRANGE
                ProductoDTO producto = new ProductoDTO();

                producto.NombreProducto = "Mouse Inalambrico";
                producto.MarcaProducto = "Logitech";
                producto.AnchoCaja = 20;
                producto.ProfundidadCaja = 20;
                producto.AltoCaja = 20;
                producto.PrecioProducto = 1250;
                producto.Stock = 300;
                producto.StockMinimo = 5;

                //ACT
                int countProductos = ProductoFile.LeerProductosDesdeJson().Count();
                _service.AgregarProducto(producto);
                
                
                //ASSERT
                Assert.That((countProductos+1).Equals(ProductoFile.LeerProductosDesdeJson().Count()));
            }

            [Test]
            public void EliminarLogicamenteUnProducto()
            {
                //ARRANGE
                Producto productoEliminar = ProductoFile.LeerProductosDesdeJson().LastOrDefault();
                //ACT
                _service.EliminarProducto(productoEliminar.CodProducto);
                //ASSERT
                Assert.That(ProductoFile.LeerProductosDesdeJson().FirstOrDefault(x => x.CodProducto == productoEliminar.CodProducto).FechaDeEliminacion.HasValue);
            }
        }
    }
}