using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Comun.Data;
using Xpinn.Comun.Entities;
using System.Web;

namespace Xpinn.Comun.Business
{
    public class GDocumentalBusiness : GlobalData
    {
        
        private GDocumentalData DAGDocumental;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public GDocumentalBusiness()
        {
            DAGDocumental = new GDocumentalData();
        }

        /// <summary>
        /// Listar cierres realizados
        /// </summary>
        /// <param name="pTipo"></param>
        /// <returns></returns>
        public List<GestionDocumental > ListarGDocumental(GestionDocumental pTipo, Usuario pUsuario)
        {
            return DAGDocumental.ListarGDocumental(pTipo, pUsuario);
        }

       


    }
}
