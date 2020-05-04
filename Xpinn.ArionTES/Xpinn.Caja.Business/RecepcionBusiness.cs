using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para Recepcion
    /// </summary>
    public class RecepcionBusiness:GlobalData
    {
         private RecepcionData DARecepcion;

        /// <summary>
        /// Constructor del objeto de negocio para Recepcion
        /// </summary>
        public RecepcionBusiness()
        {
            DARecepcion = new RecepcionData();
        }

        /// <summary>
        /// Crea un Recepcion
        /// </summary>
        /// <param name="pEntity">Entidad Recepcion</param>
        /// <returns>Entidad creada</returns>
        public Recepcion CrearRecepcion(Recepcion pRecepcion, GridView gvTraslados, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pRecepcion = DARecepcion.InsertarRecepcion(pRecepcion,gvTraslados,pUsuario);

                    ts.Complete();
                }

                return pRecepcion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecepcionBusiness", "CrearRecepcion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una Recepcion
        /// </summary>
        /// <param name="pId">identificador del Recepcion</param>
        /// <returns>Recepcion consultada</returns>
        public Recepcion ConsultarCajero(Usuario pUsuario)
        {
            try
            {
                return DARecepcion.ConsultarCajero(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecepcionBusiness", "ConsultarCajero", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Oficinas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Oficinas obtenidos</returns>
        public List<Traslado> ListarTraslado(Recepcion pRecepcion, Usuario pUsuario)
        {
            try
            {
                return DARecepcion.ListarTraslado(pRecepcion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecepcionBusiness", "ListarTraslado", ex);
                return null;
            }
        }

        /// <summary>
        /// Consulta un traslado segun la caja de origen/destino
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Traslado obtenido</returns>
        public Traslado ConsultarTraslado(Recepcion pRecepcion, Usuario pUsuario)
        {
            try
            {
                return DARecepcion.ConsultarTraslado(pRecepcion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecepcionBusiness", "ListarTraslado", ex);
                return null;
            }
        }

    }
}
