using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Reporteador.Entities;
using Xpinn.Reporteador.Services;
using Xpinn.Seguridad.Services;

public partial class Nuevo : GlobalWeb
{
    AuditoriaService _auditoriaService = new AuditoriaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_auditoriaService.CodigoProgramaAuditoriaTriggers, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_EventoClick;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_auditoriaService.CodigoProgramaAuditoriaTriggers, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InicializarPagina();
        }
    }

    void InicializarPagina()
    {
        ReporteService reporteService = new ReporteService();

        List<Tabla> listaTablas = reporteService.ListarTablaBase(new Tabla(), Usuario);

        ddlTablas.DataSource = listaTablas;
        ddlTablas.DataTextField = "nombre";
        ddlTablas.DataValueField = "nombre";
        ddlTablas.DataBind();
    }



    void btnGuardar_EventoClick(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(ddlOperacion.SelectedValue) && !string.IsNullOrWhiteSpace(ddlTablas.SelectedValue))
        {
            try
            {
                _auditoriaService.CrearTablaAuditoria(ddlTablas.SelectedValue, ddlOperacion.SelectedValue, Usuario);

                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                mvPrincipal.SetActiveView(mvFinal);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
        }
    }
}