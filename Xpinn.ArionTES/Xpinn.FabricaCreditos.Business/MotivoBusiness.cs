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
    public class MotivoBusiness : GlobalData
    {
        private MotivoData DAMotivoNegacion;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public MotivoBusiness()
        {
            DAMotivoNegacion = new MotivoData();
        }

        /// <summary>
        /// Crea un motivo de negacion
        /// </summary>
        /// <param name="pEntity">Entidad motivonegacion</param>
        /// <returns>Entidad creada</returns>
        public Motivo CrearMotivo(Motivo pMotivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMotivo = DAMotivoNegacion.InsertarMotivo(pMotivo, pUsuario);
                    ts.Complete();
                }
                return pMotivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoBusiness", "CrearMotivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de motivos de negacion
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<Motivo> ListarMotivos(Motivo pMotivo, Usuario pUsuario)
        {
            try
            {
                return DAMotivoNegacion.ListarMotivos(pMotivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoBusiness", "ListarMotivos", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un motivo de negacion
        /// </summary>
        /// <param name="pId">identificador del motivo</param>
        public void EliminarMotivo(Int32 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DAMotivoNegacion.EliminarMotivo(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoBusiness", "EliminarMotivo", ex);
            }
        }

        /// <summary>
        /// Obtiene la lista de motivos de negacion
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<Motivo> ListarMotivosFiltro(Motivo pMotivo, Usuario pUsuario, int filtro)
        {
            try
            {
                return DAMotivoNegacion.ListarMotivosFiltro(pMotivo, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoBusiness", "ListarMotivosFiltro", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de motivos de retiro
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de motivos de retiro obtenidos</returns>
        public List<Motivo> ListarMotivosRetiro(Motivo pMotivo, Usuario pUsuario)
        {
            try
            {
                return DAMotivoNegacion.ListarMotivosRetiro(pMotivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoBusiness", "ListarMotivos", ex);
                return null;
            }
        }
    }
}
