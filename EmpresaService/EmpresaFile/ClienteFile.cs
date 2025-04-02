using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using EmpresaEntities;

namespace EmpresaData
{
    public class ClienteFile
    {
      
        private static string clienteFile;


        static ClienteFile()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory; // Esto apunta a la carpeta 'bin'
            string carpetaEmpresaData = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "EmpresaFile", "ArchivosJson");

            // Comprobamos desde que proyecto estamos accediendo
            if (basePath.Contains("EmpresaTest"))
            {
                // Si la ruta contiene "EmpresaTest", usa clienteTest.json
                clienteFile = Path.Combine(basePath, "Data", "clienteTest.json");
            }
            else
            {
                // Sino, usa cliente.json
                clienteFile = Path.Combine(carpetaEmpresaData, "cliente.json");
            }

 
        }



        // Método para escribir un usuario a un archivo JSON
        public static void EscribirClienteAJson(Cliente cliente)
        {
            List<Cliente> clientes = LeerClienteDesdeJson();

            if (cliente.DniCliente == 0)
            {
                cliente.DniCliente = clientes.Count()+1;
            }
            else
            {
                clientes.RemoveAll(x => x.DniCliente == cliente.DniCliente);
            }


            clientes.Add(cliente);

            var json = JsonConvert.SerializeObject(clientes, Formatting.Indented);
            File.WriteAllText(clienteFile, json);
        }

        // Método para leer un usuario desde un archivo JSON
        public static List<Cliente> LeerClienteDesdeJson()
        {
            if (File.Exists(clienteFile))
            {
                var json = File.ReadAllText(clienteFile);
                return JsonConvert.DeserializeObject<List<Cliente>>(json);
            }
            else
            {
                File.WriteAllText(clienteFile, "[]");
                return new List<Cliente>();
            }
        }
    }
}
