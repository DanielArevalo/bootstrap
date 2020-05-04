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

partial class Nuevo : GlobalWeb
{
    private Xpinn.Seguridad.Services.ProcesoService ProcesoServicio = new Xpinn.Seguridad.Services.ProcesoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ProcesoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ProcesoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ProcesoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarModulo();
                if (Session[ProcesoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ProcesoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ProcesoServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(ProcesoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Seguridad.Entities.Proceso vProceso = new Xpinn.Seguridad.Entities.Proceso();

            if (idObjeto != "")
                vProceso = ProcesoServicio.ConsultarProceso(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vProceso.cod_proceso = Convert.ToInt64(txtCod_proceso.Text.Trim());
            vProceso.cod_modulo = Convert.ToInt64(txtCod_modulo.Text.Trim());
            vProceso.nombre = Convert.ToString(txtNombre.Text.Trim());

            if (idObjeto != "")
            {
                vProceso.cod_proceso = Convert.ToInt64(idObjeto);
                ProcesoServicio.ModificarProceso(vProceso, (Usuario)Session["usuario"]);
            }
            else
            {
                vProceso = ProcesoServicio.CrearProceso(vProceso, (Usuario)Session["usuario"]);
                idObjeto = vProceso.cod_proceso.ToString();
            }

            Session[ProcesoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesoServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[ProcesoServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Seguridad.Entities.Proceso vProceso = new Xpinn.Seguridad.Entities.Proceso();
            vProceso = ProcesoServicio.ConsultarProceso(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vProceso.cod_proceso != Int64.MinValue)
                txtCod_proceso.Text = HttpUtility.HtmlDecode(vProceso.cod_proceso.ToString().Trim());
            if (vProceso.cod_modulo != Int64.MinValue)
                txtCod_modulo.Text = HttpUtility.HtmlDecode(vProceso.cod_modulo.ToString().Trim());
            if (!string.IsNullOrEmpty(vProceso.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vProceso.nombre.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    private void CargarModulo()
    {
        try
        {
            Xpinn.Seguridad.Services.ModuloService moduloServicio = new Xpinn.Seguridad.Services.ModuloService();
            List<Xpinn.Seguridad.Entities.Modulo> lstModulo = new List<Xpinn.Seguridad.Entities.Modulo>();

            lstModulo = moduloServicio.ListarModulo(null, (Usuario)Session["usuario"]);

            txtCod_modulo.DataSource = lstModulo;
            txtCod_modulo.DataTextField = "nom_modulo";
            txtCod_modulo.DataValueField = "cod_modulo";
            txtCod_modulo.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesoServicio.CodigoPrograma, "CargarModulo", ex);
        }
    }
}