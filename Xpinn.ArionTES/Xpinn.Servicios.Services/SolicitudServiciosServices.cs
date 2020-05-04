using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Business;
using Xpinn.Util;

namespace Xpinn.Servicios.Services
{
    public class SolicitudServiciosServices
    {
        public string CodigoPrograma { get { return "80101"; } }        public string CodigoProgramaPlan { get { return "80110"; } }        public string CodigoProgramaActivacion { get { return "170807"; } }
        public string CodigoProgramaConfirmarSolicitudServicio { get { return "80117"; } }



        SolicitudServiciosBusiness BOServicios;
        ExcepcionBusiness BOExcepcion;

        public SolicitudServiciosServices()
        {
            BOServicios = new SolicitudServiciosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public Servicio CrearSolicitudServicio(Servicio pServicio, Usuario vUsuario)
        {
            try
            {
                return BOServicios.CrearSolicitudServicio(pServicio, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "CrearSolicitudServicio", ex);
                return null;
            }
        }
                
        public Servicio ModificarSolicitudServicio(Servicio pServicio, Usuario vUsuario)
        {
            try
            {
                return BOServicios.ModificarSolicitudServicio(pServicio, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "ModificarSolicitudServicio", ex);
                return null;
            }
        }



        public List<Servicio> ListarServicios(Servicio pServicio, DateTime pFechaIni, Usuario vUsuario, string filtro)
        {
            try
            {
                return BOServicios.ListarServicios(pServicio, pFechaIni, vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "ListarServicios", ex);
                return null;
            }
        }

        public Servicio ConsultarDatosPlanDePagos(Servicio servicio, Usuario usuario)
        {
            try
            {
                return BOServicios.ConsultarDatosPlanDePagos(servicio, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "ConsultarDatosPlanDePagos", ex);
                return null;
            }
        }

        public void EliminarServicio(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOServicios.EliminarServicio(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "EliminarServicio", ex);
            }
        }


        public Servicio ConsultarSERVICIO(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOServicios.ConsultarSERVICIO(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "ConsultarSERVICIO", ex);
                return null;
            }
        }

        public List<Servicio> ListarSolicitudServicio(string filtro, Usuario usuario)
        {
            try
            {
                return BOServicios.ListarSolicitudServicio(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "ListarSolicitudServicio", ex);
                return null;
            }
        }

        public bool ConsultarEstadoPersona(Int64? pCodPersona, string pIdentificacion, string pEstado, Usuario pUsuario)
        {
            try
            {
                return BOServicios.ConsultarEstadoPersona(pCodPersona, pIdentificacion, pEstado, pUsuario);

            }
            catch 
            {
                return false;
            }
        }

        public void EliminarDETALLESERVICIO(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOServicios.EliminarDETALLESERVICIO(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "EliminarDETALLESERVICIO", ex);
            }
        }


        public List<DetalleServicio> ConsultarDETALLESERVICIO(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOServicios.ConsultarDETALLESERVICIO(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "ConsultarDETALLESERVICIO", ex);
                return null;
            }
        }


        public List<Servicio> CargarPlanXLinea(Int64 pVar, Usuario vUsuario)
        {
            try
            {
                return BOServicios.CargarPlanXLinea(pVar, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "CargarPlanXLinea", ex);
                return null;
            }
        }

        public Servicio ConsultaProveedorXlinea(Int32 pVar, Usuario vUsuario)
        {
            try
            {
                return BOServicios.ConsultaProveedorXlinea(pVar, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "ConsultaProveedorXlinea", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOServicios.ObtenerSiguienteCodigo(pUsuario);
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "ObtenerSiguienteCodigo", ex);
                return 1;
            }
        }

        public int ConsultarNumeroServiciosPersona(string cod_persona, string cod_linea, Usuario usuario)
        {
            try
            {
                return BOServicios.ConsultarNumeroServiciosPersona(cod_persona, cod_linea, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "ConsultarNumeroServiciosPersona", ex);
                return 0;
            }
        }

        public ServicioEntity CrearServicioDesembolsoPorWS(Servicio pServicio, Xpinn.Tesoreria.Entities.Operacion pOperacion, int pTipo_servicio, decimal pVr_compra, decimal pVr_beneficio, decimal pVr_Mercado, int pCodProceso, Usuario vUsuario)
        {
            try
            {
                return BOServicios.CrearServicioDesembolsoPorWS(pServicio, pOperacion, pTipo_servicio, pVr_compra, pVr_beneficio, pVr_Mercado, pCodProceso, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "CrearServicioDesembolsoPorWS", ex);
                return null;
            }
        }

        public bool ModificarEstadoSolicitudServicio(Servicio solicitud, Usuario usuario)
        {
            try
            {
                return BOServicios.ModificarEstadoSolicitudservicio(solicitud, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ModificarEstadoSolicitudServicio", ex);
                return false;
            }
        }

        public Servicio CrearSolicitudServicioOficinaVirtual(Servicio pServicio, Usuario vUsuario)
        {
            try
            {
                return BOServicios.CrearSolicitudServicioOficinaVirtual(pServicio, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosServices", "CrearSolicitudServicioOficinaVirtual", ex);
                return null;
            }
        }

    }
}
