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
    /// Objeto de negocio para SolicitudCreditosRecogidos
    /// </summary>
    public class SolicitudCreditosRecogidosBusiness : GlobalBusiness
    {
        private SolicitudCreditosRecogidosData DASolicitudCreditosRecogidos;

        /// <summary>
        /// Constructor del objeto de negocio para SolicitudCreditosRecogidos
        /// </summary>
        public SolicitudCreditosRecogidosBusiness()
        {
            DASolicitudCreditosRecogidos = new SolicitudCreditosRecogidosData();
        }

        /// <summary>
        /// Crea un SolicitudCreditosRecogidos
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad SolicitudCreditosRecogidos</param>
        /// <returns>Entidad SolicitudCreditosRecogidos creada</returns>
        public SolicitudRecogidoAvance CrearSolicitudCreditosRecogidos(SolicitudRecogidoAvance pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSolicitudCreditosRecogidos = DASolicitudCreditosRecogidos.CrearSolicitudCreditosRecogidos(pSolicitudCreditosRecogidos, pUsuario);

                    ts.Complete();
                }

                return pSolicitudCreditosRecogidos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosBusiness", "CrearSolicitudCreditosRecogidos", ex);
                return null;
            }
        }



        public SolicitudCreditosRecogidos CrearSolicitudCreditosRecogidos(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSolicitudCreditosRecogidos = DASolicitudCreditosRecogidos.CrearSolicitudCreditosRecogidos(pSolicitudCreditosRecogidos, pUsuario);

                    ts.Complete();
                }

                return pSolicitudCreditosRecogidos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosBusiness", "CrearSolicitudCreditosRecogidos", ex);
                return null;
            }
        }
        /// <summary>
        /// Modifica un SolicitudCreditosRecogidos
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad SolicitudCreditosRecogidos</param>
        /// <returns>Entidad SolicitudCreditosRecogidos modificada</returns>
        public SolicitudCreditosRecogidos ModificarSolicitudCreditosRecogidos(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSolicitudCreditosRecogidos = DASolicitudCreditosRecogidos.ModificarSolicitudCreditosRecogidos(pSolicitudCreditosRecogidos, pUsuario);

                    ts.Complete();
                }

                return pSolicitudCreditosRecogidos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosBusiness", "ModificarSolicitudCreditosRecogidos", ex);
                return null;
            }
        }





        /// <summary>
        /// Enviar Parametros al sp usp_xpinn_solicred_recoger
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad SolicitudCreditosRecogidos</param>
        /// <returns>Entidad SolicitudCreditosRecogidos modificada</returns>
        public SolicitudCreditosRecogidos ParametrosSolicredRecoger(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSolicitudCreditosRecogidos = DASolicitudCreditosRecogidos.ParametrosSolicredRecoger(pSolicitudCreditosRecogidos, pUsuario);

                    ts.Complete();
                }

                return pSolicitudCreditosRecogidos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosBusiness", "ModificarSolicitudCreditosRecogidos", ex);
                return null;
            }
        }






        /// <summary>
        /// Elimina un SolicitudCreditosRecogidos
        /// </summary>
        /// <param name="pId">Identificador de SolicitudCreditosRecogidos</param>
        public void EliminarSolicitudCreditosRecogidos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DASolicitudCreditosRecogidos.EliminarSolicitudCreditosRecogidos(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosBusiness", "EliminarSolicitudCreditosRecogidos", ex);
            }
        }

        /// <summary>
        /// Obtiene un SolicitudCreditosRecogidos
        /// </summary>
        /// <param name="pId">Identificador de SolicitudCreditosRecogidos</param>
        /// <returns>Entidad SolicitudCreditosRecogidos</returns>
        public SolicitudCreditosRecogidos ConsultarSolicitudCreditosRecogidos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DASolicitudCreditosRecogidos.ConsultarSolicitudCreditosRecogidos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosBusiness", "ConsultarSolicitudCreditosRecogidos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SolicitudCreditosRecogidos obtenidos</returns>
        public List<SolicitudCreditosRecogidos> ListarSolicitudCreditosRecogidos(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            try
            {
                return DASolicitudCreditosRecogidos.ListarSolicitudCreditosRecogidos(pSolicitudCreditosRecogidos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosBusiness", "ListarSolicitudCreditosRecogidos", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SolicitudCreditosRecogidos obtenidos</returns>
        public List<SolicitudCreditosRecogidos> ListarSolicitudCreditosRecogidos(Usuario pUsuario)
        {
            try
            {
                return DASolicitudCreditosRecogidos.ListarSolicitudCreditosRecogidos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosBusiness", "ListarSolicitudCreditosRecogidos", ex);
                return null;
            }
        }


        /// <summary>
        /// Mètodo para listar los crèditos recogidos
        /// </summary>
        /// <param name="pNumeroRadicacion"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<SolicitudCreditosRecogidos> ListarCreditosRecogidos(Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            try
            {
                return DASolicitudCreditosRecogidos.ListarCreditosRecogidos(pNumeroRadicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosBusiness", "ListarCreditosRecogidos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SolicitudCreditosRecogidos obtenidos</returns>
        public List<SolicitudCreditosRecogidos> ListarTemp_recoger(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            try
            {
                return DASolicitudCreditosRecogidos.ListarTemp_recoger(pSolicitudCreditosRecogidos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosBusiness", "ListarSolicitudCreditosRecogidos", ex);
                return null;
            }
        }


        //listado solicitudes de creditos
        public List<SolicitudCreditoAAC> ListarSolicitudCreditoAAC(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DASolicitudCreditosRecogidos.ListarSolicitudCreditoAAC(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosBusiness", "ListarSolicitudCreditoAAC", ex);
                return null;
            }
        }

        public void EliminarSolicitudCreditoAAC(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DASolicitudCreditosRecogidos.EliminarSolicitudCreditoAAC(pId, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosBusiness", "ListarSolicitudCreditoAAC", ex);
            }
        }

        public Boolean ConfirmacionSolicitudCredito(List<SolicitudCreditoAAC> lstSolicitud, ref string pError, ref List<Credito> lstGenerados, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Int64? pNumero_radicacion;
                    Credito pEntidad;
                    foreach (SolicitudCreditoAAC nSolicitud in lstSolicitud)
                    {
                        pNumero_radicacion = DASolicitudCreditosRecogidos.ConfirmacionSolicitudCredito(nSolicitud, ref pError, pUsuario);
                        pEntidad = new Credito();
                        pEntidad.numero_radicacion = Convert.ToInt64(pNumero_radicacion);
                        pEntidad.numero_obligacion = nSolicitud.numerosolicitud.ToString();
                        pEntidad.cod_persona = Convert.ToInt64(nSolicitud.cod_persona);
                        pEntidad.descrpcion = pError;
                        lstGenerados.Add(pEntidad);
                    }
                    ts.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return false;
            }
        }

    }
}