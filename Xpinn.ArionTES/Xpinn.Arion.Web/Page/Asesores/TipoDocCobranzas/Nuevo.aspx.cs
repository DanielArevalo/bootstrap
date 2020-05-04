using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Asesores.Services.TiposDocCobranzasServices tipoDocumentoServicio = new Xpinn.Asesores.Services.TiposDocCobranzasServices();
    Xpinn.Asesores.Entities.TiposDocCobranzas tipoDocumento = new Xpinn.Asesores.Entities.TiposDocCobranzas();
    private Xpinn.FabricaCreditos.Services.DatosDeDocumentoService datosDeDocumentoService = new Xpinn.FabricaCreditos.Services.DatosDeDocumentoService();
    List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> Variables = new List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento>();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[tipoDocumentoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(tipoDocumentoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(tipoDocumentoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnCancelar_Click;
            toolBar.MostrarConsultar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoDocumentoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Usuario vUsuario = new Usuario();
                Int64 consecutivo = 1;

                tipoDocumento = tipoDocumentoServicio.ConsultarMaxTiposDocumento(vUsuario);
                consecutivo = tipoDocumento.tipo_documento + 1;

                txtTipoDocumento.Text = Convert.ToString(consecutivo);
                //inserta en el select los valores
                Variables = datosDeDocumentoService.ListarVariables((Usuario)Session["usuario"]);
                ddlVariables.DataSource = Variables.OrderBy(x => x.Campo).ToList();
                ddlVariables.Items.Add(new ListItem("Seleccione un item", "0"));
                ddlVariables.DataValueField = "Campo";
                ddlVariables.DataTextField = "Valor";
                ddlVariables.DataBind();
                if (Session[tipoDocumentoServicio.CodigoPrograma + ".id"] != null)
                {
                    FreeTextBox2.Visible = false;
                    idObjeto = Session[tipoDocumentoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(tipoDocumentoServicio.CodigoPrograma + ".id");
                    txtTipoDocumento.Enabled = false;


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
            BOexcepcion.Throw(tipoDocumentoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void ddltipo_documento_OnselectedIndexChanged(object sender, EventArgs e)
    {

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
            tipoDocumento = tipoDocumentoServicio.ConsultarTiposDocumento(Convert.ToInt64(txtTipoDocumento.Text), vUsuario);
            Int64 documento = Convert.ToInt64(txtTipoDocumento.Text);
            tipoDocumento.tipo_documento = documento;

            if (idObjeto != "")
            {
                tipoDocumento.tipo_documento = Convert.ToInt64(txtTipoDocumento.Text);
                tipoDocumento.Textos = Encoding.ASCII.GetBytes(FreeTextBox2.Value);
                tipoDocumento.texto = null;
                tipoDocumentoServicio.ModificarTiposDocumento(tipoDocumento, vUsuario);
            }
            else
            {
                tipoDocumento.Textos = Encoding.ASCII.GetBytes(FreeTextBox2.Value);
                tipoDocumento.texto = null;
                tipoDocumento.descripcion = Convert.ToString(this.txtDescripcion.Text);
                tipoDocumentoServicio.CrearTiposDocumento(tipoDocumento, vUsuario);
            }
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(true);
            mvTipoDopcumento.ActiveViewIndex = 1;
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
            Xpinn.Asesores.Entities.TiposDocCobranzas vTipoDoc = new Xpinn.Asesores.Entities.TiposDocCobranzas();
            vTipoDoc = tipoDocumentoServicio.ConsultarTiposDocumento(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            // tipo = Convert.ToString(Session[tipo]);

            if (!string.IsNullOrEmpty(vTipoDoc.tipo_documento.ToString()))
                txtTipoDocumento.Text = HttpUtility.HtmlDecode(vTipoDoc.tipo_documento.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoDoc.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vTipoDoc.descripcion.ToString().Trim());
            FreeTextBox2.Visible = true;
            FreeTextBox2.Value = vTipoDoc.Textos == null ? HttpUtility.HtmlDecode(vTipoDoc.texto.ToString().Trim()) : Encoding.ASCII.GetString(vTipoDoc.Textos);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoDocumentoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

}