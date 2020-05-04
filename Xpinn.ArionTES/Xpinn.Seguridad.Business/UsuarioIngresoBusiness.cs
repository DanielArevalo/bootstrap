using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Seguridad.Data;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Business
{
    /// <summary>
    /// Objeto de negocio para Proceso
    /// </summary>
    public class UsuarioIngresoBusiness : GlobalBusiness
    {
        private UsuarioIngresoData DAProceso;

        /// <summary>
        /// Constructor del objeto de negocio para Proceso
        /// </summary>
        public UsuarioIngresoBusiness()
        {
            DAProceso = new UsuarioIngresoData();
        }

       

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<Ingresos> ListarIngresos(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            try
            {
                return DAProceso.ListarIngresos(filtro, pFechaIni, pFechaFin,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioIngresoBusiness", "ListarIngresos", ex);
                return null;
            }
        }

    }
}