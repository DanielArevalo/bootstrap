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
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
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
                mvConfAporte.ActiveViewIndex = 0;
                rbTipoArchivo.SelectedIndex = 0;
                DateTime pFecha = DateTime.Now;
                txtFecIni.Text = new DateTime(pFecha.Year, pFecha.Month, 1).ToString();
                ucFecha.ToDateTime = pFecha;
                txtArchivo.Text = ((Usuario)Session["Usuario"]).cod_uiaf;
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
        if (!ucFecha.TieneDatos)
        {
            VerError("Seleccione la fecha de corte para realizar la consulta.");
            return;
        }
        if (txtFecIni.TieneDatos)
        {
            DateTime pFecCorte = ucFecha.ToDateTime;
            DateTime pFecIni = txtFecIni.ToDateTime;
            if (pFecIni > pFecCorte)
            {
                VerError("La fecha inicial no puede superar a la fecha de corte establecida.");
                return;
            }
        }
        
        if (!ObtenerDatos())
            return;
        Int64 id_reporte = Convert.ToInt64(Session["Idreporte"]);
        if (id_reporte > 0)
        {
            List<UIAF_Exonerados> listaexonerados = new List<UIAF_Exonerados>();
            listaexonerados = TransaccionEfectivoServicio.ListaUIAFExonerados((Int64)Session["Idreporte"], (Usuario)Session["Usuario"]);
            if (listaexonerados.Count > 0)
            {
                foreach (GridViewRow x in gvLista.Rows)
                {
                    if (listaexonerados.Where(y => y.consecutivo == Convert.ToInt64(((HiddenField)x.FindControl("consecutivo")).Value)).Count() > 0)
                    {
                        CheckBox cbxSeleccion = (CheckBox)x.FindControl("cbxSeleccion");
                        cbxSeleccion.Checked = true;
                    }
                }
            }
        }
    }

    public Boolean ObtenerDatos()
    {
        List<TransaccionEfectivo> listaArchivo = new List<TransaccionEfectivo>();
        UIAF_Reporte uiaf_reporte = new UIAF_Reporte();
        try
        {
            Site toolBar = (Site)this.Master;
            DateTime pFecIni = txtFecIni.TieneDatos ? txtFecIni.ToDateTime : DateTime.MinValue;
            if (Session["fecha_corte"] == null)
            {
                listaArchivo = TransaccionEfectivoServicio.ListaTransaccionEfectivo(pFecIni, ucFecha.ToDateTime, (Usuario)Session["Usuario"]);
                uiaf_reporte = TransaccionEfectivoServicio.Uiaf_Reporte(ucFecha.ToDateTime, (Usuario)Session["Usuario"]);
                if (uiaf_reporte == null)
                    Session["Idreporte"] = 0;
                else
                    Session["Idreporte"] = uiaf_reporte.idreporte;
                Session["fecha_corte"] = ucFecha.ToDateTime;
            }
            else
            {
                listaArchivo = TransaccionEfectivoServicio.ListaTransaccionEfectivo(pFecIni, (DateTime)Session["fecha_corte"], (Usuario)Session["Usuario"]);
                uiaf_reporte = TransaccionEfectivoServicio.Uiaf_Reporte((DateTime)Session["fecha_corte"], (Usuario)Session["Usuario"]);
                if (uiaf_reporte == null)
                {
                    Session["Idreporte"] = 0;
                }
                else
                {
                    Session["Idreporte"] = uiaf_reporte.idreporte;
                }
            }
            if (listaArchivo.Count == 0)
            {
                toolBar.MostrarExportar(false);
                toolBar.MostrarGuardar(false);
                VerError("No hay datos a partir de la fecha establecida");
                return false;
            }
            else
            {
                VerError("");
                listaArchivo = listaArchivo.OrderBy(x => x.fecha_tran).ToList();
                int sec = 1;
                foreach (TransaccionEfectivo y in listaArchivo)
                {
                    y.consecutivo = sec;
                    sec += 1;
                }
            }

            if (gvLista.Rows.Count == 0)
            {
                gvLista.EmptyDataText = null;
                gvLista.DataSource = listaArchivo;
                gvLista.DataBind();
            }
            Session["archivo"] = listaArchivo;
            toolBar.MostrarExportar(true);
            toolBar.MostrarGuardar(true);
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
        if (rbTipoArchivo.SelectedItem != null)
        {
            TransaccionEfectivo pTran = new TransaccionEfectivo();
            // Determinando el tipo de archivo y el Separador
            if (rbTipoArchivo.SelectedIndex == 0)
                pTran.separador = ",";
            else if (rbTipoArchivo.SelectedIndex == 1)
                pTran.separador = "  ";
            else if (rbTipoArchivo.SelectedIndex == 2)
                pTran.separador = "|";

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

            List<TransaccionEfectivo> listaArchivo = new List<TransaccionEfectivo>();
            string pError = "";
            try
            {
                ObtenerDatos();
                listaArchivo = (List<TransaccionEfectivo>)Session["archivo"];
                foreach (GridViewRow x in gvLista.Rows)
                {
                    if (((CheckBox)x.FindControl("cbxSeleccion")).Checked)
                    {
                        TransaccionEfectivo transaccion_exonerado = new TransaccionEfectivo();
                        transaccion_exonerado = listaArchivo.Where(y => y.consecutivo == Convert.ToInt64(((HiddenField)x.FindControl("consecutivo")).Value)).ToList()[0];
                        listaArchivo.Remove(transaccion_exonerado);
                    }
                }
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
                string cod_uiaf = txtArchivo.Text;
                int secuencia = 1;
                int caracteresAanterior = 0;
                int diferencia = 0;
                if (rbTipoArchivo.SelectedIndex == 1)
                {
                    texto = "         0";
                    if (cod_uiaf == "")
                        texto = texto + "00000000";
                    else
                        texto = texto + "" + cod_uiaf + "";
                    texto = texto + Convert.ToDateTime(Session["fecha_corte"]).ToString("yyyy-MM-dd") + "         " + listaArchivo.Count + "";
                    for (int i = 39; i <= 547; i++)
                    {

                        texto = texto + "X";
                    }
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();

                    foreach (TransaccionEfectivo item in listaArchivo)
                    {
                        texto = "";
                        for (int i = 1; i <= (10 - secuencia.ToString().Length); i++)
                        {
                            texto = texto + " ";
                        }
                        texto = texto + "" + secuencia + "" + item.fecha_tran.ToString("yyyy-MM-dd") + "";
                        for (int i = 1; i <= 20 - item.valor_tran.ToString().Length; i++)
                        {
                            texto = texto + " ";
                        }
                        texto = texto + "" + item.valor_tran + "" + item.tipo_moneda + "" + item.cod_oficina + "              ";

                        if (item.cod_ciudad == null)
                            item.cod_ciudad = "";
                        for (int i = 1; i <= 5 - item.cod_ciudad.ToString().Length; i++)
                        {
                            texto = texto + "0";
                        }
                        var vp = item.tipo_producto.ToString().Length == 1 ? "0" : "";
                        texto = texto + item.cod_ciudad;
                        texto = texto + vp + item.tipo_producto + "";

                        texto = texto + "" + item.tipo_tran + "" + item.num_producto + " ";
                        if (item.num_producto == null)
                            item.num_producto = "";
                        if (caracteresAanterior > texto.ToString().Length)
                        {
                            diferencia = caracteresAanterior - texto.ToString().Length;
                        }
                        else if (texto.ToString().Length > caracteresAanterior)
                        {
                            diferencia = 0;
                        }
                        caracteresAanterior = texto.ToString().Length;
                        for (int i = (item.num_producto.ToString().Length); i <= 18 - diferencia; i++)
                        {
                            texto = texto + " ";
                        }
                        if (item.tipo_identificacion1 == null)
                            item.tipo_identificacion1 = "";
                        texto = texto + "" + item.tipo_identificacion1 + "" + item.identificacion1 + "";
                        for (int i = (item.identificacion1.ToString().Length + 1); i <= 20; i++)
                        {
                            texto = texto + " ";
                        }
                        if (item.primer_apellido1 == null)
                            item.primer_apellido1 = "";
                        texto = texto + "" + item.primer_apellido1 + "";
                        for (int i = (item.primer_apellido1.Length + 1); i <= 40; i++)
                        {
                            texto = texto + " ";
                        }
                        if (item.segundo_apellido1 == null)
                            item.segundo_apellido1 = "";
                        texto = texto + "" + item.segundo_apellido1 + "";
                        for (int i = (item.segundo_apellido1.Length + 1); i <= 40; i++)
                        {
                            texto = texto + " ";
                        }
                        if (item.primer_nombre1 == null)
                            item.primer_nombre1 = "";
                        texto = texto + "" + item.primer_nombre1 + "";
                        for (int i = (item.primer_nombre1.Length + 1); i <= 40; i++)
                        {
                            texto = texto + " ";
                        }
                        if (item.segundo_nombre1 == null)
                            item.segundo_nombre1 = "";
                        texto = texto + "" + item.segundo_nombre1 + "";
                        for (int i = (item.segundo_nombre1.Length + 1); i <= 40; i++)
                        {
                            texto = texto + " ";
                        }
                        if (item.razon_social1 == null)
                            item.razon_social1 = "";
                        texto = texto + "" + item.razon_social1 + "";
                        for (int i = (item.razon_social1.Length + 1); i <= 60; i++)
                        {
                            texto = texto + " ";
                        }
                        if (item.actividad_economica == null)
                            item.actividad_economica = "";
                        texto = texto + "" + item.actividad_economica + "";
                        for (int i = (item.actividad_economica.Length); i <= 32; i++)
                        {
                            texto = texto + " ";
                        }
                        if (item.ingresos == null)
                            item.ingresos = "0";
                        texto = texto + "" + item.ingresos + "" + item.tipo_identificacion2 + "" + item.identificacion2 + "";
                        int texto_count = texto.Length;
                        for (int i = 1; i <= 12; i++)
                        {
                            texto = texto + " ";
                        }
                        //Seccion nueva
                        if (item.primer_apellido2 == null)
                            item.primer_apellido2 = "";
                        texto = texto + "" + item.primer_apellido2 + "";
                        for (int i = (item.primer_apellido2.Length + 1); i <= 40; i++)
                        {
                            texto = texto + " ";
                        }
                        if (item.segundo_apellido2 == null)
                            item.segundo_apellido2 = "";
                        texto = texto + "" + item.segundo_apellido2 + "";
                        for (int i = (item.segundo_apellido2.Length + 1); i <= 40; i++)
                        {
                            texto = texto + " ";
                        }
                        if (item.primer_nombre2 == null)
                            item.primer_nombre2 = "";
                        texto = texto + "" + item.primer_nombre2 + "";
                        for (int i = 0; i <= 34; i++)
                        {
                            texto = texto + " ";
                        }
                        if (item.segundo_nombre2 == null)
                            item.segundo_nombre2 = "";
                        texto = texto + "" + item.segundo_nombre2 + "";
                        for (int i = (item.segundo_nombre2.Length + 1); i <= 39; i++)
                        {
                            texto = texto + " ";
                        }
                        sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                        sw.WriteLine(texto);
                        sw.Close();
                        secuencia += 1;
                    }
                    texto = "         0" + cod_uiaf + "        " + listaArchivo.Count + "";
                    for (int i = 29; i <= 548; i++)
                    {

                        texto = texto + "X";
                    }
                    sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();

                    secuencia += 1;
                    
                    texto = "         0" + cod_uiaf + "        " + listaArchivo.Count + "";
                    for (int i = 29; i <= 548; i++)
                    {

                        texto = texto + "X";
                    }
                    sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();

                
                }
                else if (rbTipoArchivo.SelectedIndex == 2)
                {
                    texto = "Mes" + pTran.separador + "Año";
                    texto = texto + "|||||||||||||||||||||||";
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();
                    DateTimeFormatInfo nombreMes = new CultureInfo("es-ES", false).DateTimeFormat;
                    texto = "" + nombreMes.GetMonthName(((DateTime)Session["fecha_corte"]).Month).ToUpper() + "" + pTran.separador + "" + ((DateTime)Session["fecha_corte"]).Year + "";
                    texto = texto + "|||||||||||||||||||||||";
                    sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();


                    texto = "Consecutivo" + pTran.separador + "Código Entidad" + pTran.separador + "Fecha de Corte" + pTran.separador + "Total de Registros" + pTran.separador + "Fin Registro";
                    texto = texto + "||||||||||||||||||||";
                    sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();

                    if (cod_uiaf == "")
                        cod_uiaf = "00000000";
                    texto = "0" + pTran.separador + "" + cod_uiaf + "" + pTran.separador + "" + Convert.ToDateTime(Session["fecha_corte"]).ToString("yyyy-MM-dd") + "" + pTran.separador + "" + listaArchivo.Count + "" + pTran.separador + "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
                    texto = texto + "||||||||||||||||||||";
                    sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();



                    texto = "Consecutivo" + pTran.separador + "Fecha Transacción" + pTran.separador + "Valor Transacción" + pTran.separador + "Tipo Moneda" + pTran.separador + "Código Oficina" +
                    pTran.separador + "Código Departamento/Municipio" + pTran.separador + "Tipo Producto" + pTran.separador + "Tipo Transacción" + pTran.separador + "Nro. Cuenta o Producto" + pTran.separador +
                    "Tipo Identificación del Titular" + pTran.separador + "Nro.  Identificación del Titular" + pTran.separador + "1er. Apellido del Titular" + pTran.separador + "2do. Apellido del Titular" + pTran.separador +
                    "1er. Nombre del Titular" + pTran.separador + "Otros Nombres del Titular" + pTran.separador + "Razón Social del Titular" + pTran.separador + "Actividad Económica del Titular" + pTran.separador +
                    "Ingreso Mensual del Titular" + pTran.separador + "Tipo Identificación persona que realiza la transacción individual" + pTran.separador + "Nro. Identificación persona que realiza la transacción individual" + pTran.separador +
                    "1er. Apellido persona que realiza la transacción individual" + pTran.separador + "2do. Apellido persona que realiza la transacción individual" + pTran.separador + "1er. Nombre persona que realiza la transacción individual" + pTran.separador
                    + "Otros Nombres persona que realiza la transacción individual";
                    sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();
                    foreach (TransaccionEfectivo item in listaArchivo)
                    {
                        var vp = item.tipo_producto.ToString().Length == 1 ? "0" : "";
                        texto = "" + secuencia + "" + pTran.separador + "" + item.fecha_tran.ToString("yyyy-MM-dd") + "" + pTran.separador + "" + item.valor_tran + "" + pTran.separador + "" + item.tipo_moneda + "" + pTran.separador + "" + item.cod_oficina + "" + pTran.separador + "" + item.cod_ciudad + "" + pTran.separador + "" + vp + item.tipo_producto + "" + pTran.separador + "" + item.tipo_tran +
                                "" + pTran.separador + "" + item.num_producto + "" + pTran.separador + "" + item.tipo_identificacion1 + "" + pTran.separador + "" + item.identificacion1 + "" + pTran.separador + "" + item.primer_apellido1 + "" + pTran.separador + "" + item.segundo_apellido1 + "" + pTran.separador + "" + item.primer_nombre1 + "" + pTran.separador + "" + item.segundo_nombre1 + "" + pTran.separador + "" + item.razon_social1 + "" + pTran.separador + "" + item.actividad_economica + "" + pTran.separador + "" + item.ingresos + "" + pTran.separador + ""
                                + item.tipo_identificacion2 + "" + pTran.separador + "" + item.identificacion2 + "" + pTran.separador + "" + item.primer_apellido2 + "" + pTran.separador + "" + item.segundo_apellido2 + "" + pTran.separador + "" + item.primer_nombre2 + "" + pTran.separador + "" + item.segundo_nombre2 + "";
                        sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                        sw.WriteLine(texto);
                        sw.Close();
                        secuencia += 1;
                    }



                    texto = "Consecutivo" + pTran.separador + "Código Entidad" + pTran.separador + "Total de Registros" + pTran.separador + "Fin Registro";
                    texto = texto + "||||||||||||||||||||";
                    sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();

                    if (cod_uiaf == "")
                        cod_uiaf = "00000000";
                    texto = "0" + pTran.separador + "" + cod_uiaf + "" + "" + pTran.separador + "" + listaArchivo.Count + "" + pTran.separador + "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
                    texto = texto + "||||||||||||||||||||";
                    sw = new System.IO.StreamWriter(Server.MapPath("Archivos\\") + fic, true);
                    sw.WriteLine(texto);
                    sw.Close();


                }
                else
                {
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
                        var vp = item.tipo_producto.ToString().Length == 1 ? "0" : "";
                        texto = "" + secuencia + "" + pTran.separador + "" + item.fecha_tran.ToString("dd/MM/yyyy") + "" + pTran.separador + "" + item.valor_tran + "" + pTran.separador + "" + item.tipo_moneda + "" + pTran.separador + "" + item.cod_oficina + "" + pTran.separador + "" + item.cod_ciudad + "" + pTran.separador + "" + vp + item.tipo_producto + "" + pTran.separador + "" + item.tipo_tran +
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
                        ExpExcelConfecoop Exportar = new ExpExcelConfecoop();
                        StreamReader strReader = File.OpenText(Server.MapPath("Archivos\\") + fic);

                        DataGrid gvLista = new DataGrid();
                        StringBuilder sb = new StringBuilder();
                        StringWriter sw = new StringWriter(sb);
                        HtmlTextWriter htw = new HtmlTextWriter(sw);
                        Page pagina = new Page();
                        dynamic form = new HtmlForm();

                        gvLista.AllowPaging = false;
                        gvLista.DataSource = Exportar.EjecutarExportacion(ref Fila, strReader, (Usuario)Session["usuario"]);
                        gvLista.ItemDataBound += Item_Bound;
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
                    mvConfAporte.ActiveViewIndex = 1;
                }
                else
                {
                    VerError("No se genero el archivo para la fecha solicitado, Verifique los Datos");
                }
            }
            catch(Exception ex)
            {
                VerError("Se generó un error al realizar el archivo. En la Fila " + Fila);
            }
        }
        else
        {
            VerError("Seleccione el Tipo de Archivo");
        }
    }
    protected void Item_Bound(Object sender, DataGridItemEventArgs e)
    {
        e.Item.Attributes.Add("style", "vnd.ms-excel.numberformat:@");
    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            ctlMensaje.MostrarMensaje("Desea realizar la grabación de datos?");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TransaccionEfectivoServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        ObtenerDatos();
        List<TransaccionEfectivo> listaArchivo = new List<TransaccionEfectivo>();
        List<UIAF_Exonerados> listaexonerados = new List<UIAF_Exonerados>();
        List<UIAF_Exonerados> listaexonerados_antiguos = new List<UIAF_Exonerados>();
        List<UIAF_Exonerados> listaexonerados_seleccionados = new List<UIAF_Exonerados>();
        listaArchivo = (List<TransaccionEfectivo>)Session["archivo"];
        int validar_exonerado = (from GridViewRow y in gvLista.Rows
                                 where ((CheckBox)y.FindControl("cbxSeleccion")).Checked
                                 select y).Count();

        listaexonerados_seleccionados = (from TransaccionEfectivo x in listaArchivo
                                         where (from GridViewRow y in gvLista.Rows
                                                where ((CheckBox)y.FindControl("cbxSeleccion")).Checked
                                                select Convert.ToInt64(((HiddenField)y.FindControl("consecutivo")).Value)).Contains(x.consecutivo)
                                         select new UIAF_Exonerados
                                         {
                                             consecutivo = x.consecutivo,
                                             tipo_identificacion = x.tipo_identificacion1,
                                             identificacion = x.identificacion1,
                                             fecha_exoneracion = (DateTime)Session["fecha_corte"],
                                             primer_apellido = x.primer_apellido1,
                                             segundo_apellido = x.segundo_apellido1,
                                             primer_nombre = x.primer_nombre1,
                                             segundo_nombre = x.segundo_nombre1,
                                             razon_social = x.razon_social1
                                         }).ToList();
        listaexonerados_antiguos = TransaccionEfectivoServicio.ListaUIAFExonerados(Convert.ToInt64(Session["Idreporte"]), (Usuario)Session["Usuario"]);
        if (listaexonerados_antiguos.Count > 0)
        {
            foreach (UIAF_Exonerados x in listaexonerados_seleccionados)
            {
                if ((from UIAF_Exonerados y in listaexonerados_antiguos
                     where y.consecutivo == x.consecutivo
                     select y.consecutivo).Count() > 0)
                {
                    listaexonerados.Add(listaexonerados_antiguos.Where(y => y.consecutivo == x.consecutivo).ToList()[0]);
                }
                else
                {
                    listaexonerados.Add(x);
                }
            }
        }
        else
        {
            listaexonerados = listaexonerados_seleccionados;
        }
        DateTime pFecCorte = ucFecha.ToDateTime;
        DateTime pFecIni = txtFecIni.TieneDatos ? txtFecIni.ToDateTime : new DateTime(pFecCorte.Year, pFecCorte.Month, 1);
        TransaccionEfectivoServicio.ClientesExonerados(listaexonerados, pFecIni, pFecCorte, Convert.ToInt64(Session["Idreporte"]), (Usuario)Session["usuario"]);
    }

}