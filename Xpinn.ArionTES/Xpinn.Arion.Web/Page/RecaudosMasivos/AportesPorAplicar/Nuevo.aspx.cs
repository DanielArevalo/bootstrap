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
            VisualizarOpciones(servicerecaudos.CodigoProgramaAportesPend, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            //ctlMensaje.eventoClick += btnContinuarMen_Click;
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
                if (Session[servicerecaudos.CodigoProgramaAportesPend + ".id"] != null && Session[servicerecaudos.CodigoProgramaAportesPend + ".id"].ToString().Trim() != "")
                {
                    idObjeto = Session[servicerecaudos.CodigoProgramaAportesPend + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                }
            }
            else
            {
                if (Session["Error"] != null && !string.IsNullOrWhiteSpace(Session["Error"].ToString())) 
                {
                    VerError(Session["Error"].ToString());
                    lblError.Text = Session["Error"].ToString();
                    mpeProcesando.Hide();
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
            BOexcepcion.Throw(servicerecaudos.CodigoProgramaAportesPend + "L", "gvLista_RowDeleting", ex);
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


    private string Filtro()
    {
        string pFiltro = string.Empty;
        pFiltro += " AND D.NUMERO_RECAUDO = " + txtNumeroLista.Text.Trim();
        pFiltro += " AND D.ESTADO = 2 ";

        if (string.IsNullOrEmpty(pFiltro.Trim()))
        {
            pFiltro = pFiltro.Substring(4);
            pFiltro = " WHERE " + pFiltro.ToString();
        }

        return pFiltro;
    }

    private void Actualizar()
    {
        try
        {
            List<RecaudosMasivos> lstConsulta = new List<RecaudosMasivos>();
            string pFiltro = Filtro();
            lstConsulta = servicerecaudos.ListarDetalleAportePendientes(pFiltro, (Usuario)Session["Usuario"]);
            lblTotalReg.Visible = true;
            lblTotalReg.Text = "Se encontraron " + lstConsulta.Count() + " registros";
            gvMovGeneral.DataSource = lstConsulta;
            gvMovGeneral.DataBind();
            var Total = lstConsulta.Sum(x => x.valor);
            txtTotal.Text = Total.ToString("N");
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
            BOexcepcion.Throw(servicerecaudos.CodigoProgramaAportesPend, "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        mpeNuevo.Show();
    }

    protected void btnFinal_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        VerError("");
        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(Convert.ToDateTime(ucFechaAplicacion.ToDateTime), 132) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 132 = Aplicación de Aportes Pendientes");
            return;
        }        
        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!panelProceso.Visible && panelGeneral.Visible)
        {
            rpta = ctlproceso.Inicializar(132, Convert.ToDateTime(ucFechaAplicacion.ToDateTime), (Usuario)Session["Usuario"]);
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
                //if (AplicarRecaudo())
                    //Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                //else
                      //VerError("Se presento error");
            }
        }        
    }

    //protected bool AplicarRecaudo()
    protected void AplicarRecaudo()
    {
        // Determina variables requeridas para el proceso
        Usuario usuario = Usuario;
        Configuracion conf = new Configuracion();
        string Error = "";
        Int64 numero_reclamacion = Convert.ToInt64(txtNumeroLista.Text);

        // Cargando las reclamaciones en una lista
        List<RecaudosMasivos> lstReclamaciones = new List<RecaudosMasivos>();
        lstReclamaciones.Clear();
        foreach (GridViewRow row in gvMovGeneral.Rows)
        {
            RecaudosMasivos eRecaudos = new RecaudosMasivos();
            eRecaudos.fecha_aplicacion = ucFechaAplicacion.ToDateTime;
            eRecaudos.iddetalle = Convert.ToInt64(row.Cells[0].Text);
            eRecaudos.cod_cliente = Convert.ToInt64(row.Cells[1].Text);
            eRecaudos.identificacion = Convert.ToString(row.Cells[2].Text);
            eRecaudos.tipo_producto = Convert.ToString(row.Cells[4].Text);
            eRecaudos.numero_producto = Convert.ToString(row.Cells[5].Text);           
            eRecaudos.tipo_aplicacion = "Por Valor a Capital";
            DropDownListGrid ddlTipoAplicacion = (DropDownListGrid)row.Cells[6].FindControl("ddlTipoAplicacion");
            if (ddlTipoAplicacion != null)
                eRecaudos.tipo_aplicacion = ddlTipoAplicacion.SelectedValue;
            eRecaudos.num_cuotas = Convert.ToInt64(row.Cells[7].Text);
            eRecaudos.valor = Convert.ToDecimal(gvMovGeneral.DataKeys[row.RowIndex].Values[1].ToString());
            eRecaudos.sobrante = 0;
            eRecaudos.tipo_tran = 101;
            eRecaudos.cod_ope = Convert.ToInt64(gvMovGeneral.DataKeys[row.RowIndex].Values[0].ToString());
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
                servicerecaudos.ProcesoAplicarPago(numero_reclamacion, ucFechaAplicacion.ToDateTime, lstReclamaciones, false, usuario, ref Error, ref CodOpe, 132);
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
                    ctlproceso.CargarVariables(CodOpe, 132, usuario.codusuario, usuario);
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

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
        mvAplicar.ActiveViewIndex = 0;
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

    
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvMovGeneral.Rows.Count > 0)
            {
                //                string style;
                //                style = @"<style> 
                //                    .gridHeader { background-color: #359af2; font-weight: bold; color: White; border: 1px solid #d7e6e9; text-align: center; } 
                //                    .gridItem   { border: 1px solid #d7e6e9; mso-number-format:\@; }  
                //                  </style>";
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
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            //AplicarRecaudo();
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
        Session.Remove("Proceso");
        Timer1.Enabled = false;
        // La variable de session ->Error<- determina si el proceso se ejecuto satisfactoriamente o no.
        if (Session["Error"] != null)
        {
            if (Session["Error"].ToString().Trim() != "")
            {
                return;
            }
            Session.Remove("Error");
            // Procede a generar el comprobante
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
        }
        //Forzar el postback para que actualize los links creados
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
    }


    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (Session["Proceso"] != null)
        {
            if (Session["Proceso"].ToString() == "FINAL")
            {
                TerminarProceso();
                mpeProcesando.Hide();
            }
            else
                mpeProcesando.Show();
            //lblAvance.Text = servicerecaudos.RegistrosAplicados(Convert.ToInt64(txtNumeroLista.Text), (Usuario)Session["Usuario"]).ToString("n");
        }
        else
        {
            mpeProcesando.Hide();
        }
    }

    #endregion 

}
