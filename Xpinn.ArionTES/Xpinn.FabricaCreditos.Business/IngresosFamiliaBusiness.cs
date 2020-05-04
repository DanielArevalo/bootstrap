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
    /// Objeto de negocio para IngresosFamilia
    /// </summary>
    public class IngresosFamiliaBusiness : GlobalData
    {
        private IngresosFamiliaData DAIngresosFamilia;

        /// <summary>
        /// Constructor del objeto de negocio para IngresosFamilia
        /// </summary>
        public IngresosFamiliaBusiness()
        {
            DAIngresosFamilia = new IngresosFamiliaData();
        }

        /// <summary>
        /// Crea un IngresosFamilia
        /// </summary>
        /// <param name="pIngresosFamilia">Entidad IngresosFamilia</param>
        /// <returns>Entidad IngresosFamilia creada</returns>
        public IngresosFamilia CrearIngresosFamilia(IngresosFamilia pIngresosFamilia, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pIngresosFamilia = DAIngresosFamilia.CrearIngresosFamilia(pIngresosFamilia, pUsuario);

                    ts.Complete();
                }

                return pIngresosFamilia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresosFamiliaBusiness", "CrearIngresosFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un IngresosFamilia
        /// </summary>
        /// <param name="pIngresosFamilia">Entidad IngresosFamilia</param>
        /// <returns>Entidad IngresosFamilia modificada</returns>
        public IngresosFamilia ModificarIngresosFamilia(IngresosFamilia pIngresosFamilia, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pIngresosFamilia = DAIngresosFamilia.ModificarIngresosFamilia(pIngresosFamilia, pUsuario);

                    ts.Complete();
                }

                return pIngresosFamilia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresosFamiliaBusiness", "ModificarIngresosFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un IngresosFamilia
        /// </summary>
        /// <param name="pId">Identificador de IngresosFamilia</param>
        public void EliminarIngresosFamilia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAIngresosFamilia.EliminarIngresosFamilia(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresosFamiliaBusiness", "EliminarIngresosFamilia", ex);
            }
        }

        /// <summary>
        /// Obtiene un IngresosFamilia
        /// </summary>
        /// <param name="pId">Identificador de IngresosFamilia</param>
        /// <returns>Entidad IngresosFamilia</returns>
        public IngresosFamilia ConsultarIngresosFamilia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAIngresosFamilia.ConsultarIngresosFamilia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresosFamiliaBusiness", "ConsultarIngresosFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pIngresosFamilia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de IngresosFamilia obtenidos</returns>
        public List<IngresosFamilia> ListarIngresosFamilia(IngresosFamilia pIngresosFamilia, Usuario pUsuario)
        {
            try
            {
                return DAIngresosFamilia.ListarIngresosFamilia(pIngresosFamilia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresosFamiliaBusiness", "ListarIngresosFamilia", ex);
                return null;
            }
        }

    }
}