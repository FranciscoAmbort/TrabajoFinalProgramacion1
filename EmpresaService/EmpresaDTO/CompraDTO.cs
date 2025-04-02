using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaDTO
{
    public class CompraDTO
    {
        [Required]
        public int CodProducto { get; set; }
        [Required]
        public int DniCliente {  get; set; }
        [Required]
        public int CantidadComprada {  get; set; }
        [Required]
        public DateTime FechaDeEntrega {  get; set; }

    }
}
