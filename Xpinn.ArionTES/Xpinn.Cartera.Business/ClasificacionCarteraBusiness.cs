using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
using System.Web;


namespace Xpinn.Cartera.Business
{
    public class ClasificacionCarteraBusiness : GlobalData
    {

        private ClasificacionCarteraData DAClasificacionCartera;
        private GarantiasClasificacionData DAGarantiasClasificacion;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public ClasificacionCarteraBusiness()
        {
            DAClasificacionCartera = new ClasificacionCarteraData();
            DAGarantiasClasificacion = new GarantiasClasificacionData();
        }

        public List<ProvisionCartera> ListarConsolidadoProvision(string fecha, Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ListarConsolidadoProvision(fecha, pUsuario);
            }
            catch
            { return null; }

        }

        public List<ProvisionCartera> ListarDetalleProvision(string fecha, Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ListarDetalleProvision(fecha, pUsuario);
            }
            catch
            { return null; }

        }

        public string EliminarRegistroAbrirCierres(string fecha, string tipo, string estado, int anular_comprobante, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    string error = "";
                    error =  DAClasificacionCartera.EliminarRegistroAbrirCierres(fecha, tipo, estado, anular_comprobante, pUsuario);
                    ts.Complete();
                    return error;
                }
            }
            catch
            {
                return null;
            }
        }



        public List<ClasificacionCartera> ListarDetalleAbrirCierre(string fecha, string tipo, Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ListarDetalleAbrirCierre(fecha, tipo, pUsuario);
            }
            catch
            { return null; }
        }

        public List<ClasificacionCartera> ListarConsolidado(string fecha, Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ListarConsolidado(fecha, pUsuario);
            }
            catch
            { return null; }

        }

        public List<ClasificacionCartera> ListarDetalle(string fecha, Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ListarDetalle(fecha, pUsuario);
            }
            catch
            { return null; }
        }

        public List<CausacionCartera> ListarConsolidadoCausacion(string fecha, Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ListarConsolidadoCausacion(fecha, pUsuario);
            }
            catch
            { return null; }
        }

        public List<CausacionCartera> ListarDetalleCausacion(string fecha, Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ListarDetalleCausacion(fecha, pUsuario);
            }
            catch
            { return null; }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pclasificacion">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de clasificacion obtenidos</returns>
        public List<ClasificacionCartera> ListarClasificacion(Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ListarClasificacion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionCarteraBusiness", "ListarClasificacion", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pclasificacion">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de clasificacion obtenidos</returns>
        public List<ClasificacionCartera> ListarDiasCategoria(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ListarDiasCategoria(pId,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionCarteraBusiness", "ListarDiasCategoria", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pclasificacion">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de clasificacion obtenidos</returns>
        public List<ClasificacionCartera> ListarCategorias(Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ListarCategorias(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionCarteraBusiness", "ListarCategorias", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pclasificacion">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de clasificacion obtenidos</returns>
        public ClasificacionCartera ConsultarClasificacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ConsultarClasificacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionCarteraBusiness", "ConsultarClasificacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pclasificacion">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de clasificacion obtenidos</returns>
        public ClasificacionCartera ConsultarDiasCategoria(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ConsultarDiasCategoria(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionCarteraBusiness", "ConsultarDiasCategoria", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un dao de la tabla dias_categorias
        /// </summary>
        /// <param name="pSolicitud">Entidad creada</param>
        /// <returns>Entidad Solicitud creada</returns>
        public ClasificacionCartera CrearCategorias(ClasificacionCartera pSolicitud, Usuario pUsuario)
        {
            try
            {
                // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                pSolicitud = DAClasificacionCartera.CrearCategorias(pSolicitud, pUsuario);

                // ts.Complete();
                // }

                return pSolicitud;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionCarteraBusiness", "CrearCategorias", ex);
                return null;
            }
        }



        /// <summary>
        /// Modifica un dao de la tabla dias_categorias
        /// </summary>
        /// <param name="pSolicitud">Entidad creada</param>
        /// <returns>Entidad Solicitud creada</returns>
        public ClasificacionCartera ModificarCategorias(ClasificacionCartera pSolicitud, Usuario pUsuario)
        {
            try
            {
                // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                pSolicitud = DAClasificacionCartera.ModificarCategorias(pSolicitud, pUsuario);

                // ts.Complete();
                // }

                return pSolicitud;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionCarteraBusiness", "ModificarCategorias", ex);
                return null;
            }
        }

        public List<ClasificacionCartera> ListarCategoriasVencidas(Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ListarCategoriasVencidas(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClasificacionCarteraBusiness", "ListarCategoriasVencidas", ex);
                return null;
            }
        }


        public GarantiasClasificacion CrearGarantiasClasificacion(GarantiasClasificacion pGarantiasClasificacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pGarantiasClasificacion = DAGarantiasClasificacion.CrearGarantiasClasificacion(pGarantiasClasificacion, pusuario);

                    ts.Complete();

                }

                return pGarantiasClasificacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasClasificacionBusiness", "CrearGarantiasClasificacion", ex);
                return null;
            }
        }


        public GarantiasClasificacion ModificarGarantiasClasificacion(GarantiasClasificacion pGarantiasClasificacion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pGarantiasClasificacion = DAGarantiasClasificacion.ModificarGarantiasClasificacion(pGarantiasClasificacion, pusuario);

                    ts.Complete();

                }

                return pGarantiasClasificacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasClasificacionBusiness", "ModificarGarantiasClasificacion", ex);
                return null;
            }
        }


        public void EliminarGarantiasClasificacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAGarantiasClasificacion.EliminarGarantiasClasificacion(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasClasificacionBusiness", "EliminarGarantiasClasificacion", ex);
            }
        }


        public GarantiasClasificacion ConsultarGarantiasClasificacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                GarantiasClasificacion GarantiasClasificacion = new GarantiasClasificacion();
                GarantiasClasificacion = DAGarantiasClasificacion.ConsultarGarantiasClasificacion(pId, pusuario);
                return GarantiasClasificacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasClasificacionBusiness", "ConsultarGarantiasClasificacion", ex);
                return null;
            }
        }


        public List<GarantiasClasificacion> ListarGarantiasClasificacion(GarantiasClasificacion pGarantiasClasificacion, Usuario pusuario)
        {
            try
            {
                return DAGarantiasClasificacion.ListarGarantiasClasificacion(pGarantiasClasificacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasClasificacionBusiness", "ListarGarantiasClasificacion", ex);
                return null;
            }
        }

        public Xpinn.Comun.Entities.Cierea ConsultarControlCierres(DateTime pFecha, String pTipo, Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ConsultarControlCierres(pFecha, pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasClasificacionBusiness", "ConsultarControlCierres", ex);
                return null;
            }
        }

        public List<String> ListarErroresParametrizacion(int pTipo, Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ListarErroresParametrizacion(pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasClasificacionBusiness", "ListarErroresParametrizacion", ex);
                return null;
            }
        }

        public List<String> ListarErroresParametrizacionClasif(int pTipo, Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ListarErroresParametrizacionClasif(pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasClasificacionBusiness", "ListarErroresParametrizacionClasif", ex);
                return null;
            }
        }

        public List<String> ListarErroresParametrizacionCausa(int pTipo, Usuario pUsuario)
        {
            try
            {
                return DAClasificacionCartera.ListarErroresParametrizacionCausa(pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasClasificacionBusiness", "ListarErroresParametrizacionCausa", ex);
                return null;
            }
        }

    }
}
