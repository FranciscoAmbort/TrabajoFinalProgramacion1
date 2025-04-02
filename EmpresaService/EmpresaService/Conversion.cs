using EmpresaDTO;
using EmpresaEntities;
using System.ComponentModel.DataAnnotations;
namespace EmpresaService
{
    public static class Conversion
    {

        //DE CLIENTE A CLIENTEDTO
        public static ClienteDTO ConvertirClienteADTO(Cliente cliente)
        {
            ClienteDTO clienteDTO = new ClienteDTO();

            clienteDTO.DniCliente = cliente.DniCliente;
            clienteDTO.NombreCliente = cliente.NombreCliente;
            clienteDTO.ApellidoCliente = cliente.ApellidoCliente;
            clienteDTO.EmailCliente = cliente.EmailCliente;
            clienteDTO.TelefonoCliente = cliente.TelefonoCliente;
            clienteDTO.FechaNacimientoCliente = cliente.FechaNacimientoCliente;
            clienteDTO.LatitudCliente = cliente.LatitudCliente;
            clienteDTO.LongitudCliente = cliente.LongitudCliente;

            return clienteDTO;
            

        }

        //DE CLIENTEDTO A CLIENTE
        public static Cliente ConvertirDTOACliente(ClienteDTO clienteDTO)
        {
           Cliente cliente = new Cliente();
            cliente.DniCliente = clienteDTO.DniCliente;
            cliente.NombreCliente = clienteDTO.NombreCliente;
            cliente.ApellidoCliente = clienteDTO.ApellidoCliente;
            cliente.EmailCliente = clienteDTO.EmailCliente;
            cliente.TelefonoCliente = clienteDTO.TelefonoCliente;
            cliente.FechaNacimientoCliente = clienteDTO.FechaNacimientoCliente;
            cliente.LatitudCliente = clienteDTO.LatitudCliente;
            cliente.LongitudCliente = clienteDTO.LongitudCliente;

            return cliente;

        }

        //DE COMPRA A COMPRADTO
        public static CompraDTO ConvertirCompraADTO(Compra compra)
        {
            CompraDTO compraDTO = new CompraDTO();
            compraDTO.CodProducto = compra.CodProducto;
            compraDTO.DniCliente = compra.DniCliente;
            compraDTO.CantidadComprada = compra.CantidadComprada;
            compraDTO.FechaDeEntrega = compra.FechaDeEntrega;
            return compraDTO;
            
        }
        //DE COMPRADTO A COMPRA
        public static Compra ConvertirDTOACompra(CompraDTO compradto)
        {
            Compra compra = new Compra();

            compra.CodProducto = compradto.CodProducto;
            compra.DniCliente = compradto.DniCliente;
            compra.CantidadComprada = compradto.CantidadComprada;
            compra.FechaDeEntrega = compradto.FechaDeEntrega;

            return compra;
        }

        //PRODUCTO A PRODUCTODTO

        public static ProductoDTO ConvertirProductoADTO(Producto producto)
        {
            ProductoDTO productoDTO = new ProductoDTO();
            productoDTO.NombreProducto = producto.NombreProducto;
            productoDTO.MarcaProducto = producto.MarcaProducto;
            productoDTO.AnchoCaja = producto.AnchoCaja;
            productoDTO.AltoCaja = producto.AltoCaja;
            productoDTO.ProfundidadCaja = producto.ProfundidadCaja;
            productoDTO.PrecioProducto = producto.PrecioProducto;
            productoDTO.Stock = producto.Stock;
                productoDTO.StockMinimo = producto.StockMinimo;
            return productoDTO;
        }
        //PRODUCTODTO A PRODUCTO

        public static Producto ConvertirDTOAProducto(ProductoDTO productoDTO)
        {
            Producto producto = new Producto();
            producto.NombreProducto = productoDTO.NombreProducto;
            producto.MarcaProducto = productoDTO.MarcaProducto;
            producto.AnchoCaja = productoDTO.AnchoCaja;
            producto.AltoCaja = productoDTO.AltoCaja;
            producto.ProfundidadCaja = productoDTO.ProfundidadCaja;
            producto.PrecioProducto = productoDTO.PrecioProducto;
            producto.Stock = productoDTO.Stock;
            producto.StockMinimo = productoDTO.StockMinimo;
            
           return producto;
        }

        //VIAJE A VIAJEDTO
        public static ViajeDTO ConvertirViajeADTO(Viaje viaje)
        {
            ViajeDTO viajeDTO = new ViajeDTO();

            viajeDTO.FechaDeEntregaDesde = viaje.FechaDeEntregaDesde;
            viajeDTO.FechaEntregaHasta = viaje.FechaEntregaHasta;
         
            return viajeDTO;
        }
        //VIAJEDTO A VIAJE

        public static Viaje ConvertirDTOAViaje(ViajeDTO viajeDTO)
        {
            Viaje viaje = new Viaje();
            
                viaje.FechaDeEntregaDesde = viajeDTO.FechaDeEntregaDesde;
                viaje.FechaEntregaHasta = viajeDTO.FechaEntregaHasta;
             
                return viaje;
        }

    }
}
