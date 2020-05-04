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
    TipoCotizanteService service = new TipoCotizanteService();

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
            VisualizarOpciones(service.CodigoPrograma, "L");
            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;

            toolBar.eventoNuevo += (s, evt) => {
                Session.Remove(service.CodigoPrograma + ".id");
                Navegar(Pagina.Nuevo);
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(service.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.TipoCotizante, ddlTipoCotizante);
    

    }
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvContent.SelectedRow.Cells[2].Text);

        Session[service.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    



    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtconsecutivo.Text = string.Empty;
    }

    void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)

    {
        string id = gvContent.DataKeys[e.NewEditIndex].Values[0].ToString();

        Session[service.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    void Actualizar()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();

            List<TipoCotizante> lstCantidad= service.ListarTipoCotizante(filtro, Usuario);

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

       if (txtconsecutivo.Text!="")
        {
            filtro += " and CONSECUTIVO  = " + txtconsecutivo.Text;
        }

        if (ddlTipoCotizante.SelectedItem.Text != "Seleccione un Item")
        {
            filtro += " and DESCRIPCION  like '%" + ddlTipoCotizante.SelectedItem.Text + "%'";
        }

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            StringHelper stringHelper = new StringHelper();
            filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);
        }

        return filtro;
    }
}