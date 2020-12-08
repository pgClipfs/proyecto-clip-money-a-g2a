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

                gVPassword.SendMail(mail.email, tokenCambioPassword);

                var email = mail.email;
                return Ok(email);
            }
            else
            {
                return Unauthorized();
            }
        }

      
    }
}
