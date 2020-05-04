using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

using System.Globalization;

public partial class Detalle : GlobalWeb
{
    StringHelper _stringHelper = new StringHelper();
    LiquidacionVacacionesEmpleadoService _vVacacionesService = new LiquidacionVacacionesEmpleadoService();
    long? _consecutivoEmpleado;
    long? _consecutivoVacaciones;
    bool _esNuevoRegistro;


    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_vVacacionesService.CodigoPrograma2, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_vVacacionesService.CodigoPrograma2, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
            // Si aqui viene algo significa que voy a crear uno
            _consecutivoEmpleado = Session[_vVacacionesService.CodigoPrograma2 + ".idEmpleado"] as long?;

            // Si aqui viene algo significa que voy a modificar uno
            _consecutivoVacaciones = Session[_vVacacionesService.CodigoPrograma2 + ".idDiasVacaciones"] as long?;

            _esNuevoRegistro = !_consecutivoVacaciones.HasValue;

            if (!IsPostBack)
            {
                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_vVacacionesService.CodigoPrograma2, "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoIdentificacion);

        txtFechaInicial.Attributes.Add("readonly", "readonly");

        EmpleadoService empleadoService = new EmpleadoService();
        List<NominaEmpleado> listaNominas = empleadoService.ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo(_consecutivoEmpleado.Value, Usuario);

        ddlTipoNomina.DataSource = listaNominas;
        ddlTipoNomina.DataValueField = "consecutivo";
        ddlTipoNomina.DataTextField = "descripcion";
        ddlTipoNomina.DataBind();

      
        if (!_esNuevoRegistro)
        {
            LlenarDiasVacaciones();
        }
        else
        {
            ConsultarDatosPersona();
        }
    }

    void LlenarDiasVacaciones()
    {
        LiquidacionVacacionesEmpleado diasvacaciones = _vVacacionesService.ConsultarDiasVacaciones(_consecutivoVacaciones.Value, Usuario);

        txtCodigoVacaciones.Text = diasvacaciones.consecutivo.ToString();
        txtIdentificacionn.Text = diasvacaciones.identificacion;

        if (!string.IsNullOrWhiteSpace(diasvacaciones.tipo_identificacion))
        {
            ddlTipoIdentificacion.SelectedValue = diasvacaciones.tipo_identificacion;
        }

        txtCodigoEmpleado.Text = diasvacaciones.codigoempleado.ToString();
      //  hiddenCodigoPersona.Value = diasvacaciones.codigopersona.ToString();
        txtNombreCliente.Text = diasvacaciones.nombre_empleado;
        txtFechaInicial.Text = diasvacaciones.fechainicio.ToShortDateString();
        txtFechaFinal.Text = diasvacaciones.fechaterminacion.ToShortDateString();
        txtFechaInicioPeriodo.Text = diasvacaciones.fechainicioperiodo.ToShortDateString();
        txtFechaTerminacionPeriodo.Text = diasvacaciones.fechaterminacionperiodo.ToShortDateString();
        txtCantidadDias.Text = diasvacaciones.cantidaddias.ToString() ;
        if (diasvacaciones.codigonomina > 0)
        {                                  
            ddlTipoNomina.SelectedValue = diasvacaciones.codigonomina.ToString();
        }

       
    }

    void ConsultarDatosPersona()
    {
        LiquidacionVacacionesEmpleadoService _liquidacionVacacionesServices = new LiquidacionVacacionesEmpleadoService();

        EmpleadoService empleadoService = new EmpleadoService();

        Empleados empleado = empleadoService.ConsultarInformacionPersonaEmpleado(_consecutivoEmpleado.Value, Usuario);

        txtIdentificacionn.Text = empleado.identificacion.ToString();
        ddlTipoIdentificacion.SelectedValue = empleado.cod_identificacion;
        txtNombreCliente.Text = empleado.nombre;
        txtCodigoEmpleado.Text = _consecutivoEmpleado.Value.ToString();
        hiddenCodigoPersona.Value = empleado.cod_persona.ToString();

        LiquidacionVacacionesEmpleado liquidacion = _liquidacionVacacionesServices.ConsultarLiquidacionVacacionesEmpleadoXCodigo(_consecutivoEmpleado.Value, Usuario);

        txtFechaInicioPeriodo.Text = liquidacion.fechainicioperiodo.ToShortDateString();
        txtFechaTerminacionPeriodo.Text = liquidacion.fechaterminacionperiodo.ToShortDateString();
        if (txtFechaInicioPeriodo.Text == "1/01/0001" || txtFechaInicioPeriodo.Text == "01/01/0001")
        {

            txtFechaInicioPeriodo.Text = empleado.fechainicioperiodo.ToShortDateString();
            txtFechaTerminacionPeriodo.Text = empleado.fechaterminacionperiodo.ToShortDateString();

        }
    }


        #endregion


        #region Eventos Botonera


        void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        // Borramos las sesiones para no mezclar cosas luego
        Session.Remove(_vVacacionesService.CodigoPrograma2 + ".idDiasVacaciones");
        Session.Remove(_vVacacionesService.CodigoPrograma2 + ".idEmpleado");

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
                LiquidacionVacacionesEmpleado DiasvacacionesEmpleado = ObtenerValores();

                if (_esNuevoRegistro)
                {
                    DiasvacacionesEmpleado = _vVacacionesService.CrearDiasVacacionesEmpleados(DiasvacacionesEmpleado, Usuario);
                }
                else
                {
                    DiasvacacionesEmpleado = _vVacacionesService.ModificarDiasVacacionesEmpleados(DiasvacacionesEmpleado, Usuario);
                }

                if (DiasvacacionesEmpleado.consecutivo > 0)
                {
                    mvDatos.SetActiveView(vFinal);

                    // Borramos las sesiones para no mezclar cosas luego
                    Session.Remove(_vVacacionesService.CodigoPrograma2 + ".idDiasVacaciones");
                    Session.Remove(_vVacacionesService.CodigoPrograma2 + ".idEmpleado");

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
        
        return true;
    }


    #endregion


    #region Metodos Ayuda


    LiquidacionVacacionesEmpleado ObtenerValores()
    {
        LiquidacionVacacionesEmpleado diaVacaciones = new LiquidacionVacacionesEmpleado
        {
            codigoempleado = Convert.ToInt64(txtCodigoEmpleado.Text),
          
            fechainicio = !string.IsNullOrWhiteSpace(txtFechaInicial.Text) ? Convert.ToDateTime(txtFechaInicial.Text) : default(DateTime),
            fechaterminacion = !string.IsNullOrWhiteSpace(txtFechaFinal.Text) ? Convert.ToDateTime(txtFechaFinal.Text) : default(DateTime),
            consecutivo = _consecutivoVacaciones.HasValue ? _consecutivoVacaciones.Value : 0,
            codigonomina = !string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) ? Convert.ToInt64(ddlTipoNomina.SelectedValue) : default(long),
            cantidaddias = Convert.ToInt64(txtCantidadDias.Text),
            fechainicioperiodo=!string.IsNullOrWhiteSpace(txtFechaInicioPeriodo.Text) ? Convert.ToDateTime(txtFechaInicioPeriodo.Text) : default(DateTime),
            fechaterminacionperiodo = !string.IsNullOrWhiteSpace(txtFechaTerminacionPeriodo.Text) ? Convert.ToDateTime(txtFechaTerminacionPeriodo.Text) : default(DateTime),


        };

        return diaVacaciones;
    }


    #endregion



    protected void txtCantidadDias_TextChanged(object sender, EventArgs e)
    {
        DateTimeHelper dateTimeHelper = new DateTimeHelper();
        String format = "dd/MM/yyyy";
        Int32 dias = 0;
        long numeroDias = 0;
        DateTime fechaInicio = DateTime.ParseExact(txtFechaInicial.Text, format, CultureInfo.InvariantCulture);
        DateTime fechaFinal;
        DateTime fechaFinalmostrar;
        dias = Convert.ToInt32(txtCantidadDias.Text);       
        fechaFinal = dateTimeHelper.SumarDiasSegunTipoCalendario(fechaInicio, dias,1);
        txtFechaFinal.Text = (fechaFinal.ToShortDateString());
    }
}