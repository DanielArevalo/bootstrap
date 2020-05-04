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
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

public partial class Lista : GlobalWeb
{
    private Xpinn.CDATS.Services.RepVencimientoCDATService RepVencimientoSer = new Xpinn.CDATS.Services.RepVencimientoCDATService();
    private Xpinn.CDATS.Services.AperturaCDATService AperturaCDATSer = new Xpinn.CDATS.Services.AperturaCDATService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(RepVencimientoSer.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarImprimir(false);
            toolBar.MostrarLimpiar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RepVencimientoSer.CodigoPrograma, "Page_PreInit", ex);
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
                txt_fechainicial.ToDateTime = System.DateTime.Now;
                txt_fechafinal.ToDateTime = System.DateTime.Now;
                pDatos.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RepVencimientoSer.CodigoPrograma, "Page_Load", ex);
        }
    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, RepVencimientoSer.CodigoPrograma);
        Actualizar();
    }
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, RepVencimientoSer.CodigoPrograma);
        DataTable dtVacio = new DataTable();
        gvLista.DataSource = dtVacio;
        gvLista.DataBind();
        Site toolBar = (Site)this.Master;
        toolBar.eventoConsultar += btnConsultar_Click;
        toolBar.eventoLimpiar += btnLimpiar_Click;
        toolBar.eventoExportar += btnExportar_Click;
        toolBar.eventoImprimir += btnImprimir_Click;
        toolBar.MostrarExportar(false);
        toolBar.MostrarImprimir(false);
        toolBar.MostrarLimpiar(false);
        ddlOficina.SelectedIndex = 0;
        txt_fechainicial.ToDateTime = DateTime.Now;
        txt_fechafinal.ToDateTime = DateTime.Now;
        pDatos.Visible = false;
    }
    protected void LlenarCombos()
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
        ddlOficina.DataTextField = "nombre";
        ddlOficina.DataValueField = "cod_oficina";
        ddlOficina.Items.Insert(0,new ListItem {Text="Seleccione oficina",Value="0" });
        ddlOficina.DataSource = oficinaServicio.ListarOficina(oficina, pUsuario);
        ddlOficina.DataBind();

    }
    private void Actualizar()
    {
        try
        {         

            string[] data = new string[3];
            data[0] = txt_fechainicial.Text; 
            data[1] = txt_fechafinal.Text;
            data[2] = ddlOficina.SelectedValue;
            Usuario pUsuario = (Usuario)Session["Usuario"];
            List<Xpinn.CDATS.Entities.RepVencimientoCDAT> lstRepVencimientoCDAT = new List<RepVencimientoCDAT>();
            lstRepVencimientoCDAT = RepVencimientoSer.ListarRepVencimientoCDAT(data, pUsuario);
            int recorrido = 0;
            foreach (RepVencimientoCDAT RV in lstRepVencimientoCDAT)
            {
                LiquidacionCDATService LiquiService = new LiquidacionCDATService();
                LiquidacionCDAT pLiqui = new LiquidacionCDAT();
                LiquidacionCDAT Liquiresult = new LiquidacionCDAT();
                pLiqui.fecha_liquidacion = Convert.ToDateTime(RV.fecha_vencimiento);
                pLiqui.numero_cdat = RV.numero_cdat;
                pLiqui.valor = 0;
                pLiqui.interes_causado = 0;
                pLiqui.interes = 0;
                pLiqui.retencion = 0;
                pLiqui.valor_gmf = 0;
                pLiqui.valor_pagar = 0;
                Liquiresult = LiquiService.CalculoLiquidacionCDAT(pLiqui, pUsuario);
                lstRepVencimientoCDAT[recorrido].interes_retencion= Liquiresult.retencion;
                lstRepVencimientoCDAT[recorrido].interes_mes = Liquiresult.interes;
                lstRepVencimientoCDAT[recorrido].interes_causado = Liquiresult.interes_causado;
                recorrido += 1; 
            }
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstRepVencimientoCDAT;
            if (lstRepVencimientoCDAT.Count > 0)
            {
                decimal total_valor = lstRepVencimientoCDAT.ToList().Select(x => x.valor).Sum();
                decimal? total_causado = lstRepVencimientoCDAT.ToList().Select(x => x.interes_causado).Sum();
                decimal? total_mes = lstRepVencimientoCDAT.ToList().Select(x => x.interes_mes).Sum();
                decimal? total_retencion = lstRepVencimientoCDAT.ToList().Select(x => x.interes_retencion).Sum();
                txtTotalCDAT.Text = total_valor.ToString();
                txtTotalCau.Text = total_causado.ToString();
                txtTotalMes.Text = total_mes.ToString();
                txtTotalRet.Text = total_retencion.ToString();
                Site toolBar = (Site)this.Master;
                gvLista.Visible = true;
                gvLista.DataBind();
                pDatos.Visible = true;
                Session["DTCDAT"] = lstRepVencimientoCDAT;
                toolBar.MostrarExportar(true);
                toolBar.MostrarImprimir(true);
                toolBar.MostrarLimpiar(true);
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                Session["DTCDAT"] = null;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarExportar(false);
                toolBar.MostrarImprimir(false);
                toolBar.MostrarLimpiar(false);
                gvLista.Visible = false;
                pDatos.Visible = false;
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RepVencimientoSer.CodigoPrograma, "Actualizar", ex);
        }
    }
    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (gvLista.Rows.Count > 0 && Session["DTCDAT"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTCDAT"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=CDATVencimiento.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
            else
            {
                VerError("No existen Datos");
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 1;
        Site toolBar = (Site)Master;
        toolBar.MostrarExportar(false);
        toolBar.MostrarImprimir(false);
        toolBar.MostrarConsultar(false);
        toolBar.MostrarLimpiar(false);
        muestraInformeReporte();
    }

    void muestraInformeReporte()
    {
        VerError("");
        if (Session["DTCDAT"] == null)
        {
            VerError("No ha generado el Reporte para poder imprimir informacion");
        }
        else
        {
            List<RepVencimientoCDAT> lstConsulta = new List<RepVencimientoCDAT>();
            lstConsulta = (List<RepVencimientoCDAT>)Session["DTCDAT"];

            // LLenar data table con los datos a recoger
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("numero_cdat");
            table.Columns.Add("fecha_apertura");
            table.Columns.Add("fecha_vencimiento");
            table.Columns.Add("valor");
            table.Columns.Add("plazo");
            table.Columns.Add("identificacion");
            table.Columns.Add("nombres");
            table.Columns.Add("apellidos");
            table.Columns.Add("direccion");
            table.Columns.Add("telefono");
            table.Columns.Add("tasa_efectiva");
            table.Columns.Add("tasa_nominal");
            table.Columns.Add("nom_modalidad");
            table.Columns.Add("nom_periodo");
            table.Columns.Add("interes_causado");
            table.Columns.Add("interes_mes");
            table.Columns.Add("interes_retencion");

            foreach (RepVencimientoCDAT item in lstConsulta)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = item.numero_cdat;
                datarw[1] = item.fecha_apertura.ToShortDateString();
                datarw[2] = item.fecha_vencimiento.ToShortDateString();
                datarw[3] = item.valor.ToString("n");
                datarw[4] = item.plazo;
                datarw[5] = item.identificacion;
                datarw[6] = item.nombres;
                datarw[7] = item.apellidos;
                datarw[8] = item.direccion;
                datarw[9] = item.telefono;
                datarw[10] = item.tasa_efectiva;
                datarw[11] = item.tasa_nominal;
                datarw[12] = item.nom_modalidad;
                datarw[13] = item.nom_periodo;
                datarw[14] = item.interes_causado;             
                datarw[15] = item.interes_mes;
                datarw[16] = item.interes_retencion;
                table.Rows.Add(datarw);
            }
            // ---------------------------------------------------------------------------------------------------------
            // Pasar datos al reporte
            // ---------------------------------------------------------------------------------------------------------

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[4];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("Usuario", pUsuario.nombre);
            param[3] = new ReportParameter("ImagenReport", ImagenReporte());

            rvReporte.LocalReport.EnableExternalImages = true;
            rvReporte.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvReporte.LocalReport.DataSources.Clear();
            rvReporte.LocalReport.DataSources.Add(rds);
            rvReporte.LocalReport.Refresh();
        }
    }
    protected void btnDatos_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarExportar(true);
        toolBar.MostrarImprimir(true);
        toolBar.MostrarConsultar(true);
        mvPrincipal.ActiveViewIndex = 0;
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        AperturaCDATService ApertuService = new AperturaCDATService();
        //RECUPERAR  PERMITE CIERRE ANTICIPADO
        Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
        pData = ConsultaData.ConsultarGeneral(581, (Usuario)Session["usuario"]);
        DateTime fechavencimiento = Convert.ToDateTime(gvLista.Rows[e.NewEditIndex].Cells[4].Text);

        if (fechavencimiento < DateTime.Now)
        {
            String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
            Session[ApertuService.CodigoProgramaCierre + ".id"] = id;
            Navegar("../Cierre/Nuevo.aspx");
        }


        if (pData.valor == "1" && fechavencimiento > DateTime.Now)
        {
            String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
            Session[ApertuService.CodigoProgramaCierre + ".id"] = id;
            Navegar("../Cierre/Nuevo.aspx");
        }
        else
        {
            VerError("El Cdat no permite cierre anticipado");
            e.NewEditIndex = -1;
        }


    }

}