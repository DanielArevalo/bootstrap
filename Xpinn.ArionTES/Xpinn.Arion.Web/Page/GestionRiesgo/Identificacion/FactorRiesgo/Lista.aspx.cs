using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.Data;

public partial class Lista : GlobalWeb
{
    IdentificacionServices identificacionServicio = new IdentificacionServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(identificacionServicio.CodigoProgramaF, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaF, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaF, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("GR_RIESGO_GENERAL", "COD_RIESGO, SIGLA", "", "1", ddlSistemaRiesgo, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("GR_SUBPROCESO_ENTIDAD", "COD_SUBPROCESO, DESCRIPCION", "", "1", ddlProcedimiento, (Usuario)Session["usuario"]);

        ddlFactorRiesgo.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlFactorRiesgo.Items.Insert(1, new ListItem("Humano", "H"));
        ddlFactorRiesgo.Items.Insert(2, new ListItem("Tecnólogico", "T"));
        ddlFactorRiesgo.Items.Insert(3, new ListItem("Mixto", "M"));
        ddlFactorRiesgo.DataBind();
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarPanel(pConsulta);
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvFactorRiesgo.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteFactores.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            ExpGrilla expGrilla = new ExpGrilla();
            sw = expGrilla.ObtenerGrilla(gvFactorRiesgo, null);
            Response.Write("<div>" + expGrilla.style + "</div>");
            Response.Output.Write("<div>" + sw.ToString() + "</div>");
            Response.Flush();
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarExportar(true);
        toolBar.MostrarNuevo(true);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarRegresar(false);

        mvFactores.ActiveViewIndex = 0;
    }

    private void Actualizar()
    {
        List<Identificacion> lstFactores = new List<Identificacion>();

        lstFactores = identificacionServicio.ListarFactoresRiesgo(ObtenerFiltro(), "", (Usuario)Session["usuario"]);
        if (lstFactores.Count > 0)
        {
            panelGrilla.Visible = true;
            gvFactorRiesgo.DataSource = lstFactores;
            gvFactorRiesgo.DataBind();
            lblTotalRegs.Text = "Registros encontrados: " + lstFactores.Count;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarExportar(true);
        }
        else
        {
            panelGrilla.Visible = false;
            lblTotalRegs.Text = "La consulta no obtuvo resultado";
        }

    }

    private Identificacion ObtenerFiltro()
    {
        Identificacion pCausa = new Identificacion();
        if (txtCodigoFactorRiesgo.Text != "")
            pCausa.cod_factor = Convert.ToInt64(txtCodigoFactorRiesgo.Text);
        if (txtDescripcionFactor.Text != "")
            pCausa.descripcion = Convert.ToString(txtDescripcionFactor.Text);
        if (ddlFactorRiesgo.SelectedValue != "")
            pCausa.factor_riesgo = ddlFactorRiesgo.SelectedValue;
        if (ddlSistemaRiesgo.SelectedValue != "")
            pCausa.cod_riesgo = Convert.ToInt64(ddlSistemaRiesgo.SelectedValue);

        return pCausa;
    }

    protected void gvFactorRiesgo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Identificacion pFactor = new Identificacion();
            MatrizServices matrizServicio = new MatrizServices();
            List<Matriz> lstMatriz = new List<Matriz>();
            Label lbRiesgo = (Label)gvFactorRiesgo.Rows[e.RowIndex].FindControl("lbSRiesgo");
            Int64 cod_riesgo = 0;

            pFactor.cod_factor = Convert.ToInt64(gvFactorRiesgo.DataKeys[e.RowIndex].Values[0]);
            string filtro = " WHERE COD_FACTOR = " + pFactor.cod_factor + " AND CALIFICACION_RIESGO > 0";
            cod_riesgo = Convert.ToInt64(lbRiesgo.Text);

            lstMatriz = matrizServicio.ListarMatriz(cod_riesgo, filtro, (Usuario)Session["usuario"]);

            if (lstMatriz.Count == 0)
            {
                identificacionServicio.EliminarFactorRiesgo(pFactor, (Usuario)Session["usuario"]);
                Actualizar();
            }
            else
            {
                VerError("No se puede eliminar el factor, se encuentra registrado en la matriz de riesgo inherente");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaF, "gvFactorRiesgo_RowDeleting", ex);
        }
    }

    protected void gvFactorRiesgo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvFactorRiesgo.DataKeys[e.NewEditIndex].Value.ToString();
        Session[identificacionServicio.CodigoProgramaF + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvFactorRiesgo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvFactorRiesgo.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(identificacionServicio.CodigoProgramaF, "gvFactorRiesgo_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Evento para realizar impresión del reporte de evaluación del factor de riesgo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 

    protected void gvFactorRiesgo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Imprimir")
        {
            Label lbRiesgo = (Label)gvFactorRiesgo.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lbSRiesgo");
            Int64 cod_riesgo = 0;
            Int64 cod_factor = 0;

            cod_factor = Convert.ToInt64(gvFactorRiesgo.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0]);
            cod_riesgo = Convert.ToInt64(lbRiesgo.Text);



            //obtener codigo de causa
            MatrizServices matrizServicio = new MatrizServices();
            List<Matriz> lsmatrizEvaluacionXcausa = new List<Matriz>();
            Identificacion causas = new Identificacion();
            List<Identificacion> lstConsulta = new List<Identificacion>();
            List<Identificacion> lstcausas = new List<Identificacion>();

            lsmatrizEvaluacionXcausa = matrizServicio.listarEvaluacionRiesgoXcausa(cod_factor, cod_riesgo, (Usuario)Session["usuario"]);
            // si el Factor de riesgo tiene mas de una causa

            if (lsmatrizEvaluacionXcausa.Count > 1)
            {


                foreach (Matriz item in lsmatrizEvaluacionXcausa)
                {

                    causas.cod_causa = item.cod_causa;
                    lstConsulta = identificacionServicio.ListarCausas(causas, "", (Usuario)Session["usuario"]);
                    lstcausas.Add(lstConsulta[0]);
                }
                // LLenar data table con los datos a recoger
                System.Data.DataTable table = new System.Data.DataTable();
                table.Columns.Add("cod_causa");
                table.Columns.Add("descripcion");
                table.Columns.Add("nom_area");
                table.Columns.Add("nom_cargo");

                foreach (Identificacion item in lstcausas)
                {
                    DataRow datarw;
                    datarw = table.NewRow();
                    datarw[0] = item.cod_causa;
                    datarw[1] = item.descripcion;
                    datarw[2] = item.nom_area;
                    datarw[3] = item.nom_cargo;
                    table.Rows.Add(datarw);
                }
                // LLenar data table con los datos de control
                System.Data.DataTable tablecontrol = new System.Data.DataTable();
                tablecontrol.Columns.Add("cod_control");
                tablecontrol.Columns.Add("descripcion");

                foreach (Matriz item in lsmatrizEvaluacionXcausa)
                {
                    DataRow datarw;
                    datarw = tablecontrol.NewRow();
                    datarw[0] = item.cod_control;
                    datarw[1] = item.desc_control;

                    tablecontrol.Rows.Add(datarw);
                }
                ReportDataSource rds = new ReportDataSource("DataSetCausas", table);
                ReportDataSource rdsCon = new ReportDataSource("DTcontrol", tablecontrol);
                ReportViewerFactor.LocalReport.DataSources.Clear();
                ReportViewerFactor.LocalReport.DataSources.Add(rdsCon);
                ReportViewerFactor.LocalReport.DataSources.Add(rds);
                ReportViewerFactor.LocalReport.Refresh();

            }
            else
            // si  el factor de Riesgo tiene   una causa asociada
            {
                foreach (Matriz item in lsmatrizEvaluacionXcausa)
                {

                    causas.cod_causa = item.cod_causa;
                    lstConsulta = identificacionServicio.ListarCausas(causas, "", (Usuario)Session["usuario"]);
                    lstcausas.Add(lstConsulta[0]);
                }

                // LLenar data table con los datos a recoger
                System.Data.DataTable table = new System.Data.DataTable();
                table.Columns.Add("cod_causa");
                table.Columns.Add("descripcion");
                table.Columns.Add("nom_area");
                table.Columns.Add("nom_cargo");

                foreach (Identificacion item in lstcausas)
                {
                    DataRow datarw;
                    datarw = table.NewRow();
                    datarw[0] = item.cod_causa;
                    datarw[1] = item.descripcion;
                    datarw[2] = item.nom_area;
                    datarw[3] = item.nom_cargo;
                    table.Rows.Add(datarw);
                }
                // LLenar data table con los datos de control
                System.Data.DataTable tablecontrol = new System.Data.DataTable();
                tablecontrol.Columns.Add("cod_control");
                tablecontrol.Columns.Add("descripcion");

                foreach (Matriz item in lsmatrizEvaluacionXcausa)
                {
                    DataRow datarw;
                    datarw = tablecontrol.NewRow();
                    datarw[0] = item.cod_control;
                    datarw[1] = item.desc_control;

                    tablecontrol.Rows.Add(datarw);
                }
                // asignar dataset cargado a el reporte Controles
                ReportDataSource rds = new ReportDataSource("DataSetCausas", table);
                ReportDataSource rdsCon = new ReportDataSource("DTcontrol", tablecontrol);
                ReportViewerFactor.LocalReport.DataSources.Clear();
                ReportViewerFactor.LocalReport.DataSources.Add(rdsCon);
                ReportViewerFactor.LocalReport.DataSources.Add(rds);
                ReportViewerFactor.LocalReport.Refresh();
            }


            //Agregar parametros al reporte
            ReportParameter[] param = LlenarParametrosReporte(cod_factor, cod_riesgo);
            if (param != null)
            {

                ReportViewerFactor.LocalReport.ReportPath = "Page\\GestionRiesgo\\HojaVidaRiesgo\\ReporteRiesgo.rdlc";
                ReportViewerFactor.LocalReport.EnableExternalImages = true;
                ReportViewerFactor.LocalReport.SetParameters(param);
                ReportViewerFactor.LocalReport.Refresh();
                ReportViewerFactor.Visible = true;

             
                Site toolBar = (Site)this.Master;
                toolBar.MostrarExportar(false);
                toolBar.MostrarNuevo(false);
                toolBar.MostrarConsultar(false);
                toolBar.MostrarLimpiar(false);
                toolBar.MostrarRegresar(true);

                mvFactores.ActiveViewIndex = 1;
            }
        }
    }

    private ReportParameter[] LlenarParametrosReporte(Int64 cod_factor, Int64 cod_riesgo)
    {
        MatrizServices matrizServicio = new MatrizServices();
        Matriz matrizEvaluacion = new Matriz();

        matrizEvaluacion = matrizServicio.ConsultarEvaluacionRiesgo(cod_factor, cod_riesgo, (Usuario)Session["usuario"]);

        if (matrizEvaluacion.valor_rinherente != 0 && matrizEvaluacion.valor_rresidual != 0)
        {
            ReportParameter[] param = new ReportParameter[23];

            param[0] = new ReportParameter("abreviatura", matrizEvaluacion.sigla);
            param[1] = new ReportParameter("nom_sistema", matrizEvaluacion.desc_sistema);
            param[2] = new ReportParameter("cod_factor", matrizEvaluacion.abreviatura + "-" + matrizEvaluacion.cod_factor);
            param[3] = new ReportParameter("desc_factor", matrizEvaluacion.descripcion);
            param[4] = new ReportParameter("nivel_probabilidad", matrizEvaluacion.nivel);
            param[5] = new ReportParameter("valor_rinherente", matrizServicio.ColoresRInherente[matrizEvaluacion.valor_rinherente].descripcion);
            param[6] = new ReportParameter("valor_rresidual", matrizServicio.ColoresRResidual[matrizEvaluacion.valor_rresidual].descripcion);
            param[7] = new ReportParameter("clase_control", matrizEvaluacion.desc_clase);
            param[8] = new ReportParameter("forma_control", matrizEvaluacion.desc_forma);
            param[9] = new ReportParameter("estado_alerta", matrizEvaluacion.desc_alerta);
            param[10] = new ReportParameter("desc_probabilidad", matrizEvaluacion.desc_probabilidad);
            param[11] = new ReportParameter("frecuencia", matrizEvaluacion.frecuencia);
            param[12] = new ReportParameter("imp_reputacional", matrizEvaluacion.impacto_reputacional);
            param[13] = new ReportParameter("imp_legal", matrizEvaluacion.impacto_legal);
            param[14] = new ReportParameter("imp_operativo", matrizEvaluacion.impacto_operativo);
            param[15] = new ReportParameter("imp_contagio", matrizEvaluacion.impacto_contagio);
            param[16] = new ReportParameter("desc_control", matrizEvaluacion.desc_control);
            param[17] = new ReportParameter("responsable_control", matrizEvaluacion.desc_cargo);
            param[18] = new ReportParameter("area_control", matrizEvaluacion.desc_area);
            param[19] = new ReportParameter("ImagenReport", ImagenReporte());
            param[20] = new ReportParameter("color_rinherente", matrizServicio.ColoresRInherente[matrizEvaluacion.valor_rinherente].nivel);
            param[21] = new ReportParameter("color_rresidual", matrizServicio.ColoresRResidual[matrizEvaluacion.valor_rresidual].nivel);
            param[22] = new ReportParameter("nivel_impacto", matrizEvaluacion.nivel_impacto);

            return param;
        }
        else
        {
            VerError("El factor de riesgo no ha sido registrado en las matrices para su evaluación");
            return null;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

}