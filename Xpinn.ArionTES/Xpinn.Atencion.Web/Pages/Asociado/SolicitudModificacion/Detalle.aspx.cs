using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Detalle : GlobalWeb
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

            parametroahorros = _service.ConsultarGeneral(3, Datospersona.identificacion, Datospersona.clavesinecriptar, "");

            xpinnWSCredito.Persona Persona = BOCredito.ConsultarPersona(Datospersona.cod_persona, Datospersona.clavesinecriptar, "");
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

            //LISTANDO AHORROS A LA VISTA
            if (parametroahorros != null && !string.IsNullOrEmpty(parametroahorros.valor) && parametroahorros.valor == "3")
            {
                List<xpinnWSDeposito.AhorroVista> lstAhorros = new List<xpinnWSDeposito.AhorroVista>();
                string pFiltroAho = " AND A.ESTADO NOT IN (4) AND A.COD_PERSONA = " + Datospersona.cod_persona;
                lstAhorros = BODeposito.ListarAhorroVistaClubAhorrador(Datospersona.cod_persona, pFiltroAho, ManejaClubAhorrador, Datospersona.identificacion, Datospersona.clavesinecriptar, "");
                gvAhorros.DataSource = lstAhorros;
                if (lstAhorros.Count > 0)
                {
                    ViewState["DTAHORRO"] = lstAhorros;
                    panelAhorros.Visible = true;
                    lblTotRegAhorro.Visible = true;
                    lblTotRegAhorro.Text = "<br/> Registros encontrados " + lstAhorros.Count.ToString();
                    lblInfoAhorro.Visible = false;
                }
                else
                {
                    ViewState["DTAHORRO"] = null;
                    panelAhorros.Visible = false;
                    lblTotRegAhorro.Visible = false;
                    lblInfoAhorro.Visible = true;
                }
                gvAhorros.DataBind();
            }            

            //LISTANDO AHORRO PROGRAMADO
            List<xpinnWSDeposito.CuentasProgramado> lstProgramado = new List<xpinnWSDeposito.CuentasProgramado>();
            string pFiltroProg = " WHERE A.ESTADO NOT IN (2) AND A.COD_PERSONA = " + Datospersona.cod_persona;
            lstProgramado = BODeposito.ListarAhorrosProgramado(pFiltroProg, DateTime.MinValue, Datospersona.identificacion, Datospersona.clavesinecriptar, "");
            gvAhoProgra.DataSource = lstProgramado;
            if (lstProgramado.Count > 0)
            {
                ViewState["DTAHOPRO"] = lstProgramado;
                panelProgra.Visible = true;

            }
            else
            {
                ViewState["DTAHOPRO"] = null;
                panelProgra.Visible = false;
            }
            gvAhoProgra.DataBind();

            TotalizarGridView();
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
            lblMsj1.Text = "Registros encontrados " + lstConsulta.Count();
        }
        else
        {
            panelAporte.Visible = false;
            lblMsj1.Text = "No se encontraron Datos";
        }

        TotalizarGridView();
    }


    protected void TotalizarGridView()
    {
        //TOTALIZANDO APORTES
        decimal totalsaldo = 0;
        decimal totalcuotas = 0;
        decimal pendientepago = 0;

        foreach (GridViewRow rfila in gvAportes.Rows)
        {
            if (gvAportes.DataKeys[rfila.RowIndex].Values[0] != null)
                if (gvAportes.DataKeys[rfila.RowIndex].Values[0].ToString() != "" && gvAportes.DataKeys[rfila.RowIndex].Values[0].ToString() != "&nbsp;")
                    totalsaldo += Convert.ToDecimal(gvAportes.DataKeys[rfila.RowIndex].Values[0].ToString().Replace("$", "").Replace(".", "").Replace(",", ""));

            if (gvAportes.DataKeys[rfila.RowIndex].Values[1] != null)
                if (gvAportes.DataKeys[rfila.RowIndex].Values[1].ToString() != "" && gvAportes.DataKeys[rfila.RowIndex].Values[1].ToString() != "&nbsp;")
                    totalcuotas += Convert.ToDecimal(gvAportes.DataKeys[rfila.RowIndex].Values[1].ToString().Replace("$", "").Replace(".", "").Replace(",", ""));

            if (gvAportes.DataKeys[rfila.RowIndex].Values[2] != null)
                if (gvAportes.DataKeys[rfila.RowIndex].Values[2].ToString() != "" && gvAportes.DataKeys[rfila.RowIndex].Values[2].ToString() != "&nbsp;")
                    pendientepago += Convert.ToDecimal(gvAportes.DataKeys[rfila.RowIndex].Values[2].ToString().Replace("$", "").Replace(".", "").Replace(",", ""));
        }

        txtTotalAportes.Text = Convert.ToString(totalsaldo.ToString("###,### "));
        txtTotalCuotasAportes.Text = Convert.ToString(totalcuotas.ToString("###,###"));
        txtAPortesPendientesporPagar.Text = Convert.ToString(pendientepago.ToString("###,###"));

        //TOTALIZANDO LOS AHORROS 
        //   ahorros 
        decimal ahorros = 0;
        decimal totalcuotasahorros = 0;
        decimal totalcausacion_ahorros = 0;
        decimal totalcanje_ahorros = 0;
        foreach (GridViewRow rfila in gvAhorros.Rows)
        {

            if (gvAhorros.DataKeys[rfila.RowIndex].Values[0] != null)
                if (gvAhorros.DataKeys[rfila.RowIndex].Values[0].ToString() != "" && gvAhorros.DataKeys[rfila.RowIndex].Values[0].ToString() != "&nbsp;")
                    ahorros += Convert.ToDecimal(gvAhorros.DataKeys[rfila.RowIndex].Values[0].ToString());

            if (gvAhorros.DataKeys[rfila.RowIndex].Values[1] != null)
                if (gvAhorros.DataKeys[rfila.RowIndex].Values[1].ToString() != "" && gvAhorros.DataKeys[rfila.RowIndex].Values[1].ToString() != "&nbsp;")
                    totalcuotasahorros += Convert.ToDecimal(gvAhorros.DataKeys[rfila.RowIndex].Values[1].ToString());

            if (gvAhorros.DataKeys[rfila.RowIndex].Values[2] != null)
                if (gvAhorros.DataKeys[rfila.RowIndex].Values[2].ToString() != "" && gvAhorros.DataKeys[rfila.RowIndex].Values[2].ToString() != "&nbsp;")
                    totalcanje_ahorros += Convert.ToDecimal(gvAhorros.DataKeys[rfila.RowIndex].Values[2].ToString());

            if (gvAhorros.DataKeys[rfila.RowIndex].Values[3] != null)
                if (gvAhorros.DataKeys[rfila.RowIndex].Values[3].ToString() != "" && gvAhorros.DataKeys[rfila.RowIndex].Values[3].ToString() != "&nbsp;")
                    totalcausacion_ahorros += Convert.ToDecimal(gvAhorros.DataKeys[rfila.RowIndex].Values[3].ToString());

            txtTotalAhorros.Text = Convert.ToString(ahorros.ToString("n2"));
            txtTotalCuotasAhorros.Text = Convert.ToString(totalcuotasahorros.ToString("n2"));            
        }
        
    }

    protected void gvAportes_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        btnGuardarCambioCuota.Visible = true;
        lblError.Visible = false;

        txtNumeroProducto.Text = gvAportes.Rows[e.NewEditIndex].Cells[1].Text;
        txtDescripcion.Text = gvAportes.Rows[e.NewEditIndex].Cells[3].Text;
        txtUltimaMod.Text = HttpUtility.HtmlDecode(gvAportes.Rows[e.NewEditIndex].Cells[8].Text);
        txtCuota.Text = gvAportes.Rows[e.NewEditIndex].Cells[4].Text;
        txtNuevoValorCuota.Text = string.Empty;
        txtObservaciones.Text = string.Empty;
        txtFechaEmpiezaCambio.Text = string.Empty;
        txtCambio.Text = "aportes";

        e.NewEditIndex = -1;

        panelAporte.Visible = false;
        pnlCambioCuota.Visible = true;
    }
    protected void gvAhorros_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        btnGuardarCambioCuota.Visible = true;
        lblError.Visible = false;

        txtNumeroProducto.Text = gvAhorros.Rows[e.NewEditIndex].Cells[1].Text;
        txtDescripcion.Text = gvAhorros.Rows[e.NewEditIndex].Cells[2].Text;
        txtCuota.Text = gvAhorros.Rows[e.NewEditIndex].Cells[9].Text;
        txtNuevoValorCuota.Text = string.Empty;
        txtObservaciones.Text = string.Empty;
        txtFechaEmpiezaCambio.Text = string.Empty;
        txtCambio.Text = "ahorros";

        e.NewEditIndex = -1;

        panelAporte.Visible = false;
        pnlCambioCuota.Visible = true;
    }
    protected void gvAhoProgra_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        btnGuardarCambioCuota.Visible = true;
        lblError.Visible = false;

        txtNumeroProducto.Text = gvAhoProgra.Rows[e.NewEditIndex].Cells[1].Text;
        txtDescripcion.Text = gvAhoProgra.Rows[e.NewEditIndex].Cells[2].Text;        
        txtCuota.Text = gvAhoProgra.Rows[e.NewEditIndex].Cells[12].Text;
        txtNuevoValorCuota.Text = string.Empty;
        txtObservaciones.Text = string.Empty;
        txtFechaEmpiezaCambio.Text = string.Empty;
        txtCambio.Text = "ahoprogra";

        e.NewEditIndex = -1;

        panelAporte.Visible = false;
        pnlCambioCuota.Visible = true;
    }


    protected void btnCerrarCambioCuota_Click(object sender, EventArgs e)
    {
        panelAporte.Visible = true;
        pnlCambioCuota.Visible = false;
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

            bool? valido = _wsEstadoCuenta.ValidarFechaSolicitudCambio(aporte, "");

            if (valido != true)
            {
                lblError.Text = "Para la fecha solicitada del cambio ya se genero el corte de la pagaduria";
                lblError.Visible = true;
            }
            else
            {
                string cambio = txtCambio.Text;                
                switch (cambio)
                {
                    case "aportes":
                        //validar cuota mínima
                        aporte.nuevo_valor_cuota = Convert.ToDecimal(txtNuevoValorCuota.Text.Replace(".",""));
                        string aporteValido = _wsEstadoCuenta.ValidarAporte(aporte, "");
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
                        break;
                    case "ahorros":
                        xpinnWSEstadoCuenta.AhorroVista ahorroVista = new xpinnWSEstadoCuenta.AhorroVista();
                        ahorroVista.fecha_empieza_cambio = txtFechaEmpiezaCambio.ToDateTime;
                        ahorroVista.cod_empresa_reca = Convert.ToInt32(((xpinnWSLogin.Persona1)Session["persona"]).idEmpresa);
                        InsertarCambioAhorros(ahorroVista);
                        break;
                    case "ahoprogra":
                        xpinnWSEstadoCuenta.CuentasProgramado ahoProgra = new xpinnWSEstadoCuenta.CuentasProgramado();
                        ahoProgra.fecha_empieza_cambio = txtFechaEmpiezaCambio.ToDateTime;
                        ahoProgra.cod_empresa = Convert.ToInt32(((xpinnWSLogin.Persona1)Session["persona"]).idEmpresa);
                        InsertarCambioAhoProgra(ahoProgra);
                        break;
                    default:
                        break;
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
        aporte.cuota = Convert.ToDecimal(txtCuota.Text.Replace(",00", "").Replace(",", "").Replace(".", ""));
        aporte.nuevo_valor_cuota = Convert.ToDecimal(txtNuevoValorCuota.Text);
        aporte.observaciones = txtObservaciones.Text;
        
        aporte = _wsEstadoCuenta.InsertarSolicitudCambioCuotaAportes(aporte, "");

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
        ahorroVista.cuota = Convert.ToDecimal(txtCuota.Text.Replace(",00", "").Replace(",", "").Replace(".", ""));
        ahorroVista.nuevo_valor_cuota = Convert.ToDecimal(txtNuevoValorCuota.Text);
        ahorroVista.observaciones = txtObservaciones.Text;
        ahorroVista = _wsEstadoCuenta.InsertarCambioCuotaAhorro(ahorroVista, "");

        if (ahorroVista.id_novedad_cambio != 0)
        {
            lblError.Text = "Se ha registrado la solicitud correctamente!.";
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
        ahoProgra.cuota = Convert.ToInt32(txtCuota.Text.Replace(",00", "").Replace(",", "").Replace(".", ""));
        ahoProgra.nuevo_valor_cuota = Convert.ToDecimal(txtNuevoValorCuota.Text);
        ahoProgra.observaciones = txtObservaciones.Text;
        ahoProgra = _wsEstadoCuenta.InsertarCambioCuotaProgra(ahoProgra, "");

        if (ahoProgra.id_novedad_cambio != 0)
        {
            lblError.Text = "Se ha registrado la solicitud correctamente!.";
        }
        else
        {
            lblError.Text = "No se pudo realizar la solicitud!.";
        }

        lblError.Visible = true;
    }
}