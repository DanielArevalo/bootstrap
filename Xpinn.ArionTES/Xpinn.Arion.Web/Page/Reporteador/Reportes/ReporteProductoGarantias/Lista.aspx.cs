using System;
using System.IO;
using System.Web.UI;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using Xpinn.Util;
using System.Web;
using Xpinn.Reporteador.Services;

public partial class Lista : GlobalWeb
{
    ReporteService objReporteService = new ReporteService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(objReporteService.CodigoProgramaReporteProductoGarantias, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ucFechaCorte.Text = DateTime.Now.ToString();
        }
    }
    public void LlenarGridview()
    {
        
        DataTable dtResultado = objReporteService.ConsultarProductos_GarantiasComunitarias(DateTime.Parse(ucFechaCorte.Text), (Usuario)Session["Usuario"]);
        gvLista.DataSource = dtResultado;
        gvLista.DataBind();
    }

    private void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0)
            {
                foreach (GridViewRow item in gvLista.Rows)
                {
                    Label lblNicho = (Label)item.FindControl("lblNicho");
                    Label lblMonto = (Label)item.FindControl("lblMonto");
                    Label lblTasa = (Label)item.FindControl("lblTasa");
                    TextBox txtNicho = (TextBox)item.FindControl("txtNicho");
                    TextBox txtMonto = (TextBox)item.FindControl("txtMonto");
                    TextBox txtTasa = (TextBox)item.FindControl("txtTasa");
                    lblNicho.Visible = true;
                    lblMonto.Visible=true;
                    lblTasa.Visible = true;
                    txtNicho.Visible = false;
                    txtMonto.Visible =false ;
                    txtTasa.Visible =false;
                    lblNicho.Text = txtNicho.Text;
                    lblMonto.Text = txtMonto.Text;
                    lblTasa.Text = txtTasa.Text;

                }
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvLista);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename="+txtArchivo.Text+".xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.UTF8;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        
            LlenarGridview();
        
        
    }

    }