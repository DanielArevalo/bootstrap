using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;

public partial class Nuevo : GlobalWeb
{

    GiroDistribucionService objGiro = new GiroDistribucionService();
    PoblarListas Poblar = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[objGiro.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(objGiro.CodigoPrograma, "E");
            else
                VisualizarOpciones(objGiro.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objGiro.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAplicar.ActiveViewIndex = 0;
                Session["DatosGiro"] = null;
                if (Session[objGiro.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[objGiro.CodigoPrograma + ".id"].ToString();
                    Session.Remove(objGiro.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);                    
                }
            }
            else
            {
                Calcular();
            }
    }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objGiro.GetType().Name + "L", "Page_Load", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            GiroDistribucion giro = objGiro.ConsultarGiroDistribucionServices(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            if (giro != null)
            {
                if (giro.idgiro != 0)
                {
                    txtCodigoG.Text = giro.idgiro.ToString();
                    txtFecha.Text = giro.fecha_distribucion != null ? giro.fecha_distribucion.ToShortDateString() : "";
                    txtTipoActoGiro.Text = giro.Descripcion != "" ? giro.Descripcion : "";
                    txtValor.Text = giro.valor != 0 ? giro.valor.ToString() : "";
                    txtCodPersona.Text = giro.cod_persona != 0 ? giro.cod_persona.ToString() : "";
                    try {
                        txtIdentific.Text = Session["Idccion"] != null ? Session["Idccion"].ToString() : "";
                        txtNombre.Text = Session["Nombre"] != null ? Session["Nombre"].ToString() : "";
                    }
                    catch
                    {
                        VerError("No se pudo obtener datos");
                    }
                    txtCodOperacion.Text = giro.cod_Operacion != 0 ? giro.cod_Operacion.ToString() : "";
                    txtRadicacion.Text = giro.numero_radicacion != 0 ? giro.numero_radicacion.ToString() : "";
                    txtNumComprobante.Text = giro.numero_Com != 0 ? giro.numero_Com.ToString() : "";
                    if (giro.tipo_Comp != null && giro.tipo_Comp != "")
                    {
                        txtTipoComprobante.Text = giro.tipo_Comp.ToString();
                    }
                    //CONSULTAR SI EXISTEN DISTRIBUIDOS GIROS
                    Xpinn.FabricaCreditos.Services.Credito_GiroService GiroDistri = new Xpinn.FabricaCreditos.Services.Credito_GiroService();
                    List<Xpinn.FabricaCreditos.Entities.Credito_Giro> lstDistribucion = new List<Xpinn.FabricaCreditos.Entities.Credito_Giro>();
                    Xpinn.FabricaCreditos.Entities.Credito_Giro pEntidad = new Xpinn.FabricaCreditos.Entities.Credito_Giro();
                    if (txtRadicacion.Text != "")
                    {
                        // Trae los datos de distribución del crédito dados en la aprobación, si el giro no ha sido distribuido ya.                       
                        if (giro.distribucion == 0)
                        {
                            pEntidad.numero_radicacion = Convert.ToInt64(txtRadicacion.Text);
                            lstDistribucion = GiroDistri.ConsultarCredito_Giro(pEntidad, (Usuario)Session["usuario"]);

                            if (lstDistribucion.Count > 0)
                            {
                                List<Giro> lstDist = new List<Giro>();
                                foreach (Xpinn.FabricaCreditos.Entities.Credito_Giro rData in lstDistribucion)
                                {
                                    Giro pGiro = new Giro();
                                    pGiro.idgiro = rData.idgiro;
                                    pGiro.cod_persona = rData.cod_persona;
                                    pGiro.identificacion = rData.identificacion;
                                    pGiro.nombre = rData.nombre;
                                    pGiro.valor = rData.valor;
                                    pGiro.forma_pago = null;
                                    pGiro.cod_banco = null;
                                    pGiro.num_referencia = null;
                                    pGiro.cod_banco1 = null;
                                    pGiro.num_referencia1 = null;
                                    lstDist.Add(pGiro);
                                }
                                Session["DatosGiro"] = lstDist;
                                gvGiros.DataSource = lstDist;
                                gvGiros.DataBind();
                                Calcular();
                            }
                            else
                            {
                                //CONSULTANDO EN LA TABLA AUXILIO_GIRO
                                Xpinn.Auxilios.Services.Auxilio_GiroService BOAuxilios = new Xpinn.Auxilios.Services.Auxilio_GiroService();
                                List<Xpinn.Auxilios.Entities.Auxilios_Giros> lstAuxilios = new List<Xpinn.Auxilios.Entities.Auxilios_Giros>();
                                Xpinn.Auxilios.Entities.Auxilios_Giros pEntAux = new Xpinn.Auxilios.Entities.Auxilios_Giros();
                                try
                                {
                                    pEntAux.numero_auxilio = Convert.ToInt32(txtRadicacion.Text);
                                    lstAuxilios = BOAuxilios.ListarAuxilio_giro(pEntAux, (Usuario)Session["usuario"]);
                                    if (lstAuxilios.Count > 0)
                                    {
                                        List<Giro> lstDist = new List<Giro>();
                                        foreach (Xpinn.Auxilios.Entities.Auxilios_Giros rDist in lstAuxilios)
                                        {
                                            Giro pGiro = new Giro();
                                            pGiro.idgiro = rDist.idgiro;
                                            pGiro.cod_persona = rDist.cod_persona;
                                            pGiro.identificacion = rDist.identificacion;
                                            pGiro.nombre = rDist.nombre;
                                            pGiro.valor = rDist.valor;
                                            pGiro.forma_pago = null;
                                            pGiro.cod_banco = null;
                                            pGiro.num_referencia = null;
                                            pGiro.cod_banco1 = null;
                                            pGiro.num_referencia1 = null;
                                            lstDist.Add(pGiro);
                                        }
                                        Session["DatosGiro"] = lstDist;
                                        gvGiros.DataSource = lstDist;
                                        gvGiros.DataBind();
                                        Calcular();
                                    }
                                    else
                                        InicializarGiros();
                                }
                                catch
                                {
                                    InicializarGiros();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                VerError("Error al cargar los datos de la persona");
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objGiro.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            ctlMensaje.MostrarMensaje("Desea registrar la distribución de los datos ingresados?");
        }
    }

    protected void ddlFormaPagotemfie_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlFormaPagotemfie = (DropDownListGrid)sender;

        int rowIndex = Convert.ToInt32(ddlFormaPagotemfie.CommandArgument);

        DropDownListGrid ddlBancoGiroTem = (DropDownListGrid)gvGiros.Rows[rowIndex].FindControl("ddlBancoGiroTem");
        DropDownListGrid ddlCuentaGiroTemp = (DropDownListGrid)gvGiros.Rows[rowIndex].FindControl("ddlCuentaGiroTemp");
        DropDownListGrid ddlBancoDestinoTem = (DropDownListGrid)gvGiros.Rows[rowIndex].FindControl("ddlBancoDestinoTem");
        TextBox txtCuentaDestino = (TextBox)gvGiros.Rows[rowIndex].FindControl("txtCuentaDestino");
        DropDownListGrid ddlTipoCuenta = (DropDownListGrid)gvGiros.Rows[rowIndex].FindControl("ddlTipoCuenta");

        if (ddlFormaPagotemfie.SelectedValue != "")
        {
            if (ddlFormaPagotemfie.SelectedValue == "1" || ddlFormaPagotemfie.SelectedIndex == 0)
            {
                ddlBancoGiroTem.Visible = false;
                ddlCuentaGiroTemp.Visible = false;
                ddlBancoDestinoTem.Visible = false;
                txtCuentaDestino.Visible = false;
                ddlTipoCuenta.Visible = false;
            }
            if (ddlFormaPagotemfie.SelectedValue == "2")
            {
                ddlBancoGiroTem.Visible = true;
                ddlCuentaGiroTemp.Visible = true;
                txtCuentaDestino.Visible = false;
                ddlBancoDestinoTem.Visible = false;
                ddlTipoCuenta.Visible = false;
            }
            if (ddlFormaPagotemfie.SelectedValue == "3")
            {
                ddlBancoGiroTem.Visible = true;
                ddlCuentaGiroTemp.Visible = true;
                ddlBancoDestinoTem.Visible = true;
                txtCuentaDestino.Visible = true;
                ddlTipoCuenta.Visible = true;
            }
        }
    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            TextBoxGrid txtIdentificacion = (TextBoxGrid)sender;
            int rowIndex = Convert.ToInt32(txtIdentificacion.CommandArgument);

            string Nombre = "";
            DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdentificacion.Text, (Usuario)Session["usuario"]);

            Label lblcod_persona = (Label)gvGiros.Rows[rowIndex].FindControl("lblcod_persona");
            if (DatosPersona.cod_persona != 0)
            {
                if (lblcod_persona != null)
                    lblcod_persona.Text = DatosPersona.cod_persona.ToString();
                if (DatosPersona.nombre != null)
                {
                    Nombre = DatosPersona.nombre;
                    gvGiros.Rows[rowIndex].Cells[3].Text = Nombre;
                }
            }
            else
            {
                gvGiros.Rows[rowIndex].Cells[3].Text = Nombre;
                lblcod_persona.Text = "";
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objGiro.CodigoPrograma, "txtIdentificacion_TextChanged", ex);
        }
    }


    void CargarCuentas(DropDownListGrid ddlDrop,int pIndice)
    {
        Int64 codbanco = 0;
        try
        {
            codbanco = Convert.ToInt64(ddlDrop.SelectedValue);
        }
        catch
        {
        }
        if (codbanco != 0)
        {
            DropDownListGrid ddlCuentaOrigen = (DropDownListGrid)gvGiros.Rows[pIndice].FindControl("ddlCuentaGiroTemp");
            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Usuario usuario = (Usuario)Session["usuario"];
            ddlCuentaOrigen.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
            ddlCuentaOrigen.DataTextField = "num_cuenta";
            ddlCuentaOrigen.DataValueField = "idctabancaria";
            ddlCuentaOrigen.DataBind();
        }
    }


    protected void ddlBancoGiroTem_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlBancoGiroTem = (DropDownListGrid)sender;
        int rowIndex = Convert.ToInt32(ddlBancoGiroTem.CommandArgument);
        if (ddlBancoGiroTem != null)
        {
            DropDownListGrid ddlCuentaGiroTemp = (DropDownListGrid)gvGiros.Rows[rowIndex].FindControl("ddlCuentaGiroTemp");
            if (ddlBancoGiroTem.SelectedIndex != 0)
                CargarCuentas(ddlBancoGiroTem, rowIndex);
            else
            {
                if (ddlCuentaGiroTemp != null)
                    ddlCuentaGiroTemp.Items.Clear();
            }
        }
    }


    protected void gvGiros_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Usuario pUsuario = (Usuario)Session["usuario"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Dictionary<int, string> ListaFormaPago = new Dictionary<int, string>();
                ListaFormaPago.Add(0, "Seleccione un item");
                ListaFormaPago.Add(1, "Efectivo");
                ListaFormaPago.Add(2, "Cheque");
                ListaFormaPago.Add(3, "Transferencia");

                DropDownListGrid ddlFormaPagotemfie = e.Row.FindControl("ddlFormaPagotemfie") as DropDownListGrid;
                DropDownListGrid ddlBancoGiroTem = e.Row.FindControl("ddlBancoGiroTem") as DropDownListGrid;
                DropDownListGrid ddlCuentaGiroTemp = e.Row.FindControl("ddlCuentaGiroTemp") as DropDownListGrid;
                DropDownListGrid ddlBancoDestinoTem = e.Row.FindControl("ddlBancoDestinoTem") as DropDownListGrid;
                TextBox txtCuentaDestino = e.Row.FindControl("txtCuentaDestino") as TextBox;
                DropDownListGrid ddlTipoCuenta = (DropDownListGrid)e.Row.FindControl("ddlTipoCuenta");
                
                if (ddlFormaPagotemfie != null)
                {
                    ddlFormaPagotemfie.CssClass = "textbox";
                    ddlFormaPagotemfie.DataSource = ListaFormaPago.ToList();
                    ddlFormaPagotemfie.DataTextField = "Value";
                    ddlFormaPagotemfie.DataValueField = "Key";
                    ddlFormaPagotemfie.DataBind();

                    Label lblFormaPagotemfie = e.Row.FindControl("lblFormaPagotemfie") as Label;
                    if (lblFormaPagotemfie.Text != "")                    
                        ddlFormaPagotemfie.SelectedValue = lblFormaPagotemfie.Text;

                    if (ddlFormaPagotemfie.SelectedValue == "1" || ddlFormaPagotemfie.SelectedIndex == 0)
                    {
                        ddlBancoGiroTem.Visible = false;
                        ddlCuentaGiroTemp.Visible = false;
                        ddlBancoDestinoTem.Visible = false;
                        txtCuentaDestino.Visible = false;
                        ddlTipoCuenta.Visible = false;
                    }
                    if (ddlFormaPagotemfie.SelectedValue == "2")
                    {
                        ddlBancoGiroTem.Visible = true;
                        ddlCuentaGiroTemp.Visible = true;
                        txtCuentaDestino.Visible = false;
                        ddlBancoDestinoTem.Visible = false;
                        ddlTipoCuenta.Visible = false;
                    }
                    if (ddlFormaPagotemfie.SelectedValue == "3")
                    {
                        ddlBancoGiroTem.Visible = true;
                        ddlCuentaGiroTemp.Visible = true;
                        ddlBancoDestinoTem.Visible = true;
                        txtCuentaDestino.Visible = true;
                        ddlTipoCuenta.Visible = true;
                    }
                }

                if (ddlBancoGiroTem != null)
                    Poblar.PoblarListaDesplegable("V_BANCOS_ENTIDAD", ddlBancoGiroTem, pUsuario);
                
                if(ddlBancoDestinoTem != null)
                    Poblar.PoblarListaDesplegable("BANCOS", "COD_BANCO,NOMBREBANCO", "", "1", ddlBancoDestinoTem, pUsuario);

                if (ddlTipoCuenta != null)
                {
                    ddlTipoCuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
                    ddlTipoCuenta.Items.Insert(1, new ListItem("Corriente", "1"));
                    ddlTipoCuenta.DataBind();
                }

                Label lblBancoGiroTem = e.Row.FindControl("lblBancoGiroTem") as Label;
                if (lblBancoGiroTem.Text != "")
                {
                    ddlBancoGiroTem.SelectedValue = lblBancoGiroTem.Text;                    
                    Int64 codbanco = 0;
                    try
                    {
                        codbanco = Convert.ToInt64(ddlBancoGiroTem.SelectedValue);
                    }
                    catch
                    {
                    }
                    if (codbanco != 0)
                    {
                        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
                        Usuario usuario = (Usuario)Session["usuario"];
                        ddlCuentaGiroTemp.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
                        ddlCuentaGiroTemp.DataTextField = "num_cuenta";
                        ddlCuentaGiroTemp.DataValueField = "idctabancaria";
                        ddlCuentaGiroTemp.DataBind();
                    }
                }
                
                Label lblCuentaGiro = e.Row.FindControl("lblCuentaGiro") as Label;
                if (lblCuentaGiro.Text != "")
                    ddlCuentaGiroTemp.SelectedValue = lblCuentaGiro.Text;

                Label lblBancoDestino = e.Row.FindControl("lblBancoDestino") as Label;
                if (lblBancoDestino.Text != "")
                    ddlBancoDestinoTem.SelectedValue = lblBancoDestino.Text;

                Label lblTipoCuenta = e.Row.FindControl("lblTipoCuenta") as Label;
                if (lblTipoCuenta.Text != "")
                    ddlTipoCuenta.SelectedValue = lblTipoCuenta.Text;

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objGiro.CodigoPrograma, "gvLista_RowDataBound", ex);
        }
    }

    protected void InicializarGiros()
    {
        List<Giro> listaGiro = new List<Giro>();
        for (int i = gvGiros.Rows.Count; i < 4; i++)
        {
            Giro egiro = new Giro();
            egiro.idgiro = -1;
            egiro.identificacion = "";
            egiro.nombre = "";
            egiro.valor = 0;
            egiro.forma_pago = null;
            egiro.cod_banco = null;
            egiro.num_referencia = null;
            egiro.cod_banco1 = null;
            egiro.num_referencia1 = null;
            egiro.tipo_cuenta = 0;
            egiro.cod_persona = null;
            listaGiro.Add(egiro);
        }
        gvGiros.DataSource = listaGiro;
        gvGiros.DataBind();

        Session["DatosGiro"] = listaGiro;
    }

    protected void btnAdicionarFila_Click(object sender, EventArgs e)
    {
        List<Giro> LstPrograma = new List<Giro>();

        // Obtener los datos de la lista de giros
        ObtenerListaGiro();
        
        // Crear giro vacio para adicionar al detalle
        Giro egiro = new Giro();
        egiro.idgiro = -1;
        egiro.identificacion = "";
        egiro.nombre = "";
        egiro.valor = 0;
        egiro.forma_pago = null;
        egiro.cod_banco = null;
        egiro.num_referencia = null;
        egiro.cod_banco1 = null;
        egiro.num_referencia1 = null;
        egiro.tipo_cuenta = 0;
        egiro.cod_persona = null;

        // Si ya existen detalles cargar en la lista
        if (Session["DatosGiro"] != null)
        {
            LstPrograma = (List<Giro>)Session["DatosGiro"];                        
        }

        // Adicionar el nuevo detalle a la lista y cargar a la gridview
        LstPrograma.Add(egiro);
        gvGiros.DataSource = LstPrograma;
        gvGiros.DataBind();
        Calcular();
        Session["DatosGiro"] = LstPrograma;
    }


    protected void gvGiros_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ObtenerListaGiro();
        Int64 conseID = Convert.ToInt64(gvGiros.DataKeys[e.RowIndex].Value.ToString());
        List<Giro> lista = new List<Giro>();
        lista = (List<Giro>)Session["DatosGiro"];
        if (conseID > 0)
        {
            try
            {
                foreach (Giro acti in lista)
                {
                    if (acti.idgiro == conseID)
                    {
                        lista.Remove(acti);
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
            lista.RemoveAt((gvGiros.PageIndex * gvGiros.PageSize) + e.RowIndex);
        }
        Session["DatosGiro"] = lista;
        gvGiros.DataSource = lista;
        gvGiros.DataBind();
        Calcular();
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objGiro.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
            GiroServices GiroService = new GiroServices();
            Usuario pUsuario = (Usuario)Session["usuario"];

            Giro giro = GiroService.ConsultarGiro(idObjeto, (Usuario)Session["usuario"]);
            
            List<Xpinn.Tesoreria.Entities.Giro> lstDetalle = new List<Xpinn.Tesoreria.Entities.Giro>();
            //CARGANDO LOS DATOS DE LA GRID
            if (giro != null)
            {
                foreach (GridViewRow rfila in gvGiros.Rows)
                {
                    Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
                    Xpinn.Tesoreria.Entities.Giro eGiro = new Xpinn.Tesoreria.Entities.Giro();

                    decimales txtValortem = (decimales)rfila.FindControl("txtValortem");

                    DropDownListGrid ddlFormaPagotemfie = (DropDownListGrid)rfila.FindControl("ddlFormaPagotemfie");
                    DropDownListGrid ddlBancoGiroTem = (DropDownListGrid)rfila.FindControl("ddlBancoGiroTem");
                    DropDownListGrid ddlCuentaGiroTemp = (DropDownListGrid)rfila.FindControl("ddlCuentaGiroTemp");
                    DropDownListGrid ddlBancoDestinoTem = (DropDownListGrid)rfila.FindControl("ddlBancoDestinoTem");
                    TextBox txtCuentaDestino = (TextBox)rfila.FindControl("txtCuentaDestino");
                    DropDownListGrid ddlTipoCuenta = (DropDownListGrid)rfila.FindControl("ddlTipoCuenta");
                    TextBoxGrid txtIdentificacionDeta = (TextBoxGrid)rfila.FindControl("txtIdentificacion");
                    String lblNombre = rfila.Cells[3].Text.Replace("&nbsp;", "");
                    Label lblcod_persona = (Label)rfila.FindControl("lblcod_persona");

                    if (txtIdentificacionDeta.Text != "" && lblNombre != "" && lblNombre != "&nbsp;" && ddlFormaPagotemfie.SelectedIndex != 0 && txtValortem.Text != "0")
                    {
                        Int64 pNum_Radica = txtRadicacion.Text == "" ? 0 : Convert.ToInt64(txtRadicacion.Text);

                        eGiro.idgiro = 0;
                        eGiro.cod_ope = Convert.ToInt64(giro.cod_ope);
                        try
                        {

                            //se quita la parte comentareada 12/11/2019 por que no estaba recorriendo para conocer el respectivo titular de cada giro 
                            //estaba dejando por defecto el cliente creado en el giro original
                            if (lblcod_persona.Text.Trim() != "" || lblcod_persona.Text != "0")
                                eGiro.cod_persona = Convert.ToInt64(lblcod_persona.Text);
                            else
                               eGiro.cod_persona = Convert.ToInt64(txtCodPersona.Text);
                        }
                        catch 
                        {
                            eGiro.cod_persona = Convert.ToInt64(txtCodPersona.Text);
                        }
                        eGiro.forma_pago = Convert.ToInt32(ddlFormaPagotemfie.SelectedValue);
                        eGiro.tipo_acto = Convert.ToInt32(giro.tipo_acto);
                        eGiro.fec_reg = DateTime.Now;
                        eGiro.fec_giro = DateTime.MinValue;
                        eGiro.numero_radicacion = pNum_Radica;
                        eGiro.usu_gen = pUsuario.nombre;
                        eGiro.usu_apli = null;
                        eGiro.estado = 1;
                        eGiro.usu_apro = null;

                        if (ddlFormaPagotemfie.SelectedValue == "3" || ddlFormaPagotemfie.SelectedValue == "2")
                            CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlBancoGiroTem.SelectedValue), ddlCuentaGiroTemp.SelectedItem.Text.Trim(), (Usuario)Session["usuario"]);
                        Int64 idCta = CuentaBanc.idctabancaria;

                        //DATOS DE FORMA DE PAGO
                        if (ddlFormaPagotemfie.SelectedValue == "3") //"Transferencia"
                        {
                            eGiro.idctabancaria = idCta;
                            eGiro.cod_banco = Convert.ToInt32(ddlBancoDestinoTem.SelectedValue);
                            eGiro.num_cuenta = txtCuentaDestino.Text;
                            eGiro.tipo_cuenta = Convert.ToInt32(ddlTipoCuenta.SelectedValue);
                        }
                        else if (ddlFormaPagotemfie.SelectedValue == "2") //Cheque
                        {
                            eGiro.idctabancaria = idCta;
                            eGiro.cod_banco = 0;        //NULO
                            eGiro.num_cuenta = null;    //NULO
                            eGiro.tipo_cuenta = -1;     //NULO
                        }
                        else
                        {
                            eGiro.idctabancaria = 0;
                            eGiro.cod_banco = 0;
                            eGiro.num_cuenta = null;
                            eGiro.tipo_cuenta = -1;
                        }
                        eGiro.fec_apro = DateTime.MinValue;
                        eGiro.cob_comision = 0;
                        eGiro.valor = Convert.ToInt64(txtValortem.Text.Replace(".", ""));

                        if (lblcod_persona.Text != "")
                            eGiro.cod_persona_deta = Convert.ToInt64(lblcod_persona.Text);

                        eGiro.identificacion = txtIdentificacionDeta.Text;
                        if (lblNombre != "" && lblNombre != null)
                            eGiro.nombre = lblNombre;

                        if (eGiro.identificacion != null && eGiro.nombre != null && eGiro.forma_pago != null && eGiro.valor != 0 && eGiro.valor != null)
                        {
                            lstDetalle.Add(eGiro);
                        }
                    }
                }
            }
            //INSERTANDO LA DIFERENCIA DEL GIRO A NOMBRE DEL CLIENTE
            Calcular();
            TextBox txtValortotal = (TextBox)gvGiros.FooterRow.FindControl("txtValortotal");
            if (txtValortotal.Text != "")
            {
                if (Convert.ToInt64(txtValortotal.Text.Replace(".", "")) < Convert.ToInt64(txtValor.Text.Replace(".", "")))
                {
                    Decimal pValor = (Convert.ToDecimal(txtValor.Text.Replace(".", "")) - Convert.ToDecimal(txtValortotal.Text.Replace(".", "")));
                    Giro pGiro = new Giro();
                    pGiro = GiroService.ConsultarGiro(txtCodigoG.Text, (Usuario)Session["usuario"]);
                    Xpinn.Tesoreria.Entities.Giro ultGiro = new Xpinn.Tesoreria.Entities.Giro();
                    ultGiro.idgiro = 0;
                    ultGiro.cod_ope = pGiro.cod_ope == null ? 0 : Convert.ToInt64(pGiro.cod_ope);
                    ultGiro.cod_persona = Convert.ToInt64(pGiro.cod_persona);
                    ultGiro.forma_pago = Convert.ToInt32(pGiro.forma_pago);
                    ultGiro.tipo_acto = Convert.ToInt32(pGiro.tipo_acto);
                    ultGiro.fec_reg = Convert.ToDateTime(pGiro.fec_reg);
                    ultGiro.fec_giro = pGiro.fec_giro == null ? DateTime.MinValue : Convert.ToDateTime(pGiro.fec_giro);
                    ultGiro.numero_radicacion = pGiro.numero_radicacion == null ? 0 : Convert.ToInt64(pGiro.numero_radicacion);
                    ultGiro.usu_gen = pGiro.usu_gen == null ? null : pGiro.usu_gen;
                    ultGiro.usu_apli = pGiro.usu_apli == null ? null : pGiro.usu_apli;
                    ultGiro.estado = 1;
                    ultGiro.usu_apro = pGiro.usu_apro == null ? null : pGiro.usu_apro;
                    ultGiro.idctabancaria = pGiro.idctabancaria;
                    ultGiro.cod_banco = pGiro.cod_banco == null ? 0 : Convert.ToInt32(pGiro.cod_banco);
                    ultGiro.num_cuenta = pGiro.num_cuenta == null ? null : pGiro.num_cuenta;
                    ultGiro.tipo_cuenta = Convert.ToInt32(pGiro.tipo_cuenta);
                    ultGiro.fec_apro = pGiro.fec_apro == null ? DateTime.MinValue : Convert.ToDateTime(pGiro.fec_apro);
                    ultGiro.cob_comision = Convert.ToInt32(pGiro.cob_comision);
                    ultGiro.valor = Convert.ToInt64(pValor);

                    ultGiro.cod_persona_deta = Convert.ToInt64(txtCodPersona.Text.Trim());
                    ultGiro.identificacion = txtIdentific.Text.Trim();
                    ultGiro.nombre = txtNombre.Text.Trim();

                    lstDetalle.Add(ultGiro);
                }
            }

                //CREAR                
            if(lstDetalle.Count > 0)
                objGiro.InsertarGiros(lstDetalle, Convert.ToInt64(txtCodigoG.Text), (Usuario)Session["usuario"]);

            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            lblCodGiro.Text = txtCodigoG.Text;
            mvAplicar.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objGiro.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }


    protected List<Giro> ObtenerListaGiro()
    {
        //try
        //{
            List<Giro> lstDetalle = new List<Giro>();
            List<Giro> lista = new List<Giro>();

            foreach (GridViewRow rfila in gvGiros.Rows)
            {
                Giro eGiro = new Giro();

                Label lblCodigo = (Label)rfila.FindControl("lblCodigo");
                if (lblCodigo.Text != "")
                    eGiro.idgiro = Convert.ToInt64(lblCodigo.Text);

                TextBoxGrid txtIdentificacion = (TextBoxGrid)rfila.FindControl("txtIdentificacion");
                if (txtIdentificacion.Text != "")
                    eGiro.identificacion = txtIdentificacion.Text;

                Label lblcod_persona = (Label)rfila.FindControl("lblcod_persona");
                if (lblcod_persona.Text != "")
                    eGiro.cod_persona = Convert.ToInt64(lblcod_persona.Text);
               
                String lblNombre = rfila.Cells[3].Text.Replace("&nbsp;","");
                if (lblNombre != "" && lblNombre != null)
                    eGiro.nombre = lblNombre;

                decimales txtValortem = (decimales)rfila.FindControl("txtValortem");
                if (txtValortem != null)
                    eGiro.valor = Convert.ToDecimal(txtValortem.Text);

                DropDownListGrid ddlFormaPagotemfie = (DropDownListGrid)rfila.FindControl("ddlFormaPagotemfie");
                if (ddlFormaPagotemfie.SelectedIndex != 0 || ddlFormaPagotemfie.SelectedValue != "")
                    eGiro.forma_pago = Convert.ToInt64(ddlFormaPagotemfie.SelectedValue);

                DropDownListGrid ddlBancoGiroTem = (DropDownListGrid)rfila.FindControl("ddlBancoGiroTem");
                if (ddlBancoGiroTem.SelectedIndex != 0 || ddlBancoGiroTem.SelectedValue != "")
                    eGiro.cod_banco = Convert.ToInt64(ddlBancoGiroTem.SelectedValue);
                
                DropDownListGrid ddlCuentaGiroTemp = (DropDownListGrid)rfila.FindControl("ddlCuentaGiroTemp");
                if (ddlCuentaGiroTemp.SelectedValue != "" || ddlCuentaGiroTemp.SelectedIndex != 0)
                    eGiro.num_referencia = ddlCuentaGiroTemp.SelectedValue;

                DropDownListGrid ddlBancoDestinoTem = (DropDownListGrid)rfila.FindControl("ddlBancoDestinoTem");
                if (ddlBancoDestinoTem.SelectedValue != "" || ddlBancoDestinoTem.SelectedIndex != 0)
                    eGiro.cod_banco1 = Convert.ToInt64(ddlBancoDestinoTem.SelectedValue);

                TextBox txtCuentaDestino = (TextBox)rfila.FindControl("txtCuentaDestino");
                if (txtCuentaDestino.Text != "")
                    eGiro.num_referencia1 = txtCuentaDestino.Text;

                DropDownListGrid ddlTipoCuenta = (DropDownListGrid)rfila.FindControl("ddlTipoCuenta");
                if (ddlTipoCuenta != null)
                    eGiro.tipo_cuenta = Convert.ToInt32(ddlTipoCuenta.SelectedValue);

                lista.Add(eGiro);
                Session["DatosGiro"] = lista;

                if (eGiro.identificacion != null && eGiro.nombre != null && eGiro.forma_pago != null && eGiro.valor != 0 && eGiro.valor != null)
                {
                    lstDetalle.Add(eGiro);
                }
            }
            return lstDetalle;
        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(objGiro.CodigoPrograma, "ObtenerListaGiro", ex);
        //    return null;
        //}
    }

    public Boolean ValidarDatos()
    {
        TextBox txtValortotal = (TextBox)gvGiros.FooterRow.FindControl("txtValortotal");
        txtValortotal.Text = txtValortotal.Text == "" ? "0" : txtValortotal.Text; 
        if (txtValor.Text.Trim() != "")
        {
            if (Convert.ToDecimal(txtValor.Text) < Convert.ToDecimal(txtValortotal.Text))
            {
                VerError("El valor total excede el valor del Giro");
                return false;
            }
        }
        else
        {
            VerError("No hay Valor En este Giro");
            return false;
        }

        if (gvGiros.Rows.Count > 0)
        {
            foreach (GridViewRow rfila in gvGiros.Rows)
            {
                TextBoxGrid txtIdentificacion = (TextBoxGrid)rfila.FindControl("txtIdentificacion");
                String lblNombre = rfila.Cells[3].Text.Replace("&nbsp;", "");
                decimales txtValortem = (decimales)rfila.FindControl("txtValortem");
                
                DropDownListGrid ddlFormaPagotemfie = (DropDownListGrid)rfila.FindControl("ddlFormaPagotemfie");
                DropDownListGrid ddlBancoGiroTem = (DropDownListGrid)rfila.FindControl("ddlBancoGiroTem");
                DropDownListGrid ddlCuentaGiroTemp = (DropDownListGrid)rfila.FindControl("ddlCuentaGiroTemp");
                DropDownListGrid ddlBancoDestinoTem = (DropDownListGrid)rfila.FindControl("ddlBancoDestinoTem");
                TextBox txtCuentaDestino = (TextBox)rfila.FindControl("txtCuentaDestino");

                if (txtIdentificacion.Text != "" && lblNombre == "")
                {
                    VerError("Error en el detalle Fila : " + (rfila.RowIndex + 1) + " Ingrese una identificación válida.");
                    return false;
                }
                if (txtIdentificacion.Text != "" && lblNombre != "" && txtValor.Text != "" && txtValor.Text != "0")
                {
                    if (ddlFormaPagotemfie.SelectedIndex == 0)
                    {
                        VerError("Error en el detalle Fila : " + (rfila.RowIndex + 1) + " Debe seleccionar la forma de pago.");
                        return false;
                    }                    
                }
                if (txtIdentificacion.Text != "" && lblNombre != "" && txtValor.Text != "" && txtValor.Text != "0" && ddlFormaPagotemfie.SelectedIndex != 0)
                {
                    if (ddlFormaPagotemfie.SelectedValue == "2" || ddlFormaPagotemfie.SelectedValue == "3")
                    {
                        if (ddlBancoGiroTem.SelectedIndex == 0)
                        {
                            VerError("Error en el detalle Fila : " + (rfila.RowIndex + 1) + " Debe seleccionar el banco de Giro.");
                            return false;
                        }
                        if (ddlCuentaGiroTemp.SelectedItem == null)
                        {
                            VerError("Error en el detalle Fila : " + (rfila.RowIndex + 1) + " El banco seleccionado no cuenta con cuenta de giro.");
                            return false;
                        }
                        if (ddlFormaPagotemfie.SelectedValue == "3")
                        {
                            if (ddlBancoDestinoTem.SelectedIndex == 0)
                            {
                                VerError("Error en el detalle Fila : " + (rfila.RowIndex + 1) + " Debe seleccionar el banco destino.");
                                return false;
                            }
                            if (txtCuentaDestino.Text == "")
                            {
                                VerError("Error en el detalle Fila : " + (rfila.RowIndex + 1) + " Debe ingresar la cuenta destino.");
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return true;
    }

    
    protected void Calcular()
    {
        try
        {
            TextBox txtValortotal = (TextBox)gvGiros.FooterRow.FindControl("txtValortotal");
            if (txtValortotal != null)
            {
                decimal vr = 0;
                foreach (GridViewRow item in gvGiros.Rows)
                {
                    decimales txtValortem = (decimales)item.FindControl("txtValortem");

                    txtValortem.Text = txtValortem.Text.Trim() != "" ? txtValortem.Text.Replace(".", "") : "0";
                    vr += Convert.ToDecimal(txtValortem.Text);
                }
                gvGiros.FooterRow.Cells[3].Text = "Total :";
                txtValortotal.Text = vr.ToString("n0");
            }
        }
        catch
        { }
    }
   
}