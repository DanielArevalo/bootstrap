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

public partial class Detalle : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AporteServicio.ProgramaAperturaAporte, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];
            if (!IsPostBack)
            {             
                LlenarComboLineaAporte(DdlLineaAporte);
                LlenarComboPeriodicidad(DdlPeriodicidad);                
                CargarListas();
                if (Session[AporteServicio.ProgramaAperturaAporte + ".id"] != null)
                {
                    idObjeto = Session[AporteServicio.ProgramaAperturaAporte.ToString() + ".id"].ToString();
                    Session.Remove(AporteServicio.ProgramaAperturaAporte.ToString() + ".id");
                    ObtenerDatos(idObjeto);
                    this.LblMensaje.Text = "";
                    Distribucion();
                }                  
                if (Session[AporteServicio.ProgramaAperturaAporte + ".consulta"] != null)
                    Actualizar();
                this.LblMensaje.Text = "";

                DdlFormaPago_SelectedIndexChanged(DdlFormaPago, null);
                  
            }            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "Page_Load", ex);
        }
    }

    protected void btnEditar_Click(object sender, EventArgs e)
    {
        Session[AporteServicio.ProgramaAperturaAporte + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);        
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session[AporteServicio.ProgramaAperturaAporte + ".id"] = idObjeto;
        Navegar(Pagina.Lista);
    }

    
    private Boolean ConsultarClienteAporte(Int64 Id)
    {
        Boolean result = true;
        VerError("");
        Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 persona1 = new Persona1();
        persona1 = personaServicio.ConsultarPersona1(Id, _usuario);
        if (persona1 == null)
            result = false;
        else
            result = ConsultarClienteAporte(persona1.identificacion);
        return result;
    }


    private Boolean ConsultarClienteAporte(string Id)
    {
        Boolean result = true;
        VerError("");
        Int64 numeroaporte = 1;
        AporteServices AportesServicio = new AporteServices();
        Aporte aporte = new Aporte();
        aporte = AportesServicio.ConsultarClienteAporte(Id,0, _usuario);

        if (!string.IsNullOrEmpty(aporte.numero_aporte.ToString()))
            numeroaporte = aporte.numero_aporte;
        if (numeroaporte > 0)
        {
            this.LblMensaje.Text = "Cliente ya tiene cuenta de aportes creada";
            result = false;
        }
        return result;
    }


    public void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AporteServicio.ProgramaAperturaAporte);
        Navegar(Pagina.Lista);
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
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "gvLista_PageIndexChanging", ex);
        }
    }
    private void ConsultarCliente(String pIdObjeto)
    {
            Xpinn.Aportes.Services.AporteServices AportesServicio = new Xpinn.Aportes.Services.AporteServices();
        Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();
        String IdObjeto = txtNumeIdentificacion.Text;

        aporte = AportesServicio.ConsultarClienteAporte(IdObjeto,0, _usuario);
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

    private void Actualizar()
    {
        if (txtNumeIdentificacion.Text != "")
        {
            ConsultarCliente(txtNumeIdentificacion.Text);           
        }
        else
        {
            txtNombre.Text = "";
        }             
    }

    private void Distribucion()
    {       
        MvDistribucion.Visible = true;
        MvDistribucion.ActiveViewIndex = 0;
        /// llenar GvLista
        try
        {
            List<Aporte> lstConsulta = new List<Aporte>();
            List<Aporte> lstCuentaGrupo = new List<Aporte>();
            string pLinea = DdlLineaAporte.SelectedItem != null ? DdlLineaAporte.SelectedValue : null;
            lstConsulta = AporteServicio.ListarDistribucionAporte(_usuario , Convert.ToInt64(txtCodigoCliente.Text), pLinea);

            var aportePrincipal = lstConsulta.FirstOrDefault();

            if (aportePrincipal == null)
            {
                VerError("no se pudo conseguir el aporte principal");
                return;
            }

            // Si el aporte tiene grupo de aporte valida que esten creadas las cuentas requeridas
            if (aportePrincipal.grupo != 0)
            {
                lstCuentaGrupo = AporteServicio.ConsultarCuentasPorGrupoAporte(aportePrincipal.grupo, _usuario);

                // Valida que tenga las cuentas creadas que requiere el grupo
                var validacionCuentas = from cuentaGrupo in lstCuentaGrupo
                                        where !lstConsulta.Exists(x => x.cod_linea_aporte == cuentaGrupo.cod_linea_aporte)
                                        select cuentaGrupo;

                if (validacionCuentas.Count() > 0)
                {
                    // Extension Method en ExtensionMethodsHelper XPINN.UTIL
                    validacionCuentas.ForEach(x => x.pendiente_crear = true);

                    lstConsulta.AddRange(validacionCuentas);
                }
            }

            foreach (var consulta in lstConsulta)
            {
                decimal valorCuota = Convert.ToDecimal(txtValorCuota.Text);
                consulta.cuota = valorCuota * (Convert.ToDecimal(consulta.porcentaje) / 100);
            }

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
            Session.Add(AporteServicio.ProgramaAperturaAporte + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "Actualizar", ex);
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
        AporteServices aporteService = new AporteServices();
        Aporte aporte = new Aporte();
        DdlLineaAporte.DataSource = aporteService.ListarLineaAporte(aporte, _usuario);
        DdlLineaAporte.DataTextField = "nom_linea_aporte";
        DdlLineaAporte.DataValueField = "cod_linea_aporte";
        DdlLineaAporte.DataBind();
        DdlLineaAporte.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    protected void LlenarComboPeriodicidad(DropDownList DdlPeriodicidad)
    {
        PeriodicidadService periodicidadService = new PeriodicidadService();
        Periodicidad periodicidad = new Periodicidad();
        DdlPeriodicidad.DataSource = periodicidadService.ListarPeriodicidad(periodicidad, _usuario);
        DdlPeriodicidad.DataTextField = "Descripcion";
        DdlPeriodicidad.DataValueField = "Codigo";
        DdlPeriodicidad.DataBind();
        DdlPeriodicidad.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }


    protected void LlenarComboTipoTasa(DropDownList DdlTipoTasa)
    {
        TipoTasaService tipotasaService = new TipoTasaService();
        TipoTasa tipotasa = new TipoTasa();
        DdlTipoTasa.DataSource = tipotasaService.ListarTipoTasa(tipotasa, _usuario);
        DdlTipoTasa.DataTextField = "nombre";
        DdlTipoTasa.DataValueField = "cod_tipo_tasa";
        DdlTipoTasa.DataBind();
        DdlTipoTasa.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }


    protected void LlenarComboTasaHistorica(DropDownList DdlTipoHistorico)
    {
        TipoTasaHistService tipotasahistService = new TipoTasaHistService();
        TipoTasaHist tipotasa = new TipoTasaHist();
        DdlTipoHistorico.DataSource = tipotasahistService.ListarTipoTasaHist(tipotasa, _usuario);
        DdlTipoHistorico.DataTextField = "Descripcion";
        DdlTipoHistorico.DataValueField = "tipo_historico";
        DdlTipoHistorico.DataBind();
        DdlTipoHistorico.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, _usuario);
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
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, _usuario);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }
          
          
    protected void ObtenerDatosDistribucion(String pIdObjeto)
    {

        try
        {
            Aporte aporte = new Aporte();
            if (pIdObjeto != null)
            {

                aporte = AporteServicio.ConsultarGrupoAporte(Convert.ToInt64(DdlLineaAporte.SelectedValue), _usuario);

                if (aporte.grupo == 0)
                {
                    //this.LblMensaje.Text = "Esta cuenta no pertenece a ningun grupo";
                }
                if (aporte.grupo != 0)
                {
                    txtgrupoaporte.Text = aporte.grupo.ToString();
                    this.LblMensaje.Text = "";
                }
            }

        }        
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.GetType().Name + "A", "ObtenerDatosDistribucion", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {       
        try
        {
            Aporte aporte = new Aporte();
            if (pIdObjeto != null)
            {
                aporte.numero_aporte = Int64.Parse(pIdObjeto);
                aporte = AporteServicio.ConsultarAporte(Convert.ToInt64(pIdObjeto), _usuario);
                if (!string.IsNullOrEmpty(aporte.numero_aporte.ToString()))
                {
                    txtNumAporte.Text = aporte.numero_aporte.ToString();
                    this.DdlLineaAporte.SelectedValue = aporte.cod_linea_aporte.ToString();
                    txtOficina.Text = aporte.cod_oficina.ToString();
                    txtFecha_apertura.Text = aporte.fecha_apertura.ToString(gFormatoFecha);
                    txtCodigoCliente.Text = aporte.cod_persona.ToString();
                    txtNumeIdentificacion.Text = aporte.identificacion.ToString();
                    this.DdlTipoIdentificacion.SelectedValue = aporte.tipo_identificacion.ToString();
                    txtNombre.Text = aporte.nombre.ToString();
                    txtValorCuota.Text = aporte.cuota.ToString();
                    DdlPeriodicidad.SelectedValue = aporte.cod_periodicidad.ToString();                 
                    DdlFormaPago.SelectedValue = aporte.forma_pago.ToString();
                    txtFecha_Proxppago.Text = aporte.fecha_proximo_pago.ToString(gFormatoFecha);
                    if(aporte.fecha_cierre != DateTime.MinValue)
                        txtFecha_interes.Text = aporte.fecha_cierre.ToString(gFormatoFecha);
                    DdlEstado.SelectedValue = aporte.estado.ToString();
                    txtOficina.Text = aporte.cod_oficina.ToString();
                    txtOficinaNombre.Text = aporte.nom_oficina;
                    if (aporte.cod_empresa != 0 && aporte.cod_empresa != null)
                    {
                        ddlEmpresa.SelectedValue = aporte.cod_empresa.ToString();
                    }
                    DdlFormaPago_SelectedIndexChanged(DdlFormaPago, null);                  
                }                            
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
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

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox checkbox = e.Row.FindControl("chkPendiente") as CheckBox;

            if (checkbox != null && checkbox.Checked)
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
        }
    }
}