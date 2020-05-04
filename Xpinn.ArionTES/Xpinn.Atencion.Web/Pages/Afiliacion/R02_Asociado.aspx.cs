using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using xpinnWSEstadoCuenta;

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
            cargarCombos();
            lblError.Text = "";
            //Asignando estilo radioButonList al CheckboxList
            cblSexoPer.Attributes.Add("onclick", "radioSex(event);");
            cblCabezaFamilia.Attributes.Add("onclick", "radioMeCbz(event);");
            //cblDocumento.Attributes.Add("onclick", "radioMeDocu(event);");
            cblEstadoCivil.Attributes.Add("onclick", "radioMeEstadoCi(event);");

            DateTime pFechaActual = DateTime.Now;
            cargarDatos();
        }
    }

    protected void cargarCombos()
    {
        //LLenando CheckBoxList Tipo Identificacion
        List<xpinnWSEstadoCuenta.ListaDesplegable> lstTipoIdenti;
        lstTipoIdenti = EstadoServicio.PoblarListaDesplegable("TIPOIDENTIFICACION", "CODTIPOIDENTIFICACION,DESCRIPCION", " CODTIPOIDENTIFICACION IN (1,4,5,7,8)", "2", Session["sec"].ToString());
        if (lstTipoIdenti.Count > 0)
        {
            for (int i = 0; i < lstTipoIdenti.Count(); i++)
            {
                ddlDocumento.Items.Add(new ListItem(lstTipoIdenti[i].descripcion.ToString().Trim(), lstTipoIdenti[i].idconsecutivo.ToString()));
            }
        }


        List<xpinnWSEstadoCuenta.ListaDesplegable> lstCiudades = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstCiudades = EstadoServicio.PoblarListaDesplegable("CIUDADES", "", " tipo = 3 ", "2", Session["sec"].ToString());
        xpinnWSEstadoCuenta.ListaDesplegable pEntidad = new xpinnWSEstadoCuenta.ListaDesplegable();

        //Llenando ciudades
        if (lstCiudades.Count > 0)
        {

            ddlCiudadExpedicion.DataSource = lstCiudades;
            ddlCiudadExpedicion.DataTextField = "descripcion";
            ddlCiudadExpedicion.DataValueField = "idconsecutivo";
            ddlCiudadExpedicion.AppendDataBoundItems = true;
            ddlCiudadExpedicion.Items.Insert(0, new ListItem("Pendiente", "0"));
            ddlCiudadExpedicion.DataBind();

            ddlCiudadNacimiento.DataSource = lstCiudades;
            ddlCiudadNacimiento.DataTextField = "descripcion";
            ddlCiudadNacimiento.DataValueField = "idconsecutivo";
            ddlCiudadNacimiento.AppendDataBoundItems = true;
            ddlCiudadNacimiento.Items.Insert(0, new ListItem("Pendiente", "0"));
            ddlCiudadNacimiento.DataBind();
        }


        //paises nacionalidad
        List<xpinnWSEstadoCuenta.ListaDesplegable> lstPaises = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstPaises = EstadoServicio.PoblarListaDesplegable("CIUDADES", "", " tipo = 1 ", "2", Session["sec"].ToString());
        if (lstPaises.Count > 0)
        {
            ddlNacionalidad.DataSource = lstPaises;
            ddlNacionalidad.DataTextField = "descripcion";
            ddlNacionalidad.DataValueField = "idconsecutivo";
            ddlNacionalidad.AppendDataBoundItems = true;
            ddlNacionalidad.Items.Insert(0, new ListItem("Pendiente", "0"));
            ddlNacionalidad.DataBind();
        }


        List<xpinnWSEstadoCuenta.ListaDesplegable> lstEstadoCi = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstEstadoCi = EstadoServicio.PoblarListaDesplegable("ESTADOCIVIL", "", "codestadocivil not in(6,7)", "1", Session["sec"].ToString());
        if (lstEstadoCi.Count > 0)
        {
            for (int i = 0; i < lstEstadoCi.Count(); i++)
            {
                cblEstadoCivil.Items.Add(new ListItem(" " + lstEstadoCi[i].descripcion.ToString().Trim() + " ", lstEstadoCi[i].idconsecutivo.ToString()));
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


    protected void cblCabezaFamilia_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cblCabezaFamilia.SelectedValue == "1")
            dependientes.Visible = true;
        else
            dependientes.Visible = false;
    }


    protected void txtDianacimiento_TextChanged(object sender, EventArgs e)
    {
        DateTime fecha = Convert.ToDateTime(txtDianacimiento.Text);
        int edad = DateTime.Today.AddTicks(-fecha.Ticks).Year - 1;
        if (edad < 18)
        {
            lblError.Text = "El usuario que se esta registrando es menor de edad.";
            Response.AddHeader("REFRESH", "2;R01_Identificacion.aspx");
            //Response.Redirect("R01_Identificacion.aspx");
        }
        return;      
    }

    protected void cargarDatos()
    {
        if (Session["afiliacion"] != null)
        {
            pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

            //DATOS ASOCIADO
            if (pEntidad.primer_nombre != null) txtNombre1.Text = pEntidad.primer_nombre;
            if (pEntidad.segundo_nombre != null) txtNombre2.Text = pEntidad.segundo_nombre;
            if (pEntidad.primer_apellido != null) txtApellido1.Text = pEntidad.primer_apellido;
            if (pEntidad.segundo_apellido != null) txtApellido2.Text = pEntidad.segundo_apellido;
            if (pEntidad.identificacion != null) txtNumeroDocumento.Text = pEntidad.identificacion;
            if (pEntidad.tipo_identificacion != 0) ddlDocumento.SelectedValue = pEntidad.tipo_identificacion.ToString();
            if (pEntidad.ciudad_expedicion > 0)
            {
                ddlCiudadExpedicion.SelectedValue = pEntidad.ciudad_expedicion.ToString();
                if (pEntidad.sexo != null)
                    cblSexoPer.SelectedValue = pEntidad.sexo;
                if (pEntidad.codestadocivil != null)
                    cblEstadoCivil.SelectedValue = pEntidad.codestadocivil.ToString();
                if (pEntidad.cabeza_familia != null)
                    cblCabezaFamilia.SelectedValue = pEntidad.cabeza_familia.ToString();
            }

            if (!string.IsNullOrWhiteSpace(pEntidad.fecha_expedicion.ToString()))
                txtDia.Text = pEntidad.fecha_expedicion.ToString("dd/MM/yyyy");

            //--ddlDepartamentoNacimiento //No se almacena, se encuentra en base a la ciudad

            if (pEntidad.ciudad_nacimiento != null)
                ddlCiudadNacimiento.SelectedValue = pEntidad.ciudad_nacimiento.ToString();

            if (pEntidad.fecha_nacimiento != null)
                txtDianacimiento.Text = ((DateTime)pEntidad.fecha_nacimiento).ToString("dd/MM/yyyy");


            if (!string.IsNullOrEmpty(pEntidad.pais))
                ddlNacionalidad.SelectedValue = pEntidad.pais;
            if (pEntidad.personas_cargo != null)
                txtPersonaCargo.Text = pEntidad.personas_cargo.ToString();

            if (pEntidad.codestadocivil != null)
                cblEstadoCivil.SelectedValue = pEntidad.codestadocivil.ToString();

            if (pEntidad.cabeza_familia == 1)
            {
                cblCabezaFamilia.SelectedValue = "1";
                dependientes.Visible = true;
                if (pEntidad.lstBeneficiarios != null)
                {
                    gvBeneficiarios.DataSource = pEntidad.lstBeneficiarios;
                    gvBeneficiarios.DataBind();
                }
            }

        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["afiliacion"] != null)
                pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

            //DATOS ASOCIADO
            pEntidad.ciudad_expedicion = ddlCiudadExpedicion.SelectedValue != "" ? Convert.ToInt64(ddlCiudadExpedicion.SelectedValue) : 0;
            pEntidad.nom_ciudad = ddlCiudadExpedicion.SelectedItem.Text;
            if (!string.IsNullOrWhiteSpace(txtDia.Text)) pEntidad.fecha_expedicion = DateTime.ParseExact(txtDia.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //--ddlDepartamentoNacimiento //No se almacena, se encuentra en base a la ciudad
            pEntidad.ciudad_nacimiento = ddlCiudadNacimiento.SelectedValue != "" ? Convert.ToInt64(ddlCiudadNacimiento.SelectedValue) : 0;
            if (!string.IsNullOrWhiteSpace(txtDianacimiento.Text)) pEntidad.fecha_nacimiento = DateTime.ParseExact(txtDianacimiento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            pEntidad.pais = ddlNacionalidad.SelectedValue != "" ? ddlNacionalidad.SelectedValue : "0";
            pEntidad.sexo = cblSexoPer.SelectedValue != "" ? cblSexoPer.SelectedValue : "";
            pEntidad.codestadocivil = cblEstadoCivil.SelectedValue != "" ? Convert.ToInt64(cblEstadoCivil.SelectedValue) : 0;
            pEntidad.cabeza_familia = cblCabezaFamilia.SelectedValue == null ? 0 : Convert.ToInt32(cblCabezaFamilia.SelectedValue);

            ObtenerListaBeneficiarios();
            List<BeneficiarioPersonaAfi> LstBene = null;
            LstBene = (List<BeneficiarioPersonaAfi>)ViewState[pEntidad.id_persona + "DatosBene"];
            if (LstBene != null)
                pEntidad.personas_cargo = LstBene.Count();
            else
                pEntidad.personas_cargo = 0;
            pEntidad.lstBeneficiarios = LstBene;

            //ALMACENAR INFORMACION
            pEntidad = EstadoServicio.CrearSolicitudPersonaAfi(pEntidad, 2, Session["sec"].ToString());
            //ALMACENAR INFORMACION
            Session["afiliacion"] = pEntidad;
            Response.Redirect("R03_Contacto.aspx");
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
    }


    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        if (Session["afiliacion"] != null)
            pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

        ObtenerListaBeneficiarios();

        List<xpinnWSEstadoCuenta.BeneficiarioPersonaAfi> lstBene = new List<xpinnWSEstadoCuenta.BeneficiarioPersonaAfi>();

        if (ViewState[pEntidad.id_persona + "DatosBene"] != null)
        {
            lstBene = (List<xpinnWSEstadoCuenta.BeneficiarioPersonaAfi>)ViewState[pEntidad.id_persona + "DatosBene"];

            for (int i = 1; i <= 1; i++)
            {
                xpinnWSEstadoCuenta.BeneficiarioPersonaAfi eBenef = new xpinnWSEstadoCuenta.BeneficiarioPersonaAfi();
                eBenef.cod_beneficiario = 0;
                eBenef.nombres = "";
                eBenef.apellidos = "";
                eBenef.tipo_id = 0;
                //eBenef.sexo = 3;
                eBenef.fecha_nac = DateTime.Now;
                eBenef.ocupacion = null;
                eBenef.nivel_educativo = 0;
                eBenef.ocupacion = null;
                lstBene.Add(eBenef);
            }
            gvBeneficiarios.DataSource = lstBene;
            gvBeneficiarios.DataBind();

            ViewState[pEntidad.id_persona + "DatosBene"] = lstBene;
        }
        else
        {
            for (int i = 1; i <= 1; i++)
            {
                xpinnWSEstadoCuenta.BeneficiarioPersonaAfi eBenef = new xpinnWSEstadoCuenta.BeneficiarioPersonaAfi();
                eBenef.cod_beneficiario = 0;
                eBenef.nombres = "";
                eBenef.apellidos = "";
                eBenef.tipo_id = 0;
                //eBenef.sexo = 3;
                eBenef.fecha_nac = DateTime.Now;
                eBenef.ocupacion = null;
                eBenef.nivel_educativo = 0;
                eBenef.ocupacion = null;
                lstBene.Add(eBenef);
            }
            gvBeneficiarios.DataSource = lstBene;
            gvBeneficiarios.DataBind();
            ViewState[pEntidad.id_persona + "DatosBene"] = lstBene;
        }
    }

    protected List<BeneficiarioPersonaAfi> ObtenerListaBeneficiarios()
    {
        if (Session["afiliacion"] != null)
            pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

        List<BeneficiarioPersonaAfi> lstBeneficiarios = new List<BeneficiarioPersonaAfi>();
        List<BeneficiarioPersonaAfi> lista = new List<BeneficiarioPersonaAfi>();

        foreach (GridViewRow rfila in gvBeneficiarios.Rows)
        {
            BeneficiarioPersonaAfi eBenef = new BeneficiarioPersonaAfi();
            Label lblidbeneficiario = (Label)rfila.FindControl("lblidbeneficiario");
            if (lblidbeneficiario != null)
                eBenef.cod_beneficiario = Convert.ToInt32(lblidbeneficiario.Text);

            //Nombres
            TextBox txtNombres = (TextBox)rfila.FindControl("txtNombres");
            if (txtNombres != null)
                eBenef.nombres = Convert.ToString(txtNombres.Text);

            //Apellidos
            TextBox txtApellidos = (TextBox)rfila.FindControl("txtApellidos");
            if (txtApellidos != null)
                eBenef.apellidos = Convert.ToString(txtApellidos.Text);

            //Tipo id
            DropDownList ddlTipoId = (DropDownList)rfila.FindControl("ddlTipoId");
            if (!string.IsNullOrEmpty(ddlTipoId.SelectedValue) && ddlTipoId.SelectedIndex != 0)
                eBenef.tipo_id = Convert.ToInt32(ddlTipoId.SelectedValue);

            //Identificacion
            TextBox txtIdentificacion = (TextBox)rfila.FindControl("txtIdentificacion");
            if (txtIdentificacion != null)
                eBenef.identificacion = Convert.ToString(txtIdentificacion.Text);

            //Sexo
            DropDownList ddlSexo = (DropDownList)rfila.FindControl("ddlSexo");
            if (!string.IsNullOrEmpty(ddlSexo.SelectedValue))
                eBenef.sexo = Convert.ToInt32(ddlSexo.SelectedValue);

            //Fecha Nac
            TextBox txtFechaNac = (TextBox)rfila.FindControl("txtFechaNac");
            if (txtFechaNac != null)
                if (txtFechaNac.Text != "")
                    eBenef.fecha_nac = DateTime.ParseExact(txtFechaNac.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //Ocupación
            TextBox txtOcupacion = (TextBox)rfila.FindControl("txtOcupacion");
            if (txtOcupacion != null)
                eBenef.ocupacion = Convert.ToString(txtOcupacion.Text);

            //Nivel Educativo
            DropDownList ddlNivel = (DropDownList)rfila.FindControl("ddlNivel");
            if (!string.IsNullOrEmpty(ddlNivel.SelectedValue) && ddlNivel.SelectedIndex != 0)
                eBenef.nivel_educativo = Convert.ToInt32(ddlNivel.SelectedValue);

            //Parentezco
            DropDownList ddlParentesco = (DropDownList)rfila.FindControl("ddlParentesco");
            if (!string.IsNullOrEmpty(ddlParentesco.SelectedValue) && ddlParentesco.SelectedIndex != 0)
                eBenef.codparentesco = Convert.ToInt32(ddlParentesco.SelectedValue);


            eBenef.cod_solicitud = pEntidad.id_persona;
            lista.Add(eBenef);
            txtPersonaCargo.Text = lista.Count.ToString();
            ViewState[pEntidad.id_persona + "DatosBene"] = lista;

            if (eBenef.identificacion.Trim() != "" && eBenef.nombres.Trim() != null)
            {
                lstBeneficiarios.Add(eBenef);
            }
        }
        return lstBeneficiarios;
    }

    protected void gvBeneficiarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //Llenar parentesco
            DropDownList ddlParentesco = (DropDownList)e.Row.FindControl("ddlParentesco");
            if (ddlParentesco != null)
            {
                List<xpinnWSEstadoCuenta.ListaDesplegable> lstParentescos;
                lstParentescos = EstadoServicio.PoblarListaDesplegable("PARENTESCOS", "", "", "2", Session["sec"].ToString());
                LlenarDrop(ddlParentesco, lstParentescos);

                Label lblParentesco = (Label)e.Row.FindControl("lblParentesco");
                if (lblParentesco.Text != null)
                    ddlParentesco.SelectedValue = lblParentesco.Text;
            }


            //llenar tipo id
            DropDownList ddlTipoId = (DropDownList)e.Row.FindControl("ddlTipoId");
            if (ddlTipoId != null)
            {
                List<xpinnWSEstadoCuenta.ListaDesplegable> lstTipoIdenti;
                lstTipoIdenti = EstadoServicio.PoblarListaDesplegable("TIPOIDENTIFICACION", "CODTIPOIDENTIFICACION,DESCRIPCION", " CODTIPOIDENTIFICACION IN (1,4,5,7,8)", "2", Session["sec"].ToString());
                LlenarDrop(ddlTipoId, lstTipoIdenti);

                Label lbltipoid = (Label)e.Row.FindControl("lbltipoid");
                if (lbltipoid.Text != null)
                    ddlTipoId.SelectedValue = lbltipoid.Text;
            }



            //llenar nivel escolaridad
            DropDownList ddlNivel = (DropDownList)e.Row.FindControl("ddlNivel");
            if (ddlNivel != null)
            {
                List<xpinnWSEstadoCuenta.ListaDesplegable> lstNivelEsc = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
                lstNivelEsc = EstadoServicio.PoblarListaDesplegable("NIVELESCOLARIDAD", "", "", "1", Session["sec"].ToString());
                LlenarDrop(ddlNivel, lstNivelEsc);

                Label lblNivel = (Label)e.Row.FindControl("lblNivel");
                if (lblNivel.Text != null)
                    ddlNivel.SelectedValue = lblNivel.Text;
            }

            //llenar sexo
            DropDownList ddlSexo = (DropDownList)e.Row.FindControl("ddlSexo");
            if (ddlSexo != null)
            {
                ddlSexo.Items.Add(new ListItem("Seleccione", ""));
                ddlSexo.Items.Add(new ListItem("Mujer", "0"));
                ddlSexo.Items.Add(new ListItem("Hombre", "1"));
                ddlSexo.DataBind();

                Label lblSexo = (Label)e.Row.FindControl("lblSexo");
                if (lblSexo.Text != null)
                    ddlNivel.SelectedValue = lblSexo.Text;
            }
        }
    }

    protected void gvBeneficiarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["afiliacion"] != null)
            pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

        //obtiene indice de fila a eliminar
        int conseID = e.RowIndex;
        ObtenerListaBeneficiarios();
        List<BeneficiarioPersonaAfi> LstBene;
        LstBene = (List<BeneficiarioPersonaAfi>)ViewState[pEntidad.id_persona + "DatosBene"];
        LstBene.RemoveAt((gvBeneficiarios.PageIndex * gvBeneficiarios.PageSize) + e.RowIndex);
        //LstBene.RemoveAt(conseID);

        gvBeneficiarios.DataSourceID = null;
        gvBeneficiarios.DataBind();

        gvBeneficiarios.DataSource = LstBene;
        gvBeneficiarios.DataBind();

        ViewState[pEntidad.id_persona + "DatosBene"] = LstBene;
    }

}