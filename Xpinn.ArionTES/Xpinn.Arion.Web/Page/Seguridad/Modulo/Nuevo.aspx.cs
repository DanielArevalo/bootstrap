using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Seguridad.Services.ModuloService ModuloServicio = new Xpinn.Seguridad.Services.ModuloService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ModuloServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ModuloServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ModuloServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
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
                if (Session[ModuloServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ModuloServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ModuloServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else if (idObjeto.Trim() != null)
                {
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    // Ingresa a la pagina a agregar registros
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Seguridad.Entities.Modulo vModulo = new Xpinn.Seguridad.Entities.Modulo();

            if (idObjeto != "")
                vModulo = ModuloServicio.ConsultarModulo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vModulo.cod_modulo = Convert.ToInt64(txtCod_modulo.Text.Trim());
            vModulo.nom_modulo = Convert.ToString(txtNom_modulo.Text.Trim());

            if (idObjeto != "")
            {
                vModulo.cod_modulo = Convert.ToInt64(idObjeto);
                ModuloServicio.ModificarModulo(vModulo, (Usuario)Session["usuario"]);
            }
            else
            {
                vModulo = ModuloServicio.CrearModulo(vModulo, (Usuario)Session["usuario"]);
                idObjeto = vModulo.cod_modulo.ToString();
            }

            Session[ModuloServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModuloServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[ModuloServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
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
                txtNom_modulo.Text = HttpUtility.HtmlDecode(vModulo.nom_modulo.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModuloServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
}