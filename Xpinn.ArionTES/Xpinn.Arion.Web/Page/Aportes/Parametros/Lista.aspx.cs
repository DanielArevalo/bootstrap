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
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    Par_Cue_LinApoervices ParametroService = new Par_Cue_LinApoervices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["opcion"] != null)
            {
                string codigoOpcion = Request.QueryString["opcion"].ToString().Trim();
                VisualizarOpciones(codigoOpcion, "L");                
            }

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoExportar += btnExportar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObtenerCodigoPrograma(), "Page_PreInit", ex);
        }
    }


    public string ObtenerCodigoPrograma()
    {
        string codPrograma = "";
        if (Request.QueryString["opcion"] != null)
        {
            codPrograma = Request.QueryString["opcion"].ToString().Trim();

            if (codPrograma == "20207") Session["OpcionParam"] = 1;
            if (codPrograma == "20206") Session["OpcionParam"] = 2;
            if (codPrograma == "20201") Session["OpcionParam"] = 3;
            if (codPrograma == "20208") Session["OpcionParam"] = 4;
            if (codPrograma == "20203") Session["OpcionParam"] = 5;
            if (codPrograma == "20205") Session["OpcionParam"] = 6;
            if (codPrograma == "20204") Session["OpcionParam"] = 7;
            if (codPrograma == "20210") Session["OpcionParam"] = 8;
        }
        return codPrograma;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropDown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObtenerCodigoPrograma(), "Page_Load", ex);
        }
    }

    void CargarDropDown()
    {
        ddlOrden.Items.Insert(0,new ListItem("Seleccione un item","0"));
        ddlOrden.Items.Insert(1, new ListItem("Código", "CODIGO"));
        ddlOrden.Items.Insert(2, new ListItem("Descripción", "DESCRIPCION"));
        ddlOrden.SelectedIndex = 0;
        ddlOrden.DataBind();    
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {        
        LimpiarPanel(pEncabezado);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        string codPrograma = Request.QueryString["opcion"].ToString().Trim();
        Navegar("~/Page/Aportes/Parametros/Nuevo.aspx?opcion="+ codPrograma);
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
            BOexcepcion.Throw(ParametroService.CodigoProgramaActivi, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        Int32 Opcion = Convert.ToInt32(gvLista.DataKeys[e.NewEditIndex].Values[1].ToString());
       
        Session[ObtenerCodigoPrograma() + ".id"] = id;
        ObtenerCodigoPrograma();        
        Navegar("~/Page/Aportes/Parametros/Nuevo.aspx?opcion=" + Request.QueryString["opcion"].ToString().Trim());
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string id = gvLista.DataKeys[e.RowIndex].Values[0].ToString();
        int opcion = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[1].ToString());
        Session["ID"] = id;
        Session["OPCIONPARAM"] = opcion;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación del Parametro?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ParametroService.EliminarParametroAporte(Session["ID"].ToString(),Convert.ToInt32(Session["OPCIONPARAM"].ToString()),(Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObtenerCodigoPrograma(), "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<ParametrosAporte> lstConsulta = new List<ParametrosAporte>();
            String filtro = obtFiltro();
            string orden = "";
            if (ddlOrden.SelectedIndex != 0)
                orden = ddlOrden.SelectedValue;

            lstConsulta = ParametroService.ListarParametrosAporte(filtro,orden,(Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTPARAMETRO"] = lstConsulta;
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
                Session["DTPARAMETRO"] = null;
            }
            Session.Add(ObtenerCodigoPrograma() + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ObtenerCodigoPrograma(), "Actualizar", ex);
        }
    }



    private string obtFiltro()
    {
        String filtro = String.Empty;

        if (txtCodigo.Text.Trim() != "")
            filtro += " and CODIGO = '" + txtCodigo.Text.Trim()+"'";       
        if (txtDescripcion.Text.Trim() != "")
            filtro += " and DESCRIPCION like '%" + txtDescripcion.Text.Trim() + "%'";
        ObtenerCodigoPrograma();
        filtro += "and COD_OPCION = " + Session["OpcionParam"].ToString();

        return filtro;
    }

    private void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["DTPARAMETRO"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.Columns[0].Visible = false;
            gvLista.Columns[1].Visible = false;            
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTPARAMETRO"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=ParametrosAporte.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }

}