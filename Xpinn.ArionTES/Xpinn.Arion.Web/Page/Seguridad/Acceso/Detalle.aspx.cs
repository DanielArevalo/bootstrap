using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Seguridad.Services;
using Xpinn.Seguridad.Entities;
using Xpinn.Util;

partial class Detalle : GlobalWeb
{
    private Xpinn.Seguridad.Services.PerfilService perfilServicio = new Xpinn.Seguridad.Services.PerfilService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(perfilServicio.CodigoPrograma, "D");
            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pConsulta.Enabled = false;
                CargarModulo();
                if (Session[perfilServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[perfilServicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }
                else { txtCodigo.Text = perfilServicio.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString(); }
                CargarOpciones(idObjeto, ddlModulo.SelectedValue);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void CargarOpciones(string sidPerfil, string sidModulo)
    {
        Int64 idPerfil = 0;
        if (sidPerfil.Trim() != "")
            idPerfil = Convert.ToInt64(sidPerfil);
        List<Xpinn.Seguridad.Entities.Acceso> lstAccesos = new List<Xpinn.Seguridad.Entities.Acceso>();
        lstAccesos = perfilServicio.ListarOpciones(idPerfil, Convert.ToInt64(sidModulo), (Usuario)Session["Usuario"]);
        gvLista.DataSource = lstAccesos;
        gvLista.DataBind();
    }

    /// <summary>
    /// Crear los datos del perfil
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DropDownList ddlCiudadOtr = (DropDownList)e.Row.Cells[1].FindControl("ddlCiudadOtr");
            //if (ddlCiudadOtr != null)
            //{
            //    ddlCiudadOtr.Visible = true;
            //}
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Perfil vPerfil = new Perfil();
            vPerfil = perfilServicio.ConsultarPerfil(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vPerfil.codperfil.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vPerfil.codperfil.ToString().Trim());
            if (!string.IsNullOrEmpty(vPerfil.nombreperfil.ToString()))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vPerfil.nombreperfil.ToString().Trim());

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    private void CargarModulo()
    {
        try
        {
            Xpinn.Seguridad.Services.ModuloService moduloServicio = new Xpinn.Seguridad.Services.ModuloService();
            List<Xpinn.Seguridad.Entities.Modulo> lstModulo = new List<Xpinn.Seguridad.Entities.Modulo>();
            lstModulo = moduloServicio.ListarModulo(null, (Usuario)Session["usuario"]);
            ddlModulo.DataSource = lstModulo;
            ddlModulo.DataTextField = "nom_modulo";
            ddlModulo.DataValueField = "cod_modulo";
            ddlModulo.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(perfilServicio.CodigoPrograma, "CargarModulo", ex);
        }
    }

    protected void ddlModulo_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarOpciones(idObjeto, ddlModulo.SelectedValue);
    }
}