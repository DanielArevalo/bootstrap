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
using Xpinn.NIIF.Services;
using Xpinn.NIIF.Entities;
using System.IO;

partial class Lista : GlobalWeb
{
    PlanCuentasNIIFService PlanNIIFServicio = new PlanCuentasNIIFService();
    private Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
    Xpinn.Contabilidad.Services.BalanceGeneralService BalanceGeneral = new Xpinn.Contabilidad.Services.BalanceGeneralService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            Site toolBar = (Site)this.Master;
            if (Request.QueryString["modificar"] != null)
            {
                if (Request.QueryString["modificar"].ToString().Trim() == "1")
                {
                    PlanNIIFServicio.CodigoProgramaPlan = PlanNIIFServicio.CodigoProgramaUpdate;
                    toolBar.MostrarImportar(false);
                    toolBar.MostrarNuevo(false);
                }
            }

            VisualizarOpciones(PlanNIIFServicio.CodigoProgramaPlan, "L");
            toolBar.eventoNuevo += btNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanNIIFServicio.CodigoProgramaPlan, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["lstData"] = null;
                Session["lstDataBalance"] = null;

                CargarValoresConsulta(pConsulta, PlanNIIFServicio.CodigoProgramaPlan);
                if (Session[PlanNIIFServicio.CodigoProgramaPlan + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanNIIFServicio.CodigoProgramaPlan, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(PlanNIIFServicio.CodigoProgramaPlan + ".id");
        Session.Remove(PlanNIIFServicio.CodigoProgramaUpdate + ".id");
        GuardarValoresConsulta(pConsulta, PlanNIIFServicio.CodigoProgramaPlan);
        Navegar(Pagina.Nuevo);
    }

    protected void btNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        GuardarValoresConsulta(pConsulta, PlanNIIFServicio.CodigoProgramaPlan);
        Actualizar();        
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        LimpiarValoresConsulta(pConsulta, PlanNIIFServicio.CodigoProgramaPlan);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanNIIFServicio.CodigoProgramaPlan + "L", "gvLista_RowDataBound", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id;
        id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[PlanNIIFServicio.CodigoProgramaPlan + ".id"] = id;
        Session[PlanNIIFServicio.CodigoProgramaUpdate + ".id"] = id;
        //Session[PlanCuentasServicio.CodigoProgramaModif + ".tipo"] = tipo;
        e.NewEditIndex = -1;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        VerError("");
        try
        {
            string id = e.Keys[0].ToString();
            if (id.Trim() != "")
                try
                {
                    PlanNIIFServicio.EliminarPlanCuentasNIIF(id, (Usuario)Session["usuario"]);
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                }
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanNIIFServicio.CodigoProgramaPlan, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(PlanNIIFServicio.CodigoProgramaPlan, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Contabilidad.Entities.PlanCuentas> lstConsulta = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
            string filtro = obtFiltro();
            lstConsulta = PlanCuentasServicio.ListarPlanCuentasNif(ObtenerValores(), (Usuario)Session["usuario"], filtro);
                
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

            Session.Add(PlanNIIFServicio.CodigoProgramaPlan + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanNIIFServicio.CodigoProgramaPlan, "Actualizar", ex);
        }
    }

    public string obtFiltro()
    { 
        string filtro = "";
        if (txtCodCuenta.Text != "")
            filtro += " and plan_cuentas_niif.cod_cuenta_niif like '" + txtCodCuenta.Text + "%'";
            
        return filtro;
    }

    private Xpinn.Contabilidad.Entities.PlanCuentas ObtenerValores()
    {
        Xpinn.Contabilidad.Entities.PlanCuentas vPlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        if (txtNombre.Text.Trim() != "")
            vPlanCuentas.nombre_niif = Convert.ToString(txtNombre.Text.Trim());
        if (ddlDepende.Text.Trim() != "")
            vPlanCuentas.depende_de_niif = ddlDepende.SelectedValue;        
        if (txtNivel.Text.Trim() != "")
            vPlanCuentas.nivel = Convert.ToInt64(txtNivel.Text.Trim());        
        if (ddlEstado.Text.Trim() != "")
            vPlanCuentas.estado = Convert.ToInt64(ddlEstado.SelectedValue.ToString());
        if (chkTerceros.Checked == true)
            vPlanCuentas.maneja_ter = 1;
        if (chkCentroCosto.Checked == true)
            vPlanCuentas.maneja_cc = 1;
        if (chkCentroGestion.Checked == true)
            vPlanCuentas.maneja_sc = 1;
        if (chkImpuestos.Checked == true)
            vPlanCuentas.impuesto = 1;
        if (chkGiro.Checked == true)
            vPlanCuentas.maneja_gir = 1;
        if (chkReportarMayor.Checked == true)
            vPlanCuentas.reportarmayor = 1;
        if (ddlTipo.SelectedIndex != 0)
            vPlanCuentas.tipo = ddlTipo.SelectedValue;
        return vPlanCuentas;
    }


}