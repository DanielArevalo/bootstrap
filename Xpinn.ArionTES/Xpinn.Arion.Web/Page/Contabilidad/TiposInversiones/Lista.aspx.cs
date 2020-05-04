using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Contabilidad.Entities;
using Xpinn.Contabilidad.Services;
using Xpinn.Util;
public partial class Lista : GlobalWeb
{
    InversionesService inversionesService = new InversionesService();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(inversionesService.CodigoProgramaTipoInv, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(inversionesService.CodigoProgramaTipoInv, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CargarValoresConsulta(pConsulta, inversionesService.CodigoProgramaTipoInv);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(inversionesService.CodigoProgramaTipoInv, "Page_Load", ex);
        }
    }
    

    private void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, inversionesService.CodigoProgramaTipoInv);
    }

    private void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, inversionesService.CodigoProgramaTipoInv);
            Actualizar();
        }
    }

    private void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    private string obtFiltro()
    {
        string pFiltro = string.Empty;
        if (txtCodigo.Text.Trim() != "")
            pFiltro += " AND COD_TIPO = " + txtCodigo.Text.Trim();
        if (txtDescripcion.Text.Trim() != "")
            pFiltro += " AND UPPER(DESCRIPCION) LIKE '%" + txtDescripcion.Text.ToUpper().Trim() + "%'";

        if (!string.IsNullOrEmpty(pFiltro))
        {
            pFiltro = pFiltro.Substring(4);
            pFiltro = " WHERE " + pFiltro;
        }
        return pFiltro;
    }

    private void Actualizar()
    {
        try
        {
            List<TipoInversiones> lstConsulta = new List<TipoInversiones>();
            string pFiltro = obtFiltro();
            lstConsulta = inversionesService.ListarTipoInversiones(pFiltro, Usuario);

            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                ViewState["DTTipoInv"] = lstConsulta;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                lblInfo.Visible = false;
                gvLista.DataBind();
            }
            else
            {
                ViewState["DTTipoInv"] = null;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(inversionesService.CodigoProgramaTipoInv + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(inversionesService.CodigoProgramaTipoInv, "Actualizar", ex);
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[inversionesService.CodigoProgramaTipoInv + ".id"] = id;
        Navegar(Pagina.Nuevo);
        e.NewEditIndex = -1;
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int32 id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Value.ToString());
        try
        {
            ViewState["ID"] = id;
            ctlMensaje.MostrarMensaje("Está seguro de eliminar el registro seleccionado?");
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
            if (ViewState["DTTipoInv"] != null)
            {
                List<TipoInversiones> lstConsulta = new List<TipoInversiones>();
                lstConsulta = (List<TipoInversiones>)ViewState["DTTipoInv"];
                if (lstConsulta.Count > 0)
                {
                    gvLista.Visible = true;
                    gvLista.DataSource = lstConsulta;
                    gvLista.DataBind();
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    lblInfo.Visible = false;
                }
                else
                {
                    gvLista.Visible = false;
                    lblTotalRegs.Visible = false;
                    lblInfo.Visible = true;
                }
            }
            else
                Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(inversionesService.CodigoProgramaTipoInv, "gvLista_PageIndexChanging", ex);
        }
    }


    private void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            if (ViewState["ID"] != null)
            {
                Int32 pId = Convert.ToInt32(ViewState["ID"]);
                inversionesService.EliminarTipoInversiones(pId, Usuario);
                Actualizar();
                ViewState["ID"] = null;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


}