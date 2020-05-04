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
    public class EnvioDeCorreoExtractoBusiness : GlobalData
    {
        private EnvioDeCorreoExtractoData DAEnvioDeCorreoExtracto;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public EnvioDeCorreoExtractoBusiness()
        {
            DAEnvioDeCorreoExtracto = new EnvioDeCorreoExtractoData();
        }

        /// <summary>
        /// Obtiene la lista de EnvioDeCorreoExtractos
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de EnvioDeCorreoExtractos obtenidas</returns>        
        public List<EnvioDeCorreoExtracto> ListarEnvioDeCorreoExtractos(EnvioDeCorreoExtracto pEnvioDeCorreoExtracto, Usuario pUsuario)
        {
            try
            {
                return DAEnvioDeCorreoExtracto.ListarEnvioDeCorreoExtractos(pEnvioDeCorreoExtracto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EnvioDeCorreoExtractoBusiness", "ListarEnvioDeCorreoExtractos", ex);
                return null;
            }
        }
    }
}