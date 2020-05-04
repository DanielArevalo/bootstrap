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
      //  LlenarListasDesplegables(TipoLista.Contratacion, ddlTipoContrato);




        IngresoPersonalService ingresoservicio = new IngresoPersonalService();
        IngresoPersonal ingreso = new IngresoPersonal();
        ddlTipoContrato.DataSource = ingresoservicio.ListarContratacion(ingreso, (Usuario)Session["usuario"]);
        ddlTipoContrato.DataTextField = "descripcion";
        ddlTipoContrato.DataValueField = "consecutivo";
        ddlTipoContrato.DataBind();

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
        if (nominaEmpleado.direccion_oficina != null)
        {
            txtDireccionOficina.Text = nominaEmpleado.direccion_oficina.ToString();
        }
        else
            txtDireccionOficina.Text = "";

      /*  if (nominaEmpleado.codigotipocontrato.HasValue)
        //{
          // ddlTipoContrato.SelectedValue = "0";
        }
        */
        if (nominaEmpleado.codigooficina.HasValue)
        {
            ddlOficina.SelectedValue = nominaEmpleado.codigooficina.Value.ToString();
        }

        if (nominaEmpleado.tiponomina.HasValue)
        {
            checkBoxTipoNomina.SelectedValue = nominaEmpleado.tiponomina.Value.ToString();
        }

        if (nominaEmpleado.periodicidad_anticipos.HasValue)
        {
            checkBoxPeriodAnticipos.SelectedValue = nominaEmpleado.periodicidad_anticipos.Value.ToString();
        }

        if (nominaEmpleado.codigociudad.HasValue)
        {
            ddlCiudad.SelectedValue = nominaEmpleado.codigociudad.Value.ToString();
        }
        chkPermite_anticipos.Checked = nominaEmpleado.permite_anticipos == 1 ? true : false;
        if (chkPermite_anticipos.Checked == true)
        {
            txtPorcentaje.Visible = true;
            lblporcentaje.Visible = true;
            txtPorcentaje.Text = nominaEmpleado.porcentaje_anticipos.ToString();
        }
        else
        {
            txtPorcentaje.Visible = false;
            lblporcentaje.Visible = false;       
        }

        chkPermite_anticipossubsidio.Checked = nominaEmpleado.permite_anticipos_sub_trans == 1 ? true : false;
        if (chkPermite_anticipossubsidio.Checked == true)
        {
            txtPorcentajesubsidio.Visible = true;
            lblporcentajesubsidio.Visible = true;
            txtPorcentajesubsidio.Text = nominaEmpleado.porcentaje_anticipos_sub_trans.ToString();
        }
        else
        {
            txtPorcentajesubsidio.Visible = false;
            lblporcentajesubsidio.Visible = false;
          
        }

      

        txtFechaCorte.Text = nominaEmpleado.fecha_ult_liquidacion.Date.ToShortDateString();
       


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
            if (ValidarPagina())
                {
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


    protected void chkPermite_anticipos_CheckedChanged(object sender, EventArgs e)
    {
        if(chkPermite_anticipos.Checked==true)
        {
        lblporcentaje.Visible = true;
        txtPorcentaje.Visible = true;
        }
        if (chkPermite_anticipos.Checked == false)
        {
        lblporcentaje.Visible = false;
        txtPorcentaje.Visible = false;
        }

    }

    protected void chkPermite_anticipossubsidio_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPermite_anticipossubsidio.Checked == true)
        {
            lblporcentajesubsidio.Visible = true;
            txtPorcentajesubsidio.Visible = true;
        }
        if (chkPermite_anticipossubsidio.Checked == false)
        {
            lblporcentajesubsidio.Visible = false;
            txtPorcentajesubsidio.Visible = false;
        }

    }

    #endregion


    #region Metodos Ayuda


    NominaEmpleado ObtenerValores()
    {
        if (txtPorcentaje.Text == "") txtPorcentaje.Text = "0";
        if (txtPorcentajesubsidio.Text == "") txtPorcentajesubsidio.Text = "0";


        NominaEmpleado nominaEmpleado = new NominaEmpleado
        {
            codigociudad = Convert.ToInt64(ddlCiudad.SelectedValue),
            codigooficina = Convert.ToInt64(ddlOficina.SelectedValue),
            tiponomina = !string.IsNullOrWhiteSpace(checkBoxTipoNomina.SelectedValue) ? Convert.ToInt64(checkBoxTipoNomina.SelectedValue) : default(long?),
            consecutivo = _consecutivoNomina.HasValue ? Convert.ToInt32(_consecutivoNomina.Value) : 0,
            codigotipocontrato = Convert.ToInt64(ddlTipoContrato.SelectedValue),
            descripcion = txtNombreNomina.Text,
            permite_anticipos= chkPermite_anticipos.Checked == true ? 1 : 0,
            permite_anticipos_sub_trans= chkPermite_anticipossubsidio.Checked == true ? 1 : 0,
            porcentaje_anticipos = Convert.ToInt32(txtPorcentaje.Text),
            porcentaje_anticipos_sub_trans = Convert.ToInt32(txtPorcentajesubsidio.Text),
            periodicidad_anticipos = !string.IsNullOrWhiteSpace(checkBoxPeriodAnticipos.SelectedValue) ? Convert.ToInt64(checkBoxPeriodAnticipos.SelectedValue) : default(long?),

        };

        return nominaEmpleado;
    }

    bool ValidarPagina()
    {
        Int32 porcentaje = 100;
        Int32 porcentajedigitado = 0;
        Int32 porcentajesubsidio = 100;
        Int32 porcentajedigitadosubsidio = 0;


        if (txtPorcentaje.Text!=null && txtPorcentaje.Text!="" )
        {
          porcentajedigitado = Convert.ToInt32(txtPorcentaje.Text);

        }
        if (txtPorcentajesubsidio.Text != null && txtPorcentajesubsidio.Text != "")
        {
            porcentajedigitadosubsidio = Convert.ToInt32(txtPorcentajesubsidio.Text);

        }

        if (porcentajedigitado > porcentaje)
        {
            VerError("El porcentaje de Anticipos de Nómina no puede ser superior al 100%");
            return false;
        }


        if (porcentajedigitadosubsidio > porcentajesubsidio)
        {
            VerError("El porcentaje de Anticipos para subsidio de transporte  de Nómina no puede ser superior al 100%");
            return false;
        }

        if (chkPermite_anticipossubsidio.Checked == false)
        {
            txtPorcentajesubsidio.Text = "0";
        }

        if (chkPermite_anticipos.Checked == false)
        {
            txtPorcentaje.Text = "0";
        }
        return true;
    }



    #endregion




}