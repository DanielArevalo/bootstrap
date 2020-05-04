using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;

public partial class Detalle : GlobalWeb
{
    ScoringService _scoringService = new ScoringService();
    Usuario _usuario;
    long _idAnalisis;


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += (s, evt) => Navegar("ListaScoring.aspx");
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
            _idAnalisis = Convert.ToInt64(Session[_scoringService + ".id"]);

            if (!IsPostBack)
            {
                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_scoringService.CodigoPrograma, "Page_Load", ex);
        }
    }


    void InicializarPagina()
    {
        try
        {
            byte[] byteDocumento = _scoringService.ConsultarDocumentoScoring(_idAnalisis, _usuario);

            if (byteDocumento != null)
            {
                MostrarArchivoEnLiteral(byteDocumento, _usuario, ltPagare, "_scoring");
            }
            else
            {
                VerError("No se encontro documento de scoring para mostrar");
            }
        }
        catch (Exception ex)
        {
            VerError("Error al mostrar el pdf del scoring generado " + ex.Message);
        }
    }
}