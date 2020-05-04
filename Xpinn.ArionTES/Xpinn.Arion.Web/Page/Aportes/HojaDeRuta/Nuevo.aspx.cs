using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    ParametrizacionProcesoAfilicacionService _paramProceso = new ParametrizacionProcesoAfilicacionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(_paramProceso.CodigoProgramaH, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_paramProceso.CodigoProgramaH, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //CargarHistorialRuta();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_paramProceso.CodigoProgramaH, "Page_Load", ex);
        }
    }
    protected void llenarListaDetalle()
    {
        List<ParametrizacionProcesoAfiliacion> lstParamProceso = new List<ParametrizacionProcesoAfiliacion>();
        lstParamProceso = _paramProceso.ListarDetalleRuta(txtIdentificacion.Text, (Usuario)Session["Usuario"]);
        if(lstParamProceso.Count > 0)
        {
            gvDetalle.DataSource = lstParamProceso;
            gvDetalle.DataBind();
        }
        
    }
    protected void CargarHistorialRuta()
    {
        List<ParametrizacionProcesoAfiliacion> lstParamProceso = new List<ParametrizacionProcesoAfiliacion>();
        lstParamProceso = _paramProceso.ListarHistorialRuta(txtIdentificacion.Text, (Usuario)Session["Usuario"]);
        if(lstParamProceso.Count > 0)
        {
            gvLista.DataSource = lstParamProceso;
            gvLista.DataBind();
        }
        if(lstParamProceso.Count >= 1)
            llenarListaDetalle();
    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        CargarHistorialRuta();
    }

}