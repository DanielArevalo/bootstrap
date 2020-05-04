using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;

public partial class Nuevo : GlobalWeb
{
    DirectivoService _directivoService = new DirectivoService();
    string _codDirectivo;


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[_directivoService.CodigoPrograma + ".id"] == null)
            {
                VisualizarOpciones(_directivoService.CodigoPrograma, "A");
            }
            else
            {
                VisualizarOpciones(_directivoService.CodigoPrograma, "D");
            }
            
            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += ctlMensaje_Click;
            ctlBusquedaPersonas.eventotxtIdentificacion_TextChanged += (s, evt) => ConsultarPersonaNoSeaDirectivo();
            toolBar.eventoCancelar += (s, evt) => Navegar(Pagina.Lista);
            toolBar.eventoRegresar += (s, evt) =>
            {
                Session.Remove(_directivoService.CodigoPrograma + ".id");
                Navegar(Pagina.Lista);
            };

            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_directivoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        VerError("");

        if (Session[_directivoService.CodigoPrograma + ".id"] != null)
        {
            _codDirectivo = Session[_directivoService.CodigoPrograma + ".id"].ToString();
        }

        if (!IsPostBack)
        {
            InicializarPagina();

            if (!string.IsNullOrWhiteSpace(_codDirectivo))
            {
                InicializarEditarRegistro();
            }
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.TipoDirectivo, ddlTipoDirectivo);
    }

    void InicializarEditarRegistro()
    {
        Directivo directivo = ConsultarDirectivo();

        if (directivo == null) return;

        LlenarDirectivo(directivo);

        PrepararFormularioParaModificar(directivo);
    }

    Directivo ConsultarDirectivo()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_codDirectivo)) return null;

            Directivo directivo = _directivoService.ConsultarDirectivo(Convert.ToInt64(_codDirectivo), Usuario);

            return directivo;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar el directivo, " + ex.Message);
            return null;
        }
    }

    void LlenarDirectivo(Directivo directivo)
    {
        txtCodCliente.Text = directivo.cod_persona.ToString();
        txtNombreCliente.Text = directivo.nombre;
        txtIdentificacion.Text = directivo.identificacion;
        ddlTipoDirectivo.SelectedValue = directivo.tipo_directivo.HasValue ? directivo.tipo_directivo.Value.ToString() : "1";
        ddlEstado.SelectedValue = directivo.estado;
        ddlCalidad.SelectedValue = directivo.calidad.ToString();
        txtVigenciaInicial.Texto = directivo.vigencia_inicio.HasValue ? directivo.vigencia_inicio.Value.ToShortDateString() : string.Empty;
        txtVigenciaFinal.Texto = directivo.vigencia_final.HasValue ? directivo.vigencia_final.Value.ToShortDateString() : string.Empty;
        txtFechaNombramiento.Texto = directivo.fecha_nombramiento.HasValue ? directivo.fecha_nombramiento.Value.ToShortDateString() : string.Empty;
        txtFechaPosesion.Texto = directivo.fecha_posesion.HasValue ? directivo.fecha_posesion.Value.ToShortDateString() : string.Empty;
        txtEmpresa.Text = directivo.empresa;
        chkPariente.Checked = directivo.parientes == "1";
        chkVinculos.Checked = directivo.vinculos_organiza == "1";
        txtEmail.Text = directivo.email;
        txtNumeroRadi.Text = directivo.num_radi_pose;
        txtTarjetaProfesional.Text = directivo.tarj_rev_fiscar;
    }

    void PrepararFormularioParaModificar(Directivo directivo)
    {
        btnConsultaPersonas.Enabled = false;
    }


    #endregion


    #region Eventos Intermedios


    protected void ddlTipoDirectivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoDirectivo.SelectedItem.Text.ToUpperInvariant() == "Revisor Fiscal".ToUpperInvariant())
        {
            txtTarjetaProfesional.Visible = true;
            lblTarjetaProfesional.Visible = true;
            pnlEmpresa.Visible = true;
        }
        else
        {
            txtTarjetaProfesional.Visible = false;
            lblTarjetaProfesional.Visible = false;
            pnlEmpresa.Visible = false;
        }
    }

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
            Directivo directivo = ObtenerEntidadGuardar();

            if (string.IsNullOrWhiteSpace(_codDirectivo))
            {
                directivo = _directivoService.CrearDirectivo(directivo, Usuario);
            }
            else
            {
                directivo = _directivoService.ModificarDirectivo(directivo, Usuario);
            }

            //if (directivo.consecutivo != 0)
            //{
                mvFinal.ActiveViewIndex = 1;

                Site toolBar = (Site)Master;
                toolBar.MostrarRegresar(true);
                toolBar.MostrarCancelar(false);
                toolBar.MostrarGuardar(false);

                Session.Remove(_directivoService.CodigoPrograma + ".id");
            //}
        }
        catch (Exception ex)
        {
            VerError("Error al guardar el registro, " + ex.Message);
        }
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodCliente", "txtIdentificacion", "txtNombreCliente");
    }

    protected void btnConsultaEmpresa_Click(object sender, EventArgs e)
    {
        ctlListarEmpresa.Motrar(true, "txtCodEmpresa", "txtEmpresa");
    }

    #endregion


    #region Métodos de Ayuda


    void ConsultarPersonaNoSeaDirectivo()
    {
        try
        {
            VerError("");
            bool existe = _directivoService.ValidarPersonaNoSeaDirectivoYa(txtIdentificacion.Text, Usuario);

            Site toolBar = (Site)Master;
            if (existe)
            {
                VerError("La persona ya se encuentra registrada como directivo!.");
                toolBar.MostrarGuardar(false);
            }
            else
            {
                toolBar.MostrarGuardar(true);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al validar la identificación, " + ex.Message);
        }
    }

    bool ValidarCampos()
    {
        Page.Validate();
        if (!Page.IsValid || string.IsNullOrWhiteSpace(txtIdentificacion.Text) || string.IsNullOrWhiteSpace(txtVigenciaInicial.Texto) || string.IsNullOrWhiteSpace(txtVigenciaFinal.Texto)
            || string.IsNullOrWhiteSpace(txtFechaNombramiento.Texto) || string.IsNullOrWhiteSpace(txtFechaPosesion.Texto) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtNumeroRadi.Text))
        {
            VerError("Faltan campos por validar!.");
            return false;
        }

        return true;
    }

    Directivo ObtenerEntidadGuardar()
    {
        Directivo directivo = new Directivo();

        directivo.consecutivo = !string.IsNullOrWhiteSpace(_codDirectivo) ? Convert.ToInt64(_codDirectivo) : 0;
        directivo.cod_persona = Convert.ToInt64(txtCodCliente.Text);
        directivo.identificacion = txtIdentificacion.Text;
        directivo.tipo_directivo = Convert.ToInt32(ddlTipoDirectivo.SelectedValue);
        directivo.estado = ddlEstado.SelectedValue;
        directivo.calidad = Convert.ToInt32(ddlCalidad.SelectedValue);
        directivo.vigencia_inicio = txtVigenciaInicial.TieneDatos ? txtVigenciaInicial.ToDateTime : default(DateTime?);
        directivo.vigencia_final = txtVigenciaFinal.TieneDatos ? txtVigenciaFinal.ToDateTime : default(DateTime?);
        directivo.fecha_nombramiento = txtFechaNombramiento.TieneDatos ? txtFechaNombramiento.ToDateTime : default(DateTime?);
        directivo.fecha_posesion = txtFechaPosesion.TieneDatos ? txtFechaPosesion.ToDateTime : default(DateTime?);
        directivo.parientes = chkPariente.Checked ? "1" : "0";
        directivo.vinculos_organiza = chkVinculos.Checked ? "1" : "0";
        directivo.empresa = txtEmpresa.Text;
        directivo.email = txtEmail.Text;
        directivo.num_radi_pose = txtNumeroRadi.Text;
        directivo.tarj_rev_fiscar = txtTarjetaProfesional.Text;

        return directivo;
    }


    #endregion

}