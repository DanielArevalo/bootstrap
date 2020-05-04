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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Comun.Services;
using Xpinn.Comun.Entities;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using System.Data.Common;

public partial class Nuevo : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar

    String operacion = "";


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AporteServicio.ProgramaModificacion, "E");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;  
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaModificacion, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {            
            if (!IsPostBack)
            {

                LlenarComboLineaAporte(DdlLineaAporte);
                LlenarComboPeriodicidad(DdlPeriodicidad);     
              //  txtFecha_apertura.Text = DateTime.Now.ToShortDateString();
                CargarListas();
                Usuario usuap = (Usuario)Session["usuario"];
                Int64 oficina = Convert.ToInt64(usuap.cod_oficina);
                txtOficina.Text = Convert.ToString(oficina);
                txtOficinaNombre.Text = Convert.ToString(usuap.nombre_oficina);

                CargarValoresConsulta(pConsulta, AporteServicio.ProgramaModificacion);
                if (Session[AporteServicio.ProgramaModificacion + ".consulta"] != null)
                   Actualizar();
                this.LblMensaje.Text = "";
                if (Session[AporteServicio.ProgramaModificacion + ".id"] != null)
                {
                    idObjeto = Session[AporteServicio.ProgramaModificacion.ToString() + ".id"].ToString();
                    Session.Remove(AporteServicio.ProgramaModificacion.ToString() + ".id");
                    //if (txtNumeIdentificacion.Text != "")
                    //{   YA NO SE USA EL FILTRO POR IDENTIFICACION
                    //    ObtenerDatosCliente(txtNumeIdentificacion.Text);
                    //    Distribucion();
                    //}
                    if (idObjeto != "")
                    {
                        ObtenerDatosAporte(idObjeto);
                        Distribucion();
                    }                    
                    txtOficina.Text = Convert.ToString(oficina);
                    txtOficinaNombre.Text = Convert.ToString(usuap.nombre_oficina);
                    
                    this.LblMensaje.Text = "";
                }

                DdlFormaPago_SelectedIndexChanged(DdlFormaPago, null);
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaModificacion, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if(txtNumeIdentificacion.Text == "" || txtFecha_apertura.Text == "" || txtValorCuota.Text == "")
        {
            this.LblMensaje.Text = "Por favor verificar los campos Identificacion,Cuota,fechaApertura";
        }
        else
        {
            ctlMensaje.MostrarMensaje("Desea realizar la modificación de datos?");
        }

    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            this.grabar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaModificacion, "btnContinuarMen_Click", ex);
        }
    }



    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AporteServicio.ProgramaModificacion + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[AporteServicio.ProgramaModificacion + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaModificacion, "gvLista_PageIndexChanging", ex);
        }
    }

    private void ConsultarCliente(String pIdObjeto)
    {
        Xpinn.Aportes.Services.AporteServices AportesServicio = new Xpinn.Aportes.Services.AporteServices();
        Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();
        String IdObjeto = txtNumeIdentificacion.Text;

        aporte = AportesServicio.ConsultarClienteAporte(IdObjeto,0, (Usuario)Session["usuario"]);
        if (aporte.cod_persona == 0)
        {
            LblMensaje.Text = "ESTE PERSONA NO ESTA CREADA";
            txtNombre.Text = "";
        }
        else
        {
            if (aporte.cod_persona != 0)
            {
                LblMensaje.Text = "";
                if (!string.IsNullOrEmpty(aporte.nombre.ToString()))
                    txtNombre.Text = Convert.ToString(aporte.nombre);


                if (!string.IsNullOrEmpty(aporte.tipo_identificacion.ToString()))
                    DdlTipoIdentificacion.SelectedValue = HttpUtility.HtmlDecode(aporte.tipo_identificacion);
                DdlTipoIdentificacion.Enabled = false;
                if (!string.IsNullOrEmpty(aporte.cod_persona.ToString()))
                    txtCodigoCliente.Text = Convert.ToString(aporte.cod_persona);
            }
        }

    }

    private void ConsultarMaxAporte()
    {
        Int64 maxaporte = 0;
        Int64 numeroaporte = 1;
        Xpinn.Aportes.Services.AporteServices AportesServicio = new Xpinn.Aportes.Services.AporteServices();
        Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();
        aporte = AportesServicio.ConsultarMaxAporte((Usuario)Session["usuario"]);

        if (!string.IsNullOrEmpty(aporte.numero_aporte.ToString()))
            maxaporte = Convert.ToInt64(aporte.numero_aporte) + numeroaporte;
        this.txtNumAporte.Text = Convert.ToInt64(maxaporte).ToString();

    }

    private void Actualizar()
    {
        //Distribucion();

        if (txtNumeIdentificacion.Text != "")
        {
            ConsultarCliente(txtNumeIdentificacion.Text);

        }
        else
        {
            txtNombre.Text = "";
        }
    }

    private void DistribucionNuevo()
    {

        this.MvDistribucion.Visible = true;
        MvDistribucion.ActiveViewIndex = 0;
        /// llenar GvLista
        try
        {
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            lstConsulta = AporteServicio.ListarDistribucionAporteNuevo((Usuario)Session["usuario"], Convert.ToInt64(txtgrupoaporte.Text));

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session["LSTGRUPO"] = lstConsulta;
            Session.Add(AporteServicio.ProgramaModificacion + ".consulta", 1);


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaModificacion, "Actualizar", ex);
        }
    }

    private void Distribucion()
    {

        this.MvDistribucion.Visible = true;
        MvDistribucion.ActiveViewIndex = 0;
        /// llenar GvLista
        try
        {
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            lstConsulta = AporteServicio.ListarDistrAporCambiarCuota((Usuario)Session["usuario"], this.txtCodigoCliente.Text != "" ? Convert.ToInt64(this.txtCodigoCliente.Text) : 0);
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session["LSTGRUPO"] = lstConsulta;
            Session.Add(AporteServicio.ProgramaModificacion + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaModificacion, "Actualizar", ex);
        }
    }

    private Aporte ObtenerValores()
    {
        Aporte vAporte = new Aporte();
        if (txtNumAporte.Text.Trim() != "")
            vAporte.cod_persona = Convert.ToInt64(txtNumAporte.Text.Trim());
        return vAporte;
    }

    protected void LlenarComboLineaAporte(DropDownList ddlOficina)
    {
        DdlLineaAporte.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        AporteServices aporteService = new AporteServices();
        Usuario usuap = (Usuario)Session["usuario"];
        Aporte aporte = new Aporte();
        DdlLineaAporte.DataSource = aporteService.ListarLineaAporte(aporte, (Usuario)Session["usuario"]);
        DdlLineaAporte.DataTextField = "nom_linea_aporte";
        DdlLineaAporte.DataValueField = "cod_linea_aporte";
        DdlLineaAporte.DataBind();
        DdlLineaAporte.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    protected void LlenarComboPeriodicidad(DropDownList DdlPeriodicidad)
    {
        PeriodicidadService periodicidadService = new PeriodicidadService();
        Usuario usuap = (Usuario)Session["usuario"];
        Periodicidad periodicidad = new Periodicidad();
        DdlPeriodicidad.DataSource = periodicidadService.ListarPeriodicidad(periodicidad, (Usuario)Session["usuario"]);
        DdlPeriodicidad.DataTextField = "Descripcion";
        DdlPeriodicidad.DataValueField = "Codigo";
        DdlPeriodicidad.DataBind();
       // DdlPeriodicidad.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }
    

    protected void LlenarComboTipoTasa(DropDownList DdlTipoTasa)
    {
        TipoTasaService tipotasaService = new TipoTasaService();
        Usuario usuap = (Usuario)Session["usuario"];
        TipoTasa tipotasa = new TipoTasa();
        DdlTipoTasa.DataSource = tipotasaService.ListarTipoTasa(tipotasa, (Usuario)Session["usuario"]);
        DdlTipoTasa.DataTextField = "nombre";
        DdlTipoTasa.DataValueField = "cod_tipo_tasa";
        DdlTipoTasa.DataBind();
      //  DdlTipoTasa.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }


    protected void LlenarComboTasaHistorica(DropDownList DdlTipoHistorico)
    {
        TipoTasaHistService tipotasahistService = new TipoTasaHistService();
        Usuario usuap = (Usuario)Session["usuario"];
        TipoTasaHist tipotasa = new TipoTasaHist();
        DdlTipoHistorico.DataSource = tipotasahistService.ListarTipoTasaHist(tipotasa, (Usuario)Session["usuario"]);
        DdlTipoHistorico.DataTextField = "Descripcion";
        DdlTipoHistorico.DataValueField = "tipo_historico";
        DdlTipoHistorico.DataBind();
        DdlTipoHistorico.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }
    private void TraerResultadosLista()
    {

        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);

    }
    private void CargarListas()
    {
        try
        {

            ListaSolicitada = "TipoIdentificacion";
            TraerResultadosLista();
            DdlTipoIdentificacion.DataSource = lstDatosSolicitud;
            DdlTipoIdentificacion.DataTextField = "ListaDescripcion";
            DdlTipoIdentificacion.DataValueField = "ListaId";
            DdlTipoIdentificacion.DataBind();

            PoblarLista("EMPRESA_RECAUDO", ddlEmpresa);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "L", "CargarListas", ex);
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
   


    protected void txtValorCuota_TextChanged(object sender, EventArgs e)
    {                          
        VerError("");
        decimal cuota_total = 0;
        try
        {
            cuota_total = Convert.ToDecimal(txtValorCuota.Text);
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            System.Data.DataTable table = new System.Data.DataTable();

            if (Session["LSTGRUPO"] != null)
            {
                lstConsulta = (List<Xpinn.Aportes.Entities.Aporte>)Session["LSTGRUPO"];
                foreach (Aporte lItem in lstConsulta)
                {
                    lItem.cuota = Math.Round((lItem.porcentaje * cuota_total) / 100);
                    Session[AporteServicio.Codigoaporte + ".id"] = idObjeto;
                    //   vDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];

                }
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
            }
        }
        catch
        {
            VerError("No pudo distribuir la cuota");
        }
    }


    private void grabar()
    {
        Usuario usuap = new Usuario();
        try
        {
            Aporte aporte = new Aporte();
            String idObjeto2 = txtNumAporte.Text;

            aporte.cod_periodicidad = Int32.Parse(DdlPeriodicidad.SelectedValue);
            aporte.forma_pago = Int32.Parse(DdlFormaPago.SelectedValue);
            aporte.cuota = Int64.Parse(txtValorCuota.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
            aporte.cod_periodicidad = Int32.Parse(DdlPeriodicidad.SelectedValue);    
            aporte.cod_usuario = Int32.Parse(usuap.codusuario.ToString());            
            aporte.fecha_proximo_pago = DateTime.Parse(txtFecha_Prox_pago.Text);
            aporte.fecha_crea = DateTime.Now;
            if(ddlEmpresa.Visible == true && ddlEmpresa.SelectedIndex != 0)
                aporte.cod_empresa = Convert.ToInt64(ddlEmpresa.SelectedValue);
            else
                aporte.cod_empresa = 0;
            //  Recorre la grilla
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();

            if (operacion  != "N")
            {
                if (Session["LSTGRUPO"] != null)
                {
                    lstConsulta = (List<Xpinn.Aportes.Entities.Aporte>)Session["LSTGRUPO"];
                    foreach (Aporte lItem in lstConsulta)
                    {

                        aporte.valor_base = lItem.cuota;
                        aporte.cod_linea_aporte = lItem.cod_linea_aporte;
                        aporte.nom_linea_aporte = lItem.nom_linea_aporte;
                        aporte.numero_aporte = lItem.numero_aporte;

                        aporte = AporteServicio.ModificarAporte(aporte, (Usuario)Session["usuario"]);
                    }
                }
                else
                {
                    aporte = AporteServicio.ModificarAporte(aporte, (Usuario)Session["usuario"]);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.GetType().Name, "btnGuardar_Click", ex);
        }

        //Response.Redirect("~/Page/Aportes/CuentasAportes/Lista.aspx");
        Navegar(Pagina.Lista);
    }
   
    protected void ObtenerDatosDistribucion(String pIdObjeto)
    {
        try
        {
            Aporte aporte = new Aporte();
            if (pIdObjeto != null)
            {

                aporte = AporteServicio.ConsultarGrupoAporte(Convert.ToInt64(DdlLineaAporte.SelectedValue), (Usuario)Session["usuario"]);

                if (aporte.grupo == 0)
                {
                    this.LblMensaje.Text = "Esta cuenta no pertenece a ningun grupo";
                    Session["LSTGRUPO"] = null;
                    gvLista.DataSource = null;
                    gvLista.DataBind();
                    gvLista.Visible = false;
                }
                if (aporte.grupo != 0)
                {
                    txtgrupoaporte.Text = aporte.grupo.ToString();
                    DistribucionNuevo();
                    this.LblMensaje.Text = "";
                }
            }

        }


        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.GetType().Name + "A", "ObtenerDatosDistribucion", ex);
        }
    }

    protected void ObtenerDatosCliente(String pIdObjeto)
    {
        pIdObjeto = txtNumeIdentificacion.Text;
        try
        {
            Aporte aporte = new Aporte();
            if (pIdObjeto != null)
            {
                aporte.numero_aporte = Int32.Parse(pIdObjeto);
                aporte = AporteServicio.ConsultarClienteAporte(pIdObjeto,0, (Usuario)Session["usuario"]);
                
                if (aporte.numero_aporte != 0 && aporte.numero_aporte != null)
                {

                    if (!string.IsNullOrEmpty(aporte.numero_aporte.ToString()))
                        txtNumAporte.Text = HttpUtility.HtmlDecode(aporte.numero_aporte.ToString());
                    if (!string.IsNullOrEmpty(aporte.cod_linea_aporte.ToString()))
                        DdlLineaAporte.SelectedValue = HttpUtility.HtmlDecode(aporte.cod_linea_aporte.ToString());
                    if (!string.IsNullOrEmpty(aporte.cod_oficina.ToString()))
                        txtOficina.Text = HttpUtility.HtmlDecode(aporte.cod_oficina.ToString());
                    if (!string.IsNullOrEmpty(aporte.fecha_apertura.ToString()))
                        txtFecha_apertura.Text = HttpUtility.HtmlDecode(aporte.fecha_apertura.ToString());
                    if (!string.IsNullOrEmpty(aporte.cod_persona.ToString()))
                        txtCodigoCliente.Text = HttpUtility.HtmlDecode(aporte.cod_persona.ToString());
                    if (!string.IsNullOrEmpty(aporte.identificacion.ToString()))
                        txtNumeIdentificacion.Text = HttpUtility.HtmlDecode(aporte.identificacion.ToString());
                    if (!string.IsNullOrEmpty(aporte.tipo_identificacion.ToString()))
                        DdlTipoIdentificacion.SelectedValue = HttpUtility.HtmlDecode(aporte.tipo_identificacion.ToString());
                    if (!string.IsNullOrEmpty(aporte.nombre.ToString()))
                        txtNombre.Text = HttpUtility.HtmlDecode(aporte.nombre.ToString());
                    if (!string.IsNullOrEmpty(aporte.cuota.ToString()))
                        txtValorCuota.Text = HttpUtility.HtmlDecode(aporte.cuota.ToString());
                    if (!string.IsNullOrEmpty(aporte.cod_periodicidad.ToString()))
                        DdlPeriodicidad.SelectedValue = HttpUtility.HtmlDecode(aporte.cod_periodicidad.ToString());

                    if (aporte.forma_pago != 0)
                        DdlFormaPago.SelectedValue = aporte.forma_pago.ToString();
                    DdlFormaPago_SelectedIndexChanged(DdlFormaPago, null);
                    if (aporte.cod_empresa != 0 && aporte.cod_empresa != null)
                        ddlEmpresa.SelectedValue = aporte.cod_empresa.ToString();
                }
                if (string.IsNullOrEmpty(aporte.numero_aporte.ToString()))
                {
                }
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.GetType().Name + "A", "ObtenerDatosCliente", ex);
        }
    }


    protected void ObtenerDatosAporte(String pIdObjeto)
    {
        //pIdObjeto = txtNumAporte.Text;
        try
        {
            Aporte aporte = new Aporte();
            if (pIdObjeto != null)
            {
                if (aporte.numero_aporte == 0)
                {
                    aporte.numero_aporte = Int32.Parse(pIdObjeto);
                    aporte = AporteServicio.ConsultarAporte(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
                    if (!string.IsNullOrEmpty(aporte.numero_aporte.ToString()))
                    {
                        if (!string.IsNullOrEmpty(aporte.numero_aporte.ToString()))
                            txtNumAporte.Text = HttpUtility.HtmlDecode(aporte.numero_aporte.ToString());
                        if (!string.IsNullOrEmpty(aporte.cod_linea_aporte.ToString()))
                            DdlLineaAporte.SelectedValue = HttpUtility.HtmlDecode(aporte.cod_linea_aporte.ToString());
                        if (!string.IsNullOrEmpty(aporte.cod_oficina.ToString()))
                            txtOficina.Text = HttpUtility.HtmlDecode(aporte.cod_oficina.ToString());
                        if (!string.IsNullOrEmpty(aporte.fecha_apertura.ToString()))
                            txtFecha_apertura.Text = HttpUtility.HtmlDecode(aporte.fecha_apertura.ToString());
                        if (!string.IsNullOrEmpty(aporte.cod_persona.ToString()))
                            txtCodigoCliente.Text = HttpUtility.HtmlDecode(aporte.cod_persona.ToString());
                        if (!string.IsNullOrEmpty(aporte.identificacion.ToString()))
                            txtNumeIdentificacion.Text = HttpUtility.HtmlDecode(aporte.identificacion.ToString());
                        if (!string.IsNullOrEmpty(aporte.tipo_identificacion.ToString()))
                            DdlTipoIdentificacion.SelectedValue = HttpUtility.HtmlDecode(aporte.tipo_identificacion.ToString());
                        if (!string.IsNullOrEmpty(aporte.nombre.ToString()))
                            txtNombre.Text = HttpUtility.HtmlDecode(aporte.nombre.ToString());
                        if (!string.IsNullOrEmpty(aporte.cuota.ToString()))
                            txtValorCuota.Text = HttpUtility.HtmlDecode(aporte.cuota.ToString());
                        if (!string.IsNullOrEmpty(aporte.cod_periodicidad.ToString()))
                            DdlPeriodicidad.SelectedValue = HttpUtility.HtmlDecode(aporte.cod_periodicidad.ToString());
                        if (!string.IsNullOrEmpty(aporte.fecha_proximo_pago.ToString()))
                            txtFecha_Prox_pago.Text = HttpUtility.HtmlDecode(aporte.fecha_proximo_pago.ToShortDateString());

                        if (aporte.forma_pago != 0)
                            DdlFormaPago.SelectedValue = aporte.forma_pago.ToString();
                        DdlFormaPago_SelectedIndexChanged(DdlFormaPago, null);
                        if (aporte.cod_empresa != 0 && aporte.cod_empresa != null)
                            ddlEmpresa.SelectedValue = aporte.cod_empresa.ToString();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.GetType().Name + "A", "ObtenerDatosAporte", ex);
        }
    }
    

    protected void DdlLineaAporte_SelectedIndexChanged(object sender, EventArgs e)
    {

        ObtenerDatosDistribucion(DdlLineaAporte.SelectedValue);
    }

    protected void txtValorCuota_Unload(object sender, EventArgs e)
    {
        txtValorCuota.Attributes.Add("onkeypress", "return ValidNum(event);");
    }


    protected void DdlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DdlFormaPago.SelectedItem.Value == "2" || DdlFormaPago.SelectedItem.Text == "Nomina")
        {
            lblEmpresa.Visible = true;
            ddlEmpresa.Visible = true;
        }
        else
        {
            lblEmpresa.Visible = false;
            ddlEmpresa.Visible = false;
        }
    }
}