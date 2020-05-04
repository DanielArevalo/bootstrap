using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Entities;
using System.Threading;
using System.Globalization;

public partial class Lista : GlobalWeb
{
    Xpinn.Aportes.Services.CierreHisAporteService AportesServicio = new Xpinn.Aportes.Services.CierreHisAporteService();
    Thread tareaEjecucion;
    string estado = "";
    DateTime fecha = System.DateTime.MinValue;
    int codusuario = 0;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(AportesServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AportesServicio.GetType().Name + "L", "Page_PreInit", ex);
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
               
                    //ValidarCierre();
             }
         

                GridView grv = (GridView)error.FindControl("gvLista");
                if (grv.Rows.Count > 0)
                {
                    grv.Visible = true;
                    mpeErrores.Show();
                }

                Label2.Visible = true;
                Label2.Text = Convert.ToString(Session["MensajeAportes"]);

            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AportesServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }
    //public void ValidarCierre()
    //{
    //    Usuario usuap = (Usuario)Session["usuario"];
    //    codusuario = Convert.ToInt32(usuap.codusuario);
    //    try
    //    {
    //        String format = "dd/MM/yyyy";
    //        fecha = DateTime.ParseExact(ddlFechaCorte.SelectedValue, format, CultureInfo.InvariantCulture);
    //    }
    //    catch
    //    {
    //        Label1.Visible = true;
    //        Label1.Text = "Error al convertir la fecha " + ddlFechaCorte.SelectedValue;
    //    }
    //    estado = Convert.ToString(rbEstado.SelectedValue);
    //    lblCuentasNegativas.Text = AportesServicio.ValidarCierre(estado, fecha, codusuario, (Usuario)Session["usuario"]);
    //}
    /// <summary>
    /// Método para llenar los DDLs requeridos para las consultas
    /// </summary>
    protected void LlenarCombos()
    {
        // Llenar el DDL de la fecha de corte 
        Configuracion conf = new Configuracion();
        List<Xpinn.Comun.Entities.Cierea> lstFechaCierre = new List<Xpinn.Comun.Entities.Cierea>();
        lstFechaCierre = AportesServicio.ListarFechaCierre((Usuario)Session["Usuario"]);
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

    /// <summary>CC
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
        Label2.Text = Session["MensajeAportes"].ToString();
    }

    public void EjecutaProceso()
    {
        string sError = "";
        try
        {
            Xpinn.Aportes.Services.CierreHisAporteService AportesServicio = new Xpinn.Aportes.Services.CierreHisAporteService();
            Xpinn.Aportes.Entities.Aporte aportes = new Xpinn.Aportes.Entities.Aporte();
            AportesServicio.CierreHistorico(aportes,estado, fecha, codusuario, ref sError, (Usuario)Session["usuario"]);
            Session["MensajeAportes"] = "Cierre Mensual Terminado Correctamente";
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
                Session["MensajeAportes"] = lblError.Text;
                Label2.Visible = true;
                Label2.Text = Convert.ToString(Session["MensajeAportes"]);
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
    protected void ddlFechaCorte_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ValidarCierre();
    }

    protected void btnErrores_Click(object sender, EventArgs e)
    {
        mpeProcesando.Hide();
        mpeFinal.Hide();
        pFinal.Visible = false;
        error.Actualizar("A");
        mpeErrores.Show();
    }
}
