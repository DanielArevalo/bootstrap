using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web;
public partial class Lista : GlobalWeb
{
    LiquidacionNominaService _liquidacionServices = new LiquidacionNominaService();

    #region Eventos Iniciales
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_liquidacionServices.CodigoProgramareportes, "L");

            Site toolBar = (Site)Master;
         //   toolBar.eventoLimpiar += btnLimpiar_Click;
            mvPrincipal.ActiveViewIndex = 0;
            //  toolBar.eventoConsultar += btnConsultar_Click;
        //    toolBar.eventoNuevo += (s, evt) => {
                Session.Remove(_liquidacionServices.CodigoProgramareportes + ".id");
              //  Navegar(Pagina.Detalle);
           // };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionServices.CodigoProgramareportes, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InicializarPagina();
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.NominaEmpleado, ddlTipoNomina);
        LlenarListasDesplegables(TipoLista.CentroCostos, ddlCentroCosto);

        txtFechaInicio.Attributes.Add("readonly", "readonly");
        txtFechaFin.Attributes.Add("readonly", "readonly");
    }


    #endregion


    #region Eventos Intermedios GridView - Botonera


 

    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtCodigoLiquidacion.Text = string.Empty;
        txtFechaInicio.Text = string.Empty;
        txtFechaFin.Text = string.Empty;
        ddlTipoNomina.SelectedIndex = 0;
        ddlCentroCosto.SelectedIndex = 0;
    }

    void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }


    #endregion


    #region Métodos Ayuda


    void Actualizar()
    {
        string pError = "";
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();
            LiquidacionNomina liquidacion = new LiquidacionNomina();

            List<LiquidacionNomina> lstLiquidacion = _liquidacionServices.ListarReportesNomina(ObtenerValores(),ref pError, Usuario);

        }
        catch (Exception ex)
        {
            VerError("Error al actualizar la grilla, " + ex.Message);
        }
    }
    private Xpinn.Nomina.Entities.LiquidacionNomina ObtenerValores()
    {
        Xpinn.Nomina.Entities.LiquidacionNomina vnomina = new Xpinn.Nomina.Entities.LiquidacionNomina();
        try
        {

            if (!string.IsNullOrWhiteSpace(txtCodigoLiquidacion.Text))
            {
                vnomina.codigonomina =Convert.ToInt64(txtCodigoLiquidacion.Text.Trim());
            }
            if (!string.IsNullOrWhiteSpace(txtFechaInicio.Text) && !string.IsNullOrWhiteSpace(txtFechaFin.Text))
            {
                vnomina.fechainicio = Convert.ToDateTime(txtFechaInicio.Text.Trim());
                    
            }

            if (!string.IsNullOrWhiteSpace(txtFechaFin.Text) && !string.IsNullOrWhiteSpace(txtFechaFin.Text))
            {
                vnomina.fechaterminacion = Convert.ToDateTime(txtFechaFin.Text.Trim());

            }

            if (!string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue))
            {
                vnomina.tiponomina =  Convert.ToInt64(ddlTipoNomina.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            
        }
        return vnomina;
    }



    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtCodigoLiquidacion.Text))
        {
            filtro += " and liq.consecutivo = " + txtCodigoLiquidacion.Text.Trim();
        }
        
        if (!string.IsNullOrWhiteSpace(txtFechaInicio.Text) && !string.IsNullOrWhiteSpace(txtFechaFin.Text))
        {
            filtro += " and liq.FECHAINICIO = to_Date('" + txtFechaInicio.Text.Trim() + "', 'dd/MM/yyyy') and liq.FECHATERMINACION = to_Date('" + txtFechaFin.Text.Trim() + "', 'dd/MM/yyyy') ";
        }
        else if (!string.IsNullOrWhiteSpace(txtFechaInicio.Text))
        {
            filtro += " and liq.FECHAINICIO = to_Date('" + txtFechaInicio.Text.Trim() + "', 'dd/MM/yyyy') ";
        }
        else if(!string.IsNullOrWhiteSpace(txtFechaFin.Text))
        {
            filtro += " and liq.FECHATERMINACION = to_Date('" + txtFechaFin.Text.Trim() + "', 'dd/MM/yyyy') ";
        }

        if (!string.IsNullOrWhiteSpace(ddlCentroCosto.SelectedValue))
        {
            filtro += " and liq.CODIGOCENTROCOSTO = " + ddlCentroCosto.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue))
        {
            filtro += " and liq.CODIGONOMINA = " + ddlTipoNomina.SelectedValue;
        }

        filtro += " and liq.estado = 'D'";

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            StringHelper stringHelper = new StringHelper();
            filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);
        }

        return filtro;
    }


    #endregion

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes.Add("class", "textmode");
        }
    }

    protected void btnImprimirPila_Click(object sender, EventArgs e)
    {
        VerError("");
        if (txtFechaInicio.Text == null||  txtFechaFin.Text == null)
        {
            VerError("No existen fechas para seleccionar, verifique los datos");
            return;
        }
        if (rbTipoArchivo.SelectedItem != null)
        {
            LiquidacionNomina liquidacion = new LiquidacionNomina();
            // Determinando el tipo de archivo y el Separador
            if (rbTipoArchivo.SelectedIndex == 0)
                liquidacion.separador = ";";
            else if (rbTipoArchivo.SelectedIndex == 1)
                liquidacion.separador = "  ";
            else if (rbTipoArchivo.SelectedIndex == 2)
                liquidacion.separador = "|";
         
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
            string texto = "";

            // Validar si ya se ejecuto el proceso

            List<LiquidacionNomina> listaArchivo = new List<LiquidacionNomina>();
            liquidacion.fechainicio = Convert.ToDateTime(txtFechaInicio.Text);
            liquidacion.fechaterminacion= Convert.ToDateTime(txtFechaFin.Text);
            liquidacion.tiponomina = Convert.ToInt64(ddlTipoNomina.SelectedValue);
            liquidacion.origen = 2;
            string pError = "";
            try
            {
                listaArchivo = _liquidacionServices.ListarReportesNomina(liquidacion, ref pError, (Usuario)Session["Usuario"]);
                if (pError != "")
                {
                    VerError(pError);
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
                foreach (LiquidacionNomina item in listaArchivo)
                {
                    texto = item.linea;
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            Int32 Fila = 0;
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
                        HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
                        HttpContext.Current.Response.Flush();
                        File.Delete(Server.MapPath("Archivos\\") + fic);
                        HttpContext.Current.Response.End();
                    }
                    else
                    {
                        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                        ExpExcelConfecoop Exportar = new ExpExcelConfecoop();
                        StreamReader strReader = File.OpenText(Server.MapPath("Archivos\\") + fic);

                        GridView1.DataSource = Exportar.EjecutarExportacion(ref Fila, strReader, (Usuario)Session["usuario"]);
                        GridView1.DataBind();

                        Response.ClearContent();
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + fic);
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);

                        GridView1.RenderControl(htw);
                        Response.Write(style);
                        Response.Write(sw.ToString());
                        Response.End();
                    }
                    mvPrincipal.ActiveViewIndex = 1;
                }
                else
                {
                    VerError("No se genero el archivo para la fecha solicitado, Verifique los Datos");
                }
            }
            catch
            {
                VerError("Se generó un error al realizar el archivo. En la Fila " + Fila);
            }
        }
        else
        {
            VerError("Seleccione el Tipo de Archivo");
        }
    }

    protected void btnImprimirUGGP_Click(object sender, EventArgs e)
    {
        VerError("");
        if (txtFechaInicio.Text == null || txtFechaFin.Text == null)
        {
            VerError("No existen fechas para seleccionar, verifique los datos");
            return;
        }
        if (rbTipoArchivo.SelectedItem != null)
        {
            LiquidacionNomina liquidacion = new LiquidacionNomina();
            // Determinando el tipo de archivo y el Separador
            if (rbTipoArchivo.SelectedIndex == 0)
                liquidacion.separador = ";";
            else if (rbTipoArchivo.SelectedIndex == 1)
                liquidacion.separador = "  ";
            else if (rbTipoArchivo.SelectedIndex == 2)
                liquidacion.separador = "|";
         
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
            string texto = "";

            // Validar si ya se ejecuto el proceso

            List<LiquidacionNomina> listaArchivo = new List<LiquidacionNomina>();
            liquidacion.fechainicio = Convert.ToDateTime(txtFechaInicio.Text);
            liquidacion.fechaterminacion = Convert.ToDateTime(txtFechaFin.Text);
            liquidacion.tiponomina = Convert.ToInt64(ddlTipoNomina.SelectedValue);
            liquidacion.origen = 1;
            string pError = "";
            try
            {
                listaArchivo = _liquidacionServices.ListarReportesNomina(liquidacion, ref pError, (Usuario)Session["Usuario"]);
                if (pError != "")
                {
                    VerError(pError);
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
                foreach (LiquidacionNomina item in listaArchivo)
                {
                    texto = item.linea;
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            Int32 Fila = 0;
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
                        HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
                        HttpContext.Current.Response.Flush();
                        File.Delete(Server.MapPath("Archivos\\") + fic);
                        HttpContext.Current.Response.End();
                    }
                    else
                    {
                        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                        ExpExcelConfecoop Exportar = new ExpExcelConfecoop();
                        StreamReader strReader = File.OpenText(Server.MapPath("Archivos\\") + fic);

                        GridView1.DataSource = Exportar.EjecutarExportacion(ref Fila, strReader, (Usuario)Session["usuario"]);
                        GridView1.DataBind();

                        Response.ClearContent();
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + fic);
                        StringWriter sw = new StringWriter();
                        HtmlTextWriter htw = new HtmlTextWriter(sw);

                        GridView1.RenderControl(htw);
                        Response.Write(style);
                        Response.Write(sw.ToString());
                        Response.End();
                    }
                    mvPrincipal.ActiveViewIndex = 1;
                }
                else
                {
                    VerError("No se genero el archivo para la fecha solicitado, Verifique los Datos");
                }
            }
            catch
            {
                VerError("Se generó un error al realizar el archivo. En la Fila " + Fila);
            }
        }
        else
        {
            VerError("Seleccione el Tipo de Archivo");
        }
    }
}