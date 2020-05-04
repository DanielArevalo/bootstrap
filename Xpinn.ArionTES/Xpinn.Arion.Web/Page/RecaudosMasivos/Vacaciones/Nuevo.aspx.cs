using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    EmpresaNovedadService _empresaService = new EmpresaNovedadService();
    Usuario _usuario;
    string _codVacaciones;


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_empresaService.CodigoProgramaVacaciones, "A");

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += ctlMensaje_Click;
            toolBar.eventoCancelar += (s, evt) => Navegar(Pagina.Lista);
            toolBar.eventoRegresar += (s, evt) =>
            {
                Session.Remove(_empresaService.CodigoProgramaVacaciones + ".id");
                Navegar(Pagina.Lista);
            };

            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_empresaService.CodigoProgramaVacaciones, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        VerError("");
        _usuario = (Usuario)Session["usuario"];

        if (Session[_empresaService.CodigoProgramaVacaciones + ".id"] != null)
        {
            _codVacaciones = Session[_empresaService.CodigoProgramaVacaciones + ".id"].ToString();
        }

        if (!IsPostBack)
        {
            InicializarPagina();

            if (!string.IsNullOrWhiteSpace(_codVacaciones))
            {
                InicializarEditarRegistro();
            }
        }
    }

    void InicializarPagina()
    {
        LlenarListas();
    }

    void InicializarEditarRegistro()
    {
        EmpresaNovedad novedad = ConsultarNovedad();

        if (novedad == null) return;

        LlenarNovedad(novedad);

        PrepararFormularioParaModificar(novedad);     
    }

    EmpresaNovedad ConsultarNovedad()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_codVacaciones)) return null;

            EmpresaNovedad novedad = _empresaService.ConsultarVacaciones(_codVacaciones, _usuario);

            return novedad;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar los datos de la novedad, " + ex.Message);
            return null;
        }
    }

    void LlenarListas()
    {
        LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoIdentificacion);
        LlenarListasDesplegables(TipoLista.Empresa, ddlEntidad);
    }

    void LlenarNovedad(EmpresaNovedad novedad)
    {
        txtCodCliente.Text = novedad.cod_persona.ToString();
        txtNombreCliente.Text = novedad.nombres;
        txtIdentificacion.Text = novedad.identificacion;
        ddlTipoIdentificacion.SelectedValue = novedad.tipo_identificacion.ToString();
        txtNumeroCuotas.Text = novedad.numero_cuotas.ToString();
        txtFecha.Text = novedad.fecha_novedad.Value.ToShortDateString();
        ddlEntidad.SelectedValue = novedad.codigo_pagaduria.ToString();
        ddlTipoCalculo.SelectedValue = novedad.tipo_calculo.ToString();
        if (novedad.fecha_inicial != null && novedad.fecha_inicial != DateTime.MinValue)
            txtFecIni.Text = Convert.ToDateTime(novedad.fecha_inicial).ToString(gFormatoFecha);
        if (novedad.fecha_final != null && novedad.fecha_final != DateTime.MinValue)
            txtFecFin.Text = Convert.ToDateTime(novedad.fecha_final).ToString(gFormatoFecha);
    }

    void PrepararFormularioParaModificar(EmpresaNovedad novedad)
    {
        btnConsultaPersonas.Enabled = false;
    }


    #endregion


    #region Eventos Intermedios


    void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (ValidarCampos())
        {
            ctlMensaje.MostrarMensaje("Desea seguir con la operación?");
        }
    }

    void ctlMensaje_Click(object sender, EventArgs e)
    {
        try
        {
            Vacaciones vacaciones = ObtenerEntidadGuardar();

            if (string.IsNullOrWhiteSpace(_codVacaciones))
            {
                vacaciones = _empresaService.InsertarVacaciones(vacaciones, _usuario);
            }
            else
            {
                vacaciones = _empresaService.ModificarVacaciones(vacaciones, _usuario);
            }

            if (vacaciones.consecutivo != 0)
            {
                mvFinal.ActiveViewIndex = 1;

                Site toolBar = (Site)Master;
                toolBar.MostrarRegresar(true);
                toolBar.MostrarCancelar(false);
                toolBar.MostrarGuardar(false);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar el registro, " + ex.Message);
        }
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodCliente", "txtIdentificacion", "ddlTipoIdentificacion", "txtNombreCliente");
    }


    #endregion


    #region Métodos de Ayuda


    bool ValidarCampos()
    {
        if (string.IsNullOrWhiteSpace(txtIdentificacion.Text) || string.IsNullOrWhiteSpace(txtFecha.Text))
        {
            VerError("Faltan campos por validar!.");
            return false;
        }
        else if (string.IsNullOrWhiteSpace(txtNumeroCuotas.Text) && txtNumeroCuotas.Text == "0")
        {
            VerError("El valor del número de cuotas no puede ser cero!.");
            return false;
        }
        else if (string.IsNullOrWhiteSpace(ddlEntidad.SelectedValue))
        {
            VerError("La pagaduría no puede estar vacia!.");
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtFecIni.Text))
        {
            VerError("No se selecciono la fecha inicial!.");
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtFecFin.Text))
        {
            VerError("No se selecciono la fecha final!.");
            return false;
        }
        if (Convert.ToDateTime(txtFecIni.Text) > Convert.ToDateTime(txtFecFin.Text))
        {
            VerError("Error al ingresar el rango de fechas!.");
            return false;
        }

        //Validar que el periodo no se encuentre ya generado 
        if (ddlTipoCalculo.SelectedValue == "0")
        {
            EmpresaNovedad pNovedad = new EmpresaNovedad();
            pNovedad.periodo_corte = Convert.ToDateTime(txtFecha.Text);
            pNovedad.cod_empresa = Convert.ToInt64(ddlEntidad.SelectedValue);

            pNovedad = _empresaService.ListarRecaudo(pNovedad, "", (Usuario)Session["usuario"]).FirstOrDefault();
            if (pNovedad != null)
            {
                if (pNovedad.estado != "3" && pNovedad.estado != null && pNovedad.numero_novedad != 0)
                {
                    VerError("No puede registar datos de vacaciones para la fecha, ya se realizó el proceso de generación de novedades");
                    return false;
                }
            }
        }

        //Validar que el periodo no se encuentre ya aplicado 
        if (ddlTipoCalculo.SelectedValue == "1")
        {
            RecaudosMasivos pRecaudo = new RecaudosMasivos();
            RecaudosMasivosService recaudoService = new RecaudosMasivosService();
            pRecaudo.periodo_corte = Convert.ToDateTime(txtFecha.Text);
            pRecaudo.cod_empresa = Convert.ToInt64(ddlEntidad.SelectedValue);

            pRecaudo = recaudoService.ConsultarRecaudo(pRecaudo, (Usuario)Session["usuario"]);
            if (pRecaudo.estado == "2" )
            {
                VerError("No puede registar datos de vacaciones para la fecha, ya se realizó la aplicación del recaudo");
                return false;
            }
        }
        return true;
    }


    Vacaciones ObtenerEntidadGuardar()
    {
        Vacaciones vacaciones = new Vacaciones();
        vacaciones.consecutivo = !string.IsNullOrWhiteSpace(_codVacaciones) ? Convert.ToInt64(_codVacaciones) : 0;
        vacaciones.cod_persona = Convert.ToInt64(txtCodCliente.Text);
        vacaciones.numero_cuotas = !string.IsNullOrWhiteSpace(txtNumeroCuotas.Text) ? Convert.ToInt32(txtNumeroCuotas.Text) : 0;
        vacaciones.fecha_grabacion = DateTime.Today;
        vacaciones.fecha_novedad = Convert.ToDateTime(txtFecha.Text);
        vacaciones.identificacion = txtIdentificacion.Text;
        vacaciones.codigo_pagaduria = Convert.ToInt64(ddlEntidad.SelectedValue);
        vacaciones.tipo_calculo = Convert.ToInt32(ddlTipoCalculo.SelectedValue);
        vacaciones.fecha_inicial = Convert.ToDateTime(txtFecIni.Text);
        vacaciones.fecha_final = Convert.ToDateTime(txtFecFin.Text);
        return vacaciones;
    }


    #endregion
    
}