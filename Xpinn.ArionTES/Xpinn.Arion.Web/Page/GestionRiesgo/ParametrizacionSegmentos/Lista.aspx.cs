using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;

partial class Lista : GlobalWeb
{
    HistoricoSegmentacionService _historicoService = new HistoricoSegmentacionService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_historicoService.CodigoPrograma3, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma3, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, _historicoService.CodigoPrograma3);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma3, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, _historicoService.CodigoPrograma3);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, _historicoService.CodigoPrograma3);
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[_historicoService.CodigoPrograma3 + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[_historicoService.CodigoPrograma3 + ".id"] = id;
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
            BOexcepcion.Throw(_historicoService.CodigoPrograma3, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Segmentos> lstConsulta = _historicoService.ListarSegmentos(ObtenerValores(), Usuario);
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(_historicoService.CodigoPrograma3 + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma3, "Actualizar", ex);
        }
    }

    Segmentos ObtenerValores()
    {
        Segmentos vSegmento = new Segmentos();

        if (txtCodigo.Text.Trim() != "")
            vSegmento.codsegmento = Convert.ToInt32(txtCodigo.Text.Trim());
        if (txtDescripcion.Text.Trim() != "")
            vSegmento.nombre = Convert.ToString(txtDescripcion.Text.Trim().ToUpper());

        return vSegmento;
    }
    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
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


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["ID"].ToString() != "")
            {
                _historicoService.EliminarSegmentos(Convert.ToInt32(Session["ID"]), Usuario);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            //if(ex.)
            BOexcepcion.Throw(_historicoService.CodigoPrograma3, "btnContinuarMen_Click", ex);
        }
    }
   

}