using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Entities;
using Xpinn.Caja.Entities;
using Xpinn.Caja.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using Xpinn.Tesoreria.Services;


public partial class Nuevo : GlobalWeb
{
    CambioAProductoServices _cambioProducService = new CambioAProductoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            //VisualizarOpciones(CambioProducService.CodigoPrograma, "A");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += (s, evt) => limpiar();
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cambioProducService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                divDatos.Visible = false;
                mvOperacion.Visible = false;

                string ip = Request.ServerVariables["REMOTE_ADDR"];

                Session["val"] = 0;
                ObtenerDatos();

                AsignarEventoConfirmar();

                CargarDropDown();
                // Llenar los DROPDOWNLIST de tipos de monedas, tipos de identificaciòn, formas de pago y entidades bancarias
                LlenarComboTipoProducto(ddlTipoProducto);//se carga los tipos de transaccion
                ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                mvOperacion.ActiveViewIndex = 0;
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cambioProducService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void limpiar()
    {
        divDatos.Visible = false;
        gvConsultaDatos.DataSource = null;
        gvConsultaDatos.Visible = false;
        txtIdentificacion.Text = "";
        txtIdentificacion.Enabled = true;
        mvOperacion.Visible = false;
        //gvAportes.Visible = false;
        txtNombreCliente.Text = "";
        ddlTipoIdentificacion.SelectedIndex = 0;
        ddlTipoProducto.SelectedIndex = 0;

        txtNumProd.Text = "";
        ddlFormaPago.SelectedIndex = 0;
        ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
        txtObservacion.Text = "";
        try
        {
            gvEmpresaRecaudora.DataSource = null;
            ddlEmpresaRecaudo.SelectedIndex = 0;
        }
        catch
        {
        }
    }

    protected void LlenarComboTipoProducto(DropDownList ddlTipoProducto)
    {
        TipoOperacionService tipoopeservices = new TipoOperacionService();

        // Cargando listado de tipos de productos
        List<TipoOperacion> lsttipo = tipoopeservices.ListarTipoProducto(Usuario);
        ddlTipoProducto.DataTextField = "nom_tipo_producto";
        ddlTipoProducto.DataValueField = "tipo_producto";
        ddlTipoProducto.DataSource = lsttipo;
        ddlTipoProducto.DataBind();

        // Seleccionando tipo de producto por defecto y cargandolo
        try
        {
            ddlTipoProducto.SelectedIndex = 2;
            Session["tipoproducto"] = Convert.ToInt64(ddlTipoProducto.SelectedValue);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void CargarDropDown()
    {
        //TIPO IDENTIFICACION
        TipoIdenService IdenService = new TipoIdenService();
        TipoIden identi = new TipoIden();
        ddlTipoIdentificacion.DataSource = IdenService.ListarTipoIden(identi, Usuario);
        ddlTipoIdentificacion.DataTextField = "descripcion";
        ddlTipoIdentificacion.DataValueField = "codtipoidentificacion";
        ddlTipoIdentificacion.DataBind();
    }


    protected void ObtenerDatos()
    {
        Configuracion conf = new Configuracion();
        try
        {
            txtFechaReal.Text = DateTime.Now.ToString();
            txtFechaNota.Text = DateTime.Now.ToString(conf.ObtenerFormatoFecha());
            txtOficina.Text = Usuario.nombre_oficina;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cambioProducService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        Lblerror.Text = ""; 
        try
        {
            DateTime fechaactual = DateTime.Now;
            DateTime Fechatransaccion = Convert.ToDateTime(txtFechaReal.Text);
            TipoDeProducto tipoProducto = ddlTipoProducto.SelectedValue.ToEnum<TipoDeProducto>();

            if (Fechatransaccion > fechaactual)
            {
                Lblerror.Text = "La fecha no puede ser superior a la fecha actual";
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtNumProd.Text))
            {
                Lblerror.Text = "No existe ningun producto seleccionado a Modificar";
                return;
            }
            else if (tipoProducto == TipoDeProducto.Otros || tipoProducto == TipoDeProducto.Devoluciones)
            {
                Lblerror.Text = "No se pueden modificar productos de Devoluciones y Otros!.";
                return;
            }

            CambioAProducto nCambioProd = new CambioAProducto();
            string tabla = DevolverTablaPorTipoDeProducto();
            TipoFormaPago formaPago = ddlFormaPago.SelectedValue.ToEnum<TipoFormaPago>();

            if (tipoProducto != TipoDeProducto.Credito)
            {
                if (formaPago == TipoFormaPago.Nomina) // si es nomina
                {
                    if (ddlEmpresaRecaudo.SelectedIndex == 0 && ddlEmpresaRecaudo.Visible == true)
                    {
                        Lblerror.Text = "Debe seleccionar una empresa";
                        return;
                    }
                    nCambioProd.cod_empresa = Convert.ToInt32(ddlEmpresaRecaudo.SelectedValue);
                    if (ddlEmpresaRecaudo.Visible == false)
                        nCambioProd.cod_empresa = 0;
                }
                else
                    nCambioProd.cod_empresa = 0;

                if (EsAporteOAhorrro())
                {
                    if (txtFechaDeseada.Text == "")
                    {
                        VerError("La fecha deseada de modificación es obligatoria");
                        return;
                    }

                    nCambioProd.fecha_empieza_cambio = DateTime.Parse(txtFechaDeseada.Text);
                    nCambioProd.valor_cuota = txtValorCuota.Text != "" ? decimal.Parse(txtValorCuota.Text) : nCambioProd.valor_cuota;
                    nCambioProd.valor_cuota_anterior = hfAnteriorCuota.Value != "" ? decimal.Parse(hfAnteriorCuota.Value) : nCambioProd.valor_cuota_anterior;
                }
            }
            else
            { 
                if (formaPago == TipoFormaPago.Nomina)
                {
                    List<CreditoEmpresaRecaudo> lstDatos = ObtenerListaEmpresaRecaudadora(formaPago);
                    nCambioProd.lstDetalle = lstDatos;
                    decimal porcentaje = 0;
                    int cont = 0;

                    foreach (CreditoEmpresaRecaudo rCred in lstDatos)
                    {
                        if (lstDatos[cont].porcentaje != 0)
                            porcentaje += lstDatos[cont].porcentaje;
                        cont++;
                    }
                    if (porcentaje > 100)
                    {
                        Lblerror.Text = "El valor total del Porcentaje no puede ser mayor al 100%";
                        return;
                    }
                    else if (porcentaje < 100)
                    {
                        Lblerror.Text = "El valor total del porcentaje debe ser el 100%";
                        return;
                    }
                    else if (porcentaje == 0)
                    {
                        Lblerror.Text = "Debe Ingresar el valor de porcentaje";
                        return;
                    }
                }
            }

            //MODIFICAR LOS DATOS DEL PRODUCTO
            nCambioProd.val_forma_pago = Convert.ToInt32(ddlFormaPago.SelectedValue);
            nCambioProd.numero_producto = Convert.ToInt64(txtNumProd.Text);
            PersonaService personaService = new PersonaService();
            Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();
            persona.identificacion = txtIdentificacion.Text;
            persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
            persona = personaService.ConsultarPersona(persona, Usuario);
            nCambioProd.cod_persona = persona.cod_persona;
            _cambioProducService.ModificarProducto(nCambioProd, tabla, Usuario);
            limpiar();
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cambioProducService.GetType().Name + "A", "btnGuardar_Click", ex);
        }
    }


    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Response.Redirect("nuevo.aspx", false);
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnGuardar"), "Desea grabar los datos?");
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Lblerror.Text = "";
        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            // aqui se coloca los datos de la persona, Nro Radicacion, Nombre, Valor CUota, saldo capital
            ddlTipoTipoProducto_SelectedIndexChanged(this, EventArgs.Empty);
            mvOperacion.Visible = true;
            txtIdentificacion.Enabled = false;
            ddlTipoIdentificacion.Enabled = false;
        }
        else
        {
            mvOperacion.Visible = false;
        }
    }

    private void Actualizar(Xpinn.Caja.Entities.Persona pEntidad)
    {
        gvConsultaDatos.DataSource = null;
        gvConsultaDatos.Visible = false;
        gvDatosAfiliacion.DataSource = null;
        gvDatosAfiliacion.Visible = false;
        gvAhorroVista.DataSource = null;
        gvAhorroVista.Visible = false;
        gvProgramado.DataSource = null;
        gvProgramado.Visible = false;
        gvCdat.DataSource = null;
        gvCdat.Visible = false;
        gvServicios.DataSource = null;
        gvServicios.Visible = false;
        divDatos.Visible = false;

        PersonaService personaService = new PersonaService();

        try
        {
            List<Xpinn.Caja.Entities.Persona> lstConsulta = new List<Xpinn.Caja.Entities.Persona>();
            txtNumProd.Text = "";

            TipoDeProducto tipoProducto = ddlTipoProducto.SelectedValue.ToEnum<TipoDeProducto>();

            if (tipoProducto == TipoDeProducto.Aporte)
            {
                pEntidad.tipo_linea = Convert.ToInt64(Session["tipoproducto"]);

                lstConsulta = personaService.ListarDatosCreditoPersona(pEntidad, Usuario);

                if (lstConsulta.Count > 0)
                {
                    divDatos.Visible = true;
                    gvConsultaDatos.Visible = true;
                    gvConsultaDatos.DataSource = lstConsulta;
                    gvConsultaDatos.DataBind();
                }
            }
            else if (tipoProducto == TipoDeProducto.Credito)
            {
                pEntidad.tipo_linea = Convert.ToInt64(Session["tipoproducto"]);

                lstConsulta = personaService.ListarDatosCreditoPersona(pEntidad, Usuario);

                if (lstConsulta.Count > 0)
                {
                    divDatos.Visible = true;
                    gvConsultaDatos.Visible = true;
                    gvConsultaDatos.DataSource = lstConsulta;
                    gvConsultaDatos.DataBind();
                }
            }
            else if (tipoProducto == TipoDeProducto.AhorrosVista)
            {
                Xpinn.Ahorros.Services.ReporteMovimientoServices ReporteMovService = new Xpinn.Ahorros.Services.ReporteMovimientoServices();
                List<Xpinn.Ahorros.Entities.AhorroVista> lstAhorros = new List<Xpinn.Ahorros.Entities.AhorroVista>();
                string filtro = " WHERE A.ESTADO IN (0,1) AND A.COD_PERSONA = " + pEntidad.cod_persona + " ";
                DateTime pFechaApert = DateTime.MinValue;

                lstAhorros = ReporteMovService.ListarAhorroVista(filtro, pFechaApert, Usuario);
                if (lstAhorros.Count > 0)
                {
                    gvAhorroVista.Visible = true;
                    gvAhorroVista.DataSource = lstAhorros;
                    gvAhorroVista.DataBind();
                    divDatos.Visible = true;
                }
            }
            else if (tipoProducto == TipoDeProducto.Servicios)
            {
                Xpinn.Servicios.Services.AprobacionServiciosServices AproServicios = new Xpinn.Servicios.Services.AprobacionServiciosServices();
                List<Xpinn.Servicios.Entities.Servicio> lstServicios = new List<Xpinn.Servicios.Entities.Servicio>();
                string filtro = " and S.COD_PERSONA = " + pEntidad.cod_persona + " AND S.ESTADO = 'C' ";

                string pOrden = "fecha_solicitud desc";

                lstServicios = AproServicios.ListarServicios(filtro, pOrden, DateTime.MinValue, Usuario);

                if (lstServicios.Count > 0)
                {
                    divDatos.Visible = true;
                    gvServicios.Visible = true;
                    gvServicios.DataSource = lstServicios;
                    gvServicios.DataBind();
                }
            }
            else if (tipoProducto == TipoDeProducto.CDATS)
            {
                Xpinn.CDATS.Services.AperturaCDATService AperturaService = new Xpinn.CDATS.Services.AperturaCDATService();
                List<Xpinn.CDATS.Entities.Cdat> lstCdat = new List<Xpinn.CDATS.Entities.Cdat>();
                string filtro = " AND C.ESTADO = 1 and T.COD_PERSONA = " + pEntidad.cod_persona + " AND T.PRINCIPAL = 1 ";
                DateTime FechaApe = DateTime.MinValue;

                lstCdat = AperturaService.ListarCdats(filtro, FechaApe, Usuario);

                if (lstCdat.Count > 0)
                {
                    divDatos.Visible = true;
                    gvCdat.Visible = true;
                    gvCdat.DataSource = lstCdat;
                    gvCdat.DataBind();
                }
            }
            else if (tipoProducto == TipoDeProducto.Afiliacion)
            {
                gvConsultaDatos.Visible = false;

                lstConsulta = personaService.ListarPersonasAfiliacion(pEntidad, Usuario);
                if (lstConsulta.Count > 0)
                {
                    divDatos.Visible = true;
                    gvDatosAfiliacion.Visible = true;
                    gvDatosAfiliacion.DataSource = lstConsulta;
                    gvDatosAfiliacion.DataBind();
                }
            }
            else if (tipoProducto == TipoDeProducto.Otros)
            {
            }
            else if (tipoProducto == TipoDeProducto.Devoluciones)
            {

            }
            else if (tipoProducto == TipoDeProducto.AhorroProgramado)
            {
                Xpinn.Programado.Services.CuentasProgramadoServices CuentasPrograServicios = new Xpinn.Programado.Services.CuentasProgramadoServices();
                List<Xpinn.Programado.Entities.CuentasProgramado> lstPrograma = new List<Xpinn.Programado.Entities.CuentasProgramado>();
                string filtro = " WHERE A.ESTADO = 1 AND A.COD_PERSONA = " + pEntidad.cod_persona + " ";
                DateTime pFecha = DateTime.MinValue;

                lstPrograma = CuentasPrograServicios.ListarAhorrosProgramado(filtro, pFecha, Usuario);

                if (lstPrograma.Count > 0)
                {
                    gvProgramado.DataSource = lstPrograma;
                    gvProgramado.DataBind();
                    divDatos.Visible = true;
                    gvProgramado.Visible = true;
                }
            }

            Session.Add(_cambioProducService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cambioProducService.GetType().Name + "L", "Actualizar", ex);
        }
    }   

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "", "txtIdentificacion", "ddlTipoIdentificacion", "txtNombreCliente");
    }


    protected void ddlTipoTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");

        PersonaService personaService = new PersonaService();
        Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();

        TipoDeProducto tipoProducto = ddlTipoProducto.SelectedValue.ToEnum<TipoDeProducto>();
        if (tipoProducto == TipoDeProducto.Aporte)
        {
            persona.linea_credito = "1";
        }
        if (tipoProducto == TipoDeProducto.Credito)
        {
            persona.linea_credito = "2";
        }

        Session["tipoproducto"] = (long)tipoProducto;

        persona.identificacion = txtIdentificacion.Text;
        persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
        persona = personaService.ConsultarPersona(persona, Usuario);
        txtNombreCliente.Text = persona.nom_persona;

        this.Actualizar(persona);

        txtNumProd_TextChanged(txtNumProd, null);
    }

    protected void gvGridViews_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        GridView gv = sender as GridView;

        if (gv != null)
        {
            txtNumProd.Text = gv.DataKeys[e.NewEditIndex].Values[0].ToString();
            if (EsAporteOAhorrro())
            {
                hfAnteriorCuota.Value = txtValorCuota.Text = gv.DataKeys[e.NewEditIndex].Values["valor_cuota"] != null ? gv.DataKeys[e.NewEditIndex].Values["valor_cuota"].ToString() : "";
                tdFechaDeseada.Visible = true;
                tdValorCuota.Visible = true;
            }
            else
            {
                tdFechaDeseada.Visible = false;
                tdValorCuota.Visible = false;
            }

            txtNumProd_TextChanged(txtNumProd, null);
            e.NewEditIndex = -1;
        }
    }

    protected void txtNumProd_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtNumProd.Text))
        {
            ddlFormaPago.Enabled = true;
            string num_prod = txtNumProd.Text;
            string tabla = string.Empty;
            tabla = DevolverTablaPorTipoDeProducto();

            CambioAProducto vRetorno = _cambioProducService.ConsultarFormaDePagoProducto(num_prod, tabla, Usuario);

            TipoFormaPago formaPago = vRetorno.val_forma_pago.ToEnum<TipoFormaPago>();

            if (formaPago == TipoFormaPago.Caja)
                ddlFormaPago.SelectedValue = vRetorno.val_forma_pago.ToString(); //Asignando forma de pago a nomina 
            else if (formaPago == TipoFormaPago.Nomina)
                ddlFormaPago.SelectedValue = vRetorno.val_forma_pago.ToString(); //Asignando forma de pago a caja



            ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
            //DE SER APORTE O AFILIACION SE RECUPERA EL COD_EMPRESA
            try
            {
                if (vRetorno.cod_empresa != 0)
                    ddlEmpresaRecaudo.SelectedValue = vRetorno.cod_empresa.ToString();
            }
            catch { }
        }
        else
        {
            ddlFormaPago.Enabled = false;
            ddlFormaPago.SelectedIndex = 0;
            ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
        }
    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        TipoFormaPago formaPago = ddlFormaPago.SelectedValue.ToEnum<TipoFormaPago>();

        if (formaPago == TipoFormaPago.Nomina)
        {
            lbltituloEmpresa.Visible = true;
            TipoDeProducto tipoProdcuto = ddlTipoProducto.SelectedValue.ToEnum<TipoDeProducto>();
            PersonaEmpresaRecaudo pDatos = new PersonaEmpresaRecaudo() { identificacion = txtIdentificacion.Text };

            if (tipoProdcuto == TipoDeProducto.Credito)
            {
                panelAporte.Visible = false;
                PanelCredito.Visible = true;

                //CARGAR GRILLA
                long num_prod = !string.IsNullOrWhiteSpace(txtNumProd.Text) ? Convert.ToInt64(txtNumProd.Text) : 0;
                List<CambioAProducto> lstDatosEMP = _cambioProducService.ListarCreditoEmpresa_Recaudo(num_prod, Usuario);

                if (lstDatosEMP.Count > 0)
                {
                    gvEmpresaRecaudora.DataSource = lstDatosEMP;
                    gvEmpresaRecaudora.DataBind();
                    lblInfo.Visible = false;
                    gvEmpresaRecaudora.Visible = true;
                }
                else
                {
                    List<PersonaEmpresaRecaudo> lstConsulta = _cambioProducService.ListarPersonaEmpresaRecaudo(pDatos, Usuario);

                    if (lstConsulta.Count > 0)
                    {
                        gvEmpresaRecaudora.DataSource = lstConsulta;
                        gvEmpresaRecaudora.DataBind();
                        lblInfo.Visible = false;
                        gvEmpresaRecaudora.Visible = true;
                    }
                    else
                    {
                        gvEmpresaRecaudora.DataSource = null;
                        gvEmpresaRecaudora.Visible = false;
                        lblInfo.Visible = true;
                    }
                }
            }
            else
            {
                panelAporte.Visible = true;
                PanelCredito.Visible = false;

                //CARGAR DROPDOWNLIST
                if (txtIdentificacion.Text != "")
                {
                    List<PersonaEmpresaRecaudo> lstConsulta = _cambioProducService.ListarPersonaEmpresaRecaudo(pDatos, Usuario);
                    ddlEmpresaRecaudo.Items.Clear();

                    if (lstConsulta.Count > 0)
                    {
                        ddlEmpresaRecaudo.DataSource = lstConsulta;
                        ddlEmpresaRecaudo.DataTextField = "nom_empresa";
                        ddlEmpresaRecaudo.DataValueField = "cod_empresa";
                    }
                    ddlEmpresaRecaudo.AppendDataBoundItems = true;
                    ddlEmpresaRecaudo.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                    ddlEmpresaRecaudo.SelectedIndex = 0;
                    ddlEmpresaRecaudo.DataBind();
                }
            }
        }
        else
        {
            lbltituloEmpresa.Visible = false;
            panelAporte.Visible = false;
            PanelCredito.Visible = false;
        }
    }

    protected List<CreditoEmpresaRecaudo> ObtenerListaEmpresaRecaudadora(TipoFormaPago formaPago)
    {
        List<CreditoEmpresaRecaudo> lstLista = new List<CreditoEmpresaRecaudo>();

        foreach (GridViewRow rFila in gvEmpresaRecaudora.Rows)
        {
            CreditoEmpresaRecaudo vData = new CreditoEmpresaRecaudo();

            Label lblidcrerecaudo = (Label)rFila.FindControl("lblidcrerecaudo"); //CODIGO DE LA EMPRESA
            if (!string.IsNullOrWhiteSpace(lblidcrerecaudo.Text))
                vData.idcrerecaudo = Convert.ToInt32(lblidcrerecaudo.Text);

            Label lblCodigo = (Label)rFila.FindControl("lblCodigo"); //CODIGO DE LA EMPRESA
            if (lblCodigo != null && lblCodigo.Text != "")
                vData.cod_empresa = Convert.ToInt32(lblCodigo.Text);

            Label lblNombre = (Label)rFila.FindControl("lblNombre");
            if (lblNombre != null && lblNombre.Text != "")
                vData.nom_empresa = lblNombre.Text;

            TextBox txtPorcentaje = (TextBox)rFila.FindControl("txtPorcentaje");
            if (!string.IsNullOrWhiteSpace(txtPorcentaje.Text))
                vData.porcentaje = Convert.ToDecimal(txtPorcentaje.Text);

            vData.valor = 0;

            // Si soy nomina valido que tenga un porcentaje valido y un id de empresa recaudo valido para asi proceder a modificar / crear
            if (formaPago == TipoFormaPago.Nomina)
            {
                if (vData.porcentaje != 0 || vData.idcrerecaudo != 0)
                    lstLista.Add(vData);
            }
            else
            {
                // Si soy Caja, dejo los porcentaje en 0 y las agrego, ya que el pl los borra si tiene porcentaje 0
                // Si no lo haces asi cambia la forma de pago pero deja la empresa recaudo
                vData.porcentaje = 0;
                lstLista.Add(vData);
            }

        }
        return lstLista;
    }


    string DevolverTablaPorTipoDeProducto()
    {
        string tabla = string.Empty;
        TipoDeProducto tipoProducto = ddlTipoProducto.SelectedValue.ToEnum<TipoDeProducto>();

        switch (tipoProducto)
        {
            case TipoDeProducto.Aporte:
                tabla = "APORTE";
                break;
            case TipoDeProducto.Credito:
                tabla = "CREDITO";
                break;
            case TipoDeProducto.AhorrosVista:
                tabla = "AHORRO_VISTA";
                break;
            case TipoDeProducto.Servicios:
                tabla = "SERVICIOS";
                break;
            case TipoDeProducto.Afiliacion:
                tabla = "AFILIACION";
                break;
            case TipoDeProducto.AhorroProgramado:
                tabla = "AHORRO_PROGRAMADO";
                break;
        }

        return tabla;
    }

    private bool EsAporteOAhorrro()
    {
        TipoDeProducto tipoProducto = ddlTipoProducto.SelectedValue.ToEnum<TipoDeProducto>();
        if (tipoProducto == TipoDeProducto.Aporte || tipoProducto == TipoDeProducto.AhorrosVista || tipoProducto == TipoDeProducto.AhorroProgramado)
            return true;
        else
            return false;
    }
}
