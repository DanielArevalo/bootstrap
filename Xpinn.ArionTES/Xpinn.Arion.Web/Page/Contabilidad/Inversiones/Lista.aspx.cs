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
    PoblarListas poblarLista = new PoblarListas();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(inversionesService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(inversionesService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CargarDropDown();
                CargarValoresConsulta(pConsulta, inversionesService.CodigoPrograma);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(inversionesService.CodigoProgramaTipoInv, "Page_Load", ex);
        }
    }

    private void CargarDropDown()
    {
        poblarLista.PoblarListaDesplegable("tipo_inversion", ddlTipoInv, Usuario);
    }

    private void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, inversionesService.CodigoPrograma);
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodigo", "txtIdentificacion", "txtNombres");
    }

    private void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, inversionesService.CodigoPrograma);
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
            pFiltro += " AND I.COD_INVERSION = " + txtCodigo.Text.Trim();
        if (txtNroTitulo.Text.Trim() != "")
            pFiltro += " AND I.NUMERO_TITULO = '" + txtNroTitulo.Text.ToUpper().Trim() + "'";
        if (txtFechaEmi.Text.Trim() != "")
            pFiltro += " AND I.FECHA_EMISION = TO_DATE('" + txtFechaEmi.Text + "','" + gFormatoFecha + "')";
        if (ddlTipoInv.SelectedIndex > 0)
            pFiltro += " AND I.COD_TIPO = " + ddlTipoInv.SelectedValue;
        if (txtIdentificacion.Text.Trim() != "")
            pFiltro += " AND I.COD_PERSONA = (SELECT Z.COD_PERSONA FROM Z.PERSONA WHERE Z.IDENTIFICACION = '" + txtIdentificacion.Text+ "') ";

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
            List<Inversiones> lstConsulta = new List<Inversiones>();
            string pFiltro = obtFiltro();
            lstConsulta = inversionesService.ListarInversiones(pFiltro, Usuario);

            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                ViewState["DTInversion"] = lstConsulta;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                lblInfo.Visible = false;
                gvLista.DataBind();
            }
            else
            {
                ViewState["DTInversion"] = null;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(inversionesService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(inversionesService.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[inversionesService.CodigoPrograma + ".id"] = id;
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
            if (ViewState["DTInversion"] != null)
            {
                List<Inversiones> lstConsulta = new List<Inversiones>();
                lstConsulta = (List<Inversiones>)ViewState["DTInversion"];
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
            BOexcepcion.Throw(inversionesService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
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
                inversionesService.EliminarInversiones(pId, Usuario);
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