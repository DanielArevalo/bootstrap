using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Entities;
using System.Text;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class Page_Cartera_ReportHistoricoClasifi_Lista : GlobalWeb
{
    private Xpinn.Cartera.Services.CortoyLargoPlazoService edadesServicio = new Xpinn.Cartera.Services.CortoyLargoPlazoService();
    ClasificacionService clasificacionservice = new ClasificacionService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            //VisualizarOpciones(reporteServicio.CodigoProgReportesCierre, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(reporteServicio.CodigoProgReportesCierre, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];

            if (!Page.IsPostBack)
            {

              
                CargarDropDown();


            }
        }
        catch (Exception ex)
        {

            //BOexcepcion.Throw(ComprobanteServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }
    protected void CargarDropDown()
    {
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["Usuario"];

        Xpinn.FabricaCreditos.Services.OficinaService oficinaServicio = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        ddloficina.DataTextField = "nombre";
        ddloficina.DataValueField = "codigo";
        ddloficina.DataSource = oficinaServicio.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        ddloficina.DataBind();

        List<Xpinn.Comun.Entities.Cierea> lstFechaCierre = new List<Xpinn.Comun.Entities.Cierea>();
        lstFechaCierre = edadesServicio.ListarFechaCierre((Usuario)Session["Usuario"]);
        ddlFechaInicio.DataSource = lstFechaCierre;
        ddlFechaInicio.DataTextFormatString = "{0:dd/MM/yyyy}";
        ddlFechaInicio.DataTextField = "fecha";
        ddlFechaInicio.DataBind();

        ddlFechaFinal.DataSource = lstFechaCierre;
        ddlFechaFinal.DataTextFormatString = "{0:dd/MM/yyyy}";
        ddlFechaFinal.DataTextField = "fecha";
        ddlFechaFinal.DataBind();
    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
      
        gvLista.DataSource = null;
        gvLista.DataBind();
       
      
    }
    private void Actualizar()
    {
        try
        {
            String emptyQuery = "Fila de datos vacia";
            ClasificacionCartera datosApp = new ClasificacionCartera();
            Configuracion conf = new Configuracion();
            String format = conf.ObtenerFormatoFecha();
            List<ClasificacionCartera> lstConsulta = new List<ClasificacionCartera>();
            List<ClasificacionCartera> lstMeses = new List<ClasificacionCartera>();
            ClasificacionCartera clasificacion = new ClasificacionCartera();
            string filtro="";
            if (ddloficina.SelectedValue!="")
            {
                filtro = "and cod_oficina=" + ddloficina.SelectedValue;
            }
            //BalancePrueba lstValorCentros = new BalancePrueba();

            lstConsulta = clasificacionservice.Listarpersonas((Usuario)Session["Usuario"], ddlFechaInicio.SelectedValue.ToString(), ddlFechaFinal.Text.ToString(),filtro);
            lstMeses = clasificacionservice.listarfechas((Usuario)Session["Usuario"], ddlFechaInicio.SelectedValue.ToString(), ddlFechaFinal.Text.ToString());
            //lstCentros = BalancePruebaSer.ListarCentroCosto((Usuario)Session["Usuario"]);
            Session["Meses"] = lstMeses;
            Session["Historico_Clasificacion"] = lstConsulta;
            DataRow drDatos;

            int posicion = 1;
            DataTable dtDatos = new DataTable();
          

                // Mostrar la tabla de datos
                gvLista.Columns.Clear();

                BoundField ColumnBoundCOD = new BoundField();
                ColumnBoundCOD.HeaderText = " ";
                ColumnBoundCOD.DataField = "Numero_Radicacion";

                ColumnBoundCOD.ItemStyle.Width = 100;
                ColumnBoundCOD.ControlStyle.Width = 100;
                ColumnBoundCOD.HeaderStyle.Width = 100;
                gvLista.Columns.Add(ColumnBoundCOD);
                dtDatos.Columns.Add("Numero_Radicacion", typeof(string));
                dtDatos.Columns["Numero_Radicacion"].AllowDBNull = true;
                dtDatos.Columns["Numero_Radicacion"].DefaultValue = "0";
           

                BoundField ColumnBoundVal = new BoundField();
                ColumnBoundVal.HeaderText = " ";
                ColumnBoundVal.DataField = "Identificacion";

                ColumnBoundVal.ItemStyle.Width = 100;
                ColumnBoundVal.ControlStyle.Width = 100;
                ColumnBoundVal.HeaderStyle.Width = 100;
                gvLista.Columns.Add(ColumnBoundVal);
                dtDatos.Columns.Add("Identificacion", typeof(string));
                dtDatos.Columns["Identificacion"].AllowDBNull = true;
                dtDatos.Columns["Identificacion"].DefaultValue = "0";

                BoundField ColumnBoundNom = new BoundField();
                ColumnBoundNom.HeaderText = " ";
                ColumnBoundNom.DataField = "Nombre";

                ColumnBoundNom.ItemStyle.Width = 100;
                ColumnBoundNom.ControlStyle.Width = 100;
                ColumnBoundNom.HeaderStyle.Width = 100;
                gvLista.Columns.Add(ColumnBoundNom);
                dtDatos.Columns.Add("Nombre", typeof(string));
                dtDatos.Columns["Nombre"].AllowDBNull = true;
                dtDatos.Columns["Nombre"].DefaultValue = "0";



            foreach (ClasificacionCartera rFila in lstMeses)
            {
                if (rFila.fecha != null)
                {
                    //Agrega columnas de las fechas
                    //BoundField ColumnBoundKAP = new BoundField();
                    //ColumnBoundKAP.HeaderText = rFila.fecha;
                    ////ColumnBoundKAP.DataField = rFila.fecha;

                    //ColumnBoundKAP.ItemStyle.Width = 100;
                    //ColumnBoundKAP.ControlStyle.Width = 100;
                    //ColumnBoundKAP.HeaderStyle.Width = 100;
                    //gvLista.Columns.Add(ColumnBoundKAP);
                    //dtDatos.Columns.Add(rFila.fecha, typeof(string));
                    //dtDatos.Columns[rFila.fecha].AllowDBNull = true;         
                    //dtDatos.Columns[rFila.fecha].DefaultValue = "0";


                    //Agrega las columnas de clasificaciones
                    BoundField ColumnBoundKCC = new BoundField();
                    ColumnBoundKCC.HeaderText = "CC";
                    ColumnBoundKCC.DataField = "CC"+posicion;
                    ColumnBoundKCC.ItemStyle.Width = 100;
                    ColumnBoundKCC.ControlStyle.Width = 100;
                    ColumnBoundKCC.HeaderStyle.Width = 100;
                    gvLista.Columns.Add(ColumnBoundKCC);
                    dtDatos.Columns.Add("CC"+posicion, typeof(string));
                    dtDatos.Columns["CC" + posicion].AllowDBNull = true;
                    dtDatos.Columns["CC" + posicion].DefaultValue = "0";


                    BoundField ColumnBoundKCO = new BoundField();
                    ColumnBoundKCO.HeaderText = "CO";
                    ColumnBoundKCO.DataField = "CO"+posicion;
                    ColumnBoundKCO.ItemStyle.Width = 100;
                    ColumnBoundKCO.ControlStyle.Width = 100;
                    ColumnBoundKCO.HeaderStyle.Width = 100;
                    gvLista.Columns.Add(ColumnBoundKCO);
                    dtDatos.Columns.Add("CO" + posicion, typeof(string));
                    dtDatos.Columns["CO" + posicion].AllowDBNull = true;
                    dtDatos.Columns["CO" + posicion].DefaultValue = "0";

                    BoundField ColumnBoundKVI = new BoundField();
                    ColumnBoundKVI.HeaderText = "VI";
                    ColumnBoundKVI.DataField = "VI"+posicion;
                    ColumnBoundKVI.ItemStyle.Width = 100;
                    ColumnBoundKVI.ControlStyle.Width = 100;
                    ColumnBoundKVI.HeaderStyle.Width = 100;
                    gvLista.Columns.Add(ColumnBoundKVI);
                    dtDatos.Columns.Add("VI" + posicion, typeof(string));
                    dtDatos.Columns["VI" + posicion].AllowDBNull = true;
                    dtDatos.Columns["VI" + posicion].DefaultValue = "0";

                    BoundField ColumnBoundKMI = new BoundField();
                    ColumnBoundKMI.HeaderText = "MI";
                    ColumnBoundKMI.DataField = "MI"+posicion;
                    ColumnBoundKMI.ItemStyle.Width = 100;
                    ColumnBoundKMI.ControlStyle.Width = 100;
                    ColumnBoundKMI.HeaderStyle.Width = 100;
                    gvLista.Columns.Add(ColumnBoundKMI);
                    dtDatos.Columns.Add("MI" + posicion, typeof(string));
                    dtDatos.Columns["MI" + posicion].AllowDBNull = true;
                    dtDatos.Columns["MI" + posicion].DefaultValue = "0";
                }
                posicion = posicion + 1;
            }
           
            foreach (ClasificacionCartera rFila in lstConsulta)
            {
                drDatos = dtDatos.NewRow();
                if (rFila.NUMERO_RADICACION != null)
                {
                  
                    drDatos["Numero_Radicacion"] = Convert.ToString(rFila.NUMERO_RADICACION);
                    int columna = 1;
                    foreach (ClasificacionCartera rfila1 in lstMeses)
                    {
                        clasificacion = clasificacionservice.ConsultarClasificacionHis(rFila.NUMERO_RADICACION,"1",rfila1.FECHA_HISTORICO, (Usuario)Session["Usuario"]);
                        drDatos["CC"+columna] = Convert.ToString(clasificacion.CC);
                       
                        drDatos["CO" + columna] = Convert.ToString(clasificacion.CO);
                        
                        drDatos["VI" + columna] = Convert.ToString(clasificacion.VI);
                        
                        drDatos["MI" + columna] = Convert.ToString(clasificacion.MI);
                        columna = columna + 1;
                    }
                   
                    
                }
                if (rFila.NOMBRE != null)
                {
                    drDatos["Nombre"] = Convert.ToString(rFila.NOMBRE);
                    drDatos["Identificacion"] = Convert.ToString(rFila.IDENTIFICACION);
                }

                dtDatos.Rows.Add(drDatos);
              

            }
             dtDatos.AcceptChanges();
           
            gvLista.DataSource = dtDatos;
            gvLista.DataBind();
            //gvLista.Columns[0].Visible = false;
            //gvLista.Columns[1].Visible = false;
            //gvLista.Columns[2].Visible = false;
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = "Codigo";
            cell.ColumnSpan = 1;
            //cell.RowSpan = 2;
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 1;
            //cell.RowSpan = 2;
            cell.Text = " Identificacion";
            row.Controls.Add(cell);

            cell = new TableHeaderCell();
            cell.ColumnSpan = 1;
           /* cell.RowSpan = 2*/;
            cell.Text = "Nombre";
            row.Controls.Add(cell);

            foreach (ClasificacionCartera rFila in lstMeses)
            {
                cell = new TableHeaderCell();
                cell.ColumnSpan = 4;
                cell.Text = rFila.fecha;
                row.Controls.Add(cell);
            }
            gvLista.HeaderRow.Parent.Controls.AddAt(0, row);

        }

         catch (Exception ex)
        {
            //BOexcepcion.Throw(BalancePruebaSer.CodigoPrograma, "Actualizar", ex);
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
            //BOexcepcion.Throw(BalancePruebaSer.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        
    }
    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {

        if (gvLista.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteHistoricoClasifi.xls");
            Response.Charset = "UTF-16";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

    }
}