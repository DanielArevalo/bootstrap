using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;
//Prueba s  1257

namespace Xpinn.FabricaCreditos.Business
{
    public class AnexoCreditoBusiness : GlobalData
    {
        private AnexoCreditoData DAAnexo;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public AnexoCreditoBusiness()
        {
            DAAnexo = new AnexoCreditoData();
        }
        
        /// <summary>
        /// Obtiene la lista de anexos
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de anexos obtenidos</returns>
        public List<AnexoCredito> ListarAnexos(AnexoCredito pAnexo, Usuario pUsuario)
        {
            try
            {
                return DAAnexo.ListarAnexos(pAnexo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AnexoCreditoBusiness", "ListarAnexosCredito", ex);
                return null;
            }
        }
    }
}
