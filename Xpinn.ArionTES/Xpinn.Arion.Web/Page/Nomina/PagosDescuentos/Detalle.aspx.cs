using System;
using System.Collections.Generic;
using System.Web.UI;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;
using System.Linq;
using System.Web.UI.WebControls;
public partial class Detalle : GlobalWeb
{
    PagosDescuentosFijosService _pagosDescuentosServices = new PagosDescuentosFijosService();
    long? _consecutivoEmpleado;
    long? _consecutivoRegistroPago;
    bool _esNuevoRegistro;


    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_pagosDescuentosServices.CodigoPrograma, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_pagosDescuentosServices.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Si aqui viene algo significa que voy a crear uno
            _consecutivoEmpleado = Session[_pagosDescuentosServices.CodigoPrograma + ".idEmpleado"] as long?;

            // Si aqui viene algo significa que voy a modificar uno
            _consecutivoRegistroPago = Session[_pagosDescuentosServices.CodigoPrograma + ".idRegistroPago"] as long?;

            _esNuevoRegistro = !_consecutivoRegistroPago.HasValue;

            if (!IsPostBack)
            {
                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_pagosDescuentosServices.CodigoPrograma, "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoIdentificacion);
        // LlenarListasDesplegables(TipoLista.ConceptoNomina, ddlConcepto);
        LlenarListasDesplegables(TipoLista.CentroCostos, ddlCentroCosto);


        PagosDescuentosFijosService conceptoService = new PagosDescuentosFijosService();
        PagosDescuentosFijos concepto = new PagosDescuentosFijos();
        string filtro = ObtenerFiltro();
        ddlConcepto.DataSource = conceptoService.ListarConceptosNomina(filtro, (Usuario)Session["usuario"]);
        ddlConcepto.DataTextField = "descripcion";
        ddlConcepto.DataValueField = "consecutivo";
        ddlConcepto.DataBind();
        ddlConcepto.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));





        txtFecha.Attributes.Add("readonly", "readonly");

        EmpleadoService empleadoService = new EmpleadoService();
        List<NominaEmpleado> listaNominas = empleadoService.ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo(_consecutivoEmpleado.Value, Usuario);

        ddlTipoNomina.DataSource = listaNominas;
        ddlTipoNomina.DataValueField = "consecutivo";
        ddlTipoNomina.DataTextField = "descripcion";
        ddlTipoNomina.DataBind();

        if (!_esNuevoRegistro)
        {
            LlenarDescuentoPago();
        }
        else
        {
            ConsultarDatosPersona();
        }
    }

    void LlenarDescuentoPago()
    {
        Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();

        PagosDescuentosFijos pagosDescuentos = _pagosDescuentosServices.ConsultarPagosDescuentosFijos(_consecutivoRegistroPago.Value, Usuario);

        txtCodigoPago.Text = pagosDescuentos.consecutivo.ToString();
        txtIdentificacionn.Text = pagosDescuentos.identificacion;
        txtControlSaldos.Visible = false;
        lblacumulado.Visible = true;
        txtAcumulado.Visible = true;
        if (!string.IsNullOrWhiteSpace(pagosDescuentos.tipo_identificacion))
        {
            ddlTipoIdentificacion.SelectedValue = pagosDescuentos.tipo_identificacion;
        }

        txtCodigoEmpleado.Text = pagosDescuentos.codigoempleado.ToString();
        hiddenCodigoPersona.Value = pagosDescuentos.codigopersona.ToString();
        txtNombreCliente.Text = pagosDescuentos.nombre_empleado;

        if (pagosDescuentos.codigotiponomina.HasValue)
        {
            ddlTipoNomina.SelectedValue = pagosDescuentos.codigotiponomina.Value.ToString();
        }

        txtValorCuota.Text = pagosDescuentos.valorcuota.HasValue ? pagosDescuentos.valorcuota.Value.ToString() : string.Empty;
        txtValorTotal.Text = pagosDescuentos.valortotal.HasValue ? pagosDescuentos.valortotal.Value.ToString() : string.Empty;
        txtAcumulado.Text = pagosDescuentos.acumulado.HasValue ? pagosDescuentos.acumulado.Value.ToString() : string.Empty;
        txtFecha.Text = pagosDescuentos.fecha.HasValue ? pagosDescuentos.fecha.Value.ToShortDateString() : string.Empty;
        txtControlSaldos.Text = pagosDescuentos.controlsaldos.HasValue ? pagosDescuentos.controlsaldos.Value.ToString() : string.Empty;

        if (pagosDescuentos.liquidapagodefinitiva.HasValue)
        {
            checkBoxListPagoDefinitivo.SelectedValue = pagosDescuentos.liquidapagodefinitiva.Value.ToString();
        }

        if (pagosDescuentos.liquidapagoperiodica.HasValue)
        {
            checkBoxListPagoPeriodica.SelectedValue = pagosDescuentos.liquidapagoperiodica.Value.ToString();
        }

        if (pagosDescuentos.descuentoperiocidad.HasValue)
        {
            checkBoxListPeriocidad.SelectedValue = pagosDescuentos.descuentoperiocidad.Value.ToString();
        }

        if (pagosDescuentos.codigoconceptonomina.HasValue)
        {
            ddlConcepto.SelectedValue = pagosDescuentos.codigoconceptonomina.Value.ToString();
        }

        if (pagosDescuentos.codigocentrocostos.HasValue)
        {
            ddlCentroCosto.SelectedValue = pagosDescuentos.codigocentrocostos.Value.ToString();
        }

        txtMotivos.Text = pagosDescuentos.motivos;
        Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
        pPersona.seleccionar = "Cod_persona";
        pPersona.noTraerHuella = 1;

        pPersona.cod_persona = Convert.ToInt64(pagosDescuentos.cod_proveedor);
        pPersona = PersonaService.ConsultarPersona1Param(pPersona, (Usuario)Session["usuario"]);
        if (pPersona.cod_persona != 0)
        {
            LblIdentificacion.Visible = true;
            LblCodPersona.Visible = true;
            txtCodPersona.Visible = true;
            LblNombre.Visible = true;
            txtNomPersona.Visible = true;
            btnConsultaPersonas.Visible = true;
            txtIdPersona.Visible = true;

            txtCodPersona.Text = pPersona.cod_persona.ToString();
            if (pPersona.identificacion != "")
                txtIdPersona.Text = pPersona.identificacion.ToString();
            if (pPersona.tipo_persona == "N")
                txtNomPersona.Text = pPersona.nombres + ' ' + pPersona.apellidos;
            else
                txtNomPersona.Text = pPersona.razon_social;
        }
    }

    void ConsultarDatosPersona()
    {
        EmpleadoService empleadoService = new EmpleadoService();

        Empleados empleado = empleadoService.ConsultarInformacionPersonaEmpleado(_consecutivoEmpleado.Value, Usuario);

        txtIdentificacionn.Text = empleado.identificacion.ToString();
        ddlTipoIdentificacion.SelectedValue = empleado.cod_identificacion;
        txtNombreCliente.Text = empleado.nombre;
        txtCodigoEmpleado.Text = _consecutivoEmpleado.Value.ToString();
        hiddenCodigoPersona.Value = empleado.cod_persona.ToString();


        txtAcumulado.Visible = false;

        lblacumulado.Visible = false;

        lblcontrolsaldos.Visible = false;
        txtControlSaldos.Visible = false;
    }


    #endregion


    #region Eventos Botonera


    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        // Borramos las sesiones para no mezclar cosas luego
        Session.Remove(_pagosDescuentosServices.CodigoPrograma + ".idRegistroPago");
        Session.Remove(_pagosDescuentosServices.CodigoPrograma + ".idEmpleado");

        if (mvDatos.GetActiveView() == vwDatos)
        {
            if (!_esNuevoRegistro)
            {
                Navegar(Pagina.Lista);
            }
            else
            {
                Navegar(Pagina.Nuevo);
            }
        }
        else if (mvDatos.GetActiveView() == vFinal)
        {
            Navegar(Pagina.Lista);
        }
    }

    void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarDatos())
            {
                PagosDescuentosFijos pagosDescuentos = ObtenerValores();
                _consecutivoRegistroPago = Session[_pagosDescuentosServices.CodigoPrograma + ".idRegistroPago"] as long?;
                _esNuevoRegistro = !_consecutivoRegistroPago.HasValue;


                if (_esNuevoRegistro)
                {
                    pagosDescuentos = _pagosDescuentosServices.CrearPagosDescuentosFijos(pagosDescuentos, Usuario);
                }
                else
                {
                    _pagosDescuentosServices.ModificarPagosDescuentosFijos(pagosDescuentos, Usuario);
                }

                if (pagosDescuentos.consecutivo > 0)
                {
                    mvDatos.SetActiveView(vFinal);

                    // Borramos las sesiones para no mezclar cosas luego
                    Session.Remove(_pagosDescuentosServices.CodigoPrograma + ".idRegistroPago");
                    Session.Remove(_pagosDescuentosServices.CodigoPrograma + ".idEmpleado");

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


    #endregion


    #region Metodos Ayuda
    string ObtenerFiltro()
    {
        string filtro = string.Empty;
        filtro += " and a.tipoconcepto not in(8,1,2,16,11,9,17,19,10)";


        return filtro;
    }


    PagosDescuentosFijos ObtenerValores()
    {
        PagosDescuentosFijos pagosDescuentos = new PagosDescuentosFijos
        {
            codigoempleado = Convert.ToInt64(txtCodigoEmpleado.Text),
            codigotiponomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
            codigopersona = Convert.ToInt64(hiddenCodigoPersona.Value),
            valorcuota = !string.IsNullOrWhiteSpace(txtValorCuota.Text) ? Convert.ToDecimal(txtValorCuota.Text) : default(decimal?),
            valortotal = !string.IsNullOrWhiteSpace(txtValorTotal.Text) ? Convert.ToDecimal(txtValorTotal.Text) : default(decimal?),
            acumulado = !string.IsNullOrWhiteSpace(txtAcumulado.Text) ? Convert.ToDecimal(txtAcumulado.Text) : default(decimal?),
            fecha = !string.IsNullOrWhiteSpace(txtFecha.Text) ? Convert.ToDateTime(txtFecha.Text) : default(DateTime?),
            controlsaldos = !string.IsNullOrWhiteSpace(txtControlSaldos.Text) ? Convert.ToInt64(txtControlSaldos.Text) : default(long?),
            liquidapagodefinitiva = !string.IsNullOrWhiteSpace(checkBoxListPagoDefinitivo.SelectedValue) ? Convert.ToInt32(checkBoxListPagoDefinitivo.SelectedValue) : default(int?),
            liquidapagoperiodica = !string.IsNullOrWhiteSpace(checkBoxListPagoPeriodica.SelectedValue) ? Convert.ToInt32(checkBoxListPagoPeriodica.SelectedValue) : default(int?),
            descuentoperiocidad = !string.IsNullOrWhiteSpace(checkBoxListPeriocidad.SelectedValue) ? Convert.ToInt32(checkBoxListPeriocidad.SelectedValue) : default(int?),
            codigoconceptonomina = !string.IsNullOrWhiteSpace(ddlConcepto.SelectedValue) ? Convert.ToInt32(ddlConcepto.SelectedValue) : default(int?),
            codigocentrocostos = !string.IsNullOrWhiteSpace(ddlCentroCosto.SelectedValue) ? Convert.ToInt64(ddlCentroCosto.SelectedValue) : default(long?),
            motivos = txtMotivos.Text,
            consecutivo = _consecutivoRegistroPago.HasValue ? _consecutivoRegistroPago.Value : 0,
            cod_proveedor = !string.IsNullOrWhiteSpace(txtCodPersona.Text) ? Convert.ToInt64(txtCodPersona.Text) : default(long?),

        };

        return pagosDescuentos;
    }

    bool ValidarDatos()
    {
        if (string.IsNullOrWhiteSpace(txtFecha.Text) || string.IsNullOrWhiteSpace(txtValorCuota.Text) || string.IsNullOrWhiteSpace(txtValorTotal.Text) || string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue)
            || string.IsNullOrWhiteSpace(checkBoxListPeriocidad.SelectedValue) || string.IsNullOrWhiteSpace(ddlConcepto.SelectedValue) || string.IsNullOrWhiteSpace(ddlCentroCosto.SelectedValue))
        {
            VerError("Faltan por llenar!.");
            return false;
        }
        else if (txtValorCuota.Text.Trim() == "0" || txtValorTotal.Text.Trim() == "0")
        {
            VerError("Valor cuota y valor total no pueden ser 0!.");
            return false;
        }
        else if (Convert.ToInt64(txtValorCuota.Text) > Convert.ToInt64(txtValorTotal.Text))
        {
            VerError("Valor de cuota no puede ser mayor al valor total a pagar!.");
            return false;
        }
        else if (ddlConcepto.SelectedValue == "0")
        {
            VerError("Seleccione un concepto");
            return false;
        }
        if (txtIdPersona.Visible == true)
        {
            if (txtIdPersona.Text.Trim() == "0" || txtIdPersona.Text.Trim() == "0" || txtIdPersona.Text.Trim() == "")
            { 
                VerError("Seleccione Datos del Tercero");
            return false;
            }
            return true;
        }

        return true;



    }


    #endregion



    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona");
    }

    protected void txtIdPersona_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
        Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();

        DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdPersona.Text, (Usuario)Session["usuario"]);

        if (DatosPersona.cod_persona != 0)
        {
            if (DatosPersona.cod_persona != 0)
                txtCodPersona.Text = DatosPersona.cod_persona.ToString();
            if (DatosPersona.identificacion != "")
                txtIdPersona.Text = DatosPersona.identificacion;
            if (DatosPersona.nombre != "")
                txtNomPersona.Text = DatosPersona.nombre;
        }
        else
        {
            txtNomPersona.Text = "";
            txtCodPersona.Text = "";
        }
    }

    protected void ddlConcepto_SelectedIndexChanged(object sender, EventArgs e)
    {
        PagosDescuentosFijosService conceptoService = new PagosDescuentosFijosService();
        PagosDescuentosFijos concepto = new PagosDescuentosFijos();
        String filtro = ddlConcepto.SelectedValue.ToString();
        concepto = conceptoService.ConsultarTipoConceptosNomina(filtro, (Usuario)Session["usuario"]);
        Int64 tipo = concepto.tipo;
        if (tipo == 2)
        {
            LblIdentificacion.Visible = true;
            LblCodPersona.Visible = true;
            txtCodPersona.Visible = true;
            LblNombre.Visible = true;
            txtNomPersona.Visible = true;
            btnConsultaPersonas.Visible = true;
            txtIdPersona.Visible = true;
        }

        if (tipo == 1)
        {
            LblIdentificacion.Visible = false;
            LblCodPersona.Visible = false;
            txtCodPersona.Visible = false;
            LblNombre.Visible = false;
            txtNomPersona.Visible = false;
            btnConsultaPersonas.Visible = false;
            txtIdPersona.Visible = false;
        }
    }
}