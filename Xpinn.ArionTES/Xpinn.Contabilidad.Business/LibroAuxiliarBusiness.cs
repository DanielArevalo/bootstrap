using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para concepto
    /// </summary>
    public class LibroAuxiliarBusiness : GlobalBusiness
    {
        private LibroAuxiliarData DALibroAuxiliar;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public LibroAuxiliarBusiness()
        {
            DALibroAuxiliar = new LibroAuxiliarData();
        }

        /// <summary>
        /// Método para generar el libro auxiliar
        /// </summary>
        /// <param name="CenIni">Centro de costo inicial</param>
        /// <param name="CenFin">Centro de costo final</param>
        /// <param name="FecIni">Fecha inicial</param>
        /// <param name="FecFin">Fecha final</param>
        /// <param name="sCuentas">Listado de cuentas a generar</param>
        /// <param name="vUsuario">Usuario que genera</param>
        /// <returns></returns>
        public List<LibroAuxiliar> ListarAuxiliar(Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, string cod_cuenta_ini, string cod_cuenta_fin, Boolean por_rango, Int32 moneda, string pOrdenar, Usuario vUsuario)
        {
            List<LibroAuxiliar> lstLibAux = new List<LibroAuxiliar>();
            string sCuentas = "";
            if (por_rango)
                sCuentas = " Power(10, 10-length(d.cod_cuenta))*to_number(d.cod_cuenta) Between " + Math.Pow(10, 10 - cod_cuenta_ini.Length) * Convert.ToInt64(cod_cuenta_ini) + " And " + Math.Pow(10, 10 - cod_cuenta_fin.Length) * Convert.ToInt64(cod_cuenta_fin) + " ";
            else
                sCuentas = " d.cod_cuenta In ('" + cod_cuenta_ini + "'" + CargarCuentas(cod_cuenta_ini, vUsuario) + ") ";
            lstLibAux = DALibroAuxiliar.ListarAuxiliar(CenIni, CenFin, FecIni, FecFin, sCuentas,moneda, pOrdenar, vUsuario);
            double saldo = 0;
            string cod_cuenta = "";

            foreach (LibroAuxiliar LibAux in lstLibAux)
            {
                if (LibAux.detalle == "SALDO INICIAL")
                {
                    cod_cuenta = LibAux.cod_cuenta;
                    saldo = SaldoCuenta(LibAux.cod_cuenta, FecIni, CenIni, CenFin, vUsuario);
                }
                else
                {
                    if (LibAux.tipo == LibAux.naturaleza)
                        saldo = saldo + LibAux.valor;                        
                    else
                    {
                        saldo = saldo - LibAux.valor;
                    }
                    if (LibAux.tipo == "D")
                    {
                        LibAux.debito = LibAux.valor;
                        LibAux.credito = 0;
                    }
                    else
                    {
                        LibAux.debito = 0;
                        LibAux.credito = LibAux.valor;
                    }
                }
                LibAux.saldo = saldo;
            }

            return lstLibAux;
        }

        /// <summary>
        /// Método para calcular el saldo de una cuenta a una fecha dada
        /// </summary>
        /// <param name="cod_cuenta">código de la cuenta contable</param>
        /// <param name="fecha">fecha a la cual se quiere saber el saldo</param>
        /// <param name="cenini">centro de costo inicial</param>
        /// <param name="cenfin">centro de costo final</param>
        /// <returns>retorna el saldo de la cuenta</returns>
        public double SaldoCuenta(string cod_cuenta, DateTime fecha, Int64 cenini, Int64 cenfin, Usuario pUsuario)
        {
            return DALibroAuxiliar.SaldoCuenta(cod_cuenta, fecha, cenini, cenfin, pUsuario);
        }


        /// <summary>
        /// Método para determinar cuentas hijas de una cuenta
        /// </summary>
        /// <param name="sCod_Cuenta"></param>
        /// <returns></returns>
        public string CargarCuentas(string sCod_Cuenta, Usuario pUsuario)
        {
            return DALibroAuxiliar.CargarCuentas(sCod_Cuenta, pUsuario);
        }

        /// <summary>
        /// Método para determinar la fecha inicial
        /// </summary>
        /// <param name="tipocierre"></param>
        /// <returns></returns>
        public DateTime DeterminarFechaInicial(string tipocierre, Usuario pUsuario)
        {
            return DALibroAuxiliar.DeterminarFechaInicial(tipocierre, pUsuario);
        }

    }
}

