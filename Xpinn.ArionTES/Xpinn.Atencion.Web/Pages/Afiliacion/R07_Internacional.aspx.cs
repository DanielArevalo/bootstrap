using System;
using System.Collections.Generic;
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
            lblError.Text = "";
            cargarDatos();
        }

    }

    private void cargarDatos()
    {
        if (Session["afiliacion"] != null)
            pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

        //SECCION INTERNACIONAL
        if(pEntidad.operaciones_extrang != 0)
        {
            ChkOperaciones.SelectedValue = pEntidad.operaciones_extrang.ToString();

            if (!string.IsNullOrEmpty(pEntidad.nom_banco))
                TxtBanco.Text = pEntidad.nom_banco;

            if (pEntidad.tipo_cuenta_ext != null)
                ddlTipoProductoInt.SelectedValue = pEntidad.tipo_cuenta_ext.ToString();

            if (pEntidad.ncuenta !=  0)
                TxtNumeroCuenta.Text = pEntidad.ncuenta.ToString();

            if (pEntidad.promedio != null)
                txtPromedio.Text = pEntidad.promedio.ToString();

            if (pEntidad.moneda != null)
                TxtMoneda.Text = pEntidad.moneda;

            if (pEntidad.ciudadmoneda != null)
                TxtCiudad.Text = pEntidad.ciudadmoneda;

            if (pEntidad.paismoneda != null)
                TxtPaís.Text = pEntidad.paismoneda;
        }                
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

            //SECCION INTERNACIONAL
            pEntidad.operaciones_extrang = Convert.ToInt32(ChkOperaciones.SelectedValue);
            
            pEntidad.nom_banco = TxtBanco.Text.Trim() != "" ? TxtBanco.Text : null;
            pEntidad.tipo_cuenta_ext = ddlTipoProductoInt.SelectedValue != "" ? Convert.ToInt32(ddlTipoProductoInt.SelectedValue) : 0;
            pEntidad.ncuenta = TxtNumeroCuenta.Text.Trim() == "" ? 0 : Convert.ToInt64(TxtNumeroCuenta.Text);
            pEntidad.promedio = !string.IsNullOrEmpty(txtPromedio.Text.Trim()) ? Convert.ToDecimal(txtPromedio.Text.Trim()) : 0;
            pEntidad.moneda = TxtMoneda.Text.Trim() != "" ? TxtMoneda.Text : null;
            pEntidad.ciudadmoneda = TxtCiudad.Text.Trim() != "" ? TxtCiudad.Text : null;
            pEntidad.paismoneda = TxtPaís.Text.Trim() != "" ? TxtPaís.Text : null;

            //ALMACENAR INFORMACION
            pEntidad = EstadoServicio.CrearSolicitudPersonaAfi(pEntidad, 7, Session["sec"].ToString());                
            //ALMACENAR INFORMACION
            Session["afiliacion"] = pEntidad;

            Response.Redirect("R08_Autorizacion.aspx");
        }
        catch (Exception)
        {
        }
    }
}