using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using Xpinn.Util;
using System.Text;
using Xpinn.FabricaCreditos.Entities;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.TiposDocumentoService tipoDocumentoServicio = new Xpinn.FabricaCreditos.Services.TiposDocumentoService();


    String tipo = "";
    Xpinn.FabricaCreditos.Entities.TiposDocumento tipoDocumento = new Xpinn.FabricaCreditos.Entities.TiposDocumento();

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
                Usuario vUsuario = (Usuario)Session["Usuario"];
                Int64 consecutivo = 1;

                tipoDocumento = tipoDocumentoServicio.ConsultarMaxTiposDocumento(vUsuario);
                consecutivo = tipoDocumento.tipo_documento + 1;

                txtTipoDocumento.Text = Convert.ToString(consecutivo);

                LlenarCombos(vUsuario);

                if (Session[tipoDocumentoServicio.CodigoPrograma + ".id"] != null)
                {
                    CtlEditDocument.Visible = false;
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

    void LlenarCombos(Usuario vUsuario)
    {
        List<TipoDocumento> lstTipos = tipoDocumentoServicio.ConsultarTipoDoc(vUsuario);
        ddlTipoDoc.DataSource = lstTipos;
        ddlTipoDoc.DataValueField = "idTipo";
        ddlTipoDoc.DataTextField = "Detalle";
        ddlTipoDoc.DataBind();


    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            //Textos
            var test = CtlEditDocument.DevolverTexto();
            byte[] String = Encoding.ASCII.GetBytes(test);
            Usuario vUsuario = (Usuario)Session["Usuario"];
            tipoDocumento = tipoDocumentoServicio.ConsultarTiposDocumento(Convert.ToInt64(txtTipoDocumento.Text), vUsuario);
            tipoDocumento.tipo_documento = Convert.ToInt64(txtTipoDocumento.Text);
            if (idObjeto != "")
            {
                tipoDocumento.tipo_documento = Convert.ToInt64(txtTipoDocumento.Text);
                tipoDocumento.tipo = ddlTipoDoc.SelectedValue;
                tipoDocumento.es_orden = chkOrdenServicio.Checked == true ? 1 : 0;
                tipoDocumento.Textos = String;
                tipoDocumentoServicio.ModificarTiposDocumento(tipoDocumento, vUsuario);

            }
            else
            {
                tipoDocumento.Textos = String;
                tipoDocumento.tipo = ddlTipoDoc.SelectedValue;
                tipoDocumento.descripcion = Convert.ToString(this.txtDescripcion.Text);
                tipoDocumento.es_orden = chkOrdenServicio.Checked == true ? 1 : 0;
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

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {

            Xpinn.FabricaCreditos.Entities.TiposDocumento vTipoDoc = new Xpinn.FabricaCreditos.Entities.TiposDocumento();
            vTipoDoc = tipoDocumentoServicio.ConsultarTiposDocumento(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            // tipo = Convert.ToString(Session[tipo]);

            if (!string.IsNullOrEmpty(vTipoDoc.tipo.ToString()))
                tipo = HttpUtility.HtmlDecode(vTipoDoc.tipo.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoDoc.tipo_documento.ToString()))
                txtTipoDocumento.Text = HttpUtility.HtmlDecode(vTipoDoc.tipo_documento.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoDoc.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vTipoDoc.descripcion.ToString().Trim());

            if (vTipoDoc.tipo == "1")
                vTipoDoc.tipo = "G";
            else if (vTipoDoc.tipo == "2")
                vTipoDoc.tipo = "R";
            else if (vTipoDoc.tipo == "3")
                vTipoDoc.tipo = "A";
            else if (vTipoDoc.tipo == "4")
                vTipoDoc.tipo = "C";
            chkOrdenServicio.Checked = vTipoDoc.es_orden == 1 ? true : false;
            ddlTipoDoc.SelectedValue = vTipoDoc.tipo;
            ddlTipoDoc_SelectedIndexChanged(ddlTipoDoc, null);

            CtlEditDocument.Visible = true;
            if (vTipoDoc.Textos == null)
            {
                CtlEditDocument.Texto = HttpUtility.HtmlDecode(vTipoDoc.texto.ToString().Trim());
            }
            else
            {
                CtlEditDocument.Texto = Encoding.ASCII.GetString(vTipoDoc.Textos);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoDocumentoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void ddlTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        CtlEditDocument.Visible = true;
        if (ddlTipoDoc.SelectedValue == "R" || ddlTipoDoc.SelectedValue == "A")
        {
            CtlEditDocument.Visible = false;
        }
    }

}