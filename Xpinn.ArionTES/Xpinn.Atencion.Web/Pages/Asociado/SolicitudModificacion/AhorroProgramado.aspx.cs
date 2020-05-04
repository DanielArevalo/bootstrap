using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AhorroProgramado : GlobalWeb
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
            VisualizarTitulo(OptionsUrl.SolicitarModificacionProgra, "Mod");
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

            //LISTANDO AHORRO PROGRAMADO
            List<xpinnWSDeposito.CuentasProgramado> lstProgramado = new List<xpinnWSDeposito.CuentasProgramado>();
            string pFiltroProg = " WHERE A.ESTADO NOT IN (2) AND A.COD_PERSONA = " + Datospersona.cod_persona;
            lstProgramado = BODeposito.ListarAhorrosProgramado(pFiltroProg, DateTime.MinValue, Datospersona.identificacion, Datospersona.clavesinecriptar, Session["sec"].ToString());
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
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void gvAhoProgra_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        btnGuardarCambioCuota.Visible = true;
        lblError.Visible = false;

        txtNumeroProducto.Text = gvAhoProgra.Rows[e.NewEditIndex].Cells[1].Text;
        txtDescripcion.Text = gvAhoProgra.Rows[e.NewEditIndex].Cells[2].Text;        
        txtCuota.Text = gvAhoProgra.Rows[e.NewEditIndex].Cells[10].Text;
        txtNuevoValorCuota.Text = string.Empty;
        txtObservaciones.Text = string.Empty;
        txtFechaEmpiezaCambio.Text = string.Empty;
        txtCambio.Text = "ahoprogra";

        e.NewEditIndex = -1;

        panelAhorros.Visible = false;
        pnlCambioCuota.Visible = true;
    }


    protected void btnCerrarCambioCuota_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }

    
        protected void btnGuardarCambioCuota_Click(object sender, EventArgs e)
        {
        if (validar())
        {
            xpinnWSEstadoCuenta.CuentasProgramado ahoProgra = new xpinnWSEstadoCuenta.CuentasProgramado();
            InsertarCambioAhoProgra(ahoProgra);
            panelAhorros.Visible = true;
            pnlCambioCuota.Visible = false;
        }
        else
        {
            lblError.Text = "Diligencie todos los campos";
            lblError.Visible = true;
        }
        }

    private bool validar()
    {
        if (string.IsNullOrEmpty(txtFechaEmpiezaCambio.Text))
            return false;
        if (string.IsNullOrEmpty(txtNuevoValorCuota.Text))
            return false;

        return true;
    }

    void InsertarCambioAhoProgra(xpinnWSEstadoCuenta.CuentasProgramado ahoProgra)
    {
        ahoProgra.fecha_empieza_cambio = txtFechaEmpiezaCambio.ToDateTime;
        ahoProgra.cod_empresa = Convert.ToInt32(((xpinnWSLogin.Persona1)Session["persona"]).idEmpresa);
        ahoProgra.cod_persona = ((xpinnWSLogin.Persona1)Session["persona"]).cod_persona;
        ahoProgra.numero_programado = txtNumeroProducto.Text;
        ahoProgra.valor_cuota = Convert.ToInt32(txtCuota.Text.Replace(".", "").Replace(",", "").Replace("$", "").Replace("$", ""));
        ahoProgra.nuevo_valor_cuota = Convert.ToDecimal(txtNuevoValorCuota.Text.Replace(".", "").Replace(",", "").Replace("$", ""));
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