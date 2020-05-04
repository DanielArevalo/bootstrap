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
    xpinnWSLogin.Persona1 DataPersona = new xpinnWSLogin.Persona1();

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
            Label titulo = this.Master.FindControl("Lbltitulo") as Label;
            titulo.Text = "Actualización de datos";
            cargarCombos();
            lblError.Text = "";
            cblEstrato.Attributes.Add("onclick", "radioMe(event);");
            DateTime pFechaActual = DateTime.Now;
            cargarDatos();
        }
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
        lstBarrios = EstadoServicio.PoblarListaDesplegable("BARRIO", "codbarrio,nombre", "", "1", Session["sec"].ToString());
        //Llenando Barrio
        if (lstBarrios.Count > 0)
        {
            LlenarDrop(ddlBarrio, lstBarrios);
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstTipoContrato = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstTipoContrato = EstadoServicio.PoblarListaDesplegable("TIPOCONTRATO", "", "", "2", Session["sec"].ToString());
        //Llenando Tipo Contrato
        if (lstTipoContrato.Count > 0)
        {
            LlenarDrop(ddlTipoContrato, lstTipoContrato);
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstCargo;
        lstCargo = EstadoServicio.PoblarListaDesplegable("CARGO", "", "", "", Session["sec"].ToString());
        if (lstCargo.Count > 0)
            LlenarDrop(ddlCargo, lstCargo);
    }

    void LlenarDrop(DropDownList ddlDropCarga, List<xpinnWSEstadoCuenta.ListaDesplegable> listaData)
    {
        ddlDropCarga.DataSource = listaData;
        ddlDropCarga.DataTextField = "descripcion";
        ddlDropCarga.DataValueField = "idconsecutivo";
        ddlDropCarga.AppendDataBoundItems = true;
        if (ddlDropCarga.ID == "ddlDepartamento" || ddlDropCarga.ID == "ddlCiudad")
            ddlDropCarga.Items.Insert(0, new ListItem("Pendiente", "0"));
        else
            ddlDropCarga.Items.Insert(0, new ListItem("Seleccione un item", ""));
        ddlDropCarga.DataBind();
    }

    protected void cargarDatos()
    {
        if(Session["persona"] != null)
        {
            DataPersona = Session["persona"] as xpinnWSLogin.Persona1;
            //            entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
            //            entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);            
            if (DataPersona.nombres != null) txtNombre1.Text = DataPersona.nombres;
            txtNombre1.ReadOnly = true;
            if (DataPersona.apellidos != null) txtApellido1.Text = DataPersona.apellidos;
            txtApellido1.ReadOnly = true;
            if (!string.IsNullOrEmpty(DataPersona.email)) txtEmail.Text = DataPersona.email;
            if (!string.IsNullOrEmpty(DataPersona.telefono)) txtTelefono.Text = DataPersona.telefono;
            if (!string.IsNullOrEmpty(DataPersona.celular)) txtCelular.Text = DataPersona.celular;
            
        }        
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["persona"] != null)
            {
                DataPersona = Session["persona"] as xpinnWSLogin.Persona1;
                pEntidad.id_persona = DataPersona.cod_persona;
                pEntidad.identificacion = DataPersona.identificacion;
            }
                //DATOS CONTACTO
            pEntidad.direccion = txtDireccion.Text.Trim();
            pEntidad.barrio = ddlBarrio.SelectedValue != null ? Convert.ToInt64(ddlBarrio.SelectedValue) : 0;
            pEntidad.cod_pagaduria = ddlBarrio.SelectedValue != null ? Convert.ToInt32(ddlBarrio.SelectedValue) : 0;
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

            //--ddlPaisLaboral
            if (!string.IsNullOrWhiteSpace(txtDiaInicio.Text)) pEntidad.fecha_inicio = Convert.ToDateTime(txtDiaInicio.Text.ToString());
            if (ddlCargo.SelectedIndex > 0) pEntidad.codcargo = Convert.ToInt32(ddlCargo.SelectedValue);
            pEntidad.estado_empresa = "0";
            pEntidad.codtipocontrato = Convert.ToInt64(ddlTipoContrato.SelectedValue);
            pEntidad.salario = Convert.ToInt64(txtIngsalariomensual.Text.Replace(".", "").Replace(",", ""));
            pEntidad.email_contacto = txtCorreoCorporativo.Text.Trim() != "" ? txtCorreoCorporativo.Text.Trim() : "";
            pEntidad.telefono_empresa = txtTelefonolaboral.Text.Trim() != "" ? txtTelefonolaboral.Text.Trim() : null;
            pEntidad.profesion = txtProfesion.Text.Trim() != "" ? txtProfesion.Text.Trim().ToUpper() : null;

            //ALMACENAR INFORMACION
            pEntidad = EstadoServicio.ActualizarDatosWeb(pEntidad, Session["sec"].ToString());                  
            Response.Redirect("~/Pages/Afiliacion/R10_Temas.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("~/Pages/Afiliacion/R10_Temas.aspx");
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
                ddlBarrio.DataBind();
            }
        }
    }

}