using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    InversionesService inversionesService = new InversionesService();
    PoblarListas poblarLista = new PoblarListas();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[inversionesService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(inversionesService.CodigoPrograma, "E");
            else
                VisualizarOpciones(inversionesService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(inversionesService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarDropDown();
            txtFechaEmi.Text = DateTime.Now.ToString(gFormatoFecha);
            if (Session[inversionesService.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[inversionesService.CodigoPrograma + ".id"].ToString();
                Session.Remove(inversionesService.CodigoPrograma + ".id");
                ObtenerDatos(idObjeto);
            }
        }
    }

    private void CargarDropDown()
    {
        poblarLista.PoblarListaDesplegable("TIPO_INVERSION", ddlTipoInv, Usuario);

        try
        {
            ddlTipoCalendario.Items.Clear();
            ddlTipoCalendario.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un Item", "0"));
            ddlTipoCalendario.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Comercial", "1"));
            ddlTipoCalendario.Items.Insert(2, new System.Web.UI.WebControls.ListItem("Calendario", "2"));
            ddlTipoCalendario.SelectedIndex = 0;
            ddlTipoCalendario.DataBind();
        }
        catch { }
    }

    private void ObtenerDatos(string idObjeto)
    {
        try
        {
            Inversiones pEntidad = new Inversiones();
            string pFiltro = " WHERE I.COD_INVERSION = " + idObjeto;
            pEntidad = inversionesService.ConsultarInversiones(pFiltro, Usuario);
            if (pEntidad != null)
            {
                if (pEntidad.cod_inversion != 0)
                    lblCodigo.Text = pEntidad.cod_inversion.ToString();
                if (pEntidad.numero_titulo != null)
                    txtNroTitulo.Text = pEntidad.numero_titulo;
                if (pEntidad.valor_capital != null)
                    txtVrCapital.Text = pEntidad.valor_capital.ToString();
                if (pEntidad.valor_interes != null)
                    txtVrInteres.Text = pEntidad.valor_interes.ToString();
                if (pEntidad.plazo != null)
                    txtPlazo.Text = pEntidad.plazo.ToString();
                if (pEntidad.fecha_emision != null)
                    txtFechaEmi.Text = Convert.ToDateTime(pEntidad.fecha_emision).ToString(gFormatoFecha);
                if (pEntidad.fecha_vencimiento != null)
                    txtFechaVenc.Text = Convert.ToDateTime(pEntidad.fecha_vencimiento).ToString(gFormatoFecha);
                if (pEntidad.tasa_interes != null)
                    txtTasaInteres.Text = pEntidad.tasa_interes.ToString();
                if (pEntidad.cod_tipo != null)
                    ddlTipoInv.SelectedValue = pEntidad.cod_tipo.ToString();
                if (pEntidad.identificacion != null)
                {
                    txtIdentificacion.Text = pEntidad.identificacion;
                    txtIdentificacion_TextChanged(txtIdentificacion, null);
                }
                //if (pEntidad.tipo_calendario != 0) ddlTipoCalendario.SelectedValue = pEntidad.tipo_calendario.ToString();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(inversionesService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    private void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    private bool validarDatos()
    {
        if (txtNroTitulo.Text.Trim() == "")
        {
            VerError("Ingrese el número de título");
            return false;
        }
        if (txtVrCapital.Text.Trim() == "" || txtVrCapital.Text.Trim() == "0")
        {
            VerError("Ingrese el valor de capital");
            return false;
        }
        if (txtVrInteres.Text.Trim() == "")
        {
            VerError("Ingrese el valor de intereses");
            return false;
        }
        if (txtPlazo.Text.Trim() == "")
        {
            VerError("Ingrese el plazo");
            return false;
        }
        if (txtFechaEmi.Text.Trim() == "")
        {
            VerError("Ingrese la fecha de emisión");
            return false;
        }
        if (txtFechaEmi.Text.Trim() == "")
        {
            VerError("Ingrese la fecha de vencimiento");
            return false;
        }
        if (txtTasaInteres.Text.Trim() == "")
        {
            VerError("Ingrese la tasa de interes");
            return false;
        }
        if (ddlTipoInv.SelectedIndex == 0)
        {
            VerError("Seleccione el tipo de inversión");
        }
        if (txtCodPersona.Text.Trim() == "")
        {
            VerError("Ingrese una identificación válida de la entidad ");
            return false;
        }
        if (txtCodCuenta.Text.Trim() == "")
        {
            VerError("Ingrese el código contable a la que pertenece la inversión");
            return false;
        }
        return true;
    }

    private void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        if (Page.IsValid)
        {
            if (validarDatos())
                ctlMensaje.MostrarMensaje("Desea realizar la grabación de la Inversión?");
        }
    }

    private void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            Inversiones pEntidad = new Inversiones();
            pEntidad.cod_inversion = idObjeto == "" ? 0 : Convert.ToInt64(lblCodigo.Text);
            pEntidad.numero_titulo = txtNroTitulo.Text.Trim();
            pEntidad.valor_capital = Convert.ToDecimal(txtVrCapital.Text.Replace(".", ""));
            pEntidad.valor_interes = Convert.ToDecimal(txtVrInteres.Text);
            pEntidad.plazo = Convert.ToInt32(txtPlazo.Text);
            pEntidad.fecha_emision = Convert.ToDateTime(txtFechaEmi.Text);
            pEntidad.fecha_vencimiento = Convert.ToDateTime(txtFechaVenc.Text);
            pEntidad.tasa_interes = Convert.ToDecimal(txtTasaInteres.Text);
            pEntidad.cod_tipo = Convert.ToInt32(ddlTipoInv.SelectedValue);
            pEntidad.cod_persona = Convert.ToInt64(txtCodPersona.Text);
            pEntidad.cod_cuenta_niif = txtCodCuenta.Text;
            //pEntidad.tipo_calendario = ddlTipoCalendario.SelectedValue.Trim() != "" ? ConvertirStringToInt32(ddlTipoCalendario.SelectedValue.Trim()) : 1;

            string msj = string.Empty;
            if (idObjeto != "")
            {
                pEntidad = inversionesService.CrearInversiones(pEntidad, 2, Usuario);
                msj = "modificaron";
            }
            else
            {
                pEntidad = inversionesService.CrearInversiones(pEntidad, 1, Usuario);
                msj = "grabaron";
            }

            lblMsj.Text = msj;
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            mvPrincipal.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodPersona", "txtIdentificacion", "txtNombres");
    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        if (txtIdentificacion.Text.Trim() != "")
        {
            Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            Persona1.soloPersona = 1;
            Persona1.sinImagen = 1;
            Persona1.seleccionar = "Identificacion";
            Persona1.identificacion = txtIdentificacion.Text;
            Persona1 = Persona1Servicio.ConsultarPersona1Param(Persona1, (Usuario)Session["usuario"]);
            if (Persona1.nombre == "errordedatos")
            {
                txtCodPersona.Text = "";
                txtNombres.Text = "";
            }
            else
            {
                txtCodPersona.Text = Persona1.cod_persona != 0 ? Persona1.cod_persona.ToString() : "";
                txtNombres.Text = Persona1.nombres;
            }
        }
        else
        {
            txtCodPersona.Text = "";
            txtNombres.Text = "";
        }
    }
    
    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }

    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        if (txtCodCuenta.Text != "")
        {
            // Determinar los datos de la cuenta contable
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.NIIF.Entities.PlanCuentasNIIF PlanCuentas = new Xpinn.NIIF.Entities.PlanCuentasNIIF();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentasNIIF(txtCodCuenta.Text, (Usuario)Session["usuario"]);
            //int rowIndex = Convert.ToInt32(txtCodCuenta.CommandArgument);

            // Mostrar el nombre de la cuenta            
            if (txtNomCuenta != null)
                txtNomCuenta.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuenta.Text = "";
        }
    }

    protected void txtFechaEmi_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Comun.Services.FechasService _fechaServicio = new Xpinn.Comun.Services.FechasService();
        if (txtFechaEmi.TieneDatos)
        {
            if (txtPlazo.Text.Trim() != "")
            {
                try
                {
                    Int32 tipocalendario = ddlTipoCalendario.SelectedValue == "" ? 1 : Convert.ToInt32(ddlTipoCalendario.SelectedValue);
                    Configuracion conf = new Configuracion();
                    DateTime fechaVencimiento = _fechaServicio.FecSumDia(txtFechaEmi.ToDateTime, ConvertirStringToInt32(txtPlazo.Text), tipocalendario);                
                    txtFechaVenc.Text = fechaVencimiento.ToString(conf.ObtenerFormatoFecha());
                }
                catch { }
            }
        }
    }

    protected void txtPlazo_TextChanged(object sender, EventArgs e)
    {
        txtFechaEmi_TextChanged(txtFechaEmi, e);
    }

    protected void ddlTipoCalendario_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFechaEmi_TextChanged(txtFechaEmi, e);
    }


}