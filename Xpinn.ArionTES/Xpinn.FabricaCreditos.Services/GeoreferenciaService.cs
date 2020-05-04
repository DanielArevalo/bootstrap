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
    public class GeoreferenciaService
    {
        private GeoreferenciaBusiness BOGeoreferencia;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Georeferencia
        /// </summary>
        public GeoreferenciaService()
        {
            BOGeoreferencia = new GeoreferenciaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100106"; } }

        

        /// <summary>
        /// Servicio para crear Georeferencia
        /// </summary>
        /// <param name="pEntity">Entidad Georeferencia</param>
        /// <returns>Entidad Georeferencia creada</returns>
        public Georeferencia CrearGeoreferencia(Georeferencia pGeoreferencia, Usuario pUsuario)
        {
            try
            {
                return BOGeoreferencia.CrearGeoreferencia(pGeoreferencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeoreferenciaService", "CrearGeoreferencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Georeferencia
        /// </summary>
        /// <param name="pGeoreferencia">Entidad Georeferencia</param>
        /// <returns>Entidad Georeferencia modificada</returns>
        public Georeferencia ModificarGeoreferencia(Georeferencia pGeoreferencia, Usuario pUsuario)
        {
            try
            {
                return BOGeoreferencia.ModificarGeoreferencia(pGeoreferencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeoreferenciaService", "ModificarGeoreferencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Georeferencia
        /// </summary>
        /// <param name="pId">identificador de Georeferencia</param>
        public void EliminarGeoreferencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOGeoreferencia.EliminarGeoreferencia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarGeoreferencia", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Georeferencia
        /// </summary>
        /// <param name="pId">identificador de Georeferencia</param>
        /// <returns>Entidad Georeferencia</returns>
        public Georeferencia ConsultarGeoreferencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOGeoreferencia.ConsultarGeoreferencia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeoreferenciaService", "ConsultarGeoreferencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Georeferencias a partir de unos filtros
        /// </summary>
        /// <param name="pGeoreferencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Georeferencia obtenidos</returns>
        public List<Georeferencia> ListarGeoreferencia(Georeferencia pGeoreferencia, Usuario pUsuario)
        {
            try
            {
                return BOGeoreferencia.ListarGeoreferencia(pGeoreferencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeoreferenciaService", "ListarGeoreferencia", ex);
                return null;
            }
        }
        public List<Georeferencia> ListarGeoreferenciacion(Georeferencia pGeoreferencia, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOGeoreferencia.ListarGeoreferenciacion(pGeoreferencia, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeoreferenciaService", "ListarGeoreferencia", ex);
                return null;
            }
        }


        public Georeferencia ConsultarGeoreferenciaXPERSONA(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOGeoreferencia.ConsultarGeoreferenciaXPERSONA(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeoreferenciaService", "ConsultarGeoreferenciaXPERSONA", ex);
                return null;
            }
        }

    }
}