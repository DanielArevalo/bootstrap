using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para comprobante
    /// </summary>
    public class TipoMonedaBusiness : GlobalData
    {
        private TipoMonedaData DATipoMoneda;

        /// <summary>
        /// Constructor del objeto de negocio para Usuarios
        /// </summary>
        public TipoMonedaBusiness()
        {
            DATipoMoneda = new TipoMonedaData();
        }

     

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<TipoMoneda> ListarTipoMoneda(Usuario pUsuario)
        {
            try
            {
                return DATipoMoneda.ListarTipoMoneda(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMonedaBusiness", "ListarTipoMoneda", ex);
                return null;
            }
        }
        /// <summary>
       
       
    }
}