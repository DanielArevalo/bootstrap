using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;

public partial class ctlCuentasProductos : System.Web.UI.UserControl
{
    Xpinn.Ahorros.Services.CuentasExentasServices CuentasServices = new Xpinn.Ahorros.Services.CuentasExentasServices();
    public AjaxControlToolkit.ModalPopupExtender pmpePlanCuentas { private set; get; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfTipoProd.Value = null;
            hfNroProduc.Value = null;
            hfCodigo.Value = null;
            hfIdentificacion.Value = null;
            hfNombre.Value = null;
            hfLinea.Value = null;
            hfOficina.Value = null;
        }
    }

    public void Mostrar(Boolean pMostrar, String pctlTipoProducto, String pctlNumeroProduc, String pctlCodigo, String pctlIdentificacion, String pctlNombre, String pctlLinea, String pctlCodOficina)
    {
        InicializarGridProductos();
        panelBusquedaRapida.Visible = pMostrar;
        hfTipoProd.Value = pctlTipoProducto;
        TipoDeProducto TipoProducto = hfTipoProd.Value.ToEnum<TipoDeProducto>();
        lblTipoProduct.Text = TipoProducto.ToString();
        hfNroProduc.Value = pctlNumeroProduc;
        hfCodigo.Value = pctlCodigo;
        hfIdentificacion.Value = pctlIdentificacion;
        hfNombre.Value = pctlNombre;
        hfLinea.Value = pctlLinea;
        hfOficina.Value = pctlCodOficina;
        ViewState["MostrarBusqueda"] = pMostrar;
    }

    public void InicializarGridProductos()
    {
        try
        {
            List<CuentasExenta> lstConsulta = new List<CuentasExenta>();
            CuentasExenta eCuenta = new CuentasExenta();
            for (int i = 0; i <= 15; i++)
            {
                lstConsulta.Add(eCuenta);
            }
            gvProductos.DataSource = lstConsulta;
            gvProductos.DataBind();
            gvProductos.Visible = true;
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }



    private string obtFilter(TipoDeProducto pType)
    {
        string pFilter = string.Empty;
        try
        {
            if (!string.IsNullOrEmpty(txtNroProduc.Text))
            {
                switch (pType)
                {
                    case TipoDeProducto.Aporte:
                        pFilter += " AND A.NUMERO_APORTE = " + txtNroProduc.Text;
                        break;
                    case TipoDeProducto.Credito:
                        pFilter += " AND C.NUMERO_RADICACION = " + txtNroProduc.Text;
                        break;
                    case TipoDeProducto.AhorrosVista:
                        pFilter += " AND A.NUMERO_CUENTA LIKE '%" + txtNroProduc.Text + "%'";
                        break;
                    case TipoDeProducto.AhorroProgramado:
                        pFilter += " AND A.NUMERO_PROGRAMADO LIKE '%" + txtNroProduc.Text + "%'";
                        break;
                    case TipoDeProducto.CDATS:
                        pFilter += " AND A.NUMERO_CDAT LIKE '%" + txtNroProduc.Text + "%'";
                        break;
                }
            }

            if (!string.IsNullOrEmpty(txtCod.Text))
                pFilter += " AND V.COD_PERSONA LIKE '%" + txtCod.Text + "%' ";
            if (!string.IsNullOrEmpty(txtIdenti.Text))
                pFilter += " AND V.IDENTIFICACION LIKE '%" + txtIdenti.Text + "%' ";
            if (!string.IsNullOrEmpty(txtNom.Text))
                pFilter += " AND UPPER(V.NOMBRE) LIKE '%" + txtNom.Text.ToUpper() + "%'";
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return pFilter;
    }


    public void ActualizarProductos()
    {
        List<CuentasExenta> lstCuentas = new List<CuentasExenta>();
        try
        {
            string  filtro = obtFilter(hfTipoProd.Value.ToEnum<TipoDeProducto>());
            lstCuentas = CuentasServices.ListarProductosControl(Convert.ToInt32(hfTipoProd.Value), filtro, (Usuario)Session["usuario"]);
            gvProductos.DataSource = lstCuentas;
            bool resultData = lstCuentas == null ? false : lstCuentas.Count > 0 ? true : false;
            if (resultData)
            {
                gvProductos.Visible = true;
                gvProductos.DataBind();
            }
            else
            {
                gvProductos.Visible = false;
                InicializarGridProductos();
            }

            Session.Add(CuentasServices.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ActualizarProductos();
    }

    protected void gvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvProductos.PageIndex = e.NewPageIndex;
            ActualizarProductos();
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
            rowIndex = rowIndex - (gvProductos.PageIndex * gvProductos.PageSize);
            gvProductos.Rows[rowIndex].BackColor = System.Drawing.Color.DarkBlue;
            gvProductos.Rows[rowIndex].ForeColor = System.Drawing.Color.White;
            panelBusquedaRapida.Visible = false;
            if (hfNroProduc.Value != "")
            {
                TextBoxGrid txtNroProducto = new TextBoxGrid();
                if (this.NamingContainer.ToString().Contains("GridView"))
                {
                    GridViewRow gvRow = (GridViewRow)this.NamingContainer;
                    txtNroProducto = (TextBoxGrid)gvRow.FindControl(hfNroProduc.Value);
                }
                else if (this.NamingContainer.ToString().Contains("ContentPlaceHolder"))
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    txtNroProducto = (TextBoxGrid)mpContentPlaceHolder.FindControl(hfNroProduc.Value);
                }
                else if (this.NamingContainer.ToString().Contains("TabPanel"))
                {
                    AjaxControlToolkit.TabPanel ptabPanel = (AjaxControlToolkit.TabPanel)this.NamingContainer;
                    txtNroProducto = (TextBoxGrid)ptabPanel.FindControl(hfNroProduc.Value);
                }
                if (txtNroProducto != null)
                {
                    txtNroProducto.Text = gvProductos.Rows[rowIndex].Cells[4].Text;
                }
            }
            if (hfCodigo.Value != "")
            {
                Label txtCodigo = new Label();
                if (this.NamingContainer.ToString().Contains("GridView"))
                {
                    GridViewRow gvRow = (GridViewRow)this.NamingContainer;
                    txtCodigo = (Label)gvRow.FindControl(hfCodigo.Value);
                }
                else if (this.NamingContainer.ToString().Contains("ContentPlaceHolder"))
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    txtCodigo = (Label)mpContentPlaceHolder.FindControl(hfCodigo.Value);
                }
                else if(this.NamingContainer.ToString().Contains("TabPanel"))
                {
                    AjaxControlToolkit.TabPanel ptabPanel = (AjaxControlToolkit.TabPanel)this.NamingContainer;
                    txtCodigo = (Label)ptabPanel.FindControl(hfCodigo.Value);
                }
                if (txtCodigo != null)
                {
                    txtCodigo.Text = gvProductos.Rows[rowIndex].Cells[1].Text;
                }
                
            }
            if (hfIdentificacion.Value != "")
            {
                Label txtIdentificacion = new Label();
                if (this.NamingContainer.ToString().Contains("GridView"))
                {
                    GridViewRow gvRow = (GridViewRow)this.NamingContainer;
                    txtIdentificacion = (Label)gvRow.FindControl(hfIdentificacion.Value);
                }
                else if (this.NamingContainer.ToString().Contains("ContentPlaceHolder"))
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    txtIdentificacion = (Label)mpContentPlaceHolder.FindControl(hfIdentificacion.Value);
                }
                else if (this.NamingContainer.ToString().Contains("TabPanel"))
                {
                    AjaxControlToolkit.TabPanel ptabPanel = (AjaxControlToolkit.TabPanel)this.NamingContainer;
                    txtIdentificacion = (Label)ptabPanel.FindControl(hfIdentificacion.Value);
                }
                if (txtIdentificacion != null)
                {
                    txtIdentificacion.Text = gvProductos.Rows[rowIndex].Cells[2].Text;
                }
            }
            if (hfNombre.Value != "")
            {
                Label txtNombres = new Label();
                if (this.NamingContainer.ToString().Contains("GridView"))
                {
                    GridViewRow gvRow = (GridViewRow)this.NamingContainer;
                    txtNombres = (Label)gvRow.FindControl(hfNombre.Value);
                }
                else if (this.NamingContainer.ToString().Contains("ContentPlaceHolder"))
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    txtNombres = (Label)mpContentPlaceHolder.FindControl(hfNombre.Value);
                }
                else if (this.NamingContainer.ToString().Contains("TabPanel"))
                {
                    AjaxControlToolkit.TabPanel ptabPanel = (AjaxControlToolkit.TabPanel)this.NamingContainer;
                    txtNombres = (Label)ptabPanel.FindControl(hfNombre.Value);
                }
                if (txtNombres != null)
                {
                    txtNombres.Text = gvProductos.Rows[rowIndex].Cells[3].Text;
                }
            }
            if (hfLinea.Value != "")
            {
                Label lblLinea = new Label();
                if (this.NamingContainer.ToString().Contains("GridView"))
                {
                    GridViewRow gvRow = (GridViewRow)this.NamingContainer;
                    lblLinea = (Label)gvRow.FindControl(hfLinea.Value);
                }
                else if (this.NamingContainer.ToString().Contains("ContentPlaceHolder"))
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    lblLinea = (Label)mpContentPlaceHolder.FindControl(hfLinea.Value);
                }
                else if (this.NamingContainer.ToString().Contains("TabPanel"))
                {
                    AjaxControlToolkit.TabPanel ptabPanel = (AjaxControlToolkit.TabPanel)this.NamingContainer;
                    lblLinea = (Label)ptabPanel.FindControl(hfLinea.Value);
                }
                if (lblLinea != null)
                {
                    lblLinea.Text = gvProductos.Rows[rowIndex].Cells[5].Text;
                }
            }
            if (hfOficina.Value != "")
            {
                Label lblOficina = new Label();
                if (this.NamingContainer.ToString().Contains("GridView"))
                {
                    GridViewRow gvRow = (GridViewRow)this.NamingContainer;
                    lblOficina = (Label)gvRow.FindControl(hfOficina.Value);
                }
                else if (this.NamingContainer.ToString().Contains("ContentPlaceHolder"))
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    lblOficina = (Label)mpContentPlaceHolder.FindControl(hfOficina.Value);
                }
                else if (this.NamingContainer.ToString().Contains("TabPanel"))
                {
                    AjaxControlToolkit.TabPanel ptabPanel = (AjaxControlToolkit.TabPanel)this.NamingContainer;
                    lblOficina = (Label)ptabPanel.FindControl(hfOficina.Value);
                }
                if (lblOficina != null)
                {
                    lblOficina.Text = gvProductos.Rows[rowIndex].Cells[6].Text;
                }
            }
        }
    }

    protected void gvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Int64? codPersona = Convert.ToInt64(e.Row.Cells[1].Text);
                if (codPersona == 0)
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