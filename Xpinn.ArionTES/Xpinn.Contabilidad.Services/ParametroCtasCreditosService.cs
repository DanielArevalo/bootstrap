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
    public class ParametroCtasCreditosService
    {
        private ParametroCtasCreditosBusiness BOPar_Cue_LinCred;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Par_Cue_LinCred
        /// </summary>
        public ParametroCtasCreditosService()
        {
            BOPar_Cue_LinCred = new ParametroCtasCreditosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "31003"; } }

        /// <summary>
        /// Servicio para crear parametrización de una línea
        /// </summary>
        /// <param name="pEntity">Lista de entidades vPar_Cue_LinCred</param>
        /// <returns>Variable booleana para confirmar el registro</returns>
        public bool CrearParametrizacionLinea(List<Par_Cue_LinCred> lstParametros, Usuario pUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.CrearParametrizacionLinea(lstParametros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "CrearPar_Cue_LinCred", ex);
                return false;
            }
        }

        /// <summary>
        /// Servicio para crear Par_Cue_LinCred
        /// </summary>
        /// <param name="pEntity">Entidad Par_Cue_LinCred</param>
        /// <returns>Entidad Par_Cue_LinCred creada</returns>
        public Par_Cue_LinCred CrearPar_Cue_LinCred(Par_Cue_LinCred vPar_Cue_LinCred, ref string error, Usuario pUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.CrearPar_Cue_LinCred(vPar_Cue_LinCred,ref error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "CrearPar_Cue_LinCred", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para modificar Par_Cue_LinCred
        /// </summary>
        /// <param name="pPar_Cue_LinCred">Entidad Par_Cue_LinCred</param>
        /// <returns>Entidad Par_Cue_LinCred modificada</returns>
        public Par_Cue_LinCred ModificarPar_Cue_LinCred(Par_Cue_LinCred vPar_Cue_LinCred, Usuario pUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.ModificarPar_Cue_LinCred(vPar_Cue_LinCred, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "ModificarPar_Cue_LinCred", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Par_Cue_LinCred
        /// </summary>
        /// <param name="pId">identificador de Par_Cue_LinCred</param>
        public void EliminarPar_Cue_LinCred(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOPar_Cue_LinCred.EliminarPar_Cue_LinCred(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarPar_Cue_LinCred", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Par_Cue_LinCred
        /// </summary>
        /// <param name="pId">identificador de Par_Cue_LinCred</param>
        /// <returns>Entidad Par_Cue_LinCred</returns>
        public Par_Cue_LinCred ConsultarPar_Cue_LinCred(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.ConsultarPar_Cue_LinCred(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "ConsultarPar_Cue_LinCred", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Par_Cue_LinCreds a partir de unos filtros
        /// </summary>
        /// <param name="pPar_Cue_LinCred">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Par_Cue_LinCred obtenidos</returns>
        public List<Par_Cue_LinCred> ListarPar_Cue_LinCred(Par_Cue_LinCred vPar_Cue_LinCred, Usuario pUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.ListarPar_Cue_LinCred(vPar_Cue_LinCred, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "ListarPar_Cue_LinCred", ex);
                return null;
            }
        }

        public List<Par_Cue_LinCred> ListarLibranza(Usuario vUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.ListarLibranza(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "ListarLibranza", ex);
                return null;
            }
        }

        public List<Par_Cue_LinCred> ListarGarantia(Usuario vUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.ListarGarantia(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "ListarGarantia", ex);
                return null;
            }
        }

        public List<Par_Cue_LinCred> ListarClasificacion(Usuario vUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.ListarClasificacion(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "ListarClasificacion", ex);
                return null;
            }
        }

        public List<Par_Cue_LinCred> ListarTransaccion(Usuario vUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.ListarTransaccion(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "ListarTransaccion", ex);
                return null;
            }
        }

        public List<Par_Cue_LinCred> ListarEstructura(Usuario vUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.ListarEstructura(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "ListarEstructura", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "ObtenerSiguienteCodigo", ex);
                return Int64.MinValue;
            }
        }


        public void CopiarPar_Cue_LinCred(String lineaOrigen, String LineaDestino, int? cod_atr, int? tipo, Usuario vUsuario)
        {
            try
            {
                BOPar_Cue_LinCred.CopiarPar_Cue_LinCred(lineaOrigen, LineaDestino, cod_atr, tipo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "CopiarPar_Cue_LinCred", ex);
            }
        }

        public Par_Cue_LinCred ConsultarPar_Cue_LinCred2(Par_Cue_LinCred pPar_Cue_LinCred, string pfiltro, Usuario vUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.ConsultarPar_Cue_LinCred2(pPar_Cue_LinCred,pfiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "ConsultarPar_Cue_LinCred2", ex);
                return null;
            }
        }

        public Par_Cue_LinCred ConsultarPAR_CUE_LINAPO(Par_Cue_LinCred pPar_Cue_LinCred, string pfiltro, Usuario vUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.ConsultarPAR_CUE_LINAPO(pPar_Cue_LinCred, pfiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "ConsultarPAR_CUE_LINAPO", ex);
                return null;
            }
        }

        public Par_Cue_LinCred ConsultarPAR_CUE_OTROS(Par_Cue_LinCred pPar_Cue_LinCred, string pfiltro, Usuario vUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.ConsultarPAR_CUE_OTROS(pPar_Cue_LinCred, pfiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinCredService", "ConsultarPAR_CUE_OTROS", ex);
                return null;
            }
        }

        public string ParametroGeneral(Int64 pCodigo, Usuario vUsuario)
        {
            try
            {
                return BOPar_Cue_LinCred.ParametroGeneral(pCodigo, vUsuario);
            }
            catch
            {
                return null;
            }
        }


    }
}