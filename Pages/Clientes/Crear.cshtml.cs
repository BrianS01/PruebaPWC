using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace _220529PruebaTecnica.Pages.Clientes
{
    public class CrearModel : PageModel
    {
        public ClienteInfo clienteInfo = new ClienteInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
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

            //save the new client into the database
            try
            {
                //String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Salones;Integrated Security=True";
                String connectionString = "Data Source=Salones.mssql.somee.com;Initial Catalog=Salones;user id=BrianS01_SQLLogin_1; pwd=itxrwj512x";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clientes " +
                                 "(cedula, nombres, apellidos, telefono, correo, departamento, ciudad, edad) VALUES" +
                                 "(@cedula, @nombres, @apellidos, @telefono, @correo, @departamento, @ciudad, @edad);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
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

            clienteInfo.cedula = ""; clienteInfo.nombres = ""; clienteInfo.apellidos = ""; clienteInfo.telefono = ""; 
            clienteInfo.correo = ""; clienteInfo.departamento = ""; clienteInfo.ciudad = ""; clienteInfo.edad = "";
            successMessage = "Cliente Agregado Correctamente";
            Response.Redirect("/Clientes/Index");
        }
    }
}