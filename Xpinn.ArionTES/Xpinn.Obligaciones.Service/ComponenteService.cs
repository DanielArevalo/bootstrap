using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Obligaciones.Business;
using Xpinn.Obligaciones.Entities;

namespace Xpinn.Obligaciones.Services
{
    /// <summary>
    /// Servicios para Tipo Liquidacion
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ComponenteService
    {
        private ComponenteBusiness BOComponente;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Componente
        /// </summary>
        public ComponenteService()
        {
            BOComponente = new ComponenteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public string CodigoPrograma { get { return "130203"; } }

        /// <summary>
        /// Servicio para crear Componente
        /// </summary>
        /// <param name="pEntity">Entidad Componente</param>
        /// <returns>Entidad Componente creada</returns>
        public Componente CrearComponente(Componente vComponente, Usuario pUsuario)
        {
            try
            {
                return BOComponente.CrearComponente(vComponente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteService", "CrearComponente", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Componente
        /// </summary>
        /// <param name="pComponente">Entidad Componente</param>
        /// <returns>Entidad Componente modificada</returns>
        public Componente ModificarComponente(Componente vComponente, Usuario pUsuario)
        {
            try
            {
                return BOComponente.ModificarComponente(vComponente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteService", "ModificarComponente", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Componente
        /// </summary>
        /// <param name="pId">identificador de Componente</param>
        public void EliminarComponente(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOComponente.EliminarComponente(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarComponente", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Componente
        /// </summary>
        /// <param name="pId">identificador de Componente</param>
        /// <returns>Entidad Componente</returns>
        public Componente ConsultarComponente(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOComponente.ConsultarComponente(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteService", "ConsultarComponente", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Tipo de Liquidacion a partir de unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Componente obtenidos</returns>
        public List<Componente> ListarComponentes(Componente PComponente, Usuario pUsuario)
        {
            try
            {
                return BOComponente.ListarComponentes(PComponente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteService", "ListarComponentes", ex);
                return null;
            }
        }
    }
}
