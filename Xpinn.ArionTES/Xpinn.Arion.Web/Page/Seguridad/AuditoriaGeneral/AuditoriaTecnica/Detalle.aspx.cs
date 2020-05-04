using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Seguridad.Services;
using Xpinn.Util;

public partial class Detalle : GlobalWeb
{
    AuditoriaStoredProceduresService _auditoriaService = new AuditoriaStoredProceduresService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_auditoriaService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += (obj, evt) => Navegar(Pagina.Lista);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_auditoriaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                idObjeto = Session[_auditoriaService.CodigoPrograma + ".id"] as string;

                InicializarPagina(idObjeto);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_auditoriaService.CodigoPrograma, "Page_Load", ex);
        }
    }

    void InicializarPagina(string idObjeto)
    {
        AuditoriaStoredProcedures auditoria = _auditoriaService.ConsultarAuditoriaStoredProcedures(Convert.ToInt64(idObjeto), Usuario);

        txtCodigoAuditoria.Text = auditoria.consecutivo.ToString();
        txtFecha.Text = auditoria.fechaejecucion.ToShortDateString();
        txtCodigoUsuario.Text = auditoria.codigousuario.ToString();
        txtUsuario.Text = auditoria.nombreusuario;
        txtCodigoOpcion.Text = auditoria.codigoOpcion.ToString();
        txtOpcion.Text = auditoria.nombre_opcion;
        txtExitoso.Text = auditoria.exitoso.ToString();
        txtNombreSp.Text = auditoria.nombresp;
        txtMensajeError.Text = auditoria.mensaje_error;

        if (!string.IsNullOrWhiteSpace(auditoria.informacionenviada))
        {
            List<AuditoriaParametro> listaParametros = JsonConvert.DeserializeObject<List<AuditoriaParametro>>(auditoria.informacionenviada);

            gvParametros.DataSource = listaParametros;
            gvParametros.DataBind();
        }
    }
}