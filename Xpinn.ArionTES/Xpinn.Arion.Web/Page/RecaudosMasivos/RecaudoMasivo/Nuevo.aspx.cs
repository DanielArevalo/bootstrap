using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Web.Services;
using System.Web.Script.Services;
using System.Threading.Tasks;

[ScriptService]
public partial class Nuevo : GlobalWeb
{
    Usuario usuario = new Usuario();
    RecaudosMasivos entityRecaudosMasivos = new RecaudosMasivos();
    RecaudosMasivosService servicerecaudos = new RecaudosMasivosService();
    Thread tareaEjecucion;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(servicerecaudos.CodigoProgramaAplicacion, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            ctlMensaje.eventoClick += btnContinuar_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicerecaudos.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Timer1.Enabled = false;
                CargarEmpresaRecaudo();
                mvAplicar.ActiveViewIndex = 0;
                if (Session[servicerecaudos.CodigoProgramaAplicacion + ".id"] != null && Session[servicerecaudos.CodigoProgramaAplicacion + ".id"].ToString().Trim() != "")
                {
                    idObjeto = Session[servicerecaudos.CodigoProgramaAplicacion + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }
            }
            else
            {
                if (Session["hiddenValueRecaudo"] != null)
                {
                    hiddenValor.Value = Session["hiddenValueRecaudo"].ToString();
                }
            }

            if (Session["Error"] != null && !string.IsNullOrWhiteSpace(Session["Error"].ToString()))
            {
                string error = "Error: " + Session["Error"].ToString();
                lblError.Text = error;
                VerError(error);

                if (Session["Proceso"] == null)
                {
                    Session.Remove("Error");
                }
            }

            msg.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicerecaudos.GetType().Name + "L", "Page_Load", ex);
        }

    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicerecaudos.CodigoProgramaAplicacion + "L", "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        if (evt.CommandName == "Editar")
        {
            String[] tmp = evt.CommandArgument.ToString().Split('|');
            RecaudosMasivos ejeMeta = new RecaudosMasivos();
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicerecaudos.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<RecaudosMasivos> lstConsulta = new List<RecaudosMasivos>();
            lstConsulta = servicerecaudos.ListarDetalleRecaudo(Convert.ToInt32(txtNumeroLista.Text), (Usuario)Session["Usuario"]);
            lblTotalReg.Visible = true;
            lblTotalReg.Text = "Se encontraron " + lstConsulta.Count() + " registros";
            hiddenValor.Value = lstConsulta.Count.ToString();
            lblProgreso.Text = "Progreso : 0 / " + hiddenValor.Value;
            Session["hiddenValueRecaudo"] = hiddenValor.Value;
            gvMovGeneral.DataSource = lstConsulta;
            gvMovGeneral.DataBind();
            var Total = lstConsulta.Sum(x => x.valor);
            txtTotal.Text = Total.ToString("N2");
            if (lstConsulta.Count > 0)
            {
                bool rpta = gvMovGeneral.Rows.OfType<GridViewRow>()
                           .Where(x => x.Cells[4].Text.ToUpper().Contains("APORTE", StringComparison.OrdinalIgnoreCase))
                           .Any();
                chkPorAplicar.Visible = rpta;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicerecaudos.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            RecaudosMasivos vRecaudos = new RecaudosMasivos();
            vRecaudos = servicerecaudos.ConsultarRecaudo(pIdObjeto, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vRecaudos.numero_recaudo.ToString()))
                txtNumeroLista.Text = HttpUtility.HtmlDecode(vRecaudos.numero_recaudo.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.fecha_aplicacion.ToString()))
                ucFechaAplicacion.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vRecaudos.fecha_aplicacion.ToString()));
            if (!string.IsNullOrEmpty(vRecaudos.cod_empresa.ToString()))
                ddlEntidad.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.cod_empresa.ToString());

            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicerecaudos.CodigoProgramaAplicacion, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session.Remove("Error");
        Navegar(Pagina.Lista);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        //mpeNuevo.Show();
        if (Page.IsValid)
            ctlMensaje.MostrarMensaje("Esta Seguro de Realizar la Aplicación de los Recaudos");
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        VerError("");
        lblProgreso.Text = "Progreso : 0 / " + hiddenValor.Value;
        lblError.Text = string.Empty;
        Session.Remove("Error");
        //mpeNuevo.Hide();
        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(Convert.ToDateTime(ucFechaAplicacion.ToDateTime), 119) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 119 = Aplicación de Recaudos Masivos");
            return;
        }
        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!panelProceso.Visible && panelGeneral.Visible)
        {
            rpta = ctlproceso.Inicializar(119, Convert.ToDateTime(ucFechaAplicacion.ToDateTime), (Usuario)Session["Usuario"]);
            if (rpta > 1)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarCancelar(false);
                toolBar.MostrarConsultar(false);
                panelGeneral.Visible = false;
                panelProceso.Visible = true;
            }
            else
            {
                // Crear la tarea de ejecución del proceso
                IniciarProceso();
                tareaEjecucion = new Thread(new ThreadStart(AplicarRecaudo));
                tareaEjecucion.Start();
            }
        }
    }

    //protected bool AplicarRecaudo()
    protected void AplicarRecaudo()
    {
        try
        {
            // Determina variables requeridas para el proceso
            Usuario usuario = Usuario;
            Configuracion conf = new Configuracion();
            string Error = "";
            Int64 numero_reclamacion = Convert.ToInt64(txtNumeroLista.Text);

            EmpresaRecaudo pEmpresa = new EmpresaRecaudo();
            EmpresaRecaudoServices EmpresaServicio = new EmpresaRecaudoServices();
            pEmpresa = EmpresaServicio.ConsultarEMPRESARECAUDO(Convert.ToInt64(ddlEntidad.SelectedValue),(Usuario)Session["usuario"]);


            // Cargando las reclamaciones en una lista
            List<RecaudosMasivos> lstReclamaciones = new List<RecaudosMasivos>();
            lstReclamaciones.Clear();
            foreach (GridViewRow row in gvMovGeneral.Rows)
            {
                RecaudosMasivos eRecaudos = new RecaudosMasivos();
                eRecaudos.fecha_aplicacion = ucFechaAplicacion.ToDateTime;

                long iddetalle = 0;
                long.TryParse(row.Cells[0].Text, out iddetalle);
                eRecaudos.iddetalle = iddetalle;

                long cod_cliente = 0;
                long.TryParse(row.Cells[1].Text, out cod_cliente);
                eRecaudos.cod_cliente = cod_cliente;

                eRecaudos.identificacion = row.Cells[2].Text;
                eRecaudos.tipo_producto = row.Cells[4].Text;
                eRecaudos.numero_producto = row.Cells[5].Text;
                eRecaudos.tipo_aplicacion = "Por Valor a Capital";
                DropDownListGrid ddlTipoAplicacion = row.Cells[6].FindControl("ddlTipoAplicacion") as DropDownListGrid;
                if (ddlTipoAplicacion != null)
                    eRecaudos.tipo_aplicacion = ddlTipoAplicacion.SelectedValue;

                long num_cuotas = 0;
                long.TryParse(row.Cells[7].Text, out num_cuotas);
                eRecaudos.num_cuotas = num_cuotas;

                decimal valor = 0;
                decimal.TryParse(row.Cells[8].Text.Replace(conf.ObtenerSeparadorMilesConfig(), ""), out valor);
                eRecaudos.valor = valor;
                eRecaudos.numero_recaudo = Convert.ToInt64(numero_reclamacion);
                eRecaudos.sobrante = 0;
                lstReclamaciones.Add(eRecaudos);
            }

            // Validando los descuentos
            if (usuario.tipo == 1)
                servicerecaudos.Validar(ucFechaAplicacion.ToDateTime, lstReclamaciones, usuario, ref Error);

            // Si se valido correctamente continuar con la ejecución
            if (Error.Trim() == "")
            {
                // Aplicando los descuentos
                Int64 CodOpe = 0;
                try
                {
                    Boolean AportesPendiente = chkPorAplicar.Visible == false ? false : chkPorAplicar.Checked;
                    string formaAplicacion = ConfigurationManager.AppSettings["FormaAplicacionRecaudos"];
                    Session["numeroAplicacionRecaudo"] = numero_reclamacion;

                    if (formaAplicacion.Trim() == "1")
                        servicerecaudos.AplicarRecaudo(numero_reclamacion, ucFechaAplicacion.ToDateTime, AportesPendiente, ref CodOpe, ref Error, usuario);
                    else
                        servicerecaudos.ProcesoAplicarPago(numero_reclamacion, ucFechaAplicacion.ToDateTime, lstReclamaciones, AportesPendiente, usuario, ref Error, ref CodOpe);
                }
                catch (Exception ex)
                {
                    Error += ex.Message;
                    Session["Error"] = "Error01:" + Error;
                }
                if (Error.Trim() == "")
                {
                    // Carga variables para que se pueda generar el comprobante
                    try
                    {
                        ctlproceso.CargarVariables(CodOpe, 119, pEmpresa.cod_persona, usuario);
                        Session["Error"] = Error;
                    }
                    catch (Exception ex)
                    {
                        Session["Error"] = "Error02:" + Error + " " + ex.Message + " Cod.Ope:" + CodOpe;
                    }
                }
                else
                {
                    Session["Error"] = "Error03:" + Error;
                }
            }
            else
            {
                Session["Error"] = "Error04:" + Error;
            }
            Session["Proceso"] = "FINAL";
        }
        catch (Exception ex)
        {
            Session["Error"] = "Error05:" + ex.Message;
        }
        finally
        {
            Session["Proceso"] = "FINAL";
        }
    }

    protected void gvMovGeneral_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlTipoAplicacion = (DropDownListGrid)e.Row.Cells[4].FindControl("ddlTipoAplicacion");
            if (ddlTipoAplicacion != null)
            {
                if (e.Row.Cells[2].Text == "Creditos")
                    ddlTipoAplicacion.Visible = true;
                else
                    ddlTipoAplicacion.Visible = false;
            }
        }
    }


    protected void CargarEmpresaRecaudo()
    {
        try
        {
            Xpinn.Tesoreria.Services.RecaudosMasivosService recaudoServicio = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstModulo = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();

            lstModulo = recaudoServicio.ListarEmpresaRecaudo(null, (Usuario)Session["usuario"]);

            ddlEntidad.DataSource = lstModulo;
            ddlEntidad.DataTextField = "nom_empresa";
            ddlEntidad.DataValueField = "cod_empresa";
            ddlEntidad.DataBind();

            ddlEntidad.Items.Insert(0, "");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(servicerecaudos.CodigoPrograma, "CargarEmpresaRecaudo", ex);
        }
    }

    protected void ddlEntidad_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvMovGeneral.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvMovGeneral.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvMovGeneral);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=RecaudosMasivos.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }
    }

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            lblProgreso.Text = "Progreso : 0 / " + hiddenValor.Value;
            lblError.Text = string.Empty;
            Session.Remove("Error");
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            IniciarProceso();
            tareaEjecucion = new Thread(new ThreadStart(AplicarRecaudo));
            tareaEjecucion.Start();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    #region Métodos para Control de la Tarea


    public void IniciarProceso()
    {
        mpeProcesando.Show();
        Image1.Visible = true;
        // Variable de sessión que permite controlar cuando el proceso termina
        Session["Proceso"] = "INICIO";
        Timer1.Enabled = true;
    }

    /// <summary>
    /// Este método permite controlar cuando la tarea se termine. Es ejecutado por el timer.
    /// </summary>
    public void TerminarProceso()
    {
        mpeProcesando.Hide();
        Image1.Visible = false;
        Timer1.Enabled = false;
        Session.Remove("Proceso");
        // La variable de session ->Error<- determina si el proceso se ejecuto satisfactoriamente o no.
        if (Session["Error"] != null)
        {
            if (Session["Error"].ToString().Trim() != "")
            {
                Navegar(Pagina.Nuevo);
                return;
            }
            Session.Remove("Error");
            // Procede a generar el comprobante
            ctlproceso.GenerarComprobante();
            if (ctlproceso.tipo_comp != null)
                if (ctlproceso.tipo_comp > 0)
                    ctlproceso.CargarVariables(ctlproceso.tipo_comp);
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");            
        }
        //Forzar el postback para que actualize los links creados
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
    }


    protected void Timer1_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Session["Proceso"] != null)
            {
                if (Session["Proceso"].ToString() == "FINAL")
                {
                    TerminarProceso();
                    mpeProcesando.Hide();
                    Timer1.Enabled = false;
                    RegistrarPostBack();
                }
                else
                {
                    mpeProcesando.Show();

                    int targetValue = Convert.ToInt32(Session["hiddenValueRecaudo"].ToString());
                    long numeroDeRecaudosQueVanAplicarse = (long)Session["numeroAplicacionRecaudo"];
                    int numeroDeRecaudosAplicados = servicerecaudos.ConsultarProgresoRecaudos(numeroDeRecaudosQueVanAplicarse, Usuario);

                    if (Session["Error"] == null || string.IsNullOrWhiteSpace(Session["Error"].ToString()))
                    {
                        lblProgreso.Text = "Progreso: " + numeroDeRecaudosAplicados + " / " + targetValue;
                    }
                    else
                    {
                        string error = "Error: " + Session["Error"].ToString();
                        lblProgreso.Text = error;
                        mpeProcesando.Hide();
                        Timer1.Enabled = false;
                    }
                }
            }
            else
            {
                if (Session["Error"] != null && !string.IsNullOrWhiteSpace(Session["Error"].ToString()))
                {
                    string error = "Error: " + Session["Error"].ToString();
                    lblProgreso.Text = error;
                    mpeProcesando.Hide();
                    Timer1.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblProgreso.Text = "Error: " + ex.Message;
            mpeProcesando.Hide();
            Timer1.Enabled = false;
            RegistrarPostBack();
        }
    }


    #endregion 

}
