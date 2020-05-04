using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;

public partial class Nuevo : GlobalWeb
{
    SeguimientoServices seguimientoServicio = new SeguimientoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[seguimientoServicio.CodigoProgramaM + ".id"] != null)
                VisualizarOpciones(seguimientoServicio.CodigoProgramaM, "E");
            else
                VisualizarOpciones(seguimientoServicio.CodigoProgramaM, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(seguimientoServicio.CodigoProgramaM, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                if (Session[seguimientoServicio.CodigoProgramaM + ".id"] != null)
                {
                    idObjeto = Session[seguimientoServicio.CodigoProgramaM + ".id"].ToString();
                    Session.Remove(seguimientoServicio.CodigoProgramaM + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(seguimientoServicio.CodigoProgramaM, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("GR_AREA_FUNCIONAL", "COD_AREA, DESCRIPCION", "", "1", ddlAreaEjecucion, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("GR_CARGO_ORGANIGRAMA", "COD_CARGO, DESCRIPCION", "", "1", ddlResponsable, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("GR_PERIODICIDAD", "COD_PERIODICIDAD, DESCRIPCION", "", "1", ddlPeriodicidad, (Usuario)Session["usuario"]);

        ddlEstadoAlerta.Items.Insert(0, new ListItem("Seleccione un item", ""));
        ddlEstadoAlerta.Items.Insert(1, new ListItem("Pasiva", "1"));
        ddlEstadoAlerta.Items.Insert(2, new ListItem("Pendiente", "2"));
        ddlEstadoAlerta.Items.Insert(3, new ListItem("Activa", "3"));
        ddlEstadoAlerta.DataBind();        
    }

    private bool ValidarDatos()
    {
        if (ddlPeriodicidad.SelectedValue == "")
        {    VerError("Ingrese la periodicidad del monitoreo");
            return false;
        }
        if (ddlAreaEjecucion.SelectedValue == "")
        {
            VerError("Ingrese el área donde se debe realizar el monitoreo");
            return false;
        }
        if (ddlResponsable.SelectedValue == "")
        {
            VerError("Ingrese el responsable de realizar el monitoreo");
            return false;
        }
        if (ddlEstadoAlerta.SelectedValue == "")
        {
            VerError("Ingrese el estado de la alerta");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                Seguimiento pMonitoreo = new Seguimiento();
                pMonitoreo.periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
                pMonitoreo.cod_area = Convert.ToInt64(ddlAreaEjecucion.SelectedValue);
                pMonitoreo.cod_cargo = Convert.ToInt64(ddlResponsable.SelectedValue);
                pMonitoreo.cod_alerta = Convert.ToInt32(ddlEstadoAlerta.SelectedValue);

                if (idObjeto == "")
                    pMonitoreo = seguimientoServicio.CrearTipoMonitoreo(pMonitoreo, (Usuario)Session["usuario"]);
                else
                {
                    pMonitoreo.cod_monitoreo = Convert.ToInt64(idObjeto);
                    pMonitoreo = seguimientoServicio.ModificarTipoMonitoreo(pMonitoreo, (Usuario)Session["usuario"]);
                }
                Navegar(Pagina.Lista);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar los datos " + ex.Message);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session.Remove(seguimientoServicio.CodigoProgramaM + ".id");
        Navegar(Pagina.Lista);
    }

    private void ObtenerDatos(string idObjeto)
    {
        Seguimiento pMonitoreo = new Seguimiento();
        pMonitoreo.cod_monitoreo = Convert.ToInt64(idObjeto);

        pMonitoreo = seguimientoServicio.ConsultarTipoMonitoreo(pMonitoreo, (Usuario)Session["usuario"]);

        if (pMonitoreo != null)
        {
            if (!string.IsNullOrEmpty(pMonitoreo.cod_monitoreo.ToString()))
                txtCodigoMonitoreo.Text = pMonitoreo.cod_monitoreo.ToString();
            if (!string.IsNullOrEmpty(pMonitoreo.cod_alerta.ToString()))
                ddlEstadoAlerta.SelectedValue = pMonitoreo.cod_alerta.ToString();
            if (!string.IsNullOrEmpty(pMonitoreo.cod_area.ToString()))
                ddlAreaEjecucion.SelectedValue = pMonitoreo.cod_area.ToString();
            if (!string.IsNullOrEmpty(pMonitoreo.cod_cargo.ToString()))
                ddlResponsable.SelectedValue = pMonitoreo.cod_cargo.ToString();
            if (!string.IsNullOrEmpty(pMonitoreo.periodicidad.ToString()))
                ddlPeriodicidad.SelectedValue = pMonitoreo.periodicidad.ToString();
        }
    }
}