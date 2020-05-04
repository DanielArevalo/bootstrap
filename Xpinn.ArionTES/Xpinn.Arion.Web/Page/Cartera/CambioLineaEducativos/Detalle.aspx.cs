using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Configuration;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Business;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Microsoft.CSharp;
using System.IO;

public partial class Detalle : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.FabricaCreditos.Services.codeudoresService CodeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
    private List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables
    private Xpinn.FabricaCreditos.Services.ControlTiemposService ControlProcesosServicio = new Xpinn.FabricaCreditos.Services.ControlTiemposService();
    private CambioLineaService CambioLineaServicio = new CambioLineaService();

    String FechaDatcaredito = "";
    String pIdCodLinea = "";
    String tasa = "0";
    String ListaSolicitada = null;
    Int64 tipoempresa = 0;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[CambioLineaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CambioLineaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(CambioLineaServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CambioLineaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAprobacion.ActiveViewIndex = 0;

                // Determinar si el usuario puede modificar la tasa
                OficinaService oficinaService = new OficinaService();
                Usuario usuap = (Usuario)Session["usuario"];
                int cod = Convert.ToInt32(usuap.codusuario);
                int consulta = oficinaService.UsuarioPuedecambiartasas(cod, (Usuario)Session["usuario"]);
                if (consulta == 0) // Si no tiene atribuciones                 
                    CargarDDL();
                InicialCodeudores();
                ddlEmpresa.Visible = false;
                lblEmpresa.Visible = false;
                CargarDropDownEmpresa();

                if (Session[CambioLineaServicio.CodigoPrograma + ".id"] != null)
                {
                    // Determinar si el llamado se hizo desde la hoja de ruta
                    if (Request.UrlReferrer != null)
                        if (Request.UrlReferrer.Segments[4].ToString() == "HojaDeRuta/")
                            Session["HojaDeRuta"] = "1";
                    // Mostrar los datos del crédito
                    idObjeto = Session[CambioLineaServicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }

            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CambioLineaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);

    }

    private void CargarDDL()
    {
        String ListaSolicitada = null;

        // Llena el DDL de la periodiciadad
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        ListaSolicitada = "Periodicidad";
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        ddlperiodicidad.DataSource = lstDatosSolicitud;
        ddlperiodicidad.DataTextField = "ListaDescripcion";
        ddlperiodicidad.DataValueField = "ListaIdStr";
        ddlperiodicidad.DataBind();
        ListItem selectedListItem2 = ddlperiodicidad.Items.FindByValue("1"); //Selecciona mensual por defecto
        if (selectedListItem2 != null)
            selectedListItem2.Selected = true;

        // LLena el DDL de las líneas de crédito sin auxilio  
        List<Xpinn.FabricaCreditos.Entities.LineasCredito> LstLineas = new List<Xpinn.FabricaCreditos.Entities.LineasCredito>();
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineasServicios = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        Xpinn.FabricaCreditos.Entities.LineasCredito vLineas = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        vLineas.educativo = 1;
        LstLineas = LineasServicios.ListarLineasCreditoSinAuxilio(vLineas, (Usuario)Session["Usuario"]);
        ddllineas.DataSource = LstLineas;
        ddllineas.DataTextField = "nombre";
        ddllineas.DataValueField = "cod_linea_credito";
        ddllineas.DataBind();


    }


    /// <summary>
    /// Método que muestra los datos del crédito a aprobar
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Credito credito = new Credito();

            if (pIdObjeto != null)
            {
                credito.numero_radicacion = Int64.Parse(pIdObjeto);
                credito = CambioLineaServicio.ConsultarCredito(credito, (Usuario)Session["usuario"]);

                if (!string.IsNullOrEmpty(credito.numero_radicacion.ToString()))
                    txtCredito.Text = HttpUtility.HtmlDecode(credito.numero_radicacion.ToString());
                if (!string.IsNullOrEmpty(credito.cod_deudor.ToString()))
                    txtCodigo.Text = HttpUtility.HtmlDecode(credito.cod_deudor.ToString());
                if (!string.IsNullOrEmpty(credito.forma_pago.ToString()))
                    txtFormaPago.Text = HttpUtility.HtmlDecode(credito.forma_pago.ToString());
                if (!string.IsNullOrEmpty(credito.tipo_identificacion.ToString()))
                    txtTipoIdentificacion.Text = HttpUtility.HtmlDecode(credito.tipo_identificacion.ToString());
                if (!string.IsNullOrEmpty(credito.linea_credito.ToString()))
                    txtLinea.Text = HttpUtility.HtmlDecode(credito.linea_credito.ToString());
                if (!string.IsNullOrEmpty(credito.identificacion))
                    txtId.Text = HttpUtility.HtmlDecode(credito.identificacion.ToString());
                if (!string.IsNullOrEmpty(credito.monto.ToString()))
                {
                    txtMonto.Text = HttpUtility.HtmlDecode(credito.monto.ToString());
                }
                if (!string.IsNullOrEmpty(credito.valor_cuota.ToString()))
                {
                    txtCuota.Text = HttpUtility.HtmlDecode(credito.valor_cuota.ToString());
                }
                if (!string.IsNullOrEmpty(credito.nombre))
                    txtNombres.Text = HttpUtility.HtmlDecode(credito.nombre.ToString());

                if (!string.IsNullOrEmpty(credito.periodicidad))
                {
                    txtPeriodicidad.Text = HttpUtility.HtmlDecode(credito.periodicidad);
                }
                if (credito.plazo != Int64.MinValue)
                {
                    txtPlazoold.Text = HttpUtility.HtmlDecode(credito.plazo.ToString());
                }
                if (credito.fecha_aprobacion != DateTime.MinValue)
                {
                    txtFechaAprobacion.Text = HttpUtility.HtmlDecode(credito.fecha_aprobacion.ToString());
                }
                if (credito.fecha_prox_pago != DateTime.MinValue)
                {
                    txtFechaProxPago_old.Text = HttpUtility.HtmlDecode(credito.fecha_prox_pago.ToShortDateString().ToString());
                }
                if (credito.fecha_ultimo_pago != DateTime.MinValue)
                {
                    txtFechaUltPago.Text = HttpUtility.HtmlDecode(credito.fecha_ultimo_pago.ToShortDateString().ToString());
                }
                if (!string.IsNullOrEmpty(credito.saldo_capital.ToString()))
                {
                    txtSaldoCapital.Text = HttpUtility.HtmlDecode(credito.saldo_capital.ToString());
                }
                if (!string.IsNullOrEmpty(credito.valor_a_pagar.ToString()))
                {
                    txtValorTotal.Text = HttpUtility.HtmlDecode(credito.valor_a_pagar.ToString());
                }
                TablaCodeudores();
                Tabs.Enabled = true;
                Tabs.Visible = true;
                Tabs.ActiveTabIndex = 0;
                TablaDocumentos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CambioLineaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    private Xpinn.FabricaCreditos.Entities.Persona1 ObtenerValoresCodeudores()
    {
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

        if (idObjeto != "")
            vPersona1.numeroRadicacion = Convert.ToInt64(this.txtCredito.Text.ToString());

        vPersona1.seleccionar = "CD"; //Bandera para ejecuta el select del CODEUDOR

        return vPersona1;
    }
    /// <summary>
    /// Método para consultar los codeudores del crédito
    /// </summary>
    private void TablaCodeudores()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            lstConsulta = Persona1Servicio.ListarPersona1(ObtenerValoresCodeudores(), (Usuario)Session["usuario"]);

            gvCodeudores.PageSize = 5;
            gvCodeudores.EmptyDataText = "No se encontraron registros";
            gvCodeudores.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvCodeudores.Visible = true;
                lblTotalRegsCodeudores.Visible = false;
                lblTotalRegsCodeudores.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvCodeudores.DataBind();
                ValidarPermisosGrilla(gvCodeudores);
                Session["Codeudores"] = lstConsulta;

            }
            else
            {
                idObjeto = "";
                gvCodeudores.Visible = false;
                lblTotalRegsCodeudores.Text = "No hay codeudores para este crédito";
                lblTotalRegsCodeudores.Visible = true;
                InicialCodeudores();
            }

            Session.Add(Persona1Servicio.CodigoProgramaCodeudor + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "Actualizar", ex);
        }

    }
    private void TablaDocumentos()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Documento> lstConsultaDoc;
            Int64 credito = Convert.ToInt64(txtCredito.Text);
            lstConsultaDoc = CambioLineaServicio.ListarDocumentosGarantia((Usuario)Session["usuario"], credito);

            gvDocumentos.PageSize = 5;
            gvDocumentos.EmptyDataText = "No se encontraron registros";
            gvDocumentos.DataSource = lstConsultaDoc;

            if (lstConsultaDoc.Count > 0)
            {

                // gvDocumentos.Visible = true;
                lblTotalRegsCodeudores.Visible = false;
                lblTotalRegsCodeudores.Text = "<br/> Registros encontrados " + lstConsultaDoc.Count.ToString();
                //  gvDocumentos.DataBind();
                Session["Documentos"] = lstConsultaDoc;
            }
            else
            {
                Session["Documentos"] = null;
                idObjeto = "";
                gvDocumentos.Visible = false;
            }
            //DataTable dtAgre = new DataTable();
            //// dtAgre = (DataTable)Session["Codeudores"];
            //if (dtAgre.Rows[0][0] == null || dtAgre.Rows[0][0].ToString() == "")
            //{
            //    dtAgre.Rows[0].Delete();
            //}

            //DataRow fila = dtAgre.NewRow();


            //dtAgre.Rows.Add(fila);
            //gvDocumentos.DataSource = dtAgre;
            //gvDocumentos.DataBind();
            //  Session["Documentos"] = lstConsultaDoc;


            Session.Add(Persona1Servicio.CodigoProgramaCodeudor + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "Actualizar", ex);
        }

    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }
    public void MensajeFinal(string pmensaje)
    {
       
        mvAprobacion.ActiveViewIndex = 1;
        // lblMensajeGrabar.Text = pmensaje;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //  Métodos para control de la grilla de codeudores
    //
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void gvCodeudores_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtidentificacion = (TextBox)gvCodeudores.FooterRow.FindControl("txtidentificacion");
            Label txtcodigo = (Label)gvCodeudores.FooterRow.FindControl("txtcodpersona");
            Label txtnombres = (Label)gvCodeudores.FooterRow.FindControl("txtnombres");
            if (txtidentificacion.Text.Trim() == "")
            {
                VerError("Ingrese la Identificación del Codeudor por favor.");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                return;
            }
            Xpinn.Comun.Data.GeneralData DAGeneral = new Xpinn.Comun.Data.GeneralData();
            Xpinn.Comun.Entities.General pEntidad = new Xpinn.Comun.Entities.General();
            pEntidad = DAGeneral.ConsultarGeneral(480, (Usuario)Session["usuario"]);
            try
            {
                if (pEntidad.valor != null)
                {
                    if (Convert.ToInt32(pEntidad.valor) > 0)
                    {
                        int paramCantidad = 0, cantReg = 0;
                        paramCantidad = Convert.ToInt32(pEntidad.valor);
                        Xpinn.FabricaCreditos.Entities.codeudores pCodeu = new Xpinn.FabricaCreditos.Entities.codeudores();
                        pCodeu = CodeudorServicio.ConsultarCantidadCodeudores(txtidentificacion.Text, (Usuario)Session["usuario"]);
                        if (pCodeu.cantidad != null)
                        {
                            cantReg = Convert.ToInt32(pCodeu.cantidad);
                            if (cantReg >= paramCantidad)
                            {
                                VerError("No puede adicionar esta persona debido a que ya mantiene el límite de veces como codeudor.");
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                                return;
                            }
                        }
                    }
                }
            }
            catch { }

            DataTable dtAgre = new DataTable();
            dtAgre = (DataTable)Session["Codeudores"];

            if (dtAgre.Rows[0][0] == null || dtAgre.Rows[0][0].ToString() == "")
            {
                dtAgre.Rows[0].Delete();
            }

            DataRow fila = dtAgre.NewRow();

            fila[0] = txtcodigo.Text;
            fila[1] = txtidentificacion.Text;
            fila[3] = txtnombres.Text;

            dtAgre.Rows.Add(fila);
            gvCodeudores.DataSource = dtAgre;
            gvCodeudores.DataBind();
            Session["Codeudores"] = dtAgre;
        }
    }
    protected void gvCodeudores_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Control ctrl = e.Row.FindControl("txtidentificacion");
            if (ctrl != null)
            {
                TextBox txtidentificacion = ctrl as TextBox;
                txtidentificacion.TextChanged += txtidentificacion_TextChanged;
            }
        }
    }
    protected void gvCodeudores_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable table = new DataTable();
            table = (DataTable)Session["Codeudores"];

            if ((e.RowIndex == 0) && (table.Rows[0][0] != null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
            {
                table.Rows.Add();
            }

            table.Rows[e.RowIndex].Delete();

            gvCodeudores.DataSource = table;
            gvCodeudores.DataBind();
            Session["Codeudores"] = table;

            if ((e.RowIndex == 0) && (table.Rows[0][0] == null || table.Rows[0][0].ToString() == "") && (table.Rows.Count == 1))
                gvCodeudores.Rows[0].Visible = false;

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(this.CambioLineaServicio.GetType().Name + "L", "gvCodeudores_RowDeleting", ex);
        }
    }
    /// <summary>
    ///  Método para insertar un registro en la grilla cuando no hay codeudores
    /// </summary>

    protected void InicialCodeudores()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("codpersona");
        dt.Columns.Add("identificacion");
        dt.Columns.Add("tipo_identificacion");
        dt.Columns.Add("nombres");
        dt.Rows.Add();
        Session["Codeudores"] = dt;
        gvCodeudores.DataSource = dt;
        gvCodeudores.DataBind();
        gvCodeudores.Visible = true;
    }
    /// <summary>
    /// Método para control al digitar la identificación del codeudor
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtidentificacion_TextChanged(object sender, EventArgs e)
    {
        Control ctrl = gvCodeudores.FooterRow.FindControl("txtidentificacion");
        if (ctrl != null)
        {
            TextBox txtidentificacion = (TextBox)ctrl;
            Control ctrln = gvCodeudores.FooterRow.FindControl("txtnombres");
            if (ctrln != null)
            {
                Label txtcodigo = (Label)gvCodeudores.FooterRow.FindControl("txtcodpersona");
                Label txtnombre = (Label)ctrln;
                Xpinn.FabricaCreditos.Services.codeudoresService codeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
                Xpinn.FabricaCreditos.Entities.codeudores vcodeudor = new Xpinn.FabricaCreditos.Entities.codeudores();
                vcodeudor = codeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, (Usuario)Session["Usuario"]);
                txtcodigo.Text = vcodeudor.codpersona.ToString();
                txtnombre.Text = vcodeudor.nombres;
            }
        }
    }

    /// Mètodo para botòn de continuar el cual lleva a la lista de consulta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["usuario"]);
        return lstDatosSolicitud;
    }
    protected void ddllineas_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = ControlProcesosServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["usuario"]);
    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlFormaPago.SelectedItem.Value == "N")
        {
            lblEmpresa.Visible = true;
            ddlEmpresa.Visible = true;
        }
        else
        {
            lblEmpresa.Visible = false;
            ddlEmpresa.Visible = false;
        }

    }
    void CargarDropDownEmpresa()
    {
        List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstEmpresas = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
        Xpinn.Tesoreria.Entities.EmpresaRecaudo empresa = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
        Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();
        if (txtCodigo.Text == "")
        {
            ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudo(empresa, (Usuario)Session["usuario"]);
        }
        else
        {
            try
            {
                Int64 Cod_persona = Convert.ToInt64(txtCodigo.Text.ToString());
                ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudoPersona(Cod_persona, (Usuario)Session["usuario"]);
            }
            catch
            {
                ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudo(empresa, (Usuario)Session["usuario"]);
            }
        }
        ddlEmpresa.DataTextField = "nom_empresa";
        ddlEmpresa.DataValueField = "cod_empresa";
        ddlEmpresa.AppendDataBoundItems = true;
        ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlEmpresa.SelectedIndex = 0;
        ddlEmpresa.DataBind();
    }
    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        mpeNuevo.Show();
    }
    protected void btnContinua_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    /// <summary>
    /// Método para el evento de continuar con la re-estructuración
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
       
        GrabarCredito();
        
    }
    protected void GrabarCredito()
    {
        VerError("");
        Xpinn.FabricaCreditos.Entities.CreditoRecoger cRecoger = new Xpinn.FabricaCreditos.Entities.CreditoRecoger();
        Xpinn.FabricaCreditos.Entities.Credito credito = new Xpinn.FabricaCreditos.Entities.Credito();
        string error = "";
        List<Xpinn.FabricaCreditos.Entities.CreditoRecoger> LstRecoger = new List<Xpinn.FabricaCreditos.Entities.CreditoRecoger>();


        // Cargar los datos del  crédito a recoger
        decimal totalRecoge = 0;
        cRecoger.numero_credito = Convert.ToInt64(txtCredito.Text.ToString());
        string svalor = Convert.ToString(txtValorTotal.Text.Replace("$", "").Replace(".", "").Replace(" ", "").Replace(",", ""));
        cRecoger.valor_recoge = Convert.ToDecimal(svalor);
        cRecoger.fecha_pago = Convert.ToDateTime(txtFechaProxPago_old.Text);
        LstRecoger.Add(cRecoger);

        //// Cargar los codeudores
        credito.lstCodeudores = new List<codeudores>();
        DataTable dtAgre = new DataTable();
        dtAgre = (DataTable)Session["Codeudores"];
        codeudores vCodeudores = new codeudores();
        foreach (DataRow drFila in dtAgre.Rows)
        {
            if (dtAgre.Rows[0][0].ToString() != "")
            {
                vCodeudores.idcodeud = 0;
                vCodeudores.codpersona = Convert.ToInt64(drFila[0].ToString());
                vCodeudores.identificacion = drFila[1].ToString();
                vCodeudores.tipo_codeudor = "S";
                vCodeudores.nombres = drFila[3].ToString();
                vCodeudores.opinion = " ";
                vCodeudores.parentesco = 0;
                vCodeudores.responsabilidad = " ";
                credito.lstCodeudores.Add(vCodeudores);
            }
        }


        // Cargar los datos del crédito
        Credito cred = new Credito();
        Usuario usuap = (Usuario)Session["usuario"];
        cred.estado = "C";
        if (txtFechaRes.TieneDatos == false)
        {
            VerError("Debe ingresar la fecha del Crédito");
            return;
        }
        else
        {
            cred.fecha_solicitud = Convert.ToDateTime(txtFechaRes.Text);
            DateTime fecha_inicio = Convert.ToDateTime(txtfechaproxpago.ToDate);
            DateTime fechaactual = Convert.ToDateTime(DateTime.Now);
            if (txtfechaproxpago.TieneDatos == false)
            {
                VerError("Debe ingresar la fecha del próximo pago");
                return;
            }
            if (fecha_inicio < fechaactual)
            {
                VerError("La fecha de primer pago  no puede ser inferior a la fecha actual");
                return;
            }

            cred.fecha_prim_pago = Convert.ToDateTime(txtfechaproxpago.ToDate);
            cred.cod_deudor = Convert.ToInt64(txtCodigo.Text);
            string sMonto = txtValorTotal.Text.Replace(".", "");
            cred.monto = Convert.ToInt64(sMonto);
            cred.cod_ope = 0;
            cred.cod_linea_credito = ddllineas.SelectedValue;
            cred.periodicidad = ddlperiodicidad.SelectedValue;
            cred.forma_pago = ddlFormaPago.SelectedValue;
            cred.empresa = ddlEmpresa.SelectedValue;
            cred.codigo_oficina = usuap.cod_oficina;

            cred.cod_usuario = Convert.ToInt32(usuap.codusuario);
            if (txtplazo.Text == "")
            {
                VerError("Debe ingresar el plazo del crédito");
                return;
            }
            cred.plazo = Convert.ToInt64(txtplazo.Text);
            //se crea el credito 
            Int64 numero_radicacion = Int64.MinValue;
        
        cred.lstCodeudores = credito.lstCodeudores;
        List<Xpinn.FabricaCreditos.Entities.Documento> LstDocumentos;
        LstDocumentos = (List<Xpinn.FabricaCreditos.Entities.Documento>)Session["Documentos"];

        if (LstDocumentos != null)
        {
            cred.lstDocumentos = LstDocumentos;

        }
        else
        {
            LstDocumentos = new List<Xpinn.FabricaCreditos.Entities.Documento>();
        }

        CambioLineaServicio.CrearCredito(cred, LstRecoger, ref numero_radicacion, ref  error, (Usuario)Session["Usuario"]);
        if (error == "")
        {
            mvAprobacion.ActiveViewIndex = 1;

            lblMensaje.Text = lblMensaje.Text + " Radicación: " + numero_radicacion;
            Session["numero_radicacion"] = numero_radicacion;
            GenerarDesembolso();
        }
        else
        {
            VerError(error);
        }
        }
    }
    protected void GenerarDesembolso()
    {

        string error = "";
        // Cargar los datos del crédito
        Credito cred = new Credito();
        Usuario usuap = (Usuario)Session["usuario"];
        cred.estado = "C";
        cred.fecha_solicitud = Convert.ToDateTime(txtFechaRes.Text);
        DateTime fecha_inicio = Convert.ToDateTime(txtfechaproxpago.ToDate);
        cred.fecha_desembolso = Convert.ToDateTime(txtFechaRes.Text);
        DateTime fechaactual = Convert.ToDateTime(DateTime.Now);
        if (fecha_inicio < fechaactual)
        {
            VerError("La fecha de primer pago  no puede ser inferior a la fecha actual");
            return;
        }
        else
        {
            cred.fecha_prim_pago = Convert.ToDateTime(txtfechaproxpago.ToDate);
        }
        cred.cod_deudor = Convert.ToInt64(txtCodigo.Text);
        string sMonto = txtValorTotal.Text.Replace(".", "");
        cred.monto = Convert.ToInt64(sMonto);
        cred.cod_ope = 0;
        cred.cod_linea_credito = ddllineas.SelectedValue;
        cred.periodicidad = ddlperiodicidad.SelectedValue;
        cred.forma_pago = ddlFormaPago.SelectedValue;
        cred.empresa = ddlEmpresa.SelectedValue;
        cred.codigo_oficina = usuap.cod_oficina;
        cred.numero_credito = Convert.ToInt64(Session["numero_radicacion"]);
        cred.cod_usuario = Convert.ToInt32(usuap.codusuario);
        if (txtplazo.Text == "")
        {
            rfvPlazo.Visible = true;
        }
        else
        {
            rfvPlazo.Visible = false;
            cred.plazo = Convert.ToInt64(txtplazo.Text);
        }

        cred.estado = "C";

        // Realizar el desembolso del crèdito   
        bool popcion = false;
        popcion = true;
        CambioLineaServicio.DesembolsarCredito(cred, popcion, ref error, (Usuario)Session["usuario"]);

        /// Guardar los datos de la cuenta del cliente y generar el comprobante si se pudo crear la operaciòn.
        if (cred.cod_ope != 0)
        {

            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = cred.cod_ope;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 1;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = cred.cod_deudor;
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
        }

        Session.Remove(CambioLineaServicio.CodigoPrograma + ".id");
    }
    protected void btnParar_Click(object sender, EventArgs e)
    {

    }



}

