using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.NIIF.Services;
using Xpinn.NIIF.Entities;
using System.Threading;
using System.Globalization;

public partial class Lista : GlobalWeb
{
    Xpinn.NIIF.Services.PlanCuentasNIIFService HomologacionService = new Xpinn.NIIF.Services.PlanCuentasNIIFService();
    Thread tareaEjecucion;
    string estado = ""; 
    DateTime fecha= System.DateTime.MinValue;
    int codusuario = 0;    

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(HomologacionService.CodigoProgramaHomolo, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(HomologacionService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["Error"] = null;
                Session["Proceso"] = null;
                mpeProcesando.Hide();
                mpeFinal.Hide();
                LlenarDropDown();
                Timer1.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(HomologacionService.GetType().Name + "L", "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para llenar los DDLs requeridos para las consultas
    /// </summary>
    protected void LlenarDropDown()
    {
        Xpinn.Contabilidad.Services.TipoComprobanteService TipoComprobanteService = new Xpinn.Contabilidad.Services.TipoComprobanteService();
        Xpinn.Contabilidad.Entities.TipoComprobante TipoComprobante = new Xpinn.Contabilidad.Entities.TipoComprobante();
        ddlTipoComprobante.DataSource = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "", (Usuario)Session["Usuario"]);
        ddlTipoComprobante.DataTextField = "descripcion";
        ddlTipoComprobante.DataValueField = "tipo_comprobante";
        ddlTipoComprobante.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoComprobante.SelectedIndex = 0;
        ddlTipoComprobante.DataBind();
    }


    Boolean validarData()
    {
        if (txtFechaIni.Text == "")
        {
            VerError("Ingrese la fecha inicial del periodo, verifique los datos.");
            return false;
        }
        if (txtFechaFin.Text == "")
        {
            VerError("Ingrese la fecha final del periodo, verifique los datos.");
            return false;
        }
        if (txtFechaIni.ToDateTime > txtFechaFin.ToDateTime)
        {
            VerError("Error al ingresar el rango de fechas, La fecha inicial no puede ser mayor a la fecha final, verifique los datos.");
            return false;
        }
        return true;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (validarData())
            ctlMensaje.MostrarMensaje("Desea generar la homologación para el periodo ingresado?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            IniciarProceso();        
            tareaEjecucion = new Thread(new ThreadStart(EjecutaProceso));
            tareaEjecucion.Start();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(HomologacionService.CodigoProgramaHomolo, "btnContinuarMen_Click", ex);
        }
    }
    

    protected void btnSalir_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    public void IniciarProceso()
    {
        mpeProcesando.Show();        
        Image1.Visible = true;
        Session["Proceso"] = "INICIO";  
        Timer1.Enabled = true;
    }

    public void TerminarProceso()
    {                
        mpeProcesando.Hide();
        Image1.Visible = false;        
        Session.Remove("Proceso");
        Timer1.Enabled = false;
        if (Session["Error"] != null)
        {
            if (Session["Error"].ToString().Trim() != "")
                lblError.Text = Session["Error"].ToString();
            Session.Remove("Error");
        }
        else
            mpeFinal.Show();
    }

    public void EjecutaProceso()
    {
        string sError = "";
        int pTipoComp = ddlTipoComprobante.SelectedIndex != 0 ? Convert.ToInt32(ddlTipoComprobante.SelectedValue) : 0;
        HomologacionService.GenerarHomologacionMovimientos(ref sError, txtFechaIni.ToDateTime, txtFechaFin.ToDateTime, pTipoComp, (Usuario)Session["usuario"]);
        Session["Proceso"] = "FINAL";
        Session["Error"] = sError != "" ? sError : null; 
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (Session["Proceso"] != null)
            if (Session["Proceso"].ToString() == "FINAL")
                TerminarProceso();
            else
                mpeProcesando.Show();
        else
            mpeProcesando.Hide();
    }

   

}

