using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Service;
using Xpinn.Riesgo.Data;


partial class Lista : GlobalWeb
{
    alertasService alertaries = new alertasService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(alertaries.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(alertaries.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, alertaries.CodigoPrograma);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(alertaries.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, alertaries.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, alertaries.CodigoPrograma);
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[alertaries.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[alertaries.CodigoPrograma + ".id"] = id;
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
            BOexcepcion.Throw(alertaries.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void Actualizar()
    {
        try
        {
            List<alertas_ries> lstalertas = alertaries.Listaralertas(ObtenerValores(), Usuario);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstalertas;

            if (lstalertas.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstalertas.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(alertaries.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(alertaries.CodigoPrograma, "Actualizar", ex);
        }
    }

    alertas_ries ObtenerValores()
    {
        alertas_ries AleRies = new alertas_ries();

        if (txtCodigo.Text.Trim() != "")
            AleRies.Cod_Alerta = Convert.ToInt64(txtCodigo.Text.Trim());
        if (txtnom_alerta.Text.Trim() != "")
            AleRies.Nom_Alerta = Convert.ToString(txtnom_alerta.Text.Trim());
        //if (txtdescripcion.Text.Trim() != "")
        //    AleRies.Descripcion = Convert.ToString(txtdescripcion.Text.Trim());
        if (txtperiocidad.SelectedItem != null)
            if (txtperiocidad.SelectedItem.Value != "0")
                if (txtperiocidad.SelectedItem.Text != "")
                    AleRies.Periocidad = Convert.ToString(txtperiocidad.SelectedItem.Text);
        //if (txtSecue_sql.Text.Trim() != "")
        //    AleRies.Sentencia_Sql = Convert.ToString(txtSecue_sql.Text.Trim());
        //if (txtindicador.Text.Trim() != "")
        //    AleRies.Indicador = Convert.ToString(txtindicador.Text.Trim());

        return AleRies;
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
                alertas_ries pAler = new alertas_ries();
                pAler.Cod_Alerta = Convert.ToInt64(Session["ID"]);
                alertaries.Eliminaralertas(pAler, Usuario);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(alertaries.CodigoPrograma, "btnContinuarMen_Click", ex);
        }

    }
}