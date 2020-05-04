using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Servicios.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;

partial class Lista : GlobalWeb
{
    SolicitudServiciosServices SoliServicios = new SolicitudServiciosServices();
    PlanesTelefonicosService LineaTeleServicio = new PlanesTelefonicosService();
    PoblarListas PoblarLista = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(SoliServicios.CodigoProgramaActivacion, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoProgramaActivacion, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoProgramaActivacion, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
    }


    protected void cargarDropdown()
    {
        PoblarLista.PoblarListaDesplegable("PLANES_TELEFONICOS", ddlPlan, Usuario);
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoProgramaActivacion, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[SoliServicios.CodigoProgramaActivacion + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }
    
    private void Actualizar()
    {
        try
        {
            PlanTelefonico lineafiltro = new PlanTelefonico();
            if (txtNumLinea.Text != "")
                lineafiltro.num_linea_telefonica = txtNumLinea.Text;
            if (txtIdentificacion.Text != "")
                lineafiltro.identificacion_titular = txtIdentificacion.Text;
            if (ddlPlan.SelectedValue != "")
                lineafiltro.cod_plan = Convert.ToInt32(ddlPlan.SelectedValue);
            lineafiltro.estado = "I";

            List<PlanTelefonico> lstConsulta = LineaTeleServicio.ListarLineasTelefonicas(lineafiltro, Usuario);

            gvLista.PageSize = 20;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            Session["ListServicio"] = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
            }
            else
            {
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(SoliServicios.CodigoProgramaActivacion + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoProgramaActivacion, "Actualizar", ex);
        }
    }





}