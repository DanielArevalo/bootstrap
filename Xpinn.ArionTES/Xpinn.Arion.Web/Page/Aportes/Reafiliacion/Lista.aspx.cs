using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Xpinn.Util;
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Entities;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;



partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
    private Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    private Usuario usuario = new Usuario();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AfiliacionServicio.codigoprogramaReafiliacion, "L");
                
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarCancelar(false);
            toolBar.MostrarImprimir(false);
            ctlBusquedaPersonas.eventoEditar += gvLista_SelectedIndexChanged;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaReafiliacion, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                txtfechaProy.ToDateTime = DateTime.Now;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaReafiliacion, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ///inicializar los datos del control
        ///llamar el actualizar
      

       //  ctlBusquedaPersonas.Filtro = "";
        ctlBusquedaPersonas.Actualizar(0);
        
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarCancelar(false);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarImprimir(false);
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = ctlBusquedaPersonas.gvListado.SelectedDataKey.Value.ToString();
        Session[AfiliacionServicio.codigoprogramaReafiliacion + ".id"] = id;
        Session[AfiliacionServicio.codigoprogramaReafiliacion + ".modificar"] = 0;
        Navegar(Pagina.Nuevo);
    }

    
    
}