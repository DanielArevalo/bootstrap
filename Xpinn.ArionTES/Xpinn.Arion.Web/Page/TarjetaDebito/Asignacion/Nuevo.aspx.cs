using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.TarjetaDebito.Services;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.Comun.Services;
using Xpinn.Comun.Entities;
using System.Globalization;

public partial class Nuevo : GlobalWeb
{
    TarjetaService AsignacionTarjetaServicio = new TarjetaService();
    Tarjeta entidad = new Tarjeta();

    Tarjeta ptarjeta = new Tarjeta();
    Tarjeta pvalores = new Tarjeta();
    Int64 tarjeta;
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
    Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    //Decimal aportegrupo;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[AsignacionTarjetaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(AsignacionTarjetaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(AsignacionTarjetaServicio.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            //ctlBusquedaPersonas.eventoEditar += gvControl_RowEditing;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AsignacionTarjetaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                ViewState["CurrentAlphabet"] = "TODO";
                GenerateAlphabets();
                CargarDropDown();
                if (Session[AsignacionTarjetaServicio.CodigoPrograma + ".id"] != null)
                {
                    MvPrincipal.ActiveViewIndex = 2;

                    //MvPrincipal.Visible = false; 
                    //ViewCuenta.Visible=true;
                    idObjeto = Session[AsignacionTarjetaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AsignacionTarjetaServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarConsultar(false);
                }
                else
                {
                    MvPrincipal.ActiveViewIndex = 0;
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(false);
                }
                //   ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                // rbCalculoTasa_SelectedIndexChanged(rbCalculoTasa, null);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AsignacionTarjetaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    private Xpinn.Contabilidad.Entities.Tercero ObtenerValores()
    {
        Xpinn.Contabilidad.Entities.Tercero vTercero = new Xpinn.Contabilidad.Entities.Tercero();
        if (ddlTipoPersona.SelectedValue.Trim() != "")
            vTercero.tipo_persona = Convert.ToString(ddlTipoPersona.SelectedValue.Trim());
        if (txtCodigo.Text.Trim() != "")
            vTercero.cod_persona = Convert.ToInt64(txtCodigo.Text.Trim());
        if (txtNumeIdentificacion2.Text.Trim() != "")
            vTercero.identificacion = Convert.ToString(txtNumeIdentificacion2.Text.Trim());
        if (txtNombres.Text.Trim() != "")
            vTercero.primer_nombre = Convert.ToString(txtNombres.Text.Trim());
        if (txtApellidos.Text.Trim() != "")
            vTercero.primer_apellido = Convert.ToString(txtApellidos.Text.Trim());
        if (txtRazonSocial.Text.Trim() != "")
            vTercero.razon_social = Convert.ToString(txtRazonSocial.Text.Trim());
        if (ddlCiudad.SelectedValue.Trim() != "")
            vTercero.codciudadexpedicion = Convert.ToInt64(ddlCiudad.SelectedValue.Trim());
        return vTercero;
    }

    private void Actualizar(int pOrden)
    {
        Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();

        string sLetra = ViewState["CurrentAlphabet"].ToString();
        string sFiltro = "";
        if (sLetra != "TODO" && sLetra.Trim() != "")
        {
            sFiltro = " (primer_apellido Like '" + sLetra + "%' OR razon_social Like '" + sLetra + "%') ";
        }
        try
        {
            List<Xpinn.Contabilidad.Entities.Tercero> lstConsulta = new List<Xpinn.Contabilidad.Entities.Tercero>();
            lstConsulta = TerceroServicio.ListarTercero(ObtenerValores(), sFiltro, Convert.ToString(pOrden), (Usuario)Session["usuario"]);

            gvListaAFiliados.PageSize = pageSize;
            gvListaAFiliados.EmptyDataText = emptyQuery;
            gvListaAFiliados.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvListaAFiliados.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                MvAfiliados.ActiveViewIndex = 0;
                gvListaAFiliados.DataBind();
            }
            else
            {
                gvListaAFiliados.Visible = false;
                lblTotalRegs.Visible = false;
                MvAfiliados.ActiveViewIndex = -1;
            }
            Session["DTPERSONAS"] = lstConsulta;
            Session.Add(AsignacionTarjetaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AsignacionTarjetaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void rbCalculoTasa_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar(0);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        MvPrincipal.ActiveViewIndex = 0;
    }


    private void CargarDropDown()
    {
        try
        {
            //TIPOIDENTIFICACION
            ListaSolicitada = "TipoIdentificacion";
            TraerResultadosLista();
            ddlTipoIdentifi.DataSource = lstDatosSolicitud;
            ddlTipoIdentifi.DataTextField = "ListaDescripcion";
            ddlTipoIdentifi.DataValueField = "ListaId";
            ddlTipoIdentifi.DataBind();

            Xpinn.Asesores.Data.OficinaData vDatosOficina = new Xpinn.Asesores.Data.OficinaData();
            Xpinn.Asesores.Entities.Oficina pOficina = new Xpinn.Asesores.Entities.Oficina();
            List<Xpinn.Asesores.Entities.Oficina> lstOficina = new List<Xpinn.Asesores.Entities.Oficina>();
            pOficina.Estado = 1;
            lstOficina = vDatosOficina.ListarOficina(pOficina, (Usuario)Session["usuario"]);
            if (lstOficina.Count > 0)
            {
                ddlOficina.DataSource = lstOficina;
                ddlOficina.DataTextField = "NombreOficina";
                ddlOficina.DataValueField = "IdOficina";
                ddlOficina.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlOficina.AppendDataBoundItems = true;
                ddlOficina.SelectedIndex = 0;
                ddlOficina.DataBind();
            }

            ddlTipoCuenta.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoCuenta.Items.Insert(1, new ListItem("Ahorros", "1"));
            ddlTipoCuenta.Items.Insert(2, new ListItem("Credito Rotativo", "2"));
            ddlTipoCuenta.SelectedIndex = 0;
            ddlTipoCuenta.DataBind();

            ddlConvenio.DataSource = AsignacionTarjetaServicio.ListarConvenio(entidad, (Usuario)Session["usuario"]);
            ddlConvenio.DataTextField = "nom_convenio";
            ddlConvenio.DataValueField = "codconvenio";
            ddlConvenio.DataBind();
            ddlConvenio.Items.Insert(0, new ListItem("Selecione un item", "0"));
            ddlConvenio.SelectedIndex = 0;

            LlenarListasDesplegables(TipoLista.Asesor, ddlAsesor);

            ddlEstado.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlEstado.Items.Insert(1, new ListItem("Activo", "1"));
            ddlEstado.Items.Insert(2, new ListItem("Inactivo", "2"));
            ddlEstado.SelectedIndex = 0;
            ddlEstado.DataBind();


            // PoblarLista("MOTIVO_PROGRAMADO", ddlMotivoApertura);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AsignacionTarjetaServicio.GetType().Name + "L", "CargarDropDown", ex);
        }
    }



    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Tarjeta ptarjeta = new Tarjeta();

            if (pIdObjeto != null)
            {

                txtNombres.Text = Convert.ToString(Session["nombres"]);
                txtIdentificacion.Text = Convert.ToString(Session["identificacion"]);

                ptarjeta = AsignacionTarjetaServicio.ConsultarAsignacion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
                if (ptarjeta.numtarjeta != "")
                {
                    ddlTipoCuenta.SelectedValue = ptarjeta.tipo_cuenta.ToString();
                    if (ptarjeta.numero_cuenta != "")
                        txtCuentas.Text = Convert.ToString(ptarjeta.numero_cuenta);
                    //ddlCuenta.SelectedValue = ptarjeta.numero_cuenta.ToString();
                    ddlCuenta.Visible = false;
                    txtSaldoTotal.Text = ptarjeta.saldo_total.ToString();
                    txtSaldoDisponible.Text = ptarjeta.saldo_disponible.ToString();
                    if (ptarjeta.fecha_asignacion != DateTime.MinValue)
                        TxtFechaAsignacion.Text = ptarjeta.fecha_asignacion.ToString();
                    ddlConvenio.SelectedValue = ptarjeta.cod_convenio.ToString();
                    ddlOficina.SelectedValue = ptarjeta.cod_oficina.ToString();
                    if (ptarjeta.numtarjeta != "")
                        txtTarjeta.Text = Convert.ToString(ptarjeta.numtarjeta);
                    ddlNumtarjeta.Visible = false;
                    //ddlNumtarjeta.SelectedValue= ptarjeta.numtarjeta.ToString();
                    chkCobraCuota.Checked = ptarjeta.cobra_cuota_manejo == 1 ? true : false;
                    txtCuota.Text = ptarjeta.cuota_manejo.ToString();
                    ddlEstado.SelectedValue = ptarjeta.estado.ToString();
                    txtCupoMaximo.Text = ptarjeta.cupo.ToString();
                    txtMaxTransacciones.Text = ptarjeta.max_tran.ToString();

                    if (ptarjeta.cod_asesor.HasValue)
                    {
                        ddlAsesor.SelectedValue = ptarjeta.cod_asesor.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AsignacionTarjetaServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    protected void ObtenerDatosNumTarjeta(String pIdObjeto)
    {
        try
        {
            Tarjeta ptarjeta = new Tarjeta();

            if (pIdObjeto != null)
            {


                ptarjeta = AsignacionTarjetaServicio.ConsultarNumTarjeta(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
                if (ptarjeta.numtarjeta != "")
                {
                    tarjeta = Convert.ToInt64(ptarjeta.numtarjeta.ToString());

                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AsignacionTarjetaServicio.GetType().Name + "A", "ObtenerDatosNumTarjeta", ex);
        }
    }



    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }


    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
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


    protected Boolean ValidarDatos()
    {
        VerError("");

        if (this.ddlTipoCuenta.SelectedIndex == 0)
        {
            VerError("Seleccione la  Cuenta correspondiente");
            ddlTipoCuenta.Focus();
            return false;
        }
        if (this.ddlCuenta.SelectedValue == "0")
        {
            VerError("Seleccione la  Cuenta");
            ddlTipoCuenta.Focus();
            return false;
        }

        if (ddlOficina.SelectedIndex == 0)
        {
            VerError("Seleccione la Oficina correspondiente");
            ddlOficina.Focus();
            return false;
        }

        // if (txtCodigoCliente.Text == "")
        //{
        //  VerError("Seleccione la persona");
        //return false;
        //}
        if (txtCuota.Text == "0")
        {
            VerError("Ingrese el valor de la cuota");
            txtCuota.Focus();
            return false;
        }
        if (this.ddlConvenio.SelectedIndex == 0)
        {
            VerError("Seleccione  el convenio  correspondiente");
            ddlConvenio.Focus();
            return false;
        }
        if (this.ddlNumtarjeta.SelectedIndex == 0)
        {
            VerError("Seleccione  el num tarjeta");
            ddlNumtarjeta.Focus();
            return false;
        }


        ObtenerDatosNumTarjeta(ddlNumtarjeta.SelectedValue);

        ptarjeta.numtarjeta = Convert.ToString(tarjeta);
        tarjeta = Convert.ToInt64(ptarjeta.numtarjeta);
        if (Convert.ToInt64(ddlNumtarjeta.SelectedValue) == tarjeta)
        {
            VerError("El num de la tarjeta ya se encuentra asignado");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            string msj = idObjeto != "" ? "modificación" : "grabación";
            ctlMensaje.MostrarMensaje("Desea realizar la " + msj + " de los registros");
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Usuario usuap = new Usuario();
        usuap = (Usuario)Session["usuario"];
        try
        {

            Tarjeta ptarjeta = new Tarjeta();
            Usuario usuario = new Usuario();
            if (ddlTipoCuenta.SelectedIndex != 0)
                ptarjeta.tipo_cuenta = ddlTipoCuenta.SelectedValue;

            ptarjeta.cod_persona = Convert.ToInt64(this.txtCodigoCliente.Text.Trim());
            ptarjeta.numero_cuenta = Convert.ToString(this.ddlCuenta.SelectedValue.Trim());
            ptarjeta.saldo_total = Convert.ToDecimal(this.txtSaldoTotal.Text.Trim());
            ptarjeta.saldo_disponible = Convert.ToDecimal(this.txtSaldoDisponible.Text.Trim());
            ptarjeta.fecha_asignacion = Convert.ToDateTime(this.TxtFechaAsignacion.Text);
            if (ddlConvenio.SelectedIndex != 0)
                ptarjeta.cod_convenio = Convert.ToInt32(this.ddlConvenio.SelectedValue);
            if (this.ddlOficina.SelectedIndex != 0)
                ptarjeta.cod_oficina = Convert.ToInt32(this.ddlOficina.SelectedValue);
            if (this.ddlEstado.SelectedIndex != 0)
                ptarjeta.estado = Convert.ToString(this.ddlEstado.SelectedValue);
            if (this.ddlNumtarjeta.SelectedIndex != 0)
                tarjeta = Convert.ToInt64(this.ddlNumtarjeta.SelectedItem.Text);
            ptarjeta.numtarjeta = Convert.ToString(tarjeta);

            ptarjeta.cuota_manejo = Convert.ToDecimal(this.txtCuota.Text.Trim());
            if (ddlEstado.SelectedIndex != 0)
                ptarjeta.estado = Convert.ToString(ddlEstado.SelectedValue);
            ptarjeta.cobra_cuota_manejo = chkCobraCuota.Checked ? 1 : 0;
            ptarjeta.cupo = Convert.ToDecimal(txtCupoMaximo.Text.Trim());
            ptarjeta.max_tran = Convert.ToInt32(txtMaxTransacciones.Text.Trim());

            if (!string.IsNullOrWhiteSpace(ddlAsesor.SelectedValue))
            {
                ptarjeta.cod_asesor = Convert.ToInt64(ddlAsesor.SelectedValue);
            }

            if (Session["LSTCUENTA"] != null)
            {
                List<Tarjeta> lstConsulta = new List<Tarjeta>();

                lstConsulta = (List<Tarjeta>)Session["LSTCUENTA"];
                foreach (Tarjeta lItem in lstConsulta)
                {
                    ptarjeta.numero_cuenta = lItem.numero_cuenta;
                    ptarjeta = AsignacionTarjetaServicio.CrearAsignacion(ptarjeta, usuario);
                }
            }

            //  ptarjeta = AsignacionTarjetaServicio.CrearAsignacion(ptarjeta, usuario);

            idObjeto = ptarjeta.numtarjeta.ToString();

            string mensj = "";
            if (idObjeto != "")
            {
                //MODIFICAR
                mensj = "modificada";
            }
            else
            {
                // CREAR
                mensj = "grabada";
            }
            lblmsj.Text = mensj;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        Navegar(Pagina.Lista);
    }


    protected void gvControl_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }


    protected void gvListaAFiliados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListaAFiliados.PageIndex = e.NewPageIndex;
            Actualizar(0);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AsignacionTarjetaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvListaAFiliados_RowEditing(object sender, GridViewEditEventArgs e)
    {
        LblMensaje.Text = "";

        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarConsultar(false);
        String identificacion = gvListaAFiliados.Rows[e.NewEditIndex].Cells[3].Text;
        String codigo = gvListaAFiliados.Rows[e.NewEditIndex].Cells[1].Text;
        String nombres = gvListaAFiliados.Rows[e.NewEditIndex].Cells[6].Text + "" + gvListaAFiliados.Rows[e.NewEditIndex].Cells[7].Text + " " + gvListaAFiliados.Rows[e.NewEditIndex].Cells[8].Text + "" + gvListaAFiliados.Rows[e.NewEditIndex].Cells[9].Text;
        this.txtCodigoCliente.Text = codigo;
        this.txtIdentificacion.Text = identificacion;
        this.txtNombres.Text = nombres;
        MvPrincipal.ActiveViewIndex = 2;
        // MvAfiliados.ActiveViewIndex = 1;
        lblInfo.Visible = false;
        lblTotalRegs.Visible = false;
        pEncBusqueda.Visible = false;
        pEncBusqueda.Visible = false;
        pBusqueda.Visible = false;

    }

    protected void Alphabet_Click(object sender, EventArgs e)
    {
        LinkButton lnkAlphabet = (LinkButton)sender;
        ViewState["CurrentAlphabet"] = lnkAlphabet.Text;
        this.GenerateAlphabets();
        //gvLista.PageIndex = 0;
        Actualizar(0);
    }

    private void GenerateAlphabets()
    {
        List<ListItem> alphabets = new List<ListItem>();
        ListItem alphabet = new ListItem();
        alphabet.Value = "TODO";
        alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
        alphabets.Add(alphabet);
        for (int i = 65; i <= 90; i++)
        {
            alphabet = new ListItem();
            alphabet.Value = Char.ConvertFromUtf32(i);
            alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
            alphabets.Add(alphabet);
        }
        rptAlphabets.DataSource = alphabets;
        rptAlphabets.DataBind();
    }
    protected void txtNumeIdentificacion_TextChanged(object sender, EventArgs e)
    {

        if (txtNumeIdentificacion2.Text != "")
        {
            string sLetra = ViewState["CurrentAlphabet"].ToString();
            string sFiltro = "";
            if (sLetra != "TODO" && sLetra.Trim() != "")
            {
                sFiltro = " (primer_apellido Like '" + sLetra + "%' OR razon_social Like '" + sLetra + "%') ";
            }

        }
        else
        {
            txtNombres.Text = "";
        }
    }
    protected void txtNumeIdentificacion2_TextChanged(object sender, EventArgs e)
    {

    }
    protected void rptAlphabets_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
    protected void ddlTipoCuenta_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtCuenta_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlConvenio_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int64 convenio = 0;
        convenio = Convert.ToInt64(ddlConvenio.SelectedValue);
        this.ddlNumtarjeta.DataSource = AsignacionTarjetaServicio.Listartarjeta(convenio, entidad, (Usuario)Session["usuario"]);
        ddlNumtarjeta.DataTextField = "numtarjeta";
        ddlNumtarjeta.DataValueField = "idplastico";
        ddlNumtarjeta.DataBind();
        ddlNumtarjeta.Items.Insert(0, new ListItem("Selecione un item", "0"));
        ddlNumtarjeta.SelectedIndex = 0;


    }
    protected void ddlTipoCuenta_SelectedIndexChanged1(object sender, EventArgs e)
    {
        Int64 codtipocuenta = 0;
        codtipocuenta = Convert.ToInt64(ddlTipoCuenta.SelectedValue);
        Int64 cod_deudor = 0;
        cod_deudor = Convert.ToInt64(txtCodigoCliente.Text);
        if (codtipocuenta == 1)
        {
            this.ddlCuenta.DataSource = AsignacionTarjetaServicio.ListarAhorros(cod_deudor, entidad, (Usuario)Session["usuario"]);
            List<Tarjeta> lstConsulta = new List<Tarjeta>();

            lstConsulta = AsignacionTarjetaServicio.ListarAhorros(cod_deudor, entidad, (Usuario)Session["usuario"]);
            ddlCuenta.DataTextField = "numero_cuenta";
            // ddlCuenta.DataValueField = "numero_cuenta";
            ddlCuenta.DataBind();
            Session["LSTCUENTA"] = lstConsulta;
            if (ddlCuenta.SelectedValue == "")
            {
                VerError("Cliente no tiene cuentas");
                txtSaldoTotal.Text = "0";
                txtSaldoDisponible.Text = "0";
            }
            else
            {
                TxtFechaAsignacion.Text = Convert.ToString(DateTime.Now);
                VerError("");
                pvalores = AsignacionTarjetaServicio.ConsultarValoresAhorros(Convert.ToInt64(ddlCuenta.SelectedValue), (Usuario)Session["usuario"]);
                txtSaldoTotal.Text = pvalores.saldo_total.ToString();
                txtSaldoDisponible.Text = pvalores.saldo_disponible.ToString();
            }
        }
        if (codtipocuenta == 2)
        {
            List<Tarjeta> lstConsulta = new List<Tarjeta>();
            lstConsulta = AsignacionTarjetaServicio.ListarAhorros(cod_deudor, entidad, (Usuario)Session["usuario"]);
            this.ddlCuenta.DataSource = AsignacionTarjetaServicio.ListarCredito(cod_deudor, entidad, (Usuario)Session["usuario"]);
            ddlCuenta.DataTextField = "num_radicacion";
            ddlCuenta.DataValueField = "num_radicacion";
            ddlCuenta.DataBind();
            Session["LSTCUENTA"] = lstConsulta;
            if (ddlCuenta.SelectedValue == "")
            {
                VerError("Cliente no tiene creditos");
                txtSaldoTotal.Text = "0";
                txtSaldoDisponible.Text = "0";
            }
            else
            {
                VerError("");
                TxtFechaAsignacion.Text = Convert.ToString(DateTime.Now);
                pvalores = AsignacionTarjetaServicio.ConsultarValoresCredito(Convert.ToInt64(ddlCuenta.SelectedValue), (Usuario)Session["usuario"]);
                txtSaldoTotal.Text = pvalores.saldo_total.ToString();
                txtSaldoDisponible.Text = pvalores.saldo_disponible.ToString();
            }
        }

    }
    protected void ddlCuenta_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}