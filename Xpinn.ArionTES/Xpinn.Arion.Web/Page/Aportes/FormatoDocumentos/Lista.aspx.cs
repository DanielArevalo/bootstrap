using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;

public partial class Lista : GlobalWeb
{
    FormatoDocumentoServices FormatoDocumentoServicio = new FormatoDocumentoServices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(FormatoDocumentoServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FormatoDocumentoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropDown();
                CargarValoresConsulta(pConsulta, FormatoDocumentoServicio.CodigoPrograma);
                if (Session[FormatoDocumentoServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FormatoDocumentoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void CargarDropDown()
    {
        ddlTipo.Items.Insert(0, new ListItem("Seleccione un item","0"));
        ddlTipo.Items.Insert(1, new ListItem("Afiliación", "1"));
        ddlTipo.Items.Insert(2, new ListItem("Aprobación Crédito", "2"));
        ddlTipo.Items.Insert(3, new ListItem("Cartas Paz y Salvo", "3"));
        ddlTipo.SelectedIndex = 0;
        ddlTipo.DataBind();
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, FormatoDocumentoServicio.CodigoPrograma);
            Navegar(Pagina.Nuevo);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FormatoDocumentoServicio.CodigoPrograma, "btnNuevo_Click", ex);
        }

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, FormatoDocumentoServicio.CodigoPrograma);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FormatoDocumentoServicio.CodigoPrograma, "btnConsultar_Click", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, FormatoDocumentoServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[3].Text;
        Session[FormatoDocumentoServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        Session[FormatoDocumentoServicio.CodigoPrograma + ".id"] = id;
        String tipo = gvLista.Rows[e.NewEditIndex].Cells[5].Text;
        Session[tipo] = tipo; 
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Value.ToString());
            FormatoDocumentoServicio.EliminarFormatoDocumento(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FormatoDocumentoServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(FormatoDocumentoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<FormatoDocumento> lstConsulta = new List<FormatoDocumento>();
            lstConsulta = FormatoDocumentoServicio.ListarFormatoDocumento(ObtenerValores(), (Usuario)Session["usuario"]);
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(FormatoDocumentoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FormatoDocumentoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private FormatoDocumento ObtenerValores()
    {
        FormatoDocumento FormDoc = new FormatoDocumento();
        if(txtCodigo.Text.Trim() != "")
            FormDoc.cod_documento = Convert.ToInt64(txtCodigo.Text.Trim());
        if (txtDescripcion.Text.Trim() != "")
            FormDoc.descripcion = txtDescripcion.Text.Trim().ToUpper();
        if (ddlTipo.SelectedIndex > 0)
            FormDoc.tipo = ddlTipo.SelectedValue;

        return FormDoc;
    }
}