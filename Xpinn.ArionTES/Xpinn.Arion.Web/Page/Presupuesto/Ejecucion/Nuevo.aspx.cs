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
using System.Web.UI.HtmlControls;
using System.Drawing;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Presupuesto.Services.EjecucionService PresupuestoServicio = new Xpinn.Presupuesto.Services.EjecucionService();

    private List<int> mQuantities = new List<int>();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {

            if (Session[PresupuestoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(PresupuestoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(PresupuestoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

       try
        {
            if (!IsPostBack)
            {
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                CargarDDList();
                                
                if (Session[PresupuestoServicio.CodigoPrograma + ".idpresupuesto"] != null)
                {
                    idObjeto = Session[PresupuestoServicio.CodigoPrograma + ".idpresupuesto"].ToString();
                    Session.Remove(PresupuestoServicio.CodigoPrograma + ".idpresupuesto");
                    ObtenerDatos(idObjeto);
                    CargarDDLPeriodos();
                }
            }
            gvProyeccion.UseAccessibleHeader = true;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void gvProyeccion_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow gvHeaderRow = e.Row;
            GridViewRow gvHeaderRowCopy = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            this.gvProyeccion.Controls[0].Controls.AddAt(0, gvHeaderRowCopy);

            TableCell tcFirst = e.Row.Cells[0];
            tcFirst.RowSpan = 2;
            gvHeaderRowCopy.Cells.AddAt(0, tcFirst);

            TableCell tcSecond = e.Row.Cells[0];
            tcSecond.RowSpan = 2;
            gvHeaderRowCopy.Cells.AddAt(1, tcSecond);

            TableCell tcMergePeriodo = new TableCell();
            tcMergePeriodo.Text = "Período " + ddlPeriodo.SelectedItem;
            tcMergePeriodo.ColumnSpan = 4;
            gvHeaderRowCopy.Cells.AddAt(2, tcMergePeriodo); 

            TableCell tcMergeAcumulado = new TableCell();
            tcMergeAcumulado.Text = "Acumulado a " + ddlPeriodo.SelectedItem;
            tcMergeAcumulado.ColumnSpan = 4;
            gvHeaderRowCopy.Cells.AddAt(3, tcMergeAcumulado);
        }
    }

    /// <summary>
    /// Método para llenar los drop down list
    /// </summary>
    private void CargarDDList()
    {
        Xpinn.Presupuesto.Services.TipoPresupuestoService TipoPresupuestoService = new Xpinn.Presupuesto.Services.TipoPresupuestoService();
        Xpinn.Presupuesto.Entities.TipoPresupuesto TipoPresupuesto = new Xpinn.Presupuesto.Entities.TipoPresupuesto();
        ddlTipoPresupuesto.DataSource = TipoPresupuestoService.ListarTipoPresupuesto(TipoPresupuesto, (Usuario)Session["Usuario"]);
        ddlTipoPresupuesto.DataTextField = "descripcion";
        ddlTipoPresupuesto.DataValueField = "tipo_presupuesto";
        ddlTipoPresupuesto.DataBind();

        Xpinn.Contabilidad.Services.CentroCostoService cencosService = new Xpinn.Contabilidad.Services.CentroCostoService();
        ddlCentroCosto.DataSource = cencosService.ListarCentroCosto((Usuario)Session["Usuario"], "");
        ddlCentroCosto.DataTextField = "nom_centro";
        ddlCentroCosto.DataValueField = "centro_costo";
        ddlCentroCosto.DataBind();
    }

    private void CargarDDLPeriodos()
    {
        Xpinn.Presupuesto.Entities.Presupuesto ePresupuesto = new Xpinn.Presupuesto.Entities.Presupuesto();
        ePresupuesto.idpresupuesto = Convert.ToInt64(txtCodigo.Text);
        ddlPeriodo.DataSource = PresupuestoServicio.ListarPeriodosPresupuesto(ePresupuesto, (Usuario)Session["Usuario"]);
        ddlPeriodo.DataBind();
    }

 
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {

        }
        else
        {
            Session[PresupuestoServicio.CodigoPrograma + ".id"] = idObjeto;
        }
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Presupuesto.Entities.Presupuesto vPresupuesto = new Xpinn.Presupuesto.Entities.Presupuesto();
            vPresupuesto = PresupuestoServicio.ConsultarPresupuesto(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            txtCodigo.Text = HttpUtility.HtmlDecode(vPresupuesto.idpresupuesto.ToString().Trim());
            Session["IDPRESUPUESTO"] = vPresupuesto.idpresupuesto;
            ddlTipoPresupuesto.SelectedValue = vPresupuesto.tipo_presupuesto.ToString();
            ddlCentroCosto.SelectedValue = vPresupuesto.centro_costo.ToString();
            if (!string.IsNullOrEmpty(vPresupuesto.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vPresupuesto.descripcion.ToString().Trim());

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PresupuestoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        //Buscar nivel
        Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
        pData = ConsultaData.ConsultarGeneral(1100, (Usuario)Session["usuario"]);
        Int32 nivel = Convert.ToInt32(pData.valor);
        

        // Inicializar el datatable
        DataTable dtEjecucion = new DataTable();
        dtEjecucion.Clear();

        // Cargar la grilla con las cuentas contables para el presupuesto
        dtEjecucion = PresupuestoServicio.ListarEjecucionPresupuesto(Convert.ToInt64(txtCodigo.Text), Convert.ToInt64(ddlPeriodo.SelectedValue), (Usuario)Session["Usuario"], nivel);

        // Cargando datos en la grilla                   
        gvProyeccion.DataSource = dtEjecucion;
        gvProyeccion.DataBind();
        Session["DTEJECUCION"] = dtEjecucion;

    }


    protected void btnExpPresupuesto_Click(object sender, EventArgs e)
    {
        ExportarExcelGrilla(gvProyeccion, "PresupuestoEjecucion");
    }


    protected void ExportarExcelDataTable(string NombreDataTable, string NombreArchivo)
    {
        GridView gvGrillaExcel = new GridView();
        DataTable dtTabla = new DataTable();
        dtTabla = (DataTable)Session[NombreDataTable];
        gvGrillaExcel.ID = "gv" + NombreDataTable + "Excel";
        gvGrillaExcel.HeaderStyle.CssClass = "gridHeader";
        gvGrillaExcel.PagerStyle.CssClass = "gridPager";
        gvGrillaExcel.RowStyle.CssClass = "gridItem";
        gvGrillaExcel.DataSource = dtTabla;
        gvGrillaExcel.DataBind();
        ExportarExcelGrilla(gvGrillaExcel, NombreArchivo);
    }

    protected void ExportarExcelGrilla(GridView gvGrilla, string Archivo)
    {
        try
        {
            if (gvGrilla.Rows.Count > 0)
            {
                string style = "";
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                style = "<link href=\"../../Styles/Styles.css\" rel=\"stylesheet\" type=\"text/css\" />";
                gvGrilla.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvGrilla);
                pagina.RenderControl(htw);
                Response.Clear();
                style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + Archivo + ".xls");
                Response.Charset = "UTF-8";
                Response.Write(sb.ToString());
                Response.End();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex0)
        {
            VerError(ex0.Message);
        }
    }

    protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gvProyeccion.Rows.Count > 0 && Session["DTEJECUCION"] != null)
        {
            // Inicializar el datatable
            DataTable dtEjecucion = new DataTable();
            dtEjecucion = (DataTable)Session["DTEJECUCION"];
            dtEjecucion.Clear();

            // Cargando datos en la grilla                       
            gvProyeccion.DataSource = dtEjecucion;
            gvProyeccion.DataBind();

        }
    }

}

