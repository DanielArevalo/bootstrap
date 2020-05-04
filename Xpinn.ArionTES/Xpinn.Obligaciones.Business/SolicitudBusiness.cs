using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Obligaciones.Data;
using Xpinn.Obligaciones.Entities;
using System.Web.UI.WebControls;

namespace Xpinn.Obligaciones.Business
{
    /// <summary>
    /// Objeto de negocio para Solicitud
    /// </summary>
    public class SolicitudBusiness : GlobalBusiness
    {
        private SolicitudData DASolicitud;

        /// <summary>
        /// Constructor del objeto de negocio para Solicitud
        /// </summary>
        public SolicitudBusiness()
        {
            DASolicitud = new SolicitudData();
        }

        /// <summary>
        /// Crea un Solicitud
        /// </summary>
        /// <param name="pSolicitud">Entidad Solicitud</param>
        /// <returns>Entidad Solicitud creada</returns>
        public Solicitud CrearSolicitud(Solicitud pSolicitud, DataTable dtComp, DataTable dtPagos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSolicitud = DASolicitud.CrearSolicitud(pSolicitud, dtComp, dtPagos, pUsuario);

                    ts.Complete();
                }

                return pSolicitud;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudBusiness", "CrearSolicitud", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Solicitud
        /// </summary>
        /// <param name="pSolicitud">Entidad Solicitud</param>
        /// <returns>Entidad Solicitud modificada</returns>
        public Solicitud ModificarSolicitud(Xpinn.Obligaciones.Entities.ObligacionCredito pOperacion, Solicitud pSolicitud, DataTable dtComp, DataTable dtPagos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                 
                    if (pOperacion != null)
                    {
                        pSolicitud = DASolicitud.ModificarSolicitud(pOperacion, pSolicitud, dtComp, dtPagos, pUsuario);
                    }

                    ts.Complete();
                }

                return pSolicitud;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudBusiness", "ModificarSolicitud", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Solicitud
        /// </summary>
        /// <param name="pId">Identificador de Solicitud</param>
        public void EliminarSolicitud(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DASolicitud.EliminarSolicitud(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudBusiness", "EliminarSolicitud", ex);
            }
        }

        /// <summary>
        /// Obtiene un Solicitud
        /// </summary>
        /// <param name="pId">Identificador de Solicitud</param>
        /// <returns>Entidad Solicitud</returns>
        public Solicitud ConsultarSolicitud(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DASolicitud.ConsultarSolicitud(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudBusiness", "ConsultarSolicitud", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Solicitud
        /// </summary>
        /// <param name="pId">Identificador de Solicitud</param>
        /// <returns>Entidad Solicitud</returns>
        public Solicitud ConsultarEstadoCuenta(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DASolicitud.ConsultarEstadoCuenta(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudBusiness", "ConsultarEstadoCuenta", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Solicitud obtenidos</returns>
        public List<Solicitud> ListarSolicitud(Solicitud pSolicitud, Usuario pUsuario)
        {
            try
            {
                return DASolicitud.ListarSolicitud(pSolicitud, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudBusiness", "ListarSolicitud", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un Solicitud
        /// </summary>
        /// <param name="pId">Identificador del tercero</param>
        /// <returns>Entidad Solicitud</returns>
        public string Consultar_Tercero(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DASolicitud.Consultar_Tercero(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudBusiness", "Consultar_Tercero", ex);
                return null;
            }
        }

    }
}