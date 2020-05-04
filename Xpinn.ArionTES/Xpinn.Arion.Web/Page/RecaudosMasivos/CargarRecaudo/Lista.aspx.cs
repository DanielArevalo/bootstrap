using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    RecaudosMasivos _entityRecaudosMasivos = new RecaudosMasivos();
    RecaudosMasivosService _servicerecaudos = new RecaudosMasivosService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_servicerecaudos.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_servicerecaudos.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pErrores.Visible = false;
                btnExportar.Visible = false;
                CargarEmpresaRecaudo();
                ddlEstructura.Enabled = false;
                mvAplicar.ActiveViewIndex = 0;
                btnCargarRecaudos.Visible = true;
                btnGuardar.Visible = false;
                lblFechaAplica.Visible = true;
                lblNumeroLista.Visible = false;
                txtNumeroLista.Visible = false;
                ucFecha.Visible = true;
                ucFecha.ToDateTime = DateTime.Now;
                CargarValoresConsulta(pConsulta, _servicerecaudos.GetType().Name);
                if (Session[_servicerecaudos.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
            msg.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_servicerecaudos.GetType().Name + "L", "Page_Load", ex);
        }

    }


    #endregion


    #region Eventos Grilla


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
            BOexcepcion.Throw(_servicerecaudos.CodigoPrograma + "L", "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(_servicerecaudos.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvMovGeneral_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (Session["Recaudos"] != null)
        {
            gvMovGeneral.PageIndex = e.NewPageIndex;
            gvMovGeneral.DataSource = Session["Recaudos"] as List<RecaudosMasivos>;
            gvMovGeneral.DataBind();
        }
    }


    #endregion


    #region Eventos Botonera


    protected void btnCargarRecaudos_Click(object sender, EventArgs e)
    {
        VerError("");
        List<RecaudosMasivos> lstRecaudos = new List<RecaudosMasivos>();
        string error = "";
        Int64 entidad = Int64.MinValue;
        Int64 estructura = Int64.MinValue;
        Int64? numeronovedad = null;

        try
        {
            if (FileUploadMetas.HasFile)
            {
                Stream stream = FileUploadMetas.FileContent;
                entidad = Convert.ToInt64(ddlEntidad.SelectedValue);
                estructura = Convert.ToInt64(ddlEstructura.SelectedValue);

                DateTime fecha_aplicacion = DateTime.Now;
                if (ddlNovedad.Enabled && ddlNovedad.Visible)
                {
                    if (ddlNovedad.SelectedItem != null)
                    {
                        try
                        {
                            fecha_aplicacion = DateTime.ParseExact(DeterminarPeriodo(), gFormatoFecha, null);
                        }
                        catch
                        {
                            VerError("Período de novedad incorrecto");
                        }
                        try
                        {
                            numeronovedad = Convert.ToInt64(ddlNovedad.SelectedItem.Value);
                        }
                        catch
                        {
                            VerError("Período de novedad incorrecto");
                        }
                    }
                    else
                    {
                        VerError("Debe seleccionar el período de novedad");
                        return;
                    }
                }
                else if (ucFecha.Enabled && ucFecha.Visible && !string.IsNullOrWhiteSpace(ucFecha.Text))
                {
                    try
                    {
                        fecha_aplicacion = DateTime.ParseExact(ucFecha.Text, gFormatoFecha, null);
                    }
                    catch (Exception)
                    {

                    }
                }

                if (ddlEntidad.SelectedItem == null || string.IsNullOrWhiteSpace(ddlEntidad.SelectedValue))
                {
                    VerError("Empresa seleccionada invalida!.");
                }

                EmpresaRecaudo empresa = new EmpresaRecaudo
                {
                    cod_empresa = Convert.ToInt64(ddlEntidad.SelectedItem.Value),
                    tipo_recaudo = 1, // Pagadurias o Nomina
                    periodo_novedad = fecha_aplicacion,
                    tipo_lista = DeterminarTipoLista()
                };

                EmpresaRecaudoServices empresaServicio = new EmpresaRecaudoServices();
                bool yaExiste = empresaServicio.VerificarQueYaNoSeHallaCargadoLaMismaNovedad(empresa, Usuario);
                if (yaExiste)
                {
                    VerError("Ya existe una carga previa generada para esta empresa y periodo de novedad!.");
                    return;
                }

                List<ErroresCarga> plstErrores = new List<ErroresCarga>();
                lstRecaudos = _servicerecaudos.CargarArchivo(entidad, estructura, fecha_aplicacion, stream, numeronovedad, ref error, ref plstErrores, (Usuario)Session["usuario"]);
                Session["Recaudos"] = lstRecaudos;
                if (error.Trim() != "")
                {
                    VerError(error);
                    return;
                }
                if (plstErrores.Count() > 0)
                {
                    pErrores.Visible = true;
                    gvErrores.DataSource = plstErrores;
                    gvErrores.DataBind();
                    cpeDemo.CollapsedText = "(Click Aqui para ver " + plstErrores.Count() + " errores...)";
                    cpeDemo.ExpandedText = "(Click Aqui para ocultar listado de errores...)";
                }

                BindearListaRecaudos(lstRecaudos);
            }
        }
        catch (Exception ex)
        {
            // throw new Exception(ex.ToString());
            VerError("Error al cargar el archivo. Error: " + ex.Message + ". favor revisar la estructura del archivo");
        }

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

    protected void btnAplicar_Click(object sender, EventArgs e)
    {
        if (txtNumeroLista.Text.Contains("No.Recaudo"))
        {
            int posini = txtNumeroLista.Text.IndexOf("No.Recaudo") + 11;
            int posfin = txtNumeroLista.Text.IndexOf("Ofi") - 1;
            string numeroLista = txtNumeroLista.Text.Substring(posini, posfin - posini + 1);
            Session[_servicerecaudos.CodigoProgramaAplicacion + ".id"] = numeroLista;
        }
        else
        {
            Session[_servicerecaudos.CodigoProgramaAplicacion + ".id"] = txtNumeroLista.Text;
        }
        Navegar("../../RecaudosMasivos/RecaudoMasivo/Nuevo.aspx");
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        VerError("");
        string Error = "";
        RecaudosMasivos entidad = new RecaudosMasivos();
        List<RecaudosMasivos> lstRecaudos = new List<RecaudosMasivos>();
        lstRecaudos.Clear();

        if (Session["Recaudos"] == null)
        {
            VerError("No se ha realizado la carga de recaudos desde el archivo");
            return;
        }

        lstRecaudos = (List<RecaudosMasivos>)Session["Recaudos"];

        // Determinar la empresa y tipo de recaudo
        EmpresaRecaudoServices empresaServicio = new EmpresaRecaudoServices();
        EmpresaRecaudo empresa = new EmpresaRecaudo();
        empresa.cod_empresa = Convert.ToInt64(ddlEntidad.SelectedItem.Value);
        empresa = empresaServicio.ConsultarEMPRESARECAUDO(empresa, (Usuario)Session["usuario"]);
        if (empresa == null)
        {
            VerError("No se pudo determinar datos de la empresa");
            return;
        }
        // Determinar datos del recaudo
        entidad.numero_recaudo = 0;
        entidad.tipo_recaudo = Convert.ToInt64(empresa.tipo_recaudo);
        if (ucFecha.ToDateTime == null)
            ucFecha.Text = System.DateTime.Now.ToString(gFormatoFecha);
        entidad.fecha_recaudo = ucFecha.ToDateTime;
        entidad.fecha_aplicacion = ucFecha.ToDateTime;
        entidad.estado = "1";
        entidad.cod_empresa = Convert.ToInt32(ddlEntidad.SelectedValue);
        if (ddlNovedad.Visible)
        {
            if (ddlNovedad.SelectedItem != null)
            {
                try
                {
                    entidad.periodo_corte = DateTime.ParseExact(DeterminarPeriodo(), gFormatoFecha, null);
                }
                catch
                {
                    VerError("Período de novedad incorrecto");
                }
                try
                {
                    entidad.numero_novedad = Convert.ToInt64(ddlNovedad.SelectedValue);
                }
                catch
                {
                    VerError("Período de novedad incorrecto");
                }
            }
            else
            {
                VerError("Debe seleccionar el período de novedad");
                return;
            }
        }
        if (entidad.periodo_corte == null)
            entidad.periodo_corte = System.DateTime.Now;
        // Aplicando las reclamaciones
        String numero_recaudo = String.Empty;
        numero_recaudo = _servicerecaudos.GuardarRecaudos(entidad, ucFecha.ToDateTime, lstRecaudos, (Usuario)Session["usuario"], ref Error);
        if (Error.Trim() == "" && numero_recaudo != null)
        {
            mvAplicar.ActiveViewIndex = 1;
            txtNumeroLista.Text = Convert.ToString(numero_recaudo);
            lblMensajeGrabar.Text = "Recaudos Cargados Correctamente. Recaudos No." + numero_recaudo;
        }
        else
        {
            VerError(Error);
        }

    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
        mvAplicar.ActiveViewIndex = 0;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page pagina = new Page();
        dynamic form = new HtmlForm();

        gvMovGeneral.AllowPaging = false;
        gvMovGeneral.DataSource = Session["Recaudos"];
        gvMovGeneral.DataBind();
        gvMovGeneral.EnableViewState = false;
        pagina.EnableEventValidation = false;
        pagina.DesignerInitialize();
        pagina.Controls.Add(form);
        form.Controls.Add(gvMovGeneral);
        pagina.RenderControl(htw);
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=CargaRecaudos.xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    protected string DeterminarPeriodo()
    {
        string sPeriodo = "";
        string[] sdatos = ddlNovedad.SelectedItem.Text.Split(' ');
        if (sdatos.Count() == 1)
            sPeriodo = sdatos[0];
        else if (sdatos.Count() > 1)
            sPeriodo = sdatos[0];
        else
            sPeriodo = ddlNovedad.SelectedItem.Text;
        return sPeriodo;
    }

    protected int? DeterminarTipoLista()
    {
        if (ddlNovedad.SelectedItem == null)
            return null;
        string[] sdatos = ddlNovedad.SelectedItem.Text.Split(' ');
        if (sdatos.Count() == 1)
        { 
            return null;
        }
        else if (sdatos.Count() > 1)
        {
            string sTipoLista = "";
            for (int i=1;i<sdatos.Count(); i++)
                sTipoLista += (i == 1 ? "" : " ") + sdatos[i];
            EmpresaNovedadService novedadServicio = new EmpresaNovedadService();
            return novedadServicio.ConsultarTipoLista(sTipoLista, (Usuario)Session["Usuario"]);
        }
        return null;
    }

    protected void btnGenerarAPartirNovedades_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(ddlEntidad.SelectedValue) && ddlNovedad.SelectedItem != null && !string.IsNullOrWhiteSpace(ddlNovedad.SelectedItem.Text))
        {            
            try
            {
                List<RecaudosMasivos> lstRecaudos = _servicerecaudos.ListarDetalleRecaudoDeUnPeriodoYEmpresa(ddlEntidad.SelectedValue, DeterminarPeriodo(), Usuario);
                Session["Recaudos"] = lstRecaudos;
                BindearListaRecaudos(lstRecaudos);
            }
            catch (Exception ex)
            {
                Session["Recaudos"] = "";
                VerError("No se pudo terminar periodos de novedades. Error:" + ex.Message);
            }            
           
        }
        else
        {
            VerError("Debes seleccionar la empresa recaudadora y el periodo de la novedad!.");
        }
    }

    #endregion


    #region Eventos DropDownList


    protected void ddlEntidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlEstructura.Enabled = false;
        ddlNovedad.Items.Clear();
        if (ddlEntidad.SelectedItem != null)
        {
            if (ddlEntidad.SelectedItem.Value != null)
            {
                if (ddlEntidad.SelectedItem.Value != "")
                {
                    // Cargar las estructuras
                    try
                    {
                        EstructuraRecaudoServices estructuraServicio = new EstructuraRecaudoServices();
                        List<Estructura_Carga> lstEstructura = new List<Estructura_Carga>();
                        Estructura_Carga estructura = new Estructura_Carga();
                        lstEstructura = estructuraServicio.ListarEstructuraRecaudo(estructura, (Usuario)Session["usuario"], " Where estructura_carga.cod_estructura_carga In (Select cod_estructura_carga From empresa_estructura_carga Where cod_empresa = " + ddlEntidad.SelectedItem.Value + " ) ", 0);
                        ddlEstructura.DataSource = lstEstructura;
                        ddlEstructura.DataTextField = "descripcion";
                        ddlEstructura.DataValueField = "cod_estructura_carga";
                        ddlEstructura.DataBind();
                        ddlEstructura.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        BOexcepcion.Throw(_servicerecaudos.CodigoPrograma, "CargarEmpresaRecaudo", ex);
                    }
                    // Cargar novedades generadas                    
                    EmpresaNovedadService novedadServicio = new EmpresaNovedadService();
                    EmpresaNovedad pEmpresa = new EmpresaNovedad();
                    pEmpresa.cod_empresa = Convert.ToInt64(ddlEntidad.SelectedItem.Value);
                    pEmpresa.estado = "1";
                    ddlNovedad.DataSource = novedadServicio.ListarRecaudo(pEmpresa, " 6 desc", (Usuario)Session["usuario"]);
                    ddlNovedad.DataTextField = "concepto"; //"periodo_corte";
                    ddlNovedad.DataTextFormatString = "{0:d}";
                    ddlNovedad.DataValueField = "numero_novedad";
                    ddlNovedad.DataBind();
                    // Según el tipo de recaudo activar la fecha
                    EmpresaRecaudoServices empresaServicio = new EmpresaRecaudoServices();
                    EmpresaRecaudo empresa = new EmpresaRecaudo();
                    empresa.cod_empresa = Convert.ToInt64(ddlEntidad.SelectedItem.Value);
                    empresa = empresaServicio.ConsultarEMPRESARECAUDO(empresa, (Usuario)Session["usuario"]);
                    if (empresa != null)
                    {
                        if (empresa.tipo_recaudo == 1 || ddlNovedad.Items.Count <= 0)
                        {
                            lblFechaAplica.Text = "Período de Novedades ";
                            ucFecha.Visible = false;
                            ddlNovedad.Visible = true;
                        }
                        if (empresa.tipo_recaudo == 0)
                        {
                            lblFechaAplica.Text = "Fecha de Aplicación";
                            ucFecha.Visible = true;
                            ddlNovedad.Visible = false;
                        }
                    }
                }
            }
        }
    }


    #endregion


    #region Eventos CheckBox


    protected void chkPermitePaginar_CheckedChanged(object sender, EventArgs e)
    {
        if (Session["Recaudos"] != null)
        {
            gvMovGeneral.AllowPaging = chkPermitePaginar.Checked;
            gvMovGeneral.DataSource = Session["Recaudos"];
            gvMovGeneral.DataBind();
        }
    }


    #endregion


    #region Metodos Ayuda


    void Actualizar()
    {
        try
        {
            Session.Add(_servicerecaudos.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_servicerecaudos.GetType().Name + "L", "Actualizar", ex);
        }
    }

    void CargarEmpresaRecaudo()
    {
        try
        {
            Xpinn.Tesoreria.Services.RecaudosMasivosService recaudoServicio = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstModulo = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
            EmpresaRecaudo pempresa = new EmpresaRecaudo();
            pempresa.estado = 1;
            lstModulo = recaudoServicio.ListarEmpresaRecaudo(null, (Usuario)Session["usuario"]).OrderBy(x => x.nom_empresa).ToList();
            ddlEntidad.DataSource = lstModulo;
            ddlEntidad.DataTextField = "nom_empresa";
            ddlEntidad.DataValueField = "cod_empresa";
            ddlEntidad.DataBind();

            ddlEntidad.Items.Insert(0, "");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_servicerecaudos.CodigoPrograma, "CargarEmpresaRecaudo", ex);
        }

    }

    void BindearListaRecaudos(List<RecaudosMasivos> lstRecaudos)
    {
        if (lstRecaudos.Count > 0)
        {
            btnExportar.Visible = true;
            Actualizar();
            Label1.Visible = true;
            Label1.Text = "Su Archivo " + FileUploadMetas.FileName + " Se ha Cargado";

            gvMovGeneral.AllowPaging = chkPermitePaginar.Checked;
            gvMovGeneral.DataSource = lstRecaudos;
            gvMovGeneral.DataBind();

            lblTotalRegs.Visible = true;
            lblTotalRegs.Text = "Se cargaron " + lstRecaudos.Count() + " registros. Total: " + lstRecaudos.Sum(o => o.valor).ToString("n2");
            btnCargarRecaudos.Visible = false;
            btnGuardar.Visible = true;
            lblFechaAplica.Visible = true;
            lblNumeroLista.Visible = true;
            txtNumeroLista.Text = "";
            txtNumeroLista.Visible = true;
            ddlNovedad.Enabled = false;
            ucFecha.Enabled = false;
            lblInicial.Visible = false;
            FileUploadMetas.Visible = false;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
        }
        else
        {
            btnExportar.Visible = false;
            Label1.Visible = true;
            Label1.Text = "Archivo No Valido";
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }


    #endregion


}