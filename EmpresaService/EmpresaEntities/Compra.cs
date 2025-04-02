using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geolocation;

namespace EmpresaEntities
{
    public class Compra
    {
        public int CodCompra {  get; set; }
        public int CodProducto { get; set; }
        public int DniCliente { get; set; }
        public DateTime FechaDeCompra {  get; set; }
        public int CantidadComprada {  get; set; }
        public double MontoTotal { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public DateTime? FechaDeActualizacion { get; set; }
        public DateTime? FechaDeEliminacion { get; set; }
        public double LatitudCliente {  get; set; }
        public double LongitudCliente {  get; set; }
        public Enums.EnumEstadoDeCompra EstadoDeCompra { get; set; }

        //Recibe el producto que buscamos con el codigo de compra y calcula total (iva y descuento incluidos)
        public double CalcularTotal(Producto producto)
        {
           double precioTotal = producto.PrecioProducto*CantidadComprada;

            precioTotal = precioTotal * 1.21;
            if (CantidadComprada > 4)
            {
                precioTotal = precioTotal - (precioTotal * 0.25);
            }
            return precioTotal;
        }

        public bool StockSuficiente(Producto producto)
        {
            if ( CantidadComprada<producto.Stock) { return true; }
            return false;
        }

        //Utilizamos biblioteca GeoLocation, pasando 2 pares de coordenadas devuelve una distancia en millas y para hallar la misma en km lo multiplicamos por 1.6
        public double CalcularDistancia() //es.scribd.com/document/656074216/Biblioteca-Geolocation
        {
            Coordinate empresaCoordenadas = new Coordinate (-31.25033, -61.4867);
            Coordinate clienteCoordenadas = new Coordinate (LatitudCliente,LongitudCliente);
            return (GeoCalculator.GetDistance(empresaCoordenadas, clienteCoordenadas)*1.609);
            
        }

    }
}
