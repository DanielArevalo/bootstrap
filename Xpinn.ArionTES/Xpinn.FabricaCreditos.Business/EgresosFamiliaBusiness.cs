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
    /// Objeto de negocio para EgresosFamilia
    /// </summary>
    public class EgresosFamiliaBusiness : GlobalData
    {
        private EgresosFamiliaData DAEgresosFamilia;

        /// <summary>
        /// Constructor del objeto de negocio para EgresosFamilia
        /// </summary>
        public EgresosFamiliaBusiness()
        {
            DAEgresosFamilia = new EgresosFamiliaData();
        }

        /// <summary>
        /// Crea un EgresosFamilia
        /// </summary>
        /// <param name="pEgresosFamilia">Entidad EgresosFamilia</param>
        /// <returns>Entidad EgresosFamilia creada</returns>
        public EgresosFamilia CrearEgresosFamilia(EgresosFamilia pEgresosFamilia, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEgresosFamilia = DAEgresosFamilia.CrearEgresosFamilia(pEgresosFamilia, pUsuario);

                    ts.Complete();
                }

                return pEgresosFamilia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EgresosFamiliaBusiness", "CrearEgresosFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un EgresosFamilia
        /// </summary>
        /// <param name="pEgresosFamilia">Entidad EgresosFamilia</param>
        /// <returns>Entidad EgresosFamilia modificada</returns>
        public EgresosFamilia ModificarEgresosFamilia(EgresosFamilia pEgresosFamilia, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEgresosFamilia = DAEgresosFamilia.ModificarEgresosFamilia(pEgresosFamilia, pUsuario);

                    ts.Complete();
                }

                return pEgresosFamilia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EgresosFamiliaBusiness", "ModificarEgresosFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un EgresosFamilia
        /// </summary>
        /// <param name="pId">Identificador de EgresosFamilia</param>
        public void EliminarEgresosFamilia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEgresosFamilia.EliminarEgresosFamilia(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EgresosFamiliaBusiness", "EliminarEgresosFamilia", ex);
            }
        }

        /// <summary>
        /// Obtiene un EgresosFamilia
        /// </summary>
        /// <param name="pId">Identificador de EgresosFamilia</param>
        /// <returns>Entidad EgresosFamilia</returns>
        public EgresosFamilia ConsultarEgresosFamilia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAEgresosFamilia.ConsultarEgresosFamilia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EgresosFamiliaBusiness", "ConsultarEgresosFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pEgresosFamilia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EgresosFamilia obtenidos</returns>
        public List<EgresosFamilia> ListarEgresosFamilia(EgresosFamilia pEgresosFamilia, Usuario pUsuario)
        {
            try
            {
                return DAEgresosFamilia.ListarEgresosFamilia(pEgresosFamilia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EgresosFamiliaBusiness", "ListarEgresosFamilia", ex);
                return null;
            }
        }

    }
}