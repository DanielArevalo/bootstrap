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
    public class RegistroGeneraciónExtractoBusiness : GlobalData
    {
        private RegistroGeneraciónExtractoData DARegistroGeneraciónExtracto;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public RegistroGeneraciónExtractoBusiness()
        {
            DARegistroGeneraciónExtracto = new RegistroGeneraciónExtractoData();
        }

        /// <summary>
        /// Obtiene la lista de RegistroGeneraciónExtractos
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de RegistroGeneraciónExtractos obtenidas</returns>        
        public RegistroGeneraciónExtracto AlmacenarRegistroGeneraciónExtractos(RegistroGeneraciónExtracto pRegistroGeneraciónExtracto, Usuario pUsuario)
        {
            try
            {
                return DARegistroGeneraciónExtracto.AlmacenarRegistroGeneraciónExtractos(pRegistroGeneraciónExtracto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RegistroGeneraciónExtractoBusiness", "ListarRegistroGeneraciónExtractos", ex);
                return null;
            }
        }
    }
}