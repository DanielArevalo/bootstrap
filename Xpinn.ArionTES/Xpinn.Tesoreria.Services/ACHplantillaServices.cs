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
    public class ACHplantillaService
    {
        private ACHplantillaBusiness BOACHplantilla;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ACHplantilla
        /// </summary>
        public ACHplantillaService()
        {
            BOACHplantilla = new ACHplantillaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40305"; } }

        /// <summary>
        /// Servicio para crear ACHplantilla
        /// </summary>
        /// <param name="pEntity">Entidad ACHplantilla</param>
        /// <returns>Entidad ACHplantilla creada</returns>
        public ACHplantilla CrearACHplantilla(ACHplantilla vACHplantilla, Usuario pUsuario)
        {
            try
            {
                return BOACHplantilla.CrearACHplantilla(vACHplantilla, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaService", "CrearACHplantilla", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ACHplantilla
        /// </summary>
        /// <param name="pACHplantilla">Entidad ACHplantilla</param>
        /// <returns>Entidad ACHplantilla modificada</returns>
        public ACHplantilla ModificarACHplantilla(ACHplantilla vACHplantilla, Usuario pUsuario)
        {
            try
            {
                return BOACHplantilla.ModificarACHplantilla(vACHplantilla, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaService", "ModificarACHplantilla", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ACHplantilla
        /// </summary>
        /// <param name="pId">identificador de ACHplantilla</param>
        public void EliminarACHplantilla(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOACHplantilla.EliminarACHplantilla(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarACHplantilla", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ACHplantilla
        /// </summary>
        /// <param name="pId">identificador de ACHplantilla</param>
        /// <returns>Entidad ACHplantilla</returns>
        public ACHplantilla ConsultarACHplantilla(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOACHplantilla.ConsultarACHplantilla(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaService", "ConsultarACHplantilla", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ACHplantilla 
        /// </summary>
        /// <param name="pACHplantilla">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ACHplantilla obtenidos</returns>
        public List<ACHplantilla> ListarACHplantilla(ACHplantilla vACHplantilla, Usuario pUsuario)
        {
            try
            {
                return BOACHplantilla.ListarACHplantilla(vACHplantilla, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaService", "ListarACHplantilla", ex);
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
                return BOACHplantilla.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaService", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }


        public void EliminarACH_PLANTILLA(Int64 pPlantilla, Int32 pRegistro, Usuario vUsuario)
        {
            try
            {
                BOACHplantilla.EliminarACH_PLANTILLA(pPlantilla, pRegistro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaService", "EliminarACH_PLANTILLA", ex);
            }
        }


        public ACHregistro ConsultarRegisPlantilla(Int64 pPlantilla, Int64 pRegistro, Usuario vUsuario)
        {
            try
            {
                return BOACHplantilla.ConsultarRegisPlantilla(pPlantilla, pRegistro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ACHplantillaService", "ConsultarRegisPlantilla", ex);
                return null;
            }
        }


    }
}