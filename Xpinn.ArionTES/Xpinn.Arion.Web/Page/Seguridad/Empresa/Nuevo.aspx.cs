using System;
using System.IO;
using System.Web.UI;
using Xpinn.Seguridad.Entities;
using Xpinn.Seguridad.Services;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    EmpresaService _empresaService = new EmpresaService();


    #region Metodos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_empresaService.CodigoPrograma, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btn_guardar_click;
            toolBar.eventoLimpiar += btnlimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_empresaService.CodigoPrograma, "Page_PreInit", ex);
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txtnomimg.Enabled = false;

        if (!IsPostBack)
        {
            InicializarPagina();
        }
    }

    public void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.Ciudades, ddlciudad);

        Empresa empresa = ConsultarEmpresa();

        if (empresa != null && empresa.cod_empresa > 0)
        {
            LlenarEmpresa(empresa);
        }
    }

    Empresa ConsultarEmpresa()
    {
        try
        {
            Empresa empresa = _empresaService.ConsultarEmpresa(Usuario);
            return empresa;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar la nomina, " + ex.Message);
            return null;
        }
    }

    void LlenarEmpresa(Empresa pempresa)
    {
        idObjeto = pempresa.cod_empresa.ToString();

        if (pempresa.ciudad.HasValue)
        {
            ddlciudad.SelectedValue = pempresa.ciudad.ToString();
        }

        if (pempresa.tipo.HasValue)
        {
            txtxtipo.Text = pempresa.tipo.ToString();
        }

        if (pempresa.maneja_sincronizacion.HasValue)
        {
            ddlManejo.SelectedValue = pempresa.maneja_sincronizacion.ToString();
        }

        if (pempresa.cod_persona.HasValue)
        {
            txtcodpersona.Text = pempresa.cod_persona.ToString();
        }

        if (pempresa.fecha_constitución.HasValue)
        {
            ctlFecha.Text = pempresa.fecha_constitución.Value.ToShortDateString();
        }

        txtxrevisor.Text = pempresa.revisor;
        txtxegreso.Text = pempresa.reporte_egreso;
        txtxcodigoUIAF.Text = pempresa.cod_uiaf;
        txttelefono.Text = pempresa.telefono;
        txttarjetacontador.Text = pempresa.tarjeta_contador;
        txtsigla.Text = pempresa.sigla;
        txtrepresentante.Text = pempresa.representante_legal;
        txtrazonsocial.Text = pempresa.nombre;
        txtpassword.Text = pempresa.clavecorreo;

        txtingreso.Text = pempresa.reporte_ingreso;
        txtdir.Text = pempresa.direccion;
        txtcorreo.Text = pempresa.e_mail;
        txtcontador.Text = pempresa.contador;
        txtnit.Text = pempresa.nit;
        ddlTipoEmp.SelectedValue = pempresa.tipo_empresa.ToString();

        if (pempresa.logoempresa_bytes != null)
        {
            string base64 = Convert.ToBase64String(pempresa.logoempresa_bytes);

            imgFoto.ImageUrl = @"data:image/jpeg;base64," + base64;
            hiddenFieldImageData.Value = base64;
        }
        if(pempresa.desc_regimen!=null)
        txtdescripcionRegimen.Text=pempresa.desc_regimen.ToString();
        if (pempresa.resol_facturacion != null)
            txtResolucionfacturacion.Text = pempresa.resol_facturacion.ToString();
    }


    #endregion


    #region Eventos Botones


    void btn_guardar_click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarCampos())
        {
            GuardarDatos();
        }
    }

    protected void btnlimpiar_Click(object sender, EventArgs e)
    {

        txtxtipo.Text = string.Empty;
        txtnit.Text = string.Empty;
        txtpassword.Text = string.Empty;
        txtcodpersona.Text = string.Empty;
        txtxcodigoUIAF.Text = string.Empty;
        txtcontador.Text = string.Empty;
        txtdir.Text = string.Empty;
        txtcorreo.Text = string.Empty;
        txtnit.Text = string.Empty;
    }


    #endregion


    #region Metodos Ayuda


    bool ValidarCampos()
    {
        Page.Validate();
        if (string.IsNullOrWhiteSpace(txtnit.Text) ||
            string.IsNullOrWhiteSpace(txtdir.Text) ||
            string.IsNullOrWhiteSpace(txtrazonsocial.Text) ||
            string.IsNullOrWhiteSpace(txtrepresentante.Text) ||
            string.IsNullOrWhiteSpace(txtsigla.Text))
        {
            VerError("Faltan Campos por llenar");
            return false;
        }
        return true;
    }

    void GuardarDatos()
    {
        try
        {
            Empresa entities = new Empresa();

            if (!string.IsNullOrWhiteSpace(idObjeto))
                entities.cod_empresa = Convert.ToInt32(idObjeto);

            if (!string.IsNullOrWhiteSpace(txtrazonsocial.Text))
                entities.nombre = txtrazonsocial.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtnit.Text))
                entities.nit = txtnit.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txttelefono.Text))
                entities.telefono = txttelefono.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtsigla.Text))
                entities.sigla = txtsigla.Text.Trim();

            if (!string.IsNullOrWhiteSpace(ctlFecha.Text))
                entities.fecha_constitución = Convert.ToDateTime(ctlFecha.Text.Trim());

            if (!string.IsNullOrWhiteSpace(ddlciudad.SelectedValue))
                entities.ciudad = Convert.ToInt32(ddlciudad.SelectedValue);

            if (!string.IsNullOrWhiteSpace(txtxtipo.Text))
                entities.tipo = Convert.ToInt32(txtxtipo.Text.Trim());

            if (!string.IsNullOrWhiteSpace(txtdir.Text))
                entities.direccion = txtdir.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtcorreo.Text))
                entities.e_mail = txtcorreo.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtpassword.Text))
                entities.clavecorreo = txtpassword.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtrepresentante.Text))
                entities.representante_legal = txtrepresentante.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtcontador.Text))
                entities.contador = txtcontador.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txttarjetacontador.Text))
                entities.tarjeta_contador = txttarjetacontador.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtxegreso.Text))
                entities.reporte_egreso = txtxegreso.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtxrevisor.Text))
                entities.revisor = txtxrevisor.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtcodpersona.Text))
                entities.cod_persona = Convert.ToInt32(txtcodpersona.Text.Trim());

            if (!string.IsNullOrWhiteSpace(txtxcodigoUIAF.Text))
                entities.cod_uiaf = txtxcodigoUIAF.Text.Trim();

            if (!string.IsNullOrWhiteSpace(ddlManejo.SelectedValue))
                entities.maneja_sincronizacion = Convert.ToInt32(ddlManejo.SelectedValue.Trim());

            if (!string.IsNullOrWhiteSpace(txtingreso.Text))
                entities.reporte_ingreso = txtingreso.Text.Trim();

            if (!string.IsNullOrWhiteSpace(ddlTipoEmp.SelectedValue))
                entities.tipo_empresa = Convert.ToInt32(ddlTipoEmp.SelectedValue);

            if (avatarUpload.PostedFile != null && avatarUpload.PostedFile.ContentLength > 0)
            {
                using (Stream streamImagen = avatarUpload.PostedFile.InputStream)
                {
                    StreamsHelper streamHelper = new StreamsHelper();
                    entities.logoempresa_bytes = streamHelper.LeerTodosLosBytesDeUnStream(streamImagen);
                }
            }
            //else if (!string.IsNullOrWhiteSpace(hiddenFieldImageData.Value))
            //{
            //    try
            //    {
            //        entities.logoempresa_bytes = Convert.FromBase64String(hiddenFieldImageData.Value.Replace(@"data:image/jpeg;base64,", "").Replace(@"data:image/png;base64,", ""));
            //    }
            //    catch (Exception)
            //    {

            //    }
            //}

          
            if (!string.IsNullOrWhiteSpace(txtdescripcionRegimen.Text))
                entities.desc_regimen = (txtdescripcionRegimen.Text);

            if (!string.IsNullOrWhiteSpace(txtResolucionfacturacion.Text))
                entities.resol_facturacion = (txtResolucionfacturacion.Text);


            if (idObjeto == "" && idObjeto == "0")
            {
                Empresa pentidad = _empresaService.CrearEmpresa(entities, Usuario);
                entities.cod_empresa = pentidad.cod_empresa;
            }
            else
            {
                Empresa pentidad = _empresaService.ModificarEmpresa(entities, Usuario);
            }

            if (entities.cod_empresa != 0)
            {
                Site toolbar = (Site)Master;
                toolbar.MostrarGuardar(false);
                toolbar.MostrarLimpiar(false);

                mvPrincipal.SetActiveView(vFinal);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar el registro, " + ex.Message);
        }
    }


    #endregion

}