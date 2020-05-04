using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;
using System.Web;
using Xpinn.Util;


namespace Xpinn.Cartera.Services
{
    /// <summary>
    /// Servicio para Cajero
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ClasificacionCarteraService
    {
        private ClasificacionCarteraBusiness ClasificacionCarteraBusiness;
        private DatacreditoBusines BOExcepcion;
        private ExcepcionBusiness BOExcepcionE;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public ClasificacionCarteraService()
        {
            ClasificacionCarteraBusiness = new ClasificacionCarteraBusiness();
            BOExcepcion = new DatacreditoBusines();
            BOExcepcionE = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "60305"; } }
        public string CodigoProgramaParametros { get { return "60403"; } }
        public string CodigoProgramaComprobanteCausacion { get { return "60304"; } }
        public string CodigoProgramaComprobanteProvision { get { return "60306"; } }
        public string CodigoProgramaComprobanteProvisionGeneral { get { return "60309"; } }

        public List<ProvisionCartera> ListarConsolidadoProvision(string fecha, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ListarConsolidadoProvision(fecha, pUsuario);


            }
            catch
            { return null; }

        }
        public List<ProvisionCartera> ListarDetalleProvision(string fecha, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ListarDetalleProvision(fecha, pUsuario);


            }
            catch
            { return null; }

        }


        public List<ClasificacionCartera> ListarConsolidado(string fecha, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ListarConsolidado(fecha, pUsuario);


            }
            catch
            { return null; }

        }


        public string EliminarRegistroAbrirCierres(string fecha, string tipo, string estado, int anular_comprobante, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.EliminarRegistroAbrirCierres(fecha, tipo, estado, anular_comprobante, pUsuario);
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
                return ClasificacionCarteraBusiness.ListarDetalleAbrirCierre(fecha, tipo, pUsuario);
            }
            catch
            { return null; }
        }

        public List<ClasificacionCartera> ListarDetalle(string fecha, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ListarDetalle(fecha, pUsuario);
            }
            catch
            { return null; }
        }

        public List<CausacionCartera> ListarConsolidadoCausacion(string fecha, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ListarConsolidadoCausacion(fecha, pUsuario);
            }
            catch
            { return null; }
        }

        public List<CausacionCartera> ListarDetalleCausacion(string fecha, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ListarDetalleCausacion(fecha, pUsuario);
            }
            catch
            { return null; }
        }

        /// <summary>
        /// Servicio para obtener lista de la tabla clasificacion a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de calsificacion  obtenidos</returns>
        public List<ClasificacionCartera> ListarClasificacion(Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ListarClasificacion(pUsuario);
            }
            catch 
            {
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de la tabla clasificacion  
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de calsificacion  obtenidos</returns>
        public ClasificacionCartera ConsultarClasificacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ConsultarClasificacion(pId, pUsuario);
            }
            catch 
            {

                return null;
            }
        }



        /// <summary>
        /// Servicio para obtener lista de la tabla clasificacion a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de calsificacion  obtenidos</returns>
        public List<ClasificacionCartera> ListarDiasCategoria(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ListarDiasCategoria(pId, pUsuario);
            }
            catch 
            {
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de la tabla clasificacion a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de calsificacion  obtenidos</returns>
        public List<ClasificacionCartera> ListarCategorias(Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ListarCategorias(pUsuario);
            }
            catch 
            {
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de la tabla dias?categoria  
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de calsificacion  obtenidos</returns>
        public ClasificacionCartera ConsultarDiasCategoria(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ConsultarDiasCategoria(pId, pUsuario);
            }
            catch 
            {

                return null;
            }
        }


        /// <summary>
        /// Servicio para cear datos en tabla diascategorias
        /// </summary>
        /// <param name="pSolicitud">Entidad Solicitud</param>
        /// <returns>Entidad Solicitud modificada</returns>
        public ClasificacionCartera CrearCategorias(ClasificacionCartera pSolicitud, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.CrearCategorias(pSolicitud, pUsuario);
            }
            catch 
            {

                return null;
            }

        }

        /// <summary>
        /// Servicio para modificar  tabla diascategorias
        /// </summary>
        /// <param name="pSolicitud">Entidad Solicitud</param>
        /// <returns>Entidad Solicitud modificada</returns>
        public ClasificacionCartera ModificarCategorias(ClasificacionCartera pSolicitud, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ModificarCategorias(pSolicitud, pUsuario);
            }
            catch 
            {

                return null;
            }

        }

        public List<ClasificacionCartera> ListarCategoriasVencidas(Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ListarCategoriasVencidas(pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public GarantiasClasificacion CrearGarantiasClasificacion(GarantiasClasificacion pGarantiasClasificacion, Usuario pusuario)
        {
            try
            {
                pGarantiasClasificacion = ClasificacionCarteraBusiness.CrearGarantiasClasificacion(pGarantiasClasificacion, pusuario);
                return pGarantiasClasificacion;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public GarantiasClasificacion ModificarGarantiasClasificacion(GarantiasClasificacion pGarantiasClasificacion, Usuario pusuario)
        {
            try
            {
                pGarantiasClasificacion = ClasificacionCarteraBusiness.ModificarGarantiasClasificacion(pGarantiasClasificacion, pusuario);
                return pGarantiasClasificacion;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public void EliminarGarantiasClasificacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                ClasificacionCarteraBusiness.EliminarGarantiasClasificacion(pId, pusuario);
            }
            catch (Exception ex)
            {
                return;
            }
        }


        public GarantiasClasificacion ConsultarGarantiasClasificacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                GarantiasClasificacion GarantiasClasificacion = new GarantiasClasificacion();
                GarantiasClasificacion = ClasificacionCarteraBusiness.ConsultarGarantiasClasificacion(pId, pusuario);
                return GarantiasClasificacion;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<GarantiasClasificacion> ListarGarantiasClasificacion(GarantiasClasificacion pGarantiasClasificacion, Usuario pusuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ListarGarantiasClasificacion(pGarantiasClasificacion, pusuario);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Xpinn.Comun.Entities.Cierea ConsultarControlCierres(DateTime pFecha, String pTipo, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ConsultarControlCierres(pFecha, pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcionE.Throw("ClasificacionCarteraService", "ConsultarControlCierres", ex);
                return null;
            }
        }

        public List<String> ListarErroresParametrizacion(int pTipo, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ListarErroresParametrizacion(pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcionE.Throw("ClasificacionCarteraService", "ListarErroresParametrizacion", ex);
                return null;
            }
        }

        public List<String> ListarErroresParametrizacionClasif(int pTipo, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ListarErroresParametrizacionClasif(pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcionE.Throw("ClasificacionCarteraService", "ListarErroresParametrizacionClasif", ex);
                return null;
            }
        }


        public List<String> ListarErroresParametrizacionCausa(int pTipo, Usuario pUsuario)
        {
            try
            {
                return ClasificacionCarteraBusiness.ListarErroresParametrizacionCausa(pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcionE.Throw("ClasificacionCarteraService", "ListarErroresParametrizacionCausa", ex);
                return null;
            }
        }
    }

}

