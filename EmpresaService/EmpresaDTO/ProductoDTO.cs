using System.ComponentModel.DataAnnotations;

namespace EmpresaDTO
{
    public class ProductoDTO
    {
        [Required]
        public string NombreProducto { get; set; }
        [Required]
        public string MarcaProducto { get; set; }
        [Required]
        public double AnchoCaja { get; set; }
        [Required]
        public double ProfundidadCaja { get; set; }
        [Required]
        public double AltoCaja { get; set; }
        [Required]
        public double PrecioProducto { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public int StockMinimo { get; set; }
        
    }
}
