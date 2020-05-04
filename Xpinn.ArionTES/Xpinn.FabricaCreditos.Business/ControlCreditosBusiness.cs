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
    /// Objeto de negocio para ControlCreditos
    /// </summary>
    public class ControlCreditosBusiness : GlobalBusiness
    {
        private ControlCreditosData DAControlCreditos;

        /// <summary>
        /// Constructor del objeto de negocio para ControlCreditos
        /// </summary>
        public ControlCreditosBusiness()
        {
            DAControlCreditos = new ControlCreditosData();
        }

        /// <summary>
        /// Crea un ControlCreditos
        /// </summary>
        /// <param name="pControlCreditos">Entidad ControlCreditos</param>
        /// <returns>Entidad ControlCreditos creada</returns>
        public ControlCreditos CrearControlCreditos(ControlCreditos pControlCreditos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pControlCreditos = DAControlCreditos.CrearControlCreditos(pControlCreditos, pUsuario);

                    ts.Complete();
                }

                return pControlCreditos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosBusiness", "CrearControlCreditos", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ControlCreditos
        /// </summary>
        /// <param name="pControlCreditos">Entidad ControlCreditos</param>
        /// <returns>Entidad ControlCreditos modificada</returns>
        public ControlCreditos ModificarControlCreditos(ControlCreditos pControlCreditos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pControlCreditos = DAControlCreditos.ModificarControlCreditos(pControlCreditos, pUsuario);

                    ts.Complete();
                }

                return pControlCreditos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosBusiness", "ModificarControlCreditos", ex);
                return null;
            }
        }
        
        /// <summary>
        /// Modifica un ControlCreditos
        /// </summary>
        /// <param name="pControlCreditos">Entidad ControlCreditos</param>
        /// <returns>Entidad ControlCreditos modificada</returns>
        public ControlCreditos Modificarfechadatcredito(ControlCreditos pControlCreditos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pControlCreditos = DAControlCreditos.Modificarfechadatcredito(pControlCreditos, pUsuario);

                    ts.Complete();
                }

                return pControlCreditos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosBusiness", "Modificarfechadatcredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ControlCreditos
        /// </summary>
        /// <param name="pId">Identificador de ControlCreditos</param>
        public void EliminarControlCreditos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAControlCreditos.EliminarControlCreditos(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosBusiness", "EliminarControlCreditos", ex);
            }
        }

        /// <summary>
        /// Obtiene un ControlCreditos
        /// </summary>
        /// <param name="pId">Identificador de ControlCreditos</param>
        /// <returns>Entidad ControlCreditos</returns>
        public ControlCreditos ConsultarControlCreditos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAControlCreditos.ConsultarControlCreditos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosBusiness", "ConsultarControlCreditos", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene un ControlCreditos
        /// </summary>
        /// <param name="pId">Identificador de ControlCreditos</param>
        /// <returns>Entidad ControlCreditos</returns>
        public ControlCreditos ConsultarControl_Procesos(Int64 pId,String radicacion, Usuario pUsuario)
        {
            try
            {
                return DAControlCreditos.ConsultarControl_Procesos(pId,radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosBusiness", "ConsultarControl_Procesos", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pControlCreditos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ControlCreditos obtenidos</returns>
        public List<ControlCreditos> ListarControlCreditos(ControlCreditos pControlCreditos, Usuario pUsuario)
        {
            try
            {
                return DAControlCreditos.ListarControlCreditos(pControlCreditos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosBusiness", "ListarControlCreditos", ex);
                return null;
            }
        }

        public string obtenerCodTipoProceso(string estado, Usuario pUsuario)
        {
            try
            {
                return DAControlCreditos.obtenerCodTipoProceso(estado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosBusiness", "obtenerCodTipoProceso", ex);
                return null;
            }
        }
    }
}