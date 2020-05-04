using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AhorroVista : GlobalWeb
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
            VisualizarTitulo(OptionsUrl.SolicitarModificacionAhorros, "Mod");
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


            //Configurando Tab Ahorro Vista
            if (parametroahorros.descripcion.Contains("|"))
            {
                string[] pData = parametroahorros.descripcion.Split('|');
                ClientScript.RegisterStartupScript(GetType(), "jsTabs", "javascript:ConfigureTab('tabAhoVista','" + pData[1] + "');", true);                
            }
           
            //LISTANDO AHORROS A LA VISTA
            if (parametroahorros != null && !string.IsNullOrEmpty(parametroahorros.valor) && parametroahorros.valor == "3")
            {
                List<xpinnWSDeposito.AhorroVista> lstAhorros = new List<xpinnWSDeposito.AhorroVista>();
                string pFiltroAho = " AND A.ESTADO NOT IN (4) AND A.COD_PERSONA = " + Datospersona.cod_persona;
                lstAhorros = BODeposito.ListarAhorroVistaClubAhorrador(Datospersona.cod_persona, pFiltroAho, ManejaClubAhorrador, Datospersona.identificacion, Datospersona.clavesinecriptar, Session["sec"].ToString());
                gvAhorros.DataSource = lstAhorros;
                if (lstAhorros.Count > 0)
                {
                    ViewState["DTAHORRO"] = lstAhorros;
                    panelAhorros.Visible = true;
                }
                else
                {
                    ViewState["DTAHORRO"] = null;
                    panelAhorros.Visible = false;
                }
                gvAhorros.DataBind();
            }            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    

    protected void gvAhorros_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        btnGuardarCambioCuota.Visible = true;
        lblError.Visible = false;

        txtNumeroProducto.Text = gvAhorros.Rows[e.NewEditIndex].Cells[1].Text;
        txtDescripcion.Text = gvAhorros.Rows[e.NewEditIndex].Cells[2].Text;
        txtCuota.Text = gvAhorros.Rows[e.NewEditIndex].Cells[7].Text;
        txtNuevoValorCuota.Text = string.Empty;
        txtObservaciones.Text = string.Empty;
        txtFechaEmpiezaCambio.Text = string.Empty;
        txtCambio.Text = "ahorros";

        e.NewEditIndex = -1;

        panelAhorro.Visible = false;
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
                    
                xpinnWSEstadoCuenta.AhorroVista ahorroVista = new xpinnWSEstadoCuenta.AhorroVista();
                ahorroVista.fecha_empieza_cambio = txtFechaEmpiezaCambio.ToDateTime;
                ahorroVista.cod_empresa_reca = Convert.ToInt32(((xpinnWSLogin.Persona1)Session["persona"]).idEmpresa);
                InsertarCambioAhorros(ahorroVista);

                if (valido == true)
                {
                    ObtenerDatos();
                    btnGuardarCambioCuota.Visible = false;
                    panelAhorro.Visible = false;
                    pnlCambioCuota.Visible = true;
                }
            }
        }
    }


    void InsertarCambio(xpinnWSEstadoCuenta.Aporte aporte)
    {
        aporte.cod_persona = ((xpinnWSLogin.Persona1)Session["persona"]).cod_persona;
        aporte.numero_aporte = Convert.ToInt64(txtNumeroProducto.Text);
        aporte.cuota = Convert.ToDecimal(txtCuota.Text.Replace(",00", "").Replace(",", "").Replace(".", "").Replace("$", ""));
        aporte.nuevo_valor_cuota = Convert.ToDecimal(txtNuevoValorCuota.Text.Replace(",", "").Replace(".", "").Replace("$", ""));
        aporte.observaciones = txtObservaciones.Text;
        
        aporte = _wsEstadoCuenta.InsertarSolicitudCambioCuotaAportes(aporte, Session["sec"].ToString());

        if (aporte.id_novedad_cambio != 0)
        {
            lblError.Text = "Se ha registrado la solicitud correctamente!.";
        }
        else
        {
            lblError.Text = "No se pudo realizar la solicitud!.";
        }

        lblError.Visible = true;
    }

    void InsertarCambioAhorros(xpinnWSEstadoCuenta.AhorroVista ahorroVista)
    {
        ahorroVista.cod_persona = ((xpinnWSLogin.Persona1)Session["persona"]).cod_persona;
        ahorroVista.numero_cuenta = txtNumeroProducto.Text;
        ahorroVista.cuota = Convert.ToDecimal(txtCuota.Text.Replace(",00", "").Replace(",", "").Replace(".", "").Replace("$", ""));
        ahorroVista.nuevo_valor_cuota = Convert.ToDecimal(txtNuevoValorCuota.Text.Replace(",", "").Replace(".", "").Replace("$", ""));
        ahorroVista.observaciones = txtObservaciones.Text;
        ahorroVista = _wsEstadoCuenta.InsertarCambioCuotaAhorro(ahorroVista, Session["sec"].ToString());

        if (ahorroVista.id_novedad_cambio != 0)
        {
            panelGeneral.Visible = false;
            panelFinal.Visible = true;
            lblCodigoGenerado.Text = ahorroVista.id_novedad_cambio.ToString();
        }
        else
        {
            lblError.Text = "No se pudo realizar la solicitud!.";
        }

        lblError.Visible = true;
    }

    void InsertarCambioAhoProgra(xpinnWSEstadoCuenta.CuentasProgramado ahoProgra)
    {
        ahoProgra.cod_persona = ((xpinnWSLogin.Persona1)Session["persona"]).cod_persona;
        ahoProgra.numero_cuenta = txtNumeroProducto.Text;
        ahoProgra.cuota = Convert.ToInt32(txtCuota.Text.Replace(",00", "").Replace(",", "").Replace(".", "").Replace("$", ""));
        ahoProgra.nuevo_valor_cuota = Convert.ToDecimal(txtNuevoValorCuota.Text.Replace(",", "").Replace(".", "").Replace("$", ""));
        ahoProgra.observaciones = txtObservaciones.Text;
        ahoProgra = _wsEstadoCuenta.InsertarCambioCuotaProgra(ahoProgra, Session["sec"].ToString());

        if (ahoProgra.id_novedad_cambio != 0)
        {
            panelGeneral.Visible = false;
            panelFinal.Visible = true;
            lblCodigoGenerado.Text = ahoProgra.id_novedad_cambio.ToString();
        }
        else
        {
            lblError.Text = "No se pudo realizar la solicitud!.";
        }

        lblError.Visible = true;
    }
}