using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;

public partial class AseClienteDetalle : GlobalWeb
{
    Usuario usuario = new Usuario();
    ClientePotencialService serviceCliente = new ClientePotencialService();

    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(serviceCliente.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCliente.CodigoPrograma + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[serviceCliente.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[serviceCliente.CodigoPrograma + ".id"].ToString();
                    Session.Remove(serviceCliente.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(serviceCliente.GetType().Name + "D", "Page_Load", ex);
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
            serviceCliente.EliminarCliente(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCliente.CodigoPrograma + "C", "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[serviceCliente.CodigoPrograma + ".id"] = idObjeto;
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
            ClientePotencial entityCliente = new ClientePotencial();
            entityCliente = serviceCliente.ConsultarCliente(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(entityCliente.PrimerNombre)) txtPrimerNombre.Text = entityCliente.PrimerNombre.Trim().ToString();
            if (!string.IsNullOrEmpty(entityCliente.SegundoNombre)) txtSegundoNombre.Text = entityCliente.SegundoNombre.Trim().ToString();
            if (!string.IsNullOrEmpty(entityCliente.PrimerApellido)) txtPrimerApellido.Text = entityCliente.PrimerApellido.Trim().ToString();
            if (!string.IsNullOrEmpty(entityCliente.SegundoApellido)) txtSegundoApellido.Text = entityCliente.SegundoApellido.Trim().ToString();
            if (!entityCliente.NumeroDocumento.Equals(0)) txtNumDoc.Text = entityCliente.NumeroDocumento.ToString();
            if (!string.IsNullOrEmpty(entityCliente.Direccion)) txtDireccion.Text = entityCliente.Direccion;
            if (!entityCliente.Telefono.Equals(0)) txtTelefono.Text = entityCliente.Telefono.ToString();
            if (!string.IsNullOrEmpty(entityCliente.Email)) txtEmail.Text = entityCliente.Email;
            if (!string.IsNullOrEmpty(entityCliente.RazonSocial)) txtRazonSocial.Text = entityCliente.RazonSocial;
            if (!string.IsNullOrEmpty(entityCliente.SiglaNegocio)) txtSigla.Text = entityCliente.SiglaNegocio;
            if (!string.IsNullOrEmpty(entityCliente.Zona.NombreZona)) txtZonac.Text = entityCliente.Zona.NombreZona;
            if (!string.IsNullOrEmpty(entityCliente.Actividad.NombreActividad)) txtActividad.Text = entityCliente.Actividad.NombreActividad;
            if (!entityCliente.TipoIdentificacion.IdTipoIdentificacion.Equals(0)) txtTipoDoc.Text = entityCliente.TipoIdentificacion.NombreTipoIdentificacion;
            
            //VerAuditoria(aseEntCliente.UsuarioCrea, aseEntCliente.FechaCrea, aseEntCliente.UsuarioEdita, aseEntCliente.FechaEdita);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceCliente.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }
}