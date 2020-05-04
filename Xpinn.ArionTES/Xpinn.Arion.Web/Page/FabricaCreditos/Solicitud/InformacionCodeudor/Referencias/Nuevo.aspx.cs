using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Collections.Generic;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.RefernciasService RefernciasServicio = new Xpinn.FabricaCreditos.Services.RefernciasService();
    
    //Listas desplegables:
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    List<Xpinn.FabricaCreditos.Entities.Referncias> lstListasDesplegables = new List<Xpinn.FabricaCreditos.Entities.Referncias>();  //Lista de los menus desplegables
    
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[RefernciasServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(RefernciasServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(RefernciasServicio.CodigoPrograma, "A");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;

            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                if (Session[RefernciasServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[RefernciasServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(RefernciasServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        try
        {
            ListaSolicitada = "Parentesco";
            TraerResultadosLista();
            ddlParentesco.DataSource = lstListasDesplegables;
            ddlParentesco.DataTextField = "ListaDescripcion";
            ddlParentesco.DataValueField = "ListaId";
            ddlParentesco.DataBind();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private void TraerResultadosLista()
    {
        lstListasDesplegables.Clear();
        lstListasDesplegables = RefernciasServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Referncias vReferncias = new Xpinn.FabricaCreditos.Entities.Referncias();

            if (idObjeto != "")
                vReferncias = RefernciasServicio.ConsultarReferncias(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (txtCodreferencia.Text != "") vReferncias.codreferencia = Convert.ToInt64(txtCodreferencia.Text.Trim());
            if (txtCod_persona.Text != "") vReferncias.cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());
            if (rblTipoReferencia.Text != "") vReferncias.tiporeferencia = Convert.ToInt64(rblTipoReferencia.SelectedValue);
            vReferncias.nombres = (txtNombres.Text != "") ? Convert.ToString(txtNombres.Text.Trim()) : String.Empty;
            if (ddlParentesco.Text != "") vReferncias.codparentesco = Convert.ToInt64(ddlParentesco.SelectedValue);
            //vReferncias.direccion = Convert.ToString(txtDireccion.Text.Trim());
            vReferncias.direccion = (direccion.Text != "") ? Convert.ToString(direccion.Text.Trim()) : String.Empty;
            //vReferncias.telefono = Convert.ToString(txtTelefono.Text.Trim());
            vReferncias.telefono = (txtTelefono.Text != "") ? Convert.ToString(txtTelefono.Text.Trim()) : String.Empty;
            //vReferncias.teloficina = Convert.ToString(txtTeloficina.Text.Trim());
            vReferncias.teloficina = (txtTeloficina.Text != "") ? Convert.ToString(txtTeloficina.Text.Trim()) : String.Empty;
            //vReferncias.celular = Convert.ToString(txtCelular.Text.Trim());
            vReferncias.celular = (txtCelular.Text != "") ? Convert.ToString(txtCelular.Text.Trim()) : String.Empty;
            if (txtEstado.Text != "") vReferncias.estado = Convert.ToInt64(txtEstado.Text.Trim());
            if (txtCodusuverifica.Text != "") vReferncias.codusuverifica = Convert.ToInt64(txtCodusuverifica.Text.Trim());
            if (txtFechaverifica.Text != "") vReferncias.fechaverifica = Convert.ToDateTime(txtFechaverifica.Text.Trim());
            //vReferncias.calificacion = Convert.ToString(txtCalificacion.Text.Trim());
            vReferncias.calificacion = (txtCalificacion.Text != "") ? Convert.ToString(txtCalificacion.Text.Trim()) : String.Empty;
            //vReferncias.observaciones = Convert.ToString(txtObservaciones.Text.Trim());
            vReferncias.observaciones = (txtObservaciones.Text != "") ? Convert.ToString(txtObservaciones.Text.Trim()) : String.Empty;
            if (txtNumero_radicacion.Text != "") vReferncias.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());

            if (idObjeto != "")
            {
                vReferncias.codreferencia = Convert.ToInt64(idObjeto);
                RefernciasServicio.ModificarReferncias(vReferncias, (Usuario)Session["usuario"]);
            }
            else
            {
                vReferncias = RefernciasServicio.CrearReferncias(vReferncias, (Usuario)Session["usuario"]);
                idObjeto = vReferncias.codreferencia.ToString();
            }

            Session[RefernciasServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[RefernciasServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Referncias vReferncias = new Xpinn.FabricaCreditos.Entities.Referncias();
            vReferncias = RefernciasServicio.ConsultarReferncias(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vReferncias.codreferencia != Int64.MinValue)
                txtCodreferencia.Text = HttpUtility.HtmlDecode(vReferncias.codreferencia.ToString().Trim());
            if (vReferncias.cod_persona != Int64.MinValue)
                txtCod_persona.Text = HttpUtility.HtmlDecode(vReferncias.cod_persona.ToString().Trim());
            if (vReferncias.tiporeferencia != Int64.MinValue)
                rblTipoReferencia.SelectedValue = HttpUtility.HtmlDecode(vReferncias.tiporeferencia.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferncias.nombres))
                txtNombres.Text = HttpUtility.HtmlDecode(vReferncias.nombres.ToString().Trim());
            if (vReferncias.codparentesco != Int64.MinValue)
                ddlParentesco.Text = HttpUtility.HtmlDecode(vReferncias.codparentesco.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferncias.direccion))
                direccion.Text = HttpUtility.HtmlDecode(vReferncias.direccion.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferncias.telefono))
                txtTelefono.Text = HttpUtility.HtmlDecode(vReferncias.telefono.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferncias.teloficina))
                txtTeloficina.Text = HttpUtility.HtmlDecode(vReferncias.teloficina.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferncias.celular))
                txtCelular.Text = HttpUtility.HtmlDecode(vReferncias.celular.ToString().Trim());
            if (vReferncias.estado != Int64.MinValue)
                txtEstado.Text = HttpUtility.HtmlDecode(vReferncias.estado.ToString().Trim());
            if (vReferncias.codusuverifica != Int64.MinValue)
                txtCodusuverifica.Text = HttpUtility.HtmlDecode(vReferncias.codusuverifica.ToString().Trim());
            if (vReferncias.fechaverifica != DateTime.MinValue)
                txtFechaverifica.Text = HttpUtility.HtmlDecode(vReferncias.fechaverifica.ToShortDateString());
            if (!string.IsNullOrEmpty(vReferncias.calificacion))
                txtCalificacion.Text = HttpUtility.HtmlDecode(vReferncias.calificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferncias.observaciones))
                txtObservaciones.Text = HttpUtility.HtmlDecode(vReferncias.observaciones.ToString().Trim());
            if (vReferncias.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = HttpUtility.HtmlDecode(vReferncias.numero_radicacion.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RefernciasServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    protected void txtTiporeferencia_TextChanged(object sender, EventArgs e)
    {

    }
}