using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Nuevo : GlobalWeb
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.ActualizarDatos, "Aso");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("ActualizarDatos", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarDropDown();
            xpinnWSLogin.Persona1 Data = (xpinnWSLogin.Persona1)Session["persona"];

            if (Data.cod_persona != 0 && Data.identificacion != "" && Data.cod_persona != null && Data.identificacion != null)
            {
                ObtenerDatos();
            }
        }   
    }

    void CargarDropDown()
    {
        List<xpinnWSEstadoCuenta.ListaDesplegable> lstCiudades = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        
        lstCiudades = EstadoServicio.PoblarListaDesplegable("CIUDADES", "", " tipo = 3 ", "2", Session["sec"].ToString());
        xpinnWSEstadoCuenta.ListaDesplegable pEntidad = new xpinnWSEstadoCuenta.ListaDesplegable();
        

        if (lstCiudades.Count > 0)
        {
            ddlCiudadResid.DataSource = lstCiudades;
            ddlCiudadResid.DataTextField = "descripcion";
            ddlCiudadResid.DataValueField = "idconsecutivo";
            ddlCiudadResid.AppendDataBoundItems = true;
            ddlCiudadResid.Items.Insert(0, new ListItem("Seleccione un item", ""));
            ddlCiudadResid.DataBind();

            ddlCiudadOfi.DataSource = lstCiudades;
            ddlCiudadOfi.DataTextField = "descripcion";
            ddlCiudadOfi.DataValueField = "idconsecutivo";
            ddlCiudadOfi.AppendDataBoundItems = true;
            ddlCiudadOfi.Items.Insert(0, new ListItem("Seleccione un item", ""));
            ddlCiudadOfi.DataBind();
        }
    }

    void ObtenerDatos()
    {

        xpinnWSEstadoCuenta.PersonaActualizacion pValida = new xpinnWSEstadoCuenta.PersonaActualizacion();
        xpinnWSLogin.Persona1 pEntidad = (xpinnWSLogin.Persona1)Session["persona"];
        xpinnWSEstadoCuenta.Persona1 Datospersona = new xpinnWSEstadoCuenta.Persona1();
        Datospersona.cod_persona = pEntidad.cod_persona;
        Datospersona = EstadoServicio.ConsultarPersona(Datospersona);

        Session["Cod_Persona"] = Datospersona.cod_persona;
        Session["identificacion"] = Datospersona.identificacion;

        pValida = EstadoServicio.ConsultarPersona_actualizacion(Datospersona.cod_persona, Session["sec"].ToString());
        if (pValida.estado != null)
        {
            if (pValida.estado != Convert.ToInt32(1))
            {
                VerError("Los datos que realizó anteriormente aún no se han confirmado.");
            }
        }

        if (Datospersona.primer_nombre != "")
            txtNombre1.Text = Datospersona.primer_nombre;
        if (Datospersona.segundo_nombre != "")
            txtNombre2.Text = Datospersona.segundo_nombre;
        if (Datospersona.primer_apellido != "")
            txtApellido1.Text = Datospersona.primer_apellido;
        if (Datospersona.segundo_apellido != "")
            txtApellido2.Text = Datospersona.segundo_apellido;
        if (Datospersona.codciudadresidencia != 0 || Datospersona.codciudadresidencia != null)
            ddlCiudadResid.SelectedValue = Datospersona.codciudadresidencia.ToString();
        if (Datospersona.direccion != "")
            txtDireccionResid.Text = Datospersona.direccion;
        if (Datospersona.telefono != "")
            txtTelefonoResid.Text = Datospersona.telefono;

        //falta ciudad empresa
        if (Datospersona.ciudad != 0 || Datospersona.ciudad != null)
            ddlCiudadOfi.SelectedValue = Datospersona.ciudad.ToString();

        if (Datospersona.direccionempresa != "")
            txtDireccionOfi.Text = Datospersona.direccionempresa;
        if (Datospersona.telefonoempresa != "")
            txtTelefonoOfi.Text = Datospersona.telefonoempresa;
        if (Datospersona.email != "")
            txtEmail.Text = Datospersona.email;
        if (!string.IsNullOrEmpty(Datospersona.celular))
            txtCelularResid.Text = Datospersona.celular;
    }


    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        VerError("");
        xpinnWSEstadoCuenta.PersonaActualizacion pEntidad = new xpinnWSEstadoCuenta.PersonaActualizacion();
        xpinnWSLogin.Persona1 pDatos = new xpinnWSLogin.Persona1();
        pDatos = (xpinnWSLogin.Persona1)Session["persona"];

        pDatos.cod_persona = Convert.ToInt64(Session["Cod_Persona"].ToString());
        pDatos.identificacion = Session["identificacion"].ToString();
        pEntidad.cod_persona = pDatos.cod_persona; 
        if(txtNombre1.Text != "")
            pEntidad.primer_nombre = txtNombre1.Text;
        if(txtNombre2.Text != "")
            pEntidad.segundo_nombre = txtNombre2.Text;
        if(txtApellido1.Text != "")
            pEntidad.primer_apellido = txtApellido1.Text;
        if(txtApellido2.Text != "")
            pEntidad.segundo_apellido = txtApellido2.Text;

        if (ddlCiudadResid.SelectedIndex != 0)
            pEntidad.codciudadresidencia = Convert.ToInt64(ddlCiudadResid.SelectedValue);
        else
            pEntidad.codciudadresidencia = 0;

        if(txtDireccionResid.Text != "")
            pEntidad.direccion = txtDireccionResid.Text;

        if(txtTelefonoResid.Text != "")
            pEntidad.telefono = txtTelefonoResid.Text;

        if (!string.IsNullOrEmpty(txtCelularResid.Text))
            pEntidad.celular = txtCelularResid.Text;

        if (ddlCiudadOfi.SelectedIndex != 0) //Verificar
            pEntidad.ciudadempresa = Convert.ToInt32(ddlCiudadOfi.SelectedValue);
        else
            pEntidad.ciudadempresa = 0;
        
        if(txtDireccionOfi.Text != "")
            pEntidad.direccionempresa = txtDireccionOfi.Text;

        if(txtTelefonoOfi.Text != "")
            pEntidad.telefonoempresa = txtTelefonoOfi.Text;

        if(txtEmail.Text != "")
            pEntidad.email = txtEmail.Text;
        pEntidad.estado = 0; //pendiente
        pEntidad = EstadoServicio.InsertarPersonaActualizacion(pEntidad, Session["sec"].ToString());
        Session.Remove("Cod_Persona");
        Session.Remove("identificacion");
        Session["persona"] = pDatos;
        mvPrincipal.ActiveViewIndex = 1;

        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(false);
    }


    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }
}