using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Drawing.Imaging;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
//using Subgurim.Controles;
using System.Text.RegularExpressions;
using Xpinn.Asesores.Entities;
using Xpinn.Caja.Entities;
using Xpinn.Caja.Services;
using Xpinn.ActivosFijos.Services;
using Xpinn.ActivosFijos.Entities;
using Xpinn.Comun.Entities;
using System.Configuration;
using System.Globalization;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class NuevoPersona : GlobalWeb
{
    private Xpinn.Contabilidad.Services.TerceroService DatosClienteServicio = new Xpinn.Contabilidad.Services.TerceroService();
    private Xpinn.Aportes.Services.AfiliacionServices _afiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    private ActividadesServices ActiServices = new ActividadesServices();
    private BeneficiarioService BeneficiarioServicio = new BeneficiarioService();
    private Xpinn.FabricaCreditos.Entities.Georeferencia pGeo = new Xpinn.FabricaCreditos.Entities.Georeferencia();
    private Xpinn.FabricaCreditos.Services.GeoreferenciaService Georeferencia = new Xpinn.FabricaCreditos.Services.GeoreferenciaService();
    private FormatoDocumentoServices DocumentoService = new FormatoDocumentoServices();
    private ImagenesService ImagenSERVICE = new ImagenesService();
    Persona1Service ServicePersona = new Persona1Service();
    Usuario pUsuario = new Usuario();
    UsuarioAtribuciones atrusuarios = new UsuarioAtribuciones();
    UsuarioAtribucionesService atribuciones = new UsuarioAtribucionesService();
    String Operacion;
    ParametrizacionProcesoAfilicacionService _paramProceso = new ParametrizacionProcesoAfilicacionService();

    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[_afiliacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(_afiliacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(_afiliacionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.eventoGuardar += GuardarAdicional_Click;
            ctlFormatos.eventoClick += btnImpresion_Click;
        }
        catch //(Exception ex)
        {
            //BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma + "A", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ctlFormatos.Inicializar("1");
            cargarListas();

            if (Session[_afiliacionServicio.CodigoPrograma + ".id"] != null)
            {
                string id = Session[_afiliacionServicio.CodigoPrograma + ".id"].ToString();
                Session[Usuario.codusuario + "cod_per"] = id;
                cargar_cabecera(id);
                cargar_datos();
            }
            else
            {
                Session[Usuario.codusuario + "cod_per"] = null;
            }
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

    #region eventos tab
    protected void btnTab1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Persona.aspx");
    }

    protected void btnTab2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Laboral.aspx");
    }
    protected void btnTab3_Click(object sender, EventArgs e)
    {
        Response.Redirect("Beneficiarios.aspx");
    }
    protected void btnTab4_Click(object sender, EventArgs e)
    {
        Response.Redirect("Economica.aspx");
    }
    protected void btnTab5_Click(object sender, EventArgs e)
    {
        if (almacenarDatos())
            Response.Redirect("Adicional.aspx");
    }
    #endregion

    #region metodos de carga de datos
    public void cargarListas()
    {
        String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
        List<Persona1> lstDatosSolicitud = new List<Persona1>();
        List<Persona1> lstDatosFiltrado = new List<Persona1>();

        InicializarActividades();
        //Listar Cargos Peps
        PoblarLista("CARGOS_PEP", "COD_CARGO, DESCRIPCION", "", "1", ddlCargoPEPS);
        obtenerControlesAdicionales();


        PoblarLista("GR_PERFIL_RIESGO", "COD_PERFIL, DESCRIPCION", "TIPO_PERSONA IN ('J','T')", "1", ddlAsociadosEspeciales);


        //ListaSolicitada = "AsociadosEspeciales";
        //lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
        //ddlAsociadosEspeciales.DataSource = lstDatosSolicitud;
        //ddlAsociadosEspeciales.DataTextField = "ListaDescripcion";
        //ddlAsociadosEspeciales.DataValueField = "ListaId";
        //ddlAsociadosEspeciales.DataBind();
        //ddlAsociadosEspeciales.Items.Insert(0, new ListItem("Seleccione un item", ""));
        //ddlAsociadosEspeciales.SelectedIndex = 0;

        PoblarLista("asejecutivos", "ICODIGO, QUITARESPACIOS(Substr(snombre1 || ' ' || snombre2 || ' ' || sapellido1 || ' ' || sapellido2, 0, 240))", "", "", ddlAsesor);

        List<Estado_Persona> lstEstado = new List<Estado_Persona>();
        Estado_Persona pEntidad = new Estado_Persona();
        lstEstado = _afiliacionServicio.ListarEstadoPersona(pEntidad, (Usuario)Session["usuario"]);
        if (lstEstado.Count > 0)
        {
            ddlEstadoAfi.DataSource = lstEstado;
            ddlEstadoAfi.DataTextField = "descripcion";
            ddlEstadoAfi.DataValueField = "estado";
            ddlEstadoAfi.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlEstadoAfi.SelectedIndex = 0;
            ddlEstadoAfi.DataBind();
        }
        PoblarLista("periodicidad", ddlPeriodicidad);
        /******************EMPRESAS RECAUDO ****************/
        //Listar Empresa Recaudo
        string id = Session[Usuario.codusuario + "cod_per"].ToString();
        PersonaEmpresaRecaudoServices infoEmpresaRecaudo = new PersonaEmpresaRecaudoServices();
        List<PersonaEmpresaRecaudo> LstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
        LstEmpresaRecaudo = infoEmpresaRecaudo.ListarPersonaEmpresaRecaudo(Convert.ToInt64(id), (Usuario)Session["usuario"]);
        Session["gvEmpresaRecaudoN"] = LstEmpresaRecaudo;
        RECUPERAR_EMPRESAS_NOMINA();
    }
    private List<Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Persona1Service persona1Servicio = new Persona1Service();
        List<Persona1> lstDatosSolicitud = new List<Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = persona1Servicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }
    public void cargar_datos()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];

        Persona1 Entidad = new Persona1();
        if (Session[Usuario.codusuario + "cod_per"] != null)
        {
            string id = (string)Session[Usuario.codusuario + "cod_per"].ToString();
            Entidad.cod_persona = Convert.ToInt64(id);
            Entidad.seleccionar = "Cod_persona";
            Entidad.soloPersona = 1;
            Entidad = ServicePersona.ConsultarPersona1Param(Entidad, (Usuario)Session["usuario"]);

            if (Entidad != null)
            {
                //Documentos
                //ObtenerDatosDocumentos(Convert.ToInt64(id));
                //Actividades SocioCulturales

                List<Actividades> LstActividad = new List<Actividades>();
                LstActividad = ActiServices.ConsultarActividad(Convert.ToInt64(id), (Usuario)Session["usuario"]);
                if (LstActividad.Count > 0)
                {
                    if ((LstActividad != null) || (LstActividad.Count != 0))
                    {
                        //ValidarPermisosGrilla(gvActividades);
                        gvActividades.DataSource = LstActividad;
                        gvActividades.DataBind();
                    }
                    Session[pUsuario.codusuario + "DatosActividad"] = LstActividad;
                }

                //Informacion Adicional
                List<InformacionAdicional> LstInformacion = new List<InformacionAdicional>();
                InformacionAdicionalServices infoService = new InformacionAdicionalServices();
                LstInformacion = infoService.ListarPersonaInformacion(Convert.ToInt64(id), "N", (Usuario)Session["usuario"]);
                if (LstInformacion.Count > 0)
                {
                    gvInfoAdicional.DataSource = LstInformacion;
                    gvInfoAdicional.DataBind();
                }
                //Afiliacion
                ObtenerDatosAfiliacion(Convert.ToInt64(id), Entidad);

                //Validación de tab laboral 
                if (Entidad.ocupacionApo > 0)
                {
                    string script = @"  function Alertando(valor) {
                                            valor = " + Entidad.ocupacionApo.ToString() + @"
                                            if (valor == 1 || valor == 2 || valor == 3) 
                                            { window.parent.$('#lilaboral').removeClass('clsInactivos'); }
                                            else
                                            {

                                                window.parent.$('#lilaboral').removeClass('clsInactivos').addClass('clsInactivos');
                                            }                                        
                                    }";

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Ocupacion", script, true);
                }
            }
        }
    }

    public void obtenerControlesAdicionales()
    {
        InformacionAdicionalServices informacion = new InformacionAdicionalServices();
        InformacionAdicional pInfo = new InformacionAdicional();
        List<InformacionAdicional> lstControles = new List<InformacionAdicional>();
        string tipo = "N";
        lstControles = informacion.ListarInformacionAdicional(pInfo, tipo, (Usuario)Session["usuario"]);
        if (lstControles.Count > 0)
        {
            gvInfoAdicional.DataSource = lstControles;
            gvInfoAdicional.DataBind();
        }
        ViewState.Add("ListaInfoAdicional", lstControles);
    }
    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
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
    protected List<InformacionAdicional> ObtenerListaInformacionAdicional()
    {
        List<InformacionAdicional> lstInformacionAdd = new List<InformacionAdicional>();

        foreach (GridViewRow rfila in gvInfoAdicional.Rows)
        {
            InformacionAdicional eActi = new InformacionAdicional();

            Label lblidinfadicional = (Label)rfila.FindControl("lblidinfadicional");

            if (lblidinfadicional != null)
                eActi.idinfadicional = Convert.ToInt32(lblidinfadicional.Text);

            Label lblcod_infadicional = (Label)rfila.FindControl("lblcod_infadicional");
            if (lblcod_infadicional != null)
                eActi.cod_infadicional = Convert.ToInt32(lblcod_infadicional.Text);

            Label lblopcionaActivar = (Label)rfila.FindControl("lblopcionaActivar");

            if (lblopcionaActivar != null)
            {
                if (lblopcionaActivar.Text == "1")//CARACTER
                {
                    TextBox txtCadena = (TextBox)rfila.FindControl("txtCadena");
                    if (txtCadena != null)
                        eActi.valor = txtCadena.Text;
                }
                else if (lblopcionaActivar.Text == "2")//FECHA
                {
                    fecha txtctlfecha = (fecha)rfila.FindControl("txtctlfecha");
                    if (txtctlfecha != null)
                        eActi.valor = txtctlfecha.Text;
                }
                else if (lblopcionaActivar.Text == "3") //NUMERO
                {
                    TextBox txtNumero = (TextBox)rfila.FindControl("txtNumero");
                    if (txtNumero != null)
                        eActi.valor = txtNumero.Text;
                }
                else if (lblopcionaActivar.Text == "4") // DROPDOWNLIST
                {
                    DropDownListGrid ddlDropdown = (DropDownListGrid)rfila.FindControl("ddlDropdown");
                    if (ddlDropdown != null)
                        eActi.valor = ddlDropdown.SelectedItem.Text;
                    if (ddlDropdown.Text != "")
                        eActi.valor = ddlDropdown.SelectedItem.Text;
                }
            }

            if (eActi.valor != "" && eActi.cod_infadicional != 0)
            {
                lstInformacionAdd.Add(eActi);
            }
        }
        return lstInformacionAdd;
    }
    protected void InicializarActividades()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];
        List<Actividades> lstActividades = new List<Actividades>();
        for (int i = gvActividades.Rows.Count; i < 5; i++)
        {
            Actividades eActi = new Actividades();
            eActi.idactividad = -1;
            eActi.fecha_realizacion = null;
            eActi.tipo_actividad = null;
            eActi.descripcion = "";
            eActi.participante = null;
            eActi.calificacion = "";
            eActi.duracion = "";
            lstActividades.Add(eActi);
        }
        gvActividades.DataSource = lstActividades;
        gvActividades.DataBind();
        Session[pUsuario.codusuario + "DatosActividad"] = lstActividades;

    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        Int64 idimagen = 0;
        bytes = ImagenSERVICE.DocumentosPersona(id, ref idimagen, (Usuario)Session["usuario"]);
        if (bytes != null)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=download.pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
    protected void gvDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //DropDownListGrid ddlDocumento = e.Row.FindControl("ddlDocumento") as DropDownListGrid;
            //List<FormatoDocumento> lstDatosSolicitud = DocumentoService.ListarFormatoDocumentoDrop(Usuario, 1, 0);


            //ddlDocumento.DataSource = lstDatosSolicitud;
            //ddlDocumento.DataTextField = "DESCRIPCION";
            //ddlDocumento.DataValueField = "COD_DOCUMENTO";
            //ddlDocumento.DataBind();


            //Label lblDocumento = e.Row.FindControl("lblCodDocumetno") as Label;
            //if (!string.IsNullOrWhiteSpace(lblDocumento.Text))
            //{
            //    ddlDocumento.SelectedValue = lblDocumento.Text;
            //}

        }
    }

    protected void RegistrarPostBack()
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:__doPostBack('', '');", true);
    }
    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        Usuario pUsuario = (Usuario)Session["usuario"];
        ObtenerListaActividades();
        List<Actividades> lstActividades = new List<Actividades>();

        if (Session[pUsuario.codusuario + "DatosActividad"] != null)
        {
            lstActividades = (List<Actividades>)Session[pUsuario.codusuario + "DatosActividad"];

            for (int i = 1; i <= 1; i++)
            {
                Actividades eActi = new Actividades();
                eActi.idactividad = -1;
                eActi.fecha_realizacion = null;
                eActi.tipo_actividad = null;
                eActi.descripcion = "";
                eActi.participante = null;
                eActi.calificacion = "";
                eActi.duracion = "";
                lstActividades.Add(eActi);
            }
            //gvActividades.PageIndex = gvActividades.PageCount;
            gvActividades.DataSource = lstActividades;
            gvActividades.DataBind();

            Session[pUsuario.codusuario + "DatosActividad"] = lstActividades;
        }
    }
    protected List<Actividades> ObtenerListaActividades()
    {

        Usuario pUsuario = (Usuario)Session["usuario"];

        List<Actividades> lstActividades = new List<Actividades>();
        List<Actividades> lista = new List<Actividades>();

        foreach (GridViewRow rfila in gvActividades.Rows)
        {
            Actividades eActi = new Actividades();
            Label lblactividad = (Label)rfila.FindControl("lblactividad");

            if (lblactividad != null)
                eActi.idactividad = Convert.ToInt32(lblactividad.Text);
            fecha txtfecha = (fecha)rfila.FindControl("txtfecha");
            if (txtfecha != null)
                if (txtfecha.Text != "")
                    eActi.fecha_realizacion = txtfecha.ToDateTime;
                else
                    eActi.fecha_realizacion = null;
            else
                eActi.fecha_realizacion = null;
            DropDownListGrid ddlActividad = (DropDownListGrid)rfila.FindControl("ddlActividad");
            if (ddlActividad.SelectedValue != null)
                eActi.tipo_actividad = Convert.ToInt32(ddlActividad.SelectedValue);
            TextBox txtDescripcion = (TextBox)rfila.FindControl("txtDescripcion");
            if (txtDescripcion != null)
                eActi.descripcion = Convert.ToString(txtDescripcion.Text);
            DropDownListGrid ddlParticipante = (DropDownListGrid)rfila.FindControl("ddlParticipante");
            if (ddlParticipante.SelectedValue != null)
                eActi.participante = Convert.ToInt32(ddlParticipante.SelectedValue);
            TextBox txtCalificacion = (TextBox)rfila.FindControl("txtCalificacion");
            if (txtCalificacion != null)
                eActi.calificacion = Convert.ToString(txtCalificacion.Text);
            TextBox txtDuracion = (TextBox)rfila.FindControl("txtDuracion");
            if (txtDuracion != null)
                eActi.duracion = Convert.ToString(txtDuracion.Text);
            //eActi.cod_persona = cod;
            lista.Add(eActi);
            Session[pUsuario.codusuario + "DatosActividad"] = lista;

            if (eActi.tipo_actividad.Value != 0 && eActi.participante.Value != 0 && eActi.fecha_realizacion != DateTime.MinValue)
            {
                lstActividades.Add(eActi);
            }
        }
        return lstActividades;
    }
    protected void gvActividades_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ActividadesServices ActService = new ActividadesServices();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlActividad = (DropDownList)e.Row.FindControl("ddlActividad");
            DropDownList ddlParticipante = (DropDownList)e.Row.FindControl("ddlParticipante");


            if (ddlActividad != null)
            {
                ddlActividad.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlActividad.Items.Insert(1, new ListItem("ACTIVIDAD", "1"));
                ddlActividad.Items.Insert(2, new ListItem("EVENTO", "2"));
                ddlActividad.Items.Insert(3, new ListItem("CURSO", "3"));
                ddlActividad.Items.Insert(3, new ListItem("EDUCACIÓN FINANCIERA", "4"));
                ddlActividad.Items.Insert(3, new ListItem("EDUCACIÓN E. SOLIDARIA", "5"));
                ddlActividad.SelectedIndex = 0;
                ddlActividad.DataBind();
            }

            if (ddlParticipante != null)
            {
                ddlParticipante.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlParticipante.Items.Insert(1, new ListItem("ASOCIADO", "1"));
                ddlParticipante.Items.Insert(2, new ListItem("FAMILIAR", "2"));
                ddlParticipante.SelectedIndex = 0;
                ddlParticipante.DataBind();
            }

            Label lblTipoActividad = (Label)e.Row.FindControl("lblTipoActividad");
            if (lblTipoActividad != null)
                ddlActividad.SelectedValue = lblTipoActividad.Text;

            Label lblParticipante = (Label)e.Row.FindControl("lblParticipante");
            if (lblParticipante != null)
                ddlParticipante.SelectedValue = lblParticipante.Text;

        }
    }
    protected void gvActividades_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Usuario pUsuario = (Usuario)Session["usuario"];

        int conseID = Convert.ToInt32(gvActividades.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaActividades();

        List<Actividades> LstActi;
        LstActi = (List<Actividades>)Session[pUsuario.codusuario + "DatosActividad"];

        if (conseID > 0)
        {
            try
            {
                foreach (Actividades acti in LstActi)
                {
                    if (acti.idactividad == conseID)
                    {

                        ActiServices.EliminarActividadPersona(conseID, (Usuario)Session["usuario"]);
                        LstActi.Remove(acti);
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
            LstActi.RemoveAt((gvActividades.PageIndex * gvActividades.PageSize) + e.RowIndex);
        }

        gvActividades.DataSourceID = null;
        gvActividades.DataBind();

        gvActividades.DataSource = LstActi;
        gvActividades.DataBind();

        Session[pUsuario.codusuario + "DatosActividad"] = LstActi;
    }
    protected void gvActividades_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = Convert.ToInt32(gvActividades.DataKeys[e.NewEditIndex].Values[0].ToString());

        if (conseID != 0)
        {
            gvActividades.EditIndex = e.NewEditIndex;
        }
        else
        {
            e.Cancel = true;
        }
    }
    protected void gvInfoAdicional_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInfoAdicional.PageIndex = e.NewPageIndex;
        if (ViewState["ListaInfoAdicional"] != null)
        {
            List<InformacionAdicional> LstInformacion = new List<InformacionAdicional>();
            LstInformacion = (List<InformacionAdicional>)ViewState["ListaInfoAdicional"];
            if (LstInformacion.Count > 0)
            {
                gvInfoAdicional.DataSource = LstInformacion;
                gvInfoAdicional.DataBind();
            }
        }
    }
    protected void gvInfoAdicional_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtCadena = (TextBox)e.Row.FindControl("txtCadena");
            TextBox txtNumero = (TextBox)e.Row.FindControl("txtNumero");
            fecha txtctlfecha = (fecha)e.Row.FindControl("txtctlfecha");

            DropDownListGrid ddlDropdown = (DropDownListGrid)e.Row.FindControl("ddlDropdown");

            //Llenando DropDown
            Label lblDropdown = (Label)e.Row.FindControl("lblDropdown");
            if (ddlDropdown != null)
            {
                string[] sDatos;
                sDatos = lblDropdown.Text.Split(',');
                if (sDatos.Count() > 0 && sDatos[0] != "")
                {
                    ddlDropdown.Items.Clear();
                    ddlDropdown.DataSource = sDatos;
                    ddlDropdown.DataBind();
                }
            }

            Label lblopcionaActivar = (Label)e.Row.FindControl("lblopcionaActivar");
            if (lblopcionaActivar != null)
            {
                if (lblopcionaActivar.Text == "1")//CARACTER
                {
                    txtCadena.Visible = true;
                }
                else if (lblopcionaActivar.Text == "2")//FECHA
                {
                    txtctlfecha.Visible = true;
                }
                else if (lblopcionaActivar.Text == "3") //NUMERO
                {
                    txtNumero.Visible = true;
                }
                else if (lblopcionaActivar.Text == "4") // DROPDOWNLIST
                {
                    ddlDropdown.Visible = true;
                }
            }

            //Capturando Valor del DropDown
            Label lblValorDropdown = (Label)e.Row.FindControl("lblValorDropdown");
            if (lblValorDropdown != null)
            {
                ddlDropdown.SelectedValue = lblValorDropdown.Text;
            }

            Label lblidinfadicional = (Label)e.Row.FindControl("lblcod_infadicional");
            if (lblValorDropdown != null)
            {
                // Codigo de tipo de informacion adicinal de WM
                string codigoInfoAdicionalBarCode = (string)ConfigurationManager.AppSettings["CodigoTipoInformacionAdicionalWorkManagement"];

                if (!string.IsNullOrWhiteSpace(codigoInfoAdicionalBarCode) && lblidinfadicional.Text.Trim() == codigoInfoAdicionalBarCode.Trim())
                {
                    txtCadena.Enabled = false;
                    txtNumero.Enabled = false;
                    txtctlfecha.Enabled = false;
                }
            }
        }
    }
    protected void chkAsociado_CheckedChanged(object sender, EventArgs e)
    {
        // panelAfiliacion.Enabled = chkAsociado.Checked;
        chkPEPS.Enabled = chkAsociado.Checked;
    }
    protected void ddlEstadoAfi_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlEstadoAfi.SelectedValue == "A")
        //    panelFecha.Enabled = false;
        //else
        //    panelFecha.Enabled = true;
    }
    protected void ddlFormPag_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (chkPEPS.Checked == false)
            panelPEPS.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";

        if (ddlEstadoAfi.SelectedValue != "R")
        {
            txtFechaRetiro.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
            lblFechaRet.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
        }

        if (ddlFormPag.SelectedItem.Value == "2" || ddlFormPag.SelectedItem.Text == "Nomina")
        {
            RECUPERAR_EMPRESAS_NOMINA();
            lblEmpresa.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "inline";
            ddlEmpresa.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "inline";
        }
        else
        {
            lblEmpresa.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
            ddlEmpresa.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
        }
    }
    protected void ckAfiliadoOtraOS_CheckedChanged(object sender, EventArgs e)
    {
        //if (ckAfiliadoOtraOS.Checked)
        //{
        //    txtOtraOS.Visible = true;
        //    lblOtraOS.Visible = true;
        //}
        //else
        //{
        //    txtOtraOS.Visible = false;
        //    lblOtraOS.Visible = false;
        //}
    }
    protected void ckCargosOS_CheckedChanged(object sender, EventArgs e)
    {
        //if (ckCargosOS.Checked)
        //{
        //    txtCargosDirectivos.Visible = true;
        //    lblCargoOS.Visible = true;
        //}
        //else
        //{
        //    txtCargosDirectivos.Visible = false;
        //    lblCargoOS.Visible = false;
        //}
    }


    void RECUPERAR_EMPRESAS_NOMINA()
    {
        if (Session["gvEmpresaRecaudo"] != null)
        {
            GridView gvEmpresaRecaudo = new GridView();
            gvEmpresaRecaudo = (GridView)Session["gvEmpresaRecaudo"];
            ddlEmpresa.Items.Clear();
            int cont = 0;
            ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            foreach (GridViewRow rFila in gvEmpresaRecaudo.Rows)
            {
                CheckBoxGrid chkSeleccionar = (CheckBoxGrid)rFila.FindControl("chkSeleccionar");
                Label lblDescripcion = (Label)rFila.FindControl("lblDescripcion");
                Label lblcodempresa = (Label)rFila.FindControl("lblcodempresa");
                if (chkSeleccionar.Checked)
                {
                    cont++;
                    if (lblDescripcion.Text != "" && lblcodempresa.Text != "")
                    {
                        ddlEmpresa.Items.Insert(cont, new ListItem(lblDescripcion.Text, lblcodempresa.Text));
                        ddlEmpresa.SelectedIndex = 1;
                    }
                }
            }
        }
        else
        {
            List<PersonaEmpresaRecaudo> listEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
            listEmpresaRecaudo = (List<PersonaEmpresaRecaudo>)Session["gvEmpresaRecaudoN"];
            ddlEmpresa.Items.Clear();
            int cont = 0;
            ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            foreach (PersonaEmpresaRecaudo empresa in listEmpresaRecaudo)
            {
                if (empresa.seleccionar)
                {
                    cont++;
                    ddlEmpresa.Items.Insert(cont, new ListItem(empresa.descripcion, Convert.ToString(empresa.cod_empresa)));
                    ddlEmpresa.SelectedIndex = 1;
                }
            }
            if (Session["DatosAfi"] != null)
            {
                Afiliacion Entidad = (Afiliacion)Session["DatosAfi"];
                if (Entidad.forma_pago == 2)
                {
                    if (Entidad.empresa_formapago != 0 && Entidad.empresa_formapago != null)
                    {
                        ddlEmpresa.SelectedValue = Entidad.empresa_formapago.ToString();
                    }
                    else
                    {
                        ddlEmpresa.SelectedValue = "0";
                    }
                }
                else
                {
                    ddlEmpresa.SelectedValue = "0";
                }
            }
        }
    }
    protected string FormatoFecha()
    {
        Configuracion conf = new Configuracion();
        return conf.ObtenerFormatoFecha();
    }
    private void ObtenerDatosAfiliacion(Int64 cod_persona, Persona1 entidad)
    {
        Afiliacion pAfili = new Afiliacion();
        chkAsociado.Checked = false;
        chkAsociado.Enabled = true;
        this.txtFechaAfili.Enabled = true;
        pAfili = _afiliacionServicio.ConsultarAfiliacion(cod_persona, (Usuario)Session["usuario"]);
        Session["DatosAfi"] = pAfili;
        if (pAfili.idafiliacion != 0)
        {
            txtcodAfiliacion.Text = Convert.ToString(pAfili.idafiliacion);
            chkAsociado.Checked = true;
            chkAsociado.Enabled = false;
            this.txtFechaAfili.Enabled = false;
            DatosAfi.Visible = true;
        }
        else
        {
            DatosAfi.Visible = false;
        }
        if (pAfili.fecha_afiliacion != DateTime.MinValue)
            this.txtFechaAfili.Text = Convert.ToDateTime(pAfili.fecha_afiliacion).ToString("yyyy-MM-dd");//GlobalWeb.gFormatoFecha  
        else
            this.txtFechaAfili.Text = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");//GlobalWeb.gFormatoFecha  
        if (pAfili.estado != "")
            ddlEstadoAfi.SelectedValue = pAfili.estado;

        if (pAfili.estado == "A")
            Session["Asociado"] = true;
        //ddlEstadoAfi_SelectedIndexChanged(ddlEstadoAfi, null);
        if (pAfili.fecha_retiro != DateTime.MinValue)
            this.txtFechaRetiro.Text = Convert.ToDateTime(pAfili.fecha_retiro).ToString("yyyy-MM-dd");
        if (pAfili.valor != 0)
            txtValorAfili.Text = Convert.ToString(pAfili.valor);
        if (pAfili.fecha_primer_pago != null)
            this.txtFecha1Pago.Text = Convert.ToDateTime(pAfili.fecha_primer_pago).ToString("yyyy-MM-dd");
        if (pAfili.cuotas != 0)
            txtCuotasAfili.Text = Convert.ToString(pAfili.cuotas);
        if (pAfili.cod_periodicidad != 0)
            ddlPeriodicidad.SelectedValue = Convert.ToString(pAfili.cod_periodicidad);
        if (pAfili.forma_pago != 0)
            ddlFormPag.SelectedValue = pAfili.forma_pago.ToString();
        else
            ddlFormPag.SelectedValue = "2";
        ddlFormPag_SelectedIndexChanged(ddlFormPag, null);

        if (pAfili.empresa_formapago != 0 && pAfili.empresa_formapago != null)
        {
            ddlEmpresa.SelectedValue = pAfili.empresa_formapago.ToString();
        }
        else
        {
            ddlEmpresa.SelectedValue = "0";
            try
            {
                if (entidad.lstEmpresaRecaudo != null && entidad.lstEmpresaRecaudo.Count > 0)
                    ddlEmpresa.SelectedValue = entidad.lstEmpresaRecaudo.ElementAt(0).idempresarecaudo.ToString();
            }
            catch (Exception)
            {
            }
        }

        if (pAfili.asist_ultasamblea != 0)
            chkAsistioUltAsamblea.Checked = true;
        if (pAfili.cod_asesor.HasValue)
        {
            ddlAsesor.SelectedValue = pAfili.cod_asesor.ToString();
        }
        if (pAfili.cod_asociado_especial.HasValue)
            ddlAsociadosEspeciales.SelectedValue = pAfili.cod_asociado_especial.Value.ToString();
        chkPEPS.Checked = pAfili.Es_PEPS;
        ddlCargoPEPS.SelectedValue = pAfili.cargo_PEPS;
        txtInstitucion.Text = pAfili.institucion;
        if (pAfili.fecha_vinculacion_PEPS != null)
            this.txtFechaVinculacionPEPS.Text = Convert.ToDateTime(pAfili.fecha_vinculacion_PEPS).ToString("yyyy-MM-dd");
        if (pAfili.fecha_desvinculacion_PEPS != null)
            this.txtFechaDesVinculacionPEPS.Text = Convert.ToDateTime(pAfili.fecha_desvinculacion_PEPS).ToString("yyyy-MM-dd");
        chkAdmiRecursosPublicos.Checked = pAfili.Administra_recursos_publicos;
        chkAsociado_CheckedChanged(chkAsociado, null);
        txtNoAsistencias.Text = pAfili.numero_asistencias.ToString();

        chkMiembroAministracion.Checked = pAfili.Miembro_administracion;
        ChkMiembroControl.Checked = pAfili.Miembro_control;

        if (!chkPEPS.Checked)
        {
            panelPEPS.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
            panelPEPS.Visible = false;
        }
    }
    private void Grabar_Datos_afiliacion()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];


        AfiliacionServices afiliacionService = new AfiliacionServices();
        Afiliacion afili = new Afiliacion();

        if (txtcodAfiliacion.Text != "")
            afili.idafiliacion = Convert.ToInt64(txtcodAfiliacion.Text);
        else
            afili.idafiliacion = 0;

        afili.cod_persona = Convert.ToInt64(Session[Usuario.codusuario + "cod_per"].ToString());

        if (txtFechaAfili.Text != "")
            afili.fecha_afiliacion = Convert.ToDateTime(txtFechaAfili.Text);

        if (ddlEstadoAfi.SelectedIndex != 0)
            afili.estado = ddlEstadoAfi.SelectedValue;

        if (ddlEstadoAfi.SelectedValue == "A")
            afili.fecha_retiro = DateTime.MinValue;
        else
        {
            if (txtFechaRetiro.Text != "")
                afili.fecha_retiro = Convert.ToDateTime(txtFechaRetiro.Text);
            else
                afili.fecha_retiro = DateTime.MinValue;
        }

        if (txtValorAfili.Text != "0")
            afili.valor = Convert.ToDecimal(txtValorAfili.Text.Replace(".", ""));
        else
            afili.valor = 0;

        if (ddlFormPag.SelectedValue != "")
            afili.forma_pago = Convert.ToInt32(ddlFormPag.SelectedValue);
        else
            afili.forma_pago = 0;

        if ((ddlEmpresa.SelectedValue != "" || ddlEmpresa.SelectedValue != "0") && ddlFormPag.SelectedValue == "2")
            afili.empresa_formapago = Convert.ToInt32(ddlEmpresa.SelectedValue);
        else
            afili.empresa_formapago = 0;

        if (txtFecha1Pago.Text != "")
            afili.fecha_primer_pago = Convert.ToDateTime(txtFecha1Pago.Text);
        else
            afili.fecha_primer_pago = DateTime.MinValue;

        if (txtCuotasAfili.Text != "")
            afili.cuotas = Convert.ToInt32(txtCuotasAfili.Text);
        else
            afili.cuotas = 0;

        if (ddlPeriodicidad.SelectedValue != "")
            afili.cod_periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
        else
            afili.cod_periodicidad = 0;

        if (chkAsistioUltAsamblea.Checked)
            afili.asist_ultasamblea = 1;
        else
            afili.asist_ultasamblea = 0;

        if (!String.IsNullOrWhiteSpace(txtNoAsistencias.Text))
            afili.numero_asistencias = Convert.ToInt32(txtNoAsistencias.Text);

        if (ddlAsociadosEspeciales.SelectedValue != "")
            afili.cod_asociado_especial = Convert.ToInt32(ddlAsociadosEspeciales.SelectedValue);

        afili.Es_PEPS = chkPEPS.Checked;
        if (chkAdmiRecursosPublicos.Checked)
            afili.Administra_recursos_publicos = true;
        else
            afili.Administra_recursos_publicos = false;

        if (ChkMiembroControl.Checked)
            afili.Miembro_control = true;
        else
            afili.Miembro_control = false;

        if (chkMiembroAministracion.Checked)
            afili.Miembro_administracion = true;
        else
            afili.Miembro_administracion = false;

        afili.cargo_PEPS = ddlCargoPEPS.SelectedValue;
        afili.institucion = txtInstitucion.Text;
        if (txtFechaVinculacionPEPS.Text != "")
            afili.fecha_vinculacion_PEPS = Convert.ToDateTime(txtFechaVinculacionPEPS.Text);
        if (txtFechaDesVinculacionPEPS.Text != "")
            afili.fecha_desvinculacion_PEPS = Convert.ToDateTime(txtFechaDesVinculacionPEPS.Text);

        if (!string.IsNullOrWhiteSpace(ddlAsesor.SelectedValue))
        {
            afili.cod_asesor = Convert.ToInt64(ddlAsesor.SelectedValue);
        }

        afili.entidad_externa = txtOtraOS.Text != null && txtOtraOS.Text != "" ? txtOtraOS.Text : "";
        afili.cargo_directivo = txtCargosDirectivos.Text != null && txtCargosDirectivos.Text != "" ? txtCargosDirectivos.Text : "";

        if (txtcodAfiliacion.Text != "")
        {
            if (txtFechaAfili.Text != "" && ddlEstadoAfi.SelectedIndex != 0)
            {

                if (Convert.ToDateTime(txtFechaAfili.Text) == DateTime.MinValue)
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Fecha Afiliacion", "alert('Seleccione Fecha de Afiliacion Valida.')", true);

                afiliacionService.ModificarPersonaAfiliacion(afili, (Usuario)Session["usuario"]);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Guardo", "alert('Modifico la Información de Afiliación.')", true);

                // calificacion persona  perfil de riesgo
                bool insertPerfil = false;

                Xpinn.Riesgo.Entities.SegmentacionPerfil segmentacion = new Xpinn.Riesgo.Entities.SegmentacionPerfil();
                Xpinn.Riesgo.Data.SegmentacionPerfilData segementacionIndividualPersona = new Xpinn.Riesgo.Data.SegmentacionPerfilData();
                segmentacion.cod_persona = afili.cod_persona;
                //consulta persona 
                List<Xpinn.Riesgo.Entities.SegmentacionPerfil> segmetacionIndividual = new List<Xpinn.Riesgo.Entities.SegmentacionPerfil>();
                segmetacionIndividual = segementacionIndividualPersona.ListarUnapersonaXriesgo(segmentacion, (Usuario)Session["usuario"]);
                // califica persona
                Xpinn.Riesgo.Data.PerfilData califacar = new Xpinn.Riesgo.Data.PerfilData();
                //calificar persona
                List<Xpinn.Riesgo.Entities.SegmentacionPerfil> lsCalificada = califacar.Calificarpersona(segmetacionIndividual, (Usuario)Session["usuario"]);
                // insertar persona calificada
                Xpinn.Riesgo.Data.SegmentacionPerfilData insertarperfil = new Xpinn.Riesgo.Data.SegmentacionPerfilData();
                insertPerfil = insertarperfil.Updateperfiles(lsCalificada, Usuario);
                //consulta persona calificada
                if (insertPerfil)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Guardo", "alert('Modifico la Información de Afiliación.')", true);
                }

            }
        }
        else
        {
            if (txtFechaAfili.Text != "" && ddlEstadoAfi.SelectedIndex != 0)
            {
                afili = afiliacionService.CrearPersonaAfiliacion(afili, (Usuario)Session["usuario"]);
                if (afili.idafiliacion > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Guardo", "alert('Guardo la Información de Afiliación.')", true);

                    // calificacion persona  perfil de riesgo
                    bool insertPerfil = false;

                    Xpinn.Riesgo.Entities.SegmentacionPerfil segmentacion = new Xpinn.Riesgo.Entities.SegmentacionPerfil();
                    Xpinn.Riesgo.Data.SegmentacionPerfilData segementacionIndividualPersona = new Xpinn.Riesgo.Data.SegmentacionPerfilData();
                    segmentacion.cod_persona = afili.cod_persona;
                    //consulta persona 
                    List<Xpinn.Riesgo.Entities.SegmentacionPerfil> segmetacionIndividual = new List<Xpinn.Riesgo.Entities.SegmentacionPerfil>();
                    segmetacionIndividual = segementacionIndividualPersona.ListarUnapersonaXriesgo(segmentacion, (Usuario)Session["usuario"]);
                    // califica persona
                    Xpinn.Riesgo.Data.PerfilData califacar = new Xpinn.Riesgo.Data.PerfilData();
                    //calificar persona
                    List<Xpinn.Riesgo.Entities.SegmentacionPerfil> lsCalificada = califacar.Calificarpersona(segmetacionIndividual, (Usuario)Session["usuario"]);
                    // insertar persona calificada
                    Xpinn.Riesgo.Data.SegmentacionPerfilData insertarperfil = new Xpinn.Riesgo.Data.SegmentacionPerfilData();
                    insertPerfil = insertarperfil.Insertarcalificacionperfiles(lsCalificada, Usuario);
                    //consulta persona calificada
                    if (insertPerfil)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Guardo", "alert('Modifico la Información de Afiliación.')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Alerta", "No se pudo guardar la afilliación, vrifique los datos y las fechas del cierre de personas", true);
                }
            }
        }
    }
    protected void ActListEmpresa_click(object sender, EventArgs e)
    {
        ddlEmpresa.Items.Clear();
        //Se cargara la forma y empresa de pago
        RECUPERAR_EMPRESAS_NOMINA();

        if (Session["DatosAfi"] != null)
        {
            Afiliacion Entidad = (Afiliacion)Session["DatosAfi"];
            if (Entidad.forma_pago == 2)
            {
                if (Entidad.empresa_formapago != 0 && Entidad.empresa_formapago != null)
                {
                    ddlEmpresa.SelectedValue = Entidad.empresa_formapago.ToString();
                }
                else
                {
                    ddlEmpresa.SelectedValue = "0";
                }
            }
            else
            {
                ddlEmpresa.SelectedValue = "0";
            }
        }



        if (chkPEPS.Checked == false)
            panelPEPS.Attributes.CssStyle[HtmlTextWriterStyle.Display] = "none";
    }
    public Persona1 limpiarInfoLaboral()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];
        Persona1 Entidad = new Persona1();

        Entidad.tipo_empresa = null;
        Entidad.empresa = null;
        Entidad.nit_empresa = null;
        Entidad.act_ciiu_empresa = null;
        Entidad.ActividadEconomicaEmpresaStr = null;
        Entidad.lstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();

        Entidad.codcargo = 0;
        Entidad.codtipocontrato = 0;
        Entidad.fecha_ingresoempresa = Convert.ToDateTime("01/01/0001");
        Entidad.antiguedadlugarempresa = 0;
        Entidad.sector = null;
        Entidad.cod_nomina_empleado = null;
        Entidad.zona = null;
        Entidad.idescalafon = 0;
        Entidad.empleado_entidad = 0;
        Entidad.jornada_laboral = 0;
        Entidad.ubicacion_empresa = null;
        Entidad.direccionempresa = "";
        Entidad.ciudad = null;
        Entidad.CelularEmpresa = null;
        //Entidad.email_empresa = txtWebEmp.Text.ToString();
        Entidad.telefonoempresa = null;
        Entidad.cod_persona = Convert.ToInt64(Session[Usuario.codusuario + "cod_per"].ToString());
        Entidad.fecultmod = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
        Entidad.usuultmod = pUsuario.nombre;

        return Entidad;
    }
    #endregion


    private bool almacenarDatos()
    {
        Persona1 Entidad = new Persona1();

        Entidad.lstActividad = new List<Actividades>();
        Entidad.lstActividad = ObtenerListaActividades();

        Entidad.lstInformacion = new List<InformacionAdicional>();
        Entidad.lstInformacion = ObtenerListaInformacionAdicional();

        // Guarda/Modifica
        if (Session[Usuario.codusuario + "cod_per"] != null)
        {
            Entidad.fecultmod = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
            Entidad.usuultmod = Usuario.nombre;
            try
            {
                Entidad.cod_persona = Convert.ToInt64(Session[Usuario.codusuario + "cod_per"].ToString());
                Persona1 pResult = ServicePersona.TabAdicional(Entidad, Usuario);
                if (Convert.ToInt32(Session["ocupacion"]) == 2)
                {
                    Persona1 independiente = new Persona1();
                    independiente = limpiarInfoLaboral();
                    ServicePersona.TabLaboral(independiente, (Usuario)Session["usuario"]);
                }

                if (Convert.ToInt32(Session["estadoCivil"]) != 1 && Convert.ToInt32(Session["estadoCivil"]) != 3)
                {
                    ConyugeService ConService = new ConyugeService();
                    ConService.EliminarConyuge(Convert.ToInt64(Session[Usuario.codusuario + "Cod_persona_conyuge"]), (Usuario)Session["usuario"]);
                }

                if (pResult != null)
                {
                    if (pResult.lstActividad != null)
                    {
                        // RECARGANDO LA GRIDVIEW DE ACTIVIDADES.
                        if (pResult.lstActividad.Count > 0)
                        {
                            gvActividades.DataSource = pResult.lstActividad;
                            gvActividades.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblerror.Text = ("Error al modificar los datos de la información Adicional. " + ex.Message);
                return false;
            }
            Session[Usuario.codusuario + "cod_per"] = Entidad.cod_persona;


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Interfaz WORKMANAGEMENT
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #region WORKMANAGEMENT

            // Traer los datos de la persona
            Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            vPersona1 = persona1Servicio.ConsultarPersona1(Entidad.cod_persona, Usuario);
            Session[Usuario.codusuario + "Cod_persona"] = Entidad.cod_persona;

            // Codigo de tipo de informacion adicinal de WM
            string codigoInfoAdicionalBarCode = (string)ConfigurationManager.AppSettings["CodigoTipoInformacionAdicionalWorkManagement"];

            // Parametro para habilitar operaciones con WM
            General general = ConsultarParametroGeneral(45);
            if (general != null && general.valor.Trim() == "1" && !string.IsNullOrWhiteSpace(codigoInfoAdicionalBarCode))
            {
                try
                {
                    InterfazWorkManagement interfazWM = new InterfazWorkManagement(Usuario);

                    // Homologar de codigo de tipo de identificacion a la que maneja el WM
                    // Si parece estupido pero el WM no maneja codigo solo descripcion 
                    switch (vPersona1.tipo_identificacion)
                    {
                        case 1:
                            vPersona1.tipo_identificacion_descripcion = "Cedula de Ciudadania";
                            break;
                        case 2:
                            vPersona1.tipo_identificacion_descripcion = "Nit";
                            break;
                        case 3:
                            vPersona1.tipo_identificacion_descripcion = "Nit Extranjero";
                            break;
                        case 4:
                            vPersona1.tipo_identificacion_descripcion = "Cedula de Extranjeria";
                            break;
                        case 5:
                            vPersona1.tipo_identificacion_descripcion = "Tarjeta de Identidad";
                            break;
                        default:
                            vPersona1.tipo_identificacion_descripcion = "Cedula de Ciudadania";
                            break;
                    }

                    // Homologar nombre ciudad
                    if (vPersona1.codciudadresidencia.HasValue)
                    {
                        Persona1Service personaSer = new Persona1Service();
                        Persona1 departamentoBuscado = personaSer.BuscarDepartamentoPorCodigoCiudad(vPersona1.codciudadresidencia.Value, Usuario);

                        vPersona1.nombre_ciudad = departamentoBuscado.nombre_ciudad;
                    }

                    Tuple<bool, string, bool> response = interfazWM.InteractuarRegistroFormularioHistoriaAsociado(vPersona1);
                    Session["imagen"] = true;

                    // Si fue exitoso entro
                    if (response.Item1 || response.Item2.Trim() != "")
                    {
                        InformacionAdicionalServices infoSer = new InformacionAdicionalServices();
                        InformacionAdicional pAdicional = new InformacionAdicional
                        {
                            cod_persona = vPersona1.cod_persona,
                            cod_infadicional = Convert.ToInt32(codigoInfoAdicionalBarCode),
                            valor = response.Item2
                        };

                        // Miro si creo o modifico
                        if (response.Item3)
                        {
                            // Creo la informacion adicional para esta persona con el barCode
                            pAdicional = infoSer.CrearPersona_InfoAdicional(pAdicional, Usuario);
                        }
                        else
                        {
                            // Modifica la informacion adicional para esta persona con el barCode
                            infoSer.ModificarPersona_InfoAdicional(pAdicional, Usuario);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblerror.Text = "Se presentó un problema: " + ex.Message;
                    return false;
                }
            }

            #endregion


            if (chkPEPS.Checked)
                Session["peps"] = true;
        }
        else
        {
            lblerror.Text = ("No se encontro se pudo realizar el registro o actualizacion de la información Adicional");
            return false;
        }

        Session["gvEmpresaRecaudo"] = null;
        Session["DatosAfi"] = null;
        Session.Remove("ocupacion");
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Guardado", "alert('Guardo la Información adicional')   ", true);
        return true;
    }


    private void guardarAfiliacion()
    {

        if (chkAsociado.Checked && !string.IsNullOrEmpty(txtFechaAfili.Text))
        {
            DateTime fechaAfilia = Convert.ToDateTime(txtFechaAfili.Text);

            if (fechaAfilia <= DateTime.Now)
            {
                Grabar_Datos_afiliacion();

                string script = @"var btn = window.parent.document.getElementById('cphMain_BtnDireccionar');
                                    if(btn)
                                    btn.click();";

                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "ToggleScript", script, true);

                redireccionar();
            }
            else
            {
                lblerror.Text = "La fecha de afiliación no puede superar la fecha actual";
            }
        }
    }


    protected void redireccionar()
    {
        bool redireccionImagen = false;
        bool editar = false;
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
        if (Session[_afiliacionServicio.CodigoPrograma + ".id"] != null)
        {
            editar = true;
        }

        ParametrizacionProcesoAfiliacion vParam = new ParametrizacionProcesoAfiliacion();
        vParam.lstParametros = _paramProceso.ListarParametrosProcesoAfiliacion((Usuario)Session["usuario"]).Where(x => x.cod_proceso != 1).ToList();
        Session["lstParametros"] = vParam.lstParametros;
        bool stop = false;
        int c = 0;

        #region control afiliación workflow 
        //Se ejecuta solo cuando la persona no tiene estado de asociado
        if (!Asociado)
        {
            //CONTROL DE RUTA PARA LA EVALUACIÓN 
            if (Session[_afiliacionServicio.CodigoPrograma + "next"] == null)
                Session[_afiliacionServicio.CodigoPrograma + "next"] = 3;
            registrarControl(3, cod_per);
            foreach (ParametrizacionProcesoAfiliacion redirect in vParam.lstParametros)
            {
                if (!stop)
                {
                    switch (redirect.cod_proceso)
                    {
                        case 4:
                            if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                            {
                                registrarControl(4, cod_per);
                                Session["cedula"] = Convert.ToString(Session["identificacion"]);
                                Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                                Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                                //_afiliacionServicio.notificarEmail(4, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                                Navegar("../../CuentasAportes/Nuevo.aspx");
                                stop = true;
                            }
                            break;
                        case 5:
                            if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                            {
                                registrarControl(5, cod_per);
                                Session[_afiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                                ImagenesService imagenService = new ImagenesService();
                                Session[imagenService.CodigoPrograma.ToString() + ".cod_persona"] = Session[Usuario.codusuario + "Cod_persona"].ToString();
                                Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                                Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                                //_afiliacionServicio.notificarEmail(5, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                                Navegar("../../ImagenesPersona/Nuevo.aspx");
                                stop = true;
                                _paramProceso.cambiarEstadoAsociado("", Convert.ToInt64(cod_per), (Usuario)Session["usuario"]);
                            }
                            break;
                        case 6:
                            if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                            {
                                registrarControl(6, cod_per);
                                Session[_afiliacionServicio.CodigoPrograma + ".id"] = cod_per;
                                Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                                Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                                //_afiliacionServicio.notificarEmail(6, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                                Navegar("../../../GestionRiesgo/ConsultasOFAC/Nuevo.aspx");
                                stop = true;
                                _paramProceso.cambiarEstadoAsociado("", Convert.ToInt64(cod_per), (Usuario)Session["usuario"]);
                            }
                            break;
                        case 7:
                            if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                            {
                                registrarControl(7, cod_per);
                                Int64 id = cod_per;
                                string codOpcion = "170901";
                                Session["CodOpcion"] = codOpcion;
                                Session[codOpcion.ToString() + ".id"] = id;
                                Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                                Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                                //_afiliacionServicio.notificarEmail(7, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                                Navegar("../../ProcesosAfiliacion/Nuevo.aspx");
                                stop = true;
                            }
                            break;
                        case 8:
                            if (Convert.ToBoolean(redirect.requerido) && !_paramProceso.controlRegistrado(redirect.cod_proceso, Convert.ToInt64(cod_per), (Usuario)Session["usuario"]))
                            {
                                registrarControl(8, cod_per);
                                Int64 id = cod_per;
                                string codOpcion = "170902";
                                Session["CodOpcion"] = codOpcion;
                                Session[codOpcion.ToString() + ".id"] = id;
                                Session[_afiliacionServicio.CodigoPrograma + "last"] = vParam.lstParametros[c - 1].cod_proceso;
                                Session[_afiliacionServicio.CodigoPrograma + "next"] = redirect.cod_proceso;
                                //_afiliacionServicio.notificarEmail(8, vParam.nombre, cod_per, redirect.asociado, redirect.asesor, redirect.otro, (Usuario)Session["usuario"]);
                                Navegar("../../AProcesosAfiliacion/Nuevo.aspx");
                                stop = true;
                            }
                            break;
                    }
                    if (stop)
                    {
                        break;
                    }
                    c++;
                }
            }
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


    protected void GuardarAdicional_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlFormPag.SelectedValue))
        {
            if (ddlFormPag.SelectedValue == "2")
            {
                if (string.IsNullOrEmpty(ddlEmpresa.SelectedValue) || ddlEmpresa.SelectedValue == "0")
                {
                    VerError("Debe seleccionar una empresa");
                    return;
                }
            }
        }
        if (almacenarDatos())
            if (chkAsociado.Checked)
                guardarAfiliacion();
    }

    protected void chkAsociado_CheckedChanged1(object sender, EventArgs e)
    {
        if (chkAsociado.Checked)
        {
            DatosAfi.Visible = true;
        }
        else
        {
            DatosAfi.Visible = false;
        }
    }

    protected void chkPEPS_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPEPS.Checked)
        {
            panelPEPS.Visible = true;
        }
        else
        {
            panelPEPS.Visible = false;
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
} 