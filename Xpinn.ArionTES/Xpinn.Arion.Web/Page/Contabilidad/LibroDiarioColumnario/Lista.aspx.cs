using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using Microsoft.CSharp;
using System.Text;
using System.IO;

partial class Lista : GlobalWeb
{
    Xpinn.Contabilidad.Services.LibroDiarioColumnarioService LibroDiarioColumnarioSer = new Xpinn.Contabilidad.Services.LibroDiarioColumnarioService();
    Xpinn.Contabilidad.Services.BalanceGeneralService BalanceGeneralSer = new Xpinn.Contabilidad.Services.BalanceGeneralService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["niff"] == "0" || Request.QueryString["niff"] == null)
            {
                VisualizarOpciones(LibroDiarioColumnarioSer.CodigoPrograma, "L");
                Site toolBar = (Site)this.Master;
                toolBar.eventoConsultar += btnConsultar_Click;
                toolBar.eventoLimpiar += btnLimpiar_Click;
                ViewState.Add("COD_PROGRAMA", "LOCAL");
            }
            else
            {
                VisualizarOpciones(LibroDiarioColumnarioSer.CodigoProgramaNIff, "L");
                Site toolBar = (Site)this.Master;
                toolBar.eventoConsultar += btnConsultar_Click;
                toolBar.eventoLimpiar += btnLimpiar_Click;
                ViewState.Add("COD_PROGRAMA", "NIIF");
            }   
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LibroDiarioColumnarioSer.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                LlenarCombos();
                btnInforme.Visible = false;
                btnExportar.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LibroDiarioColumnarioSer.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para llenar los DDLs requeridos para las consultas
    /// </summary>
    protected void LlenarCombos()
    {

        // LLenar el DDl de centro de costo
        Xpinn.Contabilidad.Services.CentroCostoService CentroCostoService = new Xpinn.Contabilidad.Services.CentroCostoService();
        List<Xpinn.Contabilidad.Entities.CentroCosto> LstCentroCosto = new List<Xpinn.Contabilidad.Entities.CentroCosto>();
        string sFiltro = "";
        LstCentroCosto = CentroCostoService.ListarCentroCosto(_usuario, sFiltro);
        ddlcentrocosto.DataSource = LstCentroCosto;
        ddlcentrocosto.DataTextField = "nom_centro";
        ddlcentrocosto.DataValueField = "centro_costo";
        ddlcentrocosto.DataBind();
        ddlcentrocosto.Items.Insert(0, new ListItem("CONSOLIDADO", "0"));
        // Determinando el centro de costo inicial y final
        Int64 CenIni = Int64.MinValue;
        Int64 CenFin = Int64.MinValue;
        foreach (Xpinn.Contabilidad.Entities.CentroCosto ItemCC in LstCentroCosto)
        {
            if (CenIni == Int64.MinValue)
                CenIni = ItemCC.centro_costo;
            if (CenFin == Int64.MinValue)
                CenFin = ItemCC.centro_costo;
            if (CenIni > ItemCC.centro_costo)
                CenIni = ItemCC.centro_costo;
            if (CenFin < ItemCC.centro_costo)
                CenFin = ItemCC.centro_costo;
        }
        Session["CenIni"] = CenIni;
        Session["CenFin"] = CenFin;

        //// LLenar el DDl de TipoMoneda
        //Xpinn.Contabilidad.Services.TipoMonedaService TipoMonedaService = new Xpinn.Contabilidad.Services.TipoMonedaService();
        //List<Xpinn.Contabilidad.Entities.TipoMoneda> LstTipoMoneda = new List<Xpinn.Contabilidad.Entities.TipoMoneda>();
        //LstTipoMoneda = TipoMonedaService.ListarTipoMoneda(_usuario);
        //ddlMoneda.DataSource = LstTipoMoneda;
        //ddlMoneda.DataTextField = "descripcion";
        //ddlMoneda.DataValueField = "tipo_moneda";
        //ddlMoneda.DataBind();

        // Llenar el DDL de la fecha de corte 
        List<Xpinn.Contabilidad.Entities.LibroDiarioColumnario> lstFechaCorte = new List<Xpinn.Contabilidad.Entities.LibroDiarioColumnario>();
        Xpinn.Contabilidad.Services.LibroDiarioColumnarioService LibroDiarioService = new Xpinn.Contabilidad.Services.LibroDiarioColumnarioService();
        Xpinn.Contabilidad.Entities.LibroDiarioColumnario LibroDiario = new Xpinn.Contabilidad.Entities.LibroDiarioColumnario();
        lstFechaCorte = LibroDiarioColumnarioSer.ListarFechaCorte(_usuario);
        ddlFechaCorte.DataSource = lstFechaCorte;
        ddlFechaCorte.DataTextFormatString = "{0:dd/MM/yyyy}";
        ddlFechaCorte.DataTextField = "fecha";
        //ddlFechaCorte.DataValueField = "fecha";
        ddlFechaCorte.DataBind();

    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        bool rpta = ViewState["COD_PROGRAMA"] != null && ViewState["COD_PROGRAMA"].ToString() == "NIIF" ? true : false;
        if (!rpta)
        {
            lblTipoNorma.Text = "Local";

            GuardarValoresConsulta(pConsulta, LibroDiarioColumnarioSer.CodigoPrograma);

            Actualizar(idObjeto, 0);

            Lblerror.Visible = false;

        }
        else
        {
            lblTipoNorma.Text = "NIIF";

            GuardarValoresConsulta(pConsulta, LibroDiarioColumnarioSer.CodigoPrograma);

            Actualizar(idObjeto, 1);

            Lblerror.Visible = false;
        }

    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, LibroDiarioColumnarioSer.CodigoPrograma);
        gvLista.DataSource = null;
        gvLista.DataBind();
        Lblerror.Visible = false;

    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LibroDiarioColumnarioSer.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[LibroDiarioColumnarioSer.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[LibroDiarioColumnarioSer.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            if (Session["DTLIBRODIARIO"] != null)
            {
                List<LibroDiarioColumnario> lstConsultaLibroDiario = new List<LibroDiarioColumnario>();
                lstConsultaLibroDiario = (List<LibroDiarioColumnario>)Session["DTLIBRODIARIO"];
                gvLista.DataSource = lstConsultaLibroDiario;
            }
            bool rpta = ViewState["COD_PROGRAMA"] != null && ViewState["COD_PROGRAMA"].ToString() == "NIIF" ? true : false;
            if (!rpta)
            {
                Actualizar(idObjeto, 0);
            }
            else
            {
                Actualizar(idObjeto, 1);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LibroDiarioColumnarioSer.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }    

    private void Actualizar(String pIdObjeto, int pTipoNomra)
    {

        try
        {
            String emptyQuery = "Fila de datos vacia";
            LibroDiarioColumnario datosApp = new LibroDiarioColumnario();

            String format = "dd/MM/yyyy";
            datosApp.fecha = DateTime.ParseExact(ddlFechaCorte.SelectedValue, format, CultureInfo.InvariantCulture);

            // Determinar la moneda

            datosApp.moneda = Convert.ToInt64(ddlMoneda.Value);

            // Determinar el rango de centros de costo
            try
            {
                if (ddlcentrocosto.SelectedValue.ToString() == "0")
                {
                    if (Session["CenIni"] != null && Session["CenFin"] != null)
                    {
                        datosApp.centro_costo = Convert.ToInt64(Session["CenIni"].ToString());
                        datosApp.centro_costo_fin = Convert.ToInt64(Session["CenFin"].ToString());
                    }
                    else
                    {
                        Xpinn.Contabilidad.Services.CentroCostoService CCSer = new Xpinn.Contabilidad.Services.CentroCostoService();
                        Int64 cenini = 0;
                        Int64 cenfin = 0;
                        CCSer.RangoCentroCosto(ref cenini, ref cenfin, _usuario);
                        datosApp.centro_costo = cenini;
                        datosApp.centro_costo_fin = cenfin;
                    }
                }
                else
                {
                    datosApp.centro_costo = Convert.ToInt64(ddlcentrocosto.SelectedValue);
                    datosApp.centro_costo_fin = Convert.ToInt64(ddlcentrocosto.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
                return;
            }

            // Determinar el nivel
            datosApp.nivel = Convert.ToInt64(ddlNivel.SelectedItem.Text);

            // Generar el Balance General
            List<LibroDiarioColumnario> lstConsultaLibroDiario = new List<LibroDiarioColumnario>();
            lstConsultaLibroDiario.Clear();
            if (pTipoNomra == 0)
            {
                lblTipoNorma.Text = "Local";
                lstConsultaLibroDiario = LibroDiarioColumnarioSer.ListarLibroDiario(datosApp, _usuario);
            }
            else
            {
                lblTipoNorma.Text = "NIIF";
                lstConsultaLibroDiario = LibroDiarioColumnarioSer.ListarLibroDiarioNiff(datosApp, _usuario);
            }
            gvLista.EmptyDataText = emptyQuery;
            Session["DTLIBRODIARIO"] = lstConsultaLibroDiario;
            gvLista.DataSource = lstConsultaLibroDiario;

            if (lstConsultaLibroDiario.Count > 0)
            {
                mvLibroDiario.ActiveViewIndex = 0;
                gvLista.DataBind();
                btnInforme.Visible = true;
                btnExportar.Visible = true;

                ValidarBalance(datosApp.fecha);
            }
            else
            {
                btnInforme.Visible = false;
                btnExportar.Visible = false;
            }
            Session.Add(LibroDiarioColumnarioSer.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LibroDiarioColumnarioSer.CodigoPrograma, "Actualizar", ex);
        }

    }

    private Xpinn.Util.Usuario ObtenerValores()
    {
        Xpinn.Util.Usuario vUsuario = new Xpinn.Util.Usuario();

        return vUsuario;
    }


    void ValidarBalance(DateTime fechaCorte)
    {
        if (fechaCorte != null && fechaCorte != DateTime.MinValue)
        {
            string error = BalanceGeneralSer.VerificarComprobantesYCuentas(fechaCorte, _usuario);

            if (!string.IsNullOrWhiteSpace(error))
            {
                VerError(error);
            }
        }
    }


    protected void chkCuentasCero_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkCompCentroCosto_CheckedChanged(object sender, EventArgs e)
    {

    }

    public DataTable CrearDataTableLibrioDiarioColum()
    {

        List<Xpinn.Contabilidad.Entities.LibroDiarioColumnario> lstConsultaLibroDiario = new List<Xpinn.Contabilidad.Entities.LibroDiarioColumnario>();
        lstConsultaLibroDiario = (List<Xpinn.Contabilidad.Entities.LibroDiarioColumnario>)Session["DTLIBRODIARIO"];

        // LLenar data table con los datos a recoger
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("TipoComprobante");
        table.Columns.Add("CodCuenta");
        table.Columns.Add("NombreCuenta");
        table.Columns.Add("Debito", typeof(double));
        table.Columns.Add("Credito", typeof(double));


        foreach (LibroDiarioColumnario item in lstConsultaLibroDiario)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.tipocomp;
            datarw[1] = item.cod_cuenta;
            datarw[2] = item.nombrecuenta;
            datarw[3] = item.debito.ToString("##,##0");
            datarw[4] = item.credito.ToString("##,##0");
            table.Rows.Add(datarw);
        }
        return table;
    }

    protected void btnInforme_Click(object sender, EventArgs e)
    {

        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTLIBRODIARIO"] == null)
        {
            Lblerror.Text = "No ha generado el Libro Diario Columnario para poder imprimir el reporte";
        }
        if (Session["DTLIBRODIARIO"] != null)
        {

            List<Xpinn.Contabilidad.Entities.LibroDiarioColumnario> lstConsultaLibroDiario = new List<Xpinn.Contabilidad.Entities.LibroDiarioColumnario>();
            lstConsultaLibroDiario = (List<Xpinn.Contabilidad.Entities.LibroDiarioColumnario>)Session["DTLIBRODIARIO"];

            Usuario pUsuario = _usuario;

            ReportParameter[] param = new ReportParameter[5];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("fecha", ddlFechaCorte.SelectedValue);
            param[3] = new ReportParameter("ImagenReport", ImagenReporte());
            param[4] = new ReportParameter("consecutivo", ConvertirStringToInt(txtConsecutivo.Text).ToString());

            mvLibroDiario.Visible = true;
            RptReporte.LocalReport.EnableExternalImages = true;
            RptReporte.LocalReport.SetParameters(param);
            RptReporte.LocalReport.DataSources.Clear();
            RptReporte.LocalReport.Refresh();

            ReportDataSource rds = new ReportDataSource("DataSetLibroDiaColum", CrearDataTableLibrioDiarioColum());

            RptReporte.LocalReport.DataSources.Add(rds);
            RptReporte.Visible = true;
            mvLibroDiario.ActiveViewIndex = 1;

        }

    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTLIBRODIARIO"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTLIBRODIARIO"];
                gvLista.DataBind();
                gvLista.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvLista);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }
    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvLibroDiario.ActiveViewIndex = 0;
    }
}