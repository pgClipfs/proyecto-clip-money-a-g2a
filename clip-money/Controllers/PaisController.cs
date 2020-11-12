using clip_money.Models;
using Clip_money.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace clip_money.Controllers
{
    public class PaisController : ApiController
    {
        // GET: api/Pais
        public IEnumerable<Pais> Get()
        {
            GestorPPL gpais = new GestorPPL();
            return gpais.obtenerPais();
        }

        // GET: api/Pais/5
        public Pais Get(int id)
        {
            GestorPPL gpais = new GestorPPL();
            return gpais.ObtenerPaisPorId(id);
        }

        // POST: api/Pais
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Pais/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pais/5
        public void Delete(int id)
        {
        }
    }
}
