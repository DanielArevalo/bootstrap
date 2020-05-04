using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Obligaciones.Data;
using Xpinn.Obligaciones.Entities;
using System.Web.UI.WebControls;

namespace Xpinn.Obligaciones.Business
{
    public class TipoTasaBusiness : GlobalBusiness
    {
          private TipoTasaData DATipoTasa;

        /// <summary>
        /// Constructor del objeto de negocio para TipoTasa
        /// </summary>
        public TipoTasaBusiness()
        {
            DATipoTasa = new TipoTasaData();
        }

         /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoTasa obtenidos</returns>
        public List<TipoTasa> ListarTipoTasa(TipoTasa pTipLiq, Usuario pUsuario)
        {
            try
            {
                return DATipoTasa.ListarTipoTasa(pTipLiq, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaBusiness", "ListarTipoTasa", ex);
                return null;
            }
        }

        public List<TipoTasa> ListarTipoHistorico(TipoTasa pTipLiq, Usuario pUsuario)
        {
            try
            {
                return DATipoTasa.ListarTipoHistorico(pTipLiq, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaBusiness", "ListarTipoHistorico", ex);
                return null;
            }
        }


        public double ConsultaTasaHistorica(Int64 pTipoHistorico, DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return DATipoTasa.ConsultaTasaHistorica(pTipoHistorico, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaBusiness", "ConsultaTasaHistorica", ex);
                return 0;
            }
        }

    }
}
