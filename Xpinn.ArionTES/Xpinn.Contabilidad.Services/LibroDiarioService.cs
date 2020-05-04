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
    public class LibroDiarioService
    {
        private LibroDiarioBusiness BOLibroDiario;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public LibroDiarioService()
        {
            BOLibroDiario = new LibroDiarioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30204"; } }
        public string CodigoProgramaNiif { get { return "210115"; } }

        public List<LibroDiario> ListarDiario(Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, Boolean bCuenta,Int64 moneda, Usuario vUsuario)
        {
            return BOLibroDiario.ListarDiario(CenIni, CenFin, FecIni, FecFin, bCuenta,moneda, vUsuario);
        }

        public List<LibroDiario> ListarDiarioNiif(Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, Boolean bCuenta, Int64 moneda, Usuario vUsuario)
        {
            return BOLibroDiario.ListarDiarioNiif(CenIni, CenFin, FecIni, FecFin, bCuenta, moneda, vUsuario);
        }

        /// <summary>
        /// Método para determinar la fecha inicial de un tipo de cierre dado
        /// </summary>
        /// <param name="tipocierre"></param>
        /// <returns></returns>
        public DateTime DeterminarFechaInicial(string tipocierre, Usuario pUsuario)
        {
            return BOLibroDiario.DeterminarFechaInicial(tipocierre, pUsuario);
        }
        public List<LibroDiario> ListarLibroDiario(LibroDiario pentidad, Usuario vUsuario,  ref Double TotDeb, ref Double TotCre)
        {
            return BOLibroDiario.ListarLibroDiario(pentidad, vUsuario,  ref TotDeb, ref TotCre);
        }

    }
}