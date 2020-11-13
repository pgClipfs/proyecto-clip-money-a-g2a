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
        private Cliente idCliente;
        private TipoCuentaVirtual idTipoCuenta;
        private Estado idEstado;

        public CuentaVirtual()
        {

        }

        public CuentaVirtual(long id, string alias, string cvu, string nroCuenta, decimal montoDescubierto, Cliente idCliente, TipoCuentaVirtual idTipoCuenta, Estado idEstado)
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
        public long Id { get => id; set => id = value; }
        public string Alias { get => alias; set => alias = value; }
        public string Cvu { get => cvu; set => cvu = value; }
        public string NroCuenta { get => nroCuenta; set => nroCuenta = value; }
        public decimal MontoDescubierto { get => montoDescubierto; set => montoDescubierto = value; }
        public Cliente IdCliente { get => idCliente; set => idCliente = value; }
        public TipoCuentaVirtual IdTipoCuenta { get => idTipoCuenta; set => idTipoCuenta = value; }
        public Estado IdEstado { get => idEstado; set => idEstado = value; }

        // Se genera un alias de forma aleatoria.
        public void aliasAzar()
        {
            // Hacer procedimiento almacenado
        }

        // Se genera un cvu de forma aleatoria.
        public void cvuAzar()
        {
            // Hacer procedimiento almacenado
        }

        // Se genera un número de cuenta de forma aleatoria.
        public void nroCuentaAzar()
        {
            // Hacer procedimiento almacenado
        }
    }
}