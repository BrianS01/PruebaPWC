using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace _220529PruebaTecnica.Pages.Reservas
{
    public class CrearModel : PageModel
    {
        public ReservaInfo reservaInfo = new ReservaInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            reservaInfo.cliente = Request.Form["cliente"];
            reservaInfo.fecha = Request.Form["fecha"];
            reservaInfo.cantidad = Request.Form["cantidad"];
            reservaInfo.motivo = Request.Form["motivo"];
            reservaInfo.observaciones = Request.Form["observaciones"];
            reservaInfo.estado = Request.Form["estado"];

            if (reservaInfo.cliente.Length == 0 || reservaInfo.observaciones.Length == 0 ||
                reservaInfo.cantidad.Length == 0 || reservaInfo.estado.Length == 0)
            {
                errorMessage = "Todos los campos son obligatorios";
                return;
            }

            //save the new client into the database
            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Salones;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO reservas " +
                                 "(cliente, fecha, cantidad, motivo, observaciones, estado) VALUES" +
                                 "(@cliente, @fecha, @cantidad, @motivo, @observaciones, @estado);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@cliente", reservaInfo.cliente);
                        command.Parameters.AddWithValue("@fecha", reservaInfo.fecha);
                        command.Parameters.AddWithValue("@cantidad", reservaInfo.cantidad);
                        command.Parameters.AddWithValue("@motivo", reservaInfo.motivo);
                        command.Parameters.AddWithValue("@observaciones", reservaInfo.observaciones);
                        command.Parameters.AddWithValue("@estado", reservaInfo.estado);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            reservaInfo.cliente = "";
            reservaInfo.fecha = "";
            reservaInfo.cantidad = "";
            reservaInfo.motivo = "";
            reservaInfo.observaciones = "";
            reservaInfo.estado = "";
            successMessage = "Reserva Creada Correctamente";
            Response.Redirect("/Reservas/Index");
        }
    }
}