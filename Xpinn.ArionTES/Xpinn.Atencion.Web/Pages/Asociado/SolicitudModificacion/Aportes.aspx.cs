using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Aportes : GlobalWeb
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient _wsEstadoCuenta = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient _service = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSCredito.WSCreditoSoapClient BOCredito = new xpinnWSCredito.WSCreditoSoapClient();
    xpinnWSDeposito.WSDepositoSoapClient BODeposito = new xpinnWSDeposito.WSDepositoSoapClient();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.ModificarProducto, "Mod");
            Site toolBar = (Site)Master;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("SolicitudModificacion", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ObtenerDatos();
        }
    }


    protected void frvData_DataBound(object sender, EventArgs e)
    {
        Label lblFechaAfiliacion = (Label)frvData.FindControl("lblFechaAfiliacion");
        if (lblFechaAfiliacion != null)
        {
            if (!string.IsNullOrWhiteSpace(lblFechaAfiliacion.Text))
            {
                if (Convert.ToDateTime(lblFechaAfiliacion.Text) == DateTime.MinValue)
                    lblFechaAfiliacion.Text = "";
            }
        }
    }


    private void ObtenerDatos()
    {
        try
        {
            //INICIALIZAR TABS
            xpinnWSEstadoCuenta.General parametroahorros;            

            xpinnWSLogin.Persona1 Datospersona = (xpinnWSLogin.Persona1)Session["persona"];

            parametroahorros = _service.ConsultarGeneral(3, Datospersona.identificacion, Datospersona.clavesinecriptar, Session["sec"].ToString());

            xpinnWSCredito.Persona Persona = BOCredito.ConsultarPersona(Datospersona.cod_persona, Datospersona.clavesinecriptar, Session["sec"].ToString());
            if (Persona.PrimerNombre != "" && Persona.PrimerApellido != "")
                Persona.PrimerNombre = Persona.PrimerNombre.Trim() + " " + Persona.PrimerApellido.Trim();
            Persona.SegundoNombre = Persona.Ciudad.nomciudad;
            List<xpinnWSCredito.Persona> lstData = new List<xpinnWSCredito.Persona>();
            lstData.Add(Persona);
            frvData.DataSource = lstData;
            frvData.DataBind();

            txtCodPersona.Text = Datospersona.cod_persona.ToString();
            bool ManejaClubAhorrador = ConfigurationManager.AppSettings["IncluirClubAhorradores"] == null ? true :
                (ConfigurationManager.AppSettings["IncluirClubAhorradores"].ToString() == "1") ? false : true;

            ConsultarEstadoCuenta();
            //Configurando Tab Ahorro Vista
            if (parametroahorros.descripcion.Contains("|"))
            {
                string[] pData = parametroahorros.descripcion.Split('|');
                ClientScript.RegisterStartupScript(GetType(), "jsTabs", "javascript:ConfigureTab('tabAhoVista','" + pData[1] + "');", true);                
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void ConsultarEstadoCuenta()
    {
        List<xpinnWSEstadoCuenta.Aporte> lstConsulta = new List<xpinnWSEstadoCuenta.Aporte>();
        string pFiltro = string.Empty;
        pFiltro = " And v_aportes.estado in (1)";
        lstConsulta = _service.ListarAportesEstadoCuenta(Convert.ToInt64(txtCodPersona.Text), true, pFiltro, DateTime.Now);

        if (lstConsulta.Count > 0)
        {
            panelAporte.Visible = true;
            gvAportes.DataSource = lstConsulta;
            gvAportes.DataBind();
        }
        else
        {
            panelAporte.Visible = false;
        }
    }

    protected void gvAportes_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        btnGuardarCambioCuota.Visible = true;
        lblError.Visible = false;

        txtNumeroProducto.Text = gvAportes.Rows[e.NewEditIndex].Cells[1].Text;
        txtDescripcion.Text = gvAportes.Rows[e.NewEditIndex].Cells[4].Text;
        txtCuota.Text = gvAportes.Rows[e.NewEditIndex].Cells[6].Text;
        txtNuevoValorCuota.Text = string.Empty;
        txtObservaciones.Text = string.Empty;
        txtFechaEmpiezaCambio.Text = string.Empty;
        txtCambio.Text = "aportes";

        e.NewEditIndex = -1;

        panelAporte.Visible = false;
        pnlCambioCuota.Visible = true;
    }

    protected void btnCerrarCambioCuota_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }

    protected void btnGuardarCambioCuota_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNuevoValorCuota.Text))
        {
            lblError.Text = "El valor de la nueva cuota no puede estar vacio!.";
            lblError.Visible = true;
        }
        else if (string.IsNullOrWhiteSpace(txtFechaEmpiezaCambio.Text))
        {
            lblError.Text = "La fecha del cambio no puede estar vacia!.";
            lblError.Visible = true;
        }
        else
        {
            xpinnWSEstadoCuenta.Aporte aporte = new xpinnWSEstadoCuenta.Aporte();
            aporte.fecha_empieza_cambio = txtFechaEmpiezaCambio.ToDateTime;
            aporte.cod_empresa = ((xpinnWSLogin.Persona1)Session["persona"]).idEmpresa;
            aporte.cod_persona = ((xpinnWSLogin.Persona1)Session["persona"]).cod_persona;

            bool? valido = _wsEstadoCuenta.ValidarFechaSolicitudCambio(aporte, Session["sec"].ToString());

            if (valido != true)
            {
                lblError.Text = "Para la fecha solicitada del cambio ya se genero el corte de la pagaduria";
                lblError.Visible = true;
            }
            else
            {
                string cambio = txtCambio.Text;                
                //validar cuota mínima
                aporte.nuevo_valor_cuota = Convert.ToDecimal(txtNuevoValorCuota.Text.Replace(".","").Replace(",","").Replace("$", ""));
                string aporteValido = _wsEstadoCuenta.ValidarAporte(aporte, Session["sec"].ToString());
                if (aporteValido == "OK")
                {
                    InsertarCambio(aporte);
                }
                else
                {
                    lblError.Text = aporteValido;
                    lblError.Visible = true;
                    valido = false;
                }                                        
                if (valido == true)
                {
                    ObtenerDatos();
                    btnGuardarCambioCuota.Visible = false;
                    ConsultarEstadoCuenta();
                    panelAporte.Visible = false;
                    pnlCambioCuota.Visible = true;
                }
            }
        }
    }


    void InsertarCambio(xpinnWSEstadoCuenta.Aporte aporte)
    {
        aporte.cod_persona = ((xpinnWSLogin.Persona1)Session["persona"]).cod_persona;
        aporte.numero_aporte = Convert.ToInt64(txtNumeroProducto.Text);
        aporte.cuota = Convert.ToDecimal(txtCuota.Text.Replace(",00", "").Replace(",", "").Replace(".", "").Replace("$",""));
        aporte.nuevo_valor_cuota = Convert.ToDecimal(txtNuevoValorCuota.Text.Replace(",", "").Replace(".", "").Replace("$", ""));
        aporte.observaciones = txtObservaciones.Text;
        
        aporte = _wsEstadoCuenta.InsertarSolicitudCambioCuotaAportes(aporte, Session["sec"].ToString());

        if (aporte.id_novedad_cambio != 0)
        {
            panelGeneral.Visible = false;
            panelFinal.Visible = true;
            lblCodigoGenerado.Text = aporte.id_novedad_cambio.ToString();
        }
        else
        {
            lblError.Text = "No se pudo realizar la solicitud!.";
        }

        lblError.Visible = true;
    }
        
}