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
    /// Objeto de negocio para el libro diario
    /// </summary>
    public class LibroDiarioBusiness : GlobalBusiness
    {
        private LibroDiarioData DALibroDiario;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public LibroDiarioBusiness()
        {
            DALibroDiario = new LibroDiarioData();
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
        public List<LibroDiario> ListarDiario(Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, Boolean bCuenta,Int64 moneda, Usuario vUsuario)
        {
            List<LibroDiario> lstLibAux = new List<LibroDiario>();
            lstLibAux = DALibroDiario.ListarDiario(CenIni, CenFin, FecIni, FecFin, bCuenta,moneda, vUsuario);

            return lstLibAux;
        }

        public List<LibroDiario> ListarDiarioNiif(Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, Boolean bCuenta, Int64 moneda, Usuario vUsuario)
        {
            List<LibroDiario> lstLibAux = new List<LibroDiario>();
            lstLibAux = DALibroDiario.ListarDiarioNiif(CenIni, CenFin, FecIni, FecFin, bCuenta, moneda, vUsuario);

            return lstLibAux;
        }


        /// <summary>
        /// Método para determinar la fecha inicial
        /// </summary>
        /// <param name="tipocierre"></param>
        /// <returns></returns>
        public DateTime DeterminarFechaInicial(string tipocierre, Usuario pUsuario)
        {
            return DALibroDiario.DeterminarFechaInicial(tipocierre, pUsuario);
        }
        public List<LibroDiario> ListarLibroDiario(LibroDiario pentidad, Usuario vUsuario,  ref Double TotDeb, ref Double TotCre)
        {
            List<LibroDiario> lstLibAux = new List<LibroDiario>();
            lstLibAux = DALibroDiario.ListarLibroDiario( pentidad,  vUsuario,  ref  TotDeb, ref  TotCre);

            return lstLibAux;
        }
    }
}

