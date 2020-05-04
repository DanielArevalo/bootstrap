using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Detalle : GlobalWeb
{
    HorasExtrasEmpleadosService _horasExtrasService = new HorasExtrasEmpleadosService();
    long? _consecutivoEmpleado;
    long? _consecutivoHoraExtra;
    bool _esNuevoRegistro;


    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_horasExtrasService.CodigoPrograma, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_horasExtrasService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        chkPagadas.Enabled = false;
        try
        {
            // Si aqui viene algo significa que voy a crear uno
            _consecutivoEmpleado = Session[_horasExtrasService.CodigoPrograma + ".idEmpleado"] as long?;

            // Si aqui viene algo significa que voy a modificar uno
            _consecutivoHoraExtra = Session[_horasExtrasService.CodigoPrograma + ".idHoraExtra"] as long?;

            _esNuevoRegistro = !_consecutivoHoraExtra.HasValue;

            if (!IsPostBack)
            {
                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_horasExtrasService.CodigoPrograma, "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoIdentificacion);

        txtFecha.Attributes.Add("readonly", "readonly");

        EmpleadoService empleadoService = new EmpleadoService();
        List<NominaEmpleado> listaNominas = empleadoService.ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo(_consecutivoEmpleado.Value, Usuario);

        ddlTipoNomina.DataSource = listaNominas;
        ddlTipoNomina.DataValueField = "consecutivo";
        ddlTipoNomina.DataTextField = "descripcion";
        ddlTipoNomina.DataBind();

        List<NominaEmpleado> listaConceptosHorasExtras = empleadoService.ListarConceptoNominasQueSeanHorasExtas(Usuario);

        ddlConceptoNomina.DataSource = listaConceptosHorasExtras;
        ddlConceptoNomina.DataValueField = "consecutivo";
        ddlConceptoNomina.DataTextField = "descripcion";
        ddlConceptoNomina.DataBind();

        if (!_esNuevoRegistro)
        {
            LlenarHoraExtra();
        }
        else
        {
            ConsultarDatosPersona();
        }
    }

    void LlenarHoraExtra()
    {
        HorasExtrasEmpleados horaExtra = _horasExtrasService.ConsultarHorasExtrasEmpleados(_consecutivoHoraExtra.Value, Usuario);

        txtCodigoInactividad.Text = horaExtra.consecutivo.ToString();
        txtIdentificacionn.Text = horaExtra.identificacion_empleado;

        if (!string.IsNullOrWhiteSpace(horaExtra.tipo_identificacion))
        {
            ddlTipoIdentificacion.SelectedValue = horaExtra.tipo_identificacion;
        }

        txtCodigoEmpleado.Text = horaExtra.codigoempleado.ToString();
        hiddenCodigoPersona.Value = horaExtra.codigopersona.ToString();
        txtNombreCliente.Text = horaExtra.nombre_empleado;
        txtFecha.Text = horaExtra.fecha.ToShortDateString();
        txtCantidadHoras.Text = horaExtra.cantidadhoras.ToString();

        if (horaExtra.codigonomina > 0)
        {
            ddlTipoNomina.SelectedValue = horaExtra.codigonomina.ToString();
        }

        if (horaExtra.codigoconceptohoras > 0)
        {
            ddlConceptoNomina.SelectedValue = horaExtra.codigoconceptohoras.ToString();
        }

        if (horaExtra.pagadas.HasValue)
        {
            chkPagadas.SelectedValue = horaExtra.pagadas.ToString();
        }

        chkPagadas.Visible = true;
        Lblpagadas.Visible = true;
        if (horaExtra.pagadas == 1)
        {
            ddlTipoNomina.Enabled = false;
            txtFecha.Enabled = false;
            txtCantidadHoras.Enabled = false;
            ddlConceptoNomina.Enabled = false;
            chkPagadas.Enabled = false;
            txtFecha_CalendarExtender.Enabled = false;

            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
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


        chkPagadas.Visible = false;
        Lblpagadas.Visible = false;
    }


    #endregion


    #region Eventos Botonera


    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        // Borramos las sesiones para no mezclar cosas luego
        Session.Remove(_horasExtrasService.CodigoPrograma + ".idHoraExtra");
        Session.Remove(_horasExtrasService.CodigoPrograma + ".idEmpleado");

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
                HorasExtrasEmpleados horaExtraEmpleado = ObtenerValores();

                if (_esNuevoRegistro)
                {
                    horaExtraEmpleado = _horasExtrasService.CrearHorasExtrasEmpleados(horaExtraEmpleado, Usuario);
                }
                else
                {
                    _horasExtrasService.ModificarHorasExtrasEmpleados(horaExtraEmpleado, Usuario);
                }

                if (horaExtraEmpleado.consecutivo > 0)
                {
                    mvDatos.SetActiveView(vFinal);

                    // Borramos las sesiones para no mezclar cosas luego
                    Session.Remove(_horasExtrasService.CodigoPrograma + ".idHoraExtra");
                    Session.Remove(_horasExtrasService.CodigoPrograma + ".idEmpleado");

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

    bool ValidarDatos()
    {
        if (string.IsNullOrWhiteSpace(txtFecha.Text) || string.IsNullOrWhiteSpace(txtCantidadHoras.Text) 
            || string.IsNullOrWhiteSpace(ddlConceptoNomina.SelectedValue) || string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue))
        {
            VerError("Faltan datos por llenar!.");
            return false;
        }

        return true;
    }


    #endregion


    #region Metodos Ayuda


    HorasExtrasEmpleados ObtenerValores()
    {
        HorasExtrasEmpleados horaExtra = new HorasExtrasEmpleados
        {
            codigoempleado = Convert.ToInt64(txtCodigoEmpleado.Text),
            codigopersona = Convert.ToInt64(hiddenCodigoPersona.Value),
            fecha = !string.IsNullOrWhiteSpace(txtFecha.Text) ? Convert.ToDateTime(txtFecha.Text) : default(DateTime),
            consecutivo = _consecutivoHoraExtra.HasValue ? _consecutivoHoraExtra.Value : 0,
            codigonomina = !string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) ? Convert.ToInt64(ddlTipoNomina.SelectedValue) : default(long),
            codigoconceptohoras = !string.IsNullOrWhiteSpace(ddlConceptoNomina.SelectedValue) ? Convert.ToInt64(ddlConceptoNomina.SelectedValue) : default(long),
            cantidadhoras = Convert.ToDecimal(txtCantidadHoras.Text)
        };

        return horaExtra;
    }


    #endregion


}