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
using System.Reflection;
using System.IO;
using Xpinn.Comun.Entities;
using System.Linq;

public partial class Lista : GlobalWeb
{
    Xpinn.Cartera.Services.CierreHistorioService CarteraServicio = new Xpinn.Cartera.Services.CierreHistorioService();
    Thread tareaEjecucion;
    string estado = ""; 
    DateTime fecha= System.DateTime.MinValue;
    int codusuario = 0;    

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(CarteraServicio.CodigoPrograma, "L");

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
                LlenarCombos();
                Timer1.Enabled = false;
            }
            if (Session["Mensaje"] != null)
            {

                GridView grv = (GridView)error.FindControl("gvLista");
                if (grv.Rows.Count > 0)
                {
                    grv.Visible = true;
                    mpeErrores.Show();
                }

                Label2.Visible = true;
                Label2.Text = Convert.ToString(Session["Mensaje"]);

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CarteraServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para llenar los DDLs requeridos para las consultas
    /// </summary>
    protected void LlenarCombos()
    {
        // Llenar el DDL de la fecha de corte 
        Configuracion conf = new Configuracion();
        List<Xpinn.Comun.Entities.Cierea> lstFechaCierre = new List<Xpinn.Comun.Entities.Cierea>();
        lstFechaCierre = CarteraServicio.ListarFechaCierre((Usuario)Session["Usuario"]);
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
        //error.Actualizar("R");
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
        Session.Remove("lstErrores");
        Response.Redirect("~/General/Global/inicio.aspx");
    }
    protected void btnExportarErr_Click(object sender, EventArgs e)
    {
        List<Cierea> lstConsulta = (List<Cierea>)Session["lstErrores"];
        if (Session["lstErrores"] != null)
        {
            string fic = "ErroresCierre.csv";
            try
            {
                File.Delete(fic);
            }
            catch
            {
            }
            // Generar el archivo
            bool bTitulos = false;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("") + fic, true);
            foreach (Cierea item in lstConsulta)
            {
                string texto = "";
                FieldInfo[] propiedades = item.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                if (!bTitulos)
                {
                    foreach (FieldInfo f in propiedades)
                    {
                        try
                        {
                            texto += f.Name.Split('>').First().Replace("<", "") + ";";
                        }
                        catch { texto += ";"; };
                    }
                    sw.WriteLine(texto);
                    bTitulos = true;
                }
                texto = "";
                int i = 0;
                foreach (FieldInfo f in propiedades)
                {
                    i += 1;
                    object valorObject = f.GetValue(item);
                    // Si no soy nulo
                    if (valorObject != null)
                    {
                        string valorString = valorObject.ToString();
                        if (valorObject is DateTime)
                        {
                            DateTime? fechaValidar = valorObject as DateTime?;
                            if (fechaValidar.Value != DateTime.MinValue)
                            {
                                texto += f.GetValue(item) + ";";
                            }
                            else
                            {
                                texto += "" + ";";
                            }
                        }
                        else
                        {
                            texto += f.GetValue(item) + ";";
                            texto.Replace("\r", "").Replace(";", "");
                        }
                    }
                    else
                    {
                        texto += "" + ";";
                    }
                }
                sw.WriteLine(texto);
            }
            sw.Close();
            System.IO.StreamReader sr;
            sr = File.OpenText(Server.MapPath("") + fic);
            string texo = sr.ReadToEnd();
            sr.Close();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(texo);
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
            HttpContext.Current.Response.Flush();
            File.Delete(Server.MapPath("") + fic);
            HttpContext.Current.Response.End();

        }
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
        //error.Actualizar("R");
        //GridView grv = (GridView)error.FindControl("gvLista");
        //if (grv.Rows.Count == 0)
        //{
        //    mpeFinal.Show();
        //}
        //else
        //{
        //    mpeErrores.Show();
        //}
        Session.Remove("Proceso");
        Timer1.Enabled = false;
        Session["Error"] = Session["Mensaje"];

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
        try
        {

        
        Xpinn.Cartera.Services.CierreHistorioService CarteraServicio = new Xpinn.Cartera.Services.CierreHistorioService();

            Xpinn.Cartera.Entities.CierreHistorico cartera = new Xpinn.Cartera.Entities.CierreHistorico();


          CarteraServicio.CierreHistorico(cartera,estado, fecha, codusuario, ref sError, (Usuario)Session["usuario"]);
         lblError.Text = "Cierre Mensual Terminado Correctamente";
         Label2.Text = "Cierre Mensual Terminado Correctamente";
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
                Session["Mensaje"] = lblError.Text;
                Label2.Visible = true;
                lblError.Visible = true;
                Label2.Text = lblError.Text;
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
        error.Actualizar("R");
        mpeErrores.Show();
    }
}

