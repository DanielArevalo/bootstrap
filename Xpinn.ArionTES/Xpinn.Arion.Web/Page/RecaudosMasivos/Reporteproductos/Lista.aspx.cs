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
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

partial class Lista : GlobalWeb
{
    RecaudosMasivosService SoliServicios = new RecaudosMasivosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(SoliServicios.CodigoProgramaEnMora, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoProgramaEnMora, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropdown();
                txtFechaReporte.ToDateTime = DateTime.Now;
                lineaproductos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoProgramaEnMora, "Page_Load", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        VerError("");
        mvPrincipal.ActiveViewIndex = 0;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtMora1.Text == "") 
        {
            VerError("Digite el rango de dias de mora");
            return;
        }

        if (txtMora2.Text == "")
        {
            VerError("Digite el rango de dias de mora");
            return;
        }

        Page.Validate();
        gvLista.Visible = true;
        gvimpresion.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }
    protected void btnInforme4_Click(object sender, EventArgs e)
    {

        Navegar(Pagina.Lista);
        mvPrincipal.ActiveViewIndex = 0;
    }


    protected void btnImprime_Click(object sender, EventArgs e)
    {

        if (rvExtracto.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = rvExtracto.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output.pdf"),
            FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            Session["Archivo" + Usuario.codusuario] = Server.MapPath("output.pdf");
            frmPrint.Visible = true;
            rvExtracto.Visible = false;

        }
    }

    private void CargarDropdown()
    {

        Xpinn.Tesoreria.Services.EmpresaRecaudoServices linahorroServicio = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();
        Xpinn.Tesoreria.Entities.EmpresaRecaudo linahorroVista = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
        ddlempresarecaudo.DataTextField = "NOM_EMPRESA";
        ddlempresarecaudo.DataValueField = "COD_EMPRESA";
        ddlempresarecaudo.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlempresarecaudo.DataSource = linahorroServicio.ListarEmpresaRecaudo(linahorroVista, (Usuario)Session["usuario"]);
   
        if (ddlempresarecaudo.SelectedIndex == 0)
            ddlempresarecaudo.SelectedIndex = 1;        
        ddlempresarecaudo.DataBind();
        ddlempresarecaudo.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlempresarecaudo.SelectedValue =  "0";

        ddlTipo_Producto.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipo_Producto.Items.Insert(1, new ListItem("APORTES", "1"));
        ddlTipo_Producto.Items.Insert(2, new ListItem("CREDITOS", "2"));
        ddlTipo_Producto.Items.Insert(3, new ListItem("AHORROS A LA VISTA", "3"));
        ddlTipo_Producto.Items.Insert(4, new ListItem("AHORRO CONTRACTUAL", "4"));
        ddlTipo_Producto.Items.Insert(5, new ListItem("SERVICIOS", "5"));
        ddlTipo_Producto.Items.Insert(5, new ListItem("AFILIACION", "6"));
        ddlTipo_Producto.SelectedIndex = 0;
        ddlTipo_Producto.DataBind();

        Xpinn.FabricaCreditos.Services.OficinaService oficinaService = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();

        Usuario usuap = (Usuario)Session["usuario"];

        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);
        ddlOficinas.AppendDataBoundItems = true;
        if (consulta >= 1)
        {
            ddlOficinas.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
            ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlOficinas.DataTextField = "nombre";
            ddlOficinas.DataValueField = "codigo";
            ddlOficinas.DataBind();
            ddlOficinas.SelectedValue = Convert.ToString(usuap.cod_oficina);
            ddlOficinas.Enabled = true;
        }
        else
        {
            ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlOficinas.Items.Insert(1, new ListItem(Convert.ToString(usuap.nombre_oficina), Convert.ToString(usuap.cod_oficina)));
            ddlOficinas.DataBind();
            ddlOficinas.SelectedValue = Convert.ToString(usuap.cod_oficina);
            ddlOficinas.Enabled = false;
        }

        lineaproductos(); 
    }

    protected void lineaproductos() 
    {
        if (ddlTipo_Producto.SelectedIndex == 1)
        {    
            Xpinn.Aportes.Services.LineaAporteServices linahorroServicios = new Xpinn.Aportes.Services.LineaAporteServices();
            Xpinn.Aportes.Entities.LineaAporte linahorroVistas = new Xpinn.Aportes.Entities.LineaAporte();
      
            ddlLineaProducto.DataTextField = "NOMBRE";
            ddlLineaProducto.DataValueField = "COD_LINEA_APORTE";

            ddlLineaProducto.DataSource = linahorroServicios.ListarLineaAporte(linahorroVistas, (Usuario)Session["usuario"]);
            ddlLineaProducto.DataBind();
            ddlLineaProducto.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        }
        if (ddlTipo_Producto.SelectedIndex == 2)
        {

            Xpinn.FabricaCreditos.Services.LineasCreditoService linahorroServicios = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
            Xpinn.FabricaCreditos.Entities.LineasCredito linahorroVistas = new Xpinn.FabricaCreditos.Entities.LineasCredito();
            ddlLineaProducto.DataTextField = "NOMBRE";
            ddlLineaProducto.DataValueField = "COD_LINEA_CREDITO";
           
            ddlLineaProducto.DataSource = linahorroServicios.ListarLineasCredito(linahorroVistas, (Usuario)Session["usuario"]);
            ddlLineaProducto.DataBind();
            ddlLineaProducto.Items.Insert(0, new ListItem("Seleccione un item", "0"));          
        }


        if (ddlTipo_Producto.SelectedIndex == 3)
        {
            Xpinn.Ahorros.Services.LineaAhorroServices linahorroServicios = new Xpinn.Ahorros.Services.LineaAhorroServices();
            Xpinn.Ahorros.Entities.LineaAhorro linahorroVistas = new Xpinn.Ahorros.Entities.LineaAhorro();
            ddlLineaProducto.DataTextField = "DESCRIPCION";
            ddlLineaProducto.DataValueField = "COD_LINEA_AHORRO";

            ddlLineaProducto.DataSource = linahorroServicios.ListarLineaAhorro(linahorroVistas, (Usuario)Session["usuario"]);

            if (ddlLineaProducto.SelectedIndex == 0)
                ddlLineaProducto.SelectedIndex = 1;
            ddlLineaProducto.DataBind();
            ddlLineaProducto.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        }

        if (ddlTipo_Producto.SelectedIndex == 4)
        {

            Xpinn.Programado.Services.LineasProgramadoServices linahorroServicios = new Xpinn.Programado.Services.LineasProgramadoServices();
            Xpinn.Programado.Entities.LineasProgramado linahorroVistas = new Xpinn.Programado.Entities.LineasProgramado();
            ddlLineaProducto.DataTextField = "NOMBRE";
            ddlLineaProducto.DataValueField = "COD_LINEA_PROGRAMADO";
            string FILTRO = "";
            ddlLineaProducto.DataSource = linahorroServicios.ListarLineasProgramado(FILTRO,(Usuario)Session["usuario"]);
            if (ddlLineaProducto.SelectedIndex == 0)
                
            ddlLineaProducto.DataBind();
            ddlLineaProducto.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        }

      if (ddlTipo_Producto.SelectedIndex == 5)
        {

            Xpinn.Servicios.Services.LineaServiciosServices linahorroServicios = new Xpinn.Servicios.Services.LineaServiciosServices();
            Xpinn.Servicios.Entities.LineaServicios linahorroVistas = new Xpinn.Servicios.Entities.LineaServicios();
            ddlLineaProducto.DataTextField = "NOMTIPOSERVICIO";
            ddlLineaProducto.DataValueField = "COD_LINEA_SERVICIO";
            string FILTRO = "";
            ddlLineaProducto.DataSource = linahorroServicios.ListarLineaServicios(linahorroVistas, (Usuario)Session["usuario"], FILTRO);
            if (ddlLineaProducto.SelectedIndex == 0)
           
            ddlLineaProducto.DataBind();
            ddlLineaProducto.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            gvimpresion.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoProgramaEnMora, "gvLista_PageIndexChanging", ex);
        }
    }
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        String ids = gvimpresion.DataKeys[gvimpresion.SelectedRow.RowIndex].Value.ToString();
        Session[SoliServicios.CodigoProgramaEnMora + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    private void Actualizar()
    {
        try
        {
            List<RecaudosMasivos> lstConsulta = new List<RecaudosMasivos>();            
            RecaudosMasivos eRecaudo = new RecaudosMasivos();
            DateTime pFecha;
            pFecha = txtFechaReporte.ToDateTime;
            lstConsulta = SoliServicios.ListadoProductosEnMora(pFecha, eRecaudo, obtFiltro(), (Usuario)Session["usuario"]);
            
            gvLista.PageSize = 15;
            gvimpresion.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvimpresion.EmptyDataText = emptyQuery;
            if (lstConsulta.Count > 0)
            {
                ViewState["DTLISTA"] = lstConsulta;
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                gvimpresion.DataSource = lstConsulta;
                gvimpresion.DataBind();
                panelimpresion.Visible = false;
                panelGrilla.Visible = true;
                btnImprimir.Visible = true;
                btnExportar.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();               
            }
            else
            {
                panelimpresion.Visible = false;
                panelGrilla.Visible = false;
                btnImprimir.Visible = false;
                btnExportar.Visible = false;
                lblTotalRegs.Visible = false;
            }
            Session.Add(SoliServicios.CodigoProgramaEnMora + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SoliServicios.CodigoProgramaEnMora, "Actualizar", ex);
        }
    }


    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        List<RecaudosMasivos> lstConsulta = new List<RecaudosMasivos>();

        RecaudosMasivos entidad = new RecaudosMasivos();
        DateTime pFecha;
        pFecha = txtFechaReporte.ToDateTime;
        lstConsulta = SoliServicios.ListadoProductosEnMora(pFecha, entidad, obtFiltro(), (Usuario)Session["usuario"]);

        //tabla general 
        DataTable tablegeneral = new DataTable();          
        tablegeneral.Columns.Add("Cod_persona");
        tablegeneral.Columns.Add("Identificacion");
        tablegeneral.Columns.Add("Nombre");
        tablegeneral.Columns.Add("Empresa_Recaudo");
        tablegeneral.Columns.Add("Tipo_Producto");
        tablegeneral.Columns.Add("Linea_Producto");
        tablegeneral.Columns.Add("Numero_Producto");
        tablegeneral.Columns.Add("Fecha_Prox_Pago");
        tablegeneral.Columns.Add("Dias_Mora");

        DataTable table = new DataTable();
        table.Columns.Add("Cod_persona");
        table.Columns.Add("Identificacion");
        table.Columns.Add("Nombre");
        table.Columns.Add("Nom_empresa");
        table.Columns.Add("Nom_tipo_producto");
        table.Columns.Add("Nom_linea_producto");
        table.Columns.Add("Numero_Producto");
        table.Columns.Add("Fecha_Prox_Pago");
        table.Columns.Add("Dias_Mora");

        if (lstConsulta.Count > 0)
        {
            foreach (RecaudosMasivos fila in lstConsulta)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = fila.cod_persona.ToString();
                datarw[1] = fila.identificacion;
                datarw[2] = fila.nombres;
                datarw[3] = fila.nom_empresa;
                datarw[4] = fila.nom_tipo_producto;
                datarw[5] = fila.nom_linea;
                datarw[6] = fila.numero_producto;
                datarw[7] = Convert.ToDateTime(fila.fecha_proximo_pago).ToShortDateString();
                datarw[8] = fila.valor.ToString("##,##");

                table.Rows.Add(datarw);
            }
        }

        string cRutaDeImagen;       
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";

        Usuario pUsu = (Usuario)Session["usuario"];

        rvExtracto.LocalReport.DataSources.Clear();
        ReportParameter[] param = new ReportParameter[3];
        param[0] = new ReportParameter("entidad", pUsu.empresa);
        param[1] = new ReportParameter("nit", pUsu.nitempresa);
        param[2] = new ReportParameter("ImagenReport", cRutaDeImagen);

        rvExtracto.LocalReport.EnableExternalImages = true;
        rvExtracto.LocalReport.SetParameters(param);

        ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
        rvExtracto.LocalReport.DataSources.Add(rds1);
        rvExtracto.LocalReport.Refresh();
        rvExtracto.Visible = true;
      

        Site toolBar = (Site)Master;
        rvExtracto.Visible = true;
        frmPrint.Visible = false;
        mvPrincipal.ActiveViewIndex = 1;
        toolBar.MostrarCancelar(true);
        toolBar.MostrarExportar(false);
        toolBar.MostrarImprimir(false);
        toolBar.MostrarLimpiar(false);
        toolBar.MostrarConsultar(false);
                            
        }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvimpresion);
    }


    //protected void ExportToExcel(GridView GridView1)
    //{
    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.AddHeader("content-disposition", "attachment;filename=ReporteProductos.xls");
    //    Response.Charset = "";
    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.ContentEncoding = Encoding.Default;
    //    StringWriter sw = new StringWriter();
    //    ExpGrilla expGrilla = new ExpGrilla();

    //    sw = expGrilla.ObtenerGrilla(GridView1, null);

    //    Response.Write(expGrilla.style);
    //    Response.Output.Write(sw.ToString());
    //    Response.Flush();
    //    Response.End();
    //}

    protected void ExportToExcel(GridView GridView1)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && ViewState["DTLISTA"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.AllowPaging = false;
            gvLista.DataSource = ViewState["DTLISTA"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteProductos.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            gvLista.AllowPaging = true;
            gvLista.DataSource = ViewState["DTLISTA"];
            gvLista.DataBind();
            Response.End();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    { 
    }

 
    private string obtFiltro()
    {
        String filtros = String.Empty;
        RecaudosMasivos hola = new RecaudosMasivos();
        filtros = "where 1=1 ";    
        if (ddlempresarecaudo.SelectedValue != "0" )
            filtros += " and  cod_empresa = " + ddlempresarecaudo.SelectedItem.Value;
        if (ddlTipo_Producto.SelectedIndex != 0)
            filtros += " And cod_tipo_producto = " + ddlTipo_Producto.SelectedIndex;        
        if (ddlLineaProducto.SelectedIndex > 0)
            filtros += " And cod_linea_producto = '" + ddlLineaProducto.SelectedValue + "'";
        if (txtFechaReporte.ToDateTime == System.DateTime.Now && !txtFechaReporte.TieneDatos)
        {
            if (txtMora1.Text.Trim() != "")
                filtros += " And dias_mora >= " + txtMora1.Text;
            if (txtMora2.Text.Trim() != "")
                filtros += " And dias_mora <= " + txtMora2.Text;
        }
        else
        {
            if (txtMora1.Text.Trim() != "")
                filtros += " And Round(FecDifDia(fecha_proximo_pago, To_Date('" + txtFechaReporte.Text + "', '" + gFormatoFecha + "'), 2)) >= " + txtMora1.Text;
            if (txtMora2.Text.Trim() != "")
                filtros += " And Round(FecDifDia(fecha_proximo_pago, To_Date('" + txtFechaReporte.Text + "', '" + gFormatoFecha + "'), 2)) <= " + txtMora2.Text;
        }
        if (ddlOficinas.SelectedIndex != 0) 
        {
            filtros += " And oficina like '" + ddlOficinas.SelectedItem.Text + "'";
        }

        return filtros;
    }

    protected void ddlTipo_Producto_SelectedIndexChanged(object sender, EventArgs e)
    {
        lineaproductos();
    }
}