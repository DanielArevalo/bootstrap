using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para TipSopCaj
    /// </summary>
    public class TipSopCajBusiness : GlobalBusiness
    {
        private TipSopCajData DATipSopCaj;

        /// <summary>
        /// Constructor del objeto de negocio para TipSopCaj
        /// </summary>
        public TipSopCajBusiness()
        {
            DATipSopCaj = new TipSopCajData();
        }

        /// <summary>
        /// Crea un TipSopCaj
        /// </summary>
        /// <param name="pTipSopCaj">Entidad TipSopCaj</param>
        /// <returns>Entidad TipSopCaj creada</returns>
        public TipSopCaj CrearTipSopCaj(TipSopCaj pTipSopCaj, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipSopCaj = DATipSopCaj.CrearTipSopCaj(pTipSopCaj, pUsuario);

                    ts.Complete();
                }

                return pTipSopCaj;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipSopCajBusiness", "CrearTipSopCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un TipSopCaj
        /// </summary>
        /// <param name="pTipSopCaj">Entidad TipSopCaj</param>
        /// <returns>Entidad TipSopCaj modificada</returns>
        public TipSopCaj ModificarTipSopCaj(TipSopCaj pTipSopCaj, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipSopCaj = DATipSopCaj.ModificarTipSopCaj(pTipSopCaj, pUsuario);

                    ts.Complete();
                }

                return pTipSopCaj;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipSopCajBusiness", "ModificarTipSopCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TipSopCaj
        /// </summary>
        /// <param name="pId">Identificador de TipSopCaj</param>
        public void EliminarTipSopCaj(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipSopCaj.EliminarTipSopCaj(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipSopCajBusiness", "EliminarTipSopCaj", ex);
            }
        }

        /// <summary>
        /// Obtiene un TipSopCaj
        /// </summary>
        /// <param name="pId">Identificador de TipSopCaj</param>
        /// <returns>Entidad TipSopCaj</returns>
        public TipSopCaj ConsultarTipSopCaj(Int64 pId, Usuario vUsuario)
        {
            try
            {
                TipSopCaj TipSopCaj = new TipSopCaj();

                TipSopCaj = DATipSopCaj.ConsultarTipSopCaj(pId, vUsuario);

                return TipSopCaj;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipSopCajBusiness", "ConsultarTipSopCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTipSopCaj">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipSopCaj obtenidos</returns>
        public List<TipSopCaj> ListarTipSopCaj(TipSopCaj pTipSopCaj, Usuario pUsuario)
        {
            try
            {
                return DATipSopCaj.ListarTipSopCaj(pTipSopCaj, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipSopCajBusiness", "ListarTipSopCaj", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DATipSopCaj.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipSopCajBusiness", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }


    }
}