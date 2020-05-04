using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

public delegate void txtCuenta_TextChanged_ActionsDelegate(object sender, EventArgs e);

public partial class ctlPlanCuentas : System.Web.UI.UserControl
{
    public event EventHandler<EventArgs> eventotxtCuenta_TextChanged;
    public AjaxControlToolkit.ModalPopupExtender pmpePlanCuentas { private set; get; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfCodigo.Value = null;
            hfNombre.Value = null;
        }
    }

    public void Motrar(Boolean pMostrar, String pctlCodigo, String pctlNombre)
    {
        InicializarGridPlanCuentas();
        panelBusquedaRapida.Visible = pMostrar;
        hfCodigo.Value = pctlCodigo;
        hfNombre.Value = pctlNombre;
        ViewState["MostrarBusqueda"] = pMostrar;
    }

    public void InicializarGridPlanCuentas()
    {
        try
        {
            List<Xpinn.Contabilidad.Entities.PlanCuentas> lstConsulta = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
            Xpinn.Contabilidad.Entities.PlanCuentas ePlan = new Xpinn.Contabilidad.Entities.PlanCuentas();
            for (int i = 0; i <= 10; i++)
            {
                lstConsulta.Add(ePlan);
            }
            gvPlanCuentas.DataSource = lstConsulta;
            gvPlanCuentas.DataBind();
            gvPlanCuentas.Visible = true;
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    public void ActualizarGridPlanCuentas()
    {
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentaservice = new Xpinn.Contabilidad.Services.PlanCuentasService();
        Xpinn.Contabilidad.Entities.PlanCuentas plan = new Xpinn.Contabilidad.Entities.PlanCuentas();
        try
        {
            string filtro = "";
            if (txtCod.Text.Trim() != "")
                filtro += " cod_cuenta Like '" + txtCod.Text + "%' ";
            if (txtNom.Text.Trim() != "")
                filtro += (filtro.Trim() != "" ? " And " : "") + " nombre Like '" + txtNom.Text + "%' ";
            List<Xpinn.Contabilidad.Entities.PlanCuentas> lstConsulta = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
            lstConsulta = PlanCuentaservice.ListarPlanCuentasLocal(plan, (Usuario)Session["usuario"], filtro);
            gvPlanCuentas.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvPlanCuentas.Visible = true;
                gvPlanCuentas.DataBind();
            }
            else
            {
                gvPlanCuentas.Visible = false;
                InicializarGridPlanCuentas();
            }

            Session.Add(PlanCuentaservice.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ActualizarGridPlanCuentas();
    }

    protected void gvPlanCuentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPlanCuentas.PageIndex = e.NewPageIndex;
            ActualizarGridPlanCuentas();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    protected void btnSeleccionar_Click(object sender, ImageClickEventArgs e)
    {
        ImageButtonGrid btnSeleccionar = (ImageButtonGrid)sender;
        if (btnSeleccionar != null)
        {
            int rowIndex = Convert.ToInt32(btnSeleccionar.CommandArgument);
            rowIndex = rowIndex - (gvPlanCuentas.PageIndex * gvPlanCuentas.PageSize);
            gvPlanCuentas.Rows[rowIndex].BackColor = System.Drawing.Color.DarkBlue;
            gvPlanCuentas.Rows[rowIndex].ForeColor = System.Drawing.Color.White;
            panelBusquedaRapida.Visible = false;
            if (hfCodigo.Value != "")
            {
                TextBoxGrid txtCodigo = new TextBoxGrid();
                if (this.NamingContainer.ToString().Contains("GridView"))
                {
                    GridViewRow gvRow = (GridViewRow)this.NamingContainer;
                    txtCodigo = (TextBoxGrid)gvRow.FindControl(hfCodigo.Value);
                }
                else if (this.NamingContainer.ToString().Contains("ContentPlaceHolder"))
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    txtCodigo = (TextBoxGrid)mpContentPlaceHolder.FindControl(hfCodigo.Value);
                }
                else if(this.NamingContainer.ToString().Contains("TabPanel"))
                {
                    AjaxControlToolkit.TabPanel ptabPanel = (AjaxControlToolkit.TabPanel)this.NamingContainer;
                    txtCodigo = (TextBoxGrid)ptabPanel.FindControl(hfCodigo.Value);
                }
                if (txtCodigo != null)
                {
                    txtCodigo.Text = gvPlanCuentas.Rows[rowIndex].Cells[1].Text;
                }
                
            }
            if (hfNombre.Value != "")
            {
                TextBoxGrid txtNombres = new TextBoxGrid();
                if (this.NamingContainer.ToString().Contains("GridView"))
                {
                    GridViewRow gvRow = (GridViewRow)this.NamingContainer;
                    txtNombres = (TextBoxGrid)gvRow.FindControl(hfNombre.Value);
                }
                else if (this.NamingContainer.ToString().Contains("ContentPlaceHolder"))
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    txtNombres = (TextBoxGrid)mpContentPlaceHolder.FindControl(hfNombre.Value);
                }
                else if (this.NamingContainer.ToString().Contains("TabPanel"))
                {
                    AjaxControlToolkit.TabPanel ptabPanel = (AjaxControlToolkit.TabPanel)this.NamingContainer;
                    txtNombres = (TextBoxGrid)ptabPanel.FindControl(hfNombre.Value);
                }
                if (txtNombres != null)
                {
                    txtNombres.Text = gvPlanCuentas.Rows[rowIndex].Cells[2].Text;
                }
            }
        }
        if (eventotxtCuenta_TextChanged != null)
            eventotxtCuenta_TextChanged(gvPlanCuentas, e);
    }

    protected void gvPlanCuentas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string CodCta = e.Row.Cells[1].Text;
                if (CodCta == "0" || CodCta.Trim() == "")
                {
                    ImageButtonGrid btnSeleccionar = (ImageButtonGrid)e.Row.FindControl("btnSeleccionar");
                    if (btnSeleccionar != null)
                    {
                        btnSeleccionar.Visible = false;
                        e.Row.Cells[1].Text = "";
                    }
                }
            }
            catch (Exception ex)
            { string s = ex.Message; }
        }
    }

    protected void bntCerrar_Click(object sender, ImageClickEventArgs e)
    {
        panelBusquedaRapida.Visible = false;
    }
}