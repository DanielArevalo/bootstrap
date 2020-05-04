using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ConyugeService
    {
        private ConyugeBusiness BOConyuge;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Conyuge
        /// </summary>
        public ConyugeService()
        {
            BOConyuge = new ConyugeBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100138"; } }

        /// <summary>
        /// Servicio para crear Conyuge
        /// </summary>
        /// <param name="pEntity">Entidad Conyuge</param>
        /// <returns>Entidad Conyuge creada</returns>
        public Conyuge CrearConyuge(Conyuge pConyuge, Usuario pUsuario)
        {
            try
            {
                return BOConyuge.CrearConyuge(pConyuge, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConyugeService", "CrearConyuge", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Conyuge
        /// </summary>
        /// <param name="pConyuge">Entidad Conyuge</param>
        /// <returns>Entidad Conyuge modificada</returns>
        public Conyuge ModificarConyuge(Conyuge pConyuge, Usuario pUsuario)
        {
            try
            {
                return BOConyuge.ModificarConyuge(pConyuge, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConyugeService", "ModificarConyuge", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Conyuge
        /// </summary>
        /// <param name="pId">identificador de Conyuge</param>
        public void EliminarConyuge(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOConyuge.EliminarConyuge(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarConyuge", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Conyuge
        /// </summary>
        /// <param name="pId">identificador de Conyuge</param>
        /// <returns>Entidad Conyuge</returns>
        public Conyuge ConsultarConyuge(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOConyuge.ConsultarConyuge(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConyugeService", "ConsultarConyuge", ex);
                return null;
            }
        }

   
      
        public Conyuge ConsultarRefConyuge(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOConyuge.ConsultarRefConyuge(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConyugeService", "ConsultarRefConyuge", ex);
                return null;
            }
        }


        public Conyuge ConsultarRefConyugeRepo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOConyuge.ConsultarRefConyugeRepo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarRefConyugeRepo", ex);
                return null;
            }
        }

    }
}