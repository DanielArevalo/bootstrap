using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Drawing.Printing;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using Xpinn.Asesores.Services;
using Xpinn.Reporteador.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Indicadores.Entities;

partial class Lista : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.CreditoService ComprobanteServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
    Xpinn.Reporteador.Services.ReporteService reporte = new Xpinn.Reporteador.Services.ReporteService();
    Usuario _usuario;



    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    private void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();

    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            Configuracion conf = new Configuracion();
            List<Xpinn.FabricaCreditos.Entities.Credito> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Credito>();

            string sFiltro = " ";
            if (ucFechaInicial.TieneDatos)
                if (ucFechaInicial.ToDate.Trim() != "")
                    sFiltro += " And T2.FECHA_DESEMBOLSO >= To_Date('" + ucFechaInicial.ToDate.Trim() + "', '" + conf.ObtenerFormatoFecha() + "')";

            if (ucFechaFinal.TieneDatos)
                if (ucFechaFinal.ToDate.Trim() != "")
                    sFiltro += " And T2.FECHA_DESEMBOLSO <= To_Date('" + ucFechaFinal.ToDate.Trim() + "', '" + conf.ObtenerFormatoFecha() + "')";


            if (ddloficina.SelectedValue.Trim() != "")
                sFiltro += " And T2.COD_OFICINA In (" + ddloficina.SelectedValue.Trim() + ")";


            if (ddlCategoria.SelectedValue.Trim() != "")
            {
                sFiltro += " And T4.COD_CATEGORIA In (" + ddlCategoria.SelectedValueWithCommasInValue + ")";
            }


            if (ddlLinea.SelectedValue.Trim() != "")
                sFiltro += " And T2.COD_LINEA_CREDITO In (" + ddlLinea.SelectedValue.Trim() + ")";
            lstConsulta = reporte.ListarCreditosDesembolsados(ObtenerValores(), _usuario, sFiltro);


            gvLista.DataSource = lstConsulta;
            ViewState["DTLISTA"] = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados: " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(true);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(false);
            }

        }
        catch (System.OutOfMemoryException)
        {
            VerError("No se pudieron consultar los creditos desembolsados");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Credito ObtenerValores()
    {
        Credito Creditos = new Credito();

        try
        {
            Creditos.check = (chkHistorico.Checked) ? 1 : 0;

            //if (ucFechaInicial.ToDate.Trim() != "")
            //  Creditos.fecha_desembolso = ucFechaInicial.ToDate.Trim();

            //if (ucFechaInicial.ToDate.Trim() != "")
            //    Creditos.fecha_desembolso = ucFechaInicial.Text.Trim();

            // if(ddloficina.SelectedValue != null && ddloficina. != 0)
            //     Creditos.codigo_oficina = ddloficina.

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "ObtenerValores", ex);
        }

        return Creditos;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];

            if (!Page.IsPostBack)
            {

                ucFechaInicial.ToDateTime = new DateTime(DateTime.Today.Year, 1, 1);
                ucFechaFinal.ToDateTime = DateTime.Today;
                ucFechaInicial.Visible = true;
                ucFechaFinal.Visible = true;
                CargarDropDown();


            }
        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {

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
            BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    protected void CargarDropDown()
    {
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["Usuario"];

        Xpinn.Indicadores.Services.CarteraOficinasService carteraOficinaServicio = new Xpinn.Indicadores.Services.CarteraOficinasService();
        List<CarteraOficinas> lstFechas = new List<CarteraOficinas>();
        lstFechas = carteraOficinaServicio.consultarfecha((Usuario)Session["Usuario"]);


        Xpinn.FabricaCreditos.Services.OficinaService oficinaServicio = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        ddloficina.DataTextField = "nombre";
        ddloficina.DataValueField = "codigo";
        ddloficina.DataSource = oficinaServicio.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        ddloficina.DataBind();

        Xpinn.FabricaCreditos.Services.LineasCreditoService BOLinea = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        List<Xpinn.FabricaCreditos.Entities.LineasCredito> lstLineas = new List<Xpinn.FabricaCreditos.Entities.LineasCredito>();
        Xpinn.FabricaCreditos.Entities.LineasCredito pEntidad = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        pEntidad.estado = 1;
        lstLineas = BOLinea.ListarLineasCredito(pEntidad, (Usuario)Session["usuario"]);
        if (lstLineas.Count > 0)
        {
            ddlLinea.DataSource = lstLineas;
            ddlLinea.DataTextField = "nom_linea_credito";
            ddlLinea.DataValueField = "cod_linea_credito";
            ddlLinea.DataBind();
        }

        Xpinn.Cartera.Services.ClasificacionCarteraService BOCartera = new Xpinn.Cartera.Services.ClasificacionCarteraService();
        List<Xpinn.Cartera.Entities.ClasificacionCartera> lstCategoria = new List<Xpinn.Cartera.Entities.ClasificacionCartera>();
        lstCategoria = BOCartera.ListarCategorias((Usuario)Session["usuario"]);
        if (lstCategoria.Count > 0)
        {
            ddlCategoria.DataSource = lstCategoria;
            ddlCategoria.DataTextField = "descripcion";
            ddlCategoria.DataValueField = "categoria";
            ddlCategoria.DataBind();
        }


    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && ViewState["DTLISTA"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.AllowPaging = false;
            gvLista.DataSource = ViewState["DTLISTA"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=Credito_Desembolsados.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            gvLista.AllowPaging = true;
            gvLista.DataSource = ViewState["DTLISTA"];
            gvLista.DataBind();
            Response.End();
        }
    }



}
