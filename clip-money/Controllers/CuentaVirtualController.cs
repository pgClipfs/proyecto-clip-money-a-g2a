using clip_money.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace clip_money.Controllers
{
    public class CuentaVirtualController : ApiController
    {
        // GET: api/CuentaVirtual
        public IEnumerable<CuentaVirtual> Get()
        {
            GestorCuentaVirtual gCuentavirtual   = new GestorCuentaVirtual();
            return gCuentavirtual.ObtenerCuentaVirtual();
        }

        // GET: api/CuentaVirtual/5
        public string Get(int id)
        {
           
            GestorCuentaVirtual gCuentaVirtual = new GestorCuentaVirtual();
            return gCuentaVirtual.ObtenerPorId(id);
        }

        // POST: api/CuentaVirtual
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CuentaVirtual/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CuentaVirtual/5
        public void Delete(int id)
        {
        }
    }
}
