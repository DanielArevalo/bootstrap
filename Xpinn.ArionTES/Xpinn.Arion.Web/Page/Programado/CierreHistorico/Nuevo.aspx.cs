using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Programado.Services;
using Xpinn.Programado.Entities;
using System.Threading;


public partial class Nuevo : GlobalWeb
{

    Xpinn.Programado.Services.CierreHisProgramadoService ProgramadoServicio = new Xpinn.Programado.Services.CierreHisProgramadoService();
     Thread tareaEjecucion;
    string estado = "";
    DateTime fecha = System.DateTime.MinValue;
    int codusuario = 0;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(ProgramadoServicio.CodigoProgramaHis, "L");

            Site toolBar = (Site)this.Master;
         
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProgramadoServicio.GetType().Name + "L", "Page_PreInit", ex);
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
                LlenarCombos();
                Timer1.Enabled = false;
              
            }        

                GridView grv = (GridView)error.FindControl("gvLista");
                if (grv.Rows.Count > 0)
                {
                    grv.Visible = true;
                    mpeErrores.Show();
                }

                Label2.Visible = true;
                Label2.Text = Convert.ToString(Session["MensajeProgramado"]);
              
          
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProgramadoServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    // <summary>
     //Método para llenar los DDLs requeridos para las consultas
     //</summary>
    protected void LlenarCombos()
    {
         //Llenar el DDL de la fecha de corte 
        Configuracion conf = new Configuracion();
        List<Xpinn.Comun.Entities.Cierea> lstFechaCierre = new List<Xpinn.Comun.Entities.Cierea>();
        lstFechaCierre = ProgramadoServicio.ListarFechaCierre((Usuario)Session["Usuario"]);
        ddlFechaCorte.DataSource = lstFechaCierre;
        ddlFechaCorte.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataBind();
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
            String format = "dd/MM/yyyy";
            fecha = DateTime.ParseExact(ddlFechaCorte.SelectedValue, format, CultureInfo.InvariantCulture);        
           
        }
        catch
        {
            Label1.Visible = true;
            Label1.Text = "Error al convertir la fecha " + ddlFechaCorte.SelectedValue;
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
    
        // if (Session["Error"].ToString().Trim() != "")
        Label2.Text = Session["MensajeProgramado"].ToString();
    }

  

    public void EjecutaProceso()
    {
       

        string sError = "";
        Xpinn.Programado.Services.CierreHisProgramadoService ProgramadoServicio = new Xpinn.Programado.Services.CierreHisProgramadoService();

        Xpinn.Servicios.Services.CierreHistorioService ServiciosServicio = new Xpinn.Servicios.Services.CierreHistorioService();
        Xpinn.Programado.Entities.CierreHistorico programado = new Xpinn.Programado.Entities.CierreHistorico();
        try
        {
            ProgramadoServicio.CierreHistorico(programado,estado, fecha, codusuario, ref sError, (Usuario)Session["usuario"]);
            Session["MensajeProgramado"] = "Cierre Mensual Terminado Correctamente";
            Session["Proceso"] = "FINAL";            
        }
        catch (Exception ex)
        {
            int n = 1;
            if (ex.Message.Contains("ORA-20101:"))
                n = ex.Message.IndexOf("ORA-20101:") + 1;
            if (n > 0 || sError.Contains("ORA-20101:"))
            {
                lblError.Text = ex.Message.Substring(n, ex.Message.Length - n);                
                Session["MensajeProgramado"] = lblError.Text;
                Label2.Visible = true;
                Label2.Text = Convert.ToString(Session["MensajeProgramado"]);
                Session["Proceso"] = "FINAL";
            }
            else
            {
                lblError.Text = ex.Message;
            }
        }
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

    protected void btnErrores_Click(object sender, EventArgs e)
    {
        mpeProcesando.Hide();
        mpeFinal.Hide();
        pFinal.Visible = false;
        error.Actualizar("L");
        mpeErrores.Show();
    }
}
