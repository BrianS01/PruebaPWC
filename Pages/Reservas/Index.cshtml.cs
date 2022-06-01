using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace _220529PruebaTecnica.Pages.Reservas
{
    public class IndexModel : PageModel
    {
        public List<ReservaInfo> listaReservas = new List<ReservaInfo>();

        public void OnGet()
        {
            try
            {
                //String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Salones;Integrated Security=True";
                String connectionString = "Data Source=Salones.mssql.somee.com;Initial Catalog=Salones;user id=BrianS01_SQLLogin_1; pwd=itxrwj512x";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM reservas";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReservaInfo reservaInfo = new ReservaInfo();
                                reservaInfo.id = "" + reader.GetInt32(0);
                                reservaInfo.cliente = reader.GetString(1);
                                reservaInfo.fecha = reader.GetDateTime(2).ToString();
                                reservaInfo.cantidad = reader.GetString(3);
                                reservaInfo.motivo = reader.GetString(4);
                                reservaInfo.observaciones = reader.GetString(5);
                                reservaInfo.estado = reader.GetString(6);
                                listaReservas.Add(reservaInfo);
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

    public class ReservaInfo
    {
        public String id;
        public String cliente;
        public String fecha;
        public String cantidad;
        public String motivo;
        public String observaciones;
        public String estado;
    }
}