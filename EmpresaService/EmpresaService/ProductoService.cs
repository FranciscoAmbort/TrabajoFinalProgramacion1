using EmpresaDTO;
using EmpresaEntities;
using EmpresaFile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmpresaService
{
    public class ProductoService
    {
       

        public void AgregarProducto(ProductoDTO productoDTO)
        {
            Producto producto= new Producto();
            producto = Conversion.ConvertirDTOAProducto(productoDTO); 
            producto.FechaCreacion=DateTime.Now;
            ProductoFile.EscribirProductoAJson(producto);
        }

        public Resultado ActualizarStock( int codProducto, int stockActualizado)
        {
           Resultado resultado = new Resultado();
           Producto producto = ProductoFile.LeerProductosDesdeJson().FirstOrDefault(x => x.CodProducto == codProducto);
            if (producto == null) {
                resultado.Success = false;
                resultado.Message = $"No se ha encontrado el producto con el id {codProducto}";
                return resultado;
            }
                producto.Stock += stockActualizado;
                ProductoFile.EscribirProductoAJson(producto);
                resultado.Success = true;
            resultado.Message = $"Se ha actualizado con exito";

            return resultado;    
        }

        public Resultado ActualizarProducto(int cod, ProductoDTO producto)
        {
            Producto productoAct = ProductoFile.LeerProductosDesdeJson().FirstOrDefault(x=>x.CodProducto==cod);
            Resultado resultado= new Resultado();
            if (productoAct == null) {
 
                    resultado.Success = false;
                    resultado.Message = $"No se ha encontrado el producto con el id {cod}";
                    return resultado;
            }
            productoAct.NombreProducto = producto.NombreProducto;
            productoAct.MarcaProducto = producto.MarcaProducto;
            productoAct.AnchoCaja = producto.AnchoCaja;
            productoAct.AltoCaja = producto.AltoCaja;
            productoAct.ProfundidadCaja = producto.ProfundidadCaja;
            productoAct.PrecioProducto = producto.PrecioProducto;
            productoAct.Stock = producto.Stock;
            productoAct.StockMinimo = producto.StockMinimo;
            productoAct.FechaActualizacion = DateTime.Now; 
            resultado.Success= true;
            resultado.Message = "actualizado con exito";
            ProductoFile.EscribirProductoAJson(productoAct);
            return resultado;
        }

        public Resultado EliminarProducto(int codProducto) 
        {
            Resultado resultado = new Resultado();
            var producto = ProductoFile.LeerProductosDesdeJson().FirstOrDefault(x => x.CodProducto == codProducto);
            if (producto == null)
            {
                resultado.Success = false;
                resultado.Message = $"No se encontro el producto con codigo{codProducto}";
                return resultado;
            }
            else
            {
                producto.FechaDeEliminacion = DateTime.Now;
                ProductoFile.EscribirProductoAJson(producto);
                resultado.Success = true;
                resultado.Message = $"El producto ha sido eliminado de manera correcta";
                return resultado;
            }
        }

        public ProductoDTO ObtenerProductoPorCod(int codProducto)
        {
            var producto = ProductoFile.LeerProductosDesdeJson().FirstOrDefault(x => x.CodProducto == codProducto);
            if (producto == null)
            {
                return null;
            }
            var productoConvert = Conversion.ConvertirProductoADTO(producto);
            return productoConvert;
        }

        public List<ProductoDTO> ObtenerListadoDePoductos()
        {
            var productoSinConvertir = ProductoFile.LeerProductosDesdeJson();
            var productosConvertidos = new List<ProductoDTO>();

            foreach (Producto producto in productoSinConvertir)
            {
                productosConvertidos.Add(Conversion.ConvertirProductoADTO(producto));
            }
            return productosConvertidos ;
        }
    }
}
