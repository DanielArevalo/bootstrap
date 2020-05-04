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
    AprobacionCtasPorPagarServices AprobacionCtasService = new AprobacionCtasPorPagarServices();

    PoblarListas PoblarLista = new PoblarListas();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {       
            VisualizarOpciones(AprobacionCtasService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarCancelar(false);
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarConsultar(true);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionCtasService.CodigoPrograma, "Page_PreInit", ex);
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
                txtFechaAprobacion.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                CalcularTotal();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionCtasService.CodigoPrograma, "Page_Load", ex);
        }
    }


    void CargarDropDown()
    {

        PoblarListaTipoCtaXPagar(ddlTipoCtaXpagar);

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

        ddlOrdenadoPor.Items.Insert(0, new ListItem("Seleccione un item","0"));
        ddlOrdenadoPor.Items.Insert(1, new ListItem("Código Factura", "C.CODIGO_FACTURA"));
        ddlOrdenadoPor.Items.Insert(2, new ListItem("Número Factura", "C.NUMERO_FACTURA"));
        ddlOrdenadoPor.Items.Insert(3, new ListItem("Fecha Ingreso", "C.FECHA_INGRESO"));
        ddlOrdenadoPor.Items.Insert(4, new ListItem("Fecha Factura", "C.FECHA_FACTURA"));
        ddlOrdenadoPor.Items.Insert(5, new ListItem("Fecha Vencimiento", "C.FECHA_VENCIMIENTO"));
        ddlOrdenadoPor.Items.Insert(6, new ListItem("Identificación", "V.IDENTIFICACION"));
        ddlOrdenadoPor.Items.Insert(7, new ListItem("Nombre", "V.NOMBRE"));
        ddlOrdenadoPor.Items.Insert(8, new ListItem("Vr a Pagar", "D.VALOR"));
        ddlOrdenadoPor.Items.Insert(9, new ListItem("Vr Descuento", "VALOR_DESCUENTO"));

        ddlLuegoPor.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlLuegoPor.Items.Insert(1, new ListItem("Código Factura", "C.CODIGO_FACTURA"));
        ddlLuegoPor.Items.Insert(2, new ListItem("Número Factura", "C.NUMERO_FACTURA"));
        ddlLuegoPor.Items.Insert(3, new ListItem("Fecha Ingreso", "C.FECHA_INGRESO"));
        ddlLuegoPor.Items.Insert(4, new ListItem("Fecha Factura", "C.FECHA_FACTURA"));
        ddlLuegoPor.Items.Insert(5, new ListItem("Fecha Vencimiento", "C.FECHA_VENCIMIENTO"));
        ddlLuegoPor.Items.Insert(6, new ListItem("Identificación", "V.IDENTIFICACION"));
        ddlLuegoPor.Items.Insert(7, new ListItem("Nombre", "V.NOMBRE"));
        ddlLuegoPor.Items.Insert(8, new ListItem("Vr a Pagar", "D.VALOR"));
        ddlLuegoPor.Items.Insert(9, new ListItem("Vr Descuento", "VALOR_DESCUENTO"));
    }


    void CalcularTotal()
    {
        decimal TotalAprobar = 0;
        int cont = 0;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
            DropDownListGrid ddlFormaPago = (DropDownListGrid)rFila.FindControl("ddlFormaPago");
            if (cbSeleccionar.Checked)
            {
                decimal valor;
                valor = rFila.Cells[9].Text != "&nbsp;" ? Convert.ToDecimal(rFila.Cells[9].Text) : 0;
                ddlFormaPago.Enabled = true;
                TotalAprobar += valor;
                cont++;
            }
        }
        txtVrAprobar.Text = TotalAprobar.ToString();        
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        limpiar();
    }
    
    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["DTCUENTAS"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.Columns[6].Visible = true;
            gvLista.Columns[12].Visible = false;
            gvLista.Columns[13].Visible = false;
            gvLista.Columns[14].Visible = false;
            gvLista.Columns[15].Visible = false;
            gvLista.Columns[16].Visible = false;
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTCUENTAS"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=AprobacionCuentasXPagar.xls");
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


    Boolean ValidarRangoFechas()
    {
        if (txtFechaVencIni.Text != "")
        {
            if (txtFechaVencFin.Text == "")
            {
                VerError("Debe Ingresar ambas fechas en el rango de fechas de vencimiento");
                txtFechaVencFin.Focus();
                return false;
            }
        }
        if (txtFechaVencFin.Text != "")
        {
            if (txtFechaVencIni.Text == "")
            {
                VerError("Debe Ingresar ambas fechas en el rango de fechas de vencimiento");
                txtFechaVencIni.Focus();
                return false;
            }
        }
        if (txtFechaVencIni.Text != "" && txtFechaVencFin.Text != "")
        {
            if (Convert.ToDateTime(txtFechaVencIni.Text) > Convert.ToDateTime(txtFechaVencFin.Text))
            {
                VerError("Error al Ingresar el rango de fechas de vencimiento, verifique los datos ingresados");
                return false;
            }
        }

        return true;
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        if (Page.IsValid)
        {
            if (ValidarRangoFechas())
            {
                Actualizar();
                CalcularTotal();
            }
        }
    }


    protected String obtFiltro()
    {
        String filtro = String.Empty;

        if (txtCodigo.Text != "")
            filtro += " AND C.CODIGO_FACTURA = " + txtCodigo.Text.Trim();
        if (txtNumFactura.Text != "")
            filtro += " AND C.NUMERO_FACTURA = '" + txtNumFactura.Text.Trim() + "'";
        if (ddlUsuario.SelectedIndex != 0)
            filtro += " AND C.COD_USUARIO = " + ddlUsuario.SelectedValue;
        if (txtIdentificacion.Text != "")
            filtro += " AND V.IDENTIFICACION = '" + txtIdentificacion.Text + "'";
        if (txtNombres.Text != "")
            filtro += " AND V.NOMBRE LIKE '%" + txtNombres.Text + "%'";
        if (ddlTipoCtaXpagar.SelectedIndex != 0)
            filtro += " AND C.IDTIPO_CTA_POR_PAGAR = " + ddlTipoCtaXpagar.SelectedValue;
        filtro += " and c.estado NOT IN(4,3) and d.estado=0";
        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "WHERE " + filtro;
        }
        return filtro;
    }


    private void Actualizar()
    {
        try
        {
            DateTime pFechaIng, pFechaVencIni, pFechaVencFin;
            pFechaIng = txtFechaIngreso.ToDateTime == null ? DateTime.MinValue : txtFechaIngreso.ToDateTime;
            pFechaVencIni = txtFechaVencIni.ToDateTime == null ? DateTime.MinValue : txtFechaVencIni.ToDateTime;
            pFechaVencFin = txtFechaVencFin.ToDateTime == null ? DateTime.MinValue : txtFechaVencFin.ToDateTime;

            List<AprobacionCtasPorPagar> lstConsulta = new List<AprobacionCtasPorPagar>();
           
            String Orden = "", filtro = "";
            filtro = obtFiltro();
            if (ddlOrdenadoPor.SelectedIndex != 0)
                Orden += ddlOrdenadoPor.SelectedValue;
            if (ddlLuegoPor.SelectedIndex != 0)
            {
                if (Orden != "")
                    Orden += ", " + ddlLuegoPor.SelectedValue;
                else
                    Orden +=  ddlLuegoPor.SelectedValue;
            }
            lstConsulta = AprobacionCtasService.ListarCuentasXpagar(filtro, Orden, pFechaIng,pFechaVencIni,pFechaVencFin, (Usuario)Session["usuario"]);

            gvLista.PageSize = 20;
            gvLista.EmptyDataText = emptyQuery;

            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                ValidarPermisosGrilla(gvLista);
                Session["DTCUENTAS"] = lstConsulta;
                lblInfo.Visible = false;
                toolBar.MostrarGuardar(true);
                toolBar.MostrarExportar(true);
            }
            else
            {
                gvLista.DataSource = null;
                Session["DTCUENTAS"] = null;
                panelGrilla.Visible = false;
                lblInfo.Visible = true;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarExportar(false);
            }

            Session.Add(AprobacionCtasService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionCtasService.CodigoPrograma, "Actualizar", ex);
        }
    }


    Boolean validarDatos()
    {
        if (txtFechaAprobacion.Text == "")
        {
            VerError("Ingrese la fecha de aprobación");
            txtFechaAprobacion.Focus();
            return false;
        }

        int cont = 0;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar != null)
            {
                if (cbSeleccionar.Checked)
                {
                    cont++;
                    DropDownListGrid ddlFormaPago = (DropDownListGrid)rFila.FindControl("ddlFormaPago");
                    if (ddlFormaPago != null)
                    {
                        if (ddlFormaPago.SelectedIndex == 0)
                        {
                            VerError("Error en la Fila " + (rFila.RowIndex + 1) + ". Debe seleccionar la forma de Pago");
                            return false;
                        }
                        else
                        {
                            if (ddlFormaPago.SelectedValue == "3" || ddlFormaPago.SelectedItem.Text == "Transferencia")
                            {
                                DropDownListGrid ddlEntidad = (DropDownListGrid)rFila.FindControl("ddlEntidad");
                                if (ddlEntidad != null)
                                {
                                    if (ddlEntidad.SelectedIndex == 0)
                                    {
                                        VerError("Error en la Fila " + (rFila.RowIndex + 1) + ". Debe de seleccionar la entidad bancaria");
                                        return false;
                                    }
                                }

                                TextBoxGrid txtNum_Cuenta = (TextBoxGrid)rFila.FindControl("txtNum_Cuenta");
                                if (txtNum_Cuenta.Text == "")
                                {
                                    VerError("Error en la Fila " + (rFila.RowIndex + 1) + ". Debe de ingresar el número de cuenta bancaria");
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (cont == 0)
        {
            VerError("No existen Cuentas por pagar seleccionadas");
            return false;
        }

        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (validarDatos())
            ctlMensaje.MostrarMensaje("Desea realizar la aprobación de las cuentas por pagar seleccionadas?");
    }


    Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
    Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {

        try
        {
            Usuario pUsu = (Usuario)Session["usuario"];
            List<Xpinn.Tesoreria.Entities.Giro> lstGiro = new List<Xpinn.Tesoreria.Entities.Giro>();
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
                if (cbSeleccionar != null)
                {
                    if (cbSeleccionar.Checked)
                    {
                        DropDownListGrid ddlFormaPago = (DropDownListGrid)rFila.FindControl("ddlFormaPago");
                        if (ddlFormaPago != null)
                        {
                            if (ddlFormaPago.SelectedIndex != 0)
                            {
                                int codFormaPago = Convert.ToInt32(gvLista.DataKeys[rFila.RowIndex].Values[0].ToString());
                                //CAPTURANDO LOS DATOS PARA LA GRABACION DEL GIRO
                                Xpinn.Tesoreria.Entities.Giro pGiro = new Xpinn.Tesoreria.Entities.Giro();

                                pGiro.idgiro = 0;
                                Int64 cod_persona = Convert.ToInt32(gvLista.DataKeys[rFila.RowIndex].Values[1].ToString());
                                pGiro.cod_persona = cod_persona;
                                pGiro.forma_pago = Convert.ToInt32(ddlFormaPago.SelectedValue);
                                pGiro.tipo_acto = 10;
                                pGiro.fec_reg = Convert.ToDateTime(txtFechaAprobacion.Text);
                                pGiro.fec_giro = DateTime.Now;
                                pGiro.numero_radicacion = 0;
                                pGiro.usu_gen = pUsu.nombre;
                                pGiro.usu_apli = null;
                                pGiro.estadogi = 1;
                                pGiro.usu_apro = pUsu.nombre;
                                pGiro.fec_apro = Convert.ToDateTime(txtFechaAprobacion.Text);

                                //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
                                DropDownListGrid ddlEntidad = (DropDownListGrid)rFila.FindControl("ddlEntidad");
                                TextBoxGrid txtNum_Cuenta = (TextBoxGrid)rFila.FindControl("txtNum_Cuenta");
                                DropDownListGrid ddlTipo_Cuenta = (DropDownListGrid)rFila.FindControl("ddlTipo_Cuenta");

                                //DATOS DE FORMA DE PAGO
                                if (ddlFormaPago.SelectedItem.Text == "Transferencia")
                                {
                                    pGiro.cod_banco = Convert.ToInt32(ddlEntidad.SelectedValue);
                                    pGiro.num_cuenta = txtNum_Cuenta.Text;
                                    pGiro.tipo_cuenta = Convert.ToInt32(ddlTipo_Cuenta.SelectedValue);
                                   
                                }
                                else if (ddlFormaPago.SelectedItem.Text == "Cheque")
                                {
                                    if(ddlTipo_Cuenta.SelectedValue!="0")
                                    { 
                                      pGiro.idctabancaria = Convert.ToInt32(ddlTipo_Cuenta.SelectedValue);
                                    }
                                    pGiro.cod_banco = Convert.ToInt32(ddlEntidad.SelectedValue);        //NULO
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
                                pGiro.cob_comision = 0;
                                Int64 Valor = rFila.Cells[9].Text != "&nbsp;" && rFila.Cells[9].Text != "0" ? Convert.ToInt64(rFila.Cells[9].Text.Replace(".", "").Replace(",", "")) : 0;
                                pGiro.valor = Valor;
                                pGiro.codpagofac = codFormaPago;
                                Int64 operacion = 0;
                                operacion=Convert.ToInt64((rFila.Cells[17].Text));
                                pGiro.cod_ope = operacion;
                                lstGiro.Add(pGiro);
                            }
                        }
                    }
                }
            }

            //GRABAR LOS GIROS  Y ACTUALIZAR LOS ESTADOS
            AprobacionCtasService.AprobarCuentaXpagar(lstGiro, (Usuario)Session["usuario"]);
            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            mvPrincipal.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        limpiar();
    }

    void limpiar()
    {
        LimpiarValoresConsulta(pBusqueda, AprobacionCtasService.CodigoPrograma);        
        txtFechaIngreso.Text = "";
        txtFechaVencIni.Text = "";
        txtFechaVencFin.Text = "";
        txtVrAprobar.Text = "";
        lblInfo.Visible = false;
        gvLista.DataSource = null;
        panelGrilla.Visible = false;
        txtFechaAprobacion.Text = DateTime.Now.ToShortDateString();

        Site toolBar = (Site)this.Master;
        toolBar.MostrarCancelar(false);
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarGuardar(false);
        toolBar.MostrarExportar(false);

        mvPrincipal.ActiveViewIndex = 0;
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
            BOexcepcion.Throw(AprobacionCtasService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CuentasPorPagarService CuotasXpagar = new CuentasPorPagarService();       
        CuentasPorPagar cuentas = new CuentasPorPagar();                

        try
        {           

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                  Int64 ncodigo = Convert.ToInt64(gvLista.DataKeys[e.Row.RowIndex].Values[0].ToString());

                cuentas.codigo_factura = Convert.ToInt32(ncodigo);
                cuentas = CuotasXpagar.ConsultarGiroXfactura(cuentas, (Usuario)Session["usuario"]);




                DropDownListGrid ddlFormaPago = (DropDownListGrid)e.Row.FindControl("ddlFormaPago");
                if (ddlFormaPago != null)
                {
                    ddlFormaPago.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                    ddlFormaPago.Items.Insert(1, new ListItem("Efectivo", "1"));
                    ddlFormaPago.Items.Insert(2, new ListItem("Cheque", "2"));
                    ddlFormaPago.Items.Insert(3, new ListItem("Transferencia", "3"));
                    ddlFormaPago.Items.Insert(4, new ListItem("Giro Ahorro Interno", "4"));
                    ddlFormaPago.SelectedIndex = 0;
                    ddlFormaPago.DataBind();
                }

                Label lblFormaPago = (Label)e.Row.FindControl("lblFormaPago");
                if (lblFormaPago != null)
                    if (lblFormaPago.Text != "" )
                        ddlFormaPago.SelectedValue = lblFormaPago.Text;
                    if  (cuentas.forma_pago > 0)
                        ddlFormaPago.SelectedValue = Convert.ToString(cuentas.forma_pago);

                DropDownListGrid ddlEntidad = (DropDownListGrid)e.Row.FindControl("ddlEntidad");         


                if (ddlFormaPago.SelectedValue == "3")
                {

                if (ddlEntidad != null)
                {
                    PoblarLista.PoblarListaDesplegable("BANCOS", "", "", "1", ddlEntidad, (Usuario)Session["usuario"]);

                        if (cuentas.cod_bancodestino > 0)
                            ddlEntidad.SelectedValue = Convert.ToString(cuentas.cod_bancodestino);
                    }


                }

                else if (ddlFormaPago.SelectedValue == "2")
                {
                    if (ddlEntidad != null)
                    {
                        PoblarLista.PoblarListaDesplegable("V_BANCOS_ENTIDAD", ddlEntidad, (Usuario)Session["usuario"]);
                        if (cuentas.cod_bancodestino > 0)
                            ddlEntidad.SelectedValue = Convert.ToString(cuentas.cod_bancodestino);


                    }
                }

                Label lblEntidad = (Label)e.Row.FindControl("lblEntidad");
                if (lblEntidad != null)
                    if (lblEntidad.Text != "")
                        ddlEntidad.SelectedValue = lblEntidad.Text;
                if (cuentas.cod_bancodestino > 0)
                    ddlEntidad.SelectedValue = Convert.ToString(cuentas.cod_bancodestino);


                DropDownListGrid ddlTipo_Cuenta = (DropDownListGrid)e.Row.FindControl("ddlTipo_Cuenta");

                if (ddlFormaPago.SelectedValue == "3")
                {
                    if (ddlTipo_Cuenta != null)
                {
                    ddlTipo_Cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
                    ddlTipo_Cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
                    ddlTipo_Cuenta.SelectedIndex = 0;
                    ddlTipo_Cuenta.DataBind();

                        if (cuentas.tipo_cuenta > 0)
                            ddlTipo_Cuenta.SelectedValue = Convert.ToString(cuentas.tipo_cuenta);

                    }
                }
                else if (ddlFormaPago.SelectedValue == "2")
                {


                    Int64 codbanco = 0;
                    try
                    {
                        codbanco = Convert.ToInt64(ddlEntidad.SelectedValue);
                    }
                    catch
                    {
                    }
                    if (codbanco != 0)
                    {
                        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
                        Usuario usuario = (Usuario)Session["usuario"];
                        ddlTipo_Cuenta.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
                        ddlTipo_Cuenta.DataTextField = "num_cuenta";
                        ddlTipo_Cuenta.DataValueField = "idctabancaria";
                        ddlTipo_Cuenta.DataBind();
                    }

                }

                Label lblTipoCuenta = (Label)e.Row.FindControl("lblTipoCuenta");
                if (lblTipoCuenta != null)
                    if (lblTipoCuenta.Text != "")
                        ddlTipo_Cuenta.SelectedValue = lblTipoCuenta.Text;
                if (cuentas.tipo_cuenta > 0)
                    ddlTipo_Cuenta.SelectedValue = Convert.ToString(cuentas.tipo_cuenta);



                TextBoxGrid txtNum_Cuenta = (TextBoxGrid)e.Row.FindControl("txtNum_Cuenta");

                if (cuentas.num_cuenta_destino!="")
                    txtNum_Cuenta.Text = Convert.ToString(cuentas.num_cuenta_destino);


                CheckBoxGrid cbSeleccionar = (CheckBoxGrid)e.Row.FindControl("cbSeleccionar");
                if (cbSeleccionar != null)
                {
                    ddlFormaPago.Enabled = false;
                    ddlEntidad.Enabled = false;
                    ddlTipo_Cuenta.Enabled = false;
                    txtNum_Cuenta.Enabled = false;
                    if (cbSeleccionar.Checked)
                    {
                        ddlFormaPago.Enabled = true;
                        ddlEntidad.Enabled = true;
                        ddlTipo_Cuenta.Enabled = true;
                        txtNum_Cuenta.Enabled = true;

                    }
                } 
            
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionCtasService.CodigoPrograma, "gvLista_RowDataBound", ex);
        }
    }



    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownListGrid ddlFormaPago = (DropDownListGrid)sender;
            int nItem = Convert.ToInt32(ddlFormaPago.CommandArgument);

            CheckBoxGrid cbSeleccionar = (CheckBoxGrid)gvLista.Rows[nItem].FindControl("cbSeleccionar");
            DropDownListGrid ddlEntidad = (DropDownListGrid)gvLista.Rows[nItem].FindControl("ddlEntidad");
            DropDownListGrid ddlTipo_Cuenta = (DropDownListGrid)gvLista.Rows[nItem].FindControl("ddlTipo_Cuenta");
            TextBoxGrid txtNum_Cuenta = (TextBoxGrid)gvLista.Rows[nItem].FindControl("txtNum_Cuenta");

            if (cbSeleccionar.Checked)
            {
                if (ddlFormaPago != null)
                {
                    ddlEntidad.Enabled = false;
                    ddlTipo_Cuenta.Enabled = false;
                    txtNum_Cuenta.Enabled = false;
                    if (ddlFormaPago.SelectedIndex != 0)
                    {
                        if (ddlFormaPago.SelectedItem.Text == "Transferencia"  )
                        {


                            if (ddlEntidad != null)
                            {
                                PoblarLista.PoblarListaDesplegable("BANCOS", "", "", "1", ddlEntidad, (Usuario)Session["usuario"]);
                            }


                            if (ddlTipo_Cuenta != null)
                            {
                                ddlTipo_Cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
                                ddlTipo_Cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
                                ddlTipo_Cuenta.SelectedIndex = 0;
                                ddlTipo_Cuenta.DataBind();
                            }

                            ddlEntidad.Enabled = true;
                            ddlTipo_Cuenta.Enabled = true;
                            txtNum_Cuenta.Enabled = true;
                        }

                        if (ddlFormaPago.SelectedItem.Text == "Cheque")
                        {
                            if (ddlEntidad != null)
                                {
                                    PoblarLista.PoblarListaDesplegable("V_BANCOS_ENTIDAD", ddlEntidad, (Usuario)Session["usuario"]);
                                }
                           
                            ddlEntidad.Enabled = true;
                            ddlTipo_Cuenta.Enabled = true;

                      
                        }
                    }
                }
            }
            else
            {
                ddlEntidad.Enabled = false;
                ddlTipo_Cuenta.Enabled = false;
                txtNum_Cuenta.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            VerError("ddlFormaPago_SelectedIndexChanged : " + ex.Message);
        }
    }


    protected void ddlEntidad_Cuenta_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlFormaPago = (DropDownListGrid)sender;
        int nItem = Convert.ToInt32(ddlFormaPago.CommandArgument);

        CheckBoxGrid cbSeleccionar = (CheckBoxGrid)gvLista.Rows[nItem].FindControl("cbSeleccionar");
        DropDownListGrid ddlEntidad = (DropDownListGrid)gvLista.Rows[nItem].FindControl("ddlEntidad");
        DropDownListGrid ddlTipo_Cuenta = (DropDownListGrid)gvLista.Rows[nItem].FindControl("ddlTipo_Cuenta");
        DropDownListGrid ddlforma = (DropDownListGrid)gvLista.Rows[nItem].FindControl("ddlFormaPago");
        TextBoxGrid txtNum_Cuenta = (TextBoxGrid)gvLista.Rows[nItem].FindControl("txtNum_Cuenta");
        Int64 codbanco = 0;
        try
        {
            codbanco = Convert.ToInt64(ddlEntidad.SelectedValue);
        }
        catch
        {
        }
        if (codbanco != 0)
        {
            if (ddlforma.SelectedItem.Text == "Cheque")
            {
                
            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Usuario usuario = (Usuario)Session["usuario"];
            ddlTipo_Cuenta.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
            ddlTipo_Cuenta.DataTextField = "num_cuenta";
            ddlTipo_Cuenta.DataValueField = "idctabancaria";
            ddlTipo_Cuenta.DataBind();
            }
        }


    }


    protected void cbSeleccionar_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CalcularTotal();
        }
        catch (Exception ex)
        {
            VerError("cbSeleccionar_CheckedChanged : " + ex.Message);
        }
    }

}