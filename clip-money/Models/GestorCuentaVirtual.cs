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
                comm.CommandText = "SELECT * FROM cuenta_virtual";
                //comm.CommandText = "obtener_Cuentas_Virtuales";
                //comm.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    long id = dr.GetInt64(0);
                    string alias = dr.GetString(1).Trim();
                    string cvu = dr.GetString(2).Trim();
                    string nroCuenta = dr.GetString(3).Trim();
                    decimal montoDescubierto = dr.GetDecimal(4);
                    //Cliente idCliente = dr.GetInt32(5);
                    //TipoCuentaVirtual idTipoCuenta = dr.GetByte(6);
                    //Estado idEstado = dr.GetByte(7);

                    CuentaVirtual cv = new CuentaVirtual(id, alias, cvu, nroCuenta, montoDescubierto/*, idCliente, idTipoCuenta, idEstado*/);

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

                SqlCommand comm = new SqlCommand("SELECT * FROM cuenta_virtual WHERE id=@id", conn);
                //SqlCommand comm = new SqlCommand("obtener_Cuenta_Virtual", conn);
                //comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader dr = comm.ExecuteReader();
                if (dr.Read())
                {
                    string alias = dr.GetString(1);
                    string cvu = dr.GetString(2);
                    string nroCuenta = dr.GetString(3);
                    decimal montoDescubierto = dr.GetDecimal(4);
                    //Cliente idCliente = dr.GetInt32(5);
                    //TipoCuentaVirtual idTipoCuenta = dr.GetByte(6);
                    //Estado idEstado = dr.GetByte(7);

                    cv = new CuentaVirtual(id, alias, cvu, nroCuenta, montoDescubierto/*, idCliente, idTipoCuenta, idEstado*/);
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

                comm.CommandText = "INSERT INTO cuenta_virtual(alias, cvu, nro_cuenta, monto_descubierto, id_cliente, id_tipo_cuenta, id_estado) VALUES (@alias, @cvu, @nroCuenta, @montoDescubierto, @idCliente, @idTipoCuenta, @idEstado)";
                //comm.CommandText = "nueva_Cuenta_Virtual";
                //comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@alias", nueva.Alias));
                comm.Parameters.Add(new SqlParameter("@cvu", nueva.Cvu));
                comm.Parameters.Add(new SqlParameter("@nroCuenta", nueva.NroCuenta));
                comm.Parameters.Add(new SqlParameter("@montoDescubierto", nueva.MontoDescubierto));
                //comm.Parameters.Add(new SqlParameter("@idCliente", nueva.IdCliente));
                //comm.Parameters.Add(new SqlParameter("@idTipoCuenta", nueva.IdTipoCuenta));
                //comm.Parameters.Add(new SqlParameter("@idEstado", nueva.IdEstado));

                comm.ExecuteNonQuery();
            }
        }

        public void EliminarCuenta_Virtual(int id)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("DELETE FROM cliente WHERE id=@id", conn);
                //SqlCommand comm = new SqlCommand("eliminar_Cuenta_Virtual", conex);
                //comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@id", id));

                comm.ExecuteNonQuery();
            }
        }

        public void ModificarCuenta_Virtual(CuentaVirtual mod)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "UPDATE cliente SET alias=@alias, cvu=@cvu, nro_cuenta=@nroCuenta, monto_descubierto=@montoDescubierto, id_cliente=@idCliente, id_tipo_cuenta=@idTipoCuenta, id_estado=@idEstado WHERE id=@id";
                //comm.CommandText = "modificar_Cuenta_Virtual";
                //comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@id", mod.Id));
                comm.Parameters.Add(new SqlParameter("@alias", mod.Alias));
                comm.Parameters.Add(new SqlParameter("@cvu", mod.Cvu));
                comm.Parameters.Add(new SqlParameter("@nroCuenta", mod.NroCuenta));
                comm.Parameters.Add(new SqlParameter("@montoDescubierto", mod.MontoDescubierto));
                //comm.Parameters.Add(new SqlParameter("@idCliente", mod.IdCliente));
                //comm.Parameters.Add(new SqlParameter("@idTipoCuenta", mod.IdTipoCuenta));
                //comm.Parameters.Add(new SqlParameter("@idEstado", mod.IdEstado));

                comm.ExecuteNonQuery();
            }
        }
    }
}