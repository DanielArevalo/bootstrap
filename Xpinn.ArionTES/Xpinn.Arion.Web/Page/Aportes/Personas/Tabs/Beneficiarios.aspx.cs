using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;


public partial class Page_Aportes_Personas_Tabs_Personal : GlobalWeb // System.Web.UI.Page
{
    BeneficiarioService BeneficiarioServicio = new BeneficiarioService();
    Persona1Service ServicePersona = new Persona1Service();
    AfiliacionServices _afiliacionServicio = new AfiliacionServices();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cargarListas();
            InicializarBeneficiarios();
            if (Session[Usuario.codusuario + "cod_per"] != null)
            {
                cargar_datos();
            }
        }
    }
    public void cargar_datos()
    {
        Persona1 Entidad = new Persona1();
        if (Session[Usuario.codusuario + "cod_per"] != null)
        {

            string id = (string)Session[Usuario.codusuario + "cod_per"];

            //if (Session["Persona"] != null)
            //{
            //    Entidad = (Persona1)Session["Persona"];
            //}
            //else
            //{
            if (Convert.ToInt64(id) != 0)
            {
                Entidad.cod_persona = Convert.ToInt64(id);
                Entidad.seleccionar = "Cod_persona";
                Entidad.soloPersona = 1;
                Entidad = ServicePersona.ConsultarPersona1Param(Entidad, (Usuario)Session["usuario"]);
            }
            if (Entidad != null)
            {
                if (Entidad != null)
                {                    
                    // Conyugue  
                    Xpinn.FabricaCreditos.Services.Persona1Service ContugueServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
                    Xpinn.FabricaCreditos.Entities.Persona1 Ent_Conyugue = new Xpinn.FabricaCreditos.Entities.Persona1();
                    ActividadesServices ActividadServicio = new ActividadesServices();
                    Ent_Conyugue.cod_persona = Convert.ToInt64(id);
                    Ent_Conyugue = ContugueServicio.ConsultarPersona1conyuge(Ent_Conyugue, (Usuario)Session["usuario"]);

                    if (!string.IsNullOrEmpty(Ent_Conyugue.primer_nombre))
                        txtnombre1_cony.Text = Ent_Conyugue.primer_nombre.ToString();
                    if (!string.IsNullOrEmpty(Ent_Conyugue.segundo_nombre))
                        txtnombre2_cony.Text = Ent_Conyugue.segundo_nombre.ToString();
                    if (!string.IsNullOrEmpty(Ent_Conyugue.primer_apellido))
                        txtapellido1_cony.Text = Ent_Conyugue.primer_apellido.ToString();
                    if (!string.IsNullOrEmpty(Ent_Conyugue.segundo_apellido))
                        txtapellido2_cony.Text = Ent_Conyugue.segundo_apellido.ToString();
                    if (Ent_Conyugue.tipo_identificacion != Int64.MinValue && Ent_Conyugue.tipo_identificacion > 0)
                        ddlTipo.SelectedValue = Ent_Conyugue.tipo_identificacion.ToString();
                    if (!string.IsNullOrEmpty(Ent_Conyugue.identificacion))
                    {
                        txtIdent_cony.Text = Ent_Conyugue.identificacion.ToString();
                        txtIdent_cony.Enabled = false;
                        txtnombre1_cony.Enabled = false;
                        txtnombre2_cony.Enabled = false;
                        txtapellido1_cony.Enabled = false;
                        txtapellido2_cony.Enabled = false;
                    }
                    else
                    {
                        txtIdent_cony.Enabled = true;
                        txtnombre1_cony.Enabled = true;
                        txtnombre2_cony.Enabled = true;
                        txtapellido1_cony.Enabled = true;
                        txtapellido2_cony.Enabled = true;                        
                    }
                    if (Ent_Conyugue.fechaexpedicion != null && Ent_Conyugue.fechaexpedicion != DateTime.MinValue)
                        this.txtFechaExp_Cony.Text = Convert.ToDateTime(Ent_Conyugue.fechaexpedicion).ToString("yyyy-MM-dd");

                    if (Ent_Conyugue.codciudadexpedicion != 0 && Ent_Conyugue.codciudadexpedicion != null)
                        ddlcuidadExp_Cony.SelectedValue = Ent_Conyugue.codciudadexpedicion.ToString();
                                            
                    if (Ent_Conyugue.sexo != "" && Ent_Conyugue.sexo != null)
                        ddlsexo.SelectedValue = Ent_Conyugue.sexo.ToString();
                    if (Ent_Conyugue.fechanacimiento != null && Ent_Conyugue.fechanacimiento != DateTime.MinValue)
                    {
                        this.txtfechaNac_Cony.Text = Convert.ToDateTime(Ent_Conyugue.fechanacimiento).ToString("yyyy-MM-dd");
                        txtEdad_Cony.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtfechaNac_Cony.Text)));
                    }

                    if (Ent_Conyugue.codciudadnacimiento != 0 && Ent_Conyugue.codciudadnacimiento != null)
                        ddlLugNacimiento_Cony.SelectedValue = Ent_Conyugue.codciudadnacimiento.ToString();                        
                    if (!string.IsNullOrEmpty(Ent_Conyugue.email))
                        txtemail_cony.Text = Ent_Conyugue.email.ToString();
                    if (!string.IsNullOrEmpty(Ent_Conyugue.celular))
                        txtCell_cony.Text = Ent_Conyugue.celular.ToString();
                    if (Ent_Conyugue.Estrato != null)
                        txtEstrato_Cony.Text = Ent_Conyugue.Estrato.ToString();
                    if (Ent_Conyugue.ocupacion != "" && Ent_Conyugue.ocupacion != "0" && Ent_Conyugue.ocupacion != null)
                        ddlOcupacion_Cony.SelectedValue = Ent_Conyugue.ocupacion.ToString();

                    if (!string.IsNullOrEmpty(Ent_Conyugue.empresa))
                        txtempresa_cony.Text = Ent_Conyugue.empresa.ToString();
                    if (!string.IsNullOrEmpty(Ent_Conyugue.telefonoempresa))
                        txtTelefonoEmp_Cony.Text = Ent_Conyugue.telefonoempresa.ToString().Trim();
                    if (Ent_Conyugue.codtipocontrato != 0)
                        ddlcontrato_cony.SelectedValue = Ent_Conyugue.codtipocontrato.ToString();
                    if (Ent_Conyugue.codcargo != 0)
                        ddlCargo_cony.SelectedValue = Ent_Conyugue.codcargo.ToString();
                    if (Ent_Conyugue.antiguedadlugarempresa != Int64.MinValue && Ent_Conyugue.antiguedadlugarempresa != 0)
                        txtantiguedad_cony.Text = Ent_Conyugue.antiguedadlugarempresa.ToString().Trim();
                    
                    //Lista de Beneficiarios
                    try
                    {
                        List<Beneficiario> LstBeneficiario = new List<Beneficiario>();

                        LstBeneficiario = BeneficiarioServicio.ConsultarBeneficiario(Convert.ToInt64(id), (Usuario)Session["usuario"]);
                        if (LstBeneficiario.Count > 0)
                        {
                            if ((LstBeneficiario != null) || (LstBeneficiario.Count != 0))
                            {
                                gvBeneficiarios.DataSource = LstBeneficiario;
                                gvBeneficiarios.DataBind();
                            }
                            //ViewState[Usuario.codusuario + "DatosBene"] = LstBeneficiario;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblerror.Text = ("Error al consultar la lista de Bebeficiarios, error:" + ex.Message);
                    }

                    //Lista de Parentescos
                    try
                    {
                        List<PersonaParentescos> listaParentescos = _afiliacionServicio.ListarParentescoDeUnaPersona(Convert.ToInt64(id), Usuario);

                        if (listaParentescos.Count <= 0)
                        {
                            listaParentescos.Add(new PersonaParentescos());
                        }
                        Session["lstParentescos"] = listaParentescos;

                        gvParentescos.DataSource = listaParentescos;
                        gvParentescos.DataBind();
                    }
                    catch (Exception ex)
                    {
                        lblerror.Text = ("Error al consultar la lista de parentesco, error:" + ex.Message);
                    }
                }
                else
                {
                    txtIdent_cony.Text = "";
                    txtnombre1_cony.Text = "";
                    txtnombre2_cony.Text = "";
                    txtapellido1_cony.Text = "";
                    txtapellido2_cony.Text = "";
                    ddlcuidadExp_Cony.SelectedValue = "0";
                    this.txtFechaExp_Cony.Text = "";
                    this.txtfechaNac_Cony.Text = "";
                    txtEdad_Cony.Text = "";
                    ddlLugNacimiento_Cony.SelectedValue = "0";
                    txtemail_cony.Text = "";
                    txtCell_cony.Text = "";
                    txtEstrato_Cony.Text = "";
                    ddlOcupacion_Cony.SelectedValue = "0";
                    txtempresa_cony.Text = "";
                    txtTelefonoEmp_Cony.Text = "";
                    ddlcontrato_cony.SelectedValue = "0";
                    ddlCargo_cony.SelectedValue = "0";
                    txtantiguedad_cony.Text = "";
                    txtIdent_cony.Enabled = true;
                }
            }
        }
    }
    private List<Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = persona1Servicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }
    protected void cargarListas()
    {
        String ListaSolicitada = null;
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosFiltrado = new List<Xpinn.FabricaCreditos.Entities.Persona1>();

        //Tipo de identificacion - Conyugue
        ListaSolicitada = "TipoIdentificacion";
        lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
        lstDatosFiltrado = null;
        lstDatosFiltrado = lstDatosSolicitud.Where(x => x.ListaId != 2 && x.ListaId != 4 && x.ListaId != 6 && x.ListaId != 7).ToList();
        ddlTipo.DataSource = lstDatosFiltrado;
        ddlTipo.DataTextField = "ListaDescripcion";
        ddlTipo.DataValueField = "ListaId";
        ddlTipo.DataBind();

        //Ciudad de expedicion del documento - Conyugue
        ListaSolicitada = "Lugares";
        lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
        ddlcuidadExp_Cony.DataSource = lstDatosSolicitud;
        ddlcuidadExp_Cony.DataTextField = "ListaDescripcion";
        ddlcuidadExp_Cony.DataValueField = "ListaIdStr";
        ddlcuidadExp_Cony.DataBind();

        //Ciudad de nacimiento - Conyugue
        ddlLugNacimiento_Cony.DataSource = lstDatosSolicitud;
        ddlLugNacimiento_Cony.DataTextField = "ListaDescripcion";
        ddlLugNacimiento_Cony.DataValueField = "ListaIdStr";
        ddlLugNacimiento_Cony.DataBind();

        //Tipo cargo - Conyugue
        ListaSolicitada = "TipoCargo";
        lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
        ddlCargo_cony.DataSource = lstDatosSolicitud;
        ddlCargo_cony.DataTextField = "ListaDescripcion";
        ddlCargo_cony.DataValueField = "ListaId";
        ddlCargo_cony.DataBind();

        //Tipo  contrato - Conyugue
        ListaSolicitada = "TipoContrato";
        lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
        ddlcontrato_cony.DataSource = lstDatosSolicitud;
        ddlcontrato_cony.DataTextField = "ListaDescripcion";
        ddlcontrato_cony.DataValueField = "ListaId";
        ddlcontrato_cony.DataBind();


    }
    protected void txtIdentificacionE_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Persona1 Entidad = new Persona1();
            lblerror.Text = ("");
            Entidad.seleccionar = "Identificacion";
            Entidad.soloPersona = 1;
            Entidad.identificacion = txtIdent_cony.Text;
            Entidad = ServicePersona.ConsultarPersona1Param(Entidad, (Usuario)Session["usuario"]);
            if (Entidad.identificacion != "")
            {                
                if (Entidad.identificacion != "" && Entidad.identificacion != null)//&& Entidad.identificacion != Session["IDENTIFICACION"].ToString())
                {
                    txtnombre1_cony.Text = Entidad.primer_nombre;
                    txtnombre1_cony.ReadOnly = true;
                    txtnombre2_cony.Text = Entidad.segundo_nombre;
                    txtnombre2_cony.ReadOnly = true;
                    txtapellido1_cony.Text = Entidad.primer_apellido;
                    txtapellido1_cony.ReadOnly = true;
                    txtapellido2_cony.Text = Entidad.segundo_apellido;
                    txtapellido2_cony.ReadOnly = true;
                    txtFechaExp_Cony.Text = Convert.ToDateTime(Entidad.fechaexpedicion).ToString("yyyy-MM-dd");
                    ddlcuidadExp_Cony.SelectedValue = Entidad.ciudadexpedicion;
                    ddlsexo.SelectedValue = Entidad.sexo;
                    txtfechaNac_Cony.Text = Convert.ToDateTime(Entidad.fechanacimiento).ToString("yyyy-MM-dd");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "edad("+ Convert.ToDateTime(Entidad.fechanacimiento).ToString("yyyy-MM-dd") + ")", true);
                    ddlLugNacimiento_Cony.SelectedValue = Convert.ToString(Entidad.codciudadnacimiento);
                    txtemail_cony.Text = Entidad.email;
                    txtEstrato_Cony.Text = Convert.ToString(Entidad.Estrato);
                    ddlOcupacion_Cony.SelectedValue = Entidad.ocupacion;
                    txtempresa_cony.Text = Entidad.empresa;
                    txtTelefonoEmp_Cony.Text = Entidad.telefonoempresa;
                    ddlcontrato_cony.SelectedValue = Entidad.tipocontrato;
                    ddlCargo_cony.Text = Convert.ToString(Entidad.codcargo);
                    txtantiguedad_cony.Text = Convert.ToString(Entidad.antiguedadlugarempresa);
                    /*ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Identificación", "alert('La Identificación ingresada ya existe.')", true);
                    lblerror.Text = ("La Identificación del conyuge ingresada ya existe");*/
                }
                else
                {
                    LimpiarConyugue();
                }
            }
            else
            {
                txtnombre1_cony.ReadOnly = true;
                txtnombre2_cony.ReadOnly = true;
                txtapellido1_cony.ReadOnly = true;
                txtapellido2_cony.ReadOnly = true;
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = (ex.Message);
        }
    }

    private void LimpiarConyugue()
    {
        txtnombre1_cony.Text = "";
        txtnombre2_cony.Text = "";
        txtapellido1_cony.Text = "";
        txtapellido2_cony.Text = "";
        txtFechaExp_Cony.Text = "";
        ddlcuidadExp_Cony.SelectedValue = "";
        ddlsexo.SelectedValue = "";
        txtfechaNac_Cony.Text = "";
        ddlLugNacimiento_Cony.SelectedValue = "";
        txtemail_cony.Text = "";
        txtEstrato_Cony.Text = "";
        ddlOcupacion_Cony.SelectedValue = "";
        txtempresa_cony.Text = "";
        txtTelefonoEmp_Cony.Text = "";
        ddlcontrato_cony.SelectedValue = "";
        ddlCargo_cony.Text = "";
        txtantiguedad_cony.Text = "";
        txtEdad_Cony.Text = "";
        txtCell_cony.Text = "";
        txtnombre1_cony.ReadOnly = false;
        txtnombre2_cony.ReadOnly = false;
        txtapellido1_cony.ReadOnly = false;
        txtapellido2_cony.ReadOnly = false;        
    }
    #region Conyugue
    private void GuardarConyuge()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];

        Conyuge vConyuge = new Conyuge();
        ConyugeService ConyugeServicio = new ConyugeService();
        Persona1 Conyugue = new Persona1();
        Persona1Service persona1Servicio = new Persona1Service();

        Conyugue.identificacion = txtIdent_cony.Text;
        Conyugue.seleccionar = "Identificacion";
        Conyugue.soloPersona = 1;
        Conyugue = persona1Servicio.ConsultarPersona1Param(Conyugue, (Usuario)Session["usuario"]);
        Conyugue.cod_persona = Conyugue.nombre != "errordedatos" ? Conyugue.cod_persona : 0;

        //DATOS BASICOS CONYUGUE
        Conyugue.origen = "Afiliacion-Conyuge";
        Conyugue.tipo_persona = "N";
        Conyugue.identificacion = (txtIdent_cony.Text != "") ? Convert.ToString(txtIdent_cony.Text.Trim()) : String.Empty;
        Conyugue.digito_verificacion = 0;
        if (ddlTipo.Text != "") Conyugue.tipo_identificacion = Convert.ToInt64(ddlTipo.SelectedValue);
        Conyugue.fechaexpedicion = !string.IsNullOrEmpty(txtFechaExp_Cony.Text) ? Convert.ToDateTime(txtFechaExp_Cony.Text) : DateTime.MinValue;
        if (ddlcuidadExp_Cony.Text != "") Conyugue.codciudadexpedicion = Convert.ToInt64(ddlcuidadExp_Cony.SelectedValue);
        Conyugue.sexo = ddlsexo.SelectedValue.ToString();
        Conyugue.primer_nombre = (txtnombre1_cony.Text != "") ? Convert.ToString(txtnombre1_cony.Text.Trim()) : String.Empty;
        Conyugue.segundo_nombre = (txtnombre2_cony.Text != "") ? Convert.ToString(txtnombre2_cony.Text.Trim()) : String.Empty;
        Conyugue.primer_apellido = (txtapellido1_cony.Text != "") ? Convert.ToString(txtapellido1_cony.Text.Trim()) : String.Empty;
        Conyugue.segundo_apellido = (txtapellido2_cony.Text != "") ? Convert.ToString(txtapellido2_cony.Text.Trim()) : String.Empty;
        if (txtfechaNac_Cony.Text != "") Conyugue.fechanacimiento = Convert.ToDateTime(txtfechaNac_Cony.Text.Trim());
        if (ddlLugNacimiento_Cony.SelectedValue != "")
            Conyugue.codciudadnacimiento = Convert.ToInt64(ddlLugNacimiento_Cony.SelectedValue);
        //estado Civil
        Conyugue.codescolaridad = 0;
        //codactividadStr admite null
        Conyugue.direccion = "";
        if (txtdirec_cony.Text != "") Conyugue.direccionempresa = txtdirec_cony.Text; else Conyugue.direccionempresa = "0";
        //telefono admite null
        Conyugue.codciudadresidencia = 0;
        Conyugue.antiguedadlugar = 0;
        // tipovivienda admite null
        // arrendador admite null
        //telefonoarrendador admite null
        //ValorArriendo admite null
        Conyugue.celular = (txtCell_cony.Text != "") ? Convert.ToString(txtCell_cony.Text.Trim()) : String.Empty;
        Conyugue.email = (txtemail_cony.Text != "") ? Convert.ToString(txtemail_cony.Text.Trim()) : String.Empty;
        Conyugue.cod_oficina = pUsuario.cod_oficina;
        Conyugue.estado = "A";
        try
        {
            Conyugue.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
        }
        catch
        {
            Conyugue.fechacreacion = DateTime.Now;
        }
        Conyugue.usuariocreacion = pUsuario.nombre;
        Conyugue.barrioResidencia = 0;
        Conyugue.dirCorrespondencia = "";
        Conyugue.barrioCorrespondencia = 0;
        Conyugue.telCorrespondencia = "";
        Conyugue.ciuCorrespondencia = 0;
        Conyugue.PersonasAcargo = 0;
        Conyugue.profecion = " ";
        Conyugue.Estrato = txtEstrato_Cony.Text != "" ? Convert.ToInt32(txtEstrato_Cony.Text) : 0;
        if (ddlOcupacion_Cony.SelectedValue != "")
            Conyugue.ocupacionApo = Convert.ToInt32(ddlOcupacion_Cony.SelectedValue);


        //DATOS BASICOS EMPRESA
        Conyugue.empresa = (txtempresa_cony.Text != "") ? Convert.ToString(txtempresa_cony.Text.Trim()) : String.Empty;
        if (txtantiguedad_cony.Text != "") Conyugue.antiguedadlugarempresa = Convert.ToInt64(txtantiguedad_cony.Text.Trim());

        if (ddlcontrato_cony.Text != "") Conyugue.codtipocontrato = Convert.ToInt64(ddlcontrato_cony.SelectedValue);
        if (ddlCargo_cony.SelectedValue != "") Conyugue.codcargo = Convert.ToInt64(ddlCargo_cony.Text.Trim());
        Conyugue.telefonoempresa = (txtTelefonoEmp_Cony.Text != "") ? Convert.ToString(txtTelefonoEmp_Cony.Text.Trim()) : String.Empty;

        if (txtdirec_cony.Text != "")
            Conyugue.direccionempresa = txtdirec_cony.Text;
        else
            Conyugue.direccionempresa = "0";

        // Asignar fecha de ùltima modificaciòn del conyùge
        try
        {
            Conyugue.fecultmod = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
        }
        catch
        {
            Conyugue.fecultmod = DateTime.Now;
        }

        if (ddlOcupacion_Cony.SelectedIndex > 0)
            Conyugue.ocupacion = ddlOcupacion_Cony.SelectedValue;


        PersonaResponsable pResponsable = new PersonaResponsable();
        if (txtIdent_cony.Text != "")
        {
            if (Conyugue.cod_persona != 0)
            {
                Session[Usuario.codusuario + "Cod_persona_conyuge"] = txtcod_conyuge.Text;
                persona1Servicio.TabPersonal(Conyugue, 2, (Usuario)Session["usuario"]);
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "ConyugueM", "alert('Modifico la Información del Conyugue.')", true);
            }
            else
            {
                persona1Servicio.TabPersonal(Conyugue, 1, (Usuario)Session["usuario"]);
               // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "ConyugueC", "alert('Guardo la Información del Conyugue.')", true);
            }
        }

        vConyuge.cod_conyuge = Conyugue.cod_persona;
        Session[Usuario.codusuario + "Cod_persona_conyuge"] = vConyuge.cod_conyuge;
        vConyuge.cod_persona = Convert.ToInt64(Session[Usuario.codusuario + "cod_per"].ToString());
        if (vConyuge.cod_conyuge != 0)
        {
            ConyugeServicio.CrearConyuge(vConyuge, (Usuario)Session["usuario"]);
            string idObjeto = Conyugue.cod_persona.ToString();
        }

    }
    protected void chkConyuge_CheckedChanged(object sender, EventArgs e)
    {
        if (chkConyuge.Checked)
        {
            PanelConyuge.Enabled = true;
        }
        else
        {
            PanelConyuge.Enabled = false;
            if (txtcod_conyuge.Text != "")
            {
                ConyugeService ConService = new ConyugeService();
                ConService.EliminarConyuge(Convert.ToInt64(txtcod_conyuge.Text), (Usuario)Session["usuario"]);
            }
        }
    }
    #endregion
    #region Beneficiarios
    protected void TxtIde_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid Textact = (TextBoxGrid)sender;
        int index = Convert.ToInt32(Textact.CommandArgument);

        bool resultSearch = false;

        foreach (GridViewRow row in gvBeneficiarios.Rows)
        {
            TextBoxGrid rowtxtIdentificacion = row.FindControl("txtIdentificacion") as TextBoxGrid;
            if (index != row.RowIndex)
            {
                if (Textact.Text.Trim() == rowtxtIdentificacion.Text.Trim())
                {
                    if (Textact.Text.Trim() != "" && rowtxtIdentificacion.Text.Trim() != "")
                    {
                        resultSearch = true;
                        break;
                    }
                }
            }
        }

        TextBox txtNombres = (TextBox)gvBeneficiarios.Rows[index].FindControl("txtNombres");
        if (resultSearch)
        {
            lblerror.Text = ("Identifiación ya ingresada -- Beneficiarios");
            Textact.Text = "";
            txtNombres.Text = "";
            return;
        }

        if (!string.IsNullOrEmpty(Textact.Text))
        {
            // REALIZANDO BUSQUEDA DE LA PERSONA SI EXISTE.
            Persona1Service persona1Servicio = new Persona1Service();
            Persona1 vPersona1 = new Persona1();

            vPersona1.identificacion = Textact.Text;
            vPersona1.seleccionar = "Identificacion";
            vPersona1.soloPersona = 1;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
            if (vPersona1.nombre != "errordedatos")
            {
                txtNombres.Text = vPersona1.tipo_persona == "N" ? vPersona1.nombres + " " + vPersona1.apellidos : vPersona1.razon_social;
            }
        }
        else
            txtNombres.Text = "";

    }
    protected void InicializarBeneficiarios()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];

        List<Beneficiario> lstBeneficiarios = new List<Beneficiario>();
        for (int i = gvBeneficiarios.Rows.Count; i < 2; i++)
        {
            Beneficiario eBenef = new Beneficiario();
            eBenef.idbeneficiario = -1;
            eBenef.nombre = "";
            eBenef.identificacion_ben = "";
            eBenef.tipo_identificacion_ben = null;
            eBenef.nombre_ben = "";
            eBenef.fecha_nacimiento_ben = null;
            eBenef.parentesco = null;
            eBenef.porcentaje_ben = null;
            lstBeneficiarios.Add(eBenef);
        }
        gvBeneficiarios.DataSource = lstBeneficiarios;
        gvBeneficiarios.DataBind();

        ViewState[pUsuario.codusuario + "DatosBene"] = lstBeneficiarios;
    }
    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        Usuario pUsuario = (Usuario)Session["usuario"];

        ObtenerListaBeneficiarios();

        List<Beneficiario> lstBene = new List<Beneficiario>();

        if (ViewState[pUsuario.codusuario + "DatosBene"] != null)
        {
            lstBene = (List<Beneficiario>)ViewState[pUsuario.codusuario + "DatosBene"];
            int porcentaje = 0;
            porcentaje = Convert.ToInt32(lstBene.Where(x => x.porcentaje_ben > 0).Sum(x => x.porcentaje_ben));
            if (porcentaje < 100)
            {
                for (int i = 1; i <= 1; i++)
                {
                    Beneficiario eBenef = new Beneficiario();
                    eBenef.idbeneficiario = -1;
                    eBenef.nombre = "";
                    eBenef.identificacion_ben = "";
                    eBenef.tipo_identificacion_ben = null;
                    eBenef.nombre_ben = "";
                    eBenef.fecha_nacimiento_ben = null;
                    eBenef.parentesco = null;
                    eBenef.porcentaje_ben = null;
                    lstBene.Add(eBenef);
                }
                gvBeneficiarios.DataSource = lstBene;
                gvBeneficiarios.DataBind();

                ViewState[pUsuario.codusuario + "DatosBene"] = lstBene;
            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Beneficiario", "alert('Por Favor Dismunuya o ajuste porcentaje de Beneficiarios')", true);
            }

        }
        else
        {
            for (int i = 1; i <= 1; i++)
            {
                Beneficiario eBenef = new Beneficiario();
                eBenef.idbeneficiario = -1;
                eBenef.nombre = "";
                eBenef.identificacion_ben = "";
                eBenef.tipo_identificacion_ben = null;
                eBenef.nombre_ben = "";
                eBenef.fecha_nacimiento_ben = null;
                eBenef.parentesco = null;
                eBenef.porcentaje_ben = null;
                lstBene.Add(eBenef);
            }
            gvBeneficiarios.DataSource = lstBene;
            gvBeneficiarios.DataBind();
            ViewState[pUsuario.codusuario + "DatosBene"] = lstBene;
        }
    }
    protected void gvBeneficiarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlParentezco = (DropDownList)e.Row.FindControl("ddlParentezco");
            if (ddlParentezco != null)
            {
                Beneficiario Ben = new Beneficiario();
                ddlParentezco.DataSource = BeneficiarioServicio.ListarParentesco(Ben, (Usuario)Session["usuario"]);
                ddlParentezco.DataTextField = "DESCRIPCION";
                ddlParentezco.DataValueField = "CODPARENTESCO";
                ddlParentezco.Items.Insert(0, new ListItem("<Seleccione un item>", "0"));
                ddlParentezco.DataBind();

            }

            Label lblParentezco = (Label)e.Row.FindControl("lblParentezco");
            if (lblParentezco.Text != null)
                ddlParentezco.SelectedValue = lblParentezco.Text;

        }
    }
    protected void gvBeneficiarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Usuario pUsuario = (Usuario)Session["usuario"];

        int conseID = Convert.ToInt32(gvBeneficiarios.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaBeneficiarios();

        List<Beneficiario> LstBene;
        LstBene = (List<Beneficiario>)ViewState[pUsuario.codusuario + "DatosBene"];

        if (conseID > 0)
        {
            try
            {
                foreach (Beneficiario bene in LstBene)
                {
                    if (bene.idbeneficiario == conseID)
                    {
                        BeneficiarioServicio.EliminarBeneficiario(conseID, (Usuario)Session["usuario"]);
                        LstBene.Remove(bene);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                lblerror.Text = (ex.Message);
            }
        }
        else
        {
            LstBene.RemoveAt((gvBeneficiarios.PageIndex * gvBeneficiarios.PageSize) + e.RowIndex);
        }

        gvBeneficiarios.DataSourceID = null;
        gvBeneficiarios.DataBind();

        gvBeneficiarios.DataSource = LstBene;
        gvBeneficiarios.DataBind();

        ViewState[pUsuario.codusuario + "DatosBene"] = LstBene;
    }
    protected List<Beneficiario> ObtenerListaBeneficiarios(long CodPersona = 0)
    {
        Usuario pUsuario = (Usuario)Session["usuario"];

        List<Beneficiario> lstBeneficiarios = new List<Beneficiario>();
        List<Beneficiario> lista = new List<Beneficiario>();

        foreach (GridViewRow rfila in gvBeneficiarios.Rows)
        {
            Beneficiario eBenef = new Beneficiario();
            Label lblidbeneficiario = (Label)rfila.FindControl("lblidbeneficiario");
            if (lblidbeneficiario != null)
                eBenef.idbeneficiario = Convert.ToInt64(lblidbeneficiario.Text);

            DropDownListGrid ddlParentezco = (DropDownListGrid)rfila.FindControl("ddlParentezco");
            if (ddlParentezco.SelectedValue != null || ddlParentezco.SelectedIndex != 0)
                eBenef.parentesco = Convert.ToInt32(ddlParentezco.SelectedValue);

            TextBox txtIdentificacion = (TextBox)rfila.FindControl("txtIdentificacion");
            if (txtIdentificacion != null)
                eBenef.identificacion_ben = Convert.ToString(txtIdentificacion.Text);
            TextBox txtNombres = (TextBox)rfila.FindControl("txtNombres");
            if (txtNombres != null)
                eBenef.nombre_ben = Convert.ToString(txtNombres.Text);
            fecha txtFechaNacimientoBen = (fecha)rfila.FindControl("txtFechaNacimientoBen");
            if (txtFechaNacimientoBen != null)
                if (txtFechaNacimientoBen.Text != "")
                    eBenef.fecha_nacimiento_ben = txtFechaNacimientoBen.ToDateTime;
                else
                    eBenef.fecha_nacimiento_ben = null;
            else
                eBenef.fecha_nacimiento_ben = null;
            TextBox txtPorcentaje = (TextBox)rfila.FindControl("txtPorcentaje");
            if (txtPorcentaje != null)
                eBenef.porcentaje_ben = txtPorcentaje.Text == "" ? 0 : Convert.ToDecimal(txtPorcentaje.Text);
            eBenef.cod_persona = CodPersona;
            lista.Add(eBenef);
            ViewState[pUsuario.codusuario + "DatosBene"] = lista;

            if (eBenef.identificacion_ben.Trim() != "" && eBenef.nombre_ben.Trim() != null)
            {
                lstBeneficiarios.Add(eBenef);
            }
        }
        return lstBeneficiarios;
    }
    #endregion
    #region Parentescos
    protected void btnAgregarFilaParentesco_Click(object sender, EventArgs e)
    {
        List<PersonaParentescos> listaParentescos = RecorrerGrillaParentescos();
        listaParentescos.Add(new PersonaParentescos());

        gvParentescos.DataSource = listaParentescos;
        gvParentescos.DataBind();
    }
    protected void gvParentescos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlParentescoLocal = e.Row.FindControl("ddlParentesco") as DropDownListGrid;
            DropDownListGrid ddlTipoIdentificacion = e.Row.FindControl("ddlTipoIdentificacion") as DropDownListGrid;
            DropDownListGrid ddlActividadEconomica = e.Row.FindControl("ddlActividadEconomica") as DropDownListGrid;
            CheckBoxList chListaEstatus = e.Row.FindControl("chListaEstatus") as CheckBoxList;
            TextBox txtIdenficacion = e.Row.FindControl("txtIdenficacion") as TextBox;


            LlenarListasDesplegables(TipoLista.Parentesco, ddlParentescoLocal);
            LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoIdentificacion);
            LlenarListasDesplegables(TipoLista.Actividad_Negocio, ddlActividadEconomica);

            Label lblCodParentesco = e.Row.FindControl("lblCodParentesco") as Label;
            if (!string.IsNullOrWhiteSpace(lblCodParentesco.Text))
            {
                ddlParentescoLocal.SelectedValue = lblCodParentesco.Text;
            }

            Label lblTipoIdentificacion = e.Row.FindControl("lblTipoIdentificacion") as Label;
            if (!string.IsNullOrWhiteSpace(lblTipoIdentificacion.Text))
            {
                ddlTipoIdentificacion.SelectedValue = lblTipoIdentificacion.Text;
            }

            Label lblCodigoActividadEconomica = e.Row.FindControl("lblCodigoActividadEconomica") as Label;
            if (!string.IsNullOrWhiteSpace(lblCodigoActividadEconomica.Text))
            {
                ddlActividadEconomica.SelectedValue = lblCodigoActividadEconomica.Text;
            }

            List<PersonaParentescos> listaParentescos = (List<PersonaParentescos>)Session["lstParentescos"];
            if (listaParentescos != null)
            {
                if (listaParentescos.Count > 0)
                {
                    PersonaParentescos persona = new PersonaParentescos();
                    persona = listaParentescos.Where(x => x.identificacion == txtIdenficacion.Text).FirstOrDefault();
                    if (persona != null)
                    {
                        if (persona.empleado_entidad == 1)
                            chListaEstatus.Items[0].Selected = true;
                        if (persona.miembro_administracion == 1)
                            chListaEstatus.Items[1].Selected = true;
                        if (persona.miembro_control == 1)
                            chListaEstatus.Items[2].Selected = true;
                        if (persona.es_pep == 1)
                            chListaEstatus.Items[3].Selected = true;
                    }
                }
            }
        }
    }
    protected void gvParentescos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string consecutivoParaBorrar = gvParentescos.DataKeys[e.RowIndex].Value.ToString();

            if (!string.IsNullOrWhiteSpace(consecutivoParaBorrar))
            {
                _afiliacionServicio.EliminarPersonaParentesco(Convert.ToInt64(consecutivoParaBorrar), Usuario);
                var a = 1;
            }

            List<PersonaParentescos> listaParentescos = RecorrerGrillaParentescos();
            listaParentescos.RemoveAt(e.RowIndex);

            if (listaParentescos.Count <= 0)
            {
                listaParentescos.Add(new PersonaParentescos());
            }

            gvParentescos.DataSource = listaParentescos;
            gvParentescos.DataBind();
        }
        catch (Exception)
        {
            lblerror.Text = ("No se pudo borrar el registro del parentesco!.");
            RegistrarPostBack();
        }
    }
    List<PersonaParentescos> RecorrerGrillaParentescos(bool filtrarValidos = false, long pCod_Persona = 0)
    {
        List<PersonaParentescos> listaParentescos = new List<PersonaParentescos>();

        foreach (GridViewRow row in gvParentescos.Rows)
        {
            PersonaParentescos parentesco = new PersonaParentescos();

            string consecutivo = gvParentescos.DataKeys[row.RowIndex].Value.ToString();
            if (!string.IsNullOrWhiteSpace(consecutivo))
            {
                parentesco.consecutivo = Convert.ToInt64(consecutivo);
            }

            DropDownListGrid ddlParentescoLocal = row.FindControl("ddlParentesco") as DropDownListGrid;
            if (!string.IsNullOrWhiteSpace(ddlParentescoLocal.SelectedValue))
            {
                parentesco.codigoparentesco = Convert.ToInt64(ddlParentescoLocal.SelectedValue);
            }

            DropDownListGrid ddlTipoIdentificacion = row.FindControl("ddlTipoIdentificacion") as DropDownListGrid;
            if (!string.IsNullOrWhiteSpace(ddlTipoIdentificacion.SelectedValue))
            {
                parentesco.codigotipoidentificacion = Convert.ToInt64(ddlTipoIdentificacion.SelectedValue);
            }

            DropDownListGrid ddlActividadEconomica = row.FindControl("ddlActividadEconomica") as DropDownListGrid;
            if (!string.IsNullOrWhiteSpace(ddlActividadEconomica.SelectedValue))
            {
                parentesco.codigoactividadnegocio = ddlActividadEconomica.SelectedValue;
            }

            TextBox txtIdenficacion = row.FindControl("txtIdenficacion") as TextBox;
            parentesco.identificacion = txtIdenficacion.Text;

            TextBox txtNombres = row.FindControl("txtNombres") as TextBox;
            parentesco.nombresapellidos = txtNombres.Text;

            TextBox txtEmpresa = row.FindControl("txtEmpresa") as TextBox;
            parentesco.empresa = txtEmpresa.Text;

            TextBox txtCargo = row.FindControl("txtCargo") as TextBox;
            parentesco.cargo = txtCargo.Text;

            CheckBoxList chListaEstatus = row.FindControl("chListaEstatus") as CheckBoxList;
            foreach (ListItem item in chListaEstatus.Items)
            {
                if (item.Value == "0" && item.Selected == true)
                    parentesco.empleado_entidad = 1;
                else if (item.Value == "0" && item.Selected == false)
                    parentesco.empleado_entidad = 0;

                if (item.Value == "1" && item.Selected == true)
                    parentesco.miembro_administracion = 1;
                else if (item.Value == "1" && item.Selected == false)
                    parentesco.miembro_administracion = 0;

                if (item.Value == "2" && item.Selected == true)
                    parentesco.miembro_control = 1;
                else if (item.Value == "2" && item.Selected == false)
                    parentesco.miembro_control = 0;

                if (item.Value == "3" && item.Selected == true)
                    parentesco.es_pep = 1;
                if (item.Value == "3" && item.Selected == false)
                    parentesco.es_pep = 0;
            }
            if (parentesco.miembro_control == 1 && parentesco.miembro_administracion == 1)
            {
                lblerror.Text = ("El familiar registrado no puede ser miembro de administración y control a la vez");
                parentesco.miembro_administracion = 0;
                parentesco.miembro_control = 0;
            }

            TextBox txtIngresoMensual = row.FindControl("txtIngresoMensual") as TextBox;
            if (!string.IsNullOrWhiteSpace(txtIngresoMensual.Text))
            {
                parentesco.ingresomensual = Convert.ToDecimal(txtIngresoMensual.Text);
            }

            parentesco.codigopersona = pCod_Persona;
            // Si solo estoy recorriendo la grilla para agregar un registro mas, no valido nada y agrego todo
            // Si si estoy validando (Porque estoy guardando o modificando) filtro los validos
            if (!filtrarValidos || (parentesco.codigoparentesco > 0 && parentesco.codigotipoidentificacion > 0 && !string.IsNullOrWhiteSpace(txtNombres.Text) && !string.IsNullOrWhiteSpace(txtIdenficacion.Text)))
            {
                listaParentescos.Add(parentesco);
            }
        }

        return listaParentescos;
    }
    #endregion
    protected string FormatoFecha()
    {
        Configuracion conf = new Configuracion();
        return conf.ObtenerFormatoFecha();
    }
    public static int GetAge(DateTime birthDate)
    {
        return (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.242199);
    }
    public void btnGuardarBeneficiarios_click(object sender, EventArgs e)
    {
        Conyuge vConyuge = new Conyuge();
        ConyugeService ConyugeServicio = new ConyugeService();
        Persona1Service persona1Servicio = new Persona1Service();

        Usuario pUsuario = (Usuario)Session["usuario"];
        Persona1 Entidad = new Persona1();

        if (Session[Usuario.codusuario + "cod_per"] != null)
        {
            string cod_per = Session[Usuario.codusuario + "cod_per"].ToString();
            Entidad.cod_persona = Convert.ToInt64(cod_per);

            string ec = "";
            if (Session["estadoCivil"] != null)
                ec = Session["estadoCivil"] as string;

            //Conyugue
            if (ec == "1" || ec == "3")
                GuardarConyuge();
            else
            {
                LimpiarConyugue();
                txtIdent_cony.Text = "";
                txtIdent_cony.Enabled = true;
            }

            BeneficiarioService ServicesBeneficiarios = new BeneficiarioService();

            // GRABADO DE GRILLAS
            //BENEFICIARIOS
            List<Beneficiario> lstBeneficiarios = ObtenerListaBeneficiarios(Entidad.cod_persona);
            
            //PARENTESCOS          
            List<PersonaParentescos> listaParentescos = RecorrerGrillaParentescos(true, Entidad.cod_persona);

            string pError = string.Empty;
            bool result = ServicesBeneficiarios.GrabarDatosTabBeneficiario(ref pError, ref lstBeneficiarios, ref listaParentescos, Usuario);
            if (!result)
            {
                if (!string.IsNullOrEmpty(pError))
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Beneficiario", "alert('" + pError + "')", true);
                    return;
                }
            }
            else
            {
                // ESTO SE AGREGA PARA QUE RECARGUE LA GRIDVIEW Y SI EN CASO ESTE REGRESANDO AL TAB, NO ESTE CREANDO CADA VEZ QUE SALGA.
                if (lstBeneficiarios != null)
                    if (lstBeneficiarios.Count > 0)
                    {
                        gvBeneficiarios.DataSource = lstBeneficiarios;
                        gvBeneficiarios.DataBind();
                    }

                if (listaParentescos != null)
                    if (listaParentescos.Count > 0)
                    {
                        gvParentescos.DataSource = listaParentescos;
                        gvParentescos.DataBind();
                    }
            }
        }
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Beneficiario", "alert('Información de Beneficiarios Actualizada con exito!')", true);

    }
}