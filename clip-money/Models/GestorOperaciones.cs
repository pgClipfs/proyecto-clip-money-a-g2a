﻿using System;
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

                    Operaciones p = new Operaciones(fecha, hora, tipoOperacion, Math.Round(monto, 2), idCV);
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

            if (deposito.Monto > 0)
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

            if (extraccion.Monto > 0)
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

                        if (resultado == 1)
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

        public int transferencia(Transferencia transferencia)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            int message = 0;

            if (transferencia.Monto > 0)
            {
                using (SqlConnection conn = new SqlConnection(StrConn))
                {
                    try
                    {
                        conn.Open();

                        SqlCommand comm = new SqlCommand("transferencia", conn);
                        comm.CommandType = System.Data.CommandType.StoredProcedure;

                        comm.Parameters.Add(new SqlParameter("@id_cuenta_virtual", transferencia.Id_cuenta_virtual));
                        comm.Parameters.Add(new SqlParameter("@monto", transferencia.Monto));
                        comm.Parameters.Add(new SqlParameter("@alias", transferencia.Alias));

                        SqlDataReader dr = comm.ExecuteReader();
                        dr.Read();
                        int resultado = dr.GetInt32(0);

                        if (resultado == 1)
                        {
                            return message; //Es cero y significa que la transferencia se realizó exitosamente.
                        }
                        else
                        {
                            message = 3; //No posee fondos suficientes
                            return message;
                        }
                    }
                    catch (Exception)
                    {
                        message = 2; //No se ha podido registrar la transferencia por alguna excepción
                        return message;
                    }
                }
            }
            else
            {
                message = 1; //Indica que el monto que ingreso el usuario no es válido
                return message;
            }
        }

        public List<Operaciones> obtenerOperacionesTodas(long idCV, string fechadesde, string fechahasta, int concepto)
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