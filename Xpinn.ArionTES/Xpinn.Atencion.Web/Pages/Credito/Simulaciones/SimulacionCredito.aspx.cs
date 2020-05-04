using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class SimulacionCredito : GlobalWeb
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.SimulacionCredito, "L");
            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("SimulaciónCrédito","Page_PreInit", ex);
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

    protected Boolean CalcularMenosSMLMV()
    {
        if (txtnumeroSMLMV.SelectedValue == "3")
        {
            txtMenosSMLMV.Enabled = true;
            return true;
        }
        else
        {
            txtMenosSMLMV.Enabled = false;
        }
        // Determinar el valor del salrio mínimo
        xpinnWSEstadoCuenta.General salarios = new xpinnWSEstadoCuenta.General();
        salarios = EstadoServicio.consultarsalariominimo(10);        
        // Determinar el valor de la deducción
        decimal valorDeduccion = 0;
        try
        {
            if (txtnumeroSMLMV.SelectedValue == "1")
                valorDeduccion = Convert.ToInt64(txtnumeroSMLMV.SelectedItem.Value.Replace(".", "")) * Convert.ToInt64(salarios.valor.Replace(".", ""));
            else
                valorDeduccion = Math.Round(Convert.ToDecimal(0) / 2);
        }
        catch
        {
            return false;
        }
        txtMenosSMLMV.Text = Convert.ToString(valorDeduccion);
        return true;
    }

    protected void Actualizar()
    {
        try
        {
            xpinnWSLogin.Persona1 pPersona = new xpinnWSLogin.Persona1();
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            if (pPersona.cod_persona == 0)
                return;
            Int64 pCodPersona = pPersona.cod_persona;
            decimal pDisponible = (0 - Convert.ToInt64(txtMenosSMLMV.Text.Replace(".", "")));
            decimal pMontoSolicitado = Convert.ToDecimal(txtMontoSolicitado.Text.Replace(".",""));
            Int64 pNumeroCuotas = 0;
            if (txtNroCuotas.Text.Trim() != "")
                pNumeroCuotas = Convert.ToInt64(txtNroCuotas.Text);

            List<xpinnWSEstadoCuenta.Credito> lstCredito = new List<xpinnWSEstadoCuenta.Credito>();            
            lstCredito = EstadoServicio.RealizarPreAnalisis(DateTime.Now, pCodPersona, pDisponible, pNumeroCuotas, pMontoSolicitado, Convert.ToInt32(ddlPeriodicidad.SelectedValue), cbeducativo.Checked);
            if (lstCredito.Count <= 0)
            {
                xpinnWSEstadoCuenta.Credito credito = new xpinnWSEstadoCuenta.Credito();
                credito.cod_linea_credito = "";
                credito.cod_persona = 0;
                credito.monto = 0;
                lstCredito.ToList().Add(credito);
            }
            gvLista.PageIndex = 0;
            gvLista.Visible = true;
            gvLista.DataSource = lstCredito;
            gvLista.DataBind();
            if (lstCredito.Count() > 0)
                panelLineas.Visible = true;
            else
                panelLineas.Visible = false;
            for (int i = 0; i < gvLista.Columns.Count; i++)
            {
                if (gvLista.Columns[i].HeaderText.Contains("Auxilio"))
                {
                    if (cbeducativo.Checked)
                        gvLista.Columns[i].Visible = true;
                    else
                        gvLista.Columns[i].Visible = false;
                }
            }
            Session["DatosGrid"] = lstCredito;   
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void cbeducativo_CheckedChanged(object sender, EventArgs e)
    {
        // Actualizar la gridView si ya se generaron datos
        if (gvLista.Visible == true && gvLista.Rows.Count > 0)
        {
            VerError("");
            if (!validarConsulta())
                return;
            CalcularMenosSMLMV();
            Actualizar();
        }
    }

    private Boolean validarConsulta()
    {
        if (txtMontoSolicitado.Text == "0" || txtMontoSolicitado.Text == "")
        {
            VerError("Ingrese el monto a solicitar, verifique los datos.");
            txtMontoSolicitado.Focus();
            return false;
        }
        if (txtNroCuotas.Text == "0" || txtNroCuotas.Text == "")
        {
            VerError("Ingrese el número de cuotas a solicitar, verifique los datos.");
            txtNroCuotas.Focus();
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

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (!validarConsulta())
                return;
            CalcularMenosSMLMV();
            Actualizar();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    private void Limpiar()
    {
        VerError("");
        panelLineas.Visible = false;
        gvLista.DataSource = null;
        gvLista.DataBind();
        cbeducativo.Checked = false;
        txtMontoSolicitado.Text = "0";
        txtNroCuotas.Text = "";
        ddlPeriodicidad.SelectedIndex = 0;
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        Limpiar();
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex >= 0)
            {
                gvLista.PageIndex = e.NewPageIndex;
                if (Session["DatosGrid"] != null)
                {
                    xpinnWSEstadoCuenta.Credito[] lstCredito;
                    lstCredito = (xpinnWSEstadoCuenta.Credito[])Session["DatosGrid"];
                    gvLista.DataSource = lstCredito;
                    gvLista.DataBind();
                }
                else
                {
                    Actualizar();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBoxGrid chkSeleccionar = (CheckBoxGrid)sender;
            int rowIndex = Convert.ToInt32(chkSeleccionar.CommandArgument);
            if (chkSeleccionar != null)
            {
                foreach (GridViewRow rFila in gvLista.Rows)
                {
                    CheckBoxGrid chkSelec = (CheckBoxGrid)rFila.FindControl("chkSeleccionar");
                    if (chkSelec != null)
                    {
                        chkSelec.Checked = false;
                        if (rFila.RowIndex == rowIndex)
                            chkSelec.Checked = true;
                    }
                }
            }
        }
        catch
        {
        }
    }

}