using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Comun.Services;
using Xpinn.Comun.Entities;
using Xpinn.Util;

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.ProcesosService ProcesosServicio = new Xpinn.FabricaCreditos.Services.ProcesosService(); // Permite iniciar la consulta del historial (Segundo GridView)
    private EliminarRegistroServices eliminarRegistroServices = new EliminarRegistroServices();
    string vAsesor = null;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ProcesosServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                CargarValoresConsulta(pConsulta, ProcesosServicio.CodigoPrograma);
                if (Session[ProcesosServicio.CodigoPrograma + ".consulta"] != null)
                {
                    Actualizar();
                }

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables
            Xpinn.FabricaCreditos.Services.ControlTiemposService ControlProcesosServicio = new Xpinn.FabricaCreditos.Services.ControlTiemposService();
            lstDatosSolicitud.Clear();
            lstDatosSolicitud = ControlProcesosServicio.ListasDesplegables("EstadoProceso", (Usuario)Session["Usuario"]);
            ddlAntecesor.DataSource = lstDatosSolicitud;
            ddlAntecesor.DataTextField = "ListaDescripcion";
            ddlAntecesor.DataValueField = "ListaId";
            ddlAntecesor.DataBind();
            ddlAntecesor.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ProcesosServicio.GetType().Name);
        Navegar(Pagina.Nuevo);

        Session["operacion"] = "N";
    }



    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ProcesosServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }

    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ProcesosServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);

    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[4].Text;
        Session[ProcesosServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            ProcesosServicio.EliminarProcesos(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Procesos> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Procesos>();
            String filtro = obtFiltroListarValores(ObtenerValores());
            lstConsulta = ProcesosServicio.ListarProcesos(ObtenerValores(), (Usuario)Session["usuario"], filtro);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados: " + lstConsulta.Count.ToString();
                gvLista.DataBind();

            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ProcesosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma, "Actualizar", ex);
        }
    }



    private Xpinn.FabricaCreditos.Entities.Procesos ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Procesos vProcesos = new Xpinn.FabricaCreditos.Entities.Procesos();

        if (txtCodProceso.Text.Trim() != "")
            vProcesos.cod_proceso = Convert.ToInt64(txtCodProceso.Text);

        if (ddlTipoProceso.SelectedIndex != 0)
            vProcesos.tipo_proceso = Convert.ToInt64(ddlTipoProceso.SelectedValue);

        if (ddlAntecesor.SelectedIndex != 0)
            vProcesos.cod_proceso_antec = Convert.ToInt64(ddlAntecesor.SelectedValue);



        return vProcesos;
    }

    private string obtFiltroListarValores(Xpinn.FabricaCreditos.Entities.Procesos proceso)
    {
        String filtro = String.Empty;

        if (txtCodProceso.Text.Trim() != "")
        {
            proceso.cod_proceso = Convert.ToInt64(txtCodProceso.Text);
            filtro += " and CODTIPOPROCESO=" + proceso.cod_proceso;
        }


        if (ddlAntecesor.SelectedIndex != 0)
        {
            proceso.cod_proceso_antec = Convert.ToInt64(ddlAntecesor.SelectedValue);
            filtro += " and CODPROANTECEDE= '" + proceso.cod_proceso_antec + "'";
        }
        if (ddlTipoProceso.SelectedIndex != 0)
        {
            proceso.tipo_proceso = Convert.ToInt64(ddlTipoProceso.SelectedValue);
            filtro += " and TIPO_PROCESO='" + proceso.tipo_proceso + "'";
        }

        else
            filtro += "";
        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "  Where " + filtro;
        }
        return filtro;

    }



    private Xpinn.FabricaCreditos.Entities.ControlCreditos ObtenerValoresHistorico()
    {
        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
        vControlCreditos.numero_radicacion = Convert.ToInt64(ViewState["Proceso"].ToString());
        return vControlCreditos;
    }

    protected void gvListaHistorico_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProcesosServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }



    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        mvProcesos.ActiveViewIndex = 0;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ProcesosServicio.CodigoPrograma);
        Actualizar();

    }

    private void Borrar()
    {
        //ddlAccion.Text = "";
        //  txtFechaProximaAccion.Text = "";
        //  txtObservaciones.Text = "";
    }

    protected void ddlTipoProceso_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void ddlAntecesor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void gvLista_OnRowDeleted(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            EliminarRegistro eliminar = new EliminarRegistro();
            eliminar.IdConsecutivo = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[4].Text);
            eliminar.NombreTabla = "TipoProcesos";
            eliminar.IdTablaRes = "CodTipoProceso";
            var perror = eliminarRegistroServices.EliminarRegistro(eliminar, (Usuario)Session["usuario"]);
            if (string.IsNullOrEmpty(perror.Perror))
            {
                Actualizar();
            }
            else
            {
                Response.Write("<script language=javascript>alert('No se pudo elimnar el registro.');</script>");
            }
        }
        catch (Exception exception)
        {
            Response.Write("<script language=javascript>alert('No se pudo elimnar el registro.');</script>");
            throw;
        }
    }
}