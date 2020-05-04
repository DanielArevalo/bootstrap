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
    private Xpinn.Seguridad.Services.ModuloService ModuloServicio = new Xpinn.Seguridad.Services.ModuloService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ModuloServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModuloServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[ModuloServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ModuloServicio.CodigoPrograma + ".id"].ToString();
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
            BOexcepcion.Throw(ModuloServicio.CodigoPrograma, "Page_Load", ex);
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
            ModuloServicio.EliminarModulo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModuloServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[ModuloServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.Seguridad.Entities.Modulo vModulo = new Xpinn.Seguridad.Entities.Modulo();
            vModulo = ModuloServicio.ConsultarModulo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vModulo.cod_modulo != Int64.MinValue)
                txtCod_modulo.Text = vModulo.cod_modulo.ToString().Trim();
            if (!string.IsNullOrEmpty(vModulo.nom_modulo))
                txtNom_modulo.Text = vModulo.nom_modulo.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModuloServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}