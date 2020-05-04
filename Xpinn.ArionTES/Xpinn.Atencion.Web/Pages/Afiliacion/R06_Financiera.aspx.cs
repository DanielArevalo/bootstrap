using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Afiliacion_Default : System.Web.UI.Page
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSAppFinancial.WSAppFinancialSoapClient AppService = new xpinnWSAppFinancial.WSAppFinancialSoapClient();
    xpinnWSEstadoCuenta.SolicitudPersonaAfi pEntidad = new xpinnWSEstadoCuenta.SolicitudPersonaAfi();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {            
            TextBox identificacion = Master.FindControl("IDENTIFICACION") as TextBox;

            txtOtrosing.TextChanged += txtOtrosing_TextChanged;
            txtDeducciones.TextChanged += txtDeducciones_TextChanged;
            txtIngsalariomensualCopia.TextChanged += txtIngsalariomensualCopia_TextChanged;
            TxtTotalActivos.TextChanged += txtPatrimonio_TextChanged;
            TxtTotalPasivos.TextChanged += txtPatrimonio_TextChanged;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        btnContinuar.Click += btnContinuar_Click;
        if (!Page.IsPostBack)
        {
            cargarCombos();
            cargarDatos();
        }

        if (!Page.IsPostBack)
        {
            lblError.Text = "";         
        }
    }

    private void cargarDatos()
    {
        if (Session["afiliacion"] != null)
            pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

        //DATOS FINANCIEROS        
        if (pEntidad.total_activos != null)
            TxtTotalActivos.Text = pEntidad.total_activos.ToString("C0");

        if (pEntidad.otros_ingresos != null)
            txtOtrosing.Text = Convert.ToDecimal(pEntidad.otros_ingresos).ToString("C0");

        if (pEntidad.total_pasivos != null)
            TxtTotalPasivos.Text = pEntidad.total_pasivos.ToString("C0");

        if (pEntidad.total_patrimonio != null)
            TxtTotalPatrimonio.Text = pEntidad.total_patrimonio.ToString("C0");

        if (pEntidad.egresos_mensuales != null)
            txtDeducciones.Text = pEntidad.egresos_mensuales.ToString("C0");

        if (pEntidad.ingresos_mensuales != null)
            txtTotalIng.Text = pEntidad.ingresos_mensuales.ToString("C0");

        if (!string.IsNullOrEmpty(pEntidad.detotros_ingresos))
            txtDetalleIngresos.Text = pEntidad.detotros_ingresos;

        if (pEntidad.salario != null)
            txtIngsalariomensualCopia.Text = Convert.ToDecimal(pEntidad.salario).ToString("C0");

        TotalizarIngresos();
    }

    protected void cargarCombos()
    {
    }

    void LlenarDrop(DropDownList ddlDropCarga, List<xpinnWSEstadoCuenta.ListaDesplegable> listaData)
    {
        ddlDropCarga.DataSource = listaData;
        ddlDropCarga.DataTextField = "descripcion";
        ddlDropCarga.DataValueField = "idconsecutivo";
        ddlDropCarga.AppendDataBoundItems = true;
        ddlDropCarga.Items.Insert(0, new ListItem("Seleccione un item", ""));
        ddlDropCarga.DataBind();
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["afiliacion"] != null)
                pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;
            
            //DATOS FINANCIEROS
            TxtTotalActivos.Text = string.IsNullOrEmpty(TxtTotalActivos.Text) ? "0" : TxtTotalActivos.Text;
            pEntidad.total_activos = Convert.ToInt64(TxtTotalActivos.Text.Replace(",", "").Replace(".", "").Replace("$",""));
            txtOtrosing.Text = string.IsNullOrEmpty(txtOtrosing.Text) ? "0" : txtOtrosing.Text.Replace("$", "").Replace(",", "").Replace(".", "");
            pEntidad.otros_ingresos = Convert.ToDecimal(txtOtrosing.Text.Replace(",", "").Replace(".", "").Replace("$", ""));
            if(pEntidad.otros_ingresos > 0)
            {
                if (string.IsNullOrEmpty(txtDetalleIngresos.Text.Trim()))
                {
                    lblError.Text = "Debe especificar el concepto de sus ingresos adicionales.";
                    return;
                }
            }

            TxtTotalPasivos.Text = string.IsNullOrEmpty(TxtTotalPasivos.Text) ? "0" : TxtTotalPasivos.Text;
            pEntidad.total_pasivos = Convert.ToInt64(TxtTotalPasivos.Text.Replace(",", "").Replace(".", "").Replace("$", ""));
            TxtTotalPatrimonio.Text = string.IsNullOrEmpty(TxtTotalPatrimonio.Text) ? "0" : TxtTotalPatrimonio.Text;
            pEntidad.total_patrimonio = Convert.ToInt64(TxtTotalPatrimonio.Text.Replace(",", "").Replace(".", "").Replace("$", ""));
            txtDeducciones.Text = string.IsNullOrEmpty(txtDeducciones.Text) ? "0" : txtDeducciones.Text.Replace("$", "").Replace(",", "").Replace(".", "");
            pEntidad.deducciones = Convert.ToDecimal(txtDeducciones.Text.Replace(",", "").Replace(".", "").Replace("$", ""));
            pEntidad.egresos_mensuales = Convert.ToInt64(pEntidad.deducciones);
            decimal total = (decimal)pEntidad.salario + (decimal)pEntidad.otros_ingresos;
            txtTotalIng.Text = string.IsNullOrEmpty(txtTotalIng.Text) ? total.ToString() : txtTotalIng.Text;
            pEntidad.ingresos_mensuales = Convert.ToInt64(txtTotalIng.Text.Replace(",", "").Replace(".", "").Replace("$", ""));
            pEntidad.detotros_ingresos = !string.IsNullOrEmpty(txtDetalleIngresos.Text) ? txtDetalleIngresos.Text : "";

            //ALMACENAR INFORMACION
            pEntidad = EstadoServicio.CrearSolicitudPersonaAfi(pEntidad, 6, Session["sec"].ToString());
            //ALMACENAR INFORMACION
            Session["afiliacion"] = pEntidad;

            if (ConfigurationManager.AppSettings["Empresa"] != null)
            {
                string empresa = ConfigurationManager.AppSettings["Empresa"].ToString();
                if (empresa == "FECEM")
                {
                    Response.Redirect("R07_Internacional.aspx");
                }
                else
                {
                    Response.Redirect("R11_Documentos.aspx");
                }
            }
            else
            {
                Response.Redirect("R11_Documentos.aspx");
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "ups: "+ex.Message;
        }
    }

    protected void TotalizarIngresos()
    {
        decimal pVrTotal = 0, pVrIngSalario = 0, pVrOtros = 0, pVrDeduccion = 0;

        pVrIngSalario = !string.IsNullOrWhiteSpace(txtIngsalariomensualCopia.Text) ?  Convert.ToDecimal(txtIngsalariomensualCopia.Text.Replace(".", "").Replace("$", "").Replace(",","")) : 0;
        pVrOtros = !string.IsNullOrWhiteSpace(txtOtrosing.Text) ? Convert.ToDecimal(txtOtrosing.Text.Replace(".", "").Replace("$", "").Replace(",", "")) : 0;
        pVrDeduccion = !string.IsNullOrWhiteSpace(txtDeducciones.Text) ? Convert.ToDecimal(txtDeducciones.Text.Replace(".", "").Replace("$", "").Replace(",", "")) : 0;
        pVrTotal = pVrIngSalario + pVrOtros;
        txtTotalIng.Text = pVrTotal.ToString("c0");
    }

    protected void TotalizarPatrimonio()
    {
        decimal pVrTotal = 0, pVrActivos = 0, pVrPasivos = 0;

        pVrActivos = Convert.ToDecimal(TxtTotalActivos.Text.Replace(".", "").Replace("$", "").Replace(",", ""));
        pVrPasivos = Convert.ToDecimal(TxtTotalPasivos.Text.Replace(".", "").Replace("$", "").Replace(",", ""));
        pVrTotal = pVrActivos - pVrPasivos;
        TxtTotalPatrimonio.Text = pVrTotal.ToString("c0");
    } 

    protected void txtPatrimonio_TextChanged(object sender, EventArgs e)
    {
        TotalizarPatrimonio();
    }
    protected void txtOtrosing_TextChanged(object sender, EventArgs e)
    {
        string OtrosIngresos = txtOtrosing.Text.Replace("$", "");
        if (string.IsNullOrEmpty(OtrosIngresos) || OtrosIngresos == "0")
        {
            txtDetalleIngresos.Visible = false;
        }        
        TotalizarIngresos();
    }
    protected void txtDeducciones_TextChanged(object sender, EventArgs e)
    {
        TotalizarIngresos();
    }

    protected void txtIngsalariomensualCopia_TextChanged(object sender, EventArgs e)
    {
        TotalizarIngresos();
    }
}