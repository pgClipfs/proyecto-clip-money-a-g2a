using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using clip_money.Models;

namespace clip_money.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/dni")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DniController : ApiController
    {
        
        [HttpPost]
        public bool Post([FromBody] Cliente nuevo)
        {
            GestorCliente gCliente = new GestorCliente();
            return gCliente.insertarFotosDni(nuevo);
        }
    }
}
