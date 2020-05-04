using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Data;

namespace Xpinn.Cartera.Business
{
    /// <summary>
    /// Objeto de negocio para Credito
    /// </summary>
    public class CondonacionTotalBusiness : GlobalData
    {
        private CondonaciontotalData DACondonacionInteres;

        /// <summary>
        /// Constructor del objeto de negocio para Credito
        /// </summary>
        public CondonacionTotalBusiness()
        {
            DACondonacionInteres = new CondonaciontotalData();
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<CondonacionTotal> ListarCredito(CondonacionTotal pCredito, Usuario pUsuario)
        {
            try
            {
                return DACondonacionInteres.ListarCredito(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CondonaciontotalBusiness", "ListarCredito", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public CondonacionTotal CrearCondonacion(CondonacionTotal pcondonacion, Usuario pUsuario)
        {
            try
            {
                return DACondonacionInteres.CrearCondonacion(pcondonacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CondonaciontotalBusiness", "CrearCondonacion", ex);
                return null;
            }
        }
      
    }
}