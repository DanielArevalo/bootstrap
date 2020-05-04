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
    /// Objeto de negocio para CentroCosto
    /// </summary>
    public class CentroCostoBusiness : GlobalBusiness
    {
        private CentroCostoData DACentroCosto;

        /// <summary>
        /// Constructor del objeto de negocio para CentroCosto
        /// </summary>
        public CentroCostoBusiness()
        {
            DACentroCosto = new CentroCostoData();
        }

        /// <summary>
        /// Crea un CentroCosto
        /// </summary>
        /// <param name="pCentroCosto">Entidad CentroCosto</param>
        /// <returns>Entidad CentroCosto creada</returns>
        public CentroCosto CrearCentroCosto(CentroCosto pCentroCosto, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCentroCosto = DACentroCosto.CrearCentroCosto(pCentroCosto, vusuario);

                    ts.Complete();
                }

                return pCentroCosto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroCostoBusiness", "CrearCentroCosto", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un CentroCosto
        /// </summary>
        /// <param name="pCentroCosto">Entidad CentroCosto</param>
        /// <returns>Entidad CentroCosto modificada</returns>
        public CentroCosto ModificarCentroCosto(CentroCosto pCentroCosto, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCentroCosto = DACentroCosto.ModificarCentroCosto(pCentroCosto, vusuario);

                    ts.Complete();
                }

                return pCentroCosto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroCostoBusiness", "ModificarCentroCosto", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un CentroCosto
        /// </summary>
        /// <param name="pId">Identificador de CentroCosto</param>
        public void EliminarCentroCosto(Int64 pId, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACentroCosto.EliminarCentroCosto(pId, vusuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroCostoBusiness", "EliminarCentroCosto", ex);
            }
        }

        /// <summary>
        /// Obtiene un CentroCosto
        /// </summary>
        /// <param name="pId">Identificador de CentroCosto</param>
        /// <returns>Entidad CentroCosto</returns>
        public CentroCosto ConsultarCentroCosto(Int64 pId, Usuario vusuario)
        {
            try
            {
                CentroCosto CentroCosto = new CentroCosto();

                CentroCosto = DACentroCosto.ConsultarCentroCosto(pId, vusuario);

                return CentroCosto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroCostoBusiness", "ConsultarCentroCosto", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCentroCosto">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CentroCosto obtenidos</returns>
        public List<CentroCosto> ListarCentroCosto(CentroCosto pCentroCosto, Usuario vUsuario)
        {
            try
            {
                return DACentroCosto.ListarCentroCosto(pCentroCosto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroCostoBusiness", "ListarCentroCosto", ex);
                return null;
            }
        }


        /// <summary>
        /// Método que trae listado de centros de costo
        /// </summary>
        /// <param name="vUsuario"></param>
        /// <param name="sFiltro"></param>
        /// <returns></returns>
        public List<CentroCosto> ListarCentroCosto(Usuario vUsuario, string sFiltro)
        {
            return DACentroCosto.ListarCentroCosto(vUsuario, sFiltro);
        }

        /// <summary>
        /// Método que trae el centro de costo inicial y el final
        /// </summary>
        /// <param name="CenIni"></param>
        /// <param name="CenFin"></param>
        public void RangoCentroCosto(ref Int64 CenIni, ref Int64 CenFin, Usuario pUsuario)
        {
            DACentroCosto.RangoCentroCosto(ref CenIni, ref CenFin, pUsuario);
        }
    }
}

