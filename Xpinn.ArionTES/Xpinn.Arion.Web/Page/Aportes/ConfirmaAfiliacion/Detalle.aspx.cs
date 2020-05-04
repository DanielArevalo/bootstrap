using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;

public partial class Detalle : GlobalWeb
{
    AfiliacionServices AfiliacionServicio = new AfiliacionServices();
    ParametrizacionProcesoAfilicacionService _paramProceso = new ParametrizacionProcesoAfilicacionService();
    AfiliacionServices BOActualizar = new AfiliacionServices();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AfiliacionServicio.codigoprogramaConfirmarAfili, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaConfirmarAfili, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session[AfiliacionServicio.codigoprogramaConfirmarAfili + ".id"] != null)
                {
                    idObjeto = Session[AfiliacionServicio.codigoprogramaConfirmarAfili + ".id"].ToString();
                    Session.Remove(AfiliacionServicio.codigoprogramaConfirmarAfili + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaConfirmarAfili, "Page_Load", ex);
        }
    }

    protected void ObtenerDatos(string idObjeto)
    {
        try
        {
            SolicitudPersonaAfi pEntidad = new SolicitudPersonaAfi();
            pEntidad = AfiliacionServicio.ConsultarSolicitudAfiliacion(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            if (pEntidad != null)
            {
                if(pEntidad.id_persona > 0)
                    txtCodigo.Text = Convert.ToString(pEntidad.id_persona);
                if (pEntidad.fecha_creacion != DateTime.MinValue)
                    lblFechaSoli.Text = pEntidad.fecha_creacion.ToShortDateString();
                if (pEntidad.primer_nombre != null || pEntidad.segundo_nombre != null)
                {
                    string nombre = string.Empty;
                    if (pEntidad.primer_nombre != null)
                        nombre += pEntidad.primer_nombre.Trim();
                    if (pEntidad.segundo_nombre != null && (pEntidad.segundo_nombre != pEntidad.primer_nombre))
                        nombre += " " + pEntidad.segundo_nombre.Trim();

                    lblNombres.Text = nombre.Trim();
                }
                if (pEntidad.primer_apellido != null || pEntidad.segundo_apellido != null)
                {
                    string apellidos = string.Empty;
                    if (pEntidad.primer_apellido != null)
                        apellidos += pEntidad.primer_apellido.Trim();
                    if (pEntidad.segundo_apellido != null && (pEntidad.segundo_apellido != pEntidad.primer_apellido))
                        apellidos += " " + pEntidad.segundo_apellido.Trim();

                    lblApellidos.Text = apellidos.Trim();
                }

                lblTipoIdentificacion.Text = pEntidad.nom_tipo_identificacion != null ? pEntidad.nom_tipo_identificacion : "";
                lblIdentificacion.Text = pEntidad.identificacion != null ? pEntidad.identificacion : "";
                if (pEntidad.sexo != null)
                {
                    switch (pEntidad.sexo)
                    {
                        case "M": { lblSexo.Text = "Masculino"; break; }
                        case "F": { lblSexo.Text = "Femenino"; break; }
                        case "O": { lblSexo.Text = "Otro"; break; }
                    }
                }
                lblFechaExp.Text = pEntidad.fecha_expedicion != DateTime.MinValue ? pEntidad.fecha_expedicion.ToShortDateString() : "";
                lblCiudadExp.Text = pEntidad.nom_ciudadExp != null ? pEntidad.nom_ciudadExp.Trim() : "";
                lblEstadoCiv.Text = pEntidad.nom_estadoCivil != null ? pEntidad.nom_estadoCivil.Trim() : "";
                lblFechaNac.Text = pEntidad.fecha_nacimiento != null ? Convert.ToDateTime(pEntidad.fecha_nacimiento).ToShortDateString() : "";
                lblCiudadNac.Text = pEntidad.nom_ciudadNaci != null ? pEntidad.nom_ciudadNaci.Trim() : "";
                if(pEntidad.cabeza_familia != null)
                {
                    lblCabezaFam.Text = Convert.ToInt32(pEntidad.cabeza_familia) == 1 ? "SI" : "NO";
                }
                lblPersonasCargo.Text = pEntidad.personas_cargo.ToString();
                lblNivelAcad.Text = pEntidad.nom_escolaridad != null ? pEntidad.nom_escolaridad.Trim() : "";
                lblProfesion.Text = pEntidad.profesion != null ? pEntidad.profesion.Trim() : "";
                lblDireccion.Text = pEntidad.direccion != null ? pEntidad.direccion.Trim() : "";
                lblCiudad.Text = pEntidad.nom_ciudad != null ? pEntidad.nom_ciudad.Trim() : "";
                lblEmail.Text = pEntidad.email != null ? pEntidad.email.Trim() : "";
                lblBarrio.Text = pEntidad.nom_barrio != null ? pEntidad.nom_barrio.Trim() : "";
                lblTelefono.Text = pEntidad.telefono != null ? pEntidad.telefono.Trim() : "";
                lblCelular.Text = pEntidad.celular != null ? pEntidad.celular.Trim() : "";

                lblEmpresa.Text = pEntidad.empresa != null ? pEntidad.empresa.Trim() : "";
                lblNit.Text = pEntidad.nit != null ? pEntidad.nit.Trim() : "";
                lblTeleLabor.Text = pEntidad.telefono_empresa != null ? pEntidad.telefono_empresa : "";
                lblDireccionEmp.Text = pEntidad.direccion_empresa != null ? pEntidad.direccion_empresa : "";
                lblCiudadEmp.Text = pEntidad.nom_ciudadempresa != null ? pEntidad.nom_ciudadempresa : "";
                lblTipoContrato.Text = pEntidad.nom_tipo_contrato != null ? pEntidad.nom_tipo_contrato : "";
                lblCargo.Text = pEntidad.cargo_contacto != null ? pEntidad.cargo_contacto : "";
                lblFecInicioLab.Text = pEntidad.fecha_inicio != null ? Convert.ToDateTime(pEntidad.fecha_inicio).ToShortDateString() : "";
                lblFecUltLiqui.Text = pEntidad.fecha_ult_liquidacion != null ? Convert.ToDateTime(pEntidad.fecha_ult_liquidacion).ToShortDateString() : "";
                lblSalario.Text = pEntidad.salario != null ? Convert.ToDecimal(pEntidad.salario).ToString() : "0";
                lblOtrosIng.Text = pEntidad.otros_ingresos != null ? Convert.ToDecimal(pEntidad.otros_ingresos).ToString() : "0";
                lblDeduccion.Text = pEntidad.deducciones != null ? Convert.ToDecimal(pEntidad.deducciones).ToString() : "0";
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramaConfirmarAfili, "ObtenerDatos", ex);
        }
    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Session["TIPO"] = "GRABAR";
        ctlMensaje.MostrarMensaje("Desea generar la confirmación de Afiliación de las personas seleccionadas?");
    }
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Usuario pUsuario = (Usuario)Session["Usuario"];
        //OBTENER LOS DATOS POR AFILIAR
        SolicitudPersonaAfi solicitud = new SolicitudPersonaAfi();
        solicitud.id_persona = Convert.ToInt64(txtCodigo.Text);
        //ejecutar metodo de actualizar los datos
        string pError = "";
        Int64 rpta = 0;
        rpta = BOActualizar.ConfirmacionSolicitudAfiliacion(solicitud, ref pError, pUsuario);
        if (rpta == 0 || pError != "")
        {
            if (pError == "")
                VerError("Ocurrio un error al generar la confirmación de las personas seleccionadas.");
            else
                VerError(pError);
            return;
        }
        Session.Remove("TIPO");
        redireccionar(rpta);
    }
    void registrarControl(Int32 cod_proceso, Int64 cod_per)
    {
        Usuario us = new Usuario();
        us = (Usuario)Session["usuario"];

        ParametrizacionProcesoAfiliacion control = new ParametrizacionProcesoAfiliacion();
        control.numero_solicitud = 0;
        control.identificacion = Convert.ToInt64(lblIdentificacion.Text);
        control.cod_persona = cod_per;
        control.ip_local = us.IP;
        control.cod_proceso = cod_proceso;

        AfiliacionServicio.controlRutaAfiliacion(control, (Usuario)Session["Usuario"]);
    }
    protected void redireccionar(Int64 cod_per)
    {
        ParametrizacionProcesoAfiliacion vParam = new ParametrizacionProcesoAfiliacion();
        vParam.lstParametros = _paramProceso.ListarParametrosProcesoAfiliacion((Usuario)Session["usuario"]).Where(x => x.cod_proceso != 1).ToList();
        Session["lstParametros"] = vParam.lstParametros;
        bool stop = false;
        int c = 0;
        //CONTROL DE RUTA PARA LA EVALUACIÓN 
        if (Session[AfiliacionServicio.CodigoPrograma + "next"] == null)
            Session[AfiliacionServicio.CodigoPrograma + "next"] = 3;
        registrarControl(2, cod_per);
        try
        {
            foreach (ParametrizacionProcesoAfiliacion redirect in vParam.lstParametros)
            {
                switch (redirect.cod_proceso)
                {
                    case 3:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            //registrarControl(4, cod_per);
                            Session[AfiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                            Session[AfiliacionServicio.CodigoPrograma + ".modificar"] = 0;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(3, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../Aportes/Personas/Tab/Persona.aspx");
                            stop = true;
                        }
                        break;
                    case 4:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            //registrarControl(4, cod_per);
                            Session["cedula"] = (string)Session["identificacion"].ToString();
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(4, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../CuentasAportes/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 5:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            //registrarControl(5, cod_per);
                            Session[AfiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                            ImagenesService imagenService = new ImagenesService();
                            Session[imagenService.CodigoPrograma.ToString() + ".cod_persona"] = Session[Usuario.codusuario + "Cod_persona"].ToString();
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(5, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../ImagenesPersona/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                    case 6:
                        if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                        {
                            //registrarControl(6, cod_per);
                            Session[AfiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(6, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
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
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(7, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
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
                            Session[AfiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                            Session[AfiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                            AfiliacionServicio.notificarEmail(8, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                            Navegar("../../AProcesosAfiliacion/Nuevo.aspx");
                            stop = true;
                        }
                        break;
                }
                if (stop)
                {
                    _paramProceso.cambiarEstadoAsociado("F", Convert.ToInt64(cod_per), (Usuario)Session["usuario"]);
                    break;
                }
                c++;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Guardado", "alert('" + ex.Message + "');", true);
        }
        if (stop == false)
        {
            // Limpiar variable de sesion. 
            if (Session[Usuario.codusuario + "Cod_persona"] != null)
                Session.Remove(Usuario.codusuario + "Cod_persona");

            Session.Remove(AfiliacionServicio.CodigoPrograma + ".id");
            Navegar("../../Aportes/Afiliaciones/Lista.aspx");
        }

    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


}