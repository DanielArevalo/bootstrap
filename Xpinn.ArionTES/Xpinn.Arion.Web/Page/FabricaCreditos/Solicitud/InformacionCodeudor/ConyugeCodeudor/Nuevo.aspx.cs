using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Collections.Generic;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.FabricaCreditos.Services.ConyugeService ConyugeServicio = new Xpinn.FabricaCreditos.Services.ConyugeService();

    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
    Xpinn.FabricaCreditos.Entities.Conyuge vConyuge = new Xpinn.FabricaCreditos.Entities.Conyuge();
    List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
           

    //Listas:
    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    //Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[Persona1Servicio.CodigoProgramaConyuge + ".id"] != null)
                VisualizarOpciones(Persona1Servicio.CodigoProgramaConyuge, "E");
            else
                VisualizarOpciones(Persona1Servicio.CodigoProgramaConyuge, "A");

            Site2 toolBar = (Site2)this.Master;
            //toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoAdelante += btnAdelante_Click;
            toolBar.eventoAtras += btnAtras_Click;

            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();

            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            btnAdelante.ImageUrl = "~/Images/btnCodeudor.jpg";

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaConyuge, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                lstConsulta = Persona1Servicio.ListarPersona1(ObtenerValores(), (Usuario)Session["usuario"]);
                //Verifica si ya se registraron datos del negocio:
                if (lstConsulta.Count > 0)
                {
                    idObjeto = lstConsulta[0].cod_persona.ToString();
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    CargarListas();
                }

            }

            //CargarListas();
            //if (!IsPostBack)
            //{
            //    if (Session[Persona1Servicio.CodigoProgramaConyuge + ".id"] != null)
            //    {
            //        idObjeto = Session[Persona1Servicio.CodigoProgramaConyuge + ".id"].ToString();
            //        Session.Remove(Persona1Servicio.CodigoProgramaConyuge + ".id");
            //        ObtenerDatos(idObjeto);
            //    }
            //}
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaConyuge, "Page_Load", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.Persona1 ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        vPersona1.seleccionar = "C"; //Bandera para ejecuta el select del conyuge

        return vPersona1;
    }
    private void CargarListas()
    {
        try
        {
            ListaSolicitada = "TipoIdentificacion";
            TraerResultadosLista();
            ddlTipo.DataSource = lstDatosSolicitud;
            ddlTipo.DataTextField = "ListaDescripcion";
            ddlTipo.DataValueField = "ListaId";
            ddlTipo.DataBind();

            ListaSolicitada = "Lugares";
            TraerResultadosLista();
            ddlLugarExpedicion.DataSource = lstDatosSolicitud;
            ddlLugarNacimiento.DataSource = lstDatosSolicitud;
            ddlLugarResidencia.DataSource = lstDatosSolicitud;
            ddlLugarExpedicion.DataTextField = "ListaDescripcion";
            ddlLugarNacimiento.DataTextField = "ListaDescripcion";
            ddlLugarResidencia.DataTextField = "ListaDescripcion";
            ddlLugarExpedicion.DataValueField = "ListaIdStr";
            ddlLugarNacimiento.DataValueField = "ListaIdStr";
            ddlLugarResidencia.DataValueField = "ListaIdStr";
            ddlLugarExpedicion.DataBind();
            ddlLugarNacimiento.DataBind();
            ddlLugarResidencia.DataBind();

            ListaSolicitada = "EstadoCivil";
            TraerResultadosLista();
            ddlEstadoCivil.DataSource = lstDatosSolicitud;
            ddlEstadoCivil.DataTextField = "ListaDescripcion";
            ddlEstadoCivil.DataValueField = "ListaId";
            ddlEstadoCivil.DataBind();

            ListaSolicitada = "NivelEscolaridad";
            TraerResultadosLista();
            ddlNivelEscolaridad.DataSource = lstDatosSolicitud;
            ddlNivelEscolaridad.DataTextField = "ListaDescripcion";
            ddlNivelEscolaridad.DataValueField = "ListaId";
            ddlNivelEscolaridad.DataBind();

            ListaSolicitada = "Actividad";
            TraerResultadosLista();
            ddlActividad.DataSource = lstDatosSolicitud;
            ddlActividad.DataTextField = "ListaDescripcion";
            ddlActividad.DataValueField = "ListaId";
            ddlActividad.DataBind();

            ListaSolicitada = "TipoContrato";
            TraerResultadosLista();
            ddlTipoContrato.DataSource = lstDatosSolicitud;
            ddlTipoContrato.DataTextField = "ListaDescripcion";
            ddlTipoContrato.DataValueField = "ListaId";
            ddlTipoContrato.DataBind();

            ListaSolicitada = "TipoCargo";
            TraerResultadosLista();
            ddlCargo.DataSource = lstDatosSolicitud;
            ddlCargo.DataTextField = "ListaDescripcion";
            ddlCargo.DataValueField = "ListaId";
            ddlCargo.DataBind();

            ListaSolicitada = "ESTADO_ACTIVO";
            TraerResultadosLista();
            ddlEstado.DataSource = lstDatosSolicitud;
            ddlEstado.DataTextField = "ListaDescripcion";
            ddlEstado.DataValueField = "ListaId";
            ddlEstado.DataBind();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.GetType().Name + "L", "CargarListas", ex);
        }
    }


    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = Persona1Servicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }

   

    private void Guardar()
    {

        try
        {
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

            if (idObjeto != "")
                vPersona1 = DatosClienteServicio.ConsultarPersona1(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vPersona1.origen = "Solicitud";     //Permite reconocer que se modifica persona desde el formulario "Solicitud"
            vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            vPersona1.tipo_persona = (rblTipoPersona.Text != "") ? Convert.ToString(rblTipoPersona.SelectedValue) : String.Empty;
            vPersona1.identificacion = (txtIdentificacion.Text != "") ? Convert.ToString(txtIdentificacion.Text.Trim()) : String.Empty;
            if (txtDigito_verificacion.Text != "") vPersona1.digito_verificacion = Convert.ToInt64(txtDigito_verificacion.Text.Trim());
            if (ddlTipo.Text != "") vPersona1.tipo_identificacion = Convert.ToInt64(ddlTipo.SelectedValue);
            if (txtFechaexpedicion.Text != "") vPersona1.fechaexpedicion = Convert.ToDateTime(txtFechaexpedicion.Text.Trim());
            if (ddlLugarExpedicion.Text != "") vPersona1.codciudadexpedicion = Convert.ToInt64(ddlLugarExpedicion.SelectedValue);
            vPersona1.sexo = (rblSexo.Text != "") ? Convert.ToString(rblSexo.SelectedValue) : String.Empty;
            vPersona1.primer_nombre = (txtPrimer_nombre.Text != "") ? Convert.ToString(txtPrimer_nombre.Text.Trim()) : String.Empty;
            vPersona1.segundo_nombre = (txtSegundo_nombre.Text != "") ? Convert.ToString(txtSegundo_nombre.Text.Trim()) : String.Empty;
            vPersona1.primer_apellido = (txtPrimer_apellido.Text != "") ? Convert.ToString(txtPrimer_apellido.Text.Trim()) : String.Empty;
            vPersona1.segundo_apellido = (txtSegundo_apellido.Text != "") ? Convert.ToString(txtSegundo_apellido.Text.Trim()) : String.Empty;
            vPersona1.razon_social = (txtRazon_social.Text != "") ? Convert.ToString(txtRazon_social.Text.Trim()) : String.Empty;
            if (txtFechanacimiento.Text != "") vPersona1.fechanacimiento = Convert.ToDateTime(txtFechanacimiento.Text.Trim());
            if (ddlLugarNacimiento.Text != "") vPersona1.codciudadnacimiento = Convert.ToInt64(ddlLugarNacimiento.SelectedValue);
            if (ddlEstadoCivil.Text != "") vPersona1.codestadocivil = Convert.ToInt64(ddlEstadoCivil.SelectedValue);
            if (ddlNivelEscolaridad.Text != "") vPersona1.codescolaridad = Convert.ToInt64(ddlNivelEscolaridad.SelectedValue);
            if (ddlActividad.Text != "") vPersona1.codactividadStr = ddlActividad.SelectedValue;
            vPersona1.direccion = (txtDireccion.Text != "") ? Convert.ToString(txtDireccion.Text.Trim()) : String.Empty;
            vPersona1.telefono = (txtTelefono.Text != "") ? Convert.ToString(txtTelefono.Text.Trim()) : String.Empty;
            if (ddlLugarResidencia.Text != "") vPersona1.codciudadresidencia = Convert.ToInt64(ddlLugarResidencia.SelectedValue);
            if (txtAntiguedadlugar.Text != "") vPersona1.antiguedadlugar = Convert.ToInt64(txtAntiguedadlugar.Text.Trim());
            vPersona1.tipovivienda = (rblTipoVivienda.Text != "") ? Convert.ToString(rblTipoVivienda.SelectedValue) : String.Empty;
            vPersona1.arrendador = (txtArrendador.Text != "") ? Convert.ToString(txtArrendador.Text.Trim()) : String.Empty;
            vPersona1.telefonoarrendador = (txtTelefonoarrendador.Text != "") ? Convert.ToString(txtTelefonoarrendador.Text.Trim()) : String.Empty;
            //if (txtValorArriendo.Text != "") vPersona1.ValorArriendo = Convert.ToInt64(txtValorArriendo.Text.Trim());
            vPersona1.celular = (txtCelular.Text != "") ? Convert.ToString(txtCelular.Text.Trim()) : String.Empty;
            vPersona1.email = (txtEmail.Text != "") ? Convert.ToString(txtEmail.Text.Trim()) : String.Empty;
            vPersona1.empresa = (txtEmpresa.Text != "") ? Convert.ToString(txtEmpresa.Text.Trim()) : String.Empty;
            vPersona1.telefonoempresa = (txtTelefonoempresa.Text != "") ? Convert.ToString(txtTelefonoempresa.Text.Trim()) : String.Empty;

            vPersona1.direccionempresa = String.Empty;// (txtDireccionEmpresa.Text != "") ? Convert.ToString(txtDireccionEmpresa.Text.Trim()) : String.Empty;
            //if (txtAntiguedadlugarEmpresa.Text != "") vPersona1.antiguedadlugarempresa = Convert.ToInt64(txtAntiguedadlugarEmpresa.Text.Trim());


            if (ddlCargo.Text != "") vPersona1.codcargo = Convert.ToInt64(ddlCargo.Text.Trim());
            if (ddlTipoContrato.Text != "") vPersona1.codtipocontrato = Convert.ToInt64(ddlTipoContrato.SelectedValue);
            if (txtCod_asesor.Text != "") vPersona1.cod_asesor = Convert.ToInt64(txtCod_asesor.Text.Trim());
            //if (txtResidente.Text != "") vPersona1.residente = Convert.ToString(txtResidente.Text.Trim());
            //vPersona1.residente = String.Empty;
            vPersona1.residente = (rblResidente.Text != "") ? Convert.ToString(rblResidente.SelectedValue) : String.Empty;
            if (txtFecha_residencia.Text != "") vPersona1.fecha_residencia = Convert.ToDateTime(txtFecha_residencia.Text.Trim());
            if (txtCod_oficina.Text != "") vPersona1.cod_oficina = Convert.ToInt64(txtCod_oficina.Text.Trim());
            vPersona1.tratamiento = (txtTratamiento.Text != "") ? Convert.ToString(txtTratamiento.Text.Trim()) : String.Empty;
            vPersona1.estado = (ddlEstado.Text != "") ? Convert.ToString(ddlEstado.SelectedValue) : String.Empty;

            vPersona1.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));

            Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");
            vPersona1.usuariocreacion = lblUsuario.Text;
            vPersona1.fecultmod = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            vPersona1.usuultmod = lblUsuario.Text;

            if (ddlLugarResidencia.Text != "") vPersona1.codciudadresidencia = Convert.ToInt64(ddlLugarResidencia.SelectedValue);

            vPersona1.telCorrespondencia = "";
            vPersona1.dirCorrespondencia = "";

            if (idObjeto != "")
            {
                vPersona1.cod_persona = Convert.ToInt64(idObjeto);
                DatosClienteServicio.ModificarPersona1(vPersona1, (Usuario)Session["usuario"]);
            }
            else
            {
                DatosClienteServicio.CrearPersona1(vPersona1, (Usuario)Session["usuario"]);
                vConyuge.cod_conyuge = vPersona1.cod_persona;
                vConyuge.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
                ConyugeServicio.CrearConyuge(vConyuge, (Usuario)Session["usuario"]);
                idObjeto = vPersona1.cod_persona.ToString();
            }
            Session[Persona1Servicio.CodigoProgramaConyuge + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);



        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.CodigoProgramaConyuge, "btnGuardar_Click", ex);
        }
    }

    private void CargarCliente(String pIdObjeto)
    {

        try
        {
            //Session[DatosClienteServicio.CodigoProgramaConyuge + ".id"] = txtCod_persona.Text.ToString().Trim();
            Session[DatosClienteServicio.CodigoProgramaConyuge + ".NumDoc"] = txtIdentificacion.Text.ToString().Trim();

            vPersona1.identificacion = txtIdentificacion.Text;
            vPersona1.seleccionar = "Identificacion";
            vPersona1.noTraerHuella = 1;
            vPersona1 = DatosClienteServicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);

            //Habilita Razon social solo para Nits
            if ((ddlTipo.SelectedValue.ToString() != "2") && (ddlTipo.SelectedValue.ToString() != "3"))
            {
                Panel2.Enabled = false;
                //Panel1.Visible = false;
            }
            else
            {
                Panel2.Enabled = true;
                //Panel1.Visible = true;
            }

            //if (vPersona1.cod_persona != Int64.MinValue)
            //{
            //    txtCod_persona.Text = HttpUtility.HtmlDecode(vPersona1.cod_persona.ToString().Trim());
            //    txtCod_personaE.Text = HttpUtility.HtmlDecode(vPersona1.cod_persona.ToString().Trim());
            //}
            if (!string.IsNullOrEmpty(vPersona1.tipo_persona))
                rblTipoPersona.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_persona.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.identificacion))
            {
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vPersona1.identificacion.ToString().Trim());
             }
            if (vPersona1.digito_verificacion != Int64.MinValue)
                txtDigito_verificacion.Text = HttpUtility.HtmlDecode(vPersona1.digito_verificacion.ToString().Trim());
            if (vPersona1.tipo_identificacion != Int64.MinValue)
            {
                ddlTipo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_identificacion.ToString().Trim());
            }
            if (vPersona1.fechaexpedicion != DateTime.MinValue)
                txtFechaexpedicion.Text = HttpUtility.HtmlDecode(vPersona1.fechaexpedicion.Value.ToShortDateString());
            if (vPersona1.codciudadexpedicion != Int64.MinValue)
                ddlLugarExpedicion.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadexpedicion.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.sexo))
                rblSexo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.sexo.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.primer_nombre))
            {
                txtPrimer_nombre.Text = HttpUtility.HtmlDecode(vPersona1.primer_nombre.ToString().Trim());

            }

            if (!string.IsNullOrEmpty(vPersona1.segundo_nombre))
            {
                txtSegundo_nombre.Text = HttpUtility.HtmlDecode(vPersona1.segundo_nombre.ToString().Trim());

            }
            if (!string.IsNullOrEmpty(vPersona1.primer_apellido))
            {
                txtPrimer_apellido.Text = HttpUtility.HtmlDecode(vPersona1.primer_apellido.ToString().Trim());
            }
            if (!string.IsNullOrEmpty(vPersona1.segundo_apellido))
            {
                txtSegundo_apellido.Text = HttpUtility.HtmlDecode(vPersona1.segundo_apellido.ToString().Trim());
            }
            if (!string.IsNullOrEmpty(vPersona1.razon_social))
            {
                txtRazon_social.Text = HttpUtility.HtmlDecode(vPersona1.razon_social.ToString().Trim());
            }
            if (vPersona1.fechanacimiento != DateTime.MinValue)
            {
                txtFechanacimiento.Text = HttpUtility.HtmlDecode(vPersona1.fechanacimiento.Value.ToShortDateString());
            }

            if (vPersona1.codciudadnacimiento != Int64.MinValue)
                ddlLugarNacimiento.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadnacimiento.ToString().Trim());
            if (vPersona1.codestadocivil != Int64.MinValue)
                ddlEstadoCivil.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codestadocivil.ToString().Trim());
            if (vPersona1.codescolaridad != Int64.MinValue)
                ddlNivelEscolaridad.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codescolaridad.ToString().Trim());
            if (vPersona1.codactividad != Int64.MinValue)
            {
                ddlActividad.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codactividad.ToString().Trim());
            }
            if (!string.IsNullOrEmpty(vPersona1.direccion))
            {
                txtDireccion.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
            }

            if (!string.IsNullOrEmpty(vPersona1.telefono))
            {
                txtTelefono.Text = HttpUtility.HtmlDecode(vPersona1.telefono.ToString().Trim());
            }
            if (vPersona1.codciudadresidencia != Int64.MinValue)
            {
                ddlLugarResidencia.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadresidencia.ToString().Trim());
            }
            if (vPersona1.antiguedadlugar != Int64.MinValue)
                txtAntiguedadlugar.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugar.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.tipovivienda))
            {
                rblTipoVivienda.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipovivienda.ToString().Trim());


            }

            if (!string.IsNullOrEmpty(vPersona1.arrendador))
                txtArrendador.Text = HttpUtility.HtmlDecode(vPersona1.arrendador.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.telefonoarrendador))
                txtTelefonoarrendador.Text = HttpUtility.HtmlDecode(vPersona1.telefonoarrendador.ToString().Trim());
            //if (vPersona1.ValorArriendo != Int64.MinValue)
            //    txtValorArriendo.Text = HttpUtility.HtmlDecode(vPersona1.ValorArriendo.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.celular))
                txtCelular.Text = HttpUtility.HtmlDecode(vPersona1.celular.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.email))
                txtEmail.Text = HttpUtility.HtmlDecode(vPersona1.email.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.empresa))
                txtEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.empresa.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.telefonoempresa))
                txtTelefonoempresa.Text = HttpUtility.HtmlDecode(vPersona1.telefonoempresa.ToString().Trim());

            //if (!string.IsNullOrEmpty(vPersona1.direccionempresa))
            //    txtDireccionEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.direccionempresa.ToString().Trim());
            //if (vPersona1.antiguedadlugarempresa != Int64.MinValue)
            //    txtAntiguedadlugarEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugarempresa.ToString().Trim());

            if (vPersona1.codcargo != Int64.MinValue)
                ddlCargo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codcargo.ToString().Trim());
            if (vPersona1.codtipocontrato != Int64.MinValue)
                ddlTipoContrato.Text = HttpUtility.HtmlDecode(vPersona1.codtipocontrato.ToString().Trim());
            if (vPersona1.cod_asesor != Int64.MinValue)
                txtCod_asesor.Text = HttpUtility.HtmlDecode(vPersona1.cod_asesor.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.residente))
                rblResidente.Text = HttpUtility.HtmlDecode(vPersona1.residente.ToString().Trim());
            if (vPersona1.fecha_residencia != DateTime.MinValue)
                txtFecha_residencia.Text = HttpUtility.HtmlDecode(vPersona1.fecha_residencia.ToShortDateString());
            if (vPersona1.cod_oficina != Int64.MinValue)
                txtCod_oficina.Text = HttpUtility.HtmlDecode(vPersona1.cod_oficina.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.tratamiento))
                txtTratamiento.Text = HttpUtility.HtmlDecode(vPersona1.tratamiento.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.estado))
                ddlEstado.SelectedValue = HttpUtility.HtmlDecode(vPersona1.estado.ToString().Trim());


           
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.CodigoProgramaConyuge, "ObtenerDatos", ex);
        }

        //Session["Cod_persona"] = txtCod_persona.Text;
        //Session["Nombres"] = txtPrimer_nombre.Text.ToString().Trim() + " " + txtSegundo_nombre.Text.ToString().Trim() + " " + txtPrimer_apellido.Text.ToString().Trim() + " " + txtSegundo_apellido.Text.ToString().Trim();


    }

    public static int GetAge(DateTime birthDate)
    {
        return (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.242199);
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
            Session[Persona1Servicio.CodigoProgramaConyuge + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            vPersona1 = Persona1Servicio.ConsultarPersona1(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            //if (vPersona1.cod_persona != Int64.MinValue)
            //    txtCod_persona.Text = HttpUtility.HtmlDecode(vPersona1.cod_persona.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.tipo_persona))
                rblTipoPersona.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_persona.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.identificacion))
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vPersona1.identificacion.ToString().Trim());
            if (vPersona1.digito_verificacion != Int64.MinValue)
                txtDigito_verificacion.Text = HttpUtility.HtmlDecode(vPersona1.digito_verificacion.ToString().Trim());
           if (vPersona1.fechaexpedicion != DateTime.MinValue)
               txtFechaexpedicion.Text = HttpUtility.HtmlDecode(vPersona1.fechaexpedicion.Value.ToShortDateString());
             if (!string.IsNullOrEmpty(vPersona1.sexo))
                rblSexo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.sexo.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.primer_nombre))
                txtPrimer_nombre.Text = HttpUtility.HtmlDecode(vPersona1.primer_nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.segundo_nombre))
                txtSegundo_nombre.Text = HttpUtility.HtmlDecode(vPersona1.segundo_nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.primer_apellido))
                txtPrimer_apellido.Text = HttpUtility.HtmlDecode(vPersona1.primer_apellido.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.segundo_apellido))
                txtSegundo_apellido.Text = HttpUtility.HtmlDecode(vPersona1.segundo_apellido.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.razon_social))
                txtRazon_social.Text = HttpUtility.HtmlDecode(vPersona1.razon_social.ToString().Trim());
            if (vPersona1.fechanacimiento != DateTime.MinValue)
                txtFechanacimiento.Text = HttpUtility.HtmlDecode(vPersona1.fechanacimiento.Value.ToShortDateString());
            if (!string.IsNullOrEmpty(vPersona1.direccion))
                txtDireccion.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.telefono))
                txtTelefono.Text = HttpUtility.HtmlDecode(vPersona1.telefono.ToString().Trim());
            if (vPersona1.antiguedadlugar != Int64.MinValue)
                txtAntiguedadlugar.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugar.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.tipovivienda))
                rblTipoVivienda.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipovivienda.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.arrendador))
                txtArrendador.Text = HttpUtility.HtmlDecode(vPersona1.arrendador.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.telefonoarrendador))
                txtTelefonoarrendador.Text = HttpUtility.HtmlDecode(vPersona1.telefonoarrendador.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.celular))
                txtCelular.Text = HttpUtility.HtmlDecode(vPersona1.celular.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.email))
                txtEmail.Text = HttpUtility.HtmlDecode(vPersona1.email.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.empresa))
                txtEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.empresa.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.telefonoempresa))
                txtTelefonoempresa.Text = HttpUtility.HtmlDecode(vPersona1.telefonoempresa.ToString().Trim());
              if (vPersona1.cod_asesor != Int64.MinValue)
                txtCod_asesor.Text = HttpUtility.HtmlDecode(vPersona1.cod_asesor.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.residente))
                rblResidente.SelectedValue = HttpUtility.HtmlDecode(vPersona1.residente.ToString().Trim());
            if (vPersona1.fecha_residencia != DateTime.MinValue)
                txtFecha_residencia.Text = HttpUtility.HtmlDecode(vPersona1.fecha_residencia.ToShortDateString());
            if (vPersona1.cod_oficina != Int64.MinValue)
                txtCod_oficina.Text = HttpUtility.HtmlDecode(vPersona1.cod_oficina.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.tratamiento))
                txtTratamiento.Text = HttpUtility.HtmlDecode(vPersona1.tratamiento.ToString().Trim());
           
            //Despues de obtener datos se carga el valor seleccionado en las listas
            CargarListas();
            if (vPersona1.tipo_identificacion != Int64.MinValue)
                ddlTipo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_identificacion.ToString().Trim());
            if (vPersona1.codciudadexpedicion != Int64.MinValue)
                ddlLugarExpedicion.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadexpedicion.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.estado))
                ddlEstado.SelectedValue = HttpUtility.HtmlDecode(vPersona1.estado.ToString().Trim());
            if (vPersona1.codcargo != Int64.MinValue)
                ddlCargo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codcargo.ToString().Trim());
            if (vPersona1.codtipocontrato != Int64.MinValue)
                ddlTipoContrato.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codtipocontrato.ToString().Trim());
            if (vPersona1.codciudadresidencia != Int64.MinValue)
                ddlLugarResidencia.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadresidencia.ToString().Trim());
            if (vPersona1.codciudadnacimiento != Int64.MinValue)
                ddlLugarNacimiento.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadnacimiento.ToString().Trim());
            if (vPersona1.codestadocivil != Int64.MinValue)
                ddlEstadoCivil.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codestadocivil.ToString().Trim());
            if (vPersona1.codescolaridad != Int64.MinValue)
                ddlNivelEscolaridad.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codescolaridad.ToString().Trim());
            if (vPersona1.codactividad != Int64.MinValue)
                ddlActividad.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codactividad.ToString().Trim());                    
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaConyuge, "ObtenerDatos", ex);
        }
    }

    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionCodeudor/Codeudor/Lista.aspx");
    }
    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        //Guardar();
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionCodeudor/Codeudor/Lista.aspx");
       
    }
}