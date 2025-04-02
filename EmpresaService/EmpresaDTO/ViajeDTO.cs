using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaDTO
{
    public class ViajeDTO
    {
       
        [Required]
        [DataType(DataType.Date)]

        public DateTime FechaDeEntregaDesde { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaEntregaHasta { get; set; }
      

       
    
    }
}
