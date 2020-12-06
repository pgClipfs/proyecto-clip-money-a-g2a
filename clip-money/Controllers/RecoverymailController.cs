using clip_money.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace clip_money.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/recoverymail")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RecoverymailController : ApiController
    {

        [HttpPost]
        [Route("validation")]
        public IHttpActionResult validation(EmailValidator mail)
        {
            if (mail == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            GestorValidarPassword gVPassword = new GestorValidarPassword();

            bool isCredentialValid = gVPassword.validarEmail(mail);

            if (isCredentialValid)
            {
                string tokenCambioPassword = Guid.NewGuid().ToString();

                //insertar el token en el cliente
                gVPassword.insertarTokenEmail(tokenCambioPassword,mail.email);

                // enviar el mail

                SendMail(mail.email, tokenCambioPassword);

                /*var token = TokenGenerator.GenerateTokenJwt(login.NombreUsuario);*/
                var email = mail.email;
                return Ok(email);
            }
            else
            {
                return Unauthorized();
            }
        }

        #region HELPERS

        /*FUNCION PARA HASHEAR UN STRING*/
        private string GetSha256(string str) 
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
        private void SendMail(string emailDestino, string token)
        {
            string usuario;
            GestorCliente gcliente = new GestorCliente();
            usuario = gcliente.obtenerUsuario(emailDestino);

            string emailOrigen = "clip.money.cba@gmail.com";
            string contraseña = "clipmoney2020";

            MailMessage oMailMessage = new MailMessage(emailOrigen, emailDestino, "Cambio Contraseña",
                "<p>Para realizar el cambio de contraseña utilize el siguiente token<p><br>" +
                "<p>Usuario: <p>" + usuario +"<br>" +
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
