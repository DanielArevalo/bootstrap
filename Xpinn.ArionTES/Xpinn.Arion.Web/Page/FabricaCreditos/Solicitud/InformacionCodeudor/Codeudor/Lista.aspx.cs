using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
  
partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.FabricaCreditos.Services.ConyugeService ConyugeServicio = new Xpinn.FabricaCreditos.Services.ConyugeService();
    private Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
    private Xpinn.FabricaCreditos.Services.codeudoresService CodeudoresServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
      
    //Listas:
    private List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    private String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    private Int64 codconyuge;

    private string EstadoCodeudor = null;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //try
        //{  
            VisualizarOpciones(Persona1Servicio.CodigoProgramaCodeudor, "L");

            Site1 toolBar = (Site1)this.Master;
            if (Session["Retorno"] != null)
                if (Session["Retorno"].ToString() != "1")
            toolBar.eventoAdelante += btnAdelante_Click;
            toolBar.eventoAtras += btnAtras_Click;

            if (Session["Nombres"] != null) ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            if (Session["Identificacion"] != null) ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();

            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            btnAdelante.ValidationGroup = "";
            Int64 tipoempresa = 0;
            Usuario usuap = (Usuario)Session["usuario"];
            tipoempresa = Convert.ToInt64(usuap.tipo);

            if (tipoempresa == 1)
            {
                btnAdelante.ImageUrl = "~/Images/btnPatrimonioCodeudor.jpg";
            }
            if (tipoempresa == 2)
            {
                btnAdelante.ImageUrl = "~/Images/btnPlanPagos.jpg";
            }

            if (Session["NumeroSolicitud"] != null)
                txtNumeroRadicacion.Text = Session["NumeroSolicitud"].ToString();
        
        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "Page_PreInit", ex);
        //}
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, Persona1Servicio.CodigoPrograma);
                CargarListas();
                Actualizar();                      
                Int64 tipoempresa = 0;
                Usuario usuap = (Usuario)Session["usuario"];
                tipoempresa = Convert.ToInt64(usuap.tipo);

                if (tipoempresa == 2)
                {
                    ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
                     btnAdelante.PostBackUrl = "~/Page/FabricaCreditos/Solicitud/PlanPagos/Lista.aspx";
                }
                if (tipoempresa == 1)
                {
                     ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
                     btnAdelante.PostBackUrl = "~/Page/FabricaCreditos/Solicitud/InformacionCodeudor/PatrimonioCodeudor/Default.aspx";
                }
                                
            }
        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(Persona1Servicio.GetType().Name, "Page_Load", ex);
        //}
    }


    private void Borrar()
    {
        txtIdentificacion.Text = "";
        txtPrimer_nombre.Text = "";
        txtSegundo_nombre.Text = "";
        txtPrimer_apellido.Text = "";
        txtSegundo_apellido.Text = "";

        txtDireccion.Text = "";
        txtTelefono.Text = "";
        txtRazon_social.Text = "";

        txtDigito_verificacion.Text = "";
        txtFechaexpedicion.Text = "";

        txtFechanacimiento.Text = "";
        txtAntiguedadlugar.Text = "";

        txtTratamiento.Text = "";
        txtCelular.Text = "";
        txtTelefonoarrendador.Text = "";
        txtArrendador.Text = "";
        txtTelefonoempresa.Text = "";

        txtEmail.Text = "";
        txtEmpresa.Text = "";

        txtFecha_residencia.Text = "";

        txtCod_asesor.Text = "";
        lblEdad.Text = "";
    }


    private void BorrarConyuge()
    {
        txtIdentificacionConyuge.Text = "";
        txtPrimer_nombreConyuge.Text = "";
        txtSegundo_nombreConyuge.Text = "";
        txtPrimer_apellidoConyuge.Text = "";
        txtSegundo_apellidoConyuge.Text = "";
        txtDireccionConyuge.Text = "";
        txtTelefonoConyuge.Text = "";
        txtRazon_socialConyuge.Text = "";
        txtDigito_verificacionConyuge.Text = "";
        txtFechaexpedicionConyuge.Text = "";
        txtFechanacimientoConyuge.Text = "";
        txtAntiguedadlugarConyuge.Text = "";
        txtTratamientoConyuge.Text = "";
        txtCelularConyuge.Text = "";
        txtTelefonoarrendadorConyuge.Text = "";
        txtArrendadorConyuge.Text = "";
        txtTelefonoempresaConyuge.Text = "";

        txtEmailConyuge.Text = "";
        txtEmpresaConyuge.Text = "";

        txtFecha_residenciaConyuge.Text = "";

        txtCod_asesorConyuge.Text = "";
        lblEdadConyuge.Text = "";

    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
       
    }

 
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, Persona1Servicio.CodigoProgramaCodeudor);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {        
        DetalleFila();      
    }

    private void DetalleFila()
    {
        String id = gvListaAfliados.Rows[0].Cells[0].Text;
        Session[Persona1Servicio.CodigoProgramaCodeudor + ".id"] = id;       
        Edicion();
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvListaAfliados.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[Persona1Servicio.CodigoProgramaCodeudor + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvListaAfliados.Rows[e.RowIndex].Cells[0].Text);
            long solicitud = Convert.ToInt64(txtNumeroRadicacion.Text);
            //Persona1Servicio.EliminarPersona1(id, (Usuario)Session["usuario"]);
            CodeudoresServicio.EliminarcodeudoresSol(id, solicitud, (Usuario)Session["usuario"]);
            //ConyugeServicio.EliminarConyuge(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListaAfliados.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "gvLista_PageIndexChanging", ex);
        }
    }

    private void  Actualizar()
    {
        //try
        //{
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();

            lstConsulta = Persona1Servicio.ListarPersona1(ObtenerValores(), (Usuario)Session["usuario"]);

            gvListaAfliados.PageSize = 15;
            gvListaAfliados.EmptyDataText = "No se encontraron registros";
            gvListaAfliados.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                try
                {
                    gvListaAfliados.Visible = true;
                    lblTotalRegs.Visible = false;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    gvListaAfliados.DataBind();
                    Session["CodCodeudor"] = lstConsulta[0].cod_persona.ToString();
                    ValidarPermisosGrilla(gvListaAfliados);
                    PanelListaCon.Visible = true;
                    Session["CodCodeudor1"] = lstConsulta[0].cod_persona.ToString();
                }
                finally
                {
                    DetalleFila();
                }
            }
            else
            {
                idObjeto = "";
                gvListaAfliados.Visible = false;
                PanelListaCon.Visible = false;
                lblTotalRegs.Text = "";
                lblTotalRegs.Visible = false;
                Session["CodCodeudor"] = "";  // Se asigna null a la variable para que al buscar el conyuge no genere error
            }
            Session.Add(Persona1Servicio.CodigoProgramaCodeudor + ".consulta", 1);
        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "Actualizar", ex);
        //}
    }

    private Xpinn.FabricaCreditos.Entities.Persona1 ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();       

        if(txtIdentificacion.Text.Trim() != "")
            vPersona1.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
        if(txtDigito_verificacion.Text.Trim() != "")
            vPersona1.digito_verificacion = Convert.ToInt64(txtDigito_verificacion.Text.Trim());
        if(txtFechaexpedicion.Text.Trim() != "")
            vPersona1.fechaexpedicion = Convert.ToDateTime(txtFechaexpedicion.Text.Trim());
        if(txtPrimer_nombre.Text.Trim() != "")
            vPersona1.primer_nombre = Convert.ToString(txtPrimer_nombre.Text.Trim());
        if(txtSegundo_nombre.Text.Trim() != "")
            vPersona1.segundo_nombre = Convert.ToString(txtSegundo_nombre.Text.Trim());
        if(txtPrimer_apellido.Text.Trim() != "")
            vPersona1.primer_apellido = Convert.ToString(txtPrimer_apellido.Text.Trim());
        if(txtSegundo_apellido.Text.Trim() != "")
            vPersona1.segundo_apellido = Convert.ToString(txtSegundo_apellido.Text.Trim());
        if(txtRazon_social.Text.Trim() != "")
            vPersona1.razon_social = Convert.ToString(txtRazon_social.Text.Trim());
        if(txtFechanacimiento.Text.Trim() != "")
            vPersona1.fechanacimiento = Convert.ToDateTime(txtFechanacimiento.Text.Trim());
        if(txtDireccion.Text.Trim() != "")
            vPersona1.direccion = Convert.ToString(txtDireccion.Text.Trim());
        if(txtTelefono.Text.Trim() != "")
            vPersona1.telefono = Convert.ToString(txtTelefono.Text.Trim());
        if(txtAntiguedadlugar.Text.Trim() != "")
            vPersona1.antiguedadlugar = Convert.ToInt64(txtAntiguedadlugar.Text.Trim());
        if(txtArrendador.Text.Trim() != "")
            vPersona1.arrendador = Convert.ToString(txtArrendador.Text.Trim());
        if(txtTelefonoarrendador.Text.Trim() != "")
            vPersona1.telefonoarrendador = Convert.ToString(txtTelefonoarrendador.Text.Trim());
        if(txtCelular.Text.Trim() != "")
            vPersona1.celular = Convert.ToString(txtCelular.Text.Trim());
        if(txtEmail.Text.Trim() != "")
            vPersona1.email = Convert.ToString(txtEmail.Text.Trim());
        if(txtEmpresa.Text.Trim() != "")
            vPersona1.empresa = Convert.ToString(txtEmpresa.Text.Trim());
        if(txtTelefonoempresa.Text.Trim() != "")
            vPersona1.telefonoempresa = Convert.ToString(txtTelefonoempresa.Text.Trim());
        if(txtCod_asesor.Text.Trim() != "")
            vPersona1.cod_asesor = Convert.ToInt64(txtCod_asesor.Text.Trim());
        if(txtFecha_residencia.Text.Trim() != "")
            vPersona1.fecha_residencia = Convert.ToDateTime(txtFecha_residencia.Text.Trim());
        if(txtCod_oficina.Text.Trim() != "")
            vPersona1.cod_oficina = Convert.ToInt64(txtCod_oficina.Text.Trim());
        if(txtTratamiento.Text.Trim() != "")
            vPersona1.tratamiento = Convert.ToString(txtTratamiento.Text.Trim());      
        if (txtNumeroRadicacion.Text.Trim() != "")
            vPersona1.numeroRadicacion = Convert.ToInt64(txtNumeroRadicacion.Text.Trim());

        // Campos nuevos conyuge
        if (txtNumeroHijos.Text.Trim() != "")
            vPersona1.numHijos = Convert.ToInt64(txtNumeroHijos.Text.Trim());
        if (txtNumeroPersonasCargo.Text.Trim() != "")
            vPersona1.numPersonasaCargo = Convert.ToInt64(txtNumeroPersonasCargo.Text.Trim());
        if (txtOcupacion.Text.Trim() != "")
            vPersona1.ocupacion =  txtOcupacion.Text.Trim();
        string salario = txtSalario.Text.Replace(".", "");
        if (txtSalario.Text.Trim() != "")
            vPersona1.salario = Convert.ToInt64(salario);
        if (txtAntiguedadLaboral.Text.Trim() != "")
            vPersona1.antiguedadLaboral = Convert.ToInt64(txtAntiguedadLaboral.Text.Trim());

        vPersona1.seleccionar = "CDS"; //Bandera para ejecuta el select del CODEUDOR
   
        return vPersona1;
    }
   



    private void Edicion()
    {
        //try
        //{
            if (Session[Persona1Servicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(Persona1Servicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(Persona1Servicio.CodigoPrograma, "A");

            //Page_Load
            if (Session[Persona1Servicio.CodigoProgramaCodeudor + ".id"] != null)
            {
                idObjeto = Session[Persona1Servicio.CodigoProgramaCodeudor + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                    Session.Remove(Persona1Servicio.CodigoProgramaCodeudor + ".id");
                }
            }
            else
            {
                CargarListas();
            }
        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(Persona1Servicio.GetType().Name + "A", "Page_PreInit", ex);
        //}
    }

    private void Conyuge()

    {
        EstadoCodeudor = Session["EstadoCodeudor"].ToString();
        //Solo permite consultar conyuge codeudor si estado civil = casado o union libre

        try
        {
            if (EstadoCodeudor == "1")
            {
                GuardarValoresConsulta(pConsulta, Persona1Servicio.CodigoProgramaCodeudor);
                ActualizarConyuge();
            }
        }
        catch (ExceptionBusiness ex)
        {
            string error = ex.ToString();
        }   
    }

    private void ActualizarConyuge()
    {
        try
        {
            //Se realiza Consulta en conyuge con el codpersona del codeudor
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            lstConsulta = Persona1Servicio.ListarPersona1(ObtenerValoresConyuge(), (Usuario)Session["usuario"]);

                    gvListaConyuge.PageSize = 15;
            gvListaConyuge.EmptyDataText = "No se encontraron registros";
            gvListaConyuge.DataSource = lstConsulta;

            if (lstConsulta.Count > 0) //Si existe conyuge se debe actualizar, sino se debe crear
            {
                try
                {
                    PanelListaCon.Visible = true;
                    gvListaConyuge.Visible = true;
                  //  lblTotalRegsConyuge.Visible = false;
                    //lblTotalRegsConyuge.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    gvListaConyuge.DataBind();
                    idObjeto = gvListaConyuge.Rows[0].Cells[0].Text;  // Toma el id del conyuge
                    ValidarPermisosGrilla(gvListaConyuge);
                }
                finally
                {
                    DetalleFilaConyuge();
                }
            }
            else
            {                
                idObjeto = "";
                PanelListaCon.Visible = false;
                gvListaConyuge.Visible = false;
               // lblTotalRegsConyuge.Text = "";
            //    lblTotalRegsConyuge.Visible = false;
                BorrarConyuge();
            }

            Session.Add(Persona1Servicio.CodigoProgramaCodeudor + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "Actualizar", ex);
        }
    }

    private void GuardaConyuge(string idObjetoConyuge)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            Xpinn.FabricaCreditos.Entities.Conyuge vConyuge = new Xpinn.FabricaCreditos.Entities.Conyuge();
            Xpinn.FabricaCreditos.Entities.Persona1 vData = new Xpinn.FabricaCreditos.Entities.Persona1();

            if (idObjetoConyuge != "")
            {
                vData.seleccionar = "Identificacion";
                vPersona1.noTraerHuella = 1;
                vData.identificacion = idObjetoConyuge;
                vPersona1 = DatosClienteServicio.ConsultarPersona1Param(vData, (Usuario)Session["usuario"]);
            }
            
                vPersona1.origen = "Solicitud";     //Permite reconocer que se modifica persona desde el formulario "Solicitud"

                vPersona1.tipo_persona = (rblTipoPersonaConyuge.Text != "") ? Convert.ToString(rblTipoPersonaConyuge.SelectedValue) : String.Empty;
                vPersona1.identificacion = (txtIdentificacionConyuge.Text != "") ? Convert.ToString(txtIdentificacionConyuge.Text.Trim()) : String.Empty;
                if (txtDigito_verificacionConyuge.Text != "") vPersona1.digito_verificacion = Convert.ToInt64(txtDigito_verificacionConyuge.Text.Trim());
                if (ddlTipoConyuge.Text != "") vPersona1.tipo_identificacion = Convert.ToInt64(ddlTipoConyuge.SelectedValue);
                if (txtFechaexpedicionConyuge.Text != "") vPersona1.fechaexpedicion = Convert.ToDateTime(txtFechaexpedicionConyuge.Text.Trim());
                if (ddlLugarExpedicionConyuge.Text != "") vPersona1.codciudadexpedicion = Convert.ToInt64(ddlLugarExpedicionConyuge.SelectedValue);
                vPersona1.sexo = (rblSexoConyuge.Text != "") ? Convert.ToString(rblSexoConyuge.SelectedValue) : String.Empty;
                vPersona1.primer_nombre = (txtPrimer_nombreConyuge.Text != "") ? Convert.ToString(txtPrimer_nombreConyuge.Text.Trim()) : String.Empty;
                vPersona1.segundo_nombre = (txtSegundo_nombreConyuge.Text != "") ? Convert.ToString(txtSegundo_nombreConyuge.Text.Trim()) : String.Empty;
                vPersona1.primer_apellido = (txtPrimer_apellidoConyuge.Text != "") ? Convert.ToString(txtPrimer_apellidoConyuge.Text.Trim()) : String.Empty;
                vPersona1.segundo_apellido = (txtSegundo_apellidoConyuge.Text != "") ? Convert.ToString(txtSegundo_apellidoConyuge.Text.Trim()) : String.Empty;
                vPersona1.razon_social = (txtRazon_socialConyuge.Text != "") ? Convert.ToString(txtRazon_socialConyuge.Text.Trim()) : String.Empty;
                if (txtFechanacimientoConyuge.Text != "") vPersona1.fechanacimiento = Convert.ToDateTime(txtFechanacimientoConyuge.Text.Trim());
                if (ddlLugarNacimientoConyuge.Text != "") vPersona1.codciudadnacimiento = Convert.ToInt64(ddlLugarNacimientoConyuge.SelectedValue);
                if (ddlEstadoCivilConyuge.Text != "") vPersona1.codestadocivil = Convert.ToInt64(ddlEstadoCivilConyuge.SelectedValue);
                if (ddlNivelEscolaridadConyuge.Text != "") vPersona1.codescolaridad = Convert.ToInt64(ddlNivelEscolaridadConyuge.SelectedValue);
                if (ddlActividadConyuge.Text != "") vPersona1.codactividadStr = ddlActividadConyuge.SelectedValue;
                vPersona1.direccion = (txtDireccionConyuge.Text != "") ? Convert.ToString(txtDireccionConyuge.Text.Trim()) : String.Empty;
                vPersona1.telefono = (txtTelefonoConyuge.Text != "") ? Convert.ToString(txtTelefonoConyuge.Text.Trim()) : String.Empty;
                vPersona1.codciudadresidencia = 0;
                if (txtAntiguedadlugarConyuge.Text != "") vPersona1.antiguedadlugar = Convert.ToInt64(txtAntiguedadlugarConyuge.Text.Trim());
                vPersona1.tipovivienda = (rblTipoViviendaConyuge.Text != "") ? Convert.ToString(rblTipoViviendaConyuge.SelectedValue) : String.Empty;
                vPersona1.arrendador = (txtArrendadorConyuge.Text != "") ? Convert.ToString(txtArrendadorConyuge.Text.Trim()) : String.Empty;
                vPersona1.telefonoarrendador = (txtTelefonoarrendadorConyuge.Text != "") ? Convert.ToString(txtTelefonoarrendadorConyuge.Text.Trim()) : String.Empty;

                vPersona1.celular = (txtCelularConyuge.Text != "") ? Convert.ToString(txtCelularConyuge.Text.Trim()) : String.Empty;
                vPersona1.email = (txtEmailConyuge.Text != "") ? Convert.ToString(txtEmailConyuge.Text.Trim()) : String.Empty;
                vPersona1.empresa = (txtEmpresaConyuge.Text != "") ? Convert.ToString(txtEmpresaConyuge.Text.Trim()) : String.Empty;
                vPersona1.telefonoempresa = (txtTelefonoempresaConyuge.Text != "") ? Convert.ToString(txtTelefonoempresaConyuge.Text.Trim()) : String.Empty;

                vPersona1.direccionempresa = String.Empty;

                if (ddlCargoConyuge.Text != "") vPersona1.codcargo = Convert.ToInt64(ddlCargoConyuge.Text.Trim());
                if (ddlTipoContratoConyuge.Text != "") vPersona1.codtipocontrato = Convert.ToInt64(ddlTipoContratoConyuge.SelectedValue);
                if (txtCod_asesorConyuge.Text != "") vPersona1.cod_asesor = Convert.ToInt64(txtCod_asesorConyuge.Text.Trim());

                vPersona1.residente = (rblResidenteConyuge.Text != "") ? Convert.ToString(rblResidenteConyuge.SelectedValue) : String.Empty;
                if (txtFecha_residenciaConyuge.Text != "") vPersona1.fecha_residencia = Convert.ToDateTime(txtFecha_residenciaConyuge.Text.Trim());
                if (txtCod_oficinaConyuge.Text != "") vPersona1.cod_oficina = Convert.ToInt64(txtCod_oficinaConyuge.Text.Trim());
                vPersona1.tratamiento = (txtTratamientoConyuge.Text != "") ? Convert.ToString(txtTratamientoConyuge.Text.Trim()) : String.Empty;
                vPersona1.estado = (ddlEstadoConyuge.Text != "") ? Convert.ToString(ddlEstadoConyuge.SelectedValue) : String.Empty;

                vPersona1.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));

                Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                usuario = (Xpinn.Util.Usuario)Session["usuario"];

                vPersona1.usuariocreacion = usuario.nombre;
                vPersona1.fecultmod = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
                vPersona1.usuultmod = usuario.nombre;

                if (ddlLugarResidenciaConyuge.Text != "") vPersona1.codciudadresidencia = Convert.ToInt64(ddlLugarResidenciaConyuge.SelectedValue);
                if (ddlBarrioConyuge.Text != "") vPersona1.barrioResidencia = Convert.ToInt64(ddlBarrioConyuge.SelectedValue);
                vPersona1.dirCorrespondencia = "";
                vPersona1.barrioCorrespondencia = 0;
                vPersona1.telCorrespondencia = "";
                vPersona1.ciuCorrespondencia = 0;

                // Nuevos datos conyuge
                vPersona1.numHijos = (txtNumeroHijosConyuge.Text != "") ? Convert.ToInt64(txtNumeroHijosConyuge.Text.Trim()) : 0;
                vPersona1.numPersonasaCargo = (txtNumeroPersonasCargoConyuge.Text != "") ? Convert.ToInt64(txtNumeroPersonasCargoConyuge.Text.Trim()) : 0;
                vPersona1.ocupacion = (txtOcupacionConyuge.Text != "") ? Convert.ToString(txtOcupacionConyuge.Text.Trim()) : String.Empty;
                vPersona1.salario = (txtSalarioConyuge.Text != "") ? Convert.ToInt64(txtSalarioConyuge.Text.Trim().Replace(".", "")) : 0;
                vPersona1.antiguedadLaboral = (txtAntiguedadLaboralConyuge.Text != "") ? Convert.ToInt64(txtAntiguedadLaboralConyuge.Text.Trim()) : 0;

                vPersona1.ActividadEconomicaEmpresa = 0;
                vPersona1.ciudad = 0;
                vPersona1.relacionEmpleadosEmprender = 0;
                vPersona1.CelularEmpresa = " ";
                vPersona1.profecion = " ";
                vPersona1.Estrato = 0;
                vPersona1.PersonasAcargo = 0;

                if (idObjetoConyuge != "")
                {
                    vPersona1.cod_persona = Convert.ToInt64(idObjeto);
                    DatosClienteServicio.ModificarPersona1(vPersona1, (Usuario)Session["usuario"]);
                    vConyuge.cod_conyuge = vPersona1.cod_persona;  // Toma el codigo de la persona modificada para editarla en la tabla Conyuge


                    vConyuge.cod_persona = Convert.ToInt64(vPersona1.cod_persona);
                    vConyuge = ConyugeServicio.ModificarConyuge(vConyuge, (Usuario)Session["usuario"]);
                }
                else
                {
                    vPersona1 = Persona1Servicio.CrearPersona1(vPersona1, (Usuario)Session["usuario"]);
                    idObjeto = vPersona1.cod_persona.ToString();

                    vConyuge.cod_conyuge = vPersona1.cod_persona;  // Toma el codigo de la persona registrarla para editarla en la tabla Conyuge
                    vConyuge.cod_persona = Convert.ToInt64(vPersona1.cod_persona);
                    vConyuge = ConyugeServicio.CrearConyuge(vConyuge, (Usuario)Session["usuario"]);

                }

                Session[Persona1Servicio.CodigoProgramaCodeudor + ".id"] = idObjeto;
            }
        
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.CodigoProgramaCodeudor, "btnGuardar_Click", ex);
        }
    }

    private void DetalleFilaConyuge()
    {
        String id = gvListaConyuge.Rows[0].Cells[0].Text;
        Session[Persona1Servicio.CodigoProgramaCodeudor + ".id"] = id;
        EdicionConyuge();
    }

    private void EdicionConyuge()
    {
        try
        {
            if (Session[Persona1Servicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(Persona1Servicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(Persona1Servicio.CodigoPrograma, "A");

            //Page_Load
            if (Session[Persona1Servicio.CodigoProgramaCodeudor + ".id"] != null)
            {
                idObjeto = Session[Persona1Servicio.CodigoProgramaCodeudor + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatosConyuge(Convert.ToInt64(idObjeto));
                    Session.Remove(Persona1Servicio.CodigoProgramaCodeudor + ".id");
                }
            }
            else
            {
                CargarListas();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void ObtenerDatosConyuge(Int64 pIdObjeto)
    {
        //BorrarConyuge();
        try
        {
         
            Xpinn.FabricaCreditos.Entities.Conyuge vConyuge = new Xpinn.FabricaCreditos.Entities.Conyuge();
            vConyuge = ConyugeServicio.ConsultarConyuge(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            codconyuge = Convert.ToInt64(vConyuge.cod_conyuge);
            if (codconyuge>0)
            {
                Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
                vPersona1 = Persona1Servicio.ConsultarPersona1(Convert.ToInt64(codconyuge), (Usuario)Session["usuario"]);

                if (!string.IsNullOrEmpty(vPersona1.tipo_persona))
                    rblTipoPersonaConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_persona.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.identificacion))
                    txtIdentificacionConyuge.Text = HttpUtility.HtmlDecode(vPersona1.identificacion.ToString().Trim());
                if (vPersona1.digito_verificacion != Int64.MinValue)
                    txtDigito_verificacionConyuge.Text = HttpUtility.HtmlDecode(vPersona1.digito_verificacion.ToString().Trim());
                if (vPersona1.tipo_identificacion != Int64.MinValue)
                    ddlTipoConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_identificacion.ToString().Trim());
                if (vPersona1.fechaexpedicion != DateTime.MinValue)
                    txtFechaexpedicionConyuge.Text = HttpUtility.HtmlDecode(vPersona1.fechaexpedicion.Value.ToShortDateString());
                if (vPersona1.codciudadexpedicion != Int64.MinValue)
                    ddlLugarExpedicionConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadexpedicion.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.sexo))
                    rblSexoConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.sexo.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.primer_nombre))
                    txtPrimer_nombreConyuge.Text = HttpUtility.HtmlDecode(vPersona1.primer_nombre.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.segundo_nombre))
                    txtSegundo_nombreConyuge.Text = HttpUtility.HtmlDecode(vPersona1.segundo_nombre.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.primer_apellido))
                    txtPrimer_apellidoConyuge.Text = HttpUtility.HtmlDecode(vPersona1.primer_apellido.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.segundo_apellido))
                    txtSegundo_apellidoConyuge.Text = HttpUtility.HtmlDecode(vPersona1.segundo_apellido.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.razon_social))
                    txtRazon_socialConyuge.Text = HttpUtility.HtmlDecode(vPersona1.razon_social.ToString().Trim());
                if (vPersona1.fechanacimiento != DateTime.MinValue)
                    txtFechanacimientoConyuge.Text = HttpUtility.HtmlDecode(vPersona1.fechanacimiento.Value.ToShortDateString());
                if (vPersona1.codciudadnacimiento != Int64.MinValue)
                    ddlLugarNacimientoConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadnacimiento.ToString().Trim());
                if (vPersona1.codestadocivil != Int64.MinValue)
                    ddlEstadoCivilConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codestadocivil.ToString().Trim());
                if (vPersona1.codescolaridad != Int64.MinValue)
                    ddlNivelEscolaridadConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codescolaridad.ToString().Trim());
                if (vPersona1.codactividad != Int64.MinValue)
                    ddlActividadConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codactividad.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.direccion))
                    txtDireccionConyuge.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.telefono))
                    txtTelefonoConyuge.Text = HttpUtility.HtmlDecode(vPersona1.telefono.ToString().Trim());
                if (vPersona1.codciudadresidencia != Int64.MinValue)
                    ddlLugarResidenciaConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadresidencia.ToString().Trim());
                if (vPersona1.antiguedadlugar != Int64.MinValue)
                    txtAntiguedadlugarConyuge.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugar.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.tipovivienda))
                    rblTipoViviendaConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipovivienda.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.arrendador))
                    txtArrendadorConyuge.Text = HttpUtility.HtmlDecode(vPersona1.arrendador.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.telefonoarrendador))
                    txtTelefonoarrendadorConyuge.Text = HttpUtility.HtmlDecode(vPersona1.telefonoarrendador.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.celular))
                    txtCelularConyuge.Text = HttpUtility.HtmlDecode(vPersona1.celular.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.email))
                    txtEmailConyuge.Text = HttpUtility.HtmlDecode(vPersona1.email.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.empresa))
                    txtEmpresaConyuge.Text = HttpUtility.HtmlDecode(vPersona1.empresa.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.telefonoempresa))
                    txtTelefonoempresaConyuge.Text = HttpUtility.HtmlDecode(vPersona1.telefonoempresa.ToString().Trim());
                if (vPersona1.codcargo != Int64.MinValue)
                    ddlCargoConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codcargo.ToString().Trim());
                if (vPersona1.codtipocontrato != Int64.MinValue)
                    ddlTipoContratoConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codtipocontrato.ToString().Trim());
                if (vPersona1.cod_asesor != Int64.MinValue)
                    txtCod_asesorConyuge.Text = HttpUtility.HtmlDecode(vPersona1.cod_asesor.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.residente))
                    rblResidenteConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.residente.ToString().Trim());
                if (vPersona1.fecha_residencia != DateTime.MinValue)
                    txtFecha_residenciaConyuge.Text = HttpUtility.HtmlDecode(vPersona1.fecha_residencia.ToShortDateString());
                if (vPersona1.cod_oficina != Int64.MinValue)
                    txtCod_oficinaConyuge.Text = HttpUtility.HtmlDecode(vPersona1.cod_oficina.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.tratamiento))
                    txtTratamientoConyuge.Text = HttpUtility.HtmlDecode(vPersona1.tratamiento.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.estado))
                    ddlEstadoConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.estado.ToString().Trim());
                if (vPersona1.barrioResidencia != Int64.MinValue && vPersona1.barrioResidencia != null)
                {
                    try
                    {
                        ddlBarrioConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.barrioResidencia.ToString().Trim());
                    }
                    catch { }
                }
                    
                // Nuevos datos conyuge
                if (vPersona1.numHijos != Int64.MinValue)
                    txtNumeroHijosConyuge.Text = HttpUtility.HtmlDecode(vPersona1.numHijos.ToString().Trim());
                if (vPersona1.numPersonasaCargo != Int64.MinValue)
                    txtNumeroPersonasCargoConyuge.Text = HttpUtility.HtmlDecode(vPersona1.numPersonasaCargo.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.ocupacion))
                    txtOcupacionConyuge.Text = HttpUtility.HtmlDecode(vPersona1.ocupacion.ToString().Trim());
                if (vPersona1.salario != Int64.MinValue)
                    txtSalarioConyuge.Text = HttpUtility.HtmlDecode(vPersona1.salario.ToString().Trim());
                if (vPersona1.antiguedadLaboral != Int64.MinValue)
                    txtAntiguedadLaboralConyuge.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadLaboral.ToString().Trim());

                CalcularEdadConyuge();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "ObtenerDatosConyuge", ex);
        }
    }


    private void CargarListas()
    {
        try
        {
            ListaSolicitada = "Lugares";
            TraerResultadosLista();
            ddlLugarExpedicion.DataSource = lstDatosSolicitud;
            ddlLugarExpedicionConyuge.DataSource = lstDatosSolicitud;
            ddlLugarNacimiento.DataSource = lstDatosSolicitud;
            ddlLugarNacimientoConyuge.DataSource = lstDatosSolicitud; 
            ddlLugarExpedicion.DataTextField = "ListaDescripcion";
            ddlLugarExpedicionConyuge.DataTextField = "ListaDescripcion";
            ddlLugarNacimiento.DataTextField = "ListaDescripcion";
            ddlLugarNacimientoConyuge.DataTextField = "ListaDescripcion";
            ddlLugarExpedicion.DataValueField = "ListaIdStr";
            ddlLugarExpedicionConyuge.DataValueField = "ListaIdStr";
            ddlLugarNacimiento.DataValueField = "ListaIdStr";
            ddlLugarNacimientoConyuge.DataValueField = "ListaIdStr";   
            ddlLugarExpedicion.DataBind();
            ddlLugarExpedicionConyuge.DataBind();
            ddlLugarNacimiento.DataBind();
            ddlLugarNacimientoConyuge.DataBind();

            ListaSolicitada = "Barrio";
            TraerResultadosLista();
            ddlBarrio.DataSource = lstDatosSolicitud;
            ddlBarrio.DataTextField = "ListaDescripcion";
            ddlBarrio.DataValueField = "ListaId";
            ddlBarrio.DataBind();
            ddlBarrioConyuge.DataSource = lstDatosSolicitud;
            ddlBarrioConyuge.DataTextField = "ListaDescripcion";
            ddlBarrioConyuge.DataValueField = "ListaId";
            ddlBarrioConyuge.DataBind();
          
            ListaSolicitada = "EstadoCivil";
            TraerResultadosLista();
            ddlEstadoCivil.DataSource = lstDatosSolicitud;
            ddlEstadoCivil.DataTextField = "ListaDescripcion";
            ddlEstadoCivil.DataValueField = "ListaId";
            ddlEstadoCivil.DataBind();
            ddlEstadoCivilConyuge.DataSource = lstDatosSolicitud;
            ddlEstadoCivilConyuge.DataTextField = "ListaDescripcion";
            ddlEstadoCivilConyuge.DataValueField = "ListaId";
            ddlEstadoCivilConyuge.DataBind();

            ListaSolicitada = "TipoCargo";
            TraerResultadosLista();
            ddlCargo.DataSource = lstDatosSolicitud;
            ddlCargo.DataTextField = "ListaDescripcion";
            ddlCargo.DataValueField = "ListaId";
            ddlCargo.DataBind();

            ddlCargoConyuge.DataSource = lstDatosSolicitud;
            ddlCargoConyuge.DataTextField = "ListaDescripcion";
            ddlCargoConyuge.DataValueField = "ListaId";
            ddlCargoConyuge.DataBind();

            ListaSolicitada = "Parentesco";
            TraerResultadosLista();
            ddlParentesco.DataSource = lstDatosSolicitud;
            ddlParentesco.DataTextField = "ListaDescripcion";
            ddlParentesco.DataValueField = "ListaId";
            ddlParentesco.DataBind();

            ddlParentescoConyuge.DataSource = lstDatosSolicitud;
            ddlParentescoConyuge.DataTextField = "ListaDescripcion";
            ddlParentescoConyuge.DataValueField = "ListaId";
            ddlParentescoConyuge.DataBind();

            ListaSolicitada = "TipoContrato";
            TraerResultadosLista();
            ddlTipoContrato.DataSource = lstDatosSolicitud;
            ddlTipoContrato.DataTextField = "ListaDescripcion";
            ddlTipoContrato.DataValueField = "ListaId";
            ddlTipoContrato.DataBind();

            ddlTipoContratoConyuge.DataSource = lstDatosSolicitud;
            ddlTipoContratoConyuge.DataTextField = "ListaDescripcion";
            ddlTipoContratoConyuge.DataValueField = "ListaId";
            ddlTipoContratoConyuge.DataBind();

            ListaSolicitada = "NivelEscolaridad";
            TraerResultadosLista();
            ddlNivelEscolaridad.DataSource = lstDatosSolicitud;
            ddlNivelEscolaridad.DataTextField = "ListaDescripcion";
            ddlNivelEscolaridad.DataValueField = "ListaId";
            ddlNivelEscolaridad.DataBind();

            ddlNivelEscolaridadConyuge.DataSource = lstDatosSolicitud;
            ddlNivelEscolaridadConyuge.DataTextField = "ListaDescripcion";
            ddlNivelEscolaridadConyuge.DataValueField = "ListaId";
            ddlNivelEscolaridadConyuge.DataBind();

            ListaSolicitada = "TipoIdentificacion";
            TraerResultadosLista();
            ddlTipo.DataSource = lstDatosSolicitud;
            ddlTipo.DataTextField = "ListaDescripcion";
            ddlTipo.DataValueField = "ListaId";
            ddlTipo.DataBind();

            ddlTipoConyuge.DataSource = lstDatosSolicitud;
            ddlTipoConyuge.DataTextField = "ListaDescripcion";
            ddlTipoConyuge.DataValueField = "ListaId";
            ddlTipoConyuge.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

  

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = Persona1Servicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["usuario"]);
    }


 
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        CargarCodeudores(txtIdentificacion.Text);
    }

    private void CargarCliente(String pIdObjeto)
    {

        try
        {
            Session[DatosClienteServicio.CodigoProgramaCodeudor + ".NumDoc"] = txtIdentificacion.Text.ToString().Trim();

            vPersona1.identificacion = txtIdentificacion.Text;
            vPersona1.seleccionar = "Identificacion";
            vPersona1.noTraerHuella = 1;
            vPersona1 = DatosClienteServicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);

            // Habilita Razon social solo para Nits
            //if ((ddlTipo.SelectedValue.ToString() != "2") && (ddlTipo.SelectedValue.ToString() != "3"))
            //{
                //Panel2.Enabled = false;
                //Panel1.Visible = false;
            //}
            //else
            //{
                //Panel2.Enabled = true;
                //Panel1.Visible = true;
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
            if (!string.IsNullOrEmpty(vPersona1.celular))
                txtCelular.Text = HttpUtility.HtmlDecode(vPersona1.celular.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.email))
                txtEmail.Text = HttpUtility.HtmlDecode(vPersona1.email.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.empresa))
                txtEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.empresa.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.telefonoempresa))
                txtTelefonoempresa.Text = HttpUtility.HtmlDecode(vPersona1.telefonoempresa.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.direccionempresa))
                txtDireccionTrabajo.Text = HttpUtility.HtmlDecode(vPersona1.direccionempresa.ToString().Trim());
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
            try
            {
                if (vPersona1.barrioResidencia != Int64.MinValue)
                    ddlBarrio.SelectedValue = HttpUtility.HtmlDecode(vPersona1.barrioResidencia.ToString().Trim());
            }
            catch
            { }
            // Nuevos datos conyuge
            if (vPersona1.numHijos != Int64.MinValue)
                txtNumeroHijos.Text = HttpUtility.HtmlDecode(vPersona1.numHijos.ToString().Trim());
            if (vPersona1.numPersonasaCargo != Int64.MinValue)
                txtNumeroPersonasCargo.Text = HttpUtility.HtmlDecode(vPersona1.numPersonasaCargo.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.ocupacion))
                txtOcupacion.Text = HttpUtility.HtmlDecode(vPersona1.ocupacion.ToString().Trim());
            if (vPersona1.salario != Int64.MinValue)
                txtSalario.Text = HttpUtility.HtmlDecode(vPersona1.salario.ToString().Trim());
            if (vPersona1.antiguedadLaboral != Int64.MinValue)
                txtAntiguedadLaboral.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadLaboral.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.CodigoProgramaCodeudor, "ObtenerDatos", ex);
        }

        CalcularEdad();
    }

    private void CargarCodeudores(String pIdObjeto)
    {

        try
        {
            Session[DatosClienteServicio.CodigoProgramaCodeudor + ".NumDoc"] = txtIdentificacion.Text.ToString().Trim();

            vPersona1.identificacion = pIdObjeto;
            vPersona1.seleccionar = "Identificacion";
            vPersona1.noTraerHuella = 1;
            vPersona1 = DatosClienteServicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
            if (vPersona1.identificacion == null)
            {
            }
            else
            {
                if (vPersona1.cod_persona!= Int64.MinValue)
                    txtcodpersona.Text = HttpUtility.HtmlDecode(vPersona1.cod_persona.ToString().Trim());
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
                if (vPersona1.fechanacimiento ==null)
                {
                    txtFechanacimiento.Text = DateTime.Now.ToShortDateString();                   
                }
                else
                {
                    if (vPersona1.fechanacimiento != DateTime.MinValue)
                    {
                        txtFechanacimiento.Text = HttpUtility.HtmlDecode(vPersona1.fechanacimiento.Value.ToShortDateString());
                    }
                }
                if (vPersona1.codciudadnacimiento == null)
                    ddlLugarNacimiento.SelectedValue = "0";
                else
                {
                    if (vPersona1.codciudadnacimiento != Int64.MinValue)
                        ddlLugarNacimiento.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadnacimiento.ToString().Trim());
                }
                if (vPersona1.codestadocivil == 0 || vPersona1.codestadocivil==null)
                    ddlEstadoCivil.SelectedValue = "1";
                else
                {
                    if (vPersona1.codestadocivil != Int64.MinValue)
                        ddlEstadoCivil.SelectedValue=HttpUtility.HtmlDecode(vPersona1.codestadocivil.ToString().Trim());
                }
                validarEstadoCivil();
                if (vPersona1.codescolaridad == 0 || vPersona1.codescolaridad == null)
                    ddlNivelEscolaridad.SelectedValue = "1";
                else
                {
                    if (vPersona1.codescolaridad != Int64.MinValue)
                        ddlNivelEscolaridad.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codescolaridad.ToString().Trim());
                }
                if (vPersona1.codactividad == null)
                    ddlActividad.SelectedValue = "0010";
                else
                {
                    if (vPersona1.codactividad != Int64.MinValue)
                    {
                        ddlActividad.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codactividad.ToString().Trim());
                    }
                }
                if (!string.IsNullOrEmpty(vPersona1.direccion))
                {
                    txtDireccion.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
                }
                if (!string.IsNullOrEmpty(vPersona1.telefono))
                {
                    txtTelefono.Text = HttpUtility.HtmlDecode(vPersona1.telefono.ToString().Trim());
                }
                if (vPersona1.codciudadresidencia == null)
                    ddlLugarResidencia.SelectedValue = "0";
                else
                {
                    if (vPersona1.codciudadresidencia != Int64.MinValue)
                    {
                        ddlLugarResidencia.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadresidencia.ToString().Trim());
                    }
                }
                if (vPersona1.antiguedadlugar != Int64.MinValue)
                    txtAntiguedadlugar.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugar.ToString().Trim());
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
                if (!string.IsNullOrEmpty(vPersona1.direccionempresa))
                    txtDireccionTrabajo.Text = HttpUtility.HtmlDecode(vPersona1.direccionempresa.ToString().Trim());
                if (vPersona1.codcargo == 0)
                    ddlCargo.SelectedValue = "1";
                else
                {
                    if (vPersona1.codcargo != Int64.MinValue)
                        ddlCargo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codcargo.ToString().Trim());
                }
                if (vPersona1.codtipocontrato == 0)
                    ddlTipoContrato.SelectedValue = "1";
                else
                {
                    if (vPersona1.codtipocontrato != Int64.MinValue)
                        ddlTipoContrato.Text = HttpUtility.HtmlDecode(vPersona1.codtipocontrato.ToString().Trim());
                }
                if (vPersona1.cod_asesor != Int64.MinValue)
                    txtCod_asesor.Text = HttpUtility.HtmlDecode(vPersona1.cod_asesor.ToString().Trim());
                if (vPersona1.fecha_residencia != DateTime.MinValue)
                    txtFecha_residencia.Text = HttpUtility.HtmlDecode(vPersona1.fecha_residencia.ToShortDateString());
                if (vPersona1.cod_oficina != Int64.MinValue)
                    txtCod_oficina.Text = HttpUtility.HtmlDecode(vPersona1.cod_oficina.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.tratamiento))
                    txtTratamiento.Text = HttpUtility.HtmlDecode(vPersona1.tratamiento.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.estado))
                    ddlEstado.SelectedValue = HttpUtility.HtmlDecode(vPersona1.estado.ToString().Trim());
                if (vPersona1.barrioResidencia != Int64.MinValue)
                    if(vPersona1.barrioResidencia != null)
                    {
                        try
                        {
                            ddlBarrio.SelectedValue = HttpUtility.HtmlDecode(vPersona1.barrioResidencia.ToString().Trim());
                        }
                        catch { }
                    }

                // Nuevos datos conyuge
                if (vPersona1.numHijos != Int64.MinValue)
                    txtNumeroHijos.Text = HttpUtility.HtmlDecode(vPersona1.numHijos.ToString().Trim());
                if (vPersona1.numPersonasaCargo != Int64.MinValue)
                    txtNumeroPersonasCargo.Text = HttpUtility.HtmlDecode(vPersona1.numPersonasaCargo.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.ocupacion))
                    txtOcupacion.Text = HttpUtility.HtmlDecode(vPersona1.ocupacion.ToString().Trim());
                if (vPersona1.salario != Int64.MinValue)
                    txtSalario.Text = HttpUtility.HtmlDecode(vPersona1.salario.ToString().Trim());
                if (vPersona1.antiguedadLaboral != Int64.MinValue)
                    txtAntiguedadLaboral.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadLaboral.ToString().Trim());
                codconyuge = Convert.ToInt64(vPersona1.cod_persona);
                ObtenerDatosConyuge(codconyuge);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.CodigoProgramaCodeudor, "ObtenerDatosCodeudores", ex);
        }

        CalcularEdad();
    }

   

    protected void ObtenerDatos(String pIdObjeto)
    {
        //try
        //{
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            vPersona1 = Persona1Servicio.ConsultarPersona1(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            Xpinn.FabricaCreditos.Entities.codeudores vCodeudores = new Xpinn.FabricaCreditos.Entities.codeudores();
            vCodeudores = CodeudoresServicio.Consultarcodeudores(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);

            Session["CodCodeudor"] = vPersona1.cod_persona; // Variable de sesion que permite consultar el conyuge del codeudor
           
            if (!string.IsNullOrEmpty(vPersona1.tipo_persona))
                rblTipoPersona.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_persona.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.identificacion))
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vPersona1.identificacion.ToString().Trim());
            if (vPersona1.digito_verificacion != Int64.MinValue)
                txtDigito_verificacion.Text = HttpUtility.HtmlDecode(vPersona1.digito_verificacion.ToString().Trim());

            if (vPersona1.tipo_identificacion == 0)
            {
                ddlTipo.SelectedValue = "0";
            }
            else
            {
                if (vPersona1.tipo_identificacion != Int64.MinValue)
                    ddlTipo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_identificacion.ToString().Trim());
            }
            if (vPersona1.fechaexpedicion == null)
            {
                txtFechaexpedicion.Text = "01/01/2001";
            }
            else
            {
                if (vPersona1.fechaexpedicion != DateTime.MinValue)
                    txtFechaexpedicion.Text = HttpUtility.HtmlDecode(vPersona1.fechaexpedicion.Value.ToShortDateString());
            }
            if (vPersona1.codciudadexpedicion != Int64.MinValue)
                ddlLugarExpedicion.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadexpedicion.ToString().Trim());
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
            if (vPersona1.codciudadnacimiento == 0)
            {
                ddlLugarNacimiento.SelectedValue = "0";
            }
            else
            {
                if (vPersona1.codciudadnacimiento != Int64.MinValue)
                    ddlLugarNacimiento.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadnacimiento.ToString().Trim());
            }
            if (vPersona1.codestadocivil == 0)
            {
                ddlEstadoCivil.SelectedValue = "0";
            }
            else
            {
                // Variable de sesion que permite conocer estado civil del codeudor
                if (vPersona1.codestadocivil != Int64.MinValue)
                {
                    ddlEstadoCivil.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codestadocivil.ToString().Trim());
                    if (ddlEstadoCivil.SelectedValue == "1" || ddlEstadoCivil.SelectedValue == "3")
                    Session["EstadoCodeudor"] = "1";                    
                    else
                    Session["EstadoConyuge"] = "0";                    
                }
            }

            if (vPersona1.codescolaridad != Int64.MinValue || vPersona1.codescolaridad == null)
                ddlNivelEscolaridad.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codescolaridad.ToString().Trim());
            if (vPersona1.codactividad != Int64.MinValue)
                ddlActividad.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codactividad.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.direccion))
                txtDireccion.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.telefono))
                txtTelefono.Text = HttpUtility.HtmlDecode(vPersona1.telefono.ToString().Trim());
            if (vPersona1.codciudadresidencia != Int64.MinValue)
                ddlLugarResidencia.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadresidencia.ToString().Trim());
            if (vPersona1.antiguedadlugar != Int64.MinValue)
                txtAntiguedadlugar.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugar.ToString().Trim());            
            if (vPersona1.tipovivienda == "0")
            {
                rblTipoVivienda.SelectedValue = "P";
            }
            if (vPersona1.tipovivienda == "P")
            {
                rblTipoVivienda.SelectedValue = "P";
            }
            else
            {
                if (!string.IsNullOrEmpty(vPersona1.tipovivienda))
                    rblTipoVivienda.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipovivienda.ToString().Trim());
            }
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
            if (vPersona1.codcargo == 0)
            {
                ddlCargo.SelectedValue = "0";
            }
            else
            {
                if (vPersona1.codcargo != Int64.MinValue)
                    ddlCargo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codcargo.ToString().Trim());
            }
            if (vPersona1.codtipocontrato == 0)
            {
                ddlTipoContrato.SelectedValue = "0";
            }
            else
            {
            if (vPersona1.codtipocontrato != Int64.MinValue)
                ddlTipoContrato.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codtipocontrato.ToString().Trim());
            }
            if (vPersona1.cod_asesor != Int64.MinValue)
                txtCod_asesor.Text = HttpUtility.HtmlDecode(vPersona1.cod_asesor.ToString().Trim());

            if (vPersona1.residente == "1")
            {
                rblResidente.SelectedValue = "S";
            }
            if (vPersona1.residente == null ||vPersona1.residente == "0")
            {
                rblResidente.SelectedValue = "S";                     
                if (!string.IsNullOrEmpty(vPersona1.residente))
                    rblResidente.SelectedValue = HttpUtility.HtmlDecode(vPersona1.residente.ToString().Trim());
            }                        
            if (vPersona1.fecha_residencia != DateTime.MinValue)
                txtFecha_residencia.Text = HttpUtility.HtmlDecode(vPersona1.fecha_residencia.ToShortDateString());
            if (vPersona1.cod_oficina != Int64.MinValue)
                txtCod_oficina.Text = HttpUtility.HtmlDecode(vPersona1.cod_oficina.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.tratamiento))
                txtTratamiento.Text = HttpUtility.HtmlDecode(vPersona1.tratamiento.ToString().Trim());
            if (vPersona1.estado == null)
                ddlEstado.SelectedValue = "0";
            else
            {
                if (!string.IsNullOrEmpty(vPersona1.estado))
                    ddlEstado.SelectedValue = HttpUtility.HtmlDecode(vPersona1.estado.ToString().Trim());
            }
            if (vPersona1.barrioResidencia != null)
            {
                if (vPersona1.barrioResidencia != Int64.MinValue)
                {
                    try
                    {
                        ddlBarrio.SelectedValue = HttpUtility.HtmlDecode(vPersona1.barrioResidencia.ToString().Trim());
                    }
                    catch { }
                }
            }
            if (!string.IsNullOrEmpty(vPersona1.direccionempresa))
                txtDireccionTrabajo.Text = HttpUtility.HtmlDecode(vPersona1.direccionempresa.ToString().Trim());

            // Nuevos datos conyuge
            if (vPersona1.numHijos != Int64.MinValue)
                txtNumeroHijos.Text = HttpUtility.HtmlDecode(vPersona1.numHijos.ToString().Trim());
            if (vPersona1.numPersonasaCargo != Int64.MinValue)
                txtNumeroPersonasCargo.Text = HttpUtility.HtmlDecode(vPersona1.numPersonasaCargo.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.ocupacion))
                txtOcupacion.Text = HttpUtility.HtmlDecode(vPersona1.ocupacion.ToString().Trim());
            if (vPersona1.salario != Int64.MinValue)
                txtSalario.Text = HttpUtility.HtmlDecode(vPersona1.salario.ToString().Trim());
            if (vPersona1.antiguedadLaboral  != Int64.MinValue)
                txtAntiguedadLaboral.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadLaboral.ToString().Trim());
            
            validarArriendo();
            validarEstadoCivil();

            //Datos tabla Codeudor:
            if (vPersona1.parentesco == null)
            {
                ddlParentesco.SelectedValue = "11";
            }
            else
            {
                if (vCodeudores.parentesco != Int64.MinValue)
                    ddlParentesco.SelectedValue = HttpUtility.HtmlDecode(vCodeudores.parentesco.ToString().Trim());
            }
            if (!string.IsNullOrEmpty(vCodeudores.opinion))
                rblOpinion.SelectedValue = HttpUtility.HtmlDecode(vCodeudores.opinion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCodeudores.responsabilidad))
                rblResponsabilidad.SelectedValue = HttpUtility.HtmlDecode(vCodeudores.responsabilidad.ToString().Trim());
            /////OBTENER DATOS DEL CONYUGE
            if (txtcodpersona.Text.Trim() != "")
                ObtenerDatosConyuge(Convert.ToInt64(txtcodpersona.Text));
        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "ObtenerDatos", ex);
        //}
    
    }

    public static int GetAge(DateTime birthDate)
    {
        return (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.242199);
    }

    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {              
        string TipoPersona = "";
        TipoPersona = Session["TipoIdentificacion"].ToString();

        Int64 tipoempresa = 0;
        Usuario usuap = (Usuario)Session["usuario"];
        tipoempresa = Convert.ToInt64(usuap.tipo);

        if (tipoempresa == 2)
        {
            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            btnAdelante.ImageUrl = "~/Images/btnPlanPagos.jpg";
            btnAdelante.PostBackUrl = "~/Page/FabricaCreditos/Solicitud/PlanPagos/Lista.aspx";
        }
        if (tipoempresa == 1)
        {
            Session["CodCodeudor"] = Label1.Text;

            if (gvListaAfliados.Visible == true) //Si ya registro el codeudor puede registar su patrimonio
            {
                Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionCodeudor/PatrimonioCodeudor/Default.aspx");
            }
        }

    }

    protected void btnAdelante2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionCodeudor/Referencias/Lista.aspx");
    }

    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Int64 tipoempresa = 0;
        Usuario usuap = (Usuario)Session["usuario"];
        tipoempresa = Convert.ToInt64(usuap.tipo);

        if (tipoempresa == 2)
        {

            Response.Redirect("~/Page/FabricaCreditos/Solicitud/DatosSolicitud/SolicitudCredito.aspx");

        }
        if (tipoempresa == 1)
        {
            if (Session["Retorno"].ToString() == "1")
                Response.Redirect("~/Page/FabricaCreditos/Solicitud/ModificarSolicitud/SolicitudCredito.aspx");
            else
                Response.Redirect("~/Page/FabricaCreditos/Solicitud/DatosSolicitud/SolicitudCredito.aspx");
        }
    }

    protected void btnGuardar_Click1(object sender, ImageClickEventArgs e)
    {
       
        try
        {
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            Xpinn.FabricaCreditos.Entities.codeudores vCodeudores = new Xpinn.FabricaCreditos.Entities.codeudores();

            if (idObjeto != "")
                vPersona1 = DatosClienteServicio.ConsultarPersona1(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vPersona1.origen = "Solicitud";     //Permite reconocer que se modifica persona desde el formulario "Solicitud"

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
            vPersona1.codciudadresidencia = null;
            if (txtAntiguedadlugar.Text != "") vPersona1.antiguedadlugar = Convert.ToInt64(txtAntiguedadlugar.Text.Trim());

            if (vPersona1.tipovivienda == "0")
            {
                rblTipoVivienda.SelectedValue = "P";
            }
            else
            {
                vPersona1.tipovivienda = (rblTipoVivienda.Text != "") ? Convert.ToString(rblTipoVivienda.SelectedValue) : String.Empty;
            }

            vPersona1.arrendador = (txtArrendador.Text != "") ? Convert.ToString(txtArrendador.Text.Trim()) : String.Empty;
            vPersona1.telefonoarrendador = (txtTelefonoarrendador.Text != "") ? Convert.ToString(txtTelefonoarrendador.Text.Trim()) : String.Empty;
            vPersona1.celular = (txtCelular.Text != "") ? Convert.ToString(txtCelular.Text.Trim()) : String.Empty;
            vPersona1.email = (txtEmail.Text != "") ? Convert.ToString(txtEmail.Text.Trim()) : String.Empty;
            vPersona1.empresa = (txtEmpresa.Text != "") ? Convert.ToString(txtEmpresa.Text.Trim()) : String.Empty;
            vPersona1.telefonoempresa = (txtTelefonoempresa.Text != "") ? Convert.ToString(txtTelefonoempresa.Text.Trim()) : String.Empty;
            vPersona1.direccionempresa = (txtDireccionTrabajo.Text != "") ? Convert.ToString(txtDireccionTrabajo.Text.Trim()) : String.Empty;

            if (ddlCargo.Text != "") vPersona1.codcargo = Convert.ToInt64(ddlCargo.Text.Trim());
            if (ddlTipoContrato.Text != "") vPersona1.codtipocontrato = Convert.ToInt64(ddlTipoContrato.SelectedValue);
            if (txtCod_asesor.Text != "") vPersona1.cod_asesor = Convert.ToInt64(txtCod_asesor.Text.Trim());
            vPersona1.residente = (rblResidente.Text != "") ? Convert.ToString(rblResidente.SelectedValue): String.Empty;
            if (txtFecha_residencia.Text != "") vPersona1.fecha_residencia = Convert.ToDateTime(txtFecha_residencia.Text.Trim());
            if (txtCod_oficina.Text != "") vPersona1.cod_oficina = Convert.ToInt64(txtCod_oficina.Text.Trim());
            vPersona1.tratamiento = (txtTratamiento.Text != "") ? Convert.ToString(txtTratamiento.Text.Trim()) : String.Empty;
            vPersona1.estado = (ddlEstado.Text != "") ? Convert.ToString(ddlEstado.SelectedValue) : String.Empty;
            vPersona1.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
            Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
            usuario = (Xpinn.Util.Usuario)Session["usuario"];
            vPersona1.usuariocreacion = usuario.nombre;
            vPersona1.fecultmod = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
            vPersona1.usuultmod = usuario.nombre;
            if (ddlLugarResidencia.Text != "") 
                if (ddlLugarResidencia.Text != "") 
                    vPersona1.codciudadresidencia = Convert.ToInt64(ddlLugarResidencia.SelectedValue);
            try
            {
                if (ddlBarrio.SelectedValue != null)
                    if (ddlBarrio.SelectedValue != "") 
                        vPersona1.barrioResidencia = Convert.ToInt64(ddlBarrio.SelectedValue);                
            }
            catch
            {
                vPersona1.barrioResidencia = null;
            }
            vPersona1.dirCorrespondencia = "";
            vPersona1.barrioCorrespondencia = 0;
            vPersona1.telCorrespondencia = "";
            vPersona1.ciuCorrespondencia = 0;
                      
            // Nuevos datos conyuge
            vPersona1.numHijos = (txtNumeroHijos.Text != "") ? Convert.ToInt64(txtNumeroHijos.Text.Trim()) : 0;
            vPersona1.numPersonasaCargo = (txtNumeroPersonasCargo.Text != "") ? Convert.ToInt64(txtNumeroPersonasCargo.Text.Trim()) : 0;
            vPersona1.ocupacion = (txtOcupacion.Text != "") ? Convert.ToString(txtOcupacion.Text.Trim()) : String.Empty;
            vPersona1.salario = (txtSalario.Text != "") ? Convert.ToInt64(txtSalario.Text.Trim().Replace(gSeparadorMiles, "")) : 0;
            vPersona1.antiguedadLaboral = (txtAntiguedadLaboral.Text != "") ? Convert.ToInt64(txtAntiguedadLaboral.Text.Trim()) : 0;

            // Asignar el número de solicitud
            vPersona1.numeroRadicacion = Convert.ToInt64(txtNumeroRadicacion.Text);   // Adiciona el numero de radicacion para poder relacionar los codeudores

            //Toma datos para registrar en tabla codeudor
            vCodeudores.numero_radicacion = vPersona1.numeroRadicacion;
            vCodeudores.identificacion = vPersona1.identificacion;
            
            vCodeudores.tipo_codeudor = "C";
            if (ddlParentesco.Text != "") vCodeudores.parentesco = Convert.ToInt64(ddlParentesco.SelectedValue);
            if (rblOpinion.Text != "") vCodeudores.opinion = Convert.ToString(rblOpinion.SelectedValue);
            if (rblResponsabilidad.Text != "") vCodeudores.responsabilidad = Convert.ToString(rblResponsabilidad.SelectedValue);
                
            vPersona1.ActividadEconomicaEmpresa = null;
            vPersona1.ciudad = null;
            vPersona1.relacionEmpleadosEmprender = 0;
            vPersona1.CelularEmpresa = " ";
            vPersona1.profecion = " ";
            vPersona1.Estrato = 0;
            vPersona1.PersonasAcargo = 0;
            idObjeto = txtIdentificacion.Text;

            if (idObjeto != "0" && idObjeto != "")
            {
                vPersona1.cod_persona = Convert.ToInt64(txtcodpersona.Text);
                DatosClienteServicio.ModificarPersona1(vPersona1, (Usuario)Session["usuario"]);
                Session["CodCodeudor"] = vPersona1.cod_persona;

                // Registrar dato de codeudor
                string sError = "";
                VerError("");
                CodeudoresServicio.ValidarCodeudor(vCodeudores, (Usuario)Session["usuario"], ref sError);
                if (sError.Trim() != "")
                {
                    VerError(sError);
                    return;
                }

                // Toma el codigo de la persona modificada para editarla en la tabla codeudor
                vCodeudores.codpersona = vPersona1.cod_persona;  
                vCodeudores = CodeudoresServicio.Modificarcodeudores(vCodeudores, (Usuario)Session["usuario"]);
            }
            else
            {
                // Crear el codeudor como Persona
                vPersona1 = Persona1Servicio.CrearPersona1(vPersona1, (Usuario)Session["usuario"]);            
            }
            idObjeto = txtcodpersona.Text;
            if (idObjeto != "0" || idObjeto != "")
            {
                idObjeto = vPersona1.cod_persona.ToString();
                // Validar datos del codeudor
                string sError = "";
                VerError("");
                CodeudoresServicio.ValidarCodeudor(vCodeudores, (Usuario)Session["usuario"], ref sError);
                if (sError.Trim() != "")
                {
                    VerError(sError);
                    return;
                }
                   idObjeto = vPersona1.cod_persona.ToString();
                Session["CodCodeudor"] = vPersona1.cod_persona; 
                vCodeudores.codpersona = vPersona1.cod_persona;  // Toma el codigo de la persona registrarla para editarla en la tabla codeudor
                //vCodeudores = CodeudoresServicio.Crearcodeudores(vCodeudores, (Usuario)Session["usuario"]);  MODIFICADO 
                Actualizar();
            }

            Label1.Text = Session["CodCodeudor"].ToString();

            Session[Persona1Servicio.CodigoProgramaCodeudor + ".id"] = idObjeto;
      
        }
        catch (Exception ex)
        {
            VerError("Se presentaron errores al grabar los datos del codeudor. btnGuardar_Click:" + ex.Message);
        }
        Actualizar();

        //-------------------------------------------- Guardar Conyuge --------------------------------------------

        //Se realiza Consulta en conyuge con el codpersona del codeudor
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstConsulta = Persona1Servicio.ListarPersona1(ObtenerValoresConyuge(), (Usuario)Session["usuario"]);

        gvListaConyuge.DataSource = lstConsulta;

        if (lstConsulta.Count > 0) //Si existe conyuge se debe actualizar, sino se debe crear
        {
            PanelListaCon.Visible = false;
            gvListaConyuge.Visible = false;
            //lblTotalRegsConyuge.Visible = false;
          //  lblTotalRegsConyuge.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
            gvListaConyuge.DataBind();
            idObjeto = gvListaConyuge.Rows[0].Cells[0].Text;  // Toma el id del conyuge
            ValidarPermisosGrilla(gvListaConyuge);
        }
        else
        {
            idObjeto = "";
            PanelListaCon.Visible = false;
            gvListaConyuge.Visible = false;
         //   lblTotalRegsConyuge.Text = "";
           // lblTotalRegsConyuge.Visible = false;
        }

        Session.Add(Persona1Servicio.CodigoProgramaCodeudor + ".consulta", 1);

        if (txtIdentificacionConyuge.Text != Convert.ToString(Session["Identificacion"]) && txtIdentificacionConyuge.Text != "")
        {
            if (ddlEstadoCivil.SelectedValue == "1" || ddlEstadoCivil.SelectedValue == "3")
            {
                GuardaConyuge(txtIdentificacionConyuge.Text);
                Conyuge(); //Muestra los resultados de la edicion del conyuge
                Session["CodCodeudor"] = Label1.Text;
            }
        }
   
    }

    protected void btnConsultar_Click1(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, Persona1Servicio.CodigoProgramaCodeudor);
      //  Borrar();
        Actualizar();
        if(Session["CodCodeudor"].ToString() != "") //Actualiza conyuge codeudor solo si existe codeudor
            ActualizarConyuge();
        
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
  

    private Xpinn.FabricaCreditos.Entities.Persona1 ObtenerValoresConyuge()
    {
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        string CodCodeudor = Session["CodCodeudor"].ToString();
        if (CodCodeudor == "")
            vPersona1.cod_persona = 0;
        else
            vPersona1.cod_persona = Convert.ToInt64(CodCodeudor);      //Captura el codigo del codeudor

        if (txtIdentificacionConyuge.Text.Trim() != "")
            vPersona1.identificacion = Convert.ToString(txtIdentificacionConyuge.Text.Trim());
        if (txtDigito_verificacionConyuge.Text.Trim() != "")
            vPersona1.digito_verificacion = Convert.ToInt64(txtDigito_verificacionConyuge.Text.Trim());

        if (txtFechaexpedicionConyuge.Text.Trim() != "")
            vPersona1.fechaexpedicion = Convert.ToDateTime(txtFechaexpedicionConyuge.Text.Trim());

        if (txtPrimer_nombreConyuge.Text.Trim() != "")
            vPersona1.primer_nombre = Convert.ToString(txtPrimer_nombreConyuge.Text.Trim());
        if (txtSegundo_nombreConyuge.Text.Trim() != "")
            vPersona1.segundo_nombre = Convert.ToString(txtSegundo_nombreConyuge.Text.Trim());
        if (txtPrimer_apellidoConyuge.Text.Trim() != "")
            vPersona1.primer_apellido = Convert.ToString(txtPrimer_apellidoConyuge.Text.Trim());
        if (txtSegundo_apellidoConyuge.Text.Trim() != "")
            vPersona1.segundo_apellido = Convert.ToString(txtSegundo_apellidoConyuge.Text.Trim());
        if (txtRazon_socialConyuge.Text.Trim() != "")
            vPersona1.razon_social = Convert.ToString(txtRazon_socialConyuge.Text.Trim());
        if (txtFechanacimientoConyuge.Text.Trim() != "")
            vPersona1.fechanacimiento = Convert.ToDateTime(txtFechanacimientoConyuge.Text.Trim());

        if (txtDireccionConyuge.Text.Trim() != "")
            vPersona1.direccion = Convert.ToString(txtDireccionConyuge.Text.Trim());
        if (txtTelefonoConyuge.Text.Trim() != "")
            vPersona1.telefono = Convert.ToString(txtTelefonoConyuge.Text.Trim());

        if (txtAntiguedadlugarConyuge.Text.Trim() != "")
            vPersona1.antiguedadlugar = Convert.ToInt64(txtAntiguedadlugarConyuge.Text.Trim());

        if (txtArrendadorConyuge.Text.Trim() != "")
            vPersona1.arrendador = Convert.ToString(txtArrendadorConyuge.Text.Trim());
        if (txtTelefonoarrendadorConyuge.Text.Trim() != "")
            vPersona1.telefonoarrendador = Convert.ToString(txtTelefonoarrendadorConyuge.Text.Trim());
        if (txtCelularConyuge.Text.Trim() != "")
            vPersona1.celular = Convert.ToString(txtCelularConyuge.Text.Trim());
        if (txtEmailConyuge.Text.Trim() != "")
            vPersona1.email = Convert.ToString(txtEmailConyuge.Text.Trim());
        if (txtEmpresaConyuge.Text.Trim() != "")
            vPersona1.empresa = Convert.ToString(txtEmpresaConyuge.Text.Trim());
        if (txtTelefonoempresaConyuge.Text.Trim() != "")
            vPersona1.telefonoempresa = Convert.ToString(txtTelefonoempresaConyuge.Text.Trim());

        if (txtCod_asesorConyuge.Text.Trim() != "")
            vPersona1.cod_asesor = Convert.ToInt64(txtCod_asesorConyuge.Text.Trim());

        if (txtFecha_residenciaConyuge.Text.Trim() != "")
            vPersona1.fecha_residencia = Convert.ToDateTime(txtFecha_residenciaConyuge.Text.Trim());
        if (txtCod_oficinaConyuge.Text.Trim() != "")
            vPersona1.cod_oficina = Convert.ToInt64(txtCod_oficinaConyuge.Text.Trim());
        if (txtTratamientoConyuge.Text.Trim() != "")
            vPersona1.tratamiento = Convert.ToString(txtTratamientoConyuge.Text.Trim());

        // Campos nuevos conyuge
        if (txtNumeroHijosConyuge.Text.Trim() != "")
            vPersona1.numHijos = Convert.ToInt64(txtNumeroHijosConyuge.Text.Trim());
        if (txtNumeroPersonasCargoConyuge.Text.Trim() != "")
            vPersona1.numPersonasaCargo = Convert.ToInt64(txtNumeroPersonasCargoConyuge.Text.Trim());
        if (txtOcupacionConyuge.Text.Trim() != "")
            vPersona1.ocupacion = txtOcupacionConyuge.Text.Trim();
        string salario = txtSalario.Text.Replace(".", "");
        if (txtSalarioConyuge.Text.Trim() != "")
            vPersona1.salario = Convert.ToInt64(salario);
        if (txtAntiguedadLaboralConyuge.Text.Trim() != "")
            vPersona1.antiguedadLaboral = Convert.ToInt64(txtAntiguedadLaboralConyuge.Text.Trim());

        vPersona1.seleccionar = "C"; //Bandera para ejecuta el select del Conyuge

        return vPersona1;
    }

    protected void ddlEstadoCivil_SelectedIndexChanged(object sender, EventArgs e)
    {
        validarEstadoCivil();       
    }

    private void validarEstadoCivil()
    { 
     if (ddlEstadoCivil.SelectedValue == "1" || ddlEstadoCivil.SelectedValue == "3")
        {
            pnlConyuge.Visible = true;
            gvListaConyuge.Visible = true;
        }
        else
        {
            pnlConyuge.Visible = false;
        }    
    }
    
    protected void ddlCargo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtFechanacimiento_TextChanged(object sender, EventArgs e)
    {
        CalcularEdad();
    }

    private void CalcularEdad()
    {
        if (txtFechanacimiento.Text != "")
            lblEdad.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtFechanacimiento.Text)));
    }

    protected void txtFechanacimientoConyuge_TextChanged(object sender, EventArgs e)
    {
        CalcularEdadConyuge();
    }

    private void CalcularEdadConyuge()
    {
        if (txtFechanacimientoConyuge.Text != "")
            lblEdadConyuge.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtFechanacimientoConyuge.Text)));
    }

    protected void txtPrimer_apellidoConyuge_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
       
    }

    protected void btnConsultarCod_Click(object sender, ImageClickEventArgs e)
    {
       
    }

    protected void btnGrabarSeguimiento_Click(object sender, ImageClickEventArgs e)
    {
        CargarCodeudores(txtIdentificacion.Text);

    }

    protected void btnConsultar0_Click(object sender, ImageClickEventArgs e)
    {
        CargarCodeudores(txtIdentificacion.Text);

    }

    protected void gvListaAf_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void gvListaAfliados_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvListaAfliados.Rows[e.RowIndex].Cells[0].Text);
            long solicitud = Convert.ToInt64(txtNumeroRadicacion.Text);
            //Persona1Servicio.EliminarPersona1(id, (Usuario)Session["usuario"]);
            CodeudoresServicio.EliminarcodeudoresSol(id, solicitud, (Usuario)Session["usuario"]);
            //ConyugeServicio.EliminarConyuge(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "gvLista_RowDeleting", ex);
        }
    }

}