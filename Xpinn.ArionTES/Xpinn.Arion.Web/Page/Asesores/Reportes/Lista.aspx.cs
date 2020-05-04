using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Configuration;

public partial class Page_Asesores_Colocacion_Lista : GlobalWeb
{
    private ReporteService reporteServicio = new ReporteService();
    private Xpinn.FabricaCreditos.Services.UsuarioAtribucionesService UsuarioAtribucionesServicio = new Xpinn.FabricaCreditos.Services.UsuarioAtribucionesService();
    private Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones vUsuarioAtribuciones = new Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones();
    PoblarListas Poblar = new PoblarListas();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {

            VisualizarOpciones(reporteServicio.CodigoProgramaReportes, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgramaReportes, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];

            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;

            if (!IsPostBack)
            {
                txtFechagenerada.ToDateTime = DateTime.Now;
                Panel1.Visible = false;
                Panel3.Visible = false;
                ddlAsesores.Visible = false;
                Labelejecutivos.Visible = false;
                CargarValoresConsulta(pConsulta, reporteServicio.CodigoProgramaReportes);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgramaReportes, "Page_Load", ex);
        }
    }

    protected void cargardropdown()
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService linahorroServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        Xpinn.FabricaCreditos.Entities.LineasCredito linahorroVista = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        ddllineacredito.DataTextField = "NOMBRE";
        ddllineacredito.DataValueField = "COD_LINEA_CREDITO";
        ddllineacredito.DataSource = linahorroServicio.ListarLineasCredito(linahorroVista, _usuario);
        ddllineacredito.DataBind();
        ddllineacredito.Items.Insert(0, new ListItem("Seleccione Un Item", "0"));

        Poblar.PoblarListaDesplegable("OFICINA", " COD_OFICINA,NOMBRE ", " ESTADO = 1 ", " 1 ", ddlOficina, _usuario);

    }

    /// <summary>
    /// Método que permite generar el reporte segùn las condiciones dadas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        Page.Validate();
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, reporteServicio.CodigoProgramaReportes);

            switch (ddlConsultar.SelectedIndex)
            {
                case 1:
                    ViewState["op1"] = 1;
                    break;
                case 2:
                    ViewState["op1"] = 2;
                    break;
                case 3:
                    ViewState["op1"] = 3;
                    break;
                case 4:
                    ViewState["op1"] = 4;
                    break;
                case 5:
                    ViewState["op1"] = 5;
                    break;
                case 6:
                    ViewState["op1"] = 6;
                    break;
                case 7:
                    ViewState["op1"] = 7;
                    break;
                case 8:
                    ViewState["op1"] = 8;
                    break;
                case 9:
                    ViewState["op1"] = 9;
                    break;
            }

            Actualizar();
        }
    }

    /// <summary>
    /// Méotodo para limpiar datos de la conuslta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            LimpiarValoresConsulta(pConsulta, reporteServicio.CodigoProgramaReportes);
        }
        catch
        {
            mvLista.ActiveViewIndex = -1;
        }
    }

    protected void gvListaCreditos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgramaReportes, "gvListaCreditos_PageIndexChanging", ex);
        }
    }

    protected void gvReoirtemora_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReoirtemora.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgramaReportes, "gvListaClientes_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para traer los datos según las condiciones ingresadas
    /// </summary>
    private void Actualizar()
    {
        try
        {
            Usuario usuap = _usuario;
            CreditosService creditoServicios = new CreditosService();

            // Determinar el còdigo del usuario y determinar si el usuario es un ejecutivo
            int cod = Convert.ToInt32(usuap.codusuario);
            int consulta = creditoServicios.UsuarioEsEjecutivo(cod, _usuario);
            int oficinaus = Convert.ToInt32(usuap.cod_oficina);
            int perfil = Convert.ToInt32(usuap.codperfil);

            // Generar el reporte
            switch ((int)ViewState["op1"])
            {
                case 1:
                    try
                    {
                        vUsuarioAtribuciones = UsuarioAtribucionesServicio.ConsultarUsuarioAtribuciones(usuap.codusuario, 0, _usuario);
                    }
                    catch { }
                    vUsuarioAtribuciones.tipoatribucion = 0;
                    vUsuarioAtribuciones.activo = 1;
                    consulta = 0;
                    if (vUsuarioAtribuciones.tipoatribucion == 0 && vUsuarioAtribuciones.activo == 1 && consulta == 0 && ddlAsesores.SelectedValue == "0") // Trae Todos los creditos
                    {
                        List<Reporte> lstConsultaClientes = new List<Reporte>();
                        lstConsultaClientes = reporteServicio.ListarReporteMoraofici(_usuario, oficinaus);
                        gvReoirtemora.EmptyDataText = emptyQuery;
                        gvReoirtemora.DataSource = lstConsultaClientes;
                        if (lstConsultaClientes.Count > 0)
                        {
                            mvLista.SetActiveView(vGridReporteMora);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaClientes.Count.ToString();
                            gvReoirtemora.DataBind();
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                    }

                    if (vUsuarioAtribuciones.tipoatribucion == 0 && vUsuarioAtribuciones.activo == 1 && consulta == 0 && ddlAsesores.SelectedValue != "0" && !string.IsNullOrWhiteSpace(ddlAsesores.SelectedValue)) //Consulta   los creditos segun el asesor
                    {
                        if (string.IsNullOrWhiteSpace(ddlAsesores.SelectedValue))
                        {
                            VerError("No tienes permisos para ver este tipo de reporte");
                            return;
                        }

                        List<Reporte> lstConsultaClientes = new List<Reporte>();

                        lstConsultaClientes = reporteServicio.ListarReporteMora(_usuario, Convert.ToInt64(ddlAsesores.SelectedValue));

                        gvReoirtemora.EmptyDataText = emptyQuery;
                        gvReoirtemora.DataSource = lstConsultaClientes;
                        if (lstConsultaClientes.Count > 0)
                        {
                            mvLista.SetActiveView(vGridReporteMora);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaClientes.Count.ToString();
                            gvReoirtemora.DataBind();
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                    }
                    Session.Add(reporteServicio.CodigoProgramaReportes + ".consulta", 1);

                    break;

                case 2:
                    DateTime fechaini, fechafinal;
                    if (txtFechaIni.Text == "" && txtFechaFin.Text == "")
                    {
                        Label4.Text = "falta fecha inicial";
                        Label3.Text = " falta fecha final";
                    }
                    else
                    {
                        fechaini = txtFechaIni.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaIni.Text);
                        fechafinal = txtFechaFin.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaFin.Text);

                        List<Reporte> lstConsultareportemora = new List<Reporte>();
                        long oficina = (usuap.cod_oficina);
                        lstConsultareportemora = reporteServicio.ListarReportepoliza(_usuario, oficina, fechaini, fechafinal);
                        Gvpreportepolizas.EmptyDataText = emptyQuery;
                        Gvpreportepolizas.DataSource = lstConsultareportemora;
                        if (lstConsultareportemora.Count > 0)
                        {
                            mvLista.SetActiveView(VGreportepolisas);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultareportemora.Count.ToString();
                            Gvpreportepolizas.DataBind();
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                    }
                    break;

                case 3:
                    DateTime fechainicolo, fechafinalcolo;
                    if (txtFechaIni.Text == "" && txtFechaFin.Text == "")
                    {
                        Label4.Text = "falta fecha inicial";
                        Label3.Text = " falta fecha final";
                    }
                    else
                    {
                        fechainicolo = txtFechaIni.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaIni.Text);
                        fechafinalcolo = txtFechaFin.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaFin.Text);

                        List<Reporte> lstConsultareportecoloca = new List<Reporte>();
                        long oficina = (usuap.cod_oficina);
                        lstConsultareportecoloca = reporteServicio.Listarcolocacioneje(_usuario, oficina, fechainicolo, fechafinalcolo);
                        Gvcolocacionejecutivo.EmptyDataText = emptyQuery;
                        Gvcolocacionejecutivo.DataSource = lstConsultareportecoloca;
                        if (lstConsultareportecoloca.Count > 0)
                        {
                            mvLista.SetActiveView(Vreportecolocacionejecutivo);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultareportecoloca.Count.ToString();
                            Gvcolocacionejecutivo.DataBind();
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                    }
                    break;

                case 4:
                    List<Reporte> lstConsultareportecartejecutivos = new List<Reporte>();
                    long codigo = (usuap.codusuario);
                    lstConsultareportecartejecutivos = reporteServicio.ListarReportecartejecutivo(_usuario, codigo);
                    Gvcarteraejecutivo.EmptyDataText = emptyQuery;
                    Gvcarteraejecutivo.DataSource = lstConsultareportecartejecutivos;
                    if (lstConsultareportecartejecutivos.Count > 0)
                    {
                        mvLista.SetActiveView(Vreportecarteraejecutivo);
                        lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultareportecartejecutivos.Count.ToString();
                        Gvcarteraejecutivo.DataBind();
                    }
                    else
                    {
                        mvLista.ActiveViewIndex = -1;
                    }
                    break;

                case 5:
                    vUsuarioAtribuciones.tipoatribucion = 0;
                    vUsuarioAtribuciones.activo = 1;
                    List<Reporte> lstConsultareporteactivaoficina = new List<Reporte>();
                    long cod_oficina_activa = (usuap.cod_oficina);
                    if (vUsuarioAtribuciones.tipoatribucion == 0 && vUsuarioAtribuciones.activo == 1 && consulta == 0) //Consulta   los creditos segun el asesor
                    {
                        long codigoasesor = (usuap.codusuario);
                        lstConsultareporteactivaoficina = reporteServicio.ListarReporteactivoporasesor(_usuario, codigoasesor);
                        Gvreporactivo.EmptyDataText = emptyQuery;
                        Gvreporactivo.DataSource = lstConsultareporteactivaoficina;
                        if (lstConsultareporteactivaoficina.Count > 0)
                        {
                            mvLista.SetActiveView(Vreporteactivo);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultareporteactivaoficina.Count.ToString();
                            Gvreporactivo.DataBind();
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                    }
                    else
                    {
                        lstConsultareporteactivaoficina = reporteServicio.ListarReporteactivo(_usuario, cod_oficina_activa);
                        Gvreporactivo.EmptyDataText = emptyQuery;
                        Gvreporactivo.DataSource = lstConsultareporteactivaoficina;
                        if (lstConsultareporteactivaoficina.Count > 0)
                        {
                            mvLista.SetActiveView(Vreporteactivo);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultareporteactivaoficina.Count.ToString();
                            Gvreporactivo.DataBind();
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                    }
                    break;

                case 6:
                    vUsuarioAtribuciones = UsuarioAtribucionesServicio.ConsultarUsuarioAtribuciones(usuap.codusuario, _usuario);
                    if (vUsuarioAtribuciones != null)
                    {
                        if (vUsuarioAtribuciones.tipoatribucion == 0 && vUsuarioAtribuciones.activo == 1 && consulta == 1 && perfil == 1 || perfil == 8 || perfil == 15) //Consulta  todos los creditos perfil administrador
                        {
                            DateTime fechacierre = txtFechaIni.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaIni.Text);
                            List<Reporte> lstConsultaCarteracierre = new List<Reporte>();
                            lstConsultaCarteracierre = reporteServicio.ListarReportecierreoficinatodos(_usuario, fechacierre);
                            GvCarteracierre.EmptyDataText = emptyQuery;
                            GvCarteracierre.DataSource = lstConsultaCarteracierre;

                            if (lstConsultaCarteracierre.Count > 0)
                            {
                                mvLista.SetActiveView(Carteracierre);
                                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaCarteracierre.Count.ToString();
                                GvCarteracierre.DataBind();
                            }
                            else
                            {
                                mvLista.ActiveViewIndex = -1;
                            }
                        }
                    }
                    else
                    {
                        DateTime fechacierre = txtFechaIni.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaIni.Text);
                        List<Reporte> lstConsultaCarteracierre = new List<Reporte>();
                        long oficina_usu = (usuap.cod_oficina);
                        lstConsultaCarteracierre = reporteServicio.ListarReportecierreoficina(_usuario, oficina_usu, fechacierre);
                        GvCarteracierre.EmptyDataText = emptyQuery;
                        GvCarteracierre.DataSource = lstConsultaCarteracierre;
                        if (lstConsultaCarteracierre.Count > 0)
                        {
                            mvLista.SetActiveView(Carteracierre);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaCarteracierre.Count.ToString();
                            GvCarteracierre.DataBind();
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                        Session.Add(reporteServicio.CodigoProgramaReportes + ".consulta", 6);
                    }
                    break;

                case 7:
                    String filtro = "";
                    Configuracion conf = new Configuracion();

                    if (ddllineacredito.SelectedIndex != -1)
                    {
                        if (ddllineacredito.SelectedIndex != 0)
                        {
                            filtro += " And lineascredito.nombre = '" + ddllineacredito.SelectedItem + "'";
                        }
                    }
                    if (ddlOficina.SelectedIndex != -1)
                    {
                        if (ddlOficina.SelectedIndex != 0)
                        {
                            filtro += " And oficina.Nombre like '" + ddlOficina.SelectedItem + "'";
                        }
                    }

                    List<Reporte> lstConsultaCarteracierresd = new List<Reporte>();
                    lstConsultaCarteracierresd = reporteServicio.Consultarusuariopagoespecial(usuap.codusuario, _usuario, filtro);
                    mvLista.SetActiveView(gvpagoespecial);
                    gvpagosespeciales.EmptyDataText = emptyQuery;
                    gvpagosespeciales.DataSource = lstConsultaCarteracierresd;

                    if (lstConsultaCarteracierresd.Count > 0)
                    {
                        lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaCarteracierresd.Count.ToString();
                        gvpagosespeciales.DataBind();
                    }
                    else
                    {
                        lblTotalRegs.Text = "No se encontraron datos";
                    }

                    break;
                case 8:
                    ConsultarReporteAfiliacionPorAsesor();
                    break;
                case 9:
                    ConsultarReporteAsesoresProductos();
                    break;
            }
        }

        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
    }

    void ConsultarReporteAfiliacionPorAsesor()
    {
        if (string.IsNullOrWhiteSpace(ddlAsesores.SelectedValue) || ddlAsesores.SelectedValue == "0")
        {
            VerError("Seleccione un asesor");
            return;
        }

        List<Reporte> lstAfiliacionAsesor = reporteServicio.ListarAfiliacionPorAsesor(ddlAsesores.SelectedValue, Usuario);

        mvLista.SetActiveView(vAfiliacionAsesor);

        if (lstAfiliacionAsesor.Count > 0)
        {
            gvAfiliacionAsesor.DataSource = lstAfiliacionAsesor;
            gvAfiliacionAsesor.DataBind();

            btnAfiliacionAsesor.Visible = true;
            hiddenAsesor.Value = ddlAsesores.SelectedValue;
            lblAfiliacionAsesor.Text = "Se encontraron " + lstAfiliacionAsesor.Count + " registros!.";
        }
        else
        {
            btnAfiliacionAsesor.Visible = false;
            hiddenAsesor.Value = string.Empty;
            lblAfiliacionAsesor.Text = "No se encontraron registros!.";
        }
    }

    private string obtfilfiltro()
    {
        Configuracion conf = new Configuracion();

        String filtro = String.Empty;

        if (ddllineacredito.SelectedValue.Trim() != "")
            filtro += " and credito.Nombres like '%" + ddllineacredito.SelectedValue + "%'";
        if (ddlOficina.SelectedValue.Trim() != "")
            filtro += " and credito.Nombre like '%" + ddlOficina.SelectedValue + "%'";

        return filtro;
    }
    /// <summary>
    /// Método para llenar el combo de oficinas
    /// </summary>
    /// <param name="Ddloficinas"></param>
    protected void LlenarComboOficinasAsesores(DropDownList Ddloficinas)
    {

        OficinaService oficinaService = new OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        Ddloficinas.DataSource = oficinaService.ListarOficinasAsesores(oficina, _usuario);
        Ddloficinas.DataTextField = "Nombre";
        Ddloficinas.DataValueField = "Codigo";
        Ddloficinas.DataBind();
        Ddloficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    /// <summary>
    /// Método para obtener datos del filtro
    /// </summary>
    /// <returns></returns>
    private string obtFiltro()
    {

        String filtro = String.Empty;

        return filtro;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvReoirtemora.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();

            gvReoirtemora.EnableViewState = false;
            pagina.EnableEventValidation = false;

            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvReoirtemora);
            //gvReoirtemora.HeaderRow.Cells[1].Visible = false;
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void btnExportarpoliza_Click(object sender, EventArgs e)
    {
        if (Gvpreportepolizas.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            Gvpreportepolizas.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(Gvpreportepolizas);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void btnExportarcolocacion_Click(object sender, EventArgs e)
    {
        if (Gvcolocacionejecutivo.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            Gvcolocacionejecutivo.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(Gvcolocacionejecutivo);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void btnExportarcarteraejecutivo_Click(object sender, EventArgs e)
    {
        if (Gvcarteraejecutivo.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            Gvcarteraejecutivo.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(Gvcarteraejecutivo);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void btnExportarreporactivo_Click(object sender, EventArgs e)
    {
        if (Gvreporactivo.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            Gvreporactivo.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(Gvreporactivo);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void btnExportarCarteracierre_Click(object sender, EventArgs e)
    {
        if (GvCarteracierre.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            GvCarteracierre.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(GvCarteracierre);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }



    protected void btn_export_gvpagos(object sender, EventArgs e)
    {
        if (gvpagosespeciales.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvpagosespeciales.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvpagosespeciales);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void btn_export_gvAfiliacionAsesor(object sender, EventArgs e)
    {
        if (gvAfiliacionAsesor.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvAfiliacionAsesor.EnableViewState = false;
            gvAfiliacionAsesor.AllowPaging = false;

            var lstReporte = reporteServicio.ListarAfiliacionPorAsesor(hiddenAsesor.Value, Usuario);
            gvAfiliacionAsesor.DataSource = lstReporte;
            gvAfiliacionAsesor.DataBind();

            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvAfiliacionAsesor);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());

            gvAfiliacionAsesor.AllowPaging = true;
            gvAfiliacionAsesor.DataSource = lstReporte;
            gvAfiliacionAsesor.DataBind();

            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void btn_export_gvAsesoresProductos(object sender, EventArgs e)
    {
        if (gvAsesoresProductos.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvAsesoresProductos.EnableViewState = false;
            gvAsesoresProductos.AllowPaging = false;

            var lstReporte = reporteServicio.ListarProductosPorAsesor(hiddenAsesor.Value, Usuario);
            gvAsesoresProductos.DataSource = lstReporte;
            gvAsesoresProductos.DataBind();

            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvAsesoresProductos);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());

            gvAsesoresProductos.AllowPaging = true;
            gvAsesoresProductos.DataSource = lstReporte;
            gvAsesoresProductos.DataBind();

            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Page/Asesores/GestionarAgenda/Lista.aspx");
    }

    protected void gvAfiliacionAsesor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAfiliacionAsesor.PageIndex = e.NewPageIndex;
            ConsultarReporteAfiliacionPorAsesor();
        }
        catch (Exception ex)
        {
            VerError("Ocurrio un error al paginar la tabla de afiliación por asesor, " + ex.Message);
        }
    }

    protected void gvAsesoresProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAsesoresProductos.PageIndex = e.NewPageIndex;
            ConsultarReporteAsesoresProductos();
        }
        catch (Exception ex)
        {
            VerError("Ocurrio un error al paginar la tabla de afiliación por asesor, " + ex.Message);
        }
    }

    void ConsultarReporteAsesoresProductos()
    {
        if (string.IsNullOrWhiteSpace(ddlAsesores.SelectedValue) || ddlAsesores.SelectedValue == "0")
        {
            VerError("Seleccione un asesor");
            return;
        }

        List<Reporte> lstProductosAsesor = reporteServicio.ListarProductosPorAsesor(ddlAsesores.SelectedValue, Usuario);

        mvLista.SetActiveView(vAsesoresProductos);

        if (lstProductosAsesor.Count > 0)
        {
            gvAsesoresProductos.DataSource = lstProductosAsesor;
            gvAsesoresProductos.DataBind();

            btnAsesoresProductos.Visible = true;
            hiddenAsesor.Value = ddlAsesores.SelectedValue;
            lblAsesoresProductos.Text = "Se encontraron " + lstProductosAsesor.Count + " registros!.";
        }
        else
        {
            btnAsesoresProductos.Visible = false;
            hiddenAsesor.Value = string.Empty;
            lblAsesoresProductos.Text = "No se encontraron registros!.";
        }
    }

    protected void ddlConsultar_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlAsesores.Visible = false;
        Labelejecutivos.Visible = false;
        Panel1.Visible = false;
        Panel2.Visible = false;
        Panel3.Visible = false;
        if (ddlConsultar.SelectedIndex == 1)
        {
            CreditosService creditoServicios = new CreditosService();
            Usuario usuap = _usuario;
            int cod = Convert.ToInt32(usuap.codusuario);
            int consulta = creditoServicios.UsuarioEsEjecutivo(cod, _usuario);

            vUsuarioAtribuciones = UsuarioAtribucionesServicio.ConsultarUsuarioAtribuciones(usuap.codusuario, _usuario);
            consulta = 0;
            if (vUsuarioAtribuciones.tipoatribucion == 0 && vUsuarioAtribuciones.activo == 1 && consulta == 0) //Consulta todos los creditos
            {
                ddlAsesores.Visible = true;
                Labelejecutivos.Visible = true;
                EjecutivoService serviceEjecutivo = new EjecutivoService();
                Ejecutivo ejec = new Ejecutivo();
                long iOficina = (usuap.cod_oficina);
                ejec.IOficina = iOficina;
                ddlAsesores.DataSource = serviceEjecutivo.ListarAsesores(ejec, _usuario);
                ddlAsesores.DataTextField = "NombreCompleto";
                ddlAsesores.DataValueField = "IdEjecutivo";
                ddlAsesores.DataBind();
                ddlAsesores.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            }
            if (vUsuarioAtribuciones.tipoatribucion == 1 && vUsuarioAtribuciones.activo == 1) //Consulta  los creditos de la oficna
            {
                ddlAsesores.Visible = true;
                Labelejecutivos.Visible = true;
                EjecutivoService serviceEjecutivo = new EjecutivoService();
                Ejecutivo ejec = new Ejecutivo();
                long iOficina = (usuap.cod_oficina);
                ejec.IOficina = iOficina;
                ddlAsesores.DataSource = serviceEjecutivo.ListartodosAsesores(_usuario);
                ddlAsesores.DataTextField = "NombreCompleto";
                ddlAsesores.DataValueField = "IdEjecutivo";
                ddlAsesores.DataBind();
                ddlAsesores.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            }
            if (vUsuarioAtribuciones.tipoatribucion == 1 && vUsuarioAtribuciones.activo == 1 && consulta == 1)
            {
                ddlAsesores.Visible = false;
            }
        }

        if (ddlConsultar.SelectedIndex == 2)
        {
            Panel1.Visible = true;
            Panel3.Visible = true;
        }

        if (ddlConsultar.SelectedIndex == 3)
        {
            Panel1.Visible = true;
            Panel3.Visible = true;
        }
        if (ddlConsultar.SelectedIndex == 6)
        {
            Panel1.Visible = true;
            LabelFecha_gara.Text = "Fecha Cierre";
        }
        if (ddlConsultar.SelectedIndex == 7)
        {
            Panel2.Visible = true;
            cargardropdown();
        }
        if (ddlConsultar.SelectedIndex == 8 || ddlConsultar.SelectedIndex == 9)
        {
            LlenarListasDesplegables(TipoLista.Asesor, ddlAsesores);
            ddlAsesores.Visible = true;
        }

        mvLista.ActiveViewIndex = -1;
    }

    Int64 subtotalnumerocreditos = 0;
    decimal subtotalsaldocierre = 0;
    Int64 subtotalcolocacion_mes = 0;
    decimal subtotalmonto_colocacion_mes = 0;
    Int64 subtotalmora = 0;
    decimal subtotalsaldo_mora = 0;
    Int64 subtotalmora_menor_30 = 0;
    decimal subtotalmonto_menor_30 = 0;
    Int64 subtotalmora_mayor_30 = 0;
    decimal subtotalmonto_mayor_30 = 0;

    protected void GvCarteracierre_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            subtotalnumerocreditos += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "numero_credito"));
            subtotalsaldocierre += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo_cierre"));
            subtotalcolocacion_mes += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "numero_colocacion_mes"));
            subtotalmonto_colocacion_mes += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "monto_colocacion_mes"));
            subtotalmora += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "total_mora"));
            subtotalsaldo_mora += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo_mora"));
            subtotalmora_menor_30 += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "mora_menor_30"));
            subtotalmonto_menor_30 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "monto_menor_30"));
            subtotalmora_mayor_30 += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "mora_mayor_30"));
            subtotalmonto_mayor_30 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "monto_mayor_30"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[4].Text = "Total:";
            e.Row.Cells[5].Text = subtotalnumerocreditos.ToString("d");
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].Text = subtotalsaldocierre.ToString("c");
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].Text = subtotalcolocacion_mes.ToString("d");
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[8].Text = subtotalmonto_colocacion_mes.ToString("c");
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].Text = subtotalmora.ToString("d");
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[10].Text = subtotalsaldo_mora.ToString("c");
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[11].Text = subtotalmora_menor_30.ToString("d");
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[12].Text = subtotalmonto_menor_30.ToString("c");
            e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[13].Text = subtotalmora_mayor_30.ToString("d");
            e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[14].Text = subtotalmonto_mayor_30.ToString("c");
            e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Font.Bold = true;
        }
    }

    Int64 subtotalconteo = 0;
    decimal subtotalvalordesembolsado = 0;

    protected void Gvcolocacionejecutivo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            subtotalconteo += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "cuenta"));
            subtotalvalordesembolsado += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "monto_pago"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Total:";
            e.Row.Cells[4].Text = subtotalvalordesembolsado.ToString("c");
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].Text = subtotalconteo.ToString("d");
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Font.Bold = true;
        }
    }

    decimal subtotalgarantiascomunitarias = 0;
    decimal subtotalsaldo_capital = 0;
    decimal subtotalvalor_cuota = 0;
    decimal subtotalpendite_cuota = 0;


    protected void gvReoirtemora_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            subtotalgarantiascomunitarias += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "garantia_comunitaria"));
            subtotalsaldo_capital += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo_capital"));
            subtotalvalor_cuota += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor_cuota"));
            subtotalpendite_cuota += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pendite_cuota"));


            Int64 diasmora_cierre = Convert.ToInt64(e.Row.Cells[14].Text);
            Int64 diasmora = Convert.ToInt64(e.Row.Cells[13].Text);
            if (diasmora_cierre == 0 && diasmora != 0)
            {
                e.Row.BackColor = System.Drawing.Color.Thistle;
                e.Row.Font.Bold = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Total:";

            e.Row.Cells[6].Text = subtotalvalor_cuota.ToString("c");
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[8].Text = subtotalsaldo_capital.ToString("c");
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[9].Text = subtotalgarantiascomunitarias.ToString("c");
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[10].Text = subtotalpendite_cuota.ToString("c");
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Left;

            e.Row.Font.Bold = true;
        }
    }

}

