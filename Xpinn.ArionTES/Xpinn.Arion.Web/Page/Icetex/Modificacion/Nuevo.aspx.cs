using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Icetex.Services;
using Xpinn.Icetex.Entities;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Comun.Entities;
using System.Configuration;

public partial class Nuevo : GlobalWeb
{
    ConvocatoriaServices ConvocatoriaService = new ConvocatoriaServices();
    AprobacionServices ModificacionIctx = new AprobacionServices();
    PoblarListas poblar = new PoblarListas();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[ModificacionIctx.CodigoProgramaModifi + ".id"] != null)
                VisualizarOpciones(ModificacionIctx.CodigoProgramaModifi, "E");
            else
                VisualizarOpciones(ModificacionIctx.CodigoProgramaModifi, "N");
            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModificacionIctx.CodigoProgramaModifi, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                CargarDropDown();
                panelGeneral.Visible = true;
                panelFinal.Visible = false;
                if (Session[ModificacionIctx.CodigoProgramaModifi + ".id"] != null)
                {
                    idObjeto = Session[ModificacionIctx.CodigoProgramaModifi + ".id"].ToString();
                    Session.Remove(ModificacionIctx.CodigoProgramaModifi + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModificacionIctx.CodigoProgramaModifi, "Page_Load", ex);
        }
    }

    protected void CargarDropDown()
    {
        ddlTipoBeneficiario.Items.Insert(0, new ListItem("Asociado", "0"));
        ddlTipoBeneficiario.Items.Insert(1, new ListItem("Hijo del Asociado", "1"));
        ddlTipoBeneficiario.Items.Insert(2, new ListItem("Nieto del Asociado", "2"));
        ddlTipoBeneficiario.Items.Insert(3, new ListItem("Empleado", "3"));

        poblar.PoblarListaDesplegable("tipoidentificacion", ddlTipoDoc, Usuario);
        poblar.PoblarListaDesplegable("universidad", "", "","2",ddlUniversidad, Usuario);
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            CreditoIcetex pEntidad = new CreditoIcetex();
            string pFiltro = obtFiltro();
            if (Session[ModificacionIctx.CodigoProgramaModifi + ".cod_convocatoria"] != null)
            {
                lblConvocatoria.Text = Session[ModificacionIctx.CodigoProgramaModifi + ".cod_convocatoria"].ToString();
            }
            pEntidad = ConvocatoriaService.ConsultarCreditoIcetex(pFiltro, Usuario);
            if (pEntidad != null)
            {
                if (pEntidad.numero_credito > 0)
                {
                    //Consultando datos de la persona
                    Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
                    Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
                    pPersona = PersonaService.ConsultaDatosPersona(pEntidad.cod_persona, Usuario);
                    if (pPersona.cod_persona > 0)
                    {
                        lblCod_Persona.Text = pPersona.cod_persona.ToString();
                        lblIdentificacion.Text = pPersona.identificacion != null ? pPersona.identificacion : "";
                        string Nombre = "";
                        if (pPersona.primer_nombre != null)
                            Nombre += pPersona.primer_nombre;
                        if (pPersona.segundo_nombre != null)
                            Nombre += " " + pPersona.segundo_nombre;
                        if (pPersona.primer_apellido != null)
                            Nombre += " " + pPersona.primer_apellido;
                        if (pPersona.segundo_apellido != null)
                            Nombre += " " + pPersona.segundo_apellido;
                        lblNombre.Text = Nombre;
                    }

                    if (pEntidad.fecha_solicitud != DateTime.MinValue)
                        txtFecha.Text = pEntidad.fecha_solicitud.ToString(gFormatoFecha);
                    if (pEntidad.tipo_beneficiario != null)
                        ddlTipoBeneficiario.SelectedValue = pEntidad.tipo_beneficiario;
                    if (pEntidad.codtipoidentificacion != null)
                        ddlTipoDoc.SelectedValue = pEntidad.codtipoidentificacion.ToString();
                    if (pEntidad.identificacion != null)
                        txtNroDoc.Text = pEntidad.identificacion;
                    if (pEntidad.primer_apellido != null)
                        txtApellido1.Text = pEntidad.primer_apellido;
                    if (pEntidad.segundo_apellido != null)
                        txtApellido2.Text = pEntidad.segundo_apellido;
                    if (pEntidad.primer_nombre != null)
                        txtNombre1.Text = pEntidad.primer_nombre;
                    if (pEntidad.segundo_nombre != null)
                        txtNombre2.Text = pEntidad.segundo_nombre;
                    if (pEntidad.direccion != null)
                        txtDireccion.Text = HttpUtility.HtmlDecode(pEntidad.direccion);
                    if (pEntidad.telefono != null)
                        txtTelefono.Text = pEntidad.telefono;
                    if (pEntidad.email != null)
                        txtEmail.Text = pEntidad.email;
                    if (pEntidad.estrato != null)
                        txtEstrato.Text = pEntidad.estrato.ToString();
                    if (pEntidad.cod_universidad != null)
                    {
                        ddlUniversidad.SelectedValue = pEntidad.cod_universidad;
                        ddlUniversidad_SelectedIndexChanged(ddlUniversidad, null);
                    }
                    if (pEntidad.cod_programa != null)
                        ddlPrograma.SelectedValue = pEntidad.cod_programa;
                    if (pEntidad.tipo_programa != 0)
                        ddlTipoPrograma.SelectedValue = pEntidad.tipo_programa.ToString();
                    if (pEntidad.valor > 0)
                        txtValorPrograma.Text = pEntidad.valor.ToString();
                    if (pEntidad.periodos != null)
                        ddlPeriodos.SelectedValue = pEntidad.periodos.ToString();
                    if (pEntidad.estado != null)
                    {
                        lblCodEstado.Text = pEntidad.estado;
                        switch (pEntidad.estado)
                        {
                            case "S":
                                lblEstado.Text = "Pre-Inscrito";
                                break;
                            case "A":
                                lblEstado.Text = "Aprobado";
                                break;
                            case "Z":
                                lblEstado.Text = "Aplazado";
                                break;
                            case "N":
                                lblEstado.Text = "Negado";
                                break;
                            case "I":
                                lblEstado.Text = "Inscrito";
                                break;
                            case "T":
                                lblEstado.Text = "Terminado";
                                break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModificacionIctx.CodigoProgramaModifi, "ObtenerDatos", ex);
        }
    }

    private string obtFiltro()
    {
        string pFiltro = string.Empty;
        if (idObjeto != null && idObjeto != "")
            pFiltro = " WHERE c.numero_credito = " + idObjeto;

        return pFiltro;
    }

    protected void ddlUniversidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDropDownPrograma();
    }

    private void CargarDropDownPrograma()
    {
        ddlPrograma.Items.Clear();
        if (ddlUniversidad.SelectedIndex > 0)
            poblar.PoblarListaDesplegable("PROGRAMA", "cod_programa, descripcion", " Cod_universidad = " + ddlUniversidad.SelectedValue, "2", ddlPrograma, Usuario);
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Navegar(Pagina.Lista);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModificacionIctx.CodigoProgramaModifi, "btnCancelar_Click", ex);
        }
    }


    private Boolean validarDatos()
    {
        if (ddlTipoDoc.SelectedIndex == 0)
        {
            VerError("Seleccione el tipo de Documento del beneficiario");
            return false;
        }
        if (string.IsNullOrEmpty(txtNroDoc.Text.Trim()))
        {
            VerError("Ingrese la identificación del beneficiario");
            return false;
        }
        if (string.IsNullOrEmpty(txtApellido1.Text.Trim()))
        {
            VerError("Ingrese el primer apellido del beneficiario");
            return false;
        }
        if (string.IsNullOrEmpty(txtNombre1.Text.Trim()))
        {
            VerError("Ingrese el primer nombre del beneficiario");
            return false;
        }
        if (string.IsNullOrEmpty(txtDireccion.Text.Trim()))
        {
            VerError("Ingrese la dirección del beneficiario");
            return false;
        }
        if (string.IsNullOrEmpty(txtTelefono.Text.Trim()))
        {
            VerError("Ingrese el número de teléfono del beneficiario");
            return false;
        }
        if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
        {
            VerError("Ingrese la dirección de correo electrónico del beneficiario");
            return false;
        }
        else
        {
            Validadores ExpRegular = new Validadores();
            if (!ExpRegular.IsValidEmail(txtEmail.Text))
            {
                VerError("La dirección del correo electrónico no tiene el formato correcto.");
                return false;
            }
        }
        if (string.IsNullOrEmpty(txtEstrato.Text.Trim()))
        {
            VerError("Ingrese el estrato al que pertenece el beneficiario.");
            return false;
        }
        else
        {
            int pEstrato = Convert.ToInt32(txtEstrato.Text);
            if (pEstrato > 3)
            {
                VerError("El estrato ingresado no está dentro del rango permitido [1-3].");
                return false;
            }
        }

        if (ddlUniversidad.SelectedIndex == 0)
        {
            VerError("Seleccione la Universidad o Institución del beneficiario");
            return false;
        }
        if (ddlPrograma.SelectedIndex == 0)
        {
            VerError("Seleccione el programa del beneficiario");
            return false;
        }
        if (txtValorPrograma.Text.Trim() == "" || txtValorPrograma.Text == "0")
        {
            VerError("Ingrese el valor a solicitar.");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (!validarDatos())
                return;
            ctlMensaje.MostrarMensaje("Desea realizar la grabación?");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModificacionIctx.CodigoProgramaModifi, "btnGuardar_Click", ex);
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            CreditoIcetex pEntidad = new CreditoIcetex();
            string pError = string.Empty;
            if (idObjeto == "")
            {
                VerError("Se generó un error interno por favor vuelva a generar la modificación");
                return;
            }
            pEntidad.numero_credito = Convert.ToInt64(idObjeto);
            pEntidad.cod_convocatoria = Convert.ToInt32(lblConvocatoria.Text);
            pEntidad.cod_persona = Convert.ToInt32(lblCod_Persona.Text);
            pEntidad.fecha_solicitud = Convert.ToDateTime(txtFecha.Text);
            pEntidad.tipo_beneficiario = ddlTipoBeneficiario.SelectedValue;
            pEntidad.identificacion = txtNroDoc.Text;
            pEntidad.codtipoidentificacion = Convert.ToInt32(ddlTipoDoc.SelectedValue);
            pEntidad.primer_nombre = txtNombre1.Text;   
            pEntidad.segundo_nombre = txtNombre2.Text.Trim() != "" ? txtNombre2.Text : null;
            pEntidad.primer_apellido = txtApellido1.Text;
            pEntidad.segundo_apellido = txtApellido2.Text.Trim() != "" ? txtApellido2.Text : null;
            pEntidad.direccion = txtDireccion.Text == "" ? null : txtDireccion.Text;
            pEntidad.telefono = txtTelefono.Text;
            pEntidad.email = txtEmail.Text;
            pEntidad.estrato = Convert.ToInt32(txtEstrato.Text);
            pEntidad.cod_universidad = ddlUniversidad.SelectedValue;
            pEntidad.cod_programa = ddlPrograma.SelectedValue;
            pEntidad.tipo_programa = Convert.ToInt32(ddlTipoPrograma.SelectedValue);
            pEntidad.valor = Convert.ToDecimal(txtValorPrograma.Text.Replace(".", ""));
            pEntidad.periodos = Convert.ToDecimal(ddlPeriodos.SelectedValue);
            pEntidad.estado = lblCodEstado.Text;
            pEntidad = ConvocatoriaService.ModificarCreditoIcetex(pEntidad, ref pError, Usuario);
            if (!string.IsNullOrEmpty(pError))
            {
                VerError(pError);
                return;
            }
            else
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                panelGeneral.Visible = false;
                panelFinal.Visible = true;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModificacionIctx.CodigoProgramaModifi, "btnContinuarMen_Click", ex);
        }
    }

    

}