using System;
using System.Collections.Generic;
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
    public class TerceroService
    {
        private TerceroBusiness BOTercero;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Tercero
        /// </summary>
        public TerceroService()
        {
            BOTercero = new TerceroBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30401"; } }

        /// <summary>
        /// Servicio para crear Tercero
        /// </summary>
        /// <param name="pEntity">Entidad Tercero</param>
        /// <returns>Entidad Tercero creada</returns>
        public Tercero CrearTercero(Tercero vTercero, Usuario pUsuario)
        {
            try
            {
                return BOTercero.CrearTercero(vTercero, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroService", "CrearTercero", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Tercero
        /// </summary>
        /// <param name="pTercero">Entidad Tercero</param>
        /// <returns>Entidad Tercero modificada</returns>
        public Tercero ModificarTercero(Tercero vTercero, Usuario pUsuario)
        {
            try
            {
                return BOTercero.ModificarTercero(vTercero, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroService", "ModificarTercero", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para cambiar tipo Persona
        /// </summary>
        /// <param name="pTercero">Entidad Tercero</param>
        /// <returns>Entidad Tercero modificada</returns>
        public Tuple<bool, string> CambiarTipoDePersona(long id,string tipoPersona, Usuario pUsuario)
        {
            try
            {
                return BOTercero.CambiarTipoDePersona(id, tipoPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroService", "CambiarTipoDePersona", ex);
                return Tuple.Create(false, ex.Message);
            }
        }

        /// <summary>
        /// Servicio para Eliminar Tercero
        /// </summary>
        /// <param name="pId">identificador de Tercero</param>
        public void EliminarTercero(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTercero.EliminarTercero(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTercero", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Tercero
        /// </summary>
        /// <param name="pId">identificador de Tercero</param>
        /// <returns>Entidad Tercero</returns>
        public Tercero ConsultarTercero(Int64? pCod, string pId, Usuario pUsuario)
        {
            try
            {
                return BOTercero.ConsultarTercero(pCod, pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroService", "ConsultarTercero", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Terceros a partir de unos filtros
        /// </summary>
        /// <param name="pTercero">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tercero obtenidos</returns>
        public List<Tercero> ListarTercero(Tercero vTercero,Usuario pUsuario)
        {
            try
            {
                return BOTercero.ListarTercero(vTercero,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroService", "ListarTercero", ex);
                return null;
            }
        }


        public List<Tercero> ListarTerceroSoloAfiliados(Tercero vTercero,string pFiltro,Usuario pUsuario, string pOrden = null)
        {
            try
            {
                return BOTercero.ListarTerceroSoloAfiliados(vTercero,pFiltro, pUsuario, pOrden);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroService", "ListarTerceroSoloAfiliados", ex);
                return null;
            }
        }

        public List<Tercero> ListarTerceroNoAfiliados(Tercero vTercero, string pFiltro, Usuario pUsuario, string pOrden = null)
        {
            try
            {
                return BOTercero.ListarTerceroNoAfiliados(vTercero, pFiltro, pUsuario, pOrden);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroService", "ListarTerceroNoAfiliados", ex);
                return null;
            }
        }


        public List<Tercero> ListarTercero(Tercero vTercero, string pFiltro, string pOrden, Usuario pUsuario)
        {
            try
            {
                return BOTercero.ListarTercero(vTercero, pFiltro, pOrden, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroService", "ListarTercero", ex);
                return null;
            }
        }

        /// <summary>
        /// Valida la existencia y acceso del Tercero en el sistema
        /// </summary>
        /// <param name="pTercero">nombre de Tercero</param>
        /// <param name="pPassword">clave de acceso</param>
        /// <returns>Entidad Tercero</returns>
        public Tercero ValidarTercero(Int64 pTercero, Usuario pUsuario)
        {
            try
            {
                return BOTercero.ValidarTercero(pTercero, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroService", "ValidarTercero", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario vusuario)
        {
            try
            {
                return BOTercero.ObtenerSiguienteCodigo(vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroService", "ObtenerSiguienteCodigo", ex);
                return Int64.MinValue;
            }
        }

        /// <summary>
        /// Servicio para listar asociados de una persona juridica
        /// </summary>
        /// <param name="cod_persona">Código de la persona juridica</param>
        /// <param name="pUsuario">Variable de Usuario</param>
        /// <returns></returns>
        public List<Tercero> ListarAsociados(Int64 cod_persona, Usuario pUsuario)
        {
            try
            {
                return BOTercero.ListarAsociados(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TerceroService", "ListarAsociados", ex);
                return null;
            }
        }

    }
}