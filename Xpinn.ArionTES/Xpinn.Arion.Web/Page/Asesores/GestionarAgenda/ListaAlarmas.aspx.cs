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
using Xpinn.Asesores.Entities;

partial class ListaAlarmas : GlobalWeb
{
    AgendaAlarmaService AgendaAlarmaServicio = new AgendaAlarmaService();
    AgendaActividadService AgendaActividadServicio = new AgendaActividadService();
    AgendaTipoActividadService tipoActividadServicio = new AgendaTipoActividadService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            //VisualizarOpciones(AgendaAlarmaServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaAlarmaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[AgendaActividadServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AgendaActividadServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AgendaActividadServicio.CodigoPrograma + ".id");
                    txtFecha.Text = Session[AgendaActividadServicio.CodigoPrograma + ".fecha"].ToString();
                    Session.Remove(AgendaActividadServicio.CodigoPrograma + ".fecha");
                    LlenarComboTipoActividad();
                    Actualizar();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaAlarmaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AgendaAlarmaServicio.CodigoPrograma);
        Navegar(Pagina.Lista);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AgendaAlarmaServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, AgendaAlarmaServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaAlarmaServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AgendaAlarmaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[AgendaAlarmaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            AgendaAlarmaServicio.EliminarAgendaAlarma(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaAlarmaServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(AgendaAlarmaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Asesores.Entities.AgendaAlarma> lstConsulta = new List<Xpinn.Asesores.Entities.AgendaAlarma>();
            lstConsulta = AgendaAlarmaServicio.ListarAgendaAlarma(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                //ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(AgendaAlarmaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AgendaAlarmaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Asesores.Entities.AgendaAlarma ObtenerValores()
    {
        Xpinn.Asesores.Entities.AgendaAlarma vAgendaAlarma = new Xpinn.Asesores.Entities.AgendaAlarma();

        if (txtFecha.Text.Trim() != "")
            vAgendaAlarma.fecha = Convert.ToDateTime(txtFecha.Text.Trim());
        if (txtHora.Text.Trim() != "")
            vAgendaAlarma.hora = Convert.ToInt64(txtHora.Text.Trim());
        if (txtTipoalarma.Text.Trim() != "")
            vAgendaAlarma.tipoalarma = Convert.ToInt64(txtTipoalarma.Text.Trim());
        if (txtIdcliente.Text.Trim() != "")
            vAgendaAlarma.idcliente = Convert.ToInt64(txtIdcliente.Text.Trim());
            
        //if (txtDescripcion.Text.Trim() != "")
        //    vAgendaAlarma.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
        //if (txtRepeticiones.Text.Trim() != "")
        //    vAgendaAlarma.repeticiones = Convert.ToInt64(txtRepeticiones.Text.Trim());
        if (ddlEstado.SelectedIndex!=0)
            vAgendaAlarma.estado = Convert.ToInt64(ddlEstado.SelectedValue);
        if (ddlTipoActividad.SelectedIndex != 0 && ddlTipoActividad.SelectedIndex != -1)
            vAgendaAlarma.tipoactividad = Convert.ToInt64(ddlTipoActividad.SelectedValue);
        if(idObjeto !="")
        vAgendaAlarma.idasesor = Convert.ToInt64(idObjeto);

        return vAgendaAlarma;
    }

    protected void LlenarComboTipoActividad()
    {
        List<AgendaTipoActividad> lstConsultaTipoActividad = new List<AgendaTipoActividad>();
        AgendaTipoActividad tipoActividad = new AgendaTipoActividad();
        ddlTipoActividad.DataSource = tipoActividadServicio.ListarAgendaTipoActividad(tipoActividad, (Usuario)Session["usuario"]);
        ddlTipoActividad.DataTextField = "descripcion";
        ddlTipoActividad.DataValueField = "idtipo";
        ddlTipoActividad.DataBind();
        ddlTipoActividad.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }
}