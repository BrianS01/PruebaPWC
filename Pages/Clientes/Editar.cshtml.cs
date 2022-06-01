using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace _220529PruebaTecnica.Pages.Clientes
{
    public class EditarModel : PageModel
    {
        public ClienteInfo clienteInfo = new ClienteInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                //String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Salones;Integrated Security=True";
                String connectionString = "Data Source=Salones.mssql.somee.com;Initial Catalog=Salones;user id=BrianS01_SQLLogin_1; pwd=itxrwj512x";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clientes WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clienteInfo.id = "" + reader.GetInt32(0);
                                clienteInfo.cedula = reader.GetString(1);
                                clienteInfo.nombres = reader.GetString(2);
                                clienteInfo.apellidos = reader.GetString(3);
                                clienteInfo.telefono = reader.GetString(4);
                                clienteInfo.correo = reader.GetString(5);
                                clienteInfo.departamento = reader.GetString(6);
                                clienteInfo.ciudad = reader.GetString(7);
                                clienteInfo.edad = reader.GetString(8);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            clienteInfo.id = Request.Form["id"];
            clienteInfo.cedula = Request.Form["cedula"];
            clienteInfo.nombres = Request.Form["nombres"];
            clienteInfo.apellidos = Request.Form["apellidos"];
            clienteInfo.telefono = Request.Form["telefono"];
            clienteInfo.correo = Request.Form["correo"];
            clienteInfo.departamento = Request.Form["departamento"];
            clienteInfo.ciudad = Request.Form["ciudad"];
            clienteInfo.edad = Request.Form["edad"];

            if (clienteInfo.cedula.Length == 0 || clienteInfo.nombres.Length == 0 ||
                clienteInfo.apellidos.Length == 0 || clienteInfo.telefono.Length == 0)
            {
                errorMessage = "Todos los campos son obligatorios";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Salones;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clientes " +
                                 "SET cedula=@cedula, nombres=@nombres, apellidos=@apellidos, telefono=@telefono, correo=@correo, departamento=@departamento, ciudad=@ciudad, edad=@edad " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", clienteInfo.id);
                        command.Parameters.AddWithValue("@cedula", clienteInfo.cedula);
                        command.Parameters.AddWithValue("@nombres", clienteInfo.nombres);
                        command.Parameters.AddWithValue("@apellidos", clienteInfo.apellidos);
                        command.Parameters.AddWithValue("@telefono", clienteInfo.telefono);
                        command.Parameters.AddWithValue("@correo", clienteInfo.correo);
                        command.Parameters.AddWithValue("@departamento", clienteInfo.departamento);
                        command.Parameters.AddWithValue("@ciudad", clienteInfo.ciudad);
                        command.Parameters.AddWithValue("@edad", clienteInfo.edad);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clientes/Index");
        }
    }
}