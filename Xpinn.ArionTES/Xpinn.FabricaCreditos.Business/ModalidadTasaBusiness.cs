using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    public class ModalidadTasaBusiness : GlobalData
    {
        private ModalidadtasaData DAModalidadTasa;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public ModalidadTasaBusiness()
        {
            DAModalidadTasa = new ModalidadtasaData();
        }
        /// <summary>
        /// Obtiene la lista de Oficinas
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Oficinas obtenidas</returns> 
        public List<ModalidadTasa> ListarModalidadTasa(String pIdCodLinea, Usuario pUsuario)
        {
            try
            {
                return DAModalidadTasa.ListarModalidadTasa(pIdCodLinea, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ModalidadTasaBusiness", "ListarModalidadTasa", ex);
                return null;
            }
        }
    }
        
}
