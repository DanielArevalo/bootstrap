using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Aportes.Business;
using Xpinn.Aportes.Entities;
using Xpinn.FabricaCreditos.Entities;
using System.Configuration;

namespace Xpinn.Aportes.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AfiliacionServices
    {

        private AfiliacionBusiness BOAfiliacion;
        private ExcepcionBusiness BOExcepcion;
      
        public int Codigoaporte;
        /// <summary>
        /// Constructor del servicio para Aporte
        /// </summary>
        public AfiliacionServices()
        {
            BOAfiliacion = new AfiliacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        
        public string CodigoPrograma { get { return "170104"; } }
        public string codigoprogramabiometria { get { return "170110"; } }
        public string codigoprogramaafiliacion { get { return "170113"; } }
        public string codigoprogramaReafiliacion { get { return "170115"; } }
        public string codigoprogramaConfirmarData { get { return "170116"; } }
        public string codigoprogramaConfirmarAfili { get { return "170117"; } }
    

        public Afiliacion CrearPersonaAfiliacion(Afiliacion pAfiliacion, Usuario pUsuario)
        {
            try
            {
                return BOAfiliacion.CrearPersonaAfiliacion(pAfiliacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "CrearPersonaAfiliacion", ex);
                return null;
            }
        }

        public Int64 crearcausacionafiliacion( Afiliacion pAfiliacion, Usuario pUsuario)
        {
            try
            {
                pAfiliacion.cod_ope=BOAfiliacion.crearcausacionafiliacion( pAfiliacion, pUsuario);
                return pAfiliacion.cod_ope;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "crearcausacionafiliacion", ex);
                return 0;
            }
        }

        public Afiliacion ModificarPersonaAfiliacion(Afiliacion pAfiliacion, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ModificarPersonaAfiliacion(pAfiliacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "CrearPersonaAfiliacion", ex);
                return null;
            }
        }

        public List<Afiliacion> listarpersonaafiliacion(Afiliacion pAfiliacion, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.listarpersonaafiliacion(pAfiliacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "CrearPersonaAfiliacion", ex);
                return null;
            }
        }

        public Afiliacion ConsultarAfiliacion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ConsultarAfiliacion(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ConsultarAfiliacion", ex);
                return null;
            }
        }

        

        public DateTime? FechaInicioAfiliacion(DateTime pFechaDesc, Int64 pCodEmpresaDesc, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.FechaInicioAfiliacion(pFechaDesc, pCodEmpresaDesc, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "FechaInicioAfiliacion", ex);
                return null;
            }
        }

        public List<Afiliacion> ConsultarAportes(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ConsultarAportes(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarAportes", ex);
                return null;
            }
        }

        //consulta para controlar la cantidad de realifialiciones permitidas
        public int ConsultarReafilPerm(Afiliacion pAfiliacion, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ConsultarReafilPerm(pAfiliacion, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ConsultarAfiliacion", ex);
                return 0;
            }
        }

        public bool ModificarReafiliacion(Afiliacion pAfiliacion, List<Afiliacion> lstAportes, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ModificarReafiliacion(pAfiliacion, lstAportes, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsultarReafilPerm", "ConsultarReafilPerm", ex);
                return false;
            }
        }
        
        public List<Estado_Persona> ListarEstadoPersona(Estado_Persona pEstado, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ListarEstadoPersona(pEstado, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ListarEstadoPersona", ex);
                return null;
            }
        }

        public List<PersonaActualizacion> ListarDataPersonasXactualizar(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ListarDataPersonasXactualizar(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ListarDataPersonasXactualizar", ex);
                return null;
            }
        }

        public List<TranAfiliacion> ListarMovAfiliacion(TranAfiliacion pTranAfiliacion, Usuario pUsuario)
        {
            try
            {
                return BOAfiliacion.ListarMovAfiliacion(pTranAfiliacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ListarMovAfiliacion", ex);
                return null;
            }
        }

        public bool Eliminar_PersonaResponsable(PersonaResponsable pPersona, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.Eliminar_PersonaResponsable(pPersona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "Eliminar_PersonaResponsable", ex);
                return false;
            }
        }
        public PersonaResponsable ConsultarPersonaResponsable(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ConsultarPersonaResponsable(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ConsultarPersonaResponsable", ex);
                return null;
            }
        }

        public string ConsultarEstadoAfiliacion(string identificacion, Usuario usuario)
        {
            try
            {
                return BOAfiliacion.ConsultarEstadoAfiliacion(identificacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ConsultarEstadoAfiliacion", ex);
                return null;
            }
        }

        public string ConsultarEstadoAfiliacion(Int64 pCodPersona, Usuario usuario)
        {
            try
            {
                return BOAfiliacion.ConsultarEstadoAfiliacion(pCodPersona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ConsultarEstadoAfiliacion", ex);
                return null;
            }
        }

        public List<SolicitudPersonaAfi> ListarDataSolicitudAfiliacion(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ListarDataSolicitudAfiliacion(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ListarDataSolicitudAfiliacion", ex);
                return null;
            }
        }
        public void notificarEmail(Int32 cod_proceso, string paso, Int64 cod_per, Int32 aso, Int32 ase, string otro, Usuario vUsuario)
        {
            /******ASUNTO*********/
            string pSubject = "Notificación proceso de afiliación";
            /****CUERPO DEL MENSAJE***/
            ParametrizacionProcesoAfiliacion cuerpoCorreo = consultarTextoCorreo(cod_proceso, cod_per, vUsuario);
            cuerpoCorreo.correo = cuerpoCorreo.correo.Replace("@NOMBRE_ASOCIADO", cuerpoCorreo.nombre_asociado);
            cuerpoCorreo.correo = cuerpoCorreo.correo.Replace("@NOMBRE_ASESOR", cuerpoCorreo.nombre_asesor);
            cuerpoCorreo.correo = cuerpoCorreo.correo.Replace("@PASO", paso);

            // RECUPERANDO DATOS DEL CORREO SERVER
            string correo = ConfigurationManager.AppSettings["CorreoServidor"] as string;
            string claveCorreo = ConfigurationManager.AppSettings["claveCorreo"] as string;
            string hosting = ConfigurationManager.AppSettings["Hosting"] as string;
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"].ToString());
            string salida = "";
            /************ENVIAR CORREO DE NOTIFICACIÓN*******/
            if (Convert.ToBoolean(aso))
            {
                CorreoHelper correoHelper = new CorreoHelper(cuerpoCorreo.email_asociado, correo, claveCorreo);
                bool resultNotification = correoHelper.sendEmail(cuerpoCorreo.correo,out salida, pSubject, cuerpoCorreo.email_asociado);
            }
            if (Convert.ToBoolean(ase) && cuerpoCorreo.email_asesor == null && cuerpoCorreo.email_asesor == "")
            {
                CorreoHelper correoHelper = new CorreoHelper(cuerpoCorreo.email_asesor, correo, claveCorreo);
                bool resultNotification = correoHelper.sendEmail(cuerpoCorreo.correo, out salida, pSubject, cuerpoCorreo.email_asesor);
            }
            if (otro != "" && otro != null)
            {
                CorreoHelper correoHelper = new CorreoHelper(otro, correo, claveCorreo);
                bool resultNotification = correoHelper.sendEmail(cuerpoCorreo.correo, out salida, pSubject, otro);
            }

        }
        public ParametrizacionProcesoAfiliacion consultarTextoCorreo(Int32 cod_proceso, Int64 cod_per, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.consultarTextoCorreo(cod_proceso, cod_per, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "consultarTextoCorreo", ex);
                return null;
            }
        }
        public ParametrizacionProcesoAfiliacion controlRutaAfiliacion(ParametrizacionProcesoAfiliacion control, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.controlRutaAfiliacion(control, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ListarDataSolicitudAfiliacion", ex);
                return null; 
            }
        }
        public Int64 ConfirmacionSolicitudAfiliacion(SolicitudPersonaAfi solicitud, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ConfirmacionSolicitudAfiliacion(solicitud, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ConfirmacionDeAfiliaciones", ex);
                return 0;
            }
        }

        public void EliminarSolicitudAfiliacion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOAfiliacion.EliminarSolicitudAfiliacion(pId, vUsuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "EliminarSolicitudAfiliacion", ex);
            }
        }

        public SolicitudPersonaAfi ConsultarSolicitudAfiliacion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ConsultarSolicitudAfiliacion(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ConsultarSolicitudAfiliacion", ex);
                return null;
            }
        }

        public DateTime? FechaAfiliacion(string pCodPersona, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.FechaAfiliacion(pCodPersona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "FechaAfiliacion", ex);
                return null;
            }
        }
       
        public List<Afiliacion> ListarReafiliaciones(Afiliacion pReafiliacion, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOAfiliacion.ListarReafiliaciones(pReafiliacion, pUsuario, filtro);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ListarReafiliaciones", ex);
                return null;              
            }
        }

        public int ConsultarCantidadAfiliados(string pCondicion, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ConsultarCantidadAfiliados(pCondicion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ConsultarCantidadAfiliados", ex);
                return 0;
            }
        }

        public List<ConsultarPersonaBasico> ListarPersonasAfiliadasPaginado(string pCondicion, int pIndicePagina, int pRegistrosPagina, Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ListarPersonasAfiliadasPaginado(pCondicion, pIndicePagina, pRegistrosPagina, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ListarTerceroSoloAfiliados", ex);
                return null;
            }
        }


        public List<ConsultarPersonaBasico> ListarPersonasOficinaVirtual(Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ListarPersonasOficinaVirtual(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ListarTerceroSoloAfiliados", ex);
                return null;
            }
        }        

        public void EliminarPersonaParentesco(long consecutivoParaBorrar, Usuario usuario)
        {
            try
            {
                BOAfiliacion.EliminarPersonaParentesco(consecutivoParaBorrar, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "EliminarPersonaParentesco", ex);
            }
        }

        public List<PersonaParentescos> ListarParentescoDeUnaPersona(long codigoPersona, Usuario usuario)
        {
            try
            {
                return BOAfiliacion.ListarParentescoDeUnaPersona(codigoPersona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ListarParentescoDeUnaPersona", ex);
                return null;
            }
        }

        public PersonaParentescos ConsultarParentescoDeUnaPersona(long IdPersona, Usuario usuario)
        {
            try
            {
                return BOAfiliacion.ConsultarParentescoDeUnaPersona(IdPersona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ConsultarParentescoDeUnaPersona", ex);
                return null;
            }
        }

        public void CrearPersonaParentesco(PersonaParentescos parentesco, Usuario usuario)
        {
            try
            {
                BOAfiliacion.CrearPersonaParentesco(parentesco, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "CrearPersonaParentesco", ex);
            }
        }

        public void ModificarPersonaParentesco(PersonaParentescos parentesco, Usuario usuario)
        {
            try
            {
                BOAfiliacion.ModificarPersonaParentesco(parentesco, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ModificarPersonaParentesco", ex);
            }
        }

        public LineaAporte ConsultarLineaObligatoria(Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ConsultarLineaObligatoria(vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarLineaObligatoria", ex);
                return null;
            }
        }


        public Afiliacion ConsultarCierrePersonas(Usuario vUsuario)
        {
            try
            {
                return BOAfiliacion.ConsultarCierrePersonas(vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ConsultarCierrePersonas", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para crear un proceso de afiliación 
        /// </summary>
        /// <param name="pProceso">Objeto a registrar</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public ProcesoAfiliacion CrearProceso(ProcesoAfiliacion pProceso, Usuario pUsuario)
        {
            try
            {
                pProceso = BOAfiliacion.CrearProceso(pProceso, pUsuario);
                return pProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "CrearProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar un registro de un proceso de afiliación 
        /// </summary>
        /// <param name="pProceso">Objeto a modificar</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public ProcesoAfiliacion ModificarProceso(ProcesoAfiliacion pProceso, Usuario pUsuario)
        {
            try
            {
                pProceso = BOAfiliacion.ModificarProceso(pProceso, pUsuario);

                return pProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ModificarProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para listar datos de procesos basado en un filtro
        /// </summary>
        /// <param name="filtro">Filtro del listado</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<ProcesoAfiliacion> ListarProcesos(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOAfiliacion.ListarProcesos(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ListarProcesos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para consultar los datos de una entrevista en especifico
        /// </summary>
        /// <param name="pProceso">Objeto para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public ProcesoAfiliacion ConsultarProceso(ProcesoAfiliacion pProceso, Usuario pUsuario)
        {
            try
            {
                return BOAfiliacion.ConsultarProceso(pProceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionBusiness", "ConsultarProceso", ex);
                return null;
            }
        }
    }
}