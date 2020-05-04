using System;
using System.Web.UI;
using Xpinn.Caja.Entities;
using Xpinn.Caja.Services;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Detalle : GlobalWeb
{
    StringHelper _stringHelper = new StringHelper();
    NominaEmpleadoService _nominaEmpleadoService = new NominaEmpleadoService();
    long? _consecutivoNomina;
    bool _esNuevoRegistro;


    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_nominaEmpleadoService.CodigoPrograma, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_nominaEmpleadoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Si aqui viene algo significa que voy a crear uno
            _consecutivoNomina = Session[_nominaEmpleadoService.CodigoPrograma + ".idNomina"] as long?;

            _esNuevoRegistro = !_consecutivoNomina.HasValue;

            if (!IsPostBack)
            {
                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_nominaEmpleadoService.CodigoPrograma, "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.Oficinas, ddlOficina);
        LlenarListasDesplegables(TipoLista.Ciudades, ddlCiudad);
        LlenarListasDesplegables(TipoLista.TipoContrato, ddlTipoContrato);

        if (!_esNuevoRegistro)
        {
            LlenarNomina();
        }
    }

    void LlenarNomina()
    {
        NominaEmpleado nominaEmpleado = _nominaEmpleadoService.ConsultarNominaEmpleado(_consecutivoNomina.Value, Usuario);

        txtCodigoNomina.Text = nominaEmpleado.consecutivo.ToString();
        txtNombreNomina.Text = nominaEmpleado.descripcion;
        txtDireccionOficina.Text = nominaEmpleado.direccion_oficina.ToString();

        if (nominaEmpleado.codigotipocontrato.HasValue)
        {
            ddlTipoContrato.SelectedValue = nominaEmpleado.codigotipocontrato.Value.ToString();
        }

        if (nominaEmpleado.codigooficina.HasValue)
        {
            ddlOficina.SelectedValue = nominaEmpleado.codigooficina.Value.ToString();
        }

        if (nominaEmpleado.tiponomina.HasValue)
        {
            checkBoxTipoNomina.SelectedValue = nominaEmpleado.tiponomina.Value.ToString();
        }

        if (nominaEmpleado.codigociudad.HasValue)
        {
            ddlCiudad.SelectedValue = nominaEmpleado.codigociudad.Value.ToString();
        }
    }


    #endregion


    #region Eventos Botonera


    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        // Borramos las sesiones para no mezclar cosas luego
        Session.Remove(_nominaEmpleadoService.CodigoPrograma + ".idNomina");

        Navegar(Pagina.Lista);
    }

    void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            NominaEmpleado nominaEmpleado = ObtenerValores();

            if (_esNuevoRegistro)
            {
                nominaEmpleado = _nominaEmpleadoService.CrearNominaEmpleado(nominaEmpleado, Usuario);
            }
            else
            {
                _nominaEmpleadoService.ModificarNominaEmpleado(nominaEmpleado, Usuario);
            }

            if (nominaEmpleado.consecutivo > 0)
            {
                mvDatos.SetActiveView(vFinal);

                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_nominaEmpleadoService.CodigoPrograma + ".idNomina");
                Session.Remove(_nominaEmpleadoService.CodigoPrograma + ".idEmpleado");

                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar el registro, " + ex.Message);
        }
    }


    #endregion


    #region Eventos Varios


    protected void ddlOficina_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(ddlOficina.SelectedValue))
        {
            long codigoOficina = Convert.ToInt64(ddlOficina.SelectedValue);

            OficinaService oficinaService = new OficinaService();

            Oficina oficina = oficinaService.ConsultarDireccionYCiudadDeUnaOficina(codigoOficina, Usuario);

            if (oficina.cod_ciudad > 0)
            {
                ddlCiudad.SelectedValue = oficina.cod_ciudad.ToString();
            }

            txtDireccionOficina.Text = oficina.direccion;
        }
    }


    #endregion


    #region Metodos Ayuda


    NominaEmpleado ObtenerValores()
    {
        NominaEmpleado nominaEmpleado = new NominaEmpleado
        {
            codigociudad = Convert.ToInt64(ddlCiudad.SelectedValue),
            codigooficina = Convert.ToInt64(ddlOficina.SelectedValue),
            tiponomina = !string.IsNullOrWhiteSpace(checkBoxTipoNomina.SelectedValue) ? Convert.ToInt64(checkBoxTipoNomina.SelectedValue) : default(long?),
            consecutivo = _consecutivoNomina.HasValue ? Convert.ToInt32(_consecutivoNomina.Value) : 0,
            codigotipocontrato = Convert.ToInt64(ddlTipoContrato.SelectedValue),
            descripcion = txtNombreNomina.Text
        };

        return nominaEmpleado;
    }


    #endregion


}