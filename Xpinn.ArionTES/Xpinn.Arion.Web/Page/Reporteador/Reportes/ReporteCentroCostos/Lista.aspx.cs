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
using Xpinn.Contabilidad.Entities;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using Microsoft.CSharp;
using System.Text;
using System.IO;
using Xpinn.Reporteador.Services;

partial class Lista : GlobalWeb
{

    ReporteService objReporteService = new ReporteService();
      private Xpinn.Contabilidad.Services.BalancePruebaService BalancePruebaSer = new Xpinn.Contabilidad.Services.BalancePruebaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(BalancePruebaSer.CodigoProgramaReporteCentroCostos, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalancePruebaSer.CodigoProgramaReporteCentroCostos, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarCombos();
                btnExportar.Visible = false;
                btnInforme.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalancePruebaSer.CodigoPrograma, "Page_Load", ex);
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
        ddlcentrocosto.DataSource = LstCentroCosto;
        ddlcentrocosto.DataTextField = "Descripcion";
        ddlcentrocosto.DataValueField = "centro_costo";
        ddlcentrocosto.DataBind();
        ddlcentrocosto.Items.Insert(0, new ListItem("CONSOLIDADO", "0"));
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

        // Llenar el DDL de la fecha de corte 
        try
        {
            Configuracion conf = new Configuracion();
            List<Xpinn.Contabilidad.Entities.BalancePrueba> lstFechaCierre = new List<Xpinn.Contabilidad.Entities.BalancePrueba>();
            Xpinn.Contabilidad.Services.BalancePruebaService BalancePruebaService = new Xpinn.Contabilidad.Services.BalancePruebaService();
            Xpinn.Contabilidad.Entities.BalancePrueba BalancePrueba = new Xpinn.Contabilidad.Entities.BalancePrueba();
            lstFechaCierre = BalancePruebaService.ListarFechaCorte((Usuario)Session["Usuario"]);
            ddlfechacierre.DataSource = lstFechaCierre;
            ddlfechacierre.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
            ddlfechacierre.DataTextField = "fecha";
            ddlfechacierre.DataBind();
            if (ddlfechacierre.SelectedIndex != null)
                ddlFechaCorte_SelectedIndexChanged(ddlfechacierre, null);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, BalancePruebaSer.CodigoPrograma);
        Actualizar(idObjeto);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, BalancePruebaSer.CodigoPrograma);
        gvLista.DataSource = null;
        gvLista.DataBind();
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar(idObjeto);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalancePruebaSer.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar(String pIdObjeto)
    {
        try
        {
            String emptyQuery = "Fila de datos vacia";   
            BalancePrueba datosApp = new BalancePrueba();
            Configuracion conf = new Configuracion();
            String format = conf.ObtenerFormatoFecha();
            List<BalancePrueba> lstConsulta = new List<BalancePrueba>();
            List<BalancePrueba> lstCentros = new List<BalancePrueba>();
           BalancePrueba lstValorCentros = new BalancePrueba();
            lstConsulta = BalancePruebaSer.ListarBalanceCentroCosto((Usuario)Session["Usuario"], ddlcentrocosto.SelectedValue.ToString(),ddlfechacierre.Text.ToString());
            lstCentros = BalancePruebaSer.ListarCentroCosto((Usuario)Session["Usuario"]);
            Session["Centro_Costos"] = lstCentros;
            Session["BalanceCentCostos"] = lstConsulta;
            DataRow drDatos;

            int posicion = 0;
            DataTable dtDatos = new DataTable();
            if (ddlcentrocosto.SelectedValue.ToString() == "0")
            {

                // Mostrar la tabla de datos
                gvLista.Columns.Clear();
               
                BoundField ColumnBoundCOD = new BoundField();
                ColumnBoundCOD.HeaderText = "Cod.Cuenta";
                ColumnBoundCOD.DataField = "Cod_Cuenta";

                ColumnBoundCOD.ItemStyle.Width = 100;
                ColumnBoundCOD.ControlStyle.Width = 100;
                ColumnBoundCOD.HeaderStyle.Width = 100;
                gvLista.Columns.Add(ColumnBoundCOD);
                dtDatos.Columns.Add("Cod_Cuenta", typeof(string));
                dtDatos.Columns["Cod_Cuenta"].AllowDBNull = true;
                dtDatos.Columns["Cod_Cuenta"].DefaultValue = "0";

                BoundField ColumnBoundNom = new BoundField();
                ColumnBoundNom.HeaderText = "Nombre";
                ColumnBoundNom.DataField = "Nombre";

                ColumnBoundNom.ItemStyle.Width = 100;
                ColumnBoundNom.ControlStyle.Width = 100;
                ColumnBoundNom.HeaderStyle.Width = 100;
                gvLista.Columns.Add(ColumnBoundNom);
                dtDatos.Columns.Add("Nombre", typeof(string));
                dtDatos.Columns["Nombre"].AllowDBNull = true;
                dtDatos.Columns["Nombre"].DefaultValue = "0";

                BoundField ColumnBoundVal = new BoundField();
                ColumnBoundVal.HeaderText = "Valor";
                ColumnBoundVal.DataField = "Valor";

                ColumnBoundVal.ItemStyle.Width = 100;
                ColumnBoundVal.ControlStyle.Width = 100;
                ColumnBoundVal.HeaderStyle.Width = 100;
                gvLista.Columns.Add(ColumnBoundVal);
                dtDatos.Columns.Add("Valor", typeof(string));
                dtDatos.Columns["Valor"].AllowDBNull = true;
                dtDatos.Columns["Valor"].DefaultValue = "0";

                foreach (BalancePrueba rFila in lstCentros)
                {
                    if (rFila.descipcion != null)
                    {
                        BoundField ColumnBoundKAP = new BoundField();
                        ColumnBoundKAP.HeaderText = rFila.descipcion;
                        ColumnBoundKAP.DataField = rFila.descipcion;
                      
                        ColumnBoundKAP.ItemStyle.Width = 100;
                        ColumnBoundKAP.ControlStyle.Width = 100;
                        ColumnBoundKAP.HeaderStyle.Width = 100;
                        gvLista.Columns.Add(ColumnBoundKAP);
                        dtDatos.Columns.Add(rFila.descipcion, typeof(string));
                        dtDatos.Columns[rFila.descipcion].AllowDBNull = true;
                        dtDatos.Columns[rFila.descipcion].DefaultValue = "0";
                    }
                }
             
                foreach (BalancePrueba rFila in lstConsulta)
                {



                    drDatos = dtDatos.NewRow();
                    if (rFila.cod_cuenta != null)
                    {
                        drDatos["Cod_Cuenta"] = Convert.ToString(rFila.cod_cuenta);
                        foreach (BalancePrueba rFila1 in lstCentros)
                        {
                            lstValorCentros = BalancePruebaSer.ListarValorCentroCosto(rFila1.descipcion, rFila.cod_cuenta, (Usuario)Session["Usuario"]);
                            drDatos[rFila1.descipcion] = Convert.ToString(lstValorCentros.valorcentro);
                        }
                    }
                    if (rFila.nombrecuenta != null)
                    {
                        drDatos["Nombre"] = Convert.ToString(rFila.nombrecuenta);
                        drDatos["Valor"] = Convert.ToString(rFila.valor);
                    }

                    dtDatos.Rows.Add(drDatos);


                }
                dtDatos.AcceptChanges();
            }
            else
            {
                // Mostrar la tabla de datos
                gvLista.Columns.Clear();

                BoundField ColumnBoundCOD = new BoundField();
                ColumnBoundCOD.HeaderText = "Cod.Cuenta";
                ColumnBoundCOD.DataField = "Cod_Cuenta";

                ColumnBoundCOD.ItemStyle.Width = 100;
                ColumnBoundCOD.ControlStyle.Width = 100;
                ColumnBoundCOD.HeaderStyle.Width = 100;
                gvLista.Columns.Add(ColumnBoundCOD);
                dtDatos.Columns.Add("Cod_Cuenta", typeof(string));
                dtDatos.Columns["Cod_Cuenta"].AllowDBNull = true;
                dtDatos.Columns["Cod_Cuenta"].DefaultValue = "0";

                BoundField ColumnBoundNom = new BoundField();
                ColumnBoundNom.HeaderText = "Nombre";
                ColumnBoundNom.DataField = "Nombre";

                ColumnBoundNom.ItemStyle.Width = 100;
                ColumnBoundNom.ControlStyle.Width = 100;
                ColumnBoundNom.HeaderStyle.Width = 100;
                gvLista.Columns.Add(ColumnBoundNom);
                dtDatos.Columns.Add("Nombre", typeof(string));
                dtDatos.Columns["Nombre"].AllowDBNull = true;
                dtDatos.Columns["Nombre"].DefaultValue = "0";

                BoundField ColumnBoundVal = new BoundField();
                ColumnBoundVal.HeaderText = "Valor";
                ColumnBoundVal.DataField = "Valor";

                ColumnBoundVal.ItemStyle.Width = 100;
                ColumnBoundVal.ControlStyle.Width = 100;
                ColumnBoundVal.HeaderStyle.Width = 100;
                gvLista.Columns.Add(ColumnBoundVal);
                dtDatos.Columns.Add("Valor", typeof(string));
                dtDatos.Columns["Valor"].AllowDBNull = true;
                dtDatos.Columns["Valor"].DefaultValue = "0";

                string Centro = ddlcentrocosto.SelectedItem.Text;
                BoundField ColumnBoundKAP = new BoundField();
                ColumnBoundKAP.HeaderText = Centro;
                ColumnBoundKAP.DataField = Centro;
                ColumnBoundKAP.DataFormatString = "{0:N}";
                ColumnBoundKAP.ItemStyle.Width = 100;
                ColumnBoundKAP.ControlStyle.Width = 100;
                ColumnBoundKAP.HeaderStyle.Width = 100;
                gvLista.Columns.Add(ColumnBoundKAP);
                dtDatos.Columns.Add(Centro, typeof(string));
                dtDatos.Columns[Centro].AllowDBNull = true;
                dtDatos.Columns[Centro].DefaultValue = "0";

                foreach (BalancePrueba rFila in lstConsulta)
                {
                    lstValorCentros = BalancePruebaSer.ListarValorCentroCosto(ddlcentrocosto.SelectedItem.Text, rFila.cod_cuenta, (Usuario)Session["Usuario"]);
                    drDatos = dtDatos.NewRow();
                    if (rFila.cod_cuenta != null)
                    {
                        drDatos["Cod_Cuenta"] = Convert.ToString(rFila.cod_cuenta);
                        drDatos[Centro] = Convert.ToString(lstValorCentros.valorcentro);

                    }
                    if (rFila.nombrecuenta != null)
                    {
                        drDatos["Nombre"] = Convert.ToString(rFila.nombrecuenta);
                        drDatos["Valor"] = Convert.ToString(rFila.valor);
                    }

                    dtDatos.Rows.Add(drDatos);

                }

            }
            Session["DTBALANCE"] = dtDatos;

            if (dtDatos.Rows.Count > 0)
            {
                mvBalance.ActiveViewIndex = 0;
                gvLista.DataBind();
                btnExportar.Visible = true;
                btnInforme.Visible = true;
            }
            else
            {
                mvBalance.ActiveViewIndex = -1;
                btnExportar.Visible = false;
                btnInforme.Visible = false;
            }
            gvLista.DataSource = dtDatos;
            gvLista.DataBind();

        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(BalancePruebaSer.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Util.Usuario ObtenerValores()
    {
        Xpinn.Util.Usuario vUsuario = new Xpinn.Util.Usuario();
        return vUsuario;
    }


    protected void chkCuentasCero_CheckedChanged(object sender, EventArgs e)
    {
        //if (Session["DTBALANCE"] != null)
        //Actualizar(idObjeto); 
    }

    protected void chkCompCentroCosto_CheckedChanged(object sender, EventArgs e)
    {
        //if (Session["DTBALANCE"] != null)
        // Actualizar(idObjeto); 
    }

    /// <summary>
    /// Crear DATATABLE con el listado de cuentas para poder generar el reporte
    /// </summary>
    /// <returns></returns>

    public DataTable CrearDataTable(String pIdObjeto)
    {
        DataRow drDatos;
        DataTable dtDatos = new DataTable();
         List<BalancePrueba> lstConsulta = new List<BalancePrueba>();
         List<BalancePrueba> lstCentros = new List<BalancePrueba>();
        BalancePrueba lstValorCentros = new BalancePrueba();
        lstConsulta = (List<BalancePrueba>)Session["BalanceCentCostos"];
         lstCentros = (List<BalancePrueba>)Session["Centro_Costos"];


        dtDatos.Columns.Add("CodCuenta");
        dtDatos.Columns.Add("Nombre");
        dtDatos.Columns.Add("Valor");
        dtDatos.Columns.Add("Int_1");
        dtDatos.Columns.Add("Int_2");
        dtDatos.Columns.Add("Int_3");
        dtDatos.Columns.Add("Int_4");
        dtDatos.Columns.Add("Int_5");
        dtDatos.Columns.Add("Int_6");
        dtDatos.Columns.Add("Int_7");
        dtDatos.Columns.Add("Int_8");
        dtDatos.Columns.Add("Int_9");
        dtDatos.Columns.Add("Int_10");
        dtDatos.Columns.Add("Int_11");
        dtDatos.Columns.Add("Int_12");
        dtDatos.Columns.Add("Int_13");
        dtDatos.Columns.Add("Int_14");
        dtDatos.Columns.Add("Int_15");
        dtDatos.Columns.Add("Int_16");
        dtDatos.Columns.Add("Int_17");
        dtDatos.Columns.Add("Int_18");
        dtDatos.Columns.Add("Int_19");
        dtDatos.Columns.Add("Int_20");

        if (ddlcentrocosto.SelectedValue.ToString() == "0")
        {
            foreach (BalancePrueba rFila in lstConsulta)
            {

                drDatos = dtDatos.NewRow();
                if (rFila.cod_cuenta != null)
                {
                    drDatos["CodCuenta"] = Convert.ToString(rFila.cod_cuenta);
                    int p = 1;

                    foreach (BalancePrueba rFila1 in lstCentros)
                    {
                        lstValorCentros = BalancePruebaSer.ListarValorCentroCosto(rFila1.descipcion, rFila.cod_cuenta, (Usuario)Session["Usuario"]);

                        drDatos["Int_" + p] = lstValorCentros.valorcentro.ToString("0,0");
                        p = p + 1;

                    }
                    for (int i = p; i < 21; i++)
                    {
                        drDatos["Int_" + i] = "0";

                    }

                }
                if (rFila.nombrecuenta != null)
                {
                    drDatos["Nombre"] = Convert.ToString(rFila.nombrecuenta);
                    drDatos["Valor"] = Convert.ToString(rFila.valor);
                }

                dtDatos.Rows.Add(drDatos);


            }
      
        dtDatos.AcceptChanges();
        }
        else
        {
            foreach (BalancePrueba rFila in lstConsulta)
            {
                drDatos = dtDatos.NewRow();
                if (rFila.cod_cuenta != null)
                {
                    drDatos["CodCuenta"] = Convert.ToString(rFila.cod_cuenta);
                    drDatos["Int_1"] = Convert.ToString(rFila.valorcentro);
                    drDatos["Int_2"] = "0,0";
                    drDatos["Int_3"] = "0,0";
                    drDatos["Int_4"] = "0,0";
                    drDatos["Int_5"] = "0,0";
                    drDatos["Int_6"] = "0,0";
                    drDatos["Int_7"] = "0,0";
                    drDatos["Int_8"] = "0,0";
                    drDatos["Int_9"] = "0,0";
                    drDatos["Int_10"] = "0,0";
                    drDatos["Int_11"] = "0,0";
                    drDatos["Int_12"] = "0,0";
                    drDatos["Int_13"] = "0,0";
                    drDatos["Int_14"] = "0,0";
                    drDatos["Int_15"] = "0,0";
                    drDatos["Int_16"] = "0,0";
                    drDatos["Int_17"] = "0,0";
                    drDatos["Int_18"] = "0,0";
                    drDatos["Int_19"] = "0,0";
                    drDatos["Int_10"] = "0,0";
                }
                if (rFila.nombrecuenta != null)
                {
                    drDatos["Nombre"] = Convert.ToString(rFila.nombrecuenta);
                    drDatos["Valor"] = Convert.ToString(rFila.valor);
                }

                dtDatos.Rows.Add(drDatos);

            }

        }

        return dtDatos;


    }
    protected void btnInforme_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            string cRutaDeImagen;
            cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";
            List<BalancePrueba> lstConsulta = new List<BalancePrueba>();
            List<BalancePrueba> lstCentros = new List<BalancePrueba>();
            lstConsulta = (List<BalancePrueba>)Session["BalanceCentCostos"];
            lstCentros = (List<BalancePrueba>)Session["Centro_Costos"];
            Boolean[] bVisible = new Boolean[22];
            ReportParameter[] param = new ReportParameter[46];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
           
            int j = 1;
            if (ddlcentrocosto.SelectedValue.ToString() == "0")
            {
                foreach (BalancePrueba item in lstCentros)
                {
                    param[1 + j] = new ReportParameter("Titulo" + j, item.descipcion);
                    j = j + 1;
                }
                for (int i = j; i < 21; i++)
                {
                    param[1 + i] = new ReportParameter("Titulo" + i, " ");
                }
           
          
                for (int i = 1; i <= 21; i++)
                {
                    bVisible[i] = false;
                    i = i + 1;
                }
          
            int p = 2;
            foreach (BalancePrueba rFila in lstCentros)
            {
                if (rFila.descipcion != null) { bVisible[p] = true; }
                p = p + 1;
            }
            }
            else
            {
                param[2] = new ReportParameter("Titulo" + j, ddlcentrocosto.SelectedItem.Text);
                for (int i = 2; i < 21; i++)
                {
                    param[1 + i] = new ReportParameter("Titulo" + i, " ");
                }
                for (int i = 1; i <= 21; i++)
                {
                    bVisible[i] = false;
                    i = i + 1;
                }
               bVisible[2] = true; 

            }
            for (int i = 1; i < 21; i++)
            {
                param[21 + i] = new ReportParameter("Visible" + i, bVisible[i + 1].ToString());
            }
            param[42] = new ReportParameter("ImagenReport", cRutaDeImagen);
            param[43] = new ReportParameter("representante_legal", pUsuario.representante_legal);
            param[44] = new ReportParameter("contador", pUsuario.contador);
            param[45] = new ReportParameter("tarjeta_contador", pUsuario.tarjeta_contador);
            RptReporte.LocalReport.EnableExternalImages = true;
            RptReporte.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTable(idObjeto));
            mvBalance.Visible = true;
            RptReporte.LocalReport.DataSources.Clear();
            RptReporte.LocalReport.DataSources.Add(rds);
           
            RptReporte.LocalReport.Refresh();
            // Mostrar el reporte en pantalla.
            mvBalance.ActiveViewIndex = 1;
            RptReporte.Visible = true;
        }
        
    
        catch (Exception)
        {

            throw;
        }
       
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTBALANCE"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTBALANCE"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=AGENCIAS.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.UTF8;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
       
    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvBalance.ActiveViewIndex = 0;
    }

  
    protected void ddlFechaCorte_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
}