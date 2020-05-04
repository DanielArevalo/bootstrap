using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AprobacionGirosServices
    {
        private AprobacionGirosBusiness BOAprobacion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para AreasCaj
        /// </summary>
        public AprobacionGirosServices()
        {
            BOAprobacion = new AprobacionGirosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40302"; } }

        public List<Giro> ListarGiro(Giro pGiro, String Orden, DateTime pFechaGiro, Usuario vUsuario)
        {
            try
            {
                return BOAprobacion.ListarGiro(pGiro,Orden,pFechaGiro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionGirosServices", "ListarGiro", ex);
                return null;
            }
        }


        public Giro AprobarGiro(Giro pGiro, Usuario vUsuario)
        {
            try
            {
                return BOAprobacion.AprobarGiro(pGiro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionGirosServices", "AprobarGiro", ex);
                return null;
            }
        }


        public CuentasBancarias ConsultarCuentasBancariasXNumCuenta(String pNumCuenta, Usuario vUsuario)
        {
            try
            {
                return BOAprobacion.ConsultarCuentasBancariasXNumCuenta(pNumCuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionGirosServices", "ConsultarCuentasBancariasXNumCuenta", ex);
                return null;
            }
        }

    }
}