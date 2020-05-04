using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

public partial class Lista : GlobalWeb
{
    Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(ComprobanteServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarNuevo(false);
            Reporte.Visible = false;
            frmPrint.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];

            if (!IsPostBack)
            {
                LimpiarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
                CargarDDList();
                ListaPlanCuentas();
                ListaMonedas();
                //CargarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
                if (Request["num_comp"] != null)
                {
                    txtNumComp.Text = Request["num_comp"].ToString();
                    Actualizar();
                }
                if (Request["num_documento"] != null)
                {
                    ddlIdentificacion.Text = Request["num_documento"].ToString();
                    Actualizar();
                }
                //if (Session[ComprobanteServicio.CodigoPrograma + ".id"] != null)
                //{
                //    idObjeto = Session[ComprobanteServicio.CodigoPrograma + ".id"].ToString();
                //    Session.Remove(Session[ComprobanteServicio.CodigoPrograma + ".id"].ToString());
                //    ddlIdentificacion.Text = idObjeto;                    
                //    Actualizar();
                //}
                //else
                //{
                //    if (Session[ComprobanteServicio.GetType().Name + ".consulta"] != null)
                //        Actualizar();
                //}
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    private void CargarDDList()
    {
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["Usuario"];

        Xpinn.Contabilidad.Services.TipoComprobanteService TipoComprobanteService = new Xpinn.Contabilidad.Services.TipoComprobanteService();
        Xpinn.Contabilidad.Entities.TipoComprobante TipoComprobante = new Xpinn.Contabilidad.Entities.TipoComprobante();
        ddlTipoComprobante.DataSource = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "", _usuario);
        ddlTipoComprobante.DataTextField = "descripcion";
        ddlTipoComprobante.DataValueField = "tipo_comprobante";
        ddlTipoComprobante.DataBind();

        Xpinn.Caja.Services.CiudadService CiudadService = new Xpinn.Caja.Services.CiudadService();
        Xpinn.Caja.Entities.Ciudad Ciudad = new Xpinn.Caja.Entities.Ciudad();
        ddlCiudad.DataSource = CiudadService.ListadoCiudad(Ciudad, usuario);
        ddlCiudad.DataTextField = "nom_ciudad";
        ddlCiudad.DataValueField = "cod_ciudad";
        ddlCiudad.DataBind();

        Xpinn.Contabilidad.Services.ConceptoService ConceptoService = new Xpinn.Contabilidad.Services.ConceptoService();
        Xpinn.Contabilidad.Entities.Concepto Concepto = new Xpinn.Contabilidad.Entities.Concepto();
        ddlConcepto.DataSource = ConceptoService.ListarConcepto(Concepto, usuario);
        ddlConcepto.DataTextField = "descripcion";
        ddlConcepto.DataValueField = "concepto";
        ddlConcepto.DataBind();

        ddlOrden1.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlOrden1.Items.Insert(1, new ListItem("Número Comprobante", "V_Comprobante.Num_Comp"));
        ddlOrden1.Items.Insert(2, new ListItem("Tipo Comprobante", "V_Comprobante.Tipo_Comp"));
        ddlOrden1.Items.Insert(3, new ListItem("Fecha", "V_Comprobante.Fecha"));
        ddlOrden1.Items.Insert(4, new ListItem("Concepto", "V_Comprobante.Descripcion_Concepto"));
        ddlOrden1.Items.Insert(5, new ListItem("Ciudad", "V_Comprobante.Ciudad"));
        ddlOrden1.Items.Insert(6, new ListItem("Identificación", "V_Comprobante.Iden_Benef"));
        ddlOrden1.Items.Insert(7, new ListItem("Nombres", "V_Comprobante.Nombres"));
        ddlOrden1.Items.Insert(8, new ListItem("Apellidos", "V_Comprobante.Apellidos"));
        ddlOrden1.Items.Insert(9, new ListItem("Estado", "V_Comprobante.Estado"));
        ddlOrden1.Items.Insert(10, new ListItem("Valor", "V_Comprobante.Totalcom"));
        ddlOrden1.SelectedIndex = 0;
        ddlOrden1.DataBind();

        ddlOrdenLuego.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlOrdenLuego.Items.Insert(1, new ListItem("Número Comprobante", "V_Comprobante.Num_Comp"));
        ddlOrdenLuego.Items.Insert(2, new ListItem("Tipo Comprobante", "V_Comprobante.Tipo_Comp"));
        ddlOrdenLuego.Items.Insert(3, new ListItem("Fecha", "V_Comprobante.Fecha"));
        ddlOrdenLuego.Items.Insert(4, new ListItem("Concepto", "V_Comprobante.Descripcion_Concepto"));
        ddlOrdenLuego.Items.Insert(5, new ListItem("Ciudad", "V_Comprobante.Ciudad"));
        ddlOrdenLuego.Items.Insert(6, new ListItem("Identificación", "V_Comprobante.Iden_Benef"));
        ddlOrdenLuego.Items.Insert(7, new ListItem("Nombres", "V_Comprobante.Nombres"));
        ddlOrdenLuego.Items.Insert(8, new ListItem("Apellidos", "V_Comprobante.Apellidos"));
        ddlOrdenLuego.Items.Insert(9, new ListItem("Estado", "V_Comprobante.Estado"));
        ddlOrdenLuego.Items.Insert(10, new ListItem("Valor", "V_Comprobante.Totalcom"));
        ddlOrdenLuego.SelectedIndex = 0;
        ddlOrdenLuego.DataBind();


        //DropDownList CC
        List<Xpinn.Caja.Entities.CentroCosto> LstCentroCosto = new List<Xpinn.Caja.Entities.CentroCosto>();

        Xpinn.Caja.Entities.CentroCosto CenCos = new Xpinn.Caja.Entities.CentroCosto();
        Xpinn.Caja.Services.CentroCostoService CentroCostoServicio = new Xpinn.Caja.Services.CentroCostoService();
        LstCentroCosto = CentroCostoServicio.ListarCentroCosto(CenCos, _usuario);
        if (LstCentroCosto.Count > 0)
        {
            ddlCentroCosto.DataSource = LstCentroCosto;
            ddlCentroCosto.DataTextField = "nom_centro";
            ddlCentroCosto.DataValueField = "centro_costo";
            ddlCentroCosto.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlCentroCosto.SelectedIndex = 0;
            ddlCentroCosto.DataBind();
        }

        //DROPDOWNLIST CG
        Xpinn.Contabilidad.Entities.CentroGestion CenGes = new Xpinn.Contabilidad.Entities.CentroGestion();
        List<Xpinn.Contabilidad.Entities.CentroGestion> LstCentroGestion = new List<Xpinn.Contabilidad.Entities.CentroGestion>();
        Xpinn.Contabilidad.Services.CentroGestionService CentroGestionServicio = new Xpinn.Contabilidad.Services.CentroGestionService();

        LstCentroGestion = CentroGestionServicio.ListarCentroGestion(CenGes, "", _usuario);
        if (LstCentroGestion.Count > 0)
        {
            ddlCentroGestion.DataSource = LstCentroGestion;
            ddlCentroGestion.DataTextField = "nombre";
            ddlCentroGestion.DataValueField = "centro_gestion";
            ddlCentroGestion.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlCentroGestion.SelectedIndex = 0;
            ddlCentroGestion.DataBind();
        }
    }



    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
            Navegar(Pagina.Nuevo);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "btnNuevo_Click", ex);
        }

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "btnConsultar_Click", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        txtFechaIni.Text = "";
        txtFechaFin.Text = "";
        LimpiarValoresConsulta(pConsulta, ComprobanteServicio.GetType().Name);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        TipoComprobanteService TipoCompServicio = new TipoComprobanteService();

        Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = gvLista.SelectedRow.Cells[2].Text;

        String id = gvLista.SelectedRow.Cells[3].Text;
        Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = id;
        Session[ComprobanteServicio.CodigoPrograma + ".detalle"] = id;

        Response.Redirect("Nuevo.aspx", false);
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
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            Configuracion conf = new Configuracion();
            List<Xpinn.Contabilidad.Entities.Comprobante> lstConsulta = new List<Xpinn.Contabilidad.Entities.Comprobante>();
            string sFiltro = " ";
            if (txtFechaIni.TieneDatos)
                if (txtFechaIni.ToDate.Trim() != "")
                    sFiltro += " And v_comprobante.fecha >= To_Date('" + txtFechaIni.ToDate.Trim() + "', '" + conf.ObtenerFormatoFecha() + "')";
            if (txtFechaFin.TieneDatos)
                if (txtFechaFin.ToDate.Trim() != "")
                    sFiltro += " And v_comprobante.fecha <= To_Date('" + txtFechaFin.ToDate.Trim() + "', '" + conf.ObtenerFormatoFecha() + "')";
            if (ddlCodCuenta.SelectedValue.Trim() != "")
                sFiltro += " And v_comprobante.num_comp In (Select d.num_comp From d_comprobante d Where v_comprobante.num_comp = d.num_comp And v_comprobante.tipo_comp = d.tipo_comp And d.cod_cuenta = '" + ddlCodCuenta.SelectedValue.Trim() + "')";
            if (txtNumSop.Text.Trim() != "")
                sFiltro += " And v_comprobante.n_documento = '" + txtNumSop.Text.Trim() + "' ";
            if (txtValorTotal.Text.Trim() != "" && txtValorTotal.Text.Trim() != "0")
                sFiltro += " And v_comprobante.totalcom = " + txtValorTotal.Text.Trim().Replace(".", "") + " ";
            if (txtDetalle.Text.Trim() != "")
                sFiltro += " And v_comprobante.num_comp In (Select d.num_comp From d_comprobante d Where v_comprobante.num_comp = d.num_comp And v_comprobante.tipo_comp = d.tipo_comp And d.detalle Like '" + txtDetalle.Text.Trim() + "%')";
            if (txtValor.Text.Trim() != "" && txtValor.Text.Trim() != "0")
                sFiltro += " And v_comprobante.num_comp In (Select d.num_comp From d_comprobante d Where v_comprobante.num_comp = d.num_comp And v_comprobante.tipo_comp = d.tipo_comp And d.valor = " + txtValor.Text.Trim().Replace(".", "") + ")";
            if (ddlCentroCosto.SelectedItem != null)
                if (ddlCentroCosto.SelectedIndex != 0)
                    sFiltro += " And v_comprobante.num_comp In (Select d.num_comp From d_comprobante d Where v_comprobante.num_comp = d.num_comp And v_comprobante.tipo_comp = d.tipo_comp And d.Centro_Costo = " + ddlCentroCosto.SelectedValue.Trim() + ")";
            if (ddlCentroGestion.SelectedItem != null)
                if (ddlCentroGestion.SelectedIndex != 0)
                    sFiltro += " And v_comprobante.num_comp In (Select d.num_comp From d_comprobante d Where v_comprobante.num_comp = d.num_comp And v_comprobante.tipo_comp = d.tipo_comp And d.Centro_Gestion = " + ddlCentroGestion.SelectedValue.Trim() + ")";


            string Orden = "";
            if (ddlOrden1.SelectedIndex != 0)
            {
                Orden += ddlOrden1.SelectedValue;
                if (chkDesc.Checked)
                    Orden += " Desc";
            }
            if (ddlOrdenLuego.SelectedIndex != 0)
            {
                if (Orden != "")
                    Orden += ", " + ddlOrdenLuego.SelectedValue;
                else
                    Orden += ddlOrdenLuego.SelectedValue;
                if (chkDescLuego.Checked)
                    Orden += " Desc";
            }

            lstConsulta = ComprobanteServicio.ListarComprobantes(ObtenerValores(), _usuario, sFiltro, Orden);

            gvLista.DataSource = lstConsulta;
            Session["DTLISTA"] = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados: " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

        }
        catch (System.OutOfMemoryException)
        {
            VerError("No se pudieron consultar los comprobantes, utilice algunos filtros para consultar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Xpinn.Contabilidad.Entities.Comprobante ObtenerValores()
    {
        Xpinn.Contabilidad.Entities.Comprobante Comprobante = new Xpinn.Contabilidad.Entities.Comprobante();

        try
        {
            if (txtNumComp.Text.Trim() != "")
                Comprobante.num_comp = Convert.ToInt64(txtNumComp.Text.Trim());
            if (ddlTipoComprobante.SelectedValue != null && ddlTipoComprobante.SelectedIndex != 0)
                Comprobante.tipo_comp = Convert.ToInt64(ddlTipoComprobante.SelectedValue);
            if (ddlConcepto.SelectedValue != null && ddlConcepto.SelectedIndex != 0)
                Comprobante.concepto = Convert.ToInt64(ddlConcepto.SelectedValue);
            if (ddlCiudad.SelectedValue != null && ddlCiudad.SelectedIndex != 0)
                Comprobante.ciudad = Convert.ToInt64(ddlCiudad.SelectedValue);
            if (ddlEstado.SelectedValue != null && ddlEstado.SelectedIndex != 0)
                Comprobante.estado = ddlEstado.SelectedValue;
            if (ddlIdentificacion.Text != null && !ddlIdentificacion.Text.Equals("0"))
                Comprobante.iden_benef = ddlIdentificacion.Text;
            if (ddlNombres.Text != null && !ddlNombres.Text.Equals("0"))
                Comprobante.nombres = ddlNombres.Text;
            if (ddlApellidos.Text != null && !ddlApellidos.Text.Equals("0"))
                Comprobante.apellidos = ddlApellidos.Text;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "ObtenerValores", ex);
        }

        return Comprobante;
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        String id;
        TipoComprobanteService TipoCompServicio = new TipoComprobanteService();

        if (!ValidarAccionesGrilla("UPDATE"))
            return;

        id = gvLista.Rows[e.NewEditIndex].Cells[3].Text;

        Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = id;
        Session[ComprobanteServicio.CodigoPrograma + ".detalle"] = null;

        TipoComprobante tipComprobante = TipoCompServicio.ConsultarTipoComprobante(Convert.ToInt64(id), _usuario);
        DateTime dtUltCierre = DateTime.MinValue;
        bool requiereSegundaValidacion = false;
        DateTime dtUltCierreSegundaValidacion = DateTime.MinValue;

        try
        {
            if (tipComprobante.tipo_norma == null || tipComprobante.tipo_norma == (int)TipoNormaComprobante.Local)
            {
                dtUltCierre = Convert.ToDateTime(ComprobanteServicio.Consultafecha(_usuario));
            }
            else if (tipComprobante.tipo_norma == (int)TipoNormaComprobante.Niff)
            {
                dtUltCierre = Convert.ToDateTime(ComprobanteServicio.Consultafecha(_usuario, "'G'"));
            }
            else if (tipComprobante.tipo_norma == (int)TipoNormaComprobante.Local_Niff)
            {
                dtUltCierre = Convert.ToDateTime(ComprobanteServicio.Consultafecha(_usuario));
                dtUltCierreSegundaValidacion = Convert.ToDateTime(ComprobanteServicio.Consultafecha(_usuario, "'G'"));
                requiereSegundaValidacion = true;
            }
        }
        catch (Exception)
        {
            VerError("No se encontro la fecha del último cierre contable");
            e.NewEditIndex = -1;
            return;
        }

        DateTime fechaComp = Convert.ToDateTime(gvLista.Rows[e.NewEditIndex].Cells[4].Text);
        if (fechaComp <= dtUltCierre)
        {
            VerError("No puede modificar comprobantes en períodos ya cerrados. Fecha Ultimo Cierre:" + dtUltCierre.ToShortDateString());
            e.NewEditIndex = -1;
            return;
        }
        if (requiereSegundaValidacion && fechaComp <= dtUltCierreSegundaValidacion)
        {
            VerError("No puede modificar comprobantes en períodos ya cerrados. Fecha Ultimo Cierre:" + dtUltCierreSegundaValidacion.ToShortDateString());
            e.NewEditIndex = -1;
            return;
        }

        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto1 = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[3].Text);
            Actualizar();
            Navegar(Pagina.Lista);
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "gvLista_RowDeleting", ex);
        }
    }

    protected void ListaPlanCuentas()
    {
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        List<PlanCuentas> LstPlanCuentas = new List<PlanCuentas>();
        PlanCuentas pPlanCuentas = new PlanCuentas();
        LstPlanCuentas = PlanCuentasServicio.ListarPlanCuentasLocal(pPlanCuentas, _usuario, "");
        ddlCodCuenta.DataSource = LstPlanCuentas;
        ddlCodCuenta.DataTextField = "cod_cuenta";
        ddlCodCuenta.DataValueField = "cod_cuenta";
        ddlCodCuenta.DataBind();
    }

    protected void ListaMonedas()
    {
        Xpinn.Caja.Entities.TipoMoneda eMoneda = new Xpinn.Caja.Entities.TipoMoneda();
        Xpinn.Caja.Services.TipoMonedaService TipoMonedaServicio = new Xpinn.Caja.Services.TipoMonedaService();
        List<Xpinn.Caja.Entities.TipoMoneda> LstMoneda = new List<Xpinn.Caja.Entities.TipoMoneda>();
        LstMoneda = TipoMonedaServicio.ListarTipoMoneda(eMoneda, _usuario);
        ddlMoneda.DataSource = LstMoneda;
        ddlMoneda.DataValueField = "cod_moneda";
        ddlMoneda.DataTextField = "descripcion";
        ddlMoneda.DataBind();
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Comprobantes.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = Encoding.Default;
            StringWriter sw = new StringWriter();
            ExpGrilla expGrilla = new ExpGrilla();

            sw = expGrilla.ObtenerGrilla(GridView1, (List<Xpinn.Contabilidad.Entities.Comprobante>)Session["DTLISTA"]);

            Response.Write(expGrilla.style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        catch
        { }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "ddlIdentificacion", "");
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        Imprimir();
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Reporte.Visible = false;
        pConsulta.Visible = true;
        Principal.Visible = true;
    }

    protected void btnImprime_Click(object sender, EventArgs e)
    {
        if (RpviewComprobante.Visible == true)
        {
            var bytes = RpviewComprobante.LocalReport.Render("PDF");
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "inline;attachment; filename=ListadoComprobantes.pdf");
            Response.BinaryWrite(bytes);
            Response.Flush(); // send it to the client to download
            Response.Clear();
        }


        if (RpviewComprobanteInd.Visible == true)
        {
            var bytes = RpviewComprobanteInd.LocalReport.Render("PDF");
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "inline;attachment; filename=ListadoComprobantes.pdf");
            Response.BinaryWrite(bytes);
            Response.Flush(); // send it to the client to download
            Response.Clear();
        }
    }

    public String vacios(String texto)
    {
        if (String.IsNullOrEmpty(texto))
        {
            return " ";
        }
        else
        {
            return texto;
        }
    }

    private void Imprimir()
    {
        if (Session["DTLISTA"] == null)
        {
            VerError("No ha seleccionado los comprobantes a imprimir");
            return;
        }

        // Determinar datos del usuario y entidad
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        // Cargar listado de comprobantes a imprimir
        List<Xpinn.Contabilidad.Entities.Comprobante> lstConsulta = new List<Xpinn.Contabilidad.Entities.Comprobante>();
        lstConsulta = (List<Xpinn.Contabilidad.Entities.Comprobante>)Session["DTLISTA"];

        //CREAR TABLA GENERAL;
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("num_comp");
        table.Columns.Add("tipo_comp");
        table.Columns.Add("nomtipo_comp");
        System.Data.DataColumn fecha = new System.Data.DataColumn();
        fecha.DataType = typeof(DateTime);
        fecha.ColumnName = "fecha";
        fecha.AllowDBNull = true;
        table.Columns.Add(fecha);
        table.Columns.Add("identificacion");
        table.Columns.Add("nombre");
        table.Columns.Add("concepto");
        table.Columns.Add("cod_cuenta");
        table.Columns.Add("nomcuenta");
        table.Columns.Add("centro_costo");
        System.Data.DataColumn valordebito = new System.Data.DataColumn();
        valordebito.DataType = typeof(decimal);
        valordebito.ColumnName = "valordebito";
        valordebito.AllowDBNull = true;
        table.Columns.Add(valordebito);
        System.Data.DataColumn valorcredito = new System.Data.DataColumn();
        valorcredito.DataType = typeof(decimal);
        valorcredito.ColumnName = "valorcredito";
        valorcredito.AllowDBNull = true;
        table.Columns.Add(valorcredito);
        table.Columns.Add("detalle");
        table.Columns.Add("tercero");
        table.Columns.Add("nomtercero");
        table.Columns.Add("soporte");
        table.Columns.Add("elaboro");
        table.Columns.Add("Observaciones");
        //CARGAR LOS DATOS DEL DETALLE
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];

        string cRutaDeImagen;
        cRutaDeImagen = new Uri(Server.MapPath("~/Images/LogoEmpresa.jpg")).AbsoluteUri;

        //RECUPERAR EL DETALLE DEL COMPROBANTE SELECCIONADO
        foreach (Comprobante compro in lstConsulta)
        {
            LstDetalleComprobante = ComprobanteServicio.ConsultarDetalleComprobante(compro.num_comp, compro.tipo_comp, (Usuario)Session["Usuario"]);
            TipoComprobanteService tipoCompServicio = new TipoComprobanteService();
            TipoComprobante tipocomp = new TipoComprobante();
            tipocomp = tipoCompServicio.ConsultarTipoComprobante(compro.tipo_comp, (Usuario)Session["Usuario"]);
            foreach (DetalleComprobante item in LstDetalleComprobante)
            {
                System.Data.DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = compro.num_comp;
                datarw[1] = compro.tipo_comp;
                datarw[2] = tipocomp.descripcion;
                datarw[3] = compro.fecha;
                datarw[4] = compro.iden_benef;
                datarw[5] = compro.nombre;
                datarw[6] = compro.descripcion_concepto;
                datarw[7] = item.cod_cuenta;
                datarw[8] = item.nombre_cuenta;
                datarw[9] = item.centro_costo;
                if (item.tipo == "D")
                {
                    datarw[10] = item.valor;
                    datarw[11] = DBNull.Value;
                }
                if (item.tipo == "C")
                {
                    datarw[10] = DBNull.Value;
                    datarw[11] = item.valor;
                }
                datarw[12] = item.detalle;
                datarw[13] = item.identificacion;
                datarw[14] = item.nom_tercero;
                datarw[15] = compro.soporte;
                datarw[16] = compro.elaboro;
                datarw[17] = compro.observaciones;
                table.Rows.Add(datarw);
            }
        }


        // Parámetros del comprobante
        ReportParameter[] param = new ReportParameter[2];
        param[0] = new ReportParameter("pEntidad", HttpUtility.HtmlDecode(pUsuario.empresa));
        param[1] = new ReportParameter("ImagenReport", cRutaDeImagen);

        RpviewComprobante.Visible = true;
        RpviewComprobanteInd.Visible = false;


        
        RpviewComprobante.LocalReport.EnableExternalImages = true;
        RpviewComprobante.LocalReport.SetParameters(param);

            var sa = RpviewComprobante.LocalReport.GetDefaultPageSettings();
        RpviewComprobante.LocalReport.DataSources.Clear();

     
        ReportDataSource rds = new ReportDataSource("dsListadoComp", table);
        RpviewComprobante.LocalReport.DataSources.Add(rds);
        RpviewComprobante.LocalReport.Refresh();


        Reporte.Visible = true;
        pConsulta.Visible = false;
        Principal.Visible = false;
    }

    private void Imprimir2()
    {

        if (Session["DTLISTA"] == null)
        {
            VerError("No ha seleccionado los comprobantes a imprimir");
            return;
        }

        // Determinar datos del usuario y entidad
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        // Cargar listado de comprobantes a imprimir
        List<Xpinn.Contabilidad.Entities.Comprobante> lstConsulta = new List<Xpinn.Contabilidad.Entities.Comprobante>();
        lstConsulta = (List<Xpinn.Contabilidad.Entities.Comprobante>)Session["DTLISTA"];

        //CREAR TABLA GENERAL;
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("num_comp");
        table.Columns.Add("tipo_comp");
        table.Columns.Add("nomtipo_comp");
        System.Data.DataColumn fecha = new System.Data.DataColumn();
        fecha.DataType = typeof(DateTime);
        fecha.ColumnName = "fecha";
        fecha.AllowDBNull = true;
        table.Columns.Add(fecha);
        table.Columns.Add("identificacion");
        table.Columns.Add("nombre");
        table.Columns.Add("concepto");
        table.Columns.Add("cod_cuenta");
        table.Columns.Add("nomcuenta");
        table.Columns.Add("centro_costo");
        System.Data.DataColumn valordebito = new System.Data.DataColumn();
        valordebito.DataType = typeof(decimal);
        valordebito.ColumnName = "valordebito";
        valordebito.AllowDBNull = true;
        table.Columns.Add(valordebito);
        System.Data.DataColumn valorcredito = new System.Data.DataColumn();
        valorcredito.DataType = typeof(decimal);
        valorcredito.ColumnName = "valorcredito";
        valorcredito.AllowDBNull = true;
        table.Columns.Add(valorcredito);
        table.Columns.Add("detalle");
        table.Columns.Add("tercero");
        table.Columns.Add("nomtercero");
        table.Columns.Add("soporte");
        table.Columns.Add("elaboro");
        table.Columns.Add("Observaciones");
        //CARGAR LOS DATOS DEL DETALLE
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];

        string cRutaDeImagen;
        cRutaDeImagen = new Uri(Server.MapPath("~/Images/LogoEmpresa.jpg")).AbsoluteUri;

        //RECUPERAR EL DETALLE DEL COMPROBANTE SELECCIONADO
        foreach (Comprobante compro in lstConsulta)
        {
            LstDetalleComprobante = ComprobanteServicio.ConsultarDetalleComprobante(compro.num_comp, compro.tipo_comp, (Usuario)Session["Usuario"]);
            TipoComprobanteService tipoCompServicio = new TipoComprobanteService();
            TipoComprobante tipocomp = new TipoComprobante();
            tipocomp = tipoCompServicio.ConsultarTipoComprobante(compro.tipo_comp, (Usuario)Session["Usuario"]);
            foreach (DetalleComprobante item in LstDetalleComprobante)
            {
                System.Data.DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = compro.num_comp;
                datarw[1] = compro.tipo_comp;
                datarw[2] = tipocomp.descripcion;
                datarw[3] = compro.fecha;
                datarw[4] = compro.iden_benef;
                datarw[5] = compro.nombre;
                datarw[6] = compro.descripcion_concepto;
                datarw[7] = item.cod_cuenta;
                datarw[8] = item.nombre_cuenta;
                datarw[9] = item.centro_costo;
                if (item.tipo == "D")
                {
                    datarw[10] = item.valor;
                    datarw[11] = DBNull.Value;
                }
                if (item.tipo == "C")
                {
                    datarw[10] = DBNull.Value;
                    datarw[11] = item.valor;
                }
                datarw[12] = item.detalle;
                datarw[13] = item.identificacion;
                datarw[14] = item.nom_tercero;
                datarw[15] = compro.soporte;
                datarw[16] = compro.elaboro;
                datarw[17] = compro.observaciones;
                table.Rows.Add(datarw);
            }
        }


        // Parámetros del comprobante
        ReportParameter[] param = new ReportParameter[2];
        param[0] = new ReportParameter("pEntidad", HttpUtility.HtmlDecode(pUsuario.empresa));
        param[1] = new ReportParameter("ImagenReport", cRutaDeImagen);

        RpviewComprobante.Visible = false;
        RpviewComprobanteInd.Visible = true;
        RpviewComprobanteInd.LocalReport.EnableExternalImages = true;
        RpviewComprobanteInd.LocalReport.SetParameters(param);

        var sa = RpviewComprobanteInd.LocalReport.GetDefaultPageSettings();
        RpviewComprobanteInd.LocalReport.DataSources.Clear();


        ReportDataSource rds = new ReportDataSource("dsListadoComp", table);
        RpviewComprobanteInd.LocalReport.DataSources.Add(rds);
        RpviewComprobanteInd.LocalReport.Refresh();


        Reporte.Visible = true;
        pConsulta.Visible = false;
        Principal.Visible = false;
    }


    protected void btnImprimir2_Click(object sender, EventArgs e)
    {
        Imprimir2();
    }
}