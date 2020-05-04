using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;
using Xpinn.Riesgo.Data;


partial class Lista : GlobalWeb
{
    valoracion_controlService valoracioncontrol = new valoracion_controlService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(valoracioncontrol.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(valoracioncontrol.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, valoracioncontrol.CodigoPrograma);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(valoracioncontrol.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, valoracioncontrol.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, valoracioncontrol.CodigoPrograma);
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[valoracioncontrol.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[valoracioncontrol.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(valoracioncontrol.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void Actualizar()
    {
        try
        {
            List<valoracion_control> lstvaloracion_control = valoracioncontrol.Listarvaloracion_control(ObtenerValores(), Usuario);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstvaloracion_control;

            if (lstvaloracion_control.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstvaloracion_control.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(valoracioncontrol.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(valoracioncontrol.CodigoPrograma, "Actualizar", ex);
        }
    }

    valoracion_control ObtenerValores()
    {
        valoracion_control ValoCon = new valoracion_control();

        if (txtCodigo.Text.Trim() != "")
            ValoCon.cod_control = Convert.ToInt64(txtCodigo.Text.Trim());
        if (txtDescripcion.Text.Trim() != "")
            ValoCon.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
        if (txtcalificacion.SelectedItem != null)
            if (txtcalificacion.SelectedItem.Value != "0")
                if (txtcalificacion.SelectedItem.Text != "")
                ValoCon.calificacion = Convert.ToString(txtcalificacion.SelectedItem.Text);
        return ValoCon;
    }

    public void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 id = Convert.ToInt32(e.Keys[0]);

            Session["ID"] = id;
            ctlMensaje.MostrarMensaje("Desea eliminar el registro seleccionado?");
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    public void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {

            if (Session["ID"].ToString() != "")
            {
                valoracion_control pValCod = new valoracion_control();
                pValCod.cod_control = Convert.ToInt64(Session["ID"]);
                valoracioncontrol.Eliminarvaloracion_control(pValCod, Usuario);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(valoracioncontrol.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    
    }
}