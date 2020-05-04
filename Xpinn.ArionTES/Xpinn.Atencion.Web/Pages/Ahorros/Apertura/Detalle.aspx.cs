using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;


public partial class Pages_Ahorros_Apertura_Detalle : GlobalWeb
{
    xpinnWSDeposito.WSDepositoSoapClient service = new xpinnWSDeposito.WSDepositoSoapClient();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            AdicionarTitulo("Apertura Depositos", "L");
            Site toolBar = (Site)Master;
            toolBar.eventoNuevo += btnNuevo_Click;        
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Apertura", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            ObtenerDatos();
        }

    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        string iniUrl = ResolveUrl("~/Pages");
        string ruta = "/Ahorros/Apertura/Nuevo.aspx?cod_persona={0}";
        //int largo = 0;

        string urlFull = iniUrl + ruta;
        if (ruta.Contains("{0}"))
        {
            xpinnWSLogin.Persona1 pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            if (ruta.Contains("cod_persona"))
                urlFull = string.Format(urlFull, pPersona.cod_persona);
        }
        Response.Redirect(urlFull);
    }
    private void ObtenerDatos()
    {
        List<xpinnWSDeposito.SolicitudProductosWeb> lstAhorro = new List<xpinnWSDeposito.SolicitudProductosWeb>();
        lstAhorro = service.ListaSolicitud("WHERE S.TIPOAHORRO='1'", Session["sec"].ToString());
        if (lstAhorro.Count>0)
        {
            gvAhorros.DataSource = lstAhorro;
            gvAhorros.DataBind();
        }
        List<xpinnWSDeposito.SolicitudProductosWeb> lstProgramado = new List<xpinnWSDeposito.SolicitudProductosWeb>();
        lstProgramado = service.ListaSolicitud("WHERE S.TIPOAHORRO='2'", Session["sec"].ToString());
        if (lstProgramado.Count > 0)
        {
            gvAhoProgra.DataSource = lstProgramado;
            gvAhoProgra.DataBind();
        }
        List<xpinnWSDeposito.SolicitudProductosWeb> lstCDAT = new List<xpinnWSDeposito.SolicitudProductosWeb>();
        lstCDAT = service.ListaSolicitud("WHERE S.TIPOAHORRO='3'", Session["sec"].ToString());
        if (lstCDAT.Count > 0)
        {
            gvCDATS.DataSource = lstCDAT;
            gvCDATS.DataBind();
        }
    }

}