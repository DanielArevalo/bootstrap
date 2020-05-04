using System;
using System.Linq;
using System.Web.UI;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class SimulacionInternaCredito : GlobalWeb
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    Configuracion global = new Configuracion();
    string FormatoFecha = " ";


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.SimulacionInterna, "L");
            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("SimulaciónCrédito", "Page_PreInit", ex);
        }
    }

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

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (ValidarConsulta())
        {
            Actualizar();
        }
    }


    Boolean ValidarConsulta()
    {
        if (txtMontoSolicitado.Text == "0" || string.IsNullOrWhiteSpace(txtMontoSolicitado.Text))
        {
            VerError("Ingrese el monto a solicitar, verifique los datos.");
            txtMontoSolicitado.Focus();
            return false;
        }
        if (txtNroCuotas.Text == "0" || string.IsNullOrWhiteSpace(txtNroCuotas.Text))
        {
            VerError("Ingrese el número de cuotas a solicitar, verifique los datos.");
            txtNroCuotas.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtInteresMensual.Text))
        {
            VerError("Ingrese una tasa de interés verifique los datos.");
            txtInteresMensual.Focus();
            return false;
        }

        if (ddlPeriodicidad.SelectedIndex == 0)
        {
            VerError("Seleccione la periodicidad para calcular el cupo de los créditos, verifique los datos.");
            ddlPeriodicidad.Focus();
            return false;
        }
        return true;
    }


    void Actualizar()
    {
        VerError("");
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
            datosApp.plazo = Convert.ToInt32(txtNroCuotas.Text);
            if (txtInteresMensual.Text != "")
                datosApp.tasa = Convert.ToDecimal(txtInteresMensual.Text);
            datosApp.periodic = Convert.ToInt32(ddlPeriodicidad.Text);
            datosApp.for_pag = 1;
            datosApp.fecha = DateTime.Today;

            xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient Acceso = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();

            List<xpinnWSEstadoCuenta.DatosPlanPagos> lstConsulta = Acceso.SimularPlanPagosInterno(datosApp);

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
            throw ex;
        }
    }


    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        VerError("");
        panelLineas.Visible = false;
        gvLista.DataSource = null;
        gvLista.DataBind();
        txtMontoSolicitado.Text = string.Empty;
        txtNroCuotas.Text = string.Empty;
        txtInteresMensual.Text = string.Empty;
        ddlPeriodicidad.SelectedIndex = 0;
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
            throw ex;
        }
    }
}