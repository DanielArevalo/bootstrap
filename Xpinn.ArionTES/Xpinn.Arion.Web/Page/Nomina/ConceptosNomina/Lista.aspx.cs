using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Xpinn.Util;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class Page_Nomina_ConceptosNomina_Lista : GlobalWeb
{
    ConceptoNominaService _ConceptosNominoService = new ConceptoNominaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {

            VisualizarOpciones(_ConceptosNominoService.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ConceptosNominoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarListasDesplegables(TipoLista.TipoConceptoNomina, ddlTipoConcepto);
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public void ObtenerDatos(Usuario vUsuario)
    {
        try
        {
            string filtro = ObtenerFiltro();
            List<ConceptosNomina> lstConceptos = new List<ConceptosNomina>();
            lstConceptos = _ConceptosNominoService.ListarConceptosNomina(filtro,vUsuario);
            Session["DTConceptos"] = lstConceptos;
            gvLista.DataSource = lstConceptos;
            gvLista.DataBind();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtConsecutivo.Text))
        {
            filtro += " and con.consecutivo =" + txtConsecutivo.Text.Trim() ;
        }

        if (!string.IsNullOrWhiteSpace(txtDescripcion.Text))
        {
            filtro += " and con.descripcion = '" + txtDescripcion.Text.Trim() +"'";
        }

        if (!string.IsNullOrWhiteSpace(ddlTipo.SelectedValue))
        {
            filtro += " and con.TIPO = '" + ddlTipo.SelectedValue.Trim() +"'";
        }

        if (!string.IsNullOrWhiteSpace(ddlTipoConcepto.SelectedValue))
        {
            filtro += " and con.TIPOCONCEPTO  = " + ddlTipoConcepto.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(ddlClase.SelectedValue))
        {
            filtro += " and con.CLASE = " + ddlClase.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            StringHelper stringHelper = new StringHelper();
            filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);
        }

        return filtro;
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 id = Convert.ToInt32(gvLista.Rows[e.RowIndex].Cells[3].Text);
            ConceptosNomina entidad = new ConceptosNomina();
            entidad.CONSECUTIVO = id;
            _ConceptosNominoService.EliminarConceptoNomina(entidad, (Usuario)Session["usuario"]);
            ObtenerDatos((Usuario)Session["usuario"]);
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ConceptosNominoService.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            ObtenerDatos((Usuario)Session["usuario"]);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ConceptosNominoService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        Session[_ConceptosNominoService.CodigoPrograma + ".id"] = id;

        Navegar(Pagina.Editar);
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Usuario vUsuario = new Usuario();
        ObtenerDatos(vUsuario);
    }
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        try
        {
          
            GuardarValoresConsulta(pConsulta, _ConceptosNominoService.CodigoPrograma);
            Session[_ConceptosNominoService.CodigoPrograma + ".id"] = null;

            Navegar(Pagina.Nuevo);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ConceptosNominoService.CodigoPrograma, "btnNuevo_Click", ex);
        }
    }
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTConceptos"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTConceptos"];
                gvLista.DataBind();
                gvLista.Columns[0].Visible = false;
                  gvLista.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvLista);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=Conceptos de Nomina.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.UTF8;
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
    }

}