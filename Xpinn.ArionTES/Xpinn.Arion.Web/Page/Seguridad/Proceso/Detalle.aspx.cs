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
    private Xpinn.Seguridad.Services.ProcesoService ProcesoServicio = new Xpinn.Seguridad.Services.ProcesoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ProcesoServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
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
                AsignarEventoConfirmar();
                if (Session[ProcesoServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ProcesoServicio.CodigoPrograma + ".id"].ToString();
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
            ProcesoServicio.EliminarProceso(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesoServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[ProcesoServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.Seguridad.Entities.Proceso vProceso = new Xpinn.Seguridad.Entities.Proceso();
            vProceso = ProcesoServicio.ConsultarProceso(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vProceso.cod_proceso != Int64.MinValue)
                txtCod_proceso.Text = vProceso.cod_proceso.ToString().Trim();
            if (vProceso.cod_modulo != Int64.MinValue)
                txtCod_modulo.Text = vProceso.cod_modulo.ToString().Trim();
            if (!string.IsNullOrEmpty(vProceso.nombre))
                txtNombre.Text = vProceso.nombre.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesoServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
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