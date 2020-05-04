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
    private Xpinn.Seguridad.Services.OpcionService OpcionServicio = new Xpinn.Seguridad.Services.OpcionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[OpcionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(OpcionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(OpcionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
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
                CargarProcesos();
                if (Session[OpcionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[OpcionServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(OpcionServicio.CodigoPrograma + ".id");
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Seguridad.Entities.Opcion vOpcion = new Xpinn.Seguridad.Entities.Opcion();

            if (idObjeto != "")
                vOpcion = OpcionServicio.ConsultarOpcion(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vOpcion.cod_opcion = Convert.ToInt64(txtCod_opcion.Text.Trim());
            vOpcion.nombre = Convert.ToString(txtNombre.Text.Trim());
            vOpcion.cod_proceso = Convert.ToInt64(txtCod_proceso.Text.Trim());
            vOpcion.ruta = Convert.ToString(txtRuta.Text.Trim());
            vOpcion.generalog = Convert.ToInt64(txtGeneralog.Text.Trim());
            vOpcion.accion = Convert.ToString(txtAccion.Text.Trim());
            vOpcion.refayuda = Convert.ToInt64(txtRefayuda.Text.Trim());
            vOpcion.PermisosCampos = Convert.ToInt32(DropDownList1.SelectedValue.Trim());

            if (idObjeto != "")
            {
                vOpcion.cod_opcion = Convert.ToInt64(idObjeto);
                OpcionServicio.ModificarOpcion(vOpcion, (Usuario)Session["usuario"]);
            }
            else
            {
                vOpcion = OpcionServicio.CrearOpcion(vOpcion, (Usuario)Session["usuario"]);
                idObjeto = vOpcion.cod_opcion.ToString();
            }

            Session[OpcionServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[OpcionServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Seguridad.Entities.Opcion vOpcion = new Xpinn.Seguridad.Entities.Opcion();
            vOpcion = OpcionServicio.ConsultarOpcion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vOpcion.cod_opcion != Int64.MinValue)
                txtCod_opcion.Text = HttpUtility.HtmlDecode(vOpcion.cod_opcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vOpcion.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vOpcion.nombre.ToString().Trim());
            if (vOpcion.cod_proceso != Int64.MinValue)
                txtCod_proceso.Text = HttpUtility.HtmlDecode(vOpcion.cod_proceso.ToString().Trim());
            if (!string.IsNullOrEmpty(vOpcion.ruta))
                txtRuta.Text = HttpUtility.HtmlDecode(vOpcion.ruta.ToString().Trim());
            if (vOpcion.generalog != Int64.MinValue)
                txtGeneralog.Text = HttpUtility.HtmlDecode(vOpcion.generalog.ToString().Trim());
            if (!string.IsNullOrEmpty(vOpcion.accion))
                if (vOpcion.accion.Trim() == "1") txtAccion.Text = vOpcion.accion.ToString().Trim(); else txtAccion.Text = "";
            if (vOpcion.refayuda != Int64.MinValue)
                txtRefayuda.Text = HttpUtility.HtmlDecode(vOpcion.refayuda.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(OpcionServicio.CodigoPrograma, "ObtenerDatos", ex);
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