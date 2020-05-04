using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.Util;
using Xpinn.Seguridad.Services;
using Xpinn.Seguridad.Entities;

public partial class Lista : GlobalWeb
{
    Xpinn.Seguridad.Services.PerfilService perfilServicio = new Xpinn.Seguridad.Services.PerfilService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {       

        try
        {
            VisualizarOpciones(perfilServicio.CodigoPrograma, "L");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "Page_PreInit", ex);
        }

        Site toolBar = (Site)this.Master;        
        toolBar.eventoExportar += btnExportar_Click;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, perfilServicio.CodigoPrograma);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsulta_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, perfilServicio.CodigoPrograma);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "btnConsultar_Click", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, perfilServicio.CodigoPrograma);
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[3].Text;
        Session[perfilServicio.CodigoPrograma + ".id"] = id;
        Session[perfilServicio.CodigoPrograma + ".from"] = "l";
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        if (id.Trim() != "0")
        {
            Session[perfilServicio.CodigoPrograma + ".id"] = id;
            Navegar(Pagina.Editar);
        }
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 id = Convert.ToInt32(gvLista.Rows[e.RowIndex].Cells[3].Text);
            if (id != 0)
            {
                perfilServicio.EliminarPerfil(id, (Usuario)Session["usuario"]);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
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
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }
    //PageIndexChanging
    private void Actualizar()
    {
        try
        {
            List<Xpinn.Seguridad.Entities.Perfil> lstConsulta = new List<Xpinn.Seguridad.Entities.Perfil>();
            lstConsulta = perfilServicio.ListarPerfil(ObtenerValores(), (Usuario)Session["usuario"]);
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count == 0)
            {
                Xpinn.Seguridad.Entities.Perfil rFila = new Xpinn.Seguridad.Entities.Perfil();
                lstConsulta.Add(rFila);
                lblTotalRegs.Text = "<br/> Registros encontrados 0";
            }
            else
            {
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
            }
            lblTotalRegs.Visible = true;
            gvLista.Visible = true;
            lblInfo.Visible = false;
            gvLista.DataBind();
            Session["DTPERFILES"] = lstConsulta;
            ValidarPermisosGrilla(gvLista);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Seguridad.Entities.Perfil ObtenerValores()
    {
        Xpinn.Seguridad.Entities.Perfil Perfiles = new Xpinn.Seguridad.Entities.Perfil();
        if (txtCodigo.Text != "")
            Perfiles.codperfil = Convert.ToInt64(txtCodigo.Text);
        if (txtDescripcion.Text != "")
            Perfiles.nombreperfil = txtDescripcion.Text;
        return Perfiles;
    }


    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        GridView gvCopia = new GridView();
        List<Xpinn.Seguridad.Entities.Perfil> lstConsulta = new List<Xpinn.Seguridad.Entities.Perfil>();
        lstConsulta = (List<Xpinn.Seguridad.Entities.Perfil>)Session["DTPERFILES"];
        gvCopia.DataSource = lstConsulta;
        gvCopia.DataBind();
        ExportarExcelGrilla(gvCopia, "Perfiles");
    }


    protected void ExportarExcelDataTable(string NombreDataTable, string NombreArchivo)
    {
        GridView gvGrillaExcel = new GridView();
        DataTable dtTabla = new DataTable();
        dtTabla = (DataTable)Session[NombreDataTable];
        gvGrillaExcel.ID = "gv" + NombreDataTable + "Excel";
        gvGrillaExcel.HeaderStyle.CssClass = "gridHeader";
        gvGrillaExcel.PagerStyle.CssClass = "gridPager";
        gvGrillaExcel.RowStyle.CssClass = "gridItem";
        gvGrillaExcel.DataSource = dtTabla;
        gvGrillaExcel.DataBind();
        ExportarExcelGrilla(gvGrillaExcel, NombreArchivo);
    }

    protected void ExportarExcelGrilla(GridView gvGrilla, string Archivo)
    {
        try
        {
            if (gvGrilla.Rows.Count > 0)
            {
                string style = "";
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                style = "<link href=\"../../Styles/Styles.css\" rel=\"stylesheet\" type=\"text/css\" />";
                gvGrilla.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvGrilla);
                pagina.RenderControl(htw);
                Response.Clear();
                style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + Archivo + ".xls");
                Response.Charset = "UTF-8";
                Response.Write(sb.ToString());
                Response.End();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

   
}