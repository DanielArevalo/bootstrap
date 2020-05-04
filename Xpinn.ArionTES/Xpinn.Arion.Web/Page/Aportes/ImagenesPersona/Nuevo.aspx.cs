using System;
using System.Collections.Generic;
using Xpinn.Aportes.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;
using System.Linq;
using System.IO;
using System.Web.UI;
using Xpinn.Comun.Entities;
using System.Configuration;
using Xpinn.Interfaces.Services;
using Xpinn.Interfaces.Entities;
using Xpinn.Aportes.Entities;

public partial class Nuevo : GlobalWeb
{
    ImagenesService _imagenService = new ImagenesService();
    ParametrizacionProcesoAfilicacionService _paramProceso = new ParametrizacionProcesoAfilicacionService();
    private Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    Usuario _usuario;


    #region Eventos Page PreInit y Load


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_imagenService.CodigoPrograma, "A");
            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            if (Session[AfiliacionServicio.CodigoPrograma + ".id"] != null)
            {
                btnSiguiente.Visible = true;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_imagenService.CodigoPrograma, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];
            VerError("");
            lblNotificacionGuardado.Text = string.Empty;
            lblNotificacionGuardado.Visible = false;

            if (!IsPostBack)
            {
                string cod_persona = (string)Session[_imagenService.CodigoPrograma.ToString() + ".cod_persona"];

                InicializarPagina(cod_persona);


                #region Interfaz WorkManagement


                General parametroHabilitaOperacionesWM = ConsultarParametroGeneral(45);
                // Consultamos que tengamos el parametro habilitado
                if (parametroHabilitaOperacionesWM != null && parametroHabilitaOperacionesWM.valor.Trim() == "1" && cod_persona != null)
                {
                    try
                    {
                        // Se necesita el codigo de tipo de informacion adicional de persona del barCode, si no existe bam
                        InformacionAdicionalServices informacionAdicionalService = new InformacionAdicionalServices();
                        string codigoTipoInformacionPersonalBarCode = ConfigurationManager.AppSettings["CodigoTipoInformacionAdicionalWorkManagement"];

                        // Valido que tenga el codigo de tipo de informacion adicional para el barcode
                        if (!string.IsNullOrWhiteSpace(codigoTipoInformacionPersonalBarCode))
                        {
                            string barCode = informacionAdicionalService.ConsultarInformacionPersonalDeUnaPersona(Convert.ToInt64(cod_persona), Convert.ToInt64(codigoTipoInformacionPersonalBarCode), Usuario);

                            // Si el barcode no existe en financial, procedo a validar que ya exista en el WM
                            if (string.IsNullOrWhiteSpace(barCode))
                            {
                                InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);

                                Persona1Service personaService = new Persona1Service();
                                string identificacion = personaService.ConsultarIdentificacionPersona(Convert.ToInt64(cod_persona), Usuario);

                                Persona1 personaConsultada = interfaz.ConsultarInformacionPersonaPorIdentificacionDeFormularioHistoriaAsociado(identificacion);

                                // Si la persona ya existe en el WM, entonces la creo y prosigo
                                if (personaConsultada != null && !string.IsNullOrWhiteSpace(personaConsultada.barCode))
                                {
                                    InformacionAdicionalServices infoSer = new InformacionAdicionalServices();
                                    InformacionAdicional pAdicional = new InformacionAdicional
                                    {
                                        cod_persona = Convert.ToInt64(cod_persona),
                                        cod_infadicional = Convert.ToInt32(codigoTipoInformacionPersonalBarCode),
                                        valor = personaConsultada.barCode
                                    };

                                    // Creo la informacion adicional para esta persona con el barCode
                                    pAdicional = infoSer.CrearPersona_InfoAdicional(pAdicional, Usuario);
                                }
                                else
                                {
                                    // Si definitivamente no existe ni en Financial ni en el WM, muestro la alerta
                                    VerError("WorkManagement: No se encuentra el codigo del registro del WorkManagement para esta persona, si prosigues no se guardara en el WorkManagement");
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        VerError("WorkManagement: Ocurrio un error al buscar a la persona en el WorkManagement, si prosigues no se guardara en el WorkManagement!.");
                    }
                }


                #endregion


            }
            else
            {
                if (!string.IsNullOrWhiteSpace(hiddenFieldImageData.Value))
                {
                    imgDocumento.ImageUrl = hiddenFieldImageData.Value;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_imagenService.CodigoPrograma, "Page_Load", ex);
        }
    }
    void registrarControl(Int32 cod_proceso, Int64 cod_per)
    {
        Usuario us = new Usuario();
        us = (Usuario)Session["usuario"];

        ParametrizacionProcesoAfiliacion control = new ParametrizacionProcesoAfiliacion();
        control.numero_solicitud = 0;
        control.identificacion = Convert.ToInt64(Session["identificacion"]);
        control.cod_persona = cod_per;
        control.ip_local = us.IP;
        control.cod_proceso = cod_proceso;

        AfiliacionServicio.controlRutaAfiliacion(control, (Usuario)Session["Usuario"]);
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        ImagenesService imagenService = new ImagenesService();
        if (Session[AfiliacionServicio.CodigoPrograma + ".id"] != null)
        {
            string cod_per = Session[AfiliacionServicio.CodigoPrograma + ".id"].ToString();
            Int32 act = Convert.ToInt32(Session[AfiliacionServicio.CodigoPrograma + "last"].ToString());
            String id = Session[AfiliacionServicio.CodigoPrograma + ".id"].ToString();
            /************VARIFICAR DONDE ESTABA ANTES DE LLEGAR ACA***********/
            ParametrizacionProcesoAfiliacion vParam = new ParametrizacionProcesoAfiliacion();
            vParam.lstParametros = (List<ParametrizacionProcesoAfiliacion>)Session["lstParametros"];
            int c = 0;
            foreach (ParametrizacionProcesoAfiliacion redirect in vParam.lstParametros)
            {
                if (redirect.cod_proceso == act)
                    break;
                c++;
            }
            if (c > 0)
                c = c - 1;
            switch (act)
            {
                case 2:
                    Session[AfiliacionServicio.CodigoPrograma + ".id"] = id;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Session[AfiliacionServicio.CodigoPrograma + ".modificar"] = 0;
                    Navegar("../../Aportes/ConfirmaAfiliacion/Lista.aspx");
                    break;
                case 3:
                    Session[AfiliacionServicio.CodigoPrograma + ".id"] = id;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Session[AfiliacionServicio.CodigoPrograma + ".modificar"] = 0;
                    Navegar("../../Aportes/Personas/Tabs/Nuevo.aspx");
                    break;
                case 4:
                    Session["cedula"] = txtIdentificacion.Text;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Navegar("../../Aportes/CuentasAportes/Nuevo.aspx");
                    break;
                case 5:
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = 5;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Navegar("../../Aportes/ImagenesPersona/Nuevo.aspx");
                    break;
                case 6:
                    Session[AfiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Navegar("../../GestionRiesgo/ConsultasOFAC/Nuevo.aspx");
                    break;
                case 7:
                    string codOpcion = "170901";
                    Session["CodOpcion"] = codOpcion;
                    Session[codOpcion.ToString() + ".id"] = cod_per;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Navegar("../../Aportes/ProcesosAfiliacion/Nuevo.aspx");
                    break;
                case 8:
                    string codOpci = "170903";
                    Session["CodOpcion"] = codOpci;
                    Session[codOpci.ToString() + ".id"] = cod_per;
                    Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c].cod_proceso;
                    Session[AfiliacionServicio.CodigoPrograma + "next"] = "lst";
                    Navegar("../../Aportes/ProcesosAfiliacion/Nuevo.aspx");
                    break;
            }
        }
        else
        {
            Session.Remove(imagenService.CodigoPrograma.ToString() + ".cod_persona");
            Session.Remove(AfiliacionServicio.CodigoPrograma + "last");
            Session.Remove(AfiliacionServicio.CodigoPrograma + "next");
            Session.Remove(AfiliacionServicio.CodigoPrograma + ".id");
            Session.Remove("lstParametros");
            Navegar(Pagina.Lista);
        }
    }
    #endregion


    #region Metodos Iniciales (Inicializar Pagina)


    void InicializarPagina(string cod_persona)
    {
        Persona1 persona = ConsultarDatosPersona(cod_persona);

        if (persona == null) return;

        LlenarDatosPersona(persona);

        List<TiposDocumento> documentos = ConsultarTiposDocumentos();

        if (documentos == null || documentos.Count == 0)
        {
            btnGuardarImagen.Visible = false;
            VerError("No hay tipos de documentos al cual guardar");
            return;
        }

        LlenarTiposDocumentos(documentos);

        Imagenes imagen = ConsultarImagen(Convert.ToInt64(cod_persona), Convert.ToInt64(lstBoxTipoDocumentos.SelectedValue));

        if (imagen == null) return;

        LlenarImagenDocumento(imagen);
    }


    private Persona1 ConsultarDatosPersona(string cod_persona)
    {
        try
        {
            Persona1Service personaServicio = new Persona1Service();
            Persona1 persona = new Persona1() { cod_persona = long.Parse(cod_persona), seleccionar = "Cod_persona" };

            return personaServicio.ConsultaDatosPersona(long.Parse(cod_persona), _usuario); ;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar datos de la persona: " + ex.Message);
            return null;
        }
    }


    private void LlenarDatosPersona(Persona1 persona)
    {
        txtIdentificacion.Text = persona.identificacion;
        txtNombres.Text = persona.primer_nombre + " " + persona.segundo_nombre;
        txtApellidos.Text = persona.primer_apellido + " " + persona.segundo_apellido;
        txtDireccion.Text = persona.direccion;
        txtTelefono.Text = persona.telefono;
        txtCodPersona.Text = persona.cod_persona.ToString();
        txtTipoIdentificacion.Text = persona.tipo_identificacion.ToString() + "-" + persona.nomtipo_identificacion;
        txtEstado.Text = persona.estado;
    }


    private List<TiposDocumento> ConsultarTiposDocumentos()
    {
        try
        {
            TiposDocumentoService tipoDocumento = new TiposDocumentoService();
            TiposDocumento documento = new TiposDocumento() { tipo = "A" };

            return tipoDocumento.ListarTiposDocumento(documento, _usuario);
        }
        catch (Exception ex)
        {
            VerError("Error Consultando los tipos de Documentos: " + ex.Message);
            return null;
        }
    }


    private void LlenarTiposDocumentos(List<TiposDocumento> documentos)
    {
        ReflectionHelper reflectionHelper = new ReflectionHelper();

        lstBoxTipoDocumentos.DataTextField = reflectionHelper.GetPropertyName(() => documentos.First().descripcion);
        lstBoxTipoDocumentos.DataValueField = reflectionHelper.GetPropertyName(() => documentos.First().tipo_documento);

        lstBoxTipoDocumentos.DataSource = documentos;
        lstBoxTipoDocumentos.DataBind();
        lstBoxTipoDocumentos.SelectedIndex = 0;
    }


    private Imagenes ConsultarImagen(long cod_persona, long selectedValue)
    {
        try
        {
            Imagenes imagen = new Imagenes() { cod_persona = cod_persona, tipo = selectedValue };
            return _imagenService.ConsultarImageneDocumentosPersona(imagen, _usuario);
        }
        catch (Exception ex)
        {
            VerError("Error al consultar la imagen: " + ex.Message);
            return null;
        }
    }


    void LlenarImagenDocumento(Imagenes imagen)
    {
        if (imagen.imagen != null)
        {
            if (imagen.imagenEsPDF == false)
            {
                string base64 = Convert.ToBase64String(imagen.imagen);

                imgDocumento.ImageUrl = @"data:image/jpeg;base64," + base64;
                hiddenFieldImageData.Value = base64;
                pnlImagen.Visible = true;
                pnlPDF.Visible = false;
            }
            else
            {
                MostrarArchivoEnLiteral(imagen.imagen, Usuario, ltrPDF);
                pnlImagen.Visible = false;
                pnlPDF.Visible = true;
            }
        }
        else
        {
            imgDocumento.ImageUrl = "";
            hiddenFieldImageData.Value = "";
        }

        lblIdImagen.Text = imagen.idimagen.ToString();
    }


    #endregion


    #region Metodos Finales (Guardar Imagen)


    protected void btnGuardarImagen_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            lblNotificacionGuardado.Text = string.Empty;
            lblNotificacionGuardado.Visible = false;

            if (avatarUpload.PostedFile.ContentLength > 0)
            {
                StreamsHelper streamHelper = new StreamsHelper();
                byte[] bytesArrImagen;
                string fileName = string.Empty;
                string extension = string.Empty;

                using (Stream streamImagen = avatarUpload.PostedFile.InputStream)
                {
                    bytesArrImagen = streamHelper.LeerTodosLosBytesDeUnStream(streamImagen);
                    fileName = avatarUpload.PostedFile.FileName;

                    // Hallamos la extension del archivo
                    FileInfo fi = new FileInfo(avatarUpload.PostedFile.FileName);
                    extension = fi.Extension;
                }

                Imagenes imagen = LlenarEntidadImagen(bytesArrImagen);

                if (avatarUpload.PostedFile.ContentType == "application/pdf")
                {
                    imagen.imagenEsPDF = true;
                }

                if (lblIdImagen.Text.ToEnum<Tiene>() == Tiene.No)
                {
                    imagen = _imagenService.CrearImagenesPersona(imagen, _usuario);
                }
                else
                {
                    imagen = _imagenService.ModificarImagenesPersona(imagen, _usuario);
                }

                // Parametro general para habilitar proceso de WM
                General parametroHabilitarOperacionesWM = ConsultarParametroGeneral(45);
                if (parametroHabilitarOperacionesWM != null && parametroHabilitarOperacionesWM.valor.Trim() == "1")
                {
                    try
                    {
                        // Se necesita el codigo de tipo de informacion adicional de persona del barCode, si no existe bam
                        InformacionAdicionalServices informacionAdicionalService = new InformacionAdicionalServices();
                        string codigoTipoInformacionPersonal = ConfigurationManager.AppSettings["CodigoTipoInformacionAdicionalWorkManagement"];

                        if (!string.IsNullOrWhiteSpace(codigoTipoInformacionPersonal))
                        {
                            WorkManagementServices workManagementService = new WorkManagementServices();

                            string barCodePersona = informacionAdicionalService.ConsultarInformacionPersonalDeUnaPersona(Convert.ToInt64(txtCodPersona.Text), Convert.ToInt64(codigoTipoInformacionPersonal), Usuario);

                            if (!string.IsNullOrWhiteSpace(barCodePersona))
                            {
                                InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);

                                WorkFlowFilesDTO file = new WorkFlowFilesDTO
                                {
                                    Base64DataFile = Convert.ToBase64String(bytesArrImagen),
                                    Descripcion = lstBoxTipoDocumentos.SelectedItem.Text,
                                    Extension = extension
                                };

                                bool anexoExitoso = interfaz.AnexarArchivoAFormularioAfiliacion(file, barCodePersona, TipoArchivoWorkManagement.DocumentosAnexoAfiliacion);
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }

                lblNotificacionGuardado.Text = "Guardado Exitoso!";
                lblIdImagen.Text = imagen.idimagen.ToString();
                lblNotificacionGuardado.Visible = true;
                LlenarImagenDocumento(imagen);

            }
        }
        catch (Exception ex)
        {
            VerError("Problemas al cargar la imagen: " + ex.Message);
            lblNotificacionGuardado.Text = "Problemas al Guardar!";
            lblNotificacionGuardado.Visible = true;
        }
    }


    private Imagenes LlenarEntidadImagen(byte[] bytesArrImagen)
    {
        Imagenes imagen = new Imagenes();
        imagen.imagen = bytesArrImagen;
        imagen.cod_persona = Convert.ToInt64(txtCodPersona.Text);
        imagen.fecha = DateTime.Now;
        imagen.tipo_imagen = Convert.ToInt64(lstBoxTipoDocumentos.SelectedValue);
        imagen.tipo_documento = Convert.ToInt32(lstBoxTipoDocumentos.SelectedValue);
        imagen.tipo = imagen.tipo_imagen;
        return imagen;
    }


    #endregion


    #region Metodos Intermedios (Eventos)


    protected void lstBoxTipoDocumentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");
        lblNotificacionGuardado.Text = string.Empty;
        lblNotificacionGuardado.Visible = false;

        Imagenes imagen = ConsultarImagen(Convert.ToInt64(txtCodPersona.Text), Convert.ToInt64(lstBoxTipoDocumentos.SelectedValue));

        if (imagen == null) return;

        LlenarImagenDocumento(imagen);
    }


    #endregion


    //hoja de ruta afilaicion
    protected void btnSiguiente_Click(object sender, ImageClickEventArgs e)
    {
        string cod_per = Session[AfiliacionServicio.CodigoPrograma + ".id"].ToString();
        string nvNext = Session[AfiliacionServicio.CodigoPrograma + "next"].ToString();
        string nvLast = Session[AfiliacionServicio.CodigoPrograma + "last"].ToString();
        ImagenesService imagenService = new ImagenesService();
        ParametrizacionProcesoAfiliacion vParam = new ParametrizacionProcesoAfiliacion();
        vParam.lstParametros = _paramProceso.ListarParametrosProcesoAfiliacion((Usuario)Session["usuario"]).Where(x => x.cod_proceso != 1).ToList();
        bool stop = false;
        int c = -1;
        //DETERMINAR EL ORDEN EN QUE VA EL PROCESO
        int orden = 0;
        if (nvLast != null)
        { 
            ParametrizacionProcesoAfiliacion vParamActual = new ParametrizacionProcesoAfiliacion();
            vParamActual.lstParametros = vParam.lstParametros.Where(x => x.cod_proceso == ConvertirStringToInt32(nvLast)).ToList();
            if (vParamActual.lstParametros.Count > 0)
            { 
                orden = vParamActual.lstParametros[0].orden;
            }
        }
        //CONTROL DE RUTA PARA LA EVALUACIÓN  
        if (nvNext == null || nvNext == "")
            nvNext = Convert.ToString(5);
        registrarControl(Convert.ToInt32(nvNext), Convert.ToInt64(cod_per));
        if (txtEstado.Text != "A")
            _paramProceso.cambiarEstadoAsociado("D", Convert.ToInt64(cod_per), (Usuario)Session["usuario"]);
        foreach (ParametrizacionProcesoAfiliacion redirect in vParam.lstParametros)
        {
            c++;
            if (redirect.cod_proceso != Convert.ToInt32(nvLast) && redirect.cod_proceso != Convert.ToInt32(nvNext) && redirect.orden > orden)
            {
                switch (redirect.cod_proceso)
                {
                    case 3:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            Session[AfiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                            Session[AfiliacionServicio.CodigoPrograma + ".modificar"] = 0;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(4, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../Aportes/Personas/Tabs/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 4:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            Session["cedula"] =  Convert.ToString(Session["identificacion"]);
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(4, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../Aportes/CuentasAportes/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 5:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            Session[imagenService.CodigoPrograma.ToString() + ".cod_persona"] = Session[Usuario.codusuario + "Cod_persona"].ToString();
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(5, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../ImagenesPersona/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 6:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            Session[AfiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(6, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../GestionRiesgo/ConsultasOFAC/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 7:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            Int64 id = Convert.ToInt64(cod_per);
                            string codOpcion = "170901";
                            Session["CodOpcion"] = codOpcion;
                            Session[codOpcion.ToString() + ".id"] = id;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(7, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../AProcesosAfiliacion/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 8:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            Int64 id = Convert.ToInt64(cod_per);
                            string codOpcion = "170902";
                            Session["CodOpcion"] = codOpcion;
                            Session[codOpcion.ToString() + ".id"] = id;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(8, vParam.nombre, Convert.ToInt64(cod_per), redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../AProcesosAfiliacion/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                }
            }
            if (stop) break;
        }
        if (stop == false)
        {
            if (txtEstado.Text != "A")
                _paramProceso.cambiarEstadoAsociado("A", Convert.ToInt64(cod_per), (Usuario)Session["usuario"]);
            Session.Remove(imagenService.CodigoPrograma.ToString() + ".cod_persona");
            Session.Remove(AfiliacionServicio.CodigoPrograma + "last");
            Session.Remove(AfiliacionServicio.CodigoPrograma + "next");
            Session.Remove(AfiliacionServicio.CodigoPrograma + ".id");
            Session.Remove("lstParametros");
            Session.Remove(AfiliacionServicio.CodigoPrograma + ".id");
            Navegar("../../Aportes/Afiliaciones/Lista.aspx");
        }
    }
}