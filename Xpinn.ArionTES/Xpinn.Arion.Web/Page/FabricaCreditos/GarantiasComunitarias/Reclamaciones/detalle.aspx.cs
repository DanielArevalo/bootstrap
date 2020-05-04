using System;
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
            VisualizarOpciones(servicegrantias.CodigoProgramaReclamaciones, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;

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
                Label3.Visible = true;
                ucFecha.Visible = true;
                ucFecha.ToDateTime = DateTime.Today;
                Actualizar();
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
    /// Mostrar los datos de las reclamaciones
    /// </summary>
    private void Actualizar()
    {
        List<GarantiasComunitarias> listgarantias = new List<GarantiasComunitarias>();
        int reclamacion = 0;

        if (Session["numero_reclamacion"].ToString() != "0")
        {
            reclamacion = Convert.ToInt32(Session["numero_reclamacion"].ToString());
            ucFecha.ToDateTime = Convert.ToDateTime(Session["fecha_reclamacion"].ToString());
        }
        else
        {
            reclamacion = 0;
        }

        listgarantias = servicegrantias.consultargarantiascomunitariasReclamacionesdetalle(ucFecha.ToDateTime.ToString("dd/MM/yyyy"), reclamacion, (Usuario)Session["Usuario"]);
        gvMovGeneral.DataSource = listgarantias;
        gvMovGeneral.DataBind();
    }

    /// <summary>
    /// Exportar el archivo a excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// Consultar los datos de las reclamaciones
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }


}