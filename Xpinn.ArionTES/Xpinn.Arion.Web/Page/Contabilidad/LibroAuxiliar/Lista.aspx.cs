using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Microsoft.Reporting.WebForms;
using Microsoft.CSharp;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Reflection;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Services;
partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.LibroAuxiliarService LibroAuxiliarSer = new Xpinn.Contabilidad.Services.LibroAuxiliarService();
    Int64 registros = 0;
    String registro = "";
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(LibroAuxiliarSer.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LibroAuxiliarSer.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                btnInforme.Visible = false;
                btnExportar.Visible = false;
                LlenarCombos();
                VerError("");
                Configuracion conf = new Configuracion();
                try
                {
                    Xpinn.Comun.Services.CiereaService cierreServicio = new Xpinn.Comun.Services.CiereaService();
                    txtFecIni.ToDateTime = cierreServicio.FechaUltimoCierre("C", (Usuario)Session["Usuario"]).AddDays(1);
                }
                catch
                {
                    VerError("No se pudo determinar fecha de cierre inicial");
                }
                txtFecFin.ToDateTime = System.DateTime.Now;
                PanelCuentaFin.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LibroAuxiliarSer.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para llenar los DDLs requeridos para las consultas
    /// </summary>
    protected void LlenarCombos()
    {
        // LLenar el DDl de centro de costo
        Xpinn.Contabilidad.Services.CentroCostoService CentroCostoService = new Xpinn.Contabilidad.Services.CentroCostoService();
        List<Xpinn.Contabilidad.Entities.CentroCosto> LstCentroCosto = new List<Xpinn.Contabilidad.Entities.CentroCosto>();
        string sFiltro = "";
        LstCentroCosto = CentroCostoService.ListarCentroCosto((Usuario)Session["Usuario"], sFiltro);
        ddlCentroCosto.DataSource = LstCentroCosto;
        ddlCentroCosto.DataTextField = "nom_centro";
        ddlCentroCosto.DataValueField = "centro_costo";
        ddlCentroCosto.DataBind();
        ddlCentroCosto.Items.Insert(0, new ListItem("CONSOLIDADO", "0"));

        // Determinando el centro de costo inicial y final
        Int64 CenIni = Int64.MinValue;
        Int64 CenFin = Int64.MinValue;
        foreach (Xpinn.Contabilidad.Entities.CentroCosto ItemCC in LstCentroCosto)
        {
            if (CenIni == Int64.MinValue)
                CenIni = ItemCC.centro_costo;
            if (CenFin == Int64.MinValue)
                CenFin = ItemCC.centro_costo;
            if (CenIni > ItemCC.centro_costo)
                CenIni = ItemCC.centro_costo;
            if (CenFin < ItemCC.centro_costo)
                CenFin = ItemCC.centro_costo;
        }
        Session["CenIni"] = CenIni;
        Session["CenFin"] = CenFin;

    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, LibroAuxiliarSer.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, LibroAuxiliarSer.CodigoPrograma);
        DataTable dtVacio = new DataTable();
        gvLista.DataSource = dtVacio;
        gvLista.DataBind();
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string sDetalle = "";
                sDetalle = e.Row.Cells[6].Text;
                if (sDetalle.StartsWith("SALDO INICIAL") == true)
                {
                    e.Row.BackColor = System.Drawing.Color.LightBlue;
                    e.Row.Font.Bold = true;
                }
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LibroAuxiliarSer.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");

        try
        {
            List<Xpinn.Contabilidad.Entities.LibroAuxiliar> lstConsulta = new List<Xpinn.Contabilidad.Entities.LibroAuxiliar>();
            Int64 CenIni = Int64.MinValue;
            Int64 CenFin = Int64.MinValue;
            DateTime FecIni;
            DateTime FecFin;

            // Determinando el centro de costo
            if (ddlCentroCosto.SelectedValue.ToString() == "0")
            {
                try
                {
                    if (Session["CenIni"] != null && Session["CenFin"] != null)
                    {
                        CenIni = Convert.ToInt64(Session["CenIni"].ToString());
                        CenFin = Convert.ToInt64(Session["CenFin"].ToString());
                    }
                    else
                    {
                        Xpinn.Contabilidad.Services.CentroCostoService CCSer = new Xpinn.Contabilidad.Services.CentroCostoService();
                        CCSer.RangoCentroCosto(ref CenIni, ref CenFin, (Usuario)Session["Usuario"]);
                    }
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                    return;
                }
            }
            else
            {
                try
                {
                    CenIni = Convert.ToInt64(ddlCentroCosto.SelectedValue);
                    CenFin = Convert.ToInt64(ddlCentroCosto.SelectedValue);
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                    return;
                }
            }

            // determinar los datos para generar el libro auxiliar
            FecIni = Convert.ToDateTime(txtFecIni.ToDate);
            FecFin = Convert.ToDateTime(txtFecFin.ToDate);

            if (cbRango.Checked)
            {
                if(string.IsNullOrWhiteSpace(txtCodCuenta.Text) || string.IsNullOrWhiteSpace(txtCodCuentaFin.Text))
                {
                    VerError("Debe seleccionar ambas cuentas contables para determinar el rango");
                    return;
                }
            }

            // Determinando la fecha Inicial y Final
            lstConsulta = LibroAuxiliarSer.ListarAuxiliar(CenIni, CenFin, FecIni, FecFin, txtCodCuenta.Text, txtCodCuentaFin.Text, cbRango.Checked, Convert.ToInt32(ddlTipoMoneda.Value), ddlOrdenar.SelectedValue, (Usuario)Session["Usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            Session["DTLIBROAUXILIAR"] = lstConsulta;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                btnInforme.Visible = true;
                btnExportar.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["Cantidad"] = lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                btnInforme.Visible = false;
                btnExportar.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(LibroAuxiliarSer.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LibroAuxiliarSer.CodigoPrograma, "Actualizar", ex);
        }
    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        { 
            
            Int64 registros = Convert.ToInt64(Session["Cantidad"].ToString());
       
            if (registros< 50000 && Session["DTLIBROAUXILIAR"] != null)
            { 

                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                GridView gvExportar = CopiarGridViewParaExportar(gvLista, "DTLIBROAUXILIAR");
                gvLista.AllowPaging = false;
                //gvLista.DataSource = Session["DTLIBROAUXILIAR"];
                //gvLista.DataBind();
                //gvLista.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                //form.Controls.Add(gvLista);
                form.Controls.Add(gvExportar);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=LibroAuxiliar.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                //gvLista.AllowPaging = true;
                //gvLista.DataBind();

            }

            if (registros > 50001 && Session["DTLIBROAUXILIAR"] != null)
            {

                string fic = "LibroAuxiliar.csv";
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
                List<Xpinn.Contabilidad.Entities.LibroAuxiliar> lstConsulta = (List<Xpinn.Contabilidad.Entities.LibroAuxiliar>)Session["DTLIBROAUXILIAR"];
                foreach (Xpinn.Contabilidad.Entities.LibroAuxiliar item in lstConsulta)
                {
                    string texto = "";
                    FieldInfo[] propiedades = item.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                    if (!bTitulos)
                    {                        
                        foreach (FieldInfo f in propiedades)
                        {
                            try { texto += f.Name.Split('>').First().Replace("<", "") + ";"; } catch { texto += ";"; };
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
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }

    }

    protected void ExportToExcel(GridView GridView1)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Libroauxiliar.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = Encoding.Default;
            StringWriter sw = new StringWriter();
            ExpGrilla expGrilla = new ExpGrilla();

            sw = expGrilla.ObtenerGrilla(GridView1, (List<Xpinn.Contabilidad.Entities.Comprobante>)Session["DTLIBROAUXILIAR"]);

            Response.Write(expGrilla.style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        catch
        { }
    }
    protected void btnInforme_Click(object sender, EventArgs e)
    {
        string separadorDecimal = System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTLIBROAUXILIAR"] == null)
        {
            VerError("No ha generado el libro auxiliar para poder imprimir el reporte");
        }

        List<Xpinn.Contabilidad.Entities.LibroAuxiliar> lstConsulta = new List<Xpinn.Contabilidad.Entities.LibroAuxiliar>();
        lstConsulta = (List<Xpinn.Contabilidad.Entities.LibroAuxiliar>)Session["DTLIBROAUXILIAR"];

        // LLenar data table con los datos a recoger
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("fecha");
        table.Columns.Add("num_comp");
        table.Columns.Add("tipo_comp");
        table.Columns.Add("sop_egreso");
        table.Columns.Add("sop_ingreso");
        table.Columns.Add("detalle");
        table.Columns.Add("tipo");
        table.Columns.Add("valor", typeof(decimal));
        table.Columns.Add("debito", typeof(decimal));
        table.Columns.Add("credito", typeof(decimal));
        table.Columns.Add("saldo", typeof(decimal));
        table.Columns.Add("concepto");
        table.Columns.Add("identificacion");
        table.Columns.Add("nombre");
        table.Columns.Add("identific_tercero");
        table.Columns.Add("nombre_tercero");
        table.Columns.Add("cod_cuenta");
        table.Columns.Add("nombrecuenta");
        table.Columns.Add("naturaleza");
        table.Columns.Add("centro_costo");
        table.Columns.Add("centro_gestion");
        table.Columns.Add("regimen");
        table.Columns.Add("consecutivo");
        table.Columns.Add("depende_de");
        table.Columns.Add("base_minima");
        table.Columns.Add("porcentaje_imp");
      

        DataRow datarw;
        if (lstConsulta.Count == 0)
        {
            datarw = table.NewRow();
            for (int i = 0; i <= 22; i++)
            {
                datarw[i] = " ";
            }
            table.Rows.Add(datarw);
        }
        else
        {
            foreach (Xpinn.Contabilidad.Entities.LibroAuxiliar refe in lstConsulta)
            {
                datarw = table.NewRow();
                if (refe.fecha == null)
                    datarw[0] = " ";
                else
                    datarw[0] = " " + refe.fecha.Value.ToShortDateString();
                datarw[1] = " " + refe.num_comp;
                datarw[2] = " " + refe.tipo_comp;
                datarw[3] = " " + refe.sop_egreso;
                datarw[4] = " " + refe.sop_ingreso;
                datarw[5] = " " + refe.detalle;
                datarw[6] = " " + refe.tipo;
                datarw[7] = Convert.ToDecimal(refe.valor);
                datarw[8] = Convert.ToDecimal(refe.debito);
                datarw[9] = Convert.ToDecimal(refe.credito);
                datarw[10] = Convert.ToDecimal(refe.saldo);
                datarw[11] = " " + refe.concepto;
                datarw[12] = " " + refe.identificacion;
                datarw[13] = " " + refe.nombre;
                datarw[14] = " " + refe.identific_tercero;
                datarw[15] = " " + refe.nombre_tercero;
                datarw[16] = " " + refe.cod_cuenta;
                datarw[17] = " " + refe.nombrecuenta;
                if (refe.naturaleza == "C")
                    datarw[18] = "Crédito";
                else if (refe.naturaleza == "D")
                    datarw[18] = "Débito";
                else
                    datarw[18] = " " + refe.naturaleza;
                datarw[19] = " " + refe.centro_costo;
                datarw[20] = " " + refe.centro_gestion;
                datarw[21] = " " + refe.regimen;
                datarw[22] = " " + refe.consecutivo;
                datarw[23] = " " + refe.depende_de;
                datarw[24] = " " + refe.base_minima;
                datarw[25] = " " + refe.porcentaje_impuesto;
                table.Rows.Add(datarw);
            }
        }


        // ---------------------------------------------------------------------------------------------------------
        // Pasar datos al reporte
        // ---------------------------------------------------------------------------------------------------------

        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        ReportParameter[] param = new ReportParameter[5];
        param[0] = new ReportParameter("entidad", pUsuario.empresa);
        param[1] = new ReportParameter("nit", pUsuario.nitempresa);
        param[2] = new ReportParameter("fecha_inicial", txtFecIni.Texto);
        param[3] = new ReportParameter("fecha_final", txtFecFin.Texto);
        param[4] = new ReportParameter("ImagenReport", ImagenReporte());

        rvLibAux.LocalReport.EnableExternalImages = true;
        rvLibAux.LocalReport.SetParameters(param);
        //Se deja comentariado mientras se coloca un paràmetro general para que salga horizontal o vertical
        //rvLibAux.LocalReport.ReportPath = @"Page\Contabilidad\LibroAuxiliar\ReporteLibroAuxiliar.rdlc";
        //Consultando nombre de ciudad para el reporte
        GeneralService BOGeneral = new GeneralService();
        General pEntidad = BOGeneral.ConsultarGeneral(90174, Usuario);
        Int16 valor = pEntidad.valor == "" ? Convert.ToInt16(0) : Convert.ToInt16(pEntidad.valor);
        if (valor == 1) 
           rvLibAux.LocalReport.ReportPath = @"Page\Contabilidad\LibroAuxiliar\ReporteLibroAuxiliarH.rdlc";
        else
           rvLibAux.LocalReport.ReportPath = @"Page\Contabilidad\LibroAuxiliar\ReporteLibroAuxiliar.rdlc";
        
        ReportDataSource rds = new ReportDataSource("DataSet1", table);
        rvLibAux.LocalReport.DataSources.Clear();
        rvLibAux.LocalReport.DataSources.Add(rds);
        rvLibAux.LocalReport.Refresh();

        frmPrint.Visible = false;
        rvLibAux.Visible = true;
        // Mostrar el reporte en pantalla.
        mvLibroAuxiliar.ActiveViewIndex = 1;

    }


    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvLibroAuxiliar.ActiveViewIndex = 0;
    }

    protected void cbRango_CheckedChanged(object sender, EventArgs e)
    {
        if (cbRango.Checked == true)
        {
            PanelCuentaFin.Visible = true;
        }
        else
        {
            PanelCuentaFin.Visible = false;
        }
    }

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }

    protected void btnListadoPlan1_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuentaFin", "txtNomCuenta1");
    }

    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        if (txtCodCuenta.Text != "")
        {
            // Determinar los datos de la cuenta contable
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);
            //int rowIndex = Convert.ToInt32(txtCodCuenta.CommandArgument);

            // Mostrar el nombre de la cuenta            
            if (txtNomCuenta != null)
                txtNomCuenta.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuenta.Text = "";
        }
    }

    protected void txtCodCuentaFin_TextChanged(object sender, EventArgs e)
    {
        if (txtCodCuentaFin.Text != "")
        {
            // Determinar los datos de la cuenta contable
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuentaFin.Text, (Usuario)Session["usuario"]);
            //int rowIndex = Convert.ToInt32(txtCodCuenta.CommandArgument);

            // Mostrar el nombre de la cuenta            
            if (txtNomCuenta1 != null)
                txtNomCuenta1.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuenta1.Text = "";
        }
    }


    protected void btnImprime_Click(object sender, EventArgs e)
    {
        if (rvLibAux.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = rvLibAux.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
            Usuario pUsuario = new Usuario();
            string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_" + pUsuario.nombre : "";
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output" + pNomUsuario + ".pdf"),
            FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            Session["Archivo" + Usuario.codusuario] = Server.MapPath("output" + pNomUsuario + ".pdf");
            frmPrint.Visible = true;
            rvLibAux.Visible = false;
        }
    }


}