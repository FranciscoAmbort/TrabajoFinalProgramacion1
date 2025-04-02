using EmpresaEntities;
using EmpresaFile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaData
{
    public class ViajeFile
    {
        private static string viajeFile;
        static ViajeFile()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory; // Esto apunta a la carpeta 'bin'
            string carpetaEmpresaData = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "EmpresaFile", "ArchivosJson");

            // Comprobamos desde que proyecto estamos accediendo
            if (basePath.Contains("EmpresaTest"))
            {
                // Si la ruta contiene "EmpresaTest", usa clienteTest.json
                viajeFile = Path.Combine(basePath, "Data", "viajeTest.json");
            }
            else
            {
                // Sino, usa cliente.json
                viajeFile = Path.Combine(carpetaEmpresaData, "viaje.json");
            } 
        }

        // Método para escribir un viaje a un archivo JSON
        public static void EscribirViajeAJson(Viaje viaje)
        {
            List<Viaje> viajes = LeerViajesDesdeJson();

            if (viaje.CodViaje == 0)
            {
                viaje.CodViaje = viajes.Count() + 1;
            }
            else
            {
                viajes.RemoveAll(x => x.CodViaje == viaje.CodViaje);
            }


            viajes.Add(viaje);

            var json = JsonConvert.SerializeObject(viajes, Formatting.Indented);
            File.WriteAllText(viajeFile, json);
        }

        // Método para leer un viaje desde un archivo JSON
        public static List<Viaje> LeerViajesDesdeJson()
        {
            if (File.Exists(viajeFile))
            {
                var json = File.ReadAllText(viajeFile);
                return JsonConvert.DeserializeObject<List<Viaje>>(json);
            }
            else
            {
                File.WriteAllText(viajeFile, "[]");
                return new List<Viaje>();
            }
        }
    }
}

