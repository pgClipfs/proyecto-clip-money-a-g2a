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
                    string fecha = dr.GetString(0);
                    string hora = dr.GetString(1);
                    string stringTipoOperacion = dr.GetString(2);
                    TipoOperacion tipoOperacion = new TipoOperacion(stringTipoOperacion);
                    decimal monto = dr.GetSqlMoney(3).ToDecimal();

                    Operaciones p = new Operaciones(fecha,hora,tipoOperacion, Math.Round(monto, 2),idCV);
                    lista.Add(p);
                }

                dr.Close();
            }
            return lista;
        }

        public void insertarOperacion(Operaciones nuevaop)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "insertarOperacion";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@numOperacion", nuevaop.NumOperacion));
                comm.Parameters.Add(new SqlParameter("@fecha", nuevaop.Fecha));
                comm.Parameters.Add(new SqlParameter("@hora", nuevaop.Hora));
                comm.Parameters.Add(new SqlParameter("@monto", nuevaop.Monto));
                comm.Parameters.Add(new SqlParameter("@destino", nuevaop.Destino));
                comm.Parameters.Add(new SqlParameter("@idTipoOperacion", nuevaop.TipoOperacion.Id));
                comm.Parameters.Add(new SqlParameter("@idEstado", nuevaop.Estado.Id));
                comm.Parameters.Add(new SqlParameter("@idCuentaVirtual", nuevaop.IdCuentaVirtual));
               

                comm.ExecuteNonQuery();
            }
        }

        public List<Operaciones> obtenerOperacionesTodas(long idCV, string fechadesde, string fechahasta, int concepto )
        {
            List<Operaciones> lista = new List<Operaciones>();
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                if (concepto == 0)
                {
                    SqlCommand comm = conn.CreateCommand();
                    comm.CommandText = "obtenerOperacionesTodas";

                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.Parameters.Add(new SqlParameter("@id_cuenta_virtual", idCV));
                    comm.Parameters.Add(new SqlParameter("@fechadesde", fechadesde));
                    comm.Parameters.Add(new SqlParameter("@fechahasta", fechahasta));

                    SqlDataReader dr = comm.ExecuteReader();
                    while (dr.Read())
                    {
                        string fecha = dr.GetString(0);
                        string hora = dr.GetString(1);
                        string stringTipoOperacion = dr.GetString(2);
                        TipoOperacion tipoOperacion = new TipoOperacion(stringTipoOperacion);
                        decimal monto = dr.GetSqlMoney(3).ToDecimal();

                        Operaciones p = new Operaciones(fecha, hora, tipoOperacion, Math.Round(monto, 2), idCV);
                        lista.Add(p);
                    }
                    dr.Close();
                }
                else
                {
                    SqlCommand comm = conn.CreateCommand();
                    comm.CommandText = "obtenerOperacionesFiltradas";

                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.Parameters.Add(new SqlParameter("@id_cuenta_virtual", idCV));
                    comm.Parameters.Add(new SqlParameter("@fechadesde", fechadesde));
                    comm.Parameters.Add(new SqlParameter("@fechahasta", fechahasta));
                    comm.Parameters.Add(new SqlParameter("@tipoOpe", concepto));

                    SqlDataReader dr = comm.ExecuteReader();
                    while (dr.Read())
                    {
                        string fecha = dr.GetString(0);
                        string hora = dr.GetString(1);
                        string stringTipoOperacion = dr.GetString(2);
                        TipoOperacion tipoOperacion = new TipoOperacion(stringTipoOperacion);
                        decimal monto = dr.GetSqlMoney(3).ToDecimal();

                        Operaciones p = new Operaciones(fecha, hora, tipoOperacion, Math.Round(monto, 2), idCV);
                        lista.Add(p);
                    }
                    dr.Close();

                }

             

               
            }
            return lista;
        }
    }
}