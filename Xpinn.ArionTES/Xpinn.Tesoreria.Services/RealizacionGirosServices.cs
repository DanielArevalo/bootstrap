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
    public class RealizacionGirosServices
    {
        private RealizacionGirosBusiness BORealizacion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para AreasCaj
        /// </summary>
        public RealizacionGirosServices()
        {
            BORealizacion = new RealizacionGirosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40303"; } }

        public List<Giro> ListarGiroAprobados(Giro pGiro, String Orden, DateTime pFechaGiro, DateTime pFechaAprobacion, Boolean Forma_Pago, Usuario vUsuario)
        {
            try
            {
                return BORealizacion.ListarGiroAprobados(pGiro, Orden, pFechaGiro, pFechaAprobacion,Forma_Pago, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RealizacionGirosServices", "ListarGiroAprobados", ex);
                return null;
            }
        }


        public List<Xpinn.Tesoreria.Entities.Operacion> RealizarGiro(bool pParametro, Giro pGiroTot, DateTime Fecha, Int64 pProcesoCont, Boolean rptaArchivo, String NombreArchivo, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BORealizacion.RealizarGiro(pParametro,pGiroTot, Fecha, pProcesoCont, rptaArchivo, NombreArchivo,ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RealizacionGirosServices", "RealizarGiro", ex);
                return null;
            }
        }


        public List<Xpinn.Tesoreria.Entities.Operacion> RealizarGiroOtros(Giro pGiroTot, DateTime Fecha, Int64 pProcesoCont, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BORealizacion.RealizarGiroOtros(pGiroTot, Fecha, pProcesoCont,ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RealizacionGirosServices", "RealizarGiroOtros", ex);
                return null;
            }
        }


        public void ReemplazarConsultaSQL(string pConsulta, ref string pResult, ref string pError, Usuario vUsuario)
        {
            try
            {
                BORealizacion.ReemplazarConsultaSQL(pConsulta,ref pResult,ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RealizacionGirosServices", "ReemplazarConsultaSQL", ex);          
            }
        }

    }
}