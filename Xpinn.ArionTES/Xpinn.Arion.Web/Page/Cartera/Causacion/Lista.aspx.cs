using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Entities;
using System.Threading;



public partial class Lista : GlobalWeb
{
    Xpinn.Cartera.Services.CierreHistorioService CarteraServicio = new Xpinn.Cartera.Services.CierreHistorioService();    
    Thread tareaEjecucion;
    string estado = "";
    DateTime fecha = System.DateTime.MinValue;
    int codusuario = 0;


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(CarteraServicio.CodigoProgramaCausacion, "L");

            Site toolBar = (Site)this.Master;


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CarteraServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mpeProcesando.Hide();
                mpeFinal.Hide();
                Timer1.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CarteraServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para iniciar la ejecución del proceso
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnEjecutar_Click(object sender, EventArgs e)
    {
        pConsulta.Visible = false;
        mpeNuevo.Show();
    }

    /// <summary>
    /// Método paa confirmar la ejecución del proceso
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinuar_Click(object sender, EventArgs e)
    {              
        IniciarProceso();

        // Ejecutar el proceso de cierre desde el PL  
        Usuario usuap = (Usuario)Session["usuario"];
        codusuario = Convert.ToInt32(usuap.codusuario);
        try
        {
            fecha = Convert.ToDateTime(txtFechaIni.Text);
        }
        catch
        {
            Label1.Visible = true;
            Label1.Text = "Error al convertir la fecha " + txtFechaIni.Text;
        }
        estado = Convert.ToString(rbEstado.SelectedValue);              
        tareaEjecucion = new Thread(new ThreadStart(EjecutaProceso));
        tareaEjecucion.Start();
    }

    /// <summary>
    /// Método para no realizar la ejecución del proceso
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        pConsulta.Visible = true;
        mpeNuevo.Hide();
        mpeProcesando.Hide();
    }

    protected void btnSalir_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    public void IniciarProceso()
    {
        btnContinuar.Enabled = false;
        btnCancelar.Enabled = false;
        mpeNuevo.Hide();
        mpeProcesando.Show();
        Image1.Visible = true;
        Session["Proceso"] = "INICIO";
        Timer1.Enabled = true;
    }

    public void TerminarProceso()
    {
        mpeProcesando.Hide();
        Image1.Visible = false;
        mpeFinal.Show();
        Session.Remove("Proceso");
        Timer1.Enabled = false;
        if (Session["Error"] != null)
        {

            if (Session["Error"].ToString().Trim() != "")
                lblError.Text = Session["Error"].ToString();
            Session.Remove("Error");
        }
    }


    public void EjecutaProceso()
    {
        string sError = "";
        Xpinn.Cartera.Services.CierreHistorioService CarteraServicio = new Xpinn.Cartera.Services.CierreHistorioService();
        CarteraServicio.Causacion(estado, fecha, codusuario, ref sError, (Usuario)Session["usuario"]);
        Session["Proceso"] = "FINAL";
        Session["Error"] = sError;
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

