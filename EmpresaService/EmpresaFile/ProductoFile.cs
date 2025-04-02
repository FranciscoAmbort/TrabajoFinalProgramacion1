using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using EmpresaEntities;
using Newtonsoft.Json;

namespace EmpresaFile
{
    public class ProductoFile
    {

        private static string productoFile;
        static ProductoFile()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory; // Esto apunta a la carpeta 'bin'
            string carpetaEmpresaData = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "EmpresaFile", "ArchivosJson");

            // Comprobamos desde que proyecto estamos accediendo
            if (basePath.Contains("EmpresaTest"))
            {
                // Si la ruta contiene "EmpresaTest", usa clienteTest.json
                productoFile = Path.Combine(basePath, "Data", "productoTest.json");
            }
            else
            {
                // Sino, usa cliente.json
                productoFile = Path.Combine(carpetaEmpresaData, "producto.json");
            }

        }

        // Método para escribir un producto a un archivo JSON
        public static void EscribirProductoAJson(Producto producto)
        {
            List<Producto> productos = LeerProductosDesdeJson();

            if (producto.CodProducto == 0)
            {
                producto.CodProducto = productos.Count()+1;
            }
            else
            {
                productos.RemoveAll(x => x.CodProducto==producto.CodProducto);
            }


            productos.Add(producto);

            var json = JsonConvert.SerializeObject(productos, Formatting.Indented);
            File.WriteAllText(productoFile, json);
        }

        // Método para leer un producto desde un archivo JSON
        public static List<Producto> LeerProductosDesdeJson()
        {
            if (File.Exists(productoFile))
            {
                var json = File.ReadAllText(productoFile);
                return JsonConvert.DeserializeObject<List<Producto>>(json);
            }
            else
            {
                File.WriteAllText(productoFile, "[]");
                return new List<Producto>();
            }
        }
    }
}
