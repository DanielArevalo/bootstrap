using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class LibroTercerosService
    {
        private LibroTercerosBusiness BOLibroTerceros;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public LibroTercerosService()
        {
            BOLibroTerceros = new LibroTercerosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30201"; } }
        public string CodigoProgramaNIIF { get { return "210114"; } }

        public List<LibroTerceros> ListarAuxiliarTerceros(string CodCueIni, string CodCueFin, string IdenIni, string IdenFin, Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, Boolean bCuenta, Usuario vUsuario)
        {
            List<LibroTerceros> lstLibAux = new List<LibroTerceros>();
            lstLibAux = BOLibroTerceros.ListarAuxiliarTerceros(CodCueIni, CodCueFin, IdenIni, IdenFin, CenIni, CenFin, FecIni, FecFin, bCuenta, vUsuario);
            return lstLibAux;
        }

        public List<LibroTerceros> ListarAuxiliarTercerosNIIF(string CodCueIni, string CodCueFin, string IdenIni, string IdenFin, Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, Boolean bCuenta, Usuario vUsuario)
        {
            List<LibroTerceros> lstLibAux = new List<LibroTerceros>();
            lstLibAux = BOLibroTerceros.ListarAuxiliarTercerosNIIF(CodCueIni, CodCueFin, IdenIni, IdenFin, CenIni, CenFin, FecIni, FecFin, bCuenta, vUsuario);
            return lstLibAux;
        }

        public List<Tercero> ListarTerceros(Usuario pUsuario, String filtro)
        {
            List<Tercero> lstTercero = new List<Tercero>();
            lstTercero = BOLibroTerceros.ListarTerceros(pUsuario, filtro);
            return lstTercero;
        }

        /// <summary>
        /// Método para determinar la fecha inicial de un tipo de cierre dado
        /// </summary>
        /// <param name="tipocierre"></param>
        /// <returns></returns>
        public DateTime DeterminarFechaInicial(string tipocierre, Usuario pUsuario)
        {
            return BOLibroTerceros.DeterminarFechaInicial(tipocierre, pUsuario);
        }

    }
}