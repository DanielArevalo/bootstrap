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
    public class SoporteCajService
    {
        private SoporteCajBusiness BOSoporteCaj;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para SoporteCaj
        /// </summary>
        public SoporteCajService()
        {
            BOSoporteCaj = new SoporteCajBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40602"; } }
        public string CodigoProgramaReintegro { get { return "40603"; } }

        /// <summary>
        /// Servicio para crear SoporteCaj
        /// </summary>
        /// <param name="pEntity">Entidad SoporteCaj</param>
        /// <returns>Entidad SoporteCaj creada</returns>
        public SoporteCaj CrearSoporteCaj(SoporteCaj vSoporteCaj, Usuario pUsuario)
        {
            try
            {
                return BOSoporteCaj.CrearSoporteCaj(vSoporteCaj, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SoporteCajService", "CrearSoporteCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar SoporteCaj
        /// </summary>
        /// <param name="pSoporteCaj">Entidad SoporteCaj</param>
        /// <returns>Entidad SoporteCaj modificada</returns>
        public SoporteCaj ModificarSoporteCaj(SoporteCaj vSoporteCaj, Usuario pUsuario)
        {
            try
            {
                return BOSoporteCaj.ModificarSoporteCaj(vSoporteCaj, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SoporteCajService", "ModificarSoporteCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar SoporteCaj
        /// </summary>
        /// <param name="pId">identificador de SoporteCaj</param>
        public void EliminarSoporteCaj(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOSoporteCaj.EliminarSoporteCaj(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarSoporteCaj", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener SoporteCaj
        /// </summary>
        /// <param name="pId">identificador de SoporteCaj</param>
        /// <returns>Entidad SoporteCaj</returns>
        public SoporteCaj ConsultarSoporteCaj(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOSoporteCaj.ConsultarSoporteCaj(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SoporteCajService", "ConsultarSoporteCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de SoporteCaj 
        /// </summary>
        /// <param name="pSoporteCaj">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SoporteCaj obtenidos</returns>
        public List<SoporteCaj> ListarSoporteCaj(SoporteCaj vSoporteCaj, Usuario pUsuario)
        {
            try
            {
                return BOSoporteCaj.ListarSoporteCaj(vSoporteCaj, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SoporteCajService", "ListarSoporteCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtener el siguiente código
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOSoporteCaj.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SoporteCajService", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

        public List<SoporteCaj> ComprobanteSoporteCaj(List<SoporteCaj> pLstSoporteCaj, ref Giro pGiro, Operacion pOperacion, ref string pError, Usuario pUsuario)
        {
            try
            {
                return BOSoporteCaj.ComprobanteSoporteCaj(pLstSoporteCaj, ref pGiro, pOperacion, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                pError = ex.Message;                
                return null;
            }
        }
        public SoporteCaj ActualizarSoporteArqueo(Int64 id_areas, Int64? id_Arqueo, Usuario pUsuario)
        {
            try
            {
                return BOSoporteCaj.ActualizarSoporteArqueo(id_areas, id_Arqueo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SoporteCajService", "ActualizarSoporteArqueo", ex);
                return null;
            }
        }

    }
}