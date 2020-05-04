using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Asesores.Services.DiligenciaService DiligenciaServicio = new Xpinn.Asesores.Services.DiligenciaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[DiligenciaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(DiligenciaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(DiligenciaServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            txtFecha_diligencia.Text = DateTime.Today.ToString("dd/MM/yyyy");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DiligenciaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboTipo();
                if (Session[DiligenciaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[DiligenciaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(DiligenciaServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    lblCambiarArchivo.Visible = true;
                    chkCambiarArchivo.Visible = true;
                    txtAnexo.Visible = true;
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DiligenciaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();

            //if (idObjeto != "")
            //    vDiligencia = DiligenciaServicio.ConsultarDiligencia(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vDiligencia.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());
            vDiligencia.fecha_diligencia = Convert.ToDateTime(txtFecha_diligencia.Text.Trim());
            vDiligencia.tipo_diligencia = Convert.ToInt64(ddlTipoDiligencia.SelectedValue);
            vDiligencia.atendio = Convert.ToString(txtAtendio.Text.Trim());
            vDiligencia.respuesta = Convert.ToString(txtRespuesta.Text.Trim());
            if (chkAprueba.Checked)
                vDiligencia.acuerdo = 1;
            else
                vDiligencia.acuerdo = 0;

            vDiligencia.fecha_acuerdo = Convert.ToDateTime(txtFecha_acuerdo.Text.Trim());
            vDiligencia.valor_acuerdo = Convert.ToInt64(txtValor_acuerdo.Text.Trim());
            vDiligencia.observacion = Convert.ToString(txtObservacion.Text.Trim());
            //vDiligencia.codigo_usuario_regis = Convert.ToInt64(txtCodigo_usuario_regis.Text.Trim());

            if (idObjeto != "")
            {
                if (chkCambiarArchivo.Checked)
                {
                    vDiligencia.anexo = Upload();
                }
                else
                {
                    vDiligencia.anexo = txtAnexo.Text;
                }
                vDiligencia.codigo_diligencia = Convert.ToInt64(idObjeto);
                DiligenciaServicio.ModificarDiligencia(vDiligencia, (Usuario)Session["usuario"]);
            }
            else
            {
                vDiligencia.anexo = Upload();
                vDiligencia = DiligenciaServicio.CrearDiligencia(vDiligencia, (Usuario)Session["usuario"]);
                idObjeto = vDiligencia.codigo_diligencia.ToString();
            }

            Session[DiligenciaServicio.CodigoPrograma + ".id"] = idObjeto;
            lblCambiarArchivo.Visible = false;
            chkCambiarArchivo.Visible = false;
            txtAnexo.Visible = false;
            Navegar(Pagina.Detalle);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DiligenciaServicio.CodigoPrograma, "btnGuardar_Click", ex);
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
            Session[DiligenciaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();
            vDiligencia = DiligenciaServicio.ConsultarDiligencia(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vDiligencia.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = HttpUtility.HtmlDecode(vDiligencia.numero_radicacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vDiligencia.fecha_diligencia.ToString()))
                txtFecha_diligencia.Text = HttpUtility.HtmlDecode(vDiligencia.fecha_diligencia.ToString("dd/MM/yyyy").Trim());
            if (vDiligencia.tipo_diligencia != Int64.MinValue)
                ddlTipoDiligencia.SelectedValue= HttpUtility.HtmlDecode(vDiligencia.tipo_diligencia.ToString().Trim());
            if (!string.IsNullOrEmpty(vDiligencia.atendio))
                txtAtendio.Text = HttpUtility.HtmlDecode(vDiligencia.atendio.ToString().Trim());
            if (!string.IsNullOrEmpty(vDiligencia.respuesta))
                txtRespuesta.Text = HttpUtility.HtmlDecode(vDiligencia.respuesta.ToString().Trim());
            //if (vDiligencia.acuerdo != Int64.MinValue)
            //    txtAcuerdo.Text = HttpUtility.HtmlDecode(vDiligencia.acuerdo.ToString().Trim());
            if (!string.IsNullOrEmpty(vDiligencia.fecha_acuerdo.ToString()))
                txtFecha_acuerdo.Text = HttpUtility.HtmlDecode(vDiligencia.fecha_acuerdo.ToString("dd/MM/yyyy").Trim());
            if (vDiligencia.valor_acuerdo != Int64.MinValue)
                txtValor_acuerdo.Text = HttpUtility.HtmlDecode(vDiligencia.valor_acuerdo.ToString().Trim());
            if (!string.IsNullOrEmpty(vDiligencia.anexo))
                txtAnexo.Text = HttpUtility.HtmlDecode(vDiligencia.anexo.ToString().Trim());
            if (!string.IsNullOrEmpty(vDiligencia.observacion))
                txtObservacion.Text = HttpUtility.HtmlDecode(vDiligencia.observacion.ToString().Trim());
            //if (vDiligencia.codigo_usuario_regis != Int64.MinValue)
            //    txtCodigo_usuario_regis.Text = HttpUtility.HtmlDecode(vDiligencia.codigo_usuario_regis.ToString().Trim());

            if (vDiligencia.acuerdo != Int64.MinValue)
            {
                if (vDiligencia.acuerdo == 1)
                    chkAprueba.Checked = true;
                else
                    chkAprueba.Checked = false;
            }


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DiligenciaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void LlenarComboTipo()
    {
        TipoDiligenciaService tipoDiligenciaServicio = new TipoDiligenciaService();
        TipoDiligencia tipoDiligencia = new TipoDiligencia();
        ddlTipoDiligencia.DataSource = tipoDiligenciaServicio.ListarTipoDiligencia(tipoDiligencia, (Usuario)Session["usuario"]);
        ddlTipoDiligencia.DataTextField = "descripcion";
        ddlTipoDiligencia.DataValueField = "tipo_diligencia";
        ddlTipoDiligencia.DataBind();
        ddlTipoDiligencia.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    private String Upload()
    {
        string saveDir = @"Page\Asesores\Diligencia\Uploads\";
        string appPath = Request.PhysicalApplicationPath;
        if (FileUpload1.HasFile)
        {
            String fileName = FileUpload1.FileName;
            string savePath = appPath + saveDir + fileName;

            //Server.HtmlEncode(FileUpload1.FileName);

            FileUpload1.SaveAs(savePath);
            return savePath;
            //UploadStatusLabel.Text = "Your file was saved as " + fileName;
        }
        else
        {
            return String.Empty;
            //UploadStatusLabel.Text = "You did not specify a file to upload.";
        }
    }
}