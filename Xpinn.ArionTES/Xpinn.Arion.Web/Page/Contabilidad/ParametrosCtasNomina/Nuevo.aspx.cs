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

using Xpinn.Contabilidad.Services;
using Xpinn.Contabilidad.Entities;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;

partial class Nuevo : GlobalWeb
{
    ParametrosCtasNominaService _parametroCtaNominaService = new ParametrosCtasNominaService();
    AreaService AreaServise = new AreaService();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[_parametroCtaNominaService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(_parametroCtaNominaService.CodigoPrograma, "E");
            else
                VisualizarOpciones(_parametroCtaNominaService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_parametroCtaNominaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvParametros.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                CargarListas();
                if (Session[_parametroCtaNominaService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[_parametroCtaNominaService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(_parametroCtaNominaService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtCodigo.Text = "Autogenerado";
                    InicializarAreas();
                    //List<Area> lstEmpleado = AreaServise.ListarAreas(Usuario);
                    //gv_Areas.DataSource = lstEmpleado;
                    //gv_Areas.DataBind();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_parametroCtaNominaService.CodigoPrograma, "Page_Load", ex);
        }
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

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, ctlListarCodigo ctlControlListar)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ctlControlListar.LimpiarControl();

        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);

        ctlControlListar.TextField = "descripcion";
        ctlControlListar.ValueField = "idconsecutivo";
        ctlControlListar.BindearControl(plista);
    }


    private void CargarListas()
    {
        try
        {
            //PoblarLista("ESTRUCTURA_DETALLE", ddlEstructura);

            LlenarListasDesplegables(TipoLista.ConceptoNomina, ddlConceptoNomina);

            ddlTipoMov.Items.Insert(0, new ListItem("Débito", "1"));
            ddlTipoMov.Items.Insert(1, new ListItem("Crédito", "2"));
            ddlTipoMov.DataBind();

            //PoblarLista("tipo_tran", "", " (tipo_producto Not In (1, 2, 3, 4, 6, 9, 12) Or (tipo_producto = 2 And tipo_caja = 0) Or tipo_tran In (402, 403, 981,225,226)) ", "", ctlListarCodigoTransaccion);
            //PoblarLista("tipo_tran", "", " ", "", ctlListarCodigoTransaccion);

          
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }



    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Par_Cue_Nomina pConsulta = _parametroCtaNominaService.ConsultarPar_Cue_Nomina(Convert.ToInt32(pIdObjeto), Usuario);

            if (pConsulta.idparametro != 0)
                txtCodigo.Text = pConsulta.idparametro.ToString();

            //if (pConsulta.cod_est_det != 0)
            //    ddlEstructura.SelectedValue = pConsulta.cod_est_det.ToString();
            if (!string.IsNullOrWhiteSpace(pConsulta.cod_cuenta))
                txtCodCuenta.Text = pConsulta.cod_cuenta;

            txtCodCuenta_TextChanged(txtCodCuenta, null);

            if (!string.IsNullOrWhiteSpace(pConsulta.cod_cuenta_niif))
                txtCodCuentaNIF.Text = pConsulta.cod_cuenta_niif;

            txtCodCuentaNIF_TextChanged(txtCodCuentaNIF, null);

            if (!string.IsNullOrWhiteSpace(pConsulta.cod_cuenta_contra))
                txtCodCuentaNomina.Text = pConsulta.cod_cuenta_contra;

            txtCodCuentaNomina_TextChanged(txtCodCuentaNIF, null);

            if (pConsulta.cod_tercero.HasValue)
            {
                txtCodCliente.Text = pConsulta.cod_tercero.ToString();
                txtIdentificacion.Text = pConsulta.identificacion_tercero;
                txtNombreCliente.Text = !string.IsNullOrWhiteSpace(pConsulta.nom_tercero) ? pConsulta.nom_tercero : pConsulta.razon_social;
            }

            if (pConsulta.tipo_mov != 0)
                ddlTipoMov.SelectedValue = pConsulta.tipo_mov.ToString();

            if(pConsulta.cod_concepto != null)
            ddlConceptoNomina.SelectedValue = pConsulta.cod_concepto.ToString();


            //if (pConsulta.tipo_tran != 0)
            //    ctlListarCodigoTransaccion.SelectedValue(pConsulta.tipo_tran.ToString());

            //CONSULTANDO AREA
            List<Area> lstArea = new List<Area>();
            Area pImpuesto = new Area();


            lstArea = AreaServise.ListarAreasContable(Convert.ToInt64(txtCodigo.Text), Usuario);

            if (lstArea.Count > 0)
            {
                gv_Areas.DataSource = lstArea;
                gv_Areas.DataBind();
            }
            else
                InicializarAreas();

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    Boolean ValidarDatos()
    {
        VerError("");

        if (string.IsNullOrWhiteSpace(txtCodCuenta.Text))
        {
            VerError("Seleccione una Cuenta Contable");
            return false;
        }
       
        //if (string.IsNullOrWhiteSpace(ctlListarCodigoTransaccion.Codigo))
        //{
        //    VerError("Seleccione el tipo de transacción");
        //    return false;
        //}
        if (string.IsNullOrWhiteSpace(ddlConceptoNomina.SelectedValue))
        {
            VerError("Debes seleccionar un concepto de nomina!.");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                Int64 idparametro = 0;
                Par_Cue_Nomina vData = new Par_Cue_Nomina();

                if (!string.IsNullOrWhiteSpace(idObjeto))
                {
                    vData.idparametro = Convert.ToInt32(txtCodigo.Text.Trim());
                }

                //if (ddlEstructura.SelectedValue != null && ddlEstructura.SelectedIndex != 0)
                //    vData.cod_est_det = Convert.ToInt32(ddlEstructura.SelectedValue);
                else
                    vData.cod_est_det = 0;

                vData.cod_cuenta = txtCodCuenta.Text;
                vData.cod_cuenta_contra = txtCodCuentaNomina.Text;
                vData.cod_cuenta_niif =txtCodCuentaNIF.Text;

                // TIPO DE TRANSACCION
                vData.tipo_tran = 0;
                vData.tipo_mov = Convert.ToInt32(ddlTipoMov.SelectedValue);
                //if (!string.IsNullOrWhiteSpace(ctlListarCodigoTransaccion.Codigo))
                //        vData.tipo_tran = Convert.ToInt32(ctlListarCodigoTransaccion.Codigo);

                vData.cod_tercero = !string.IsNullOrWhiteSpace(txtCodCliente.Text) ? Convert.ToInt64(txtCodCliente.Text) : 0;
                vData.tipo_tercero = 4;
                vData.cod_concepto = ddlConceptoNomina.SelectedValue;

                if (vData.idparametro != 0)
                {
                    _parametroCtaNominaService.ModificarPar_Cue_Nomina(vData, Usuario);
                    idparametro = vData.idparametro;
                }
                else
                {
                    Par_Cue_Nomina nomina = _parametroCtaNominaService.CrearPar_Cue_Nomina(vData, Usuario);
                    idparametro = nomina.idparametro;
                    if (nomina.idparametro == 0)
                    {
                        VerError("No se pudo realizar la operación de guardado");
                        return;
                    }


                    if (Session["Proceso"] !=null)
                    {
                        Xpinn.Caja.Services.TipoOperacionService objOpercion = new Xpinn.Caja.Services.TipoOperacionService();
                        Xpinn.Caja.Entities.TipoOperacion vOpe = (Xpinn.Caja.Entities.TipoOperacion)Session["Proceso"];
                        vOpe.tipo_tran = vData.tipo_tran.HasValue ? vData.tipo_tran.Value : 0;
                        objOpercion.ModificaTipoOpServices(vOpe, Usuario);
                        Session["Proceso"] = null;
                    }
                               
                }
                foreach (GridViewRow item in gv_Areas.Rows)
                {
                    Area entidad = new Area();
                    entidad.idparametro = idparametro;

          
                 
                    TextBoxGrid txtCod_cuenta = (TextBoxGrid)item.FindControl("txtCodCuentaGv");
                    DropDownListGrid ddlArea = (DropDownListGrid)item.FindControl("ddlAreas");
                    entidad.IdArea =Convert.ToInt64(ddlArea.SelectedItem.Value);
                    Label lblCodigo = (Label)item.FindControl("lblCodigo");
                

                    int index = gv_Areas.SelectedIndex;

                    if (txtCod_cuenta.Text!="")
                    {
                        entidad.cod_cuenta = txtCod_cuenta.Text;
                    }
                    else
                    {
                        entidad.cod_cuenta = null;
                    }
                    if (lblCodigo.Text!="-1")
                    {
                        entidad.consecutivo = Convert.ToInt64(lblCodigo.Text);
                        entidad = AreaServise.Area_CtaContable_modificar(entidad, Usuario);
                    }
                    else
                    {

                        if (entidad.cod_cuenta!=null)
                        {
                            entidad = AreaServise.Area_CtaContable_crear(entidad, Usuario);
                        }
                       
                    }

                }

                string msj = idObjeto != "" ? "Modifico" : "Grabo";
                lblmsj.Text = "Se " + msj + " Correctamente los Datos";
                mvParametros.ActiveViewIndex = 1;
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_parametroCtaNominaService.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnFin_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();

            Xpinn.Caja.Entities.Persona persona = personaService.ConsultarPersonaXIdentificacion(txtIdentificacion.Text, Usuario);

            if (persona != null)
            {
                txtCodCliente.Text = persona.cod_persona.ToString();
                txtNombreCliente.Text = !string.IsNullOrWhiteSpace(persona.nom_persona) ? persona.nom_persona : persona.razon_social;
            }
        }
    }


    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodCliente", "txtIdentificacion", "txtNombreCliente");
    }


    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtCodCuenta.Text))
        {
            PlanCuentasService PlanCuentasServicio = new PlanCuentasService();
            PlanCuentas PlanCuentas = new PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, Usuario);

            // Mostrar el nombre de la cuenta           
            if (txtNomCuenta != null)
                txtNomCuenta.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuenta.Text = "";
        }
    }

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNomCuenta");
    }


    protected void txtCodCuentaNomina_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtCodCuentaNomina.Text))
        {
            PlanCuentasService PlanCuentasServicio = new PlanCuentasService();
            PlanCuentas PlanCuentas = new PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuentaNomina.Text, Usuario);

            // Mostrar el nombre de la cuenta           
            if (txtNombreCuentaNomina != null)
                txtNombreCuentaNomina.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNombreCuentaNomina.Text = "";
        }
    }
    protected void txtCodCuenta1_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtCodCuenta = (TextBoxGrid)sender;
        if (txtCodCuenta != null)
        {
            // Determinar los datos de la cuenta contable
            PlanCuentasService PlanCuentasServicio = new PlanCuentasService();
            PlanCuentas PlanCuentas = new PlanCuentas();

            int rowIndex = Convert.ToInt32(txtCodCuenta.CommandArgument);
            TextBoxGrid lblNombreCuenta = (TextBoxGrid)gv_Areas.Rows[rowIndex].FindControl("lblNombreCuenta");
            if (txtCodCuenta.Text.Trim() != "")
            {
                PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, Usuario);
                if (lblNombreCuenta != null)
                    lblNombreCuenta.Text = PlanCuentas.nombre;
            }
            else
                lblNombreCuenta.Text = "";
        }
    }

    protected void btnListadoPlanNomina_Click(object sender, EventArgs e)
    {
        ctlCuentasNomina.Motrar(true, "txtCodCuentaNomina", "txtNombreCuentaNomina");
    }
    protected void btnListadoPlanHomo_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlanHomo = (ButtonGrid)sender;
        if (btnListadoPlanHomo != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPlanHomo.CommandArgument);
            ctlPlanCuentas ctlListadoPlanHomo = (ctlPlanCuentas)gv_Areas.Rows[rowIndex].FindControl("ctlCuentasNomina1");
            //ctlPlanCuentas ctlListadoPlanHomo = (ctlPlanCuentas)gvHomologa.Rows[rowIndex].FindControl("ctlListadoPlanHomo");
            TextBoxGrid txtCodCuentaGv = (TextBoxGrid)gv_Areas.Rows[rowIndex].FindControl("txtCodCuentaGv");
            TextBoxGrid lblNombreCuenta = (TextBoxGrid)gv_Areas.Rows[rowIndex].FindControl("lblNombreCuenta");
            ctlListadoPlanHomo.Motrar(true, "txtCodCuentaGv", "lblNombreCuenta");
        }
    }

    protected void txtCodCuentaNIF_TextChanged(object sender, EventArgs e)
    {
        if (txtCodCuentaNIF.Text != "")
        {
            PlanCuentasService PlanCuentasServicio = new PlanCuentasService();
            Xpinn.NIIF.Entities.PlanCuentasNIIF PlanCuentas = new Xpinn.NIIF.Entities.PlanCuentasNIIF();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentasNIIF(txtCodCuentaNIF.Text, Usuario);

            // Mostrar el nombre de la cuenta
            if (txtNomCuentaNif != null)
                txtNomCuentaNif.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuentaNif.Text = "";
            txtCodCuentaNIF.Text = "";
        }
    }

    protected void btnListadoPlanNIF_Click(object sender, EventArgs e)
    {
        ctlListadoPlanNif.Motrar(true, "txtCodCuentaNIF", "txtNomCuentaNif");
    }
    protected void gvArea_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlAreas = (DropDownListGrid)e.Row.FindControl("ddlAreas");
            List<Area> lstArea = new List<Area>();

            lstArea = AreaServise.ListarAreas(Usuario);
            if (ddlAreas != null)
                if (lstArea.Count > 0)
                {
                    ddlAreas.DataSource = lstArea;
                    ddlAreas.DataTextField = "Nombre";
                    ddlAreas.DataValueField = "IdArea";
                    ddlAreas.Items.Insert(0, new ListItem("Seleccione un item", "-1"));
                    ddlAreas.SelectedIndex = 0;
                    ddlAreas.DataBind();
                }

            Label lblTipoImpuesto = (Label)e.Row.FindControl("lblTipoImpuesto");
            if (lblTipoImpuesto != null)
                ddlAreas.SelectedValue = lblTipoImpuesto.Text;

           
        }
    }
    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        ObtenerListaImpuestos();
        List<Area> lstArea = new List<Area>();


        if (Session["DatosArea"] != null)
        {
            lstArea = (List<Area>)Session["DatosArea"];

            for (int i = 1; i <= 1; i++)
            {
                Area eImpu = new Area();
                eImpu.consecutivo = -1;
                eImpu.IdArea = 0;
                eImpu.idparametro = 0;
                eImpu.cod_cuenta = null;

                lstArea.Add(eImpu);
            }
            gv_Areas.PageIndex = gv_Areas.PageCount;
            gv_Areas.DataSource = lstArea;
            gv_Areas.DataBind();

            Session["DatosArea"] = lstArea;
        }
    }
    protected void InicializarAreas()
    {
        List<Area> lstArea = new List<Area>();

        for (int i = gv_Areas.Rows.Count; i < 2; i++)
        {
            
            Area eImpu = new Area();
            eImpu.consecutivo = -1;
            eImpu.IdArea = 0;
            eImpu.idparametro = 0;
            eImpu.cod_cuenta = null;

            lstArea.Add(eImpu);
        }
        gv_Areas.DataSource = lstArea;
        gv_Areas.DataBind();
        Session["DatosArea"] = lstArea;
    }
    protected List<Area> ObtenerListaImpuestos()//Int64 cod
    {
        List<Area> lstArea = new List<Area>();
        List<Area> lista = new List<Area>();


        foreach (GridViewRow rfila in gv_Areas.Rows)
        {
            Area eImpu = new Area();
            Label lblCodigo = (Label)rfila.FindControl("lblCodigo");

            if (lblCodigo != null)
                eImpu.consecutivo = Convert.ToInt64(lblCodigo.Text);

            DropDownListGrid ddlAreas = (DropDownListGrid)rfila.FindControl("ddlAreas");
            if (ddlAreas.SelectedValue != null)
                eImpu.IdArea = Convert.ToInt64(ddlAreas.SelectedValue);

            TextBoxGrid txtCodCuenta = (TextBoxGrid)rfila.FindControl("txtCodCuentaGv");
            if (txtCodCuenta != null)
                eImpu.cod_cuenta = txtCodCuenta.Text;


            TextBoxGrid lblNombreCuenta = (TextBoxGrid)rfila.FindControl("lblNombreCuenta");
            if (lblNombreCuenta.Text != "")
                eImpu.nombre_cuenta =lblNombreCuenta.Text;


          
            lista.Add(eImpu);
            Session["DatosArea"] = lista;

            if (eImpu.cod_cuenta != null && ddlAreas.SelectedIndex != 0)
            {
                lstArea.Add(eImpu);
            }
        }
        return lstArea;
    }
    protected void gvArea_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gv_Areas.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaImpuestos();

        List<Area> LstActi;
        LstActi = (List<Area>)Session["DatosArea"];

        try
        {
            foreach (Area acti in LstActi)
            {
                if (acti.consecutivo == conseID)
                {
                    //if (conseID > 0)
                    //    ImpuService.EliminarPlanCuentaImpuesto(conseID, _usuario);
                    LstActi.Remove(acti);
                    break;
                }
            }
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }

        gv_Areas.DataSourceID = null;
        gv_Areas.DataBind();

        gv_Areas.DataSource = LstActi;
        gv_Areas.DataBind();

        Session["DatosArea"] = LstActi;
    }
}