using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace _220529PruebaTecnica.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        public List<ClienteInfo> listaClientes = new List<ClienteInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Salones;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clientes";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClienteInfo clienteInfo = new ClienteInfo();
                                clienteInfo.id = "" + reader.GetInt32(0);
                                clienteInfo.cedula = reader.GetString(1);
                                clienteInfo.nombres = reader.GetString(2);
                                clienteInfo.apellidos = reader.GetString(3);
                                clienteInfo.telefono = reader.GetString(4);
                                clienteInfo.correo = reader.GetString(5);
                                clienteInfo.departamento = reader.GetString(6);
                                clienteInfo.ciudad = reader.GetString(7);
                                clienteInfo.edad = reader.GetString(8);
                                listaClientes.Add(clienteInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class ClienteInfo
    {
        public String id;
        public String cedula;
        public String nombres;
        public String apellidos;
        public String telefono;
        public String correo;
        public String departamento;
        public String ciudad;
        public String edad;
    }
}