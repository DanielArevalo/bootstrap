using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;

public partial class AseEjecutivosDetalle : GlobalWeb
{
    EjecutivoService serviceEjecutivo = new EjecutivoService();
    Ejecutivo aseEntiEjecutivo = new Ejecutivo();

    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(serviceEjecutivo.CodigoPrograma, "D");
            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.CodigoPrograma + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[serviceEjecutivo.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[serviceEjecutivo.CodigoPrograma + ".id"].ToString();
                    Session.Remove(serviceEjecutivo.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(serviceEjecutivo.GetType().Name + "D", "Page_Load", ex);
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
            serviceEjecutivo.EliminarEjecutivo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.CodigoPrograma + "C", "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[serviceEjecutivo.CodigoPrograma + ".id"] = idObjeto;
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
            aseEntiEjecutivo = new Ejecutivo();
            aseEntiEjecutivo = serviceEjecutivo.ConsultarEjecutivo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(aseEntiEjecutivo.PrimerNombre)) txtPrimerNombre.Text = aseEntiEjecutivo.PrimerNombre.Trim().ToString();
            if (!string.IsNullOrEmpty(aseEntiEjecutivo.SegundoNombre)) txtSegundoNombre.Text = aseEntiEjecutivo.SegundoNombre.Trim().ToString();
            if (!string.IsNullOrEmpty(aseEntiEjecutivo.PrimerApellido)) txtPrimerApellido.Text = aseEntiEjecutivo.PrimerApellido.Trim().ToString();
            if (!string.IsNullOrEmpty(aseEntiEjecutivo.SegundoApellido)) txtSegundoApellido.Text = aseEntiEjecutivo.SegundoApellido.Trim().ToString();
            if (!aseEntiEjecutivo.TipoDocumento.Equals(0)) txtTipoDoc.Text = aseEntiEjecutivo.NombreTipoDocumento;
            if (!aseEntiEjecutivo.NumeroDocumento.Equals(0)) txtNumeDoc.Text = aseEntiEjecutivo.NumeroDocumento.ToString();
            if (!string.IsNullOrEmpty(aseEntiEjecutivo.Direccion)) txtDireccion.Text = aseEntiEjecutivo.Direccion.ToString();
            //CONSULTAR BARRIOS DEL ASESOR 
            //if (!string.IsNullOrEmpty(aseEntiEjecutivo.Barrio)) txtBarrio.Text = aseEntiEjecutivo.Barrio.Trim().ToString();
            txtBarrio.Text = serviceEjecutivo.DetalleBarriosEjecutivo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            if (!aseEntiEjecutivo.Telefono.Equals(0)) txtTeleResi.Text = aseEntiEjecutivo.Telefono.ToString();
            if (!aseEntiEjecutivo.TelefonoCel.Equals(0)) txtCelular.Text = aseEntiEjecutivo.TelefonoCel.ToString();
            if (!string.IsNullOrEmpty(aseEntiEjecutivo.Codigo)) txtCodigo.Text = aseEntiEjecutivo.Codigo;
            //CONSULTAR ZONAS DEL ASESOR 
            //if (!aseEntiEjecutivo.IdZona.Equals(0)) txtZona.Text = aseEntiEjecutivo.NombreZona;
            txtZona.Text = serviceEjecutivo.DetalleZonasEjecutivo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            if (!aseEntiEjecutivo.IdEstado.Equals(0)) txtEstado.Text = aseEntiEjecutivo.NombreEstado;
            if (!string.IsNullOrEmpty(aseEntiEjecutivo.Email)) txtEmail.Text = aseEntiEjecutivo.Email.Trim().ToString();
            if (aseEntiEjecutivo.FechaIngreso != null) txtFechaIngreso.Text = aseEntiEjecutivo.FechaIngreso.ToShortDateString().ToString();
            if (!string.IsNullOrEmpty(aseEntiEjecutivo.NombreOficina)) txtOficina.Text = aseEntiEjecutivo.NombreOficina.ToString();

            //VerAuditoria(aseEntiEjecutivo.UsuarioCrea, aseEntiEjecutivo.FechaCrea, aseEntiEjecutivo.UsuarioEdita, aseEntiEjecutivo.FechaEdita);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }
}