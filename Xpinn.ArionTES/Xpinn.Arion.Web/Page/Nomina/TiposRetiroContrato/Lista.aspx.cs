using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    TipoContratoService service = new TipoContratoService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InicializarPagina();
        }
    }
    public void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(service.CodigoProgramaTiposRetiroContrato, "L");
            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;

            toolBar.eventoNuevo += (s, evt) => {
                Session.Remove(service.CodigoProgramaTiposRetiroContrato + ".id");
                Navegar(Pagina.Nuevo);
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(service.CodigoProgramaTiposRetiroContrato, "Page_PreInit", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.TipoRetiroContrato, ddlTipoRetiroContrato);
    

    }
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvContent.SelectedRow.Cells[2].Text);

        Session[service.CodigoProgramaTiposRetiroContrato + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    



    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtcodtiporetiro.Text = string.Empty;
    }

    void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)

    {
        string id = gvContent.DataKeys[e.NewEditIndex].Values[0].ToString();

        Session[service.CodigoProgramaTiposRetiroContrato + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    void Actualizar()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();

            List<TipoContrato> lstCantidad= service.ListarTipoRetiroContrato(filtro, Usuario);

            if (lstCantidad.Count > 0)
            {
                lblTotalRegs.Text = "Se encontraron " + lstCantidad.Count + " registros!.";
            }
            else
            {
                lblTotalRegs.Text = "Su consulta no obtuvo ningún resultado!.";
            }

            gvContent.DataSource = lstCantidad;
            gvContent.DataBind();
        }
        catch (Exception ex)
        {
            VerError("Error al actualizar la grilla, " + ex.Message);
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtcodtiporetiro.Text))
        {
            filtro += " and TIPORETIROCONTRATO.CONSECUTIVO  = " + txtcodtiporetiro.Text;
        }

        if (ddlTipoRetiroContrato.SelectedItem.Text != "Seleccione un Item")
        {
            filtro += " and TIPORETIROCONTRATO.DESCRIPCION  like '%" + ddlTipoRetiroContrato.SelectedItem.Text + "%'";
        }

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            StringHelper stringHelper = new StringHelper();
            filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);
        }

        return filtro;
    }
}