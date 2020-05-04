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
    AprobacionGirosServices AprobacionService = new AprobacionGirosServices();
    Xpinn.Tesoreria.Services.GiroServices GiroServicio = new Xpinn.Tesoreria.Services.GiroServices();
    Xpinn.FabricaCreditos.Services.Credito_GiroService GiroService = new Xpinn.FabricaCreditos.Services.Credito_GiroService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {       
            VisualizarOpciones(AprobacionService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoCancelar += btnRegresar_Click;
            toolBar.MostrarCancelar(false);
            toolBar.MostrarExportar(false);
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarConsultar(true);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarImprimir(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                txtFechaApro.Text = DateTime.Now.ToShortDateString();
                CargarDropDown();
            }
            else
            {
                CalcularTotal();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionService.CodigoPrograma, "Page_Load", ex);
        }
    }


    void CargarDropDown()
    {
        PoblarListas PoblarLista = new PoblarListas();
        PoblarLista.PoblarListaDesplegable("TIPO_ACTO_GIRO","","","1", ddlGenerado, (Usuario)Session["usuario"]);

        ddlTipoGiro.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoGiro.Items.Insert(1, new ListItem("Desembolsos", "1"));
        ddlTipoGiro.Items.Insert(2, new ListItem("Cruce de Cuentas", "2"));
        ddlTipoGiro.Items.Insert(3, new ListItem("Devoluciones", "3"));
        ddlTipoGiro.Items.Insert(4, new ListItem("Retiro de Aportes", "4"));
        ddlTipoGiro.Items.Insert(5, new ListItem("Retiro de Ahorros", "5"));
        ddlTipoGiro.Items.Insert(6, new ListItem("Desembolso de Auxiolios", "6"));
        ddlTipoGiro.Items.Insert(7, new ListItem("Cierre de CDAT", "7"));
        ddlTipoGiro.Items.Insert(8, new ListItem("Liquidación de Interés CDAT", "8"));
        ddlTipoGiro.Items.Insert(9, new ListItem("Renovación CDAT", "9"));
        ddlTipoGiro.Items.Insert(10, new ListItem("Órdenes de Pago", "10"));
        ddlTipoGiro.Items.Insert(11, new ListItem("Facturas", "11"));
        ddlTipoGiro.SelectedIndex = 0;
        ddlTipoGiro.DataBind();


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


        ddlEstado.Items.Insert(0, new ListItem("Pendiente", "0"));
        ddlEstado.Items.Insert(1, new ListItem("Aprobado", "1"));
        ddlEstado.Items.Insert(2, new ListItem("Desembolsado", "2"));
        ddlEstado.Items.Insert(3, new ListItem("Anulado", "3"));
        ddlEstado.Items.Insert(4, new ListItem("Fusionado", "4"));
        ddlEstado.SelectedIndex = 0;
        ddlEstado.DataBind();

        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        ddlEntidad_giro.DataSource = bancoService.ListarBancosEntidad(banco, (Usuario)Session["usuario"]);
        ddlEntidad_giro.DataTextField = "nombrebanco";
        ddlEntidad_giro.DataValueField = "cod_banco";
        ddlEntidad_giro.AppendDataBoundItems = true;
        ddlEntidad_giro.Items.Insert(0,new ListItem("Seleccione un item","0"));
        ddlEntidad_giro.SelectedIndex = 0;
        ddlEntidad_giro.DataBind();
        CargarCuentas("");

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

        Xpinn.Seguridad.Services.UsuarioService UsuarioService = new Xpinn.Seguridad.Services.UsuarioService();
        List<Usuario> lstusuarios = new List<Usuario>();
        Usuario pUsu = new Usuario();
        lstusuarios = UsuarioService.ListarUsuario(pUsu, (Usuario)Session["usuario"]);

        if (lstusuarios.Count > 0)
        {
            ddlUsuarioAplica.DataSource = lstusuarios;
            ddlUsuarioAplica.DataTextField = "nombre";
            ddlUsuarioAplica.DataValueField = "nombre";
            ddlUsuarioAplica.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlUsuarioAplica.AppendDataBoundItems = true;
            ddlUsuarioAplica.SelectedIndex = 0;
            ddlUsuarioAplica.DataBind();

            ddlUsuarioAproba.DataSource = lstusuarios;
            ddlUsuarioAproba.DataTextField = "nombre";
            ddlUsuarioAproba.DataValueField = "nombre";
            ddlUsuarioAproba.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlUsuarioAproba.AppendDataBoundItems = true;
            ddlUsuarioAproba.SelectedIndex = 0;
            ddlUsuarioAproba.DataBind();

            ddlUsuarioGenera.DataSource = lstusuarios;
            ddlUsuarioGenera.DataTextField = "nombre";
            ddlUsuarioGenera.DataValueField = "nombre";
            ddlUsuarioGenera.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlUsuarioGenera.AppendDataBoundItems = true;
            ddlUsuarioGenera.SelectedIndex = 0;
            ddlUsuarioGenera.DataBind();
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
        ddlOrdenadoPor.Items.Insert(10, new ListItem("Primer_Nombre", "PRIMER_NOMBRE"));
        ddlOrdenadoPor.Items.Insert(11, new ListItem("Segundo_Nombre", "SEGUNDO_NOMBRE"));
        ddlOrdenadoPor.Items.Insert(12, new ListItem("Primer_Apellido", "PRIMER_APELLIDO"));
        ddlOrdenadoPor.Items.Insert(13, new ListItem("Segundo_Apellido", "SEGUNDO_APELLIDO"));



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
        ddlLuegoPor.Items.Insert(10, new ListItem("Primer_Nombre", "PRIMER_NOMBRE"));
        ddlLuegoPor.Items.Insert(11, new ListItem("Segundo_Nombre", "SEGUNDO_NOMBRE"));
        ddlLuegoPor.Items.Insert(12, new ListItem("Primer_Apellido", "PRIMER_APELLIDO"));
        ddlLuegoPor.Items.Insert(13, new ListItem("Segundo_Apellido", "SEGUNDO_APELLIDO"));


    }

    void CargarCuentas(String opcion)
    {
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Usuario usuario = (Usuario)Session["usuario"];
        if (opcion != "DETALLE")
        {
            Int64 codbanco = 0;
            try
            {
                codbanco = Convert.ToInt64(ddlEntidad_giro.SelectedValue);
            }
            catch
            {
            }
            if (codbanco != 0)
            {                
                ddlCuenta_Giro.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
                ddlCuenta_Giro.DataTextField = "num_cuenta";
                ddlCuenta_Giro.DataValueField = "idctabancaria";
                ddlCuenta_Giro.DataBind();
            }
        }
        else
        {
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
    }


    protected void ddlEntidad_giro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEntidad_giro.SelectedIndex != 0)
            CargarCuentas("");
        else
            ddlCuenta_Giro.Items.Clear();
    }

    protected void ddlEntidad_giro2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEntidad_giro2.SelectedIndex != 0)
            CargarCuentas("DETALLE");
        else
            ddlCuenta_Giro2.Items.Clear();
    }
    

    void CalcularTotal()
    {
        decimal Total = 0;
        List<Xpinn.Tesoreria.Entities.Giro> lstDatos = (List<Xpinn.Tesoreria.Entities.Giro>)Session["DTGIROS"];
        if (Session["DTGIROS"] != null)
        {
            foreach (Xpinn.Tesoreria.Entities.Giro rDato in lstDatos)
            {
                Total += Convert.ToDecimal(rDato.valor);
            }
        }
        txtVrTotal.Text = Total.ToString();
        decimal TotalAprobar = 0;
        int cont = 0;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar.Checked)
            {
                decimal valor;
                valor = rFila.Cells[15].Text != "&nbsp;" ? Convert.ToDecimal(rFila.Cells[15].Text) : 0;
                TotalAprobar += valor;
                cont++;
            }
        }
        txtVrAprobar.Text = TotalAprobar.ToString();
        txtNumGirosApro.Text = cont.ToString();
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
            lstConsulta = AprobacionService.ListarGiro(ObtenerValores(),Orden,pFechaGiro, (Usuario)Session["usuario"]);

            gvLista.PageSize = 20;
            gvLista.EmptyDataText = emptyQuery;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                txtNumTotalReg.Text = lstConsulta.Count.ToString();
                mvPrincipal.ActiveViewIndex = 1;
                ValidarPermisosGrilla(gvLista);
                Session["DTGIROS"] = lstConsulta;
                lblInfo.Visible = false;
            }
            else
            {
                mvPrincipal.ActiveViewIndex = 0;
                Session["DTGIROS"] = null;
                panelGrilla.Visible = false;
                txtNumTotalReg.Text = lstConsulta.Count.ToString();
                lblInfo.Visible = true;
            }

            Session.Add(AprobacionService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionService.CodigoPrograma, "Actualizar", ex);
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
            if(Cta[0].ToString() != "")
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
            if (mvPrincipal.ActiveViewIndex == 1)
            {
                Site toolBar = (Site)this.Master;
                toolBar.MostrarCancelar(true);
                toolBar.MostrarExportar(true);
                toolBar.MostrarLimpiar(true);
                toolBar.MostrarConsultar(false);
                toolBar.MostrarGuardar(true);
                toolBar.MostrarImprimir(false);
            }
        }
    }

    bool ValidarDatos()
    {
        Giro pEntidad = new Giro();

        pEntidad.lstGiro = new List<Giro>();
        pEntidad.lstGiro = ObtenerListaAprobarGiros();
        if (pEntidad.lstGiro == null || pEntidad.lstGiro.Count == 0)
        {
            VerError("No existen giros seleccionados por aprobar");
            return false;
        }

       

        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar != null)
                if (cbSeleccionar.Checked == true)
                {
                    if (rFila.Cells[10].Text != "Efectivo")
                    {
                        if (rFila.Cells[11].Text == "&nbsp;")
                        {
                            if (ddlEntidad_giro.SelectedIndex == 0)
                            {
                                VerError("ERROR en la Fila " + (rFila.RowIndex + 1) + " : La forma de pago es de tipo " + rFila.Cells[10].Text + " y debe seleccionar el Banco y Cuenta de Giro");
                                ddlEntidad_giro.Focus();
                                return false;
                            }
                        }
                    }                   
                }
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if(ValidarDatos())
            ctlMensaje.MostrarMensaje("Desea realizar la aprobación de los giros seleccionados?");
    }

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
                VerError("No existen giros seleccionados por aprobar");
                return;
            }

            
         
            Int32 idCta = 0;
            if (ddlEntidad_giro.SelectedIndex != 0)
            {
                Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
                Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidad_giro.SelectedValue), ddlCuenta_Giro.SelectedItem.Text, (Usuario)Session["usuario"]);
                idCta = CuentaBanc.idctabancaria;
            }
            pEntidad.idctabancaria = idCta;
            pEntidad.fec_apro = Convert.ToDateTime(txtFechaApro.Text);
            AprobacionService.AprobarGiro(pEntidad, pUsuario);
            //Actualizar();
            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarExportar(false);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarImprimir(true);
            mvPrincipal.ActiveViewIndex = 4;
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
                    pEntidad.idgiro = Convert.ToInt32(rFila.Cells[2].Text);
                    pEntidad.fec_apro = Convert.ToDateTime(txtFechaApro.Text);
                    if (rFila.Cells[10].Text != "&nbsp;")
                        pEntidad.nom_forma_pago = rFila.Cells[10].Text;
                    String NumCta = rFila.Cells[12].Text.Trim().ToString();
                    if (NumCta != "" || NumCta != "&nbsp;")
                    {
                        CuentasBancarias vData = new CuentasBancarias();
                        vData = AprobacionService.ConsultarCuentasBancariasXNumCuenta(NumCta, (Usuario)Session["usuario"]);
                        if (vData.idctabancaria != 0)
                            pEntidad.idctabancaria = vData.idctabancaria;
                    }
                    lstGiros.Add(pEntidad);
                }
        }

        return lstGiros;
    }



    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        LimpiarValoresConsulta(pBusqueda, AprobacionService.CodigoPrograma);
        mvPrincipal.ActiveViewIndex = 0;
        txtFechaApro.Text = DateTime.Now.ToShortDateString();
        txtFechaGiro.Text = "";
        lblInfo.Visible = false;
        ddlEntidad_giro.SelectedIndex = 0;
        ddlEntidad_giro_SelectedIndexChanged(ddlEntidad_giro, null);
        Site toolBar = (Site)this.Master;
        toolBar.MostrarCancelar(false);
        toolBar.MostrarExportar(false);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarGuardar(false);
        toolBar.MostrarImprimir(false);
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        try
        {
            //CREACION DE LA TABLA
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("idGiro");
            table.Columns.Add("fec_reg");
            table.Columns.Add("cod_persona");
            table.Columns.Add("identificacion");
            table.Columns.Add("nombre");
            table.Columns.Add("cod_ope");
            table.Columns.Add("num_comp");
            table.Columns.Add("nom_tipo_comp");
            table.Columns.Add("forma_pago");
            table.Columns.Add("nom_banco");
            table.Columns.Add("num_referencia");
            table.Columns.Add("nom_banco_dest");
            table.Columns.Add("num_cta_dest");
            table.Columns.Add("valor");
            table.Columns.Add("generado");


            List<Xpinn.Tesoreria.Entities.Giro> lstConsulta = new List<Xpinn.Tesoreria.Entities.Giro>();
            //lstConsulta = (List<Xpinn.Tesoreria.Entities.Giro>)Session["DTREPORTE"];
            //CARGANDO LA TABLA
            int cont = 0;
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
                if (cbSeleccionar != null)
                    if (cbSeleccionar.Checked == true)
                    {
                        DataRow dr;
                        dr = table.NewRow();
                        dr[0] = rFila.Cells[2].Text;
                        dr[1] = rFila.Cells[3].Text != "&nbsp;" ? rFila.Cells[3].Text : "";
                        dr[2] = rFila.Cells[4].Text != "&nbsp;" ? rFila.Cells[4].Text : "";
                        dr[3] = rFila.Cells[5].Text != "&nbsp;" ? rFila.Cells[5].Text : "";
                        dr[4] = rFila.Cells[6].Text != "&nbsp;" ? rFila.Cells[6].Text : "";
                        dr[5] = rFila.Cells[7].Text != "&nbsp;" ? rFila.Cells[7].Text : "";
                        dr[6] = rFila.Cells[8].Text != "&nbsp;" ? rFila.Cells[8].Text : "";
                        dr[7] = rFila.Cells[9].Text != "&nbsp;" ? rFila.Cells[9].Text : "";
                        dr[8] = rFila.Cells[10].Text != "&nbsp;" ? rFila.Cells[10].Text : "";//FORMA PAGO
                        
                        if (rFila.Cells[10].Text != "&nbsp;")//FORMA PAGO
                        {
                            if (rFila.Cells[10].Text != "Efectivo")
                            {
                                if (rFila.Cells[11].Text == "&nbsp;")
                                {
                                    dr[9] = ddlEntidad_giro.SelectedItem.Text; //nom_banco
                                    dr[10] = ddlCuenta_Giro.SelectedItem.Text; //NUM REFERENCIA = CTA GIRO
                                }
                                else
                                {
                                    dr[9] = rFila.Cells[11].Text;
                                    dr[10] = rFila.Cells[12].Text;
                                }
                            }
                            else
                            {
                                dr[9] = rFila.Cells[11].Text != "&nbsp;" ? rFila.Cells[11].Text : ""; //nom_banco
                                dr[10] = rFila.Cells[12].Text != "&nbsp;" ? rFila.Cells[12].Text : ""; //NUM REFERENCIA = CTA GIRO    
                            }
                        }
                        
                        dr[11] = rFila.Cells[13].Text != "&nbsp;" ? rFila.Cells[13].Text : "";
                        dr[12] = rFila.Cells[14].Text != "&nbsp;" ? rFila.Cells[14].Text : "";
                        dr[13] = rFila.Cells[15].Text != "&nbsp;" ? rFila.Cells[15].Text : "";
                        dr[14] =  "";//--FALTA GENERADO --//rFila.Cells[15].Text != "&nbsp;" ? rFila.Cells[15].Text : ""
                        table.Rows.Add(dr);
                        cont++;
                    }
            }

            if (cont == 0)
            {
                VerError("No existen datos seleccionados, verifique por favor");
                return;
            }
            

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[7];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("ImagenReport", ImagenReporte());
            param[3] = new ReportParameter("fechaApro", Convert.ToDateTime(txtFechaApro.Text).ToShortDateString());
            string Entidad = ddlEntidad_giro.SelectedIndex != 0 ? ddlEntidad_giro.SelectedItem.Text : " ";
            param[4] = new ReportParameter("banco", Entidad);
            string Cta = ddlEntidad_giro.SelectedIndex != 0 ? ddlCuenta_Giro.SelectedItem.Text : " ";
            param[5] = new ReportParameter("ctaGiro", Cta);

            string rpta = "true";
            if (ddlEntidad_giro.SelectedIndex != 0)
                rpta = "false";
            param[6] = new ReportParameter("MostrarDDL", rpta);

            rvGirosAprobados.LocalReport.EnableExternalImages = true;
            rvGirosAprobados.LocalReport.SetParameters(param);

            rvGirosAprobados.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvGirosAprobados.LocalReport.DataSources.Add(rds);
            rvGirosAprobados.LocalReport.Refresh();

            rvGirosAprobados.Visible = true;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarExportar(false);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarImprimir(false);

            mvPrincipal.ActiveViewIndex = 3;
        }
        catch(Exception ex)
        {
            BOexcepcion.Throw(AprobacionService.CodigoPrograma, "btn_Imprimir", ex);
            //VerError(ex.Message);
        }
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["DTGIROS"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.Columns[0].Visible = false;
            gvLista.Columns[1].Visible = false;
            gvLista.Columns[18].Visible = false;
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTGIROS"];
            gvLista.DataBind();
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=AprobacionGiros.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }


    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Site toolBar = (Site)this.Master;
        if (mvPrincipal.ActiveViewIndex == 1 || mvPrincipal.ActiveViewIndex == 3 || mvPrincipal.ActiveViewIndex == 4)
        {
            toolBar.MostrarCancelar(false);
            toolBar.MostrarExportar(false);
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarConsultar(true);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarImprimir(false);
            mvPrincipal.ActiveViewIndex = 0;
        }
        else if (mvPrincipal.ActiveViewIndex == 2)
        {
            toolBar.MostrarCancelar(true);
            toolBar.MostrarExportar(true);
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarGuardar(true);
            toolBar.MostrarImprimir(false);
            mvPrincipal.ActiveViewIndex = 1;
        }

    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        //Session[GiroServicio.CodigoProgramaConsulta + ".id"] = id;
        ObtenerDatos(id);
        e.NewEditIndex = -1;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarCancelar(true);
        toolBar.MostrarExportar(false);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarConsultar(false);
        toolBar.MostrarGuardar(false);
        toolBar.MostrarImprimir(false);
        mvPrincipal.ActiveViewIndex = 2;
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Tesoreria.Services.GiroServices GiroServicios = new Xpinn.Tesoreria.Services.GiroServices();
            Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Giro pGiro = new Giro();
            pGiro = GiroServicios.ConsultarGiro(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);

            if (pGiro.idgiro != 0)
                txtCodigo.Text = pGiro.idgiro.ToString();

            if (pGiro.cod_persona != 0)
            {
                Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
                pPersona.seleccionar = "Cod_persona";
                pPersona.noTraerHuella = 1;
                pPersona.cod_persona = Convert.ToInt64(pGiro.cod_persona);

                pPersona = PersonaService.ConsultarPersona1Param(pPersona, (Usuario)Session["usuario"]);
                if (pPersona.cod_persona != 0)
                {
                    txtCodPersona.Text = pPersona.cod_persona.ToString();
                    if (pPersona.identificacion != "")
                        txtIdPersona.Text = pPersona.identificacion.ToString();
                    if (pPersona.tipo_persona == "N")
                        txtNomPersona.Text = pPersona.nombres + ' ' + pPersona.apellidos;
                    else
                        txtNomPersona.Text = pPersona.razon_social;
                }
            }
            ddlForma_Desem.SelectedValue = pGiro.forma_pago.ToString();

            txtFecha.Text = Convert.ToDateTime(pGiro.fec_reg).ToShortDateString();

            if (pGiro.fec_giro != DateTime.MinValue && pGiro.fec_giro != null)
                txtFechaGiro.Text = Convert.ToDateTime(pGiro.fec_giro).ToShortDateString();

            if (pGiro.tipo_acto != 0)
                ddlTipoGiro.SelectedValue = pGiro.tipo_acto.ToString();

            if (pGiro.cod_ope != 0)
                txtCodOperacion.Text = pGiro.cod_ope.ToString();

            if (pGiro.numero_radicacion != 0)
                txtNumRadicacion.Text = pGiro.numero_radicacion.ToString();

            if (pGiro.num_comp > 0)
                txtNumComprobante.Text = pGiro.num_comp.ToString();

            if (pGiro.tipo_comp != null && pGiro.tipo_comp > 0)
                ddlTipoComprobante.SelectedValue = pGiro.tipo_comp.ToString();

            if (pGiro.valor != 0)
                txtValor.Text = pGiro.valor.ToString();

            if (pGiro.cob_comision == 1)
                chkCobraComision.Checked = true;

            if (pGiro.estado != 0)
                ddlEstado.SelectedValue = pGiro.estado.ToString();

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

            if (pGiro.usu_apro != "" || pGiro.usu_apro != null)
            {
                try
                {
                    ddlUsuarioAproba.SelectedValue = pGiro.usu_apro;
                }
                catch
                {
                    ddlUsuarioAproba.SelectedValue = "0";
                }
            }
            if (pGiro.usu_apli != "" || pGiro.usu_apli != null)
            {
                try
                {
                    ddlUsuarioAplica.SelectedValue = pGiro.usu_apli;
                }
                catch
                {
                    ddlUsuarioAplica.SelectedValue = "0";
                }
                
            }
            if (pGiro.usu_gen != "" || pGiro.usu_gen != null)
            {
                try
                {
                    ddlUsuarioGenera.SelectedValue = pGiro.usu_gen;
                }
                catch
                {
                    ddlUsuarioGenera.SelectedValue = "0";
                }
            }
            ddlForma_Desem_SelectedIndexChanged(ddlForma_Desem, null);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionService.CodigoPrograma, "ObtenerDatos", ex);
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
            BOexcepcion.Throw(AprobacionService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Ver")
            {
                gvDistribucion.DataSource = null;
                //Cargar los datos de la Distribucion
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                //int rowIndex = Convert.ToInt32(gvDistribucion.SelectedRow.RowIndex);
                string pNum_Radica = gvLista.DataKeys[rowIndex].Values[1].ToString();
                pNum_Radica = pNum_Radica != null ? pNum_Radica : "0";
                List<Xpinn.FabricaCreditos.Entities.Credito_Giro> lstCreditos = new List<Xpinn.FabricaCreditos.Entities.Credito_Giro>();
                Xpinn.FabricaCreditos.Entities.Credito_Giro pEntidad = new Xpinn.FabricaCreditos.Entities.Credito_Giro();
                pEntidad.numero_radicacion = Convert.ToInt64(pNum_Radica);
                lstCreditos = GiroService.ConsultarCredito_Giro(pEntidad, (Usuario)Session["usuario"]);
                if (lstCreditos.Count > 0)
                {
                    gvDistribucion.Visible = true;
                    lblmsjDis.Visible = false;
                    gvDistribucion.DataSource = lstCreditos;
                    gvDistribucion.DataBind();
                }
                else
                {
                    //CONSULTAR EN LA TABLA AUXILIOS_GIRO
                    Xpinn.Auxilios.Services.Auxilio_GiroService BOAuxilios = new Xpinn.Auxilios.Services.Auxilio_GiroService();
                    List<Xpinn.Auxilios.Entities.Auxilios_Giros> lstAuxilios = new List<Xpinn.Auxilios.Entities.Auxilios_Giros>();
                    Xpinn.Auxilios.Entities.Auxilios_Giros pEntAux = new Xpinn.Auxilios.Entities.Auxilios_Giros();
                    pEntAux.numero_auxilio = Convert.ToInt32(pNum_Radica);
                    lstAuxilios = BOAuxilios.ListarAuxilio_giro(pEntAux, (Usuario)Session["usuario"]);
                    if (lstAuxilios.Count > 0)
                    {
                        gvDistribucion.Visible = true;
                        lblmsjDis.Visible = false;
                        gvDistribucion.DataSource = lstAuxilios;
                        gvDistribucion.DataBind();
                    }
                    else
                    {
                        gvDistribucion.Visible = false;
                        lblmsjDis.Visible = true;
                    }
                }

                mpeDistribucion.Show();        
            }
        }
        catch(Exception ex)
        {
            BOexcepcion.Throw(AprobacionService.CodigoPrograma, "gvLista_RowCommand", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        mpeDistribucion.Hide();
    }
}