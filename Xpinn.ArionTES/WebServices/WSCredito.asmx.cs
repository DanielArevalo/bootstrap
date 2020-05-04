using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;

namespace WebServices
{
    /// <summary>
    /// Descripción breve de WSCredito
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSCredito : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Xpinn.Asesores.Entities.ProductoResumen> ConsultaCredito(int pnumero_credito, string pClave)
        {
            string sLogin = "";
            if (sLogin.Trim() == "")
                sLogin = "52213921";
            // Definición de entidades y servicios
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            // Validar usuario y obtener accesos
            usuario.identificacion = sLogin;
            usuario.clave_sinencriptar = pClave;
            usuario = usuarioServicio.ValidarUsuario(usuario.identificacion, usuario.clave_sinencriptar, "", "", usuario);
            
            // Consultar productos de la persona
            Xpinn.Asesores.Services.ProductoService productoServicio = new Xpinn.Asesores.Services.ProductoService();
            List<Xpinn.Asesores.Entities.ProductoResumen> lstProducto = new List<Xpinn.Asesores.Entities.ProductoResumen>();
            lstProducto = productoServicio.ListarProductosCreditoResumen(pnumero_credito, usuario);
            // Devolver listado de productos
            return lstProducto;
        }


        [WebMethod]
        public List<Xpinn.Asesores.Entities.MovimientoResumen> UltimosMovimientos(int pnumero_credito, int pnumeromovimientos, string pClave)
        {
            string sLogin = "";
            if (sLogin.Trim() == "")
                sLogin = "52213921";
            // Definición de entidades y servicios
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            // Validar usuario y obtener accesos
            usuario.identificacion = sLogin;
            usuario.clave_sinencriptar = pClave;
            usuario = usuarioServicio.ValidarUsuario(usuario.identificacion, usuario.clave_sinencriptar, "", "", usuario);
            // Consultar productos de la persona
            Xpinn.Asesores.Services.ProductoService productoServicio = new Xpinn.Asesores.Services.ProductoService();
            List<Xpinn.Asesores.Entities.MovimientoResumen> lstProducto = new List<Xpinn.Asesores.Entities.MovimientoResumen>();
            lstProducto = productoServicio.ListarMovimientoResumen(pnumero_credito, pnumeromovimientos, usuario);
            // Devolver listado de productos
            return lstProducto;
        }

        [WebMethod]
        public List<Xpinn.FabricaCreditos.Entities.CreditoPlan> ListarCredito(Xpinn.FabricaCreditos.Entities.CreditoPlan pCredito, String filtro, string sec)
        {
            Xpinn.FabricaCreditos.Services.CreditoPlanService BOCreditoPlan = new Xpinn.FabricaCreditos.Services.CreditoPlanService();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<Xpinn.FabricaCreditos.Entities.CreditoPlan> lstResult = BOCreditoPlan.ListarCredito(pCredito, usuario, filtro);
            return lstResult;
        }


        [WebMethod]
        public List<Xpinn.FabricaCreditos.Entities.LineasCredito> ListarDocumentosCredito(int idCredito, string sec)
        {
            Xpinn.FabricaCreditos.Services.LineasCreditoService BOCreditoPlan = new Xpinn.FabricaCreditos.Services.LineasCreditoService();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<Xpinn.FabricaCreditos.Entities.LineasCredito> lstResult = BOCreditoPlan.ConsultarGarantiasPorCredito(idCredito, usuario);
            return lstResult;
        }


        [WebMethod]
        public TiposDocCobranzas ConsultarInformacionCorreoSolicitudCred(string sec)
        {
            TiposDocCobranzasServices tipoDocumentoServicio = new TiposDocCobranzasServices();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            TiposDocCobranzas datosCorreo = tipoDocumentoServicio.ConsultarFormatoDocumentoCorreo((int)TipoDocumentoCorreo.SolicitudCreditoAtencionWeb, usuario);
            datosCorreo.empresa = tipoDocumentoServicio.ConsultarCorreoEmpresa(0, usuario);

            return datosCorreo;
        }


        [WebMethod(Description = "Consulta de Credito, Movimiento de productos por AAC")]
        public Xpinn.FabricaCreditos.Entities.CreditoPlan ConsultarInformacionCreditos(Int64 pId, Boolean btasa, string sec)
        {
            Xpinn.FabricaCreditos.Services.CreditoPlanService BOCreditoPlan = new Xpinn.FabricaCreditos.Services.CreditoPlanService();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.FabricaCreditos.Entities.CreditoPlan pResult = BOCreditoPlan.ConsultarCredito(pId, btasa, usuario);
            return pResult;
        }


        [WebMethod]
        public List<Xpinn.FabricaCreditos.Entities.DatosPlanPagos> ListarDatosPlanPagos(Xpinn.FabricaCreditos.Entities.Credito pDatos, string sec)
        {
            Xpinn.FabricaCreditos.Services.DatosPlanPagosService BODatosPlanPagos = new Xpinn.FabricaCreditos.Services.DatosPlanPagosService();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<Xpinn.FabricaCreditos.Entities.DatosPlanPagos> lstResult = BODatosPlanPagos.ListarDatosPlanPagos(pDatos, usuario);
            return lstResult;
        }

        [WebMethod]
        public List<Xpinn.Asesores.Entities.ConsultaAvance> ListarAvancesCredito(long radicado, string filtro, string sec)
        {
            Xpinn.Asesores.Services.EstadoCuentaService BODatosAvances = new EstadoCuentaService();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<Xpinn.Asesores.Entities.ConsultaAvance> lstResult = BODatosAvances.ListarAvances(radicado, usuario, filtro);
            return lstResult;
        }



        [WebMethod]
        public List<Xpinn.FabricaCreditos.Entities.Atributos> GenerarAtributosPlan(string sec)
        {
            Xpinn.FabricaCreditos.Services.DatosPlanPagosService BODatosPlanPagos = new Xpinn.FabricaCreditos.Services.DatosPlanPagosService();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<Xpinn.FabricaCreditos.Entities.Atributos> lstResult = BODatosPlanPagos.GenerarAtributosPlan(usuario);
            return lstResult;
        }


        [WebMethod]
        public Xpinn.Asesores.Entities.Persona ConsultarPersona(Int64 pCod_Persona, string pClave, string sec)
        {
            Xpinn.Asesores.Services.PersonaService BOPersona = new Xpinn.Asesores.Services.PersonaService();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Asesores.Entities.Persona pResult = BOPersona.ConsultarPersona(pCod_Persona, usuario);
            return pResult;
        }

        [WebMethod]
        public Xpinn.FabricaCreditos.Entities.LineasCredito ConsultarLineasCredito(string pId, string pClave, string sec)
        {
            Xpinn.FabricaCreditos.Services.LineasCreditoService BOLinea = new Xpinn.FabricaCreditos.Services.LineasCreditoService();

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.FabricaCreditos.Entities.LineasCredito pResult = BOLinea.ConsultarLineasCredito(pId, usuario);
            return pResult;
        }

        [WebMethod]
        public List<LineasCredito> ListarDocumentos(string pCod_linea_credito, string pIdentificacion, string pClave, string sec)
        {
            LineasCreditoService BOLineasCredito = new LineasCreditoService();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<LineasCredito> lstResult = BOLineasCredito.ListarDocumentosXLinea(pCod_linea_credito, usuario);
            return lstResult;
        }

        [WebMethod]
        public byte[] ConsultarPagare(string numeroRadicacion, string sec)
        {
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);

            if (usuario == null)
                return null;

            DocumentosAnexosService docService = new DocumentosAnexosService();
            string filtro = string.Format("WHERE numero_radicacion = {0} and tipo_documento = 1 and estado = 1", numeroRadicacion);

            DocumentosAnexos documento = docService.ConsultarDocumentosAnexosConFiltro(filtro, usuario);

            if (documento != null && documento.imagen != null)
            {
                return documento.imagen;
            }
            else
            {
                return null;
            }
        }


        [WebMethod]
        public Xpinn.Comun.Entities.ComisionAporteSeguro ValidarComisionAporte(string pCod_Linea, string pClave, string sec)
        {
            Xpinn.FabricaCreditos.Services.OficinaService BOValidar = new Xpinn.FabricaCreditos.Services.OficinaService();

            Xpinn.Comun.Entities.ComisionAporteSeguro pResult = new Xpinn.Comun.Entities.ComisionAporteSeguro();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            bool comision = false;
            bool aporte = false;
            bool seguro = false;
            BOValidar.ValidarComisionAporte(pCod_Linea, ref comision, ref aporte, ref seguro, usuario);

            pResult.comision = comision;
            pResult.aporte = aporte;
            pResult.seguro = seguro;

            return pResult;
        }

        [WebMethod]
        public LineasCredito Calcular_Cupo(String pcod_linea_credito, Int64 pcod_persona, DateTime pfecha, string pClave, string sec)
        {
            LineasCreditoService BOLineaCredito = new LineasCreditoService();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            LineasCredito pResult = BOLineaCredito.Calcular_Cupo(pcod_linea_credito, pcod_persona, pfecha, usuario);
            return pResult;
        }

        [WebMethod]
        public LineasCredito ConsultarTasaInteresLineaCredito(string pCod_linea_, string pClave, string sec)
        {
            LineasCreditoService BOLineaCredito = new LineasCreditoService();
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            LineasCredito pResult = BOLineaCredito.ConsultarTasaInteresLineaCredito(pCod_linea_, usuario);
            return pResult;
        }



        [WebMethod]
        public Xpinn.FabricaCreditos.Entities.Simulacion ConsultarSimulacionCuota(long monto, int plazo, int periodicidad, string cod_cred, int tipo_liquidacion, decimal tasa,
            decimal Comision, decimal Aporte, DateTime? FechaPrimerPago, string pClave, long pCodPersona, List<CuotasExtras> lstCuotasExtras, string sec)
        {
            string pError = "";
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            // CONSULTANDO LA TASA DE LA LINEA POR DEFECTO
            if (tasa == 0)
            {
                LineasCreditoService LineaServices = new LineasCreditoService();
                LineasCredito pResult = LineaServices.ConsultarTasaInteresLineaCredito(cod_cred, usuario);
                if (pResult != null)
                {
                    tasa = pResult.tasa != null && pResult.tasa != 0 ? Convert.ToDecimal(pResult.tasa) : 0;
                }
            }
            SimulacionService BOSimulacion = new SimulacionService();
            Xpinn.FabricaCreditos.Entities.Simulacion Response = BOSimulacion.ConsultarSimulacionCuota(monto, plazo, periodicidad, cod_cred, tipo_liquidacion, tasa, Comision, Aporte, FechaPrimerPago, ref pError, usuario, pCodPersona, lstCuotasExtras);
            if (Response != null)
            {
                if (!string.IsNullOrEmpty(pError))
                    Response.message = pError;
            }
            return Response;
        }

        [WebMethod]
        public List<DatosPlanPagos> SimularPlanPagos(Xpinn.FabricaCreditos.Entities.Simulacion pDatos, string pClave, string sec)
        {
            SimulacionService BOSimulacion = new SimulacionService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<DatosPlanPagos> lstPlanPagos = BOSimulacion.SimularPlanPagos(pDatos, usuario);
            return lstPlanPagos;
        }

        [WebMethod]
        public List<Atributos> SimularAtributosPlan(string pClave, string sec)
        {
            SimulacionService BOSimulacion = new SimulacionService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<Atributos> lstResult = BOSimulacion.SimularAtributosPlan(usuario);
            return lstResult;
        }



        [WebMethod(Description = "Creacion de Solicitud de Crédito / Documentos")]
        public SolicitudCreditoAAC CrearSolicitudCreditoAAC(SolicitudCreditoAAC pSolicitudCreditoAAC, string pIdentificacion, string pClave, string sec, List<DocumentosAnexos> lstDocumentos = null, List<CuotasExtras> lstCuotasExtras = null)
        {
            // Verificar conexión a la base de datos
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            // Determinar el còdigo de la persona 
            Xpinn.FabricaCreditos.Services.Persona1Service personaSer = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 eper = new Persona1();
            eper.soloPersona = 1;
            eper.sinImagen = 1;
            eper.noTraerHuella = 0;
            eper.seleccionar = "Identificacion";
            eper.identificacion = pIdentificacion;
            eper = personaSer.ConsultarPersona1Param(eper, usuario);
            if (eper == null)
                return null;            
            pSolicitudCreditoAAC.cod_persona = eper.cod_persona;
            // Crear la solicitud
            Xpinn.FabricaCreditos.Services.CreditoSolicitadoService BOSolicitud = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();
            Xpinn.FabricaCreditos.Entities.SolicitudCreditoAAC pEntidad = new Xpinn.FabricaCreditos.Entities.SolicitudCreditoAAC();
            pEntidad = BOSolicitud.CrearSolicitudCreditoAAC(pSolicitudCreditoAAC, usuario, 1, lstDocumentos, lstCuotasExtras);
            try
            {
                bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.SolicitudCredito, usuario, pSolicitudCreditoAAC.cod_persona);
            }
            catch (Exception)
            {
            }
            return pEntidad;
        }

        [WebMethod(Description = "Creacion de Solicitud de Crédito / Documentos")]
        public SolicitudCreditoAAC CrearSolicitudCreditoProveedor(SolicitudCreditoAAC pSolicitudCreditoAAC, string sec, List<DocumentosAnexos> lstDocumentos = null)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            Xpinn.FabricaCreditos.Services.CreditoSolicitadoService BOSolicitud = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();
            Xpinn.FabricaCreditos.Entities.SolicitudCreditoAAC pEntidad = new Xpinn.FabricaCreditos.Entities.SolicitudCreditoAAC();

            pEntidad = BOSolicitud.CrearSolicitudCreditoProveedor(pSolicitudCreditoAAC, usuario, lstDocumentos);
            try
            {
                //string mensaje;
                bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.SolicitudCredito, usuario, pSolicitudCreditoAAC.cod_persona);
                pEntidad.estado = resultNotification ? 1 : 0;
            }
            catch (Exception)
            {
            }
            return pEntidad;
        }
        


        [WebMethod(Description = "Confirmación automática de Solicitud de crédito")]
        public Int64 ConfirmarSolicitudCreditoAutomatico(SolicitudCreditoAAC pSolicitudCreditoAAC, string sec)
        {
            try
            {
                Conexion conexion = new Conexion();
                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                usuario = conexion.DeterminarUsuarioOficina(sec);
                if (usuario == null)
                    return 0;
                Xpinn.FabricaCreditos.Services.CreditoSolicitadoService BOSolicitud = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();
                Int64 radicado = 0;
                radicado = BOSolicitud.ConfirmarSolicitudCreditoAutomatico(pSolicitudCreditoAAC, usuario);


                Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
                vControlCreditos.numero_radicacion = Convert.ToInt64(radicado);
                Xpinn.FabricaCreditos.Services.ControlCreditosService controlService = new ControlCreditosService();
                vControlCreditos.codtipoproceso = controlService.obtenerCodTipoProceso("S", usuario);
                vControlCreditos.fechaproceso = Convert.ToDateTime(DateTime.Now);
                vControlCreditos.cod_persona = 0;
                vControlCreditos.cod_motivo = 0;
                vControlCreditos.anexos = null;
                vControlCreditos.nivel = 0;
                vControlCreditos = controlService.CrearControlCreditos(vControlCreditos, usuario);

                return radicado;
            }
            catch //(Exception e)
            {
                return 0;
            }
        }


        [WebMethod(Description = "Creacion de giros para un credito aprobado")]
        public bool CrearGiros(List<Xpinn.FabricaCreditos.Entities.Credito_Giro> lstGiros, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return false;

            Xpinn.FabricaCreditos.Services.Credito_GiroService BOGiros = new Xpinn.FabricaCreditos.Services.Credito_GiroService();                        
            return BOGiros.CrearGiros(lstGiros, usuario);
        }

        [WebMethod(Description = "Lista de giros establecidos")]
        public List<Xpinn.FabricaCreditos.Entities.Credito_Giro> listarGiros(string radicado, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.FabricaCreditos.Services.Credito_GiroService BOGiros = new Xpinn.FabricaCreditos.Services.Credito_GiroService();
            return BOGiros.ListarGiros(radicado, usuario);
        }


        [WebMethod(Description = "Creacion documentos de garantia para un credito aprobado")]
        public bool CrearDocGarantias(Xpinn.FabricaCreditos.Entities.Credito credito, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return false;
                        
            Xpinn.FabricaCreditos.Services.DocumentoService BODocs = new Xpinn.FabricaCreditos.Services.DocumentoService();
            return BODocs.CrearDocGarantias(credito, usuario);
        }


        [WebMethod(Description = "listar documentos de garantia para un credito aprobado por numero radicado")]
        public List<Documento> ListarDocGarantias(Xpinn.FabricaCreditos.Entities.Documento credito, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.FabricaCreditos.Services.DocumentoService BODocs = new Xpinn.FabricaCreditos.Services.DocumentoService();
            return BODocs.ListarDocumentoGenerado(credito, usuario);
        }




        [WebMethod]
        public List<MovimientoProducto> ListarMovCreditos(Int64 pNumeroRadicacion, int detalle, string pIdentificacion, string pClave, string sec)
        {
            try
            {
                DetalleProductoService BOCredito = new DetalleProductoService();
                Conexion conexion = new Conexion();
                Usuario usuario = new Usuario();
                usuario = conexion.DeterminarUsuarioOficina(sec);
                if (usuario == null)
                    return null;
                return BOCredito.ListarMovCreditos(pNumeroRadicacion, usuario, detalle);
            }
            catch
            {
                return null;
            }
        }
        

        [WebMethod]
        public List<Xpinn.Aportes.Entities.MovimientoAporte> ListarMovAporte(Int64 pNumeroAporte, DateTime pfechaInicial, DateTime pfechaFinal, string pIdentificacion, string pClave, string sec)
        {
            try
            {
                Xpinn.Aportes.Services.AporteServices BOAporte = new Xpinn.Aportes.Services.AporteServices();
                Conexion conexion = new Conexion();
                Usuario usuario = new Usuario();
                usuario = conexion.DeterminarUsuarioOficina(sec);
                if (usuario == null)
                    return null;

                List<Xpinn.Aportes.Entities.MovimientoAporte> lstMovAporte = BOAporte.ListarMovAporte(pNumeroAporte, pfechaInicial, pfechaFinal, usuario);
                return lstMovAporte;
            }
            catch
            {
                return null;
            }
        }

        [WebMethod(Description = "Consulta de Aporte, Movimiento de Producto en AAC")]
        public Xpinn.Aportes.Entities.Aporte ConsultarAporte(Int64 pId, string pIdentificacion, string pClave, string sec)
        {
            try
            {
                Xpinn.Aportes.Services.AporteServices BOAporte = new Xpinn.Aportes.Services.AporteServices();
                Conexion conexion = new Conexion();
                Usuario usuario = new Usuario();
                usuario = conexion.DeterminarUsuarioOficina(sec);
                if (usuario == null)
                    return null;

                return BOAporte.ConsultarAporte(pId, usuario);
            }
            catch
            {
                return null;
            }
        }

        [WebMethod(Description = "GENERACION DE SERVICIO Y DESEMBOLSO")]
        public Xpinn.Servicios.Entities.ServicioEntity CrearServicioDesembolso(string pEntidad, string pIdentificacion, string pCod_linea_servicio,
            decimal pValorTotal, int pNumeroCuotas, int pPeriodicidad, int pForma_pago, decimal pVr_compra, decimal pVr_beneficio, decimal pVr_Mercado, int pCodProceso)
        {
            Xpinn.Servicios.Services.SolicitudServiciosServices SolicServicios = new Xpinn.Servicios.Services.SolicitudServiciosServices();
            Xpinn.Servicios.Entities.ServicioEntity pResult = new Xpinn.Servicios.Entities.ServicioEntity();

            //REALIZANDO VALIDACIONES DE DATOS OBTENIDOS
            if (string.IsNullOrEmpty(pEntidad))
            {
                pResult.esCorrecto = false;
                pResult.mensaje = "El nombre de la entidad indicada no puede ser nulo.";
                return pResult;
            }
            if (string.IsNullOrEmpty(pIdentificacion))
            {
                pResult.esCorrecto = false;
                pResult.mensaje = "No se obtuvo la identificación del asociado.";
                return pResult;
            }
            if (string.IsNullOrEmpty(pCod_linea_servicio))
            {
                pResult.esCorrecto = false;
                pResult.mensaje = "El código de la linea es incorrecto.";
                return pResult;
            }
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(pEntidad);
            if (usuario == null)
            {
                pResult.esCorrecto = false;
                pResult.mensaje = "No se obtuvo los datos para realizar la conexión.";
                return pResult;
            }
            if (pForma_pago != 1 && pForma_pago != 2)
            {
                pResult.esCorrecto = false;
                pResult.mensaje = "El campo de la forma de pago es inválido, Usar [ 1 = Caja, 2 = Nómina ].";
                return pResult;
            }

            // RECUPERANDO PARAMETRO DE USUARIO
            if (ConfigurationManager.AppSettings["CodUsuario"] == null)
            {
                pResult.esCorrecto = false;
                pResult.mensaje = "No se configuró el usuario por defecto.";
                return pResult;
            }
            string pCodUsuario = ConfigurationManager.AppSettings["CodUsuario"];
            if (string.IsNullOrEmpty(pCodUsuario))
            {
                pResult.esCorrecto = false;
                pResult.mensaje = "No se configuró el usuario por defecto.";
                return pResult;
            }
            Xpinn.Seguridad.Services.UsuarioService ServicesUsuario = new Xpinn.Seguridad.Services.UsuarioService();
            Usuario pUsuDefault = ServicesUsuario.ConsultarUsuario(Convert.ToInt64(pCodUsuario), usuario);
            if (pUsuDefault == null)
            {
                pResult.esCorrecto = false;
                pResult.mensaje = "Error al consultar al usuario por defecto.";
                return pResult;
            }
            usuario.IP = "0.0.0.0";
            usuario.codusuario = pUsuDefault.codusuario;
            usuario.cod_oficina = pUsuDefault.cod_oficina;

            //VALIDACION DEL ESTADO DE LA PERSONA
            Persona1Service ServicePersona = new Persona1Service();

            Persona1 DatosTitular = new Persona1();
            DatosTitular.seleccionar = "Identificacion";
            DatosTitular.identificacion = pIdentificacion;
            DatosTitular.noTraerHuella = 1;
            DatosTitular.soloPersona = 1;
            try
            {
                DatosTitular = ServicePersona.ConsultarPersona1Param(DatosTitular, usuario);
                if (DatosTitular.nombre != "errordedatos")
                {
                    if (DatosTitular.estado != "A")
                    {
                        pResult.esCorrecto = false;
                        pResult.mensaje = "El cliente no tiene un estado activo.";
                        return pResult;
                    }
                    //else
                    //{
                    //    pResult.esCorrecto = false;
                    //    pResult.mensaje = "Se generó un error al consultar los datos del Titular";
                    //    return pResult;
                    //}
                }
            }
            catch
            {
                pResult.esCorrecto = false;
                pResult.mensaje = "Se generó un error al consultar los datos del Titular";
                return pResult;
            }

            //CONSULTAR DATOS DE LA AFILIACION
            Xpinn.Aportes.Services.AfiliacionServices ServicesAfiliacion = new Xpinn.Aportes.Services.AfiliacionServices();
            Afiliacion pAfiliacion = new Afiliacion();
            pAfiliacion = ServicesAfiliacion.ConsultarAfiliacion(DatosTitular.cod_persona, usuario);
            if (pForma_pago == 2)
            {
                if (pAfiliacion.empresa_formapago == null || pAfiliacion.empresa_formapago == 0)
                {
                    pResult.esCorrecto = false;
                    pResult.mensaje = "El asociado no se encuentra con una empresa de recaudo válido, verifique los datos de afiliación.";
                    return pResult;
                }
            }
            pAfiliacion.empresa_formapago = pAfiliacion.empresa_formapago == null ? 0 : pAfiliacion.empresa_formapago;
            //Calculando fecha de terminacion
            DateTime? fechaTerminacion = null;

            //PENDIENTE CALCULO DE FECHA PRIMERA CUOTA
            Xpinn.Servicios.Services.DesembolsoServiciosServices ServicesDesembolso = new Xpinn.Servicios.Services.DesembolsoServiciosServices();
            DateTime? pFecIni = ServicesDesembolso.ObtenerFechaInicioServicio(DateTime.Today, pForma_pago, Convert.ToInt64(pAfiliacion.empresa_formapago), pPeriodicidad.ToString(), usuario);

            if (pFecIni == null)
            {
                pResult.esCorrecto = false;
                pResult.mensaje = "No se puedo obtener la fecha de la primera cuota";
                return pResult;
            }

            DateTime pFechaPrimeraCuota = Convert.ToDateTime(pFecIni);
            if (pPeriodicidad != 0 && pFechaPrimeraCuota != DateTime.MinValue && pNumeroCuotas != 0)
            {
                PeriodicidadService periodicidadService = new PeriodicidadService();
                Periodicidad periodicidad = periodicidadService.ConsultarPeriodicidad(pPeriodicidad, usuario);

                int numeroCuotas = pNumeroCuotas - 1;
                DateTime fechaPrimeraCuota = pFechaPrimeraCuota;
                DateTimeHelper dateTimeHelper = new DateTimeHelper();
                fechaTerminacion = dateTimeHelper.SumarDiasSegunTipoCalendario(fechaPrimeraCuota, Convert.ToInt32(Math.Round(periodicidad.numero_dias * numeroCuotas)), Convert.ToInt32(periodicidad.tipo_calendario));
            }

            //Consultando Datos de la linea
            Xpinn.Servicios.Entities.LineaServicios vDetalle = new Xpinn.Servicios.Entities.LineaServicios();
            Xpinn.Servicios.Services.LineaServiciosServices BOLineaServ = new Xpinn.Servicios.Services.LineaServiciosServices();
            try
            {
                vDetalle = BOLineaServ.ConsultarLineaSERVICIO(pCod_linea_servicio, usuario);
            }
            catch
            {
                pResult.esCorrecto = false;
                pResult.mensaje = "Se generó un error al consultar los datos de la linea";
                return pResult;
            }

            //Consultando el proveedor perteneciente a la linea
            Persona1 DatosProveedor = new Persona1();
            DatosProveedor.seleccionar = "Identificacion";
            DatosProveedor.identificacion = vDetalle.identificacion_proveedor;
            DatosProveedor.noTraerHuella = 1;
            DatosProveedor.soloPersona = 1;
            try
            {
                DatosProveedor = ServicePersona.ConsultarPersona1Param(DatosProveedor, usuario);
            }
            catch
            {
                pResult.esCorrecto = false;
                pResult.mensaje = "Se generó un error al consultar los datos del Proveedor, verifique la parametrización en la linea.";
                return pResult;
            }

            try
            {
                Xpinn.Servicios.Entities.Servicio pVar = new Xpinn.Servicios.Entities.Servicio();
                pVar.numero_servicio = 0;
                pVar.fecha_solicitud = DateTime.Today;
                pVar.cod_persona = DatosTitular.cod_persona;
                pVar.cod_linea_servicio = pCod_linea_servicio;
                pVar.cod_plan_servicio = null;
                pVar.fecha_inicio_vigencia = pFechaPrimeraCuota;
                pVar.fecha_final_vigencia = fechaTerminacion;
                pVar.num_poliza = null;

                pVar.valor_total = pValorTotal;
                pVar.fecha_primera_cuota = pFechaPrimeraCuota;
                pVar.numero_cuotas = pNumeroCuotas;
                //Calculada internamente
                pVar.valor_cuota = 0;
                pVar.cod_periodicidad = pPeriodicidad;
                pVar.forma_pago = pForma_pago.ToString();
                pVar.cod_empresa = pForma_pago == 2 ? pAfiliacion.empresa_formapago : null;

                pVar.identificacion_titular = pIdentificacion;
                pVar.nombre_titular = (DatosTitular.tipo_persona == "J") ? DatosTitular.razon_social : DatosTitular.nombres + " " + DatosTitular.apellidos;
                pVar.observaciones = null;

                pVar.saldo = 0;
                pVar.fecha_proximo_pago = DateTime.MinValue;
                pVar.fecha_ultimo_pago = DateTime.MinValue;
                pVar.fecha_aprobacion = DateTime.Today;
                pVar.estado = vDetalle.no_requiere_aprobacion == 1 ? "A" : "S";
                pVar.codigo_proveedor = DatosProveedor.cod_persona;
                pVar.identificacion_proveedor = DatosProveedor.identificacion;
                pVar.nombre_proveedor = (DatosProveedor.tipo_persona == "J") ? DatosProveedor.razon_social : DatosProveedor.nombres + " " + DatosProveedor.apellidos;

                Xpinn.Servicios.Services.SolicitudServiciosServices BOServicios = new Xpinn.Servicios.Services.SolicitudServiciosServices();

                //GRABACION DE LA OPERACION
                Xpinn.Tesoreria.Services.OperacionServices xTesoreria = new Xpinn.Tesoreria.Services.OperacionServices();
                Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
                vOpe.cod_ope = 0;
                vOpe.tipo_ope = 110;
                vOpe.cod_caja = 0;
                vOpe.cod_cajero = 0;
                vOpe.observacion = "Desembolso de Servicio";
                vOpe.cod_proceso = null;
                vOpe.fecha_oper = DateTime.Now;
                vOpe.fecha_calc = DateTime.Now;

                pResult = BOServicios.CrearServicioDesembolsoPorWS(pVar, vOpe, vDetalle.tipo_servicio, pVr_compra, pVr_beneficio, pVr_Mercado, pCodProceso, usuario);
            }
            catch
            {
                pResult.esCorrecto = false;
                pResult.mensaje = "Se presentó un inconveniente al realizar la grabación de la solicitud del servicio.";
                return pResult;
            }
            return pResult;
        }


        [WebMethod]
        public List<LineaCred_Destinacion> ListaDestinacionCredito(string pCod_linea_Credito, string sec)
        {
            LineasCreditoService LineaService = new LineasCreditoService();
            if (string.IsNullOrEmpty(pCod_linea_Credito))
                return null;

            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<LineaCred_Destinacion> lstDestinaciones = LineaService.ListaDestinacionCredito(pCod_linea_Credito, usuario);

            return lstDestinaciones.Count > 0 ? lstDestinaciones : null;
        }

        [WebMethod]
        public Xpinn.FabricaCreditos.Entities.Documento ConsultarSolicitudDoc(string sec)
        {
            Xpinn.FabricaCreditos.Services.DatosDeDocumentoService BOSolicitud = new Xpinn.FabricaCreditos.Services.DatosDeDocumentoService();
            Xpinn.FabricaCreditos.Entities.Documento pEntidad = new Xpinn.FabricaCreditos.Entities.Documento();


            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            pEntidad = BOSolicitud.ConsultarSolicitud(usuario);
            return pEntidad;
        }

        [WebMethod]
        public Xpinn.FabricaCreditos.Entities.Documento CrearDocSolicitud(Xpinn.FabricaCreditos.Entities.Documento pSolicitudCreditoAAC, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            Xpinn.FabricaCreditos.Services.DatosDeDocumentoService BOSolicitud = new Xpinn.FabricaCreditos.Services.DatosDeDocumentoService();
            Xpinn.FabricaCreditos.Entities.Documento pEntidad = new Xpinn.FabricaCreditos.Entities.Documento();

            pEntidad = BOSolicitud.CrearDocSolicitud(pSolicitudCreditoAAC, usuario);
            return pEntidad;
        }


        [WebMethod(Description = "Consulta de Servicio")]
        public Xpinn.Servicios.Entities.Servicio ConsultarServicioWS(long pNumeroServicio, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            if (pNumeroServicio == 0)
                return null;

            Xpinn.Servicios.Services.AprobacionServiciosServices ServiciosServ = new Xpinn.Servicios.Services.AprobacionServiciosServices();
            Xpinn.Servicios.Entities.Servicio pResult = ServiciosServ.ConsultarSERVICIO(pNumeroServicio, usuario);

            return pResult;
        }

        [WebMethod(Description = "Lista de movimientos de un servicio")]
        public List<Xpinn.Servicios.Entities.Servicio> ReporteMovimientoServicio(Xpinn.Servicios.Entities.Servicio pServicio, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            if (pServicio == null)
                return null;
            else
            {
                if (pServicio.Fec_ini == DateTime.MinValue)
                    return null;
                if (pServicio.Fec_fin == DateTime.MinValue)
                    return null;
                if (pServicio.numero_servicio == 0)
                    return null;
            }

            Xpinn.Servicios.Services.AprobacionServiciosServices ServiciosServ = new Xpinn.Servicios.Services.AprobacionServiciosServices();
            List<Xpinn.Servicios.Entities.Servicio> lstConsulta = new List<Xpinn.Servicios.Entities.Servicio>();
            lstConsulta = ServiciosServ.Reportemovimiento(pServicio, usuario);
            return lstConsulta;
        }


        [WebMethod(Description = "Envia correo de prueba")]
        public string EnvioCorreoSolicitudPrueba(long cod_persona, string sec)
        {
            string salida = "";
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.FabricaCreditos.Services.CreditoSolicitadoService BOSolicitud = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();
            Xpinn.FabricaCreditos.Entities.SolicitudCreditoAAC pEntidad = new Xpinn.FabricaCreditos.Entities.SolicitudCreditoAAC();

            try
            {
                bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.SolicitudCredito, usuario, cod_persona);
            }
            catch (Exception ex)
            {
                salida = ex.Message;
            }
            return salida;
        }

        [WebMethod(Description = "Creacion de Solicitud de Crédito / Documentos")]
        public Int64? ConsultarPersonaSolicitudCreditoAAC(string pIdentificacion, string sec)
        {
            // Verificar conexión a la base de datos
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            // Determinar el còdigo de la persona 
            Xpinn.FabricaCreditos.Services.Persona1Service personaSer = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Int64? _cod_persona = personaSer.ConsultarCodigoPersona(pIdentificacion, usuario);
            return _cod_persona;
        }


        //[WebMethod(Description = "Envio de notificaciones a una persona")]
        //public string EnviarNotificacion(Int64 pcod_persona, string pClave)
        //{
        //Conexion conexion = new Conexion();
        //Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
        //usuario = conexion.DeterminarUsuarioOficina(sec);
        //if (usuario == null)
        //return "No pudo determinar el usuario";            
        //try
        //{
        //string resultNotification = ConfigureNotification.PruebaNotifification(usuario, pcod_persona);
        //if (resultNotification.Trim() != "")
        //return "Error al enviar email." + resultNotification;
        //}
        //catch (Exception ex)
        //{
        //return "Error: " + ex.Message;
        //}
        //return "Notificacion. Emvail enviado correctamente";
        //}

    }
}
