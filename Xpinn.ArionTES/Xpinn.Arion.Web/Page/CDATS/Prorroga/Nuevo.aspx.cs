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


partial class Nuevo : GlobalWeb
{

    AperturaCDATService AperturaService = new AperturaCDATService();
    ProrrogaCDATService ProrrService = new ProrrogaCDATService();
    LiquidacionCDATService LiquiService = new LiquidacionCDATService();
    int _tipoOpe = 9;

    int periodicidad;
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ProrrService.CodigoProgramaPRO + ".id"] != null)
                VisualizarOpciones(ProrrService.CodigoProgramaPRO, "E");
            else
                VisualizarOpciones(ProrrService.CodigoProgramaPRO, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;

            ctlMensaje.eventoClick += btnContinuarMen_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProrrService.CodigoProgramaPRO, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["DatosDetalle"] = null;
            PanelBloqueo.Enabled = false;
            if (!Page.IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;

                cargarDropdown();
                if (Session[ProrrService.CodigoProgramaPRO + ".id"] != null)
                {
                    idObjeto = Session[ProrrService.CodigoProgramaPRO + ".id"].ToString();
                    Session.Remove(ProrrService.CodigoProgramaPRO + ".id");
                    ObtenerDatos(idObjeto);
                    lblMsj.Text = " Creada";

                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProrrService.CodigoProgramaPRO, "Page_Load", ex);
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




    void cargarDropdown()
    {
        ctlTasaInteres.Inicializar();
        ctlTasaPro.Inicializar();
        ctlGiro.Inicializar();
        Cdat Data = new Cdat();
        List<Cdat> lstTipoLinea = new List<Cdat>();

        lstTipoLinea = AperturaService.ListarTipoLineaCDAT(Data, (Usuario)Session["usuario"]);
        if (lstTipoLinea.Count > 0)
        {
            ddlTipoLinea.DataSource = lstTipoLinea;
            ddlTipoLinea.DataTextField = "NOMBRE";
            ddlTipoLinea.DataValueField = "COD_LINEACDAT";
            ddlTipoLinea.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoLinea.SelectedIndex = 0;
            ddlTipoLinea.DataBind();
        }

        ddlModalidad.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
        ddlModalidad.Items.Insert(1, new ListItem("INDIVIDUAL", "IND"));
        ddlModalidad.Items.Insert(2, new ListItem("CONJUNTA", "CON"));
        ddlModalidad.Items.Insert(3, new ListItem("ALTERNA", "ALT"));
        ddlModalidad.SelectedIndex = 0;
        ddlModalidad.DataBind();

        PoblarLista("Tipomoneda", ddlTipoMoneda);

        ddlTipoCalendario.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoCalendario.Items.Insert(1, new ListItem("Comercial", "1"));
        ddlTipoCalendario.Items.Insert(2, new ListItem("Calendario", "2"));
        ddlTipoCalendario.SelectedIndex = 0;
        ddlTipoCalendario.DataBind();


        List<Cdat> lstOficina = new List<Cdat>();

        lstOficina = AperturaService.ListarOficinas(Data, (Usuario)Session["usuario"]);
        if (lstOficina.Count > 0)
        {
            ddlOficina.DataSource = lstOficina;
            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "COD_OFICINA";
            ddlOficina.Items.Insert(0, new ListItem("Seleccione un item", "0"));
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
    }



    //Eventos Grilla

    protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlConjuncion = (DropDownListGrid)e.Row.FindControl("ddlConjuncion");
            if (ddlConjuncion != null)
            {
                ddlConjuncion.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlConjuncion.Items.Insert(1, new ListItem("Y", "Y"));
                ddlConjuncion.Items.Insert(2, new ListItem("O", "O"));
            }


            Label lblConjuncion = (Label)e.Row.FindControl("lblConjuncion");
            if (lblConjuncion != null)
                ddlConjuncion.SelectedValue = lblConjuncion.Text;

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
            foreach (Detalle_CDAT Deta in LstDeta)
            {
                if (Deta.cod_usuario_cdat == conseID)
                {
                    LstDeta.Remove(Deta);
                    break;
                }
            }
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
            TextBoxGrid txtIdentificacion = (TextBoxGrid)gvDetalle.Rows[rowIndex].FindControl("txtIdentificacion");
            TextBox lblNombre = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblNombre");
            //TextBox lblApellidos = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblApellidos");
            ctlListadoPer.Motrar(true, "txtIdentificacion", "lblNombre"); //Recupera el nombre completo
            txtIdentificacion_TextChanged(txtIdentificacion, null);
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
            {
                eDeta.principal = 1;
                txtcodigotitular.Text = Convert.ToString(eDeta.cod_persona);
            }
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

            if (eDeta.cod_persona != 0 && eDeta.cod_persona != null && eDeta.nombres.Trim() != null && eDeta.apellidos.Trim() != null)
            {
                lstDetalle.Add(eDeta);
                Session["DTAPERTURA"] = lstDetalle; // CAPTURA DATOS PARA IMPRESION
            }
        }

        return lstDetalle;
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            txtfechaProrr.Text = Convert.ToString(DateTime.Now);
            Cdat vApe = new Cdat();

            vApe = AperturaService.ConsultarApertu(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (vApe.codigo_cdat != 0) txtCodigo.Text = vApe.codigo_cdat.ToString();

            if (vApe.numero_cdat != "") txtNumCDAT.Text = vApe.numero_cdat;

            if (vApe.fecha_apertura != DateTime.MinValue) txtFechaApertura.Text = vApe.fecha_apertura.ToShortDateString();
            if (vApe.fecha_vencimiento != DateTime.MinValue) txtFechaVenci.Text = vApe.fecha_vencimiento.ToShortDateString();

            if (vApe.cod_lineacdat != "") ddlTipoLinea.SelectedValue = vApe.cod_lineacdat;

            if (vApe.valor != 0) txtValor.Text = vApe.valor.ToString();

            if (vApe.cod_moneda != 0) ddlTipoMoneda.SelectedValue = vApe.cod_moneda.ToString();

            if (vApe.plazo != 0) txtPlazo.Text = vApe.plazo.ToString();

            if (vApe.tipo_calendario != 0) ddlTipoCalendario.SelectedValue = vApe.tipo_calendario.ToString();

            if (vApe.cod_oficina != 0) ddlOficina.SelectedValue = vApe.cod_oficina.ToString();

            if (vApe.modalidad != "" && vApe.modalidad != null) ddlModalidad.SelectedValue = vApe.modalidad;

            if (vApe.cod_periodicidad_int != 0 && vApe.cod_periodicidad_int != null)
            {
                ddlPeriodicidad.SelectedValue = vApe.cod_periodicidad_int.ToString();
                ddlPeriodicidad_SelectedIndexChanged(ddlPeriodicidad, null);
            }
            if (vApe.cod_periodicidad_int == 0 && vApe.cod_periodicidad_int != null)
            {
                ddlPeriodicidad.Visible = false;
                Txtperiodicidad.Visible = true;
                Txtperiodicidad.Text = "VENCIMIENTO";

            }
            else
            {
                if (vApe.cod_periodicidad_int != 0 && vApe.cod_periodicidad_int == null)
                {
                    ddlPeriodicidad.Visible = false;
                    Txtperiodicidad.Visible = true;
                    Txtperiodicidad.Text = "VENCIMIENTO";
                }
            }


            //Generar Consulta de la Linea Seleccionada
            LineaCDAT vLineaCdat = new LineaCDAT();
            LineaCDATService linahorroServicio = new LineaCDATService();
            vLineaCdat = linahorroServicio.ConsultarLineaCDAT(Convert.ToString(vApe.cod_lineacdat), (Usuario)Session["usuario"]);

            if (vLineaCdat.interes_prroroga == 0)
            {
                cbInteresCuenta.Enabled = false;
                PanelTasaPro.Enabled = false;
            }
            if (vLineaCdat.interes_prroroga == 1)
            {
                PanelTasaPro.Enabled = true;
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

            if (vApe.tipo_interes != null)
            {
                if (!string.IsNullOrEmpty(vApe.tipo_interes.ToString()))
                    ctlTasaPro.FormaTasa = HttpUtility.HtmlDecode(vApe.tipo_interes.ToString().Trim());
                if (!string.IsNullOrEmpty(vApe.tipo_historico.ToString()))
                    ctlTasaPro.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vApe.tipo_historico.ToString().Trim()));
                if (!string.IsNullOrEmpty(vApe.desviacion.ToString()))
                    ctlTasaPro.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vApe.desviacion.ToString().Trim()));
                if (!string.IsNullOrEmpty(vApe.cod_tipo_tasa.ToString()))
                    ctlTasaPro.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vApe.cod_tipo_tasa.ToString().Trim()));
                if (!string.IsNullOrEmpty(vApe.tasa_interes.ToString()))
                    ctlTasaPro.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vApe.tasa_interes.ToString().Trim()));
            }
            txtfechaProrr.Text = txtFechaVenci.Text;
            txtPlazoPro.Text = (this.txtPlazo.Text);
            txtPlazoPro_TextChanged(txtPlazoPro.Text, null);
            txtfechaInt.Text = vApe.fecha_intereses != DateTime.MinValue ? vApe.fecha_intereses.ToString() : "";
            txtfechaInt.Enabled = vApe.fecha_intereses != DateTime.MinValue ? false : true;

            //Session["FECHA_INICIO"] = DateTime.MinValue;
            //if (vApe.fecha_inicio != DateTime.MinValue)
            //    Session["FECHA_INICIO"] = vApe.fecha_inicio;


            //RECUPERAR GRILLA DETALLE 
            List <Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();

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
                        txtcodigotitular.Text = Convert.ToString(pCodpersona);
                        TextBoxGrid txtIdentificacion = (TextBoxGrid)rFila.FindControl("txtIdentificacion");
                        if (txtIdentificacion != null)
                            if (txtIdentificacion.Text != "")
                                pIdentificacion = txtIdentificacion.Text.Trim();
                        break;
                    }
                }
            }
            
            //RECUPERAR   liquida INTERES 
            Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
            Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
            pData = ConsultaData.ConsultarGeneral(579, (Usuario)Session["usuario"]);
            Int64 liquida =Convert.ToInt64(pData.valor);
            if (liquida == 0)
            {
                PanelLiquida.Visible = false;
            }
            if (liquida == 1)
            {
                Int64 COD_PERSONA = 0;

                ObtenerListaDetalle();
                COD_PERSONA = Buscar_Titular();
                ctlGiro.cargarCuentasAhorro(COD_PERSONA);

                LiquidarCDAT();
                PanelLiquida.Visible = true;
            }

            ddlModalidad_SelectedIndexChanged(ddlModalidad, null);
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProrrService.CodigoProgramaPRO, "ObtenerDatos", ex);
        }
    }
    protected Int64 Buscar_Titular()
    {
        Int64 codigo = 0;
        int cont = 0;
        foreach (GridViewRow rFila in gvDetalle.Rows)
        {
            CheckBoxGrid chkPrincipal = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
            TextBox lblcod_persona = (TextBox)rFila.FindControl("lblcod_persona");

            if (chkPrincipal.Checked)
                cont++;

            if (cont == 1)
            {
                string cod = "";
                cod = lblcod_persona.Text;
                if (cod != "")
                    codigo = Convert.ToInt64(cod);
                Session["Titular"] = codigo;
                break;
            }
        }
        return codigo;
    }

    Boolean ValidarProcesoContable()
    {
        Usuario vUsuario = new Usuario();
        vUsuario = (Usuario)Session["Usuario"];
        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();

        // Valida que exista parametrización contable para la operación        
        List<Xpinn.Contabilidad.Entities.ProcesoContable> LstProcesoContable;
        LstProcesoContable = ComprobanteServicio.ConsultaProceso(0, _tipoOpe, DateTime.Now, vUsuario);
        if (LstProcesoContable.Count() == 0)
        {
            VerError("No existen comprobantes parametrizados para esta operación (Tipo " + _tipoOpe + "=Prorroga de Cdat)");
            return false;
        }
          
        Int64 pcod_proceso = Convert.ToInt64(LstProcesoContable[0].cod_proceso);
        Int64 pnum_comp = 0;
        Int64 ptipo_comp = 0;
      return true;
    }
    protected void LiquidarCDAT()
    {
        LiquidacionCDAT pLiqui = new LiquidacionCDAT();
        LiquidacionCDAT entidad = new LiquidacionCDAT();

        if (txtfechaProrr.Texto == "")
        {
            VerError("Ingrese la fecha de Cierre para realizar Prorroga.");
            return;
        }
        pLiqui.fecha_liquidacion = Convert.ToDateTime(txtfechaProrr.Texto);
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
            this.txttotalapagar.Text = Convert.ToString( entidad.interes - entidad.retencion - entidad.valor_gmf).ToString();
    }


    Boolean ValidarDatos()
    {
        if (txtfechaProrr.Text == "")
        {
            VerError("Ingrese la Fecha de Prorroga");
            return false;
        }

        if (txtPlazoPro.Text == "")
        {
            VerError("Ingrese el Nuevo Plazo");
            return false;
        }

        if (txtFechaVenciPro.Text == "")
        {
            VerError("Ingrese la nueva Fecha de vencimiento");
            return false;
        }

        if (Convert.ToDateTime(txtfechaProrr.Text) > Convert.ToDateTime(txtFechaVenciPro.Text))
        {
            VerError("No puede Ingresar una fecha de vencimiento menor a la nueva fecha de Prorroga");
            return false;
        }
        if (txtfechaInt.Text.Trim() == "")
        {
            VerError("Debe ingresar la fecha de interes anterior para realizar la prorroga");
            return false;
        }
        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarProcesoContable())
            {
                if (ValidarDatos())
                {
                    ctlMensaje.MostrarMensaje("Desea generar la prorroga?");
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProrrService.CodigoProgramaPRO, "btnGuardar_Click", ex);
        }
    }
    

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Int64 COD_OPE = 0, COD_PERSONA = 0;
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        Xpinn.Tesoreria.Services.OperacionServices xTesoreria = new Xpinn.Tesoreria.Services.OperacionServices();
        Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
        Usuario vUsuario = new Usuario();
        Detalle_CDAT pVar = new Detalle_CDAT();
        vUsuario = (Usuario)Session["Usuario"];
        bool pGenerarGiro = false;
        try
        {

            //CDAT A MODIFICAR
            Cdat vApert = new Cdat();

            vApert.codigo_cdat = Convert.ToInt64(txtCodigo.Text);
            vApert.fecha_inicio = Convert.ToDateTime(txtfechaProrr.Text);
            vApert.plazo = Convert.ToInt32(txtPlazoPro.Text);
            if (txtFechaVenciPro.Text != "")
                vApert.fecha_vencimiento = Convert.ToDateTime(txtFechaVenciPro.Text);
            else
                vApert.fecha_vencimiento = DateTime.MinValue;

            //datos de la tasa
            vApert.tipo_interes = ctlTasaPro.FormaTasa;
            if (ctlTasaPro.Indice == 0)//NIGUNA
            {
                vApert.tipo_historico = 0;
                vApert.desviacion = 0;
                vApert.tasa_interes = 0;
                vApert.cod_tipo_tasa = 0;
            }
            else if (ctlTasaPro.Indice == 1)//FIJO
            {
                vApert.tipo_historico = 0;
                vApert.desviacion = 0;
                if (ctlTasaPro.Tasa != 0)
                    vApert.tasa_interes = ctlTasaPro.Tasa;
                vApert.cod_tipo_tasa = ctlTasaPro.TipoTasa;
            }
            else // HISTORICO
            {
                vApert.cod_tipo_tasa = 0;
                vApert.tipo_historico = ctlTasaPro.TipoHistorico;
                if (ctlTasaPro.Desviacion != 0)
                    vApert.desviacion = ctlTasaPro.Desviacion;
            }

            if (ddlPeriodicidad.SelectedIndex != 0)
                vApert.cod_periodicidad_int = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
            else
                vApert.cod_periodicidad_int = 0;
            vApert.fecha_intereses = Convert.ToDateTime(txtfechaProrr.Text); // Convert.ToDateTime(txtfechaInt.Text); 

            //GRABACION DE DATOS ANTERIORES EN TABLA PRORROGA
            ProrrogaCDAT vPro = new ProrrogaCDAT();

            vPro.cod_prorroga = 0;
            vPro.codigo_cdat = Convert.ToInt64(txtCodigo.Text);
            vPro.fecha_inicio = Convert.ToDateTime(txtfechaProrr.Text);
            vPro.fecha_final = Convert.ToDateTime(txtFechaVenciPro.Text);
            if (ddlPeriodicidad.SelectedIndex != 0)
                vPro.cod_periodicidad_int = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
            
            //datos de la tasa
            vPro.tipo_interes = ctlTasaPro.FormaTasa;
            if (ctlTasaPro.Indice == 0)//NIGUNA
            {
                vPro.tipo_historico = 0;
                vPro.desviacion = 0;
                vPro.tasa_interes = 0;
                vPro.cod_tipo_tasa = 0;
            }
            else if (ctlTasaPro.Indice == 1)//FIJO
            {
                vPro.tipo_historico = 0;
                vPro.desviacion = 0;
                if (ctlTasaPro.Tasa != 0)
                    vPro.tasa_interes = ctlTasaPro.Tasa;
                vPro.cod_tipo_tasa = ctlTasaPro.TipoTasa;
            }
            else // HISTORICO
            {
                vPro.cod_tipo_tasa = 0;
                vPro.tipo_historico = ctlTasaPro.TipoHistorico;
                if (ctlTasaPro.Desviacion != 0)
                    vPro.desviacion = ctlTasaPro.Desviacion;
            }           

            //MODIFICACION DE AUDITORIA
            CDAT_AUDITORIA Audi = new CDAT_AUDITORIA();
            Usuario vUsu = (Usuario)Session["usuario"];
            Audi.cod_auditoria_cdat = 0;
            Audi.codigo_cdat = vApert.codigo_cdat;
            Audi.tipo_registro_aud = 3;
            Audi.fecha_aud = DateTime.Now;
            Audi.cod_usuario_aud = vUsu.codusuario;
            Audi.ip_aud = vUsu.IP;

            //GRABACION DE LA OPERACION
            vOpe.cod_ope = 0;
            vOpe.tipo_ope = _tipoOpe;
            //COD USUARIO EN CAPA DATOS
            //COD OFICINA EN CAPA DATOS
            vOpe.cod_caja = 0;
            vOpe.cod_cajero = 0;
            vOpe.observacion = null;
            vOpe.cod_proceso = null;
            vOpe.fecha_oper = Convert.ToDateTime(DateTime.Now);
            vOpe.fecha_calc = Convert.ToDateTime(txtfechaProrr.Text);
            var usu = vUsuario.codusuario;

            //GRABACION DEL GIRO A REALIZAR
            Xpinn.FabricaCreditos.Services.AvanceService AvancServices = new Xpinn.FabricaCreditos.Services.AvanceService();
            Usuario pusu = (Usuario)Session["usuario"];
            Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();
            pGiro.idgiro = 0;
           
            pGiro.cod_persona = Convert.ToInt64(txtcodigotitular.Text);
            pGiro.forma_pago = Convert.ToInt32(ctlGiro.ValueFormaDesem);
            pGiro.tipo_acto = 7;
            pGiro.fec_reg = Convert.ToDateTime(DateTime.Now);
            pGiro.fec_giro = DateTime.Now;
            pGiro.numero_radicacion = 0;
            pGiro.usu_gen = pusu.nombre;
            pGiro.usu_apli = null;
            pGiro.estadogi = 0;
            pGiro.usu_apro = null;
            if (ctlGiro.IndiceFormaDesem == 1) //"eFECTIVO"
            {
                pGiro.idctabancaria = 0;
                pGiro.cod_banco = 0;
                pGiro.num_cuenta = null;
                pGiro.tipo_cuenta = -1;
                pGenerarGiro = true;
            }
            if (ctlGiro.IndiceFormaDesem != 1 && ctlGiro.IndiceFormaDesem != 4) //"eFECTIVO"
            {

                //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ctlGiro.ValueEntidadOrigen), ctlGiro.TextCuentaOrigen, Usuario);
                Int64 idCta = CuentaBanc.idctabancaria;
                //DATOS DE FORMA DE PAGO
                if (ctlGiro.IndiceFormaDesem == 3) //"Transferencia"
                {
                    pGiro.idctabancaria = idCta;
                    pGiro.cod_banco = Convert.ToInt32(ctlGiro.ValueEntidadDest);
                    pGiro.num_cuenta = ctlGiro.TextNumCuenta;
                    pGiro.tipo_cuenta = Convert.ToInt32(ctlGiro.ValueTipoCta);
                    pGenerarGiro = true;
                }
                else if (ctlGiro.IndiceFormaDesem == 2) //Cheque
                {
                    pGiro.idctabancaria = idCta;
                    pGiro.cod_banco = 0;        //NULO
                    pGiro.num_cuenta = null;    //NULO
                    pGiro.tipo_cuenta = -1;      //NULO
                    pGenerarGiro = true;
                }
                else if (ctlGiro.IndiceFormaDesem == 4)
                {
                    pGenerarGiro = false;
                    pVar.numero_cuenta_ahorro_vista = !string.IsNullOrWhiteSpace(ctlGiro.ValueCuentaAhorro) ? Convert.ToInt64(ctlGiro.ValueCuentaAhorro) : default(long?);
                    if (!pVar.numero_cuenta_ahorro_vista.HasValue)
                    {
                        VerError("No tiene una cuenta de ahorro a la vista asociada para esta forma de pago!.");
                    }
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

            if (!string.IsNullOrWhiteSpace(txttotalapagar.Text))
            {
                pGiro.valor = Convert.ToInt64(this.txttotalapagar.Text.Replace(".", ""));
            }
            
            pGiro.cod_ope = 0;
            // AvancServices.CrearGiro(pGiro, (Usuario)Session["usuario"], 1);

            LiquidacionCDAT pLiqui = new LiquidacionCDAT();
            pLiqui.fecha_int = vPro.fecha_inicio; // Convert.ToDateTime(DateTime.Now);
            pLiqui.codigo_cdat = Convert.ToInt64(txtCodigo.Text);
            pLiqui.cod_ope = vOpe.cod_ope;

            if (!string.IsNullOrWhiteSpace(txttotalapagar.Text))
                pLiqui.valor = Convert.ToInt64(txttotalapagar.Text.Replace(".", ""));

            if (!string.IsNullOrWhiteSpace(txtinteres.Text))
                pLiqui.interes = Convert.ToInt64(txtinteres.Text.Replace(".", ""));

            if (!string.IsNullOrWhiteSpace(txtretencion.Text))
                pLiqui.retencion = Convert.ToInt64(txtretencion.Text.Replace(".", ""));

            if (!string.IsNullOrWhiteSpace(txtmenosgmf.Text))
                pLiqui.valor_gmf= 0;
                pLiqui.valor_gmf = Convert.ToInt64(txtmenosgmf.Text.Replace(".", ""));

            pLiqui.capitalizar_int = cbCapitalizaInteres.Checked == true ? 1 : 0;
                     
            ctlGiro.cargarCuentasAhorro(Convert.ToInt64(txtcodigotitular.Text));
            if(ctlGiro.ValueCuentaAhorro != "" )
            { 
                pLiqui.numero_cuenta_ahorro_vista = Convert.ToInt64(ctlGiro.ValueCuentaAhorro);
            }
            pLiqui.valor_pagar = Convert.ToInt32(txttotalapagar.Text.Replace(".", ""));
            pLiqui.fecha_liquidacion = Convert.ToDateTime(txtfechaProrr.Texto);
            COD_PERSONA=Buscar_Titular();
            pLiqui.cod_deudor = Convert.ToInt64(COD_PERSONA);


            //consultar cierre historico
            String estado = "";
            DateTime fechacierrehistorico;
            DateTime fechaliquidacion =DateTime.Now;
            Xpinn.CDATS.Entities.LiquidacionCDAT vliquidacioncdat = new Xpinn.CDATS.Entities.LiquidacionCDAT();
            vliquidacioncdat = LiquiService.ConsultarCierreCdats((Usuario)Session["usuario"]);
            estado = vliquidacioncdat.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vliquidacioncdat.fecha_cierre.ToString());

            if (estado == "D" && fechaliquidacion <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO M,'CDAT'S'");
            }
            else
            {
                if (ctlGiro.Visible == false)
                {
                    pGenerarGiro = false;
                    lblgiro.Visible = false;
                    pVar.numero_cuenta_ahorro_vista = 0;
                    pGiro.valor = 0;
                }
                if (ctlGiro.Visible == true)
                {
                    pGenerarGiro = true;
                    lblgiro.Visible = true;
                }

                LiquiService.GuardarLiquidacionCDATPrroroga(pGenerarGiro, Convert.ToInt64(ctlGiro.IndiceFormaDesem), pGiro, ref COD_OPE, vApert, vPro, Audi, vOpe, pLiqui, Usuario);

                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);

                if (COD_OPE != 0)
                {
                    Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicios = new Xpinn.Contabilidad.Services.ComprobanteService();

                    Session[ComprobanteServicios.CodigoPrograma + ".cod_ope"] = COD_OPE;
                    Session[ComprobanteServicios.CodigoPrograma + ".tipo_ope"] = _tipoOpe;
                    Session[ComprobanteServicios.CodigoPrograma + ".cod_persona"] = Convert.ToInt64(COD_PERSONA); //"<Colocar Aquí el código de la persona del servicio>"
                    Session[ComprobanteServicios.CodigoPrograma + ".idgiro"] = pGiro.idgiro.ToString();
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                    // ctlproceso.CargarVariables(pGiro.cod_ope, Convert.ToInt32(126), Convert.ToInt64(Session["cod_persona"]), (Usuario)Session["Usuario"]);
                    //return true;
                    //Session[AhorroVistaServicio.CodigoProgramaReporteGMF + ".id"] = idObjeto;
                }
            }

            Site toolbar = (Site)Master;
            toolbar.MostrarGuardar(false);

            mvPrincipal.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProrrService.CodigoProgramaPRO, "btnContinuarMen_Click", ex);
        }
    }



    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    
    protected void txtPlazoPro_TextChanged(object sender, EventArgs e)
    {
        DateTime fechavencimiento = DateTime.Now;
        Int32 plazo = Convert.ToInt32(txtPlazoPro.Text);
        Int32 tipocalendario = Convert.ToInt32(ddlTipoCalendario.SelectedValue);
        DateTime fecha_proximo_pago = Convert.ToDateTime(this.txtfechaProrr.Text.ToString());

        if (ValidarPlazosMaximosYMinimos())
        {
            fechavencimiento = AperturaService.Calcularfecha(fechavencimiento, fecha_proximo_pago, plazo, tipocalendario);
            txtFechaVenciPro.Text = Convert.ToString(fechavencimiento);
        }
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

            Int32 plazoSolicitado = Convert.ToInt32(txtPlazoPro.Text);

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

    protected void ddlPeriodicidad_SelectedIndexChanged(object sender, EventArgs e)
    {

        Cdat Data = new Cdat();

        Data = AperturaService.ConsultarDiasPeriodicidad(Convert.ToInt32(ddlPeriodicidad.SelectedValue), (Usuario)Session["usuario"]);

        if (Data.numdias != 0)

            txtDiasValida.Text = Data.numdias.ToString();

    }
    

    protected void cbCapitalizaInteres_CheckedChanged(object sender, EventArgs e)
    {
        if (cbCapitalizaInteres.Checked == true)
        {
            ctlGiro.Visible = false;
            lblgiro.Visible = false;
        }
        if (cbCapitalizaInteres.Checked == false)
        {
            ctlGiro.Visible = true;
            lblgiro.Visible = true;
        }
    }
}