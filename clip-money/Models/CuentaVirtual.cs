using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clip_money.Models
{
    public class CuentaVirtual
    {
        private long id;
        private string alias;
        private string cvu;
        private string nroCuenta;
        private decimal montoDescubierto;
        private int idCliente;
        private TipoCuentaVirtual idTipoCuenta;
        private int idEstado;
        private decimal saldo; //Agregue este campo para cuando se obtengan las cuentas del cliente con su saldo porder guardar
        //el saldo calculado desde la DB en este atributo. El saldo de las cuentas se calcula, no esta guardado en ningun atributo.

        public CuentaVirtual()
        {

        }
        public CuentaVirtual(long id)
        {
            this.id = id;
        }

        public CuentaVirtual(long id, string alias, string cvu, string nroCuenta, decimal montoDescubierto, int idCliente, TipoCuentaVirtual idTipoCuenta, int idEstado)
        {
            this.id = id;
            this.alias = alias;
            this.cvu = cvu;
            this.nroCuenta = nroCuenta;
            this.montoDescubierto = montoDescubierto;
            this.idCliente = idCliente;
            this.idTipoCuenta = idTipoCuenta;
            this.idEstado = idEstado;
        }

        public CuentaVirtual(long id, string nroCuenta, TipoCuentaVirtual tipoCuenta, decimal saldo)
        {
            this.id = id;
            this.nroCuenta = nroCuenta;
            this.idTipoCuenta = tipoCuenta;
            this.saldo = saldo;
        }

        public CuentaVirtual(long id, string alias, string cvu, string nroCuenta, decimal montoDescubierto, int idCliente, TipoCuentaVirtual idTipoCuenta, int idEstado, decimal saldo)
        {
            this.id = id;
            this.alias = alias;
            this.cvu = cvu;
            this.nroCuenta = nroCuenta;
            this.montoDescubierto = montoDescubierto;
            this.idCliente = idCliente;
            this.idTipoCuenta = idTipoCuenta;
            this.idEstado = idEstado;
            this.saldo = saldo;
        }

        public long Id { get => id; set => id = value; }
        public string Alias { get => alias; set => alias = value; }
        public string Cvu { get => cvu; set => cvu = value; }
        public string NroCuenta { get => nroCuenta; set => nroCuenta = value; }
        public decimal MontoDescubierto { get => montoDescubierto; set => montoDescubierto = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }
        public TipoCuentaVirtual IdTipoCuenta { get => idTipoCuenta; set => idTipoCuenta = value; }
        public int IdEstado { get => idEstado; set => idEstado = value; }
        public decimal Saldo { get => saldo; set => saldo = value; }

    }
}