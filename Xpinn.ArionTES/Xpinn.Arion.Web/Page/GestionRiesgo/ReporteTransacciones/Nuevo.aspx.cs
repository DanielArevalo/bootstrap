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
using System.Web.UI.HtmlControls;
using Xpinn.Reporteador.Entities;
using Xpinn.Reporteador.Services;

public partial class Nuevo : GlobalWeb
{

    TransaccionEfectivoService TransaccionEfectivoServicio = new TransaccionEfectivoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TransaccionEfectivoServicio.CodigoPrograma, "N");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TransaccionEfectivoServicio.GetType().Name + "N", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                DateTime pFecha = DateTime.Now;
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Session["fecha_corte"] = null;
        Session["Idreporte"] = null;
        gvLista.EmptyDataText = null;
        gvLista.DataSource = null;
        gvLista.DataBind();

        // GENERANDO VALIDACION  
        if (!ObtenerDatos())
            return;
    }

    public Boolean ObtenerDatos()
    {
        List<TransaccionEfectivo> listaArchivo = new List<TransaccionEfectivo>();
        UIAF_Reporte uiaf_reporte = new UIAF_Reporte();
        try
        {
            DateTime pFecIni = DateTime.Now;
             listaArchivo = TransaccionEfectivoServicio.ListaTransaccionEfectivo(pFecIni, (DateTime)Session["fecha_corte"], (Usuario)Session["Usuario"]);
            uiaf_reporte = TransaccionEfectivoServicio.Uiaf_Reporte((DateTime)Session["fecha_corte"], (Usuario)Session["Usuario"]);

            if (gvLista.Rows.Count == 0)
            {
                gvLista.EmptyDataText = null;
                gvLista.DataSource = listaArchivo;
                gvLista.DataBind();
            }
            Session["archivo"] = listaArchivo;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarExportar(true);
            return true;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            return false;
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        Configuracion conf = new Configuracion();
        VerError("");
        TransaccionEfectivo pTran = new TransaccionEfectivo();
        pTran.separador = "|";

        // Determinando el nombre del archivo
        string fic = ".csv";
        string texto = "";

        // Validar si ya se ejecuto el proceso

        List<TransaccionEfectivo> listaArchivo = new List<TransaccionEfectivo>();
        string pError = "";
        try
        {
            ObtenerDatos();
            listaArchivo = (List<TransaccionEfectivo>)Session["archivo"];
            if (listaArchivo.Count == 0)
            {
                VerError("No hay datos a partir de la fecha establecida");
                return;
            }
            else
                VerError("");

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
            gvLista.EmptyDataText = null;
            gvLista.DataSource = listaArchivo;
            gvLista.DataBind();
            if (((Usuario)Session["Usuario"]).cod_uiaf == null)
                ((Usuario)Session["Usuario"]).cod_uiaf = "";
            string cod_uiaf = ((Usuario)Session["Usuario"]).cod_uiaf;
            int secuencia = 1;
                
            texto = "Consecutivo" + pTran.separador + "Fecha Transaccion" + pTran.separador + "Valor Transaccion" + pTran.separador + "Tipo Moneda" + pTran.separador + "Codigo Oficina" +
            pTran.separador + "Codigo Departamento/Municipio" + pTran.separador + "Tipo Producto" + pTran.separador + "Tipo Transaccion" + pTran.separador + "Nro. Cuenta o Producto" + pTran.separador +
            "Tipo Identificacion del Titular" + pTran.separador + "Nro.  Identificacion del Titular" + pTran.separador + "1er. Apellido del Titular" + pTran.separador + "2do. Apellido del Titular" + pTran.separador +
            "1er. Nombre del Titular" + pTran.separador + "Otros Nombres del Titular" + pTran.separador + "Razon Social del Titular" + pTran.separador + "Actividad Economica del Titular" + pTran.separador +
            "Ingreso Mensual del Titular" + pTran.separador + "Tipo Identificacion persona que realiza la transaccion individual" + pTran.separador + "Nro. Identificacion persona que realiza la transaccion individual" + pTran.separador +
            "1er. Apellido persona que realiza la transaccion individual" + pTran.separador + "2do. Apellido persona que realiza la transaccion individual" + pTran.separador + "1er. Nombre persona que realiza la transaccion individual" + pTran.separador
            + "Otros Nombres persona que realiza la transaccion individual";
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true, Encoding.GetEncoding(1252));
            sw.WriteLine(texto);
            sw.Close();
            foreach (TransaccionEfectivo item in listaArchivo)
            {
                texto = "" + secuencia + "" + pTran.separador + "" + item.fecha_tran.ToString("dd/MM/yyyy") + "" + pTran.separador + "" + item.valor_tran + "" + pTran.separador + "" + item.tipo_moneda + "" + pTran.separador + "" + item.cod_oficina + "" + pTran.separador + "" + item.cod_ciudad + "" + pTran.separador + "" + item.tipo_producto + "" + pTran.separador + "" + item.tipo_tran +
                        "" + pTran.separador + "" + item.num_producto + "" + pTran.separador + "" + item.tipo_identificacion1 + "" + pTran.separador + "" + item.identificacion1 + "" + pTran.separador + "" + item.primer_apellido1 + "" + pTran.separador + "" + item.segundo_apellido1 + "" + pTran.separador + "" + item.primer_nombre1 + "" + pTran.separador + "" + item.segundo_nombre1 + "" + pTran.separador + "" + item.razon_social1 + "" + pTran.separador + "" + item.actividad_economica + "" + pTran.separador + "" + item.ingresos + "" + pTran.separador + ""
                        + item.tipo_identificacion2 + "" + pTran.separador + "" + item.identificacion2 + "" + pTran.separador + "" + item.primer_apellido2 + "" + pTran.separador + "" + item.segundo_apellido2 + "" + pTran.separador + "" + item.primer_nombre2 + "" + pTran.separador + "" + item.segundo_nombre2 + "";
                sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true, Encoding.GetEncoding(1252));
                sw.WriteLine(texto);
                sw.Close();
                secuencia += 1;
            }
            texto = "Consecutivo" + pTran.separador + "Código Entidad" + pTran.separador + "Total de Registros" + pTran.separador;
            texto = texto + ",,,,,,,,,,,,,,,,,,,,";
            sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
            sw.WriteLine(texto);
            sw.Close();

            if (cod_uiaf == "")
                cod_uiaf = "00000000";
            texto = "0" + pTran.separador + "" + cod_uiaf + "" + "" + pTran.separador + "" + listaArchivo.Count + "" + pTran.separador + "";
            texto = texto + ",,,,,,,,,,,,,,,,,,,,";
            sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
            sw.WriteLine(texto);
            sw.Close();

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
                VerError("No se genero el archivo para la fecha solicitado, Verifique los Datos");
            }
        }
        catch
        {
            VerError("Se generó un error al realizar el archivo. En la Fila " + Fila);
        }
    }
   

}