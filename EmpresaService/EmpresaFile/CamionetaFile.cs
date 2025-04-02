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
    public class CamionetaFile
    {
        private static string camionetaFile;


        static CamionetaFile()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory; // Esto apunta a la carpeta 'bin'
            string carpetaEmpresaData = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "EmpresaFile", "ArchivosJson");

            // Comprobamos desde que proyecto estamos accediendo
            if (basePath.Contains("EmpresaTest"))
            {
                // Si la ruta contiene "EmpresaTest", usa clienteTest.json
                camionetaFile = Path.Combine(basePath, "Data", "camionetaTest.json");
            }
            else
            {
                // Sino, usa cliente.json
                camionetaFile = Path.Combine(carpetaEmpresaData, "camioneta.json");
            }


        }

        public static List<Camioneta> LeerCamionetaDesdeJson()
        {
            if (File.Exists(camionetaFile))
            {
                var json = File.ReadAllText(camionetaFile);
                return JsonConvert.DeserializeObject<List<Camioneta>>(json);
            }
            else
            {
                File.WriteAllText(camionetaFile, "[]");
                return new List<Camioneta>();
            }
        }
    }
}
