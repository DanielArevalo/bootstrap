using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.CDATS.Services;
using Xpinn.CDATS.Entities;
using Microsoft.Reporting.WebForms;
using Xpinn.FabricaCreditos.Services;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using Cantidad_a_Letra;
using System.Configuration;
using Xpinn.Ahorros.Services;
using Xpinn.Ahorros.Entities;
using System.Linq;
using System.Text;

partial class Nuevo : GlobalWeb
{
    AperturaCDATService AperturaService = new AperturaCDATService();
    AhorroVistaServices _ahorroService = new AhorroVistaServices();
    LineaCDATService LineaService = new LineaCDATService();
    NumeracionCuentas BONumeracionCuentaCDAT = new NumeracionCuentas();
    LiquidacionCDATService LiquiService = new LiquidacionCDATService();
    private Xpinn.CDATS.Services.BeneficiarioService BeneficiarioServicio = new Xpinn.CDATS.Services.BeneficiarioService();


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[AperturaService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(AperturaService.codigoprogramaFormatoImpresion, "E");
            else
                VisualizarOpciones(AperturaService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarImprimir(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                Session["DatosDetalle"] = null;
                Session["nroCDAT"] = null;
                mvPrincipal.Visible = true;
                mvReporte.Visible = false;
                mvPrincipal.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                cbInteresCuenta.Enabled = false;
                cargarDropdown();
                if (Session[AperturaService.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AperturaService.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AperturaService.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);


                    lblMsj.Text = " modificada ";
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarImprimir(true);
                    if (Session["ADMI"].ToString() == "REIMPRIMIR")
                    {
                        toolBar.MostrarGuardar(false);
                        btnDatos.Visible = false;
                        muestraInformeReporte();
                    }
                }
                else
                {
                    txtFechaApertura.Text = DateTime.Today.ToShortDateString();
                    lblMsj.Text = " grabada ";
                    Usuario vUsu = (Usuario)Session["usuario"];
                    ddlAsesor.SelectedValue = vUsu.codusuario.ToString();
                    ddlOficina.SelectedValue = vUsu.cod_oficina.ToString();

                    InicializarDetalle();
                    ddlModalidad_SelectedIndexChanged(ddlModalidad, null);

                    //VALIDA SI SE GENERA EL NUMERADO
                    Cdat Data = new Cdat();
                    Data = AperturaService.ConsultarNumeracionCDATS(Data, (Usuario)Session["usuario"]);

                    if (Data.valor == 1)
                    {
                        txtNumCDAT.Visible = false;
                        lblNumDV.Visible = true;
                    }
                    else
                    {
                        txtNumCDAT.Visible = true;
                        lblNumDV.Visible = false;
                    }
                }
                if (Session["solicitudProducto"] != null)
                {
                    cargarDatosSolicitud();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoPrograma, "Page_Load", ex);
        }
    }

    public void CargarCuentasAhorro(Int64 pCodPersona)
    {

        if (pCodPersona != 0)
        {
            AhorroVistaServices ahorroServices = new AhorroVistaServices();
            Usuario usuario = (Usuario)Session["usuario"];

            ddlnumAhorros.DataSource = ahorroServices.ListarCuentaAhorroVistaGiros(pCodPersona, usuario);
            ddlnumAhorros.DataTextField = "numero_cuenta";
            ddlnumAhorros.DataValueField = "numero_cuenta";
            ddlnumAhorros.DataBind();
        }
    }




    protected void cbSeleccionar_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCuenta.Checked)
        {
            ddlnumAhorros.Visible = true;
            long cod_persona = 0;
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                CheckBoxGrid chkPrincipal = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
                if (chkPrincipal != null)
                {
                    if (chkPrincipal.Checked)
                    {
                        TextBox txtCod_persona = (TextBox)rFila.FindControl("lblcod_persona");
                        if (txtCod_persona != null)
                            if (txtCod_persona.Text != "")
                                cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());


                        break;
                    }
                }
            }

            CargarCuentasAhorro(cod_persona);

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

    protected void cargarDropdown()
    {
        ctlTasaInteres.Inicializar();


        Cdat Data = new Cdat();
        List<Cdat> lstTipoLinea = new List<Cdat>();

        LineaCDAT LineaCDAT = new LineaCDAT();

        //CARGAR SOLO LINEAS ACTIVAS
        Data.estado = 1;
        lstTipoLinea = AperturaService.ListarTipoLineaCDAT(Data, (Usuario)Session["usuario"]);
        if (lstTipoLinea.Count > 0)
        {
            ddlTipoLinea.DataSource = lstTipoLinea;
            ddlTipoLinea.DataTextField = "NOMBRE";
            ddlTipoLinea.DataValueField = "COD_LINEACDAT";
            ddlTipoLinea.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un item", "0"));
            ddlTipoLinea.SelectedIndex = 0;
            ddlTipoLinea.DataBind();
        }

        ddlModalidad.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un Item", "0"));
        ddlModalidad.Items.Insert(1, new System.Web.UI.WebControls.ListItem("INDIVIDUAL", "IND"));
        ddlModalidad.Items.Insert(2, new System.Web.UI.WebControls.ListItem("CONJUNTA", "CON"));
        ddlModalidad.Items.Insert(3, new System.Web.UI.WebControls.ListItem("ALTERNA", "ALT"));
        ddlModalidad.SelectedIndex = 0;
        ddlModalidad.DataBind();

        PoblarLista("Formacaptacion_CDAT", ddlFormaCaptacion);
        ddlFormaCaptacion.SelectedIndex = 1;

        PoblarLista("Tipomoneda", ddlTipoMoneda);
        ddlTipoMoneda.SelectedIndex = 1;

        ddlTipoCalendario.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un Item", "0"));
        ddlTipoCalendario.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Comercial", "1"));
        ddlTipoCalendario.Items.Insert(2, new System.Web.UI.WebControls.ListItem("Calendario", "2"));
        ddlTipoCalendario.SelectedIndex = 0;
        ddlTipoCalendario.DataBind();

        PoblarLista("DESTINACION_CDAT", ddlDestinacion);

        List<Cdat> lstAsesores = new List<Cdat>();

        string ddlusuarios = "0";
        if (ddlusuarios == "0")
        {
            lstAsesores = AperturaService.ListartodosUsuarios(Data, (Usuario)Session["usuario"]);
            if (lstAsesores.Count > 0)
            {
                ddlAsesor.DataSource = lstAsesores;
                ddlAsesor.DataTextField = "nombre";
                ddlAsesor.DataValueField = "codusuario";
                ddlAsesor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un item", "-1"));
                ddlAsesor.SelectedIndex = 0;
                ddlAsesor.DataBind();
            }
            else
            {
                ddlusuarios = "2";
            }
        }
        if (ddlusuarios == "2")
        {
            Data.estado = 1;
            lstAsesores = AperturaService.ListarUsuariosAsesores(Data, (Usuario)Session["usuario"]);
            if (lstAsesores.Count > 0)
            {
                ddlAsesor.DataSource = lstAsesores;
                ddlAsesor.DataTextField = "nombre";
                ddlAsesor.DataValueField = "codusuario";
                ddlAsesor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un item", "0"));
                ddlAsesor.SelectedIndex = 0;
                ddlAsesor.DataBind();
            }
        }
        // Colocar por defecto el usuario
        if (Session["usuario"] != null)
        {
            try
            {
                if (Usuario.nombre != null && Usuario.nombre != "")
                    ddlAsesor.SelectedValue = Usuario.codusuario.ToString();
            }
            catch { }
        }

        List<Cdat> lstOficina = new List<Cdat>();
        lstOficina = AperturaService.ListarOficinas(Data, (Usuario)Session["usuario"]);
        if (lstOficina.Count > 0)
        {
            ddlOficina.DataSource = lstOficina;
            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "COD_OFICINA";
            ddlOficina.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un item", "0"));
            ddlOficina.SelectedIndex = 0;
            ddlOficina.DataBind();
        }

        PoblarLista("Periodicidad", ddlPeriodicidad);


    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        ObtenerListaDetalle();

        List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();

        if (Session["DatosDetalle"] != null)
        {
            lstDetalle = (List<Detalle_CDAT>)Session["DatosDetalle"];

            for (int i = 1; i <= 1; i++)
            {
                Detalle_CDAT eApert = new Detalle_CDAT();
                eApert.cod_usuario_cdat = -1;
                eApert.cod_persona = null;
                eApert.principal = null;
                eApert.conjuncion = "";
                lstDetalle.Add(eApert);
            }
            gvDetalle.PageIndex = gvDetalle.PageCount;
            gvDetalle.DataSource = lstDetalle;
            gvDetalle.DataBind();

            Session["DatosDetalle"] = lstDetalle;
        }
        else
        {
            InicializarDetalle();
        }
    }

    protected void InicializarDetalle()
    {
        List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();
        for (int i = gvDetalle.Rows.Count; i < 3; i++)
        {
            Detalle_CDAT eApert = new Detalle_CDAT();
            eApert.cod_usuario_cdat = -1;
            eApert.cod_persona = null;
            eApert.principal = null;
            eApert.conjuncion = "";
            lstDetalle.Add(eApert);
        }
        gvDetalle.DataSource = lstDetalle;
        gvDetalle.DataBind();
        Session["DatosDetalle"] = lstDetalle;
    }

    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlModalidad.SelectedItem.Text == "CONJUNTA")
        {
            for (int i = 0; i < gvDetalle.Rows.Count; i++)
            {
                gvDetalle.Columns[10].Visible = true;
            }
        }
        else
        {
            for (int i = 0; i < gvDetalle.Rows.Count; i++)
            {
                gvDetalle.Columns[10].Visible = false;
            }
        }
        btnAddRow.Visible = true;
        if (ddlModalidad.SelectedItem.Text == "INDIVIDUAL")
            btnAddRow.Visible = false;
    }

    //Eventos Grilla
    protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlConjuncion = (DropDownListGrid)e.Row.FindControl("ddlConjuncion");
            if (ddlConjuncion != null)
            {
                ddlConjuncion.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un item", "0"));
                ddlConjuncion.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Y", "Y"));
                ddlConjuncion.Items.Insert(2, new System.Web.UI.WebControls.ListItem("O", "O"));
            }

            Label lblConjuncion = (Label)e.Row.FindControl("lblConjuncion");
            if (lblConjuncion != null)
                ddlConjuncion.SelectedValue = lblConjuncion.Text;

            TextBoxGrid txtIdentificacion = (TextBoxGrid)e.Row.FindControl("txtIdentificacion");
            if (txtIdentificacion != null)
                txtIdentificacion.TextChanged += txtIdentificacion_TextChanged;

            BusquedaRapida ctlListadoPersona = (BusquedaRapida)e.Row.FindControl("ctlListadoPersona");
            if (ctlListadoPersona != null)
                ctlListadoPersona.eventotxtIdentificacion_TextChanged += txtIdentificacion_TextChanged;
        }
    }

    protected void gvDetalle_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvDetalle.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaDetalle();

        List<Detalle_CDAT> LstDeta;
        LstDeta = (List<Detalle_CDAT>)Session["DatosDetalle"];

        if (conseID > 0)
        {
            try
            {
                foreach (Detalle_CDAT Deta in LstDeta)
                {
                    if (Deta.cod_usuario_cdat == conseID)
                    {
                        AperturaService.EliminarTitularCdat(conseID, (Usuario)Session["usuario"]);
                        LstDeta.Remove(Deta);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstDeta.RemoveAt((gvDetalle.PageIndex * gvDetalle.PageSize) + e.RowIndex);
        }

        gvDetalle.DataSourceID = null;
        gvDetalle.DataBind();

        gvDetalle.DataSource = LstDeta;
        gvDetalle.DataBind();

        Session["DatosDetalle"] = LstDeta;
    }

    protected void btnListadoPersona_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPersona = (ButtonGrid)sender;
        if (btnListadoPersona != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPersona.CommandArgument);
            BusquedaRapida ctlListadoPer = (BusquedaRapida)gvDetalle.Rows[rowIndex].FindControl("ctlListadoPersona");
            ctlListadoPer.Motrar(true, "lblcod_persona", "txtIdentificacion", "", "lblNombre", "lblApellidos", "lblDireccion", "lbltelefono", "lblCiudad");
        }
    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtIdentificacion = (TextBoxGrid)sender;
        int rowIndex = Convert.ToInt32(txtIdentificacion.CommandArgument);

        TextBox lblcod_persona = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblcod_persona");
        TextBox lblNombre = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblNombre");
        TextBox lblApellidos = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblApellidos");
        TextBox lblCiudad = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblCiudad");
        TextBox lblDireccion = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblDireccion");
        TextBox lbltelefono = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lbltelefono");
        CheckBox chkPrincipal = (CheckBox)gvDetalle.Rows[rowIndex].FindControl("chkPrincipal");

        Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 DataPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
        DataPersona.identificacion = txtIdentificacion.Text;
        DataPersona = Persona1Servicio.ConsultaDatosPersona(Convert.ToString(txtIdentificacion.Text), (Usuario)Session["usuario"]);
        if (DataPersona.cod_persona != 0)
        {
            if (DataPersona.cod_persona != 0)
                lblcod_persona.Text = DataPersona.cod_persona.ToString();
            if (lblcod_persona.Text == "")
                chkPrincipal.Visible = false;
            else
                chkPrincipal.Visible = true;

            if (DataPersona.nombres != "" && DataPersona.nombres != null)
                lblNombre.Text = DataPersona.nombres;

            if (DataPersona.apellidos != "" && DataPersona.apellidos != null)
                lblApellidos.Text = DataPersona.apellidos;

            if (DataPersona.nomciudad_resid != "" && DataPersona.nomciudad_resid != null)
                lblCiudad.Text = DataPersona.nomciudad_resid;

            if (DataPersona.direccion != "" && DataPersona.direccion != null)
                lblDireccion.Text = DataPersona.direccion;

            if (DataPersona.telefono != "" && DataPersona.telefono != null)
                lbltelefono.Text = DataPersona.telefono;
        }
        else
        {
            lblcod_persona.Text = ""; lblNombre.Text = ""; lblApellidos.Text = "";
            lblCiudad.Text = ""; lblDireccion.Text = ""; lbltelefono.Text = "";
            chkPrincipal.Visible = false;
        }

    }

    protected void chkPrincipal_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkPrincipal = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkPrincipal.CommandArgument);
        string Cod_persona = ((TextBox)gvDetalle.Rows[rowIndex].FindControl("lblcod_persona")).Text;

        if (chkPrincipal != null)
        {
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                CheckBoxGrid check = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
                check.Checked = false;
                if (rFila.RowIndex == rowIndex)
                {
                    check.Checked = true;
                    //Para saber si es afiliado 
                    Cdat vpersona = new Cdat();
                    AperturaCDATService lineaCdatServicio = new AperturaCDATService();
                    vpersona = lineaCdatServicio.ConsultarAfiliacion(Convert.ToString(Cod_persona), (Usuario)Session["usuario"]);
                    String Estado = vpersona.estado_persona;
                    Int64 Persona = Convert.ToInt64(vpersona.cod_persona);
                    if (Persona == 0)
                    {
                        lblError.Visible = true;
                        lblError.Text = "La persona no se encuentra afiliada";
                    }
                    if (Persona > 0 && Estado != "A")
                    {
                        lblError.Visible = true;
                        lblError.Text = "La persona no es un afiliado activo";
                    }
                }
            }
        }
    }

    protected List<Detalle_CDAT> ObtenerListaDetalle()
    {
        List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();
        List<Detalle_CDAT> lista = new List<Detalle_CDAT>();

        foreach (GridViewRow rfila in gvDetalle.Rows)
        {
            Detalle_CDAT eDeta = new Detalle_CDAT();

            Label lblcodigo = (Label)rfila.FindControl("lblcodigo");
            if (lblcodigo != null)
                eDeta.cod_usuario_cdat = Convert.ToInt64(lblcodigo.Text);

            TextBoxGrid txtIdentificacion = (TextBoxGrid)rfila.FindControl("txtIdentificacion");
            if (txtIdentificacion != null)
                eDeta.identificacion = txtIdentificacion.Text;

            TextBox lblcod_persona = (TextBox)rfila.FindControl("lblcod_persona");
            if (lblcod_persona.Text != "")
                eDeta.cod_persona = Convert.ToInt64(lblcod_persona.Text);

            TextBox lblNombre = (TextBox)rfila.FindControl("lblNombre");
            if (lblNombre.Text != "")
                eDeta.nombres = lblNombre.Text;

            TextBox lblApellidos = (TextBox)rfila.FindControl("lblApellidos");
            if (lblApellidos.Text != "")
                eDeta.apellidos = lblApellidos.Text;

            TextBox lblCiudad = (TextBox)rfila.FindControl("lblCiudad");
            if (lblCiudad.Text != "")
                eDeta.ciudad = lblCiudad.Text;

            TextBox lblDireccion = (TextBox)rfila.FindControl("lblDireccion");
            if (lblDireccion.Text != "")
                eDeta.direccion = lblDireccion.Text;

            TextBox lbltelefono = (TextBox)rfila.FindControl("lbltelefono");
            if (lbltelefono.Text != "")
                eDeta.telefono = lbltelefono.Text;

            CheckBoxGrid chkPrincipal = (CheckBoxGrid)rfila.FindControl("chkPrincipal");
            if (chkPrincipal.Checked)
                eDeta.principal = 1;
            else
                eDeta.principal = 0;

            if (ddlModalidad.SelectedItem.Text == "CONJUNTA")
            {
                DropDownListGrid ddlConjuncion = (DropDownListGrid)rfila.FindControl("ddlConjuncion");
                if (ddlConjuncion.SelectedIndex != 0)
                    eDeta.conjuncion = ddlConjuncion.SelectedValue;
                else
                    eDeta.conjuncion = null;
            }
            else
                eDeta.conjuncion = null;

            lista.Add(eDeta);
            Session["DatosDetalle"] = lista;

            if (eDeta.cod_persona != 0 && eDeta.cod_persona != null)
            {
                lstDetalle.Add(eDeta);
                Session["DTAPERTURA"] = lstDetalle; // CAPTURA DATOS PARA IMPRESION
            }
        }

        return lstDetalle;
    }

    protected void ddlPeriodicidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cdat Data = new Cdat();

        Data = AperturaService.ConsultarDiasPeriodicidad(Convert.ToInt32(ddlPeriodicidad.SelectedValue), (Usuario)Session["usuario"]);

        if (Data.numdias != 0)
            txtDiasValida.Text = Data.numdias.ToString();
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Cdat vApe = new Cdat();

            vApe = AperturaService.ConsultarApertu(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            Session["Inf"] = vApe;

            if (vApe.codigo_cdat != 0) txtCodigo.Text = vApe.codigo_cdat.ToString();

            if (vApe.numero_cdat != "" && vApe.numero_cdat != null) txtNumCDAT.Text = vApe.numero_cdat;

            Session["nroCDAT"] = vApe.numero_cdat;

            if (vApe.numero_fisico != "") txtNumPreImpreso.Text = vApe.numero_fisico;

            if (vApe.cod_oficina != 0) ddlOficina.SelectedValue = vApe.cod_oficina.ToString();

            if (vApe.cod_lineacdat != "") ddlTipoLinea.SelectedValue = vApe.cod_lineacdat;

            if (vApe.cod_destinacion != 0) ddlDestinacion.SelectedValue = vApe.cod_destinacion.ToString();

            if (vApe.fecha_apertura != DateTime.MinValue) txtFechaApertura.Text = vApe.fecha_apertura.ToShortDateString();

            if (vApe.modalidad != "" && vApe.modalidad != null) ddlModalidad.SelectedValue = vApe.modalidad;
            ddlModalidad_SelectedIndexChanged(ddlModalidad, null);


            if (vApe.codforma_captacion != 0) ddlFormaCaptacion.SelectedValue = vApe.codforma_captacion.ToString();
            lblplazo.Visible = true;
            txtPlazo.Visible = true;
            if (vApe.plazo != 0) txtPlazo.Text = vApe.plazo.ToString();
            lbltipocalendario.Visible = true;
            ddlTipoCalendario.Visible = true;
            if (vApe.tipo_calendario != 0) ddlTipoCalendario.SelectedValue = vApe.tipo_calendario.ToString();

            if (vApe.valor != 0) txtValor.Text = vApe.valor.ToString();

            if (vApe.cod_moneda != 0) ddlTipoMoneda.SelectedValue = vApe.cod_moneda.ToString();


            if (vApe.fecha_vencimiento != DateTime.MinValue) txtfechaVenci.Text = vApe.fecha_vencimiento.ToShortDateString();

            if (vApe.cod_asesor_com != 0) ddlAsesor.SelectedValue = vApe.cod_asesor_com.ToString();


            if (vApe.cod_periodicidad_int != 0 && vApe.cod_periodicidad_int != null)
            {
                ddlPeriodicidad.SelectedValue = vApe.cod_periodicidad_int.ToString();
                ddlPeriodicidad_SelectedIndexChanged(ddlPeriodicidad, null);
            }
            if (vApe.modalidad_int != 0)
                rblModalidadInt.SelectedValue = vApe.modalidad_int.ToString();

            if (vApe.capitalizar_int != 0)
                chkCapitalizaInt.Checked = true;
            else
                chkCapitalizaInt.Checked = false;

            if (vApe.cobra_retencion != 0)
                chkCobraReten.Checked = true;
            else
                chkCobraReten.Checked = false;
            if (vApe.fecha_intereses != DateTime.MinValue)
                txtfechaInteres.Text = vApe.fecha_intereses.ToShortDateString();

            /*Datos que no recupero
             * Tasa nominal,TASA_EFECTIVA,INTERESES_CAP,RETENCION_CAP,FECHA_INTERESES,ESTADO
             */

            chkDesmaterial.Checked = vApe.desmaterializado != 0 ? true : false;

            //Generar Consulta de la Linea Seleccionada
            LineaCDAT vLineaCdat = new LineaCDAT();
            LineaCDATService linahorroServicio = new LineaCDATService();
            vLineaCdat = linahorroServicio.ConsultarLineaCDAT(Convert.ToString(vApe.cod_lineacdat), (Usuario)Session["usuario"]);
            Session["RangoCDAT"] = vLineaCdat.lstRangos;
            if (vLineaCdat.interes_por_cuenta == 0)
            {
                cbInteresCuenta.Enabled = false;
                panelTasa.Enabled = false;
            }
            if (vLineaCdat.interes_por_cuenta == 1)
            {
                panelTasa.Enabled = true;
                cbInteresCuenta.Checked = true;
            }
            if (vApe.tipo_interes != null)
            {
                if (!string.IsNullOrEmpty(vApe.tipo_interes.ToString()))
                    ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vApe.tipo_interes.ToString().Trim());
                if (!string.IsNullOrEmpty(vApe.tipo_historico.ToString()))
                    ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vApe.tipo_historico.ToString().Trim()));
                if (!string.IsNullOrEmpty(vApe.desviacion.ToString()))
                    ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vApe.desviacion.ToString().Trim()));
                if (!string.IsNullOrEmpty(vApe.cod_tipo_tasa.ToString()))
                    ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vApe.cod_tipo_tasa.ToString().Trim()));
                if (!string.IsNullOrEmpty(vApe.tasa_interes.ToString()))
                    ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vApe.tasa_interes.ToString().Trim()));
            }

            //RECUPERAR GRILLA DETALLE 
            List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();

            lstDetalle = AperturaService.ListarDetalleTitulares(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (lstDetalle.Count > 0)
            {
                gvDetalle.DataSource = lstDetalle;
                gvDetalle.DataBind();
            }
            else
            {
                InicializarDetalle();
            }

            //RECUPERAR LA GRIDVIEW DE BENEFICIARIOS
            List<Beneficiario> lstBeneficiarios = new List<Beneficiario>();
            Xpinn.CDATS.Services.BeneficiarioService BenefiService = new Xpinn.CDATS.Services.BeneficiarioService();
            lstBeneficiarios = BenefiService.ConsultarBeneficiarioCdat(vApe.numero_cdat, (Usuario)Session["usuario"]);

            if (lstBeneficiarios.Count > 0)
            {
                if ((lstBeneficiarios != null) || (lstBeneficiarios.Count != 0))
                {
                    gvBeneficiarios.DataSource = lstBeneficiarios;
                    gvBeneficiarios.DataBind();
                }
                Session["DatosBene"] = lstBeneficiarios;
                chkBeneficiario.Checked = true;
                upBeneficiarios.Visible = true;
            }


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected Boolean ValidarDatos()
    {

        if (ddlTipoLinea.SelectedValue == "0")
        {
            VerError("Ingrese el Tipo/Linea de CDAT");
            return false;
        }
        if (txtNumCDAT.Visible == true)
        {
            if (txtNumCDAT.Text == "")
            {
                VerError("Ingrese el numero de CDAT");
                return false;
            }
        }

        if (txtFechaApertura.Text == "")
        {
            VerError("Ingrese la Fecha de Apertura");
            return false;
        }
        if (txtfechaVenci.Text == "")
        {
            VerError("Ingrese la Fecha de Vencimiento");
            return false;
        }
        if (ddlTipoLinea.SelectedItem == null)
        {
            VerError("No existen lineas para CDATS, Verifique los datos.");
            return false;
        }
        if (ddlModalidad.SelectedIndex == 0)
        {
            VerError("Seleccione la Modalidad");
            return false;
        }
        if (ddlModalidad.SelectedValue == "CON")
        {
            int ContTol = 0;
            string pIdentificacion = string.Empty;
            Int64 pCodpersona = 0;
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                TextBox txtCod_persona = (TextBox)rFila.FindControl("lblcod_persona");
                if (txtCod_persona != null)
                    if (txtCod_persona.Text != "")
                        pCodpersona = Convert.ToInt64(txtCod_persona.Text.Trim());

                TextBoxGrid txtIdentificacion = (TextBoxGrid)rFila.FindControl("txtIdentificacion");
                if (txtIdentificacion != null)
                    if (txtIdentificacion.Text != "")
                        pIdentificacion = txtIdentificacion.Text.Trim();
                if (pCodpersona != 0 && pIdentificacion != "")
                {
                    ContTol += 1;
                }
                pIdentificacion = "";
                pCodpersona = 0;
            }
            if (ContTol <= 1)
            {
                VerError("Debe tener mas de un titular para la Modalidad CONJUNTA");
                return false;
            }
        }

        if (txtValor.Text == "0")
        {
            VerError("Ingrese el Valor");
            return false;
        }
        if (ddlTipoMoneda.SelectedIndex == 0)
        {
            VerError("Seleccione el Tipo de Moneda");
            return false;
        }
        if (txtPlazo.Text == "")
        {
            VerError("Ingrese el Plazo correspondiente");
            return false;
        }
        if (ddlOficina.SelectedIndex == 0)
        {
            VerError("Seleccione la Oficina perteneciente al Asesor");
            return false;
        }
        if (rblModalidadInt.SelectedValue == "0")
        {
            VerError("Seleccione la modalidad intereses ");
            return false;
        }

        DateTime fechavencimiento = DateTime.Now;

        Int32 tipocalendario = ddlTipoCalendario.SelectedValue == "" ? 1 : Convert.ToInt32(ddlTipoCalendario.SelectedValue);
        DateTime fecha_proximo_pago = Convert.ToDateTime(txtFechaApertura.Text.ToString());

        if (!ValidarPlazosMaximosYMinimos())
        {
            return false;
        }

        List<Detalle_CDAT> LstDetalle = new List<Detalle_CDAT>();
        LstDetalle = ObtenerListaDetalle();

        if (LstDetalle.Count == 0)
        {
            VerError("Debe Ingresar un Titular Principal");
            return false;
        }
        int cont = 0;
        if (ddlModalidad.SelectedItem.Text == "INDIVIDUAL")
        {
            if (LstDetalle.Count > 1)
            {
                VerError("Solo debe ingresar un Titular para la Modalidad INDIVIDUAL");
                return false;
            }

            foreach (Detalle_CDAT deta in LstDetalle)
            {
                if (deta.principal == 1)
                {
                    cont++;
                }
            }
            if (cont != 1)
            {
                VerError("Debe selecciona un titular principal");
                return false;
            }
        }
        else
        {
            if (LstDetalle.Count > 0)
            {
                foreach (Detalle_CDAT deta in LstDetalle)
                {
                    if (deta.principal == 1)
                    {
                        cont++;
                    }
                }
                if (cont != 1)
                {
                    VerError("Debe selecciona un titular principal");
                    return false;
                }
            }
        }

        LineaCDAT linea = LineaService.ConsultarLineaCDAT(ddlTipoLinea.SelectedValue, Usuario);
        List<RangoCDAT> LstTopes = linea.lstRangos;
        Int64 valormin, valormax;
        decimal monto = Convert.ToDecimal(txtValor.Text);
        /// para montos
        try
        {
            foreach (RangoCDAT topesmonto in LstTopes)
            {
                //para plazos 
                if (Convert.ToInt64(topesmonto.tipo_tope) == 1)
                {
                    Session["ValorMin"] = topesmonto.minimo;
                    Session["ValorMax"] = topesmonto.maximo;

                    break;
                }
            }
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        valormin = Convert.ToInt64(Session["ValorMin"]);
        valormax = Convert.ToInt64(Session["ValorMax"]);
        if (monto < valormin)
        {
            VerError("El Monto debe ser minimo " + Session["ValorMin"]);
            return false;
        }
        if (monto > valormax)
        {
            VerError("El Monto no puede  ser superior a " + Session["ValorMax"]);
            return false;
        }


        if (chkCuenta.Checked)
        {
            if (ddlnumAhorros.Text == "")
            {

                VerError("Por favor seleccione Cuenta de Ahorros");
                return false;
            }
        }

        return true;
    }

    bool ValidarPlazosMaximosYMinimos()
    {
        if (!string.IsNullOrWhiteSpace(ddlTipoLinea.SelectedValue))
        {
            LineaCDATService lineaService = new LineaCDATService();
            RangoCDAT rangoABuscar = new RangoCDAT
            {
                cod_lineacdat = ddlTipoLinea.SelectedValue,
                tipo_tope = 2 // dias
            };

            RangoCDAT rangoBuscado = lineaService.ConsultarRangoCDATPorLineaYTipoTope(rangoABuscar, Usuario);

            Int32 plazoSolicitado = Convert.ToInt32(txtPlazo.Text);

            if (!string.IsNullOrWhiteSpace(rangoBuscado.minimo) && !string.IsNullOrWhiteSpace(rangoBuscado.maximo))
            {
                int plazoMaximo = Convert.ToInt32(rangoBuscado.maximo);
                int plazoMinimo = Convert.ToInt32(rangoBuscado.minimo);

                if (plazoSolicitado > plazoMaximo || plazoSolicitado < plazoMinimo)
                {
                    VerError("El plazo no puede superar de " + plazoMaximo + " días y no puede ser menor de " + plazoMinimo + " dias ");
                    RegistrarPostBack();
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarDatos())
            {
                string msj;
                msj = idObjeto != "" ? "Modificar" : "Grabar";
                ctlMensaje.MostrarMensaje("Desea " + msj + " los datos ingresados?");
            }
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(AperturaService.CodigoPrograma, "btnGuardar_Click", ex);
            VerError(ex.Message);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Int16 numero_preimpreso = 0;
        try
        {
            Usuario pUsu = (Usuario)Session["usuario"];
            //OBTENER LOS DATOS DEL TITULAR PRINCIPAL
            string pIdentificacion = string.Empty;
            Int64 pCodpersona = 0;
            if (this.validarreglasgrabar())
            {
                foreach (GridViewRow rFila in gvDetalle.Rows)
                {
                    CheckBoxGrid chkPrincipal = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
                    if (chkPrincipal != null)
                    {
                        if (chkPrincipal.Checked)
                        {
                            TextBox txtCod_persona = (TextBox)rFila.FindControl("lblcod_persona");
                            if (txtCod_persona != null)
                                if (txtCod_persona.Text != "")
                                    pCodpersona = Convert.ToInt64(txtCod_persona.Text.Trim());

                            TextBoxGrid txtIdentificacion = (TextBoxGrid)rFila.FindControl("txtIdentificacion");
                            if (txtIdentificacion != null)
                                if (txtIdentificacion.Text != "")
                                    pIdentificacion = txtIdentificacion.Text.Trim();
                            break;
                        }
                    }
                }

                Cdat vApert = new Cdat();

                if (idObjeto != "")
                    vApert.codigo_cdat = Convert.ToInt64(txtCodigo.Text);
                else
                    vApert.codigo_cdat = 0;

                //CONSULTA DE GENERACION NUMERICA CDAT
                Cdat Data = new Cdat();
                Data = AperturaService.ConsultarNumeracionCDATS(Data, (Usuario)Session["usuario"]);

                if (idObjeto == "")
                {
                    if (Data.valor == 1)
                    {
                        vApert.opcion = 1;
                        string pError = "";
                        string autogenerado = BONumeracionCuentaCDAT.ObtenerCodigoParametrizado(3, pIdentificacion, pCodpersona, ddlTipoLinea.SelectedValue, ref pError, pUsu);
                        if (pError != "")
                        {
                            VerError(pError);
                            return;
                        }
                        if (autogenerado == "ErrorGeneracion")
                        {
                            VerError("Se generó un error al construir el consecutivo CDAT");
                            return;
                        }
                        vApert.numero_cdat = autogenerado;
                    }
                    else
                    {
                        vApert.opcion = 0;//NO AUTOGENERE
                        vApert.numero_cdat = txtNumCDAT.Text;
                    }
                }
                else
                {
                    vApert.opcion = 0;
                    vApert.numero_cdat = txtNumCDAT.Text;
                }

                if (txtNumPreImpreso.Text != "")
                    vApert.numero_fisico = txtNumPreImpreso.Text;
                else
                    vApert.numero_fisico = null;
                vApert.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);
                if (ddlTipoLinea.SelectedIndex != 0)
                    vApert.cod_lineacdat = ddlTipoLinea.SelectedValue;
                else
                    vApert.cod_lineacdat = null;
                if (ddlDestinacion.SelectedIndex != 0)
                    vApert.cod_destinacion = Convert.ToInt64(ddlDestinacion.SelectedValue);
                else
                    vApert.cod_destinacion = 0;
                vApert.fecha_apertura = Convert.ToDateTime(txtFechaApertura.Text);
                vApert.modalidad = ddlModalidad.SelectedValue;
                if (ddlFormaCaptacion.SelectedIndex != 0)
                    vApert.codforma_captacion = Convert.ToInt32(ddlFormaCaptacion.SelectedValue);
                else
                    vApert.codforma_captacion = 0;
                vApert.plazo = Convert.ToInt32(txtPlazo.Text);
                vApert.tipo_calendario = Convert.ToInt32(ddlTipoCalendario.SelectedValue);
                vApert.valor = Convert.ToDecimal(txtValor.Text);
                vApert.cod_moneda = Convert.ToInt32(ddlTipoMoneda.SelectedValue);
                // vApert.fecha_inicio = Convert.ToDateTime(txtFechaEmi.Text);

                vApert.fecha_vencimiento = Convert.ToDateTime(txtfechaVenci.Text);
                vApert.cod_asesor_com = Convert.ToInt32(ddlAsesor.SelectedValue);

                //  vApert.tipo_interes = ctlTasaInteres.FormaTasa;
                vApert.tipo_interes = ctlTasaInteres.FormaTasa;
                if (ctlTasaInteres.Indice == 0)//NIGUNA
                {
                    vApert.tipo_historico = 0;
                    vApert.desviacion = 0;
                    vApert.tasa_interes = 0;
                    vApert.cod_tipo_tasa = 0;
                }
                else if (ctlTasaInteres.Indice == 1)//FIJO
                {
                    vApert.tipo_historico = 0;
                    vApert.desviacion = 0;
                    if (ctlTasaInteres.Tasa != 0)
                        vApert.tasa_interes = ctlTasaInteres.Tasa;
                    vApert.cod_tipo_tasa = ctlTasaInteres.TipoTasa;
                }
                else // HISTORICO
                {
                    vApert.cod_tipo_tasa = 0;
                    vApert.tipo_historico = ctlTasaInteres.TipoHistorico;
                    if (ctlTasaInteres.Desviacion != 0)
                        vApert.desviacion = ctlTasaInteres.Desviacion;
                }

                if (ddlPeriodicidad.SelectedIndex != 0)
                    vApert.cod_periodicidad_int = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
                else
                    vApert.cod_periodicidad_int = 0;

                if (rblModalidadInt.SelectedItem != null)
                    vApert.modalidad_int = Convert.ToInt32(rblModalidadInt.SelectedValue);
                else
                    vApert.modalidad_int = 0;

                vApert.capitalizar_int = chkCapitalizaInt.Checked ? 1 : 0;


                if (chkCapitalizaInt.Checked)
                {

                    if (txtFechaEmi.Text == "")
                    {
                        VerError("Ingrese la Fecha de Emision");
                        return;
                    }
                    else
                    {
                        if (Convert.ToDateTime(txtFechaEmi.Text) < Convert.ToDateTime(txtFechaApertura.Text))
                        {
                            VerError("Ingrese una Fecha mayor a la Fecha de Apertura");
                            return;
                        }

                        vApert.fecha_inicio = Convert.ToDateTime(txtFechaEmi.Text);

                    }
                }
                vApert.fecha_inicio = Convert.ToDateTime(this.txtFechaApertura.Text);

                vApert.cobra_retencion = chkCobraReten.Checked ? 1 : 0;

                vApert.fecha_intereses = string.IsNullOrEmpty(txtfechaInteres.Text.Trim()) ? DateTime.MinValue : Convert.ToDateTime(txtfechaInteres.Text);

                //VALORES NULOS
                vApert.tasa_nominal = 0;
                vApert.tasa_efectiva = 0;
                vApert.intereses_cap = 0;
                vApert.retencion_cap = 0;

                vApert.estado = 1; //por defecto
                vApert.desmaterializado = chkDesmaterial.Checked ? 1 : 0;

                vApert.lstDetalle = new List<Detalle_CDAT>();
                vApert.lstDetalle = ObtenerListaDetalle();


                List<Beneficiario> lstBeneficiariosCdat = new List<Beneficiario>();



                if (chkBeneficiario.Checked)
                {
                    lstBeneficiariosCdat = ObtenerListaBeneficiariosCdats();
                }


                vApert.forma_pago = (chkCuenta.Checked) ? 1 : 0;
                vApert.numero_cuenta = ddlnumAhorros.Text;


                Xpinn.CDATS.Entities.LineaCDAT vLineaCDAT = new Xpinn.CDATS.Entities.LineaCDAT();
                numero_preimpreso = 0;
                vLineaCDAT = LineaService.ConsultarLineaCDAT(Convert.ToString(ddlTipoLinea.SelectedValue), (Usuario)Session["usuario"]);

                numero_preimpreso = Convert.ToInt16(vLineaCDAT.numero_pre_impreso);
                if (numero_preimpreso == 1)
                {
                    txtNumPreImpreso.Text = vApert.numero_cdat;
                    txtNumPreImpreso.Enabled = true;

                }

                if (numero_preimpreso == 1)
                {
                    if (txtNumPreImpreso.Text == "")
                    {
                        VerError("Ingrese el numero preimpreso");

                    }
                }
                if (txtNumPreImpreso.Text != "")
                {
                    //consultar cierre historico
                    String estado = "";
                    DateTime fechacierrehistorico;
                    DateTime fechaliquidacion = Convert.ToDateTime(txtFechaApertura.Text);
                    Xpinn.CDATS.Entities.LiquidacionCDAT vliquidacioncdat = new Xpinn.CDATS.Entities.LiquidacionCDAT();
                    vliquidacioncdat = LiquiService.ConsultarCierreCdats((Usuario)Session["usuario"]);
                    estado = vliquidacioncdat.estadocierre;
                    fechacierrehistorico = Convert.ToDateTime(vliquidacioncdat.fecha_cierre.ToString());

                    if (estado == "D" && fechaliquidacion <= fechacierrehistorico)
                    {
                        VerError("NO PUEDE INGRESAR APERTURAS EN PERIODOS YA CERRADOS, TIPO M,'CDAT'S'");
                    }
                    else
                    {

                        if (idObjeto != "")
                        {
                            AperturaService.ModificarAperturaCDAT(vApert, (Usuario)Session["usuario"], lstBeneficiariosCdat);
                        }
                        else
                        {
                            vApert.fecha_intereses = vApert.fecha_apertura;

                            vApert.origen = 0;
                            vApert.cdat_renovado = "0";
                            vApert.cod_persona = pCodpersona;
                            vApert.capitalizar_int_old = 0;
                            vApert.valor_capitalizar = 0;

                            Xpinn.CDATS.Entities.LineaCDAT vLineaCDAT1 = new Xpinn.CDATS.Entities.LineaCDAT();

                            vLineaCDAT1 = LineaService.ConsultarLineaCDAT(Convert.ToString(ddlTipoLinea.SelectedValue), (Usuario)Session["usuario"]);
                            numero_preimpreso = Convert.ToInt16(vLineaCDAT1.numero_pre_impreso);
                            if (numero_preimpreso == 1)
                            {
                                txtNumPreImpreso.Text = vApert.numero_cdat;
                                txtNumPreImpreso.Enabled = true;

                            }


                            AperturaService.CrearAperturaCDAT(vApert, (Usuario)Session["usuario"], lstBeneficiariosCdat);
                            idObjeto = vApert.codigo_cdat.ToString();
                        }

                        Session["nroCDAT"] = vApert.numero_cdat.ToString();

                        //GRABAR AUDITORIA

                        CDAT_AUDITORIA Audi = new CDAT_AUDITORIA();
                        Usuario vUsu = (Usuario)Session["usuario"];

                        Audi.cod_auditoria_cdat = 0;
                        Audi.codigo_cdat = vApert.codigo_cdat;
                        Audi.tipo_registro_aud = 1;
                        Audi.fecha_aud = DateTime.Now;
                        Audi.cod_usuario_aud = vUsu.codusuario;
                        Audi.ip_aud = vUsu.IP;

                        if (idObjeto == "")
                        {
                            AperturaService.CrearAuditoriaCdat(Audi, (Usuario)Session["usuario"]);//Crear
                            actualizarSolicitud(pCodpersona);
                        }

                        Site toolBar = (Site)Master;
                        toolBar.MostrarGuardar(false);
                        toolBar.MostrarImprimir(false);

                        lblgenerado.Text = vApert.numero_cdat;
                        mvPrincipal.ActiveViewIndex = 1;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(AperturaService.CodigoPrograma, "btnContinuarMen_Click", ex);
            VerError(ex.Message);
        }
    }

    protected List<Beneficiario> ObtenerListaBeneficiariosCdats()
    {
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

            lista.Add(eBenef);
            Session["DatosBene"] = lista;

            if (eBenef.identificacion_ben.Trim() != "" && eBenef.nombre_ben.Trim() != null)
            {
                lstBeneficiarios.Add(eBenef);
            }
        }
        return lstBeneficiarios;
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        if (Session["solicitudProducto"] != null)
        {
            Session["solicitudProducto"] = null;
            Response.Redirect("../../Aportes/ConfirmarProductoAprobado/Lista.aspx", false);
        }
        else
        {
            Navegar(Pagina.Lista);
        }
    }

    protected void btnRegresarAdministracion_Click(object sender, EventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 0;
    }

    //IMPRIMIR
    void muestraInformeReporte()
    {
        VerError("");

        // Solicitando la información que debe ser mostrada en el documento
        DatosDeDocumentoService datosDeDocumentoServicio = new DatosDeDocumentoService();
        List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> lstDatosDeDocumento = new List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento>();

        TiposDocumentoService tipoDocumentoServicio = new TiposDocumentoService();
        Xpinn.FabricaCreditos.Entities.Documento document = new Xpinn.FabricaCreditos.Entities.Documento();


        string cRutaLocalDeArchivoPDF = Server.MapPath("~/Page/CDATS/Documentos/CDAT_" + txtNumCDAT.Text.Trim() + ".pdf");
        Xpinn.FabricaCreditos.Entities.TiposDocumento tipoDOC = new Xpinn.FabricaCreditos.Entities.TiposDocumento();
        lstDatosDeDocumento = datosDeDocumentoServicio.ListarDatosDeDocumentoCDAT(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
        List<Xpinn.FabricaCreditos.Entities.TiposDocumento> lsDocumentos = tipoDocumentoServicio.ConsultarTiposDocumento("C", (Usuario)Session["usuario"]);
        if (lsDocumentos.Count > 0)
        {
            tipoDOC = lsDocumentos[0];
        }
        if (lsDocumentos.Count > 0)
        {
            if (tipoDOC.Textos != null)
                ReemplazarEnDocumentoDeWordYGuardarPDF(Encoding.ASCII.GetString(tipoDOC.Textos), lstDatosDeDocumento, cRutaLocalDeArchivoPDF);
            else
                ReemplazarEnDocumentoDeWordYGuardarPDF(tipoDOC.texto, lstDatosDeDocumento, cRutaLocalDeArchivoPDF);

            //Para Descargar el archivo

            Boolean bExiste = System.IO.File.Exists(cRutaLocalDeArchivoPDF);
            if (bExiste)
            {
                Session["Generado"] = true;
                ObtenerDatos(txtNumCDAT.Text);
                Response.Write("<script>window.__doPostBack('','');</script>");

                VerError("");

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = false;
                Response.AddHeader("Content-Disposition", "attachment;filename=" + txtNumCDAT.Text.Trim() + ".pdf");
                Response.ContentType = "application/octet-stream";
                Response.Flush();
                Response.WriteFile(cRutaLocalDeArchivoPDF);
                File.Delete(cRutaLocalDeArchivoPDF);
                //Response.End(); -- Si se decomentarea da un excepcion                        
            }
            else
            {
                VerError("No se pudo generar el reporte");
                return;
            }



        }
        else
        {
            ObtenerListaDetalle();
            if (Session["DTAPERTURA"] == null)
            {
                VerError("No ha generado el Reporte para poder imprimir información");
                return;
            }
            else
            {
                mvPrincipal.Visible = false;
                mvReporte.Visible = true;
                mvReporte.ActiveViewIndex = 0;

                List<Detalle_CDAT> lstConsulta = new List<Detalle_CDAT>();
                lstConsulta = (List<Detalle_CDAT>)Session["DTAPERTURA"];

                // LLenar data table con los datos a recoger
                System.Data.DataTable table = new System.Data.DataTable();
                table.Columns.Add("identificacion");
                table.Columns.Add("cod_persona");
                table.Columns.Add("nombres");
                table.Columns.Add("apellidos");
                table.Columns.Add("ciudad");
                table.Columns.Add("direccion");
                table.Columns.Add("telefono");
                table.Columns.Add("principal");

                foreach (Detalle_CDAT item in lstConsulta)
                {
                    DataRow datarw;
                    datarw = table.NewRow();
                    datarw[0] = item.identificacion;
                    datarw[1] = item.cod_persona;
                    datarw[2] = item.nombres;
                    datarw[3] = item.apellidos;
                    datarw[4] = item.ciudad;
                    datarw[5] = item.direccion;
                    datarw[6] = item.telefono;
                    if (item.principal == 1)
                        datarw[7] = "*";
                    else
                        datarw[7] = null;
                    table.Rows.Add(datarw);
                }
                // ---------------------------------------------------------------------------------------------------------
                // Pasar datos al reporte
                // ---------------------------------------------------------------------------------------------------------

                Usuario pUsuario = new Usuario();
                pUsuario = (Usuario)Session["Usuario"];

                ReportParameter[] param = new ReportParameter[17];
                param[0] = new ReportParameter("entidad", pUsuario.empresa);
                param[1] = new ReportParameter("nit", pUsuario.nitempresa);
                param[2] = new ReportParameter("Oficina", pUsuario.nombre_oficina);
                param[3] = new ReportParameter("Usuario", pUsuario.nombre);
                param[4] = new ReportParameter("NroCdat", Session["nroCDAT"].ToString());
                param[5] = new ReportParameter("fecha_aper", txtFechaApertura.Text);
                param[6] = new ReportParameter("tipoLinea", ddlTipoLinea.SelectedItem.Text);
                param[7] = new ReportParameter("modalidad", ddlModalidad.SelectedItem.Text);
                param[8] = new ReportParameter("Valor", txtValor.Text);
                param[9] = new ReportParameter("Moneda", ddlTipoMoneda.SelectedItem.Text);
                param[10] = new ReportParameter("Plazo", txtPlazo.Text);
                param[11] = new ReportParameter("TipoCalendario", ddlTipoCalendario.SelectedItem.Text);
                param[12] = new ReportParameter("Destinacion", ddlDestinacion.SelectedItem.Text);
                param[13] = new ReportParameter("Asesor", ddlAsesor.SelectedItem.Text);
                param[14] = new ReportParameter("OficinaAsesor", ddlOficina.SelectedItem.Text);
                param[15] = new ReportParameter("NumPreImpreso", txtNumPreImpreso.Text);
                param[16] = new ReportParameter("ImagenReport", ImagenReporte());

                rvReporte.LocalReport.EnableExternalImages = true;
                rvReporte.LocalReport.SetParameters(param);

                ReportDataSource rds = new ReportDataSource("DataSet1", table);
                rvReporte.LocalReport.DataSources.Clear();
                rvReporte.LocalReport.DataSources.Add(rds);
                rvReporte.LocalReport.Refresh();

            }
        }
        Session.Remove("Generado");
    }


    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        muestraInformeReporte();
    }

    private void ReemplazarEnDocumentoDeWordYGuardarPDF(string pTexto, List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> plstReemplazos, string pDocumentoGenerado)
    {
        //  cargo la session para saber la modalidad de int
        Cdat vApe = new Cdat();
        vApe = (Cdat)Session["Inf"];

        // Validar que exista el texto
        if (pTexto.Trim().Length <= 0)
            return;

        // Hacer los reemplazos de los campos
        foreach (Xpinn.FabricaCreditos.Entities.DatosDeDocumento dFila in plstReemplazos)
        {
            try
            {
                string cCampo = dFila.Campo.ToString().Trim();
                string cValor = "";
                if (dFila.Valor != null)
                    cValor = dFila.Valor.ToString().Trim().Replace("'", "");
                else
                    cValor = "";

                //Valor en letras
                if (cCampo == "pValor")
                {
                    string ValorCDAT = cValor;
                    Xpinn.Util.CardinalidadNum objCardinalidad = new Xpinn.Util.CardinalidadNum();
                    string cardinal = " ";
                    if (ValorCDAT != "0")
                    {
                        cardinal = objCardinalidad.enletras(ValorCDAT.Replace(".", ""));
                        int cont = cardinal.Trim().Length - 1;
                        int cont2 = cont - 7;
                        if (cont2 >= 0)
                        {
                            string c = cardinal.Substring(cont2);
                            if (cardinal.Trim().Substring(cont2) == "MILLONES" || cardinal.Trim().Substring(cont2 + 2) == "MILLON")
                                cardinal = cardinal + " DE PESOS M/CTE";
                            else
                                cardinal = cardinal + " PESOS M/CTE";
                        }
                    }
                    pTexto = pTexto.Replace("pValorLetras", cardinal).Replace("'", "");
                }

                pTexto = pTexto.Replace(cCampo, cValor).Replace("'", "");

                pTexto = pTexto.Replace("pFechaApertura", txtFechaApertura.Text.ToString()).Replace("'", "");
                pTexto = pTexto.Replace("pFechaVencimiento", txtfechaVenci.Text.ToString()).Replace("'", "");

                if (vApe.modalidad_int == 1)
                {
                    pTexto = pTexto.Replace("pModalidadV", "X").Replace("'", "");
                    pTexto = pTexto.Replace("pModalidadA", "-").Replace("'", "");
                }
                else if (vApe.modalidad_int == 2)
                {
                    pTexto = pTexto.Replace("pModalidadA", "X").Replace("'", "");
                    pTexto = pTexto.Replace("pModalidadV", "-").Replace("'", "");
                }
                else
                {
                    pTexto = pTexto.Replace("pModalidadA", "-").Replace("'", "");
                    pTexto = pTexto.Replace("pModalidadV", "-").Replace("'", "");
                }

            }
            catch
            {
            }
        }



        //Deja los campos beneficiarios vacios en caso de que no los tenga
        pTexto = pTexto.Replace("pNomBeneficiario1", " ");
        pTexto = pTexto.Replace("pIdenBeneficiario1", " ");
        pTexto = pTexto.Replace("pCodBeneficiario1", " ");
        pTexto = pTexto.Replace("pNomBeneficiario2", " ");
        pTexto = pTexto.Replace("pIdenBeneficiario2", " ");
        pTexto = pTexto.Replace("pCodBeneficiario2", " ");
        pTexto = pTexto.Replace("pNomBeneficiario3", " ");
        pTexto = pTexto.Replace("pIdenBeneficiario3", " ");
        pTexto = pTexto.Replace("pCodBeneficiario3", " ");

        //Añadir la imagen al reporte
        string cRutaDeImagen, cRutaDeImagens;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";
        pTexto = pTexto.Replace("pImagenReporte", cRutaDeImagen);

        cRutaDeImagens = Server.MapPath("~/Images\\") + "SelloEmpresa.png";
        pTexto = pTexto.Replace("pSelloReporte", cRutaDeImagens);

        // Convertir a PDF
        StringReader sr = new StringReader(pTexto.Replace("'", ""));
        Document pdfDoc = new Document(PageSize.A4, 20f, 10f, 10f, 10f);
        PdfWriter.GetInstance(pdfDoc, new FileStream(pDocumentoGenerado, FileMode.OpenOrCreate));
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
    }

    private void CreatePDF()
    {
        //path you want to store PDF 
        string pdfPath = string.Format(@"E:\PDF\{0}.pdf", DateTime.Now.ToString("yyyy-MM-dd hhmmss"));

        using (FileStream msReport = new FileStream(pdfPath, FileMode.Create))
        {
            //step 1
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 140f, 30f);

            // step 2
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, msReport);

            //open the stream 
            pdfDoc.Open();

            iTextSharp.text.Image gif = null;

            string base64string = "yourbase 64 string";
            try
            {
                //  Convert base64string to bytes array
                Byte[] bytes = Convert.FromBase64String(base64string);
                gif = iTextSharp.text.Image.GetInstance(bytes);

            }
            catch (DocumentException dex)
            {
                //log exception here
            }
            catch (IOException ioex)
            {
                //log exception here
            }
            gif.Alignment = iTextSharp.text.Image.ALIGN_LEFT;
            gif.Border = iTextSharp.text.Rectangle.NO_BORDER;
            gif.ScaleToFit(170f, 100f);

            pdfDoc.Add(gif);

            pdfDoc.Close();


        }
    }

    protected void btnImprime_Click(object sender, EventArgs e)
    {
        muestraInformeReporte();
    }
    protected void btnDatos_Click(object sender, EventArgs e)
    {

        mvPrincipal.Visible = true;
        Site toolBar = (Site)Master;
        toolBar.MostrarImprimir(true);
        toolBar.MostrarGuardar(true);

        mvPrincipal.ActiveViewIndex = 0;
        mvReporte.Visible = false;
    }

    protected void txtPlazo_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtPlazo.Text) && !string.IsNullOrWhiteSpace(ddlTipoCalendario.SelectedValue) && !string.IsNullOrWhiteSpace(txtFechaApertura.Text))
        {
            DateTime fechavencimiento = DateTime.Now;
            Int32 plazo = Convert.ToInt32(txtPlazo.Text);
            Int32 tipocalendario = Convert.ToInt32(ddlTipoCalendario.SelectedValue);
            DateTime fecha_proximo_pago = Convert.ToDateTime(txtFechaApertura.Text.ToString());
            if (ValidarPlazosMaximosYMinimos())
            {
                lblError.Visible = false;
                fechavencimiento = AperturaService.Calcularfecha(fechavencimiento, fecha_proximo_pago, plazo, tipocalendario);
                txtfechaVenci.Text = Convert.ToString(fechavencimiento);
            }
        }
    }
    protected void chkDesmaterial_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void cbInteresCuenta_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void ddlTipoLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");
        //Generar Consulta de la Linea Seleccionada
        LineaCDAT vLineaCdat = new LineaCDAT();
        LineaCDATService lineacdatServicio = new LineaCDATService();
        if (this.ddlTipoLinea.SelectedValue == null || ddlTipoLinea.SelectedValue == "" || this.ddlTipoLinea.SelectedValue == "0")
        {
        }
        else
        {
            vLineaCdat = lineacdatServicio.ConsultarLineaCDAT(Convert.ToString(ddlTipoLinea.SelectedValue), (Usuario)Session["usuario"]);
            lblplazo.Visible = true;
            txtPlazo.Visible = true;
            if (vLineaCdat.tipo_calendario != null)
            {
                ddlTipoCalendario.SelectedValue = Convert.ToString(vLineaCdat.tipo_calendario);
                lbltipocalendario.Visible = true;
                ddlTipoCalendario.Visible = true;
            }
            else
            {
                lbltipocalendario.Visible = false;
                ddlTipoCalendario.Visible = false;
            }

            if (vLineaCdat.interes_por_cuenta == 0)
            {
                panelTasa.Enabled = false;
                cbInteresCuenta.Enabled = false;
            }
            if (vLineaCdat.interes_por_cuenta == 1)
            {
                panelTasa.Enabled = true;
                cbInteresCuenta.Checked = true;
            }
            if (vLineaCdat.calculo_tasa != null)
            {
                //ctlTasaInteres.Inicializar();
                if (!string.IsNullOrEmpty(vLineaCdat.calculo_tasa.ToString()))
                    ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vLineaCdat.calculo_tasa.ToString().Trim());
                if (!string.IsNullOrEmpty(vLineaCdat.tipo_historico.ToString()))
                    ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaCdat.tipo_historico.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaCdat.desviacion.ToString()))
                    ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaCdat.desviacion.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaCdat.cod_tipo_tasa.ToString()))
                    ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaCdat.cod_tipo_tasa.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaCdat.tasa.ToString()))
                    ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaCdat.tasa.ToString().Trim()));

            }


        }
    }
    protected void ddlTipoCalendario_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtPlazo.Text) && !string.IsNullOrWhiteSpace(ddlTipoCalendario.SelectedValue) && !string.IsNullOrWhiteSpace(txtFechaApertura.Text))
        {
            DateTime fechavencimiento = DateTime.Now;
            Int32 plazo = Convert.ToInt32(txtPlazo.Text);
            Int32 tipocalendario = Convert.ToInt32(ddlTipoCalendario.SelectedValue);
            DateTime fecha_proximo_pago = Convert.ToDateTime(txtFechaApertura.Text.ToString());
            if (ValidarPlazosMaximosYMinimos())
            {
                lblError.Visible = false;
                fechavencimiento = AperturaService.Calcularfecha(fechavencimiento, fecha_proximo_pago, plazo, tipocalendario);
                txtfechaVenci.Text = Convert.ToString(fechavencimiento);
            }
        }
    }

    protected string FormatoFecha()
    {
        Configuracion conf = new Configuracion();
        return conf.ObtenerFormatoFecha();
    }


    protected void gvBeneficiarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvBeneficiarios.DataKeys[e.RowIndex].Values[0].ToString());
        String filtro = "";
        ObtenerListaBeneficiariosCdats();

        List<Beneficiario> LstBene;
        LstBene = (List<Beneficiario>)Session["DatosBene"];

        if (conseID > 0)
        {
            try
            {
                foreach (Beneficiario bene in LstBene)
                {
                    if (bene.idbeneficiario == conseID)
                    {
                        BeneficiarioServicio.EliminarBeneficiarioCdat(filtro, conseID, (Usuario)Session["usuario"]);
                        LstBene.Remove(bene);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstBene.RemoveAt((gvBeneficiarios.PageIndex * gvBeneficiarios.PageSize) + e.RowIndex);
        }

        gvBeneficiarios.DataSource = LstBene;
        gvBeneficiarios.DataBind();

        Session["DatosBene"] = LstBene;
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
                ddlParentezco.DataSource = BeneficiarioServicio.ListarParentesco(Ben, (Usuario)Session["usuario"]);
                ddlParentezco.DataTextField = "DESCRIPCION";
                ddlParentezco.DataValueField = "CODPARENTESCO";
                ddlParentezco.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Seleccione un item>", "0"));
                ddlParentezco.DataBind();

            }

            Label lblParentezco = (Label)e.Row.FindControl("lblParentezco");
            if (lblParentezco.Text != null)
                ddlParentezco.SelectedValue = lblParentezco.Text;

            Label lblSexo = (Label)e.Row.FindControl("lblSexo");
            if (lblSexo.Text != null)
                ddlSexo.SelectedValue = lblSexo.Text;
        }
    }

    protected void chkBeneficiario_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkbeneficiario = (CheckBox)sender;
        if (chkbeneficiario.Checked)
            upBeneficiarios.Visible = true;
        else
            upBeneficiarios.Visible = false;
    }

    protected void btnAddRowBeneficio_Click(object sender, EventArgs e)
    {
        Session["DatosBene"] = null;
        ObtenerListaBeneficiariosCdats();

        List<Beneficiario> lstBene = new List<Beneficiario>();

        if (Session["DatosBene"] != null)
        {
            lstBene = (List<Beneficiario>)Session["DatosBene"];

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
                eBenef.edad = null;
                eBenef.sexo = null;
                lstBene.Add(eBenef);
            }
            gvBeneficiarios.DataSource = lstBene;
            gvBeneficiarios.DataBind();

            Session["DatosBene"] = lstBene;
        }
        else
        {
            lstBene = new List<Beneficiario>(); ;

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
                eBenef.edad = null;
                eBenef.sexo = null;
                lstBene.Add(eBenef);
            }
            gvBeneficiarios.DataSource = lstBene;
            gvBeneficiarios.DataBind();

            Session["DatosBene"] = lstBene;
        }
    }

    protected void FechaNacimiento_Changed(object sender, EventArgs e)
    {
        TextBox fechaNacimiento = sender as TextBox;
        TextBox edadBeneficiaro = fechaNacimiento.NamingContainer.NamingContainer.FindControl("txtEdadBen") as TextBox;

        if (fechaNacimiento != null && edadBeneficiaro != null && !string.IsNullOrWhiteSpace(fechaNacimiento.Text))
        {
            DateTimeHelper dateHelper = new DateTimeHelper();
            edadBeneficiaro.Text = dateHelper.DiferenciaEntreDosFechasAños(DateTime.Today, Convert.ToDateTime(fechaNacimiento.Text)).ToString();
        }
    }


    private Boolean validarreglasgrabar()
    {
        Boolean result = true;
        decimal totalPorcentaje = 0;
        int contPrincipal = 0;
        int contar = 0;

        List<Beneficiario> LstLbeneficiario = new List<Beneficiario>();
        LstLbeneficiario = (List<Beneficiario>)Session["BeneficiarioCdat"];
        this.lblError.Text = "";
        LstLbeneficiario = ObtenerListaBeneficiariosCdats();

        //RECUPERAR PARAMETROS SI ES OBLIGATORIO BENEFICIARIOS
        Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
        pData = ConsultaData.ConsultarGeneral(92, (Usuario)Session["usuario"]);

        if (pData.valor == "1")
        {
            if (LstLbeneficiario != null)
            {
                //primer regla suma de porcentajes...
                foreach (Beneficiario bebeficiariocdat in LstLbeneficiario)
                {
                    if (bebeficiariocdat.identificacion_ben != null)
                    {
                        totalPorcentaje = Convert.ToDecimal(bebeficiariocdat.porcentaje_ben + totalPorcentaje);
                        contar++;
                    }

                }
            }

            //valido se adicionen beneficiarios 


            //valido que la suma del porcentaje no sea diferente a 100
            if ((totalPorcentaje) > 100)
            {
                String Error = "El porcentaje de distribución de loas beneficiarios es superior  al 100%";
                this.lblError.Text = Error;
                this.lblError.Visible = true;
                result = false;

            }

            if ((totalPorcentaje) < 100)
            {
                String Error = "El porcentaje de distribución de los beneficarios es inferior al 100%";
                this.lblError.Text = Error;
                this.lblError.Visible = true;
                result = false;
            }


            if (LstLbeneficiario.Count == 0)
            {
                String Error = "Falta ingresar beneficiarios del Cdat";
                this.lblError.Text = Error;
                this.lblError.Visible = true;
                result = false;

            }

        }
        return result;
    }

    protected void chkLiquidaVencimiento_CheckedChanged(object sender, EventArgs e)
    {
        if (chkLiquidaVencimiento.Checked)
            ddlPeriodicidad.Enabled = false;
        else
            ddlPeriodicidad.Enabled = true;
    }


    public void cargarDatosSolicitud()
    {
        //Creo la lista
        List<CuentaHabientes> lstDetalle = new List<CuentaHabientes>();
        int EnteTerri = 0;
        //Si tiene datos de solicitud los carga primero
        if (Session["solicitudProducto"] != null)
        {
            AhorroVista solicitud = new AhorroVista();
            solicitud = Session["solicitudProducto"] as AhorroVista;

            Xpinn.FabricaCreditos.Services.Persona1Service personaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 persona = new Xpinn.FabricaCreditos.Entities.Persona1();
            persona.cod_persona = Convert.ToInt64(solicitud.cod_persona);
            persona.identificacion = solicitud.identificacion;
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = personaService.ListadoPersonas1(persona, (Usuario)Session["usuario"]);
            persona = lstConsulta.ElementAt(0);

            CuentaHabientes eDeta = new CuentaHabientes();

            eDeta.identificacion = persona.identificacion;
            eDeta.cod_persona = persona.cod_persona;
            eDeta.nombres = persona.nombres;
            eDeta.apellidos = persona.apellidos;
            eDeta.ciudad = persona.nomciudad_resid;
            eDeta.direccion = persona.direccion;
            eDeta.telefono = persona.telefono;
            eDeta.principal = 1;
            eDeta.conjuncion = null;
            eDeta.cod_usuario_cdat = -1;
            lstDetalle.Add(eDeta);

            //Carga los demás datos
            try
            {
                ddlTipoLinea.SelectedValue = Convert.ToString(solicitud.cod_linea_ahorro);
                txtPlazo.Text = Convert.ToString(solicitud.plazo * 30);
                //txtPlazo.Visible = true;
                txtValor.Text = Convert.ToString(solicitud.valor_cuota).Replace(".", "");
                ddlTipoLinea_SelectedIndexChanged(new object(), new EventArgs());
                txtPlazo_TextChanged(new object(), new EventArgs());
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(AperturaService.CodigoPrograma, "gvDetMovs_PageIndexChanging", ex);
            }
        }

        if (lstDetalle.Count > 0)
        {
            gvDetalle.Visible = true;
            gvDetalle.DataSource = lstDetalle;
            gvDetalle.DataBind();
            Session["DatosDetalle"] = lstDetalle;
        }
    }

    public void actualizarSolicitud(long cod_persona)
    {
        if (Session["solicitudProducto"] != null)
        {
            AhorroVista solicitud = Session["solicitudProducto"] as AhorroVista;
            solicitud.estado_modificacion = "1"; // aprobando solicitud                 
            _ahorroService.ModificarEstadoSolicitudProducto(solicitud, (Usuario)Session["usuario"]);
            Session["solicitudProducto"] = null;
            Xpinn.Comun.Services.Formato_NotificacionService COServices = new Xpinn.Comun.Services.Formato_NotificacionService();
            Xpinn.Comun.Entities.Formato_Notificacion noti = new Xpinn.Comun.Entities.Formato_Notificacion(Convert.ToInt32(cod_persona), 17, "nombreProducto;CDAT");
            COServices.SendEmailPerson(noti, (Usuario)Session["usuario"]);
        }
    }
}
