using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace clip_money.Models
{
    public class GestorValidarPassword
    {
        //Metodo para validar que el mail ingresado en el cambio de contraseña exista en la BD
        public bool validarEmail(EmailValidator email)
        {
            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            bool result = false;

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("obtenerEmail", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@email", email.email));

                SqlDataReader reader = comm.ExecuteReader();

                if (reader.HasRows)
                {
                    result = true;
                }
            }
            return result;
        }

        //Metodo para insertar el token en la tabla clientes, en la columna token_recovery, y que enviaremos al mail de cambio de contraseña
        public void insertarTokenEmail(string token, string email)
        {
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("insertarTokenEmail", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add(new SqlParameter("@email", email));
                comm.Parameters.Add(new SqlParameter("@token", token));

                SqlDataReader dr = comm.ExecuteReader();
            }
        }

        //Metodo para validar el token ingresado en el formulario de cambio de contraseña con el token que esta en la BD
        public bool validarTokenEmail(NewPasswordValidator np)
        {
            string strConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();
            bool result = false;

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("validarTokenEmail", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@email", np.email));
                comm.Parameters.Add(new SqlParameter("@token", np.token));

                SqlDataReader reader = comm.ExecuteReader();

                if (reader.HasRows)
                {
                    result = true;
                }
            }
            return result;
        }

        //Metodo que se llama una vez se valide todo, y se realiza el cambio de contraseña
        public void modificarPassword(NewPasswordValidator np)
        {

            string encriptedPassword = GetSha256(np.newpassword);
            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("modificarPassword", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@password", encriptedPassword));
                comm.Parameters.Add(new SqlParameter("@email", np.email));

                SqlDataReader dr = comm.ExecuteReader();
            }
        }

        public int modificarPassdesdeApp(PasswordModify pw)
        {
            int message = 1;
            string encriptedNewPassword = GetSha256(pw.passwordNueva);
            string encriptedActualPassword = GetSha256(pw.passwordActual);

            string StrConn = ConfigurationManager.ConnectionStrings["BDLocal"].ToString();

            using (SqlConnection conn = new SqlConnection(StrConn))
            {
                conn.Open();

                SqlCommand comm = new SqlCommand("modificarPassdesdeApp", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@idCliente", pw.idCliente));
                comm.Parameters.Add(new SqlParameter("@newpassword", encriptedNewPassword));
                comm.Parameters.Add(new SqlParameter("@actualpassword",encriptedActualPassword));

                SqlDataReader dr = comm.ExecuteReader();
                dr.Read();
                int resultado = dr.GetInt32(0);

                if (resultado == 1)
                {
                    return message; // se modifico correctamente
                }
                else if(resultado == 2)
                {
                    message = 2; //la contraseña actual ingresada no coincide con la que esta en la base
                    return message;
                }
                else if (resultado == 3)
                {
                    message = 3; //la contraseña actual y la contraseña nueva son iguales
                    return message;
                }else
                {
                    message = 4; //Error inesperado
                    return message;
                }
            }
        }



        //Metodos HELPERS
        #region HELPERS

        /*FUNCION PARA HASHEAR UN STRING*/
        public string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();

        }

        /*FUNCION ENVIAR EMAIL*/
        public void SendMail(string emailDestino, string token)
        {
            string usuario;
            GestorCliente gcliente = new GestorCliente();
            usuario = gcliente.obtenerUsuario(emailDestino);

            string emailOrigen = "clip.money.cba@gmail.com";
            string contraseña = "clipmoney2020";

            MailMessage oMailMessage = new MailMessage(emailOrigen, emailDestino, "Cambio Contraseña",
                "<p>Para realizar el cambio de contraseña utilize el siguiente token<p><br>" +
                "<p>Usuario: <p>" + usuario + "<br>" +
                "<p> TOKEN: <p>" + token); ;

            oMailMessage.IsBodyHtml = true;

            SmtpClient osmtpClient = new SmtpClient("smtp.gmail.com");
            osmtpClient.EnableSsl = true;
            osmtpClient.UseDefaultCredentials = false;
            osmtpClient.Port = 587;
            osmtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, contraseña);

            osmtpClient.Send(oMailMessage);
        }

        #endregion
    }
}