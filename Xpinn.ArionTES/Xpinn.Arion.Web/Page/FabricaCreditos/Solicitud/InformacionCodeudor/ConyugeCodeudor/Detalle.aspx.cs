using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

partial class Detalle : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(Persona1Servicio.CodigoPrograma, "D");

            Site2 toolBar = (Site2)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[Persona1Servicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[Persona1Servicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(Persona1Servicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Persona1Servicio.EliminarPersona1(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[Persona1Servicio.CodigoPrograma + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            vPersona1 = Persona1Servicio.ConsultarPersona1(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vPersona1.cod_persona != Int64.MinValue)
                txtCod_persona.Text = vPersona1.cod_persona.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.tipo_persona))
                txtTipo_persona.Text = vPersona1.tipo_persona.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.identificacion))
                txtIdentificacion.Text = vPersona1.identificacion.ToString().Trim();
            if (vPersona1.digito_verificacion != Int64.MinValue)
                txtDigito_verificacion.Text = vPersona1.digito_verificacion.ToString().Trim();
            if (vPersona1.tipo_identificacion != Int64.MinValue)
                txtTipo_identificacion.Text = vPersona1.tipo_identificacion.ToString().Trim();
            if (vPersona1.fechaexpedicion != DateTime.MinValue)
                txtFechaexpedicion.Text = vPersona1.fechaexpedicion.Value.ToShortDateString();
            if (vPersona1.codciudadexpedicion != Int64.MinValue)
                txtCodciudadexpedicion.Text = vPersona1.codciudadexpedicion.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.sexo))
                txtSexo.Text = vPersona1.sexo.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.primer_nombre))
                txtPrimer_nombre.Text = vPersona1.primer_nombre.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.segundo_nombre))
                txtSegundo_nombre.Text = vPersona1.segundo_nombre.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.primer_apellido))
                txtPrimer_apellido.Text = vPersona1.primer_apellido.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.segundo_apellido))
                txtSegundo_apellido.Text = vPersona1.segundo_apellido.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.razon_social))
                txtRazon_social.Text = vPersona1.razon_social.ToString().Trim();
            if (vPersona1.fechanacimiento != DateTime.MinValue)
                txtFechanacimiento.Text = vPersona1.fechanacimiento.Value.ToShortDateString();
            if (vPersona1.codciudadnacimiento != Int64.MinValue)
                txtCodciudadnacimiento.Text = vPersona1.codciudadnacimiento.ToString().Trim();
            if (vPersona1.codestadocivil != Int64.MinValue)
                txtCodestadocivil.Text = vPersona1.codestadocivil.ToString().Trim();
            if (vPersona1.codescolaridad != Int64.MinValue)
                txtCodescolaridad.Text = vPersona1.codescolaridad.ToString().Trim();
            if (vPersona1.codactividad != Int64.MinValue)
                txtCodactividad.Text = vPersona1.codactividad.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.direccion))
                txtDireccion.Text = vPersona1.direccion.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.telefono))
                txtTelefono.Text = vPersona1.telefono.ToString().Trim();
            if (vPersona1.codciudadresidencia != Int64.MinValue)
                txtCodciudadresidencia.Text = vPersona1.codciudadresidencia.ToString().Trim();
            if (vPersona1.antiguedadlugar != Int64.MinValue)
                txtAntiguedadlugar.Text = vPersona1.antiguedadlugar.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.tipovivienda))
                txtTipovivienda.Text = vPersona1.tipovivienda.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.arrendador))
                txtArrendador.Text = vPersona1.arrendador.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.telefonoarrendador))
                txtTelefonoarrendador.Text = vPersona1.telefonoarrendador.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.celular))
                txtCelular.Text = vPersona1.celular.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.email))
                txtEmail.Text = vPersona1.email.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.empresa))
                txtEmpresa.Text = vPersona1.empresa.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.telefonoempresa))
                txtTelefonoempresa.Text = vPersona1.telefonoempresa.ToString().Trim();
            if (vPersona1.codcargo != Int64.MinValue)
                txtCodcargo.Text = vPersona1.codcargo.ToString().Trim();
            if (vPersona1.codtipocontrato != Int64.MinValue)
                txtCodtipocontrato.Text = vPersona1.codtipocontrato.ToString().Trim();
            if (vPersona1.cod_asesor != Int64.MinValue)
                txtCod_asesor.Text = vPersona1.cod_asesor.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.residente))
                txtResidente.Text = vPersona1.residente.ToString().Trim();
            if (vPersona1.fecha_residencia != DateTime.MinValue)
                txtFecha_residencia.Text = vPersona1.fecha_residencia.ToShortDateString();
            if (vPersona1.cod_oficina != Int64.MinValue)
                txtCod_oficina.Text = vPersona1.cod_oficina.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.tratamiento))
                txtTratamiento.Text = vPersona1.tratamiento.ToString().Trim();
            if (!string.IsNullOrEmpty(vPersona1.estado))
                txtEstado.Text = vPersona1.estado.ToString().Trim();
            if (vPersona1.fechacreacion != DateTime.MinValue)
                txtFechacreacion.Text = vPersona1.fechacreacion.ToShortDateString();
            if (!string.IsNullOrEmpty(vPersona1.usuariocreacion))
                txtUsuariocreacion.Text = vPersona1.usuariocreacion.ToString().Trim();
            if (vPersona1.fecultmod != DateTime.MinValue)
                txtFecultmod.Text = vPersona1.fecultmod.ToShortDateString();
            if (!string.IsNullOrEmpty(vPersona1.usuultmod))
                txtUsuultmod.Text = vPersona1.usuultmod.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}