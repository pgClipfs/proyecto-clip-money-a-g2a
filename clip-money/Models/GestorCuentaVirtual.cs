using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace clip_money.Models
{
    public class GestorCuentaVirtual
    {
        public List<CuentaVirtual> obtenerCuentasVirtuales()
        {
            List<CuentaVirtual> lista = new List<CuentaVirtual>();

            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "obtenerCuentasVirtuales";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    long id = dr.GetInt64(0);
                    string alias = dr.GetString(1).Trim();
                    string cvu = dr.GetString(2).Trim();
                    string nroCuenta = dr.GetString(3).Trim();
                    decimal montoDescubierto = dr.GetDecimal(4);
                    int idCliente = dr.GetInt32(5);
                    TipoCuentaVirtual idTipoCuenta = new TipoCuentaVirtual();
                    int idEstado = dr.GetInt32(7);

                    CuentaVirtual cv = new CuentaVirtual(id, alias, cvu, nroCuenta, montoDescubierto, idCliente, idTipoCuenta, idEstado);

                    lista.Add(cv);
                }
                dr.Close();
            }
            return lista;
        }

        public CuentaVirtual obtenerPorId(int id)
        {
            CuentaVirtual cv = null;
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("obtenerCuentaVirtual", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    string alias = dr.GetString(1);
                    string cvu = dr.GetString(2);
                    string nroCuenta = dr.GetString(3);
                    decimal montoDescubierto = dr.GetInt32(4);
                    int idCliente = dr.GetInt32(5);
                    int idTipoCuenta = dr.GetInt32(6);
                    TipoCuentaVirtual tipocuenta = new TipoCuentaVirtual(idTipoCuenta);
                    int idEstado = dr.GetInt32(7);

                    cv = new CuentaVirtual(id, alias, cvu, nroCuenta, montoDescubierto, idCliente, tipocuenta, idEstado);
                }
                dr.Close();
            }
            return cv;
        }

        //Devuelve las cuentas de un cliente dado su Id de cliente
        public List<CuentaVirtual> obtenerPorIdCliente(int id)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            List<CuentaVirtual> lista = new List<CuentaVirtual>();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "obtenerCuentasCliente";

                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@id_cliente", id));

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    long idCuentaVirtual = dr.GetInt64(0);
                    string nroCuenta = dr.GetString(1);
                    string tipoCuenta = dr.GetString(2);
                    TipoCuentaVirtual tipoCuentaVirtual = new TipoCuentaVirtual(tipoCuenta);
                    decimal saldo = dr.GetSqlMoney(3).ToDecimal();

                    CuentaVirtual cuentaVirtual = new CuentaVirtual(idCuentaVirtual, nroCuenta, tipoCuentaVirtual, Math.Round(saldo,2));
                    lista.Add(cuentaVirtual);
                }
                dr.Close();
            }
            return lista;
        }
        public void nuevaCuentaVirtual(CuentaVirtual nueva)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "nuevaCuentaVirtual";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@alias", nueva.Alias));
                comm.Parameters.Add(new SqlParameter("@cvu", nueva.Cvu));
                comm.Parameters.Add(new SqlParameter("@nro_cuenta", nueva.NroCuenta));
                comm.Parameters.Add(new SqlParameter("@monto_descubierto", nueva.MontoDescubierto));
                comm.Parameters.Add(new SqlParameter("@id_cliente", nueva.IdCliente));
                comm.Parameters.Add(new SqlParameter("@id_tipo_cuenta", nueva.IdTipoCuenta.Id));
                comm.Parameters.Add(new SqlParameter("@id_estado", nueva.IdEstado));

                comm.ExecuteNonQuery();
            }
        }

        public void eliminarCuentaVirtual(int id)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("eliminarCuentaVirtual", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@id", id));

                comm.ExecuteNonQuery();
            }
        }

        public void modificarCuentaVirtual(CuentaVirtual mod)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "modificarCuentaVirtual";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@id", mod.Id));
                comm.Parameters.Add(new SqlParameter("@alias", mod.Alias));
                comm.Parameters.Add(new SqlParameter("@cvu", mod.Cvu));
                comm.Parameters.Add(new SqlParameter("@nroCuenta", mod.NroCuenta));
                comm.Parameters.Add(new SqlParameter("@montoDescubierto", mod.MontoDescubierto));
                comm.Parameters.Add(new SqlParameter("@idCliente", mod.IdCliente));
                comm.Parameters.Add(new SqlParameter("@idTipoCuenta", mod.IdTipoCuenta));
                comm.Parameters.Add(new SqlParameter("@idEstado", mod.IdEstado));

                comm.ExecuteNonQuery();
            }
        }

        //OBTENER ID CUENTA VIRTUAL A TRAVES DEL NUMERO DE CUENTA
        public long obtenerPorNroCuenta(string nrocuenta)
        {
            long id = 0;
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("obtenerCuentaVirtualporNumero", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@nrocuenta", nrocuenta));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                     id = dr.GetInt64(0);
                }
                dr.Close();
            }
            return id;
        }

        //METODOS PARA GENERAR CVU Y CUENTA ALEATORIAS
        public string generarcvuAleatorio()
        {
            int longitud = 22;
            const string alfabeto = "0123456789";
            StringBuilder cvu = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < longitud; i++)
            {
                int indice = rnd.Next(alfabeto.Length);
                cvu.Append(alfabeto[indice]);
            }

            return cvu.ToString();
        }
        public string generarCuentaAleatoria()
        {
            int longitud = 7;
            const string alfabeto = "0123456789";
            StringBuilder cuenta = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < longitud; i++)
            {
                int indice = rnd.Next(alfabeto.Length);
                cuenta.Append(alfabeto[indice]);
            }

            return cuenta.ToString();
        }

        //VALIDACION PARA VER SI LA CUENTA O EL CVU RANDOM YA EXISTEN 
        public bool existeCuentaCvu(string cuenta, string cvu)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("existeCuentaoCvu", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@cuenta", cuenta));
                comm.Parameters.Add(new SqlParameter("@cvu", cvu));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //GENERAR NUMERO DE OPERACION DE UNA CUENTA ALEATORIO
        public string generarNroOperacionAleatorio()
        {
            int longitud = 7;
            const string alfabeto = "0123456789";
            StringBuilder nroOperacion = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < longitud; i++)
            {
                int indice = rnd.Next(alfabeto.Length);
                nroOperacion.Append(alfabeto[indice]);
            }

            return nroOperacion.ToString();
        }
        //VALIDACION SI EXISTE UN NUMERO DE OPERACION EN LA CUENTA VIRTUAL SELECCIONADA
        public bool existeNroOperacion(long idCuenta, string nroOperacion)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("existeNroOperacion", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@idCuenta", idCuenta));
                comm.Parameters.Add(new SqlParameter("@nroOperacion", nroOperacion));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


    }
}