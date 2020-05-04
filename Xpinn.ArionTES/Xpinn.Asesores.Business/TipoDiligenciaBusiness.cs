using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    /// <summary>
    /// Objeto de negocio para TipoDiligencia
    /// </summary>
    public class TipoDiligenciaBusiness : GlobalBusiness
    {
        private TipoDiligenciaData DATipoDiligencia;

        /// <summary>
        /// Constructor del objeto de negocio para TipoDiligencia
        /// </summary>
        public TipoDiligenciaBusiness()
        {
            DATipoDiligencia = new TipoDiligenciaData();
        }

        /// <summary>
        /// Crea un TipoDiligencia
        /// </summary>
        /// <param name="pTipoDiligencia">Entidad TipoDiligencia</param>
        /// <returns>Entidad TipoDiligencia creada</returns>
        public TipoDiligencia CrearTipoDiligencia(TipoDiligencia pTipoDiligencia, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoDiligencia = DATipoDiligencia.CrearTipoDiligencia(pTipoDiligencia, pUsuario);

                    ts.Complete();
                }

                return pTipoDiligencia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoDiligenciaBusiness", "CrearTipoDiligencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un TipoDiligencia
        /// </summary>
        /// <param name="pTipoDiligencia">Entidad TipoDiligencia</param>
        /// <returns>Entidad TipoDiligencia modificada</returns>
        public TipoDiligencia ModificarTipoDiligencia(TipoDiligencia pTipoDiligencia, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoDiligencia = DATipoDiligencia.ModificarTipoDiligencia(pTipoDiligencia, pUsuario);

                    ts.Complete();
                }

                return pTipoDiligencia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoDiligenciaBusiness", "ModificarTipoDiligencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TipoDiligencia
        /// </summary>
        /// <param name="pId">Identificador de TipoDiligencia</param>
        public void EliminarTipoDiligencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoDiligencia.EliminarTipoDiligencia(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoDiligenciaBusiness", "EliminarTipoDiligencia", ex);
            }
        }

        /// <summary>
        /// Obtiene un TipoDiligencia
        /// </summary>
        /// <param name="pId">Identificador de TipoDiligencia</param>
        /// <returns>Entidad TipoDiligencia</returns>
        public TipoDiligencia ConsultarTipoDiligencia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DATipoDiligencia.ConsultarTipoDiligencia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoDiligenciaBusiness", "ConsultarTipoDiligencia", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTipoDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoDiligencia obtenidos</returns>
        public List<TipoDiligencia> ListarTipoDiligencia(TipoDiligencia pTipoDiligencia, Usuario pUsuario)
        {
            try
            {
                return DATipoDiligencia.ListarTipoDiligencia(pTipoDiligencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoDiligenciaBusiness", "ListarTipoDiligencia", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTipoDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoDiligencia obtenidos</returns>
        public List<TipoDiligencia> ListarTipoDiligenciaAgregar(TipoDiligencia pTipoDiligencia, Usuario pUsuario)
        {
            try
            {
                return DATipoDiligencia.ListarTipoDiligenciaAgregar(pTipoDiligencia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoDiligenciaBusiness", "ListarTipoDiligenciaModificar", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTipoDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoContacto obtenidos</returns>
        public List<TipoContacto> ListarTipoContacto(TipoContacto pTipoContacto, Usuario pUsuario)
        {
            try
            {
                return DATipoDiligencia.ListarTipoContacto(pTipoContacto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoContactoBusiness", "ListarTipoContacto", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTipoDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoContacto obtenidos</returns>
        public List<TipoContacto> ListarTipoContactoAgregar(TipoContacto pTipoContacto, Usuario pUsuario)
        {
            try
            {
                return DATipoDiligencia.ListarTipoContactoAgregar(pTipoContacto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoContactoBusiness", "ListarTipoContactoAgregar", ex);
                return null;
            }
        }

    }
}