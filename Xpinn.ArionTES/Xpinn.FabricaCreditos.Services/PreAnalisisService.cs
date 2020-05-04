using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para PreAnalisis
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PreAnalisisService
    {
        private FabricaCreditos.Business.PreAnalisisBusiness BOpreanalisis;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Programa
        /// </summary>
        public PreAnalisisService()
        {
            BOpreanalisis = new PreAnalisisBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100202"; } }

        /// <summary>
        /// Crea un Programa
        /// </summary>
        /// <param name="pEntity">Entidad Programa</param>
        /// <returns>Entidad creada</returns>
        public Parametrizar CrearPrograma(Parametrizar pPrograma, Usuario pUsuario)
        {
            try
            {
                return BOpreanalisis.CrearPrograma(pPrograma, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProgramaService", "CrearPrograma", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Programa
        /// </summary>
        /// <param name="pEntity">Entidad Programa</param>
        /// <returns>Entidad modificada</returns>
        public Parametrizar ModificarPrograma(Parametrizar pPrograma, Usuario pUsuario)
        {
            try
            {
                return BOpreanalisis.ModificarPrograma(pPrograma, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProgramaService", "ModificarPrograma", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina un Programa
        /// </summary>
        /// <param name="pId">identificador del Programa</param>
        public void EliminarPrograma(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOpreanalisis.EliminarPrograma(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProgramaService", "EliminarPrograma", ex);
            }
        }

        /// <summary>
        /// Obtiene un Programa
        /// </summary>
        /// <param name="pId">identificador del Programa</param>
        /// <returns>Programa consultado</returns>
        public Parametrizar ConsultarPrograma(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOpreanalisis.ConsultarPrograma(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProgramaService", "ConsultarPrograma", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Programas obtenidos</returns>
        public List<Parametrizar> ListarPrograma(Parametrizar pPrograma, Usuario pUsuario)
        {
            try
            {
                return BOpreanalisis.ListarPrograma(pPrograma, pUsuario);
                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProgramaService", "ListarPrograma", ex);
                return null;
            }
        }








        /// <summary>
        /// Crea una Central
        /// </summary>
        /// <param name="pEntity">Entidad Central</param>
        /// <returns>Entidad creada</returns>
        public Parametrizar CrearCentral(Parametrizar pCentral, Usuario pUsuario)
        {
            try
            {
                return BOpreanalisis.CrearCentral(pCentral, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PreAnalisisService", "CrearCentral", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica una Central
        /// </summary>
        /// <param name="pEntity">Entidad Central</param>
        /// <returns>Entidad modificada</returns>
        public Parametrizar ModificarCentral(Parametrizar pCentral, Usuario pUsuario)
        {
            try
            {
                return BOpreanalisis.ModificarCentral(pCentral, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PreAnalisisService", "ModificarCentral", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina una Central
        /// </summary>
        /// <param name="pId">Identificador de la Central</param>
        public void EliminarCentral(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOpreanalisis.EliminarCentral(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PreAnalisisService", "EliminarCentral", ex);
            }
        }

        /// <summary>
        /// Obtiene una Central
        /// </summary>
        /// <param name="pId">Identificador de la Central</param>
        /// <returns>Central consultado</returns>
        public Parametrizar ConsultarCentral(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOpreanalisis.ConsultarCentral(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProgramaService", "ConsultarPrograma", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Centrales dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Centrales obtenidos</returns>
        public List<Parametrizar> ListarCentral(Parametrizar pCentral, Usuario pUsuario)
        {
            try
            {
                return BOpreanalisis.ListarCentrales(pCentral, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PreAnalisisService", "ListarCentral", ex);
                return null;
            }
        }


        public Credito ConsultarPreAnalisis_credito(Credito pEntidad, Usuario vUsuario)
        {
            try
            {
                return BOpreanalisis.ConsultarPreAnalisis_credito(pEntidad, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PreAnalisisService", "ConsultarPreAnalisis_credito", ex);
                return null;
            }
        }

    }






}
