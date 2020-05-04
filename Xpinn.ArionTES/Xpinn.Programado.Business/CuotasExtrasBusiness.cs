using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Programado.Data;
using Xpinn.Programado.Entities;

namespace Xpinn.Programado.Business
{
    /// <summary>
    /// Objeto de negocio para ProgramadoCuotasExtras
    /// </summary>
    public class CuotasExtrasBusiness : GlobalData
    {
        private CuotasExtrasData DACuotasExtras;

        /// <summary>
        /// Constructor del objeto de negocio para ProgramadoCuotasExtras
        /// </summary>
        public CuotasExtrasBusiness()
        {
            DACuotasExtras = new CuotasExtrasData();
        }

        /// <summary>
        /// Crea un ProgramadoCuotasExtras
        /// </summary>
        /// <param name="pCuotasExtras">Entidad ProgramadoCuotasExtras</param>
        /// <returns>Entidad ProgramadoCuotasExtras creada</returns>
        public ProgramadoCuotasExtras CrearCuotasExtras(ProgramadoCuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCuotasExtras = DACuotasExtras.CrearCuotasExtras(pCuotasExtras, pUsuario);

                    ts.Complete();
                }

                return pCuotasExtras;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProgramadoCuotasExtrasBusiness", "CrearCuotasExtras", ex);
                return null;
            }
        }


       /// <summary>
        /// Obtiene un ProgramadoCuotasExtras
        /// </summary>
        /// <param name="pId">Identificador de ProgramadoCuotasExtras</param>
        /// <returns>Entidad ProgramadoCuotasExtras</returns>
        public ProgramadoCuotasExtras ConsultarCuotasExtras(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACuotasExtras.ConsultarCuotasExtras(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProgramadoCuotasExtrasBusiness", "ConsultarCuotasExtras", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCuotasExtras">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProgramadoCuotasExtras obtenidos</returns>
        public List<ProgramadoCuotasExtras> ListarCuotasExtras(ProgramadoCuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                return DACuotasExtras.ListarCuotasExtras(pCuotasExtras, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProgramadoCuotasExtrasBusiness", "ListarCuotasExtras", ex);
                return null;
            }
        }

    }
}