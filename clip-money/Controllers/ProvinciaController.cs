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
    public class ProvinciaController : ApiController
    {
        // GET: api/Provincia
        
        public IEnumerable<Provincia> Get()
        {
            GestorPPL provincias = new GestorPPL();
            return provincias.obtenerProvincias();
        }
        
        // GET: api/Provincia/5
        public List<Provincia> Get(int id)
        {
            GestorPPL provincia = new GestorPPL();
            return provincia.obtenerProvincia(id);
        }
        

        // POST: api/Provincia
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Provincia/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Provincia/5
        public void Delete(int id)
        {
        }
    }
}
