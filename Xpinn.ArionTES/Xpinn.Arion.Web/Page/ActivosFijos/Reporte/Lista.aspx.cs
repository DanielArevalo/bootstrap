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
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

partial class Lista : GlobalWeb
{
    private Xpinn.ActivosFijos.Services.ActivosFijoservices ActivosFijoservicio = new Xpinn.ActivosFijos.Services.ActivosFijoservices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ActivosFijoservicio.CodigoProgramaReporteActivos, "L");

            Site toolBar = (Site)this.Master;
            txtfecha.ToDateTime = DateTime.Now;
            //toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarImprimir(false);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaReporteActivos, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            if (!IsPostBack)
            {
              
                btnExportar.Visible = false;
                CargarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaReporteActivos);
                mvPrincipal.ActiveViewIndex = 0;
                CargarListar();
         
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaReporteActivos, "Page_Load", ex);
        }
    }

    /*protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session[ActivosFijoservicio.CodigoProgramaReporteActivos + ".id"] = null;
        GuardarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaReporteActivos);
        Navegar(Pagina.Nuevo);
    }*/

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaReporteActivos);
        Actualizar();
        DDLCC.SelectedIndex = 0;
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
        btnExportar.Visible = false;
        LimpiarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaReporteActivos);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaReporteActivos + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ActivosFijoservicio.CodigoProgramaReporteActivos + ".id"] = id;
        DDLCC.SelectedIndex = 0;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[ActivosFijoservicio.CodigoProgramaReporteActivos + ".id"] = id;
        DDLCC.SelectedIndex = 0;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            ActivosFijoservicio.EliminarActivoFijo(id, (Usuario)Session["usuario"]);
            Actualizar();
            DDLCC.SelectedIndex = 0;
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaReporteActivos, "gvLista_RowDeleting", ex);
            VerError(ex.Message);
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
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaReporteActivos, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            List<Xpinn.ActivosFijos.Entities.ActivoFijo> lstConsulta = new List<Xpinn.ActivosFijos.Entities.ActivoFijo>();
            lstConsulta = ActivosFijoservicio.ListarActivoFijo(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                Site toolBar = (Site)this.Master;
                btnExportar.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTActivosFijos"] = lstConsulta;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
                toolBar.MostrarImprimir(true);
            }
            else
            {
                btnExportar.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ActivosFijoservicio.CodigoProgramaReporteActivos + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaReporteActivos, "Actualizar", ex);
        }
    }

    private Xpinn.ActivosFijos.Entities.ActivoFijo ObtenerValores()
    {
        Xpinn.ActivosFijos.Entities.ActivoFijo vActivoFijo = new Xpinn.ActivosFijos.Entities.ActivoFijo();
        if (DDLCC.SelectedIndex!= 0)
            vActivoFijo.cod_costo = Convert.ToInt16(DDLCC.SelectedValue);
        return vActivoFijo;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);  
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ActivosFijos.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(GridView1, null);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    private void CargarListar()
    {
        Xpinn.Contabilidad.Services.CentroCostoService CentroCostoService = new Xpinn.Contabilidad.Services.CentroCostoService();
        List<Xpinn.Contabilidad.Entities.CentroCosto> LstCentroCosto = new List<Xpinn.Contabilidad.Entities.CentroCosto>();
        string sFiltro = "";
        LstCentroCosto = CentroCostoService.ListarCentroCosto((Usuario)Session["Usuario"], sFiltro);
        DDLCC.DataSource = LstCentroCosto;
        DDLCC.DataTextField = "nom_centro";
        DDLCC.DataValueField = "centro_costo";
        DDLCC.DataBind();
        DDLCC.Items.Insert(0, new ListItem("CONSOLIDADO", "0"));
    }


    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        List<Xpinn.ActivosFijos.Entities.ActivoFijo> lstConsulta = new List<Xpinn.ActivosFijos.Entities.ActivoFijo>();

        Xpinn.ActivosFijos.Entities.ActivoFijo entidad = new Xpinn.ActivosFijos.Entities.ActivoFijo();
        DateTime pFecha;
       
        lstConsulta = ActivosFijoservicio.ListarActivoFijo(ObtenerValores(), (Usuario)Session["usuario"]);
       

        //tabla general 
        DataTable tablegeneral = new DataTable();
        tablegeneral.Columns.Add("consecutivo");
        tablegeneral.Columns.Add("cod_act");
        tablegeneral.Columns.Add("nomclase");
        tablegeneral.Columns.Add("nomtipo");
        tablegeneral.Columns.Add("nomubica");
        tablegeneral.Columns.Add("nomcosto");
        tablegeneral.Columns.Add("nombre");
        tablegeneral.Columns.Add("anos_util");
        tablegeneral.Columns.Add("nomestado");
        tablegeneral.Columns.Add("serial");
        tablegeneral.Columns.Add("cod_encargado");
        tablegeneral.Columns.Add("fecha_compra");
        tablegeneral.Columns.Add("valor_compra");
        tablegeneral.Columns.Add("valor_avaluo");
        tablegeneral.Columns.Add("valor_salvamen");
        tablegeneral.Columns.Add("num_factura");
        tablegeneral.Columns.Add("cod_proveedor");
        tablegeneral.Columns.Add("observaciones");
        tablegeneral.Columns.Add("nomoficina");
        tablegeneral.Columns.Add("fecha_ult_depre");
        tablegeneral.Columns.Add("acumulado_depreciacion");
        tablegeneral.Columns.Add("saldo_por_depreciar");
        tablegeneral.Columns.Add("fechacreacion");
        tablegeneral.Columns.Add("usuariocreacion");
        tablegeneral.Columns.Add("nomTipoNif");
        tablegeneral.Columns.Add("nomMetodo");

        DataTable table = new DataTable();
        table.Columns.Add("consecutivo");
        table.Columns.Add("cod_act");
        table.Columns.Add("nomclase");
        table.Columns.Add("nomtipo");
        table.Columns.Add("nomubica");
        table.Columns.Add("nomcosto");
        table.Columns.Add("nombre");
        table.Columns.Add("anos_util");
        table.Columns.Add("nomestado");
        table.Columns.Add("serial");
        table.Columns.Add("cod_encargado");
        table.Columns.Add("fecha_compra");
        table.Columns.Add("valor_compra");
        table.Columns.Add("valor_avaluo");
        table.Columns.Add("valor_salvamen");
        table.Columns.Add("num_factura");
        table.Columns.Add("cod_proveedor");
        table.Columns.Add("observaciones");
        table.Columns.Add("nomoficina");
        table.Columns.Add("fecha_ult_depre");
        table.Columns.Add("acumulado_depreciacion");
        table.Columns.Add("saldo_por_depreciar");
        table.Columns.Add("fechacreacion");
        table.Columns.Add("usuariocreacion");
        table.Columns.Add("nomTipoNif");
        table.Columns.Add("nomMetodo");

        if (lstConsulta.Count > 0)
        {
            string pFecCompra = string.Empty;
            foreach (Xpinn.ActivosFijos.Entities.ActivoFijo fila in lstConsulta)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = fila.consecutivo;
                datarw[1] = fila.cod_act;
                datarw[2] = fila.nomclase;
                datarw[3] = fila.nomtipo;
                datarw[4] = fila.nomubica;
                datarw[5] = fila.nomcosto;
                datarw[6] = fila.nombre;
                datarw[7] = fila.anos_util.ToString();
                datarw[8] = fila.nomestado;
                if (fila.serial != null)
                    datarw[9] = fila.serial.ToString();
                else
                    fila.serial = "";
                datarw[10] = fila.cod_encargado;
                pFecCompra = fila.fecha_compra != null ? Convert.ToDateTime(fila.fecha_compra).ToShortDateString() : " ";
                datarw[11] = pFecCompra;
                datarw[12] = fila.valor_compra;
                datarw[13] = fila.valor_avaluo;
                datarw[14] = fila.valor_salvamen;
                datarw[15] = fila.num_factura;
                datarw[16] = fila.cod_proveedor;
                datarw[17] = fila.observaciones;
                datarw[18] = fila.nomoficina;
                datarw[19] = fila.fecha_ult_depre.ToShortDateString();
                datarw[20] = fila.acumulado_depreciacion;
                datarw[21] = fila.saldo_por_depreciar;
                datarw[22] = fila.fechacreacion.ToShortDateString();
                datarw[23] = fila.usuariocreacion;
                datarw[24] = fila.nomTipoNif;
                if (fila.nomMetodo != null)
                    datarw[25] = fila.nomMetodo;
                else
                    fila.nomMetodo = "";
              



                table.Rows.Add(datarw);
            }
        }

        Usuario pUsu = (Usuario)Session["usuario"];
        string Fecha = txtfecha.Text.Trim() != "" ? Convert.ToDateTime(txtfecha.Text).ToString("yyyy/MM/dd").Replace("/", " ") : "";
        
        rvExtracto.LocalReport.DataSources.Clear();
        ReportParameter[] param = new ReportParameter[4];
        param[0] = new ReportParameter("entidad", pUsu.empresa);
        param[1] = new ReportParameter("nit", pUsu.nitempresa);
        param[2] = new ReportParameter("fecha_corte", Fecha);
        param[3] = new ReportParameter("ImagenReport", ImagenReporte());


        rvExtracto.LocalReport.EnableExternalImages = true;
        rvExtracto.LocalReport.SetParameters(param);

        ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
        rvExtracto.LocalReport.DataSources.Add(rds1);
        rvExtracto.LocalReport.Refresh();
        rvExtracto.Visible = true;


        Site toolBar = (Site)Master;
        rvExtracto.Visible = true;
        mvPrincipal.ActiveViewIndex = 1;
        toolBar.MostrarCancelar(false);
        toolBar.MostrarExportar(false);
        toolBar.MostrarImprimir(false);
        toolBar.MostrarLimpiar(false);
        toolBar.MostrarConsultar(false);

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

}