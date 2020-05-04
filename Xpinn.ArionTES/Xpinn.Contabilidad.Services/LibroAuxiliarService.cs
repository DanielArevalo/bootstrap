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
    public class LibroAuxiliarService
    {
        private LibroAuxiliarBusiness BOLibroAuxiliar;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public LibroAuxiliarService()
        {
            BOLibroAuxiliar = new LibroAuxiliarBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30202"; } }

        public List<LibroAuxiliar> ListarAuxiliar(Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, string cod_cuenta_ini, string cod_cuenta_fin, Boolean por_rango,Int32 moneda, string pOrdenar, Usuario vUsuario)
        {
            return BOLibroAuxiliar.ListarAuxiliar(CenIni, CenFin, FecIni, FecFin, cod_cuenta_ini, cod_cuenta_fin, por_rango,moneda, pOrdenar, vUsuario);
        }

        /// <summary>
        /// Método para generar las cuentas hijas de una cuenta contable
        /// </summary>
        /// <param name="sCod_Cuenta"></param>
        /// <returns></returns>
        public string CargarCuentas(string sCod_Cuenta, Usuario pUsuario)
        {
            return BOLibroAuxiliar.CargarCuentas(sCod_Cuenta, pUsuario);
        }

        /// <summary>
        /// Método para determinar la fecha inicial de un tipo de cierre dado
        /// </summary>
        /// <param name="tipocierre"></param>
        /// <returns></returns>
        public DateTime DeterminarFechaInicial(string tipocierre, Usuario pUsuario)
        {
            return BOLibroAuxiliar.DeterminarFechaInicial(tipocierre, pUsuario);
        }

    }
}