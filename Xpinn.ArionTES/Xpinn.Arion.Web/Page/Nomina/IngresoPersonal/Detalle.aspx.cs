using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Detalle : GlobalWeb
{
    StringHelper _stringHelper = new StringHelper();
    IngresoPersonalService _ingresoPersonalService = new IngresoPersonalService();
    long? _consecutivoEmpleado;
    long? _consecutivoIngreso;
    bool _esNuevoRegistro;


    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_ingresoPersonalService.CodigoPrograma, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ingresoPersonalService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Si aqui viene algo significa que voy a crear uno
            _consecutivoEmpleado = Session[_ingresoPersonalService.CodigoPrograma + ".idEmpleado"] as long?;

            // Si aqui viene algo significa que voy a modificar uno
            _consecutivoIngreso = Session[_ingresoPersonalService.CodigoPrograma + ".idIngreso"] as long?;

            _esNuevoRegistro = _consecutivoEmpleado.HasValue && !_consecutivoIngreso.HasValue;

            if (!IsPostBack)
            {
                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ingresoPersonalService.CodigoPrograma, "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoIdentificacion);
        List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstEmpresas = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
      
        // LlenarListasDesplegables(TipoLista.Area, ddlArea);
        LlenarListasDesplegables(TipoLista.CentroCostos, ddlCentroCosto);
        LlenarListasDesplegables(TipoLista.NominaEmpleado, ddlTipoNomina);
        LlenarListasDesplegables(TipoLista.TipoCargo, ddlCargo);
        // LlenarListasDesplegables(TipoLista.Contratacion, ddlContrato);
        LlenarListasDesplegables(TipoLista.Bancos, ddlBancoConsignacion);

        LlenarListasDesplegables(TipoLista.FondoSalud, ddlFondoSalud);
        LlenarListasDesplegables(TipoLista.FondoPension, ddlFondoPension, ddlPensionVoluntaria);
        LlenarListasDesplegables(TipoLista.FondoCesantias, ddlFondoCesantias);
    

        /*MotivosCambioService motivosServicio = new MotivosCambioService();
           MotivosCambio motivo = new MotivosCambio();
           ddlARL.DataSource = motivosServicio.ListarParametrosArl(motivo, (Usuario)Session["usuario"]);
           ddlARL.DataTextField = "descripcion";
           ddlARL.DataValueField = "cod_motivo_cambio";
           ddlARL.DataBind();
           ddlARL.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
   */

        LlenarListasDesplegables(TipoLista.FondoARL, ddlARL);


        string filtro = string.Empty;
        ParametrosArlService parametroarlservicio = new ParametrosArlService();
        ParametrosArl parametroaarl = new ParametrosArl();
        ddlTipoRiesgoARL.DataSource = parametroarlservicio.ListarParametrosArl(filtro, (Usuario)Session["usuario"]);
        ddlTipoRiesgoARL.DataTextField = "descripcion";
        ddlTipoRiesgoARL.DataValueField = "consecutivo";
        ddlTipoRiesgoARL.DataBind();
        ddlTipoRiesgoARL.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));





        CargosService Cargosservicio = new CargosService();
        Cargos cargos = new Cargos();
        ddlCargo.DataSource = Cargosservicio.ListarCargos(filtro, (Usuario)Session["usuario"]);
        ddlCargo.DataTextField = "nombre";
        ddlCargo.DataValueField = "idcargo";
        ddlCargo.DataBind();
        ddlCargo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));


        IngresoPersonalService ingresoservicio = new IngresoPersonalService();
        IngresoPersonal ingreso = new IngresoPersonal();
        ddlArea.DataSource = ingresoservicio.ListarAreas(ingreso, (Usuario)Session["usuario"]);
        ddlArea.DataTextField = "descripcion";
        ddlArea.DataValueField = "consecutivo";
        ddlArea.DataBind();

        IngresoPersonalService ingresoservicio1 = new IngresoPersonalService();
        IngresoPersonal ingreso1 = new IngresoPersonal();
        ddlContrato.DataSource = ingresoservicio.ListarContratacion(ingreso1, (Usuario)Session["usuario"]);
        ddlContrato.DataTextField = "descripcion";
        ddlContrato.DataValueField = "consecutivo";
        ddlContrato.DataBind();


        //LlenarListasDesplegables(TipoLista.TipoRiesgoArl, ddlTipoRiesgoARL);

        LlenarListasDesplegables(TipoLista.CajaCompensacion, ddlCajaCompensacion);

        LlenarListasDesplegables(TipoLista.FormaPago, ddlFormaPago);
        LlenarListasDesplegables(TipoLista.TipoCotizante, ddlTipoCotizante);

        txtFechaIngreso.Attributes.Add("readonly", "readonly");
        txtFechaInicioPrueba.Attributes.Add("readonly", "readonly");
        txtFechaFinPrueba.Attributes.Add("readonly", "readonly");

        txtFechaIngresoCajaCompensacion.Attributes.Add("readonly", "readonly");
        txtFechaIngresoFondoCesantias.Attributes.Add("readonly", "readonly");
        txtFechaIngresoFondoPension.Attributes.Add("readonly", "readonly");
        txtFechaIngresoFondoSalud.Attributes.Add("readonly", "readonly");

        txtFechaRetiroCajaCompensacion.Attributes.Add("readonly", "readonly");
        txtFechaRetiroFondoCesantias.Attributes.Add("readonly", "readonly");
        txtFechaRetiroFondoPension.Attributes.Add("readonly", "readonly");
        txtFechaRetiroFondoSalud.Attributes.Add("readonly", "readonly");

        if (!_esNuevoRegistro)
        {
            LlenarIngresoPersonal();
            LlenarConceptosFijos();
        }
        else
        {
            ConsultarDatosPersona();

            gvConceptosFijos.DataSource = new List<ConceptosFijosNominaEmpleados>();
            gvConceptosFijos.DataBind();
        }

        ddlFormaPago_SelectedIndexChanged(ddlFormaPago, EventArgs.Empty);
        // ddlTipoRiesgoARL_SelectedIndexChanged(ddlTipoRiesgoARL, EventArgs.Empty);
    }

    void LlenarIngresoPersonal()
    {
        IngresoPersonal ingresoPersonal = _ingresoPersonalService.ConsultarIngresoPersonal(_consecutivoIngreso.Value, Usuario);

        if (ingresoPersonal.estaactivocontrato == 0)
        {
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
        }

        if (ingresoPersonal.estaactivocontrato == 1)
        {
            txtFechaRetiroFondoSalud.Enabled = false;
            txtFechaRetiroFondoPension.Enabled = false;
            txtFechaRetiroCajaCompensacion.Enabled = false;
            txtFechaRetiroFondoCesantias.Enabled = false;

            CalendarExtender6.Enabled = false;
            CalendarExtender4.Enabled = false;

            CalendarExtender9.Enabled = false;
            CalendarExtender11.Enabled = false;

        }


        txtCodigoIngreso.Text = ingresoPersonal.consecutivo.ToString();
        txtIdentificacionn.Text = ingresoPersonal.identificacion;

        if (!string.IsNullOrWhiteSpace(ingresoPersonal.tipo_identificacion))
        {
            ddlTipoIdentificacion.SelectedValue = ingresoPersonal.tipo_identificacion;
        }

        txtCodigoEmpleado.Text = ingresoPersonal.codigoempleado.ToString();
        hiddenCodigoPersona.Value = ingresoPersonal.codigopersona.ToString();
        txtNombreEmpleado.Text = ingresoPersonal.nombre_empleado;

        if (ingresoPersonal.codigonomina.HasValue)
        {
            ddlTipoNomina.SelectedValue = ingresoPersonal.codigonomina.Value.ToString();
        }

        if (ingresoPersonal.codigocentrocosto.HasValue)
        {
            ddlCentroCosto.SelectedValue = ingresoPersonal.codigocentrocosto.Value.ToString();
        }

        if (ingresoPersonal.codigocargo.HasValue)
        {
            ddlCargo.SelectedValue = ingresoPersonal.codigocargo.Value.ToString();
        }

        if (ingresoPersonal.codigotipocontrato.HasValue)
        {
            ddlContrato.SelectedValue = ingresoPersonal.codigotipocontrato.Value.ToString();
        }

        if (ingresoPersonal.esextranjero.HasValue)
        {
            chkExtranjero.SelectedValue = ingresoPersonal.esextranjero.Value.ToString();
        }

        rdButtonLey50.Checked = ingresoPersonal.tieneley50.HasValue && ingresoPersonal.tieneley50.Value == 1;

        txtFechaIngreso.Text = ingresoPersonal.fechaingreso.HasValue ? ingresoPersonal.fechaingreso.Value.ToShortDateString() : " ";
        txtFechaInicioPrueba.Text = ingresoPersonal.fechainicioperiodoprueba.HasValue ? ingresoPersonal.fechainicioperiodoprueba.Value.ToShortDateString() : " ";
        txtFechaFinPrueba.Text = ingresoPersonal.fechaterminacionperiodoprueba.HasValue ? ingresoPersonal.fechaterminacionperiodoprueba.Value.ToShortDateString() : " ";

        if (ingresoPersonal.salario.HasValue)
        {
            txtSalario.Text = ingresoPersonal.salario.Value.ToString();
        }

        if (ingresoPersonal.essueldovariable.HasValue)
        {
            chkSalarioVariable.SelectedValue = ingresoPersonal.essueldovariable.ToString();
        }

        if (ingresoPersonal.essalariointegral.HasValue)
        {
            chkSalarioIntegral.SelectedValue = ingresoPersonal.essalariointegral.ToString();
        }

        if (ingresoPersonal.auxiliotransporte.HasValue)
        {
            chkAuxilioTransporte.SelectedValue = ingresoPersonal.auxiliotransporte.ToString();
        }

        if (ingresoPersonal.formapago.HasValue)
        {
            ddlFormaPago.SelectedValue = ingresoPersonal.formapago.ToString();
        }

        if (ingresoPersonal.tipocuenta.HasValue)
        {
            chkTipoCuenta.SelectedValue = ingresoPersonal.tipocuenta.ToString();
        }

        if (ingresoPersonal.codigobanco.HasValue)
        {
            ddlBancoConsignacion.SelectedValue = ingresoPersonal.codigobanco.ToString();
        }

        if (!string.IsNullOrWhiteSpace(ingresoPersonal.numerocuentabancaria))
        {
            txtNumeroCuentaBancariaConsignacion.Text = ingresoPersonal.numerocuentabancaria;
        }

        if (ingresoPersonal.codigofondosalud.HasValue)
        {
            ddlFondoSalud.SelectedValue = ingresoPersonal.codigofondosalud.ToString();
        }

        if (ingresoPersonal.codigofondopension.HasValue)
        {
            ddlFondoPension.SelectedValue = ingresoPersonal.codigofondopension.ToString();
        }

        if (ingresoPersonal.codigofondocesantias.HasValue)
        {
            ddlFondoCesantias.SelectedValue = ingresoPersonal.codigofondocesantias.ToString();
        }

        if (ingresoPersonal.codigocajacompensacion.HasValue)
        {
            ddlCajaCompensacion.SelectedValue = ingresoPersonal.codigocajacompensacion.ToString();
        }

        if (ingresoPersonal.codigoarl.HasValue)
        {
            ddlARL.SelectedValue = ingresoPersonal.codigoarl.ToString();
        }

        if (ingresoPersonal.codigopensionvoluntaria.HasValue)
        {
            ddlPensionVoluntaria.SelectedValue = ingresoPersonal.codigopensionvoluntaria.ToString();
        }

        if (ingresoPersonal.fechaafiliacionsalud.HasValue)
        {
            txtFechaIngresoFondoSalud.Text = ingresoPersonal.fechaafiliacionsalud.Value.ToShortDateString();
        }

        if (ingresoPersonal.fechaafiliacionpension.HasValue)
        {
            txtFechaIngresoFondoPension.Text = ingresoPersonal.fechaafiliacionpension.Value.ToShortDateString();
        }

        if (ingresoPersonal.fechaafiliacioncesantias.HasValue)
        {
            txtFechaIngresoFondoCesantias.Text = ingresoPersonal.fechaafiliacioncesantias.Value.ToShortDateString();
        }

        if (ingresoPersonal.fechaafiliacajacompensacion.HasValue)
        {
            txtFechaIngresoCajaCompensacion.Text = ingresoPersonal.fechaafiliacajacompensacion.Value.ToShortDateString();
        }

        if (ingresoPersonal.fecharetirosalud.HasValue)
        {
            txtFechaRetiroFondoSalud.Text = ingresoPersonal.fecharetirosalud.Value.ToShortDateString();
        }

        if (ingresoPersonal.fecharetiropension.HasValue)
        {
            txtFechaRetiroFondoPension.Text = ingresoPersonal.fecharetiropension.Value.ToShortDateString();
        }

        if (ingresoPersonal.fecharetirocesantias.HasValue)
        {
            txtFechaRetiroFondoCesantias.Text = ingresoPersonal.fecharetirocesantias.Value.ToShortDateString();
        }

        if (ingresoPersonal.fecharetirocajacompensacion.HasValue)
        {
            txtFechaRetiroCajaCompensacion.Text = ingresoPersonal.fecharetirocajacompensacion.Value.ToShortDateString();
        }

        if (ingresoPersonal.tipocotizante.HasValue)
        {
            ddlTipoCotizante.SelectedValue = ingresoPersonal.tipocotizante.ToString();
        }

        if (ingresoPersonal.espensionadoporvejez.HasValue)
        {
            chkPensionadoPorVejez.SelectedValue = ingresoPersonal.espensionadoporvejez.ToString();
        }

        if (ingresoPersonal.espensionadoporinvalidez.HasValue)
        {
            chkPensionadoPorInvalidez.SelectedValue = ingresoPersonal.espensionadoporinvalidez.ToString();
        }

        if (ingresoPersonal.escontratoprestacional.HasValue)
        {
            chkEsContratoPrestacional.SelectedValue = ingresoPersonal.escontratoprestacional.ToString();
        }

        ddlArea.SelectedValue = ingresoPersonal.area.ToString();


        if (ingresoPersonal.tipo_riesgo.HasValue)
        {
            ddlTipoRiesgoARL.SelectedValue = ingresoPersonal.tipo_riesgo.ToString();
        }

        if (ingresoPersonal.porcentajearl.HasValue)
        {
            txtPorcentaje.Text = ingresoPersonal.porcentajearl.ToString();
        }

        if (ingresoPersonal.procesoretencion.HasValue)
        {
            chkProcRetencion.SelectedValue = ingresoPersonal.procesoretencion.ToString();
        }



        if (!string.IsNullOrWhiteSpace(ingresoPersonal.dia_habil))
        {
            ddldiahabil.SelectedValue = ingresoPersonal.dia_habil;
        }

        //ESTO EN EN CASO QUE INACTIVEN AL EMPLEADO CUANDO NO PUEDEN LIQUIDAR CONTRATO AUN Y TAMPOCO QUIERE QUE SALGA EN LA LQIUDIACION DE NOMINA DE 
        //LOS PERIODOS PENDIENTES 
        if (ingresoPersonal.inactivacion.HasValue)
        {
            chkInactiviacion.SelectedValue = ingresoPersonal.inactivacion.ToString();
        }


        if (ingresoPersonal.cod_empresa.HasValue)
        {
            Int64 codigopersona = Convert.ToInt64(hiddenCodigoPersona.Value);
            ddlEmpresa.DataSource = _ingresoPersonalService.ListarEmpresaRecaudoPersona(codigopersona, (Usuario)Session["usuario"]);

            if (ddlEmpresa.DataSource != null)
            {
                ddlEmpresa.Visible = true;
                Lblpagaduria.Visible = true;
                ddlEmpresa.DataTextField = "nom_empresa";
                ddlEmpresa.DataValueField = "cod_empresa";
                ddlEmpresa.DataBind();

            }

            ddlEmpresa.Visible = true;
            Lblpagaduria.Visible = true;
            ddlEmpresa.SelectedValue = ingresoPersonal.cod_empresa.ToString();
        }
    }

    void LlenarConceptosFijos()
    {
        List<ConceptosFijosNominaEmpleados> listaConceptosFijos = _ingresoPersonalService.ListarConceptosFijosNominaEmpleados(new ConceptosFijosNominaEmpleados { codigoIngresoPersonal = _consecutivoIngreso.Value }, Usuario);

        if (listaConceptosFijos.Count <= 0)
        {
            listaConceptosFijos.Add(new ConceptosFijosNominaEmpleados());
        }

        gvConceptosFijos.DataSource = listaConceptosFijos;
        gvConceptosFijos.DataBind();
    }



    void ConsultarDatosPersona()
    {
        EmpleadoService empleadoService = new EmpleadoService();

        Empleados empleado = empleadoService.ConsultarInformacionPersonaEmpleado(_consecutivoEmpleado.Value, Usuario);

        if (empleado == null)
        {
            VerError("No se pudo encontrar a la persona!.");
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
        }
        else
        {
            txtIdentificacionn.Text = empleado.identificacion.ToString();
            ddlTipoIdentificacion.SelectedValue = empleado.cod_identificacion;
            txtNombreEmpleado.Text = empleado.nombre;
            txtCodigoEmpleado.Text = _consecutivoEmpleado.Value.ToString();
            hiddenCodigoPersona.Value = empleado.cod_persona.ToString();
        }


        Int64 codigopersona = Convert.ToInt64(hiddenCodigoPersona.Value);
        if (codigopersona > 0)
        {

            ddlEmpresa.DataSource = _ingresoPersonalService.ListarEmpresaRecaudoPersona(codigopersona, (Usuario)Session["usuario"]);

            if(ddlEmpresa.DataSource!=null)
            {
                ddlEmpresa.Visible = true;
                Lblpagaduria.Visible = true;
                ddlEmpresa.DataTextField = "nom_empresa";
            ddlEmpresa.DataValueField = "cod_empresa";
            ddlEmpresa.DataBind();

            }
        }



        txtFechaRetiroFondoSalud.Enabled = false;
        txtFechaRetiroFondoPension.Enabled = false;
        txtFechaRetiroCajaCompensacion.Enabled = false;
        txtFechaRetiroFondoCesantias.Enabled = false;

        CalendarExtender6.Enabled = false;
        CalendarExtender4.Enabled = false;

        CalendarExtender9.Enabled = false;
        CalendarExtender11.Enabled = false;
        chkInactiviacion.Visible = false;
        Lblinactivacion.Visible = false;
    }


    #endregion


    #region Eventos Botonera


    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        // Borramos las sesiones para no mezclar cosas luego
        Session.Remove(_ingresoPersonalService.CodigoPrograma + ".idIngreso");
        Session.Remove(_ingresoPersonalService.CodigoPrograma + ".idEmpleado");

        if (!_esNuevoRegistro)
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Navegar(Pagina.Nuevo);
        }
    }

    void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");

            if (ValidarDatos())
            {
                IngresoPersonal ingresoPersonal = ObtenerValores();
                ingresoPersonal.inactivacion = 0;

                List<ConceptosFijosNominaEmpleados> listaConceptosFijos = ObtenerListaConceptoFijos(true);

                if (_esNuevoRegistro)
                {
                    ingresoPersonal = _ingresoPersonalService.CrearIngresoPersonal(ingresoPersonal, listaConceptosFijos, Usuario);
                }
                else
                {
                    _ingresoPersonalService.ModificarIngresoPersonal(ingresoPersonal, listaConceptosFijos, Usuario);
                }

                if (ingresoPersonal.consecutivo > 0)
                {
                    mvDatos.SetActiveView(vFinal);

                    // Borramos las sesiones para no mezclar cosas luego
                    Session.Remove(_ingresoPersonalService.CodigoPrograma + ".idIngreso");
                    Session.Remove(_ingresoPersonalService.CodigoPrograma + ".idEmpleado");

                    Site toolBar = (Site)Master;
                    toolBar.MostrarGuardar(false);
                }
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar el registro, " + ex.Message);
        }
    }

    protected void btnAgregarConcepto_Click(object sender, EventArgs e)
    {
        List<ConceptosFijosNominaEmpleados> listaConceptos = ObtenerListaConceptoFijos();
        listaConceptos.Add(new ConceptosFijosNominaEmpleados());

        gvConceptosFijos.DataSource = listaConceptos;
        gvConceptosFijos.DataBind();
    }


    #endregion


    #region Eventos Varios


    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(ddlFormaPago.SelectedValue))
        {
            FormaPagoEnum formaPago = ddlFormaPago.SelectedValue.ToEnum<FormaPagoEnum>();

            if (formaPago == FormaPagoEnum.Transferencia)
            {
                pnlInformacionBancaria.Visible = true;
            }
            else
            {
                pnlInformacionBancaria.Visible = false;
            }
        }
    }

    protected void txtFechaIngreso_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtFechaIngreso.Text))
        {
            txtFechaIngresoCajaCompensacion.Text = txtFechaIngreso.Text;
            txtFechaIngresoFondoCesantias.Text = txtFechaIngreso.Text;
            txtFechaIngresoFondoPension.Text = txtFechaIngreso.Text;
            txtFechaIngresoFondoSalud.Text = txtFechaIngreso.Text;
        }
    }

    protected void gvConceptosFijos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlConceptosGridView = e.Row.FindControl("ddlConceptosGridView") as DropDownList;
            LlenarListasDesplegables(TipoLista.ConceptoNomina, ddlConceptosGridView);

            ConceptosFijosNominaEmpleados conceptos = e.Row.DataItem as ConceptosFijosNominaEmpleados;

            if (conceptos.codigoconcepto > 0)
            {
                ddlConceptosGridView.SelectedValue = conceptos.codigoconcepto.ToString();
            }
        }
    }

    protected void gvConceptosFijos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        if (!string.IsNullOrWhiteSpace(TextoLaberError))
        {
            VerError("");
            RegistrarPostBack();
        }

        try
        {
            long idBorrar = Convert.ToInt64(gvConceptosFijos.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

            List<ConceptosFijosNominaEmpleados> listaConceptosNomina = ObtenerListaConceptoFijos();
            listaConceptosNomina.RemoveAt(Convert.ToInt32(e.CommandArgument));

            if (idBorrar > 0)
            {
                _ingresoPersonalService.EliminarConceptosFijosNominaEmpleados(idBorrar, Usuario);
            }

            if (listaConceptosNomina.Count <= 0)
            {
                listaConceptosNomina.Add(new ConceptosFijosNominaEmpleados());
            }

            gvConceptosFijos.DataSource = listaConceptosNomina;
            gvConceptosFijos.DataBind();
        }
        catch (Exception ex)
        {
            VerError("Error al borrar el registro, " + ex.Message);
            RegistrarPostBack();
        }
    }

    protected void gvConceptosFijos_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {

    }


    #endregion


    #region Metodos Ayuda


    bool ValidarDatos()
    {
        if (string.IsNullOrWhiteSpace(chkEsContratoPrestacional.SelectedValue))
        {
            VerError("Debes seleccionar si el contrato es prestacional o no!.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtFechaIngreso.Text))
        {
            VerError("Debes seleccionar la fecha de ingreso del empleado!.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(chkAuxilioTransporte.SelectedValue))
        {
            VerError("Debes seleccionar si recibe auxilio de transporte!.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtSalario.Text))
        {
            VerError("Debes seleccionar digitar el salario del empleado!.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(ddlContrato.SelectedValue))
        {
            VerError("Debes seleccionar el tipo de contrato del empleado!.");
            return false;
        }

        FormaPagoEnum formaPago = ddlFormaPago.SelectedValue.ToEnum<FormaPagoEnum>();
        if (formaPago == FormaPagoEnum.Transferencia)
        {
            if (string.IsNullOrWhiteSpace(chkTipoCuenta.SelectedValue) || string.IsNullOrWhiteSpace(ddlBancoConsignacion.SelectedValue) || string.IsNullOrWhiteSpace(txtNumeroCuentaBancariaConsignacion.Text))
            {
                VerError("Si la forma de pago es consignacion debes dar todos los datos bancarios del empleado (Faltan datos por llenar)!.");
                return false;
            }
        }

        if (string.IsNullOrWhiteSpace(txtCodigoEmpleado.Text))
        {
            VerError("Codigo de empleado invalido!.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue))
        {
            VerError("Codigo de nomina invalido!.");
            return false;
        }

        IngresoPersonal ingresoPersonal = new IngresoPersonal
        {
            codigoempleado = Convert.ToInt64(txtCodigoEmpleado.Text),
            codigonomina = !string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) ? Convert.ToInt64(ddlTipoNomina.SelectedValue) : default(long?)
        };
        // Un empleado no puede tener dos contratos en la misma nomina o los calculos en la liquidacion saldran raros
        // Por el amor a cristo no quitar esto o explota medio modulo
        if (_esNuevoRegistro)
        {
            bool existe = _ingresoPersonalService.VerificarQueEmpleadoNoTengaUnContratoActivoYaParaEstaNomina(ingresoPersonal, Usuario);
            if (existe)
            {
                VerError("Este empleado ya tiene un contrato activo para esta nomina, no puede tener mas de un contrato activo por nomina sin importar el tipo de contrato!.");
                return false;
            }
        }

        return true;
    }

    IngresoPersonal ObtenerValores()
    {

        IngresoPersonal ingresoPersonal = new IngresoPersonal
        {
            consecutivo = _consecutivoIngreso.HasValue ? _consecutivoIngreso.Value : 0,
            codigoempleado = Convert.ToInt64(txtCodigoEmpleado.Text),
            codigopersona = Convert.ToInt64(hiddenCodigoPersona.Value),
            fechaingreso = !string.IsNullOrWhiteSpace(txtFechaIngreso.Text) ? Convert.ToDateTime(txtFechaIngreso.Text) : default(DateTime?),
            fechainicioperiodoprueba = !string.IsNullOrWhiteSpace(txtFechaInicioPrueba.Text) ? Convert.ToDateTime(txtFechaInicioPrueba.Text) : default(DateTime?),
            fechaterminacionperiodoprueba = !string.IsNullOrWhiteSpace(txtFechaFinPrueba.Text) ? Convert.ToDateTime(txtFechaFinPrueba.Text) : default(DateTime?),
            codigocargo = !string.IsNullOrWhiteSpace(ddlCargo.SelectedValue) ? Convert.ToInt64(ddlCargo.SelectedValue) : default(long?),
            codigocentrocosto = !string.IsNullOrWhiteSpace(ddlCentroCosto.SelectedValue) ? Convert.ToInt64(ddlCentroCosto.SelectedValue) : default(long?),
            codigonomina = !string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) ? Convert.ToInt64(ddlTipoNomina.SelectedValue) : default(long?),
            codigotipocontrato = !string.IsNullOrWhiteSpace(ddlContrato.SelectedValue) ? Convert.ToInt64(ddlContrato.SelectedValue) : default(long?),
            esextranjero = !string.IsNullOrWhiteSpace(chkExtranjero.SelectedValue) ? Convert.ToInt32(chkExtranjero.SelectedValue) : default(int?),
            tieneley50 = rdButtonLey50.Checked ? 1 : 0,
            salario = Convert.ToDecimal(txtSalario.Text),
            essueldovariable = !string.IsNullOrWhiteSpace(chkSalarioVariable.SelectedValue) ? Convert.ToInt64(chkSalarioVariable.SelectedValue) : default(long?),

            auxiliotransporte = !string.IsNullOrWhiteSpace(chkAuxilioTransporte.SelectedValue) ? Convert.ToInt64(chkAuxilioTransporte.SelectedValue) : default(long?),
            formapago = !string.IsNullOrWhiteSpace(ddlFormaPago.SelectedValue) ? Convert.ToInt64(ddlFormaPago.SelectedValue) : default(long?),
            tipocuenta = !string.IsNullOrWhiteSpace(chkTipoCuenta.SelectedValue) ? Convert.ToInt64(chkTipoCuenta.SelectedValue) : default(long?),
            codigobanco = !string.IsNullOrWhiteSpace(ddlBancoConsignacion.SelectedValue) ? Convert.ToInt64(ddlBancoConsignacion.SelectedValue) : default(long?),
            numerocuentabancaria = txtNumeroCuentaBancariaConsignacion.Text,
            codigofondosalud = !string.IsNullOrWhiteSpace(ddlFondoSalud.SelectedValue) ? Convert.ToInt64(ddlFondoSalud.SelectedValue) : default(long?),
            codigofondopension = !string.IsNullOrWhiteSpace(ddlFondoPension.SelectedValue) ? Convert.ToInt64(ddlFondoPension.SelectedValue) : default(long?),
            codigofondocesantias = !string.IsNullOrWhiteSpace(ddlFondoCesantias.SelectedValue) ? Convert.ToInt64(ddlFondoCesantias.SelectedValue) : default(long?),
            codigocajacompensacion = !string.IsNullOrWhiteSpace(ddlCajaCompensacion.SelectedValue) ? Convert.ToInt64(ddlCajaCompensacion.SelectedValue) : default(long?),
            codigoarl = !string.IsNullOrWhiteSpace(ddlARL.SelectedValue) ? Convert.ToInt64(ddlARL.SelectedValue) : default(long?),
            codigopensionvoluntaria = !string.IsNullOrWhiteSpace(ddlPensionVoluntaria.SelectedValue) ? Convert.ToInt64(ddlPensionVoluntaria.SelectedValue) : default(long?),
            fechaafiliacionsalud = !string.IsNullOrWhiteSpace(txtFechaIngresoFondoSalud.Text) ? Convert.ToDateTime(txtFechaIngresoFondoSalud.Text) : default(DateTime?),
            fechaafiliacionpension = !string.IsNullOrWhiteSpace(txtFechaIngresoFondoPension.Text) ? Convert.ToDateTime(txtFechaIngresoFondoPension.Text) : default(DateTime?),
            fechaafiliacioncesantias = !string.IsNullOrWhiteSpace(txtFechaIngresoFondoCesantias.Text) ? Convert.ToDateTime(txtFechaIngresoFondoCesantias.Text) : default(DateTime?),
            fechaafiliacajacompensacion = !string.IsNullOrWhiteSpace(txtFechaIngresoCajaCompensacion.Text) ? Convert.ToDateTime(txtFechaIngresoCajaCompensacion.Text) : default(DateTime?),
            fecharetirosalud = !string.IsNullOrWhiteSpace(txtFechaRetiroFondoSalud.Text) ? Convert.ToDateTime(txtFechaRetiroFondoSalud.Text) : default(DateTime?),
            fecharetiropension = !string.IsNullOrWhiteSpace(txtFechaRetiroFondoPension.Text) ? Convert.ToDateTime(txtFechaRetiroFondoPension.Text) : default(DateTime?),
            fecharetirocesantias = !string.IsNullOrWhiteSpace(txtFechaRetiroFondoCesantias.Text) ? Convert.ToDateTime(txtFechaRetiroFondoCesantias.Text) : default(DateTime?),
            fecharetirocajacompensacion = !string.IsNullOrWhiteSpace(txtFechaRetiroCajaCompensacion.Text) ? Convert.ToDateTime(txtFechaRetiroCajaCompensacion.Text) : default(DateTime?),
            tipocotizante = !string.IsNullOrWhiteSpace(ddlTipoCotizante.SelectedValue) ? Convert.ToInt64(ddlTipoCotizante.SelectedValue) : default(long?),
            espensionadoporvejez = !string.IsNullOrWhiteSpace(chkPensionadoPorVejez.SelectedValue) ? Convert.ToInt64(chkPensionadoPorVejez.SelectedValue) : default(long?),
            espensionadoporinvalidez = !string.IsNullOrWhiteSpace(chkPensionadoPorInvalidez.SelectedValue) ? Convert.ToInt64(chkPensionadoPorInvalidez.SelectedValue) : default(long?),
            escontratoprestacional = !string.IsNullOrWhiteSpace(chkEsContratoPrestacional.SelectedValue) ? Convert.ToInt64(chkEsContratoPrestacional.SelectedValue) : default(long?),
            area = !string.IsNullOrWhiteSpace(ddlArea.SelectedValue) ? Convert.ToInt64(ddlArea.SelectedValue) : default(long),
            tipo_riesgo = Convert.ToInt64(ddlTipoRiesgoARL.SelectedValue),
            porcentajearl = Convert.ToDecimal(txtPorcentaje.Text),
            procesoretencion = !string.IsNullOrWhiteSpace(chkProcRetencion.SelectedValue) ? Convert.ToInt64(chkProcRetencion.SelectedValue) : default(long?),
            essalariointegral = !string.IsNullOrWhiteSpace(chkSalarioIntegral.SelectedValue) ? Convert.ToInt64(chkSalarioIntegral.SelectedValue) : default(long?),
            dia_habil = Convert.ToString(ddldiahabil.SelectedValue),
            inactivacion = !string.IsNullOrWhiteSpace(chkInactiviacion.SelectedValue) ? Convert.ToInt32(chkInactiviacion.SelectedValue) : default(long?),
            cod_empresa = !string.IsNullOrWhiteSpace(ddlEmpresa.SelectedValue) ? Convert.ToInt64(ddlEmpresa.SelectedValue) : default(long?)

        };

       
        return ingresoPersonal;
    }

    List<ConceptosFijosNominaEmpleados> ObtenerListaConceptoFijos(bool filtrarInvalidos = false)
    {
        List<ConceptosFijosNominaEmpleados> listaConceptosFijos = new List<ConceptosFijosNominaEmpleados>();

        foreach (GridViewRow row in gvConceptosFijos.Rows)
        {
            DropDownList ddlConceptosGridView = row.FindControl("ddlConceptosGridView") as DropDownList;
            long consecutivo = Convert.ToInt64(gvConceptosFijos.DataKeys[row.RowIndex].Value);

            if (!string.IsNullOrWhiteSpace(ddlConceptosGridView.SelectedValue))
            {
                ConceptosFijosNominaEmpleados concepto = new ConceptosFijosNominaEmpleados
                {
                    consecutivo = consecutivo,
                    codigoconcepto = Convert.ToInt64(ddlConceptosGridView.SelectedValue)
                };

                listaConceptosFijos.Add(concepto);
            }
            else if (!filtrarInvalidos)
            {
                listaConceptosFijos.Add(new ConceptosFijosNominaEmpleados());
            }
        }

        return listaConceptosFijos;
    }


    #endregion



    protected void chkPensionadoPorVejez_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in chkPensionadoPorVejez.Items)
        {
            if (item.Selected == true)
            {
                chkPensionadoPorInvalidez.Enabled = false;
                chkPensionadoPorInvalidez.ClearSelection();
            }

            if (item.Selected == false)
            {
                chkPensionadoPorInvalidez.Enabled = true;

            }
        }
    }



    protected void chkPensionadoPorInvalidez_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in chkPensionadoPorInvalidez.Items)
        {
            if (item.Selected == true)
            {
                chkPensionadoPorVejez.Enabled = false;
                chkPensionadoPorVejez.ClearSelection();
            }

            if (item.Selected == false)
            {
                chkPensionadoPorVejez.Enabled = true;

            }
        }
    }

    protected void ddlTipoRiesgoARL_SelectedIndexChanged(object sender, EventArgs e)
    {
        // if (_esNuevoRegistro)
        //{
        EmpleadoService empleadoService = new EmpleadoService();
        Int64 consecutivoarl = Convert.ToInt64(ddlTipoRiesgoARL.SelectedValue);
        Empleados empleados = empleadoService.ConsultarInformacioPorcentajeArl(consecutivoarl, Usuario);
        // ddlTipoRiesgoARL.SelectedValue = (empleados.tipo_riesgo.ToString());
        txtPorcentaje.Text = (empleados.porcentajearl.ToString());


        //}
        //else
        //{
        // EmpleadoService empleadoService = new EmpleadoService();
        //  Int64 consecutivoarl = Convert.ToInt64(ddlTipoRiesgoARL.SelectedValue);
        /// Empleados empleado = empleadoService.ConsultarInformacioPorcentajeArl(consecutivoarl, Usuario);
        //txtPorcentaje.Text = (empleado.porcentajearl.ToString());
        //}

    }

    protected void txtIdentificacionn_TextChanged(object sender, EventArgs e)
    {

    }
}