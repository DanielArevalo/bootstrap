using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;


public partial class Nuevo : GlobalWeb
{

    GiroServices GiroServicios = new GiroServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(GiroServicios.CodigoProgramaConsulta, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GiroServicios.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["Beneficiario"] = null;
                CargarDropdown();
                mvAplicar.ActiveViewIndex = 0;
                panelEncabezado.Enabled = false;
                panelPiePagina.Enabled = false;
                if (Session[GiroServicios.CodigoProgramaConsulta + ".id"] != null)
                {
                    idObjeto = Session[GiroServicios.CodigoProgramaConsulta + ".id"].ToString();
                    Session.Remove(GiroServicios.CodigoProgramaConsulta + ".id");
                    ObtenerDatos(idObjeto);
                    Session["TEXTO"] = "modificar";
                }
                else
                {
                    idObjeto = null;
                    Session["TEXTO"] = "grabar";
                    txtFecha.Text = DateTime.Now.ToShortDateString();
                    panelEncabezado.Enabled = true;
                    // Habilitar el botón para guardar los datos
                    Site toolBar = (Site)this.Master;
                    toolBar.eventoGuardar += btnGuardar_Click;
                    toolBar.MostrarGuardar(true);
                    // Deshabilitar los campos que no se necesitan
                    lblCodigo.Visible = false;
                    txtCodigo.Visible = false;
                    lblFechaGiro.Visible = false;
                    txtFechaGiro.Visible = false;
                    lblCodOperacion.Visible = false;
                    txtCodOperacion.Visible = false;
                    lblUsuarioGenera.Visible = false;
                    ddlUsuarioGenera.Visible = false;
                    lblbUsuarioAproba.Visible = false;
                    ddlUsuarioAproba.Visible = false;
                    lblUsuarioAplica.Visible = false;
                    ddlUsuarioAplica.Visible = false;
                    btnConsultaPersonas.Enabled = true;
                    txtIdPersona.Enabled = true;
                }
                ddlForma_Desem_SelectedIndexChanged(ddlForma_Desem, null);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GiroServicios.GetType().Name + "L", "Page_Load", ex);
        }
    }


    protected void CargarDropdown()
    {
        ctlGiro.Inicializar();
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


        PoblarListas PoblarLista = new PoblarListas();
        PoblarLista.PoblarListaDesplegable("TIPO_ACTO_GIRO", "", "", "1", ddlTipoGiro, (Usuario)Session["usuario"]);

        PoblarLista.PoblarListaDesplegable("TIPO_COMP", ddlTipoComprobante, (Usuario)Session["usuario"]);

        ddlEstado.DataSource = ListaEstadosGiro();
        ddlEstado.DataTextField = "descripcion";
        ddlEstado.DataValueField = "codigo";
        ddlEstado.SelectedIndex = 0;
        ddlEstado.DataBind();

        ddlTipo_cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
        ddlTipo_cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
        ddlTipo_cuenta.SelectedIndex = 0;
        ddlTipo_cuenta.DataBind();

        ddlForma_Desem.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        ddlForma_Desem.Items.Insert(1, new ListItem("Efectivo", "1"));
        ddlForma_Desem.Items.Insert(2, new ListItem("Cheque", "2"));
        ddlForma_Desem.Items.Insert(3, new ListItem("Transferencia", "3"));
        ddlForma_Desem.Items.Insert(4, new ListItem("Otros", "4"));
        ddlForma_Desem.SelectedIndex = 0;
        ddlForma_Desem.DataBind();

        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        ddlEntidad_giro.DataSource = bancoService.ListarBancosEntidad(banco, (Usuario)Session["usuario"]);
        ddlEntidad_giro.DataTextField = "nombrebanco";
        ddlEntidad_giro.DataValueField = "cod_banco";
        ddlEntidad_giro.DataBind();
        CargarCuentas();

        ddlEntidad.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlEntidad.DataTextField = "nombrebanco";
        ddlEntidad.DataValueField = "cod_banco";
        ddlEntidad.DataBind();
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {

        try
        {
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
            //  ddlForma_Desem.SelectedValue = pGiro.forma_pago.ToString();

            txtFecha.Text = Convert.ToDateTime(pGiro.fec_reg).ToShortDateString();

            if (pGiro.fec_giro != DateTime.MinValue && pGiro.fec_giro != null)
                txtFechaGiro.Text = Convert.ToDateTime(pGiro.fec_giro).ToShortDateString();

            if (pGiro.tipo_acto != 0)
                ddlTipoGiro.SelectedValue = pGiro.tipo_acto.ToString();

            if (pGiro.cod_ope != 0)
                txtCodOperacion.Text = pGiro.cod_ope.ToString();

            if (pGiro.numero_radicacion != 0)
                txtNumRadicacion.Text = pGiro.numero_radicacion.ToString();

            if (pGiro.num_comp != 0)
                txtNumComprobante.Text = pGiro.num_comp.ToString();

            if (pGiro.tipo_comp != 0)
                ddlTipoComprobante.SelectedValue = pGiro.tipo_comp.ToString();

            if (pGiro.valor != 0)
            {
                txtValor.Text = pGiro.valor.ToString();
                txtValor.Enabled = true;
            }

            if (pGiro.cob_comision == 1)
                chkCobraComision.Checked = true;

            txtValor.Enabled = false;
            panelFormaPago.Enabled = false;
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            if (pGiro.estado != null)
            {
                ddlEstado.SelectedValue = pGiro.estado.ToString();
                if (pGiro.estado == 0 || pGiro.estado == 1) //SI ES PENDIENTE O APROBADO
                {
                    toolBar.MostrarGuardar(true);
                    panelFormaPago.Enabled = true;
                    txtValor.Enabled = true;
                }
            }

            Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
            Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
            /* if (pGiro.idctabancaria != 0)
            {
                CuentaBanc = bancoService.ConsultarCuentasBancarias(Convert.ToInt32(pGiro.idctabancaria), (Usuario)Session["usuario"]);
                if (CuentaBanc.cod_banco != 0)
                {
                    ddlEntidad_giro.SelectedValue = CuentaBanc.cod_banco.ToString();
                    ddlEntidad_giro_SelectedIndexChanged(ddlEntidad_giro, null);
                    ddlCuenta_Giro.SelectedValue = pGiro.idctabancaria.ToString();
                }
            }
            if (pGiro.cod_banco != 0)
                ddlEntidad.SelectedValue = pGiro.cod_banco.ToString();
            if (pGiro.num_cuenta != "")
                txtNum_cuenta.Text = pGiro.num_cuenta;
            if (pGiro.tipo_cuenta != null)
                ddlTipo_cuenta.SelectedValue = pGiro.tipo_cuenta.ToString();
            */
            panelFormaPago.Visible = false;

            ctlGiro.ValueFormaDesem = Convert.ToString(pGiro.forma_pago);

            ctlGiro.ValueEntidadOrigen = Convert.ToString(pGiro.cod_banco1);

            if (pGiro.cod_banco1 > 0)
            {

                ctlGiro.ValueEntidadDest = Convert.ToString(pGiro.cod_banco1);
                ctlGiro.ValueTipoCta = Convert.ToString(pGiro.tipo_cuenta);
                ctlGiro.TextNumCuenta = Convert.ToString(pGiro.num_cuenta);
            }

            Int64 COD_PERSONA = 0;
            COD_PERSONA = Convert.ToInt64(txtCodPersona.Text);
            ctlGiro.cargarCuentasAhorro(COD_PERSONA);

            if (pGiro.usu_apro != "" && pGiro.usu_apro != null)
                ddlUsuarioAproba.SelectedValue = pGiro.usu_apro;
            if (pGiro.usu_apli != "" && pGiro.usu_apli != null)
                ddlUsuarioAplica.SelectedValue = pGiro.usu_apli;
            if (pGiro.usu_gen != "" && pGiro.usu_gen != null)
                ddlUsuarioGenera.SelectedValue = pGiro.usu_gen;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GiroServicios.CodigoProgramaConsulta, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
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


    protected void CargarCuentas()
    {
        Int64 codbanco = Convert.ToInt64(ddlEntidad_giro.SelectedValue);
        if (codbanco != 0)
        {
            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Usuario usuario = (Usuario)Session["usuario"];
            ddlCuenta_Giro.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
            ddlCuenta_Giro.DataTextField = "num_cuenta";
            ddlCuenta_Giro.DataValueField = "idctabancaria";
            ddlCuenta_Giro.DataBind();
        }
    }

    protected void ddlEntidad_giro_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCuentas();
    }


    protected Boolean validarDatos()
    {
        if (idObjeto != "")
        {
            if (ddlTipoGiro.SelectedValue == "")
            {
                VerError("Seleccione el tipo de giro. verifique los datos.");
                ddlTipoGiro.Focus();
                return false;
            }
            //if (ddlTipoComprobante.SelectedValue == "")
            //{
            //    VerError("Seleccione el tipo de comprobante. verifique los datos.");
            //    ddlTipoComprobante.Focus();
            //    return false;
            //}
            if (ddlTipoGiro.SelectedIndex == 0)
            {
                VerError("Seleccione el tipo de giro. verifique los datos.");
                ddlTipoGiro.Focus();
                return false;
            }
            decimal valor = txtValor.Text != "" ? Convert.ToDecimal(txtValor.Text) : 0;
            if (valor <= 0)
            {
                VerError("El valor del giro debe ser mayor de cero. verifique los datos.");
                txtValor.Focus();
                return false;
            }
            if (txtCodPersona.Text.Trim() == "")
            {
                VerError("Debe ingresar la persona a quien se le va a realizar el giro. verifique los datos.");
                txtValor.Focus();
                return false;
            }
            Int64 cod_persona = Convert.ToInt64(txtCodPersona.Text);
            if (cod_persona <= 0)
            {
                VerError("Debe ingresar la persona a quien se le va a realizar el giro. verifique los datos.");
                txtValor.Focus();
                return false;
            }
        }
        /*if (panelFormaPago.Enabled == true)
        {
            if (ddlForma_Desem.SelectedIndex == 0)
            {
                VerError("Seleccione la Forma de pago. verifique los datos.");
                ddlForma_Desem.Focus();
                return false;
            }
            if (ddlForma_Desem.SelectedItem.Text == "Transferencia")
            {
                if (txtNum_cuenta.Text == "")
                {
                    VerError("Ingrese el numero de cuenta");
                    return false;
                }
            }
        }
        */
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (validarDatos())
                ctlMensaje.MostrarMensaje("Desea realizar la actualización de datos?");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GiroServicios.CodigoProgramaConsulta, "btnGuardar_Click", ex);
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        bool pGenerarGiro = false;
        int opcion = 2;
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        Xpinn.Ahorros.Entities.AhorroVista pVar = new Xpinn.Ahorros.Entities.AhorroVista();
        try
        {
            //GRABAR GIRO
            Giro pGiro = new Giro();
            if (idObjeto != "")
            {
                pGiro = GiroServicios.ConsultarGiro(Convert.ToString(idObjeto), (Usuario)Session["usuario"]);
            }
            else
            {
                opcion = 1;
                pGiro.cod_persona = Convert.ToInt64(txtCodPersona.Text);
                pGiro.tipo_acto = Convert.ToInt32(ddlTipoGiro.SelectedValue);
                pGiro.fec_giro = txtFecha.ToDateTime;
                pGiro.fec_reg = txtFecha.ToDateTime;
                pGiro.num_comp = Convert.ToInt64(txtNumComprobante.Text);
                pGiro.tipo_comp = Convert.ToInt32(ddlTipoComprobante.SelectedValue);
                pGiro.usu_gen = ((Usuario)Session["usuario"]).nombre;
                pGiro.estado = 0;
                pGiro.cod_ope = -100;
                pGiro.numero_radicacion = 0;
            }

            pGiro.forma_pago = Convert.ToInt32(ddlForma_Desem.SelectedValue);
            // CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidad_giro.SelectedValue), ddlCuenta_Giro.SelectedItem.Text, (Usuario)Session["usuario"]);
            // Int32 idCta = CuentaBanc.idctabancaria;

            if (ctlGiro.IndiceFormaDesem == 1) //"EFECTIVO"
            {
                pGiro.idctabancaria = 0;
                pGiro.cod_banco = 0;
                pGiro.num_cuenta = null;
                pGiro.tipo_cuenta = -1;
                pGenerarGiro = true;
            }
            if (ctlGiro.IndiceFormaDesem != 1 && ctlGiro.IndiceFormaDesem != 4) //"EFECTIVO"
            {
                //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ctlGiro.ValueEntidadOrigen), ctlGiro.TextCuentaOrigen, (Usuario)Session["usuario"]);
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

                else
                {
                    pGiro.idctabancaria = 0;
                    pGiro.cod_banco = 0;
                    pGiro.num_cuenta = null;
                    pGiro.tipo_cuenta = -1;
                }

            }

            if (ctlGiro.IndiceFormaDesem == 4)
            {
                pGenerarGiro = false;
                pVar.numero_cuenta_ahorro_vista = !string.IsNullOrWhiteSpace(ctlGiro.ValueCuentaAhorro) ? Convert.ToInt64(ctlGiro.ValueCuentaAhorro) : default(long?);
                pGiro.numero_cuenta_ahorro_vista = pVar.numero_cuenta_ahorro_vista;
                pGiro.formadesembolso = "TranferenciaAhorroVistaInterna";
                pGiro.fec_apro_giro = Convert.ToDateTime(txtFechaGiro.Text);
                pGiro.forma_pago = 5;
                pGiro.usu_apro = Convert.ToString(Usuario.nombre);

                if (!pVar.numero_cuenta_ahorro_vista.HasValue)
                {
                    VerError("No tiene una cuenta de ahorro a la vista asociada para esta forma de pago!.");
                }
            }


            pGiro.valor = txtValor.Text != "" ? Convert.ToDecimal(txtValor.Text) : 0;
            Giro pEntidad = new Giro();

            pEntidad = GiroServicios.Crear_ModGiro(pGiro, (Usuario)Session["usuario"], opcion);


           
            if (pEntidad.idgiro != 0)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                if (opcion == 2)
                    lblOperacion.Text = "Modificado";
                else
                    lblOperacion.Text = "Creado";
                lblMsj.Text = pEntidad.idgiro.ToString();
                mvAplicar.ActiveViewIndex = 1;
            }



        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(GiroServicios.CodigoProgramaConsulta, "btnGuardar_Click", ex);
        }
    }

}
