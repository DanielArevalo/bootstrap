using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Aportes.Services;
using Xpinn.Comun.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;

//json
using Xpinn.Util;

public partial class Page_Aportes_Personas_Tabs_Personal : GlobalWeb //System.Web.UI.Page
{
    Persona1Service ServicePersona = new Persona1Service();
    AfiliacionServices _AfiliacionServicio = new AfiliacionServices();
    UsuarioAtribucionesService atribuciones = new UsuarioAtribucionesService();
    Usuario pUsuario = new Usuario();
    UsuarioAtribuciones atrusuarios = new UsuarioAtribuciones();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cargarListas();

            if (Session[Usuario.codusuario + "cod_per"] != null)
            {
                pUsuario = (Usuario)Session["Usuario"];
                txtIdentificacionE.Enabled = false;
                txtPrimer_nombreE.Enabled = false;
                txtSegundo_nombreE.Enabled = false;
                txtPrimer_apellidoE.Enabled = false;
                txtSegundo_apellidoE.Enabled = false;
                atrusuarios.codusuario = pUsuario.codusuario;
                List<UsuarioAtribuciones> atrusuario = atribuciones.ListarUsuarioAtribuciones(atrusuarios, pUsuario);
                ddloficina.SelectedValue = Convert.ToString(pUsuario.cod_oficina);
                cargar_datos();
                CheckAct.Checked = true;
                // CHK tabla  auditoria  gestion de riesgos
                CheckAct.Visible = true;
                foreach (UsuarioAtribuciones item in atrusuario)
                {
                    if (item.tipoatribucion == 7 && item.activo == 1)
                    {
                        ddloficina.Enabled = true;
                        break;
                    }
                    else
                    {
                        ddloficina.Enabled = false;
                    }
                }
                txtfechaAct.Visible = true;
            }

            else
            {
                Usuario pUsuario = new Usuario();
                pUsuario = (Usuario)Session["Usuario"];
                ddloficina.SelectedValue = Convert.ToString(pUsuario.cod_oficina);
                VerificarLlamadoEstadoCuenta();
                CheckAct.Visible = false;
                txtfechaAct.Visible = false;
                lblfechaAct.Visible = false;
                txtIdentificacionE.Enabled = true;
                txtPrimer_nombreE.Enabled = true;
                txtSegundo_nombreE.Enabled = true;
                txtPrimer_apellidoE.Enabled =true;
                txtSegundo_apellidoE.Enabled = true;

            }
        }
    }

    private bool VerificarLlamadoEstadoCuenta()
    {
        Xpinn.Asesores.Services.EstadoCuentaService serviceEstadoCuenta = new Xpinn.Asesores.Services.EstadoCuentaService();
        if (Session[serviceEstadoCuenta.CodigoPrograma + ".id"] != null)
        {
            Session[Usuario.codusuario + "cod_per"] = Session[serviceEstadoCuenta.CodigoPrograma + ".id"];
            Session[_AfiliacionServicio.CodigoPrograma + ".id"] = Session[serviceEstadoCuenta.CodigoPrograma + ".id"];
            Session[_AfiliacionServicio.CodigoPrograma + ".modificar"] = 1;
            return true;
        }
        else
        {
            if (Session[_AfiliacionServicio.CodigoPrograma + ".id"] != null)
                Session.Remove(_AfiliacionServicio.CodigoPrograma + ".id");
            if (Session[_AfiliacionServicio.CodigoPrograma + ".modificar"] != null)
                Session.Remove(_AfiliacionServicio.CodigoPrograma + ".modificar");
        }
        return false;
    }
    protected void buscarBarrioResidencia(object sender, EventArgs e)
    {
        DropDownList dropDownCambiado = sender as DropDownList;

        List<Persona1> lstBarrios = new List<Persona1>();

        Persona1 ciudad = new Persona1();
        Persona1Service persona1Servicio = new Persona1Service();
        try { lstBarrios = persona1Servicio.ListasBarrios(Convert.ToInt32(dropDownCambiado.SelectedValue), (Usuario)Session["Usuario"]); }
        catch { lblerror.Text = "No pudo cargar listado de barrios"; }
        ddlBarrioResid.DataSource = lstBarrios;
        ddlBarrioResid.DataValueField = "ListaId";
        ddlBarrioResid.DataTextField = "ListaDescripcion";
        ddlBarrioResid.DataBind();
    }
    protected void buscarBarrioCorrespondencia(object sender, EventArgs e)
    {
        DropDownList dropDownCambiado = sender as DropDownList;

        List<Persona1> lstBarrios = new List<Persona1>();

        Persona1 ciudad = new Persona1();
        Persona1Service persona1Servicio = new Persona1Service();
        try { lstBarrios = persona1Servicio.ListasBarrios(Convert.ToInt32(dropDownCambiado.SelectedValue), (Usuario)Session["Usuario"]); }
        catch { lblerror.Text = "No pudo cargar listado de barrios"; }
        ddlBarrioCorrespondencia.DataSource = lstBarrios;
        ddlBarrioCorrespondencia.DataValueField = "ListaId";
        ddlBarrioCorrespondencia.DataTextField = "ListaDescripcion";
        ddlBarrioCorrespondencia.DataBind();
    }
    public void cargarListas()
    {
        try
        {

            //Lista Tipos de Documentos
            String ListaSolicitada = null;
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosFiltrado = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            ListaSolicitada = "TipoIdentificacion";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            lstDatosFiltrado = null;
            lstDatosFiltrado = lstDatosSolicitud.Where(x => x.ListaId != 2 && x.ListaId != 6 && x.ListaId != 7).ToList();
            ddlTipoE.DataSource = lstDatosFiltrado;
            ddlTipoE.DataTextField = "ListaDescripcion";
            ddlTipoE.DataValueField = "ListaId";
            ddlTipoE.DataBind();

            //Lista de oficinas de la entidad
            List<Xpinn.Caja.Entities.Oficina> lstOficina = new List<Xpinn.Caja.Entities.Oficina>();
            Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
            Xpinn.Caja.Entities.Oficina pOficina = new Xpinn.Caja.Entities.Oficina();
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            lstOficina = oficinaServicio.ListarOficina(pOficina, pUsuario);
            ddloficina.DataSource = lstOficina;
            ddloficina.DataTextField = "nombre";
            ddloficina.DataValueField = "cod_oficina";
            ddloficina.DataBind();

            //Lista de ciudades
            ListaSolicitada = "Ciudades";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlLugarExpedicion.DataSource = lstDatosSolicitud;
            ddlLugarNacimiento.DataSource = lstDatosSolicitud;
            ddlCiuCorrespondencia.DataSource = lstDatosSolicitud;
            ddlCiudadResidencia.DataSource = lstDatosSolicitud;
            ddlLugarExpedicion.DataTextField = "ListaDescripcion";
            ddlLugarNacimiento.DataTextField = "ListaDescripcion";
            ddlCiuCorrespondencia.DataTextField = "ListaDescripcion";
            ddlCiudadResidencia.DataTextField = "ListaDescripcion";
            ddlLugarExpedicion.DataValueField = "ListaId";
            ddlLugarNacimiento.DataValueField = "ListaId";
            ddlCiuCorrespondencia.DataValueField = "ListaId";
            ddlCiudadResidencia.DataValueField = "ListaId";
            ddlLugarExpedicion.DataBind();
            ddlLugarNacimiento.DataBind();
            ddlCiuCorrespondencia.DataBind();
            ddlCiudadResidencia.DataBind();

            PoblarLista("CIUDADES", "CODCIUDAD, NOMCIUDAD", " TIPO = 1", "1", ddlPais);

            // Definir ciudad por defecto
            String CargarCiudad = System.Configuration.ConfigurationManager.AppSettings["CargarCiudad"].ToString();
            if (CargarCiudad == "true")
            {
                String CiudadDefault = System.Configuration.ConfigurationManager.AppSettings["Ciudad"].ToString();
                ddlLugarExpedicion.SelectedValue = CiudadDefault;
                ddlLugarNacimiento.SelectedValue = CiudadDefault;
                ddlCiuCorrespondencia.SelectedValue = CiudadDefault;
                ddlCiudadResidencia.SelectedValue = CiudadDefault;
            }

            //Lista de Barrios
            ListaSolicitada = "Barrio";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlBarrioCorrespondencia.DataSource = lstDatosSolicitud;
            ddlBarrioResid.DataSource = lstDatosSolicitud;
            ddlBarrioCorrespondencia.DataTextField = "ListaDescripcion";
            ddlBarrioResid.DataTextField = "ListaDescripcion";
            ddlBarrioCorrespondencia.DataValueField = "ListaId";
            ddlBarrioResid.DataValueField = "ListaId";
            ddlBarrioCorrespondencia.DataBind();
            ddlBarrioResid.DataBind();

            //Carga la lista de CIIU
            ListaSolicitada = "Actividad2";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ViewState["DTACTIVIDAD" + pUsuario.codusuario] = lstDatosSolicitud;
            gvActividadesCIIU.DataSource = lstDatosSolicitud;
            gvActividadesCIIU.DataBind();

            //Nivel de Escolaridad
            ListaSolicitada = "NivelEscolaridad";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlNivelEscolaridad.DataSource = lstDatosSolicitud;
            ddlNivelEscolaridad.DataTextField = "ListaDescripcion";
            ddlNivelEscolaridad.DataValueField = "ListaId";
            ddlNivelEscolaridad.DataBind();

            //listar Zona
            ListaSolicitada = "Zona";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlZona.DataSource = lstDatosSolicitud;
            ddlZona.DataTextField = "ListaDescripcion";
            ddlZona.DataValueField = "ListaId";
            ddlZona.DataBind();

            //Lista Estado Civil
            ListaSolicitada = "EstadoCivil";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlEstadoCivil.DataSource = lstDatosSolicitud;
            ddlEstadoCivil.DataTextField = "ListaDescripcion";
            ddlEstadoCivil.DataValueField = "ListaId";
            ddlEstadoCivil.DataBind();

            //Mayoria de edad
            mayoredad();

        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
    }

    protected void cargar_datos()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];
        Persona1 Entidad = new Persona1();
        string validar;
        if (Session[Usuario.codusuario + "cod_per"] != null)
        {

            string id;
            if (Session[Usuario.codusuario.ToString() + "cod_per"] == null)
            {
                VerError("No se pudieron cargar datos no se encontro código de la persona");
                return;
            }
            try
            {
                id = Session[Usuario.codusuario.ToString() + "cod_per"].ToString();
            }
            catch
            {
                VerError("Error:" + Session[Usuario.codusuario.ToString() + "cod_per"].ToString());
                return;
            }

            Entidad.cod_persona = Convert.ToInt64(id);
            Entidad.seleccionar = "Cod_persona";
            Entidad.soloPersona = 1;
            Entidad = ServicePersona.ConsultarPersona1Param(Entidad, (Usuario)Session["usuario"]);
            Session["Persona"] = Entidad;

            if (Entidad.tipo_identificacion != 0)
                ddlTipoE.SelectedValue = Entidad.tipo_identificacion.ToString();
            if (!string.IsNullOrEmpty(Entidad.identificacion))
                txtIdentificacionE.Text = Entidad.identificacion.ToString();
            if (Entidad.cod_oficina != 0)
                ddloficina.SelectedValue = Entidad.cod_oficina.ToString();
            if (Entidad.fechaexpedicion != null && Entidad.fechaexpedicion != DateTime.MinValue)
                this.txtFechaexpedicion.Text = Convert.ToDateTime(Entidad.fechaexpedicion).ToString("yyyy-MM-dd");
            if (Entidad.codciudadexpedicion != 0)
                ddlLugarExpedicion.SelectedValue = Entidad.codciudadexpedicion.ToString();
            if (!string.IsNullOrEmpty(Entidad.primer_nombre))
                txtPrimer_nombreE.Text = Entidad.primer_nombre.ToString();
            if (!string.IsNullOrEmpty(Entidad.segundo_nombre))
                txtSegundo_nombreE.Text = Entidad.segundo_nombre.ToString();
            if (!string.IsNullOrEmpty(Entidad.primer_apellido))
                txtPrimer_apellidoE.Text = Entidad.primer_apellido.ToString();
            if (!string.IsNullOrEmpty(Entidad.segundo_apellido))
                txtSegundo_apellidoE.Text = Entidad.segundo_apellido.ToString();
            //DATOS DE LOCALIZACION
            if (Entidad.ubicacion_residencia != 0 && Entidad.ubicacion_residencia != null)
                //ddlTipoUbic.SelectedValue = Entidad.ubicacion_residencia.ToString();
                txtDireccionE.Tipo_Ubicacion = Convert.ToInt32(Entidad.ubicacion_residencia);
            if (!string.IsNullOrEmpty(Entidad.direccion))
                txtDireccionE.Text = Entidad.direccion;
            if (Entidad.codciudadresidencia != 0 && Entidad.codciudadresidencia != null)
                ddlCiudadResidencia.SelectedValue = Entidad.codciudadresidencia.ToString();
            if (Entidad.barrioResidencia != 0 && Entidad.barrioResidencia != null)
                ddlBarrioResid.SelectedValue = Entidad.barrioResidencia.ToString();
            if (!string.IsNullOrEmpty(Entidad.telefono))
                txtTelefonoE.Text = Entidad.telefono.ToString();
            else
            {
                if (!string.IsNullOrEmpty(Entidad.telCorrespondencia))
                    txtTelefonoE.Text = Entidad.telCorrespondencia.ToString();
            }
            if (Entidad.ubicacion_correspondencia != 0 && Entidad.ubicacion_correspondencia != null)
                //ddlTipoUbicCorr.SelectedValue = Entidad.ubicacion_correspondencia.ToString();
                txtDirCorrespondencia.Tipo_Ubicacion = Convert.ToInt32(Entidad.ubicacion_correspondencia);
            if (!string.IsNullOrEmpty(Entidad.dirCorrespondencia))
                txtDirCorrespondencia.Text = Entidad.dirCorrespondencia;
            if (Entidad.ciuCorrespondencia != 0 && Entidad.ciuCorrespondencia != null)
                ddlCiuCorrespondencia.SelectedValue = Entidad.ciuCorrespondencia.ToString();
            if (Entidad.barrioCorrespondencia != 0 && Entidad.barrioCorrespondencia != null)
                ddlBarrioCorrespondencia.SelectedValue = Entidad.barrioCorrespondencia.ToString();
            if (!string.IsNullOrEmpty(Entidad.telCorrespondencia))
                txtTelCorrespondencia.Text = Entidad.telCorrespondencia;
            if (!string.IsNullOrEmpty(Entidad.email))
                txtEmail.Text = Entidad.email.ToString();
            //ZONA
            if (Entidad.zona != null && Entidad.zona != 0)
                ddlZona.SelectedValue = Entidad.zona.ToString();
            //DATOS GENERALES
            if (!string.IsNullOrEmpty(Entidad.celular))
                txtCelular.Text = Entidad.celular.ToString();
            #region CIIU
            Label lblCodigo;
            foreach (GridViewRow rFila in gvActividadesCIIU.Rows)
            {
                lblCodigo = (Label)rFila.FindControl("lbl_codigo");

                //Identificar la actividad principal
                if (lblCodigo.Text == Entidad.codactividadStr)
                {
                    CheckBoxGrid chkPrincipal = rFila.FindControl("chkPrincipal") as CheckBoxGrid;
                    chkPrincipal.Checked = true;
                    Label lblDescripcion = (Label)rFila.FindControl("lbl_descripcion");
                    txtActividadCIIU.Text = lblDescripcion.Text;
                }

                foreach (Xpinn.FabricaCreditos.Entities.Actividades objActividad in Entidad.lstActEconomicasSecundarias)
                {
                    CheckBoxGrid chkSeleccionado = rFila.FindControl("chkSeleccionar") as CheckBoxGrid;

                    if (objActividad.codactividad == lblCodigo.Text)
                    {
                        chkSeleccionado.Checked = true;
                        break;
                    }
                }
            }
            #endregion
            if (Entidad.nacionalidad != 0 && Entidad.nacionalidad != null)
                ddlPais.SelectedValue = Entidad.nacionalidad.ToString();
            if (Entidad.fechanacimiento != null && Entidad.fechanacimiento != DateTime.MinValue)
            {
                this.txtFechanacimiento.Text = Convert.ToDateTime(Entidad.fechanacimiento).ToString("yyyy-MM-dd");
                txtEdadCliente.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtFechanacimiento.Text)));
            }

            if (Entidad.codciudadnacimiento != 0 && Entidad.codciudadnacimiento != null)
                ddlLugarNacimiento.SelectedValue = Entidad.codciudadnacimiento.ToString();
            if (!string.IsNullOrEmpty(Entidad.sexo))
            {
                ddlsexo.SelectedValue = Entidad.sexo.ToString();
                if (Entidad.sexo.ToString() == "F")
                {
                    chkMujerCabeFami.Visible = true;
                    lblMujerCabeFami.Visible = true;
                }
                else
                {
                    chkMujerCabeFami.Visible = false;
                    lblMujerCabeFami.Visible = false;
                }
            }
            if (Entidad.codescolaridad != 0 && Entidad.codescolaridad != null)
                ddlNivelEscolaridad.SelectedValue = Entidad.codescolaridad.ToString();
            if (!string.IsNullOrEmpty(Entidad.profecion))
                txtProfesion.Text = Entidad.profecion.ToString();
            if (Entidad.codestadocivil != 0 && Entidad.codestadocivil != null)
                ddlEstadoCivil.SelectedValue = Entidad.codestadocivil.ToString();
            if (Entidad.PersonasAcargo != null)
                txtPersonasCargo.Text = Entidad.PersonasAcargo.ToString();
            if (Entidad.Estrato != null)
                txtEstrato.Text = Entidad.Estrato.ToString();
            if (Entidad.ocupacionApo != 0)
            {
                ddlOcupacion.SelectedValue = Entidad.ocupacionApo.ToString();

                string script = @"  function Alertando(valor) {
                                        if (valor == 1 || valor == 2 || valor == 3) 
                                        { window.parent.$('#lilaboral').removeClass('clsInactivos'); }
                                        else
                                        { window.parent.$('#lilaboral').removeClass('clsInactivos').addClass('clsInactivos'); }                                                                                 
                                    
                                        if (valor == 3) {
                                        var elements = window.parent.document.formularios.iframeLaboral.contentDocument.getElementsByClassName('datoEmpleado');
                                            for (var i = 0; i < elements.length; i++)
                                            {
                                                elements[i].style.display = 'none';
                                            }
                                        }
            }
            " +"  Alertando(" + Entidad.ocupacionApo.ToString() + ");                      ";

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Ocupacion", script, true);                
            }
            if (Entidad.tipovivienda == "P")
            {
                ViviendaP.Checked = true;
                txtArrendador.Attributes.Add("required", "false");
                txtTelefonoarrendador.Attributes.Add("required", "false");

                //valida campos                 
                lblPropietario.Text = "Nombre Propietario *";
                lblTelefPropietario.Text = "Teléfono Propietario *";
                lblValorArriendo.Text = "Gastos Vivienda *";
            }
            else if (Entidad.tipovivienda == "A")
            {
                ViviendaA.Checked = true;
                txtArrendador.Attributes.Add("required", "true");
                txtArrendador.BorderColor = System.Drawing.Color.Empty;
                txtTelefonoarrendador.Attributes.Add("required", "true");
                lblPropietario.Text = "Nombre Arrendador *";
                lblTelefPropietario.Text = "Teléfono Arrendador *";
                lblValorArriendo.Text = "Valor Arriendo *";
            }
            else
            {
                ViviendaF.Checked = true;
                txtArrendador.Attributes.Add("required", "true");
                txtTelefonoarrendador.Attributes.Add("required", "true");
                lblPropietario.Text = "Nombre Propietario *";
                lblTelefPropietario.Text = "Teléfono Propietario *";
                lblValorArriendo.Text = "Gastos Vivienda *";
            }
            
               validar = @"validar_vivienda('" + Entidad.tipovivienda+"');";

            if (!string.IsNullOrEmpty(Entidad.arrendador))
                txtArrendador.Text = Entidad.arrendador;
            if (!string.IsNullOrEmpty(Entidad.telefonoarrendador))
                txtTelefonoarrendador.Text = Entidad.telefonoarrendador;
            //if (Entidad.antiguedadlugar != null)
            txtAntiguedadlugar.Text = Entidad.antiguedadlugar.ToString();
            //if (Entidad.ValorArriendo != null)
            txtValorArriendo.Value = Entidad.ValorArriendo.ToString();
            if (Entidad.relacionEmpleadosEmprender != 0)
                ddlparentesco.SelectedValue = Entidad.relacionEmpleadosEmprender.ToString();
            if (Entidad.parentesco_pep != 0)
                ddlFamiliarPEPS.SelectedValue = Entidad.parentesco_pep.ToString();
            if (Entidad.mujer_familia == 1) { chkMujerCabeFami.Checked = true; } else { chkMujerCabeFami.Checked = false; };
            if (Entidad.parentesco_madminis != 0 && Entidad.parentesco_madminis != null)
                ddlFamiliarAdmin.SelectedValue = Entidad.parentesco_madminis.ToString();
            if (Entidad.parentesco_mcontrol != 0 && Entidad.parentesco_mcontrol != null)
                ddlFamiliarControl.SelectedValue = Entidad.parentesco_mcontrol.ToString();
            //Imagen
            if (Entidad.foto != null)
            {
                try
                {
                    imgFoto.ImageUrl = Bytes_A_Archivo(id, Entidad.foto);
                    imgFoto.ImageUrl = string.Format("..//Handler.ashx?id={0}", Entidad.idimagen + "&Us=" + pUsuario.identificacion + "&Pw=" + pUsuario.clave);

                }
                catch // (Exception ex)
                {
                    // VerError("No pudo abrir archivo con imagen de la persona " + ex.Message);
                }
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", validar, true);
        }
    }
    protected void mayoredad()
    {
        //Consultar parametro general de mayoria de edad
        Xpinn.Comun.Services.GeneralService generalServicio = new Xpinn.Comun.Services.GeneralService();
        General pGeneral = new General();
        pGeneral = generalServicio.ConsultarGeneral(104, (Usuario)Session["usuario"]);
        int mayoriaEdad = 0;
        mayoriaEdad = pGeneral.valor != null && pGeneral.valor != "" ? Convert.ToInt32(pGeneral.valor) : 18;
        txtmayoredad.Text = mayoriaEdad.ToString();
    }
    public string Bytes_A_Archivo(string idPersona, Byte[] ImgBytes)
    {
        Stream stream = null;
        string fileName = Server.MapPath("..\\Images\\") + Path.GetFileName(idPersona + ".jpg");
        if (ImgBytes != null)
        {
            try
            {
                // Guardar imagen en un archivo
                stream = File.OpenWrite(fileName);
                foreach (byte b in ImgBytes)
                {
                    stream.WriteByte(b);
                }
                stream.Close();
                this.hdFileName.Value = Path.GetFileName(idPersona + ".jpg");
                mostrarImagen();
            }
            finally
            {
                /*Limpiamos los objetos*/
                stream.Dispose();
                stream = null;
            }
        }
        return fileName;
    }
    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();

    }
    private List<Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = persona1Servicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }
    protected void txtIdentificacionE_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Persona1 Entidad = new Persona1();
            lblerror.Text = ("");
            Entidad.seleccionar = "Identificacion";
            Entidad.soloPersona = 1;
            Entidad.identificacion = txtIdentificacionE.Text;
            Entidad = ServicePersona.ConsultarPersona1Param(Entidad, (Usuario)Session["usuario"]);
            if (Entidad.identificacion != "")
            {
                if (Entidad.identificacion != "" && Entidad.identificacion != null)//&& Entidad.identificacion != Session["IDENTIFICACION"].ToString())
                {

                    lblerror.Text = ("La Identificación ingresada ya existe");
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Identificacion", "alert('La Identificación ingresada ya existe')", true);

                }
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = (ex.Message);
        }
    }
    protected void linkBt_Click(object sender, EventArgs e)
    {
        try
        {
            /*Este evento se generara desde la funcion javascript asignarRutaFoto*/
            if (fuFoto.HasFile == true)
            {
                cargarFotografia();
                mostrarImagen();
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = (ex.Message);
        }
    }
    protected void btnCargarImagen_Click(object sender, EventArgs e)
    {
        try
        {/*Este evento se generara desde la funcion javascript asignarRutaFoto*/
            if (fuFoto.HasFile == true)
            {
                cargarFotografia();
                mostrarImagen();
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = (ex.Message);
        }
    }
    private void mostrarImagen()
    {
        /*Muestra la imagen como un thumbnail*/
        System.Drawing.Image objImage = null, objThumbnail = null;
        Int32 width, height;
        String fileName = Server.MapPath("..\\Images\\") + Path.GetFileName(this.hdFileName.Value);
        Stream stream = null;
        try
        {
            /*Se guarda la imagen en un stream para despues colocarla en un objeto para que la imagen no quede abierta en el servidor*/
            stream = File.OpenRead(fileName);
            if (stream.Length > 2000000)
            {
                lblerror.Text = ("La imagen tiene un valor muy grande");
            }

            objImage = System.Drawing.Image.FromStream(stream);
            width = 100;
            height = objImage.Height / (objImage.Width / width);
            this.Response.Clear();
            /*Se crea el thumbnail y se muestra en la imagen*/
            objThumbnail = objImage.GetThumbnailImage(width, height, null, IntPtr.Zero);
            objThumbnail.Save(Server.MapPath("..\\Images\\") + "thumb_" + this.hdFileName.Value, ImageFormat.Jpeg);
            imgFoto.Visible = true;
            String nombreImgThumb = "thumb_" + this.hdFileName.Value;
            this.hdFileNameThumb.Value = nombreImgThumb;
            imgFoto.ImageUrl = "..\\Images\\" + nombreImgThumb;
        }
        catch (Exception ex)
        {
            lblerror.Text = ("No pudro abrir archivo con imagen de la persona " + ex.Message);
        }
        finally
        {
            /*Limpiamos los objetos*/
            objImage.Dispose();
            objThumbnail.Dispose();
            stream.Dispose();
            objImage = null;
            objThumbnail = null;
            stream = null;
        }
    }
    private void cargarFotografia()
    {
        /*Obtenemos el nombre y la extension del archivo*/
        String fileName = Path.GetFileName(this.fuFoto.PostedFile.FileName);
        String extension = Path.GetExtension(this.fuFoto.PostedFile.FileName).ToLower();

        try
        {
            if (extension != ".png" && extension != ".jpg" && extension != ".bmp")
            {
                lblerror.Text = ("El archivo ingresado no es una imagen");
            }
            else
            {
                /*Se guarda la imagen en el servidor*/
                fuFoto.PostedFile.SaveAs(Server.MapPath("..\\Images\\") + fileName);
                /*Obtenemos el nombre temporal de la imagen con la siguiente funcion*/
                String nombreImgServer = getNombreImagenServidor(extension);
                hdFileName.Value = nombreImgServer;
                /*Cambiamos el nombre de la imagen por el nuevo*/
                File.Move(Server.MapPath("..\\Images\\") + fileName, Server.MapPath("..\\Images\\" + nombreImgServer));
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = (ex.Message);
        }
    }
    public String getNombreImagenServidor(String extension)
    {
        /*Devuelve el nombre temporal de la imagen*/
        Random nRandom = new Random();
        String nr = Convert.ToString(nRandom.Next(0, 32000));
        String nombre = nr + "_" + DateTime.Today.ToString("ddMMyyyy") + extension;
        nRandom = null;
        return nombre;
    }
    protected void imgBuscar_Click(object sender, EventArgs e) //ImageClickEventArgs
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        if (ViewState["DTACTIVIDAD" + pUsuario.codusuario] != null)
        {
            List<Persona1> lstActividad = (List<Persona1>)ViewState["DTACTIVIDAD" + pUsuario.codusuario];
            if (lstActividad != null)
            {
                if (!string.IsNullOrEmpty(txtBuscarCodigo.Text.Trim()) && !string.IsNullOrEmpty(txtBuscarDescripcion.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaIdStr.Contains(txtBuscarCodigo.Text) || x.ListaDescripcion.Contains(txtBuscarDescripcion.Text)).ToList();
                else if (!string.IsNullOrEmpty(txtBuscarCodigo.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaIdStr.Contains(txtBuscarCodigo.Text)).ToList();
                else if (!string.IsNullOrEmpty(txtBuscarCodigo.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaDescripcion.Contains(txtBuscarDescripcion.Text)).ToList();
                gvActividadesCIIU.DataSource = lstActividad;
                gvActividadesCIIU.DataBind();
            }
        }
        MostrarModal();
    }
    private void MostrarModal()
    {
        var ahh = txtRecoger_PopupControlExtender.ClientID;
        var script = @"Sys.Application.add_load(function() { $find('" + ahh + "').showPopup(); });";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPopup", script, true);
    }
    protected void gvActividadesCIIU_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkPrincipal = (CheckBox)e.Row.FindControl("chkPrincipal");
            Label lblDescripcion = (Label)e.Row.FindControl("lbl_descripcion");
            chkPrincipal.Attributes.Add("onclick", "MostrarCIIUPrincipal('" + lblDescripcion.Text + "')");
        }
    }
    public static int GetAge(DateTime birthDate)
    {
        return (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.242199);
    }
    public void btnGuardarPersonal_click(object sender, EventArgs e)
    {
        Session["pensionado"] = ddlOcupacion.SelectedValue == "3" ? ddlOcupacion.SelectedValue : null;
        Session["estadoCivil"] = ddlEstadoCivil.SelectedValue;
        Usuario pUsuario = (Usuario)Session["usuario"];
        Persona1 Entidad = new Persona1();

        Entidad.estado = "A";
        Entidad.tipo_persona = "N";
        Entidad.digito_verificacion = 0;
        //antiguedadlugarempresa
        //DATOS BASICOS        
        Entidad.tipo_identificacion = Convert.ToInt64(ddlTipoE.SelectedValue);
        Entidad.identificacion = txtIdentificacionE.Text.Trim();
        Entidad.cod_oficina = Convert.ToInt64(ddloficina.SelectedValue);
        Entidad.fechaexpedicion = Convert.ToDateTime(txtFechaexpedicion.Text);
        Entidad.codciudadexpedicion = Convert.ToInt64(ddlLugarExpedicion.SelectedValue);
        Entidad.primer_nombre = txtPrimer_nombreE.Text.Trim().ToUpper();
        Entidad.segundo_nombre = txtSegundo_nombreE.Text.Trim().ToUpper();
        Entidad.primer_apellido = txtPrimer_apellidoE.Text.Trim().ToUpper();
        Entidad.segundo_apellido = txtSegundo_apellidoE.Text.Trim().ToUpper();
        //DATOS DE LOCALIZACION
        Entidad.ubicacion_residencia = txtDireccionE.Tipo_Ubicacion;  //Convert.ToInt32(ddlTipoUbic.SelectedValue);
        Entidad.direccion = txtDireccionE.Text.Trim();
        Entidad.codciudadresidencia = Convert.ToInt64(ddlCiudadResidencia.SelectedValue);
        Entidad.barrioResidencia = Convert.ToInt64(ddlBarrioResid.SelectedValue);
        Entidad.telefono = txtTelefonoE.Text.Trim();
        Entidad.ubicacion_correspondencia = txtDirCorrespondencia.Tipo_Ubicacion; //Convert.ToInt32(ddlTipoUbicCorr.SelectedValue);
        Entidad.dirCorrespondencia = txtDirCorrespondencia.Direccion;
        Entidad.ciuCorrespondencia = Convert.ToInt64(ddlCiuCorrespondencia.SelectedValue);
        Entidad.barrioCorrespondencia = Convert.ToInt64(ddlBarrioCorrespondencia.SelectedValue);
        Entidad.telCorrespondencia = txtTelCorrespondencia.Text.Trim();
        Entidad.email = txtEmail.Text.Trim();
        //SE AGREGA ZONA
        Entidad.zona = Convert.ToInt64(ddlZona.SelectedValue.Trim());
        //DATOS GENERALES
        Entidad.celular = txtCelular.Text.Trim();
        #region CIIU
        byte NumActiSeleccionadas = 0;
        bool ActPrincipalSeleccionada = false;
        Label lblCodigo;
        Entidad.lstActEconomicasSecundarias = new List<Actividades>();
        foreach (GridViewRow rFila in gvActividadesCIIU.Rows)
        {
            CheckBoxGrid chkSeleccionado = rFila.FindControl("chkSeleccionar") as CheckBoxGrid;
            CheckBoxGrid chkPrincipal = rFila.FindControl("chkPrincipal") as CheckBoxGrid;
            if (chkSeleccionado.Checked)
            {
                if (!chkPrincipal.Checked)
                {
                    Actividades objActividad = new Actividades();
                    lblCodigo = rFila.FindControl("lbl_codigo") as Label;
                    objActividad.codactividad = lblCodigo.Text;
                    Entidad.lstActEconomicasSecundarias.Add(objActividad);
                }
                else
                {
                    Label lblDescripcion = rFila.FindControl("lbl_descripcion") as Label;
                    lblerror.Text = ("La actividad económica " + lblDescripcion.Text + " fue seleccionada tanto como principal como secundaria");
                    return;
                }
                NumActiSeleccionadas++;
            }

            if (chkPrincipal.Checked)
            {
                if (!ActPrincipalSeleccionada)
                {
                    ActPrincipalSeleccionada = true;
                    lblCodigo = rFila.FindControl("lbl_codigo") as Label;
                    Entidad.codactividadStr = lblCodigo.Text;
                }
                else
                {
                    lblerror.Text = ("Ha seleccionado más de una actividad económica principal");
                    return;
                }
            }

            if (NumActiSeleccionadas > 3)
            {
                lblerror.Text = ("Se han seleccionado mas de 3 actividades económicas secundarias");
                return;
            }
        }

        if (!ActPrincipalSeleccionada)
        {
            lblerror.Text = ("La activida económica principal no fue seleccionada");
            return;
        }
        #endregion
        Entidad.nacionalidad = Convert.ToInt64(ddlPais.SelectedValue);
        Entidad.fechanacimiento = Convert.ToDateTime(txtFechanacimiento.Text);
        Entidad.codciudadnacimiento = Convert.ToInt64(ddlLugarNacimiento.SelectedValue);
        Entidad.sexo = ddlsexo.SelectedValue;
        Entidad.codescolaridad = Convert.ToInt64(ddlNivelEscolaridad.SelectedValue);
        Entidad.profecion = txtProfesion.Text.Trim();
        Entidad.codestadocivil = Convert.ToInt64(ddlEstadoCivil.SelectedValue);
        Entidad.PersonasAcargo = txtPersonasCargo.Text != "" ? Convert.ToInt32(txtPersonasCargo.Text.Trim()) : 0;
        Entidad.Estrato = Convert.ToInt32(txtEstrato.Text.Trim());
        Entidad.ocupacionApo = Convert.ToInt32(ddlOcupacion.SelectedValue);
        if (ViviendaP.Checked == true) { Entidad.tipovivienda = ViviendaP.Value; } else if (ViviendaA.Checked == true) { Entidad.tipovivienda = ViviendaA.Value; } else { Entidad.tipovivienda = ViviendaF.Value; }
        Entidad.arrendador = txtArrendador.Text.Trim();
        Entidad.telefonoarrendador = txtTelefonoarrendador.Text.Trim();
        Entidad.antiguedadlugar = txtAntiguedadlugar.Text != "" ? Convert.ToInt32(txtAntiguedadlugar.Text.Trim()) : 0;
        Entidad.ValorArriendo = txtValorArriendo.Value != "" ? Convert.ToInt32(txtValorArriendo.Value.Trim()) : 0;
        Entidad.relacionEmpleadosEmprender = Convert.ToInt32(ddlparentesco.SelectedValue);
        Entidad.parentesco_pep = Convert.ToInt32(ddlFamiliarPEPS.SelectedValue);
        if (chkMujerCabeFami.Checked == true) { Entidad.mujer_familia = 1; } else { Entidad.mujer_familia = 0; };
        Entidad.parentesco_madminis = Convert.ToInt32(ddlFamiliarAdmin.SelectedValue);
        Entidad.parentesco_mcontrol = Convert.ToInt32(ddlFamiliarControl.SelectedValue);
        Session["ocupacion"] = Convert.ToInt32(ddlOcupacion.SelectedValue);
        Session["estadoCivil"] = Convert.ToString(ddlEstadoCivil.SelectedValue);

        //Imagen
        if (hdFileName.Value != null)
        {
            try
            {
                Stream stream = null;
                /*Se guarda la imagen en un stream para despues colocarla en un objeto para que la imagen no quede abierta en el servidor*/
                stream = File.OpenRead(Server.MapPath("..\\Images\\") + Path.GetFileName(this.hdFileName.Value));
                this.Response.Clear();
                if (stream.Length > 2000000)
                {
                    lblerror.Text = ("La imagen excede el tamaño máximo que es de " + 100000);
                    return;
                }
                using (BinaryReader br = new BinaryReader(stream))
                {
                    Entidad.foto = br.ReadBytes(Convert.ToInt32(stream.Length));
                }
            }
            catch
            {
                Entidad.foto = null;
            }
        }


        // Guarda/Modifica
        if (Session[Usuario.codusuario + "cod_per"] == null)
        {
            try
            {
                Entidad.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
            }
            catch
            {
                Entidad.fechacreacion = System.DateTime.Now;
            }

            try
            {
                Entidad.usuariocreacion = pUsuario.nombre;
                ServicePersona.TabPersonal(Entidad, 1, pUsuario);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Guardado", "alert('Guardo la Información Personal');", true);
            }
            catch (Exception ex)
            {
                lblerror.Text = ("Error al crear la persona. " + ex.Message);
                return;
            }
            Session[Usuario.codusuario + "cod_per"] = Entidad.cod_persona.ToString();
            Session["identificacion"] = Entidad.identificacion.ToString();
            //Entidad.nombres = Entidad.primer_nombre + " " + Entidad.segundo_nombre + " " + Entidad.primer_apellido + "  " + Entidad.segundo_apellido;

            if (Entidad.cod_persona != 0 && Entidad.identificacion != "")
            {
                string script = @" window.parent.document.getElementById('cphMain_lblcodpersona').textContent = '" + Entidad.cod_persona.ToString() + "';" +
                                    "window.parent.document.getElementById('cphMain_lblidentificacion').textContent ='" + Entidad.identificacion + "';" +
                                    "window.parent.document.getElementById('cphMain_lblnombre').textContent = '" + Entidad.nombreCompleto + "';";

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "ToggleScript", script, true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Error", "alert('La información no fue grabada a satisfacción')", true);
            }

        }
        else
        {
            Entidad.fecultmod = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
            Entidad.usuultmod = pUsuario.nombre;
            try
            {
                Entidad.cod_persona = Convert.ToInt64(Session[Usuario.codusuario + "cod_per"].ToString());
                ServicePersona.TabPersonal(Entidad, 2, pUsuario);

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Modificación", "alert('Modifico la Información Personal');", true);

                if (CheckAct.Checked == true)
                {
                    //registro CONTROL  ACTUALIZAR DATOS
                    Xpinn.Aportes.Services.PersonaActDatosServices PersonaActDatosServices = new Xpinn.Aportes.Services.PersonaActDatosServices();
                    Xpinn.Aportes.Entities.PersonaActDatos PersonaActDatosEnti = new Xpinn.Aportes.Entities.PersonaActDatos();

                    PersonaActDatosEnti.cod_persona = Entidad.cod_persona;
                    PersonaActDatosEnti.cod_usua = pUsuario.codusuario;
                    PersonaActDatosEnti.fecha_act = DateTime.Now;

                    PersonaActDatosServices.CrearPersonaActDatos(PersonaActDatosEnti, pUsuario);

                }
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Modificación", "alert('Modifico la Información Personal')", true);
            }
            catch (Exception ex)
            {
                lblerror.Text = ("Error al modificar la persona. " + ex.Message);
                return;
            }
            Session[Usuario.codusuario + "cod_per"] = Entidad.cod_persona.ToString();
            txtCod_persona.Text = Entidad.cod_persona.ToString();
        }

        string script1 = @"function EstadoCivil(valor) {
                            if (valor == 1  || valor == 3 ) {" +
                            "window.parent.document.formularios.iframeBenefi.contentDocument.getElementById('UpdConyugue').style.display = 'block';}" +
                            "else {window.parent.document.formularios.iframeBenefi.contentDocument.getElementById('UpdConyugue').style.display = 'none';}}" +
                            "EstadoCivil(" + ddlEstadoCivil.SelectedValue + ");";

        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "EstadoCv", script1, true);
        


    }
}

