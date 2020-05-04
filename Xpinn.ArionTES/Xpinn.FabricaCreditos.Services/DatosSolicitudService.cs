using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Solicitud
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DatosSolicitudService
    {
        private DatosSolicitudBusiness BOdatosSolicitud;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para DatosSolicitud
        /// </summary>
        public DatosSolicitudService()
        {
            BOdatosSolicitud = new DatosSolicitudBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "SEG010"; } }
        public string CodigoProgramaModificacion { get { return "100150"; } }
        public string CodigoProgramaRotativo { get { return "100507"; } }

        /// <summary>
        /// Crea una Solicitud
        /// </summary>
        /// <param name="pEntity">Entidad Solicitud</param>
        /// <returns>Entidad creada</returns>
        public DatosSolicitud CrearSolicitud(DatosSolicitud pSolicitud, Usuario pUsuario)
        {
            try
            {
                return BOdatosSolicitud.CrearSolicitud(pSolicitud, pUsuario); 
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "CrearSolicitud", ex);
                return null;
            }


        }


        public DatosSolicitud ModificarSolicitudes(DatosSolicitud pSolicitud, Usuario pUsuario)
        {
            try
            {
                return BOdatosSolicitud.ModificarSolicitudes(pSolicitud, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "ModificarSolicitudes", ex);
                return null;
            }


        }


     public DatosSolicitud CrearRadicadoRotativo(DatosSolicitud pSolicitud, Usuario pUsuario)
        {
            try
            {
                return BOdatosSolicitud.CrearRadicadoRotativo(pSolicitud, pUsuario); 
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "CrearRadicadoRotativo", ex);
                return null;
            }
        }

     public DatosSolicitud ModificarSolicitudRotativo(DatosSolicitud pSolicitud, Usuario pUsuario)
        {
            try
            {
                return BOdatosSolicitud.ModificarSolicitudRotativo(pSolicitud, pUsuario); 
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "ModificarSolicitudRotativo", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener Persona1 por parametros
        /// </summary>
        /// <param name="pId">identificador de Persona1</param>
        /// <returns>Entidad Persona1</returns>
        public DatosSolicitud ListarDatosSolicitud(DatosSolicitud pDatosSolicitud, Usuario pUsuario)
        {
            try
            {
                return BOdatosSolicitud.ListarDatosSolicitud(pDatosSolicitud, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "ConsultarPersona1", ex);
                return null;
            }
        }
       
        /// <summary>
        /// Servicio para obtener Persona1 por parametros
        /// </summary>
        /// <param name="pId">identificador de Persona1</param>
        /// <returns>Entidad Persona1</returns>
        public DatosSolicitud ListarSolicitudCrtlTiempos(DatosSolicitud pDatosSolicitud, Usuario pUsuario)
        {
            try
            {
                return BOdatosSolicitud.ListarSolicitudCrtlTiempos(pDatosSolicitud, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "ConsultarPersona1", ex);
                return null;
            }
        }

        public DatosSolicitud ValidarSolicitud(DatosSolicitud pSolicitud, Usuario pUsuario, ref string sError)
        {
            try
            {
                return BOdatosSolicitud.ValidarSolicitud(pSolicitud, pUsuario, ref sError);
            }
            catch
            {
                return pSolicitud;
            }
        }

        public DatosSolicitud ValidarSolicitudRotativo(DatosSolicitud pSolicitud, Usuario pUsuario, ref string sError)
        {
            try
            {
                return BOdatosSolicitud.ValidarSolicitudRotativo(pSolicitud, pUsuario, ref sError);
            }
            catch
            {
                return pSolicitud;
            }
        }
        /// <summary>
        /// Servicio para obtener Aporte
        /// </summary>
        /// <param name="pId">identificador de Aporte</param>
        /// <returns>Entidad Cliente</returns>
        public DatosSolicitud ConsultarCliente(String pId, Usuario pUsuario)
        {
            try
            {
                return BOdatosSolicitud.ConsultarCliente(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosSolicitudService", "ConsultarCliente", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener de lata tab;la generar parametro edad minima y maxima
        /// </summary>
        /// <param name="pId">identificador de Aporte</param>
        /// <returns>Entidad Cliente</returns>
        public DatosSolicitud ConsultarParametroEdadMinima(Usuario pUsuario)
        {
            try
            {
                return BOdatosSolicitud.ConsultarParametroEdadMinima(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosSolicitudService", "ConsultarParametroEdadMinima", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener de lata tab;la generar parametro edad minima y maxima
        /// </summary>
        /// <param name="pId">identificador de Aporte</param>
        /// <returns>Entidad Cliente</returns>
        public DatosSolicitud ConsultarParametroEdadMaxima(Usuario pUsuario)
        {
            try
            {
                return BOdatosSolicitud.ConsultarParametroEdadMaxima(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosSolicitudService", "ConsultarParametroEdadMaxima", ex);
                return null;
            }
        }


        public DatosSolicitud ValidarCliente(DatosSolicitud pSolicitud, Usuario pUsuario, ref string sError)
        {
            try
            {
                return BOdatosSolicitud.ValidarCliente(pSolicitud, pUsuario, ref sError);
            }
            catch
            {
                return pSolicitud;
            }
        }
        /// <summary>
        /// Servicio para obtener Persona1 por parametros
        /// </summary>
        /// <param name="pId">identificador de Persona1</param>
        /// <returns>Entidad Persona1</returns>
        public List<DatosSolicitud> ListarLineasCredito(Usuario pUsuario)
        {
            try
            {
                return BOdatosSolicitud.ListarLineasCredito(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "ListarLineasCredito", ex);
                return null;
            }
        }


        public DatosSolicitud ConsultarSolicitudCreditos(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOdatosSolicitud.ConsultarSolicitudCreditos(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "ConsultarSolicitudCreditos", ex);
                return null;
            }
        }

        public int ConsultarCreditosActivosXLinea(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOdatosSolicitud.ConsultarCreditosActivosXLinea(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosSolicitudService", "ConsultarCreditosActivosXLinea", ex);
                return 0;
            }
        }

        public int? ConsultarCreditosPermitidosXLinea(string cod_linea, Usuario vUsuario)
        {
            try
            {
                return BOdatosSolicitud.ConsultarCreditosPermitidosXLinea(cod_linea, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosSolicitudService", "ConsultarCreditosPermitidosXLinea", ex);
                return null;
            }
        }


        public string  ConsultaValorGarantia(string num_radica, Usuario pUsuario)
        {
            try
            {
                return BOdatosSolicitud.ConsultaValorGarantia(num_radica, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "ConsultaValorGarantia", ex);
                return null;
            }
        }



    }
}
