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
    private Xpinn.Obligaciones.Services.ComponenteService ComponenteServicio = new Xpinn.Obligaciones.Services.ComponenteService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ComponenteServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComponenteServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[ComponenteServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ComponenteServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ComponenteServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(ComponenteServicio.CodigoPrograma, "Page_Load", ex);
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
            ComponenteServicio.EliminarComponente(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComponenteServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[ComponenteServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.Obligaciones.Entities.Componente vComponente = new Xpinn.Obligaciones.Entities.Componente();
            vComponente = ComponenteServicio.ConsultarComponente(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            txtCodigo.Text = HttpUtility.HtmlDecode(vComponente.CODCOMPONENTE.ToString().Trim());
            if (!string.IsNullOrEmpty(vComponente.NOMBRE))
                txtNombre.Text = HttpUtility.HtmlDecode(vComponente.NOMBRE.ToString().Trim());
            if (vComponente.CAUSA.ToString() == "1")
                chkCausa.Checked = true;
            else
                chkCausa.Checked = false;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComponenteServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }

}