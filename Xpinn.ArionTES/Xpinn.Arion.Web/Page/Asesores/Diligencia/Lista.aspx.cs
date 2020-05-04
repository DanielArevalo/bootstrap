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
using Xpinn.Asesores.Services;

partial class Lista : GlobalWeb
{
    DiligenciaService DiligenciaServicio = new DiligenciaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(DiligenciaServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
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
                CargarValoresConsulta(pConsulta, DiligenciaServicio.CodigoPrograma);
                if (Session[DiligenciaServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DiligenciaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, DiligenciaServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, DiligenciaServicio.CodigoPrograma);
            Actualizar();
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, DiligenciaServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DiligenciaServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[DiligenciaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[DiligenciaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            DiligenciaServicio.EliminarDiligencia(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DiligenciaServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DiligenciaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Asesores.Entities.Diligencia> lstConsulta = new List<Xpinn.Asesores.Entities.Diligencia>();
            lstConsulta = DiligenciaServicio.ListarDiligencia(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(DiligenciaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DiligenciaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Asesores.Entities.Diligencia ObtenerValores()
    {
        Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();

    if (txtCodigo_diligencia.Text.Trim() != "")
        vDiligencia.codigo_diligencia = Convert.ToInt64(txtCodigo_diligencia.Text.Trim());
    if(txtNumero_radicacion.Text.Trim() != "")
        vDiligencia.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());
    if(txtFecha_diligencia.Text.Trim() != "")
        vDiligencia.fecha_diligencia = Convert.ToDateTime(txtFecha_diligencia.Text.Trim());
    if(txtTipo_diligencia.Text.Trim() != "")
        vDiligencia.tipo_diligencia = Convert.ToInt64(txtTipo_diligencia.Text.Trim());
    if(txtAtendio.Text.Trim() != "")
        vDiligencia.atendio = Convert.ToString(txtAtendio.Text.Trim());
    if(txtRespuesta.Text.Trim() != "")
        vDiligencia.respuesta = Convert.ToString(txtRespuesta.Text.Trim());
    if(txtAcuerdo.Text.Trim() != "")
        vDiligencia.acuerdo = Convert.ToInt64(txtAcuerdo.Text.Trim());
    if(txtFecha_acuerdo.Text.Trim() != "")
        vDiligencia.fecha_acuerdo = Convert.ToDateTime(txtFecha_acuerdo.Text.Trim());
    if(txtValor_acuerdo.Text.Trim() != "")
        vDiligencia.valor_acuerdo = Convert.ToInt64(txtValor_acuerdo.Text.Trim());
    if(txtObservacion.Text.Trim() != "")
        vDiligencia.observacion = Convert.ToString(txtObservacion.Text.Trim());
    if(txtCodigo_usuario_regis.Text.Trim() != "")
        vDiligencia.codigo_usuario_regis = Convert.ToInt64(txtCodigo_usuario_regis.Text.Trim());

        return vDiligencia;
    }
}