using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Riesgo.Services;
using Xpinn.Riesgo.Entities;
using Xpinn.Util;


public partial class Lista : GlobalWeb
{
    IdentificacionServices identificacionServicio = new IdentificacionServices();
    SeguimientoServices seguimientoServicio = new SeguimientoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(identificacionServicio.CodigoProgramaC, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaC, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaC, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarPanel(pConsulta);
    }

    private void Actualizar()
    {
        List<Identificacion> lstCargos = new List<Identificacion>();
        Identificacion vCargo = new Identificacion();

        if (txtCodCargo.Text != "")
            vCargo.cod_cargo = Convert.ToInt64(txtCodCargo.Text);
        if (txtDescripcionCargo.Text != "")
            vCargo.descripcion = Convert.ToString(txtDescripcionCargo.Text);

        lstCargos = identificacionServicio.ListarCargosEntidad(vCargo, (Usuario)Session["usuario"]);
        if (lstCargos.Count > 0)
        {
            panelGrilla.Visible = true;
            gvCargo.DataSource = lstCargos;
            gvCargo.DataBind();
            lblTotalRegs.Text = "Registros encontrados: " + lstCargos.Count;
        }
        else
        {
            panelGrilla.Visible = false;
            lblTotalRegs.Text = "La consulta no obtuvo resultado";
        }

    }

    protected void gvCargo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            VerError("");
            Identificacion pCargo = new Identificacion();
            List<Identificacion> lstCausas = new List<Identificacion>();
            List<Seguimiento> lstControl = new List<Seguimiento>();
            List<Seguimiento> lstMonitoreo = new List<Seguimiento>();
            Seguimiento pSeguimiento = new Seguimiento();

            pCargo.cod_cargo = Convert.ToInt64(gvCargo.DataKeys[e.RowIndex].Values[0]);

            lstCausas = identificacionServicio.ListarCausas(pCargo, "", (Usuario)Session["usuario"]);

            pSeguimiento.cod_cargo = Convert.ToInt64(gvCargo.DataKeys[e.RowIndex].Values[0]);
            lstControl = seguimientoServicio.ListarFormasControl(pSeguimiento, (Usuario)Session["usuario"]);
            lstMonitoreo = seguimientoServicio.ListarTiposMonitoreo(pSeguimiento, (Usuario)Session["usuario"]);
            
            if (lstCausas.Count == 0 && lstControl.Count == 0 && lstMonitoreo.Count == 0)
            {
                identificacionServicio.EliminarCargo(pCargo, (Usuario)Session["usuario"]);
                Actualizar();
            }
            else if (lstCausas.Count > 0 || lstControl.Count > 0 || lstMonitoreo.Count > 0)
            {
                VerError("No puede eliminar el cargo, hay procesos asociados");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaC, "gvCargo_RowDeleting", ex);
        }
    }

    protected void gvCargo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //String id = gvCargo.Rows[e.NewEditIndex].Cells[2].Text;
        string id = gvCargo.DataKeys[e.NewEditIndex].Value.ToString();
        Session[identificacionServicio.CodigoProgramaC + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvCargo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCargo.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaC, "gvCargo_PageIndexChanging", ex);
        }
    }
}