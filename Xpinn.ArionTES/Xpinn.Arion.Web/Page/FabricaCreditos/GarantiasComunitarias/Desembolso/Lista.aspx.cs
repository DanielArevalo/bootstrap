﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;


public partial class garantiascomunitarias : GlobalWeb
{
    GarantiasComunitariasService servicegrantias = new GarantiasComunitariasService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(servicegrantias.CodigoProgramaDesembolso, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicegrantias.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Cargar DDL de oficinas
                OficinaService oficinaService = new OficinaService();
                Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
                ddlOficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
                ddlOficina.DataTextField = "Nombre";
                ddlOficina.DataValueField = "Codigo";
                ddlOficina.DataBind();
                ddlOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

                // Ocultar botón de exportar
                btnExportar.Visible = false;

                // Mostrar los datos por defecto
                Label1.Visible = true;                     
                Label2.Visible = true;
                Label3.Visible = true;
                ucFecha.Visible = true;
                ucFecha0.Visible = true;
                ucFecha.ToDateTime = DateTime.Today;
                ucFecha0.ToDateTime = DateTime.Today;
                                        
             }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicegrantias.GetType().Name + "D", "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        List<GarantiasComunitarias> listgarantias = new List<GarantiasComunitarias>();
        Configuracion conf = new Configuracion();

        string fecha1 = System.DateTime.Now.ToString();
        string fecha2 = System.DateTime.Now.ToString();
        int oficina = 1;

        try
        {
            fecha1 = ucFecha.ToDateTime.ToString(conf.ObtenerFormatoFecha());
            fecha2 = ucFecha0.ToDateTime.ToString(conf.ObtenerFormatoFecha());
            oficina = Convert.ToInt32(ddlOficina.SelectedValue);
        }
        catch (Exception ex)
        {
            VerError(ex.Message + conf.ObtenerFormatoFecha());
            return;
        }

        listgarantias = servicegrantias.consultargarantiascomunitarias(fecha1, fecha2, oficina, (Usuario)Session["Usuario"]);
        gvMovGeneral.DataSource = listgarantias;
        gvMovGeneral.DataBind();
        btnExportar.Visible = true;
    }

    protected void gvMovGeneral_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMovGeneral.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicegrantias.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }



    /// <summary>
    /// Mostrar los datos del crèdito junto con los movimientos
    /// </summary>
    private void Actualizar()
    {
        try
        {
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicegrantias.GetType().Name + "L", "Actualizar", ex);
        }
    }


     protected void btnExportarExcel_Click(object sender, EventArgs e)
    {
        if (gvMovGeneral.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvMovGeneral.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvMovGeneral);
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


}