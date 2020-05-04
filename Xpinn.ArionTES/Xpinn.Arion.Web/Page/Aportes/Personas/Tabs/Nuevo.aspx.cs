using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using Xpinn.Asesores.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Util;

public partial class Page_Aportes_Personas_Tabs_Nuevo : GlobalWeb //System.Web.UI.Page
{
    AfiliacionServices _afiliacionServicio = new AfiliacionServices();
    Persona1Service ServicePersona = new Persona1Service();
    ParametrizacionProcesoAfilicacionService _paramProceso = new ParametrizacionProcesoAfilicacionService();
    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            ctlFormatos.eventoClick += btnImpresion_Click;
        }
        catch
        {

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ctlFormatos.Inicializar("1");

            if (Session[_afiliacionServicio.CodigoPrograma + ".id"] != null)
            {
                string id = Session[_afiliacionServicio.CodigoPrograma + ".id"].ToString();
                Session[Usuario.codusuario + "cod_per"] = id;
                cargar_cabecera(id);
            }
            else
            {
                Session[Usuario.codusuario + "cod_per"] = null;
            }
        }

    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        /*******QUITAR VARIABLES DE SESION DE JHOJA DE RUTA*************/
        ImagenesService imagenService = new ImagenesService();
        Session.Remove(imagenService.CodigoPrograma.ToString() + ".cod_persona");
        Session.Remove(_afiliacionServicio.CodigoPrograma + "last");
        Session.Remove(_afiliacionServicio.CodigoPrograma + "next");
        Session.Remove(_afiliacionServicio.CodigoPrograma + ".id");
        Session.Remove(_afiliacionServicio.CodigoPrograma + ".modificar");
        Session.Remove("lstParametros");

        EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
        if (Session["ocupacion"] != null) { Session.Remove("ocupacion"); }
        if (Session[serviceEstadoCuenta.CodigoPrograma + ".id"] != null)
        {
            Session.Remove(serviceEstadoCuenta.CodigoPrograma + ".id");
            Session.Remove("Persona");
            Session.Remove(Usuario.codusuario + "cod_per");
            Navegar("../../../Asesores/EstadoCuenta/Detalle.aspx");
        }
        else
        {
            Session.Remove("Persona");
            Session.Remove(Usuario.codusuario + "cod_per");
            Navegar("../../Afiliaciones/Lista.aspx");
        }

    }
    protected void btnImpresion_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abc", "javascript: btnImpresion2.click()", true);
    }
    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        ctlFormatos.lblErrorText = "";
        if (ctlFormatos.ddlFormatosItem != null)
            ctlFormatos.ddlFormatosIndex = 0;
        ctlFormatos.MostrarControl();
    }
    protected void btnImpresion2_Click(object sender, EventArgs e)
    {
        try
        {
            // ELIMINANDO ARCHIVOS GENERADOS
            try
            {
                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Documentos\\"));
                foreach (string ficheroActual in ficherosCarpeta)
                    File.Delete(ficheroActual);
            }
            catch
            { }
            string pRuta = "~/Page/Aportes/Personas/Documentos/";
            string pVariable = (string)Session[Usuario.codusuario + "cod_per"];
            ctlFormatos.ImprimirFormato(pVariable, pRuta);

            //Descargando el Archivo PDF
            string cNombreDeArchivo = pVariable.Trim() + "_" + ctlFormatos.ddlFormatosValue + ".pdf";
            string cRutaLocalDeArchivoPDF = Server.MapPath("..\\Documentos\\" + cNombreDeArchivo);

            if (GlobalWeb.bMostrarPDF == true)
            {
                // Copiar el archivo a una ruta local
                try
                {
                    FileStream archivo = new FileStream(cRutaLocalDeArchivoPDF, FileMode.Open, FileAccess.Read);
                    FileInfo file = new FileInfo(cRutaLocalDeArchivoPDF);
                    Response.Clear();
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + cNombreDeArchivo);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/pdf";
                    Response.TransmitFile(file.FullName);
                    Response.End();
                }
                catch (Exception ex)
                {
                    ctlFormatos.lblErrorText = ex.Message;
                    ctlFormatos.lblErrorIsVisible = true;
                }
            }
            else
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = false;
                Response.AddHeader("Content-Disposition", "attachment;filename=" + cNombreDeArchivo);
                Response.ContentType = "application/octet-stream";
                Response.Flush();
                Response.WriteFile(cRutaLocalDeArchivoPDF);
                Response.End();
            }
            //RegistrarPostBack();
            //Response.Clear();
        }
        catch (Exception ex)
        {
            ctlFormatos.lblErrorIsVisible = true;
            ctlFormatos.lblErrorText = ex.Message;
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        }
    }
    protected void cargar_cabecera(string cod_persona)
    {
        if (cod_persona != null && cod_persona != "")
        {
            Int64 cod_per = Convert.ToInt64(cod_persona);
            Persona1 Entidad = new Persona1();
            Entidad.cod_persona = cod_per;
            Entidad.seleccionar = "Cod_persona";
            Entidad.soloPersona = 1;
            Entidad = ServicePersona.ConsultarPersona1Param(Entidad, (Usuario)Session["usuario"]);

            //carga la informacion
            lblcodpersona.Text += Entidad.cod_persona;
            lblidentificacion.Text += Entidad.identificacion;
            lblnombre.Text += Entidad.nombreCompleto;
            lblcodpersona.Visible = true;
            lblidentificacion.Visible = true;
            lblnombre.Visible = true;
        }
        else
        {
            lblcodpersona.Visible = false;
            lblidentificacion.Visible = false;
            lblnombre.Visible = false;
        }
    }
    void registrarControl(Int32 cod_proceso, Int64 cod_per)
    {
        try
        {
            Usuario us = new Usuario();
            us = (Usuario)Session["usuario"];

            ParametrizacionProcesoAfiliacion control = new ParametrizacionProcesoAfiliacion();
            control.numero_solicitud = 0;
            control.identificacion = Convert.ToInt64(Session["identificacion"]);
            control.cod_persona = cod_per;
            control.ip_local = us.IP;
            control.cod_proceso = cod_proceso;

            _afiliacionServicio.controlRutaAfiliacion(control, (Usuario)Session["Usuario"]);
        }
        catch
        { }
    }

    protected void redireccionar(object sender, EventArgs e)
    {
        bool redireccionImagen = false;
        bool peps = false;
        bool Asociado = false;
        long cod_per = 0;
        if (Session["peps"] != null)
        {
            peps = (bool)Session["peps"];
        }
        if (Session["Asociado"] != null)
        {
            Asociado = (bool)Session["Asociado"];
        }
        if (Session[Usuario.codusuario + "cod_per"].ToString() != null)
        {
            cod_per = Convert.ToInt64(Session[Usuario.codusuario + "cod_per"].ToString());
        }
        if (Session["imagen"] != null)
        {
            redireccionImagen = (bool)Session["imagen"];
        }
       
        ParametrizacionProcesoAfiliacion vParam = new ParametrizacionProcesoAfiliacion();
        vParam.lstParametros = _paramProceso.ListarParametrosProcesoAfiliacion((Usuario)Session["usuario"]).Where(x => x.cod_proceso != 1 && x.cod_proceso != 3).ToList();
        Session["lstParametros"] = vParam.lstParametros;
        bool stop = false;
        int c = 0;

        #region control afiliación workflow
        //CONTROL DE RUTA PARA LA EVALUACIÓN 
        if (Session[_afiliacionServicio.CodigoPrograma + "next"] == null)        
            Session[_afiliacionServicio.CodigoPrograma + "next"] = 3;
        registrarControl(3, cod_per);
        foreach (ParametrizacionProcesoAfiliacion redirect in vParam.lstParametros)
        {
            #region ir al proceso
            switch (redirect.cod_proceso)
            {
                case 4:
                    if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                    {
                        //registrarControl(4, cod_per);
                        Session["cedula"] = Convert.ToString(Session["identificacion"]);
                        Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c-1].cod_proceso;
                        Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                        _afiliacionServicio.notificarEmail(4, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                        Navegar("../../CuentasAportes/Nuevo.aspx");
                        stop = true;
                    }
                    break;
                case 5:
                    if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                    {
                        //registrarControl(5, cod_per);
                        Session[_afiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                        ImagenesService imagenService = new ImagenesService();
                        Session[imagenService.CodigoPrograma.ToString() + ".cod_persona"] = Session[Usuario.codusuario + "Cod_persona"].ToString();
                        Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                        Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                        _afiliacionServicio.notificarEmail(5, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                        Navegar("../../ImagenesPersona/Nuevo.aspx");
                        stop = true;
                    }
                    break;
                case 6:
                    if (Convert.ToBoolean(redirect.requerido) /*&& !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"])*/)
                    {                     
                        //registrarControl(6, cod_per);
                        Session[_afiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                        Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                        Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                        _afiliacionServicio.notificarEmail(6, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                        Navegar("../../../GestionRiesgo/ConsultasOFAC/Nuevo.aspx");
                        stop = true;            
                    }
                    break;
                case 7:
                    if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                    {
                        //registrarControl(7, cod_per);
                        Int64 id = cod_per;
                        string codOpcion = "170901";
                        Session["CodOpcion"] = codOpcion;
                        Session[codOpcion.ToString() + ".id"] = id;
                        Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                        Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                        _afiliacionServicio.notificarEmail(7, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                        Navegar("../../AProcesosAfiliacion/Nuevo.aspx");
                        stop = true;
                    }
                    break;
                case 8:
                    if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                    {
                        //registrarControl(8, cod_per);
                        Int64 id = cod_per;
                        string codOpcion = "170902";
                        Session["CodOpcion"] = codOpcion;
                        Session[codOpcion.ToString() + ".id"] = id;
                        Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                        Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                        _afiliacionServicio.notificarEmail(8, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                        Navegar("../../AProcesosAfiliacion/Nuevo.aspx");
                        stop = true;
                    }
                    break;
            }
            #endregion
            if (stop)
            {
            //    _paramProceso.cambiarEstadoAsociado("F", Convert.ToInt64(cod_per), (Usuario)Session["usuario"]);
                break;
            }
            c++;
        }
        #endregion

        if (stop == false)
        {
            // Limpiar variable de sesion. 
            if (Session[Usuario.codusuario + "Cod_persona"] != null)
                Session.Remove(Usuario.codusuario + "Cod_persona");

            Session.Remove(_afiliacionServicio.CodigoPrograma + ".id");
            Navegar("../../../Aportes/Afiliaciones/Lista.aspx");
        }       
    }
    

}