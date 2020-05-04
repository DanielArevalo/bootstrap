using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoDiligenciaService
    {
        private TipoDiligenciaBusiness BOTipoDiligencia;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipoDiligencia
        /// </summary>
        public TipoDiligenciaService()
        {
            BOTipoDiligencia = new TipoDiligenciaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110115"; } }

        /// <summary>
        /// Servicio para crear TipoDiligencia
        /// </summary>
        /// <param name="pEntity">Entidad TipoDiligencia</param>
        /// <returns>Entidad TipoDiligencia creada</returns>
        public TipoDiligencia CrearTipoDiligencia(TipoDiligencia pTipoDiligencia, Usuario pUsuario)
        {
            try
            {
                return BOTipoDiligencia.CrearTipoDiligencia(pTipoDiligencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoDiligenciaService", "CrearTipoDiligencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar TipoDiligencia
        /// </summary>
        /// <param name="pTipoDiligencia">Entidad TipoDiligencia</param>
        /// <returns>Entidad TipoDiligencia modificada</returns>
        public TipoDiligencia ModificarTipoDiligencia(TipoDiligencia pTipoDiligencia, Usuario pUsuario)
        {
            try
            {
                return BOTipoDiligencia.ModificarTipoDiligencia(pTipoDiligencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoDiligenciaService", "ModificarTipoDiligencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TipoDiligencia
        /// </summary>
        /// <param name="pId">identificador de TipoDiligencia</param>
        public void EliminarTipoDiligencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoDiligencia.EliminarTipoDiligencia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTipoDiligencia", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TipoDiligencia
        /// </summary>
        /// <param name="pId">identificador de TipoDiligencia</param>
        /// <returns>Entidad TipoDiligencia</returns>
        public TipoDiligencia ConsultarTipoDiligencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoDiligencia.ConsultarTipoDiligencia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoDiligenciaService", "ConsultarTipoDiligencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de TipoDiligencias a partir de unos filtros
        /// </summary>
        /// <param name="pTipoDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoDiligencia obtenidos</returns>
        public List<TipoDiligencia> ListarTipoDiligencia(TipoDiligencia pTipoDiligencia, Usuario pUsuario)
        {
            try
            {
                return BOTipoDiligencia.ListarTipoDiligencia(pTipoDiligencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoDiligenciaService", "ListarTipoDiligencia", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de TipoDiligencias a partir de unos filtros
        /// </summary>
        /// <param name="pTipoDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoDiligencia obtenidos</returns>
        public List<TipoDiligencia> ListarTipoDiligenciaAgregar(TipoDiligencia pTipoDiligencia, Usuario pUsuario)
        {
            try
            {
                return BOTipoDiligencia.ListarTipoDiligenciaAgregar(pTipoDiligencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoDiligenciaService", "ListarTipoDiligenciaModificar", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de TipoContacto a partir de unos filtros
        /// </summary>
        /// <param name="pTipoDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoContacto obtenidos</returns>
        public List<TipoContacto> ListarTipoContacto(TipoContacto pTipocontacto, Usuario pUsuario)
        {
            try
            {
                return BOTipoDiligencia.ListarTipoContacto(pTipocontacto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoContactoService", "ListarTipoContacto", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de TipoContacto a partir de unos filtros
        /// </summary>
        /// <param name="pTipoDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoContacto obtenidos</returns>
        public List<TipoContacto> ListarTipoContactoAgregar(TipoContacto pTipocontacto, Usuario pUsuario)
        {
            try
            {
                return BOTipoDiligencia.ListarTipoContactoAgregar(pTipocontacto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoContactoService", "ListarTipoContactoAgregar", ex);
                return null;
            }
        }
    }
}