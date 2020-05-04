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
    ClienteService clienteServicio = new ClienteService();
    CreditosService creditoServicios = new CreditosService();
    ReporteService reporteServicio = new ReporteService();

    Usuario usuario = new Usuario();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(clienteServicio.CodigoProgramaReportesAsesores, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoProgramaReportesAsesores, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtFechaIni.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtFechaIni.Enabled = false;
                LlenarComboOficinas();
                Usuario usuap = (Usuario)Session["usuario"];
                Int64 oficina = usuap.cod_oficina;
                LlenarComboAsesores(oficina);
                CargarValoresConsulta(pConsulta, clienteServicio.CodigoProgramaReportesAsesores);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoProgramaReportesAsesores, "Page_Load", ex);
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
        LimpiarValoresConsulta(pConsulta, clienteServicio.CodigoProgramaReportesAsesores);
        
    }
    
    protected void gvListaCreditos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {           
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoProgramaReportesAsesores, "gvListaCreditos_PageIndexChanging", ex);
        }
    }

    protected void gvReoirtemora_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReoirtecobranza.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoProgramaReportesAsesores, "gvListaClientes_PageIndexChanging", ex);
        }
    }
    /// <summary>
    /// Método para traer los datos según las condiciones ingresadas
    /// </summary>
    private void Actualizar()
    {
        try
        {
            string rango = "", oficina, asesor;

            Usuario usuap = (Usuario)Session["usuario"];           

            if (ddlOficina.SelectedValue == "0") 
                oficina = "todos";
            else
                oficina = ddlOficina.SelectedValue;

            if (ddlAsesores.SelectedValue == "0")
                asesor = "todos";
            else
                asesor = ddlAsesores.SelectedValue;


            rango = RadioButtonList1.SelectedValue;


            if (RadioButtonList1.SelectedValue == "7")
            {
                rango = TxtDesde.Text  + " AND "  + TxtHasta.Text ;
            }

           

            List<Reporte> lstConsultaClientes = new List<Reporte>();
              
            long cod_usuario = (usuap.codusuario);
            lstConsultaClientes = reporteServicio.ListarReportecobranza((Usuario)Session["usuario"], cod_usuario, rango, asesor, oficina);
            gvReoirtecobranza.EmptyDataText = emptyQuery;
            gvReoirtecobranza.DataSource = lstConsultaClientes;
            if (lstConsultaClientes.Count > 0)
            {
                mvLista.SetActiveView(vGridReporteCobranza);
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaClientes.Count.ToString();
                gvReoirtecobranza.DataBind();
                ValidarPermisosGrilla(gvReoirtecobranza);
            }
            else
            {
                mvLista.ActiveViewIndex = -1;
            }

            Session.Add(clienteServicio.CodigoProgramaReportesAsesores + ".consulta", 1);
            
        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
    }

    private string obtFiltro()
    {

        String filtro = String.Empty;

        return filtro;
    }
   

    protected void ddlOficina_SelectedIndexChanged(object sender, EventArgs e)
    {
        LlenarComboAsesores(Convert.ToInt64(ddlOficina.SelectedValue));
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvReoirtecobranza.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvReoirtecobranza.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvReoirtecobranza);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=reporte_cartera_cobranza.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");  
    }

   
     protected void LlenarComboAsesores(Int64 iOficina)
     {
        Usuario usuap = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap.codusuario);
                    
        EjecutivoService serviceEjecutivo = new EjecutivoService();
        Ejecutivo ejec = new Ejecutivo();
        ejec.IOficina = iOficina;
        ddlAsesores.DataSource = serviceEjecutivo.ListarAsesores(ejec, (Usuario)Session["usuario"]);
        ddlAsesores.DataTextField = "NombreCompleto";
        ddlAsesores.DataValueField = "IdEjecutivo";
        ddlAsesores.DataBind();
        ddlAsesores.Items.Insert(0, new ListItem("<Todos>", "0"));        
     }


     protected void LlenarComboOficinas()
     {
         OficinaService oficinaService = new OficinaService();
     
         Usuario usuap = (Usuario)Session["usuario"];
         int cod = Convert.ToInt32(usuap.codusuario);
         int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);

         Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
         ddlOficina.DataSource = oficinaService.ListarOficinasUsuarios(oficina, (Usuario)Session["usuario"]);

         if (consulta >= 1) // Trae Todos las oficinas
        {
            ddlOficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
           
       }
         ddlOficina.DataTextField = "Nombre";
         ddlOficina.DataValueField = "Codigo";
         ddlOficina.DataBind();
     }
}
