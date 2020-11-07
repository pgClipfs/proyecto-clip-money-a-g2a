using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class CuentaVirtual
    {
        private int id;
        private string alias;
        private long cvu;
        private int nroCuenta;
        private Decimal montoDescubierto;
        private Cliente id_cliente;
        private TipoCuentaVirtual id_tipo_cuenta;
        private Estado id_estado;
    }
}