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
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

partial class Lista : GlobalWeb
{
    FusionGirosServices FusionService = new FusionGirosServices();
    Xpinn.Tesoreria.Services.GiroServices GiroServicio = new Xpinn.Tesoreria.Services.GiroServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {       
            VisualizarOpciones(FusionService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoCancelar += btnRegresar_Click;
            toolBar.MostrarCancelar(false);
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarConsultar(true);
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FusionService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                panelGrilla.Visible = false;
                CargarDropDown();
            }
            else
            { CalcularTotal(); }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FusionService.CodigoPrograma, "Page_Load", ex);
        }
    }


    void CargarDropDown()
    {
        PoblarListas PoblarLista = new PoblarListas();
        PoblarLista.PoblarListaDesplegable("TIPO_ACTO_GIRO","","","1", ddlGenerado, (Usuario)Session["usuario"]);

        PoblarLista.PoblarListaDesplegable("TIPO_ACTO_GIRO", "", "", "1", ddlTipoGiro, (Usuario)Session["usuario"]);

        ddlFormaPago.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlFormaPago.Items.Insert(1, new ListItem("Efectivo", "1"));
        ddlFormaPago.Items.Insert(2, new ListItem("Cheque", "2"));
        ddlFormaPago.Items.Insert(3, new ListItem("Transferencia", "3"));
        ddlFormaPago.Items.Insert(4, new ListItem("Giro Ahorro Interno", "4"));
        ddlFormaPago.SelectedIndex = 0;
        ddlFormaPago.DataBind();
        //DDL DETALLE FORMA PAGO
        ddlForma_Desem.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlForma_Desem.Items.Insert(1, new ListItem("Efectivo", "1"));
        ddlForma_Desem.Items.Insert(2, new ListItem("Cheque", "2"));
        ddlForma_Desem.Items.Insert(3, new ListItem("Transferencia", "3"));
        ddlForma_Desem.SelectedIndex = 0;
        ddlForma_Desem.DataBind();

        Xpinn.Contabilidad.Services.TipoComprobanteService TipoComprobanteService = new Xpinn.Contabilidad.Services.TipoComprobanteService();
        Xpinn.Contabilidad.Entities.TipoComprobante TipoComprobante = new Xpinn.Contabilidad.Entities.TipoComprobante();
        ddlTipoComp.DataSource = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "", (Usuario)Session["Usuario"]);
        ddlTipoComp.DataTextField = "descripcion";
        ddlTipoComp.DataValueField = "tipo_comprobante";
        ddlTipoComp.DataBind();

        ddlTipoComprobante.DataSource = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "", (Usuario)Session["Usuario"]);
        ddlTipoComprobante.DataTextField = "descripcion";
        ddlTipoComprobante.DataValueField = "tipo_comprobante";
        ddlTipoComprobante.DataBind();

        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();

        ddlEntidad_giro2.DataSource = bancoService.ListarBancosEntidad(banco, (Usuario)Session["usuario"]);
        ddlEntidad_giro2.DataTextField = "nombrebanco";
        ddlEntidad_giro2.DataValueField = "cod_banco";
        ddlEntidad_giro2.AppendDataBoundItems = true;
        ddlEntidad_giro2.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlEntidad_giro2.SelectedIndex = 0;
        ddlEntidad_giro2.DataBind();
        

        ddlEntidad.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlEntidad.DataTextField = "nombrebanco";
        ddlEntidad.DataValueField = "cod_banco";
        ddlEntidad.AppendDataBoundItems = true;
        ddlEntidad.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlEntidad.DataBind();

        ddlEntidad2.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlEntidad2.DataTextField = "nombrebanco";
        ddlEntidad2.DataValueField = "cod_banco";
        ddlEntidad2.AppendDataBoundItems = true;
        ddlEntidad2.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlEntidad2.DataBind();

        Xpinn.Caja.Services.BancosService ServiceCuentasBanc = new Xpinn.Caja.Services.BancosService();
        Usuario usuario = (Usuario)Session["usuario"];
        ddlCuentas.DataSource = ServiceCuentasBanc.ListarCuentaBancaria_Bancos(usuario);
        ddlCuentas.DataTextField = "nombrebanco";
        ddlCuentas.DataValueField = "ctabancaria";
        ddlCuentas.AppendDataBoundItems = true;
        ddlCuentas.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlCuentas.SelectedIndex = 0;
        ddlCuentas.DataBind();


        string ddlusuarios = ConfigurationManager.AppSettings["ddlusuarios"].ToString();
        if (ddlusuarios == "1")
        {
            // Cargar los asesores ejecutivos
            Xpinn.Asesores.Services.UsuarioAseService serviceEjecutivo = new Xpinn.Asesores.Services.UsuarioAseService();
            Xpinn.Asesores.Entities.UsuarioAse ejec = new Xpinn.Asesores.Entities.UsuarioAse();
            List<Xpinn.Asesores.Entities.UsuarioAse> lstAsesores = new List<Xpinn.Asesores.Entities.UsuarioAse>();
            lstAsesores = serviceEjecutivo.ListartodosUsuarios(ejec, (Usuario)Session["usuario"]);
            if (lstAsesores.Count > 0)
            {
                ddlUsuario.DataSource = lstAsesores;
                ddlUsuario.DataTextField = "nombre";
                ddlUsuario.DataValueField = "codusuario";
                ddlUsuario.DataBind();
                ddlUsuario.Items.Insert(0, new ListItem("Seleccione un item", "-1"));
            }
            else
            {
                ddlusuarios = "0";
            }
        }
        if (ddlusuarios != "1")
        {
            // Cargar usuarios cuando no se manejan asesores
            Xpinn.Seguridad.Services.UsuarioService serviceEjecutivo = new Xpinn.Seguridad.Services.UsuarioService();
            Xpinn.Util.Usuario usu = new Xpinn.Util.Usuario();
            usu.estado = 1;
            ddlUsuario.DataSource = serviceEjecutivo.ListarUsuario(usu, (Usuario)Session["usuario"]);
            ddlUsuario.DataTextField = "nombre";
            ddlUsuario.DataValueField = "codusuario";
            ddlUsuario.DataBind();
            ddlUsuario.Items.Insert(0, new ListItem("Seleccione un item", "-1"));
        }

        ddlTipo_cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
        ddlTipo_cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
        ddlTipo_cuenta.SelectedIndex = 0;
        ddlTipo_cuenta.DataBind();


        ddlOrdenadoPor.Items.Insert(0, new ListItem("Seleccione un item","0"));
        ddlOrdenadoPor.Items.Insert(1, new ListItem("Número de Giro", "IDGIRO"));
        ddlOrdenadoPor.Items.Insert(2, new ListItem("Fecha Registro", "FEC_REG"));
        ddlOrdenadoPor.Items.Insert(3, new ListItem("Código Persona", "COD_PERSONA"));
        ddlOrdenadoPor.Items.Insert(4, new ListItem("Identificación", "IDENTIFICACION"));
        ddlOrdenadoPor.Items.Insert(5, new ListItem("Nombre", "NOMBRE"));
        ddlOrdenadoPor.Items.Insert(6, new ListItem("Num Comprobante", "NUM_COMP"));
        ddlOrdenadoPor.Items.Insert(7, new ListItem("Tipo Comprobante", "TIPO_COMP"));
        ddlOrdenadoPor.Items.Insert(8, new ListItem("Forma de Pago", "FORMA_PAGO"));
        ddlOrdenadoPor.Items.Insert(9, new ListItem("Valor", "VALOR"));

        ddlLuegoPor.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlLuegoPor.Items.Insert(1, new ListItem("Número de Giro", "IDGIRO"));
        ddlLuegoPor.Items.Insert(2, new ListItem("Fecha Registro", "FEC_REG"));
        ddlLuegoPor.Items.Insert(3, new ListItem("Código Persona", "COD_PERSONA"));
        ddlLuegoPor.Items.Insert(4, new ListItem("Identificación", "IDENTIFICACION"));
        ddlLuegoPor.Items.Insert(5, new ListItem("Nombre", "NOMBRE"));
        ddlLuegoPor.Items.Insert(6, new ListItem("Num Comprobante", "NUM_COMP"));
        ddlLuegoPor.Items.Insert(7, new ListItem("Tipo Comprobante", "TIPO_COMP"));
        ddlLuegoPor.Items.Insert(8, new ListItem("Forma de Pago", "FORMA_PAGO"));
        ddlLuegoPor.Items.Insert(9, new ListItem("Valor", "VALOR"));

    }

    void CargarCuentas()
    {
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Usuario usuario = (Usuario)Session["usuario"];

        Int64 codbanco = 0;
        try
        {
            codbanco = Convert.ToInt64(ddlEntidad_giro2.SelectedValue);
        }
        catch
        {
        }
        if (codbanco != 0)
        {
            ddlCuenta_Giro2.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
            ddlCuenta_Giro2.DataTextField = "num_cuenta";
            ddlCuenta_Giro2.DataValueField = "idctabancaria";
            ddlCuenta_Giro2.DataBind();
        }
    }

    protected void ddlEntidad_giro2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEntidad_giro2.SelectedIndex != 0)
            CargarCuentas();
        else
            ddlCuenta_Giro2.Items.Clear();
    }


    void CalcularTotal()
    {
        decimal TotalAprobar = 0;
        int cont = 0;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar.Checked)
            {
                decimal valor;
                valor = rFila.Cells[14].Text != "&nbsp;" ? Convert.ToDecimal(rFila.Cells[14].Text) : 0;
                TotalAprobar += valor;
                cont++;
            }
        }
        txtVrFusiona.Text = TotalAprobar.ToString();
        txtNumGirosReali.Text = cont.ToString();
    }

    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEncabezado = (CheckBox)sender;
        if (cbSeleccionarEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;
            }
            CalcularTotal();
        }
    }


    private void Actualizar()
    {
        try
        {
            DateTime pFechaGiro;
            pFechaGiro = txtFechaGiro.ToDateTime == null ? DateTime.MinValue : txtFechaGiro.ToDateTime;
            List<Xpinn.Tesoreria.Entities.Giro> lstConsulta = new List<Xpinn.Tesoreria.Entities.Giro>();

            String Orden = "";
            if (ddlOrdenadoPor.SelectedIndex != 0)
                Orden += "g." + ddlOrdenadoPor.SelectedValue;
            if (ddlLuegoPor.SelectedIndex != 0)
            {
                if (Orden != "")
                    Orden += ", g." + ddlLuegoPor.SelectedValue;
                else
                    Orden += " g." + ddlLuegoPor.SelectedValue;
            }
            lstConsulta = FusionService.ListarGiroAprobadosOpendientes(ObtenerValores(), Orden, pFechaGiro, (Usuario)Session["usuario"]);

            gvLista.PageSize = 20;
            gvLista.EmptyDataText = emptyQuery;

            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                ValidarPermisosGrilla(gvLista);
                Session["DTGIROS"] = lstConsulta;
                lblInfo.Visible = false;
                toolBar.MostrarGuardar(true);
            }
            else
            {
                gvLista.DataSource = null;
                Session["DTGIROS"] = null;
                panelGrilla.Visible = false;
                lblInfo.Visible = true;
                toolBar.MostrarGuardar(false);
            }

            Session.Add(FusionService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FusionService.CodigoPrograma, "Actualizar", ex);
        }
    }


    private Xpinn.Tesoreria.Entities.Giro ObtenerValores()
    {
        Xpinn.Tesoreria.Entities.Giro vGiro = new Xpinn.Tesoreria.Entities.Giro();

        if (txtCodGiro.Text.Trim() != "")
            vGiro.idgiro = Convert.ToInt32(txtCodGiro.Text.Trim());
        if (txtIdentificacion.Text.Trim() != "")
            vGiro.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
        if (txtNombres.Text.Trim() != "")
            vGiro.nombre = txtNombres.Text.Trim();
        if (txtNumComp.Text.Trim() != "")
            vGiro.num_comp = Convert.ToInt32(txtNumComp.Text.Trim());
        if (ddlTipoComp.SelectedIndex != 0)
            vGiro.tipo_comp = Convert.ToInt32(ddlTipoComp.SelectedValue);
        if (txtFechaGiro.TieneDatos)
            vGiro.fec_giro = txtFechaGiro.ToDateTime;
        if (ddlFormaPago.SelectedIndex != 0)
            vGiro.forma_pago = Convert.ToInt32(ddlFormaPago.SelectedValue);
        if (ddlEntidad.SelectedIndex != 0)
            vGiro.cod_banco = Convert.ToInt64(ddlEntidad.SelectedValue);
        if (ddlCuentas.SelectedIndex != 0)
        {
            string[] Cta = ddlCuentas.SelectedItem.Text.Trim().Split('-');
            if (Cta[0].ToString() != "")
                vGiro.num_referencia = Cta[0].ToString().Trim();
        }
        if (ddlUsuario.SelectedIndex != 0)
            vGiro.usu_gen = ddlUsuario.SelectedValue.Trim();
        if (ddlGenerado.SelectedIndex != 0)
            vGiro.tipo_acto = Convert.ToInt32(ddlGenerado.SelectedValue);
        return vGiro;
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        if (Page.IsValid)
        {
            Actualizar();
            CalcularTotal();            
        }
    }


    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona");
    }

    protected void txtIdPersona_TextChanged(object sender, EventArgs e)
    {
        Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
        Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();

        DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdPersona.Text, (Usuario)Session["usuario"]);

        if (DatosPersona.cod_persona != 0)
        {
            if (DatosPersona.cod_persona != 0)
                txtCodPersona.Text = DatosPersona.cod_persona.ToString();
            if (DatosPersona.identificacion != "")
                txtIdPersona.Text = DatosPersona.identificacion;
            if (DatosPersona.nombre != "")
                txtNomPersona.Text = DatosPersona.nombre;
        }
        else
        {
            txtNomPersona.Text = "";
            txtCodPersona.Text = "";
        }
    }


    Boolean validarDatos()
    {
        if (ddlForma_Desem.SelectedIndex == 0)
        {
            VerError("Seleccione una forma de pago");
            ddlForma_Desem.Focus();
            return false;
        }
        if (PanelCheque.Visible == true)
        {
            if (ddlEntidad_giro2.SelectedIndex == 0)
            {
                VerError("Seleccione el Banco de giro");
                ddlEntidad_giro2.Focus();
                return false;
            }
        }
        if (PanelTransfe.Visible == true)
        {
            if (ddlEntidad2.SelectedIndex == 0)
            {
                VerError("Seleccione el banco al que se girará");
                ddlEntidad2.Focus();
                return false;
            }
            if (txtNum_cuenta.Text == "")
            {
                VerError("Ingrese el número de cuenta a girar");
                txtNum_cuenta.Focus();
                return false;
            }
        }

        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (mvPrincipal.ActiveViewIndex == 0)
        {
            Giro pEntidad = new Giro();
            pEntidad.lstGiro = new List<Giro>();
            pEntidad.lstGiro = ObtenerListaAprobarGiros();

            if (pEntidad.lstGiro == null || pEntidad.lstGiro.Count == 0)
            {
                VerError("No existen giros seleccionados por Fusionar");
                return;
            }

            int cont = 0,cod_persona = 0, cod_personaAnterior = 0;           
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
                if (cbSeleccionar != null)
                    if (cbSeleccionar.Checked == true)
                    {
                        idObjeto = Convert.ToString(rFila.Cells[1].Text);
                        cod_persona = Convert.ToInt32(rFila.Cells[3].Text);

                        if (cont == 0)
                        {
                            ObtenerDatos(idObjeto);
                            cod_personaAnterior = cod_persona;
                        }

                        if (cod_persona != cod_personaAnterior)
                        {
                            VerError("Los giros seleccionados deben pertenecer a una sola persona, verifique los datos seleccionados");
                            return;
                        }
                        cod_personaAnterior = cod_persona;
                        cont++;
                    }
            }
            mvPrincipal.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarGuardar(true);
            VisualizarOpciones(FusionService.CodigoPrograma, "A");
        }
        else if (mvPrincipal.ActiveViewIndex == 1)
        {
            if(validarDatos())
            ctlMensaje.MostrarMensaje("Desea realizar la aprobación de los giros seleccionados?");
        }
    }


    Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
    Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario pUsuario = (Usuario)Session["Usuario"];

            Giro pEntidad = new Giro();

            pEntidad.lstGiro = new List<Giro>();
            pEntidad.lstGiro = ObtenerListaAprobarGiros();

            if (pEntidad.lstGiro == null || pEntidad.lstGiro.Count == 0)
            {
                VerError("No existen giros seleccionados por Fusionar");
                return;
            }
            if (txtCodPersona.Text == "")
            {
                VerError("Seleccione la persona por favor");
                txtIdentificacion.Focus();
                return;
            }
            //GRABACION DEL GIRO A REALIZAR
            Xpinn.FabricaCreditos.Services.AvanceService AvancServices = new Xpinn.FabricaCreditos.Services.AvanceService();
            Usuario pusu = (Usuario)Session["usuario"];
            Giro pGiro = new Giro();
            pGiro.idgiro = 0;
            pGiro.cod_persona = Convert.ToInt64(txtCodPersona.Text);
            pGiro.forma_pago = Convert.ToInt32(ddlForma_Desem.SelectedValue);
            if (ddlTipoGiro.SelectedIndex == 0)
            {
                VerError("Seleccione el tipo de Giro");
                ddlTipoGiro.Focus();
                return;
            }
            pGiro.tipo_acto = Convert.ToInt32(ddlTipoGiro.SelectedValue);
            //pGiro.cod_ope = Convert.ToInt32(cod_operacion);
            pGiro.fec_reg = Convert.ToDateTime(txtFecha.Text);
            pGiro.fec_giro = DateTime.Now;
            pGiro.numero_radicacion = 0;
            pGiro.usu_gen = pusu.nombre;
            pGiro.usu_apli = null;
            pGiro.estadogi = 0;
            pGiro.usu_apro = null;



            //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
         

            //DATOS DE FORMA DE PAGO
            if (ddlForma_Desem.SelectedItem.Text == "Transferencia")
            {
                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidad_giro2.SelectedValue), ddlCuenta_Giro2.SelectedItem.Text, (Usuario)Session["usuario"]);
                Int64 idCta = CuentaBanc.idctabancaria;
                pGiro.idctabancaria = idCta;
                pGiro.cod_banco = Convert.ToInt32(ddlEntidad2.SelectedValue);
                pGiro.num_cuenta = txtNum_cuenta.Text;
                pGiro.tipo_cuenta = Convert.ToInt32(ddlTipo_cuenta.SelectedValue);
            }
            else if (ddlForma_Desem.SelectedItem.Text == "Cheque")
            {
                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidad_giro2.SelectedValue), ddlCuenta_Giro2.SelectedItem.Text, (Usuario)Session["usuario"]);
                Int64 idCta = CuentaBanc.idctabancaria;
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
            pGiro.fec_apro = DateTime.MinValue;
            pGiro.cob_comision = chkCobraComision.Checked == true ? 1 : 0;
            pGiro.valor = Convert.ToInt64(txtValor.Text.Replace(".", ""));

            FusionService.FusionarGiro(pEntidad, pGiro, pUsuario);            
            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarGuardar(false);
            mvPrincipal.ActiveViewIndex = 2;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected List<Giro> ObtenerListaAprobarGiros()
    {
        List<Giro> lstGiros = new List<Giro>();
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar != null)
                if (cbSeleccionar.Checked == true)
                {
                    Giro pEntidad = new Giro();
                    pEntidad.idgiro = Convert.ToInt32(rFila.Cells[1].Text);
                    if (rFila.Cells[10].Text != "&nbsp;")
                        pEntidad.nom_forma_pago = rFila.Cells[9].Text;
                    lstGiros.Add(pEntidad);
                }
        }

        return lstGiros;
    }



    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        LimpiarValoresConsulta(pBusqueda, FusionService.CodigoPrograma);
        mvPrincipal.ActiveViewIndex = 0;
        txtFechaGiro.Text = "";
        lblInfo.Visible = false;
        gvLista.DataSource = null;
        panelGrilla.Visible = false;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarCancelar(false);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarGuardar(false);
    }

   
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Site toolBar = (Site)this.Master;
        if (mvPrincipal.ActiveViewIndex == 1 || mvPrincipal.ActiveViewIndex == 2)
        {
            

            toolBar.MostrarCancelar(false);
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarConsultar(true);
            toolBar.MostrarGuardar(true);
            if (mvPrincipal.ActiveViewIndex == 2)
            {
                toolBar.MostrarGuardar(false);
                VerError("");
                LimpiarValoresConsulta(pBusqueda, FusionService.CodigoPrograma);
                mvPrincipal.ActiveViewIndex = 0;
                txtFechaGiro.Text = "";
                lblInfo.Visible = false;
                gvLista.DataSource = null;
                panelGrilla.Visible = false;
            }
            mvPrincipal.ActiveViewIndex = 0;
            VisualizarOpciones(FusionService.CodigoPrograma, "L");
        }
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Tesoreria.Services.GiroServices GiroServicios = new Xpinn.Tesoreria.Services.GiroServices();
            Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Giro pGiro = new Giro();
            pGiro = GiroServicios.ConsultarGiro(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);

            txtCodigo.Enabled = false;

            if (pGiro.cod_persona != 0)
            {
                Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
                pPersona.seleccionar = "Cod_persona";
                pPersona.noTraerHuella = 1;
                pPersona.cod_persona = Convert.ToInt64(pGiro.cod_persona);

                pPersona = PersonaService.ConsultarPersona1Param(pPersona, (Usuario)Session["usuario"]);
                if (pPersona.cod_persona != 0)
                    txtCodPersona.Text = pPersona.cod_persona.ToString();
                if (pPersona.identificacion != "")
                    txtIdPersona.Text = pPersona.identificacion.ToString();
                if (pPersona.tipo_persona == "N")
                    txtNomPersona.Text = pPersona.nombres + ' ' + pPersona.apellidos;
                else
                    txtNomPersona.Text = pPersona.razon_social;
            }
            ddlForma_Desem.SelectedValue = pGiro.forma_pago.ToString();

            txtFecha.Text = DateTime.Now.ToShortDateString();
            txtFechaGiroNuevo.Text = DateTime.Now.ToShortDateString();
            
            if (pGiro.tipo_acto != 0)
                ddlTipoGiro.SelectedValue = pGiro.tipo_acto.ToString();

            txtCodOperacion.Enabled = false; 
            
            if (pGiro.numero_radicacion != 0)
                txtNumRadicacion.Text = pGiro.numero_radicacion.ToString();

            if (pGiro.num_comp > 0)
                txtNumComprobante.Text = pGiro.num_comp.ToString();

            if (pGiro.tipo_comp != null && pGiro.tipo_comp > 0)
                ddlTipoComprobante.SelectedValue = pGiro.tipo_comp.ToString();

            if (txtVrFusiona.Text != "0")
                txtValor.Text = txtVrFusiona.Text;

            if (pGiro.cob_comision == 1)
                chkCobraComision.Checked = true;

            Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
            Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
            if (pGiro.idctabancaria != 0)
            {
                CuentaBanc = bancoService.ConsultarCuentasBancarias(Convert.ToInt32(pGiro.idctabancaria), (Usuario)Session["usuario"]);
                if (CuentaBanc.cod_banco != 0)
                {
                    ddlEntidad_giro2.SelectedValue = CuentaBanc.cod_banco.ToString();
                    ddlEntidad_giro2_SelectedIndexChanged(ddlEntidad_giro2, null);
                    ddlCuenta_Giro2.SelectedValue = pGiro.idctabancaria.ToString();
                }
            }
            if (pGiro.cod_banco != null)
            {
                if (pGiro.cod_banco != 0)
                    ddlEntidad2.SelectedValue = pGiro.cod_banco.ToString();
            }
            if (pGiro.num_cuenta != "")
                txtNum_cuenta.Text = pGiro.num_cuenta;
            if (pGiro.tipo_cuenta != null)
                ddlTipo_cuenta.SelectedValue = pGiro.tipo_cuenta.ToString();

            ddlForma_Desem_SelectedIndexChanged(ddlForma_Desem, null);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(FusionService.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void ddlForma_Desem_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActivarDesembolso();
    }

    protected void ActivarDesembolso()
    {
        if (ddlForma_Desem.SelectedItem.Text == "Transferencia")
        {
            PanelCheque.Visible = true;
            PanelTransfe.Visible = true;
        }
        else if (ddlForma_Desem.SelectedItem.Text == "Efectivo")
        {
            PanelCheque.Visible = false;
            PanelTransfe.Visible = false;
        }
        else if (ddlForma_Desem.SelectedItem.Text == "Cheque")
        {
            PanelCheque.Visible = true;
            PanelTransfe.Visible = false;
        }
        else
        {
            PanelCheque.Visible = false;
            PanelTransfe.Visible = false;
        }
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
            BOexcepcion.Throw(FusionService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    
}