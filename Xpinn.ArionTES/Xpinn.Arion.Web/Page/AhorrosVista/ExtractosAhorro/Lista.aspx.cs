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
using System.Threading.Tasks;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using System.Threading;
using System.Globalization;

partial class Lista : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices ahorrosServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ahorrosServicio.CodigoProgramaExtractosAhorro + ".id"] != null)
                VisualizarOpciones(ahorrosServicio.CodigoProgramaExtractosAhorro, "E");
            else
                VisualizarOpciones(ahorrosServicio.CodigoProgramaExtractosAhorro, "A");

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
            
            if (Ejecutando != null && Ejecutando.Status == TaskStatus.Running)
            {
                Label2.Text = @"Proceso corriendo " + Ejecutando.Id + @"/" + Convert.ToString(Session["Valorescompletos"]);
            }
            if (!IsPostBack)
            {

                // txtObservacionesExtracto.Text = "Periodo Comprendido entre " + fechain.ToShortDateString() + "  y  " + fechafin.ToShortDateString();
                CargaDropDown();
                mvPrincipal.ActiveViewIndex = 0;
                txtFecha_corte.ToDateTime = DateTime.Now;
                if (Ejecutando == null || Ejecutando.Status != TaskStatus.Running)
                    Session["Valorescompletos"] = null;

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
        else if (mvPrincipal.ActiveViewIndex == 2 || mvPrincipal.ActiveViewIndex == 3)
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
    private string obtFiltro(AhorroVista ahorro)
    {
        String filtro = String.Empty;

        if (txtCodigo.Text != "")
            if (txtCodigo.Text != "" && txtcodigo_final.Text != "")
                filtro += " and p.Cod_Persona between " + txtCodigo.Text + " and " + txtcodigo_final.Text;
            else
                filtro += " and p.Cod_Persona  = " + txtCodigo.Text;
        else if (txtcodigo_final.Text != "")
            filtro += " and p.Cod_Persona  = " + txtcodigo_final.Text;

        if (txtidentificacion.Text.Trim() != "in(")
        {
            if (txtidentificacion.Text != "")
                if (txtidentificacion.Text != "" && txtidentificacion_final.Text != "")
                    filtro += " and ESNULO(StrToNumber(p.Identificacion),0) between " + txtidentificacion.Text + " and " + txtidentificacion_final.Text + "";
                else
                    filtro += " and p.Identificacion = '" + txtidentificacion.Text + "' ";
            else if (txtidentificacion_final.Text != "")
                filtro += " and p.Identificacion <= '" + txtidentificacion_final.Text + "' ";
        }

        if (txtidentificacion.Text.Trim() == "in(")
            filtro += " and p.Identificacion In (" + txtidentificacion_final.Text + ")";

        if (txtNumeroCuenta.Text != "")
            if (txtNumeroCuenta.Text != "" && this.txtNumCuenta_final.Text != "")
                filtro += " and a.numero_cuenta between " + txtNumeroCuenta.Text + " and " + txtNumCuenta_final.Text + "";
            else
                filtro += " and  a.numero_cuenta  = '" + txtNumeroCuenta.Text + "' ";
        else if (txtNumCuenta_final.Text != "")
            filtro += " and a.numero_cuenta <= '" + txtNumCuenta_final.Text + "' ";


        if (txtNombres.Text != "")
            filtro += " and p.Nombres like '%" + txtNombres.Text + "%'";

        if (txtApellidos.Text != "")
            filtro += " and p.Apellidos like '%" + txtApellidos.Text + "%'";

        if (ddlLineaAhorro.Indice != 0)

        {
            filtro += " and a.COD_LINEA_AHORRO=" + ddlLineaAhorro.Value;

        }


        //Fecha de último envío 

        if (txtUltimoEnvio.ToDate.Trim() != "")
        {
            filtro += @" and p.cod_persona not in (
                        select distinct cod_persona
                        from Historial_Notificacion
                        where FECHA_ENVIO >= TO_DATE('" + Convert.ToDateTime(txtUltimoEnvio.ToDate.Trim()).ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY')
                        and codigo = 5)";
        }



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
            String filtro = obtFiltro(ObtenerValores());
            List<Xpinn.Ahorros.Entities.AhorroVista> lstConsulta = new List<Xpinn.Ahorros.Entities.AhorroVista>();
            DateTime pFechaIni, pFechaFin;
            pFechaIni = txtFecha_periodoInicial.ToDateTime == null ? DateTime.MinValue : txtFecha_periodoInicial.ToDateTime;
            pFechaFin = txtFecha_corte.ToDateTime == null ? DateTime.MinValue : txtFecha_corte.ToDateTime;

            lstConsulta = ahorrosServicio.ListaAhorroExtractos(ObtenerValores(), (Usuario)Session["usuario"], filtro);
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTAhorroVista"] = lstConsulta;
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

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                IniciarProceso();
                Ejecutando = Task.Factory.StartNew(() => { EnviarCorreos(); }, TaskCreationOptions.AttachedToParent);
                Ejecutando.ContinueWith((t1) => { if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled) Label4.Text = @"Proceso " + t1.Id + @"/" + gvLista.Rows.Count; });
                Session["Valorescompletos"] = gvLista.Rows.Count;
            }
        }
        catch (Exception ex)
        {
            VerError("Se ha producido un error" + ex.Message.ToString());
        }
    }



    public void IniciarProceso()
    {
        mvPrincipal.SetActiveView(ViewProceso);
        btnContinuar.Enabled = false;
        btnCancelar.Enabled = false;
        mpeNuevo.Hide();
        mpeProcesando.Show();
        Image1.Visible = true;
        Session["Proceso"] = "INICIO";
        Timer1.Enabled = true;
        Site toolbar = (Site)this.Master;
        toolbar.MostrarConsultar(false);
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (Session["Proceso"] != null)
            if (Session["Proceso"].ToString() == "FINAL")
                TerminarProceso();
            else
                mpeProcesando.Show();
        else
            mpeProcesando.Hide();
    }
    public void TerminarProceso()
    {
        mpeProcesando.Hide();
        Image1.Visible = false;
        Session.Remove("Proceso");
        Timer1.Enabled = false;
        mpeFinal.Show();
        if (Session["Error"] != null)
        {
            if (Session["Error"].ToString().Trim() != "")
                lblError.Text = Session["Error"].ToString();
            Session.Remove("Error");
        }
    }




    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            mvPrincipal.SetActiveView(ViewProceso);
            mpeNuevo.Show();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("EnvioMasivo", "btnGuardar_Click", ex);
        }
    }

    private Xpinn.Ahorros.Entities.AhorroVista ObtenerValores()
    {
        Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();

        //fecha de aprovacion

        if (txtFecha_corte.ToDate.Trim() != "")
            vAhorroVista.fec_realiza = Convert.ToDateTime(txtFecha_corte.ToDate.Trim());

        //fecha de aprovacion

        if (txtFecha_periodoInicial.ToDate.Trim() != "")
            vAhorroVista.fecha_apertura = Convert.ToDateTime(txtFecha_periodoInicial.ToDate.Trim());
        //fecha de aprovacion

        if (txtFecha_corte.ToDate.Trim() != "")
            vAhorroVista.fecha_cierre = Convert.ToDateTime(txtFecha_corte.ToDate.Trim());
        //fecha de aprovacion

        if (txtCodigo.Text.Trim() != "")
            vAhorroVista.codigo_inicial = Convert.ToInt64(txtCodigo.Text.Trim());
        //codigo
        if (txtcodigo_final.Text.Trim() != "")
            vAhorroVista.codigo_final = Convert.ToInt64(txtcodigo_final.Text.Trim());
        //codigo
        if (txtidentificacion.Text.Trim() != "")
            vAhorroVista.identificacion = Convert.ToString(txtidentificacion.Text.Trim());
        //identificacion
        if (txtidentificacion_final.Text.Trim() != "")
            vAhorroVista.identificacion_final = Convert.ToString(txtidentificacion_final.Text.Trim());
        //identificacion

        if (txtNombres.Text.Trim() != "")
            vAhorroVista.nombres = Convert.ToString(txtNombres.Text.Trim());
        //nombres
        if (txtApellidos.Text.Trim() != "")
            vAhorroVista.apellidos = Convert.ToString(txtApellidos.Text.Trim());
        //apellidos


        if (ddlEmpresa.Text.Trim() != "")
            vAhorroVista.empresa = Convert.ToString(ddlEmpresa.Text.Trim());
        //linea de ahorro

        if (ddlCiudadResidencia.SelectedIndex != 0)
            vAhorroVista.CiudadResidencia = Convert.ToString(ddlCiudadResidencia.SelectedValue.Trim());
        //Nombre oficina

        if (txtNumeroCuenta.Text.Trim() != "")
            vAhorroVista.numero_cuenta = Convert.ToString(txtNumeroCuenta.Text.Trim());
        //numero cuenta
        if (txtNumCuenta_final.Text.Trim() != "")
            vAhorroVista.numero_cuenta_final = Convert.ToString(txtNumCuenta_final.Text.Trim());
        //numero cuenta
        if (ddlLineaAhorro.Indice != 0)
            vAhorroVista.cod_linea_ahorro = Convert.ToString(this.ddlLineaAhorro.Value);




        return vAhorroVista;
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
        lstEmpresaRecaudo = EmpresaRecaudoService.ListarEmpresaRecaudo(true, (Usuario)Session["usuario"]);
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
                    Response.AddHeader("content-disposition", "attachment;filename=AhorroVista.xls");
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



    protected Boolean ValidarDatos()
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

        if (txtUltimoEnvio.Text == "")
        {
            VerError("Ingrese la fecha del último envío");
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

        if (txtidentificacion.Text != "in(")
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
                        string identificacionn = rFila.Cells[6].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[6].Text) : "";
                        string ciudad = rFila.Cells[5].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[5].Text) : "";
                        string NombreCompleto = rFila.Cells[7].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[7].Text) : "";
                        string linea = rFila.Cells[3].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[3].Text) : "";

                        #region Trayendo datos desde BD para el informe

                        ReporteMovimiento reporteMovimientoCuenta = ahorrosServicio.ConsultarExtractoAhorroVista(numeroCuenta, Convert.ToDateTime(txtFecha_corte.Text), fechaInicio, fechaFinal, (Usuario)Session["usuario"]);
                        AhorroVista datosAhorroVista = ahorrosServicio.ConsultarAhorroVistaDatosOficina(numeroCuenta, (Usuario)Session["usuario"]);
                        List<ReporteMovimiento> lstExtractos = ahorrosServicio.ListarDetalleExtracto(numeroCuenta, fechaFinal, (Usuario)Session["usuario"], fechaInicio, fechaFinal, reporteMovimientoCuenta.SaldoInicio);
                        string nombreLinea = ahorrosServicio.ConsultarNombreLineaDeAhorroPorCodigo(linea, Usuario);

                        #endregion Trayendo datos desde BD para el informe  

                        string saldo_total = stringHelper.FormatearNumerosComoMilesConDecimales(reporteMovimientoCuenta.SaldoFinal);
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

                        if (lstExtractos.Count > 0)
                        {
                            foreach (ReporteMovimiento fila in lstExtractos)
                            {
                                DataRow datarw;
                                datarw = table.NewRow();
                                datarw[0] = fila.fecha_oper.ToShortDateString();
                                datarw[1] = fila.nomtipo_ope;
                                datarw[2] = fila.cod_ope;
                                datarw[3] = fila.nombre;
                                datarw[4] = fila.tipo_mov;
                                datarw[5] = stringHelper.FormatearNumerosComoMilesConDecimales(fila.valor);
                                datarw[6] = stringHelper.FormatearNumerosComoMilesConDecimales(fila.saldo);
                                datarw[7] = fila.nombre;
                                datarw[8] = fila.soporte;

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
                            table.Rows.Add(datos);
                        }

                        foreach (DataRow rData in table.Rows)
                        {
                            DataRow datarw;

                            datarw = tablegeneral.NewRow();
                            datarw[0] = numeroCuenta;
                            datarw[1] = stringHelper.FormatearNumerosComoMilesConDecimales(identificacionn);
                            datarw[2] = NombreCompleto;
                            datarw[3] = datosAhorroVista.CiudadResidencia;
                            datarw[4] = datosAhorroVista.direccion_persona;
                            datarw[5] = rData[0].ToString();
                            datarw[6] = rData[1].ToString();
                            datarw[7] = rData[2].ToString();
                            datarw[8] = rData[3].ToString();
                            datarw[9] = nombreLinea;
                            datarw[10] = saldo_total;
                            datarw[11] = rData[4].ToString();
                            datarw[12] = rData[5].ToString();
                            datarw[13] = rData[6].ToString();
                            datarw[14] = new Uri(Server.MapPath("~/Images/LogoEmpresa.jpg")).AbsoluteUri;
                            datarw[15] = contDetalle;
                            datarw[16] = 1;

                            datarw[17] = numeroCuenta;
                            datarw[18] = stringHelper.FormatearNumerosComoMilesConDecimales(reporteMovimientoCuenta.Abonos);
                            datarw[19] = stringHelper.FormatearNumerosComoMilesConDecimales(reporteMovimientoCuenta.Cargos);
                            datarw[20] = stringHelper.FormatearNumerosComoMilesConDecimales(reporteMovimientoCuenta.Intereses);
                            datarw[21] = stringHelper.FormatearNumerosComoMilesConDecimales(reporteMovimientoCuenta.GMF);
                            datarw[22] = stringHelper.FormatearNumerosComoMilesConDecimales(reporteMovimientoCuenta.Retencion);
                            datarw[23] = stringHelper.FormatearNumerosComoMilesConDecimales(reporteMovimientoCuenta.SaldoInicio);

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
            ReportParameter[] param = new ReportParameter[8];
            param[0] = new ReportParameter("entidad", pUsu.nitempresa);
            param[1] = new ReportParameter("nit", pUsu.nitempresa);
            param[2] = new ReportParameter("fecha_corte", Convert.ToDateTime(txtFecha_corte.Text).ToShortDateString());
            param[3] = new ReportParameter("fecha_pago", Convert.ToDateTime(txtFecha_corte.Text).ToShortDateString());
            param[4] = new ReportParameter("observaciones", txtObservacionesExtracto.Text);
            param[5] = new ReportParameter("DireccionEmpresa", " ");
            param[6] = new ReportParameter("TelefonoEmpresa", " ");
            param[7] = new ReportParameter("FechaInicio", fechaInicio.ToShortDateString());
            string rutaReporte = Server.MapPath("~/Page/AhorrosVista/ExtractosAhorro/rptExtracto.rdlc");

            rvExtracto.LocalReport.EnableExternalImages = true;
            rvExtracto.LocalReport.SetParameters(param);

            ReportDataSource rds1 = new ReportDataSource("DataSet1", tablegeneral);
            rvExtracto.LocalReport.DataSources.Add(rds1);
            rvExtracto.LocalReport.Refresh();

            if (tablegeneral.Rows.Count > 0)
            {

                Site toolBar = (Site)Master;
                mvPrincipal.ActiveViewIndex = 3;
                toolBar.MostrarCancelar(true);
                toolBar.MostrarExportar(false);
                toolBar.MostrarImprimir(false);
                toolBar.MostrarLimpiar(false);
                toolBar.MostrarConsultar(false);
            }
        }
    }





    protected void EnviarCorreos()
    {
        List<Xpinn.Ahorros.Entities.AhorroVista> lstClientes = new List<AhorroVista>();
        lstClientes = (List<Xpinn.Ahorros.Entities.AhorroVista>)Session["DTAhorroVista"];
        int cont = 0;
        try
        {


            bool error = EnviarDocumento();
            Session["mensajeFinal"] = "Se enviaron " + cont + " estados de cuenta correctamente";
            Session["Proceso"] = "FINAL";

        }
        catch (Exception ex)
        {
            VerError("Ha ocurrido un error en el envio" + ex.Message.ToString());
            Session["Proceso"] = "FINAL";
            TerminarProceso();
        }
    }


    protected bool EnviarDocumento()
    {
        //vCreditoSeleccionado codigo seleccionado NUMERO CREDITO
        //Obtiene datos para el envío del correo            
        Xpinn.Comun.Services.Formato_NotificacionService COServices = new Xpinn.Comun.Services.Formato_NotificacionService();
        Configuracion conf = new Configuracion();
        Xpinn.Comun.Entities.Formato_Notificacion noti = new Xpinn.Comun.Entities.Formato_Notificacion(16);
        noti = COServices.ConsultarDatosEnvio(noti, (Usuario)Session["usuario"]);
        var fecha = txtFecha_corte.Texto;
        noti.fecha_consulta = Convert.ToDateTime(fecha);


        string separadorDecimal = System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
        string alerta = "";
        //Definición del reporte 
        ReportViewer reporte = new ReportViewer();
        reporte.ID = "rvExtracto";
        reporte.Font.Name = "Verdana";
        reporte.Font.Size = 8;
        reporte.Visible = false;
        reporte.Height = 500;
        reporte.WaitMessageFont.Name = "Verdana";
        reporte.EnableViewState = true;


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
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ES-CO");
            DateTime fechaInicio = !string.IsNullOrWhiteSpace(txtFecha_periodoInicial.Text) ? Convert.ToDateTime(txtFecha_periodoInicial.Text) : DateTime.MinValue;
            DateTime fechaFinal = !string.IsNullOrWhiteSpace(txtFecha_corte.Text) ? Convert.ToDateTime(txtFecha_corte.Text) : DateTime.MinValue;

            int cont = 0;

            foreach (GridViewRow rFila in gvLista.Rows)
            {

                CheckBox check = (CheckBox)rFila.FindControl("check");
                if (check != null)
                {
                    if (check.Checked == true)
                    {

                        cont++;
                        noti.emailReceptor = null;
                        noti.texto = noti.textoBase;
                        noti.adjunto = null;
                        noti.cod_persona = 0;
                        string email = rFila.Cells[9].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[9].Text) : "";
                        string NombreCompleto = rFila.Cells[7].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[7].Text) : "";
                        if (!string.IsNullOrEmpty(email) && email != "0")
                        {
                            string cod = rFila.Cells[10].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[10].Text) : "";
                            noti.cod_persona = Convert.ToInt32(cod);
                            noti.emailReceptor = email;
                            noti.texto = noti.texto.Replace("NombreCompletoPersona", NombreCompleto);
                            noti.fecha_consulta = txtFecha_corte.ToDateTime == null ? DateTime.Now : txtFecha_corte.ToDateTime;

                            tablegeneral.Clear();

                            // Determinar el período de generación
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("ES-CO");
                            fechaInicio = !string.IsNullOrWhiteSpace(txtFecha_periodoInicial.Text) ? Convert.ToDateTime(txtFecha_periodoInicial.Text) : DateTime.MinValue;
                            fechaFinal = !string.IsNullOrWhiteSpace(txtFecha_corte.Text) ? Convert.ToDateTime(txtFecha_corte.Text) : DateTime.MinValue;

                            // Determinar datos de la cuenta
                            String numeroCuenta = Convert.ToString(rFila.Cells[2].Text);
                            string identificacionn = rFila.Cells[6].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[6].Text) : "";
                            string ciudad = rFila.Cells[5].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[5].Text) : "";
                            string linea = rFila.Cells[3].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[3].Text) : "";
                            #region Trayendo datos desde BD para el informe

                            // Traer los movimientos de la cuenta
                            ReporteMovimiento reporteMovimientoCuenta = ahorrosServicio.ConsultarExtractoAhorroVista(numeroCuenta, Convert.ToDateTime(txtFecha_corte.Text), fechaInicio, fechaFinal, (Usuario)Session["usuario"]);
                            AhorroVista datosAhorroVista = ahorrosServicio.ConsultarAhorroVistaDatosOficina(numeroCuenta, (Usuario)Session["usuario"]);
                            List<ReporteMovimiento> lstExtractos = ahorrosServicio.ListarDetalleExtracto(numeroCuenta, fechaFinal, (Usuario)Session["usuario"], fechaInicio, fechaFinal, reporteMovimientoCuenta.SaldoInicio);
                            string nombreLinea = ahorrosServicio.ConsultarNombreLineaDeAhorroPorCodigo(linea, Usuario);

                            #endregion Trayendo datos desde BD para el informe  

                            string saldo_total = stringHelper.FormatearNumerosComoMilesConDecimales(reporteMovimientoCuenta.SaldoFinal);
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

                            if (lstExtractos.Count > 0)
                            {
                                foreach (ReporteMovimiento fila in lstExtractos)
                                {
                                    DataRow datarw;
                                    datarw = table.NewRow();
                                    datarw[0] = fila.fecha_oper.ToShortDateString();
                                    datarw[1] = fila.nomtipo_ope;
                                    datarw[2] = fila.cod_ope;
                                    datarw[3] = fila.nombre;
                                    datarw[4] = fila.tipo_mov;
                                    datarw[5] = stringHelper.FormatearNumerosComoMilesConDecimales(fila.valor);
                                    datarw[6] = stringHelper.FormatearNumerosComoMilesConDecimales(fila.saldo);
                                    datarw[7] = fila.nombre;
                                    datarw[8] = fila.soporte;

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
                                table.Rows.Add(datos);
                            }

                            // Cargar todos los datos en una tabla para poder generar el reporte
                            foreach (DataRow rData in table.Rows)
                            {
                                DataRow datarw;

                                datarw = tablegeneral.NewRow();
                                datarw[0] = numeroCuenta;
                                datarw[1] = stringHelper.FormatearNumerosComoMilesConDecimales(identificacionn);
                                datarw[2] = NombreCompleto;
                                datarw[3] = datosAhorroVista.CiudadResidencia;
                                datarw[4] = datosAhorroVista.direccion_persona;
                                datarw[5] = rData[0].ToString();
                                datarw[6] = rData[1].ToString();
                                datarw[7] = rData[2].ToString();
                                datarw[8] = rData[3].ToString();
                                datarw[9] = nombreLinea;
                                datarw[10] = saldo_total;
                                datarw[11] = rData[4].ToString();
                                datarw[12] = rData[5].ToString();
                                datarw[13] = rData[6].ToString();
                                datarw[14] = new Uri(Server.MapPath("~/Images/LogoEmpresa.jpg")).AbsoluteUri;
                                datarw[15] = contDetalle;
                                datarw[16] = 1;

                                datarw[17] = numeroCuenta;
                                datarw[18] = stringHelper.FormatearNumerosComoMilesConDecimales(reporteMovimientoCuenta.Abonos);
                                datarw[19] = stringHelper.FormatearNumerosComoMilesConDecimales(reporteMovimientoCuenta.Cargos);
                                datarw[20] = stringHelper.FormatearNumerosComoMilesConDecimales(reporteMovimientoCuenta.Intereses);
                                datarw[21] = stringHelper.FormatearNumerosComoMilesConDecimales(reporteMovimientoCuenta.GMF);
                                datarw[22] = stringHelper.FormatearNumerosComoMilesConDecimales(reporteMovimientoCuenta.Retencion);
                                datarw[23] = stringHelper.FormatearNumerosComoMilesConDecimales(reporteMovimientoCuenta.SaldoInicio);

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

                            Usuario pUsu = (Usuario)Session["usuario"];

                            reporte.LocalReport.DataSources.Clear();
                            ReportParameter[] param = new ReportParameter[4];
                            param[0] = new ReportParameter("nit", pUsu.nitempresa);
                            param[1] = new ReportParameter("FechaInicio", fechaInicio.ToShortDateString());
                            param[2] = new ReportParameter("fecha_corte", Convert.ToDateTime(txtFecha_corte.Text).ToShortDateString());
                            param[3] = new ReportParameter("observaciones", txtObservacionesExtracto.Text);
                            string rutaReporte = Server.MapPath("~/Page/AhorrosVista/ExtractosAhorro/rptExtracto.rdlc");
                            reporte.LocalReport.ReportPath = rutaReporte;
                            reporte.LocalReport.EnableExternalImages = true;
                            reporte.LocalReport.SetParameters(param);

                            ReportDataSource rds1 = new ReportDataSource("DataSet1", tablegeneral);
                            reporte.LocalReport.DataSources.Add(rds1);
                            reporte.ServerReport.DisplayName = numeroCuenta;
                            reporte.LocalReport.DisplayName = numeroCuenta;
                            reporte.LocalReport.Refresh();

                            // Enviar el email con el extracto
                            if (email != "")
                            {
                                var bytes = reporte.LocalReport.Render("PDF"); //Reporte en bytes
                                //EnviarCorreoAsociado(NombreCompleto, email, bytes, numeroCuenta);
                                if (bytes != null) //Enviar correo si el reporte se generó
                                {
                                    noti.adjunto = bytes;
                                    return COServices.SendEmailExtracto(noti, (Usuario)Session["usuario"]);
                                    //return Noti EnviarCorreoAsociado(nomAsociado, emailAsociado, bytes, ident);                    
                                }
                            }
                        }
                    }
                }
            }

            // Si se generaron registros mostrar el reporte
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

        return false;
    }


    protected bool EnviarCorreoAsociado(string nomAsociado, string emailAsociado, byte[] bytes, string ident)
    {
        try
        {
            Usuario pUsuario = (Usuario)Session["usuario"];
            TiposDocCobranzasServices _tipoDocumentoServicio = new TiposDocCobranzasServices();

            Xpinn.Comun.Entities.Empresa empresa = _tipoDocumentoServicio.ConsultarCorreoEmpresa(pUsuario.idEmpresa, pUsuario);
            ParametroCorreo parametroCorreo = (ParametroCorreo)Enum.Parse(typeof(ParametroCorreo), ((int)TipoDocumentoCorreo.EstadoCuenta).ToString());
            TiposDocCobranzas modificardocumento = _tipoDocumentoServicio.ConsultarFormatoDocumentoCorreo((int)parametroCorreo, pUsuario);

            parametrosFormatoCorreo = new Dictionary<ParametroCorreo, string>();
            parametrosFormatoCorreo.Add(ParametroCorreo.NombreCompletoPersona, nomAsociado);
            modificardocumento.texto = ReemplazarParametrosEnElMensajeCorreo(modificardocumento.texto);

            CorreoHelper correoHelper = new CorreoHelper(emailAsociado, empresa.e_mail, empresa.clave_e_mail);
            bool exitoso = correoHelper.EnviarCorreoArchivoAdjunto(modificardocumento.texto, Correo.Gmail, bytes, "AhorroVista" + ident + ".pdf", "Ahorro Vista", pUsuario.empresa); //Se adjunta en la clase Correo Helper
            return exitoso;
        }
        catch (Exception ex)
        {
            VerError("Error en el envio del mensaje" + ex.Message);
            return false;
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
