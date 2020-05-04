using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;

public delegate void txtTasa_ActionsDelegate(object sender, EventArgs e);

public partial class ctlTasa : System.Web.UI.UserControl
{

    public event txtTasa_ActionsDelegate eventoCambiar;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rbCalculoTasa_SelectedIndexChanged(rbCalculoTasa, null);
        }
    }

    public void Inicializar()
    {
        // Llena el DDL de los tipos de tasas
        List<TipoTasa> lstTipoTasa = new List<TipoTasa>();
        TipoTasaService TipoTasaServicios = new TipoTasaService();
        TipoTasa vTipoTasa = new TipoTasa();
        lstTipoTasa = TipoTasaServicios.ListarTipoTasa(vTipoTasa, (Usuario)Session["Usuario"]);
        ddlTipoTasa.DataSource = lstTipoTasa;
        ddlTipoTasa.DataTextField = "nombre";
        ddlTipoTasa.DataValueField = "cod_tipo_tasa";
        ddlTipoTasa.DataBind();
        ListItem selectedListItem1 = ddlTipoTasa.Items.FindByValue("2");
        if (selectedListItem1 != null)
            selectedListItem1.Selected = true;

        // Llena el DDL de los tipos de tasas historicas
        List<TipoTasaHist> lstTipoTasaHist = new List<TipoTasaHist>();
        TipoTasaHistService TipoTasaHistServicios = new TipoTasaHistService();
        TipoTasaHist vTipoTasaHist = new TipoTasaHist();
        lstTipoTasaHist = TipoTasaHistServicios.ListarTipoTasaHist(vTipoTasaHist, (Usuario)Session["Usuario"]);
        ddlHistorico.DataSource = lstTipoTasaHist;
        ddlHistorico.DataTextField = "descripcion";
        ddlHistorico.DataValueField = "tipo_historico";
        ddlHistorico.DataBind();

        // Inicializar datos
        rbCalculoTasa.SelectedValue = "0";
        txtTasa.Text = "null";
        txtDesviacion.Text = "0";
        panelFijo.Visible = false;
        panelHistorico.Visible = false;
    }

    /// <summary>
    /// Método para seleccionar el tipo de tasa
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbCalculoTasa_SelectedIndexChanged(object sender, EventArgs e)
    {
        panelFijo.Visible = false;
        panelHistorico.Visible = false;
        if (rbCalculoTasa.SelectedIndex == 0)        
            panelFijo.Visible = false;
        if (rbCalculoTasa.SelectedIndex == 1)
            panelFijo.Visible = true;
        if (rbCalculoTasa.SelectedIndex == 2)
            panelHistorico.Visible = true;
        if (rbCalculoTasa.SelectedIndex == 3)
            panelHistorico.Visible = true;
    }

    public int Indice
    {
        set { rbCalculoTasa.SelectedIndex = value; }
        get { return rbCalculoTasa.SelectedIndex; }
    }

    public string FormaTasa
    {
        set
        {
            try
            {
                rbCalculoTasa.SelectedIndex = Convert.ToInt32(value);
            }
            catch
            {
                return;
            }
            ViewState["rbCalculoTasa"] = value;
            rbCalculoTasa_SelectedIndexChanged(rbCalculoTasa, null);
        }
        get { return rbCalculoTasa.SelectedValue; }
    }

    public int TipoHistorico
    {
        set { ddlHistorico.SelectedValue = value.ToString(); ViewState["ddlHistorico"] = value; }
        get { return Convert.ToInt32(ddlHistorico.SelectedValue); }
    }

    public decimal Desviacion
    {
        set { txtDesviacion.Text = value.ToString(); ViewState["txtDesviacion"] = value; }
        get { return Convert.ToDecimal(txtDesviacion.Text); }
    }

    public int TipoTasa
    {
        set { ddlTipoTasa.SelectedValue = value.ToString(); ViewState["ddlTipoTasa"] = value; }
        get { return Convert.ToInt32(ddlTipoTasa.SelectedValue); }
    }

    public decimal Tasa
    {
        set { txtTasa.Text = value.ToString(); ViewState["txtTasa"] = value; }
        get
        {
            if (txtTasa.Text == null)
                return 0;
            if (txtTasa.Text.Trim() == "")
                return 0;
            GlobalWeb global = new GlobalWeb();
            return global.ConvertirStringToDecimal(txtTasa.Text);
            // return Convert.ToDecimal(txtTasa.Text);
        }
    }

    public bool VisibleHistorico
    {
        set { panelHistorico.Visible = value; ViewState["panelHistorico"] = value; }
        get { return panelHistorico.Visible; }
    }

    public bool VisibleFijo
    {
        set { panelFijo.Visible = value; ViewState["panelFijo"] = value; }
        get { return panelFijo.Visible; }
    }

    public void txtTasa_TextChanged(object sender, EventArgs e)
    {
        if (eventoCambiar != null)
            eventoCambiar(sender, e);
    }



    public void Enabled_Tasa(Boolean b) 
    {

       txtTasa.ReadOnly = b;
       txtTasa.Enabled = b;
    }
    public void Enabled_Campos(Boolean b)
    {

   
        txtTasa.Enabled = b;
        ddlHistorico.Enabled = b;
        ddlTipoTasa.Enabled = b;
        txtDesviacion.Enabled = b;
    }


}