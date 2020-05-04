using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : GlobalWeb
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            AdicionarTitulo(string.Empty, "L");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Index", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!Page.IsPostBack)
        {
            ConfigurarHyperlinks();
        }
    }

    // ACTIVACION DE LINKS EN BOTONES
    protected void ConfigurarHyperlinks()
    {
        if (Session["Procesos"] != null)
        {
        //    string UrlOption = FindOption(240102);
        //    ActiveUrl(hplEstadoCuenta, UrlOption);

        //    UrlOption = FindOption(240101);
        //    ActiveUrl(hplActualizarData, UrlOption);

        //    UrlOption = FindOption(240301);
        //    ActiveUrl(hplSimulacion, UrlOption);

        //    //UrlOption = FindOption(240401);
        //    //ActiveUrl(hplCertificacion, UrlOption);

        //    UrlOption = FindOption(240304);
        //    ActiveUrl(hplPlanPagos, UrlOption);

        //    UrlOption = FindOption(240103);
        //    ActiveUrl(hplModificarProduc, UrlOption);

        //    UrlOption = FindOption(240103);
        //    ActiveUrl(hplModificarProduc, UrlOption);            
        }
    }

    protected void ActiveUrl(HyperLink hplOption, string UrlOption)
    {
        if(!string.IsNullOrEmpty(UrlOption))
            hplOption.NavigateUrl = UrlOption;
    }

    protected string FindOption(long pOpcion)
    {
        ArrayList arOpciones = new ArrayList();
        string iniUrl = ResolveUrl("~/Pages");
        string UrlResponse = string.Empty;
        List<xpinnWSLogin.Acceso> lstAccesos = (List<xpinnWSLogin.Acceso>)Session["Procesos"];

        foreach (xpinnWSLogin.Acceso acc in lstAccesos)
        {
            if (acc.cod_opcion == pOpcion && acc.accion == "1")
            {
                UrlResponse = iniUrl + acc.ruta;
                break;
            }
        }
        return UrlResponse;
    }
    
}