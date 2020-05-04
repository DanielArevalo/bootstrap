using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Aportes.Services.FormatoDocumentoServices FormatoDocumentoServicio = new Xpinn.Aportes.Services.FormatoDocumentoServices();
    String tipo = "";
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[FormatoDocumentoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(FormatoDocumentoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(FormatoDocumentoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnCancelar_Click;
            toolBar.MostrarConsultar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FormatoDocumentoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtTipoDocumento.Enabled = false;
            if (!IsPostBack)
            {
                CargarDropDown();
                if (Session[FormatoDocumentoServicio.CodigoPrograma + ".id"] != null)
                {
                    CtlEditDocument.Visible = false;
                    idObjeto = Session[FormatoDocumentoServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(FormatoDocumentoServicio.CodigoPrograma + ".id");
                    txtTipoDocumento.Enabled = false;
                    ObtenerDatos(idObjeto);
                }
                else
                    txtTipoDocumento.Text = FormatoDocumentoServicio.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FormatoDocumentoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void CargarDropDown()
    {
        ddlFormato.Items.Insert(0, new ListItem("Afiliación", "1"));
        ddlFormato.Items.Insert(1, new ListItem("Aprobación Crédito", "2"));
        ddlFormato.Items.Insert(2, new ListItem("Cartas Paz y Salvo", "3"));
        ddlFormato.Items.Insert(2, new ListItem("Afiliación virtual", "4"));

        ddlFormato.SelectedIndex = 0;
        ddlFormato.DataBind();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (txtTipoDocumento.Text == "")
        {
            VerError("Ingrese el codigo del Documento");
            return;
        }
        int pTipoDocumento = Convert.ToInt32(txtTipoDocumento.Text);
        try
        {
            Usuario vUsuario = new Usuario();
            var test = CtlEditDocument.DevolverTexto();
            byte[] String = Encoding.ASCII.GetBytes(test);
            Xpinn.Aportes.Entities.FormatoDocumento tipoDocumento = new Xpinn.Aportes.Entities.FormatoDocumento();
            tipoDocumento = FormatoDocumentoServicio.ConsultarFormatoDocumento(Convert.ToInt64(pTipoDocumento), vUsuario);

            if (ddlFormato.SelectedValue == "1")
                tipoDocumento.nombre_pl = "USP_XPINN_APO_GENDOCU_PERSONA";
            else if (ddlFormato.SelectedValue == "2" || ddlFormato.SelectedValue == "3")
                tipoDocumento.nombre_pl = "USP_XPINN_CRE_GENDOC_VAR_CONS"; //PENDIENTE DE COLOCACION DEL PL QUE CARGARA LOS DATOS
            else if (ddlFormato.SelectedValue == "4")
                tipoDocumento.nombre_pl = "USP_XPINN_APO_GENDOCU_AFILIA";
            tipoDocumento.descripcion = Convert.ToString(this.txtDescripcion.Text.ToUpper());
            tipoDocumento.tipo = ddlFormato.SelectedValue;
            tipoDocumento.texto = null;
            tipoDocumento.Textos = String;


            if (tipoDocumento.cod_documento > 0)
                //MODIFICAR DATOS
                FormatoDocumentoServicio.CrearFormatoDocumentos(tipoDocumento, vUsuario, 2);
            else
                //CREAR DATOS
                FormatoDocumentoServicio.CrearFormatoDocumentos(tipoDocumento, vUsuario, 1);
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

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Aportes.Entities.FormatoDocumento vTipoDoc = new Xpinn.Aportes.Entities.FormatoDocumento();
            vTipoDoc = FormatoDocumentoServicio.ConsultarFormatoDocumento(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            // tipo = Convert.ToString(Session[tipo]);

            if (!string.IsNullOrEmpty(vTipoDoc.tipo.ToString()))
                tipo = HttpUtility.HtmlDecode(vTipoDoc.tipo.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoDoc.cod_documento.ToString()))
                txtTipoDocumento.Text = HttpUtility.HtmlDecode(vTipoDoc.cod_documento.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoDoc.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vTipoDoc.descripcion.ToString().Trim());

            if (vTipoDoc.tipo != null)
                ddlFormato.SelectedValue = vTipoDoc.tipo;

            CtlEditDocument.Visible = true;

            if (vTipoDoc.texto != null)
                CtlEditDocument.Texto = HttpUtility.HtmlDecode(vTipoDoc.texto.ToString().Trim());
            else
                CtlEditDocument.Texto = HttpUtility.HtmlDecode(Encoding.ASCII.GetString(vTipoDoc.Textos));
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FormatoDocumentoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    [WebMethod]
    public static string GrabarTipoDocumento(string pTipoDocumento, string pHTML)
    {
        try
        {
            Usuario vUsuario = new Usuario();
            Xpinn.FabricaCreditos.Services.TiposDocumentoService FormatoDocumentoServicio = new Xpinn.FabricaCreditos.Services.TiposDocumentoService();
            Xpinn.FabricaCreditos.Entities.TiposDocumento tipoDocumento = new Xpinn.FabricaCreditos.Entities.TiposDocumento();
            tipoDocumento = FormatoDocumentoServicio.ConsultarTiposDocumento(Convert.ToInt64(pTipoDocumento), vUsuario);
            tipoDocumento.texto = pHTML;
            FormatoDocumentoServicio.ModificarTiposDocumento(tipoDocumento, vUsuario);
            return "Datos grabados correctamente";
        }
        catch (Exception ex)
        {
            return "Error al grabar los datos " + ex.Message;
        }
    }


}