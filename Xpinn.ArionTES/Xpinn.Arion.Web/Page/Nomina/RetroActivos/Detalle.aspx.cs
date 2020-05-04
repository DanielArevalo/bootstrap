using System;
using System.Web.UI;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;
using System.Collections.Generic;

public partial class Detalle : GlobalWeb
{
    RetroactivoService _retroactivoService = new RetroactivoService();
    long? _consecutivoEmpleado;
    long? _consecutivoRetroactivo;
    bool _esNuevoRegistro;


    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_retroactivoService.CodigoPrograma, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_retroactivoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Si aqui viene algo significa que voy a crear uno
            _consecutivoEmpleado = Session[_retroactivoService.CodigoPrograma + ".idEmpleado"] as long?;

            // Si aqui viene algo significa que voy a modificar uno
            _consecutivoRetroactivo = Session[_retroactivoService.CodigoPrograma + ".idRetroactivo"] as long?;

            _esNuevoRegistro = !_consecutivoRetroactivo.HasValue;

            if (!IsPostBack)
            {
                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_retroactivoService.CodigoPrograma, "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoIdentificacion);
        LlenarListasDesplegables(TipoLista.CentroCostos, ddlCentroCosto);

        txtFechaInicio.Attributes.Add("readonly", "readonly");
        txtFechaFinal.Attributes.Add("readonly", "readonly");
        txtFechaPago.Attributes.Add("readonly", "readonly");

        EmpleadoService empleadoService = new EmpleadoService();
        List<NominaEmpleado> listaNominas = empleadoService.ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo(_consecutivoEmpleado.Value, Usuario);

        ddlTipoNomina.DataSource = listaNominas;
        ddlTipoNomina.DataValueField = "consecutivo";
        ddlTipoNomina.DataTextField = "descripcion";
        ddlTipoNomina.DataBind();

        if (!_esNuevoRegistro)
        {
            LlenarRetroactivo();
        }
        else
        {
            ConsultarDatosPersona();
        }
    }

    void LlenarRetroactivo()
    {
        Retroactivo retroactivo = _retroactivoService.ConsultarRetroactivo(_consecutivoRetroactivo.Value, Usuario);

        txtCodigoRetroactivo.Text = retroactivo.consecutivo.ToString();
        txtIdentificacionn.Text = retroactivo.identificacion;

        if (!string.IsNullOrWhiteSpace(retroactivo.tipo_identificacion))
        {
            ddlTipoIdentificacion.SelectedValue = retroactivo.tipo_identificacion;
        }

        txtCodigoEmpleado.Text = retroactivo.codigoempleado.ToString();
        hiddenCodigoPersona.Value = retroactivo.codigopersona.ToString();
        txtNombreCliente.Text = retroactivo.nombre_empleado;

        if (retroactivo.codigo_tipo_nomina.HasValue)
        {
            ddlTipoNomina.SelectedValue = retroactivo.codigo_tipo_nomina.Value.ToString();
        }

        if (retroactivo.periodicidad.HasValue)
        {
            checkBoxListaPeriodicidad.SelectedValue = retroactivo.periodicidad.Value.ToString();
        }

        if (retroactivo.conceptopagoretroactivo.HasValue)
        {
            checkBoxListaConcepto.SelectedValue = retroactivo.conceptopagoretroactivo.Value.ToString();
        }

        if (retroactivo.fechainicio.HasValue)
        {
            txtFechaInicio.Text = retroactivo.fechainicio.Value.ToShortDateString();
        }

        if (retroactivo.fechafinal.HasValue)
        {
            txtFechaFinal.Text = retroactivo.fechafinal.Value.ToShortDateString();
        }

        if (retroactivo.fechapago.HasValue)
        {
            txtFechaPago.Text = retroactivo.fechapago.Value.ToShortDateString();
        }

        if (retroactivo.codigocentrocosto.HasValue)
        {
            ddlCentroCosto.SelectedValue = retroactivo.codigocentrocosto.Value.ToString();
        }

        if (retroactivo.valor.HasValue)
        {
            txtValor.Text = retroactivo.valor.Value.ToString();
        }

        if (retroactivo.numeropagos.HasValue)
        {
            txtNumeroPagos.Text = retroactivo.numeropagos.Value.ToString();
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
        ddlTipoNomina.SelectedValue = empleado.cod_nomina_emp;
    }


    #endregion


    #region Eventos Botonera


    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        // Borramos las sesiones para no mezclar cosas luego
        Session.Remove(_retroactivoService.CodigoPrograma + ".idRetroactivo");
        Session.Remove(_retroactivoService.CodigoPrograma + ".idEmpleado");

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
                Retroactivo retroactivo = ObtenerValores();

                if (_esNuevoRegistro)
                {
                    retroactivo = _retroactivoService.CrearRetroactivo(retroactivo, Usuario);
                }
                else
                {
                    _retroactivoService.ModificarRetroactivo(retroactivo, Usuario);
                }

                if (retroactivo.consecutivo > 0)
                {
                    mvDatos.SetActiveView(vFinal);

                    // Borramos las sesiones para no mezclar cosas luego
                    Session.Remove(_retroactivoService.CodigoPrograma + ".idRetroactivo");
                    Session.Remove(_retroactivoService.CodigoPrograma + ".idEmpleado");

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


    Retroactivo ObtenerValores()
    {
        Retroactivo retroactivo = new Retroactivo
        {
            codigoempleado = Convert.ToInt64(txtCodigoEmpleado.Text),
            codigopersona = Convert.ToInt64(hiddenCodigoPersona.Value),
            fechainicio = !string.IsNullOrWhiteSpace(txtFechaInicio.Text) ? Convert.ToDateTime(txtFechaInicio.Text) : default(DateTime?),
            fechafinal = !string.IsNullOrWhiteSpace(txtFechaFinal.Text) ? Convert.ToDateTime(txtFechaFinal.Text) : default(DateTime?),
            fechapago = !string.IsNullOrWhiteSpace(txtFechaPago.Text) ? Convert.ToDateTime(txtFechaPago.Text) : default(DateTime?),
            conceptopagoretroactivo = !string.IsNullOrWhiteSpace(checkBoxListaConcepto.SelectedValue) ? Convert.ToInt32(checkBoxListaConcepto.SelectedValue) : default(int?),
            periodicidad = !string.IsNullOrWhiteSpace(checkBoxListaPeriodicidad.SelectedValue) ? Convert.ToInt32(checkBoxListaPeriodicidad.SelectedValue) : default(int?),
            codigocentrocosto = !string.IsNullOrWhiteSpace(ddlCentroCosto.SelectedValue) ? Convert.ToInt32(ddlCentroCosto.SelectedValue) : default(long?),
            valor = !string.IsNullOrWhiteSpace(txtValor.Text) ? Convert.ToDecimal(txtValor.Text) : default(decimal?),
            numeropagos = !string.IsNullOrWhiteSpace(txtNumeroPagos.Text) ? Convert.ToInt32(txtNumeroPagos.Text) : default(int?),
            consecutivo = _consecutivoRetroactivo.HasValue ? _consecutivoRetroactivo.Value : 0,
            codigo_tipo_nomina = !string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) ? Convert.ToInt64(ddlTipoNomina.SelectedValue) : default(long?),
        };

        return retroactivo;
    }

    bool ValidarDatos()
    {
        if (string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) || string.IsNullOrWhiteSpace(txtFechaInicio.Text) || string.IsNullOrWhiteSpace(txtFechaFinal.Text)
            || string.IsNullOrWhiteSpace(txtFechaPago.Text) || string.IsNullOrWhiteSpace(txtNumeroPagos.Text) || string.IsNullOrWhiteSpace(txtValor.Text)
            || string.IsNullOrWhiteSpace(checkBoxListaConcepto.SelectedValue) || string.IsNullOrWhiteSpace(checkBoxListaPeriodicidad.SelectedValue))
        {
            VerError("Faltan datos por llenar!.");
            return false;
        }
        else if (txtValor.Text.Trim() == "0" || txtNumeroPagos.Text.Trim() == "0")
        {
            VerError("Valor y numero de pagos no pueden ser 0!.");
            return false;
        }
        else if (Convert.ToDateTime(txtFechaInicio.Text) > Convert.ToDateTime(txtFechaFinal.Text))
        {
            VerError("La fecha de inicio del retroactivo no puede ser mayor a la fecha final!.");
            return false;
        }

        return true;
    }


    #endregion


}