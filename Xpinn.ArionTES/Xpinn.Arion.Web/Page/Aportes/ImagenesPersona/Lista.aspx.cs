using System;
using System.Web.UI.WebControls;
using Xpinn.Aportes.Services;


public partial class Lista : GlobalWeb
{
    ImagenesService _imagenService = new ImagenesService();
    Xpinn.Aportes.Services.AfiliacionServices _afiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_imagenService.CodigoPrograma, "D");
            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlBusquedaPersonas.eventoEditar += gvListaTitulares_SelectedIndexChanged;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_imagenService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Session.Remove(_imagenService.CodigoPrograma.ToString() + ".cod_persona");
        Session.Remove(_afiliacionServicio.CodigoPrograma + "last");
        Session.Remove(_afiliacionServicio.CodigoPrograma + "next");
        Session.Remove(_afiliacionServicio.CodigoPrograma + ".id");
        Session.Remove("lstParametros");
        ctlBusquedaPersonas.Filtro = "";
        ctlBusquedaPersonas.Actualizar(0);
    }


    protected void gvListaTitulares_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Determinar los datos de la persona seleccionada
        GridView gvListaAFiliados = (GridView)sender;
        string cod_persona = gvListaAFiliados.DataKeys[gvListaAFiliados.SelectedRow.RowIndex].Values["cod_persona"].ToString();

        Session[_imagenService.CodigoPrograma.ToString() + ".cod_persona"] = cod_persona;
        Navegar(Pagina.Nuevo);
    }
}