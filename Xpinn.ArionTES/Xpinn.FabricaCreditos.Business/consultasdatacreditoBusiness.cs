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
    /// Objeto de negocio para consultasdatacredito
    /// </summary>
    public class consultasdatacreditoBusiness : GlobalBusiness
    {
        private consultasdatacreditoData DAconsultasdatacredito;

        /// <summary>
        /// Constructor del objeto de negocio para consultasdatacredito
        /// </summary>
        public consultasdatacreditoBusiness()
        {
            DAconsultasdatacredito = new consultasdatacreditoData();
        }

        /// <summary>
        /// Crea un consultasdatacredito
        /// </summary>
        /// <param name="pconsultasdatacredito">Entidad consultasdatacredito</param>
        /// <returns>Entidad consultasdatacredito creada</returns>
        public consultasdatacredito Crearconsultasdatacredito(consultasdatacredito pconsultasdatacredito, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pconsultasdatacredito = DAconsultasdatacredito.Crearconsultasdatacredito(pconsultasdatacredito, pUsuario);

                    ts.Complete();
                }

                return pconsultasdatacredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("consultasdatacreditoBusiness", "Crearconsultasdatacredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un consultasdatacredito
        /// </summary>
        /// <param name="pconsultasdatacredito">Entidad consultasdatacredito</param>
        /// <returns>Entidad consultasdatacredito modificada</returns>
        public consultasdatacredito Modificarconsultasdatacredito(consultasdatacredito pconsultasdatacredito, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pconsultasdatacredito = DAconsultasdatacredito.Modificarconsultasdatacredito(pconsultasdatacredito, pUsuario);

                    ts.Complete();
                }

                return pconsultasdatacredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("consultasdatacreditoBusiness", "Modificarconsultasdatacredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un consultasdatacredito
        /// </summary>
        /// <param name="pId">Identificador de consultasdatacredito</param>
        public void Eliminarconsultasdatacredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAconsultasdatacredito.Eliminarconsultasdatacredito(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("consultasdatacreditoBusiness", "Eliminarconsultasdatacredito", ex);
            }
        }

        /// <summary>
        /// Obtiene un consultasdatacredito
        /// </summary>
        /// <param name="pId">Identificador de consultasdatacredito</param>
        /// <returns>Entidad consultasdatacredito</returns>
        public consultasdatacredito Consultarconsultasdatacredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAconsultasdatacredito.Consultarconsultasdatacredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("consultasdatacreditoBusiness", "Consultarconsultasdatacredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pconsultasdatacredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de consultasdatacredito obtenidos</returns>
        public List<consultasdatacredito> Listarconsultasdatacredito(consultasdatacredito pconsultasdatacredito, Usuario pUsuario)
        {
            try
            {
                return DAconsultasdatacredito.Listarconsultasdatacredito(pconsultasdatacredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("consultasdatacreditoBusiness", "Listarconsultasdatacredito", ex);
                return null;
            }
        }


        public List<CreditoEmpresaRecaudo> ListarPersona_Empresa_Recaudo(Int64 pIdPersona, Usuario vUsuario)
        {
            try
            {
                return DAconsultasdatacredito.ListarPersona_Empresa_Recaudo(pIdPersona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("consultasdatacreditoBusiness", "ListarPersona_Empresa_Recaudo", ex);
                return null;
            }
        }

    }
}