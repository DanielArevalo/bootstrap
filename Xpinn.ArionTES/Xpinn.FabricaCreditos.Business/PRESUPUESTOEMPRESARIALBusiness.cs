using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

using System.Configuration;
using System.Data.Common;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para PresupuestoEmpresarial 
    /// </summary>
    public class PresupuestoEmpresarialBusiness : GlobalData
    {
        private PresupuestoEmpresarialData DAPresupuestoEmpresarial;

        /// <summary>
        /// Constructor del objeto de negocio para PresupuestoEmpresarial
        /// </summary>
        public PresupuestoEmpresarialBusiness()
        {
            DAPresupuestoEmpresarial = new PresupuestoEmpresarialData();
        }

        /// <summary>
        /// Crea un PresupuestoEmpresarial
        /// </summary>
        /// <param name="pPresupuestoEmpresarial">Entidad PresupuestoEmpresarial</param>
        /// <returns>Entidad PresupuestoEmpresarial creada</returns>
        public PresupuestoEmpresarial CrearPresupuestoEmpresarial(PresupuestoEmpresarial pPresupuestoEmpresarial, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPresupuestoEmpresarial = DAPresupuestoEmpresarial.CrearPresupuestoEmpresarial(pPresupuestoEmpresarial, pUsuario);

                    ts.Complete();
                }

                return pPresupuestoEmpresarial;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoEmpresarialBusiness", "CrearPresupuestoEmpresarial", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un PresupuestoEmpresarial
        /// </summary>
        /// <param name="pPresupuestoEmpresarial">Entidad PresupuestoEmpresarial</param>
        /// <returns>Entidad PresupuestoEmpresarial modificada</returns>
        public PresupuestoEmpresarial ModificarPresupuestoEmpresarial(PresupuestoEmpresarial pPresupuestoEmpresarial, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPresupuestoEmpresarial = DAPresupuestoEmpresarial.ModificarPresupuestoEmpresarial(pPresupuestoEmpresarial, pUsuario);

                    ts.Complete();
                }

                return pPresupuestoEmpresarial;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoEmpresarialBusiness", "ModificarPresupuestoEmpresarial", ex);
                return null;

                
            }
        }

        /// <summary>
        /// Elimina un PresupuestoEmpresarial
        /// </summary>
        /// <param name="pId">Identificador de PresupuestoEmpresarial</param>
        public void EliminarPresupuestoEmpresarial(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPresupuestoEmpresarial.EliminarPresupuestoEmpresarial(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoEmpresarialBusiness", "EliminarPresupuestoEmpresarial", ex);
            }
        }

        /// <summary>
        /// Obtiene un PresupuestoEmpresarial
        /// </summary>
        /// <param name="pId">Identificador de PresupuestoEmpresarial</param>
        /// <returns>Entidad PresupuestoEmpresarial</returns>
        public PresupuestoEmpresarial ConsultarPresupuestoEmpresarial(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAPresupuestoEmpresarial.ConsultarPresupuestoEmpresarial(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoEmpresarialBusiness", "ConsultarPresupuestoEmpresarial", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pPresupuestoEmpresarial">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PresupuestoEmpresarial obtenidos</returns>
        public List<PresupuestoEmpresarial> ListarPresupuestoEmpresarial(PresupuestoEmpresarial pPresupuestoEmpresarial, Usuario pUsuario)
        {
            try
            {
                return DAPresupuestoEmpresarial.ListarPresupuestoEmpresarial(pPresupuestoEmpresarial, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoEmpresarialBusiness", "ListarPresupuestoEmpresarial", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pPresupuestoEmpresarial">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PresupuestoEmpresarial obtenidos</returns>
        public List<PresupuestoEmpresarial> ListarPresupuestoEmpresarialREPO(PresupuestoEmpresarial pPresupuestoEmpresarial, Usuario pUsuario)
        {
            try
            {
                return DAPresupuestoEmpresarial.ListarPresupuestoEmpresarialREPO(pPresupuestoEmpresarial, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoEmpresarialBusiness", "ListarPresupuestoEmpresarialREPO", ex);
                return null;
            }
        }

    }
}