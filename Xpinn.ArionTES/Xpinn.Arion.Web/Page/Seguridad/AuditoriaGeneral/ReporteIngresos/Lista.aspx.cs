using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Seguridad.Services;
using Xpinn.Seguridad.Entities;

using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

public partial class Lista : GlobalWeb
{
    UsuarioIngresoService reporteServicio = new UsuarioIngresoService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(reporteServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarImprimir(false);
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, reporteServicio.GetType().Name);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }


    private void Actualizar()
    {
        try
        {
            Configuracion conf = new Configuracion();
            List<Ingresos> lstConsulta = new List<Ingresos>();

            string filtro = obtFiltro();

            DateTime pFechaIni, pFechaFin;
            pFechaIni = txtFechaIni.ToDateTime == null ? DateTime.MinValue : txtFechaIni.ToDateTime;
            pFechaFin = txtFechaFin.ToDateTime == null ? DateTime.MinValue : txtFechaFin.ToDateTime;

            lstConsulta = reporteServicio.ListarIngresos(filtro,pFechaIni,pFechaFin, (Usuario)Session["usuario"]);

            gvLista.DataSource = lstConsulta;
            
            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                PanelListado.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                Session["DTINGRESOS"] = lstConsulta;
                toolBar.MostrarImprimir(true);
                toolBar.MostrarExportar(true);
            }
            else
            {
                PanelListado.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
                Session["DTINGRESOS"] = null;
                toolBar.MostrarImprimir(false);
                toolBar.MostrarExportar(false);
            }

            Session.Add(reporteServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

      

    private string obtFiltro()
    {

        String filtro = String.Empty;
        if (txtCodigo.Text.Trim() != "")
            filtro += " and I.Cod_Ingreso = " + txtCodigo.Text.Trim();
        if(txtUsuario.Text != "")
            filtro += " and U.Nombre Like '%"+txtUsuario.Text+"%'";
        //filtro += " and estado ='G'";

        return filtro;
    }



    Boolean ValidarDatos()
    {
        VerError("");
       
        if (txtFechaIni.Text != "" && txtFechaFin.Text != "")
        {
            if (Convert.ToDateTime(txtFechaIni.Text) > Convert.ToDateTime(txtFechaFin.Text))
            {
                VerError("No puede Ingresar una Fecha inicial mayor a la fecha final");
                return false;
            }
        }

        return true;
    }



    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                GuardarValoresConsulta(pConsulta, reporteServicio.GetType().Name);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "btnConsultar_Click", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtFechaIni.Text = "";
        txtFechaFin.Text = "";
        LimpiarValoresConsulta(pConsulta, reporteServicio.GetType().Name);
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
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }



    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (Session["DTINGRESOS"] != null && gvLista.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTINGRESOS"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=IngresosDeUsuario.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen Datos");
        }
    }


    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (Session["DTINGRESOS"] != null && gvLista.Rows.Count > 0)
        {
            //CREACION DE LA TABLA
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("Cod_ingreso");
            table.Columns.Add("nombre");
            table.Columns.Add("FechaIngreso");
            table.Columns.Add("FechaSalida");
            table.Columns.Add("DireccionIP");

            //CARGANDO DATOS
            List<Ingresos> lstIngresos = new List<Ingresos>();
            lstIngresos = (List<Ingresos>)Session["DTINGRESOS"];
            
            //LLENANDO LA TABLA
            foreach (Ingresos Ingre in lstIngresos)
            {
                System.Data.DataRow dr;
                dr = table.NewRow();
                dr[0] = Ingre.cod_ingreso;
                dr[1] = Ingre.nombre;
                dr[2] = Ingre.fecha_horaingreso; //.ToString("dd/mm/yyyy ")
                dr[3] = Ingre.fecha_horasalida;
                dr[4] = Ingre.direccionip;
                table.Rows.Add(dr);
            }

            rvIngresosUsuario.LocalReport.DataSources.Clear();

            //PASAR LOS DATOS AL REPORTE
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            ReportParameter[] param = new ReportParameter[2];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            rvIngresosUsuario.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvIngresosUsuario.LocalReport.DataSources.Add(rds);
            rvIngresosUsuario.LocalReport.Refresh();

            mvPrincipal.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarConsultar(false);
            toolBar.MostrarImprimir(false);
            toolBar.MostrarExportar(false);
            toolBar.MostrarLimpiar(false);
        }
        else
        {
            VerError("No existen Datos");
        }
    }


    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 0;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarConsultar(true);
        toolBar.MostrarImprimir(true);
        toolBar.MostrarExportar(true);
        toolBar.MostrarLimpiar(true);
    }

}