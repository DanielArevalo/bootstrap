using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para codeudores
    /// </summary>
    public class TipoTasaHistBusiness : GlobalBusiness
    {
        private TipoTasaHistData DATipoTasaHist;

        /// <summary>
        /// Constructor del objeto de negocio para tipos de tasa historico
        /// </summary>
        public TipoTasaHistBusiness()
        {
            DATipoTasaHist = new TipoTasaHistData();
        }

        /// <summary>
        /// Crea un tipo de tasa histórico
        /// </summary>
        /// <param name="pUsuario">Entidad tipotasahist</param>
        /// <returns>Entidad tipotasahist creada</returns>
        public TipoTasaHist CrearTipoTasaHist(TipoTasaHist pTipoTasaHist, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoTasaHist = DATipoTasaHist.CrearTipoTasaHist(pTipoTasaHist, vUsuario);

                    ts.Complete();
                }

                return pTipoTasaHist;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaHistBusiness", "CrearTipoTasaHist", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un tipo de tasa Hist
        /// </summary>
        /// <param name="pUsuario">Entidad tipo de tasa Hist</param>
        /// <returns>Entidad tipo tasa Hist modificada</returns>
        public TipoTasaHist ModificarTipoTasaHist(TipoTasaHist pTipoTasaHist, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoTasaHist = DATipoTasaHist.ModificarTipoTasaHist(pTipoTasaHist, vUsuario);

                    ts.Complete();
                }

                return pTipoTasaHist;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaHistBusiness", "ModificarTipoTasaHist", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un tipo de tasa Hist
        /// </summary>
        /// <param name="pId">Identificador de tipo de tasa</param>
        public void EliminarTipoTasaHist(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoTasaHist.EliminarTipoTasaHist(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaHistBusiness", "EliminarTipoTasaHist", ex);
            }
        }

        /// <summary>
        /// Obtiene un Usuario
        /// </summary>
        /// <param name="pId">Identificador de Usuario</param>
        /// <returns>Entidad Usuario</returns>
        public TipoTasaHist ConsultarTipoTasaHist(Int64 pId, Usuario vUsuario)
        {
            try
            {
                TipoTasaHist tipotasahist = new TipoTasaHist();

                tipotasahist = DATipoTasaHist.ConsultarTipoTasaHist(pId, vUsuario);

                return tipotasahist;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaHistBusiness", "ConsultarTipoTasaHist", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipo de tasa histórico obtenidos</returns>
        public List<TipoTasaHist> ListarTipoTasaHist(TipoTasaHist pTipoTasaHist, Usuario vUsuario)
        {
            try
            {
                return DATipoTasaHist.ListarTipoTasaHist(pTipoTasaHist, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaHistBusiness", "ListarTipoTasaHist", ex);
                return null;
            }
        }


    }
}