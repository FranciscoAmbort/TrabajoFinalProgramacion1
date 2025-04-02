namespace EmpresaEntities
{
    public class Cliente
    {

        public int DniCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string EmailCliente { get; set; }
        public int TelefonoCliente { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime FechaNacimientoCliente { get; set; }
        public double LatitudCliente { get; set; }
        public double LongitudCliente { get; set; }

    }
}
