using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Business;
using Xpinn.Util;

namespace Xpinn.Auxilios.Services
{
    public class SolicitudAuxilioServices
    {
        public string CodigoPrograma { get { return "70101"; } }

        private SolicitudAuxilioBusiness BOAuxilio;
        private ExcepcionBusiness BOExcepcion;

        public SolicitudAuxilioServices()
        {
            BOAuxilio = new SolicitudAuxilioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public SolicitudAuxilio CrearSolicitudAuxilio(SolicitudAuxilio pAuxilio, Auxilio_Orden_Servicio pAuxOrden, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.CrearSolicitudAuxilio(pAuxilio,pAuxOrden, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioServices", "CrearSolicitudAuxilio", ex);
                return null;
            }
        }


        public SolicitudAuxilio ModificarSolicitudAuxilio(SolicitudAuxilio pAuxilio, Auxilio_Orden_Servicio pAuxOrden, int pOpcion, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.ModificarSolicitudAuxilio(pAuxilio,pAuxOrden,pOpcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioServices", "ModificarSolicitudAuxilio", ex);
                return null;
            }
        }


        public List<SolicitudAuxilio> ListarSolicitudAuxilio(SolicitudAuxilio pAuxilio, DateTime pFechaSol, Usuario vUsuario, string filtro)
        {
            try
            {
                return BOAuxilio.ListarSolicitudAuxilio(pAuxilio, pFechaSol, vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioServices", "ListarSolicitudAuxilio", ex);
                return null;
            }
        }


        public void EliminarAuxilio(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOAuxilio.EliminarAuxilio(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioServices", "EliminarAuxilio", ex);
            }
        }

        public SolicitudAuxilio ConsultarAUXILIO(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.ConsultarAUXILIO(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioServices", "ConsultarAUXILIO", ex);
                return null;
            }
        }

        public bool ConsultarEstadoPersona(Int64? pCodPersona, string pIdentificacion, string pEstado, Usuario pUsuario)
        {
            try
            {
                return BOAuxilio.ConsultarEstadoPersona(pCodPersona, pIdentificacion, pEstado, pUsuario);
            }
            catch
            {
                return false;
            }
        }



        //Detalle

        public void EliminarDETALLEAuxilio(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOAuxilio.EliminarDETALLEAuxilio(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioServices", "EliminarDETALLEAuxilio", ex);
            }
        }


        public List<DetalleSolicitudAuxilio> ConsultarDETALLEAuxilio(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.ConsultarDETALLEAuxilio(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioServices", "ConsultarDETALLEAuxilio", ex);
                return null;
            }
        }


        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOAuxilio.ObtenerSiguienteCodigo(pUsuario);
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioServices", "ObtenerSiguienteCodigo", ex);
                return 1;
            }
        }


        public SolicitudAuxilio ListarLineasDauxilios(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.ListarLineasDauxilios(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioServices", "ListarLineasDauxilios", ex);
                return null;
            }
        }


        public List<Requisitos> ConsultarValidacionRequisitos(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.ConsultarValidacionRequisitos(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioServices", "ConsultarValidacionRequisitos", ex);
                return null;
            }
        }


        public List<Requisitos> CargarDatosRequisitos(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.CargarDatosRequisitos(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioServices", "CargarDatosRequisitos", ex);
                return null;
            }
        }

        public Auxilio_Orden_Servicio ConsultarAUX_OrdenServicio(String pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.ConsultarAUX_OrdenServicio(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioServices", "ConsultarAUX_OrdenServicio", ex);
                return null;
            }
        }

        public Int64 ObtenerNumeroPreImpreso(Usuario pUsuario)
        {
            try
            {
                return BOAuxilio.ObtenerNumeroPreImpreso(pUsuario);
            }
            catch
            {
                return 1;
            }
        }

        public void ModificarAuxilio_OrdenServ(Auxilio_Orden_Servicio pAuxi,ref string pError, Usuario pUsuario)
        {
            try
            {
                BOAuxilio.ModificarAuxilio_OrdenServ(pAuxi,ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioServices", "ModificarAuxilio_OrdenServ", ex);
            }
        }

        public SolicitudAuxilio Consultar_Auxilio_Variado(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.Consultar_Auxilio_Variado(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudAuxilioServices", "Consultar_Auxilio_Variado", ex);
                return null;
            }
        }


        public void Generar_desembolso_auxilio(SolicitudAuxilio pAuxilio, DesembolsoAuxilio pTran_Aux, ref string pError, Usuario vUsuario)
        {
            try
            {
                BOAuxilio.Generar_desembolso_auxilio(pAuxilio, pTran_Aux, ref pError, vUsuario);
            }
            catch
            {
            }
        }


    }
}
