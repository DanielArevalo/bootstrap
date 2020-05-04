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
    public class ComponenteAdicionalBusiness : GlobalBusiness
    {
        private ComponenteAdicionalData DAComponenteAdicional;

        /// <summary>
        /// Constructor del objeto de negocio para LineaObligacion
        /// </summary>
        public ComponenteAdicionalBusiness()
        {
            DAComponenteAdicional = new ComponenteAdicionalData();
        }

        /// <summary>
        /// Obtiene la lista de ComponenteAdicionals dados unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ComponenteAdicionals obtenidos</returns>
        public List<ComponenteAdicional> ListarComponenteAdicional(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAComponenteAdicional.ListarComponenteAdicional(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteAdicionalBusiness", "ListarComponenteAdicional", ex);
                return null;
            }
        }
    }
}
