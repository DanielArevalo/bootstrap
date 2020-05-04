using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Configuration;

public partial class CertificadoDIAN : GlobalWeb
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoCtaService = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.CertificadoDian, "L");
            Site toolBar = (Site)Master;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarImprimir(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("ActualizarDatos", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CargarDropDown();
            panelImpresion.Visible = false;
        }
    }

    protected void CargarDropDown()
    {
        VerError("");
        if (Session["persona"] != null)
        {
            xpinnWSLogin.Persona1 DataPersona = new xpinnWSLogin.Persona1();
            DataPersona = (xpinnWSLogin.Persona1)Session["persona"];
            List<Int32> lstAnios = new List<Int32>();
            Int64 pCod_persona = DataPersona.cod_persona;
            lstAnios = EstadoCtaService.ListarAniosPersonaCertificado(pCod_persona);
            if (lstAnios.Count > 0)
            {
                lstAniosPersona.DataSource = lstAnios;
                lstAniosPersona.DataBind();
            }
            else
            {
                VerError("No se encontraron datos, si usted tiene créditos, Aportes u otros productos con nosotros, "
                +"acérquese a una de nuestras oficinas para verificar este inconveniente.");
            }
        }
    }


    protected void lstAniosPersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (lstAniosPersona.SelectedItem != null)
            {
                //generar la consulta de datos
                DataTable dtProductos = new DataTable();
                dtProductos.Columns.Add("Concepto");
                dtProductos.Columns.Add("Saldo");

                xpinnWSLogin.Persona1 pPersona = new xpinnWSLogin.Persona1();
                pPersona = (xpinnWSLogin.Persona1)Session["persona"];

                List<xpinnWSEstadoCuenta.Persona_infcertificado> lstInformacion = new List<xpinnWSEstadoCuenta.Persona_infcertificado>();
                //List<Xpinn.Aportes.Entities.Persona_infcertificado> lstInformacion = new List<Xpinn.Aportes.Entities.Persona_infcertificado>();
                Int64 pCodPersona = pPersona.cod_persona;
                string pFiltro = " AND TO_NUMBER(TO_CHAR(FECHA_CORTE,'yyyy')) = " + lstAniosPersona.SelectedValue;
                lstInformacion = EstadoCtaService.ListarInformacionCertificado(pCodPersona, pFiltro);
                if (lstInformacion.Count > 0)
                {
                    Site toolBar = (Site)Master;
                    toolBar.MostrarImprimir(true);
                    decimal aporte = 0,credito = 0,interes = 0, otros = 0, retencion = 0;
                    //llenando el Datatable
                    foreach (xpinnWSEstadoCuenta.Persona_infcertificado pInformacion in lstInformacion)
                    {
                        if (pInformacion.valor_aportes != null)
                            aporte += Convert.ToDecimal(pInformacion.valor_aportes);
                        if (pInformacion.valor_cartera != null)
                            credito += Convert.ToDecimal(pInformacion.valor_cartera);
                        if (pInformacion.valor_intereses != null)
                            interes += Convert.ToDecimal(pInformacion.valor_intereses);
                        if (pInformacion.otros_ingresos != null)
                            otros += Convert.ToDecimal(pInformacion.otros_ingresos);
                        if (pInformacion.retefuente != null)
                            retencion += Convert.ToDecimal(pInformacion.retefuente);
                    }

                    DataRow dataA;
                    dataA = dtProductos.NewRow();
                    dataA[0] = "SALDO DE APORTES";
                    dataA[1] = aporte.ToString("c2");
                    dtProductos.Rows.Add(dataA);
                    DataRow dataC;
                    dataC = dtProductos.NewRow();
                    dataC[0] = "SALDO DE CRÉDITOS";
                    dataC[1] = credito.ToString("c2");
                    dtProductos.Rows.Add(dataC);
                    DataRow dataI;
                    dataI = dtProductos.NewRow();
                    dataI[0] = "INTERESES PAGADOS DURANTE EL AÑO";
                    dataI[1] = interes.ToString("c2");
                    dtProductos.Rows.Add(dataI);
                    DataRow dataO;
                    dataO = dtProductos.NewRow();
                    dataO[0] = "OTROS INGRESOS RECIBIDOS";
                    dataO[1] = otros.ToString("c2");
                    dtProductos.Rows.Add(dataO);
                    DataRow dataR;
                    dataR = dtProductos.NewRow();
                    dataR[0] = "RETENCIÓN POR OTROS INGRESOS TRIBUTARIOS";
                    dataR[1] = retencion.ToString("c2");
                    dtProductos.Rows.Add(dataR);
                }
                else
                {
                    DataRow datarw;
                    datarw = dtProductos.NewRow();
                    datarw[0] = " ";
                    datarw[1] = " ";
                    dtProductos.Rows.Add(datarw);
                }

                string pRepresentante = ConfigurationManager.AppSettings["Representante"] != null ? ConfigurationManager.AppSettings["Representante"].ToString() : string.Empty;
                string pIdentRepresen = ConfigurationManager.AppSettings["IdentRepresentante"] != null ? ConfigurationManager.AppSettings["IdentRepresentante"].ToString() : string.Empty;
                string pTeleFooter = ConfigurationManager.AppSettings["TeleFooter"] != null ? ConfigurationManager.AppSettings["TeleFooter"].ToString() : string.Empty;
                string pUrlWebPage = ConfigurationManager.AppSettings["WebPage"] != null ? ConfigurationManager.AppSettings["WebPage"].ToString() : string.Empty;
                string pEmpresa = ConfigurationManager.AppSettings["Empresa"] != null ? ConfigurationManager.AppSettings["Empresa"].ToString() : string.Empty;



                ReportParameter[] param = new ReportParameter[12];
                param[0] = new ReportParameter("Titulo", pEmpresa);
                param[1] = new ReportParameter("Entidad", " " + pPersona.empresa);
                param[2] = new ReportParameter("Nit", " " + pPersona.nit);
                param[3] = new ReportParameter("FechaCorte", DateTime.ParseExact("31/12/" + lstAniosPersona.SelectedValue, "dd/MM/yyyy", null).ToShortDateString());
                param[4] = new ReportParameter("Asociado", " " + pPersona.nombre);
                param[5] = new ReportParameter("Identificacion", " " + pPersona.identificacion);
                param[6] = new ReportParameter("SubTitulo", "AÑO GRAVABLE " + lstAniosPersona.SelectedValue);
                param[7] = new ReportParameter("ImagenReport", ImagenReporte());
                param[8] = new ReportParameter("representanteCertificado", " " + pRepresentante);
                param[9] = new ReportParameter("CCrepresentante", " " + pIdentRepresen);
                param[10] = new ReportParameter("UrlWebPage", " " + pUrlWebPage);
                param[11] = new ReportParameter("TeleFooter", " " + pTeleFooter);

                RptCertificado.LocalReport.EnableExternalImages = true;
                RptCertificado.LocalReport.DataSources.Clear();
                RptCertificado.LocalReport.SetParameters(param);

                ReportDataSource rds1 = new ReportDataSource("DataSet1", dtProductos);
                RptCertificado.LocalReport.DataSources.Add(rds1);
                RptCertificado.LocalReport.Refresh();
                frmPrint.Visible = false;
                RptCertificado.Visible = true;
                panelImpresion.Visible = true;

            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        try
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            xpinnWSLogin.Persona1 pPersona = new xpinnWSLogin.Persona1();
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            string pNomUsuario = pPersona.nombre != null && pPersona.nombre != "" ? "_" + pPersona.nombre : "";
            byte[] bytes = RptCertificado.LocalReport.Render("PDF", null, out mimeType,
                       out encoding, out extension, out streamids, out warnings);
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output" + pNomUsuario + ".pdf"),
            FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            Session["Archivo"] = Server.MapPath("output" + pNomUsuario + ".pdf");
            frmPrint.Visible = true;
            RptCertificado.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}