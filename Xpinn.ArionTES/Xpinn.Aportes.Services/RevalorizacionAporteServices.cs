using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Aportes.Business;
using Xpinn.Aportes.Entities;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Aportes.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class RevalorizacionAporteServices
    {
        private RevalorizacionAporteBusiness BORevalorizacionAporte;
        private ExcepcionBusiness BOExcepcion;
        public int Codigoaporte;
        /// <summary>
        /// Constructor del servicio para Aporte
        /// </summary>
        public RevalorizacionAporteServices()
        {
            BORevalorizacionAporte = new RevalorizacionAporteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

       public string CodigoPrograma { get { return "170103"; } }

       /// <summary>
       /// Obtiene un Reintegro
       /// </summary>
       /// <param name="pUsuario">identificador del Usuario</param>
       /// <returns>Reitegro consultada</returns>
       public RevalorizacionAportes ConsultarFecUltCierre(Usuario pUsuario)
       {
           try
           {
               return BORevalorizacionAporte.ConsultarFecUltCierre(pUsuario);
           }
           catch (Exception ex)
           {
               BOExcepcion.Throw("RevalorizacionAporteService", "ConsultarFecUltCierre", ex);
               return null;
           }
       }

       
       public List<RevalorizacionAportes> Listar(RevalorizacionAportes pEntidad, ref List<RevalorizacionAportes> lstNoCalculados, Usuario pUsuario)
        {
            try
            {
                return BORevalorizacionAporte.Listar(pEntidad, ref lstNoCalculados, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RevalorizacionAporteService", "Listar", ex);
                return null;
            }
        }

        public List<RevalorizacionAportes> ListarRevalorizacion(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BORevalorizacionAporte.ListarRevalorizacion(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RevalorizacionAporteService", "ListarRevalorizacion", ex);
                return null;
            }
        }


        public List<RevalorizacionAportes> ListarDatosComprobante(String filtro,Usuario pUsuario)
       {
           try
           {
               return BORevalorizacionAporte.ListarDatosComprobante(filtro, pUsuario);
           }
           catch (Exception ex)
           {
               BOExcepcion.Throw("RevalorizacionAporteService", "ListarDatosComprobante", ex);
               return null;
           }
       }


       public RevalorizacionAportes GrabarRevalorizacion(RevalorizacionAportes pEntidad, ref Int64 vCod_ope, ref string Error, Usuario pUsuario)
       {

           try
           {
               return BORevalorizacionAporte.GrabarRevalorizacion(pEntidad,ref vCod_ope, ref Error,pUsuario);
           }
           catch (Exception ex)
           {
               BOExcepcion.Throw("RevalorizacionAporteService", "CrearRangosAtributos", ex);
               return null;
           }

       }

    }
 }

    