using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Web.UI.HtmlControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Asesores.Services;

public partial class Nuevo : GlobalWeb
{
    Xpinn.Contabilidad.Services.TerceroService DatosClienteServicio = new Xpinn.Contabilidad.Services.TerceroService();    
     
    private void Page_PreInit(object sender, EventArgs e)
    {        
        try
        {
            if (Session[DatosClienteServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(DatosClienteServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(DatosClienteServicio.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;            
            toolBar.eventoCancelar += btnCancelar_Click;            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.CodigoPrograma + "A", "Page_PreInit", ex);
        }        
    }

    /// <summary>
    /// Cargar información apenas se ingresa a la opción
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {       
        try
        {
       
            Usuario usuap = (Usuario)Session["usuario"];
            if (!IsPostBack)
            {
                // Llenar información de los combos o listas desplegables                
                rblTipoVivienda.SelectedValue = "P";
                validarArriendo();
                CargarListas();

                // Cargar el código de la persona si se paso desde la consulta de datacredito
                if (Session[DatosClienteServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[DatosClienteServicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                    Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
                    if (Session[TerceroServicio.CodigoPrograma + ".modificar"].ToString() == "1")
                    {
                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarGuardar(false);
                        DeshabilitarObjetosPantalla(panelDatos);
                    }
                    txtCod_oficina.Enabled = false;
                }
                else
                {
                    idObjeto = "";
                    Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
                    txtCod_persona.Text = TerceroServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]).ToString();
                }
            }
            
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
                
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar("../Terceros/Lista.aspx");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        Usuario pUsuario = new Usuario();
        UsuarioAtribucionesService atribuciones = new UsuarioAtribucionesService();
        Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones atrusuarios = new Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones();
        pUsuario = (Usuario)Session["Usuario"];
        try
        {
            Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
       



            vPersona1.cod_persona = Convert.ToInt64(pIdObjeto);
            vPersona1.seleccionar = "Cod_persona";
            vPersona1.noTraerHuella = 1;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);

            if (vPersona1.nombre != "errordedatos")
            {
                if (vPersona1.cod_persona != Int64.MinValue)
                    txtCod_persona.Text = HttpUtility.HtmlDecode(vPersona1.cod_persona.ToString().Trim());                                   
                if (!string.IsNullOrEmpty(vPersona1.dirCorrespondencia))
                {
                    try
                    {
                        txtDirCorrespondencia.Text = HttpUtility.HtmlDecode(vPersona1.dirCorrespondencia.ToString().Trim());
                        if (txtDirCorrespondencia.Text == "")
                            txtDirCorrespondencia.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
                    }
                    catch
                    {
                        VerError("La dirección de correspondencia esta errada, no esta en el formato correcto");
                    }
                }
                if (vPersona1.barrioCorrespondencia != Int64.MinValue && vPersona1.barrioCorrespondencia != null)
                {
                    try
                    {
                        ddlBarrioCorrespondencia.SelectedValue = HttpUtility.HtmlDecode(vPersona1.barrioCorrespondencia.ToString().Trim());
                    }
                    catch
                    {
                        ddlBarrioCorrespondencia.SelectedValue = ddlBarrioCorrespondencia.SelectedValue;
                    }
                }
                if (!string.IsNullOrEmpty(vPersona1.telCorrespondencia))
                    txtTelCorrespondencia.Text = HttpUtility.HtmlDecode(vPersona1.telCorrespondencia.ToString().Trim());
                try
                {
                    if (vPersona1.ciuCorrespondencia != Int64.MinValue && vPersona1.ciuCorrespondencia != null && vPersona1.ciuCorrespondencia != -1)
                        ddlCiuCorrespondencia.SelectedValue = HttpUtility.HtmlDecode(vPersona1.ciuCorrespondencia.ToString().Trim());
                }
                catch { }
                if (!string.IsNullOrEmpty(vPersona1.tipo_persona))
                    if (string.Equals(vPersona1.tipo_persona, 'N'))
                        rblTipo_persona.SelectedValue = HttpUtility.HtmlDecode("Natural");
                if (string.Equals(vPersona1.tipo_persona, 'J'))
                    rblTipo_persona.SelectedValue = HttpUtility.HtmlDecode("Jurídica");
                if (!string.IsNullOrEmpty(vPersona1.identificacion))
                {                    
                    txtIdentificacionE.Text = HttpUtility.HtmlDecode(vPersona1.identificacion.ToString().Trim());
                }
                if (vPersona1.digito_verificacion != Int64.MinValue)
                    txtDigito_verificacion.Text = HttpUtility.HtmlDecode(vPersona1.digito_verificacion.ToString().Trim());
                else
                    txtDigito_verificacion.Text = "";
                if (vPersona1.tipo_identificacion != Int64.MinValue)
                    ddlTipoE.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_identificacion.ToString().Trim());
                if (vPersona1.fechaexpedicion != DateTime.MinValue &&vPersona1.fechaexpedicion != null)
                    txtFechaexpedicion.Text = HttpUtility.HtmlDecode(vPersona1.fechaexpedicion.Value.ToShortDateString());
                else
                    txtFechaexpedicion.Text = "";
                if (vPersona1.codciudadexpedicion != Int64.MinValue && vPersona1.codciudadexpedicion != null && vPersona1.codciudadexpedicion != -1 && vPersona1.codciudadexpedicion != 0)
                    ddlLugarExpedicion.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadexpedicion.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.sexo) && !string.Equals(vPersona1.sexo.ToString().Trim(), ""))
                {
                    try
                    {
                        rblSexo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.sexo.ToString().Trim());
                    }
                    catch
                    {
                        rblSexo.SelectedValue = rblSexo.SelectedValue;
                    }
                }
                atrusuarios.codusuario = pUsuario.codusuario;
                List<Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones> atrusuario = atribuciones.ListarUsuarioAtribuciones(atrusuarios, (Usuario)Session["usuario"]);
                foreach (Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones item in atrusuario)
                {
                    if (item.tipoatribucion == 3 && item.activo == 1)
                    {
                        txtPrimer_nombreE.Enabled= true;
                        txtSegundo_nombreE.Enabled = true;
                        txtPrimer_apellidoE.Enabled = true;
                        txtSegundo_apellidoE.Enabled = true;
                        break;
                    }
                    else
                    {
                        txtPrimer_nombreE.Enabled = false;
                        txtSegundo_nombreE.Enabled = false;
                        txtPrimer_apellidoE.Enabled = false;
                        txtSegundo_apellidoE.Enabled = false;
                    }
                }

                if (!string.IsNullOrEmpty(vPersona1.primer_nombre))
                    txtPrimer_nombreE.Text = HttpUtility.HtmlDecode(vPersona1.primer_nombre.ToString().Trim());
                else
                    txtPrimer_nombreE.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.segundo_nombre))
                    txtSegundo_nombreE.Text = HttpUtility.HtmlDecode(vPersona1.segundo_nombre.ToString().Trim());
                else
                    txtSegundo_nombreE.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.primer_apellido))
                    txtPrimer_apellidoE.Text = HttpUtility.HtmlDecode(vPersona1.primer_apellido.ToString().Trim());
                else
                    txtPrimer_apellidoE.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.segundo_apellido))
                    txtSegundo_apellidoE.Text = HttpUtility.HtmlDecode(vPersona1.segundo_apellido.ToString().Trim());
                else
                    txtSegundo_apellidoE.Text = "";
                if (vPersona1.razon_social == null && Session["Negocio"] != null)
                    vPersona1.razon_social = Session["Negocio"].ToString();
                if (vPersona1.fechanacimiento != DateTime.MinValue && vPersona1.fechanacimiento != null)
                {
                    txtFechanacimiento.Text = HttpUtility.HtmlDecode(vPersona1.fechanacimiento.Value.ToShortDateString());
                    txtEdadCliente.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtFechanacimiento.Text)));
                }
                else
                {
                    txtFechanacimiento.Text = "";
                    txtEdadCliente.Text = "";
                }
                if (vPersona1.codciudadnacimiento != Int64.MinValue && vPersona1.codciudadnacimiento != null && vPersona1.codciudadnacimiento.ToString().Trim() != "" && vPersona1.codciudadnacimiento != 0)
                    ddlLugarNacimiento.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadnacimiento.ToString().Trim());
                if (vPersona1.codestadocivil != Int64.MinValue && vPersona1.codestadocivil.ToString().Trim() != "")
                {
                    try
                    {
                        ddlEstadoCivil.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codestadocivil.ToString().Trim());
                    }
                    catch
                    {
                        ddlEstadoCivil.SelectedValue = ddlEstadoCivil.SelectedValue;
                    }
                }

                if (vPersona1.codescolaridad != Int64.MinValue && vPersona1.codescolaridad.ToString().Trim() != "")
                {
                    try
                    {
                        ddlNivelEscolaridad.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codescolaridad.ToString().Trim());
                    }
                    catch
                    {
                        ddlNivelEscolaridad.SelectedValue = ddlNivelEscolaridad.SelectedValue;
                    }
                }

                if (vPersona1.codactividadStr != null)
                {
                    if (vPersona1.codactividadStr.ToString().Trim() != "")
                    {
                        try
                        {
                            ddlActividadE.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codactividadStr.ToString().Trim());
                        }
                        catch
                        {
                            ddlActividadE.SelectedValue = ddlActividadE.SelectedValue;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(vPersona1.direccion))
                {
                    try
                    {
                        txtDireccionE.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
                    }
                    catch
                    {
                        VerError("El formato de la dirección no corresponde");
                    }
                }
                else
                {
                    txtDireccionE.Text = "";
                }
                if (!string.IsNullOrEmpty(vPersona1.telefono))
                    txtTelefonoE.Text = HttpUtility.HtmlDecode(vPersona1.telefono.ToString().Trim());
                else
                    txtTelefonoE.Text = "";
                if (vPersona1.antiguedadlugar != Int64.MinValue)
                    txtAntiguedadlugar.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugar.ToString().Trim());
                else
                    txtAntiguedadlugar.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.tipovivienda) && !string.Equals(vPersona1.tipovivienda.ToString().Trim(), "0"))
                {
                    if (vPersona1.tipovivienda != "P" && vPersona1.tipovivienda != "A" && vPersona1.tipovivienda != "F")
                        vPersona1.tipovivienda = "P";
                    rblTipoVivienda.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipovivienda.ToString().Trim());
                }
                if (!string.IsNullOrEmpty(vPersona1.arrendador))
                    txtArrendador.Text = HttpUtility.HtmlDecode(vPersona1.arrendador.ToString().Trim());
                else
                    txtArrendador.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.telefonoarrendador))
                    txtTelefonoarrendador.Text = HttpUtility.HtmlDecode(vPersona1.telefonoarrendador.ToString().Trim());
                else
                    txtTelefonoarrendador.Text = "";
                if (vPersona1.ValorArriendo != Int64.MinValue)
                    txtValorArriendo.Text = HttpUtility.HtmlDecode(vPersona1.ValorArriendo.ToString().Trim());
                else
                    txtValorArriendo.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.celular))
                    txtCelular.Text = HttpUtility.HtmlDecode(vPersona1.celular.ToString().Trim());
                else
                    txtCelular.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.email))
                    txtEmail.Text = HttpUtility.HtmlDecode(vPersona1.email.ToString().Trim());
                else
                    txtEmail.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.empresa))
                    txtEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.empresa.ToString().Trim());
                else
                    txtEmpresa.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.telefonoempresa))
                    txtTelefonoempresa.Text = HttpUtility.HtmlDecode(vPersona1.telefonoempresa.ToString().Trim());
                else
                    txtTelefonoempresa.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.direccionempresa))
                {
                    try
                    {
                        txtDireccionEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.direccionempresa.ToString().Trim());
                        if (vPersona1.direccionempresa == "" || vPersona1.direccionempresa == "0")
                            txtDireccionEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
                    }
                    catch
                    {
                        VerError("El formato de dirección de la empresa no corresponde");
                    }
                }
                else
                    txtDireccionEmpresa.Text = "";
                if (vPersona1.antiguedadlugarempresa != Int64.MinValue)
                    txtAntiguedadlugarEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugarempresa.ToString().Trim());
                else
                    txtAntiguedadlugarEmpresa.Text = "";
                if (vPersona1.codcargo != Int64.MinValue)
                    ddlCargo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codcargo.ToString().Trim());
                else
                    ddlCargo.SelectedItem.Value = "0";
                if (vPersona1.codtipocontrato != Int64.MinValue)
                    ddlTipoContrato.Text = HttpUtility.HtmlDecode(vPersona1.codtipocontrato.ToString().Trim());
                else
                    ddlTipoContrato.SelectedItem.Value = "0";
                if (vPersona1.cod_asesor != Int64.MinValue)
                    txtCod_asesor.Text = HttpUtility.HtmlDecode(vPersona1.cod_asesor.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.residente) && !string.Equals(vPersona1.residente.ToString().Trim(), "0"))
                    try
                    {
                        rblResidente.Text = HttpUtility.HtmlDecode(vPersona1.residente.ToString().Trim());
                    }
                    catch
                    {
                        VerError("La información de si es residente es incorrecta");
                    }
                if (vPersona1.fecha_residencia != DateTime.MinValue)
                    txtFecha_residencia.Text = HttpUtility.HtmlDecode(vPersona1.fecha_residencia.ToShortDateString());
                else
                    txtFecha_residencia.Text = "";
                if (vPersona1.cod_oficina != Int64.MinValue)
                    txtCod_oficina.SelectedValue = HttpUtility.HtmlDecode(vPersona1.cod_oficina.ToString().Trim());
                else
                    txtCod_oficina.SelectedValue = "";
                if (!string.IsNullOrEmpty(vPersona1.tratamiento))
                    txtTratamiento.Text = HttpUtility.HtmlDecode(vPersona1.tratamiento.ToString().Trim());
                else
                    txtTratamiento.Text = "";
                ddlActividadE0.SelectedValue = vPersona1.ActividadEconomicaEmpresa.ToString();
                ddlCiu0.SelectedValue = vPersona1.ciudad.ToString();
                ddlparentesco.SelectedValue = vPersona1.relacionEmpleadosEmprender.ToString();
                txtTelCell0.Text = vPersona1.CelularEmpresa;
                txtProfecion.Text = vPersona1.profecion;
                txtEstrato.Text = vPersona1.Estrato.ToString();
                txtPersonasCargo.Text = vPersona1.PersonasAcargo.ToString();

                validarArriendo();                
                CalcularEdad();

            }
            else
                VerError("Error de datos");
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }


    }


    /// <summary>
    /// Determinar la edad de la persona con base en la fecha de nacimiento
    /// </summary>
    /// <param name="birthDate"></param>
    /// <returns></returns>
    public static int GetAge(DateTime birthDate)
    {
        return (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.242199);
    }


    /// <summary>
    /// Cargar información de las listas desplegables
    /// </summary>
    private void CargarListas()
    {
        try
        {
            String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();

            ListaSolicitada = "Barrio";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlBarrioCorrespondencia.DataSource = lstDatosSolicitud;
            ddlBarrioCorrespondencia.DataTextField = "ListaDescripcion";
            ddlBarrioCorrespondencia.DataValueField = "ListaId";
            ddlBarrioCorrespondencia.DataBind();

            ListaSolicitada = "TipoIdentificacion";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlTipoE.DataSource = lstDatosSolicitud;
            ddlTipoE.DataTextField = "ListaDescripcion";
            ddlTipoE.DataValueField = "ListaId";
            ddlTipoE.DataBind();

            ListaSolicitada = "Ciudades";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlLugarExpedicion.DataSource = lstDatosSolicitud;
            ddlLugarNacimiento.DataSource = lstDatosSolicitud;
            ddlCiuCorrespondencia.DataSource = lstDatosSolicitud;
            ddlCiu0.DataSource = lstDatosSolicitud;

            ddlLugarExpedicion.DataTextField = "ListaDescripcion";
            ddlLugarNacimiento.DataTextField = "ListaDescripcion";
            ddlCiuCorrespondencia.DataTextField = "ListaDescripcion";
            ddlCiu0.DataTextField = "ListaDescripcion";

            ddlLugarExpedicion.DataValueField = "ListaIdStr";
            ddlLugarNacimiento.DataValueField = "ListaIdStr";
            ddlCiuCorrespondencia.DataValueField = "ListaIdStr";
            ddlCiu0.DataValueField = "ListaIdStr";

            ddlLugarExpedicion.DataBind();
            ddlLugarNacimiento.DataBind();
            ddlCiuCorrespondencia.DataBind();
            ddlCiu0.DataBind();

            // Colocar ciudad por defecto
            String CargarCiudad = System.Configuration.ConfigurationManager.AppSettings["CargarCiudad"].ToString();
            if (CargarCiudad == "true")
            {
                String CiudadDefault = System.Configuration.ConfigurationManager.AppSettings["Ciudad"].ToString();
                ddlLugarExpedicion.SelectedValue = CiudadDefault;
                ddlLugarNacimiento.SelectedValue = CiudadDefault;
                ddlCiuCorrespondencia.SelectedValue = CiudadDefault;
                ddlCiu0.SelectedValue = CiudadDefault;
            }

            ListaSolicitada = "EstadoCivil";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlEstadoCivil.DataSource = lstDatosSolicitud;
            ddlEstadoCivil.DataTextField = "ListaDescripcion";
            ddlEstadoCivil.DataValueField = "ListaId";
            ddlEstadoCivil.DataBind();

            ListaSolicitada = "NivelEscolaridad";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlNivelEscolaridad.DataSource = lstDatosSolicitud;
            ddlNivelEscolaridad.DataTextField = "ListaDescripcion";
            ddlNivelEscolaridad.DataValueField = "ListaId";
            ddlNivelEscolaridad.DataBind();

            // Determina la actividad del negocio
            ListaSolicitada = "Actividad_Negocio";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlActividadE0.DataSource = lstDatosSolicitud;
            ddlActividadE0.DataTextField = "ListaDescripcion";
            ddlActividadE0.DataValueField = "ListaIdStr";
            ddlActividadE0.DataBind();

            // Determina la actividad
            ListaSolicitada = "Actividad2";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlActividadE.DataSource = lstDatosSolicitud;
            ddlActividadE.DataTextField = "ListaDescripcion";
            ddlActividadE.DataValueField = "ListaIdStr";
            ddlActividadE.DataBind();

            ListaSolicitada = "TipoContrato";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlTipoContrato.DataSource = lstDatosSolicitud;
            ddlTipoContrato.DataTextField = "ListaDescripcion";
            ddlTipoContrato.DataValueField = "ListaId";
            ddlTipoContrato.DataBind();

            ListaSolicitada = "TipoCargo";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlCargo.DataSource = lstDatosSolicitud;
            ddlCargo.DataTextField = "ListaDescripcion";
            ddlCargo.DataValueField = "ListaId";
            ddlCargo.DataBind();

            ListaSolicitada = "ESTADO_ACTIVO";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlEstado.DataSource = lstDatosSolicitud;
            ddlEstado.DataTextField = "ListaDescripcion";
            ddlEstado.DataValueField = "ListaId";
            ddlEstado.DataBind();

            List<Xpinn.Caja.Entities.Oficina> lstOficina = new List<Xpinn.Caja.Entities.Oficina>();
            Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
            Xpinn.Caja.Entities.Oficina pOficina = new Xpinn.Caja.Entities.Oficina();
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            lstOficina = oficinaServicio.ListarOficina(pOficina, pUsuario);
            txtCod_oficina.DataSource = lstOficina;
            txtCod_oficina.DataTextField = "nombre";
            txtCod_oficina.DataValueField = "cod_oficina";
            txtCod_oficina.DataBind();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }


    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = persona1Servicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }
 

    /// <summary>
    /// Método para guardar la información de datos básicos del cliente
    /// </summary>
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (txtIdentificacionE.Text == "")
            {
                VerError("Ingrese el número de identificación");
                txtIdentificacionE.Focus();
                return;
            }


            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

            Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

            if (idObjeto != "")
            {
                // Consultar datos ya existentes de la persona si se va a modificar
                vPersona1 = persona1Servicio.ConsultarPersona1(Convert.ToInt64(idObjeto), pUsuario);
            }
            else
            {                
                // Validar que la persona no exista si se va a crear
                vPersona1 = persona1Servicio.ConsultaDatosPersona(txtIdentificacionE.Text, pUsuario);
                if (vPersona1.cod_persona != Int64.MinValue && vPersona1.cod_persona != 0)
                {
                    VerError("Ya existe una persona con la identificación dada");
                    return;
                }
            }

            vPersona1.origen = "Contabilidad";
            if (txtCod_persona.Text != "") vPersona1.cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());
            vPersona1.identificacion = (txtIdentificacionE.Text != "") ? Convert.ToString(txtIdentificacionE.Text.Trim()) : String.Empty;
            vPersona1.dirCorrespondencia = (txtDirCorrespondencia.Text != "") ? Convert.ToString(txtDirCorrespondencia.Text.Trim()) : String.Empty;
            vPersona1.barrioCorrespondencia = (ddlBarrioCorrespondencia.Text != "") ? Convert.ToInt64(ddlBarrioCorrespondencia.SelectedValue) : 0;
            vPersona1.telCorrespondencia = (txtTelCorrespondencia.Text != "") ? Convert.ToString(txtTelCorrespondencia.Text.Trim()) : String.Empty;
            vPersona1.ciuCorrespondencia = (ddlCiuCorrespondencia.Text != "") ? Convert.ToInt64(ddlCiuCorrespondencia.SelectedValue) : 0;            
            if (string.Equals(rblTipo_persona.Text, "Jurídica"))
                vPersona1.tipo_persona = "J";
            else
                vPersona1.tipo_persona = "N";            
            if (txtDigito_verificacion.Text != "") vPersona1.digito_verificacion = Convert.ToInt64(txtDigito_verificacion.Text.Trim());
            if (ddlTipoE.Text != "") vPersona1.tipo_identificacion = Convert.ToInt64(ddlTipoE.SelectedValue);
            if (txtFechaexpedicion.Text != "") vPersona1.fechaexpedicion = Convert.ToDateTime(txtFechaexpedicion.Text.Trim());
            if (ddlLugarExpedicion.Text != "") vPersona1.codciudadexpedicion = Convert.ToInt64(ddlLugarExpedicion.SelectedValue);
            vPersona1.sexo = (rblSexo.Text != "") ? Convert.ToString(rblSexo.SelectedValue) : String.Empty;
            vPersona1.primer_nombre = (txtPrimer_nombreE.Text != "") ? Convert.ToString(txtPrimer_nombreE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.segundo_nombre = (txtSegundo_nombreE.Text != "") ? Convert.ToString(txtSegundo_nombreE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.primer_apellido = (txtPrimer_apellidoE.Text != "") ? Convert.ToString(txtPrimer_apellidoE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.segundo_apellido = (txtSegundo_apellidoE.Text != "") ? Convert.ToString(txtSegundo_apellidoE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.razon_social = String.Empty;
            if (txtFechanacimiento.Text != "") vPersona1.fechanacimiento = Convert.ToDateTime(txtFechanacimiento.Text.Trim());
            if (ddlLugarNacimiento.Text != "") vPersona1.codciudadnacimiento = Convert.ToInt64(ddlLugarNacimiento.SelectedValue);
            if (ddlEstadoCivil.Text != "") vPersona1.codestadocivil = Convert.ToInt64(ddlEstadoCivil.SelectedValue);
            if (ddlNivelEscolaridad.Text != "") vPersona1.codescolaridad = Convert.ToInt64(ddlNivelEscolaridad.SelectedValue);
            try { if (ddlActividadE.Text != "") vPersona1.codactividadStr = ddlActividadE.SelectedValue; }
            catch { }
            vPersona1.direccion = (txtDireccionE.Text != "") ? Convert.ToString(txtDireccionE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.codciudadresidencia = (ddlCiuCorrespondencia.Text != "") ? Convert.ToInt64(ddlCiuCorrespondencia.SelectedValue) : 0;
            vPersona1.telefono = (txtTelefonoE.Text != "") ? Convert.ToString(txtTelefonoE.Text.Trim()) : String.Empty;            
            if (txtAntiguedadlugar.Text != "") vPersona1.antiguedadlugar = Convert.ToInt64(txtAntiguedadlugar.Text.Trim());
            vPersona1.tipovivienda = (rblTipoVivienda.Text != "") ? Convert.ToString(rblTipoVivienda.SelectedValue) : String.Empty;
            vPersona1.arrendador = (txtArrendador.Text != "") ? Convert.ToString(txtArrendador.Text.Trim()) : String.Empty;
            vPersona1.telefonoarrendador = (txtTelefonoarrendador.Text != "") ? Convert.ToString(txtTelefonoarrendador.Text.Trim()) : String.Empty;
            if (txtValorArriendo.Text != "") vPersona1.ValorArriendo = Convert.ToInt64(txtValorArriendo.Text.Trim().Replace(".", ""));
            vPersona1.celular = (txtCelular.Text != "") ? Convert.ToString(txtCelular.Text.Trim()) : String.Empty;
            vPersona1.email = (txtEmail.Text != "") ? Convert.ToString(txtEmail.Text.Trim()) : String.Empty;
            vPersona1.empresa = (txtEmpresa.Text != "") ? Convert.ToString(txtEmpresa.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.telefonoempresa = (txtTelefonoempresa.Text != "") ? Convert.ToString(txtTelefonoempresa.Text.Trim()) : String.Empty;
            vPersona1.direccionempresa = (txtDireccionEmpresa.Text != "") ? Convert.ToString(txtDireccionEmpresa.Text.Trim().ToUpper()) : String.Empty;            
            if (txtAntiguedadlugarEmpresa.Text != "") vPersona1.antiguedadlugarempresa = Convert.ToInt64(txtAntiguedadlugarEmpresa.Text.Trim());
            if (ddlCargo.Text != "") vPersona1.codcargo = Convert.ToInt64(ddlCargo.Text.Trim());
            if (ddlTipoContrato.Text != "") vPersona1.codtipocontrato = Convert.ToInt64(ddlTipoContrato.SelectedValue);
            if (txtCod_asesor.Text != "") vPersona1.cod_asesor = Convert.ToInt64(txtCod_asesor.Text.Trim()); else vPersona1.cod_asesor = pUsuario.codusuario;
            vPersona1.residente = (rblResidente.Text != "") ? Convert.ToString(rblResidente.SelectedValue) : String.Empty;
            if (txtFecha_residencia.Text != "") vPersona1.fecha_residencia = Convert.ToDateTime(txtFecha_residencia.Text.Trim());
            if (txtCod_oficina.SelectedValue != "") vPersona1.cod_oficina = Convert.ToInt64(txtCod_oficina.SelectedValue.Trim());
            vPersona1.tratamiento = (txtTratamiento.Text != "") ? Convert.ToString(txtTratamiento.Text.Trim()) : String.Empty;
            vPersona1.estado = (ddlEstado.Text != "") ? Convert.ToString(ddlEstado.SelectedValue) : String.Empty;
            vPersona1.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
            vPersona1.ActividadEconomicaEmpresaStr = ddlActividadE0.SelectedValue;
            vPersona1.ciudad = Convert.ToInt64(ddlCiu0.SelectedValue);
            vPersona1.relacionEmpleadosEmprender = Convert.ToInt32(ddlparentesco.SelectedValue);
            vPersona1.CelularEmpresa = txtTelCell0.Text;
            vPersona1.profecion = txtProfecion.Text;
            vPersona1.PersonasAcargo = Convert.ToInt32(txtPersonasCargo.Text);
            vPersona1.valor_afiliacion = 0;
            vPersona1.salario = (vPersona1.salario == null ? 0 : vPersona1.salario);
            try
            {
                vPersona1.Estrato = Convert.ToInt32(txtEstrato.Text);
            }
            catch
            {
                vPersona1.Estrato = 0;
            }            
            vPersona1.usuariocreacion = pUsuario.nombre;
            vPersona1.fecultmod = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
            vPersona1.usuultmod = pUsuario.nombre;
            vPersona1.ocupacion = "";
            vPersona1.zona = int.MinValue;
            if (idObjeto == "")
            { 
                persona1Servicio.CrearPersona1(vPersona1, pUsuario);
            }
            else
            {
                persona1Servicio.ModificarPersona1(vPersona1, pUsuario);
            }
            Session[persona1Servicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar("../Terceros/Lista.aspx");
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }


    protected void rblTipoVivienda_DataBound(object sender, EventArgs e)
    {
        RadioButtonList rbl = (RadioButtonList)sender;
        foreach (ListItem li in rblTipoVivienda.Items)
        {
            li.Attributes.Add("onclick", "javascript:DoSomething('" + li.Value + "')");
        }
    }


    protected void rblTipoVivienda_SelectedIndexChanged(object sender, EventArgs e)
    {
        validarArriendo();
    }

    private void validarArriendo()
    {
        if (rblTipoVivienda.SelectedValue == "A")
        {
            txtArrendador.Enabled = true;
            txtTelefonoarrendador.Enabled = true;
            txtValorArriendo.Enabled = true;
        }
        else
        {
            txtArrendador.Enabled = false;
            txtTelefonoarrendador.Enabled = false;
            txtValorArriendo.Enabled = false;
            txtArrendador.Text = "";
            txtTelefonoarrendador.Text = "";
            txtValorArriendo.Text = "";
        }
    }
   

    /// <summary>
    /// Método para calcular la edad según la fecha de nacimiento ingresada
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtFechanacimiento_TextChanged(object sender, EventArgs e)
    {
        CalcularEdad();
    }

    /// <summary>
    /// Cálculo de la edad
    /// </summary>
    private void CalcularEdad()
    {
        if (txtFechanacimiento.Text != "")
            txtEdadCliente.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtFechanacimiento.Text)));
    }


    protected void txtIdentificacionE_TextChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            vPersona1.seleccionar = "Identificacion";
            vPersona1.noTraerHuella = 1;
            vPersona1.identificacion = txtIdentificacionE.Text;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
            if (idObjeto != "")
            {
                if (vPersona1.identificacion != "" && vPersona1.identificacion != null && vPersona1.identificacion != Session["IDENTIFICACION"].ToString())
                    VerError("ERROR: La Identificación ingresada ya existe");
            }
            else
            {
                if (vPersona1.identificacion != "" && vPersona1.identificacion != null)
                    VerError("ERROR: La Identificación ingresada ya existe");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


}