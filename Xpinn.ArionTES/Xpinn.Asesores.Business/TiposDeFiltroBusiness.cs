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
    public class TiposDeFiltroBusiness : GlobalData
    {
        private TiposDeFiltroData DATiposDeFiltro;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public TiposDeFiltroBusiness()
        {
            DATiposDeFiltro = new TiposDeFiltroData();
        }

        /// <summary>
        /// Obtiene la lista de TiposDeFiltros
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de TiposDeFiltros obtenidas</returns>        
        public List<TiposDeFiltro> ListarTiposDeFiltros(TiposDeFiltro pTiposDeFiltro, Usuario pUsuario)
        {
            try
            {
                return DATiposDeFiltro.ListarTiposDeFiltros(pTiposDeFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDeFiltroBusiness", "ListarTiposDeFiltros", ex);
                return null;
            }
        }
    }
}