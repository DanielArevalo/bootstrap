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
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;


partial class Lista : GlobalWeb
{
    ConciliacionBancariaServices ConciliacionServ = new ConciliacionBancariaServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {       
            //VisualizarOpciones(ConciliacionServ.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConciliacionServ.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConciliacionServ.CodigoPrograma, "Page_Load", ex);
        }
    }

   

    void cargarDropdown()
    {
        ConciliacionBancaria vConci = new ConciliacionBancaria();
        List<ConciliacionBancaria> lstCuentas = new List<ConciliacionBancaria>();

        lstCuentas = ConciliacionServ.ListarCuentasBancarias(vConci, (Usuario)Session["usuario"]);
        if (lstCuentas.Count > 0)
        {
            ddlCuentaBanc.DataSource = lstCuentas;
            ddlCuentaBanc.DataTextField = "NUM_CUENTA";
            ddlCuentaBanc.DataValueField = "IDCTABANCARIA";
            ddlCuentaBanc.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlCuentaBanc.SelectedIndex = 0;
            ddlCuentaBanc.DataBind();
        }

        List<ConciliacionBancaria> lstPlan = new List<ConciliacionBancaria>();

        lstPlan = ConciliacionServ.ListarPlanCuentas(vConci, (Usuario)Session["usuario"]);
        if (lstPlan.Count > 0)
        {
            ddlCuentaCont.DataSource = lstPlan;
            ddlCuentaCont.DataTextField = "COD_CUENTA";
            ddlCuentaCont.DataValueField = "COD_CUENTA";
            ddlCuentaCont.Items.Insert(0, new ListItem("Seleccion un item","0"));
            ddlCuentaCont.SelectedIndex = 0;
            ddlCuentaCont.DataBind();
        }
    }



    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            Actualizar();
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(ConciliacionServ.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;         
        Session[ConciliacionServ.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación de la cuenta?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            ConciliacionServ.EliminarConciliacion(Convert.ToInt32(Session["ID"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConciliacionServ.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<ConciliacionBancaria> lstConsulta = new List<ConciliacionBancaria>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFechaIni, pFechaFin;
            pFechaIni = txtIngresoIni.ToDateTime == null ? DateTime.MinValue : txtIngresoIni.ToDateTime;
            pFechaFin = txtIngresoFin.ToDateTime == null ? DateTime.MinValue : txtIngresoFin.ToDateTime;
            
            lstConsulta = ConciliacionServ.ListarConciliacion(filtro, pFechaIni, pFechaFin,(Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();                
               
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }
            Session.Add(ConciliacionServ.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConciliacionServ.CodigoPrograma, "Actualizar", ex);
        }
    }

    private ConciliacionBancaria ObtenerValores()
    {
        ConciliacionBancaria vConci = new ConciliacionBancaria();
        if (ddlCuentaBanc.SelectedIndex != 0)
            vConci.idctabancaria = Convert.ToInt32(ddlCuentaBanc.SelectedValue);
        if (ddlCuentaCont.SelectedIndex != 0)
            vConci.cod_cuenta = ddlCuentaCont.SelectedValue;

        return vConci;
    }



    private string obtFiltro(ConciliacionBancaria Cuentas)
    {
        String filtro = String.Empty;

        if (ddlCuentaBanc.SelectedIndex != 0)
            filtro += " and Concbancaria.IDCTABANCARIA = " + Cuentas.idctabancaria;
        if (ddlCuentaCont.SelectedIndex != 0)
            filtro += " and Cuenta_Bancaria.COD_CUENTA = '" + Cuentas.cod_cuenta+"'";       
       
        return filtro;
    }


   
}