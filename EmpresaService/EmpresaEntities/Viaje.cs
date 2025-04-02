using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaEntities
{
    public class Viaje
    {
        public int CodViaje { get; set; }
        public string PatenteCamionAsociado { get; set; }
        public DateTime FechaDeEntregaDesde { get; set; }
        public DateTime FechaEntregaHasta { get; set; }
        public double PorcentajeOcupacionCarga { get; set; }
        public List<int> CodigosDeCompra { get; set; }
        public DateTime? FechaDeEliminacion { get; set; }
        public DateTime? FechaDeActualizacion { get; set; }
        public DateTime FechaDeCreacion { get; set; }

        public Viaje()
        {
            CodigosDeCompra = new List<int>();

        }
    }
}
