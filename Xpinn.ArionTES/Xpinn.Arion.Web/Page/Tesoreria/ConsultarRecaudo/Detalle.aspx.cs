using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Detalle : GlobalWeb
{
    Usuario usuario = new Usuario();    
    Xpinn.Tesoreria.Services.RecaudosMasivosService RecaudosMasivosServicio = new Xpinn.Tesoreria.Services.RecaudosMasivosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(RecaudosMasivosServicio.CodigoProgramaConsulta, "L");

            Site toolBar = (Site)this.Master;            
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarEmpresaRecaudo();
                mvAplicar.ActiveViewIndex = 0;
                if (Session[RecaudosMasivosServicio.CodigoProgramaConsulta + ".id"] != null)
                {
                    idObjeto = Session[RecaudosMasivosServicio.CodigoProgramaConsulta + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }
            }
            msg.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.GetType().Name + "L", "Page_Load", ex);
        }

    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta + "L", "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        if (evt.CommandName == "Editar")
        {
            String[] tmp = evt.CommandArgument.ToString().Split('|');
            RecaudosMasivos ejeMeta = new RecaudosMasivos();
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<RecaudosMasivos> lstConsulta = new List<RecaudosMasivos>();
            lstConsulta = RecaudosMasivosServicio.ListarDetalleRecaudoConsulta(Convert.ToInt32(txtNumeroLista.Text), true, (Usuario)Session["Usuario"]);
            lblTotalReg.Visible = true;
            lblTotalReg.Text = "Se encontraron " + lstConsulta.Count() + " registros";
            gvLista.DataSource = lstConsulta;
            gvLista.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            RecaudosMasivos vRecaudos = new RecaudosMasivos();
            vRecaudos = RecaudosMasivosServicio.ConsultarRecaudo(pIdObjeto, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vRecaudos.numero_recaudo.ToString()))
                txtNumeroLista.Text = HttpUtility.HtmlDecode(vRecaudos.numero_recaudo.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.fecha_aplicacion.ToString()))
                ucFechaAplicacion.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vRecaudos.fecha_aplicacion.ToString()));
            if (!string.IsNullOrEmpty(vRecaudos.cod_empresa.ToString()))
                ddlEntidad.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.cod_empresa.ToString());

            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar("~/Page/Tesoreria/ConsultarRecaudo/Lista.aspx");
    }

   
    protected void CargarEmpresaRecaudo()
    {
        try
        {
            Xpinn.Tesoreria.Services.RecaudosMasivosService recaudoServicio = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstModulo = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();

            lstModulo = recaudoServicio.ListarEmpresaRecaudo(null, (Usuario)Session["usuario"]);

            ddlEntidad.DataSource = lstModulo;
            ddlEntidad.DataTextField = "nom_empresa";
            ddlEntidad.DataValueField = "cod_empresa";
            ddlEntidad.DataBind();

            ddlEntidad.Items.Insert(0, "");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta, "CargarEmpresaRecaudo", ex);
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AplicacionRecaudosMasivos.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(GridView1, null);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

}