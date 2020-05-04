using System;
using System.Collections.Generic;
using System.Web.UI;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Detalle : GlobalWeb
{
    NovedadPrimaService _novedadPrimaService = new NovedadPrimaService();
    long? _consecutivoEmpleado;
    long? _consecutivoNovedad;
    bool _esNuevoRegistro;


    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_novedadPrimaService.CodigoPrograma, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_novedadPrimaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Si aqui viene algo significa que voy a crear uno
            _consecutivoEmpleado = Session[_novedadPrimaService.CodigoPrograma + ".idEmpleado"] as long?;

            // Si aqui viene algo significa que voy a modificar uno
            _consecutivoNovedad = Session[_novedadPrimaService.CodigoPrograma + ".idNovedad"] as long?;

            _esNuevoRegistro = !_consecutivoNovedad.HasValue;

            if (!IsPostBack)
            {
                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_novedadPrimaService.CodigoPrograma, "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoIdentificacion);
        //LlenarListasDesplegables(TipoLista.ConceptoNomina, ddlTipoNovedad);

        EmpleadoService empleadoService = new EmpleadoService();
        List<NominaEmpleado> listaNominas = empleadoService.ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo(_consecutivoEmpleado.Value, Usuario);

        ddlTipoNomina.DataSource = listaNominas;
        ddlTipoNomina.DataValueField = "consecutivo";
        ddlTipoNomina.DataTextField = "descripcion";
        ddlTipoNomina.DataBind();
        ddlTipoNovedad.SelectedValue = "54";
        //  ddlTipoNovedad.Enabled = false;


        PagosDescuentosFijosService conceptoService = new PagosDescuentosFijosService();
        PagosDescuentosFijos concepto = new PagosDescuentosFijos();
        string filtro = ObtenerFiltro();
        ddlTipoNovedad.DataSource = conceptoService.ListarConceptosNomina(filtro, (Usuario)Session["usuario"]);
        ddlTipoNovedad.DataTextField = "descripcion";
        ddlTipoNovedad.DataValueField = "consecutivo";
        ddlTipoNovedad.DataBind();



        if (!_esNuevoRegistro)
        {
            LlenarNovedad();
        }
        else
        {
            ConsultarDatosPersona();
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;
        filtro += " and a.clase not in (3) and a.tipoconcepto not in(16)";


        return filtro;
    }


    void LlenarNovedad()
    {
        NovedadPrima novedadPrima = _novedadPrimaService.ConsultarNovedadPrima(_consecutivoNovedad.Value, Usuario);

        txtCodigoNovedad.Text = novedadPrima.consecutivo.ToString();
        txtIdentificacionn.Text = novedadPrima.identificacion;

        if (!string.IsNullOrWhiteSpace(novedadPrima.tipo_identificacion))
        {
            ddlTipoIdentificacion.SelectedValue = novedadPrima.tipo_identificacion;
        }

        txtCodigoEmpleado.Text = novedadPrima.codigoempleado.ToString();
        txtNombreCliente.Text = novedadPrima.nombre;
        txtAño.Text = novedadPrima.anio.ToString();
        txtValor.Text = novedadPrima.valor.ToString();

        if (novedadPrima.codigonomina > 0)
        {
            ddlTipoNomina.SelectedValue = novedadPrima.codigonomina.ToString();
        }

        if (novedadPrima.codigotiponovedad > 0)
        {
            ddlTipoNovedad.SelectedValue = novedadPrima.codigotiponovedad.ToString();

        }

        if (novedadPrima.semestre > 0)
        {
            ddlSemestre.SelectedValue = novedadPrima.semestre.ToString();
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
        ddlTipoNomina_SelectedIndexChanged(ddlTipoNomina, null);
    }


    #endregion


    #region Eventos Botonera


    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        // Borramos las sesiones para no mezclar cosas luego
        Session.Remove(_novedadPrimaService.CodigoPrograma + ".idNovedad");
        Session.Remove(_novedadPrimaService.CodigoPrograma + ".idEmpleado");

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
                NovedadPrima novedadPrima = ObtenerValores();

                if (_esNuevoRegistro)
                {
                    novedadPrima = _novedadPrimaService.CrearNovedadPrima(novedadPrima, Usuario);
                }
                else
                {
                    _novedadPrimaService.ModificarNovedadPrima(novedadPrima, Usuario);
                }

                if (novedadPrima.consecutivo > 0)
                {
                    mvDatos.SetActiveView(vFinal);

                    // Borramos las sesiones para no mezclar cosas luego
                    Session.Remove(_novedadPrimaService.CodigoPrograma + ".idNovedad");
                    Session.Remove(_novedadPrimaService.CodigoPrograma + ".idEmpleado");

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
        if (string.IsNullOrWhiteSpace(txtValor.Text) || string.IsNullOrWhiteSpace(txtAño.Text) 
            || string.IsNullOrWhiteSpace(ddlTipoNovedad.SelectedValue) || string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue))
        {
            VerError("Faltan datos por llenar!.");
            return false;
        }

        return true;
    }

    void ConsultarUltimaLiquidacion()
    {
        LiquidacionPrimaService _liquidacionServices = new LiquidacionPrimaService();

        long tiponomina;
        DateTimeHelper dateTimeHelper = new DateTimeHelper();
        if (ddlTipoNomina.SelectedItem.Text != "Seleccione un Item")
        {
            tiponomina = Convert.ToInt64(ddlTipoNomina.SelectedValue);

            DateTime numeroDias;
            LiquidacionPrima liquidacionPrima = _liquidacionServices.ConsultarUltpago(tiponomina, Usuario);
            Int64 ultimopago = (liquidacionPrima.fechapago.Year);
            Int64 fechaactual = DateTime.Now.Year;
            ddlSemestre.SelectedValue = liquidacionPrima.semestre.ToString();
            Int64 semestre = Convert.ToInt64(ddlSemestre.SelectedValue);
            if (ultimopago == fechaactual && semestre == 1)
            {
                ultimopago = (liquidacionPrima.fechapago.Year);
                txtAño.Text = ultimopago.ToString();

            }
            else
            {
                ultimopago = (liquidacionPrima.fechapago.Year + 1);
                txtAño.Text = ultimopago.ToString();
            }
            txtAño.Enabled = false;


            if (ddlSemestre.SelectedValue == "1")
            {
                ddlSemestre.SelectedValue = "2";
            }
            else
            {
                ddlSemestre.SelectedValue = "1";
            }


            ddlSemestre.Enabled = false;
        }

    }

    protected void ddlTipoNomina_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsultarUltimaLiquidacion();
    }


    #endregion


    #region Metodos Ayuda


    NovedadPrima ObtenerValores()
    {
        NovedadPrima novedadPrima = new NovedadPrima
        {
            codigoempleado = Convert.ToInt64(txtCodigoEmpleado.Text),
            valor = Convert.ToDecimal(txtValor.Text),
            consecutivo = _consecutivoNovedad.HasValue ? _consecutivoNovedad.Value : 0,
            codigonomina = !string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) ? Convert.ToInt64(ddlTipoNomina.SelectedValue) : default(long),
            codigotiponovedad = !string.IsNullOrWhiteSpace(ddlTipoNovedad.SelectedValue) ? Convert.ToInt64(ddlTipoNovedad.SelectedValue) : default(long),
            semestre = Convert.ToInt32(ddlSemestre.SelectedValue),
            anio = Convert.ToInt64(txtAño.Text)
        };

        return novedadPrima;
    }


    #endregion



   
}