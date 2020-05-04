using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Asesores.Services;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Entities;

public partial class Lista : GlobalWeb
{
    ExcelService _excelServicio = new ExcelService();
    CuadreCarteraService _cuadreServicio = new CuadreCarteraService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_cuadreServicio.CodigoPrograma, "L");
            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cuadreServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = Usuario;
            if (!IsPostBack)
            {
                DateTimeHelper dateHelper = new DateTimeHelper();
                DateTime date = DateTime.Now;

                txtFechaIni.Text = dateHelper.PrimerDiaDelMes(date).ToShortDateString();
                txtFechaFin.Text = dateHelper.UltimoDiaDelMes(date).ToShortDateString();

                CargarDropDown();
                CargarValoresConsulta(pConsulta, _cuadreServicio.CodigoPrograma);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cuadreServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void CargarDropDown()
    {
        Xpinn.Caja.Services.TipoOperacionService tipoopeservices = new Xpinn.Caja.Services.TipoOperacionService();
        List<Xpinn.Caja.Entities.TipoOperacion> lsttipo = new List<Xpinn.Caja.Entities.TipoOperacion>();

        lsttipo = tipoopeservices.ListarTipoProducto(_usuario);
        ddlTipoProducto.DataTextField = "nom_tipo_producto";
        ddlTipoProducto.DataValueField = "tipo_producto";
        ddlTipoProducto.DataSource = lsttipo;
        ddlTipoProducto.AppendDataBoundItems = true;
        ddlTipoProducto.Items.Insert(0, new ListItem("Seleccione un item", "0"));


        ddlTipoProducto.SelectedIndex = 0;
        ddlTipoProducto.DataBind();

        ddlDeudor.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlDeudor.Items.Insert(1, new ListItem("Asociado", "1"));
        ddlDeudor.Items.Insert(2, new ListItem("Empleado", "2"));
        ddlDeudor.SelectedIndex = 1;
        ddlDeudor.DataBind();
    }

    /// <summary>
    /// Método que permite generar el reporte segùn las condiciones dadas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
       
        VerError("");
        ViewState["lstConsultaMora"] = null;
        Page.Validate();
        if (Page.IsValid)
        {
            if (ddlTipoProducto.SelectedIndex == 0)
            {
                VerError("Seleccione el tipo de producto");
                ddlTipoProducto.Focus();
                return;
            }
            if (txtFechaIni.Text == "" || txtFechaFin.Text == "")
            {
                VerError("Ingrese el rango de fechas");
                return;
            }
            else
            {
                if (Convert.ToDateTime(txtFechaIni.Text) > Convert.ToDateTime(txtFechaFin.Text))
                {
                    VerError("No puede ingresar una fecha inicial mayor a la fecha final, verifique los datos ingresados");
                    return;
                }
            }
            // GuardarValoresConsulta(pConsulta, _cuadreServicio.CodigoPrograma);
            if (ddlTipoProducto.SelectedIndex != 0 && ViewState["lstConsultaMora"] != null ||  txtDiferencia.Text != "$ 0" || txtDiferencia.Text != "0") 
            {
                Actualizar();
                gvReporte.Visible = true;
                lblTotalRegs.Visible = true;
            }
        }
    }

    /// <summary>
    /// Méotodo para limpiar datos de la conuslta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        LimpiarValoresConsulta(pConsulta, _cuadreServicio.CodigoPrograma);
        gvReporte.DataSource = null;
        gvReporte.DataBind();
        txtFechaFin.Text = "";
        txtFechaIni.Text = "";
        lblTotalRegs.Text = "";
        ddlTipoProducto.SelectedIndex = 0;
        ddlTipoProducto.Enabled = true;
        txtFechaIni.Enabled = true;
        txtFechaFin.Enabled = true;
    }

    protected void gvReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReporte.PageIndex = e.NewPageIndex;

            if (ViewState["lstConsultaMora"] != null)
            {
                gvReporte.DataSource = (List<CuadreCartera>)ViewState["lstConsultaMora"];
                gvReporte.DataBind();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cuadreServicio.CodigoPrograma, "gvReporte_PageIndexChanging", ex);
        }
    }

    protected void gvReportes_SelectIndexChanged(object sender, EventArgs e)
    {
        VerError("");

        string codAtributo = gvReporte.SelectedRow.Cells[2].Text;
        string nombreAtributo = gvReporte.SelectedRow.Cells[3].Text;
        string numeroComprobante = gvReporte.SelectedRow.Cells[5].Text;
        string tipoComprobante = gvReporte.SelectedRow.Cells[6].Text;
        string descripcion = gvReporte.SelectedRow.Cells[7].Text;
        string fecha = gvReporte.SelectedRow.Cells[9].Text;
        string valorContable = gvReporte.SelectedRow.Cells[10].Text;
        string valorOperativo = gvReporte.SelectedRow.Cells[11].Text;
        string diferencia = gvReporte.SelectedRow.Cells[12].Text;
        string tipoDiferencia = gvReporte.SelectedRow.Cells[13].Text;

        if (!string.IsNullOrWhiteSpace(numeroComprobante) && !string.IsNullOrWhiteSpace(tipoComprobante) && !string.IsNullOrWhiteSpace(codAtributo))
        {
            StringHelper stringHelper = new StringHelper();
            CuadreCartera cuadre = new CuadreCartera();

            cuadre.cod_atr = Convert.ToInt64(codAtributo);
            cuadre.nom_atr = nombreAtributo;
            cuadre.num_comp = Convert.ToInt64(numeroComprobante);
            cuadre.tipo_comp = Convert.ToInt64(tipoComprobante);
            cuadre.detalle = descripcion;
            cuadre.fecha = !string.IsNullOrWhiteSpace(fecha) ? Convert.ToDateTime(fecha) : default(DateTime);
            cuadre.valor_contable = !string.IsNullOrWhiteSpace(valorContable) ? Convert.ToDecimal(stringHelper.DesformatearNumerosEnteros(valorContable)) : 0;
            cuadre.valor_operativo = !string.IsNullOrWhiteSpace(valorOperativo) ? Convert.ToDecimal(stringHelper.DesformatearNumerosEnteros(valorOperativo)) : 0;
            cuadre.diferencia = !string.IsNullOrWhiteSpace(diferencia) ? Convert.ToDecimal(stringHelper.DesformatearNumerosEnteros(diferencia)) : 0;
            cuadre.tipo = tipoDiferencia;
            cuadre.tipo_producto_enum = ddlTipoProducto.SelectedValue.ToEnum<TipoDeProducto>();

            Session.Add(_cuadreServicio.CodigoPrograma + ".cuadre", cuadre);

            Navegar(Pagina.Detalle);
        }
        else
        {
            VerError("No se pudo obtener el numero y/o tipo de comprobante");
        }
    }


    /// <summary>
    /// Método para traer los datos según las condiciones ingresadas
    /// </summary>
    private void    Actualizar()
    {
        VerError("");

        try
        {
            CuadreCartera pCuadre = new CuadreCartera();
            pCuadre.fecha_inicial = Convert.ToDateTime(txtFechaIni.Text);
            pCuadre.fecha_final = Convert.ToDateTime(txtFechaFin.Text);
            pCuadre.tipo_producto = Convert.ToInt64(ddlTipoProducto.SelectedValue);

            Xpinn.Contabilidad.Services.ComprobanteService CompServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            List<Xpinn.Contabilidad.Entities.Comprobante> lstOperaciones = new List<Xpinn.Contabilidad.Entities.Comprobante>();

            lstOperaciones = CompServicio.ContabilizarOperacionSinComp(pCuadre.fecha_inicial, pCuadre.fecha_final, pCuadre.tipo_producto, (Usuario)Session["usuario"]);

            if (lstOperaciones.Count > 0)
            {
                pOperacionSC.Visible = true;
                gvOperaciones.DataSource = lstOperaciones;
                gvOperaciones.DataBind();
                cpeDemo.CollapsedText = "(Click Aqui para ver el listado de operaciones sin contabilizar)";
                cpeDemo.ExpandedText = "(Click Aqui para ocultar el listado de operaciones sin contabilizar)";
                lblMostrarDetalles.Text = "(Click Aqui para ocultar el listado de operaciones sin contabilizar)";
                lblTotalOpe.Visible = true;
                lblTotalOpe.Text = "<br/> Registros de operaciones encontradas " + lstOperaciones.Count.ToString();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:gvOperacionScroll();", true);
            }

            if (txtDiferencia.Text != "$ 0")
            {


                List<CuadreCartera> lstConsultaMora = _cuadreServicio.ConsultaCuadre(pCuadre, Convert.ToInt32(ddlDeudor.SelectedValue), _usuario);
                gvReporte.DataSource = lstConsultaMora;
                Site toolBar = (Site)Master;

                if (lstConsultaMora.Count > 0)
                {
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaMora.Count.ToString();
                    toolBar.MostrarExportar(true);
                }
                else
                {
                    lblTotalRegs.Text = "<br/> No se encontraron Registros";
                    toolBar.MostrarExportar(false);

                }


                gvReporte.DataBind();
               // ViewState["lstConsultaMora"] = lstConsultaMora;



               // ddlTipoProducto_SelectedIndexChanged(null, null);
                CargarSaldosCuadre();

            }
            else
            {

                gvReporte.DataSource = null;
                gvReporte.DataBind();
                gvReporte.Visible = false;
            }



        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
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


    /// <summary>
    /// Método para exportar datos a excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        List<CuadreCartera> lstConsultaMora = null;
        // Exportar Datos
        if (ViewState["lstConsultaMora"] != null && (lstConsultaMora = (List<CuadreCartera>)ViewState["lstConsultaMora"]).Count > 0)
        {
            // Generar GridView
            GridView gvExportar = new GridView();
            int contador = 0;

            foreach (DataControlField ecol in gvReporte.Columns)
            {
                // Evitar que agarre la primera columna que tiene el botón
                if (contador == 0)
                {
                    contador++;
                    continue;
                }
                gvExportar.Columns.Add(ecol);
            }

            gvExportar.DataSource = lstConsultaMora;
            gvExportar.DataBind();

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvReporte.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            try
            {
                form.Controls.Add(gvExportar);
                pagina.RenderControl(htw);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
                return;
            }
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=CuadreOperativoVsContable.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }



    protected void ddlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvReporte.DataSource = null;
        ViewState["lstConsultaMora"] = null;
        if (ddlTipoProducto.SelectedValue == "2")
        {
            ddlDeudor.Visible = true;
            lblDeudor.Visible = true;
        }
        else
        {
            ddlDeudor.Visible = false;
            lblDeudor.Visible = false;
            CargarSaldosCuadre();
        }
    }

    private void CargarSaldosCuadre()
    {
        gvReporte.DataSource = null;
        ViewState["lstConsultaMora"] = null;
        TipoDeProducto tipoProducto = ddlTipoProducto.SelectedItem.Value.ToEnum<TipoDeProducto>();

        CuadreHistorico cuadreHistorico = ConsultarSaldosContableOperativo(tipoProducto);

        if (cuadreHistorico != null)
        {
            StringHelper stringHelper = new StringHelper();

            txtSaldoContable.Text = cuadreHistorico.saldo_contable != null ? stringHelper.FormatearNumerosComoCurrency(cuadreHistorico.saldo_contable.ToString().Replace(gSeparadorMiles, "")) : "0";
            txtSaldoOperativo.Text = cuadreHistorico.saldo_operativo != null ? stringHelper.FormatearNumerosComoCurrency(cuadreHistorico.saldo_operativo.ToString()) : "0";
            txtDiferencia.Text = cuadreHistorico.diferencia != null ? stringHelper.FormatearNumerosComoCurrency(cuadreHistorico.diferencia.ToString()) : "0";
        }
        else
        {
            txtSaldoContable.Text = string.Empty;
            txtSaldoOperativo.Text = string.Empty;
            txtDiferencia.Text = string.Empty;
        }

        gvReporte.Visible = false;
        lblTotalRegs.Visible = false;
    }


    CuadreHistorico ConsultarSaldosContableOperativo(TipoDeProducto tipoProducto)
    {
        CuadreHistorico cuadreHistorico = null;

        switch (tipoProducto)
        {
            case TipoDeProducto.Aporte:
                cuadreHistorico = _cuadreServicio.ConsultarSaldosYDiferenciaAportes(txtFechaFin.Text, _usuario);
                break;
            case TipoDeProducto.Credito:
                //Si no se selecciona el tipo de deudor, se carga por defecto Asociado
                int tipo_deudor = 1;
                tipo_deudor = ddlDeudor.SelectedValue != "" && ddlDeudor.SelectedValue != "0" ? Convert.ToInt32(ddlDeudor.SelectedValue) : 1;
                cuadreHistorico = _cuadreServicio.ConsultarSaldosYDiferenciaCreditos(txtFechaFin.Text, tipo_deudor, _usuario);
                break;
            case TipoDeProducto.AhorrosVista:
                cuadreHistorico = _cuadreServicio.ConsultarSaldosYDiferenciaAhorroVista(txtFechaFin.Text, _usuario);
                break;
            case TipoDeProducto.CDATS:
                cuadreHistorico = _cuadreServicio.ConsultarSaldosYDiferenciaCDATS(txtFechaFin.Text, _usuario);
                break;
            case TipoDeProducto.AhorroProgramado:
                cuadreHistorico = _cuadreServicio.ConsultarSaldosYDiferenciaAhorroProgramado(txtFechaFin.Text, _usuario);
                break;

            case TipoDeProducto.Servicios:
                cuadreHistorico = _cuadreServicio.ConsultarSaldosYDiferenciaServicios(txtFechaFin.Text, _usuario);
                break;


            case TipoDeProducto.ActivosFijos:
                cuadreHistorico = _cuadreServicio.ConsultarSaldosYDiferenciaActivosFijos(txtFechaFin.Text, _usuario);
                break;

        }

        return cuadreHistorico;
    }


    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


        }
    }

    protected void gvOperaciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int64 cod_ope = Convert.ToInt64(gvOperaciones.SelectedRow.Cells[1].Text);
        Session["operacion"] = cod_ope;
        Navegar("~/Page/Tesoreria/GenerarComprobante/Lista.aspx");
    }

    protected void ddlDeudor_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarSaldosCuadre();
    }
}
