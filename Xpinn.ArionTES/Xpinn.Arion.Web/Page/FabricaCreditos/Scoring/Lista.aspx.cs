using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    ScoringService _scoringService = new ScoringService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {

            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoRegresar += (s, evt) => Navegar("ListaScoring.aspx");
            ctlBusquedaPersonas.eventoEditar += eventoEditarBusquedaPersona;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_scoringService.CodigoPrograma, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_scoringService.CodigoPrograma, "Page_Load", ex);
        }
    }


    void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        ctlBusquedaPersonas.Filtro = "";
        ctlBusquedaPersonas.Actualizar(0);
    }


    void eventoEditarBusquedaPersona(object sender, EventArgs e)
    {
        GridView gvLista = sender as GridView;

        if (gvLista != null)
        {
            String cod_persona = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Values[0].ToString();
            Session[".idScoring"] = cod_persona;

            Navegar(Pagina.Nuevo);
        }
    }
}