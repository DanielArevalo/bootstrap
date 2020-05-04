using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.UI.HtmlControls;
using Xpinn.Confecoop.Entities;
using Xpinn.Confecoop.Services;

public partial class Lista : GlobalWeb
{
    ConfecoopService saldosdiariosService = new ConfecoopService();
    //Thread tareaEjecucion;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(saldosdiariosService.CodigoProgramaSaldos, "L");
            Site toolBar = (Site)Master;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(saldosdiariosService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mpeProcesando.Hide();
                Timer1.Enabled = false;
                mvActivosFijos.ActiveViewIndex = 0;
                CargarDropDown();
                CargarFecha("C");
                cbCorte.Checked = true;
                ControlarFechas();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(saldosdiariosService.GetType().Name + "D", "Page_Load", ex);
        }
    }


    void CargarFecha(string pTipo)
    {
        string pFecAnterior = ddlFecha.SelectedItem != null ? ddlFecha.SelectedValue : null;
        PUC pPuc = new PUC();
        string tipo, estado;
        tipo = pTipo;
        estado = "D";
        ddlFecha.DataSource = saldosdiariosService.ListarFechaCierreGLOBAL(tipo,estado, (Usuario)Session["usuario"]);
        ddlFecha.DataTextField = "fecha";
        ddlFecha.DataTextFormatString = "{0:" + gFormatoFecha + "}";
        ddlFecha.DataValueField = "fecha";        
        ddlFecha.DataBind();
        try
        {
            if (pFecAnterior != null)
                ddlFecha.SelectedValue = pFecAnterior;
        }
        catch
        {
        }
    }

    private void CargarDropDown()
    {
        ddlTipoCuenta.Items.Insert(0, new ListItem("Local", "0"));
        ddlTipoCuenta.Items.Insert(1, new ListItem("Niif", "1"));
        ddlTipoCuenta.DataBind();
        ddlTipoCuenta.SelectedIndex = 0;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {        
        VerError("");
        lblError.Text = "";
        if (cbCorte.Checked)
        {
            if (ddlFecha.SelectedItem == null)
            {
                VerError("No existen fechas para seleccionar, verifique los datos");
                return;
            }
        }
        else
        {
            if (!txtFecIni.TieneDatos)
            { 
                VerError("Debe ingresar la fecha inicial, verifique los datos");
                return;
            }
            if (!txtFecFin.TieneDatos)
            {
                VerError("Debe ingresar la fecha final, verifique los datos");
                return;
            }
            if (txtFecFin.ToDateTime < txtFecIni.ToDateTime) 
            {
                VerError("La fecha final no puede ser anterior a la fecha incial, verifique los datos");
                return;
            }
        }

        if (rbTipoArchivo.SelectedItem != null)
        { 
             PUC pPuc = new PUC();
            // Determinando el tipo de archivo y el Separador
            if (rbTipoArchivo.SelectedIndex == 0)
                pPuc.separador = ";";
            else if (rbTipoArchivo.SelectedIndex == 1)
                pPuc.separador = "  ";
            else if (rbTipoArchivo.SelectedIndex == 2)
                pPuc.separador = "|";

            // Determinando el nombre del archivo
            string fic = "";
            if (txtArchivo.Text != "")
            {
                if (rbTipoArchivo.SelectedIndex == 0)
                {
                    fic = txtArchivo.Text.Trim().Contains(".csv") ? txtArchivo.Text : txtArchivo.Text + ".csv";
                }
                else if (rbTipoArchivo.SelectedIndex == 1)
                {
                    fic = txtArchivo.Text.Trim().Contains(".txt") ? txtArchivo.Text : txtArchivo.Text + ".txt";
                }
                else if (rbTipoArchivo.SelectedIndex == 2)
                {
                    fic = txtArchivo.Text.Trim().Contains(".xls") ? txtArchivo.Text : txtArchivo.Text + ".xls";
                }
            }
            else
            {
                VerError("Ingrese el Nombre del archivo a Generar");
                return;
            }

            // Validar si ya se ejecuto el proceso
            int pTipoNorma = Convert.ToInt32(ddlTipoCuenta.SelectedValue);
            if (cbCorte.Checked)
            {
                pPuc.fecha = Convert.ToDateTime(ddlFecha.SelectedValue);
                pPuc.fecha_ini = new DateTime(pPuc.fecha.Year, pPuc.fecha.Month, 1);
            }
            else
            {
                pPuc.fecha = txtFecFin.ToDateTime;
                pPuc.fecha_ini = txtFecIni.ToDateTime;
            }
            pPuc.nombre_archivo = fic;
            pPuc.tipo_norma = pTipoNorma;
            //IniciarProceso();
            //tareaEjecucion = new Thread(new ParameterizedThreadStart(EjecutaProceso));
            //tareaEjecucion.Start(pPuc);
            EjecutaProceso(pPuc);
            if (Session["Archivo"] != null)
            {
                if (Session["Archivo"].ToString().Trim() != "")
                {
                    GenerarArchivo(Session["Archivo"].ToString());
                    Session.Remove("Archivo");
                    return;
                }
            }
            if (Session["Error"] != null)
            {
                lblError.Text = Session["Error"].ToString();
            }
            VerError("No se pudo generar el archivo");
        }
        else
        {
            VerError("Seleccione el Tipo de Archivo");
        }
    }

    public void EjecutaProceso(Object pParametro1)
    {
        Session["Archivo"] = "";
        PUC pPuc = (PUC)pParametro1;
        string fic = pPuc.nombre_archivo;
        string texto = "";
        List<PUC> listaArchivo = new List<PUC>();
        bool estado = false;
        string pError = "";
        try
        {
            listaArchivo = saldosdiariosService.ListarTEMP_SUPERSOLIDARIA_SALDOS(pPuc, ref pError, (Usuario)Session["Usuario"], estado, pPuc.tipo_norma);
            if (pError != "")
            {
                Session["Proceso"] = "FINAL";
                Session["Error"] = pError;
                return;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }

        // ELIMINANDO ARCHIVOS GENERADOS
        try
        {
            string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
            foreach (string ficheroActual in ficherosCarpeta)
                File.Delete(ficheroActual);
        }
        catch
        { }

        try
        {
            foreach (PUC item in listaArchivo)
            {
                texto = item.linea.Replace(".", ",");
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                sw.WriteLine(texto);
                sw.Close();
            }
            Session["Archivo"] = fic;
        }
        catch
        {

        }
        Session["Proceso"] = "FINAL";
    }

    public void GenerarArchivo(string fic)
    { 
        Int32 Fila = 0;
        string texto = "";
        try
        {
            // Copiar el archivo al cliente        
            if (File.Exists(Server.MapPath("Archivos\\") + fic))
            {
                if (rbTipoArchivo.SelectedItem.Text == "CSV" || rbTipoArchivo.SelectedItem.Text == "TEXTO") // TEXTO O CSV
                {
                    System.IO.StreamReader sr;
                    sr = File.OpenText(Server.MapPath("Archivos\\") + fic);
                    texto = sr.ReadToEnd();
                    sr.Close();
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.ClearHeaders();
                    HttpContext.Current.Response.ContentType = "text/plain";
                    HttpContext.Current.Response.Write(texto);
                    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fic);
                    HttpContext.Current.Response.Flush();
                    File.Delete(Server.MapPath("Archivos\\") + fic);
                    HttpContext.Current.Response.End();
                }
                else
                {
                    ExpExcelConfecoop Exportar = new ExpExcelConfecoop();
                    StreamReader strReader = File.OpenText(Server.MapPath("Archivos\\") + fic);

                    DataGrid gvLista = new DataGrid();
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    Page pagina = new Page();
                    dynamic form = new HtmlForm();

                    gvLista.AllowPaging = false;
                    gvLista.DataSource = Exportar.EjecutarExportacion(ref Fila,strReader, (Usuario)Session["usuario"]);
                    gvLista.DataBind();
                    gvLista.EnableViewState = false;

                    pagina.EnableEventValidation = false;
                    pagina.DesignerInitialize();
                    pagina.Controls.Add(form);
                    form.Controls.Add(gvLista);
                    pagina.RenderControl(htw);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + fic);
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = Encoding.Default;
                    Response.Write(sb.ToString());
                    Response.End();
                }

                mvActivosFijos.ActiveViewIndex = 1;
            }
            else
            {
                Session["Error"] = "No se genero el archivo para la fecha solicitado, Verifique los Datos";
            }
        }
        catch (Exception ex)
        {
            Session["Error"] = "Se generó un error al realizar el archivo. En la Fila " + Fila + " " + ex.Message;
        }
    }

    protected void ddlTipoCuenta_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pTipo = ddlTipoCuenta.SelectedValue == "1" ? "G" : "C";
        CargarFecha(pTipo);
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
            lblError.Text = Session["Error"].ToString();
            Session.Remove("Error");
        }
        else
        {
            if (Session["Archivo"] != null)
            {
                if (Session["Archivo"].ToString().Trim() != "")
                {
                    GenerarArchivo(Session["Archivo"].ToString());
                    Session.Remove("Archivo");
                    return;
                }
            }
            lblError.Text = "No se pudo generar el archivo";
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

    protected void cbCorte_CheckedChanged(object sender, EventArgs e)
    {
        cbPeriodo.Checked = !cbCorte.Checked;
        ControlarFechas();
    }

    protected void cbPeriodo_CheckedChanged(object sender, EventArgs e)
    {
        cbCorte.Checked = !cbPeriodo.Checked;
        ControlarFechas();
    }

    private void ControlarFechas()
    {
        if (cbPeriodo.Checked)
        {
            Up1.Visible = false;
            lblTituloFecha.Visible = false;
            panelperiodo.Visible = true;
        }
        else
        {
            Up1.Visible = true;
            lblTituloFecha.Visible = true;
            panelperiodo.Visible = false;
        }
    }


}
