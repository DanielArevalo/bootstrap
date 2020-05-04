using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;

public partial class Lista : GlobalWeb
{
    Xpinn.Reporteador.Services.UIAFService reporteServicio = new Xpinn.Reporteador.Services.UIAFService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(reporteServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, reporteServicio.GetType().Name);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }


    private void Actualizar()
    {
        try
        {
            Configuracion conf = new Configuracion();
            List<UIAF> lstConsulta = new List<UIAF>();

            String filtro = obtFiltro(ObtenerValores());

            DateTime pFechaIni, pFechaFin;
            pFechaIni = txtFechaIni.ToDateTime == null ? DateTime.MinValue : txtFechaIni.ToDateTime;
            pFechaFin = txtFechaFin.ToDateTime == null ? DateTime.MinValue : txtFechaFin.ToDateTime;

            lstConsulta = reporteServicio.ListarReporteUIAF(filtro,pFechaIni,pFechaFin, (Usuario)Session["usuario"]);

            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(reporteServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private UIAF ObtenerValores()
    {
        UIAF vProducto = new UIAF();
        if (txtNroProducto.Text.Trim() != "")
            vProducto.idreporte = Convert.ToInt32(txtNroProducto.Text.Trim());

        return vProducto;
    }

    

    private string obtFiltro(UIAF Producto)
    {

        String filtro = String.Empty;
        if (txtNroProducto.Text.Trim() != "")
            filtro += " and IDREPORTE = " + Producto.idreporte;         
        //filtro += " and estado ='G'";

        return filtro;
    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, reporteServicio.GetType().Name);
            Navegar(Pagina.Nuevo);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "btnNuevo_Click", ex);
        }
    }


    Boolean ValidarDatos()
    {
        VerError("");
       
        if (txtFechaIni.Text != "" && txtFechaFin.Text != "")
        {
            if (Convert.ToDateTime(txtFechaIni.Text) > Convert.ToDateTime(txtFechaFin.Text))
            {
                VerError("No puede Ingresar una Fecha inicial mayor a la fecha final");
                return false;
            }
        }

        return true;
    }



    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                GuardarValoresConsulta(pConsulta, reporteServicio.GetType().Name);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "btnConsultar_Click", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtFechaIni.Text = "";
        txtFechaFin.Text = "";
        LimpiarValoresConsulta(pConsulta, reporteServicio.GetType().Name);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[2].Text;
        Session[reporteServicio.CodigoPrograma + ".idreporte"] = id;
        Response.Redirect("Nuevo.aspx", false);
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
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación del Reporte?");
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            reporteServicio.EliminarReporteUIAF(Convert.ToInt32(Session["ID"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id;
        id = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        Session[reporteServicio.CodigoPrograma + ".id"] = id;

        Navegar(Pagina.Nuevo);
    }


    
}