using EmpresaData;
using EmpresaDTO;
using EmpresaEntities;
using EmpresaFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaService
{
    public class ViajeService
    {
        public Resultado AgregarViaje(ViajeDTO viajeDTO)
        {
            Resultado resultado = new Resultado();
            double volCamioneta1 = 0; 
            double volCamioneta2 = 0;
            double volCamioneta3 = 0;
            Viaje viaje1= Conversion.ConvertirDTOAViaje(viajeDTO);
            Viaje viaje2= Conversion.ConvertirDTOAViaje(viajeDTO);
            Viaje viaje3= Conversion.ConvertirDTOAViaje(viajeDTO);
            viaje1.FechaDeCreacion = DateTime.Now;
            viaje2.FechaDeCreacion = DateTime.Now;
            viaje3.FechaDeCreacion = DateTime.Now;

            viaje1.PatenteCamionAsociado = "AA0030D";
            viaje2.PatenteCamionAsociado = "AE7180J";
            viaje3.PatenteCamionAsociado = "AB5780E";


            if (!ValidarFechas(viajeDTO))
            {
                resultado.Success = false;
                resultado.Message = "Ya existe un viaje en esas fechas";
                return resultado;
            }
            List<Compra> compras = CompraFile.LeerComprasDesdeJson().Where(x => x.EstadoDeCompra == Enums.EnumEstadoDeCompra.Open && x.FechaDeEliminacion is null).OrderBy(x=>x.FechaDeEntrega).ToList();

            if (compras.Count()==0)
            {
                resultado.Success = false;
                resultado.Message = "No existen compras entre esas fechas, no se pueden asignar viajes";
                return resultado; 
            }
            foreach (Compra compra in compras) {
                double volumen= ProductoFile.LeerProductosDesdeJson().FirstOrDefault(x => x.CodProducto == compra.CodProducto).CalcularVolumenCaja()*compra.CantidadComprada;
                double distancia = compra.CalcularDistancia();
                int cod = compra.CodCompra;
                if (distancia<350 && (volumen+volCamioneta1)<3300)
                {
                    volCamioneta1 += volumen;
                    
                    viaje1.CodigosDeCompra.Add(cod); 
                    compra.EstadoDeCompra= Enums.EnumEstadoDeCompra.Ready_To_Dispatch;
                }else if (distancia<550&&(volumen+volCamioneta2)<5800)
                {
                    volCamioneta2 += volumen;
                    viaje2.CodigosDeCompra.Add(cod);
                    compra.EstadoDeCompra = Enums.EnumEstadoDeCompra.Ready_To_Dispatch; 
                }else if (distancia < 750 && (volumen + volCamioneta3) < 6700)
                {
                    volCamioneta3 += volumen;
                    viaje3.CodigosDeCompra.Add(cod);
                    compra.EstadoDeCompra = Enums.EnumEstadoDeCompra.Ready_To_Dispatch;

                }
                CompraFile.EscribirCompraAJson(compra);
            }
            viaje1.PorcentajeOcupacionCarga = ((100*volCamioneta1)/3300);
            viaje2.PorcentajeOcupacionCarga = ((100 * volCamioneta2) / 5800);
            viaje3.PorcentajeOcupacionCarga = ((100 * volCamioneta3) / 6700);
            ViajeFile.EscribirViajeAJson(viaje1);
            ViajeFile.EscribirViajeAJson(viaje2);
            ViajeFile.EscribirViajeAJson(viaje3);
            compras = compras.Where(x => x.EstadoDeCompra == Enums.EnumEstadoDeCompra.Open).ToList(); 
            foreach (Compra compra in compras) {
                compra.FechaDeEntrega.AddDays(14);
                CompraFile.EscribirCompraAJson(compra);
            }
            resultado.Success = true;
            resultado.Message = "Se ha agregado con exito";
            return resultado;
        } 
        public Resultado ActualizarViaje(int cod, ViajeDTO viajeAct)
        {
            Resultado result = new Resultado(); 
            Viaje viajeEditar= ViajeFile.LeerViajesDesdeJson().FirstOrDefault(x=>x.CodViaje==cod);
            if (viajeEditar == null) {
                result.Message = "no se encontro el viaje a actualizar"; 
                result.Success = false;
                return result;
            }

            if (!ValidarFechas(viajeAct))
            {
                result.Message = "las fechas ingresadas no cumplen con las condiciones"; 
                result.Success = false;
                return result;
            }
            viajeEditar.FechaDeEntregaDesde=viajeAct.FechaDeEntregaDesde;
            viajeEditar.FechaEntregaHasta=viajeAct.FechaEntregaHasta;
            viajeEditar.FechaDeActualizacion = DateTime.Now; 
            ViajeFile.EscribirViajeAJson(viajeEditar);
            result.Success = true;
            result.Message = "actualizado con exito"; 
            return result;

        }

        public Resultado EliminarViaje(int cod)
        {
            Resultado result= new Resultado();
            var viajeEliminar = ViajeFile.LeerViajesDesdeJson().FirstOrDefault(x=>x.CodViaje==cod);
            if (viajeEliminar==null)
            {
                result.Success = false;
                result.Message = "No se encontro el viaje para eliminar"; 
                return result;
            }
            viajeEliminar.FechaDeEliminacion = DateTime.Now;
            result.Success= true;
            result.Message = "Viaje eliminado con exito";
            ViajeFile.EscribirViajeAJson(viajeEliminar);

            return result; 
        }
         
        private bool ValidarFechas(ViajeDTO viajeDTO)
        {
            if (!(viajeDTO.FechaDeEntregaDesde.AddDays(7) >= viajeDTO.FechaEntregaHasta && viajeDTO.FechaDeEntregaDesde > DateTime.Now))
            {
                return false;
            }
            var viajes = ViajeFile.LeerViajesDesdeJson();

            if (!viajes.All(x => (viajeDTO.FechaEntregaHasta <= x.FechaDeEntregaDesde) || (viajeDTO.FechaDeEntregaDesde >= x.FechaEntregaHasta)) )
            {
                return false;
            }
            return true;
        }

        public ViajeDTO ObtenerViajePorId(int id)
        {
            Viaje viaje = ViajeFile.LeerViajesDesdeJson().FirstOrDefault(x => x.CodViaje == id);
            if (viaje == null)
            {
                return null;
            }

            return Conversion.ConvertirViajeADTO(viaje);
        }

        public List<ViajeDTO> ObtenerViajes()
        {
            List<ViajeDTO> viajesConvertidos  = new List<ViajeDTO>();
            List<Viaje> viajes = ViajeFile.LeerViajesDesdeJson();
            foreach (Viaje viaje in viajes) { 
            
                viajesConvertidos.Add(Conversion.ConvertirViajeADTO(viaje));
            }
            return viajesConvertidos;
        }
    }
}
