using System;
using System.Collections.Generic;
using System.Globalization;
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

        //DATOS LABORALES

        if (pEntidad.empresa != null)
        {
            if(txtEmpresa.Visible)
                txtEmpresa.Text = pEntidad.empresa;
            else
                ddlEmpresa.SelectedValue = pEntidad.empresa;
        }

        if (pEntidad.cod_pagaduria > 0)
            ddlEmpresa.SelectedValue = pEntidad.cod_pagaduria.ToString();

        if (pEntidad.nit != null)
            txtNit.Text = pEntidad.nit;

        if (!string.IsNullOrEmpty(pEntidad.direccion_empresa))
            ddlZonaLaboral.SelectedValue = pEntidad.direccion_empresa;

        //--ddlPaisLaboral
        if (pEntidad.ciudad_empresa != null)
            ddlCiudadLaboral.SelectedValue = pEntidad.ciudad_empresa.ToString();

        if (pEntidad.departamento_empresa != null)
            ddlDepartamentoLaboral.SelectedValue = pEntidad.departamento_empresa.ToString();

        if (pEntidad.fecha_inicio != null)
            txtDiaInicio.Text = ((DateTime)pEntidad.fecha_inicio).ToString("dd/MM/yyyy");

        if (pEntidad.codcargo > 0)
            ddlCargo.SelectedValue = pEntidad.codcargo.ToString();

        if (pEntidad.codtipocontrato != null)
            ddlTipoContrato.SelectedValue = pEntidad.codtipocontrato.ToString();

        if (pEntidad.salario != null)
            txtIngsalariomensual.Text = pEntidad.salario.ToString();

        if (!string.IsNullOrEmpty(pEntidad.cod_nomina))
            txtCodNomina.Text = pEntidad.cod_nomina;

        if (!string.IsNullOrEmpty(pEntidad.email_contacto))
            txtCorreoCorporativo.Text = pEntidad.email_contacto;

        if (!string.IsNullOrEmpty(pEntidad.telefono_empresa))
            txtTelefonolaboral.Text = pEntidad.telefono_empresa;

        if (!string.IsNullOrEmpty(pEntidad.profesion))
            txtProfesion.Text = pEntidad.profesion;

        if (pEntidad.codescolaridad != null)
            cbNivelAcademico.SelectedValue = pEntidad.codescolaridad.ToString();

        if (pEntidad.actividad_economica != null)
            TxtDescripcionEconomica.Text = pEntidad.actividad_economica;

        if (pEntidad.ciiu != null)
            TxtCiiu.Text = pEntidad.ciiu;

        if (pEntidad.numero_empleados > 0)
            txtNumEmpleados.Text = pEntidad.numero_empleados.ToString();

        if(pEntidad.cod_nomina == "2" || pEntidad.cod_nomina == "3")        
            chkOcupacion.SelectedValue = pEntidad.cod_nomina;
        else
            chkOcupacion.SelectedValue = "1";

        chkOcupacion_SelectedIndexChanged(new object(), new EventArgs());
    }

    protected void cargarCombos()
    {
        List<xpinnWSEstadoCuenta.ListaDesplegable> lstDepartamento = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstDepartamento = EstadoServicio.PoblarListaDesplegable("CIUDADES", "", " tipo = 2 ", "2", Session["sec"].ToString());
        //Llenando Departamentos
        if (lstDepartamento.Count > 0)
        {
            LlenarDrop(ddlDepartamentoLaboral, lstDepartamento);
        }


        //llena ciudades
        List<xpinnWSEstadoCuenta.ListaDesplegable> lstCiudades = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstCiudades = EstadoServicio.PoblarListaDesplegable("CIUDADES", "", "tipo = 3 ", "2", Session["sec"].ToString());
        xpinnWSEstadoCuenta.ListaDesplegable pEntidad = new xpinnWSEstadoCuenta.ListaDesplegable();
        //Llenando ciudades
        if (lstCiudades.Count > 0)
        {
            ddlCiudadLaboral.Items.Clear();
            LlenarDrop(ddlCiudadLaboral, lstCiudades);
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstCargo;
        lstCargo = EstadoServicio.PoblarListaDesplegable("CARGO", "", "", "2", Session["sec"].ToString());
        if (lstCargo.Count > 0)
            LlenarDrop(ddlCargo, lstCargo);


        //Si es tipo 1 = cerrada carga las pagadurias, de lo contrario este dato se debe digitar. 
        xpinnWSAppFinancial.EntEmpresa emp = AppService.ConsultarEmpresa();
        if (emp != null)
        {
            if (emp.tipo_de_empresa == 1)
            {
                List<xpinnWSEstadoCuenta.ListaDesplegable> lstPagadurias = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
                lstPagadurias = EstadoServicio.PoblarListaDesplegable("empresa_recaudo", "cod_empresa, nom_empresa", " cod_empresa > 0 and tipo_recaudo <> 0 ", "2", Session["sec"].ToString());
                //Llena Pagadurias
                if (lstPagadurias.Count > 0)
                {
                    LlenarDrop(ddlEmpresa, lstPagadurias);
                    ddlEmpresa.Visible = true;
                    ddlEmpresavalidator.Enabled = true;
                }
                else
                {
                    txtEmpresa.Visible = true;
                    txtEmpresaValidator.Enabled = true;
                }
            }
            else
            {
                txtEmpresa.Visible = true;
                txtEmpresaValidator.Enabled = true;
            }
        }                

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstZonas = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstZonas = EstadoServicio.PoblarListaDesplegable("zonas", "cod_zona, descripcion", "", "2", Session["sec"].ToString());
        //Llena Zonas en campo de dirección laboral
        if (lstZonas.Count > 0)
        {
            LlenarDrop(ddlZonaLaboral, lstZonas);
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstTipoContrato = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstTipoContrato = EstadoServicio.PoblarListaDesplegable("TIPOCONTRATO", "", "", "2", Session["sec"].ToString());
        //Llenando Tipo Contrato
        if (lstTipoContrato.Count > 0)
        {
            LlenarDrop(ddlTipoContrato, lstTipoContrato);
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstNivelEsc = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstNivelEsc = EstadoServicio.PoblarListaDesplegable("NIVELESCOLARIDAD", "", "", "1", Session["sec"].ToString());
        if (lstNivelEsc.Count > 0)
        {
            for (int i = 0; i < lstNivelEsc.Count(); i++)
            {
                cbNivelAcademico.Items.Add(new ListItem(" " + lstNivelEsc[i].descripcion.ToString().Trim() + " ", lstNivelEsc[i].idconsecutivo.ToString()));
            }
        }


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

            if (chkOcupacion.SelectedValue == "1")
            {
                //DATOS LABORALES
                if (txtEmpresa.Visible)
                    pEntidad.empresa = txtEmpresa.Text;
                else
                {
                    pEntidad.empresa = ddlEmpresa.SelectedValue;
                    pEntidad.cod_pagaduria = ddlEmpresa.SelectedValue != "0" ? Convert.ToInt32(ddlEmpresa.SelectedValue) : 0;
                    pEntidad.empresa_contacto = ddlEmpresa.SelectedItem.Text;
                }
                pEntidad.nit = txtNit.Text.Trim();
                pEntidad.direccion_empresa = ddlZonaLaboral.SelectedValue;
                //--ddlPaisLaboral
                pEntidad.ciudad_empresa = Convert.ToInt64(ddlCiudadLaboral.SelectedValue);
                pEntidad.departamento_empresa = ddlDepartamentoLaboral.SelectedIndex > 0 ? Convert.ToInt64(ddlDepartamentoLaboral.SelectedValue) : 0;
                if (!string.IsNullOrWhiteSpace(txtDiaInicio.Text)) pEntidad.fecha_inicio = DateTime.ParseExact(txtDiaInicio.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                if (ddlCargo.SelectedIndex > 0) pEntidad.codcargo = Convert.ToInt32(ddlCargo.SelectedValue);
                pEntidad.estado_empresa = "0";
                pEntidad.codtipocontrato = Convert.ToInt64(ddlTipoContrato.SelectedValue);
                pEntidad.salario = Convert.ToInt64(txtIngsalariomensual.Text.Replace(".", "").Replace(",", ""));
                pEntidad.cod_nomina = !string.IsNullOrEmpty(txtCodNomina.Text.Trim()) ? txtCodNomina.Text.Trim() : "";
                pEntidad.email_contacto = txtCorreoCorporativo.Text.Trim() != "" ? txtCorreoCorporativo.Text.Trim() : "";
                pEntidad.telefono_empresa = txtTelefonolaboral.Text.Trim() != "" ? txtTelefonolaboral.Text.Trim() : null;
                pEntidad.profesion = txtProfesion.Text.Trim() != "" ? txtProfesion.Text.Trim().ToUpper() : null;
                pEntidad.codescolaridad = Convert.ToInt32(cbNivelAcademico.SelectedValue);
                pEntidad.actividad_economica = !string.IsNullOrEmpty(TxtDescripcionEconomica.Text) ? TxtDescripcionEconomica.Text : null;
                pEntidad.ciiu = TxtCiiu.Text;
                pEntidad.numero_empleados = !string.IsNullOrEmpty(txtNumEmpleados.Text.Trim()) ? Convert.ToInt32(txtNumEmpleados.Text.Trim()) : 0;
            }
            else
            {
                pEntidad.direccion_empresa = ddlZonaLaboral.SelectedValue;
                pEntidad.cod_nomina = chkOcupacion.SelectedValue;
            }
            //ALMACENAR INFORMACION
            pEntidad = EstadoServicio.CrearSolicitudPersonaAfi(pEntidad, 4, Session["sec"].ToString());
            //ALMACENAR INFORMACION
            Session["afiliacion"] = pEntidad;

            Response.Redirect("R05_Pep.aspx");
        }
        catch (Exception ex)
        {

        }
    }

    protected Boolean validarDatos()
    {
        if (string.IsNullOrWhiteSpace(txtIngsalariomensual.Text))
            return false;
        return true;
    }

    protected void ddlDepartamentoLaboral_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlDepartamentoLaboral.SelectedValue))
        {
            string filtro = "";
            filtro = " depende_de = " + ddlDepartamentoLaboral.SelectedValue + " ";
            List<xpinnWSEstadoCuenta.ListaDesplegable> lstCiudades = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
            lstCiudades = EstadoServicio.PoblarListaDesplegable("CIUDADES", "", filtro, "2", Session["sec"].ToString());
            xpinnWSEstadoCuenta.ListaDesplegable pEntidad = new xpinnWSEstadoCuenta.ListaDesplegable();
            //Llenando ciudades
            if (lstCiudades.Count > 0)
            {
                ddlCiudadLaboral.Items.Clear();
                LlenarDrop(ddlCiudadLaboral, lstCiudades);
            }
        }
    }

    protected void chkOcupacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(chkOcupacion.SelectedValue == "1")
        {
            pnlEmpleado.Visible = true;
            PanelIndependiente.Visible = false;
            txtCodNomina.Text = "";
        }
        else
        {
            pnlEmpleado.Visible = false;
            PanelIndependiente.Visible = true;
            string script = @"  function Alertando(valor) {                                            
                                            var elementsReq = document.getElementsByClassName('required');
                                            for (var i = 0; i < elementsReq.length; i++) {
                                                elementsReq[i].removeAttribute('required');
                                            }                                            
                                    }";

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Ocupacion", script, true);
        }
    }

}