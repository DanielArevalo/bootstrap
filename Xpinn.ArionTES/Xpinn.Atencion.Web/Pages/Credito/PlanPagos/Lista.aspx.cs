using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lista : GlobalWeb
{

    xpinnWSLogin.Persona1 pPersona;
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient BOCredito = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session["persona"] == null)
                Response.Redirect("~/Pages/Account/FinSesion.htm");

            VisualizarTitulo(OptionsUrl.PlandePagos, "Aso");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("PlanPagos", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        if (!Page.IsPostBack)
        {
            panelGrid.Visible = false;
            ViewState["DatosGrid"] = null;
            Actualizar();
        }
    }


    private void Actualizar()
    {
        try
        {
            VerError("");

            List<xpinnWSEstadoCuenta.ProductoResumen> lstCredito = new List<xpinnWSEstadoCuenta.ProductoResumen>();
            lstCredito = BOCredito.EstadoCuenta(true, pPersona.identificacion.Trim(), pPersona.clavesinecriptar);
            if (lstCredito.Count > 0)
            {
                ViewState.Add("DatosGrid", lstCredito);
                panelGrid.Visible = true;
                gvLista.DataSource = lstCredito;
                gvLista.DataBind();
                lblInfo.Visible = false;
                lblTotReg.Visible = true;
                lblTotReg.Text = "<br/> Registros encontrados " + lstCredito.Count().ToString();
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotReg.Visible = false;
                panelGrid.Visible = false;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex >= 0)
            {
                gvLista.PageIndex = e.NewPageIndex;
                if (ViewState["DatosGrid"] != null)
                {
                    List<xpinnWSEstadoCuenta.ProductoResumen> lstConsulta = new List<xpinnWSEstadoCuenta.ProductoResumen>();
                    lstConsulta = (List<xpinnWSEstadoCuenta.ProductoResumen>)ViewState["DatosGrid"];
                    gvLista.DataSource = lstConsulta;
                    gvLista.DataBind();
                }
                else
                {
                    Actualizar();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Navegar("~/Pages/Credito/PlanPagos/Detalle.aspx?num_radic=" + id);
    }
}