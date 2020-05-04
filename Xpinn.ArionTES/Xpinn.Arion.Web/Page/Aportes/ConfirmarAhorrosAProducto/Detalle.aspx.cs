using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Ahorros.Services;
using Xpinn.Ahorros.Entities;

public partial class Detalle : GlobalWeb
{
    CruceCtaAProductoServices CruceService = new CruceCtaAProductoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CruceService.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;

            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CruceService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session[CruceService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CruceService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CruceService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CruceService.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void ObtenerDatos(string idObjeto)
    {
        try
        {
            Solicitud_cruce_ahorro pEntidad = new Solicitud_cruce_ahorro();
            List<Solicitud_cruce_ahorro> lstDatos = new List<Solicitud_cruce_ahorro>();
            string pFiltro = obtFiltro(idObjeto);
            pEntidad = CruceService.ConsultarSolicitud_cruce(pFiltro, (Usuario)Session["usuario"]);
            if (pEntidad != null)
            {
                if (pEntidad.idcruceahorro > 0)
                    lstDatos.Add(pEntidad);

                if (lstDatos.Count > 0)
                {
                    frmDatos.DataSource = lstDatos;
                    frmDatos.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CruceService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    private string obtFiltro(string pCodigo)
    {
        String filtro = String.Empty;
        if (pCodigo == null || pCodigo.Trim() == "")
            return null;
        filtro += " WHERE S.IDCRUCEAHORRO = " + pCodigo;
        
        return filtro;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


}