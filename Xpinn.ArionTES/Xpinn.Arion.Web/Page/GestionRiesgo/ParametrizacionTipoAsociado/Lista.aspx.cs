using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;

partial class Lista : GlobalWeb
{
    TipoAsociadoServices _TipoAsociado = new TipoAsociadoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_TipoAsociado.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_TipoAsociado.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, _TipoAsociado.CodigoPrograma);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_TipoAsociado.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, _TipoAsociado.CodigoPrograma);
        Actualizar();
    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[_TipoAsociado.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        Session[_TipoAsociado.CodigoPrograma + ".id"] = id;
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
            BOexcepcion.Throw(_TipoAsociado.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<TipoAsociado> lstConsulta = _TipoAsociado.ListarTipoAsociado(ObtenerValores(), Usuario);

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

            Session.Add(_TipoAsociado.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_TipoAsociado.CodigoPrograma, "Actualizar", ex);
        }
    }

    TipoAsociado ObtenerValores()
    {
        TipoAsociado vTipoAsociado = new TipoAsociado();

        //if (txtCodigoPer.Text.Trim() != "")
        //    vTipoAsociado.Cod_perfil = Convert.ToInt64(txtCodigoPer.Text.Trim());
        //if (txtDescripcion.Text.Trim() != "")
        //    vTipoAsociado.Descripcion = Convert.ToString(txtDescripcion.Text.Trim());
        //if (txtValoracion.Text.Trim() != "")
        //    vTipoAsociado.valoracion = Convert.ToString(txtValoracion.Text.Trim());

        return vTipoAsociado;
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["ID"].ToString() != "")
            {
                TipoAsociado pTipoAsociado = new TipoAsociado();
                pTipoAsociado.Cod_tipoasociado = Convert.ToInt64(Session["ID"]);
                _TipoAsociado.EliminarTipoAsociado(pTipoAsociado, Usuario);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_TipoAsociado.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }


}