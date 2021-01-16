using clip_money.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace clip_money.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/operaciones")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OperacionesController : ApiController
    {
        // GET: api/operaciones
        [Route("top_diez")]
        public IEnumerable<Operaciones> Get(long idCV)
        {
            GestorOperaciones gOperaciones = new GestorOperaciones();
            return gOperaciones.obtenerTop10Operaciones(idCV);
        }

        //POST: api/operaciones/deposito
        [Route("deposito")]
        [HttpPost]
        public int deposito([FromBody] Deposito deposito)
        {
            GestorOperaciones gOperaciones = new GestorOperaciones();
            return gOperaciones.deposito(deposito);
        }

        //POST: api/operaciones/deposito
        [Route("extraccion")]
        [HttpPost]
        public int extraccion([FromBody] Extraccion extraccion)
        {
            GestorOperaciones gOperaciones = new GestorOperaciones();
            return gOperaciones.extraccion(extraccion);
        }


        //POST: api/operaciones/giro
        [Route("giro")]
        [HttpPost]
        public int giro([FromBody] Giro giro)
        {
            GestorOperaciones gOperaciones = new GestorOperaciones();
            return gOperaciones.giro(giro);
		}

        //GET: api/operaciones/montoPosibleGiro
        [Route("montoPosibleGiro")]
        [HttpGet]
        public decimal montoPosibleGiro( long idCuenta)
        {
            GestorOperaciones gOperaciones = new GestorOperaciones();
            return gOperaciones.obtenerMontoGiroPosible(idCuenta);
        }


        //POST: api/operaciones/transferencia
        [Route("transferencia")]
        [HttpPost]
        public int transferencia([FromBody] Transferencia transferencia)
        {
            GestorOperaciones gOperaciones = new GestorOperaciones();
            return gOperaciones.transferencia(transferencia);
        }

        // GET: api/Persona
        [Route("movimientos")]
        public IEnumerable<Operaciones> GetOpetacionesTodas(long idCV, string fechadesde, string fechahasta, int concepto)
        {
            GestorOperaciones gOperaciones = new GestorOperaciones();
            return gOperaciones.obtenerOperacionesTodas(idCV, fechadesde, fechahasta, concepto);
        }

    }
}
