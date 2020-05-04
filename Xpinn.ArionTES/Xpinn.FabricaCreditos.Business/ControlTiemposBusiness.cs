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
    /// Objeto de negocio para ControlTiempos
    /// </summary>
    public class ControlTiemposBusiness : GlobalBusiness
    {
        private ControlTiemposData DAControlTiempos;

        /// <summary>
        /// Constructor del objeto de negocio para ControlTiempos
        /// </summary>
        public ControlTiemposBusiness()
        {
            DAControlTiempos = new ControlTiemposData();
        }

        /// <summary>
        /// Crea un ControlTiempos
        /// </summary>
        /// <param name="pControlTiempos">Entidad ControlTiempos</param>
        /// <returns>Entidad ControlTiempos creada</returns>
        public ControlTiempos CrearControlTiempos(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pControlTiempos = DAControlTiempos.CrearControlTiempos(pControlTiempos, pUsuario);

                    ts.Complete();
                }

                return pControlTiempos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlTiemposBusiness", "CrearControlTiempos", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ControlTiempos
        /// </summary>
        /// <param name="pControlTiempos">Entidad ControlTiempos</param>
        /// <returns>Entidad ControlTiempos modificada</returns>
        public ControlTiempos ModificarControlTiempos(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pControlTiempos = DAControlTiempos.ModificarControlTiempos(pControlTiempos, pUsuario);

                    ts.Complete();
                }

                return pControlTiempos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlTiemposBusiness", "ModificarControlTiempos", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ControlTiempos
        /// </summary>
        /// <param name="pId">Identificador de ControlTiempos</param>
        public void EliminarControlTiempos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAControlTiempos.EliminarControlTiempos(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlTiemposBusiness", "EliminarControlTiempos", ex);
            }
        }

        /// <summary>
        /// Obtiene un ControlTiempos
        /// </summary>
        /// <param name="pId">Identificador de ControlTiempos</param>
        /// <returns>Entidad ControlTiempos</returns>
        public ControlTiempos ConsultarControlTiempos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAControlTiempos.ConsultarControlTiempos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlTiemposBusiness", "ConsultarControlTiempos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pControlTiempos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ControlTiempos obtenidos</returns>
        public List<ControlTiempos> ListarControlTiempos(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            try
            {
                return DAControlTiempos.ListarControlTiempos(pControlTiempos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlTiemposBusiness", "ListarControlTiempos", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pControlTiempos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ControlTiempos obtenidos</returns>
        public List<ControlTiempos> ListarControlTiemposEfic(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            try
            {
                return DAControlTiempos.ListarControlTiemposEfic(pControlTiempos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlTiemposBusiness", "ListarControlTiemposEfic", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pControlTiempos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ControlTiempos obtenidos</returns>
        public List<ControlTiempos> ListarReporteMensajeria(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            try
            {
                return DAControlTiempos.ListarReporteMensajeria(pControlTiempos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlTiemposBusiness", "ListarReporteMensajeria", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene la lista de tipos documento dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos documento obtenidos</returns>
        public List<ControlTiempos> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return DAControlTiempos.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "ListarTiposDoc", ex);
                return null;
            }
        }


    }
}