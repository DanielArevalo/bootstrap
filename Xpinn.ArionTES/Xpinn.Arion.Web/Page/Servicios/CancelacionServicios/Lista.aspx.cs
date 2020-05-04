using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    AprobacionServiciosServices servicioCancelar = new AprobacionServiciosServices();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones("80114", "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicioCancelar.CodigoProgramaCancelacionServ, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDDL();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicioCancelar.CodigoProgramaCancelacionServ, "Page_Load", ex);
        }
    }

    private void CargarDDL()
    {
        PoblarListas Poblar = new PoblarListas();
        Poblar.PoblarListaDesplegable("LINEASSERVICIOS", ddlLinea, (Usuario)Session["usuario"]);
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        txtCodigoNomina.Text = "";
        txtFecha.Text = "";
        txtIdentificacion.Text = "";
        txtNombre.Text = "";
        txtNumServ.Text = "";
    }

    private void Actualizar()
    {
        try
        {
            List<Servicio> lstConsulta = new List<Servicio>();
            String filtro = obtFiltro();
            DateTime pFechaIni;
            pFechaIni = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;

            lstConsulta = servicioCancelar.ListarServicios(obtFiltro(), "", pFechaIni, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
            }
            else
            {
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicioCancelar.CodigoProgramaCancelacionServ, "Actualizar", ex);
        }
    }

    private string obtFiltro()
    {
        String filtro = String.Empty;

        if (txtNumServ.Text.Trim() != "")
            filtro += " and s.numero_servicio = " + txtNumServ.Text;
        if (ddlLinea.SelectedIndex != 0)
            filtro += " and s.COD_LINEA_SERVICIO = " + ddlLinea.SelectedValue;
        if (txtIdentificacion.Text != "")
            filtro += " and p.identificacion like '%" + txtIdentificacion.Text + "%'";
        if (txtNombre.Text.Trim() != "")
            filtro += " and p.primer_nombre ||' '|| p.segundo_nombre||' '|| p.primer_apellido||' '||p.segundo_apellido like '%" + txtNombre.Text.Trim().ToUpper() + "%'";
        if (txtCodigoNomina.Text != "")
            filtro += " and p.cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        filtro += " and s.estado in ('C','S','A') ";

        return filtro;
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[servicioCancelar.CodigoProgramaCancelacionServ + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }
}