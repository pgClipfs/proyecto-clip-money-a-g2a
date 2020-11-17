using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

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
                    Cliente idCliente = new Cliente();
                    TipoCuentaVirtual idTipoCuenta = new TipoCuentaVirtual();
                    Estado idEstado = new Estado();

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
                    decimal montoDescubierto = dr.GetDecimal(4);
                    Cliente idCliente = new Cliente();
                    TipoCuentaVirtual idTipoCuenta = new TipoCuentaVirtual();
                    Estado idEstado = new Estado();

                    cv = new CuentaVirtual(id, alias, cvu, nroCuenta, montoDescubierto, idCliente, idTipoCuenta, idEstado);
                }
                dr.Close();
            }
            return cv;
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
                comm.Parameters.Add(new SqlParameter("@id_tipo_cuenta", nueva.IdTipoCuenta));
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
    }
}