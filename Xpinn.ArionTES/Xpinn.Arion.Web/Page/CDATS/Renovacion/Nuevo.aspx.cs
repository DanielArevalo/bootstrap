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
using Xpinn.CDATS.Services;
using Xpinn.CDATS.Entities;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.IO;


partial class Nuevo : GlobalWeb
{
    LiquidacionCDATService LiquiService = new LiquidacionCDATService();
    AperturaCDATService AperturaService = new AperturaCDATService();
    NumeracionCuentas BONumeracionCuentaCDAT = new NumeracionCuentas();
    Cdat vApert = new Cdat();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[AperturaService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(AperturaService.codigoprogramarenovacioncdat, "E");
            else
                VisualizarOpciones(AperturaService.codigoprogramarenovacioncdat, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarImprimir(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramarenovacioncdat, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {           
            Session["DatosDetalle"] = null;
            if (Session["RETURNO"] == null)
                Session["RETURNO"] = "";
            if (!Page.IsPostBack)
            {
                cargarDropdown();
                Site toolBar = (Site)this.Master;
                mvPrincipal.Visible = true;

                mvPrincipal.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                toolBar.MostrarGuardar(true);
                toolBar.MostrarNuevo(true);
                txtFecha.ToDateTime = DateTime.Now;
                giro.Inicializar();
                //idObjeto = Convert.ToString(Session["Cdat"]);
                // ObtenerDatos(idObjeto);
                //  CALCULAR();
                // cargarDropdown();
                //  ctlTasaInteresRenova.Inicializar();
                // ctlTasa.Inicializar();
                if (Session[AperturaService.codigoprogramarenovacioncdat + ".id"] != null)
                {
                    idObjeto = Session[AperturaService.codigoprogramarenovacioncdat + ".id"].ToString();
                    Session.Remove(AperturaService.codigoprogramarenovacioncdat + ".id");
                    idObjeto = Convert.ToString(Session["Cdat"]);
                    ObtenerDatos(idObjeto);

                    toolBar.MostrarImprimir(true);
                    if (Session["ADMI"].ToString() == "RENOVACION")
                    {
                        mvPrincipal.ActiveViewIndex = 1;
                    }
                }
                else
                {
                    txtFechaApertura.Text = DateTime.Today.ToShortDateString();
                    Usuario vUsu = (Usuario)Session["usuario"];
                }

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramarenovacioncdat, "Page_Load", ex);
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
        pentidad.descripcion = "  ";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }

    void cargarDropdown()
    {
        ctlTasaInteresRenova.Inicializar();
        ctlTasa.Inicializar();

        Cdat Data = new Cdat();
        List<Cdat> lstTipoLinea = new List<Cdat>();
        Data.estado = 1;
        lstTipoLinea = AperturaService.ListarTipoLineaCDAT(Data, (Usuario)Session["usuario"]);
        if (lstTipoLinea.Count > 0)
        {
            ddlTipoLinea.DataSource = lstTipoLinea;
            ddlTipoLinea.DataTextField = "NOMBRE";
            ddlTipoLinea.DataValueField = "COD_LINEACDAT";
            ddlTipoLinea.Items.Insert(0, new ListItem("  ", "0"));
            ddlTipoLinea.DataBind();

        }
       
        ddlModalidads.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
        ddlModalidads.Items.Insert(1, new ListItem("INDIVIDUAL", "IND"));
        ddlModalidads.Items.Insert(2, new ListItem("CONJUNTA", "CON"));
        ddlModalidads.Items.Insert(3, new ListItem("ALTERNA", "ALT"));
        ddlModalidads.SelectedIndex = 1;
        ddlModalidads.DataBind();


        PoblarLista("DESTINACION_CDAT", ddlDestinacions);
        PoblarLista("Formacaptacion_CDAT", ddlFormaCaptacions);
        ddlFormaCaptacions.SelectedIndex = 1;

        lstTipoLinea = AperturaService.ListarTipoLineaCDAT(Data, (Usuario)Session["usuario"]);
        if (lstTipoLinea.Count > 0)
        {
            ddlLineaRenova.DataSource = lstTipoLinea;
            ddlLineaRenova.DataTextField = "NOMBRE";
            ddlLineaRenova.DataValueField = "COD_LINEACDAT";
            ddlLineaRenova.Items.Insert(0, new ListItem("  ", "0"));
            ddlLineaRenova.DataBind();

        }
        List<Cdat> lstUsuarios = new List<Cdat>();
        Cdat Data2 = new Cdat();
       /* lstUsuarios = AperturaService.ListarUsuariosAsesores(Data2, (Usuario)Session["usuario"]);
       /* if (lstUsuarios.Count > 0)
        {
            DdlAsesorRenova.DataSource = lstUsuarios;
            DdlAsesorRenova.DataTextField = "nombre";
            DdlAsesorRenova.DataValueField = "cod_oficina";
            DdlAsesorRenova.SelectedIndex = 0;
            DdlAsesorRenova.DataBind();
        }
     */
        string ddlusuarios = "0";
        if (ddlusuarios == "0")
        {
            lstUsuarios = AperturaService.ListartodosUsuarios(Data, (Usuario)Session["usuario"]);
            if (lstUsuarios.Count > 0)
            {
                DdlAsesorRenova.DataSource = lstUsuarios;
                DdlAsesorRenova.DataTextField = "nombre";
                DdlAsesorRenova.DataValueField = "codusuario";
                DdlAsesorRenova.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un item", "-1"));
                DdlAsesorRenova.SelectedIndex = 0;
                DdlAsesorRenova.DataBind();
            }
            else
            {
                ddlusuarios = "2";
            }
        }
        if (ddlusuarios == "2")
        {
            Data.estado = 1;
            lstUsuarios = AperturaService.ListarUsuariosAsesores(Data, (Usuario)Session["usuario"]);
            if (lstUsuarios.Count > 0)
            {
                DdlAsesorRenova.DataSource = lstUsuarios;
                DdlAsesorRenova.DataTextField = "nombre";
                DdlAsesorRenova.DataValueField = "codusuario";
                DdlAsesorRenova.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione un item", "0"));
                DdlAsesorRenova.SelectedIndex = 0;
                DdlAsesorRenova.DataBind();
            }
        }
        // Colocar por defecto el usuario
        if (Session["usuario"] != null)
        {
            try
            {
                if (Usuario.nombre != null && Usuario.nombre != "")
                    DdlAsesorRenova.SelectedValue = Usuario.codusuario.ToString();
            }
            catch { }
        }

        //PoblarLista("TIPOTASA", ddlFormaCaptacion); //PRUEBA

        PoblarLista("Tipomoneda", ddlTipoMoneda);
        PoblarLista("Tipomoneda", ddlmonedarenovado);
        ddlmonedarenovado.SelectedIndex = 2;

        ddlTipoCalendario.Items.Insert(0, new ListItem("  ", "0"));
        ddlTipoCalendario.Items.Insert(1, new ListItem("Comercial", "1"));
        ddlTipoCalendario.Items.Insert(2, new ListItem("Calendario", "2"));
        ddlTipoCalendario.DataBind();

        ddlcalendarioRenova.Items.Insert(0, new ListItem("  ", "0"));
        ddlcalendarioRenova.Items.Insert(1, new ListItem("Comercial", "1"));
        ddlcalendarioRenova.Items.Insert(2, new ListItem("Calendario", "2"));
        ddlcalendarioRenova.DataBind();

        //PoblarLista("TIPOTASA", ddlDestinacion); //PRUEBA     

        List<Cdat> lstAsesores = new List<Cdat>();
        List<Cdat> lstOficina = new List<Cdat>();

        lstOficina = AperturaService.ListarOficinas(Data, (Usuario)Session["usuario"]);
        if (lstOficina.Count > 0)
        {
            ddlOficina.DataSource = lstOficina;
            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "COD_OFICINA";
            ddlOficina.Items.Insert(0, new ListItem("  ", "0"));

            ddlOficina.DataBind();
        }

        if (lstOficina.Count > 0)
        {
            ddloficinarenova.DataSource = lstOficina;
            ddloficinarenova.DataTextField = "nombre";
            ddloficinarenova.DataValueField = "COD_OFICINA";
            ddloficinarenova.Items.Insert(0, new ListItem("  ", "0"));

            ddloficinarenova.DataBind();
        }

        PoblarLista("Periodicidad", ddlPeriodicidad);
        PoblarLista("Periodicidad", ddlPeriodicidadRenova);
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
                eApert.cod_persona = null;
                eApert.principal = null;
                eApert.conjuncion = "";
                lstDetalle.Add(eApert);
            }
            gvDetalle.PageIndex = gvDetalle.PageCount;
            GridView1.PageIndex = GridView1.PageCount;
            gvDetalle.DataSource = lstDetalle;
            GridView1.DataSource = lstDetalle;
            gvDetalle.DataBind();
            GridView1.DataBind();

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
            eApert.cod_persona = null;
            eApert.principal = null;
            eApert.conjuncion = "";
            lstDetalle.Add(eApert);
        }
        gvDetalle.DataSource = lstDetalle;
        gvDetalle.DataBind();
        GridView1.DataSource = lstDetalle;
        GridView1.DataBind();
        Session["DatosDetalle"] = lstDetalle;
    }

    //Eventos Grilla

    protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlConjuncion = (DropDownListGrid)e.Row.FindControl("ddlConjuncion");
            if (ddlConjuncion != null)
            {
                ddlConjuncion.Items.Insert(0, new ListItem(" ", "0"));
                ddlConjuncion.Items.Insert(1, new ListItem("Y", "Y"));
                ddlConjuncion.Items.Insert(2, new ListItem("O", "O"));
            }

            Label lblConjuncion = (Label)e.Row.FindControl("lblConjuncion");
            if (lblConjuncion != null)
                ddlConjuncion.SelectedValue = lblConjuncion.Text;

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

                    AperturaService.EliminarTitularCdat(conseID, (Usuario)Session["usuario"]);
                    LstDeta.Remove(Deta);
                    break;

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

        GridView1.DataSourceID = null;
        GridView1.DataBind();

        GridView1.DataSource = LstDeta;
        GridView1.DataBind();

        Session["DatosDetalle"] = LstDeta;
    }
    protected void btnListadoPersona_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPersona = (ButtonGrid)sender;
        if (btnListadoPersona != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPersona.CommandArgument);
            BusquedaRapida ctlListadoPer = (BusquedaRapida)gvDetalle.Rows[rowIndex].FindControl("ctlListadoPersona");
            ctlListadoPer.Motrar(true, "lblcod_persona", "txtIdentificacion", "", "lblNombre", "lblApellidos");
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

        Detalle_CDAT DataPersona = new Detalle_CDAT();
        DataPersona.identificacion = txtIdentificacion.Text;
        DataPersona = AperturaService.ConsultarPersona(DataPersona, (Usuario)Session["usuario"]);

        if (DataPersona.cod_persona != 0 && DataPersona.cod_persona != null)
        {
            if (DataPersona.cod_persona != 0 && DataPersona.cod_persona != null)
                lblcod_persona.Text = DataPersona.cod_persona.ToString();

            if (DataPersona.nombres != "" && DataPersona.nombres != null)
                lblNombre.Text = DataPersona.nombres;

            if (DataPersona.apellidos != "" && DataPersona.apellidos != null)
                lblApellidos.Text = DataPersona.apellidos;

            if (DataPersona.ciudad != "" && DataPersona.ciudad != null)
                lblCiudad.Text = DataPersona.ciudad;

            if (DataPersona.direccion != "" && DataPersona.direccion != null)
                lblDireccion.Text = DataPersona.direccion;

            if (DataPersona.telefono != "" && DataPersona.telefono != null)
                lbltelefono.Text = DataPersona.telefono;
        }
        else
        {
            lblcod_persona.Text = ""; lblNombre.Text = ""; lblApellidos.Text = "";
            lblCiudad.Text = ""; lblDireccion.Text = ""; lbltelefono.Text = "";
        }
    }
    protected void chkPrincipal_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkPrincipal = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkPrincipal.CommandArgument);

        if (chkPrincipal != null)
        {
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                CheckBoxGrid check = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
                check.Checked = false;
                if (rFila.RowIndex == rowIndex)
                {
                    check.Checked = true;
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
                eDeta.codigo_cdat = Convert.ToInt64(lblcodigo.Text);

            Label lblidentificacion = (Label)rfila.FindControl("lblidentificacion");
            if (lblidentificacion != null)
                eDeta.identificacion = Convert.ToString(lblidentificacion.Text);

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

            vApe = AperturaService.ConsultarApertu(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vApe.codigo_cdat != 0) txtCodigo.Text = vApe.codigo_cdat.ToString();
            if (vApe.numero_cdat != "") txtNumCDAT.Text = vApe.numero_cdat;
            Session["nroCDAT"] = vApe.numero_cdat;

            if (vApe.cod_oficina != 0) ddlOficina.SelectedValue = vApe.cod_oficina.ToString();

            paneltasaActual.Visible = true;
            if (vApe.fecha_apertura != DateTime.MinValue) txtFechaApertura.Text = vApe.fecha_apertura.ToShortDateString();
            txtfechaApRenova.Text = Convert.ToString(DateTime.Now);
            if (vApe.plazo != 0) txtPlazo.Text = vApe.plazo.ToString();
            if (vApe.tipo_calendario != 0) ddlTipoCalendario.SelectedValue = vApe.tipo_calendario.ToString();
            if (vApe.valor != 0) txtValor.Text = vApe.valor.ToString();
            if (vApe.cod_moneda != 0) ddlTipoMoneda.SelectedValue = vApe.cod_moneda.ToString();
            
            if (vApe.tipo_interes != null)
            {
                ctlTasa.FormaTasa = vApe.tipo_interes;
                if (ctlTasa.Indice == 0)//NIGUNA
                {
                }
                else if (ctlTasa.Indice == 1)//FIJO
                {
                    if (vApe.tasa_interes != 0)
                        ctlTasa.Tasa = vApe.tasa_interes;
                    if (vApe.cod_tipo_tasa != 0)
                        ctlTasa.TipoTasa = vApe.cod_tipo_tasa;
                }
                else // HISTORICO
                {
                    if (vApe.tipo_historico != 0)
                        ctlTasa.TipoHistorico = Convert.ToInt32(vApe.tipo_historico);
                    if (vApe.desviacion != 0)
                        ctlTasa.Desviacion = Convert.ToDecimal(vApe.desviacion);
                }
            }
            if (vApe.tipo_interes != null)
            {
                ctlTasaInteresRenova.FormaTasa = vApe.tipo_interes;
                if (ctlTasaInteresRenova.Indice == 0)//NIGUNA
                {
                }
                else if (ctlTasaInteresRenova.Indice == 1)//FIJO
                {
                    if (vApe.tasa_interes != 0)
                        ctlTasaInteresRenova.Tasa = vApe.tasa_interes;
                    if (vApe.cod_tipo_tasa != 0)
                        ctlTasaInteresRenova.TipoTasa = vApe.cod_tipo_tasa;
                }
                else // HISTORICO
                {
                    if (vApe.tipo_historico != 0)
                        ctlTasaInteresRenova.TipoHistorico = Convert.ToInt32(vApe.tipo_historico);
                    if (vApe.desviacion != 0)
                        ctlTasaInteresRenova.Desviacion = Convert.ToDecimal(vApe.desviacion);
                }
            }

            //Generar Consulta de la Linea Seleccionada
            LineaCDAT vLineaCdat = new LineaCDAT();
            LineaCDATService linahorroServicio = new LineaCDATService();
            vLineaCdat = linahorroServicio.ConsultarLineaCDAT(Convert.ToString(vApe.cod_lineacdat), (Usuario)Session["usuario"]);
            Session["RangoCDAT"] = vLineaCdat.lstRangos;
            if (vLineaCdat.interes_por_cuenta == 0)
            {
                cbInteresCuenta.Enabled = false;
                panelTasa.Enabled = false;
                paneltasaActual.Enabled = false;
            }
            if (vLineaCdat.interes_por_cuenta == 1)
            {
                panelTasa.Enabled = true;
                paneltasaActual.Enabled = false;
                cbInteresCuenta.Checked = true;
            }

            if (vApe.codigo_cdat != 0) txtCodigo.Text = vApe.codigo_cdat.ToString();
            //  if (vApe.codigo_cdat != 0) TextBox22.Text = vApe.codigo_cdat.ToString();

            if (vApe.codigo_cdat != 0) txtNumPreImpresos.Text = vApe.numero_fisico.ToString();
            // if (vApe.codigo_cdat != 0) TextBox11.Text = vApe.codigo_cdat.ToString();

            if (vApe.numero_cdat != "") txtNumCDAT.Text = vApe.numero_cdat;
            //  if (vApe.numero_cdat != "") TextBox11.Text = vApe.numero_cdat;

            Session["nroCDAT"] = vApe.numero_cdat;

            if (vApe.cod_oficina != 0) ddlOficina.SelectedValue = vApe.cod_oficina.ToString();
            if (vApe.cod_oficina != 0) ddloficinarenova.SelectedValue = vApe.cod_oficina.ToString();

            if (vApe.cod_lineacdat != "") ddlTipoLinea.SelectedValue = vApe.cod_lineacdat;
            if (vApe.cod_lineacdat != "") ddlLineaRenova.SelectedValue = vApe.cod_lineacdat;

            if (vApe.fecha_apertura != DateTime.MinValue) txtFechaApertura.Text = vApe.fecha_apertura.ToShortDateString();
            if (vApe.tipo_calendario != 0)
            {
                ddlTipoCalendario.SelectedValue = Convert.ToString(vApe.tipo_calendario);
                //  ddlTipoCalendario.Text = vApe.tipo_calendario.ToString();
                ddlcalendarioRenova.SelectedValue = Convert.ToString(vApe.tipo_calendario);
            }
            if (vApe.plazo != 0) txtPlazo.Text = vApe.plazo.ToString();
            if (vApe.plazo != 0) txtplazorenova.Text = vApe.plazo.ToString();
            txtplazorenova_TextChanged(txtplazorenova, null);

            if (vApe.valor != 0) txtValor.Text = vApe.valor.ToString();
            if (vApe.valor != 0) txvalorrenovado.Text = vApe.valor.ToString();

            if (vApe.cod_moneda != 0) ddlTipoMoneda.SelectedValue = vApe.cod_moneda.ToString();
            if (vApe.cod_moneda != 0) ddlmonedarenovado.SelectedValue = vApe.cod_moneda.ToString();

            //fecha vencimiento no lo recupero
            //if (vApe.fecha_vencimiento != DateTime.MinValue) txtFecha.Text = vApe.fecha_vencimiento.ToShortDateString();
            if (vApe.fecha_apertura != DateTime.MinValue) txtFechaEmis.Text = vApe.fecha_apertura.ToShortDateString();

            if (vApe.cod_lineacdat != "")
            {
                ddlTipoLinea.SelectedValue = vApe.cod_lineacdat.ToString(); ;
                ddlLineaRenova.SelectedValue = vApe.cod_lineacdat.ToString();
                ddlLineaRenova_SelectedIndexChanged(ddlLineaRenova, null);

            }
            if (vApe.cod_periodicidad_int != 0 && vApe.cod_periodicidad_int != null)
            {
                ddlPeriodicidad.SelectedValue = vApe.cod_periodicidad_int.ToString();
                ddlPeriodicidadRenova.SelectedValue = vApe.cod_periodicidad_int.ToString();
                ddlPeriodicidadRenova_SelectedIndexChanged(ddlPeriodicidadRenova, null);
            }

            if (vApe.modalidad_int != 0 && vApe.modalidad_int != null)
                rblModalidadInt.SelectedValue = vApe.modalidad_int.ToString();
            rblModalidadInts.SelectedValue = vApe.modalidad_int.ToString();

            if (vApe.codforma_captacion != 0 && vApe.codforma_captacion != null) 
                ddlFormaCaptacions.SelectedValue = vApe.codforma_captacion.ToString();

            //RECUPERAR GRILLA DETALLE 
            List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();

            lstDetalle = AperturaService.ListarDetalleTitulares(Convert.ToInt64(txtCodigo.Text), (Usuario)Session["usuario"]);

            if (lstDetalle.Count > 0)
            {
                gvDetalle.Visible = true;
                gvDetalle.DataSource = lstDetalle;
                gvDetalle.DataBind();

            }
            else
            {
                InicializarDetalle();
            }

            LiquidarCDAT();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramarenovacioncdat, "ObtenerDatos", ex);
        }
    }


    protected void LiquidarCDAT()
    {
        LiquidacionCDAT pLiqui = new LiquidacionCDAT();
        LiquidacionCDAT entidad = new LiquidacionCDAT();

        if (txtFecha.Texto == "")
        {
            VerError("Ingrese la fecha de Cierre para realizar la Renovación.");
            return;
        }
        pLiqui.fecha_liquidacion = Convert.ToDateTime(txtFecha.Texto);
        pLiqui.numero_cdat = txtNumCDAT.Text;
        pLiqui.valor = 0;
        pLiqui.interes_causado = 0;
        pLiqui.interes = 0;
        pLiqui.retencion = 0;
        pLiqui.valor_gmf = 0;
        pLiqui.valor_pagar = 0;

        //prroroga de cdats 
        pLiqui.origen = 0;

        LiquidacionCDATService LiquiService = new LiquidacionCDATService();

        entidad = LiquiService.CalculoLiquidacionCDAT(pLiqui, (Usuario)Session["usuario"]);
        txtvalorarenovar.Text = txtValor.Text;
        //if (entidad.valor != 0)
        //    txtValor.Text = entidad.valor.ToString();
        //else
        //    txtValor.Text = "0";
        //if (entidad.interes_causado != 0)
        //    txtvalorarenovar.Text = entidad.interes_causado.ToString();
        //else
        //    txtvalorarenovar.Text = "0";
        if (entidad.interes != 0)
            txtinteres.Text = entidad.interes.ToString();
        else
            txtinteres.Text = "0";
        if (entidad.retencion != 0)
            txtretencion.Text = entidad.retencion.ToString();
        else
            txtretencion.Text = "0";
        entidad.valor_gmf = 0;
        if (entidad.valor_gmf != 0)
            txtmenosgmf.Text = entidad.valor_gmf.ToString();
        else
            txtmenosgmf.Text = "0";
        if (entidad.valor_pagar != 0)
            this.txttotalapagar.Text = Convert.ToString(entidad.interes - entidad.retencion - entidad.valor_gmf).ToString();
    }


    protected void LiquidarCDAT2()
    {
        LiquidacionCDAT pLiqui = new LiquidacionCDAT();
        LiquidacionCDAT entidad = new LiquidacionCDAT();

        if (txtFecha.Texto == "")
        {
            VerError("Ingrese la fecha de Cierre para realizar la Renovación.");
            return;
        }
        pLiqui.fecha_liquidacion = Convert.ToDateTime(txtFecha.Texto);
        pLiqui.numero_cdat = txtNumCDAT.Text;
        pLiqui.valor = 0;
        pLiqui.interes_causado = 0;
        pLiqui.interes = 0;
        pLiqui.retencion = 0;
        pLiqui.valor_gmf = 0;
        pLiqui.valor_pagar = 0;
        LiquidacionCDATService LiquiService = new LiquidacionCDATService();

        entidad = LiquiService.CalculoLiquidacionCDAT(pLiqui, (Usuario)Session["usuario"]);
        txtvalorarenovar.Text = txtValor.Text;
        //if (entidad.valor != 0)
        //    txtValor.Text = entidad.valor.ToString();
        //else
        //    txtValor.Text = "0";
        //if (entidad.interes_causado != 0)
        //    txtvalorarenovar.Text = entidad.interes_causado.ToString();
        //else
        //    txtvalorarenovar.Text = "0";
        if (entidad.interes != 0)
            txtinteres.Text = entidad.interes.ToString();
        else
            txtinteres.Text = "0";

        if (entidad.retencion != 0)
            txtretencion.Text = entidad.retencion.ToString();
        else
            txtretencion.Text = "0";
        if (entidad.valor_gmf != 0)
            txtmenosgmf.Text = entidad.valor_gmf.ToString();
        else
            txtmenosgmf.Text = "0";
        if (entidad.valor_pagar != 0)
            this.txttotalapagar.Text = Convert.ToString(entidad.interes_causado + entidad.interes - entidad.retencion - entidad.valor_gmf).ToString();
        //   this.txttotalapagar.Text = entidad.valor_pagar.ToString();

    }

    Boolean ValidarDatos()
    {
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
        if (txtFecha.Text == "")
        {
            VerError("Ingrese la Fecha de Vencimiento");
            return false;
        }

        if (txtValor.Text == "0")
        {
            VerError("Ingrese el Valor");
            return false;
        }
        //if (ddlTipoMoneda.SelectedIndex == 0)
        //{
        //    VerError("Seleccione el Tipo de Moneda");
        //    return false;
        //}
        if (txtPlazo.Text == "")
        {
            VerError("Ingrese el Plazo correspondiente");
            return false;
        }
        if (ddlTipoCalendario.SelectedIndex == 0)
        {
            VerError("Seleccione el Tipo de Calendario");
            return false;
        }

        if (ddlOficina.SelectedIndex == 0)
        {
            VerError("Seleccione la Oficina perteneciente al Asesor");
            return false;
        }




        List<Detalle_CDAT> LstDetalle = new List<Detalle_CDAT>();
        LstDetalle = ObtenerListaDetalle();

        if (LstDetalle.Count == 0)
        {
            VerError("Debe Ingresar un Titular Principal");
            return false;
        }


        int plazo;

        plazo = Convert.ToInt32(txtPlazo.Text);
        if (plazo == null)
        {
            VerError("El Plazo ingresado no es valido para la Periodicidad Seleccionada");
            return false;
        }

        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        Usuario vUsuario = new Usuario();
        vUsuario = (Usuario)Session["Usuario"];
        try
        {
            VerError("");
            if (ValidarDatos())
            {
                string msj;

                if (mvPrincipal.ActiveViewIndex == 0)
                {
                    //ctlTasaInteresRenova.Inicializar();
                    //paneltasaActual.Visible = false; 
                    mvPrincipal.ActiveViewIndex = 1;
                    PoblarLista("Tipomoneda", ddlmonedarenovado);
                    ddlmonedarenovado.SelectedIndex = 1;
                    PoblarLista("Periodicidad", ddlPeriodicidadRenova);
                    ddlPeriodicidadRenova.SelectedIndex = 0;
                }


                else
                {

                    msj = "Guardar los datos de ";
                    ctlMensaje.MostrarMensaje("Desea " + msj + "Esta pantalla");
                }
                if (mvPrincipal.ActiveViewIndex == 1)
                {
                    //RECUPERAR GRILLA DETALLE 
                    List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();

                    lstDetalle = AperturaService.ListarDetalleTitulares(Convert.ToInt64(txtCodigo.Text), (Usuario)Session["usuario"]);

                    if (lstDetalle.Count > 0)
                    {
                        GridView1.Visible = true;
                        GridView1.DataSource = lstDetalle;
                        GridView1.DataBind();
                    }
                    //else
                    //{
                    //    InicializarDetalle();
                    //}
                    mvPrincipal.ActiveViewIndex = 1;
                    Cdat Data = new Cdat();
                    Data = AperturaService.ConsultarNumeracionCDATS(Data, (Usuario)Session["usuario"]);
                    paneltasaActual.Visible = false;
                    ctlTasa.Visible = false;
                    //OBTENER LOS DATOS DEL TITULAR PRINCIPAL
                    string pIdentificacion = string.Empty;
                    Int64 pCodpersona = 0;
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
                    if (TextBox11.Text == "")
                    {
                        if (Data.valor == 1)
                        {
                            vApert.opcion = 1;
                            string pError = "";
                            string autogenerado = BONumeracionCuentaCDAT.ObtenerCodigoParametrizado(3, pIdentificacion, pCodpersona, ddlTipoLinea.SelectedValue, ref pError, vUsuario);
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
                            TextBox11.Visible = false;
                            TextBox22.Visible = false;
                            Label11.Visible = true;

                        }
                        else
                        {
                            vApert.opcion = 0;//NO AUTOGENERE
                            vApert.numero_cdat = TextBox11.Text;
                            TextBox11.Visible = true;
                        }
                    }
                    else
                    {
                        vApert.opcion = 0;
                        vApert.numero_cdat = TextBox11.Text;
                    }

                    Cdat vApe = new Cdat();
                    vApe = AperturaService.ConsultarApertu(Convert.ToInt64(this.txtCodigo.Text), (Usuario)Session["usuario"]);


                    if (vApe.tipo_interes != null)
                    {
                        ctlTasaInteresRenova.FormaTasa = vApe.tipo_interes;
                        if (ctlTasaInteresRenova.Indice == 0)//NIGUNA
                        {
                        }
                        else if (ctlTasaInteresRenova.Indice == 1)//FIJO
                        {
                            if (vApe.tasa_interes != 0)
                                ctlTasaInteresRenova.Tasa = vApe.tasa_interes;
                            if (vApe.cod_tipo_tasa != 0)
                                ctlTasaInteresRenova.TipoTasa = vApe.cod_tipo_tasa;
                        }
                        else // HISTORICO
                        {
                            if (vApe.tipo_historico != 0)
                                ctlTasaInteresRenova.TipoHistorico = Convert.ToInt32(vApe.tipo_historico);
                            if (vApe.desviacion != 0)
                                ctlTasaInteresRenova.Desviacion = Convert.ToDecimal(vApe.desviacion);
                        }
                    }
                }

            }

        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.codigoprogramarenovacioncdat, "btnGuardar_Click", ex);
        }
    }

    protected void CALCULAR()
    {

        //txttotalapagar.Text = Convert.ToString(Convert.ToInt32(txtValor.Text) - Convert.ToInt32(txtretencion.Text) - Convert.ToInt32(txtmenosgmf.Text) + Convert.ToInt32(txtinteres.Text));


    }
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        // ctlTasaInteresRenova.Inicializar();
        try
        {
            // Datos de la operación
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];
            Xpinn.Tesoreria.Entities.Operacion operacion = new Xpinn.Tesoreria.Entities.Operacion();
            operacion.cod_ope = 0;
            operacion.tipo_ope = 75;
            operacion.cod_caja = 0;
            operacion.cod_cajero = 0;
            operacion.observacion = "operacion realizada";
            operacion.cod_proceso = null;
            operacion.fecha_oper = Convert.ToDateTime(txtFecha.Text);
            operacion.fecha_calc = DateTime.Now;
            operacion.cod_ofi = vUsuario.cod_oficina;


            mvPrincipal.ActiveViewIndex = 1;
            Cdat Data = new Cdat();
            Data = AperturaService.ConsultarNumeracionCDATS(Data, (Usuario)Session["usuario"]);

            //OBTENER LOS DATOS DEL TITULAR PRINCIPAL
            string pIdentificacion = string.Empty;
            Int64 pCodpersona = 0;
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
            if (TextBox11.Text == "")
            {
                if (Data.valor == 1)
                {
                    vApert.opcion = 1;
                    string pError = "";
                    string autogenerado = BONumeracionCuentaCDAT.ObtenerCodigoParametrizado(3, pIdentificacion, pCodpersona, ddlTipoLinea.SelectedValue, ref pError, vUsuario);
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
                    TextBox11.Visible = false;
                    TextBox22.Visible = false;
                    Label11.Visible = true;

                }
                else
                {
                    vApert.opcion = 0;//NO AUTOGENERE
                    vApert.numero_cdat = TextBox11.Text;
                    TextBox11.Visible = true;
                }
            }
            else
            {
                vApert.opcion = 0;
                vApert.numero_cdat = TextBox11.Text;
            }

            vApert.codigo_cdat = 0;
            if (txtNumPreImpresos.Text != "")
                vApert.numero_fisico = txtNumPreImpresos.Text;
            else
                vApert.numero_fisico = null;

            vApert.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);
            if (ddlTipoLinea.SelectedIndex != 0)
                vApert.cod_lineacdat = ddlTipoLinea.SelectedValue;
            else
                vApert.cod_lineacdat = null;

            if (ddlDestinacions.SelectedIndex != 0)
                vApert.cod_destinacion = Convert.ToInt64(ddlDestinacions.SelectedValue);
            else
                vApert.cod_destinacion = 0;

            vApert.fecha_apertura = Convert.ToDateTime(this.txtfechaApRenova.Text);
            vApert.fecha_inicio = Convert.ToDateTime(txtfechaApRenova.Text);
            vApert.modalidad = ddlModalidads.SelectedValue;

            if (ddlFormaCaptacions.SelectedIndex != 0)
                vApert.codforma_captacion = Convert.ToInt32(ddlFormaCaptacions.SelectedValue);
            else
                vApert.codforma_captacion = 0;
            vApert.plazo = Convert.ToInt32(txtplazorenova.Text);
            vApert.tipo_calendario = Convert.ToInt32(ddlcalendarioRenova.SelectedValue);
            vApert.cod_asesor_com = Convert.ToInt32(DdlAsesorRenova.SelectedValue);
            vApert.valor = Convert.ToDecimal(txvalorrenovado.Text);
            vApert.cod_moneda = Convert.ToInt32(ddlmonedarenovado.SelectedValue);
            vApert.fecha_vencimiento = Convert.ToDateTime(txtfechaVenciRemova.Text);

            if (rblModalidadInt.SelectedItem != null)
                vApert.modalidad_int = Convert.ToInt32(rblModalidadInt.SelectedValue);
            else
                vApert.modalidad_int = 0;

            if (ddlPeriodicidadRenova.SelectedItem != null)
            {
                ddlPeriodicidadRenova.SelectedValue = vApert.cod_periodicidad_int.ToString();
                //ddlPeriodicidad_SelectedIndexChanged(ddlPeriodicidadRenova, null);
                if (vApert.cod_periodicidad_int == null)
                {
                    vApert.cod_periodicidad_int = 0;
                }
            }
            else
            {
                vApert.cod_periodicidad_int = 0;
            }

            vApert.modalidad = ddlmonedarenovado.SelectedValue;
            vApert.fecha_intereses = Convert.ToDateTime(this.txtfechaApRenova.Text);
            vApert.estado = 2; // por defecto en estado de APERTURA   

            vApert.tipo_interes = ctlTasaInteresRenova.FormaTasa;
            if (ctlTasaInteresRenova.Indice == 0)//NIGUNA
            {
                vApert.tipo_historico = 0;
                vApert.desviacion = 0;
                vApert.tasa_interes = 0;
                vApert.cod_tipo_tasa = 0;
            }
            else if (ctlTasaInteresRenova.Indice == 1)//FIJO
            {
                vApert.tipo_historico = 0;
                vApert.desviacion = 0;
                if (ctlTasaInteresRenova.Tasa != 0)
                    vApert.tasa_interes = ctlTasaInteresRenova.Tasa;
                vApert.cod_tipo_tasa = ctlTasaInteresRenova.TipoTasa;
            }
            else // HISTORICO
            {
                vApert.cod_tipo_tasa = 0;
                vApert.tipo_historico = ctlTasaInteresRenova.TipoHistorico;
                if (ctlTasaInteresRenova.Desviacion != 0)
                    vApert.desviacion = ctlTasaInteresRenova.Desviacion;
            }

            vApert.lstDetalle = new List<Detalle_CDAT>();
            vApert.lstDetalle = ObtenerListaDetalle();

            // Datos del cierre del CDAT a renovar
            AdministracionCDAT traslado_cuenta = new AdministracionCDAT();
            traslado_cuenta.valor = Convert.ToDecimal(txtValor.Text);
            traslado_cuenta.retencion = Convert.ToDecimal(txtretencion.Text);
            traslado_cuenta.intereses = Convert.ToDecimal(txtinteres.Text);
            traslado_cuenta.intereses_cap = Convert.ToDecimal(txtinteres.Text);
            traslado_cuenta.codigo_gmf = Convert.ToDecimal(txtmenosgmf.Text);
            traslado_cuenta.numero_cdat = Convert.ToString(txtNumCDAT.Text);
            traslado_cuenta.fecha_vencimiento = Convert.ToDateTime(txtFecha.ToDate);

            Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
            Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();



            List<Beneficiario> lstBeneficiariosCdat = new List<Beneficiario>();



            vApert.estado = 2;
            vApert.origen = 1;
            vApert.cdat_renovado = txtCodigo.Text;
            vApert.cod_persona = pCodpersona;

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

            //DATOS DE LA OPERACION
            Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
            vOpe.cod_ope = 0;
            vOpe.tipo_ope = 11;
            vOpe.cod_caja = 0;
            vOpe.cod_cajero = 0;
            vOpe.observacion = "Operacion-Renovacion CDAT " + this.txtNumCDAT.Text + " y apertura Cdat No." + vApert.numero_cdat;
            vOpe.cod_proceso = null;
            vOpe.fecha_oper = Convert.ToDateTime(txtFecha.Texto);
            vOpe.fecha_calc = DateTime.Now;
            vOpe.cod_ofi = pUsuario.cod_oficina;

            Int64 COD_OPE = 0, COD_PERSONA = 0;


            //GRABACION DEL GIRO A REALIZAR
            // Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();

            Xpinn.FabricaCreditos.Services.AvanceService AvancServices = new Xpinn.FabricaCreditos.Services.AvanceService();
            Usuario pusu = (Usuario)Session["usuario"];
            Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();
            pGiro.idgiro = 0;
            pGiro.cod_persona = pCodpersona;
            pGiro.forma_pago = Convert.ToInt32(giro.ValueFormaDesem);
            pGiro.tipo_acto = 7;
            pGiro.fec_reg = Convert.ToDateTime(txtFecha.Texto);
            pGiro.fec_giro = DateTime.Now;
            pGiro.numero_radicacion = 0;
            pGiro.usu_gen = pusu.nombre;
            pGiro.usu_apli = null;
            pGiro.estadogi = 0;
            pGiro.usu_apro = null;
            if (giro.IndiceFormaDesem == 1) //"eFECTIVO"
            {
                pGiro.idctabancaria = 0;
                pGiro.cod_banco = 0;
                pGiro.num_cuenta = null;
                pGiro.tipo_cuenta = -1;
            }
            if (giro.IndiceFormaDesem != 1) //"eFECTIVO"
            {

                //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(giro.ValueEntidadOrigen), giro.TextCuentaOrigen, (Usuario)Session["usuario"]);
                Int64 idCta = CuentaBanc.idctabancaria;
                //DATOS DE FORMA DE PAGO
                if (giro.IndiceFormaDesem == 3) //"Transferencia"
                {
                    pGiro.idctabancaria = idCta;
                    pGiro.cod_banco = Convert.ToInt32(giro.ValueEntidadDest);
                    pGiro.num_cuenta = giro.TextNumCuenta;
                    pGiro.tipo_cuenta = Convert.ToInt32(giro.ValueTipoCta);
                }
                else if (giro.IndiceFormaDesem == 2) //Cheque
                {
                    pGiro.idctabancaria = idCta;
                    pGiro.cod_banco = 0;        //NULO
                    pGiro.num_cuenta = null;    //NULO
                    pGiro.tipo_cuenta = -1;      //NULO
                }
                else
                {
                    pGiro.idctabancaria = 0;
                    pGiro.cod_banco = 0;
                    pGiro.num_cuenta = null;
                    pGiro.tipo_cuenta = -1;
                }
            }

            pGiro.fec_apro = DateTime.MinValue;
            pGiro.cob_comision = 0;
            pGiro.valor = Convert.ToInt64(txttotalapagar.Text.Replace(".", ""));

            //DATOS DE CIERRE LIQUIDACION
            LiquidacionCDAT pLiqui = new LiquidacionCDAT();
            pLiqui.fecha_liquidacion = Convert.ToDateTime(txtFecha.Texto); //FECHA DE CIERRE
            pLiqui.numero_cdat = txtNumCDAT.Text;
            pLiqui.valor = Convert.ToDecimal(txtValor.Text.Replace(".", ""));
            /*  pLiqui.interes_causado = Convert.ToDecimal(txtinteres.Text.Replace(".", ""));
                pLiqui.interes = Convert.ToDecimal(txtinteres.Text.Replace(".", ""));
                pLiqui.retencion = Convert.ToDecimal(txtretencion.Text.Replace(".", ""));
                pLiqui.valor_gmf = Convert.ToDecimal(txtmenosgmf.Text.Replace(".", ""));
                pLiqui.valor_pagar = Convert.ToDecimal(txttotalapagar.Text.Replace(".", ""));
              */


            if (!string.IsNullOrWhiteSpace(txttotalapagar.Text))
                pLiqui.valor = Convert.ToInt64(txttotalapagar.Text.Replace(".", ""));

            if (!string.IsNullOrWhiteSpace(txtinteres.Text))
                pLiqui.interes = Convert.ToInt64(txtinteres.Text.Replace(".", ""));

            if (!string.IsNullOrWhiteSpace(txtretencion.Text))
                pLiqui.retencion = Convert.ToInt64(txtretencion.Text.Replace(".", ""));

            if (!string.IsNullOrWhiteSpace(txtmenosgmf.Text))
                pLiqui.valor_gmf = 0;
            pLiqui.valor_gmf = Convert.ToInt64(txtmenosgmf.Text.Replace(".", ""));

            pLiqui.capitalizar_int = cbCapitalizaInteres.Checked == true ? 1 : 0;

            vApert.numero_cuenta = "0";

            string sError = string.Empty;
            LiquiService.CierreLiquidacionCDAT(ref COD_OPE, ref sError, vOpe, true, giro.IndiceFormaDesem, pGiro, pLiqui, (Usuario)Session["usuario"]);
            if (!string.IsNullOrEmpty(sError))
            {
                VerError(sError);
                return;
            }
            vApert.cod_ope = vOpe.cod_ope;
            vApert.origen = 1;
            vApert.capitalizar_int_old = Convert.ToInt32(pLiqui.capitalizar_int);
            vApert.valor_capitalizar = Convert.ToDecimal(txttotalapagar.Text);
            vApert.cod_lineacdat = (ddlLineaRenova.SelectedValue);
            
            AperturaService.CrearAperturaCDAT(vApert, (Usuario)Session["usuario"], lstBeneficiariosCdat);


            //GENERAR EL COMPROBANTE
            if (COD_OPE != 0)
            {
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = COD_OPE;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 11;
                Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.ParseExact(txtFecha.Texto, gFormatoFecha, null);
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = COD_PERSONA;
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }


            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarImprimir(false);
            toolBar.MostrarGuardar(true);

            mvPrincipal.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoProgramaCertificacion, "btnContinuarMen_Click", ex);
        }
    }



    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        if (mvPrincipal.ActiveViewIndex == 0)
        {
            Navegar(Pagina.Lista);
        }

        else
        {
            Navegar(Pagina.Nuevo);
        }
    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {

        mvPrincipal.Visible = true;
        Site toolBar = (Site)Master;
        toolBar.MostrarImprimir(true);
        toolBar.MostrarGuardar(true);

        mvPrincipal.ActiveViewIndex = 0;

    }

    protected void ddlPeriodicidadRenova_SelectedIndexChanged(object sender, EventArgs e)
    {

        Cdat Data = new Cdat();

        Data = AperturaService.ConsultarDiasPeriodicidad(Convert.ToInt32(ddlPeriodicidadRenova.SelectedValue), (Usuario)Session["usuario"]);

        if (Data.numdias != 0)
            txtDiasValida.Text = Data.numdias.ToString();
    }
    protected void ddlLineaRenova_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Generar Consulta de la Linea Seleccionada
        LineaCDAT vLineaCdat = new LineaCDAT();
        LineaCDATService lineacdatServicio = new LineaCDATService();
        if (this.ddlLineaRenova.SelectedValue == null || ddlLineaRenova.SelectedValue == "" || this.ddlLineaRenova.SelectedValue == "0")
        {
        }
        else
        {
            vLineaCdat = lineacdatServicio.ConsultarLineaCDAT(Convert.ToString(ddlLineaRenova.SelectedValue), (Usuario)Session["usuario"]);

            if (vLineaCdat.tipo_calendario != null)
            {
                ddlcalendarioRenova.SelectedValue = Convert.ToString(vLineaCdat.tipo_calendario);
                //lbltipocalendario.Visible = true;
                ddlcalendarioRenova.Visible = true;
                // lblplazo.Visible = true;
                txtplazorenova.Visible = true;

            }
            else
            {
                //lbltipocalendario.Visible = false;
                ddlcalendarioRenova.Visible = false;
                //lblplazo.Visible = false;
                txtplazorenova.Visible = false;
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
            ctlTasaInteresRenova.Inicializar();

            if (vLineaCdat.calculo_tasa != null)
            {
                //ctlTasaInteres.Inicializar();
                if (!string.IsNullOrEmpty(vLineaCdat.calculo_tasa.ToString()))
                    ctlTasaInteresRenova.FormaTasa = HttpUtility.HtmlDecode(vLineaCdat.calculo_tasa.ToString().Trim());
                if (!string.IsNullOrEmpty(vLineaCdat.tipo_historico.ToString()))
                    ctlTasaInteresRenova.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaCdat.tipo_historico.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaCdat.desviacion.ToString()))
                    ctlTasaInteresRenova.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaCdat.desviacion.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaCdat.cod_tipo_tasa.ToString()))
                    ctlTasaInteresRenova.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaCdat.cod_tipo_tasa.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaCdat.tasa.ToString()))
                    ctlTasaInteresRenova.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaCdat.tasa.ToString().Trim()));

            }
        }
    }
    protected void txtplazorenova_TextChanged(object sender, EventArgs e)
    {
        DateTime fechavencimiento = DateTime.Now;
        Int32 plazo = Convert.ToInt32(txtplazorenova.Text);
        Int32 tipocalendario = Convert.ToInt32(ddlcalendarioRenova.SelectedValue);
        DateTime fecha_proximo_pago = Convert.ToDateTime(txtfechaApRenova.Text.ToString());
        if (plazo > 365 && tipocalendario == 1)
        {
            lblError.Visible = true;
            lblError.Text = "El plazo no puede superar los 365 dias del año";

        }
        else
        {
            lblError.Visible = false;
            fechavencimiento = AperturaService.Calcularfecha(fechavencimiento, fecha_proximo_pago, plazo, Convert.ToInt32(tipocalendario));
            txtfechaVenciRemova.Text = Convert.ToString(fechavencimiento);
        }
    }
}