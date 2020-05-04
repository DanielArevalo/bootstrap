using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Xpinn.Aportes.Entities;
using Xpinn.Asesores.Entities;
using Xpinn.Caja.Services;
using Xpinn.Comun.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Tesoreria.Services;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
    Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
    int tipoOpe = 20;
    DateTime FechaAct = DateTime.Now;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[AporteServicio.ProgramaCruce + ".id"] != null)
                VisualizarOpciones(AporteServicio.ProgramaCruce, "E");
            else
                VisualizarOpciones(AporteServicio.ProgramaCruce, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarImprimir(false);
            toolBar.MostrarExportar(true);
            ctlBusquedaPersonas.eventoEditar += gvListaTitulares_SelectedIndexChanged;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            txtFecharetiro.eventoCambiar += txtFechaRetiro_TextChanged;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
            // Inicializar variables de session
            InicializarVariablesSession();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "Page_PreInit", ex);
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            //   ExportToExcel(gvLista);


            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.AllowPaging = false;
            gvLista.PageSize = 1500;
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Cruce.xls");
            Response.Charset = "UTF-8";
            Response.Write(sb.ToString());
            Response.End();

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                panelDatos.Enabled = false;
                panelGrilla.Visible = false;
                txtFecharetiro.Texto = DateTime.Now.ToShortDateString();
                MvAfiliados.ActiveViewIndex = 0;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                chkDistribuir_CheckedChanged(chkDistribuir, null);

                TipoOperacionService tipoService = new TipoOperacionService();
                Xpinn.Caja.Entities.TipoProducto producto = tipoService.ConsultarTipoProducto(new Xpinn.Caja.Entities.TipoProducto() { cod_tipo_producto = 2 }, Usuario);
                ViewState["tipoProductoCredito"] = producto;

                if (Session[AporteServicio.ProgramaCruce + ".id"] != null)
                {
                    idObjeto = Session[AporteServicio.ProgramaCruce + ".id"].ToString();
                    Session.Remove(AporteServicio.ProgramaCruce + ".id");
                    ObtenerDatos(idObjeto);
                    toolBar.MostrarConsultar(false);
                    MvAfiliados.ActiveViewIndex = 1;
                }
                else
                {
                    idObjeto = null;
                }

                if (Session["solicitudRetiro"] != null)
                {
                    Aporte solicitud = Session["solicitudRetiro"] as Aporte;                    
                    cargarDatosPersona(Convert.ToInt64(solicitud.cod_persona), solicitud.tipo_persona, solicitud.identificacion, solicitud.Nombres, solicitud.Apellidos, Convert.ToInt32(solicitud.tipo_identificacion));
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaCruce, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        PoblarListas Poblar = new PoblarListas();
        try
        {
            Poblar.PoblarListaDesplegable("MOTIVO_RETIRO", DdlMotRetiro, (Usuario)Session["usuario"]);
            Poblar.PoblarListaDesplegable("TIPOIDENTIFICACION", "*", "", "1", ddlTipoIdentificacion, (Usuario)Session["usuario"]);
            ctlGiro.Inicializar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaCruce, "CargarListas", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");
        ctlBusquedaPersonas.Filtro = "";
        ctlBusquedaPersonas.Actualizar(0);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["solicitudRetiro"] != null)
        {
            Session["solicitudRetiro"] = null;
            Response.Redirect("../../Aportes/ConfirmarSolicitudRetiro/Lista.aspx", false);
        }
        else
        {
            Navegar(Pagina.Lista);
        }
    }

    protected void txtFechaRetiro_TextChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            Actualizar();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected Boolean ValidarDatos()
    {
        if (txtCod_persona.Text == "")
        {
            VerError("No existe un código de la persona, verifique los datos por favor.");
            return false;
        }
        if (txtFecharetiro.Texto == "")
        {
            VerError("Ingrese la fecha de retiro.");
            txtFecharetiro.Focus();
            return false;
        }
        if (DdlMotRetiro.SelectedIndex <= 0)
        {
            VerError("Seleccione el motivo del retiro a generar.");
            DdlMotRetiro.Focus();
            return false;
        }

        if (ddltipo_cruce.SelectedIndex <= 0)
        {
            VerError("Seleccione Tipo pago del retiro a Generar.");
            ddltipo_cruce.Focus();
            return false;
        }
        if (panelGrilla.Visible == false)
        {
            VerError("No existen datos por cruzar, verifique los datos.");
            return false;
        }
        if (txtsaldo.Text == "")
        {
            VerError("No se puede realizar la grabación, verifique el saldo calculado");
            return false;
        }

        //Validando datos del control de Giro
        if (ctlGiro.IndiceFormaDesem == 0)
        {
            VerError("Seleccione una forma de desembolso");
            return false;
        }
        else
        {
            if (ctlGiro.IndiceFormaDesem == 2 || ctlGiro.IndiceFormaDesem == 3)
            {
                if (ctlGiro.IndiceEntidadOrigen == 0)
                {
                    VerError("Seleccione un Banco de donde se girará");
                    return false;
                }
                if (ctlGiro.IndiceFormaDesem == 3)
                {
                    if (ctlGiro.IndiceEntidadDest == 0)
                    {
                        VerError("Seleccione la Entidad de destino");
                        return false;
                    }
                    if (ctlGiro.TextNumCuenta == "")
                    {
                        VerError("Ingrese el número de la cuenta");
                        return false;
                    }
                }
            }
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ValidarDatos())
            ctlMensaje.MostrarMensaje("Desea realizar la grabación de los datos");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(FechaAct, tipoOpe) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 20 = Retiro de Aportes");
            return;
        }
        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!panelProceso.Visible && panelGeneral.Visible)
        {
            rpta = ctlproceso.Inicializar(tipoOpe, FechaAct, (Usuario)Session["Usuario"]);
            if (rpta > 1)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                // Activar demás botones que se requieran
                panelGeneral.Visible = false;
                panelProceso.Visible = true;

                if (Session["solicitudRetiro"] != null)
                {
                    Aporte sol = Session["solicitudRetiro"] as Aporte;
                    sol.estado_modificacion = "1"; // Aprueba Solicitud
                    AporteServicio.ModificarEstadoSolicitud(sol, (Usuario)Session["usuario"]);
                    Session["solicitudRetiro"] = null;
                }                
            }
            else
            {

                String estado = "";
                DateTime fechacierrehistorico;
                String formato = gFormatoFecha;
                DateTime Fecharetiro = Convert.ToDateTime(txtFecharetiro.Texto); // DateTime.ParseExact(FechaAct.ToShortDateString(), formato, CultureInfo.InvariantCulture);

                Xpinn.Aportes.Entities.Aporte vaportes = new Xpinn.Aportes.Entities.Aporte();
                vaportes = AporteServicio.ConsultarCierreAportes((Usuario)Session["usuario"]);
                estado = vaportes.estadocierre;
                fechacierrehistorico = Convert.ToDateTime(vaportes.fecha_cierre.ToString());

                if (estado == "D" && Fecharetiro <= fechacierrehistorico)
                {
                    VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO A,'APORTES'");
                    return;
                }
                else
                {
                    // Crear la tarea de ejecución del proceso                
                    if (AplicarDatos())
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }
            }
        }
    }

    protected bool AplicarDatos()
    {
        try
        {
            Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
            Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
            Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
            Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();

            // CREAR OPERACION
            pOperacion.cod_ope = 0;
            pOperacion.tipo_ope = 20;
            pOperacion.cod_caja = 0;
            pOperacion.cod_cajero = 0;
            pOperacion.observacion = "Operacion-Cruce Cuentas";
            pOperacion.cod_proceso = null;
            pOperacion.fecha_oper = Convert.ToDateTime(FechaAct.ToShortDateString());
            pOperacion.fecha_calc = DateTime.Now;
            pOperacion.cod_ofi = Usuario.cod_oficina;

            // GENERAR EL CRUCE --- DATOS PARA TRAN_AHORRO
            Aporte aportes = new Aporte();
            aportes.idretiro = 222;
            aportes.tipo_cruce = cbTipoCruce.Checked ? 1 : 0;
            aportes.cod_persona = Convert.ToInt64(txtCod_persona.Text);
            aportes.cod_motivo = Convert.ToInt64(DdlMotRetiro.SelectedValue);
            aportes.fecha_retiro = Convert.ToDateTime(txtFecharetiro.Texto);
            aportes.fecha_ultima_mod = Convert.ToDateTime(txtFecharetiro.Texto);
            aportes.acta = txtActa.Text != "" ? txtActa.Text : null;
            if (txtObservaciones.Text != "")
                aportes.observaciones = txtObservaciones.Text;
            else
                aportes.observaciones = null;

            aportes.cod_usuario = Convert.ToInt32(Usuario.codusuario);
            aportes.Saldo = Convert.ToDecimal(txtsaldo.Text.Replace(gSeparadorMiles, ""));

            //GRABACION DEL GIRO A REALIZAR
            Usuario pusu = Usuario;
            Xpinn.FabricaCreditos.Entities.Giro pGiro = ctlGiro.ObtenerEntidadGiro(Convert.ToInt64(txtCod_persona.Text), Convert.ToDateTime(FechaAct.ToShortDateString()), aportes.Saldo, pusu);

            ////DATOS DE LA DISTRIBUCION DEL GIRO
            //List<Credito_Giro> lstGiro = new List<Credito_Giro>();
            //bool pGrabarCreditoGiro = chkDistribuir.Checked ? true : false;
            //if (pGrabarCreditoGiro == true)
            //    lstGiro = ObtenerListaDistribucion();

            //GRABAR EL CRUCE DE CUENTAS
            List<Aporte> lstData = (List<Aporte>)Session["DTCRUCE"];
            List<DetalleCruce> lsDetalles = new List<DetalleCruce>();
            DetalleCruce objDetalle;
            foreach (Aporte objAporte in lstData)
            {
                objDetalle = new DetalleCruce();
                objDetalle.Cod_persona = Convert.ToInt64(txtCod_persona.Text);
                objDetalle.Numero_Producto = objAporte.numproducto.ToString();
                objDetalle.Linea_Producto = objAporte.linea_producto;
                objDetalle.Concepto = objAporte.concepto;
                objDetalle.Capital = objAporte.capital;
                objDetalle.Interes_rendimiento = objAporte.interes;
                objDetalle.Interes_Mora = objAporte.intmora;
                objDetalle.Otros = objAporte.otros;
                objDetalle.Retencion = objAporte.retencion;
                objDetalle.Signo = objAporte.signo;
                objDetalle.Interes_Causado = objAporte.interes_causado;
                objDetalle.Retencion_Causada = objAporte.rentecioncausada;
                objDetalle.Saldo = objAporte.saldo_cruce;
                objDetalle.Total = objAporte.total;
                lsDetalles.Add(objDetalle);
            }

            aportes.tipo_pago_cruce = Convert.ToInt32(ddltipo_cruce.SelectedValue.ToString());

            Aporte entidadRetorno = AporteServicio.CrearCruceCuentas(aportes, pOperacion, pGiro, lsDetalles, Usuario);

            CobroCodeudorService cobroCodeudorService = new CobroCodeudorService();
            List<Xpinn.Tesoreria.Entities.CobroCodeudor> lstCobro = RecorrerGrilla();

            foreach (var cobro in lstCobro)
            {
                cobroCodeudorService.CrearCobroCodeudor(cobro, Usuario);
            }


            #region Interfaz WorkManagement


            // Realizar la interfaz con WM
            // Parametro general para habilitar proceso de WM
            General parametroHabilitarOperacionesWM = ConsultarParametroGeneral(35);
            if (parametroHabilitarOperacionesWM != null && parametroHabilitarOperacionesWM.valor.Trim() == "1")
            {
                bool operacionFueExitosa = false;

                try
                {
                    string identificacion = txtIdentificacion.Text;
                    InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);
                    string radicado = interfaz.ConsultarRadicadoPorIdentificacionDeFormularioCorrespondeciaRecibida(identificacion);

                    if (!string.IsNullOrWhiteSpace(radicado))
                    {
                        string observacionesWM = " Cruce de Cuentas del Asociado: " + txtIdentificacion.Text;

                        // RunTask, corre al siguiente proceso, debes identificar en el proceso que estas y añadir las observaciones
                        operacionFueExitosa = interfaz.RunTaskWorkFlowRetiroAsociado(radicado, StepsWorkManagementWorkFlowRetiroAsociados.CruceDeCuentas, observacionesWM);

                        string pNomUsuario = ConstruirNombreReporte();
                        byte[] byteReporte = ConstruirReporte(pNomUsuario, lstData);

                        rvReporte.Visible = true;
                        WorkFlowFilesDTO file = new WorkFlowFilesDTO
                        {
                            Base64DataFile = Convert.ToBase64String(byteReporte),
                            Descripcion = "Documento de Cruce de Cuentas",
                            Extension = ".pdf"
                        };
                        rvReporte.Visible = false;

                        bool anexoExitoso = interfaz.AnexarArchivoAFormularioRetiro(file, radicado, TipoArchivoWorkManagement.CruceCuentas);

                        if (anexoExitoso)
                        {
                            // RunTask, corre al siguiente proceso, debes identificar en el proceso que estas y añadir las observaciones
                            operacionFueExitosa = interfaz.RunTaskWorkFlowRetiroAsociado(radicado, StepsWorkManagementWorkFlowRetiroAsociados.CargaDocumentosCruceDeCuentas, observacionesWM);

                            WorkManagementServices workManagementService = new WorkManagementServices();
                            WorkFlowCruceCuentas workFlowCruceCuentas = new WorkFlowCruceCuentas
                            {
                                barcode = radicado,
                                codigoidgiro = pGiro.idgiro > 0 ? pGiro.idgiro : default(int?),
                                codigopersona = pGiro.cod_persona
                            };
                            workFlowCruceCuentas = workManagementService.CrearWorkFlowCruceCuentas(workFlowCruceCuentas, Usuario);

                            Session[ComprobanteServicio.CodigoPrograma + ".radicadoCruceCuentas"] = radicado;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }


            #endregion


            // Generar el comprobante
            if (pOperacion.cod_ope != 0)
            {

                Session[ComprobanteServicio.CodigoPrograma + ".idgiro"] = pGiro.idgiro.ToString();
                ctlproceso.CargarVariables(pOperacion.cod_ope, tipoOpe, ConvertirStringToInt(txtCod_persona.Text.ToString().Trim()), (Usuario)Session["usuario"]);
                return true;
            }
            else
            {
                VerError("No se generó la operación");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            return false;
            //BOexcepcion.Throw(AporteServicio.ProgramaCruce, "AplicarDatos", ex);
        }

        return false;
    }

    List<Xpinn.Tesoreria.Entities.CobroCodeudor> RecorrerGrilla()
    {
        List<Xpinn.Tesoreria.Entities.CobroCodeudor> lstCobro = new List<Xpinn.Tesoreria.Entities.CobroCodeudor>();

        foreach (GridViewRow row in gvLista.Rows)
        {
            ctlListarCodigo control = row.FindControl("ctlListarCodeudores") as ctlListarCodigo;
            if (control == null || control.Visible == false || string.IsNullOrWhiteSpace(control.Codigo)) continue;

            Xpinn.Tesoreria.Entities.CobroCodeudor cobro = new Xpinn.Tesoreria.Entities.CobroCodeudor();

            cobro.numero_radicacion = Convert.ToInt64(row.Cells[1].Text);
            cobro.cod_deudor = Convert.ToInt64(txtCod_persona.Text);
            cobro.fecha_cobro = txtFecharetiro.ToDateTime;
            cobro.cod_persona = Convert.ToInt64(control.HiddenValue);
            cobro.porcentaje = 100;
            cobro.valor = Convert.ToDecimal(row.Cells[10].Text);
            cobro.cod_empresa = null;
            cobro.fechacrea = DateTime.Today;
            cobro.cod_usuario = Usuario.codusuario;
            cobro.estado = 0;

            lstCobro.Add(cobro);
        }


        return lstCobro;
    }

    protected void gvListaTitulares_SelectedIndexChanged(object sender, EventArgs e)
    {        
        // Determinar la identificacion
        GridView gvListaAFiliados = (GridView)sender;
        Int64 cod_persona = Convert.ToInt64(gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[1].Text);
        String tipo_persona = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[2].Text;
        String identificacion = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[3].Text;
        String nombre = "", apellido = "";
        Int32 TipoIdent = Convert.ToInt32(gvListaAFiliados.DataKeys[gvListaAFiliados.SelectedRow.RowIndex].Values[1].ToString());

        if (tipo_persona == "Natural")
        {
            nombre = (gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[5].Text.Replace("&nbsp;", "").Trim() + " " +
                     gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[6].Text.Replace("&nbsp;", "").Trim()).Trim();
            apellido = (gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[7].Text.Replace("&nbsp;", "").Trim() + " " +
                       gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[8].Text.Replace("&nbsp;", "").Trim()).Trim();
        }
        else
        {
            nombre = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[9].Text.Replace("&nbsp;", "").Trim();
        }
        cargarDatosPersona(cod_persona, tipo_persona, identificacion, nombre, apellido, TipoIdent);        
    }

    public void cargarDatosPersona(Int64 cod_persona, String tipo_persona, String identificacion, String nombre, String apellido,Int32 TipoIdent)
    {
        Persona1Service DatosPersona = new Persona1Service();
        VerError("");

        Persona1 pPersona = new Persona1();
        if (cod_persona != 0)
        {
            pPersona = DatosPersona.ConsultaDatosPersona(cod_persona, (Usuario)Session["usuario"]);

            if (pPersona.cod_persona != 0)
                txtCod_persona.Text = pPersona.cod_persona.ToString();
            if (pPersona.identificacion != null)
                txtIdentificacion.Text = pPersona.identificacion;
            if (tipo_persona == "Natural")
            {
                if (pPersona.nombres != null)
                    txtNombres.Text = pPersona.nombres;
                if (pPersona.apellidos != null)
                    txtApellidos.Text = pPersona.apellidos;
            }
            else
            {
                txtNombres.Text = nombre;
            }
            if (pPersona.tipo_identificacion != 0)
                ddlTipoIdentificacion.SelectedValue = pPersona.tipo_identificacion.ToString();

            Actualizar();
            GridView tex = (GridView)ListaSaldos.FindControl("gvLista");
            if (tex.Rows.Count > 0)
            {
                VerError("La persona cuenta con productos (CDATS y/o DEVOLUCIONES) sin cerrar. ");
            }

            MvAfiliados.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            Boolean bTienePermisoGuardar = true;
            // Determinar la opción actual            
            Xpinn.Seguridad.Entities.Acceso accesos = new Xpinn.Seguridad.Entities.Acceso();
            if (Session["accesos"] != null)
            {
                List<Xpinn.Seguridad.Entities.Acceso> lstAccesos = new List<Xpinn.Seguridad.Entities.Acceso>();
                lstAccesos = (List<Xpinn.Seguridad.Entities.Acceso>)Session["accesos"];

                foreach (Xpinn.Seguridad.Entities.Acceso ent in lstAccesos)
                    if (ent.cod_opcion == Convert.ToInt64(AporteServicio.ProgramaCruce))
                        accesos = ent;
            }
            if (accesos != null)
                if (accesos.insertar != 1)
                    bTienePermisoGuardar = false;
            if (bTienePermisoGuardar) toolBar.MostrarGuardar(true);
            toolBar.MostrarConsultar(false);

            #region Interfaz WorkManagement

            // Realizar la interfaz con WM
            // Parametro general para habilitar proceso de WM
            General parametroHabilitarOperacionesWM = ConsultarParametroGeneral(35);
            if (parametroHabilitarOperacionesWM != null && parametroHabilitarOperacionesWM.valor.Trim() == "1")
            {
                try
                {
                    string identificacionValidar = txtIdentificacion.Text;
                    InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);
                    string radicado = interfaz.ConsultarRadicadoPorIdentificacionDeFormularioCorrespondeciaRecibida(identificacionValidar);

                    if (string.IsNullOrWhiteSpace(radicado))
                    {
                        VerError("WorkManagement: No se encontro el radicado para el retiro de este asociado, si prosigues no se guardara en el WorkManagement!.");
                    }
                }
                catch (Exception)
                {
                    VerError("WorkManagement: Ocurrio un error al buscar el radicado para el retiro de este asociado, si prosigues no se guardara en el WorkManagement!.");
                }
            }
            #endregion

            if (Session["solicitudRetiro"] != null)
            {
                Aporte solicitud = Session["solicitudRetiro"] as Aporte;
                DdlMotRetiro.SelectedValue = solicitud.cod_motivo.ToString();
                txtObservaciones.Text = solicitud.observaciones;
            }
        }
        else
        {
            VerError("No se encontraron datos de las persona");
        }
    }                              
                                   
    private void Actualizar()
    {
        Site toolBar = (Site)this.Master;
        try
        {
            VerError("");

            bool bConsulta = false;
            if (Session[AporteServicio.ProgramaCruce + ".consulta"] != null)
                if (Session[AporteServicio.ProgramaCruce + ".consulta"].ToString() == "1")
                    bConsulta = true;

            List<Aporte> lstConsulta = new List<Aporte>();
            Xpinn.Asesores.Services.PersonaService objPersonaService = new Xpinn.Asesores.Services.PersonaService();
            Xpinn.Asesores.Entities.Persona objPersona = new Xpinn.Asesores.Entities.Persona();
            objPersona = objPersonaService.ConsultarPersona(Convert.ToInt64(txtCod_persona.Text), (Usuario)Session["usuario"]);

            if (objPersona.Estado != "Retirado" && !bConsulta)
            {
                Aporte aporte = new Aporte();
                aporte.fecha_retiro = Convert.ToDateTime(txtFecharetiro.Texto);
                aporte.cod_persona = Convert.ToInt64(txtCod_persona.Text);
                aporte.tipo_cruce = cbTipoCruce.Checked ? 1 : 0;
                aporte.tipo_pago_cruce = ddltipo_cruce.SelectedIndex == 0 ? 3 : Convert.ToInt32(ddltipo_cruce.SelectedValue.ToString());
                lstConsulta = AporteServicio.ListarCruceCuentas(aporte, (Usuario)Session["usuario"]);
                ListaSaldos.Actualizar(txtCod_persona.Text);
                if (AporteServicio.EsAfiancol(Convert.ToInt64(aporte.cod_persona), (Usuario)Session["usuario"]))
                {
                    cbTipoCruce.Enabled = false;
                    cbTipoCruce.Checked = true;
                    ddltipo_cruce.SelectedValue = "2";
                    ddltipo_cruce.Enabled = false;
                }
            }
            else
            {
                if (objPersona.Estado == "Retirado")
                {
                    VerError("La persona se encuentra en estado " + objPersona.Estado);
                    toolBar.MostrarGuardar(false);
                }
                List<DetalleCruce> lsDetalles = AporteServicio.ListarDetalleCruceCuenta(Convert.ToInt64(txtCod_persona.Text), (Usuario)Session["usuario"]);
                foreach (DetalleCruce objDetalle in lsDetalles)
                {
                    Aporte objAporte = new Aporte();
                    objAporte.cod_persona = objDetalle.Cod_persona;
                    objAporte.numproducto = Convert.ToInt64(objDetalle.Numero_Producto.ToString());
                    objAporte.linea_producto = objDetalle.Linea_Producto;
                    objAporte.concepto = objDetalle.Concepto;
                    objAporte.capital = objDetalle.Capital;
                    objAporte.interes = objDetalle.Interes_rendimiento;
                    objAporte.intmora = objDetalle.Interes_Mora;
                    objAporte.otros = objDetalle.Otros;
                    objAporte.retencion = objDetalle.Retencion;
                    objAporte.signo = objDetalle.Signo;
                    objAporte.interes_causado = objDetalle.Interes_Causado;
                    objAporte.rentecioncausada = objDetalle.Retencion_Causada;
                    objAporte.saldo_cruce = objDetalle.Saldo;
                    objAporte.total = objDetalle.Total;
                    lstConsulta.Add(objAporte);
                }
            }

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                toolBar.MostrarImprimir(true);
                panelGrilla.Visible = true;
                gvLista.DataBind();
                Session["DTCRUCE"] = lstConsulta;

                decimal valor_pendiente = (decimal)(from v in lstConsulta where v.signo.Equals("-") select v.saldo_cruce).Sum();
                decimal valor = (decimal)(from v in lstConsulta where v.signo.Equals("-") select v.total).Sum();
                decimal sumar = (decimal)(from s in lstConsulta where s.signo.Equals("+") select s.total).Sum();

                decimal saldo = sumar - valor - valor_pendiente;
                txtsaldo.Text = saldo.ToString("n0");
                if (saldo > 0)
                    lblSaldo.Text = "Saldo a favor";
                else
                    lblSaldo.Text = "Saldo en contra";

                txtsaldo.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                txtTotal.Text = (sumar - valor - valor_pendiente).ToString("n0");
                txtTotal.Attributes.CssStyle.Add("TEXT-ALIGN", "right");
                if (Convert.ToDecimal(txtsaldo.Text) < 1)
                {
                    ctlGiro.IndiceFormaDesem = 1;
                    ctlGiro.Visible = false;
                }
            }
            else
            {
                toolBar.MostrarImprimir(false);
                panelGrilla.Visible = false;
                Session["DTCRUCE"] = null;
            }

            InicializarDistribucion();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Aporte aporte = new Aporte();

            aporte = AporteServicio.ConsultarPersonaRetiro(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (aporte.cod_persona != null)
            {
                txtCod_persona.Text = aporte.cod_persona.ToString();
                if (aporte.identificacion != null)
                    txtIdentificacion.Text = aporte.identificacion.ToString();
                if (aporte.tipo_identificacion != null)
                    ddlTipoIdentificacion.SelectedValue = aporte.tipo_identificacion;
                if (aporte.Nombres != null)
                    txtNombres.Text = aporte.Nombres.Trim();
                if (aporte.Apellidos != null)
                    txtApellidos.Text = aporte.Apellidos.Trim();
                if (aporte.idretiro != 0)
                    txtIdRetiro.Text = aporte.idretiro.ToString();
                if (aporte.fecha_retiro != DateTime.MinValue)
                    txtFecharetiro.Texto = aporte.fecha_retiro.ToShortDateString();
                if (aporte.cod_motivo != 0)
                    DdlMotRetiro.SelectedValue = aporte.cod_motivo.ToString();
                if (aporte.acta != null)
                    txtActa.Text = aporte.acta;
                if (aporte.descripcion != null)
                    txtObservaciones.Text = aporte.descripcion;

                Actualizar();

                ////RECUPERAR DATOS DEL GIRO
                ActividadesServices ActividadServicio = new ActividadesServices();
                List<CuentasBancarias> LstCuentasBanc = new List<CuentasBancarias>();
                Int64 cod = Convert.ToInt64(txtCod_persona.Text);
                string filtro = " and Principal = 1";
                LstCuentasBanc = ActividadServicio.ConsultarCuentasBancarias(cod, filtro, (Usuario)Session["usuario"]);

                ctlGiro.ValueFormaDesem = "1";
                if (LstCuentasBanc.Count > 0)
                {
                    ctlGiro.ValueFormaDesem = "3";
                    if (LstCuentasBanc[0].cod_banco != null && LstCuentasBanc[0].cod_banco != 0)
                        ctlGiro.ValueEntidadDest = LstCuentasBanc[0].cod_banco.ToString();
                    if (LstCuentasBanc[0].numero_cuenta != null && LstCuentasBanc[0].numero_cuenta != "")
                        ctlGiro.TextNumCuenta = LstCuentasBanc[0].numero_cuenta;
                    if (LstCuentasBanc[0].tipo_cuenta != null && LstCuentasBanc[0].tipo_cuenta != 0)
                    {
                        try
                        {
                            ctlGiro.ValueTipoCta = LstCuentasBanc[0].tipo_cuenta.ToString();
                        }
                        catch { }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaCruce, "ObtenerDatos", ex);
        }
    }

    Int64 total = 0;
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridViewRow row = e.Row;

        if (row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToInt64(DataBinder.Eval(row.DataItem, "total"));

            Xpinn.Caja.Entities.TipoProducto productoCredito = (Xpinn.Caja.Entities.TipoProducto)ViewState["tipoProductoCredito"];

            string concepto = row.Cells[3].Text;
            if (string.IsNullOrWhiteSpace(concepto)) return;

            if (HttpUtility.HtmlDecode(concepto).ToUpperInvariant() == productoCredito.descripcion.ToUpperInvariant())
            {
                string saldoPendiente = row.Cells[10].Text;
                if (string.IsNullOrWhiteSpace(saldoPendiente)) return;

                decimal saldo = Convert.ToDecimal(saldoPendiente);
                if (saldo <= 0) return;

                string numeroCredito = row.Cells[1].Text;
                if (string.IsNullOrWhiteSpace(numeroCredito)) return;

                CreditoService creditoService = new CreditoService();
                List<Cliente> listaCodeudores = creditoService.ListarCodeudores(Convert.ToInt64(numeroCredito), Usuario);
                if (listaCodeudores == null || listaCodeudores.Count == 0) return;

                ctlListarCodigo control = row.FindControl("ctlListarCodeudores") as ctlListarCodigo;
                if (control == null) return;

                control.TextField = "NombreYApellido";
                control.ValueField = "NumeroDocumento";
                control.HiddenField = "IdCliente";

                control.Visible = control.BindearControl(listaCodeudores); // Si lo logro bindear lo muestra visible
            }
        }

        if (row.RowType == DataControlRowType.Footer)
        {
            row.Cells[7].Text = "Total:";
            row.Cells[9].Text = total.ToString("n0");
            row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            row.Font.Bold = true;
        }
        Session["total"] = total;
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void txtAseguradora_TextChanged(object sender, EventArgs e)
    {
        List<Aporte> lstConsulta = new List<Aporte>();
        if (Session["DTCRUCE"] != null)
        {
            if (lstConsulta.Count > 0)
            {
                lstConsulta = (List<Aporte>)Session["DTCRUCE"];
                decimal valor = (decimal)(from v in lstConsulta where v.signo.Equals("-") select v.total).Sum();
                decimal sumar = (decimal)(from s in lstConsulta where s.signo.Equals("+") select s.total).Sum();
                decimal aseguradora = ConvertirStringToDecimal(txtAseguradora.Text);
                txtsaldo.Text = (sumar - valor + aseguradora).ToString();
            }
        }
    }

    #region distribucion del giro

    protected void txtIdentificacionD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            TextBoxGrid txtIdentificacion = (TextBoxGrid)sender;
            int rowIndex = Convert.ToInt32(txtIdentificacion.CommandArgument);

            Label lblcod_persona = (Label)gvDistribucion.Rows[rowIndex].FindControl("lblcod_persona");
            TextBoxGrid txtNombre = (TextBoxGrid)gvDistribucion.Rows[rowIndex].FindControl("txtNombre");
            if (txtIdentificacion.Text != "")
            {
                DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdentificacion.Text, (Usuario)Session["usuario"]);

                if (DatosPersona.cod_persona != 0)
                {
                    if (lblcod_persona != null)
                        lblcod_persona.Text = DatosPersona.cod_persona.ToString();
                    if (DatosPersona.nombre != null)
                        txtNombre.Text = DatosPersona.nombre;
                }
                else
                    lblcod_persona.Text = "";
            }
            else
                lblcod_persona.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoPrograma, "txtIdentificacion_TextChanged", ex);
        }
    }

    protected List<Credito_Giro> ObtenerListaDistribucion()
    {
        try
        {
            List<Credito_Giro> lstDetalle = new List<Credito_Giro>();
            List<Credito_Giro> lista = new List<Credito_Giro>();

            foreach (GridViewRow rfila in gvDistribucion.Rows)
            {
                Credito_Giro eBene = new Credito_Giro();

                Label lblCodigo = (Label)rfila.FindControl("lblCodigo");
                if (lblCodigo != null)
                    eBene.idgiro = Convert.ToInt32(lblCodigo.Text);
                else
                    eBene.idgiro = -1;

                Label lblCod_persona = (Label)rfila.FindControl("lblCod_persona");
                if (lblCod_persona.Text != "")
                    eBene.cod_persona = Convert.ToInt64(lblCod_persona.Text);

                TextBoxGrid txtIdentificacionD = (TextBoxGrid)rfila.FindControl("txtIdentificacionD");
                if (txtIdentificacionD.Text != "")
                    eBene.identificacion = txtIdentificacionD.Text;

                TextBoxGrid txtNombre = (TextBoxGrid)rfila.FindControl("txtNombre");
                if (txtNombre.Text != "")
                    eBene.nombre = txtNombre.Text;

                DropDownListGrid ddlTipo = (DropDownListGrid)rfila.FindControl("ddlTipo");
                eBene.tipo = Convert.ToInt32(ddlTipo.SelectedValue);

                decimalesGridRow txtValor = (decimalesGridRow)rfila.FindControl("txtValor");
                if (txtValor.Text.Trim() != "")
                    eBene.valor = Convert.ToDecimal(txtValor.Text);

                lista.Add(eBene);
                Session["Distribucion"] = lista;

                if (eBene.identificacion != null && eBene.nombre != null && eBene.valor != null)
                {
                    lstDetalle.Add(eBene);
                }
            }
            return lstDetalle;
        }
        catch
        {
            return null;
        }
    }

    protected void InicializarDistribucion()
    {

        List<Credito_Giro> lstDistribucion = new List<Credito_Giro>();
        for (int i = gvDistribucion.Rows.Count; i < 2; i++)
        {
            Credito_Giro pDetalle = new Credito_Giro();
            pDetalle.idgiro = -1;
            pDetalle.cod_persona = null;
            pDetalle.identificacion = "";
            pDetalle.nombre = "";
            pDetalle.valor = null;
            pDetalle.tipo = 0;
            lstDistribucion.Add(pDetalle);
        }
        gvDistribucion.DataSource = lstDistribucion;
        gvDistribucion.DataBind();

        Session["Distribucion"] = lstDistribucion;
    }

    protected void btnAdicionarFila_Click(object sender, EventArgs e)
    {
        ObtenerListaDistribucion();

        List<Credito_Giro> LstPrograma = new List<Credito_Giro>();
        if (Session["Distribucion"] != null)
        {
            LstPrograma = (List<Credito_Giro>)Session["Distribucion"];

            for (int i = 1; i <= 1; i++)
            {
                Credito_Giro pDetalle = new Credito_Giro();
                pDetalle.idgiro = -1;
                pDetalle.cod_persona = null;
                pDetalle.identificacion = "";
                pDetalle.nombre = "";
                pDetalle.valor = null;
                pDetalle.tipo = 0;
                LstPrograma.Add(pDetalle);
            }
            gvDistribucion.DataSource = LstPrograma;
            gvDistribucion.DataBind();

            Session["Distribucion"] = LstPrograma;
            CalculaTotalXColumna();
        }
    }


    protected void gvDistribucion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvDistribucion.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaDistribucion();

        List<Credito_Giro> LstDetalle = new List<Credito_Giro>();
        LstDetalle = (List<Credito_Giro>)Session["Distribucion"];
        if (conseID > 0)
        {
            try
            {
                foreach (Credito_Giro acti in LstDetalle)
                {
                    if (acti.idgiro == conseID)
                    {
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstDetalle.RemoveAt((gvDistribucion.PageIndex * gvDistribucion.PageSize) + e.RowIndex);
        }
        Session["Distribucion"] = LstDetalle;

        gvDistribucion.DataSource = LstDetalle;
        gvDistribucion.DataBind();
        CalculaTotalXColumna();
    }

    protected void gvDistribucion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlTipo = (DropDownListGrid)e.Row.FindControl("ddlTipo");
            if (ddlTipo != null)
            {
                ddlTipo.Items.Insert(0, new ListItem("Asociado", "0"));
                ddlTipo.Items.Insert(1, new ListItem("Tercero", "1"));
                ddlTipo.SelectedIndex = 0;
                ddlTipo.DataBind();

                Label lblTipo = (Label)e.Row.FindControl("lblTipo");
                if (lblTipo.Text != "")
                    ddlTipo.SelectedValue = lblTipo.Text;
            }

        }
    }

    protected void chkDistribuir_CheckedChanged(object sender, EventArgs e)
    {
        panelDistribucion.Visible = chkDistribuir.Checked ? true : false;
    }


    protected void ddltipo_cruce_CheckedChanged(object sender, EventArgs e)
    {
        Actualizar();
    }
    protected void CalculaTotalXColumna()
    {
        lblErrorDist.Text = "";
        decimal Fvalor = 0, MontoAprobado = 0;
        Label lblTotalVr = (Label)gvDistribucion.FooterRow.FindControl("lblTotalVr");

        foreach (GridViewRow rfila in gvDistribucion.Rows)
        {
            decimalesGridRow txtValor = (decimalesGridRow)rfila.FindControl("txtValor");
            if (txtValor.Text != "")
                Fvalor += Convert.ToDecimal(txtValor.Text.Replace(gSeparadorMiles, ""));
        }
        if (lblTotalVr != null)
            lblTotalVr.Text = Fvalor.ToString("c0");
        MontoAprobado = txtsaldo.Text != "" ? Convert.ToDecimal(txtsaldo.Text.Replace(gSeparadorMiles, "")) : 0;
        if (Fvalor > MontoAprobado)
        {
            lblErrorDist.Text = "Error al ingresar los datos, El monto total de las distribuciones no puede superar el saldo del cruce";
            return;
        }
    }

    #endregion 

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            // Aquí va la función que hace lo que se requiera grabar en la funcionalidad
            AplicarDatos();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void cbTipoCruce_CheckedChanged(object sender, EventArgs e)
    {
        Actualizar();
    }

    private void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        try
        {
            if (Session["DTCRUCE"] == null)
            {
                VerError("No existen datos en el detalle de cruce de cuentas a generar");
                return;
            }

            List<Aporte> lstData = new List<Aporte>();
            lstData = (List<Aporte>)Session["DTCRUCE"];
            if (lstData.Count > 0)
            {
                string pNomUsuario = ConstruirNombreReporte();
                ConstruirReporte(pNomUsuario, lstData);

                //MOSTRANDO REPORTE
                string adjuntar = "<object data=\"{0}\" type=\"application/pdf\" width=\"90%\" height=\"700px\">";
                adjuntar += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                adjuntar += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                adjuntar += "</object>";

                ltReport.Text = string.Format(adjuntar, ResolveUrl("Archivos/" + pNomUsuario + ".pdf"));
                rvReporte.Visible = false;

                Site toolBar = (Site)Master;
                toolBar.MostrarImprimir(false);
                toolBar.MostrarGuardar(false);
                toolBar.MostrarCancelar(false);
                MvAfiliados.ActiveViewIndex = 2;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            throw;
        }
    }

    private string ConstruirNombreReporte()
    {
        return Usuario.identificacion + "_" + txtIdentificacion.Text.Trim() + "CC";
    }

    byte[] ConstruirReporte(string pNomUsuario, List<Aporte> lstData)
    {
        DataTable table = new DataTable();
        table.Columns.Add("numProducto");
        table.Columns.Add("lineaProducto");
        table.Columns.Add("concepto");
        table.Columns.Add("capital");
        table.Columns.Add("intMora");
        table.Columns.Add("otros");
        table.Columns.Add("retencion");
        table.Columns.Add("intCausados");
        table.Columns.Add("retCausados");
        table.Columns.Add("saldoPendiente");
        table.Columns.Add("totalAcausar");
        table.Columns.Add("Codeudor");
        table.Columns.Add("intRendimiento");
        table.Columns.Add("signo");
        //Llenando el Datatable 
        DataRow datarw;
        foreach (Aporte item in lstData)
        {
            datarw = table.NewRow();
            datarw[0] = item.numproducto;
            datarw[1] = item.linea_producto;
            datarw[2] = item.concepto;
            datarw[3] = item.capital.ToString("n2");
            datarw[4] = item.intmora.ToString("n2");
            datarw[5] = item.otros.ToString("n2");
            datarw[6] = item.retencion.ToString("n2");
            datarw[7] = item.interes_causado.ToString("n2");
            datarw[8] = item.rentecioncausada.ToString("n2");
            datarw[9] = item.saldo_cruce.ToString("n2");
            datarw[10] = item.total.ToString("n2");
            datarw[11] = " ";
            datarw[12] = item.interes.ToString("n2");
            datarw[13] = item.signo;
            table.Rows.Add(datarw);
        }

        ReportParameter[] param = new ReportParameter[14];
        param[0] = new ReportParameter("entidad", Usuario.empresa);
        param[1] = new ReportParameter("nit", Usuario.nitempresa);
        param[2] = new ReportParameter("ImagenReport", ImagenReporte());
        param[3] = new ReportParameter("FechaRetiro", " " + txtFecharetiro.Text);
        param[4] = new ReportParameter("identificacion", " " + txtIdentificacion.Text);
        param[5] = new ReportParameter("tipoidentificacion", " " + ddlTipoIdentificacion.SelectedItem.Text);
        param[6] = new ReportParameter("nombres", " " + txtNombres.Text);
        param[7] = new ReportParameter("apellidos", " " + txtApellidos.Text);
        param[8] = new ReportParameter("motivo_retiro", " " + DdlMotRetiro.SelectedItem.Text);
        param[9] = new ReportParameter("numActa", " " + txtActa.Text);
        param[10] = new ReportParameter("Observaciones", " " + txtObservaciones.Text);
        param[11] = new ReportParameter("saldo", " " + txtsaldo.Text);
        param[12] = new ReportParameter("vrAseguradora", " " + txtAseguradora.Text);
        param[13] = new ReportParameter("total", " " + txtTotal.Text);

        rvReporte.LocalReport.EnableExternalImages = true;
        rvReporte.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", table);
        rvReporte.LocalReport.DataSources.Clear();
        rvReporte.LocalReport.DataSources.Add(rds);
        rvReporte.LocalReport.Refresh();

        //CREANDO REPORTE
        byte[] bytes = rvReporte.LocalReport.Render("PDF");

        // ELIMINANDO ARCHIVOS GENERADOS SI LOS ENCUENTRA
        try
        {
            string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
            foreach (string ficheroActual in ficherosCarpeta)
                if (ficheroActual.Contains(pNomUsuario))
                    File.Delete(ficheroActual);
        }
        catch
        { }

        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("Archivos/" + pNomUsuario + ".pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        return bytes;
    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {
        MvAfiliados.ActiveViewIndex = 0;
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarCancelar(false);
        toolBar.MostrarImprimir(false);
        if (Session["DTCRUCE"] != null)
        {
            List<Aporte> lstData = (List<Aporte>)Session["DTCRUCE"];
            if (lstData.Count > 0)
            {
                MvAfiliados.ActiveViewIndex = 1;
                toolBar.MostrarGuardar(true);
                toolBar.MostrarCancelar(true);
                toolBar.MostrarImprimir(true);
            }
        }
    }

    protected void InicializarVariablesSession()
    {
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".num_comp"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".tipo_comp"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".cod_ope"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".tipo_ope"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".fecha_ope"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".idgiro"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".esDesembolsoCredito"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".numeroRadicacion"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".realizoGiro"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".cod_ope"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".radicadoCruceCuentas"); } catch { }
        try { Session.Remove(ComprobanteServicio.CodigoPrograma + ".cod_traslado"); } catch { }

        try { Session.Remove("DetalleComprobante"); } catch { }
        try { Session.Remove("Modificar"); } catch { }
        try { Session.Remove("Nuevo"); } catch { }
        try { Session.Remove("Carga"); } catch { }
        try { Session.Remove("idNComp"); } catch { }
        try { Session.Remove("idTComp"); } catch { }
        try { Session.Remove("cod_ope"); } catch { }
        try { Session.Remove("Ruta_Cheque"); } catch { }
        try { Session.Remove("numerocheque"); } catch { }
        try { Session.Remove("entidad"); } catch { }
        try { Session.Remove("cuenta"); } catch { }
        try { Session.Remove("NumCred_Orden"); } catch { }
        try { Session.Remove("NUM_AUXILIO"); } catch { }
        try { Session.Remove("Modificar"); } catch { }
        try { Session.Remove("Comprobantecopia"); } catch { }
        try { Session.Remove("Comprobanteanulacion"); } catch { }
        try { Session.Remove("Comprobantecarga"); } catch { }
        try { Session.Remove("Carga"); } catch { }
        try { Session.Remove("Nuevo"); } catch { }
        try { Session.Remove("CENTROSCOSTO"); } catch { }

        try { Session.Remove(Usuario.codusuario + "codOpe"); } catch { }
        try { Session.Remove(Usuario.codusuario + "cod_persona"); } catch { }

    }



}