using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;
using System.Configuration;

public partial class Lista : GlobalWeb
{
    Usuario usuario = new Usuario();
    EjecutivoService serviceEjecutivo = new EjecutivoService();
    ParametricaService serviceParametrica = new ParametricaService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(serviceEjecutivo.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;

            ucImprimir.PrintCustomEvent += ucImprimir_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ObtenerDatosDropDownList();
                if (Session[serviceEjecutivo.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.GetType().Name + "L", "Page_Load", ex);
        }
    }

    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = gvLista;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", Imprimir.JSCRIPT_PRINT);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, serviceEjecutivo.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, serviceEjecutivo.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnBorrar");
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[0].Text;
        Session[serviceEjecutivo.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[serviceEjecutivo.CodigoPrograma + ".id"] = id;
        //Session[serviceEjecutivo.CodigoPrograma + ".from"] = "l";
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            serviceEjecutivo.EliminarEjecutivo(idObjeto, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.CodigoPrograma + "L", "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(serviceEjecutivo.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Ejecutivo> lstConsulta = new List<Ejecutivo>();

            lstConsulta = serviceEjecutivo.ListarEjecutivo(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;

                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(serviceEjecutivo.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Ejecutivo ObtenerValores()
    {
        Ejecutivo entityEjecutivo = new Ejecutivo();

        if (!string.IsNullOrEmpty(txtPrimerNombre.Text.Trim()))
            entityEjecutivo.PrimerNombre = txtPrimerNombre.Text.Trim();
        if (!string.IsNullOrEmpty(txtPrimerApellido.Text.Trim()))
            entityEjecutivo.PrimerApellido = txtPrimerApellido.Text.Trim();
        if (!string.IsNullOrEmpty(txtIdentificacion.Text.Trim()))
            entityEjecutivo.NumeroDocumento = txtIdentificacion.Text.Trim();
        if (!string.IsNullOrEmpty(ddlOficina.SelectedValue.ToString()) && ddlOficina.SelectedValue != "Seleccione")
            entityEjecutivo.IdOficina = Convert.ToInt64(ddlOficina.SelectedValue.ToString());
        if (!string.IsNullOrEmpty(ddlEstado.SelectedValue.ToString()) && ddlEstado.SelectedValue != "Seleccione")
            entityEjecutivo.IdEstado = Convert.ToInt64(ddlEstado.SelectedValue);

        return entityEjecutivo;
    }

    private void ObtenerDatosDropDownList()
    {
        ddlEstado.DataSource = serviceParametrica.ListarEstado(new Estado() { }, (Usuario)Session["usuario"]);
        ddlEstado.DataTextField = "Descripcion";
        ddlEstado.DataValueField = "IdEstado";
        ddlEstado.DataBind();
        ddlEstado.SelectedIndex = ddlEstado.Items.Count;

        ddlOficina.DataSource = serviceParametrica.ListarOficina(new Oficina() { }, (Usuario)Session["usuario"]);
        ddlOficina.DataTextField = "NombreOficina";
        ddlOficina.DataValueField = "IdOficina";
        ddlOficina.DataBind();
        ddlOficina.Items.Add("Seleccione");
        ddlOficina.SelectedIndex = ddlOficina.Items.Count;
        ddlOficina.SelectedValue = "Seleccione";
    }
}