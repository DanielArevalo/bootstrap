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
    private Xpinn.Asesores.Services.DiligenciaService DiligenciaServicio = new Xpinn.Asesores.Services.DiligenciaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(DiligenciaServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DiligenciaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                if (Session[DiligenciaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[DiligenciaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(DiligenciaServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(DiligenciaServicio.CodigoPrograma, "Page_Load", ex);
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
            DiligenciaServicio.EliminarDiligencia(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DiligenciaServicio.CodigoPrograma, "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[DiligenciaServicio.CodigoPrograma + ".id"] = idObjeto;
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
            Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();
            vDiligencia = DiligenciaServicio.ConsultarDiligencia(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vDiligencia.codigo_diligencia != Int64.MinValue)
                txtCodigo_diligencia.Text = vDiligencia.codigo_diligencia.ToString().Trim();
            if (vDiligencia.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = vDiligencia.numero_radicacion.ToString().Trim();
            if (!string.IsNullOrEmpty(vDiligencia.fecha_diligencia.ToString()))
                txtFecha_diligencia.Text = vDiligencia.fecha_diligencia.ToString("dd/MM/yyyy").Trim();
            if (vDiligencia.tipo_diligencia != Int64.MinValue)
                txtTipo_diligencia.Text = vDiligencia.tipo_diligencia.ToString().Trim();
            if (!string.IsNullOrEmpty(vDiligencia.atendio))
                txtAtendio.Text = vDiligencia.atendio.ToString().Trim();
            if (!string.IsNullOrEmpty(vDiligencia.respuesta))
                txtRespuesta.Text = vDiligencia.respuesta.ToString().Trim();
            if (!string.IsNullOrEmpty(vDiligencia.fecha_acuerdo.ToString()))
                txtFecha_acuerdo.Text = vDiligencia.fecha_acuerdo.ToString("dd/MM/yyyy").Trim();
            if (vDiligencia.valor_acuerdo != Int64.MinValue)
                txtValor_acuerdo.Text = vDiligencia.valor_acuerdo.ToString().Trim();
            if (!string.IsNullOrEmpty(vDiligencia.anexo))
                txtAnexo.Text = vDiligencia.anexo.ToString().Trim();
            if (!string.IsNullOrEmpty(vDiligencia.observacion))
                txtObservacion.Text = vDiligencia.observacion.ToString().Trim();
            if (vDiligencia.codigo_usuario_regis != Int64.MinValue)
                txtCodigo_usuario_regis.Text = vDiligencia.codigo_usuario_regis.ToString().Trim();

            if (vDiligencia.acuerdo != Int64.MinValue)
            {
                if (vDiligencia.acuerdo == 1)
                    chkAprueba.Checked = true;
                else
                    chkAprueba.Checked = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DiligenciaServicio.CodigoPrograma + "D", "ObtenerDatos", ex);
        }
    }
}