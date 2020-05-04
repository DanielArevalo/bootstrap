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

public partial class EstadoCuentaComentarioDetalle : GlobalWeb
{
    ComentarioService serviceComentario = new ComentarioService();
    Producto producto;
    Usuario _usuario;

    private void Page_PreInit(object sender, EventArgs evt)
    {
        try
        {
            if (Session[serviceComentario.CodigoPrograma + ".id"] != null)
            {
                VisualizarOpciones(serviceComentario.CodigoPrograma, "E");
            }
            else
            {
                VisualizarOpciones(serviceComentario.CodigoPrograma, "A");
            }
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            ucImprimir.PrintCustomEvent += ucImprimir_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceComentario.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {    
                if (Session[MOV_GRAL_CRED_PRODUC] != null) 
                    ObtenerComentarios();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceComentario.GetType().Name + "D", "Page_Load", ex);
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {

        Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");


    }

    protected void btnGuardar_Click(object sender, EventArgs evt)
    {
        serviceComentario.CrearComentario(ObtenerDatos(), _usuario);
        if (Session[MOV_GRAL_CRED_PRODUC] != null) 
            ObtenerComentarios();
        txtComment.Text = "";
    }
    protected void chkVerComentario_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkPrincipal = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkPrincipal.CommandArgument);
        GridViewRow fila = gvComentario.Rows[rowIndex];

        if (chkPrincipal != null && fila != null)
        {
            Comentario comentario = new Comentario();
            comentario.idComentario = Convert.ToInt64(fila.Cells[1].Text);
            comentario.puedeVerAsociado = ((CheckBoxGrid)fila.FindControl("chkVerComentario")).Checked;

            serviceComentario.ModificarComentario(comentario, _usuario);
        }
    }

    protected void gvComentario_PageIndexChanging(object sender, GridViewPageEventArgs evt)
    {
        try
        {
            gvComentario.PageIndex = evt.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceComentario.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {

        var lstComentarios = (from p in producto.Comentarios orderby p.idComentario descending select p).ToList();

        gvComentario.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        gvComentario.DataSource = lstComentarios;

        if (lstComentarios.Count() > 0)
        {
            gvComentario.Visible = true;
            lblInfo.Visible = false;
            lblTotalReg.Visible = true;
            lblTotalReg.Text = "<br/> Registros encontrados " + lstComentarios.Count().ToString();
            gvComentario.DataBind();
            ValidarPermisosGrilla(gvComentario);
        }
        else
        {
            gvComentario.Visible = false;
        }
    }

    protected Comentario ObtenerDatos()
    {
        try
        {
            Comentario coment = new Comentario();
            producto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);

            coment.idPersona = Convert.ToInt64(producto.Persona.IdPersona);
            coment.fecha = DateTime.Now.ToShortDateString();
            coment.hora = DateTime.Now.ToShortTimeString();
            coment.numeroCredito = Convert.ToInt64(producto.CodRadicacion);
            coment.descripcion = txtComment.Text.ToString();
            coment.puedeVerAsociado = chkPuedeVerAsociado.Checked;

            return coment;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceComentario.GetType().Name + "A", "ObtenerDatos", ex);
            return null;
        }
    }

    private void ObtenerComentarios()
    {
        producto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);
        producto.Comentarios = serviceComentario.ListarComentario(producto, _usuario);
        Session[MOV_GRAL_CRED_PRODUC] = producto;
        Actualizar();
    }

    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = gvComentario;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", Imprimir.JSCRIPT_PRINT);
    }


    protected void gvComentario_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        VerError("");
        try
        {
                Producto p = new Producto();
                Int64 id = Convert.ToInt64(gvComentario.Rows[e.RowIndex].Cells[1].Text);                
                serviceComentario.EliminarComentario(id, _usuario);                
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
        ObtenerComentarios();
    }

    protected void imgBtnVolverHandler(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");
    }

}