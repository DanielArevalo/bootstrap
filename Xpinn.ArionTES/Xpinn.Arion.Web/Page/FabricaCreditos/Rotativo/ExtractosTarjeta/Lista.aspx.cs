using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;
using System.Text;
using System.IO;
using Microsoft.Reporting.WebForms;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Business;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using System.Linq;


partial class Lista : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices ahorrosServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    Xpinn.FabricaCreditos.Services.CreditoSolicitadoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();
    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    MovGralCreditoService servicemovgeneral = new MovGralCreditoService();
    Producto entityProducto;
    List<DetalleProducto> lstConsulta = new List<DetalleProducto>();
    Int64 tipolinea = 0;
    DetalleProductoService detCreditoServicio = new DetalleProductoService();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[creditoServicio.CodigoProgramaRotativoExtracto + ".id"] != null)
                VisualizarOpciones(creditoServicio.CodigoProgramaRotativoExtracto, "E");
            else
                VisualizarOpciones(creditoServicio.CodigoProgramaRotativoExtracto, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarConsultar(true);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarImprimir(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaExtractosAhorro, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //DateTime fechafin = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            //DateTime fechain = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            //Txtfechaperiodo_final.Text = Convert.ToString(fechafin);
            //txtFecha_periodo.Text = Convert.ToString(fechain);

            if (!IsPostBack)
            {

                // txtObservacionesExtracto.Text = "Periodo Comprendido entre " + fechain.ToShortDateString() + "  y  " + fechafin.ToShortDateString();
                CargaDropDown();
                mvPrincipal.ActiveViewIndex = 0;
                txtFecha_corte.ToDateTime = DateTime.Now;



            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaExtractosAhorro, "Page_Load", ex);
        }
    }
    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 0;
        txtFecha_corte.Text = "";
        txtFecha_periodoInicial.Text = "";
        txtCodigo.Text = "";
        txtApellidos.Text = "";
        txtcodigo_final.Text = "";
        txtidentificacion.Text = "";
        txtFecha_corte.Text = "";
        txtidentificacion_final.Text = "";

        gvLista.DataSource = null;
        gvLista.DataBind();
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {

        VerError("");


        if (txtFecha_periodoInicial.ToDateTime > txtFecha_corte.ToDateTime)
        {
            VerError("LA FECHA INICIAL NO PUEDE SER POSTERIOR A LA FECHA FINAL");
            return;
        }

        if (ValidarDatos())
        {
            mvPrincipal.ActiveViewIndex = 1;
            Actualizar();
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");

        if (txtNumeroCuenta.Text == txtNumeroCuenta.Text)
        {
            VerError("Coloque un numero de cuenta valido");
            return;
        }

        VerError("Desea guardar los datos de el traslado de la cuenta?");

    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        if (mvPrincipal.ActiveViewIndex == 1)
        {
            mvPrincipal.ActiveViewIndex = 0;

            toolBar.MostrarCancelar(false);
            toolBar.MostrarExportar(false);
            toolBar.MostrarImprimir(false);
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarConsultar(true);
        }
        else if (mvPrincipal.ActiveViewIndex == 2)
        {
            mvPrincipal.ActiveViewIndex = 1;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarExportar(true);
            toolBar.MostrarImprimir(true);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarConsultar(false);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVista = ahorrosServicio.ConsultarAhorroVista(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);


            if (!string.IsNullOrEmpty(vAhorroVista.numero_cuenta.ToString()))
                txtNumeroCuenta.Text = HttpUtility.HtmlDecode(vAhorroVista.numero_cuenta.ToString());

            //numero de cuenta

            if (!string.IsNullOrEmpty(vAhorroVista.cod_persona.ToString()))
                Session["COD_PERSONA"] = HttpUtility.HtmlDecode(vAhorroVista.cod_persona.ToString());

            //Codigo del cliente

            if (!string.IsNullOrEmpty(vAhorroVista.cod_linea_ahorro.ToString()))
                txtApellidos.Text = HttpUtility.HtmlDecode(vAhorroVista.cod_linea_ahorro.ToString().Trim());

            //linea de ahorro
            if (!string.IsNullOrEmpty(vAhorroVista.nombres))
                txtNombres.Text = HttpUtility.HtmlDecode(vAhorroVista.nombres.ToString());

            //nombres
            if (!string.IsNullOrEmpty(vAhorroVista.nom_linea))
                txtNumCuenta_final.Text = HttpUtility.HtmlDecode(vAhorroVista.nom_linea.ToString());

            //nombre linea
            if (!string.IsNullOrEmpty(vAhorroVista.saldo_canje.ToString()))
                txtidentificacion.Text = HttpUtility.HtmlDecode(vAhorroVista.saldo_canje.ToString().Trim());

            //estado

            if (!string.IsNullOrEmpty(vAhorroVista.fecha_apertura.ToString()))
                txtFecha_corte.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vAhorroVista.fecha_apertura.ToString()));

            //Fecha de apertura

            if (!string.IsNullOrEmpty(vAhorroVista.saldo_total.ToString()))

                txtObservacionesExtracto.Text = vAhorroVista.saldo_total.ToString("n0");
            //saldo total

            if (!string.IsNullOrEmpty(vAhorroVista.identificacion.ToString()))
                txtCodigo.Text = HttpUtility.HtmlDecode(vAhorroVista.tipo_identificacion.ToString().Trim());

            //tipo identificacion

            if (!string.IsNullOrEmpty(vAhorroVista.identificacion.ToString()))
                txtcodigo_final.Text = HttpUtility.HtmlDecode(vAhorroVista.identificacion.ToString().Trim());

            if (vAhorroVista.cod_linea_ahorro != null)
                txtidentificacion_final.Text = vAhorroVista.
                    cod_linea_ahorro;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaExtractosAhorro, "ObtenerDatos", ex);
        }
    }

    protected void txtNumCuenta_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        TextBoxGrid txtNumCuenta = (TextBoxGrid)sender;
        if (txtNumeroCuenta.Text == txtNumeroCuenta.Text)
        {
            VerError("Digite un numero de cuenta valido");
        }

        if (txtNumCuenta != null)
        {
            if (txtNumeroCuenta.Text == "")
            {
                VerError("coloque un numero de cuenta valido");
            }
        }

        if (txtNumCuenta != null)
        {
            if (txtNumCuenta.Text == txtNumeroCuenta.Text)
            {
                VerError("Digite un numero de cuenta diferente");
                return;
            }


            Xpinn.Ahorros.Services.AhorroVistaServices CuentasServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
            Xpinn.Ahorros.Entities.AhorroVista Cuenta = new Xpinn.Ahorros.Entities.AhorroVista();
            Cuenta = CuentasServicio.ConsultarAhorroVista(txtNumCuenta.Text, (Usuario)Session["usuario"]);
            int rowIndex = Convert.ToInt32(txtNumCuenta.CommandArgument);

            Label lblLinea = (Label)gvLista.Rows[rowIndex].FindControl("lblLinea");

            if (lblLinea != null)
                lblLinea.Text = Convert.ToString(Cuenta.nom_linea);

            Label lbloficina = (Label)gvLista.Rows[rowIndex].FindControl("lbloficina");

            if (lbloficina != null)
                lbloficina.Text = Convert.ToString(Cuenta.nom_oficina);

            Label lblidentificacion = (Label)gvLista.Rows[rowIndex].FindControl("lblidentificacion");

            if (lblidentificacion != null)
                lblidentificacion.Text = Convert.ToString(Cuenta.identificacion);

            Label lblnombre = (Label)gvLista.Rows[rowIndex].FindControl("lblnombre");

            if (lblnombre != null)
                lblnombre.Text = Convert.ToString(Cuenta.nombres);

            Label lblsaldo_total = (Label)gvLista.Rows[rowIndex].FindControl("lblsaldo_total");

            if (lblsaldo_total != null)
                lblsaldo_total.Text = Cuenta.saldo_total.ToString("n0");

        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ahorrosServicio.CodigoProgramaExtractosAhorro + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[ahorrosServicio.CodigoProgramaExtractosAhorro + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string conseID = Convert.ToString(gvLista.DataKeys[e.RowIndex].Values[0].ToString());


        List<AhorroVista> LstDeta;
        LstDeta = (List<AhorroVista>)Session["DatosDetalle"];
        if (conseID != null)
        {
            try
            {
                foreach (AhorroVista Deta in LstDeta)
                {
                    if (Deta.numero_cuenta == conseID)
                    {
                        string id = Convert.ToString(e.Keys[0]);
                        if (id.Trim() != "")
                            ahorrosServicio.EliminarAhorroVista(id, (Usuario)Session["usuario"]); //OPCION 1 Eliminar detalle
                        LstDeta.Remove(Deta);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            foreach (AhorroVista Deta in LstDeta)
            {
                if (Deta.numero_cuenta == conseID)
                {
                    LstDeta.Remove(Deta);
                    break;
                }
            }
        }

        gvLista.DataSourceID = null;
        gvLista.DataBind();

        gvLista.DataSource = LstDeta;
        gvLista.DataBind();

        Session["DatosDetalle"] = LstDeta;
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
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaExtractosAhorro, "gvLista_PageIndexChanging", ex);
        }
    }
    private string obtFiltro(CreditoSolicitado Credito)
    {
        String filtro = String.Empty;
        filtro += " and cod_linea_credito = " + Credito.cod_linea_credito;


        if (txtCodigo.Text != "")
          if (txtCodigo.Text != "" && txtcodigo_final.Text != "")
              filtro += " and Cod_Persona between " + txtCodigo.Text + " and " + txtcodigo_final.Text;
            else
             filtro += " and Cod_Persona  = " + txtCodigo.Text;
        else if (txtcodigo_final.Text != "")
           filtro += " and Cod_Persona  = " + txtcodigo_final.Text;

        if (txtidentificacion.Text != "")
            if (txtidentificacion.Text != "" && txtidentificacion_final.Text != "")
                filtro += " and ESNULO(StrToNumber(Identificacion),0) between " + txtidentificacion.Text + " and " + txtidentificacion_final.Text + "";
            else
                filtro += " and Identificacion = '" + txtidentificacion.Text + "' ";
        else if (txtidentificacion_final.Text != "")
            filtro += " and Identificacion <= '" + txtidentificacion_final.Text + "' ";


        //if (txtNumeroCuenta.Text != "")
        //    if (txtNumeroCuenta.Text != "" && this.txtNumCuenta_final.Text != "")
        //        filtro += " and a.numero_cuenta between " + txtNumeroCuenta.Text + " and " + txtNumCuenta_final.Text + "";
        //    else
        //        filtro += " and  a.numero_cuenta  = '" + txtNumeroCuenta.Text + "' ";
        //else if (txtNumCuenta_final.Text != "")
        //    filtro += " and a.numero_cuenta <= '" + txtNumCuenta_final.Text + "' ";


        //if (txtNombres.Text != "")
        //    filtro += " and p.Nombres like '%" + txtNombres.Text + "%'";

        //if (txtApellidos.Text != "")
        //    filtro += " and p.Apellidos like '%" + txtApellidos.Text + "%'";

        //if (ddlLinea.SelectedIndex != 0)

        //{
        //    filtro += " and a.COD_LINEA_AHORRO=" + ddlLinea.SelectedValue;

        //}

        return filtro;
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(true);
            toolBar.MostrarImprimir(true);            
            DateTime pFechaIni, pFechaFin;
            pFechaIni = txtFecha_periodoInicial.ToDateTime == null ? DateTime.MinValue : txtFecha_periodoInicial.ToDateTime;
            pFechaFin = txtFecha_corte.ToDateTime == null ? DateTime.MinValue : txtFecha_corte.ToDateTime;
         //   lstConsulta = ahorrosServicio.ListaAhorroExtractos(ObtenerValores(), (Usuario)Session["usuario"], filtro);




            List<CreditoSolicitado> lstConsultas = new List<CreditoSolicitado>();
            CreditoSolicitado credito = new CreditoSolicitado();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFecha;
            pFecha = DateTime.MinValue;
            lstConsultas = creditoServicio.ListarCreditosRotativos(credito, pFecha, (Usuario)Session["usuario"], filtro);





            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsultas;

            if (lstConsultas.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultas.Count.ToString();
                Session["DTAhorroVista"] = lstConsultas;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ahorrosServicio.CodigoProgramaExtractosAhorro + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaExtractosAhorro, "Actualizar", ex);
        }
    }

    private CreditoSolicitado ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.CreditoSolicitado vLineasCredito = new Xpinn.FabricaCreditos.Entities.CreditoSolicitado();


       
         vLineasCredito.cod_linea_credito = Convert.ToString(ddlLinea.SelectedValue);

        //if (ddlOficina.SelectedIndex != 0)
        //    vLineasCredito.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);


        //if (ddlEstado.SelectedIndex != 0)
        //    vLineasCredito.estado = Convert.ToString(ddlEstado.SelectedValue);

        //if (txtIdentificacion.Text.Trim() != "")
        //    vLineasCredito.Identificacion = Convert.ToString(txtIdentificacion.Text.Trim());

        //if (txtNumero_radicacion.Text.Trim() != "")
        //    vLineasCredito.NumeroCredito = Convert.ToInt32(txtNumero_radicacion.Text.Trim());


        //if (txtNombre.Text.Trim() != "")
        //    vLineasCredito.Nombres = txtNombre.Text.Trim().ToUpper();


        return vLineasCredito;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
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
    protected void CargaDropDown()
    {
        PoblarLista("Ciudades", ddlCiudadResidencia);

        Xpinn.FabricaCreditos.Services.PersonaEmpresaRecaudoServices EmpresaRecaudoService = new Xpinn.FabricaCreditos.Services.PersonaEmpresaRecaudoServices();
        List<Xpinn.FabricaCreditos.Entities.PersonaEmpresaRecaudo> lstEmpresaRecaudo = new List<Xpinn.FabricaCreditos.Entities.PersonaEmpresaRecaudo>();
        lstEmpresaRecaudo = EmpresaRecaudoService.ListarEmpresaRecaudo(false, (Usuario)Session["usuario"]);
        if (lstEmpresaRecaudo.Count > 0)
        {
            ddlEmpresa.DataSource = lstEmpresaRecaudo;
            ddlEmpresa.DataTextField = "descripcion";
            ddlEmpresa.DataValueField = "cod_empresa";
            ddlEmpresa.AppendDataBoundItems = true;
            // ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlEmpresa.SelectedIndex = 0;
            ddlEmpresa.DataBind();
        }



        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        LineasCredito eLinea = new LineasCredito();
        eLinea.tipo_linea = 2;
        eLinea.estado = 1;
        ddlLinea.DataSource = LineaCreditoServicio.ListarLineasCredito(eLinea, (Usuario)Session["usuario"]);
        ddlLinea.DataTextField = "nom_linea_credito";
        ddlLinea.DataValueField = "Codigo";
        ddlLinea.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlLinea.SelectedIndex = 0;
        ddlLinea.DataBind();



    }

    protected void ExportToExcel(GridView GridView1)
    {
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox check = (CheckBox)rFila.FindControl("check");
            if (check != null)
            {
                if (check.Checked == true)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=ExtractosTarjeta.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.ContentEncoding = Encoding.Default;
                    StringWriter sw = new StringWriter();
                    ExpGrilla expGrilla = new ExpGrilla();

                    sw = expGrilla.ObtenerGrilla(GridView1, null);

                    Response.Write(expGrilla.style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void gvLista_SelectedIndexChanged1(object sender, System.EventArgs e)
    {

    }



    Boolean ValidarDatos()
    {
        VerError("");

        if (txtFecha_corte.Text == "")
        {
            VerError("Ingrese la Fecha de Corte");
            return false;
        }
        if (txtFecha_periodoInicial.Text == "")
        {
            VerError("Ingrese la Fecha de inicio del periodo");
            return false;
        }

        if (txtFecha_periodoInicial.Text != "" && txtFecha_corte.Text != "")
            if (Convert.ToDateTime(txtFecha_periodoInicial.Text) > Convert.ToDateTime(txtFecha_corte.Text))
            {
                VerError("El Rango de Fechas fue ingresadas de forma erronea. (Detalle de Pago)");
                return false;
            }

        if (txtCodigo.Text != "" && txtcodigo_final.Text != "")
            if (Convert.ToInt64(txtCodigo.Text) > Convert.ToInt64(txtcodigo_final.Text))
            {
                VerError("El código Inicial debe ser menor que el código Final");
                return false;
            }

        if (txtNumeroCuenta.Text != "" && txtNumCuenta_final.Text != "")
            if (Convert.ToInt64(txtNumeroCuenta.Text) > Convert.ToInt64(txtNumCuenta_final.Text))
            {
                VerError("El número de cuenta Inicial debe ser menor que el número Final");
                return false;
            }

        if (txtidentificacion.Text != "" && txtidentificacion_final.Text != "")
            if (Convert.ToInt64(txtidentificacion.Text) > Convert.ToInt64(txtidentificacion_final.Text))
            {
                VerError("El número de identificacion Inicial debe ser menor que el número de Crédito Final");
                return false;
            }

        if (txtObservacionesExtracto.Text == "")
        {
            VerError("ponga una observacion al extracto");
            return false;
        }



        return true;
    }


    String sTipo = "EAN128";
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        //vCreditoSeleccionado codigo seleccionado NUMERO CREDITO

        if (ValidarDatos())
        {
            StringHelper stringHelper = new StringHelper();
            //tabla general 
            DataTable tablegeneral = new DataTable();

            tablegeneral.Columns.Add("codigo");
            tablegeneral.Columns.Add("identificacion");
            tablegeneral.Columns.Add("nombres");
            tablegeneral.Columns.Add("ciudad");
            tablegeneral.Columns.Add("direccion");
            tablegeneral.Columns.Add("fecha_oper");
            tablegeneral.Columns.Add("nomtipo_ope");
            tablegeneral.Columns.Add("cod_ope");
            tablegeneral.Columns.Add("nombre");
            tablegeneral.Columns.Add("linea");
            tablegeneral.Columns.Add("saldo_total");
            tablegeneral.Columns.Add("tipo_mov");
            tablegeneral.Columns.Add("valor");
            tablegeneral.Columns.Add("saldo");
            tablegeneral.Columns.Add("rutaImagen");
            tablegeneral.Columns.Add("iddetalle");
            tablegeneral.Columns.Add("idextracto");

            tablegeneral.Columns.Add("NumeroCuenta");
            tablegeneral.Columns.Add("Abonos");
            tablegeneral.Columns.Add("Cargos");
            tablegeneral.Columns.Add("InteresesRecibidos");
            tablegeneral.Columns.Add("CuatroPorMil");
            tablegeneral.Columns.Add("Retenciones");
            tablegeneral.Columns.Add("SaldoMesAnterior");

            tablegeneral.Columns.Add("AbonosContador");
            tablegeneral.Columns.Add("CargosContador");
            tablegeneral.Columns.Add("InteresesContador");
            tablegeneral.Columns.Add("CuatroPorMilContador");
            tablegeneral.Columns.Add("RetencionesContador");

            tablegeneral.Columns.Add("NombreOficina");
            tablegeneral.Columns.Add("DireccionOficina");
            tablegeneral.Columns.Add("TelefonoOficina");
            tablegeneral.Columns.Add("doc_soporte"); 

            int contDetalle = 0;
            DateTime fechaInicio = !string.IsNullOrWhiteSpace(txtFecha_periodoInicial.Text) ? Convert.ToDateTime(txtFecha_periodoInicial.Text) : DateTime.MinValue;
            DateTime fechaFinal = !string.IsNullOrWhiteSpace(txtFecha_corte.Text) ? Convert.ToDateTime(txtFecha_corte.Text) : DateTime.MinValue;

            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox check = (CheckBox)rFila.FindControl("check");
                if (check != null)
                {
                    if (check.Checked == true)
                    {

                        String numeroCuenta = Convert.ToString(rFila.Cells[2].Text);//NUMERO DE CREDITO Seleccionado;
                        string identificacionn = rFila.Cells[5].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[5].Text) : "";
                        string ciudad = rFila.Cells[6].Text != "&nbsp;" ? "Cali" : "Cali";
                        string NombreCompleto = rFila.Cells[6].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[6].Text) : "";
                        string linea = rFila.Cells[3].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[3].Text) : "";

                        #region Trayendo datos desde BD para el informe

                        // entityProducto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);
                        Producto entityProducto = new Producto();
                        entityProducto.noconsultaTodo = 1;
                        entityProducto.CodRadicacion = numeroCuenta;
                        
                        lstConsulta = serviceEstadoCuenta.ListarDetalleProductos(entityProducto, (Usuario)Session["usuario"], 2);
                        int cont = 0; 
                        Configuracion conf = new Configuracion();
                        List<MovimientoProducto> lstCreditosDet = new List<MovimientoProducto>();
                       
                        lstCreditosDet = detCreditoServicio.ListarMovCreditos(Convert.ToInt64(entityProducto.CodRadicacion), (Usuario)Session["usuario"], 1);
                        if (lstCreditosDet.Count > 0)
                        {
                            lstCreditosDet = lstCreditosDet.Where(x => x.FechaPago >= fechaInicio && x.FechaPago <= fechaFinal).ToList();
                        }


                        ReporteMovimiento reporteMovimientoCuenta = ahorrosServicio.ConsultarExtractoAhorroVista(numeroCuenta, Convert.ToDateTime(txtFecha_corte.Text), fechaInicio, fechaFinal, (Usuario)Session["usuario"]);
                        AhorroVista datosAhorroVista = ahorrosServicio.ConsultarAhorroVistaDatosOficina(numeroCuenta, (Usuario)Session["usuario"]);
                        List<ReporteMovimiento> lstExtractos = ahorrosServicio.ListarDetalleExtracto(numeroCuenta, fechaFinal, (Usuario)Session["usuario"], fechaInicio, fechaFinal, reporteMovimientoCuenta.SaldoInicio);
                        string nombreLinea = ahorrosServicio.ConsultarNombreLineaDeAhorroPorCodigo(linea, Usuario);

                        #endregion Trayendo datos desde BD para el informe  

                        string saldo_total = stringHelper.FormatearNumerosComoMilesSinDecimales(rFila.Cells[7].Text);
                        string cRutaDeImagen;

                        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";

                        //TABLA POR CLIENTE
                        DataTable table = new DataTable();
                        table.Columns.Add("fecha_oper");
                        table.Columns.Add("nomtipo_ope");
                        table.Columns.Add("cod_ope");
                        table.Columns.Add("cod_ofi");
                        table.Columns.Add("tipo_mov");
                        table.Columns.Add("valor");
                        table.Columns.Add("saldo");
                        table.Columns.Add("nombre");
                        table.Columns.Add("soporte");                       
                        table.Columns.Add("Abonos");
                        table.Columns.Add("cargos");


                        Double TotalCargos = 0 ;
                        Double TotalAbonos = 0;

                        if (lstCreditosDet.Count > 0)
                        {
                            
                            foreach (MovimientoProducto fila in lstCreditosDet)
                            {
                                
                                // result += String.Format("Name={0}, Number={1}\n", fila.tipo_tran, fila.Number);
                                
                                Double Valor = fila.Capital + fila.IntCte + fila.IntMora + fila.Otros;
                                DataRow datarw;
                                datarw = table.NewRow();


                                datarw[0] = fila.FechaPago.ToShortDateString();
                                datarw[1] = fila.TipoOperacion;
                                datarw[2] = fila.CodOperacion;
                                datarw[3] = fila.desc_tran;
                                datarw[4] = fila.TipoMovimiento;
                                datarw[5] = Valor.ToString("n0");
                                datarw[6] = fila.Saldo.ToString("n0");
                                datarw[7] = fila.desc_tran;
                                datarw[8] = fila.idavance;
                                datarw[10] = fila.TipoMovimiento == "Débito" ? Valor : 0 ;
                                datarw[9] =fila.TipoMovimiento != "Débito" ? Valor : 0;
                                TotalAbonos +=  Convert.ToDouble(datarw[10]);
                                TotalCargos +=  Convert.ToDouble(datarw[9]);
                                table.Rows.Add(datarw);
                            }
                        }
                        else
                        {
                            DataRow datos;
                            datos = table.NewRow();
                            datos[0] = "";
                            datos[1] = "";
                            datos[2] = "";
                            datos[3] = "";
                            datos[4] = "";
                            datos[5] = "";
                            datos[6] = "";
                            datos[7] = "";
                            datos[8] = "";
                            datos[9] = "";
                            datos[10] = "";
                            table.Rows.Add(datos);
                        }

                        foreach (DataRow rData in table.Rows)
                        {
                            DataRow datarw;

                            datarw = tablegeneral.NewRow();
                            datarw[0] = numeroCuenta;
                            datarw[1] = stringHelper.FormatearNumerosComoMilesSinDecimales(identificacionn);
                            datarw[2] = NombreCompleto;
                            datarw[3] = ciudad;
                            datarw[4] = datosAhorroVista.direccion_persona;
                            datarw[5] = rData[0].ToString();
                            datarw[6] = rData[1].ToString();
                            datarw[7] = rData[2].ToString();
                            datarw[8] = rData[3].ToString();
                            datarw[9] = linea;
                            datarw[10] = saldo_total;
                            datarw[11] = rData[4].ToString();
                            datarw[12] = rData[5].ToString();
                            datarw[13] = rData[6].ToString();
                            datarw[14] = new Uri(Server.MapPath("~/Images/LogoEmpresa.jpg")).AbsoluteUri;
                            datarw[15] = contDetalle;
                            datarw[16] = 1;

                            datarw[17] = numeroCuenta;
                            datarw[18] = TotalCargos; 
                            datarw[19] =  TotalAbonos;
                            datarw[20] = stringHelper.FormatearNumerosComoMilesSinDecimales(0);
                            datarw[21] = stringHelper.FormatearNumerosComoMilesSinDecimales(0);
                            datarw[22] = stringHelper.FormatearNumerosComoMilesSinDecimales(0);
                            datarw[23] = stringHelper.FormatearNumerosComoMilesSinDecimales(0);

                            datarw[24] = reporteMovimientoCuenta.Contador_Abonos; 
                            datarw[25] = reporteMovimientoCuenta.Contador_Cargos; 
                            datarw[26] = reporteMovimientoCuenta.Contador_Intereses;
                            datarw[27] = reporteMovimientoCuenta.Contador_GMF;
                            datarw[28] = reporteMovimientoCuenta.Contador_Retencion;

                            //datarw[29] = datosAhorroVista.nombre_oficina + " " + Usuario.empresa;
                            datarw[29] = Usuario.empresa;
                            datarw[30] = datosAhorroVista.direccion_oficina;
                            datarw[31] = datosAhorroVista.telefono_oficina;
                            datarw[32] = rData[8].ToString();
                            tablegeneral.Rows.Add(datarw);
                        }

                        Site toolBar = (Site)Master;
                        toolBar.MostrarLimpiar(false);
                        toolBar.MostrarConsultar(false);
                    }
                }
            }

            Usuario pUsu = (Usuario)Session["usuario"];

            rvExtracto.LocalReport.DataSources.Clear();
            ReportParameter[] param = new ReportParameter[4];
            param[0] = new ReportParameter("nit", pUsu.nitempresa);
            param[1] = new ReportParameter("FechaInicio", fechaInicio.ToShortDateString());
            param[2] = new ReportParameter("fecha_corte", Convert.ToDateTime(txtFecha_corte.Text).ToShortDateString());
            param[3] = new ReportParameter("observaciones", txtObservacionesExtracto.Text);


            rvExtracto.LocalReport.EnableExternalImages = true;
            rvExtracto.LocalReport.SetParameters(param);

            ReportDataSource rds1 = new ReportDataSource("DataSet2", tablegeneral);
            rvExtracto.LocalReport.DataSources.Add(rds1);
            rvExtracto.LocalReport.Refresh();

            if (tablegeneral.Rows.Count > 0)
            {


                Site toolBar = (Site)Master;
                mvPrincipal.ActiveViewIndex = 2;
                toolBar.MostrarCancelar(true);
                toolBar.MostrarExportar(false);
                toolBar.MostrarImprimir(false);
                toolBar.MostrarLimpiar(false);
                toolBar.MostrarConsultar(false);
            }
        }
    }
    protected void txtNombres_TextChanged(object sender, System.EventArgs e)
    {

    }
}




#region Titulares

/// <summary>
/// Método para instar un detalle en blanco para cuando la grilla no tiene datos
/// </summary>
/// <param name="consecutivo"></param>


/// <summary>
/// Método para cambio de página
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>






/// <summary>
/// Método para borrar un registro de la grilla
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>




#endregion
