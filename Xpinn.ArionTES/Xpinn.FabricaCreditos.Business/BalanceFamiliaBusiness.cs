using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para BalanceFamilia
    /// </summary>
    public class BalanceFamiliaBusiness : GlobalData
    {
        private BalanceFamiliaData DABalanceFamilia;

        /// <summary>
        /// Constructor del objeto de negocio para BalanceFamilia
        /// </summary>
        public BalanceFamiliaBusiness()
        {
            DABalanceFamilia = new BalanceFamiliaData();
        }

        /// <summary>
        /// Crea un BalanceFamilia
        /// </summary>
        /// <param name="pBalanceFamilia">Entidad BalanceFamilia</param>
        /// <returns>Entidad BalanceFamilia creada</returns>
        public BalanceFamilia CrearBalanceFamilia(BalanceFamilia pBalanceFamilia, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pBalanceFamilia.totalactivo = pBalanceFamilia.terrenosyedificios + pBalanceFamilia.otros;
                    pBalanceFamilia.totalpasivo = pBalanceFamilia.corriente + pBalanceFamilia.largoplazo;
                    pBalanceFamilia.patrimonio = pBalanceFamilia.totalactivo - pBalanceFamilia.totalpasivo;
                    pBalanceFamilia.totalpasivoypatrimonio = pBalanceFamilia.totalpasivo + pBalanceFamilia.patrimonio;

                    pBalanceFamilia = DABalanceFamilia.CrearBalanceFamilia(pBalanceFamilia, pUsuario);

                    ts.Complete();
                }

                return pBalanceFamilia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceFamiliaBusiness", "CrearBalanceFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un BalanceFamilia
        /// </summary>
        /// <param name="pBalanceFamilia">Entidad BalanceFamilia</param>
        /// <returns>Entidad BalanceFamilia modificada</returns>
        public BalanceFamilia ModificarBalanceFamilia(BalanceFamilia pBalanceFamilia, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {                    
                    pBalanceFamilia.totalactivo = pBalanceFamilia.terrenosyedificios + pBalanceFamilia.otros;
                    pBalanceFamilia.totalpasivo = pBalanceFamilia.corriente + pBalanceFamilia.largoplazo;
                    pBalanceFamilia.patrimonio = pBalanceFamilia.totalactivo - pBalanceFamilia.totalpasivo;
                    pBalanceFamilia.totalpasivoypatrimonio = pBalanceFamilia.totalpasivo + pBalanceFamilia.patrimonio;

                    pBalanceFamilia = DABalanceFamilia.ModificarBalanceFamilia(pBalanceFamilia, pUsuario);

                    ts.Complete();
                }

                return pBalanceFamilia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceFamiliaBusiness", "ModificarBalanceFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un BalanceFamilia
        /// </summary>
        /// <param name="pId">Identificador de BalanceFamilia</param>
        public void EliminarBalanceFamilia(Int64 pId, Usuario pUsuario,Int64 Cod_persona,Int64 Cod_InfFin)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DABalanceFamilia.EliminarBalanceFamilia(pId, pUsuario, Cod_persona, Cod_InfFin);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceFamiliaBusiness", "EliminarBalanceFamilia", ex);
            }
        }

        /// <summary>
        /// Obtiene un BalanceFamilia
        /// </summary>
        /// <param name="pId">Identificador de BalanceFamilia</param>
        /// <returns>Entidad BalanceFamilia</returns>
        public BalanceFamilia ConsultarBalanceFamilia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DABalanceFamilia.ConsultarBalanceFamilia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceFamiliaBusiness", "ConsultarBalanceFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pBalanceFamilia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de BalanceFamilia obtenidos</returns>
        public List<BalanceFamilia> ListarBalanceFamilia(BalanceFamilia pBalanceFamilia, Usuario pUsuario)
        {
            try
            {
                return DABalanceFamilia.ListarBalanceFamilia(pBalanceFamilia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceFamiliaBusiness", "ListarBalanceFamilia", ex);
                return null;
            }
        }

    }
}