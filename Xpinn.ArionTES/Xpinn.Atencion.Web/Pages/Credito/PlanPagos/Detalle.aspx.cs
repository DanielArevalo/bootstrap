using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;

public partial class Detalle : GlobalWeb
{
    xpinnWSLogin.Persona1 pPersona;
    xpinnWSCredito.WSCreditoSoapClient BOCredito = new xpinnWSCredito.WSCreditoSoapClient();
    
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session["persona"] == null)
                Response.Redirect("~/Pages/Account/FinSesion.htm");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("PlanPagos", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        if (!Page.IsPostBack)
        {
            ViewState["PlanPagos"] = null;
            ViewState["AtributosPlanPagos"] = null;
            panelGeneral.Visible = true;
            panelImprimir.Visible = false;
            if (Request.QueryString["num_radic"] != null)
            {
                string pQueryId = Request.QueryString["num_radic"].ToString();
                idObjeto = pQueryId;
                ObtenerDatos(idObjeto);
            }
        }
    }


    private void ObtenerDatos(string pIdObjeto)
    {
        try
        {
            //Mostrar Encabezado
            ActualizarEncabezado(pIdObjeto);
            //Mostrar Detalle de Plan Pagos
            TablaPlanPagos(pIdObjeto);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void ActualizarEncabezado(string pIdObjeto)
    {
        if (pIdObjeto != null)
        {
            xpinnWSCredito.CreditoPlan pCredito = new xpinnWSCredito.CreditoPlan();
            pCredito.Numero_radicacion = Int64.Parse(pIdObjeto);
            pCredito = BOCredito.ConsultarInformacionCreditos(pCredito.Numero_radicacion, true, Session["sec"].ToString());
            List<xpinnWSCredito.CreditoPlan> lstabc = new List<xpinnWSCredito.CreditoPlan>();
            //Ajustando el valor de las tasas
            if (pCredito != null)
            {
                if (!string.IsNullOrEmpty(pCredito.TasaNom.ToString()))
                    pCredito.TasaNom = pCredito.TasaNom / 100;
                if (!string.IsNullOrEmpty(pCredito.TasaInteres.ToString()))
                    pCredito.TasaInteres = pCredito.TasaInteres / 100;
                if (!string.IsNullOrEmpty(pCredito.TasaEfe.ToString()))
                    pCredito.TasaEfe = pCredito.TasaEfe / 100;
                if (!string.IsNullOrEmpty(pCredito.Estado))
                {
                    if (pCredito.Estado == "A")
                        pCredito.Estado = "Aprobado";
                    else if ((pCredito.Estado == "G"))
                        pCredito.Estado = "Generado";
                    else if ((pCredito.Estado == "C"))
                        pCredito.Estado = "Desembolsado";
                    else
                        pCredito.Estado = pCredito.Estado;
                }
            }
            lstabc.Add(pCredito);
            if (lstabc.Count > 0)
            {    
                frvData.DataSource = lstabc;
                frvData.DataBind();
            }
        }
    }

    private List<xpinnWSCredito.DatosPlanPagos> ActualizarTabla(String pIdObjeto)
    {
        xpinnWSCredito.Credito datosApp = new xpinnWSCredito.Credito();
        datosApp.numero_radicacion = Int64.Parse(pIdObjeto);
        List<xpinnWSCredito.DatosPlanPagos> lstConsulta = new List<xpinnWSCredito.DatosPlanPagos>();
        lstConsulta = BOCredito.ListarDatosPlanPagos(datosApp, Session["sec"].ToString());
        return lstConsulta;
    }

    private void TablaPlanPagos(String pIdObjeto)
    {
        try
        {
            List<xpinnWSCredito.DatosPlanPagos> lstConsulta = new List<xpinnWSCredito.DatosPlanPagos>();
            lstConsulta = ActualizarTabla(pIdObjeto);
            ViewState.Add("PlanPagos", lstConsulta);

            gvLista.DataSource = lstConsulta;
            
            // Ajustar informaciòn de la grila para mostrar en pantalla
            if (lstConsulta.Count > 0)
            {
                // Mostrando la grilla                
                gvLista.Visible = true;
                gvLista.DataBind();
                gvLista.Columns[1].ItemStyle.Width = 90;
                // Ocultando las columnas que no deben mostrarse
                List<xpinnWSCredito.Atributos> lstAtr = new List<xpinnWSCredito.Atributos>();
                lstAtr = BOCredito.GenerarAtributosPlan(Session["sec"].ToString());
                ViewState.Add("AtributosPlanPagos", lstAtr);
                for (int i = 4; i <= 18; i++)
                {
                    gvLista.Columns[i].Visible = false;
                    int j = 0;
                    foreach (xpinnWSCredito.Atributos item in lstAtr)
                    {
                        if (j == i - 4)
                            gvLista.Columns[i].HeaderText = item.nom_atr;
                        j = j + 1;
                    }
                }

                foreach (xpinnWSCredito.DatosPlanPagos ItemPlanPagos in lstConsulta)
                {
                    if (ItemPlanPagos.int_1 != 0) { gvLista.Columns[4].Visible = true; }
                    if (ItemPlanPagos.int_2 != 0) { gvLista.Columns[5].Visible = true; }
                    if (ItemPlanPagos.int_3 != 0) { gvLista.Columns[6].Visible = true; }
                    if (ItemPlanPagos.int_4 != 0) { gvLista.Columns[7].Visible = true; }
                    if (ItemPlanPagos.int_5 != 0) { gvLista.Columns[8].Visible = true; }
                    if (ItemPlanPagos.int_6 != 0) { gvLista.Columns[9].Visible = true; }
                    if (ItemPlanPagos.int_7 != 0) { gvLista.Columns[10].Visible = true; }
                    if (ItemPlanPagos.int_8 != 0) { gvLista.Columns[11].Visible = true; }
                    if (ItemPlanPagos.int_9 != 0) { gvLista.Columns[12].Visible = true; }
                    if (ItemPlanPagos.int_10 != 0) { gvLista.Columns[13].Visible = true; }
                    if (ItemPlanPagos.int_11 != 0) { gvLista.Columns[14].Visible = true; }
                    if (ItemPlanPagos.int_12 != 0) { gvLista.Columns[15].Visible = true; }
                    if (ItemPlanPagos.int_13 != 0) { gvLista.Columns[16].Visible = true; }
                    if (ItemPlanPagos.int_14 != 0) { gvLista.Columns[17].Visible = true; }
                    if (ItemPlanPagos.int_15 != 0) { gvLista.Columns[18].Visible = true; }
                }
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = true;
            }
        }
        catch
        {
        }
    }

    protected void frvData_DataBound(object sender, EventArgs e)
    {
        Label lblFecDesembolso = (Label)frvData.FindControl("lblFecDesembolso");
        if (lblFecDesembolso != null)
        {
            if (!string.IsNullOrWhiteSpace(lblFecDesembolso.Text))
            {
                if (Convert.ToDateTime(lblFecDesembolso.Text) == DateTime.MinValue)
                    lblFecDesembolso.Text = "";
            }
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (panelImprimir.Visible == true)
        {
            panelGeneral.Visible = true;
            panelImprimir.Visible = false;
        }
        else if (pnlVerPagare.Visible == true)
        {
            panelGeneral.Visible = true;
            pnlVerPagare.Visible = false;
            Site toolBar = (Site)Master;
            toolBar.MostrarImprimir(true);
        }
        else
        {
            if (Request.QueryString["num_radic"] != null)
            {
                string pQueryId = Request.QueryString["num_radic"].ToString();
                idObjeto = pQueryId;
                pPersona = (xpinnWSLogin.Persona1)Session["persona"];
                Session[pPersona.cod_persona + "NroProducto"] = idObjeto;
                Session[pPersona.cod_persona + "CodProducto"] = "2";
                Navegar("~/Pages/Asociado/Movimientos/Credito.aspx");
            }
            else
            {
                Navegar("~/Pages/Asociado/EstadoCuenta/Detalle.aspx");
            }
        }
    }


    protected void btnVerPagare_Click(object sender, EventArgs e)
    {
        VerError("");
        panelGeneral.Visible = false;
        pnlVerPagare.Visible = true;
        Site toolBar = (Site)Master;
        toolBar.MostrarImprimir(false);

        byte[] bytePagare = BOCredito.ConsultarPagare(((Label)frvData.FindControl("lblNumRadic")).Text, Session["sec"].ToString());
        
        if (bytePagare != null)
        {
            MostrarArchivoEnLiteral(bytePagare);
        }
        else
        {
            VerError("No hay pagare disponible para mostrar de este credito!.");
        }
    }

    void MostrarArchivoEnLiteral(byte[] bytes)
    {
        string pNomUsuario = pPersona.nombre != "" && pPersona.nombre != null ? "_pagare" + pPersona.nombre : "";
        // ELIMINANDO ARCHIVOS GENERADOS
        try
        {
            string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
            foreach (string ficheroActual in ficherosCarpeta)
                if (ficheroActual.Contains(pNomUsuario))
                    File.Delete(ficheroActual);
        }
        catch
        { }

        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("Archivos/output" + pNomUsuario + ".pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        //MOSTRANDO REPORTE
        string adjuntar = "<object data=\"{0}\" type=\"application/pdf\" width=\"90%\" height=\"700px\">";
        adjuntar += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
        adjuntar += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
        adjuntar += "</object>";

        ltPagare.Text = string.Format(adjuntar, ResolveUrl("Archivos/output" + pNomUsuario + ".pdf"));
    }


    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        if (ViewState["AtributosPlanPagos"] == null)
            return;
        if (ViewState["PlanPagos"] == null)
            return;

        // ---------------------------------------------------------------------------------------------------------
        // Pasando datos al reporte
        // ---------------------------------------------------------------------------------------------------------
        ReportParameter[] param = new ReportParameter[51];
        param[0] = new ReportParameter("Entidad", pPersona.empresa);
        param[1] = new ReportParameter("numero_radicacion", ((Label)frvData.FindControl("lblNumRadic")).Text);
        param[2] = new ReportParameter("cod_linea_credito", ((Label)frvData.FindControl("lblCodLinea")).Text);
        param[3] = new ReportParameter("linea", ((Label)frvData.FindControl("lblLinea")).Text);
        param[4] = new ReportParameter("nombre", pPersona.nombre);
        param[5] = new ReportParameter("identificacion", pPersona.identificacion);
        param[6] = new ReportParameter("direccion", ((TextBox)frvData.FindControl("Txtdireccion")).Text);
        param[7] = new ReportParameter("ciudad", ((TextBox)frvData.FindControl("Txtciudad")).Text);
        param[8] = new ReportParameter("palzo", ((Label)frvData.FindControl("lblPlazo")).Text);
        param[9] = new ReportParameter("fecha_solicitud", ((TextBox)frvData.FindControl("Txtgeneracion")).Text);
        param[10] = new ReportParameter("periodicidad", ((Label)frvData.FindControl("lblPeriodicidad")).Text);
        param[11] = new ReportParameter("fecha_primer", ((TextBox)frvData.FindControl("Txtprimerpago")).Text);
        param[12] = new ReportParameter("valor_cuota", ((Label)frvData.FindControl("lblVrCuota")).Text);
        param[13] = new ReportParameter("fecha_desembolso", ((Label)frvData.FindControl("lblFecDesembolso")).Text);
        param[14] = new ReportParameter("tasa_nominal", ((Label)frvData.FindControl("lblTasaInteres")).Text);
        param[15] = new ReportParameter("tasa_efectiva", ((TextBox)frvData.FindControl("Txtinteresefectiva")).Text);
        param[16] = new ReportParameter("pagare", ((TextBox)frvData.FindControl("Txtpagare")).Text);
        param[17] = new ReportParameter("forma_pago", ((TextBox)frvData.FindControl("txtFormaPago")).Text);
        param[18] = new ReportParameter("desembolso", ((Label)frvData.FindControl("lblMontoCredito")).Text);
        param[19] = new ReportParameter("nomUsuario", pPersona.nombre);
        param[20] = new ReportParameter("ImagenReport", ImagenReporte());

        List<xpinnWSCredito.Atributos> lstAtr = new List<xpinnWSCredito.Atributos>();
        lstAtr = (List<xpinnWSCredito.Atributos>)ViewState["AtributosPlanPagos"];
        int j = 0;
        foreach (xpinnWSCredito.Atributos item in lstAtr)
        {
            param[21 + j] = new ReportParameter("titulo" + j, item.nom_atr);
            j = j + 1;
        }
        for (int i = j; i < 15; i++)
        {
            param[21 + i] = new ReportParameter("titulo" + i, " ");
        }

        List<xpinnWSCredito.DatosPlanPagos> lstPlan = new List<xpinnWSCredito.DatosPlanPagos>();
        lstPlan = (List<xpinnWSCredito.DatosPlanPagos>)ViewState["PlanPagos"];
        Boolean[] bVisible = new Boolean[16];
        for (int i = 1; i <= 15; i++)
        {
            bVisible[i] = false;
            i = i + 1;
        }
        foreach (xpinnWSCredito.DatosPlanPagos ItemPlanPagos in lstPlan)
        {
            if (ItemPlanPagos.int_1 != 0) { bVisible[1] = true; }
            if (ItemPlanPagos.int_2 != 0) { bVisible[2] = true; }
            if (ItemPlanPagos.int_3 != 0) { bVisible[3] = true; }
            if (ItemPlanPagos.int_4 != 0) { bVisible[4] = true; }
            if (ItemPlanPagos.int_5 != 0) { bVisible[5] = true; }
            if (ItemPlanPagos.int_6 != 0) { bVisible[6] = true; }
            if (ItemPlanPagos.int_7 != 0) { bVisible[7] = true; }
            if (ItemPlanPagos.int_8 != 0) { bVisible[8] = true; }
            if (ItemPlanPagos.int_9 != 0) { bVisible[9] = true; }
            if (ItemPlanPagos.int_10 != 0) { bVisible[10] = true; }
            if (ItemPlanPagos.int_11 != 0) { bVisible[11] = true; }
            if (ItemPlanPagos.int_12 != 0) { bVisible[12] = true; }
            if (ItemPlanPagos.int_13 != 0) { bVisible[13] = true; }
            if (ItemPlanPagos.int_14 != 0) { bVisible[14] = true; }
            if (ItemPlanPagos.int_15 != 0) { bVisible[15] = true; }
        }
        for (int i = 0; i < 15; i++)
        {
            param[36 + i] = new ReportParameter("visible" + i, bVisible[i + 1].ToString());
        }


        rptPlanPagos.LocalReport.EnableExternalImages = true;
        rptPlanPagos.LocalReport.SetParameters(param);

        rptPlanPagos.LocalReport.DataSources.Clear();
        ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTable(idObjeto));
        rptPlanPagos.LocalReport.DataSources.Add(rds);
        rptPlanPagos.LocalReport.Refresh();

        panelGeneral.Visible = false;
        panelImprimir.Visible = true;

        // ELIMINANDO ARCHIVOS GENERADOS
        try
        {
            string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
            foreach (string ficheroActual in ficherosCarpeta)
                if (ficheroActual.Contains(pPersona.nombre))
                    File.Delete(ficheroActual);
        }
        catch
        { }

        //CREANDO REPORTE
        string pNomUsuario = pPersona.nombre != "" && pPersona.nombre != null ? "_" + pPersona.nombre + DateTime.Now.ToString("HHmmss") : "";

        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;
        byte[] bytes = rptPlanPagos.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("Archivos/output" + pNomUsuario + ".pdf"),
            FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        //MOSTRANDO REPORTE
        string adjuntar = "<object data=\"{0}\" type=\"application/pdf\" width=\"100%\" height=\"550px\">";
        adjuntar += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
        adjuntar += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
        adjuntar += "</object>";

        ltReport.Text = string.Format(adjuntar, ResolveUrl("Archivos/output" + pNomUsuario + ".pdf"));
        
        rptPlanPagos.Visible = false;
        ltReport.Visible = true;
    }

    public DataTable CrearDataTable(String pIdObjeto)
    {
        if (pIdObjeto == "")
            pIdObjeto = ((Label)frvData.FindControl("lblNumRadic")).Text;
        if (ViewState["PlanPagos"] == null)
            return null;

        System.Data.DataTable table = new System.Data.DataTable();

        xpinnWSCredito.DatosPlanPagos datosApp = new xpinnWSCredito.DatosPlanPagos();
        datosApp.numero_radicacion = Int64.Parse(pIdObjeto);
        List<xpinnWSCredito.DatosPlanPagos> lstPlanPagos = new List<xpinnWSCredito.DatosPlanPagos>();
        lstPlanPagos = (List<xpinnWSCredito.DatosPlanPagos>)ViewState["PlanPagos"];
        List<xpinnWSCredito.Atributos> lstAtr = new List<xpinnWSCredito.Atributos>();
        lstAtr = (List<xpinnWSCredito.Atributos>)ViewState["AtributosPlanPagos"];

        table.Columns.Add("numerocuota");
        table.Columns.Add("fechacuota");
        table.Columns.Add("sal_ini");
        table.Columns.Add("capital");
        table.Columns.Add("int_1");
        table.Columns.Add("int_2");
        table.Columns.Add("int_3");
        table.Columns.Add("int_4");
        table.Columns.Add("int_5");
        table.Columns.Add("int_6");
        table.Columns.Add("int_7");
        table.Columns.Add("int_8");
        table.Columns.Add("int_9");
        table.Columns.Add("int_10");
        table.Columns.Add("int_11");
        table.Columns.Add("int_12");
        table.Columns.Add("int_13");
        table.Columns.Add("int_14");
        table.Columns.Add("int_15");
        table.Columns.Add("total");
        table.Columns.Add("sal_fin");

        foreach (xpinnWSCredito.DatosPlanPagos item in lstPlanPagos)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.numerocuota;
            if (item.fechacuota != null && item.total != 0)
            {
                datarw[1] = item.fechacuota.Value.ToShortDateString();
                datarw[2] = item.sal_ini.ToString("0,0");
                datarw[3] = item.capital.ToString("0,0");
                datarw[4] = item.int_1.ToString("0,0");
                datarw[5] = item.int_2.ToString("0,0");
                datarw[6] = item.int_3.ToString("0,0");
                datarw[7] = item.int_4.ToString("0,0");
                datarw[8] = item.int_5.ToString("0,0");
                datarw[9] = item.int_6.ToString("0,0");
                datarw[10] = item.int_7.ToString("0,0");
                datarw[11] = item.int_8.ToString("0,0");
                datarw[12] = item.int_9.ToString("0,0");
                datarw[13] = item.int_10.ToString("0,0");
                datarw[14] = item.int_11.ToString("0,0");
                datarw[15] = item.int_12.ToString("0,0");
                datarw[16] = item.int_13.ToString("0,0");
                datarw[17] = item.int_14.ToString("0,0");
                datarw[18] = item.int_15.ToString("0,0");
                datarw[19] = item.total.ToString("0,0");
                datarw[20] = item.sal_fin.ToString("0,0");
                table.Rows.Add(datarw);
            }
        }

        return table;
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLista.PageIndex = e.NewPageIndex;
        List<xpinnWSCredito.DatosPlanPagos> lstConsulta = new List<xpinnWSCredito.DatosPlanPagos>();
        if (ViewState["PlanPagos"] != null)
            lstConsulta = (List<xpinnWSCredito.DatosPlanPagos>)ViewState["PlanPagos"];
        else
            lstConsulta = ActualizarTabla(idObjeto);
        gvLista.DataSource = lstConsulta;
        gvLista.DataBind();
    }

}