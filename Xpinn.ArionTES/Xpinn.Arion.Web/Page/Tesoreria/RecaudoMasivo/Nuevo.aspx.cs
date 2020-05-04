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

public partial class Nuevo : GlobalWeb
{
    Usuario usuario = new Usuario();
    RecaudosMasivos entityRecaudosMasivos = new RecaudosMasivos();
    RecaudosMasivosService servicerecaudos = new RecaudosMasivosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(servicerecaudos.CodigoProgramaAplicacion, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicerecaudos.GetType().Name + "L", "Page_PreInit", ex);
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
                if (Session[servicerecaudos.CodigoProgramaAplicacion + ".id"] != null && Session[servicerecaudos.CodigoProgramaAplicacion + ".id"].ToString().Trim() != "")
                {
                    idObjeto = Session[servicerecaudos.CodigoProgramaAplicacion + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }
            }
            msg.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicerecaudos.GetType().Name + "L", "Page_Load", ex);
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
            BOexcepcion.Throw(servicerecaudos.CodigoProgramaAplicacion + "L", "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(servicerecaudos.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<RecaudosMasivos> lstConsulta = new List<RecaudosMasivos>();
            lstConsulta = servicerecaudos.ListarDetalleRecaudo(Convert.ToInt32(txtNumeroLista.Text), (Usuario)Session["Usuario"]);
            lblTotalReg.Visible = true;
            lblTotalReg.Text = "Se encontraron " + lstConsulta.Count() + " registros";
            gvMovGeneral.DataSource = lstConsulta;
            gvMovGeneral.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicerecaudos.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            RecaudosMasivos vRecaudos = new RecaudosMasivos();
            vRecaudos = servicerecaudos.ConsultarRecaudo(pIdObjeto, (Usuario)Session["usuario"]);

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
            BOexcepcion.Throw(servicerecaudos.CodigoProgramaAplicacion, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar("~/Page/Tesoreria/RecaudoMasivo/Lista.aspx");
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        mpeNuevo.Show();
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Configuracion conf = new Configuracion();
        string Error = "";
        Int64 numero_reclamacion = Convert.ToInt64(txtNumeroLista.Text);
        List<RecaudosMasivos> lstReclamaciones = new List<RecaudosMasivos>();
        lstReclamaciones.Clear();

        // Cargando las reclamaciones en una lista
        foreach (GridViewRow row in gvMovGeneral.Rows)
        {
            RecaudosMasivos eRecaudos = new RecaudosMasivos();
            eRecaudos.fecha_aplicacion = ucFechaAplicacion.ToDateTime;
            eRecaudos.iddetalle = Convert.ToInt64(row.Cells[0].Text);
            eRecaudos.cod_cliente = Convert.ToInt64(row.Cells[1].Text);
            eRecaudos.identificacion = Convert.ToString(row.Cells[2].Text);
            eRecaudos.tipo_producto = Convert.ToString(row.Cells[4].Text);
            eRecaudos.numero_producto = Convert.ToString(row.Cells[5].Text);           
            eRecaudos.tipo_aplicacion = "Por Valor a Capital";
            DropDownListGrid ddlTipoAplicacion = (DropDownListGrid)row.Cells[6].FindControl("ddlTipoAplicacion");
            if (ddlTipoAplicacion != null)
                eRecaudos.tipo_aplicacion = ddlTipoAplicacion.SelectedValue;
            eRecaudos.num_cuotas = Convert.ToInt64(row.Cells[7].Text);
            eRecaudos.valor = Convert.ToDecimal(row.Cells[8].Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""));
            eRecaudos.sobrante = 0;
            lstReclamaciones.Add(eRecaudos);
        }

        // Validando las reclamaciones
        servicerecaudos.Validar(ucFechaAplicacion.ToDateTime, lstReclamaciones, (Usuario)Session["Usuario"], ref Error);
        if (Error.Trim() == "")
        {

            // Aplicando las reclamaciones
            Int64 CodOpe = 0;
            servicerecaudos.AplicarPago(numero_reclamacion, ucFechaAplicacion.ToDateTime, lstReclamaciones, (Usuario)Session["Usuario"], ref Error, ref CodOpe);
            if (Error.Trim() == "")
            {
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Usuario pUsuario = new Usuario();
                pUsuario = (Usuario)Session["Usuario"];
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = CodOpe;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 119;
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = pUsuario.codusuario;
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }
        }
        else
        {
            VerError(Error);
        }
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
        mvAplicar.ActiveViewIndex = 0;
    }

    protected void gvMovGeneral_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlTipoAplicacion = (DropDownListGrid)e.Row.Cells[4].FindControl("ddlTipoAplicacion");
            if (ddlTipoAplicacion != null)
            {
                if (e.Row.Cells[2].Text == "Creditos")
                    ddlTipoAplicacion.Visible = true;
                else
                    ddlTipoAplicacion.Visible = false;
            }
        }
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
            BOexcepcion.Throw(servicerecaudos.CodigoPrograma, "CargarEmpresaRecaudo", ex);
        }
    }

}