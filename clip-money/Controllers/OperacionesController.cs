﻿using clip_money.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace clip_money.Controllers
{
    [Authorize]
    [RoutePrefix("api/operaciones")]
    public class OperacionesController : ApiController
    {
        // GET: api/Persona
        [Route("top_diez")]
        public IEnumerable<Operaciones> Get(long idCV)
        {
            GestorOperaciones gOperaciones = new GestorOperaciones();
            return gOperaciones.obtenerTop10Operaciones(idCV);
        }

    }
}
