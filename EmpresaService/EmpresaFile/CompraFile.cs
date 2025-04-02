using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using EmpresaEntities;
using Newtonsoft.Json;

namespace EmpresaData
{
    public class CompraFile
    {
        private static string compraFile;
        static CompraFile()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory; // Esto apunta a la carpeta 'bin'
            string carpetaEmpresaData = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "EmpresaFile", "ArchivosJson");

            // Comprobamos desde que proyecto estamos accediendo
            if (basePath.Contains("EmpresaTest"))
            {
                // Si la ruta contiene "EmpresaTest", usa clienteTest.json
                compraFile = Path.Combine(basePath, "Data", "compraTest.json");
            }
            else
            {
                // Sino, usa cliente.json
                compraFile = Path.Combine(carpetaEmpresaData, "compra.json");
            }


        }

        // Método para escribir una compra a un archivo JSON
        public static void EscribirCompraAJson(Compra compra)
        {
            List<Compra> compras = LeerComprasDesdeJson();

            if (compra.CodCompra == 0)
            {
                compra.CodCompra = compras.Count()+1;
            }
            else
            {
                compras.RemoveAll(x => x.CodCompra == compra.CodCompra);
            }


            compras.Add(compra);

            var json = JsonConvert.SerializeObject(compras, Formatting.Indented);
            File.WriteAllText(compraFile, json);
        }

        // Método para leer una compra desde un archivo JSON
        public static List<Compra> LeerComprasDesdeJson()
        {
            if (File.Exists(compraFile))
            {
                var json = File.ReadAllText(compraFile);
                return JsonConvert.DeserializeObject<List<Compra>>(json);
            }
            else
            {
                File.WriteAllText(compraFile, "[]");
                return new List<Compra>();
            }
        }
    }
}
