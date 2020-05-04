using System;
using System.Collections.Generic;
using System.Web;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using System.Text;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.TiposDocumentoService tipoDocumentoServicio = new Xpinn.FabricaCreditos.Services.TiposDocumentoService();
    Xpinn.FabricaCreditos.Entities.TiposDocumento tipoDocumento = new Xpinn.FabricaCreditos.Entities.TiposDocumento();

    private static long CodigoTipoDocumento { get; set; }

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(tipoDocumentoServicio.CodigoPrograma, "E");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
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
                List<TiposDocumento> lsTipo = tipoDocumentoServicio.ConsultarTiposDocumento("C", (Usuario)Session["usuario"]);
                if (lsTipo.Count == 0)
                    throw new Exception("No hay ningún tipo de documento registrado para el reporte de CDATs");
                else
                    CodigoTipoDocumento = lsTipo[0].tipo_documento;
                ObtenerDatos(CodigoTipoDocumento);
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {

        VerError("");
        try
        {
            Xpinn.FabricaCreditos.Services.TiposDocumentoService tipoDocumentoServicio = new Xpinn.FabricaCreditos.Services.TiposDocumentoService();
            Xpinn.FabricaCreditos.Entities.TiposDocumento tipoDocumento = tipoDocumentoServicio.ConsultarTiposDocumento(CodigoTipoDocumento, (Usuario)Session["usuario"]);
            tipoDocumento.texto = HttpUtility.HtmlDecode(ftxReporte.Value);
            tipoDocumentoServicio.ModificarTiposDocumento(tipoDocumento, (Usuario)Session["usuario"]);

            Site toolBar = (Site)this.Master;
            mvTipoDopcumento.ActiveViewIndex = 1;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void ObtenerDatos(long pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.TiposDocumento vTipoDoc = new Xpinn.FabricaCreditos.Entities.TiposDocumento();
            vTipoDoc = tipoDocumentoServicio.ConsultarTiposDocumento(CodigoTipoDocumento, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vTipoDoc.texto))
                ftxReporte.Value = HttpUtility.HtmlDecode(vTipoDoc.texto);
            else
                ftxReporte.Value = HttpUtility.HtmlDecode(Encoding.ASCII.GetString(vTipoDoc.Textos));
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tipoDocumentoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    //[WebMethod]
    //public static string GrabarTipoDocumento(string pTipoDocumento, string pHTML)
    //{
    //    try
    //    {
    //        Usuario vUsuario = new Usuario();
    //        Xpinn.FabricaCreditos.Services.TiposDocumentoService tipoDocumentoServicio = new Xpinn.FabricaCreditos.Services.TiposDocumentoService();
    //        Xpinn.FabricaCreditos.Entities.TiposDocumento tipoDocumento = new Xpinn.FabricaCreditos.Entities.TiposDocumento();
    //        tipoDocumento.tipo_documento = CodigoTipoDocumento;
    //        tipoDocumento.texto = pHTML;
    //        tipoDocumentoServicio.ModificarTiposDocumento(tipoDocumento, vUsuario);
    //        return "Datos grabados correctamente";
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error al grabar los datos " + ex.Message;
    //    }
    //}

}