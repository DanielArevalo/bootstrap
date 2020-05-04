using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;

public partial class Lista : GlobalWeb
{
    #region Métodos Iniciales
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            SaldosTercerosService SaldoTerceros = new SaldosTercerosService();
            VisualizarOpciones(SaldoTerceros.CodigoProgramaTraslado, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            LimpiarValoresConsulta(pConsulta, "");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Configuracion conf = new Configuracion();
                LlenarCombos();
                Session["cuenta_traslado"] = null;
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("", "Page_Load", ex);
        }
    }

    protected void LlenarCombos()
    {
        // Llenar el DDL de la fecha de corte 
        Configuracion conf = new Configuracion();
        List<Xpinn.Contabilidad.Entities.BalanceGeneral> lstFechaCierre = new List<Xpinn.Contabilidad.Entities.BalanceGeneral>();
        Xpinn.Contabilidad.Services.BalanceGeneralService BalancePruebaService = new Xpinn.Contabilidad.Services.BalanceGeneralService();
        Xpinn.Contabilidad.Entities.BalanceGeneral BalancePrueba = new Xpinn.Contabilidad.Entities.BalanceGeneral();
        lstFechaCierre = BalancePruebaService.ListarFechaCierre("N", "", (Usuario)Session["usuario"]);
        ddlFechaCorte.DataSource = lstFechaCierre;
        ddlFechaCorte.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataBind();
    }

    #endregion

    #region Eventos de Botones

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, "");
        gvLista.DataSource = null;
        gvLista.DataBind();
        txtCodCuenta.Enabled = true;
        ddlFechaCorte.Enabled = true;
    }

    #endregion

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("", "gvLista_PageIndexChanging", ex);
        }
    }

    protected void Actualizar()
    {
        try
        {
            PlanCuentasService PlanCuentasServicio = new PlanCuentasService();
            List<PlanCuentas> lstCuentas = new List<PlanCuentas>();
            string filtro = "";
            if (txtCodCuenta.Text != "")
                filtro += " And Cod_Cuenta Like '"+ txtCodCuenta.Text+"%'";

            lstCuentas = PlanCuentasServicio.ListarCuentasTraslado(filtro, Convert.ToDateTime(ddlFechaCorte.SelectedValue), (Usuario)Session["usuario"]);

            if(lstCuentas.Count > 0)
            {
                pListado.Visible = true;
                gvLista.DataSource = lstCuentas;
                gvLista.DataBind();
                lblTotalRegs.Text = "Registros encontrados: " + lstCuentas.Count();
            }else
            {
                lblInfo.Visible = true;
                pListado.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("", "Actualizar", ex);
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string cod_cuenta = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        if (cod_cuenta == null)
        {
            VerError("Debe seleccionar la cuenta");
            return;
        }
        Session["cuenta_traslado"] = cod_cuenta;
        Session["fecha_traslado"] = ddlFechaCorte.SelectedValue;
        Navegar(Pagina.Nuevo);
    }

    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        try
        {            
            PlanCuentasService PlanCuentasServicio = new PlanCuentasService();
            PlanCuentas pCuenta = new PlanCuentas();
            pCuenta = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text,(Usuario)Session["usuario"]);
            txtNomCuenta.Text = pCuenta.nombre;
            Actualizar();

            if(pCuenta == null || gvLista.Rows.Count == 0)
                VerError("La cuenta no existe o no maneja traslado de saldos");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("", "Actualizar", ex);
        }

    }
}