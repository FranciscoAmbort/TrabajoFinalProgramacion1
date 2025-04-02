using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaDTO
{
    public class ClienteDTO
    {
        [Required]
       [Range(0, int.MaxValue)]
        public int DniCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string NombreCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string ApellidoCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string EmailCliente { get; set; }
        [Required]
        [Range (0,int.MaxValue)]
        public int TelefonoCliente { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimientoCliente { get; set; }
        [Required]
        
        public double LatitudCliente { get; set; }
        [Required]
        
        public double LongitudCliente { get; set; }
    }
}
