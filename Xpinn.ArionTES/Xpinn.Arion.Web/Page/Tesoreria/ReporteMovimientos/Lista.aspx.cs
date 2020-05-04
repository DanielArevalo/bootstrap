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
using Cantidad_a_Letra;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;


 
partial class Lista : GlobalWeb
{
    ReporteMovimientosServices ReportMoviService = new ReporteMovimientosServices();
    PoblarListas poblar = new PoblarListas();
  
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {       
            VisualizarOpciones(ReportMoviService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarImprimir(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReportMoviService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                CargarDllFormaPago();
                txtFechaIni.Text = DateTime.Now.ToShortDateString();
                txtFechaFin.Text = DateTime.Now.ToShortDateString();
                panelGrilla.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReportMoviService.CodigoPrograma, "Page_Load", ex);
        }
    }

    private bool validarIngresoDefechas()
    {
        if (txtFechaIni.Text != "" && txtFechaFin.Text != "")
        {
            if (Convert.ToDateTime(txtFechaIni.Text) > Convert.ToDateTime(txtFechaFin.Text))
            {
                VerError("Datos erroneos en las Fechas de Vencimiento.");
                return false;
            }
        }
        return true;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (validarIngresoDefechas())
        {
            Page.Validate();
            gvLista.Visible = true;
            if (Page.IsValid)
            {
                Actualizar();
            }
        }
    }

    
    private void cargarDropdown()
    {
       
        List<Xpinn.Asesores.Entities.Ejecutivo> lstUsuarios = new List<Xpinn.Asesores.Entities.Ejecutivo>();
        Xpinn.Asesores.Data.EjecutivoData UsuarioData = new Xpinn.Asesores.Data.EjecutivoData();
        lstUsuarios = UsuarioData.ListartodosUsuarios((Usuario)Session["usuario"]);
        if (lstUsuarios.Count > 0)
        {
            ddlCajero.DataSource = lstUsuarios;
            ddlCajero.DataTextField = "NombreCompleto";
            ddlCajero.DataValueField = "IdEjecutivo";
            ddlCajero.AppendDataBoundItems = true;
            ddlCajero.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlCajero.SelectedIndex = 0;
            ddlCajero.DataBind();
        }

        PoblarLista("Tipomoneda", ddlMoneda);
        ddlofi.Inicializar();

    }

    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();

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
            BOexcepcion.Throw(ReportMoviService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    
    private void Actualizar()
    {
        try
        {
            List<ReporteMovimientos> lstConsulta = new List<ReporteMovimientos>();
            string filtro = obtFiltro();
            DateTime pFechaIni, pFechaFin;
            pFechaIni = txtFechaIni.ToDateTime == null ? DateTime.MinValue : txtFechaIni.ToDateTime;
            pFechaFin = txtFechaFin.ToDateTime == null ? DateTime.MinValue : txtFechaFin.ToDateTime;

            lstConsulta = ReportMoviService.ListarReporteMovimientos(filtro, pFechaIni, pFechaFin, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;

            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                Session["DTREPORTE"] = lstConsulta;
                toolBar.MostrarExportar(true);
                toolBar.MostrarImprimir(true);
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
                Session["DTREPORTE"] = null;
                toolBar.MostrarExportar(false);
                toolBar.MostrarImprimir(false);
            }
            CalcularTotal();
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString(); 

            Session.Add(ReportMoviService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReportMoviService.CodigoPrograma, "Actualizar", ex);
        }
    }

    void CalcularTotal()
    {
        decimal acum = 0;
        foreach (GridViewRow fila in gvLista.Rows)
        {
            acum += decimal.Parse(fila.Cells[10].Text);            
        }
        txtTotalMovs.Text = acum.ToString("n0");
    }
    
   
    private string obtFiltro()
    {        
        String filtro = String.Empty;

        if (ddlofi.SelectedIndex != null)
            if (ddlofi.SelectedIndex != 0)
                filtro += " and D.Nombre like '" + ddlofi.SelectedItem + "'";
        if (ddlCajero.SelectedIndex != 0)
            filtro += " and op.cod_usu = " + ddlCajero.SelectedValue;
        if (ddlMoneda.SelectedIndex != 0)
            filtro += " and a.cod_moneda = " + ddlMoneda.SelectedValue;
        if (this.ddlTipoPago.SelectedIndex != 0)
            filtro += " and tp.Cod_Tipo_Pago = " + ddlTipoPago.SelectedValue;

        return filtro;
    }



    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (panelGrilla.Visible == false)
        {
            VerError("Genere la Consulta");
            return;
        }
        if (gvLista.Rows.Count == 0)
        {
            VerError("No existen Datos");
            return;
        }

        //RECUPERAR DATOS       
        List<ReporteMovimientos> lstDetalle = new List<ReporteMovimientos>();
        lstDetalle = (List<ReporteMovimientos>)Session["DTREPORTE"];

        //CREACION DE LA TABLA ENCABEZADO
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("oficina");
        table.Columns.Add("caja");
        table.Columns.Add("moneda");
        table.Columns.Add("cod_ope");
        table.Columns.Add("num_comp");
        table.Columns.Add("tipo_comp");
        table.Columns.Add("fecha");
        table.Columns.Add("tipo_operacion");
        table.Columns.Add("identificacion");
        table.Columns.Add("nombre_cliente");
        table.Columns.Add("valor");
        table.Columns.Add("tipo_pago");

        //LLENAR LAS TABLAS CON LOS DATOS CORRESPONDIENTES                
        if (lstDetalle.Count > 0)
        {
            foreach (ReporteMovimientos rFila in lstDetalle)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = " " + rFila.nom_oficina;
                datarw[1] = " " + rFila.nom_caja;
                datarw[2] = " " + rFila.nom_moneda;
                datarw[3] = " " + rFila.cod_ope;
                datarw[4] = " " + rFila.num_comp;
                datarw[5] = " " + rFila.nomtipo_comp;
                datarw[6] = " " + rFila.fecha.ToShortDateString();
                datarw[7] = " " + rFila.nomTipo_Ope;
                datarw[8] = " " + rFila.identificacion;
                datarw[9] = " " + rFila.nombre;
                datarw[10] = " " + rFila.valor.ToString("n");
                datarw[11] = " " + rFila.nomTipo_Pago;
                table.Rows.Add(datarw);
            }
        }

        //PASAR LOS DATOS AL REPORTE
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        ReportParameter[] param = new ReportParameter[4];
        param[0] = new ReportParameter("oficina", pUsuario.empresa);
        param[1] = new ReportParameter("nit", pUsuario.nitempresa);
        param[2] = new ReportParameter("total", txtTotalMovs.Text);
        param[3] = new ReportParameter("ImagenReport", ImagenReporte());
        
        rvReporte.LocalReport.EnableExternalImages = true;
        rvReporte.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", table);
        rvReporte.LocalReport.DataSources.Clear();
        rvReporte.LocalReport.DataSources.Add(rds);
        rvReporte.LocalReport.Refresh();


        // MOSTRAR REPORTE EN PANTALLA
        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;
        byte[] bytes = rvReporte.LocalReport.Render("PDF", null, out mimeType,
                       out encoding, out extension, out streamids, out warnings);
        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output.pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        Session["Archivo" + Usuario.codusuario] = Server.MapPath("output.pdf");
        frmPrint.Visible = true;
        rvReporte.Visible = false;

        mvPrincipal.ActiveViewIndex = 1;
    }


    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["DTREPORTE"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();           
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTREPORTE"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteMovimientos.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }


    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 0;       
    }

    // carga ddl
    void CargarDllFormaPago()
    {
        poblar.PoblarListaDesplegable("Tipo_Pago", ddlTipoPago, (Usuario)Session["usuario"]);
        Xpinn.Tesoreria.Data.ReporteMovimientoData listatipopago= new Xpinn.Tesoreria.Data.ReporteMovimientoData();
        Xpinn.Tesoreria.Entities.ReporteMovimientos formapago = new Xpinn.Tesoreria.Entities.ReporteMovimientos();

        var lista = listatipopago.ListarFormaPago(formapago, (Usuario)Session["usuario"]);

        if (lista != null)
        {
            lista.Insert(0, new Xpinn.Tesoreria.Entities.ReporteMovimientos { forma_pago = "Seleccione un Item", cod_tipo_pago = 0 });
           
            ddlTipoPago.DataSource = lista;
            ddlTipoPago.DataTextField = "forma_pago";
            ddlTipoPago.DataValueField = "cod_tipo_pago";
            ddlTipoPago.DataBind();
        }


    }

    protected void ddlTipoPago_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}