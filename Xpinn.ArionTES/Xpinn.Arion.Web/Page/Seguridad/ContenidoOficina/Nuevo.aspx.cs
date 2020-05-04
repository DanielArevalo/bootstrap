using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Seguridad.Services.ContenidoService contenidoService = new Xpinn.Seguridad.Services.ContenidoService();
    Xpinn.Seguridad.Entities.Contenido content = new Xpinn.Seguridad.Entities.Contenido();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[contenidoService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(contenidoService.CodigoPrograma, "E");
            else
                VisualizarOpciones(contenidoService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnCancelar_Click;
            toolBar.MostrarConsultar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(contenidoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Usuario vUsuario = new Usuario();

                txtCodOpcion.Text = "0";

                if (Session[contenidoService.CodigoPrograma + ".id"] != null)
                {
                    FreeTextBox1.Visible = false;
                    idObjeto = Session[contenidoService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(contenidoService.CodigoPrograma + ".id");
                    txtCodOpcion.Enabled = false;

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
            BOexcepcion.Throw(contenidoService.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario vUsuario = new Usuario();
            content.cod_opcion = Convert.ToInt64(txtCodOpcion.Text);
            content.nombre = txtNombre.Text;
            content.mostrarOficina = chkOficinaVirtual.Checked ? 1 : 0 ;
            content.html = HttpUtility.HtmlDecode(FreeTextBox1.Text.ToString());

            if (content.cod_opcion != 0)
            {
                content = contenidoService.ModificarContenido(content, vUsuario);                
            }
            else
            {
                content = contenidoService.CrearContenido(content, vUsuario);
            }
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(true);
            mvContenido.ActiveViewIndex = 1;
        }
         catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }    
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            content = contenidoService.ConsultarContenido(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            // tipo = Convert.ToString(Session[tipo]);

            if (!string.IsNullOrEmpty(content.cod_opcion.ToString()))
                txtCodOpcion.Text = HttpUtility.HtmlDecode(content.cod_opcion.ToString().Trim());
            if (!string.IsNullOrEmpty(content.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(content.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(content.mostrarOficina.ToString()) && content.mostrarOficina == 1)
                chkOficinaVirtual.Checked = true;
            if (content.html != null)
            {
                FreeTextBox1.Text = HttpUtility.HtmlDecode(content.html.ToString().Trim());
            }
            FreeTextBox1.Visible = true;
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(contenidoService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

}