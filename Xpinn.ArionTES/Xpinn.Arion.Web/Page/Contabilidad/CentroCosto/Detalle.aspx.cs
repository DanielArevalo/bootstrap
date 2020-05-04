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
    private Xpinn.Contabilidad.Services.CentroCostoService CentroCostoServicio = new Xpinn.Contabilidad.Services.CentroCostoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CentroCostoServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CentroCostoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[CentroCostoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CentroCostoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CentroCostoServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(CentroCostoServicio.CodigoPrograma, "Page_Load", ex);
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
            CentroCostoServicio.EliminarCentroCosto(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CentroCostoServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[CentroCostoServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.Contabilidad.Entities.CentroCosto vCentroCosto = new Xpinn.Contabilidad.Entities.CentroCosto();
            vCentroCosto = CentroCostoServicio.ConsultarCentroCosto(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            txtCentroCosto.Text = HttpUtility.HtmlDecode(vCentroCosto.centro_costo.ToString().Trim());
            if (!string.IsNullOrEmpty(vCentroCosto.nom_centro))
                txtNombre.Text = HttpUtility.HtmlDecode(vCentroCosto.nom_centro.ToString().Trim());
            if (!string.IsNullOrEmpty(vCentroCosto.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vCentroCosto.descripcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCentroCosto.principal.ToString()))
                if (vCentroCosto.principal == 1)
                    chbPrincipal.Checked = true;
                else
                    chbPrincipal.Checked = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CentroCostoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

}