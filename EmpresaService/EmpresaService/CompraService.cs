using EmpresaData;
using EmpresaDTO;
using EmpresaEntities;
using EmpresaFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaService
{
    public class CompraService
    {
        
        public Resultado AgregarCompra(CompraDTO compraDTO)
        {
            Resultado resultado = new Resultado();

            Compra compraConvertida = Conversion.ConvertirDTOACompra(compraDTO);
            Cliente cliente = ClienteFile.LeerClienteDesdeJson().FirstOrDefault(x=>x.DniCliente==compraConvertida.DniCliente);
            if (cliente == null)
            {
                resultado.Success = false;
                resultado.Message = $"no se ha encontrado un cliente con el dni {compraConvertida.DniCliente}";
                return resultado;
            }
            Producto producto = ProductoFile.LeerProductosDesdeJson().FirstOrDefault(x=>x.CodProducto==compraConvertida.CodProducto);
            if (producto == null)
            {
                resultado.Success = false;
                resultado.Message = $"no se ha encontrado un producto con el codigo {compraConvertida.CodProducto}";
                return resultado;
            }
            if (!compraConvertida.StockSuficiente(producto))
            {
                resultado.Success = false;
                resultado.Message = $"no hay stock suficiente";
                return resultado;
            }
            producto.Stock -= compraConvertida.CantidadComprada;
            ProductoFile.EscribirProductoAJson(producto);
            compraConvertida.FechaDeCompra = DateTime.Now;
            compraConvertida.MontoTotal = compraConvertida.CalcularTotal(producto);
            compraConvertida.EstadoDeCompra = Enums.EnumEstadoDeCompra.Open;
            compraConvertida.LatitudCliente = cliente.LatitudCliente;
            compraConvertida.LongitudCliente=cliente.LongitudCliente;
            CompraFile.EscribirCompraAJson(compraConvertida);
            resultado.Success = true;
            resultado.Message = "Se ha agregado con exito";
            return resultado;

        }
        public Resultado ActualizarCompra(CompraDTO compra, int codComp)
        {
            Resultado result= new Resultado();
            Compra compraAct= CompraFile.LeerComprasDesdeJson().FirstOrDefault(x=>x.CodCompra == codComp);
            if (compraAct == null)
            {
                result.Success = false;
                result.Message = "La compra que desea editar no existe";
                return result;
            }
            if (compraAct.EstadoDeCompra is Enums.EnumEstadoDeCompra.Ready_To_Dispatch)
            {
                result.Success = false;
                result.Message = "La compra ya se encuentra despachada, no se puede actualizar"; 
                return result;
            }
            compraAct.CodProducto = compra.CodProducto;
            Producto producto = ProductoFile.LeerProductosDesdeJson().FirstOrDefault(x => x.CodProducto == compraAct.CodProducto);
            if (producto == null)
            {
                result.Success = false;
                result.Message = $"no se ha encontrado un producto con el codigo {compraAct.CodProducto}";
                return result;
            }
            compraAct.CantidadComprada=compra.CantidadComprada;
            compraAct.DniCliente= compra.DniCliente;
            Cliente cliente = ClienteFile.LeerClienteDesdeJson().FirstOrDefault(x => x.DniCliente == compraAct.DniCliente);
            if (cliente == null)
            {
                result.Success = false;
                result.Message = $"no se ha encontrado un cliente con el dni {compraAct.DniCliente}";
                return result;
            }
            compraAct.FechaDeEntrega=compra.FechaDeEntrega;
            compraAct.FechaDeActualizacion = DateTime.Now;
            compraAct.LatitudCliente = cliente.LatitudCliente;
            compraAct.LongitudCliente = cliente.LongitudCliente;
            compraAct.MontoTotal = compraAct.CalcularTotal(producto);
            result.Success=true;
            result.Message = "La compra fue editada";
            CompraFile.EscribirCompraAJson(compraAct);

            return result; 
        }

        public Resultado EliminarCompra(int codigoCompra)
        {
            Resultado resultado = new Resultado();
           Compra compra = CompraFile.LeerComprasDesdeJson().FirstOrDefault(x=>x.CodCompra == codigoCompra);
            if (compra == null)
            {
                resultado.Success = false;
                resultado.Message = $"No se ha encontrado una compra con el Codigo {codigoCompra}";
            }
            compra.FechaDeEliminacion= DateTime.Now;
            CompraFile.EscribirCompraAJson(compra);
            resultado.Success = true;
            resultado.Message = "Se ha eliminado con exito";
            return resultado;
        }

        public CompraDTO ObtenerCompraPorCod(int cod)
        {
            return Conversion.ConvertirCompraADTO(CompraFile.LeerComprasDesdeJson().FirstOrDefault(x=>x.CodCompra==cod));
        }

        public List<CompraDTO> ObtenerCompras()
        {
            List<Compra> compras= CompraFile.LeerComprasDesdeJson();
            List<CompraDTO> comprasDTO= new List<CompraDTO>();
            foreach (var compra in compras)
            {
                comprasDTO.Add(Conversion.ConvertirCompraADTO(compra)); 
            }
            return comprasDTO; 
        }
    }
}
