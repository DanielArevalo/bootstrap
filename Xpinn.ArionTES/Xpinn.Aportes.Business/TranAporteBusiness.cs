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
    /// Objeto de negocio para TranAporte
    /// </summary>
    public class TranAporteBusiness : GlobalBusiness
    {
        private TranAporteData DATranAporte;

        /// <summary>
        /// Constructor del objeto de negocio para TranAporte
        /// </summary>
        public TranAporteBusiness()
        {
            DATranAporte = new TranAporteData();
        }

        /// <summary>
        /// Crea un TranAporte
        /// </summary>
        /// <param name="pTranAporte">Entidad TranAporte</param>
        /// <returns>Entidad TranAporte creada</returns>
        public TranAporte CrearTranAporte(TranAporte pTranAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTranAporte = DATranAporte.CrearTranAporte(pTranAporte, pUsuario);

                    ts.Complete();
                }

                return pTranAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TranAporteBusiness", "CrearTranAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un TranAporte
        /// </summary>
        /// <param name="pTranAporte">Entidad TranAporte</param>
        /// <returns>Entidad TranAporte modificada</returns>
        public TranAporte ModificarTranAporte(TranAporte pTranAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTranAporte = DATranAporte.ModificarTranAporte(pTranAporte, pUsuario);

                    ts.Complete();
                }

                return pTranAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TranAporteBusiness", "ModificarTranAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TranAporte
        /// </summary>
        /// <param name="pId">Identificador de TranAporte</param>
        public void EliminarTranAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATranAporte.EliminarTranAporte(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TranAporteBusiness", "EliminarTranAporte", ex);
            }
        }

        /// <summary>
        /// Obtiene un TranAporte
        /// </summary>
        /// <param name="pId">Identificador de TranAporte</param>
        /// <returns>Entidad TranAporte</returns>
        public TranAporte ConsultarTranAporte(Int64 pId, Usuario vUsuario)
        {
            try
            {
                TranAporte TranAporte = new TranAporte();

                TranAporte = DATranAporte.ConsultarTranAporte(pId, vUsuario);

                return TranAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TranAporteBusiness", "ConsultarTranAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTranAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TranAporte obtenidos</returns>
        public List<TranAporte> ListarTranAporte(TranAporte pTranAporte, Usuario pUsuario)
        {
            try
            {
                return DATranAporte.ListarTranAporte(pTranAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TranAporteBusiness", "ListarTranAporte", ex);
                return null;
            }
        }



    }
}