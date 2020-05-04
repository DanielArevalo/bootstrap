using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Business
{
    /// <summary>
    /// Objeto de negocio para LineaAporte
    /// </summary>
    public class LineaAporteBusiness : GlobalBusiness
    {
        private LineaAporteData DALineaAporte;

        /// <summary>
        /// Constructor del objeto de negocio para LineaAporte
        /// </summary>
        public LineaAporteBusiness()
        {
            DALineaAporte = new LineaAporteData();
        }

        /// <summary>
        /// Crea un LineaAporte
        /// </summary>
        /// <param name="pLineaAporte">Entidad LineaAporte</param>
        /// <returns>Entidad LineaAporte creada</returns>
        public LineaAporte CrearLineaAporte(LineaAporte pLineaAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaAporte = DALineaAporte.CrearLineaAporte(pLineaAporte, pUsuario);

                    ts.Complete();
                }

                return pLineaAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteBusiness", "CrearLineaAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un LineaAporte
        /// </summary>
        /// <param name="pLineaAporte">Entidad LineaAporte</param>
        /// <returns>Entidad LineaAporte modificada</returns>
        public LineaAporte ModificarLineaAporte(LineaAporte pLineaAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaAporte = DALineaAporte.ModificarLineaAporte(pLineaAporte, pUsuario);

                    ts.Complete();
                }

                return pLineaAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteBusiness", "ModificarLineaAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un LineaAporte
        /// </summary>
        /// <param name="pId">Identificador de LineaAporte</param>
        public void EliminarLineaAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALineaAporte.EliminarLineaAporte(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteBusiness", "EliminarLineaAporte", ex);
            }
        }

        /// <summary>
        /// Obtiene un LineaAporte
        /// </summary>
        /// <param name="pId">Identificador de LineaAporte</param>
        /// <returns>Entidad LineaAporte</returns>
        public LineaAporte ConsultarLineaAporte(Int64 pId, Usuario vUsuario)
        {
            try
            {
                LineaAporte LineaAporte = new LineaAporte();

                LineaAporte = DALineaAporte.ConsultarLineaAporte(pId, vUsuario);

                return LineaAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteBusiness", "ConsultarLineaAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pLineaAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de LineaAporte obtenidos</returns>
        public List<LineaAporte> ListarLineaAporte(LineaAporte pLineaAporte, Usuario pUsuario)
        {
            try
            {
                return DALineaAporte.ListarLineaAporte(pLineaAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaAporteBusiness", "ListarLineaAporte", ex);
                return null;
            }
        }



    }
}