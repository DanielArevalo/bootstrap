using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Business
{
    /// <summary>
    /// Objeto de negocio para TipoLiqAporte
    /// </summary>
    public class TipoLiqAporteBusiness : GlobalBusiness
    {
        private TipoLiqAporteData DATipoLiqAporte;

        /// <summary>
        /// Constructor del objeto de negocio para TipoLiqAporte
        /// </summary>
        public TipoLiqAporteBusiness()
        {
            DATipoLiqAporte = new TipoLiqAporteData();
        }

        /// <summary>
        /// Crea un TipoLiqAporte
        /// </summary>
        /// <param name="pTipoLiqAporte">Entidad TipoLiqAporte</param>
        /// <returns>Entidad TipoLiqAporte creada</returns>
        public TipoLiqAporte CrearTipoLiqAporte(TipoLiqAporte pTipoLiqAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoLiqAporte = DATipoLiqAporte.CrearTipoLiqAporte(pTipoLiqAporte, pUsuario);

                    ts.Complete();
                }

                return pTipoLiqAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiqAporteBusiness", "CrearTipoLiqAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un TipoLiqAporte
        /// </summary>
        /// <param name="pTipoLiqAporte">Entidad TipoLiqAporte</param>
        /// <returns>Entidad TipoLiqAporte modificada</returns>
        public TipoLiqAporte ModificarTipoLiqAporte(TipoLiqAporte pTipoLiqAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoLiqAporte = DATipoLiqAporte.ModificarTipoLiqAporte(pTipoLiqAporte, pUsuario);

                    ts.Complete();
                }

                return pTipoLiqAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiqAporteBusiness", "ModificarTipoLiqAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TipoLiqAporte
        /// </summary>
        /// <param name="pId">Identificador de TipoLiqAporte</param>
        public void EliminarTipoLiqAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoLiqAporte.EliminarTipoLiqAporte(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiqAporteBusiness", "EliminarTipoLiqAporte", ex);
            }
        }

        /// <summary>
        /// Obtiene un TipoLiqAporte
        /// </summary>
        /// <param name="pId">Identificador de TipoLiqAporte</param>
        /// <returns>Entidad TipoLiqAporte</returns>
        public TipoLiqAporte ConsultarTipoLiqAporte(Int64 pId, Usuario vUsuario)
        {
            try
            {
                TipoLiqAporte TipoLiqAporte = new TipoLiqAporte();

                TipoLiqAporte = DATipoLiqAporte.ConsultarTipoLiqAporte(pId, vUsuario);

                return TipoLiqAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiqAporteBusiness", "ConsultarTipoLiqAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTipoLiqAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoLiqAporte obtenidos</returns>
        public List<TipoLiqAporte> ListarTipoLiqAporte(TipoLiqAporte pTipoLiqAporte, Usuario pUsuario)
        {
            try
            {
                return DATipoLiqAporte.ListarTipoLiqAporte(pTipoLiqAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiqAporteBusiness", "ListarTipoLiqAporte", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un TipoLiqAporte
        /// </summary>
        /// <param name="pId">Identificador de TipoLiqAporte</param>
        /// <returns>Entidad TipoLiqAporte</returns>
        public TipoLiqAporte ConsultarMax(Usuario vUsuario)
        {
            try
            {
                TipoLiqAporte TipoLiqAporte = new TipoLiqAporte();

                TipoLiqAporte = DATipoLiqAporte.ConsultarMax(vUsuario);

                return TipoLiqAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiqAporteBusiness", "ConsultarMax", ex);
                return null;
            }
        }

    }
}