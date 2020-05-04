using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

public delegate void txtAvances_TextChanged_ActionsDelegate(object sender, EventArgs e);

public partial class ListadoAvances : System.Web.UI.UserControl
{
    public AjaxControlToolkit.ModalPopupExtender pmpeAvances { private set; get; }
    public event txtAvances_TextChanged_ActionsDelegate eventotxtAvances_TextChanged;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfRadicacion.Value = null;
            hfIdAvance.Value = null;
            hfControl.Value = null;
        }
    }

    public Int64? Radicado
    {
        get { return Convert.ToInt64(hfRadicacion.Value); }
        set { hfRadicacion.Value = Convert.ToString(value); }
    }

    public string Avances
    {
        get { return Convert.ToString(hfIdAvance.Value); }
        set { hfIdAvance.Value = Convert.ToString(value); }
    }

    public Boolean Mostrado
    {
        get { return Convert.ToBoolean(hfControl.Value); }
        set { hfControl.Value = Convert.ToString(value); }
    }

    public void Motrar(Boolean pMostrar, String pctNumeroRadicacion, string pctIdAvances, string pctValorAPagar)
    {
        InicializarGridPersonas(pctNumeroRadicacion);
        panelAvances.Visible = pMostrar;
        Mostrado = pMostrar;
        hfRadicacion.Value = pctNumeroRadicacion;
        hfIdAvance.Value = pctIdAvances;
        hfValorAPagar.Value = pctValorAPagar;
        ViewState["MostrarAvances"] = pMostrar;
    }

   
    public void InicializarGridPersonas(string pNumProducto)
    {
        try
        {
            Xpinn.Asesores.Services.DetalleProductoService DetalleProducto = new Xpinn.Asesores.Services.DetalleProductoService();
            List<Xpinn.Asesores.Entities.ConsultaAvance> ListaDetalleAvance = new List<Xpinn.Asesores.Entities.ConsultaAvance>();
            ListaDetalleAvance = DetalleProducto.ListarAvances(long.Parse(pNumProducto), " And saldo_avance != 0 ", (Usuario)Session["Usuario"]);
            if (ListaDetalleAvance.Count > 0)
            {
                gvAvances.DataSource = ListaDetalleAvance;
                gvAvances.DataBind();
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }


    protected void gvAvances_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAvances.PageIndex = e.NewPageIndex;
            InicializarGridPersonas(this.Radicado.ToString());
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    protected void btnCerrar_Click(object sender, ImageClickEventArgs e)
    {
        Mostrado = false;
        panelAvances.Visible = false;
    }

    protected void bntAceptar_Click(object sender, ImageClickEventArgs e)
    {
        Configuracion conf = new Configuracion();
        string radicacion = "";
        string idavancesseleccionados = "";
        decimal totalvalorapagar = 0;
        foreach (GridViewRow rfila in gvAvances.Rows)
        {
            CheckBoxGrid cbListado = (CheckBoxGrid)rfila.FindControl("cbListado");
            if (cbListado != null)
            {
                if (cbListado.Checked)
                {
                    idavancesseleccionados += (idavancesseleccionados == "" ? "" : ";") + rfila.Cells[1].Text;
                    radicacion = rfila.Cells[9].Text;
                    // Determinar saldo para el avance
                    decimal saldoavance = 0;
                    try { saldoavance = Convert.ToDecimal(rfila.Cells[7].Text.Replace("$", "").Replace(conf.ObtenerSeparadorMilesConfig(), "")); } catch { saldoavance = 0; }
                    // Determinar valor a pagar para el avance
                    decimal valorapagar = 0;
                    try { valorapagar = Convert.ToDecimal(rfila.Cells[8].Text.Replace("$", "").Replace(conf.ObtenerSeparadorMilesConfig(), "")); } catch { valorapagar = 0; }
                    // sumar el valor a pagar
                    if (valorapagar == 0 || valorapagar < saldoavance)
                        valorapagar = saldoavance;
                     totalvalorapagar += valorapagar;
                }
            }
        }
        if (hfIdAvance.Value != "")
        {            
            TextBox txtAvances;
            if (this.NamingContainer.ToString().Contains("GridViewRow"))
            {
                GridViewRow mpContentPlaceHolder = (GridViewRow)this.NamingContainer;
                txtAvances = (TextBox)mpContentPlaceHolder.FindControl(hfIdAvance.Value);
            }
            else if (this.NamingContainer.ToString().Contains("System.Web.UI.WebControls.ContentPlaceHolder"))
            {
                txtAvances = (TextBox)this.NamingContainer.FindControl(hfIdAvance.Value);
                if (txtAvances == null)
                {
                    ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                    txtAvances = (TextBox)mpContentPlaceHolder.FindControl(hfIdAvance.Value);
                }
            }
            else
            {
                ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                txtAvances = (TextBox)mpContentPlaceHolder.FindControl(hfIdAvance.Value);
            }
            if (txtAvances != null)
            {
                txtAvances.Text = idavancesseleccionados;
            }
        }
        if (hfValorAPagar.Value != "")
        {
            ctlNumeroConDecimales txtValorAPagar = null;
            decimales txtValorAPagarDec = null;
            if (this.NamingContainer.ToString().Contains("GridViewRow"))
            {
                GridViewRow mpContentPlaceHolder = (GridViewRow)this.NamingContainer;
                txtValorAPagar = (ctlNumeroConDecimales)mpContentPlaceHolder.FindControl(hfValorAPagar.Value);
            }
            else if (this.NamingContainer.ToString().Contains("System.Web.UI.WebControls.ContentPlaceHolder"))
            {
                try
                { 
                    txtValorAPagar = (ctlNumeroConDecimales)this.NamingContainer.FindControl(hfValorAPagar.Value);
                    if (txtValorAPagar == null)
                    {
                        ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                        txtValorAPagar = (ctlNumeroConDecimales)mpContentPlaceHolder.FindControl(hfValorAPagar.Value);
                    }
                }
                catch
                {
                    txtValorAPagarDec = (decimales)this.NamingContainer.FindControl(hfValorAPagar.Value);
                    if (txtValorAPagarDec == null)
                    {
                        ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                        txtValorAPagarDec = (decimales)mpContentPlaceHolder.FindControl(hfValorAPagar.Value);
                    }
                }
            }
            else
            {
                ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)this.NamingContainer;
                txtValorAPagar = (ctlNumeroConDecimales)mpContentPlaceHolder.FindControl(hfValorAPagar.Value);
            }
            if (txtValorAPagar != null)
            {
                txtValorAPagar.Text = totalvalorapagar.ToString("N").Replace(",00","");
            }
            if (txtValorAPagarDec != null)
            {
                txtValorAPagarDec.Text = totalvalorapagar.ToString("N").Replace(",00", "");
            }
        }
        Mostrado = false;
        panelAvances.Visible = false;
    }

}
