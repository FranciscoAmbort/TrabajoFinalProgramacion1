using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEntities
{
    public class Producto
    {
        public int CodProducto { get; set; }
        public string NombreProducto { get; set; }
        public string MarcaProducto { get; set; }
        public double AnchoCaja { get; set; }
        public double ProfundidadCaja { get; set; }
        public double AltoCaja {  get; set; }
        public double PrecioProducto {  get; set; }
        public int StockMinimo {  get; set; }
        public int Stock { get;set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public DateTime? FechaDeEliminacion { get; set; }

        public double CalcularVolumenCaja()
        {
            return (AnchoCaja * AltoCaja * ProfundidadCaja);
        }

    }
}
