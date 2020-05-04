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
    public class CondonacionInteresBusiness : GlobalData
    {
        private CondonacionData DACondonacionInteres;

        /// <summary>
        /// Constructor del objeto de negocio para Credito
        /// </summary>
        public CondonacionInteresBusiness()
        {
            DACondonacionInteres = new CondonacionData();
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<CondonacionInteres> ListarCredito(CondonacionInteres pCredito, Usuario pUsuario)
        {
            try
            {
                return DACondonacionInteres.ListarCredito(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CondonacionInteresBusiness", "ListarCredito", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public CondonacionInteres CrearCondonacion(CondonacionInteres pcondonacion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    CondonacionInteres entidad = new CondonacionInteres();
                    entidad = DACondonacionInteres.CrearCondonacion(pcondonacion, pUsuario);
                    ts.Complete();
                    return entidad;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CondonacionInteresBusiness", "CrearCondonacion", ex);
                return null;
            }
        }
      
    }
}