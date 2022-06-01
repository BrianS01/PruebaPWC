using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace _220529PruebaTecnica.Pages.Reservas
{
    public class EditarModel : PageModel
    {
        public ReservaInfo reservaInfo = new ReservaInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Salones;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM reservas WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                reservaInfo.id = "" + reader.GetInt32(0);
                                reservaInfo.cliente = reader.GetString(1);
                                reservaInfo.fecha = reader.GetDateTime(2).ToString();
                                reservaInfo.cantidad = reader.GetString(3);
                                reservaInfo.motivo = reader.GetString(4);
                                reservaInfo.observaciones = reader.GetString(5);
                                reservaInfo.estado = reader.GetString(6);
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
            reservaInfo.id = Request.Form["id"];
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

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Salones;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE reservas " +
                                 "SET cliente=@cliente, fecha=@fecha, cantidad=@cantidad, motivo=@motivo, observaciones=@observaciones, estado=@estado " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", reservaInfo.id);
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

            Response.Redirect("/Reservas/Index");
        }
    }
}