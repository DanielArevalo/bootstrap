using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    public class MotivoGeneracionExtractoBusiness : GlobalData
    {
        private MotivoGeneracionExtractoData DAMotivoGeneracionExtracto;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public MotivoGeneracionExtractoBusiness()
        {
            DAMotivoGeneracionExtracto = new MotivoGeneracionExtractoData();
        }

        /// <summary>
        /// Obtiene la lista de MotivoGeneracionExtractos
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de MotivoGeneracionExtractos obtenidas</returns>        
        public List<MotivoGeneracionExtracto> ListarMotivoGeneracionExtractos(MotivoGeneracionExtracto pMotivoGeneracionExtracto, Usuario pUsuario)
        {
            try
            {
                return DAMotivoGeneracionExtracto.ListarMotivoGeneracionExtractos(pMotivoGeneracionExtracto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoGeneracionExtractoBusiness", "ListarMotivoGeneracionExtractos", ex);
                return null;
            }
        }
    }
}