using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using clip_money.Models;
using System.Web.Http.Cors;

namespace clip_money.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    [RoutePrefix("api/cliente")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClienteController : ApiController
    {
        //[Authorize]
        // GET: api/Cliente
        /*  public IEnumerable<Cliente> Get()
          {
              GestorCliente gCliente = new GestorCliente();
              return gCliente.obtenerClientes();
          }

          //[Authorize]
          // GET: api/Cliente/"número de id"
          public Cliente Get(int id)
          {
              GestorCliente gCliente = new GestorCliente();
              return gCliente.obtenerPorId(id);
          }
        */
        // POST: api/Cliente
        [HttpPost]
        public int Post([FromBody] Cliente nuevo)
        {
            GestorCliente gCliente = new GestorCliente();
            return gCliente.nuevoCliente(nuevo);
        }
        /* public Cliente Post(Cliente nuevo)
         {
             int id;
             GestorCliente gCliente = new GestorCliente();
             id = gCliente.nuevoCliente(nuevo);
             nuevo.Id = id;
             return nuevo;
         }*/
        //[Authorize]
        // PUT: api/Cliente/"número de id"
        /*  public HttpResponseMessage Put([FromBody] Cliente mod)
          {
              GestorCliente gCliente = new GestorCliente();
              return gCliente.modificarCliente(mod);
          }

          [Authorize]
          // DELETE: api/Cliente/"número de id"
          public void Delete(int id)
          {
              GestorCliente gCliente = new GestorCliente();
              gCliente.eliminarCliente(id);
          }*/
    }
}
