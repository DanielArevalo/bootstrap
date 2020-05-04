using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para Consignacion
    /// </summary>
    public class ConsignacionBusiness : GlobalData
    {
        private ConsignacionData DAConsignacion;

        /// <summary>
        /// Constructor del objeto de negocio para Consignacion
        /// </summary>
        public ConsignacionBusiness()
        {
            DAConsignacion = new ConsignacionData();
        }

        /// <summary>
        /// Crea un Consignacion
        /// </summary>
        /// <param name="pConsignacion">Entidad Consignacion</param>
        /// <returns>Entidad Consignacion creada</returns>
        public Consignacion CrearConsignacionTesoreria(Consignacion pConsignacion, GridView gvConsignacion,ref Int64 pCOD_OPE, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConsignacion = DAConsignacion.CrearConsignacionTesoreria(pConsignacion, gvConsignacion,ref pCOD_OPE, pUsuario);

                    ts.Complete();
                }

                return pConsignacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionBusiness", "CrearConsignacionTesoreria", ex);
                return null;
            }
        }


        /// <summary>
        /// Crea un Consignacion
        /// </summary>
        /// <param name="pConsignacion">Entidad Consignacion</param>
        /// <returns>Entidad Consignacion creada</returns>
        public Consignacion CrearConsignacion(Consignacion pConsignacion, GridView gvConsignacion, Usuario pUsuario)
        {
            Int64 consignacion = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConsignacion = DAConsignacion.CrearConsignacion(pConsignacion, gvConsignacion, pUsuario);

                    ts.Complete();
                }

                return pConsignacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionBusiness", "CrearConsignacion", ex);
                return null;
            }
        }
        public Consignacion CrearConsignacionCheque(Consignacion pConsignacion, GridView gvConsignacion, Usuario pUsuario)
        {
            Int64 consignacion = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConsignacion = DAConsignacion.CrearConsignacionCheque(pConsignacion, gvConsignacion, pUsuario);

                    ts.Complete();
                }

                return pConsignacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionBusiness", "CrearConsignacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Consignacion
        /// </summary>
        /// <param name="pConsignacion">Entidad Consignacion</param>
        /// <returns>Entidad Consignacion modificada</returns>
        public Consignacion ModificarConsignacion(Consignacion pConsignacion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConsignacion = DAConsignacion.ModificarConsignacion(pConsignacion, pUsuario);

                    ts.Complete();
                }

                return pConsignacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionBusiness", "ModificarConsignacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Consignacion
        /// </summary>
        /// <param name="pId">Identificador de Consignacion</param>
        public void EliminarConsignacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAConsignacion.EliminarConsignacion(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionBusiness", "EliminarConsignacion", ex);
            }
        }

        /// <summary>
        /// Obtiene un Consignacion
        /// </summary>
        /// <param name="pId">Identificador de Consignacion</param>
        /// <returns>Entidad Consignacion</returns>
        public Consignacion ConsultarConsignacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAConsignacion.ConsultarConsignacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionBusiness", "ConsultarConsignacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pConsignacion">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Consignacion obtenidos</returns>
        public List<Consignacion> ListarConsignacion(Consignacion pConsignacion, Usuario pUsuario)
        {
            try
            {
                return DAConsignacion.ListarConsignacion(pConsignacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionBusiness", "ListarConsignacion", ex);
                return null;
            }
        }

        public List<Consignacion> ListarConsignacionCheque(Int64 pConsignacion, Usuario pUsuario)
        {
            try
            {
                return DAConsignacion.ListarConsignacionCheque(pConsignacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionBusiness", "ListarConsignacionCheque", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene una Traslado
        /// </summary>
        /// <param name="pId">identificador del Traslado</param>
        /// <returns>Traslado consultada</returns>
        public Consignacion ConsultarCajero(Usuario pUsuario)
        {
            try
            {
                return DAConsignacion.ConsultarCajero(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionBusiness", "ConsultarCajero", ex);
                return null;
            }
        }

        public Consignacion ConsultarUsuario(Usuario pUsuario)
        {
            try
            {
                return DAConsignacion.ConsultarUsuario(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionBusiness", "ConsultarUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica una Consignacion Cheque
        /// </summary>
        /// <param name="pConsignacionCheque">Entidad Consignacion Cheque</param>
        /// <returns>Entidad ConsignacionCheque modificada</returns>
        public Consignacion GrabarCanje(Consignacion pConsignacionCheque,MotivoDevChe pMotivoDevChe, Usuario pUsuario)
        {
            
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                   
                    pConsignacionCheque = DAConsignacion.GrabarCanje(pConsignacionCheque, pMotivoDevChe, pUsuario);

                    ts.Complete();
                }

                return pConsignacionCheque;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionBusiness", "ModificarConsignacion", ex);
                return null;
            }
        }


    }
}