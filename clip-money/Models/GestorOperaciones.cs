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

        public int deposito(Deposito deposito)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            int message = 0;

            if(deposito.Monto > 0)
            {
                using (SqlConnection conn = new SqlConnection(StrConn))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand comm = new SqlCommand("deposito", conn);
                        comm.CommandType = System.Data.CommandType.StoredProcedure;

                        comm.Parameters.Add(new SqlParameter("@id_cuenta_virtual", deposito.Id_cuenta_virtual));
                        comm.Parameters.Add(new SqlParameter("@monto", deposito.Monto));

                        comm.ExecuteNonQuery();

                        return message;

                    }
                    catch (Exception e)
                    {
                        message = 2; //No se ha podido registrar el deposito por alguna excepción
                        return message;
                    }

                }
            }
            else
            {
                message = 1; //Indica que el monto que ingreso el usuario no es valido
                return message;
            }
        }

        public int extraccion(Extraccion extraccion)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            int message = 0;

            if(extraccion.Monto > 0)
            {
                using (SqlConnection conn = new SqlConnection(StrConn))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand comm = new SqlCommand("extraccion", conn);
                        comm.CommandType = System.Data.CommandType.StoredProcedure;

                        comm.Parameters.Add(new SqlParameter("@id_cuenta_virtual", extraccion.Id_cuenta_virtual));
                        comm.Parameters.Add(new SqlParameter("@monto", extraccion.Monto));

                        SqlDataReader dr = comm.ExecuteReader();
                        dr.Read();
                        int resultado = dr.GetInt32(0);

                        if(resultado == 1)
                        {
                            return message; //Es cero y significa que la extraccion se realizo exitosamente.
                        }
                        else
                        {
                            message = 3; //No posee fondos suficientes
                            return message;
                        }


                    }
                    catch (Exception e)
                    {
                        message = 2; //No se ha podido registrar el deposito por alguna excepción
                        return message;
                    }
                }
            }
            else
            {
                message = 1; //Indica que el monto que ingreso el usuario no es valido
                return message;
            }
        }

        public int giro(Giro giro)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            int message = 0;

            if (giro.Monto > 0)
            {
                using (SqlConnection conn = new SqlConnection(StrConn))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand comm = new SqlCommand("giro", conn);
                        comm.CommandType = System.Data.CommandType.StoredProcedure;

                        comm.Parameters.Add(new SqlParameter("@id_cuenta_virtual", giro.Id_cuenta_virtual));
                        comm.Parameters.Add(new SqlParameter("@monto", giro.Monto));

                        SqlDataReader dr = comm.ExecuteReader();
                        dr.Read();
                        int resultado = dr.GetInt32(0);

                        if (resultado == 2)
                        {
                            message = 2; //significa que la extraccion se realizo exitosamente.
                            return message; 
                        }
                        else if (resultado == 3)
                        {
                            message = 3; //No hay fondos suficientes
                            return message;
                        }
                        else if (resultado == 0)
                        {
                            return message; //No lo modifico porque ya es cero cuando se define y segnifica Utilizar la opcion de retiro de dinero y no la de giro
                        }
                        else
                        {
                            message = 1; //El monto a girar el mayor al asignado
                            return message;
                        }


                    }
                    catch (Exception e)
                    {
                        message = 4; //No se ha podido registrar el deposito por alguna excepción
                        return message;
                    }
                }
            }
            else
            {
                message = 5; //Indica que el monto que ingreso el usuario no es valido
                return message;
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