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
    public class ACHregistroService
    {
        private ACHregistroBusiness BOACHregistro;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ACHregistro
        /// </summary>
        public ACHregistroService()
        {
            BOACHregistro = new ACHregistroBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40305"; } }

        /// <summary>
        /// Servicio para crear ACHregistro
        /// </summary>
        /// <param name="pEntity">Entidad ACHregistro</param>
        /// <returns>Entidad ACHregistro creada</returns>
        public ACHregistro CrearACHregistro(ACHregistro vACHregistro, Usuario pUsuario)
        {
            try
            {
                return BOACHregistro.CrearACHregistro(vACHregistro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHregistroService", "CrearACHregistro", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ACHregistro
        /// </summary>
        /// <param name="pACHregistro">Entidad ACHregistro</param>
        /// <returns>Entidad ACHregistro modificada</returns>
        public ACHregistro ModificarACHregistro(ACHregistro vACHregistro, Usuario pUsuario)
        {
            try
            {
                return BOACHregistro.ModificarACHregistro(vACHregistro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHregistroService", "ModificarACHregistro", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ACHregistro
        /// </summary>
        /// <param name="pId">identificador de ACHregistro</param>
        public void EliminarCampoXACHregistro(Int64 pRegistro,Int64 pCampo, Usuario pUsuario)
        {
            try
            {
                BOACHregistro.EliminarCampoXACHregistro(pRegistro, pCampo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarCampoXACHregistro", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ACHregistro
        /// </summary>
        /// <param name="pId">identificador de ACHregistro</param>
        /// <returns>Entidad ACHregistro</returns>
        public ACHregistro ConsultarACHregistro(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOACHregistro.ConsultarACHregistro(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHregistroService", "ConsultarACHregistro", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ACHregistro 
        /// </summary>
        /// <param name="pACHregistro">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ACHregistro obtenidos</returns>
        public List<ACHregistro> ListarACHregistro(ACHregistro vACHregistro, Usuario pUsuario)
        {
            try
            {
                return BOACHregistro.ListarACHregistro(vACHregistro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHregistroService", "ListarACHregistro", ex);
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
                return BOACHregistro.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHregistroService", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

    }
}