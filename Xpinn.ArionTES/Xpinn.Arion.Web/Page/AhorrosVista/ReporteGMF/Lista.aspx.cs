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
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    decimal subtotalgmf = 0;
    decimal subtotalbase = 0;
    private Xpinn.Ahorros.Services.AhorroVistaServices AhorroVistaServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    private Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
    PoblarListas lista = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AhorroVistaServicio.CodigoProgramaReporteGMF, "L");

            Site toolBar = (Site)this.Master;

            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaReporteGMF, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                btnExportar.Visible = false;
                ctlGiro.Visible = false;
                lblVrTotal.Visible = false;
                txtTotal.Visible = false;
                fechainicial.ToDateTime = DateTime.Now;
                fechafinal.ToDateTime = DateTime.Now.AddDays(7);
                cargar();
                CargarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaReporteGMF);
                if (Session[AhorroVistaServicio.CodigoProgramaReporteGMF + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaReporteGMF, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        Xpinn.Tesoreria.Services.OperacionServices xTesoreria = new Xpinn.Tesoreria.Services.OperacionServices();
        Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();

        try
        {
            ///carga todo a una entodad vAhorroVista en AhorroVista
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];
            Xpinn.Ahorros.Entities.ParametroGMF vAhorroVista = new Xpinn.Ahorros.Entities.ParametroGMF();
            Xpinn.Ahorros.Services.ParametroGMFService parametroservice = new Xpinn.Ahorros.Services.ParametroGMFService();

            DateTime fechainicial = Convert.ToDateTime(this.fechainicial.Texto);
            DateTime fechafinal = Convert.ToDateTime(this.fechafinal.Texto);
            int resultado = 0;

            //GRABACION DE LA OPERACION
            vOpe.cod_ope = 0;
            vOpe.tipo_ope = 126;
            vOpe.cod_caja = 0;
            vOpe.cod_cajero = 0;
            vOpe.observacion = null;
            vOpe.cod_proceso = null;
            vOpe.fecha_oper = DateTime.Now;
            vOpe.fecha_calc = DateTime.Now;
            var usu = vUsuario.codusuario;
            vOpe = xTesoreria.GrabarOperacion(vOpe, vUsuario);
            if (vOpe == null)
            {
                VerError("Se presento error al grabar la operación");
                return;
            }
            if (vOpe.cod_ope == 0)
            {
                VerError("Se presento error al generar la operación");
                return;
            }


            // ACTUALIZAR DATOS DEL ESTADO DE LOS GIROS
            vAhorroVista.operacion = vOpe.cod_ope.ToString();
            resultado = parametroservice.ModificarEstadoTranGmf(vAhorroVista, (Usuario)Session["usuario"], fechainicial, fechafinal);
            if (resultado != 0)
            {
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();

                // Valida que exista parametrización contable para la operación        
                List<Xpinn.Contabilidad.Entities.ProcesoContable> LstProcesoContable;
                LstProcesoContable = ComprobanteServicio.ConsultaProceso(0, 126, DateTime.Now, vUsuario);
                if (LstProcesoContable.Count() == 0)
                {
                    VerError("No existen comprobantes parametrizados para esta operación (Tipo 126=Devolución Gmf)");
                    return;
                }
                Int64 pcod_proceso = Convert.ToInt64(LstProcesoContable[0].cod_proceso);
            }

            //GRABACION DEL GIRO A REALIZAR
            Xpinn.FabricaCreditos.Services.AvanceService AvancServices = new Xpinn.FabricaCreditos.Services.AvanceService();
            Usuario pusu = (Usuario)Session["usuario"];
            Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();            
            pGiro = ctlGiro.ObtenerEntidadGiro(7, Convert.ToInt64(pusu.cod_persona), Convert.ToDateTime(DateTime.Now), Convert.ToInt64(this.txtTotal.Text.Replace(".", "")), pusu);
            pGiro.cod_ope = vOpe.cod_ope;
            AvancServices.CrearGiro(pGiro, (Usuario)Session["usuario"], 1);

            //GENERAR EL COMPROBANTE
            if (vOpe.cod_ope != 0)
            {
                //var usu = (Usuario)Session["usuario"];
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicios = new Xpinn.Contabilidad.Services.ComprobanteService();

                Session[ComprobanteServicios.CodigoPrograma + ".cod_ope"] = vOpe.cod_ope;
                Session[ComprobanteServicios.CodigoPrograma + ".tipo_ope"] = 126;
                Session[ComprobanteServicios.CodigoPrograma + ".cod_persona"] = vUsuario.codusuario; ; 
                Session[ComprobanteServicios.CodigoPrograma + ".idgiro"] = pGiro.idgiro.ToString();
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }

               
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }

    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
              
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        ///inicializa en 0 los campos vacios de la grilla

        if (fechainicial.Text == "")
        {
            VerError("Ingrese una fecha inicial");
            return;
        }
        if (fechafinal.Text == "")
        {
            VerError("Ingrese una fecha final");
            return;
        }
        if (fechainicial.ToDateTime >= fechafinal.ToDateTime)
        {
            VerError("La fecha inicial no puede ser menor ni igual a la fecha final");
            return;
        }
        GuardarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaReporteGMF);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        fechainicial.Text = ("");
        lblTotalRegs.Text = ("");
        fechafinal.Text = ("");
        gvLista.Visible = false;
        btnExportar.Visible = false;
        ctlGiro.Visible = false;
        lblVrTotal.Visible = false;
        txtTotal.Visible = false;
        LimpiarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaReporteGMF);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            subtotalgmf += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor_gmf"));
            subtotalbase += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor_base"));
            txtTotal.Text = Convert.ToString(subtotalgmf);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = "Totales:";
            e.Row.Cells[6].Text = subtotalbase.ToString("c");
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].Text = subtotalgmf.ToString("c");
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Font.Bold = true;
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoProgramaReporteGMF + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoProgramaReporteGMF + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = Convert.ToString(e.Keys[0]);
            AhorroVistaServicio.EliminarAhorroVista(id, (Usuario)Session["usuario"]);
            Actualizar();
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

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaReporteGMF, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            ///Guarda en una variable de la entidad ahorrovista

            Xpinn.Ahorros.Entities.AhorroVista ahorro = new Xpinn.Ahorros.Entities.AhorroVista();
            if (ddlOficina.SelectedIndex != 0)
                ahorro.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);
            DateTime pFechaIni, pFechaFin;
            pFechaIni = fechainicial.ToDateTime == null ? DateTime.MinValue : fechainicial.ToDateTime;
            pFechaFin = fechafinal.ToDateTime == null ? DateTime.MinValue : fechafinal.ToDateTime;
            List<Xpinn.Ahorros.Entities.AhorroVista> lstConsulta = new List<Xpinn.Ahorros.Entities.AhorroVista>();

            lstConsulta = AhorroVistaServicio.ReporteGMF(ahorro, pFechaIni, pFechaFin, (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                ///Si la consulta es mayor a 0  rellena la grilla
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTAhorroVista"] = lstConsulta;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
                toolBar.MostrarExportar(true);
                toolBar.MostrarGuardar(true);
                ctlGiro.Visible = true;
                lblVrTotal.Visible = true;
                txtTotal.Visible = true;
                btnExportar.Visible = true;
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                toolBar.MostrarExportar(false);
                toolBar.MostrarGuardar(true);
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros Encontrados " + lstConsulta.Count.ToString();
            }

            Session.Add(AhorroVistaServicio.CodigoProgramaReporteGMF + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaReporteGMF, "Actualizar", ex);
        }
    }

    protected void cargar()
    {
        ctlGiro.Inicializar();
        lista.PoblarListaDesplegable("oficina", " COD_OFICINA,NOMBRE", " estado = 1", "1", ddlOficina, (Usuario)Session["usuario"]);
        ddlOficina.SelectedIndex = 1;

    }

    private Xpinn.Ahorros.Entities.AhorroVista ObtenerValores()
    {
        Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
        if (ddlOficina.Text.Trim() != "")
            vAhorroVista.cod_oficina = Convert.ToInt32(ddlOficina.Text.Trim());
        ///numero de oficina actualizacion

        if (fechafinal.ToDate.Trim() != "")
            vAhorroVista.fecha_cierre = Convert.ToDateTime(fechafinal.ToDate.Trim());
        ///fecha final

        if (fechainicial.ToDate.Trim() != "")
            vAhorroVista.fecha_apertura = Convert.ToDateTime(fechainicial.ToDate.Trim());
        ///fecha inicial

        return vAhorroVista;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTAhorroVista"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                GridView gvExportar = CopiarGridViewParaExportar(gvLista, "DTAhorroVista");
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvExportar);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=LibroAuxiliar.xls");
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


    protected void ExportToExcel(GridView GridView1)
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

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    /* Verifies that the control is rendered */
    //}

}