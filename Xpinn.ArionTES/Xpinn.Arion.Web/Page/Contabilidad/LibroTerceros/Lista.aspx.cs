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

partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.LibroTercerosService LibroTercerosSer = new Xpinn.Contabilidad.Services.LibroTercerosService();
    private static string pCod_Programa;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Request.UrlReferrer.ToString().Contains("niif"))
            {
                VisualizarOpciones(LibroTercerosSer.CodigoProgramaNIIF, "L");
                pCod_Programa = LibroTercerosSer.CodigoProgramaNIIF;
                ViewState.Add("COD_PROGRAMA", "NIIF");
            }
            else
            {
                VisualizarOpciones(LibroTercerosSer.CodigoPrograma, "L");
                pCod_Programa = LibroTercerosSer.CodigoPrograma;
                ViewState.Add("COD_PROGRAMA", "LOCAL");
            }
            
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                btnExportar.Visible = false;
                btnInforme.Visible = false;
                cbCuenta.Checked = true;
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
                CargarValoresConsulta(pConsulta, pCod_Programa);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(pCod_Programa, "Page_Load", ex);
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
        foreach(Xpinn.Contabilidad.Entities.CentroCosto ItemCC in LstCentroCosto)        
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


    //    llenar el ddl de terceros
    //    list<xpinn.contabilidad.entities.tercero> lstterceros = new list<xpinn.contabilidad.entities.tercero>();
    //    lstterceros = librotercerosser.listarterceros((usuario)session["usuario"], sfiltro);
    //    ddlterceroini.datasource = lstterceros;
    //    ddlterceroini.datatextfield = "identificacion";
    //    ddlterceroini.datavaluefield = "identificacion";
    //    ddlterceroini.databind();
    //    ddltercerofin.datasource = lstterceros;
    //    ddltercerofin.datatextfield = "identificacion";
    //    ddltercerofin.datavaluefield = "identificacion";
    //    ddltercerofin.databind();
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, pCod_Programa);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, pCod_Programa);
        DataTable dtVacio = new DataTable();
        gvLista.DataSource = dtVacio;
        gvLista.DataBind();
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
        btnExportar.Visible = false;
        btnInforme.Visible = false;
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string sDetalle = "";
                sDetalle = e.Row.Cells[13].Text;
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
            BOexcepcion.Throw(pCod_Programa, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");

        try
        {
            List<Xpinn.Contabilidad.Entities.LibroTerceros> lstConsulta = new List<Xpinn.Contabilidad.Entities.LibroTerceros>();
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
            //FALTA PASAR EL PARAMETRO DE LA MONEDA


            // determinar los datos para generar el libro auxiliar
            FecIni = DateTime.ParseExact(txtFecIni.ToDate, gFormatoFecha, null);
            FecFin = DateTime.ParseExact(txtFecFin.ToDate, gFormatoFecha, null);

            // Determinando la fecha Inicial y Final
            if (ViewState["COD_PROGRAMA"] != null)
            {
                if (ViewState["COD_PROGRAMA"].ToString() == "NIIF")
                    lstConsulta = LibroTercerosSer.ListarAuxiliarTercerosNIIF(txtCodCuenta.Text, txtCodCuentaFin.Text, ddlTerceroIni.Text, ddlTerceroFin.Text, CenIni, CenFin, FecIni, FecFin, cbCuenta.Checked, (Usuario)Session["Usuario"]);
                else
                    lstConsulta = LibroTercerosSer.ListarAuxiliarTerceros(txtCodCuenta.Text, txtCodCuentaFin.Text, ddlTerceroIni.Text, ddlTerceroFin.Text, CenIni, CenFin, FecIni, FecFin, cbCuenta.Checked, (Usuario)Session["Usuario"]);
            }
            else
                lstConsulta = LibroTercerosSer.ListarAuxiliarTerceros(txtCodCuenta.Text, txtCodCuentaFin.Text, ddlTerceroIni.Text, ddlTerceroFin.Text, CenIni, CenFin, FecIni, FecFin, cbCuenta.Checked, (Usuario)Session["Usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            Session["DTLibroTerceros"] = lstConsulta;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                btnExportar.Visible = true;
                btnInforme.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();                
            }
            else
            {
                btnExportar.Visible = false;
                btnInforme.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> No se encontraron Registros";
            }

            Session.Add(pCod_Programa + ".consulta", 1);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTLibroTerceros"] != null)
            {
                ExportarGridCSVDirecto(gvLista, "LibroTerceros");
                /*StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTLibroTerceros"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=LibroTerceros.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();*/
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


    protected void btnInforme_Click(object sender, EventArgs e)
    {
        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTLibroTerceros"] == null)
        {
            VerError("No ha generado el libro de terceros para poder imprimir el reporte");
        }

        List<Xpinn.Contabilidad.Entities.LibroTerceros> lstConsulta = new List<Xpinn.Contabilidad.Entities.LibroTerceros>();
        lstConsulta = (List<Xpinn.Contabilidad.Entities.LibroTerceros>)Session["DTLibroTerceros"];

        // LLenar data table con los datos a recoger
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("cod_cuenta");
        table.Columns.Add("nombre_cuenta");
        table.Columns.Add("tipo");
        table.Columns.Add("codigo");
        table.Columns.Add("identificacion");
        table.Columns.Add("tipo_iden", typeof(Int32));
        table.Columns.Add("nom_tipo_iden");
        table.Columns.Add("nombre");
        table.Columns.Add("tipo_persona");
        table.Columns.Add("regimen");
        table.Columns.Add("fecha", typeof(DateTime));
        DataColumn colNumComp = new DataColumn();
        colNumComp.ColumnName = "num_comp";
        colNumComp.DataType = typeof(Int64);
        colNumComp.AllowDBNull = true;
        table.Columns.Add(colNumComp);
        table.Columns.Add("tipo_comp");
        table.Columns.Add("detalle");
        table.Columns.Add("tipo_mov");
        table.Columns.Add("valor", typeof(decimal));
        table.Columns.Add("saldo", typeof(decimal));
        table.Columns.Add("concepto");
        table.Columns.Add("tipo_benef");
        table.Columns.Add("num_sop");
        DataColumn colCentroCosto = new DataColumn();
        colCentroCosto.ColumnName = "centro_costo";
        colCentroCosto.DataType = typeof(Int64);
        colCentroCosto.AllowDBNull = true;
        table.Columns.Add(colCentroCosto);
        table.Columns.Add("debito", typeof(decimal));
        table.Columns.Add("credito", typeof(decimal));
        table.Columns.Add("depende_de");


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
            foreach(Xpinn.Contabilidad.Entities.LibroTerceros refe in lstConsulta)
            {
                datarw = table.NewRow();
                datarw[0] = " " + refe.cod_cuenta;
                datarw[1] = " " + refe.nombre_cuenta;
                datarw[2] = " " + refe.tipo;
                datarw[3] = " " + refe.codigo;
                datarw[4] = " " + refe.identificacion;
                datarw[5] = " " + Convert.ToInt32(refe.tipo_iden);
                datarw[6] = " " + refe.nom_tipo_iden;
                datarw[7] = " " + refe.nombre;
                datarw[8] = " " + refe.tipo_persona;
                datarw[9] = " " + refe.regimen;
                if (refe.fecha == null)
                    datarw[10] = DBNull.Value;
                else
                    datarw[10] = " " + Convert.ToDateTime(refe.fecha).ToShortDateString();
                if (refe.centro_costo == null)
                    datarw[11] = DBNull.Value;
                else
                    datarw[11] = " " + refe.num_comp;
                datarw[12] = " " + refe.tipo_comp;
                datarw[13] = " " + refe.detalle;
               //if (refe.tipo == "D" )
               //     refe.tipo_mov = refe.tipo;
               // if (refe.tipo == "C")
               //     refe.tipo_mov = refe.tipo;


                datarw[14] = " " + refe.tipo_mov;
                datarw[15] = " " + refe.valor;
                datarw[16] = " " + refe.saldo;
                datarw[17] = " " + refe.concepto;
                datarw[18] = " " + refe.tipo_benef;
                datarw[19] = " " + refe.num_sop;
                if (refe.centro_costo == null)
                    datarw[20] = DBNull.Value;
                else
                    datarw[20] = " " + refe.centro_costo;
                if (refe.tipo_mov == "D")
                {
                    datarw[21] = " " + refe.valor;
                    datarw[22] = " " + 0;
                }
                else
                {
                    datarw[21] = " " + 0;
                    datarw[22] = " " + refe.valor;
                }
                datarw[23] = " " + refe.depende_de;
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
        ReportDataSource rds = new ReportDataSource("DataSet1", table);
        rvLibAux.LocalReport.DataSources.Clear();
        rvLibAux.LocalReport.DataSources.Add(rds);
        rvLibAux.LocalReport.ReportPath = "Page/Contabilidad/LibroTerceros/ReporteLibroTerceros.rdlc";
        rvLibAux.LocalReport.Refresh();

        // Mostrar el reporte en pantalla.
        mvLibroTerceros.ActiveViewIndex = 1;
        Site toolBar = (Site)Master;
        toolBar.MostrarConsultar(false);
        toolBar.MostrarLimpiar(false);
    }


    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvLibroTerceros.ActiveViewIndex = 0;
        Site toolBar = (Site)Master;
        toolBar.MostrarConsultar(true);
        toolBar.MostrarLimpiar(true);
    }

    protected void cbCuenta_CheckedChanged(object sender, EventArgs e)
    {
        if (cbCuenta.Checked == true)
            cbTercero.Checked = false;            
        else
            cbTercero.Checked = true;  
    }

    protected void cbTercero_CheckedChanged(object sender, EventArgs e)
    {
        if (cbTercero.Checked == true)
            cbCuenta.Checked = false;
        else
            cbCuenta.Checked = true;  
    }

    protected void btnTerceroIni_Click(object sender, EventArgs e)
    {
        ctlBusquedaTerceroIni.Motrar(true, "ddlTerceroIni", "");
    }

    protected void btnTerceroFin_Click(object sender, EventArgs e)
    {
        ctlBusquedaTerceroFin.Motrar(true, "ddlTerceroFin", "");
    }

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        if (ViewState["COD_PROGRAMA"] != null)
        {
            if (ViewState["COD_PROGRAMA"].ToString() == "NIIF")
                listPlanCtasNIIF.Motrar(true, "txtCodCuenta", "txtNomCuenta");
            else
                ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
        }
    }

    protected void btnListadoPlan1_Click(object sender, EventArgs e)
    {
        if (ViewState["COD_PROGRAMA"] != null)
        {
            if (ViewState["COD_PROGRAMA"].ToString() == "NIIF")
                listPlanCtasNIIF2.Motrar(true, "txtCodCuentaFin", "txtNomCuenta1");
            else
                ctlListadoPlan.Motrar(true, "txtCodCuentaFin", "txtNomCuenta1");
        }
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

}