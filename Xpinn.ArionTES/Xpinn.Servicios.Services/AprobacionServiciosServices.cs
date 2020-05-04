using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Business;
using Xpinn.Util;

namespace Xpinn.Servicios.Services
{
    public class AprobacionServiciosServices
    {
        public string CodigoPrograma { get { return "80102"; } }
        public string CodigoProgramaDesem { get { return "80103"; } }
        public string CodigoProgramaModifi { get { return "80104"; } }
        public string CodigoProgramaCarga { get { return "80105"; } }
        public string CodigoProgramaExclu { get { return "80106"; } }
        public string CodigoProgramaReporteMovimiento { get { return "80108"; } }
        public string CodigoProgramaReclamacionServicios { get { return "80109"; } }

        public string CodigoProgramaCancelacionServ { get { return "80114"; } }

        AprobacionServiciosBusiness BOServicios;
        ExcepcionBusiness BOExcepcion;

        public AprobacionServiciosServices()
        {
            BOServicios = new AprobacionServiciosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<Servicio> Reportemovimiento(Servicio pAhorroVista, Usuario vUsuario)
        {
            try
            {
                return BOServicios.Reportemovimiento(pAhorroVista, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "Reportemovimiento", ex);
                return null;
            }
        }


        public List<Servicio> ListarOficinas(Servicio pPerso, Usuario vUsuario)
        {
            try
            {
                return BOServicios.ListarOficinas(pPerso, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATService", "ListarOficinas", ex);
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
                BOExcepcion.Throw("AprobacionServiciosServices", "ModificarSolicitudServicio", ex);
                return null;
            }
        }



        public List<Servicio> ListarServicios(string filtro, string pOrden, DateTime pFechaIni, Usuario vUsuario, DateTime? pFecPago = null,  int estadoCuenta = 0)
        {
            try
            {
                pFecPago = pFecPago == null ? DateTime.MinValue : pFecPago;
                return BOServicios.ListarServicios(filtro,pOrden, pFechaIni, Convert.ToDateTime(pFecPago), vUsuario, estadoCuenta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosServices", "ListarServicios", ex);
                return null;
            }
        }


        public List<Servicio> ListarServiciosClubAhorrador(Int64 pCodPersona, string pFiltro, Boolean pResult, Usuario pUsuario)
        {
            try
            {
                return BOServicios.ListarServiciosClubAhorrador(pCodPersona, pFiltro, pResult, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosServices", "ListarServiciosClubAhorrador", ex);
                return null;
            }
        }

        public List<Servicio> ListarCuentasPersona(Int64 pCod_Persona, Usuario vUsuario)
        {
            try
            {
                return BOServicios.ListarCuentasPersona(pCod_Persona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosServices", "ListarCuentasPersona", ex);
                return null;
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
                BOExcepcion.Throw("AprobacionServiciosServices", "ConsultarSERVICIO", ex);
                return null;
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
                BOExcepcion.Throw("AprobacionServiciosServices", "EliminarDETALLESERVICIO", ex);
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
                BOExcepcion.Throw("AprobacionServiciosServices", "ConsultarDETALLESERVICIO", ex);
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
                BOExcepcion.Throw("AprobacionServiciosServices", "CargarPlanXLinea", ex);
                return null;
            }
        }


        public CONTROLSERVICIOS CrearControlServicios(CONTROLSERVICIOS pControl, Usuario vUsuario)
        {
            try
            {
                return BOServicios.CrearControlServicios(pControl, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosServices", "CrearControlServicios", ex);
                return null;
            }
        }

        public CONTROLSERVICIOS ConsultarControlServicio(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOServicios.ConsultarControlServicio(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosServices", "ConsultarControlServicio", ex);
                return null;
            }
        }



        public void AprobarSolicitud(List<Servicio> lstServicio, Usuario vUsuario)
        {
            try
            {
                BOServicios.AprobarSolicitud(lstServicio, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosServices", "AprobarSolicitud", ex);
            }
        }

        //MODIFICACION DE SERVICIOS
        public bool ModificarServiciosActivos(ref Int64 COD_OPE, Xpinn.Tesoreria.Entities.Operacion pOperacion, Servicio pServicio, Usuario vUsuario, bool generaComprobante = true)
        {
            try
            {
                return BOServicios.ModificarServiciosActivos(ref COD_OPE, pOperacion, pServicio, vUsuario, generaComprobante);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosServices", "ModificarServiciosActivos", ex);
                return false;
            }
        }

        //CARGAR SERVICIOS
        public void RegistrarServiciosCargados(ref Int64 vCod_Ope, Xpinn.Tesoreria.Entities.Operacion pOperacion, string Cod_Linea, List<Servicio> lstServicios, Usuario vUsuario)
        {
            try
            {
                BOServicios.RegistrarServiciosCargados(ref vCod_Ope, pOperacion,Cod_Linea, lstServicios, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosServices", "RegistrarServiciosCargados", ex);
            }

            return  ;
        }

        public void ExcluirServicios(ref Int64 Cod_ope, Servicio pServicio, Xpinn.Tesoreria.Entities.Operacion pOperacion, ExclusionServicios pExclusion, Usuario vUsuario)
        {
            try
            {
                BOServicios.ExcluirServicios(ref Cod_ope,pServicio, pOperacion, pExclusion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosServices", "ExcluirServicios", ex);
            }
        }

        public Int64 ObtenerNumeroPreImpreso(Usuario pUsuario)
        {
            try
            {
                return BOServicios.ObtenerNumeroPreImpreso(pUsuario);
            }
            catch
            {
                return 1;
            }
        }

        public Servicio CrearTranServicios(Servicio pControl, Usuario vUsuario)
        {
            try
            {
                return BOServicios.CrearTranServicios(pControl, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosServices", "CrearTranServicios", ex);
                return null;
            }
        }

        public string CancelarServicio(Int64 NumServicio, Usuario vUsuario)
        {
            try
            {
                return BOServicios.CancelarServicio(NumServicio, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosServices", "CancelarServicio", ex);
                return "";
            }
        }
    }
}
