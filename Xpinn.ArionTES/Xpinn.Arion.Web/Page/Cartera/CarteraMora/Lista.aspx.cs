using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Configuration;

public partial class Page_Asesores_Colocacion_Lista : GlobalWeb
{
    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    ClienteService clienteServicio = new ClienteService();
    CreditoService creditoServicio = new CreditoService();
    CreditosService creditoServicios = new CreditosService();
    ExcelService excelServicio = new ExcelService();
    ReporteService reporteServicio = new ReporteService();
    Xpinn.FabricaCreditos.Services.UsuarioAtribucionesService UsuarioAtribucionesServicio = new Xpinn.FabricaCreditos.Services.UsuarioAtribucionesService();
    Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones vUsuarioAtribuciones = new Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones();
    
    MovGralCreditoService movGrlService = new MovGralCreditoService();
    Usuario usuario = new Usuario();
    
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(clienteServicio.CodigoProgRepoMora, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoProgRepoMora, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {           
           
            if (!IsPostBack)
            {
                Panel3.Visible = false;
                Panel4.Visible = false;
                ddlAsesores.Visible = false;
                Labelejecutivos.Visible = false;
                CargarValoresConsulta(pConsulta, clienteServicio.CodigoProgRepoMora);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                if (Session[clienteServicio.CodigoProgRepoMora + ".consulta"] != null)
                {
                    
                    
                }
            }
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoProgRepoMora, "Page_Load", ex);
        }
    }


    /// <summary>
    /// Método que permite generar el reporte segùn las condiciones dadas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
           GuardarValoresConsulta(pConsulta, clienteServicio.CodigoProgRepoMora);
            
            switch (ddlConsultar.SelectedIndex)
            {
                case 1: Session["op1"] = 1;
                        break;
                case 2: Session["op1"] = 2;
                        break;
            }
            Actualizar();
            
        }
    }

    /// <summary>
    /// Méotodo para limpiar datos de la conuslta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, clienteServicio.CodigoProgRepoMora);
        gvReoirtemora.DataSource = null;       
        gvReoirtemora.DataBind();
       // gvReoirtemora.Visible = false;
        gvRepCobranza.DataSource = null;
        gvRepCobranza.DataBind();
        //gvRepCobranza.Visible = false;
        txtFechaFin.Text = "";
        txtFechaIni.Text = "";
        lblTotalRegscobranzas.Text = "";
        lblTotalRegs.Text = "";
    }
    
    protected void gvListaCreditos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
           
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoProgRepoMora, "gvListaCreditos_PageIndexChanging", ex);
        }
    }

    protected void gvReoirtemora_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
           gvReoirtemora.PageIndex = e.NewPageIndex;
           Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoProgRepoMora, "gvListaClientes_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para traer los datos según las condiciones ingresadas
    /// </summary>
    private void Actualizar()
    {
        Configuracion conf = new Configuracion();
        VerError("");
        try
        {
            Usuario usuap = (Usuario)Session["usuario"];

            // Determinar el còdigo del usuario y determinar si el usuario es un ejecutivo      

          
          
            // Generar el reporte
            switch ((int)Session["op1"])
            {

                case 1:

                    if (ChkTodos.Checked == true)
                    {
                        List<Reporte> lstConsultaMora = new List<Reporte>();
                        lstConsultaMora = reporteServicio.ListarReporteMoraTodos((Usuario)Session["usuario"]);


                        gvReoirtemora.EmptyDataText = emptyQuery;
                        gvReoirtemora.DataSource = lstConsultaMora;
                        if (lstConsultaMora.Count > 0)
                        {
                            mvLista.SetActiveView(vGridReporteMora);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaMora.Count.ToString();
                            gvReoirtemora.DataBind();
                            ValidarPermisosGrilla(gvReoirtemora);
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                    }
                    else
                    {
                        List<Reporte> lstConsultaMora = new List<Reporte>();
                        lstConsultaMora = reporteServicio.ListarReporteMora((Usuario)Session["usuario"], Convert.ToInt64(ddlAsesores.SelectedValue));


                        gvReoirtemora.EmptyDataText = emptyQuery;
                        gvReoirtemora.DataSource = lstConsultaMora;
                        if (lstConsultaMora.Count > 0)
                        {
                            mvLista.SetActiveView(vGridReporteMora);
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaMora.Count.ToString();
                            gvReoirtemora.DataBind();
                            ValidarPermisosGrilla(gvReoirtemora);
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }

                    }

                    Session.Add(clienteServicio.CodigoProgRepoMora + ".consulta", 1);

                    break;
                case 2:
                     DateTime fechaini, fechafinal;
                     if (txtFechaIni.Text == "" && txtFechaFin.Text == "")
                     {
                         Label3.Text = "falta fecha inicial";
                         Label4.Text = " falta fecha final";
                     }
                     else
                     {


                         fechafinal = txtFechaFin.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaFin.Text);
                         fechaini = txtFechaIni.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaIni.Text);

                         List<Reporte> lstConsultaCobranzas = new List<Reporte>();
                         lstConsultaCobranzas = reporteServicio.ListarReporteGestionCobranzas((Usuario)Session["usuario"], Convert.ToInt64(ddlAsesores.SelectedValue), fechaini, fechafinal);


                         gvRepCobranza.EmptyDataText = emptyQuery;
                         gvRepCobranza.DataSource = lstConsultaCobranzas;
                         if (lstConsultaCobranzas.Count > 0)
                         {
                             mvLista.SetActiveView(VGridReporteCobranzas);
                             lblTotalRegscobranzas.Text = "<br/> Registros encontrados " + lstConsultaCobranzas.Count.ToString();
                             gvRepCobranza.DataBind();
                             ValidarPermisosGrilla(gvRepCobranza);
                         }
                         else
                         {
                             mvLista.ActiveViewIndex = -1;
                         }

                     }
                    Session.Add(clienteServicio.CodigoProgRepoMora + ".consulta", 1);

                    break;

            }
        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
    }

    /// <summary>
    /// Método para llenar el combo de oficinas
    /// </summary>
    /// <param name="Ddloficinas"></param>
    protected void LlenarComboOficinasAsesores(DropDownList Ddloficinas)
    {

        OficinaService oficinaService = new OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        Ddloficinas.DataSource = oficinaService.ListarOficinasAsesores(oficina, (Usuario)Session["usuario"]);
        Ddloficinas.DataTextField = "Nombre";
        Ddloficinas.DataValueField = "Codigo";
        Ddloficinas.DataBind();
        Ddloficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    /// <summary>
    /// Método para obtener datos del filtro
    /// </summary>
    /// <returns></returns>
    private string obtFiltro()
    {

        String filtro = String.Empty;

        return filtro;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvReoirtemora.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvReoirtemora.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvReoirtemora);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");  
    }  
   
     protected void Button1_Click(object sender, EventArgs e)
     {
        Response.Redirect("~/Page/Asesores/GestionarAgenda/Lista.aspx");
     }      
     
   protected void ddlConsultar_SelectedIndexChanged(object sender, EventArgs e)
     {
         if (ddlConsultar.SelectedIndex == 1)
         {
             Usuario usuap = (Usuario)Session["usuario"];
             ddlAsesores.Visible = true;
             Labelejecutivos.Visible = true;
             EjecutivoService serviceEjecutivo = new EjecutivoService();
             ddlAsesores.DataSource = serviceEjecutivo.ListartodosAsesores((Usuario)Session["usuario"]);
             ddlAsesores.DataTextField = "NombreCompleto";
             ddlAsesores.DataValueField = "IdEjecutivo";
             ddlAsesores.DataBind();
             ddlAsesores.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
             Panel3.Visible = false;
             Panel4.Visible = false;
         }
         else
         {
             mvLista.ActiveViewIndex = -1;
         }

         if (ddlConsultar.SelectedIndex == 2)
         {
             Panel3.Visible = true;
             Panel4.Visible = true;
             Usuario usuap = (Usuario)Session["usuario"];
             ddlAsesores.Visible = true;
             Labelejecutivos.Visible = true;
             EjecutivoService serviceEjecutivo = new EjecutivoService();
             ddlAsesores.DataSource = serviceEjecutivo.ListartodosUsuarios((Usuario)Session["usuario"]);
             ddlAsesores.DataTextField = "NombreCompleto";
             ddlAsesores.DataValueField = "IdEjecutivo";
             ddlAsesores.DataBind();
             ddlAsesores.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
         }
         else
         {
             mvLista.ActiveViewIndex = -1;
         }

     }
         
     decimal subtotalgarantiascomunitarias = 0;
     decimal subtotalsaldo_capital = 0;
     decimal subtotalvalor_cuota = 0;
     decimal subtotalpendite_cuota = 0;

     protected void gvReoirtemora_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             subtotalgarantiascomunitarias += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "garantia_comunitaria"));
             subtotalsaldo_capital += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo_capital"));
             subtotalvalor_cuota += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor_cuota"));
             subtotalpendite_cuota += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pendite_cuota"));
         }
         if (e.Row.RowType == DataControlRowType.Footer)
         {
             e.Row.Cells[3].Text = "Total:";

             e.Row.Cells[6].Text = subtotalvalor_cuota.ToString("c");
             e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
             e.Row.Cells[8].Text = subtotalsaldo_capital.ToString("c");
             e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
             e.Row.Cells[9].Text = subtotalgarantiascomunitarias.ToString("c");
             e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Left;
             e.Row.Cells[10].Text = subtotalpendite_cuota.ToString("c");
             e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Left;
        
            
             e.Row.Font.Bold = true;
         }
     }


     protected void ddlAsesores_SelectedIndexChanged(object sender, EventArgs e)
     {

     }
     protected void gvReoirtemora_SelectedIndexChanged(object sender, EventArgs e)
     {

     }
     protected void mvLista_ActiveViewChanged(object sender, EventArgs e)
     {

     }
     protected void gvReoirtemora_PageIndexChanging1(object sender, GridViewPageEventArgs e)
     {
         try
         {
             gvReoirtemora.PageIndex = e.NewPageIndex;
             Actualizar();
         }
         catch (Exception ex)
         {
             BOexcepcion.Throw(clienteServicio.CodigoProgRepoMora, "gvListaClientes_PageIndexChanging", ex);
         }
     }
     protected void btnExportarDilig_Click(object sender, EventArgs e)
     {
         if (gvRepCobranza.Rows.Count > 0)
         {
             StringBuilder sb = new StringBuilder();
             StringWriter sw = new StringWriter(sb);
             HtmlTextWriter htw = new HtmlTextWriter(sw);
             Page pagina = new Page();
             dynamic form = new HtmlForm();
             gvRepCobranza.EnableViewState = false;
             pagina.EnableEventValidation = false;
             pagina.DesignerInitialize();
             pagina.Controls.Add(form);
             form.Controls.Add(gvRepCobranza);
             pagina.RenderControl(htw);
             Response.Clear();
             Response.Buffer = true;
             Response.ContentType = "application/vnd.ms-excel";
             Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
             Response.Charset = "UTF-8";
             Response.ContentEncoding = Encoding.Default;
             Response.Write(sb.ToString());
             Response.End();
         }
         else
             VerError("Se debe generar el reporte primero");  
     }

    protected void gvRepCobranza_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string langId2 = DataBinder.Eval(e.Row.DataItem, "Acuerdo").ToString();
            e.Row.Cells[8].Text = langId2 == "0" ? "No": "Si";
        }
    }
}
