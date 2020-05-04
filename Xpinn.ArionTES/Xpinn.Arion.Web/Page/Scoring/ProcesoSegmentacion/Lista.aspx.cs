using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Xpinn.Cartera.Services;
using Xpinn.Comun.Entities;
using Xpinn.Scoring.Entities;
using Xpinn.Scoring.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    Xpinn.Scoring.Services.ScoringCreditosService _historicoService = new Xpinn.Scoring.Services.ScoringCreditosService();
    Thread _tareaEjecucion;
    DateTime _fecha = DateTime.MaxValue;
    string _estado = "D";

    #region Eventos Iniciales


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(_historicoService.CodigoProgramaSeg, "L");

            Site toolBar = (Site)this.Master;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.GetType().Name + "L", "Page_PreInit", ex);
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
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.GetType().Name + "L", "Page_Load", ex);
        }
    }

    private void LlenarCombos()
    {
        // Llenar el DDL de la fecha de corte 
        Configuracion conf = new Configuracion();

        List<Cierea> lstFechaCierre = _historicoService.ListarFechaCierre1("Z", Usuario); 
        List<RiesgoCredito> lstFechasYaCerradas = _historicoService.ListarFechaCierreYaHechas("Z", "D", Usuario);

        if (lstFechasYaCerradas != null && lstFechasYaCerradas.Count > 0)
        {
            // Se deben comparar dia, mes y año manualmente o los milisegundos o segundos pueden generar diferencias
            lstFechaCierre = lstFechaCierre.Where(x =>
            {
                int dia = x.fecha.Day;
                int mes = x.fecha.Month;
                int año = x.fecha.Year;

                foreach (var fechaCerrada in lstFechasYaCerradas)
                {
                    // Si la fecha de cierre existe en la lista de fechas cerradas descarto esta fecha de cierre
                    if (fechaCerrada.fecha_corte.Day == dia && fechaCerrada.fecha_corte.Month == mes && fechaCerrada.fecha_corte.Year == año)
                    {
                        return false;
                    }
                }

                return true;
            }).ToList();
        }
        else
        {
            Cierea pCierre = new Cierea();
            int año = DateTime.Now.Year;
            int mes = DateTime.Now.Month;
            pCierre = new Cierea();
            pCierre.fecha = new DateTime(año, mes, 1, 0, 0, 0).AddDays(-1);

            lstFechaCierre.Add(pCierre);
        }

        ddlFechaCorte.DataSource = lstFechaCierre;
        ddlFechaCorte.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataBind();
    }


    #endregion


    #region Eventos Botones


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

        try
        {
            String format = "dd/MM/yyyy";
            _fecha = DateTime.ParseExact(ddlFechaCorte.SelectedValue, format, CultureInfo.InvariantCulture);
        }
        catch
        {
            Label1.Visible = true;
            Label1.Text = "Error al convertir la fecha " + ddlFechaCorte.SelectedValue;
            return;
        }

        _tareaEjecucion = new Thread(new ThreadStart(EjecutaProceso));
        _tareaEjecucion.Start();
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


    #endregion


    #region Metodos de Ayuda


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
            if (string.IsNullOrWhiteSpace(Session["Error"].ToString()))
            {
                Label2.Text = "Proceso de Segmentaciòn de Crèditos Terminado Correctamente";
            }
            else
            {
                Label2.Text = "Error: " + Session["Error"].ToString();
                lblError.Text = Session["Error"].ToString();
                Session.Remove("Error");
            }
        }
    }

    public void EjecutaProceso()
    {
        string sError = string.Empty;

        try
        {
            _historicoService.CierreSegmentacionCredito(_fecha, _estado, ref sError, Usuario);
        }
        catch (Exception ex)
        {
            sError = ex.Message;
        }
        finally
        {
            Session["Error"] = sError;
            Session["Proceso"] = "FINAL";
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


    #endregion


}