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
    private Xpinn.Seguridad.Services.OpcionService OpcionServicio = new Xpinn.Seguridad.Services.OpcionService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(OpcionServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                CargarProcesos();
                if (Session[OpcionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[OpcionServicio.CodigoPrograma + ".id"].ToString();
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
            BOexcepcion.Throw(OpcionServicio.CodigoPrograma, "Page_Load", ex);
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
            OpcionServicio.EliminarOpcion(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[OpcionServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.Seguridad.Entities.Opcion vOpcion = new Xpinn.Seguridad.Entities.Opcion();
            vOpcion = OpcionServicio.ConsultarOpcion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vOpcion.cod_opcion != Int64.MinValue)
                txtCod_opcion.Text = vOpcion.cod_opcion.ToString().Trim();
            if (!string.IsNullOrEmpty(vOpcion.nombre))
                txtNombre.Text = vOpcion.nombre.ToString().Trim();
            if (vOpcion.cod_proceso != Int64.MinValue)
                txtCod_proceso.Text = vOpcion.cod_proceso.ToString().Trim();
            if (!string.IsNullOrEmpty(vOpcion.ruta))
                txtRuta.Text = vOpcion.ruta.ToString().Trim();
            if (vOpcion.generalog != Int64.MinValue)
                txtGeneralog.Text = vOpcion.generalog.ToString().Trim();
            if (!string.IsNullOrEmpty(vOpcion.accion))
                if (vOpcion.accion.Trim() == "1") txtAccion.Text = vOpcion.accion.ToString().Trim(); else txtAccion.Text = "";
            if (vOpcion.refayuda != Int64.MinValue)
                txtRefayuda.Text = vOpcion.refayuda.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }

    private void CargarProcesos()
    {
        try
        {
            Xpinn.Seguridad.Services.ProcesoService procesoServicio = new Xpinn.Seguridad.Services.ProcesoService();
            List<Xpinn.Seguridad.Entities.Proceso> lstProceso = new List<Xpinn.Seguridad.Entities.Proceso>();

            lstProceso = procesoServicio.ListarProceso(null, (Usuario)Session["usuario"]);

            txtCod_proceso.DataSource = lstProceso;
            txtCod_proceso.DataTextField = "nombre";
            txtCod_proceso.DataValueField = "cod_proceso";
            txtCod_proceso.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionServicio.CodigoPrograma, "CargarProcesos", ex);
        }
    }
}