using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.NIIF.Data;
using Xpinn.NIIF.Entities;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.NIIF.Business
{
    /// <summary>
    /// Objeto de negocio para Parametros
    /// </summary>
    public class LibroAuxiliarNIFBussines : GlobalData
    {
        private LibroAuxiliarNIFData BALibroAuxiliar;


        public LibroAuxiliarNIFBussines()
        {
            BALibroAuxiliar = new LibroAuxiliarNIFData();
        }


        public List<LibroAuxiliar> ListarAuxiliar(Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, string cod_cuenta_ini, string cod_cuenta_fin, Boolean por_rango, Int32 moneda, string pOrdenar, Usuario vUsuario)
        {
            List<LibroAuxiliar> lstLibAux = new List<LibroAuxiliar>();
            string sCuentas = "";
            if (por_rango)
                sCuentas = " Power(10, 10-length(d.cod_cuenta_niif))*to_number(d.cod_cuenta_niif) Between " + Math.Pow(10, 10 - cod_cuenta_ini.Length) * Convert.ToInt64(cod_cuenta_ini) + " And " + Math.Pow(10, 10 - cod_cuenta_fin.Length) * Convert.ToInt64(cod_cuenta_fin) + " ";
            else
                sCuentas = " d.cod_cuenta_niif In ('" + cod_cuenta_ini + "'" + CargarCuentasNiif(cod_cuenta_ini, vUsuario) + ") ";
            lstLibAux = BALibroAuxiliar.ListarAuxiliar(CenIni, CenFin, FecIni, FecFin, sCuentas, moneda, pOrdenar, vUsuario);
            double saldo = 0;
            string cod_cuenta = "";

            foreach (LibroAuxiliar LibAux in lstLibAux)
            {
                if (LibAux.detalle == "SALDO INICIAL")
                {
                    cod_cuenta = LibAux.cod_cuenta;
                    saldo = SaldoCuentaNIIF(LibAux.cod_cuenta, FecIni, CenIni, CenFin, vUsuario);
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


        public string CargarCuentasNiif(string sCod_Cuenta, Usuario pUsuario)
        {
            return BALibroAuxiliar.CargarCuentasNiif(sCod_Cuenta, pUsuario);
        }

        public double SaldoCuentaNIIF(string cod_cuenta, DateTime fecha, Int64 cenini, Int64 cenfin, Usuario pUsuario)
        {
            return BALibroAuxiliar.SaldoCuentaNIIF(cod_cuenta, fecha, cenini, cenfin, pUsuario);
        }


    }
}