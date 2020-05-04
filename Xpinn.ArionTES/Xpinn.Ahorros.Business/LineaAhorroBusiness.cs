using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Ahorros.Data;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Business
{
    /// <summary>
    /// Objeto de negocio para LineaAhorro
    /// </summary>
    public class LineaAhorroBusiness : GlobalBusiness
    {
        private LineaAhorroData DALineaAhorro;

        /// <summary>
        /// Constructor del objeto de negocio para LineaAhorro
        /// </summary>
        public LineaAhorroBusiness()
        {
            DALineaAhorro = new LineaAhorroData();
        }

        /// <summary>
        /// Crea un LineaAhorro
        /// </summary>
        /// <param name="pLineaAhorro">Entidad LineaAhorro</param>
        /// <returns>Entidad LineaAhorro creada</returns>
        public LineaAhorro CrearLineaAhorro(LineaAhorro pLineaAhorro, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaAhorro = DALineaAhorro.CrearLineaAhorro(pLineaAhorro, pUsuario);
                    ts.Complete();
                }

                return pLineaAhorro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAhorroBusiness", "CrearLineaAhorro", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un LineaAhorro
        /// </summary>
        /// <param name="pLineaAhorro">Entidad LineaAhorro</param>
        /// <returns>Entidad LineaAhorro modificada</returns>
        public LineaAhorro ModificarLineaAhorro(LineaAhorro pLineaAhorro, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaAhorro = DALineaAhorro.ModificarLineaAhorro(pLineaAhorro, pUsuario);

                    ts.Complete();
                }

                return pLineaAhorro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAhorroBusiness", "ModificarLineaAhorro", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un LineaAhorro
        /// </summary>
        /// <param name="pId">Identificador de LineaAhorro</param>
        public void EliminarLineaAhorro(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALineaAhorro.EliminarLineaAhorro(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAhorroBusiness", "EliminarLineaAhorro", ex);
            }
        }

        /// <summary>
        /// Obtiene un LineaAhorro
        /// </summary>
        /// <param name="pId">Identificador de LineaAhorro</param>
        /// <returns>Entidad LineaAhorro</returns>
        public LineaAhorro ConsultarLineaAhorro(Int64 pId, Usuario vUsuario)
        {
            try
            {
                LineaAhorro LineaAhorro = new LineaAhorro();
                LineaAhorro = DALineaAhorro.ConsultarLineaAhorro(pId, vUsuario);
                return LineaAhorro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAhorroBusiness", "ConsultarLineaAhorro", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pLineaAhorro">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de LineaAhorro obtenidos</returns>
        public List<LineaAhorro> ListarLineaAhorro(LineaAhorro pLineaAhorro, Usuario pUsuario)
        {
            try
            {
                return DALineaAhorro.ListarLineaAhorro(pLineaAhorro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAhorroBusiness", "ListarLineaAhorro", ex);
                return null;
            }
        }

    }
}