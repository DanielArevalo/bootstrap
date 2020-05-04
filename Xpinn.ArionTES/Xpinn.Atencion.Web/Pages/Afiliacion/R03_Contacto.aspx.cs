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
            cblEstrato.Attributes.Add("onclick", "radioMe(event);");
            cargardatos();
        }

    }

    private void cargardatos()
    {
        if (Session["afiliacion"] != null)
            pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

        //DATOS CONTACTO
        if (!string.IsNullOrEmpty(pEntidad.direccion))
            txtDireccion.Text = pEntidad.direccion;

        if (pEntidad.barrio != null)
            ddlBarrio.SelectedValue = pEntidad.barrio.ToString();
        //--pEntidad.pais   ddlPaisResidencia

        if (pEntidad.departamento != null)
            ddlDepartamento.SelectedValue = pEntidad.departamento.ToString();

        if (pEntidad.ciudad != null)
            ddlCiudad.SelectedValue = pEntidad.ciudad.ToString();

        if (!string.IsNullOrEmpty(pEntidad.tipoVivienda))
            ddlTipoVivienda.SelectedValue = pEntidad.tipoVivienda;

        if (pEntidad.estrato != null)
        {
            cblEstrato.SelectedValue = pEntidad.estrato.ToString();
            if (pEntidad.afecta_vivienda != null)
                cbAfectaVivienda.SelectedValue = pEntidad.afecta_vivienda.ToString();
        }

        if (!string.IsNullOrEmpty(pEntidad.años_vivienda))
            txtAñosVivienda.Text = pEntidad.años_vivienda;

        if (!string.IsNullOrEmpty(pEntidad.meses_vivienda))
            txtMesesVivienda.Text = pEntidad.meses_vivienda;

        if (!string.IsNullOrEmpty(pEntidad.email))
            txtEmail.Text = pEntidad.email;
        if (!string.IsNullOrEmpty(pEntidad.telefono))
            txtTelefono.Text = pEntidad.telefono;
        if (!string.IsNullOrEmpty(pEntidad.celular))
            txtCelular.Text = pEntidad.celular;
    }

    protected void cargarCombos()
    {
        List<xpinnWSEstadoCuenta.ListaDesplegable> lstCiudades = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstCiudades = EstadoServicio.PoblarListaDesplegable("CIUDADES", "", " tipo = 3 ", "2", Session["sec"].ToString());
        xpinnWSEstadoCuenta.ListaDesplegable pEntidad = new xpinnWSEstadoCuenta.ListaDesplegable();

        //Llenando ciudades
        if (lstCiudades.Count > 0)
        {
            LlenarDrop(ddlCiudad, lstCiudades);
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstDepartamento = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstDepartamento = EstadoServicio.PoblarListaDesplegable("CIUDADES", "", " tipo = 2 ", "2", Session["sec"].ToString());
        //Llenando Departamentos
        if (lstDepartamento.Count > 0)
        {
            LlenarDrop(ddlDepartamento, lstDepartamento);
        }


        List<xpinnWSEstadoCuenta.ListaDesplegable> lstBarrios = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstBarrios = EstadoServicio.PoblarListaDesplegable("CIUDADES", "", " tipo = 6 ", "1", Session["sec"].ToString());
        //Llenando Barrio
        if (lstBarrios.Count > 0)
        {
            LlenarDrop(ddlBarrio, lstBarrios);
        }
    }

    void LlenarDrop(DropDownList ddlDropCarga, List<xpinnWSEstadoCuenta.ListaDesplegable> listaData)
    {
        ddlDropCarga.Items.Clear();
        ddlDropCarga.DataSource = listaData;
        ddlDropCarga.DataTextField = "descripcion";
        ddlDropCarga.DataValueField = "idconsecutivo";
        ddlDropCarga.AppendDataBoundItems = true;                   
        if(ddlDropCarga.ID == "ddlDepartamento" || ddlDropCarga.ID == "ddlCiudad" || ddlDropCarga.ID == "ddlBarrio")
            ddlDropCarga.Items.Insert(0, new ListItem("Pendiente", "0"));
        else
            ddlDropCarga.Items.Insert(0, new ListItem("Seleccione un item", ""));
        ddlDropCarga.DataBind();
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            if (Session["afiliacion"] != null)
                pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

            if (string.IsNullOrEmpty(txtDireccion.Text.Trim()))
            {
                lblError.Text = "Debe ingresar una dirección de residencia";
                return;
            }
            //DATOS CONTACTO
            pEntidad.direccion = txtDireccion.Text.Trim();
            pEntidad.barrio = ddlBarrio.SelectedValue != null ? Convert.ToInt64(ddlBarrio.SelectedValue) : 0;
            //--pEntidad.pais   ddlPaisResidencia
            pEntidad.departamento = ddlDepartamento.SelectedValue != "0" ? Convert.ToInt64(ddlDepartamento.SelectedValue) : 0;
            pEntidad.ciudad = Convert.ToInt64(ddlCiudad.SelectedValue);
            pEntidad.tipoVivienda = ddlTipoVivienda.SelectedValue != "0" ? ddlTipoVivienda.SelectedValue : "";
            pEntidad.estrato = cblEstrato.SelectedValue == "" ? 0 : Convert.ToInt32(cblEstrato.SelectedValue);
            pEntidad.afecta_vivienda = Convert.ToInt32(cbAfectaVivienda.SelectedValue);
            pEntidad.años_vivienda = !string.IsNullOrEmpty(txtAñosVivienda.Text) ? txtAñosVivienda.Text : "";
            pEntidad.meses_vivienda = !string.IsNullOrEmpty(txtMesesVivienda.Text) ? txtMesesVivienda.Text : "";
            pEntidad.email = txtEmail.Text.Trim();
            pEntidad.telefono = txtTelefono.Text.Trim() != "" ? txtTelefono.Text.Trim() : null;
            pEntidad.celular = txtCelular.Text.Trim() != "" ? txtCelular.Text.Trim() : null;

            //ALMACENAR INFORMACION
            pEntidad = EstadoServicio.CrearSolicitudPersonaAfi(pEntidad, 3, Session["sec"].ToString());
            //ALMACENAR INFORMACION
            Response.Redirect("R04_Laboral.aspx");
            Session["afiliacion"] = pEntidad;
        }
        catch (Exception)
        {

        }
    }

    protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlDepartamento.SelectedValue))
        {
            string filtro = "";
            filtro = " depende_de = " + ddlDepartamento.SelectedValue + " ";
            List<xpinnWSEstadoCuenta.ListaDesplegable> lstCiudades = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
            lstCiudades = EstadoServicio.PoblarListaDesplegable("CIUDADES", "", filtro, "2", Session["sec"].ToString());
            xpinnWSEstadoCuenta.ListaDesplegable pEntidad = new xpinnWSEstadoCuenta.ListaDesplegable();
            //Llenando ciudades
            if (lstCiudades.Count > 0)
            {
                ddlCiudad.Items.Clear();
                LlenarDrop(ddlCiudad, lstCiudades);
            }
        }
    }

    protected void ddlCiudad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlCiudad.SelectedValue))
        {
            string filtro = "";
            filtro = " depende_de = " + ddlCiudad.SelectedValue + " ";
            ddlBarrio.Items.Clear();
            List<xpinnWSEstadoCuenta.ListaDesplegable> lstBarrios = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
            lstBarrios = EstadoServicio.PoblarListaDesplegable("CIUDADES", "", filtro, "2", Session["sec"].ToString());
            //Llenando Barrio
            if (lstBarrios.Count > 0)
            {
                LlenarDrop(ddlBarrio, lstBarrios);
            }
            else
            {
                ddlBarrio.Items.Clear();
                ddlBarrio.Items.Insert(0, new ListItem("Seleccione un item", ""));
                ddlBarrio.Items.Insert(1,new ListItem("Pendiente", "0"));
                ddlBarrio.DataBind();
            }
        }
    }
}