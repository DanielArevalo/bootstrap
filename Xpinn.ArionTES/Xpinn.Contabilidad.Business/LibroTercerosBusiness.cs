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
    public class LibroTercerosBusiness : GlobalBusiness
    {
        private LibroTercerosData DALibroTerceros;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public LibroTercerosBusiness()
        {
            DALibroTerceros = new LibroTercerosData();
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
        public List<LibroTerceros> ListarAuxiliarTerceros(string CodCueIni, string CodCueFin, string IdenIni, string IdenFin, Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, Boolean bCuenta, Usuario vUsuario)
        {
            List<LibroTerceros> lstLibAux = new List<LibroTerceros>();
            lstLibAux = DALibroTerceros.ListarAuxiliarTerceros(CodCueIni, CodCueFin, IdenIni, IdenFin, CenIni, CenFin, FecIni, FecFin, bCuenta, vUsuario);
            return lstLibAux;
        }


        public List<LibroTerceros> ListarAuxiliarTercerosNIIF(string CodCueIni, string CodCueFin, string IdenIni, string IdenFin, Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, Boolean bCuenta, Usuario vUsuario)
        {
            List<LibroTerceros> lstLibAux = new List<LibroTerceros>();
            lstLibAux = DALibroTerceros.ListarAuxiliarTercerosNIIF(CodCueIni, CodCueFin, IdenIni, IdenFin, CenIni, CenFin, FecIni, FecFin, bCuenta, vUsuario);
            return lstLibAux;
        }

        public List<Tercero> ListarTerceros(Usuario pUsuario, String filtro)
        {
            List<Tercero> lstTercero = new List<Tercero>();
            lstTercero = DALibroTerceros.ListarTerceros(pUsuario, filtro);
            return lstTercero;
        }

        /// <summary>
        /// Método para determinar la fecha inicial
        /// </summary>
        /// <param name="tipocierre"></param>
        /// <returns></returns>
        public DateTime DeterminarFechaInicial(string tipocierre, Usuario pUsuario)
        {
            return DALibroTerceros.DeterminarFechaInicial(tipocierre, pUsuario);
        }

    }
}

