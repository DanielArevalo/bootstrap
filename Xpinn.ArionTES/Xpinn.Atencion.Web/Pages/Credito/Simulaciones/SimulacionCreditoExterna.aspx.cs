using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class SimulacionCreditoExterna : GlobalWeb
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();

    Configuracion global = new Configuracion();
    string FormatoFecha = " ";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            panelLineas.Visible = false;
            CargarDropDown();
        }
    }


    void CargarDropDown()
    {
        List<xpinnWSEstadoCuenta.ListaDesplegable> lstPeriodicidad = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstPeriodicidad = EstadoServicio.PoblarListaDesplegable("PERIODICIDAD", "COD_PERIODICIDAD,DESCRIPCION", "", "1", Session["sec"].ToString());
        if (lstPeriodicidad.Count > 0)
        {
            ddlPeriodicidad.DataSource = lstPeriodicidad;
            ddlPeriodicidad.DataTextField = "descripcion";
            ddlPeriodicidad.DataValueField = "idconsecutivo";
            ddlPeriodicidad.AppendDataBoundItems = true;
            ddlPeriodicidad.Items.Insert(0, new ListItem("Seleccione un item", ""));
            ddlPeriodicidad.DataBind();
            ddlPeriodicidad.SelectedValue = "1";
        }
    }


    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        panelLineas.Visible = false;
        gvLista.DataSource = null;
        gvLista.DataBind();
        txtMontoSolicitado.Text = string.Empty;
        txtNumeroCuotas.Text = string.Empty;
        txtTasaInteres.Text = string.Empty;
        ddlPeriodicidad.SelectedIndex = 0;
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (ValidarConsulta())
        {
            Actualizar();
        }
    }


    Boolean ValidarConsulta()
    {
        lblMonto.Visible = false;
        lblCuotas.Visible = false;
        lblTasa.Visible = false;
        lblPeriodicidad.Visible = false;
        bool valido = true;

        if (txtMontoSolicitado.Text == "0" || string.IsNullOrWhiteSpace(txtMontoSolicitado.Text))
        {
            lblMonto.Visible = true;
            valido = false;
        }
        if (txtNumeroCuotas.Text == "0" || string.IsNullOrWhiteSpace(txtNumeroCuotas.Text))
        {
            lblCuotas.Visible = true;
            valido = false;
        }

        if (ddlPeriodicidad.SelectedIndex == 0)
        {
            lblPeriodicidad.Visible = true;
            valido = false;
        }

        if (string.IsNullOrWhiteSpace(txtTasaInteres.Text))
        {
            lblTasa.Visible = true;
            valido = false;
        }
        else
        {
            try
            {
                decimal pendejoNoMeQuites = Convert.ToDecimal(txtTasaInteres.Text);
            }
            catch (Exception)
            {
                lblTasa.Visible = true;
            }
        }

        return valido;
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
            BOexcepcion.Throw("Simulación Externa", "gvPlanPagos_PageIndexChanging", ex);
        }
    }


    void Actualizar()
    {
        xpinnWSEstadoCuenta.Simulacion datosApp = new xpinnWSEstadoCuenta.Simulacion();

        try
        {
            // Determinar separador de miles
            Configuracion global = new Configuracion();

            // Determinar el monto del crédito
            string texto1 = txtMontoSolicitado.Text;
            string[] sCadena = texto1.Split(',');
            string texto = sCadena[0];
            texto = texto.Replace(".", "");
            datosApp.monto = Convert.ToInt32(texto);

            // Determinar condiciones deseadas del crédito
            datosApp.plazo = Convert.ToInt32(txtNumeroCuotas.Text);
            if (txtTasaInteres.Text != "")
                datosApp.tasa = Convert.ToDecimal(txtTasaInteres.Text);
            datosApp.periodic = Convert.ToInt32(ddlPeriodicidad.Text);
            datosApp.for_pag = 1;
            datosApp.fecha = DateTime.Today;

            List<xpinnWSEstadoCuenta.DatosPlanPagos> lstConsulta = EstadoServicio.SimularPlanPagosInterno(datosApp);

            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                panelLineas.Visible = true;
                gvLista.DataBind();
            }
            else
            {
                panelLineas.Visible = true;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Simulación Externa", "Actualizar", ex);
        }
    }
}