using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Services;
using System.Xml.Linq;
using Xpinn.Util;

namespace WebServices
{
    /// <summary>
    /// Descripción breve de WSAppFinancial
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WSAppFinancial : System.Web.Services.WebService
    {
        private Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        private List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        
        [WebMethod(Description = "LISTA DE CREDITOS CON TASAS MAXIMAS - USADO POR SIMULACION EN APP")]
        public List<Xpinn.FabricaCreditos.Entities.LineasCredito> ListarTipoCreditos(string pFiltro, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.FabricaCreditos.Services.LineasCreditoService LineaServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
            List<Xpinn.FabricaCreditos.Entities.LineasCredito> lstTipoCredito = LineaServicio.ListarLineasCreditoTasaInteres(pFiltro, usuario);
            return lstTipoCredito;
        }

        [WebMethod(Description = "RETORNA UNA TASA DE INTERES ESPECIFICA SEGÚN PLAZO, MONTO, FECHA Y USUARIO - USADO POR SIMULACION EN APP")]
        public decimal obtenerTasaEspecifica(string cod_linea_credito, int plazo, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return 0;

            Xpinn.FabricaCreditos.Services.LineasCreditoService LineaServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
            decimal tasaEspecifica = LineaServicio.obtenerTasaInteresEspecifica(cod_linea_credito, plazo, usuario);
            return tasaEspecifica;
        }

        [WebMethod(Description = "LISTA DE PERIODICIDADES")]
        public List<Xpinn.FabricaCreditos.Entities.Periodicidad> ListarPeriodicidades(Xpinn.FabricaCreditos.Entities.Periodicidad pPeriodicidad, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.FabricaCreditos.Services.PeriodicidadService ServicePeriodicidad = new Xpinn.FabricaCreditos.Services.PeriodicidadService();
            List<Xpinn.FabricaCreditos.Entities.Periodicidad> lstPeriodicidad = ServicePeriodicidad.ListarPeriodicidad(pPeriodicidad, usuario);
            return lstPeriodicidad;
        }

        [WebMethod(Description = "LISTA DE MOTIVOS DE RETIRO")]
        public List<Xpinn.FabricaCreditos.Entities.Motivo> ListarMotivosRetiro(Xpinn.FabricaCreditos.Entities.Motivo pMotivo, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.FabricaCreditos.Services.MotivoService ServiceMotivo = new Xpinn.FabricaCreditos.Services.MotivoService();
            List<Xpinn.FabricaCreditos.Entities.Motivo> lstMotivo = ServiceMotivo.ListarMotivosRetiro(pMotivo, usuario);
            return lstMotivo;
        }



        [WebMethod(Description = "LISTA DE BANCOS")]
        public List<Xpinn.Caja.Entities.Bancos> ListarBancos(Xpinn.Caja.Entities.Bancos pBancos, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Caja.Services.BancosService ServiceBanco = new Xpinn.Caja.Services.BancosService();
            List<Xpinn.Caja.Entities.Bancos> lstBancos = ServiceBanco.ListarBancos(pBancos, 0,usuario);
            return lstBancos;
        }


        [WebMethod]
        public List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> ListarEmpresaRecaudo(Int64 pCod_Persona)
        {
            Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();
            List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstEmpresas = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
            Xpinn.Tesoreria.Entities.EmpresaRecaudo empresa = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();

            try
            {
                lstEmpresas = empresaServicio.ListarEmpresaRecaudoPersona(pCod_Persona, usuario);
            }
            catch
            {
                lstEmpresas = empresaServicio.ListarEmpresaRecaudo(empresa, usuario);
            }
            return lstEmpresas;
        }

        [WebMethod]
        public List<Xpinn.FabricaCreditos.Entities.TiposDocumento> ListarTipoDocumentos()
        {
            Xpinn.FabricaCreditos.Services.TiposDocumentoService tipoDocuService = new Xpinn.FabricaCreditos.Services.TiposDocumentoService();
            Xpinn.FabricaCreditos.Entities.TiposDocumento tipoDocu = new Xpinn.FabricaCreditos.Entities.TiposDocumento();
            tipoDocu.tipo = "2";

            List<Xpinn.FabricaCreditos.Entities.TiposDocumento> lstTipo = new List<Xpinn.FabricaCreditos.Entities.TiposDocumento>();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();

            lstTipo = tipoDocuService.ListarTiposDocumento(tipoDocu, usuario);
            return lstTipo;
        }


        [WebMethod]
        public List<Xpinn.FabricaCreditos.Entities.Actividades> ListarTemasInteres()
        {
            Xpinn.FabricaCreditos.Services.ActividadesServices temas = new Xpinn.FabricaCreditos.Services.ActividadesServices();
            List<Xpinn.FabricaCreditos.Entities.Actividades> lstTemas = new List<Xpinn.FabricaCreditos.Entities.Actividades>();

            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();

            lstTemas = temas.listarTemasInteres(usuario);
            return lstTemas;
        }

        /// <summary>
        /// GRABACION DE LA SOLICITUD MEDIANTE EL APP
        /// </summary>
        [WebMethod]
        public Xpinn.Seguridad.Entities.RespuestaApp GrabarSolicitudCredito_APP(string pEntidad, Int64 pCod_Persona, Int64 pMontoSoli, Int64 pPlazo, string pTipoCredito, Int64 pPeriodicidad, int pDestino,
            Int64 pFormaPago, Int64 pEmpresa, Int64 pCod_Oficina, string pObservacion, string pIdentificacion, string pClave, int pTipoUsuario)
        {
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            Conexion conexion = new Conexion();
            usuario = conexion.DeterminarUsuarioSinClave(pEntidad);
            if (usuario == null)
                return null;

            Xpinn.Seguridad.Entities.RespuestaApp Respuesta = new Xpinn.Seguridad.Entities.RespuestaApp();
            Respuesta.rpta = true;
            Xpinn.Seguridad.Services.UsuarioService usuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            if (pTipoUsuario == 2)
            {
                pPersona = usuarioServicio.ValidarPersonaUsuario(pIdentificacion, pClave, usuario);
                if (pPersona.cod_persona == 0)
                    Respuesta.rpta = false;
            }
            else
            {
                usuario = usuarioServicio.ValidarUsuario(pIdentificacion, pClave, "", "", usuario);
                if (usuario.codusuario == 0)
                    Respuesta.rpta = false;
            }
            if (Respuesta.rpta == false)
            {
                Respuesta.Mensaje = "La identificación " + pIdentificacion + " no cuenta con los permisos para realizar esta operación";
                return Respuesta;
            }

            Respuesta = GrabarSolicitudCredito(pCod_Persona, pMontoSoli, pPlazo, pTipoCredito, pPeriodicidad, pDestino, pFormaPago, pEmpresa, pCod_Oficina, pObservacion, usuario);
            //ENVIAR LOS DATOS DE RETORNO
            return Respuesta;
        }

        protected bool ValidarProcesoContable(DateTime pFecha, Int64 pTipoOpe, Usuario usuario)
        {
            Xpinn.Contabilidad.Services.ComprobanteService compServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            List<Xpinn.Contabilidad.Entities.ProcesoContable> lstProceso = new List<Xpinn.Contabilidad.Entities.ProcesoContable>();
            lstProceso = compServicio.ConsultaProceso(0, pTipoOpe, pFecha, usuario);
            if (lstProceso == null)
            {
                return false;
            }
            if (lstProceso.Count <= 0)
            {
                return false;
            }
            return true;
        }

        [WebMethod(Description = "GENERACION DE CREDITO Y DESEMBOLSO")]
        public Xpinn.FabricaCreditos.Entities.CreditoEntity GrabarCredito(string pEntidad, Int64 pCod_Persona, Int64 pMontoSoli, Int64 pPlazo, string pTipoCredito, Int64 pPeriodicidad, int pDestino,
            Int64 pFormaPago, Int64 pEmpresa, Int64 pCod_Oficina, string pObservacion, decimal pVr_compra, decimal pVr_beneficio, decimal pVr_Mercado, string pIdentProveedor, int pCodProceso)
        {
            
            Xpinn.FabricaCreditos.Entities.CreditoEntity Respuesta = new Xpinn.FabricaCreditos.Entities.CreditoEntity();

            bool isDefined = Enum.IsDefined(typeof(ClienteEnt), pEntidad.ToUpper());
            if (!isDefined)
            {
                Respuesta.esCorrecto = false;
                Respuesta.mensaje = "La entidad indicada es inválida.";
                return Respuesta;
            }
            if (pCod_Persona == 0)
            {
                Respuesta.esCorrecto = false;
                Respuesta.mensaje = "El código indicado de la persona es inválido.";
                return Respuesta;
            }
            if (pMontoSoli <= 0)
            {
                Respuesta.esCorrecto = false;
                Respuesta.mensaje = "El monto solicitado no puede ser menor o igual a 0.";
                return Respuesta;
            }
            if (pPlazo == 0)
            {
                Respuesta.esCorrecto = false;
                Respuesta.mensaje = "El plazo indicado no puede ser 0.";
                return Respuesta;
            }
            if (pFormaPago != 1 && pFormaPago != 2)
            {
                Respuesta.esCorrecto = false;
                Respuesta.mensaje = "El campo de la forma de pago es inválido, Usar [ 1 = Caja, 2 = Nómina ].";
                return Respuesta;
            }

            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            Conexion conexion = new Conexion();
            usuario = conexion.DeterminarUsuarioSinClave(pEntidad);
            if (usuario == null)
            {
                Respuesta.esCorrecto = false;
                Respuesta.mensaje = "Se generó un error al realizar la conexion a la Base de datos.";
                return Respuesta;
            }

            // CONSULTAR USUARIO POR DEFECTO
            if (ConfigurationManager.AppSettings["CodUsuario"] == null)
            {
                Respuesta.esCorrecto = false;
                Respuesta.mensaje = "No se configuró el usuario por defecto.";
                return Respuesta;
            }
            string pCodUsuario = ConfigurationManager.AppSettings["CodUsuario"];
            if (string.IsNullOrEmpty(pCodUsuario))
            {
                Respuesta.esCorrecto = false;
                Respuesta.mensaje = "No se configuró el usuario por defecto.";
                return Respuesta;
            }
            Xpinn.Seguridad.Services.UsuarioService ServicesUsuario = new Xpinn.Seguridad.Services.UsuarioService();
            Usuario pUsuDefault = ServicesUsuario.ConsultarUsuario(Convert.ToInt64(pCodUsuario), usuario);
            if (pUsuDefault == null)
            {
                Respuesta.esCorrecto = false;
                Respuesta.mensaje = "Error al consultar al usuario por defecto.";
                return Respuesta;
            }
            usuario.IP = "0.0.0.0";
            usuario.codusuario = pUsuDefault.codusuario;
            usuario.nombre = pUsuDefault.nombre;
            usuario.cod_oficina = pUsuDefault.cod_oficina;

            if (ValidarProcesoContable(DateTime.Now, 1, usuario) == false)
            {
                Respuesta.esCorrecto = false;
                Respuesta.mensaje = "No se encontró parametrización contable por procesos para el tipo de operación 1 = Desembolso de Créditos.";
                return Respuesta;
            }

            try
            {
                if (pEmpresa == 0 && pFormaPago == 2)
                {
                    //CONSULTAR DATOS DE LA AFILIACION
                    Xpinn.Aportes.Services.AfiliacionServices ServicesAfiliacion = new Xpinn.Aportes.Services.AfiliacionServices();
                    Xpinn.FabricaCreditos.Entities.Afiliacion pAfiliacion = new Xpinn.FabricaCreditos.Entities.Afiliacion();
                    pAfiliacion = ServicesAfiliacion.ConsultarAfiliacion(pCod_Persona, usuario);
                    pEmpresa = pAfiliacion.empresa_formapago == null ? 0 : Convert.ToInt64(pAfiliacion.empresa_formapago);
                }


                Xpinn.FabricaCreditos.Services.CreditoPlanService creditoPlanServicio = new Xpinn.FabricaCreditos.Services.CreditoPlanService();
                Xpinn.Seguridad.Entities.RespuestaApp pResultSoli = new Xpinn.Seguridad.Entities.RespuestaApp();
                pResultSoli = GrabarSolicitudCredito(pCod_Persona, pMontoSoli, pPlazo, pTipoCredito, pPeriodicidad, pDestino, pFormaPago, pEmpresa, pCod_Oficina, pObservacion, usuario, pIdentProveedor);
                //ENVIAR LOS DATOS DE RETORNO
                if (pResultSoli.rpta)
                {
                    Xpinn.FabricaCreditos.Entities.CreditoPlan credito = new Xpinn.FabricaCreditos.Entities.CreditoPlan();
                    credito.NumeroSolicitud = Convert.ToInt64(pResultSoli.valorRpta);
                    Xpinn.FabricaCreditos.Entities.CreditoEntity creditoResult = creditoPlanServicio.LiquidarWS(credito, pDestino, true, pVr_compra, pVr_beneficio, pVr_Mercado, pCodProceso, usuario, true);

                    Respuesta.esCorrecto = creditoResult.esCorrecto;
                    if (creditoResult.esCorrecto)
                    {
                        Respuesta.mensaje = "Su crédito quedó radicado con el número  " + creditoResult.numero_radicacion;
                        Respuesta.numero_radicacion = creditoResult.numero_radicacion;
                        Respuesta.cod_ope = creditoResult.cod_ope;
                        Respuesta.num_comp = creditoResult.num_comp;
                        Respuesta.tipo_comp = creditoResult.tipo_comp;
                    }
                    else
                        Respuesta.mensaje = creditoResult.mensaje;
                }
            }
            catch (Exception ex)
            {
                Respuesta.esCorrecto = false;
                Respuesta.mensaje = ex.Message;
                return Respuesta;
            }
            return Respuesta;
        }

        public Xpinn.Seguridad.Entities.RespuestaApp GrabarSolicitudCredito(Int64 pCod_Persona, Int64 pMontoSoli, Int64 pPlazo, string pTipoCredito, Int64 pPeriodicidad,
            int pDestino, Int64 pFormaPago, Int64 pEmpresa, Int64 pCod_Oficina, string pObservacion, Xpinn.Util.Usuario usuario, string pIdentProveedor = null)
        {
            Xpinn.Seguridad.Entities.RespuestaApp Respuesta = new Xpinn.Seguridad.Entities.RespuestaApp();
            string sError = "";
            Xpinn.FabricaCreditos.Services.DatosSolicitudService DatosSolicitudServicio = new Xpinn.FabricaCreditos.Services.DatosSolicitudService();
            Xpinn.FabricaCreditos.Entities.DatosSolicitud datosSolicitud = new Xpinn.FabricaCreditos.Entities.DatosSolicitud();

            try
            {
                Xpinn.FabricaCreditos.Services.Persona1Service ServicePersona = new Xpinn.FabricaCreditos.Services.Persona1Service();
                Xpinn.FabricaCreditos.Entities.Persona1 datosPersona;
                
                // CONSULTANDO LINEA
                Xpinn.FabricaCreditos.Entities.LineasCredito eLinea = new Xpinn.FabricaCreditos.Entities.LineasCredito();
                Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
                eLinea = LineaCreditoServicio.ConsultarLineasCredito(pTipoCredito, usuario);


                // GENERANDO VALIDACION DE PROVEEDOR EN CASO LA LINEA MANEJE ORDEN DE SERVICIO
                datosSolicitud.identificacionprov = "";
                datosSolicitud.nombreprov = null;
                if (eLinea.orden_servicio == 1)
                {
                    if (string.IsNullOrEmpty(pIdentProveedor))
                    {
                        Respuesta.rpta = false;
                        Respuesta.Mensaje = "No se puede realizar la grabación de la solicitud debido a que la identificación del proveedor es inválida.";
                        return Respuesta;
                    }
                    else
                    {
                        datosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
                        datosPersona.seleccionar = "Identificacion";
                        datosPersona.identificacion = pIdentProveedor;
                        datosPersona.noTraerHuella = 1;
                        datosPersona.soloPersona = 1;
                        datosPersona = ServicePersona.ConsultarPersona1Param(datosPersona, usuario);
                        if (datosPersona.nombre == "errordedatos")
                        {
                            Respuesta.rpta = false;
                            Respuesta.Mensaje = "No se puede realizar la grabación de la solicitud debido a que la identificación del proveedor es inválida.";
                            return Respuesta;
                        }
                        else
                        {
                            datosSolicitud.identificacionprov = datosPersona.identificacion;
                            datosSolicitud.nombreprov = datosPersona.tipo_persona == "N" ? datosPersona.nombres + " " + datosPersona.apellidos : datosPersona.razon_social;
                        }
                    }
                }
                
                // RECUPERANDO PARAMETRO DE USUARIO
                if (ConfigurationManager.AppSettings["CodUsuario"] == null)
                {
                    Respuesta.rpta = false;
                    Respuesta.Mensaje = "No se configuró el usuario por defecto.";
                    return Respuesta;
                }
                string pCodUsuario = ConfigurationManager.AppSettings["CodUsuario"];
                if (string.IsNullOrEmpty(pCodUsuario))
                {
                    Respuesta.rpta = false;
                    Respuesta.Mensaje = "No se configuró el usuario por defecto.";
                    return Respuesta;
                }
                Xpinn.Seguridad.Services.UsuarioService ServicesUsuario = new Xpinn.Seguridad.Services.UsuarioService();
                Usuario pUsuDefault = ServicesUsuario.ConsultarUsuario(Convert.ToInt64(pCodUsuario), usuario);
                if (pUsuDefault == null)
                {
                    Respuesta.rpta = false;
                    Respuesta.Mensaje = "Error al consultar al usuario por defecto.";
                    return Respuesta;
                }
                usuario.IP = "0.0.0.0";
                usuario.codusuario = pUsuDefault.codusuario;
                usuario.cod_oficina = pUsuDefault.cod_oficina;

                // Determinando datos de la solicitud
                datosSolicitud.numerosolicitud = 0;
                datosSolicitud.fechasolicitud = DateTime.Now;
                datosSolicitud.cod_cliente = Convert.ToString(pCod_Persona);
                datosSolicitud.cod_persona = Convert.ToInt64(pCod_Persona);
                datosSolicitud.montosolicitado = pMontoSoli;
                datosSolicitud.plazosolicitado = pPlazo;
                datosSolicitud.cuotasolicitada = 0;
                datosSolicitud.tipocrdito = pTipoCredito;
                datosSolicitud.periodicidad = pPeriodicidad;
                //datosSolicitud.medio = pMedio;
                datosSolicitud.destino = pDestino;
                datosSolicitud.otro = null;
                datosSolicitud.concepto = !string.IsNullOrEmpty(pObservacion) ? pObservacion : "Generado por la APP O Web Services Boleteria el " + DateTime.Now.ToShortDateString() ;
                datosSolicitud.forma_pago = pFormaPago;
                datosSolicitud.garantia = 0;
                datosSolicitud.garantia_comunitaria = 0;
                datosSolicitud.poliza = 0;
                datosSolicitud.tipo_liquidacion = Convert.ToInt64(eLinea.tipo_liquidacion.ToString());

                if (datosSolicitud.forma_pago == 2)
                    datosSolicitud.empresa_recaudo = pEmpresa;
                else
                    datosSolicitud.empresa_recaudo = null;

                datosSolicitud.cod_oficina = pCod_Oficina;

                datosSolicitud.cod_usuario = pUsuDefault.codusuario;
                
                // Validar los datos de la solicitud                
                datosSolicitud = DatosSolicitudServicio.ValidarSolicitud(datosSolicitud, usuario, ref sError);
                if (sError.Trim() != "")
                {
                    if (sError.Contains("ORA-20101"))
                    {
                        Respuesta.Mensaje = "No se pudieron validar datos de la solicitud. " + sError.Substring(10);
                    }
                    else
                    {
                        Respuesta.Mensaje = "No se pudieron validar datos de la solicitud. Error:" + sError;
                    }
                    Respuesta.rpta = false;
                    Respuesta.valorRpta = "";
                }
                else if (datosSolicitud.mensaje.Trim() != "")
                {
                    Respuesta.valorRpta = "";
                    Respuesta.Mensaje = datosSolicitud.mensaje;
                    Respuesta.rpta = false;
                }
                else
                {
                    // Grabar datos de la solicitud
                    datosSolicitud = DatosSolicitudServicio.CrearSolicitud(datosSolicitud, usuario);
                    Respuesta.valorRpta = datosSolicitud.numerosolicitud.ToString();
                    Respuesta.Mensaje = "Su solicitud de crédito quedó radicado con el número  " + datosSolicitud.numerosolicitud;
                    Respuesta.rpta = true;
                }
            }
            catch (Exception ex)
            {
                Respuesta.Mensaje = ex.Message;
                Respuesta.rpta = false;
                Respuesta.valorRpta = "";
            }
            return Respuesta;
        }

        /// <summary>
        /// ULTIMOS MOVIMIENTOS POR PERSONA
        /// </summary>
        [WebMethod(Description = "LISTA DE MOVIMIENTOS POR PERSONA. APP")]
        public List<Xpinn.Tesoreria.Entities.AnulacionOperaciones> UltimosMovimientosXpersona(Int64 pCod_persona)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            Xpinn.Tesoreria.Services.OperacionServices OperacionServicio = new Xpinn.Tesoreria.Services.OperacionServices();
            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Cod_persona";
            ePersona.cod_persona = pCod_persona;
            ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
            if (ePersona == null)
                return null;

            List<Xpinn.Tesoreria.Entities.AnulacionOperaciones> lstMovimientos = new List<Xpinn.Tesoreria.Entities.AnulacionOperaciones>();
            lstMovimientos = OperacionServicio.ListarUltimosMovimientosXpersona(pCod_persona, usuario);

            return lstMovimientos;
        }


        /// <summary>
        /// Consulta Simulación Cuota
        /// </summary>
        [WebMethod(Description = "ELIMINAR SI NO SE USA EN LA APP, se usa el de credito")]
        public Xpinn.Seguridad.Entities.RespuestaApp ConsultarSimulacionCuota(int monto, int plazo, int periodicidad, string cod_cred, decimal tasa, long pCod_Persona)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            string error = "";

            Xpinn.FabricaCreditos.Entities.Simulacion Simulacion = new Xpinn.FabricaCreditos.Entities.Simulacion();
            Xpinn.FabricaCreditos.Services.SimulacionService BOSimulacion = new Xpinn.FabricaCreditos.Services.SimulacionService();
            Xpinn.Seguridad.Entities.RespuestaApp Respuesta = new Xpinn.Seguridad.Entities.RespuestaApp();
            try
            {
                //TipoLiquidacion, Tasa, Comision, Aporte por defecto en 0
                Simulacion = BOSimulacion.ConsultarSimulacionCuota(monto, plazo, periodicidad, cod_cred, 0, 0, 0, 0, null, ref error, usuario, pCod_Persona);
                if (Simulacion != null)
                    Respuesta.valorRpta = Simulacion.cuota.ToString("n0");
                else
                    Respuesta.valorRpta = "0";
                if (error == "")
                    Respuesta.rpta = true;
                else
                    Respuesta.rpta = false;
                Respuesta.Mensaje = error;
            }
            catch (Exception ex)
            {
                Respuesta.Mensaje = "No se pudo calcular la cuota. " + ex.Message;
                Respuesta.rpta = false;
                Respuesta.valorRpta = "0";
            }
            return Respuesta;
        }



        [WebMethod]
        public List<Xpinn.Asesores.Entities.Oficina> ListarDireccionesOficinas()
        {
            Xpinn.Asesores.Services.ParametricaService OficinaData = new Xpinn.Asesores.Services.ParametricaService();
            Xpinn.Asesores.Entities.Oficina Oficina = new Xpinn.Asesores.Entities.Oficina();
            Oficina.Estado = 1;
            List<Xpinn.Asesores.Entities.Oficina> lstOficinas = new List<Xpinn.Asesores.Entities.Oficina>();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();

            lstOficinas = OficinaData.ListarDireccionesOficinas(Oficina, usuario);
            return lstOficinas;
        }

        [WebMethod(Description = "DATOS DE CONTACTENOS USADOS EN EL APP")]
        public Xpinn.Asesores.Entities.EntEmpresa ConsultarEmpresa()
        {
            Xpinn.Asesores.Services.ParametricaService OficinaData = new Xpinn.Asesores.Services.ParametricaService();
            Xpinn.Asesores.Entities.EntEmpresa vEmpresa = new Xpinn.Asesores.Entities.EntEmpresa();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();

            vEmpresa = OficinaData.ConsultarEmpresa("", usuario);
            return vEmpresa;
        }

        [WebMethod(Description = "DATOS PARA GENERAR GRÁFICO. APP")]
        public List<Xpinn.FabricaCreditos.Entities.Credito> ConsultarResumenPersona(Int64 pCodPersona, string sec)
        {
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            Conexion conexion = new Conexion();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<Xpinn.FabricaCreditos.Entities.Credito> lstResult = null;
            try
            {
                lstResult = DatosClienteServicio.ConsultarResumenPersona(pCodPersona, usuario);
            }
            catch
            {
            }
            return lstResult;
        }

        [WebMethod(Description = "ENVIO DE CODIGO DE ACTIVACION PARA REGISTRO Y RECUPERACION CLAVE. WEB APPLICACION - APP")]
        public bool EnvioCodigoActivacion(string[] pObject, ProcesoAtencionCliente pProceso)
        {
            try
            {
                // RECUPERANDO DATOS DEL CORREO SERVER
                string URLWebServices = ConfigurationManager.AppSettings["URLWebServices"] as string;
                string pNameCompany = ConfigurationManager.AppSettings["NameCompany"] != null ? ConfigurationManager.AppSettings["NameCompany"].ToString() : "Atención al Cliente";
                string correoApp = ConfigurationManager.AppSettings["CorreoServidor"] as string;
                string claveCorreoApp = ConfigurationManager.AppSettings["Clave"] as string;
                string hosting = ConfigurationManager.AppSettings["Hosting"] as string;
                int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"].ToString());
                string UrlBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Imagenes", "logoEmpresa.jpg");

                string pSubtitle = pProceso == ProcesoAtencionCliente.RecuperarClave ? "Código de recuperación" : "Código de activación";
                string pSubject = pProceso == ProcesoAtencionCliente.RecuperarClave ? "Código de recuperación" : "CREACIÓN DE USUARIO - Código de Activación";
                // RECUPERANDO DATOS DE HTML
                string fileName = "CorreoConfirmacion.txt";
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", fileName);
                string htmlCorreo = File.ReadAllText(path);

                CorreoHelper correoHelper = new CorreoHelper(pObject[0], correoApp, claveCorreoApp);
                
                htmlCorreo = htmlCorreo.Replace("@_SUBTITLE_@", pSubtitle);
                htmlCorreo = htmlCorreo.Replace("@_URLBASE_@", UrlBase);
                htmlCorreo = htmlCorreo.Replace("@_USUARIO_@", pObject[1]);
                htmlCorreo = htmlCorreo.Replace("@_CODIGO_ALEATORIO_@", pObject[2]);

                string salida = "";
                bool exitoso = correoHelper.sendEmail(htmlCorreo,out salida, pSubject);
                return exitoso;
            }
            catch //(Exception e)
            {
                return false;
            }
        }

        [WebMethod(Description = "ENVIO DE CODIGO DE ACTIVACION PARA REGISTRO Y RECUPERACION CLAVE. WEB APPLICACION - APP")]
        public string TestMetod(string[] pObject, ProcesoAtencionCliente pProceso)
        {
            string salida = "";
            try
            {
                // RECUPERANDO DATOS DEL CORREO SERVER
                string URLWebServices = ConfigurationManager.AppSettings["URLWebServices"] as string;
                string pNameCompany = ConfigurationManager.AppSettings["NameCompany"] != null ? ConfigurationManager.AppSettings["NameCompany"].ToString() : "Atención al Cliente";
                string correoApp = ConfigurationManager.AppSettings["CorreoServidor"] as string;
                string claveCorreoApp = ConfigurationManager.AppSettings["Clave"] as string;
                string hosting = ConfigurationManager.AppSettings["Hosting"] as string;
                int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"].ToString());
                string UrlBase = Path.Combine(URLWebServices, "Files", "Imagenes", "logoEmpresa.jpg");

                string pSubtitle = pProceso == ProcesoAtencionCliente.RecuperarClave ? "Código de recuperación" : "Código de activación";
                string pSubject = pProceso == ProcesoAtencionCliente.RecuperarClave ? "Código de recuperación" : "CREACIÓN DE USUARIO - Código de Activación";
                // RECUPERANDO DATOS DE HTML
                string fileName = "CorreoConfirmacion.txt";
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", fileName);
                string htmlCorreo = File.ReadAllText(path);

                CorreoHelper correoHelper = new CorreoHelper(pObject[0], correoApp, claveCorreoApp);

                htmlCorreo = htmlCorreo.Replace("@_SUBTITLE_@", pSubtitle);
                htmlCorreo = htmlCorreo.Replace("@_URLBASE_@", UrlBase);
                htmlCorreo = htmlCorreo.Replace("@_USUARIO_@", pObject[1]);
                htmlCorreo = htmlCorreo.Replace("@_CODIGO_ALEATORIO_@", pObject[2]);

                salida += "URLWebServices"+ URLWebServices + " - ";
                salida += "pNameCompany" + pNameCompany + " - ";
                salida += "correoApp" + correoApp + " - ";
                salida += "claveCorreoApp" + claveCorreoApp + " - ";
                salida += "hosting" + hosting + " - ";
                salida += "puerto" + puerto + " - ";
                salida += "UrlBase" + URLWebServices + " - ";
                string outMetod = "";
                bool exitoso = correoHelper.sendEmail(htmlCorreo, out outMetod, pSubject);
                salida += outMetod;
                return salida;
            }
            catch (Exception e)
            {
                return salida + ":App:" +e.Message;
            }
        }

        [WebMethod(Description = "LISTA DE LINEAS CDATS")]
        public List<Xpinn.CDATS.Entities.LineaCDAT> ListarLineasCDAT(Xpinn.CDATS.Entities.LineaCDAT pLinea, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.CDATS.Services.LineaCDATService ServiceCDAT = new Xpinn.CDATS.Services.LineaCDATService();
            List<Xpinn.CDATS.Entities.LineaCDAT> lstLineasCDAT = ServiceCDAT.ListarLineaCDAT(pLinea, usuario);
            return lstLineasCDAT;
        }


        [WebMethod(Description = "LISTA DE LINEAS AHORRO A LA VISTA")]
        public List<Xpinn.Ahorros.Entities.LineaAhorro> ListarLineasAhorros(Xpinn.Ahorros.Entities.LineaAhorro pLinea, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Ahorros.Services.LineaAhorroServices ServiceAhorro = new Xpinn.Ahorros.Services.LineaAhorroServices();
            List<Xpinn.Ahorros.Entities.LineaAhorro> lstLineasAhorros = ServiceAhorro.ListarLineaAhorro(pLinea, usuario);
            return lstLineasAhorros;
        }

        [WebMethod(Description = "LISTA DE LINEAS AHORRO PROGRAMADO")]
        public List<Xpinn.Programado.Entities.LineasProgramado> ListarLineasAhoProgramado(string filtro, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Programado.Services.LineasProgramadoServices LineaProgramadoServices = new Xpinn.Programado.Services.LineasProgramadoServices();
            List<Xpinn.Programado.Entities.LineasProgramado> lstLineasProgramado = LineaProgramadoServices.ListarLineasProgramado(filtro, usuario);
            return lstLineasProgramado;
        }

        [WebMethod(Description = "LISTA DE DESTINACIONES")]
        public List<Xpinn.Ahorros.Entities.Destinacion> ListarDestinacion(Xpinn.Ahorros.Entities.Destinacion pDestinacion, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Ahorros.Services.DestinacionServices DestinacionServices = new Xpinn.Ahorros.Services.DestinacionServices();
            List<Xpinn.Ahorros.Entities.Destinacion> lstDestinacion = DestinacionServices.ListarDestinacion(pDestinacion, usuario);
            return lstDestinacion;
        }


        [WebMethod(Description = "VALIDA Y CREA LA SOLICITUD DE CDAT")]
        public string GrabarSolicitudCDAT(Xpinn.CDATS.Entities.Cdat CDAT, string sec, List<Xpinn.FabricaCreditos.Entities.DocumentosAnexos> lstDocumentos = null)
        {
            string respuesta = "OK";
                        
            // RECUPERANDO PARAMETRO DE USUARIO
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            // Validar los datos de la solicitud
            Xpinn.CDATS.Services.AperturaCDATService CDATService = new Xpinn.CDATS.Services.AperturaCDATService();
            //Espacio para realizar las vaalidaciones de datos necesarias retornando "OK"
            //sError = CDATService.ValidarDatosSolicitud(CDAT, usuario);

            if(respuesta == "OK")
            {
                respuesta = CDATService.CrearSolicitudCDAT(CDAT, usuario, lstDocumentos);
            }
            if(respuesta != null)
            {
                try
                {
                    bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.SolicitudCDAT, usuario, CDAT.cod_persona);
                }
                catch (Exception)
                {
                }
            }
            return respuesta;
        }


        [WebMethod(Description = "VALIDA Y CREA LA SOLICITUD DE AHORRO A LA VISTA")]
        public string GrabarSolicitudAhorros(Xpinn.Ahorros.Entities.AhorroVista Ahorros, string sec)
        {
            string respuesta = "OK";
            // RECUPERANDO PARAMETRO DE USUARIO
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            // Validar los datos de la solicitud
            Xpinn.Ahorros.Services.AhorroVistaServices AhorroService = new Xpinn.Ahorros.Services.AhorroVistaServices();
            //Espacio para realizar las vaalidaciones de datos necesarias retornando "OK"

            if (respuesta == "OK")
            {
                respuesta = AhorroService.CrearSolicitudAhorrosVista(Ahorros, usuario);
            }
            if(respuesta != null)
            {
                try
                {
                    bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.SolicitudAhorros, usuario, Ahorros.cod_persona);
                }
                catch (Exception)
                {
                }
            }
            return respuesta;
        }

        [WebMethod(Description = "VALIDA Y CREA LA SOLICITUD DE AHORRO PROGRAMADO")]
        public string GrabarSolicitudAhorroProgramado(Xpinn.Programado.Entities.CuentasProgramado AhoProgra, string sec)
        {
            string respuesta = "OK";
            // RECUPERANDO PARAMETRO DE USUARIO
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            // Validar los datos de la solicitud
            Xpinn.Programado.Services.CuentasProgramadoServices CuentasProgramadoService = new Xpinn.Programado.Services.CuentasProgramadoServices();
            //Espacio para realizar las vaalidaciones de datos necesarias retornando "OK"

            if (respuesta == "OK")
            {
                respuesta = CuentasProgramadoService.CrearSolicitudAhorroProgramado(AhoProgra, usuario);
            }
            if(respuesta != null)
            {
                try
                {
                    bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.SolicitudAhorroProgramado, usuario, AhoProgra.cod_persona);
                }
                catch (Exception)
                {
                }
            }
            return respuesta;
        }

        [WebMethod(Description = "LISTA DE SERVICIOS")]
        public List<Xpinn.Servicios.Entities.LineaServicios> ListarServicios(Xpinn.Servicios.Entities.LineaServicios linea ,string pFiltro, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Servicios.Services.LineaServiciosServices LineaServicio = new Xpinn.Servicios.Services.LineaServiciosServices();
            List<Xpinn.Servicios.Entities.LineaServicios> lstServicios = LineaServicio.ListarLineaServicios(linea, usuario, pFiltro);
            return lstServicios;
        }

        [WebMethod(Description = "LISTA DE SERVICIOS")]
        public List<Xpinn.FabricaCreditos.Entities.LineaCred_Destinacion> ListarConvenios(Xpinn.FabricaCreditos.Entities.LineaCred_Destinacion destina, string pFiltro, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.FabricaCreditos.Services.DestinacionService LineaServicio = new Xpinn.FabricaCreditos.Services.DestinacionService();
            List<Xpinn.FabricaCreditos.Entities.LineaCred_Destinacion> lstDestina = LineaServicio.ListarLineaCred_Destinacion(destina, pFiltro , usuario);
            return lstDestina;
        }

        [WebMethod(Description = "VALIDA Y CREA LA SOLICITUD DE SERVICIO")]
        public Xpinn.Servicios.Entities.Servicio GrabarSolicitudServicio(Xpinn.Servicios.Entities.Servicio servicio, string sec)
        {
            // RECUPERANDO PARAMETRO DE USUARIO
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.Servicios.Services.SolicitudServiciosServices servicioService = new Xpinn.Servicios.Services.SolicitudServiciosServices();
            servicio = servicioService.CrearSolicitudServicioOficinaVirtual(servicio, usuario);
            if(servicio != null)
            {
                try
                {
                    bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.SolicitudServicio, usuario, servicio.cod_persona);
                }
                catch (Exception)
                {

                }
            }
            return servicio;
        }

        [WebMethod(Description = "Obtiene el html y titulo de una sección informativa")]
        public Xpinn.Seguridad.Entities.Contenido obtenerContenido(Int64 id, string sec)
        {
            Xpinn.Seguridad.Entities.Contenido content = new Xpinn.Seguridad.Entities.Contenido();
            Xpinn.Seguridad.Services.ContenidoService BOContenido = new Xpinn.Seguridad.Services.ContenidoService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            return BOContenido.ObtenerContenido(id, usuario);
        }    
    }
}
