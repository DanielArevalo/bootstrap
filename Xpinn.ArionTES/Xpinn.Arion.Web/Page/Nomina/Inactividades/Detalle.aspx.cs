using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Detalle : GlobalWeb
{
    InactividadesService _inactividadServices = new InactividadesService();
    long? _consecutivoEmpleado;
    long? _consecutivoInactividad;
    bool _esNuevoRegistro;


    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_inactividadServices.CodigoPrograma, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_inactividadServices.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        chkaplicada.Enabled = false;
        try
        {
            // Si aqui viene algo significa que voy a crear uno
            _consecutivoEmpleado = Session[_inactividadServices.CodigoPrograma + ".idEmpleado"] as long?;

            // Si aqui viene algo significa que voy a modificar uno
            _consecutivoInactividad = Session[_inactividadServices.CodigoPrograma + ".idInactividad"] as long?;

            _esNuevoRegistro = !_consecutivoInactividad.HasValue;

            if (!IsPostBack)
            {
                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_inactividadServices.CodigoPrograma, "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoIdentificacion);



        PagosDescuentosFijosService conceptoService = new PagosDescuentosFijosService();
        PagosDescuentosFijos concepto = new PagosDescuentosFijos();
        string filtro = ObtenerFiltro();
        ddlConcepto.DataSource = conceptoService.ListarConceptosNomina(filtro, (Usuario)Session["usuario"]);
        ddlConcepto.DataTextField = "descripcion";
        ddlConcepto.DataValueField = "consecutivo";
        ddlConcepto.DataBind();




        txtFechaInicio.Attributes.Add("readonly", "readonly");
        txtFechaTerminacion.Attributes.Add("readonly", "readonly");

        EmpleadoService empleadoService = new EmpleadoService();
        List<NominaEmpleado> listaNominas = empleadoService.ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo(_consecutivoEmpleado.Value, Usuario);

        ddlTipoNomina.DataSource = listaNominas;
        ddlTipoNomina.DataValueField = "consecutivo";
        ddlTipoNomina.DataTextField = "descripcion";
        ddlTipoNomina.DataBind();

        ddlTipoNomina_SelectedIndexChanged(ddlTipoNomina, EventArgs.Empty);

        if (!_esNuevoRegistro)
        {
            LlenarInactividad();
        }
        else
        {
            ConsultarDatosPersona();
        }
    }
    string ObtenerFiltro()
    {
        string filtro = string.Empty;
        filtro += " and a.tipoconcepto  in (11)";


        return filtro;
    }
    void LlenarInactividad()
    {
        Inactividades inactividad = _inactividadServices.ConsultarInactividades(_consecutivoInactividad.Value, Usuario);

        txtCodigoInactividad.Text = inactividad.consecutivo.ToString();
        txtIdentificacionn.Text = inactividad.identificacion;

        if (!string.IsNullOrWhiteSpace(inactividad.tipo_identificacion))
        {
            ddlTipoIdentificacion.SelectedValue = inactividad.tipo_identificacion;
        }

        txtCodigoEmpleado.Text = inactividad.codigoempleado.ToString();
        hiddenCodigoPersona.Value = inactividad.codigopersona.ToString();
        txtNombreCliente.Text = inactividad.nombre_empleado;

        if (inactividad.codigo_tipo_nomina.HasValue)
        {
            ddlTipoNomina.SelectedValue = inactividad.codigo_tipo_nomina.Value.ToString();
        }

        if (inactividad.tipo.HasValue)
        {
            checkBoxListTipos.SelectedValue = inactividad.tipo.Value.ToString();
        }

        if (inactividad.remunerada.HasValue)
        {
            checkBoxListRemunerada.SelectedValue = inactividad.remunerada.Value.ToString();
        }

        if (inactividad.cod_concepto.HasValue)
        {
            ddlConcepto.SelectedValue = inactividad.cod_concepto.Value.ToString();
        }

        if (inactividad.fechainicio.HasValue)
        {
            txtFechaInicio.Text = inactividad.fechainicio.Value.ToShortDateString();
        }

        if (inactividad.fechaterminacion.HasValue)
        {
            txtFechaTerminacion.Text = inactividad.fechaterminacion.Value.ToShortDateString();
        }

        if (inactividad.codigotipocontrato.HasValue)
        {
            ddlContrato.SelectedValue = inactividad.codigotipocontrato.Value.ToString();
        }

        txtDescripcion.Text = inactividad.descripcion;

        if (inactividad.aplicada.HasValue)
        {
            chkaplicada.SelectedValue = inactividad.aplicada.Value.ToString();
        }

        if (inactividad.aplicada == 1)
        {
            checkBoxListTipos.Enabled = false;
            ddlConcepto.Enabled = false;
            txtFechaInicio.Enabled = false;
            txtFechaTerminacion.Enabled = false;
            txtDescripcion.Enabled = false;
            checkBoxListRemunerada.Enabled = false;
            chkaplicada.Enabled = false;
            ddlContrato.Enabled = false;
            ddlTipoNomina.Enabled = false;

        }

      

        Site toolBar = (Site)Master;
        if(chkaplicada.SelectedValue == "1")
        { 
        toolBar.MostrarGuardar(false);
        }
        if (chkaplicada.SelectedValue == "0")
        {
            toolBar.MostrarGuardar(true);
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
    }


    #endregion


    #region Eventos Botonera


    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        // Borramos las sesiones para no mezclar cosas luego
        Session.Remove(_inactividadServices.CodigoPrograma + ".idInactividad");
        Session.Remove(_inactividadServices.CodigoPrograma + ".idEmpleado");

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
                Inactividades inactividad = ObtenerValores();

                if (_esNuevoRegistro)
                {
                    inactividad = _inactividadServices.CrearInactividades(inactividad, Usuario);
                }
                else
                {
                    _inactividadServices.ModificarInactividades(inactividad, Usuario);
                }

                if (inactividad.consecutivo > 0)
                {
                    mvDatos.SetActiveView(vFinal);

                    // Borramos las sesiones para no mezclar cosas luego
                    Session.Remove(_inactividadServices.CodigoPrograma + ".idInactividad");
                    Session.Remove(_inactividadServices.CodigoPrograma + ".idEmpleado");

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


    #region Eventos DropDownList


    protected void ddlTipoNomina_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue))
        {
            long codigoNomina = Convert.ToInt64(ddlTipoNomina.SelectedValue);

            IngresoPersonalService ingresoPersonalService = new IngresoPersonalService();
            IngresoPersonal contrato = ingresoPersonalService.ConsultarInformacionDeContratoActivoDeUnEmpleadoSegunNomina(_consecutivoEmpleado.Value, codigoNomina, Usuario);

            TipoContratoService tipoContratoService = new TipoContratoService();
            List<TipoContrato> listaTipoContratos = tipoContratoService.ListarTipoContratos(Usuario);

            ddlContrato.DataSource = listaTipoContratos.Where(x => contrato.codigotipocontrato == x.cod_tipo_contrato).ToList();
            ddlContrato.DataTextField = "descripcion";
            ddlContrato.DataValueField = "cod_tipo_contrato";
            ddlContrato.DataBind();
        }
    }


    #endregion


    #region Metodos Ayuda


    Inactividades ObtenerValores()
    {
        Inactividades inactividad = new Inactividades
        {
            codigoempleado = Convert.ToInt64(txtCodigoEmpleado.Text),
            codigopersona = Convert.ToInt64(hiddenCodigoPersona.Value),
            fechainicio = !string.IsNullOrWhiteSpace(txtFechaInicio.Text) ? Convert.ToDateTime(txtFechaInicio.Text) : default(DateTime?),
            fechaterminacion = !string.IsNullOrWhiteSpace(txtFechaTerminacion.Text) ? Convert.ToDateTime(txtFechaTerminacion.Text) : default(DateTime?),
            cod_concepto = !string.IsNullOrWhiteSpace(ddlConcepto.SelectedValue) ? Convert.ToInt32(ddlConcepto.SelectedValue) : default(int?),
            codigotipocontrato = !string.IsNullOrWhiteSpace(ddlContrato.SelectedValue) ? Convert.ToInt32(ddlContrato.SelectedValue) : default(long?),
            remunerada = !string.IsNullOrWhiteSpace(checkBoxListRemunerada.SelectedValue) ? Convert.ToInt32(checkBoxListRemunerada.SelectedValue) : default(int?),
            tipo = !string.IsNullOrWhiteSpace(checkBoxListTipos.SelectedValue) ? Convert.ToInt32(checkBoxListTipos.SelectedValue) : default(int?),
            descripcion = txtDescripcion.Text,
            consecutivo = _consecutivoInactividad.HasValue ? _consecutivoInactividad.Value : 0,
            codigo_tipo_nomina = !string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) ? Convert.ToInt64(ddlTipoNomina.SelectedValue) : default(long?),
        };

        return inactividad;
    }

    bool ValidarDatos()
    {
        if (string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) || string.IsNullOrWhiteSpace(checkBoxListTipos.SelectedValue) || string.IsNullOrWhiteSpace(ddlConcepto.SelectedValue)
            || string.IsNullOrWhiteSpace(txtFechaInicio.Text) || string.IsNullOrWhiteSpace(txtFechaTerminacion.Text) || string.IsNullOrWhiteSpace(checkBoxListRemunerada.SelectedValue) || string.IsNullOrWhiteSpace(ddlContrato.Text))
        {
            VerError("Faltan datos por llenar!.");
            return false;
        }
        else if (Convert.ToDateTime(txtFechaInicio.Text) > Convert.ToDateTime(txtFechaTerminacion.Text))
        {
            VerError("La fecha de inicio no puede ser mayor a la fecha de terminacion!.");
            return false;
        }

        return true;
    }


    #endregion


}