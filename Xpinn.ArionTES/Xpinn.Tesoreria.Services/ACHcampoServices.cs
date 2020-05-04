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
    public class ACHcampoService
    {
        private ACHcampoBusiness BOACHcampo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ACHcampo
        /// </summary>
        public ACHcampoService()
        {
            BOACHcampo = new ACHcampoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40305"; } }

        /// <summary>
        /// Servicio para crear ACHcampo
        /// </summary>
        /// <param name="pEntity">Entidad ACHcampo</param>
        /// <returns>Entidad ACHcampo creada</returns>
        public ACHcampo CrearACHcampo(ACHcampo vACHcampo, Usuario pUsuario)
        {
            try
            {
                return BOACHcampo.CrearACHcampo(vACHcampo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHcampoService", "CrearACHcampo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ACHcampo
        /// </summary>
        /// <param name="pACHcampo">Entidad ACHcampo</param>
        /// <returns>Entidad ACHcampo modificada</returns>
        public ACHcampo ModificarACHcampo(ACHcampo vACHcampo, Usuario pUsuario)
        {
            try
            {
                return BOACHcampo.ModificarACHcampo(vACHcampo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHcampoService", "ModificarACHcampo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ACHcampo
        /// </summary>
        /// <param name="pId">identificador de ACHcampo</param>
        public void EliminarACHcampo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOACHcampo.EliminarACHcampo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarACHcampo", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ACHcampo
        /// </summary>
        /// <param name="pId">identificador de ACHcampo</param>
        /// <returns>Entidad ACHcampo</returns>
        public ACHcampo ConsultarACHcampo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOACHcampo.ConsultarACHcampo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHcampoService", "ConsultarACHcampo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ACHcampo 
        /// </summary>
        /// <param name="pACHcampo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ACHcampo obtenidos</returns>
        public List<ACHcampo> ListarACHcampo(ACHcampo vACHcampo, Usuario pUsuario)
        {
            try
            {
                return BOACHcampo.ListarACHcampo(vACHcampo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHcampoService", "ListarACHcampo", ex);
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
                return BOACHcampo.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHcampoService", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

    }
}