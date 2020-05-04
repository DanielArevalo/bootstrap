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
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
           
    Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
    string EstadoCodeudor = null;
      
    //Listas:
    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            EstadoCodeudor = Session["EstadoCodeudor"].ToString();
            VisualizarOpciones(Persona1Servicio.CodigoProgramaCodeudor, "L");

            Site2 toolBar = (Site2)this.Master;
            //toolBar.eventoNuevo += btnNuevo_Click;
            //toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;
            //toolBar.eventoGuardar += btnGuardar_Click;
            //toolBar.eventoAdelante += btnAdelante_Click;
            //toolBar.eventoAdelante2 += btnAdelante2_Click;
            //toolBar.eventoAtras += btnAtras_Click;

            //((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            //((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
            
            //ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            //btnAdelante.ValidationGroup = "";
            //btnAdelante.ImageUrl = "~/Images/btnConyugeCodeudor.jpg";

            //ImageButton btnAdelante2 = Master.FindControl("btnAdelante2") as ImageButton;
            //btnAdelante2.ValidationGroup = "";
            //btnAdelante2.ImageUrl = "~/Images/btnReferenciasCodeudor.jpg";

           
            //Actualizar();            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, Persona1Servicio.CodigoPrograma);
                CargarListas();

                if (Session[Persona1Servicio.GetType().Name + ".consulta"] != null)
                {
                    if (Session["CodCodeudor"].ToString() != "0" && Session["EstadoCodeudor"].ToString() != "0")
                        Actualizar();                   
                }
                    
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.GetType().Name, "Page_Load", ex);
        }
    }


    //protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    //{               
    //        GuardarValoresConsulta(pConsulta, Persona1Servicio.CodigoProgramaCodeudor);
    //        //Navegar(Pagina.Nuevo); 
    //        Borrar();
    //}

    private void Borrar()
    {
        CargarListas();

        //ddlTipo.SelectedValue = 0;
        txtIdentificacionConyuge.Text = "";
        txtPrimer_nombreConyuge.Text = "";
        txtSegundo_nombreConyuge.Text = "";
        txtPrimer_apellidoConyuge.Text = "";
        txtSegundo_apellidoConyuge.Text = "";
        //ddlLugarResidencia.SelectedValue = 0;
        txtDireccionConyuge.Text = "";
        txtTelefonoConyuge.Text = "";
        txtRazon_socialConyuge.Text = "";
        //ddlActividad.SelectedValue = 0;
        //rblTipoPersona.SelectedValue = 0;
        txtDigito_verificacionConyuge.Text = "";
        txtFechaexpedicionConyuge.Text = "";
        //ddlLugarExpedicion.SelectedValue = 0;
        //rblSexo.SelectedValue = 0;
        //ddlNivelEscolaridad.SelectedValue = 0;
        //ddlEstadoCivil.SelectedValue = 0;
        //ddlLugarNacimiento.SelectedValue = 0;
        txtFechanacimientoConyuge.Text = "";
        txtAntiguedadlugarConyuge.Text = "";
        //rblTipoVivienda.SelectedValue = 0;
        txtTratamientoConyuge.Text = "";
        txtCelularConyuge.Text = "";
        txtTelefonoarrendadorConyuge.Text = "";
        txtArrendadorConyuge.Text = "";
        txtTelefonoempresaConyuge.Text = "";
        //ddlCargo.SelectedValue = 0;
        txtEmailConyuge.Text = "";
        txtEmpresaConyuge.Text = "";
        //rblResidente.SelectedValue = 0;
        txtFecha_residenciaConyuge.Text = "";
        //ddlTipoContrato.SelectedValue = 0;
        txtCod_asesorConyuge.Text = "";



        idObjeto = "";
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
        //String id = gvLista.SelectedRow.Cells[0].Text;
        String id = gvListaConyuge.Rows[0].Cells[0].Text;
        Session[Persona1Servicio.CodigoProgramaCodeudor + ".id"] = id;       
        Edicion();
    }
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvListaConyuge.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[Persona1Servicio.CodigoProgramaCodeudor + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvListaConyuge.Rows[e.RowIndex].Cells[0].Text);
            Persona1Servicio.EliminarPersona1(id, (Usuario)Session["usuario"]);
            ConyugeServicio.EliminarConyuge(id, (Usuario)Session["usuario"]);
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
            gvListaConyuge.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            //Se realiza Consulta en conyuge con el codpersona del codeudor
            


            List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            lstConsulta = Persona1Servicio.ListarPersona1(ObtenerValores(), (Usuario)Session["usuario"]);

            gvListaConyuge.PageSize = 15;
            gvListaConyuge.EmptyDataText = "No se encontraron registros";
            gvListaConyuge.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                try
                {
                    gvListaConyuge.Visible = true;
                    lblTotalRegsConyuge.Visible = false;
                    lblTotalRegsConyuge.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    gvListaConyuge.DataBind();
                    ValidarPermisosGrilla(gvListaConyuge);
                }
                finally
                {
                    DetalleFila();
                }
            }
            else
            {
                //Si no se ha registrado codeudor va a conyuge

                idObjeto = "";
                gvListaConyuge.Visible = false;
                lblTotalRegsConyuge.Text = "";
                lblTotalRegsConyuge.Visible = false;
                //Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFamiliar/Conyuge/Nuevo.aspx");
                //Navegar(Pagina.Editar);
            }

            Session.Add(Persona1Servicio.CodigoProgramaCodeudor + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.Persona1 ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        string CodCodeudor = Session["CodCodeudor"].ToString();
        if (CodCodeudor == "")
            vPersona1.cod_persona = 0;  
        else
            vPersona1.cod_persona = Convert.ToInt64(CodCodeudor);      //Captura el codigo del codeudor
       
        if(txtIdentificacionConyuge.Text.Trim() != "")
            vPersona1.identificacion = Convert.ToString(txtIdentificacionConyuge.Text.Trim());
        if(txtDigito_verificacionConyuge.Text.Trim() != "")
            vPersona1.digito_verificacion = Convert.ToInt64(txtDigito_verificacionConyuge.Text.Trim());
        //if(txtTipo_identificacion.Text.Trim() != "")
        //    vPersona1.tipo_identificacion = Convert.ToInt64(txtTipo_identificacion.Text.Trim());
        if(txtFechaexpedicionConyuge.Text.Trim() != "")
            vPersona1.fechaexpedicion = Convert.ToDateTime(txtFechaexpedicionConyuge.Text.Trim());
        //if(txtCodciudadexpedicion.Text.Trim() != "")
        //    vPersona1.codciudadexpedicion = Convert.ToInt64(txtCodciudadexpedicion.Text.Trim());
        //if(txtSexo.Text.Trim() != "")
        //    vPersona1.sexo = Convert.ToString(txtSexo.Text.Trim());
        if(txtPrimer_nombreConyuge.Text.Trim() != "")
            vPersona1.primer_nombre = Convert.ToString(txtPrimer_nombreConyuge.Text.Trim());
        if(txtSegundo_nombreConyuge.Text.Trim() != "")
            vPersona1.segundo_nombre = Convert.ToString(txtSegundo_nombreConyuge.Text.Trim());
        if(txtPrimer_apellidoConyuge.Text.Trim() != "")
            vPersona1.primer_apellido = Convert.ToString(txtPrimer_apellidoConyuge.Text.Trim());
        if(txtSegundo_apellidoConyuge.Text.Trim() != "")
            vPersona1.segundo_apellido = Convert.ToString(txtSegundo_apellidoConyuge.Text.Trim());
        if(txtRazon_socialConyuge.Text.Trim() != "")
            vPersona1.razon_social = Convert.ToString(txtRazon_socialConyuge.Text.Trim());
        if (txtFechanacimientoConyuge.Text.Trim() != "")
            vPersona1.fechanacimiento = Convert.ToDateTime(txtFechanacimientoConyuge.Text.Trim());
        //if(txtCodciudadnacimiento.Text.Trim() != "")
        //    vPersona1.codciudadnacimiento = Convert.ToInt64(txtCodciudadnacimiento.Text.Trim());
        //if(txtCodestadocivil.Text.Trim() != "")
        //    vPersona1.codestadocivil = Convert.ToInt64(txtCodestadocivil.Text.Trim());
        //if(txtCodescolaridad.Text.Trim() != "")
        //    vPersona1.codescolaridad = Convert.ToInt64(txtCodescolaridad.Text.Trim());
        //if(txtCodactividad.Text.Trim() != "")
            //vPersona1.codactividad = Convert.ToInt64(txtCodactividad.Text.Trim());
        if(txtDireccionConyuge.Text.Trim() != "")
            vPersona1.direccion = Convert.ToString(txtDireccionConyuge.Text.Trim());
        if(txtTelefonoConyuge.Text.Trim() != "")
            vPersona1.telefono = Convert.ToString(txtTelefonoConyuge.Text.Trim());
        //if(txtCodciudadresidencia.Text.Trim() != "")
        //    vPersona1.codciudadresidencia = Convert.ToInt64(txtCodciudadresidencia.Text.Trim());
        if(txtAntiguedadlugarConyuge.Text.Trim() != "")
            vPersona1.antiguedadlugar = Convert.ToInt64(txtAntiguedadlugarConyuge.Text.Trim());
        //if(txtTipovivienda.Text.Trim() != "")
        //    vPersona1.tipovivienda = Convert.ToString(txtTipovivienda.Text.Trim());
        if(txtArrendadorConyuge.Text.Trim() != "")
            vPersona1.arrendador = Convert.ToString(txtArrendadorConyuge.Text.Trim());
        if(txtTelefonoarrendadorConyuge.Text.Trim() != "")
            vPersona1.telefonoarrendador = Convert.ToString(txtTelefonoarrendadorConyuge.Text.Trim());
        if(txtCelularConyuge.Text.Trim() != "")
            vPersona1.celular = Convert.ToString(txtCelularConyuge.Text.Trim());
        if(txtEmailConyuge.Text.Trim() != "")
            vPersona1.email = Convert.ToString(txtEmailConyuge.Text.Trim());
        if(txtEmpresaConyuge.Text.Trim() != "")
            vPersona1.empresa = Convert.ToString(txtEmpresaConyuge.Text.Trim());
        if(txtTelefonoempresaConyuge.Text.Trim() != "")
            vPersona1.telefonoempresa = Convert.ToString(txtTelefonoempresaConyuge.Text.Trim());
        //if(txtCodcargo.Text.Trim() != "")
        //    vPersona1.codcargo = Convert.ToInt64(txtCodcargo.Text.Trim());
        //if(txtCodtipocontrato.Text.Trim() != "")
        //    vPersona1.codtipocontrato = Convert.ToInt64(txtCodtipocontrato.Text.Trim());
        if(txtCod_asesorConyuge.Text.Trim() != "")
            vPersona1.cod_asesor = Convert.ToInt64(txtCod_asesorConyuge.Text.Trim());
        //if(txtResidente.Text.Trim() != "")
        //    vPersona1.residente = Convert.ToString(txtResidente.Text.Trim());
        if(txtFecha_residenciaConyuge.Text.Trim() != "")
            vPersona1.fecha_residencia = Convert.ToDateTime(txtFecha_residenciaConyuge.Text.Trim());
        if(txtCod_oficinaConyuge.Text.Trim() != "")
            vPersona1.cod_oficina = Convert.ToInt64(txtCod_oficinaConyuge.Text.Trim());
        if(txtTratamientoConyuge.Text.Trim() != "")
            vPersona1.tratamiento = Convert.ToString(txtTratamientoConyuge.Text.Trim());      
      


        // Campos nuevos conyuge
        if (txtNumeroHijosConyuge.Text.Trim() != "")
            vPersona1.numHijos = Convert.ToInt64(txtNumeroHijosConyuge.Text.Trim());
        if (txtNumeroPersonasCargoConyuge.Text.Trim() != "")
            vPersona1.numPersonasaCargo = Convert.ToInt64(txtNumeroPersonasCargoConyuge.Text.Trim());
        if (txtOcupacionConyuge.Text.Trim() != "")
            vPersona1.ocupacion =  txtOcupacionConyuge.Text.Trim();
        if (txtSalarioConyuge.Text.Trim() != "")
            vPersona1.salario = Convert.ToInt64(txtSalarioConyuge.Text.Trim());
        if (txtAntiguedadLaboralConyuge.Text.Trim() != "")
            vPersona1.antiguedadLaboral = Convert.ToInt64(txtAntiguedadLaboralConyuge.Text.Trim());



        vPersona1.seleccionar = "C"; //Bandera para ejecuta el select del Conyuge
   
        return vPersona1;
    }
   





    private void Edicion()
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
                    ObtenerDatos(idObjeto);
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



    private void CargarListas()
    {
        try
        {
            ListaSolicitada = "Lugares";
            TraerResultadosLista();
            ddlLugarExpedicionConyuge.DataSource = lstDatosSolicitud;
            ddlLugarNacimientoConyuge.DataSource = lstDatosSolicitud;          
            ddlLugarExpedicionConyuge.DataTextField = "ListaDescripcion";
            ddlLugarNacimientoConyuge.DataTextField = "ListaDescripcion";         
            ddlLugarExpedicionConyuge.DataValueField = "ListaIdStr";
            ddlLugarNacimientoConyuge.DataValueField = "ListaIdStr";         
            ddlLugarExpedicionConyuge.DataBind();
            ddlLugarNacimientoConyuge.DataBind();

            ListaSolicitada = "Barrio";
            TraerResultadosLista();
            ddlBarrioConyuge.DataSource = lstDatosSolicitud;
            ddlBarrioConyuge.DataTextField = "ListaDescripcion";
            ddlBarrioConyuge.DataValueField = "ListaId";
            ddlBarrioConyuge.DataBind();

           
            ListaSolicitada = "EstadoCivil";
            TraerResultadosLista();
            ddlEstadoCivilConyuge.DataSource = lstDatosSolicitud;
            ddlEstadoCivilConyuge.DataTextField = "ListaDescripcion";
            ddlEstadoCivilConyuge.DataValueField = "ListaId";
            ddlEstadoCivilConyuge.DataBind();

            ListaSolicitada = "TipoCargo";
            TraerResultadosLista();
            ddlCargoConyuge.DataSource = lstDatosSolicitud;
            ddlCargoConyuge.DataTextField = "ListaDescripcion";
            ddlCargoConyuge.DataValueField = "ListaId";
            ddlCargoConyuge.DataBind();

            ListaSolicitada = "Parentesco";
            TraerResultadosLista();
            ddlParentescoConyuge.DataSource = lstDatosSolicitud;
            ddlParentescoConyuge.DataTextField = "ListaDescripcion";
            ddlParentescoConyuge.DataValueField = "ListaId";
            ddlParentescoConyuge.DataBind();

            ListaSolicitada = "TipoContrato";
            TraerResultadosLista();
            ddlTipoContratoConyuge.DataSource = lstDatosSolicitud;
            ddlTipoContratoConyuge.DataTextField = "ListaDescripcion";
            ddlTipoContratoConyuge.DataValueField = "ListaId";
            ddlTipoContratoConyuge.DataBind();

            ListaSolicitada = "NivelEscolaridad";
            TraerResultadosLista();
            ddlNivelEscolaridadConyuge.DataSource = lstDatosSolicitud;
            ddlNivelEscolaridadConyuge.DataTextField = "ListaDescripcion";
            ddlNivelEscolaridadConyuge.DataValueField = "ListaId";
            ddlNivelEscolaridadConyuge.DataBind();


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


 
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        
    }

    private void CargarCliente(String pIdObjeto)
    {

        try
        {
            //Session[DatosClienteServicio.CodigoProgramaCodeudor + ".id"] = txtCod_persona.Text.ToString().Trim();
            Session[DatosClienteServicio.CodigoProgramaCodeudor + ".NumDoc"] = txtIdentificacionConyuge.Text.ToString().Trim();

            vPersona1.identificacion = txtIdentificacionConyuge.Text;
            vPersona1.seleccionar = "Identificacion";
            vPersona1.noTraerHuella = 1;
            vPersona1 = DatosClienteServicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);

            //Habilita Razon social solo para Nits
            if ((ddlTipoConyuge.SelectedValue.ToString() != "2") && (ddlTipoConyuge.SelectedValue.ToString() != "3"))
            {
                //Panel2.Enabled = false;
                //Panel1.Visible = false;
            }
            else
            {
                //Panel2.Enabled = true;
                //Panel1.Visible = true;
            }

            //if (vPersona1.cod_persona != Int64.MinValue)
            //{
            //    txtCod_persona.Text = HttpUtility.HtmlDecode(vPersona1.cod_persona.ToString().Trim());
            //    txtCod_personaE.Text = HttpUtility.HtmlDecode(vPersona1.cod_persona.ToString().Trim());
            //}
            if (!string.IsNullOrEmpty(vPersona1.tipo_persona))
                rblTipoPersonaConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_persona.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.identificacion))
            {
                txtIdentificacionConyuge.Text = HttpUtility.HtmlDecode(vPersona1.identificacion.ToString().Trim());
            }
            if (vPersona1.digito_verificacion != Int64.MinValue)
                txtDigito_verificacionConyuge.Text = HttpUtility.HtmlDecode(vPersona1.digito_verificacion.ToString().Trim());
            if (vPersona1.tipo_identificacion != Int64.MinValue)
            {
                ddlTipoConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_identificacion.ToString().Trim());
            }
            if (vPersona1.fechaexpedicion != DateTime.MinValue)
                txtFechaexpedicionConyuge.Text = HttpUtility.HtmlDecode(vPersona1.fechaexpedicion.Value.ToShortDateString());
            if (vPersona1.codciudadexpedicion != Int64.MinValue)
                ddlLugarExpedicionConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadexpedicion.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.sexo))
                rblSexoConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.sexo.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.primer_nombre))
            {
                txtPrimer_nombreConyuge.Text = HttpUtility.HtmlDecode(vPersona1.primer_nombre.ToString().Trim());

            }

            if (!string.IsNullOrEmpty(vPersona1.segundo_nombre))
            {
                txtSegundo_nombreConyuge.Text = HttpUtility.HtmlDecode(vPersona1.segundo_nombre.ToString().Trim());

            }
            if (!string.IsNullOrEmpty(vPersona1.primer_apellido))
            {
                txtPrimer_apellidoConyuge.Text = HttpUtility.HtmlDecode(vPersona1.primer_apellido.ToString().Trim());
            }
            if (!string.IsNullOrEmpty(vPersona1.segundo_apellido))
            {
                txtSegundo_apellidoConyuge.Text = HttpUtility.HtmlDecode(vPersona1.segundo_apellido.ToString().Trim());
            }
            if (!string.IsNullOrEmpty(vPersona1.razon_social))
            {
                txtRazon_socialConyuge.Text = HttpUtility.HtmlDecode(vPersona1.razon_social.ToString().Trim());
            }
            if (vPersona1.fechanacimiento != DateTime.MinValue)
            {
                txtFechanacimientoConyuge.Text = HttpUtility.HtmlDecode(vPersona1.fechanacimiento.Value.ToShortDateString());
            }
            
            if (vPersona1.codciudadnacimiento != Int64.MinValue)
                ddlLugarNacimientoConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadnacimiento.ToString().Trim());
            if (vPersona1.codestadocivil != Int64.MinValue)
                ddlEstadoCivilConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codestadocivil.ToString().Trim());
            if (vPersona1.codescolaridad != Int64.MinValue)
                ddlNivelEscolaridadConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codescolaridad.ToString().Trim());
            if (vPersona1.codactividad != Int64.MinValue)
            {
                ddlActividadConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codactividad.ToString().Trim());
            }
            if (!string.IsNullOrEmpty(vPersona1.direccion))
            {
                txtDireccionConyuge.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
            }

            if (!string.IsNullOrEmpty(vPersona1.telefono))
            {
                txtTelefonoConyuge.Text = HttpUtility.HtmlDecode(vPersona1.telefono.ToString().Trim());
            }
            if (vPersona1.codciudadresidencia != Int64.MinValue)
            {
                ddlLugarResidenciaConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadresidencia.ToString().Trim());
            }
            if (vPersona1.antiguedadlugar != Int64.MinValue)
                txtAntiguedadlugarConyuge.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugar.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.tipovivienda))
            {
                rblTipoViviendaConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipovivienda.ToString().Trim());


            }

            if (!string.IsNullOrEmpty(vPersona1.arrendador))
                txtArrendadorConyuge.Text = HttpUtility.HtmlDecode(vPersona1.arrendador.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.telefonoarrendador))
                txtTelefonoarrendadorConyuge.Text = HttpUtility.HtmlDecode(vPersona1.telefonoarrendador.ToString().Trim());
            //if (vPersona1.ValorArriendo != Int64.MinValue)
            //    txtValorArriendo.Text = HttpUtility.HtmlDecode(vPersona1.ValorArriendo.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.celular))
                txtCelularConyuge.Text = HttpUtility.HtmlDecode(vPersona1.celular.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.email))
                txtEmailConyuge.Text = HttpUtility.HtmlDecode(vPersona1.email.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.empresa))
                txtEmpresaConyuge.Text = HttpUtility.HtmlDecode(vPersona1.empresa.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.telefonoempresa))
                txtTelefonoempresaConyuge.Text = HttpUtility.HtmlDecode(vPersona1.telefonoempresa.ToString().Trim());

            //if (!string.IsNullOrEmpty(vPersona1.direccionempresa))
            //    txtDireccionEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.direccionempresa.ToString().Trim());
            //if (vPersona1.antiguedadlugarempresa != Int64.MinValue)
            //    txtAntiguedadlugarEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugarempresa.ToString().Trim());

            if (vPersona1.codcargo != Int64.MinValue)
                ddlCargoConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codcargo.ToString().Trim());
            if (vPersona1.codtipocontrato != Int64.MinValue)
                ddlTipoContratoConyuge.Text = HttpUtility.HtmlDecode(vPersona1.codtipocontrato.ToString().Trim());
            if (vPersona1.cod_asesor != Int64.MinValue)
                txtCod_asesorConyuge.Text = HttpUtility.HtmlDecode(vPersona1.cod_asesor.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.residente))
                rblResidenteConyuge.Text = HttpUtility.HtmlDecode(vPersona1.residente.ToString().Trim());
            if (vPersona1.fecha_residencia != DateTime.MinValue)
                txtFecha_residenciaConyuge.Text = HttpUtility.HtmlDecode(vPersona1.fecha_residencia.ToShortDateString());
            if (vPersona1.cod_oficina != Int64.MinValue)
                txtCod_oficinaConyuge.Text = HttpUtility.HtmlDecode(vPersona1.cod_oficina.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.tratamiento))
                txtTratamientoConyuge.Text = HttpUtility.HtmlDecode(vPersona1.tratamiento.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.estado))
                ddlEstadoConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.estado.ToString().Trim());
            if (vPersona1.barrioResidencia != Int64.MinValue)
                ddlBarrioConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.barrioResidencia.ToString().Trim());

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
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.CodigoProgramaCodeudor, "ObtenerDatos", ex);
        }

        //Session["Cod_persona"] = txtCod_persona.Text;
        //Session["Nombres"] = txtPrimer_nombre.Text.ToString().Trim() + " " + txtSegundo_nombre.Text.ToString().Trim() + " " + txtPrimer_apellido.Text.ToString().Trim() + " " + txtSegundo_apellido.Text.ToString().Trim();
       
    }

   

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            vPersona1 = Persona1Servicio.ConsultarPersona1(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            Xpinn.FabricaCreditos.Entities.Conyuge vConyuge = new Xpinn.FabricaCreditos.Entities.Conyuge();
            vConyuge = ConyugeServicio.ConsultarConyuge(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            //if (vPersona1.cod_persona != Int64.MinValue)
            //    txtCod_persona.Text = HttpUtility.HtmlDecode(vPersona1.cod_persona.ToString().Trim());
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
            if (vPersona1.barrioResidencia != Int64.MinValue)
                ddlBarrioConyuge.SelectedValue = HttpUtility.HtmlDecode(vPersona1.barrioResidencia.ToString().Trim());

            //if (vPersona1.fechacreacion != DateTime.MinValue)
            //    txtFechacreacion.Text = HttpUtility.HtmlDecode(vPersona1.fechacreacion.ToShortDateString());
            //if (!string.IsNullOrEmpty(vPersona1.usuariocreacion))
            //    txtUsuariocreacion.Text = HttpUtility.HtmlDecode(vPersona1.usuariocreacion.ToString().Trim());
            //if (vPersona1.fecultmod != DateTime.MinValue)
            //    txtFecultmod.Text = HttpUtility.HtmlDecode(vPersona1.fecultmod.ToShortDateString());
            //if (!string.IsNullOrEmpty(vPersona1.usuultmod))
            //    txtUsuultmod.Text = HttpUtility.HtmlDecode(vPersona1.usuultmod.ToString().Trim());
            if (vPersona1.fechanacimiento != DateTime.MinValue)
            {
                txtFechanacimientoConyuge.Text = HttpUtility.HtmlDecode(vPersona1.fechanacimiento.Value.ToShortDateString());
                lblEdadConyuge.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtFechanacimientoConyuge.Text)));
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
            
            validarArriendo();

            //Datos tabla Codeudor:
            if (vConyuge.cod_conyuge != Int64.MinValue)
                txtCodCodeudor.Text = HttpUtility.HtmlDecode(vConyuge.cod_persona.ToString().Trim());
            if (vConyuge.cod_persona != Int64.MinValue)
                txtCodConyuge.Text = HttpUtility.HtmlDecode(vConyuge.cod_conyuge.ToString().Trim());
                

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "ObtenerDatos", ex);
        }
    }

    public static int GetAge(DateTime birthDate)
    {
        return (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.242199);
    }




    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionCodeudor/ConyugeCodeudor/Nuevo.aspx");
    }
    protected void btnAdelante2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionCodeudor/Referencias/Lista.aspx");
    }
    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/BalanceGeneralMicroempresa/Default.aspx");
    }
    protected void btnGuardar_Click1(object sender, ImageClickEventArgs e)
    {
        //Solo permite registrar conyuge codeudor si estado civil = casado o union libre
        try
        {
            if (EstadoCodeudor == "1")
            {
                try
                {
                    Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
                    Xpinn.FabricaCreditos.Entities.Conyuge vConyuge = new Xpinn.FabricaCreditos.Entities.Conyuge();

                    if (idObjeto != "")
                        vPersona1 = DatosClienteServicio.ConsultarPersona1(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

                    vPersona1.origen = "Solicitud";     //Permite reconocer que se modifica persona desde el formulario "Solicitud"
                    vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
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
                    //if (txtValorArriendo.Text != "") vPersona1.ValorArriendo = Convert.ToInt64(txtValorArriendo.Text.Trim());
                    vPersona1.celular = (txtCelularConyuge.Text != "") ? Convert.ToString(txtCelularConyuge.Text.Trim()) : String.Empty;
                    vPersona1.email = (txtEmailConyuge.Text != "") ? Convert.ToString(txtEmailConyuge.Text.Trim()) : String.Empty;
                    vPersona1.empresa = (txtEmpresaConyuge.Text != "") ? Convert.ToString(txtEmpresaConyuge.Text.Trim()) : String.Empty;
                    vPersona1.telefonoempresa = (txtTelefonoempresaConyuge.Text != "") ? Convert.ToString(txtTelefonoempresaConyuge.Text.Trim()) : String.Empty;

                    vPersona1.direccionempresa = String.Empty;// (txtDireccionEmpresa.Text != "") ? Convert.ToString(txtDireccionEmpresa.Text.Trim()) : String.Empty;
                    //if (txtAntiguedadlugarEmpresa.Text != "") vPersona1.antiguedadlugarempresa = Convert.ToInt64(txtAntiguedadlugarEmpresa.Text.Trim());


                    if (ddlCargoConyuge.Text != "") vPersona1.codcargo = Convert.ToInt64(ddlCargoConyuge.Text.Trim());
                    if (ddlTipoContratoConyuge.Text != "") vPersona1.codtipocontrato = Convert.ToInt64(ddlTipoContratoConyuge.SelectedValue);
                    if (txtCod_asesorConyuge.Text != "") vPersona1.cod_asesor = Convert.ToInt64(txtCod_asesorConyuge.Text.Trim());
                    //if (txtResidente.Text != "") vPersona1.residente = Convert.ToString(txtResidente.Text.Trim());
                    //vPersona1.residente = String.Empty;
                    vPersona1.residente = (rblResidenteConyuge.Text != "") ? Convert.ToString(rblResidenteConyuge.SelectedValue) : String.Empty;
                    if (txtFecha_residenciaConyuge.Text != "") vPersona1.fecha_residencia = Convert.ToDateTime(txtFecha_residenciaConyuge.Text.Trim());
                    if (txtCod_oficinaConyuge.Text != "") vPersona1.cod_oficina = Convert.ToInt64(txtCod_oficinaConyuge.Text.Trim());
                    vPersona1.tratamiento = (txtTratamientoConyuge.Text != "") ? Convert.ToString(txtTratamientoConyuge.Text.Trim()) : String.Empty;
                    vPersona1.estado = (ddlEstadoConyuge.Text != "") ? Convert.ToString(ddlEstadoConyuge.SelectedValue) : String.Empty;

                    vPersona1.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));

                    //Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");
                    Xpinn.Util.Usuario usuario = new Xpinn.Util.Usuario();
                    usuario = (Xpinn.Util.Usuario)Session["usuario"];

                    vPersona1.usuariocreacion = usuario.nombre;
                    vPersona1.fecultmod = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
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
                    vPersona1.salario = (txtSalarioConyuge.Text != "") ? Convert.ToInt64(txtSalarioConyuge.Text.Trim()) : 0;
                    vPersona1.antiguedadLaboral = (txtAntiguedadLaboralConyuge.Text != "") ? Convert.ToInt64(txtAntiguedadLaboralConyuge.Text.Trim()) : 0;
                              
                    if (idObjeto != "")
                    {
                        vPersona1.cod_persona = Convert.ToInt64(idObjeto);
                        DatosClienteServicio.ModificarPersona1(vPersona1, (Usuario)Session["usuario"]);
                        vConyuge.cod_conyuge  = vPersona1.cod_persona;  // Toma el codigo de la persona modificada para editarla en la tabla Conyuge
                        vConyuge.cod_persona = Convert.ToInt64(Session["CodCodeudor"].ToString());
                        vConyuge = ConyugeServicio.ModificarConyuge(vConyuge, (Usuario)Session["usuario"]);
                    }
                    else
                    {
                        vPersona1 = Persona1Servicio.CrearPersona1(vPersona1, (Usuario)Session["usuario"]);
                        idObjeto = vPersona1.cod_persona.ToString();

                        vConyuge.cod_conyuge  = vPersona1.cod_persona;  // Toma el codigo de la persona registrarla para editarla en la tabla Conyuge
                        vConyuge.cod_persona = Convert.ToInt64(Session["CodCodeudor"].ToString());
                        vConyuge = ConyugeServicio.CrearConyuge(vConyuge, (Usuario)Session["usuario"]);
                    }

                    Session[Persona1Servicio.CodigoProgramaCodeudor + ".id"] = idObjeto;
                    //Actualizar();
                    //Borrar();   
                }
                catch (ExceptionBusiness ex)
                {
                    VerError(ex.Message);
                }
                catch (Exception ex)
                {
                    BOexcepcion.Throw(DatosClienteServicio.CodigoProgramaCodeudor, "btnGuardar_Click", ex);
                }
                Actualizar();
            }
        }
        catch (ExceptionBusiness ex)
        {
            string error = ex.ToString();
        }
        
        
        
        
    }
    protected void btnConsultar_Click1(object sender, ImageClickEventArgs e)
    {
        //Solo permite consultar conyuge codeudor si estado civil = casado o union libre

        try
        {
            if (EstadoCodeudor == "1")
            { 
                GuardarValoresConsulta(pConsulta, Persona1Servicio.CodigoProgramaCodeudor);
                Actualizar();
            }
        }
        catch (ExceptionBusiness ex)
        {
            string error = ex.ToString();
        }
       
        
        
    }
    protected void rblTipoVivienda_SelectedIndexChanged(object sender, EventArgs e)
    {
        validarArriendo();
    }

    private void validarArriendo()
    {
        if (rblTipoViviendaConyuge.SelectedValue == "A")
        {
            txtArrendadorConyuge.Enabled = true;
            txtTelefonoarrendadorConyuge.Enabled = true;
            txtValorArriendoConyuge.Enabled = true;
            //rfvtxtArrendador.Enabled = true;
            //rfvtxtTelefonoarrendador.Enabled = true;
            //rfvtxtValorArriendo.Enabled = true;
        }
        else
        {
            txtArrendadorConyuge.Enabled = false;
            txtTelefonoarrendadorConyuge.Enabled = false;
            txtValorArriendoConyuge.Enabled = false;
            //rfvtxtArrendador.Enabled = false;
            //rfvtxtTelefonoarrendador.Enabled = false;
            //rfvtxtValorArriendo.Enabled = false;

            txtArrendadorConyuge.Text = "";
            txtTelefonoarrendadorConyuge.Text = "";
            txtValorArriendoConyuge.Text = "";
        }
    }
}