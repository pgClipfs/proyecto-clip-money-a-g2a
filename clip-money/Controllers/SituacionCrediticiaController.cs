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
    public class SituacionCrediticiaController : ApiController
    {
        // GET: api/SituacionCrediticia
        public IEnumerable<SituacionCrediticia> Get()
        {
            GestorDniSitCrediticia sitcred = new GestorDniSitCrediticia();
            return sitcred.obtenerSitCrediticia();
        }

        // GET: api/SituacionCrediticia/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SituacionCrediticia
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SituacionCrediticia/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SituacionCrediticia/5
        public void Delete(int id)
        {
        }
    }
}
