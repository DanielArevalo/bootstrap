using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Xpinn.Util;


namespace WebServices
{
    /// <summary>
    /// Descripción breve de WSDeposito
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSDeposito : System.Web.Services.WebService
    {
        Xpinn.Ahorros.Services.AhorroVistaServices ahorrosServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();

        [WebMethod]
        public List<Xpinn.Ahorros.Entities.AhorroVista> ListarCuentasAplicarPagos(string pFiltro, string pClave, string pIdentificacion, string sec)
        {
            Xpinn.Ahorros.Services.AhorroVistaServices BOAhorroVista = new Xpinn.Ahorros.Services.AhorroVistaServices();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<Xpinn.Ahorros.Entities.AhorroVista> lstResult = BOAhorroVista.ListarAhorroVistApPagos(pFiltro, usuario);
            return lstResult;
        }

        [WebMethod]
        public List<Xpinn.Caja.Entities.TipoOperacion> ListarTipoOpeTransacVent(Xpinn.Caja.Entities.TipoOperacion pTipOpe, string pClave, string pIdentificacion, string sec)
        {
            Xpinn.Caja.Services.TipoOperacionService BOTipoTran = new Xpinn.Caja.Services.TipoOperacionService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<Xpinn.Caja.Entities.TipoOperacion> lstResult = BOTipoTran.ListarTipoOpeTransacVent(pTipOpe, usuario);
            return lstResult;
        }

        [WebMethod]
        public Xpinn.Caja.Entities.TipoOperacion ConsultarTipOpeTranCaja(Xpinn.Caja.Entities.TipoOperacion pEntidad, string pClave, string pIdentificacion, string sec)
        {
            Xpinn.Caja.Services.TipoOperacionService BOTipoOperacion = new Xpinn.Caja.Services.TipoOperacionService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            Xpinn.Caja.Entities.TipoOperacion pResult = BOTipoOperacion.ConsultarTipOpeTranCaja(pEntidad, usuario);
            return pResult;
        }


        [WebMethod]
        public List<Xpinn.Programado.Entities.CuentasProgramado> ListarAhorrosProgramado(String pFiltro, DateTime pFechaApe, string pIdentificacion, string pClave, string sec)
        {
            Xpinn.Programado.Services.MovimientoCuentasServices BOMovimiento = new Xpinn.Programado.Services.MovimientoCuentasServices();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            List<Xpinn.Programado.Entities.CuentasProgramado> lstResult = BOMovimiento.ListarAhorrosProgramado(pFiltro, pFechaApe, usuario);
            return lstResult;
        }

        [WebMethod]
        public List<Xpinn.CDATS.Entities.Cdat> ListarCdats(string filtro, DateTime FechaApe, string pIdentificacion, string pClave, string sec)
        {
            Xpinn.CDATS.Services.AperturaCDATService BOCdat = new Xpinn.CDATS.Services.AperturaCDATService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<Xpinn.CDATS.Entities.Cdat> lstResult = BOCdat.ListarCdats(filtro, FechaApe, usuario);
            return lstResult;
        }

        [WebMethod]
        public List<Xpinn.CDATS.Entities.Cdat> ListarFechaVencimiento(Xpinn.CDATS.Entities.Cdat Cdat, string sec)
        {
            Xpinn.CDATS.Services.AperturaCDATService BOCdat = new Xpinn.CDATS.Services.AperturaCDATService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<Xpinn.CDATS.Entities.Cdat> lstResult = BOCdat.ListarFechaVencimiento(Cdat, usuario);
            return lstResult;
        }

        [WebMethod(Description = "APLICACION DE CRUCE DE AHORRO A PRODUCTO POR ATENCION CLIENTE")]
        public Xpinn.Seguridad.Entities.RespuestaApp Aplicar_Solicitud_Pago(List<Xpinn.Ahorros.Entities.Solicitud_cruce_ahorro> lstSolicitud, string pIdentificacion, string pClave, string sec)
        {
            Xpinn.Ahorros.Services.CruceCtaAProductoServices BOAhorro = new Xpinn.Ahorros.Services.CruceCtaAProductoServices();
            Xpinn.Seguridad.Entities.RespuestaApp pRespuesta = new Xpinn.Seguridad.Entities.RespuestaApp();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            Boolean rpta = false;
            string pError = "";
            try
            {
                rpta = BOAhorro.CrearSolicitud_cruce_ahorro(lstSolicitud, ref pError, usuario);
                if (rpta == false)
                {
                    if (pError.Trim() != "")
                        pRespuesta.rpta = false;
                    pRespuesta.Mensaje = pError == "" ? null : pError;
                }
                else
                {
                    pRespuesta.rpta = true;
                    try
                    {
                        bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.SolicitudCruceCooperativa, usuario, null, pIdentificacion);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                pRespuesta.rpta = false;
                pRespuesta.Mensaje = ex.Message;
            }
            return pRespuesta;
        }

        [WebMethod]
        public Xpinn.Seguridad.Entities.RespuestaApp SolicitarRenovacionCdat(List<Xpinn.CDATS.Entities.SolicitudRenovacion> lstRenovacion, string pIdentificacion, string pClave, string sec)
        {
            Xpinn.CDATS.Services.ProrrogaCDATService BOProrroga = new Xpinn.CDATS.Services.ProrrogaCDATService();
            Xpinn.Seguridad.Entities.RespuestaApp pRespuesta = new Xpinn.Seguridad.Entities.RespuestaApp();

            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            Boolean rpta = false;
            string pError = "";
            rpta = BOProrroga.SolicitarRenovacionCdat(lstRenovacion, ref pError, usuario);

            try
            {
                if (rpta == false)
                {
                    if (pError.Trim() != "")
                        pRespuesta.rpta = false;
                    pRespuesta.Mensaje = pError == "" ? null : pError;
                }
                else
                    pRespuesta.rpta = true;
            }
            catch (Exception ex)
            {
                pRespuesta.rpta = false;
                pRespuesta.Mensaje = ex.Message;
            }

            return pRespuesta;
        }


        [WebMethod]
        public List<Xpinn.Ahorros.Entities.AhorroVista> ListarAhorroVistaClubAhorrador(Int64 pCod_persona, string pFiltro, Boolean pSinClubAhorrador, string pIdentificacion, string pClave, string sec)
        {
            Xpinn.Ahorros.Services.ReporteMovimientoServices BOReporte = new Xpinn.Ahorros.Services.ReporteMovimientoServices();
            List<Xpinn.Ahorros.Entities.AhorroVista> lstAhorrros = new List<Xpinn.Ahorros.Entities.AhorroVista>();

            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            lstAhorrros = BOReporte.ListarAhorroVistaClubAhorrador(pCod_persona, pFiltro, pSinClubAhorrador, usuario);
            return lstAhorrros;
        }

        //PENDIENTE DE CULMINAR
        [WebMethod(Description = "Aperturación de una cuenta de Ahorros con datos básicos para algun asociado.")]
        public Xpinn.Seguridad.Entities.RespuestaApp AperturaAhorroVista(string pEntidad, string pIdentificacionTitular, string pCod_linea_ahorro, int pCod_destino,
            int pModalidad, int pCod_Oficina, int pForma_pago, int pCod_empresa, decimal pValor_cuota, int pCod_periodicidad, int pEsExentaGMF, string pClave, int pTipoUsuario)
        {
            Xpinn.Ahorros.Services.CuentasExentasServices ExentaServicio = new Xpinn.Ahorros.Services.CuentasExentasServices();
            Xpinn.Ahorros.Services.AhorroVistaServices AhorroVistaService = new Xpinn.Ahorros.Services.AhorroVistaServices();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioSinClave(pEntidad);
            if (usuario == null)
                return null;

            Xpinn.Seguridad.Entities.RespuestaApp pResult = new Xpinn.Seguridad.Entities.RespuestaApp();

            Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 ePersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            ePersona.seleccionar = "Identificacion";
            ePersona.identificacion = pIdentificacionTitular;
            ePersona.soloPersona = 1;
            try
            {
                ePersona = personaServicio.ConsultarPersona1Param(ePersona, usuario);
                if (ePersona == null)
                    return null;
            }
            catch
            {
                pResult.rpta = false;
                pResult.Mensaje = "Identificación invalida, no está registrada en la entidad.";
            }


            string pError = string.Empty;
            NumeracionCuentasWS BONumeracionCuenta = new NumeracionCuentasWS();
            string autogenerado = BONumeracionCuenta.ObtenerCodigoParametrizado(1, pIdentificacionTitular, ePersona.cod_persona, pCod_linea_ahorro, ref pError, usuario);

            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVista.numero_cuenta = autogenerado != null && !string.IsNullOrEmpty(autogenerado) ? autogenerado : null;
            vAhorroVista.cod_persona = ePersona.cod_persona;
            vAhorroVista.cod_linea_ahorro = pCod_linea_ahorro;
            vAhorroVista.cod_oficina = pCod_Oficina;
            vAhorroVista.cod_destino = pCod_destino;
            vAhorroVista.modalidad = pModalidad;
            vAhorroVista.observaciones = "Apoertura de Ahorro mediante App Inclusión";
            vAhorroVista.estado = 0;
            vAhorroVista.saldo_total = 0;
            vAhorroVista.saldo_canje = 0;
            vAhorroVista.fecha_apertura = DateTime.Now;
            vAhorroVista.fecha_interes = DateTime.Now;
            vAhorroVista.cod_forma_pago = pForma_pago;
            vAhorroVista.valor_cuota = pValor_cuota;
            vAhorroVista.cod_empresa_reca = pCod_empresa;

            try
            {
                vAhorroVista = AhorroVistaService.CrearAhorroVista(vAhorroVista, usuario);
                if (pEsExentaGMF == 1)
                {
                    Xpinn.Ahorros.Entities.CuentasExenta eExenta = new Xpinn.Ahorros.Entities.CuentasExenta();
                    eExenta.idexenta = -1;
                    eExenta.numero_cuenta = vAhorroVista.numero_cuenta;
                    //Codigo de tipo Cuenta Ahorros Vista 3 
                    eExenta.tipo_cuenta = Convert.ToInt32(3);
                    eExenta.fecha_exenta = DateTime.Now;
                    eExenta.monto = Convert.ToDecimal(999999999999);
                    eExenta.cod_persona = Convert.ToInt64(vAhorroVista.cod_persona);
                    eExenta.fecha = DateTime.Now;

                    ExentaServicio.CrearCuentaExentApertura(eExenta, usuario,1);
                }
                pResult.Mensaje = "Su solicitud de ahorro a la vista fue creado exitosamente con el código " + vAhorroVista.numero_cuenta;
                pResult.valorRpta = vAhorroVista.numero_cuenta;
                pResult.rpta = true;
            }
            catch (Exception ex)
            {
                pResult.Mensaje = ex.Message;
                pResult.rpta = false;
            }

            return pResult;
        }

        [WebMethod]
        public Xpinn.Seguridad.Entities.RespuestaApp AplicarOperacion(string pEntidad, DateTime pFechaOperacion, string pIdentificacion, string pTipoOperacion, string pNumReferencia,
            int pModalidad, int pTipoTransaccion, decimal pValor, decimal pValorEfectivo, decimal pValorCheque, decimal pValorOtros, string pClave, int pTipoUsuario)
        {
            Xpinn.Seguridad.Entities.RespuestaApp pResult = new Xpinn.Seguridad.Entities.RespuestaApp();


            return pResult;
        }

        [WebMethod]
        public Xpinn.Ahorros.Entities.SolicitudProductosWeb ConsultarSolicitud(string sec)
        {
            Xpinn.Ahorros.Services.ReporteMovimientoServices BOReporte = new Xpinn.Ahorros.Services.ReporteMovimientoServices();
            Xpinn.Ahorros.Entities.SolicitudProductosWeb pEntidad = new Xpinn.Ahorros.Entities.SolicitudProductosWeb();

            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            pEntidad = BOReporte.ConsultarSolicitud(usuario);
            return pEntidad;
        }

        [WebMethod]
        public Xpinn.Ahorros.Entities.SolicitudProductosWeb CrearSolicitudProduAAC(Xpinn.Ahorros.Entities.SolicitudProductosWeb pSolicitudProductoAAC, string sec)
        {
            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            Xpinn.Ahorros.Services.ReporteMovimientoServices BOReporte = new Xpinn.Ahorros.Services.ReporteMovimientoServices();
            Xpinn.Ahorros.Entities.SolicitudProductosWeb pEntidad = new Xpinn.Ahorros.Entities.SolicitudProductosWeb();

            pEntidad = BOReporte.CrearSolicitudProd(pSolicitudProductoAAC, usuario);
            return pEntidad;
        }
        
        

        [WebMethod]
        public List<Xpinn.Ahorros.Entities.AhorroVista>ListarAprobacionesVista(DateTime fecha, string sec)
        {
            Xpinn.Ahorros.Services.AhorroVistaServices _ahorroservice = new Xpinn.Ahorros.Services.AhorroVistaServices();

            List<Xpinn.Ahorros.Entities.AhorroVista> lstAhorrros = new List<Xpinn.Ahorros.Entities.AhorroVista>();

            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            lstAhorrros = _ahorroservice.ListarAprobaciones(usuario, fecha);
            return lstAhorrros;
        }

        [WebMethod]
        public List<Xpinn.Ahorros.Entities.AhorroVista> ListarAprobacionesCuota(string sec)
        {
            Xpinn.Ahorros.Services.AhorroVistaServices _ahorroservice = new Xpinn.Ahorros.Services.AhorroVistaServices();

            List<Xpinn.Ahorros.Entities.AhorroVista> lstAhorrros = new List<Xpinn.Ahorros.Entities.AhorroVista>();

            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            lstAhorrros = _ahorroservice.ListarAprobacionesCuota(usuario);
            return lstAhorrros;
        }

        [WebMethod]
        public List<Xpinn.Ahorros.Entities.SolicitudProductosWeb> ListaSolicitud(string Filtro, string sec)
        {
            Xpinn.Ahorros.Services.AhorroVistaServices _ahorroservice = new Xpinn.Ahorros.Services.AhorroVistaServices();

            List<Xpinn.Ahorros.Entities.SolicitudProductosWeb> lstAhorrros = new List<Xpinn.Ahorros.Entities.SolicitudProductosWeb>();

            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            lstAhorrros = _ahorroservice.ListarSolicitudCreditoAAC(Filtro,usuario);
            return lstAhorrros;
        }

        [WebMethod]
        public List<Xpinn.Programado.Entities.CuentasProgramado> ListarAprobacionesProgramado(string sec)
        {
            Xpinn.Programado.Services.MovimientoCuentasServices _ahorroservice = new Xpinn.Programado.Services.MovimientoCuentasServices();

            List<Xpinn.Programado.Entities.CuentasProgramado> lstAhorrros = new List<Xpinn.Programado.Entities.CuentasProgramado>();

            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            lstAhorrros = _ahorroservice.ListarAprobaciones(usuario);
            return lstAhorrros;
        }

        [WebMethod]
        public List<Xpinn.CDATS.Entities.Cdat> Listarsimulacion(Xpinn.CDATS.Entities.Cdat vapertura, DateTime FechaApe, string sec)
        {
            Xpinn.CDATS.Services.AperturaCDATService cdatservice = new Xpinn.CDATS.Services.AperturaCDATService();

            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;
            List<Xpinn.CDATS.Entities.Cdat> lstConsulta = new List<Xpinn.CDATS.Entities.Cdat>();

            lstConsulta = cdatservice.Listarsimulacion(vapertura, FechaApe, usuario);

            return lstConsulta;
        }

        [WebMethod]
        public Xpinn.CDATS.Entities.LineaCDAT ConsultarLineaCDAT(string pId, string sec)
        {
            Xpinn.CDATS.Services.LineaCDATService cdatservice = new Xpinn.CDATS.Services.LineaCDATService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            Xpinn.CDATS.Entities.LineaCDAT pResult = cdatservice.ConsultarLineaCDAT(pId, usuario);
            return pResult;
        }

        [WebMethod]
        public List<Xpinn.FabricaCreditos.Entities.TipoTasa> ListarTipoTasa(Xpinn.FabricaCreditos.Entities.TipoTasa vTasa, string sec)
        {
            Xpinn.FabricaCreditos.Services.TipoTasaService TipoTasaService = new Xpinn.FabricaCreditos.Services.TipoTasaService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<Xpinn.FabricaCreditos.Entities.TipoTasa> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.TipoTasa>();

            lstConsulta = TipoTasaService.ListarTipoTasa(vTasa,usuario);

            return lstConsulta;
        }

        [WebMethod]
        public List<Xpinn.FabricaCreditos.Entities.TipoTasaHist> ListarTipoTasaHist(Xpinn.FabricaCreditos.Entities.TipoTasaHist vTasa, string sec)
        {
            Xpinn.FabricaCreditos.Services.TipoTasaHistService TipoTasaService = new Xpinn.FabricaCreditos.Services.TipoTasaHistService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            List<Xpinn.FabricaCreditos.Entities.TipoTasaHist> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.TipoTasaHist>();

            lstConsulta = TipoTasaService.ListarTipoTasaHist(vTasa, usuario);

            return lstConsulta;
        }

        [WebMethod(Description = "CREA LA SOLICITUD PARA EL RETIRO DE AHORROS A LA VISTA")]
        public int CrearSolicitudRetiroAhorro(Xpinn.Ahorros.Entities.AhorroVista ahorro, string sec)
        {
            Xpinn.Ahorros.Services.AhorroVistaServices _ahorroservice = new Xpinn.Ahorros.Services.AhorroVistaServices();
            int solicitud = 0;

            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return 0;

            solicitud = _ahorroservice.CrearSolicitudRetiroAhorros(ahorro, usuario);
            if(solicitud > 0)
            {
                try
                {
                    bool resultNotification = ConfigureNotification.SendNotifification(ProcesoAtencionCliente.SolicitudRetiroAhorros, usuario, ahorro.cod_persona);
                }
                catch (Exception)
                {
                }
            }
            return solicitud;
        }



        [WebMethod]
        public List<Xpinn.Ahorros.Entities.ReporteMovimiento> ListarMovprogramado(string pNumeroAporte, DateTime pfechaInicial, DateTime pfechaFinal, string sec)
        {
            try
            {
                Xpinn.Programado.Services.MovimientoCuentasServices BOProgramado = new Xpinn.Programado.Services.MovimientoCuentasServices();
                Conexion conexion = new Conexion();
                Usuario usuario = new Usuario();
                usuario = conexion.DeterminarUsuarioOficina(sec);
                if (usuario == null)
                    return null;

                List<Xpinn.Ahorros.Entities.ReporteMovimiento> lstMovimientos = new List<Xpinn.Ahorros.Entities.ReporteMovimiento>();
                lstMovimientos = BOProgramado.ListarDetalleMovimiento(pNumeroAporte, pfechaInicial, pfechaFinal, usuario);
                return lstMovimientos;
            }
            catch
            {
                return null;
            }
        }


        [WebMethod]
        public List<Xpinn.Ahorros.Entities.ReporteMovimiento> ListarMovVista(long pNumeroAporte, DateTime pfechaInicial, DateTime pfechaFinal, string sec)
        {
            try
            {
                Xpinn.Ahorros.Services.ReporteMovimientoServices BOVista = new Xpinn.Ahorros.Services.ReporteMovimientoServices();
                Conexion conexion = new Conexion();
                Usuario usuario = new Usuario();
                usuario = conexion.DeterminarUsuarioOficina(sec);
                if (usuario == null)
                    return null;

                List<Xpinn.Ahorros.Entities.ReporteMovimiento> lstMovimientos = new List<Xpinn.Ahorros.Entities.ReporteMovimiento>();
                lstMovimientos = BOVista.ListarReporteMovimiento(pNumeroAporte, pfechaInicial, pfechaFinal, usuario);
                return lstMovimientos;
            }
            catch
            {
                return null;
            }
        }


        [WebMethod]
        public string ObtenerDocCDAT(string codigoCDAT, string apertura, string cierre, string sec)
        {
            Xpinn.Aportes.Services.FormatoDocumentoServices BOFormato = new Xpinn.Aportes.Services.FormatoDocumentoServices();
            string documento = "";

            Conexion conexion = new Conexion();
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            documento = BOFormato.ObtenerDocumentoCDAT(Convert.ToInt64(codigoCDAT),apertura, cierre, usuario);            
            return documento;

        }


    }
}
