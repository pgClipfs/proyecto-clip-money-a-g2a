using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace clip_money.Models
{
    public class GestorOperaciones
    {
        public List<Operaciones> obtenerTop10Operaciones(long idCV)
        {
            List<Operaciones> lista = new List<Operaciones>();
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "top10operaciones";

                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@id_cuenta_virtual", idCV));

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    string fecha = dr.GetDateTime(0).Date.ToString("dd-MM-yyyy");
                    string hora = dr.GetTimeSpan(1).ToString();
                    string stringTipoOperacion = dr.GetString(2);
                    TipoOperacion tipoOperacion = new TipoOperacion(stringTipoOperacion);
                    decimal monto = dr.GetSqlMoney(3).ToDecimal();

                    Operaciones p = new Operaciones(fecha,hora,tipoOperacion, Math.Round(monto, 2));
                    lista.Add(p);
                }

                dr.Close();
            }
            return lista;
        }
    }
}