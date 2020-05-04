using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
         /// <summary>
        /// Objeto de negocio para TipoTope
        /// </summary>
        public class TipoTopeBusiness : GlobalData
        {
            private TipoTopeData DATipoTope;

            /// <summary>
            /// Constructor del objeto de negocio para TipoTope
            /// </summary>
            public TipoTopeBusiness()
            {
                DATipoTope = new TipoTopeData();
            }

            /// <summary>
            /// Obtiene la lista de TipoTope-Caja dados unos filtros
            /// </summary>
            /// <param name="pEntidad">Entidad con los filtros solicitados</param>
            /// <returns>Conjunto de Tipos Topes- Caja obtenidos</returns>
            public TipoTope ConsultarTipoTopeCaja(Int64 pId, Int64 pMon, Int64 pCaja, Usuario pUsuario)
            {
                try
                {
                    return DATipoTope.ConsultarTipoTopeCaja(pId, pMon, pCaja, pUsuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("TipoTopeBusiness", "ConsultarTipoTopeCaja", ex);
                    return null;
                }
            }

            /// <summary>
            /// Obtiene la lista de Tipos de Topes dados unos filtros
            /// </summary>
            /// <param name="pEntidad">Entidad con los filtros solicitados</param>
            /// <returns>Conjunto de Tipos de Topes obtenidos</returns>
            public List<TipoTope> ListarTipoTope(TipoTope pTipoOpe, Usuario pUsuario)
            {
                try
                {
                    return DATipoTope.ListarTipoTope(pTipoOpe, pUsuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("TipoTopeBusiness", "ListarTipoTope", ex);
                    return null;
                }
            }
        }
    }

