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
    public class LineaObligacionBusiness : GlobalBusiness
    {
       private LineaObligacionData DALineaObligacion;

        /// <summary>
        /// Constructor del objeto de negocio para LineaObligacion
        /// </summary>
        public LineaObligacionBusiness()
        {
            DALineaObligacion = new LineaObligacionData();
        }

         /// <summary>
        /// Obtiene la lista de LineaObligacion dados unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de LineaObligacion obtenidos</returns>
        public List<LineaObligacion> ListarLineaObligacion(LineaObligacion pTipLiq, Usuario pUsuario)
        {
            try
            {
                return DALineaObligacion.ListarLineaObligacion(pTipLiq, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaObligacionBusiness", "ListarLineaObligacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina una Linea de Obligacion
        /// </summary>
        /// <param name="pId">identificador de la Linea Obligacion</param>
        public void EliminarLineaOb(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DALineaObligacion.EliminarLineaOb(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaObligacionBusiness", "EliminarLineaOb", ex);
            }
        }

        /// <summary>
        /// Crea una Linea de Obligacion
        /// </summary>
        /// <param name="pEntity">Entidad Linea Obligacion</param>
        /// <returns>Entidad creada</returns>
        public LineaObligacion CrearLineaOb(LineaObligacion pLineaOb, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaOb = DALineaObligacion.CrearLineaOb(pLineaOb, pUsuario);

                    ts.Complete();
                }

                return pLineaOb;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaObligacionBusiness", "CrearLineaOb", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica una Linea de Obligacion
        /// </summary>
        /// <param name="pEntity">Entidad Linea de Obligacion</param>
        /// <returns>Entidad modificada</returns>
        public LineaObligacion ModificarLineaOb(LineaObligacion pLineaOb, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaOb = DALineaObligacion.ModificarLineaOb(pLineaOb, pUsuario);

                    ts.Complete();
                }

                return pLineaOb;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaObligacionBusiness", "ModificarLineaOb", ex);
                return null;
            }

        }


        /// <summary>
        /// Obtiene una LInea de Obligacion
        /// </summary>
        /// <param name="pId">identificador de la LInea</param>
        /// <returns>LInea consultada</returns>
        public LineaObligacion ConsultarLineaOb(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DALineaObligacion.ConsultarLineaOb(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaObligacionBusiness", "ConsultarLineaOb", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene el conteo de usuarios asociados a la oficina especifica 
        /// </summary>
        /// <param name="pId">identificador de la LineaObligacion</param>
        /// <returns>Caja consultada</returns>
        public LineaObligacion ConsultarObligacionXLineaObligacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DALineaObligacion.ConsultarObligacionXLineaObligacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaObligacionBusiness", "ConsultarObligacionXLineaObligacion", ex);
                return null;
            }
        }
    }
}
