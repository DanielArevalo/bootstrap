using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    Usuario usuario = new Usuario();
    DiasNoHabilesService serviceDiasNoHabil = new DiasNoHabilesService();

    private void Page_PreInit(object sender, EventArgs evt)
    {
        try
        {
            if (Session[serviceDiasNoHabil.CodigoPrograma + ".id"] != null)
            {
                VisualizarOpciones(serviceDiasNoHabil.CodigoPrograma, "E");
            }
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceDiasNoHabil.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    List<Dias_no_habiles> lstDias;
    private List<DateTime> lstFechasDias = new List<DateTime>();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                lstDias = new List<Dias_no_habiles>();
                Session["Fechas"] = null;
                Calendar1.SelectedDate = DateTime.Now;
                Session["MES"] = DateTime.Now.Month;
                Session["ANIO"] = DateTime.Now.Year;
                lstDias = CargarDiasNoHabiles(Convert.ToInt32(Session["MES"].ToString()), Convert.ToInt32(Session["ANIO"].ToString()));
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected List<Dias_no_habiles> CargarDiasNoHabiles(Int32 Mes, Int32 Anio)
    {
        List<Dias_no_habiles> lstDias = new List<Dias_no_habiles>();
        Dias_no_habiles pDias = new Dias_no_habiles();
        pDias.mes = Mes;
        pDias.ano = Anio;
        lstDias = serviceDiasNoHabil.ListarDiasNoHabiles(pDias, (Usuario)Session["usuario"]);
        foreach (Dias_no_habiles pFecha in lstDias)
        {
            DateTime fecha;
            if (pFecha.fecha != DateTime.MinValue)
            {
                fecha = pFecha.fecha;
                lstFechasDias.Add(fecha);
                Session["Fechas"] = lstFechasDias;
            }
        }
        return lstDias;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Dias_no_habiles pDias = new Dias_no_habiles();
            pDias.lstDias = new List<Dias_no_habiles>();
            lstFechasDias = (List<DateTime>)Session["Fechas"];
            foreach (DateTime pFecha in lstFechasDias)
            {
                Dias_no_habiles pDatos = new Dias_no_habiles();
                pDatos.consecutivo = 0;
                pDatos.mes = pFecha.Month;
                pDatos.ano = pFecha.Year;
                pDatos.dia_festivo = pFecha.Day;
                pDatos.dia_semana = Convert.ToInt32(pFecha.DayOfWeek);
                pDias.lstDias.Add(pDatos);
            }
            serviceDiasNoHabil.CrearDiasNoHabiles(pDias, (Usuario)Session["usuario"]);
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            mvCalendar.ActiveViewIndex = 1;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    
    protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        int Mes = e.NewDate.Month;
        int Anio = e.NewDate.Year;
        Session["MES"] = Mes;
        Session["ANIO"] = Anio;
        Calendar1.SelectedDates.Clear();
        Session["Fechas"] = null;
        lstFechasDias = new List<DateTime>() ;
        lstDias = CargarDiasNoHabiles(Convert.ToInt32(Session["MES"].ToString()), Convert.ToInt32(Session["ANIO"].ToString()));
    }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        if (Session["Fechas"] != null)
            SeleccionarFechas();

    }

    void SeleccionarFechas()
    {
        System.Drawing.Color color = new System.Drawing.Color();
        System.Drawing.ColorConverter convert = new System.Drawing.ColorConverter();
        Calendar1.SelectedDates.Clear();
        lstFechasDias = (List<DateTime>)Session["Fechas"];
        foreach (DateTime fecha in lstFechasDias)
        {
            Calendar1.SelectedDates.Add(fecha);
            color = (System.Drawing.Color)convert.ConvertFromString("#009999");
            Calendar1.SelectedDayStyle.BackColor = color;
            color = System.Drawing.Color.White;
            Calendar1.SelectedDayStyle.ForeColor = color;
        }
        Session["Fechas"] = lstFechasDias;
    }


    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        DateTime dateSeletion = Calendar1.SelectedDate;
        if (Session["Fechas"] != null)
        {            
            lstFechasDias = (List<DateTime>)Session["Fechas"];

            if (lstFechasDias.Contains(dateSeletion))
                lstFechasDias.Remove(dateSeletion);
            else
                lstFechasDias.Add(dateSeletion);

            Session["Fechas"] = lstFechasDias;
            SeleccionarFechas();
        }
        else
        {
            lstFechasDias = new List<DateTime>();
            lstFechasDias.Add(dateSeletion);
            Session["Fechas"] = lstFechasDias;
        }
    }
}