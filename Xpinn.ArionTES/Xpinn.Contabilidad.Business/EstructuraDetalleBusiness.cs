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
    /// Objeto de negocio para EstructuraDetalle
    /// </summary>
    public class EstructuraDetalleBusiness : GlobalBusiness
    {
        private EstructuraDetalleData DAEstructuraDetalle;

        /// <summary>
        /// Constructor del objeto de negocio para EstructuraDetalle
        /// </summary>
        public EstructuraDetalleBusiness()
        {
            DAEstructuraDetalle = new EstructuraDetalleData();
        }

        /// <summary>
        /// Crea un EstructuraDetalle
        /// </summary>
        /// <param name="pEstructuraDetalle">Entidad EstructuraDetalle</param>
        /// <returns>Entidad EstructuraDetalle creada</returns>
        public EstructuraDetalle CrearEstructuraDetalle(EstructuraDetalle pEstructuraDetalle, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEstructuraDetalle = DAEstructuraDetalle.CrearEstructuraDetalle(pEstructuraDetalle, vusuario);

                    ts.Complete();
                }

                return pEstructuraDetalle;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraDetalleBusiness", "CrearEstructuraDetalle", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un EstructuraDetalle
        /// </summary>
        /// <param name="pEstructuraDetalle">Entidad EstructuraDetalle</param>
        /// <returns>Entidad EstructuraDetalle modificada</returns>
        public EstructuraDetalle ModificarEstructuraDetalle(EstructuraDetalle pEstructuraDetalle, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEstructuraDetalle = DAEstructuraDetalle.ModificarEstructuraDetalle(pEstructuraDetalle, vusuario);

                    ts.Complete();
                }

                return pEstructuraDetalle;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraDetalleBusiness", "ModificarEstructuraDetalle", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un EstructuraDetalle
        /// </summary>
        /// <param name="pId">Identificador de EstructuraDetalle</param>
        public void EliminarEstructuraDetalle(int pId, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEstructuraDetalle.EliminarEstructuraDetalle(pId, vusuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraDetalleBusiness", "EliminarEstructuraDetalle", ex);
            }
        }

        /// <summary>
        /// Obtiene un EstructuraDetalle
        /// </summary>
        /// <param name="pId">Identificador de EstructuraDetalle</param>
        /// <returns>Entidad EstructuraDetalle</returns>
        public EstructuraDetalle ConsultarEstructuraDetalle(int pId, Usuario vusuario)
        {
            try
            {
                EstructuraDetalle EstructuraDetalle = new EstructuraDetalle();

                EstructuraDetalle = DAEstructuraDetalle.ConsultarEstructuraDetalle(pId, vusuario);

                return EstructuraDetalle;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraDetalleBusiness", "ConsultarEstructuraDetalle", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pEstructuraDetalle">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EstructuraDetalle obtenidos</returns>
        public List<EstructuraDetalle> ListarEstructuraDetalle(EstructuraDetalle pEstructuraDetalle, Usuario vUsuario)
        {
            try
            {
                return DAEstructuraDetalle.ListarEstructuraDetalle(pEstructuraDetalle, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraDetalleBusiness", "ListarEstructuraDetalle", ex);
                return null;
            }
        }


    }
}