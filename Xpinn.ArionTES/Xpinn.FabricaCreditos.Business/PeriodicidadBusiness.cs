using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    public class PeriodicidadBusiness : GlobalData
    {
        private PeriodicidadData DAPeriodicidad;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public PeriodicidadBusiness()
        {
            DAPeriodicidad = new PeriodicidadData();
        }


        /// <summary>
        /// Crea un Periodicidad
        /// </summary>
        /// <param name="pTipoComprobante">Entidad Periodicidad</param>
        /// <returns>Entidad Periodicidad creada</returns>
        public Periodicidad CrearPeriodicidad(Periodicidad pPeriodicidad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPeriodicidad = DAPeriodicidad.CrearPeriodicidad(pPeriodicidad, pUsuario);

                    ts.Complete();
                }

                return pPeriodicidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadBusiness", "CrearPeriodicidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Periodicidad
        /// </summary>
        /// <param name="pTipoComprobante">Entidad Periodicidad</param>
        /// <returns>Entidad Periodicidad modificada</returns>
        public Periodicidad ModificarPeriodicidad(Periodicidad pPeriodicidad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPeriodicidad = DAPeriodicidad.ModificarPeriodicidad(pPeriodicidad, pUsuario);

                    ts.Complete();
                }

                return pPeriodicidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadBusiness", "ModificarPeriodicidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Periodicidad
        /// </summary>
        /// <param name="pId">Identificador de Periodicidad</param>
        public void EliminarPeriodicidad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPeriodicidad.EliminarPeriodicidad(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadBusiness", "EliminarPeriodicidad", ex);
            }
        }

        /// <summary>
        /// Obtiene un Periodicidad
        /// </summary>
        /// <param name="pId">Identificador de Periodicidad</param>
        /// <returns>Entidad Periodicidad</returns>
        public Periodicidad ConsultarPeriodicidad(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Periodicidad Periodicidad = new Periodicidad();

                Periodicidad = DAPeriodicidad.ConsultarPeriodicidad(pId, vUsuario);

                return Periodicidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadBusiness", "ConsultarPeriodicidad", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de anexos
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de anexos obtenidos</returns>
        public List<Periodicidad> ListarPeriodicidad(Periodicidad pPeriodicidad, Usuario pUsuario)
        {
            try
            {
                return DAPeriodicidad.ListarPeriodicidad(pPeriodicidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadBusiness", "ListarPeriodicidad", ex);
                return null;
            }
        }
        public List<CreditoSolicitado> ListarTipoTasa( Usuario pUsuario)
        {
            try
            {
                return DAPeriodicidad.ListarTipoTasa( pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadService", "ListarPeriodicidad", ex);
                return null;
            }
        }
    }
}
