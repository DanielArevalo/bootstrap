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
    private Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
    private List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    private String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    private Xpinn.Seguridad.Services.PerfilService PerfilServicio = new Xpinn.Seguridad.Services.PerfilService();

    String operacion = "";

    private Xpinn.FabricaCreditos.Services.BeneficiarioService BeneficiarioServicio = new Xpinn.FabricaCreditos.Services.BeneficiarioService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AporteServicio.ProgramaModificacion, "E");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnCancelar_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlMensajeEliminar.eventoClick += btnContinuarEliminarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaModificacion, "Page_PreInit", ex);
        }
    }

    private void btnContinuarEliminarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        string error = string.Empty;

        List<long> lstActivos = ValidarGVActivos(out error);

        if (!string.IsNullOrWhiteSpace(error))
        {
            VerError(error);
            return;
        }

        try
        {
            foreach (var activo in lstActivos)
            {
                AporteServicio.EliminarAporte(activo, (Usuario)Session["usuario"]);
            }

            MvDistribucion.ActiveViewIndex = 1;
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarEliminar(false);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("ORA-02292"))
            {
                VerError("No se puede borrar el aporte porque ya tiene movimientos");
            }
            else
            {
                VerError("Error al eliminar los aportes, " + ex.Message);
            }

            return;
        }
    }

    private void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        ctlMensajeEliminar.MostrarMensaje("Desea realizar la eliminacion?");
    }

    private List<long> ValidarGVActivos(out string error)
    {
        error = string.Empty;
        List<long> lstActivo = new List<long>();

        foreach (GridViewRow fila in gvLista.Rows)
        {
            CheckBox chkSeleccion = (CheckBox)fila.FindControl("chkSelect");

            if (chkSeleccion.Checked == true)
            {
                int index = fila.RowIndex;
                lstActivo.Add(Convert.ToInt64(gvLista.DataKeys[index].Value));
            }
        }

        // Valido una seleccion valida de Activo Fijo, retorno y notifico el error en caso de uno
        if (lstActivo.Count == 0)
        {
            error += " Debe Escoger un Aporte Fijo, ";
            return lstActivo;
        }
        if (lstActivo.Contains(0))
        {
            error += " Aporte Invalido, ";
            return lstActivo;
        }

        return lstActivo;
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
                    //if (txtNumeIdentificacion.Text != "")
                    //{   YA NO SE USA EL FILTRO POR IDENTIFICACION
                    //    ObtenerDatosCliente(txtNumeIdentificacion.Text);  
                    //    Distribucion();
                    //}
                    if (idObjeto != "")
                    {
                        ObtenerDatosAporte(idObjeto);
                        Distribucion(DdlLineaAporte.SelectedValue);
                    }
                    txtOficina.Text = Convert.ToString(oficina);
                    txtOficinaNombre.Text = Convert.ToString(usuap.nombre_oficina);

                    this.LblMensaje.Text = "";
                }

                DdlFormaPago_SelectedIndexChanged(DdlFormaPago, null);
                if (txtCodigoCliente.Text != "")
                {
                    CargarListas();
                }
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaModificacion, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (txtNumeIdentificacion.Text == "" || txtFecha_apertura.Text == "" || txtValorCuota.Text == "")
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
            grabar();

            MvDistribucion.ActiveViewIndex = 1;
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarEliminar(false);
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

        aporte = AportesServicio.ConsultarClienteAporte(IdObjeto, 0, (Usuario)Session["usuario"]);
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
                if (!string.IsNullOrEmpty(aporte.nombre))
                    txtNombre.Text = Convert.ToString(aporte.nombre);


                if (!string.IsNullOrEmpty(aporte.tipo_identificacion))
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

    private void Distribucion(string pcod_linea_aporte)
    {

        this.MvDistribucion.Visible = true;
        MvDistribucion.ActiveViewIndex = 0;
        /// llenar GvLista
        try
        {
            // Traer listado de aportes que pertenezcan a un grupo
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            lstConsulta = AporteServicio.ListarDistrAporCambiarCuota((Usuario)Session["usuario"], this.txtCodigoCliente.Text != "" ? Convert.ToInt64(this.txtCodigoCliente.Text) : 0);
            // Mirar si la lìnea del aporte que se esta modificando pertenece al grupo
            int linea = 0;
            int consulta = 0;
            if (lstConsulta != null)
            {
                linea = Convert.ToInt32(ConvertirStringToInt(pcod_linea_aporte));
                consulta = (from item in lstConsulta
                            where item.cod_linea_aporte == linea
                            select new { Aporte = item }).Count();
            }
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0 && (consulta > 0 || linea == 0))
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
                if (consulta == 0)
                    lstConsulta = null;
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

            PoblarLista("v_persona_empresa_recaudo", ddlEmpresa);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        var cod_per = "";
        cod_per = txtCodigoCliente.Text;

        PoblarLista(pTabla, "", "cod_persona = '" + cod_per + "'", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegableEmpresaaportes(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
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
            List<Aporte> lstConsulta = new List<Aporte>();

            if (Session["LSTGRUPO"] != null)
            {
                lstConsulta = (List<Aporte>)Session["LSTGRUPO"];
                foreach (Aporte lItem in lstConsulta)
                {
                    lItem.cuota = Math.Round((lItem.porcentaje * cuota_total) / 100);
                    Session[AporteServicio.Codigoaporte + ".id"] = idObjeto;
                    //   vDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];

                }
                gvLista.DataSource = lstConsulta;

                //if (!IsPostBack) // Si sacas el databind de aca el checkbox se pierde 
                //{
                gvLista.DataBind(); // Prefiero que me muestre el valor bien de la distribucion
                //}
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

            // Cargar datos del aporte
            aporte.numero_aporte = Int64.Parse(txtNumAporte.Text);
            aporte.cod_periodicidad = Int32.Parse(DdlPeriodicidad.SelectedValue);
            aporte.forma_pago = Int32.Parse(DdlFormaPago.SelectedValue);
            aporte.cuota = Int64.Parse(txtValorCuota.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
            aporte.cod_periodicidad = Int32.Parse(DdlPeriodicidad.SelectedValue);
            aporte.cod_usuario = Int32.Parse(usuap.codusuario.ToString());
            aporte.fecha_proximo_pago = DateTime.Parse(txtFecha_Prox_pago.Text);
            aporte.fecha_crea = DateTime.Now;
            aporte.fecha_apertura = Convert.ToDateTime(txtFecha_aperturaModificar.Text);

            if (DdlEstado.SelectedIndex >= 0)
                aporte.estado = Convert.ToInt32(DdlEstado.SelectedValue);
            if (ddlEmpresa.Visible == true && ddlEmpresa.SelectedIndex != 0)
                aporte.cod_empresa = Convert.ToInt64(ddlEmpresa.SelectedValue);
            else
                aporte.cod_empresa = 0;

            aporte.lstBeneficiarios = ObtenerListaBeneficiarios();
            //  Hacer la modificación dependiendo de si la cuenta pertenece a un grupo de aporte 
            if (operacion != "N")
            {
                if (Session["LSTGRUPO"] != null)
                {
                    List<Aporte> lstConsulta = new List<Aporte>();
                    lstConsulta = (List<Aporte>)Session["LSTGRUPO"];
                    if (lstConsulta.Count > 0)
                    {
                        foreach (Aporte lItem in lstConsulta)
                        {
                            if (lItem.principal != Int64.MinValue && lItem.principal == 1)
                                aporte.cuota = Convert.ToDecimal(txtValorCuota.Text);
                            else
                                aporte.cuota = 0;

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
                aporte = AporteServicio.ConsultarClienteAporte(pIdObjeto, 0, (Usuario)Session["usuario"]);

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
                    // Determinar el estado del aporte
                    if (aporte.estado != null)
                        DdlEstado.SelectedValue = aporte.estado.ToString();
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
            int perfiladministrador = 0;
            Aporte aporte = new Aporte();
            if (pIdObjeto != null)
            {
                if (aporte.numero_aporte == 0)
                {
                    aporte.numero_aporte = Int64.Parse(pIdObjeto);
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
                        {
                            txtFecha_apertura.Text = HttpUtility.HtmlDecode(aporte.fecha_apertura.ToShortDateString());
                            txtFecha_aperturaModificar.Text = HttpUtility.HtmlDecode(aporte.fecha_apertura.ToShortDateString());
                            Xpinn.Seguridad.Entities.Perfil vPerfil = new Xpinn.Seguridad.Entities.Perfil();

                            Usuario usuap = (Usuario)Session["usuario"];
                            vPerfil = PerfilServicio.ConsultarPerfil(usuap.codperfil, (Usuario)Session["usuario"]);
                             if (vPerfil.es_administrador != int.MinValue)                            
                                perfiladministrador = vPerfil.es_administrador;     
                             if(perfiladministrador ==1)                             
                               txtFecha_aperturaModificar.Enabled = true;
                             else
                            txtFecha_aperturaModificar.Enabled = false;
                        }


                        if (!string.IsNullOrEmpty(aporte.cod_persona.ToString()))
                            txtCodigoCliente.Text = HttpUtility.HtmlDecode(aporte.cod_persona.ToString());
                        if (!string.IsNullOrEmpty(aporte.identificacion))
                            txtNumeIdentificacion.Text = HttpUtility.HtmlDecode(aporte.identificacion.ToString());
                        if (!string.IsNullOrEmpty(aporte.tipo_identificacion))
                            DdlTipoIdentificacion.SelectedValue = HttpUtility.HtmlDecode(aporte.tipo_identificacion.ToString());
                        if (!string.IsNullOrEmpty(aporte.nombre))
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
                        if (aporte.estado != 0)
                            DdlEstado.SelectedValue = aporte.estado.ToString();

                        //Beneficiarios
                        if (aporte.lstBeneficiarios.Count > 0)
                        {
                            gvBeneficiarios.DataSource = aporte.lstBeneficiarios;
                            gvBeneficiarios.DataBind();
                        }
                        upBeneficiarios.Visible = true;
                        txtFecha_apertura.Enabled = false;
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
    #region Beneficiarios Aportes
    protected string FormatoFecha()
    {
        Configuracion conf = new Configuracion();
        return conf.ObtenerFormatoFecha();
    }
    protected List<Beneficiario> ObtenerListaBeneficiarios()
    {
        List<Beneficiario> lstBeneficiarios = new List<Beneficiario>();

        foreach (GridViewRow rfila in gvBeneficiarios.Rows)
        {
            Beneficiario eBenef = new Beneficiario();
            HiddenField lblidbeneficiario = (HiddenField)rfila.FindControl("hdIdBeneficiario");
            if (lblidbeneficiario.Value != "")
                eBenef.idbeneficiario = Convert.ToInt64(lblidbeneficiario.Value);

            eBenef.numero_programado = idObjeto;

            DropDownList ddlParentezco = (DropDownList)rfila.FindControl("ddlParentezco");
            if (ddlParentezco.SelectedValue != null || ddlParentezco.SelectedIndex != 0)
                eBenef.parentesco = Convert.ToInt32(ddlParentezco.SelectedValue);

            DropDownList ddlSexo = (DropDownList)rfila.FindControl("ddlsexo");
            if (ddlSexo.SelectedValue != null)
                eBenef.sexo = Convert.ToString(ddlSexo.SelectedValue);

            TextBox txtIdentificacion = (TextBox)rfila.FindControl("txtIdentificacion");
            if (txtIdentificacion != null)
                eBenef.identificacion_ben = Convert.ToString(txtIdentificacion.Text);

            TextBox txtEdadBen = (TextBox)rfila.FindControl("txtEdadBen");
            if (txtEdadBen != null)
            {
                if (txtEdadBen.Text != "")
                {
                    eBenef.edad = Convert.ToInt32(txtEdadBen.Text);
                }
            }
            TextBox txtNombres = (TextBox)rfila.FindControl("txtNombres");
            if (txtNombres != null)
                eBenef.nombre_ben = Convert.ToString(txtNombres.Text);

            fechaeditable txtFechaNacimientoBen = (fechaeditable)rfila.FindControl("txtFechaNacimientoBen");
            if (txtFechaNacimientoBen != null)
                if (txtFechaNacimientoBen.Texto != "")
                    eBenef.fecha_nacimiento_ben = txtFechaNacimientoBen.ToDateTime;
                else
                    eBenef.fecha_nacimiento_ben = null;
            else
                eBenef.fecha_nacimiento_ben = null;
            decimalesGridRow txtPorcentaje = (decimalesGridRow)rfila.FindControl("txtPorcentaje");
            if (txtPorcentaje != null)
                eBenef.porcentaje_ben = Convert.ToDecimal(txtPorcentaje.Text);

            lstBeneficiarios.Add(eBenef);
        }
        return lstBeneficiarios;
    }
    protected void gvBeneficiarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlSexo = (DropDownList)e.Row.FindControl("ddlsexo");
            DropDownList ddlParentezco = (DropDownList)e.Row.FindControl("ddlParentezco");
            if (ddlParentezco != null)
            {
                Beneficiario Ben = new Beneficiario();
                ddlParentezco.DataTextField = "DESCRIPCION";
                ddlParentezco.DataValueField = "CODPARENTESCO";
                ddlParentezco.DataSource = BeneficiarioServicio.ListarParentesco(Ben, (Usuario)Session["usuario"]);
                ddlParentezco.Items.Insert(0, new ListItem("<Seleccione un item>", "0"));
                ddlParentezco.DataBind();
            }
            Label lblParentesco = (Label)e.Row.FindControl("lblParentesco");
            if (lblParentesco.Text != null)
                ddlParentezco.SelectedValue = lblParentesco.Text;
        }
    }
    protected void btnAddRowBeneficio_Click(object sender, EventArgs e)
    {
        //Session["DatosBene"] = null;

        List<Beneficiario> lstBene = new List<Beneficiario>();
        lstBene = ObtenerListaBeneficiarios();
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
                eBenef.parentesco = 0;
                eBenef.porcentaje_ben = null;
                eBenef.edad = null;
                eBenef.sexo = "0";
                lstBene.Add(eBenef);
            }
            if (lstBene.Count > 0)
            {
                gvBeneficiarios.DataSource = lstBene;
                gvBeneficiarios.DataBind();
            }
        }
        else if (porcentaje == 100)
        {
            VerError("El porcentaje total de beneficiarios se encuentra completo");
        }
    }
    protected void gvBeneficiarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            List<Beneficiario> LstBene = ObtenerListaBeneficiarios();
            int IdBeneficiario = Convert.ToInt32(gvBeneficiarios.DataKeys[e.RowIndex].Values[0].ToString());

            if (IdBeneficiario > 0)
            {
                BeneficiarioServicio.EliminarBeneficiarioAporte(IdBeneficiario, (Usuario)Session["usuario"]);
            }

            LstBene.RemoveAt((gvBeneficiarios.PageIndex * gvBeneficiarios.PageSize) + e.RowIndex);
            gvBeneficiarios.DataSource = LstBene;
            gvBeneficiarios.DataBind();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }
    #endregion

    protected void txtPorcentaje_eventoCambiar(object sender, EventArgs e)
    {
        TextBox txtPorcentaje = (TextBox)sender;
        List<Beneficiario> lstBeneficiarios = ObtenerListaBeneficiarios();
        if (lstBeneficiarios != null)
        {
            int porcentaje = 0;
            porcentaje = Convert.ToInt32(lstBeneficiarios.Where(x => x.porcentaje_ben > 0).Sum(x => x.porcentaje_ben));
            if (porcentaje > 100)
            {
                txtPorcentaje.Text = "";
                VerError("La sumatoria del porcentaje es mayor a 100");
                RegistrarPostBack();
            }
        }
    }
}