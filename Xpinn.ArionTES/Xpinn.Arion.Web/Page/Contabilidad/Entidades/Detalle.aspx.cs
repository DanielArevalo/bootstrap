using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

partial class Detalle : GlobalWeb
{
    private Xpinn.Caja.Services.BancosService BancosServicio = new Xpinn.Caja.Services.BancosService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[BancosServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(BancosServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(BancosServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BancosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[BancosServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[BancosServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(BancosServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BancosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }



    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Caja.Entities.Bancos vBancos = new Xpinn.Caja.Entities.Bancos();
            vBancos = BancosServicio.ConsultarBancos(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vBancos.cod_banco.ToString()))
                txtCodBanco.Text = HttpUtility.HtmlDecode(vBancos.cod_banco.ToString().Trim());
            if (!string.IsNullOrEmpty(vBancos.nombrebanco))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vBancos.nombrebanco.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BancosServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


}