using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.IO;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;
using System.Text;

public partial class Lista : GlobalWeb
{
    private Xpinn.Aportes.Services.OperacionApoServices operacionServicio = new Xpinn.Aportes.Services.OperacionApoServices();
    private Xpinn.Aportes.Services.RevalorizacionAporteServices RevalorizaciondeAporteServicios = new Xpinn.Aportes.Services.RevalorizacionAporteServices();

    private Configuracion conf = new Configuracion();
    private Usuario user = new Usuario();
    private Int16 nActiva = 0;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(RevalorizaciondeAporteServicios.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            
            toolBar.eventoConsultar += btnConsultar_Click;            
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            toolBar.eventoImportar += btnImportar_Click;            
            toolBar.eventoCargar += btnCargar_Click;
            toolBar.MostrarCargar(false);
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RevalorizaciondeAporteServicios.GetType().Name + "A", "Page_PreInit", ex);
        }
    }
    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
              //  Actualizar();
                // Llenar los DROPDOWNLIST de Lineas de aportes
                LlenarComboLineaAporte(this.DdlLineaAporte);
                txtFechaOperacion.Text = DateTime.Now.ToString(gFormatoFecha);
                pnlPendientes.Visible = false;
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RevalorizaciondeAporteServicios.GetType().Name + "A", "Page_Load", ex);
        }

    }

    /// <summary>
    /// LLenar el combo de  las lineas de aportes
    /// </summary>
    /// <param name="DdlLineaApo"></param>

    protected void LlenarComboLineaAporte(DropDownList DdlLineaApo)
    {

        AporteServices aporteService = new AporteServices();
        Usuario usuap = (Usuario)Session["usuario"];
        Aporte aporte = new Aporte();
        DdlLineaApo.DataSource = aporteService.ListarLineaAporte(aporte, (Usuario)Session["usuario"]);
        DdlLineaApo.DataTextField = "nom_linea_aporte";
        DdlLineaApo.DataValueField = "cod_linea_aporte";
        DdlLineaApo.DataBind();
        DdlLineaApo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }




    /// <summary>
    /// Cancelar y salir de la opción y regresar al menu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        if (mvRevaloriza.ActiveViewIndex == 2)
        {
            mvRevaloriza.ActiveViewIndex = 0;
            Site toolBar = (Site)Master;
            toolBar.MostrarCargar(false);
            toolBar.MostrarImportar(true);
            toolBar.MostrarConsultar(true);
            bool rpta = gvConsultaDatos.Rows.Count > 0 ? true : false;
            toolBar.MostrarGuardar(rpta);
        }
        else
            Navegar("../../../General/Global/inicio.aspx");
    }



    /// <summary>
    /// Mètodo para aplicar las transacciones registradas segùn las formas de pago
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            if (Page.IsValid)
            {
                //Validando si tiene ya realizado una revalorizacion para la fecha seleccionada
                List<RevalorizacionAportes> lstRevalo = new List<RevalorizacionAportes>();
                string pFiltro = string.Empty;
                pFiltro += " WHERE FECHA = to_Date('" + txtFechaRevalorizacion.Text + "','" + gFormatoFecha + "') ";
                lstRevalo = RevalorizaciondeAporteServicios.ListarRevalorizacion(pFiltro, Usuario);
                if (lstRevalo.Count > 0)
                {
                    VerError("No se puede realizar la revalorización debido a que ya existe generado a la fecha [ " + txtFechaRevalorizacion.Text + " ]");
                    return;
                }
                mpeNuevo.Show();
            }
        }
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
    }

    protected void Grabar()
    {
        RevalorizacionAportes revalorizacion = new RevalorizacionAportes();
        RevalorizacionAporteServices RevalorizacionService = new RevalorizacionAporteServices();


        revalorizacion.lstRevalorizacionAportes = new List<RevalorizacionAportes>();
        revalorizacion.lstRevalorizacionAportes = ObtenerListaRevalorizacion();
        revalorizacion.fecharevalorizacion = Convert.ToDateTime(txtFechaRevalorizacion.Text.ToString());
        revalorizacion.fecha = Convert.ToDateTime(txtFechaOperacion.Text.ToString());
        if (ddlTipoDistribucion.SelectedIndex == 0)
        {
            revalorizacion.asretirados = 0;
        }
        else
        {
            revalorizacion.asretirados = ChkAsociRetirados.Checked ? 1 : 0;
        }

        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        string Error = string.Empty;
        Int64 pCod_Ope = 0;

        //Graba la revalorizacion

        revalorizacion = RevalorizacionService.GrabarRevalorizacion(revalorizacion, ref pCod_Ope, ref Error, (Usuario)Session["usuario"]);
        if (!string.IsNullOrEmpty(Error))
        {
            VerError(Error);
            return;
        }
        if (pCod_Ope == 0)
        {
            VerError("Se generó un error al crear la operación de las revalorizaciones.");
            return;
        }
        gvConsultaDatos.Visible = false;
        ConsultarComprobante(pCod_Ope);
        blComprobante.Visible = true;
    }

    protected void ConsultarComprobante(Int64 vCod_Ope)
    {
        String filtro = String.Empty;
        String Fecha = Convert.ToDateTime(txtFechaRevalorizacion.Text).ToLongDateString();
        if (txtFechaRevalorizacion.Text.Trim() != "")
        {
            DateTime FechaRevalorizacion = txtFechaOperacion.ToDateTime;
            filtro += " AND to_CHAR(fecha_oper,'" + conf.ObtenerFormatoFecha() + "')=  '" + FechaRevalorizacion.ToString("" + conf.ObtenerFormatoFecha() + "") + "'";
        }
        filtro += " And a.COD_OPE = " + vCod_Ope;

        RevalorizacionAporteServices RevalorizacionService = new RevalorizacionAporteServices();
        List<RevalorizacionAportes> lstConsulta = new List<RevalorizacionAportes>();
        try
        {
            lstConsulta = RevalorizacionService.ListarDatosComprobante(filtro, (Usuario)Session["usuario"]);

            gvConsultaDatosCom.DataSource = lstConsulta;

            Site toolBar = (Site)Master;
            if (lstConsulta.Count > 0)
            {
                pConsulta.Visible = false;
                gvConsultaDatosCom.DataBind();
                mvRevaloriza.ActiveViewIndex = 1;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarCancelar(false);
                toolBar.MostrarImportar(false);
                toolBar.MostrarConsultar(false);
            }
            else
            {
                pConsulta.Visible = true;
                gvConsultaDatosCom.Visible = false;
            }
            ViewState["RevalorizacionAportesCom"] = lstConsulta;
            Session.Add(RevalorizaciondeAporteServicios.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RevalorizaciondeAporteServicios.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void GenerarComporbante()
    {
        List<Xpinn.Aportes.Entities.OperacionApo> lstConsulta = new List<Xpinn.Aportes.Entities.OperacionApo>();

        foreach (GridViewRow rFila in gvConsultaDatosCom.Rows)
        {
            Xpinn.Aportes.Entities.OperacionApo voperacion = new Xpinn.Aportes.Entities.OperacionApo();
            voperacion.cod_ope = Convert.ToInt64(rFila.Cells[2].Text);
            voperacion.tipo_ope = 19;
            voperacion.cod_ofi = Convert.ToInt64(rFila.Cells[4].Text);
            voperacion.fecha_oper = Convert.ToDateTime(rFila.Cells[5].Text);
            voperacion.cod_cliente = Convert.ToInt64(rFila.Cells[6].Text);

            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = voperacion.cod_ope;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = voperacion.tipo_ope;
            Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = voperacion.fecha_oper;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = -1;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = voperacion.cod_ofi;
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");

        }
    }

    protected List<RevalorizacionAportes> ObtenerListaRevalorizacion()
    {
        List<RevalorizacionAportes> lstRevalorizacionAportes = new List<RevalorizacionAportes>();
        List<RevalorizacionAportes> lista = new List<RevalorizacionAportes>();

        RevalorizacionAportes erevalorizacion;
        foreach (GridViewRow rfila in gvConsultaDatos.Rows)
        {
            erevalorizacion = new RevalorizacionAportes();

            erevalorizacion.codigo = Convert.ToInt64(rfila.Cells[2].Text.Trim());
            erevalorizacion.identificacion = rfila.Cells[3].Text.Trim();
            erevalorizacion.nombres = Convert.ToString(rfila.Cells[4].Text.Trim());
            erevalorizacion.estado = Convert.ToString(rfila.Cells[5].Text.Trim());
            erevalorizacion.num_aporte = Convert.ToInt64(rfila.Cells[6].Text.Trim());
            erevalorizacion.saldo_base = Convert.ToDecimal(gvConsultaDatos.DataKeys[rfila.RowIndex].Values[1].ToString());
            erevalorizacion.valordist = Convert.ToDecimal(gvConsultaDatos.DataKeys[rfila.RowIndex].Values[2].ToString());
            erevalorizacion.retencion = 0;

            lstRevalorizacionAportes.Add(erevalorizacion);
        }
        return lstRevalorizacionAportes;
    }




    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnGuardar"), "Desea Aplicar los Pagos?");
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (Page.IsValid)
        {
            ViewState["Validacion"] = null;
            txtporcdistribuir.Enabled = true;
            if (ValidarDatos())
            {
                Actualizar();
            }
        }
    }


    private RevalorizacionAportes ObtenerValores()
    {
        RevalorizacionAportes RevalorizacionAportes = new RevalorizacionAportes();

        if (DdlLineaAporte.Text.Trim() != "")
            RevalorizacionAportes.lineaaporte = Convert.ToInt64(DdlLineaAporte.SelectedValue);

        if (ddlTipoDistribucion.Text.Trim() != "")
            RevalorizacionAportes.tipodistrib = Convert.ToInt64(ddlTipoDistribucion.SelectedValue);

        RevalorizacionAportes.asretirados = ChkAsociRetirados.Checked ? 1 : 0;
        RevalorizacionAportes.pordist = txtporcdistribuir.Text.Trim() != "" ? Convert.ToDecimal(txtporcdistribuir.Text) : 0;

        if (txtFechaRevalorizacion.Text.Trim() != "")
        {
            RevalorizacionAportes.fecharevalorizacion = Convert.ToDateTime(txtFechaRevalorizacion.Text);
            RevalorizacionAportes.fecha = Convert.ToDateTime(txtFechaRevalorizacion.Text);
        }

        return RevalorizacionAportes;
    }

    private void Actualizar()
    {
        RevalorizacionAporteServices RevalorizacionService = new RevalorizacionAporteServices();
        List<RevalorizacionAportes> lstConsulta = new List<RevalorizacionAportes>();
        List<RevalorizacionAportes> lstNoRevalori = new List<RevalorizacionAportes>();
        try
        {
            lstConsulta = RevalorizacionService.Listar(ObtenerValores(), ref lstNoRevalori, (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);

            gvConsultaDatos.DataSource = lstConsulta;

            lblTotalRev.Visible = lstConsulta.Count > 0 ? true : false;
            Site toolBar = (Site)Master;
            if (lstConsulta.Count > 0)
            {
                lblTotalRev.Text = "<br/> Registros generados : " + lstConsulta.Count;
                gvConsultaDatos.Visible = true;
                gvConsultaDatos.DataBind();
                toolBar.MostrarGuardar(true);
                toolBar.MostrarExportar(true);
            }
            else
            {
                toolBar.MostrarGuardar(false);
                toolBar.MostrarExportar(false);
                gvConsultaDatos.Visible = false;
            }
            pnlPendientes.Visible = false;
            if (lstNoRevalori != null && lstNoRevalori.Count > 0)
            {
                pnlPendientes.Visible = true;
                gvNoReval.DataSource = lstNoRevalori;
                gvNoReval.DataBind();
            }
            Session["RevalorizacionAportes"] = lstConsulta;
            TotalizarRevalorizacion();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RevalorizaciondeAporteServicios.GetType().Name + "L", "Actualizar", ex);
        }
    }


    protected void TotalizarRevalorizacion()
    {
        List<RevalorizacionAportes> lstConsulta = new List<RevalorizacionAportes>();
        decimal totalRev = 0;
        if (Session["RevalorizacionAportes"] != null)
        {
            lstConsulta = (List<RevalorizacionAportes>)Session["RevalorizacionAportes"];
            if (lstConsulta.Count > 0)
            {
                totalRev = (decimal)(from s in lstConsulta select s.valordist).Sum();
            }
        }
        txtTotalReva.Text = totalRev.ToString("n2");
    }
    

    private Boolean ValidarDatos()
    {
        Boolean result = true;

        Boolean rpta = ValidarProcesoContable(txtFechaOperacion.ToDateTime, 19);
        if (rpta == false)
        {
            VerError("No existen comprobantes parametrizados para esta operación (Tipo 19 = Revalorización de Aportes)");
            return false;
        }
        if (string.IsNullOrEmpty(txtFechaRevalorizacion.Text))
        {
            VerError("Seleccione una fecha de revalorización");
            return false;
        }
        if (DdlLineaAporte.SelectedIndex == 0)
        {
            String Error = "Seleccione una Linea de Aporte";
            VerError(Error);
            result = false;
        }
        if (string.IsNullOrEmpty(txtFechaOperacion.Text))
        {
            VerError("Seleccione una fecha de Operación");
            return false;
        }
        if (ddlTipoDistribucion.SelectedValue == "1")
        {
            if (ViewState["Validacion"] == null)
            {
                if (txtporcdistribuir.Text == "")
                {
                    String Error = "Digite un valor o porcentaje a Distribuir ";
                    VerError(Error);
                    result = false;
                }
                else
                {
                    if (Convert.ToDecimal(txtporcdistribuir.Text) < 0)
                    {
                        VerError("El porcentaje no puede ser menor al 0%");
                        txtporcdistribuir.Focus();
                        result = false;
                    }
                    if (Convert.ToDecimal(txtporcdistribuir.Text) > 100)
                    {
                        VerError("El porcentaje no puede ser mayor al 100%");
                        txtporcdistribuir.Focus();
                        result = false;
                    }
                }
            }
        }
        return result;
    }
    protected void gvConsultaDatos_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvConsultaDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        Int64 num_aporte = Convert.ToInt64(gvConsultaDatos.DataKeys[e.RowIndex].Values[0].ToString());
        int rIndex = (gvConsultaDatos.PageIndex * gvConsultaDatos.PageSize) + e.RowIndex;

        this.eliminar(num_aporte, rIndex);
    }
    private void eliminar(Int64 conseID, int rIndex)
    {

        List<RevalorizacionAportes> LstRevalorizacionAportes;   
        LstRevalorizacionAportes =  (List<RevalorizacionAportes>)Session["RevalorizacionAportes"];

        LstRevalorizacionAportes.RemoveAt(rIndex);
        
        gvConsultaDatos.DataSourceID = null;
        gvConsultaDatos.DataBind();

        gvConsultaDatos.DataSource = LstRevalorizacionAportes;
        gvConsultaDatos.DataBind();

        Session["RevalorizacionAportes"] = LstRevalorizacionAportes;
        TotalizarRevalorizacion();
    }
    
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Grabar();
    }
    protected void BtnComprobante_Click(object sender, EventArgs e)
    {
        GenerarComporbante();
    }
    protected void BtnCancelar_Click(object sender, EventArgs e)
    {
        mpeComprobante.Hide();
    }
    protected void btnGuardarCom_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
     //   mpeNuevo.EnableViewState = false;
        mpeComprobante.Show();
    }

    private void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvConsultaDatos.Rows.Count > 0 && Session["RevalorizacionAportes"] != null)
        {
            /*
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            GridView gvExportar = CopiarGridViewParaExportar(gvConsultaDatos, "RevalorizacionAportes");
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvExportar);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Revalorizacion.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
            */
            ExportarCSV(gvConsultaDatos);
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }


    protected void ExportarCSV(GridView gvInfo)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition",
         "attachment;filename=Revalorizacion.csv");
        Response.Charset = "";
        Response.ContentType = "application/text";

        gvInfo.AllowPaging = false;
        
        StringBuilder sb = new StringBuilder();
        string pItem;
        for (int k = 0; k < gvInfo.Columns.Count; k++)
        {
            string s = gvInfo.Columns[k].HeaderStyle.CssClass;
            //add separator
            if (s != "gridIco")
            {
                pItem = HttpUtility.HtmlDecode(gvInfo.Columns[k].HeaderText);
                sb.Append(pItem + ';');
            }
        }
        //append new line
        sb.Append("\r\n");
        
        for (int i = 0; i < gvInfo.Rows.Count; i++)
        {
            for (int k = 0; k < gvInfo.Columns.Count; k++)
            {
                //add separator
                string s = gvInfo.Columns[k].ItemStyle.CssClass;
                if (s != "gridIco")
                {
                    if (gvInfo.Rows[i].Cells[k].Text != "&nbsp;" || gvInfo.Rows[i].Cells[k].Text != "")
                    {
                        pItem = HttpUtility.HtmlDecode(gvInfo.Rows[i].Cells[k].Text);
                        sb.Append(pItem + ';');
                    } 
                }

            }
            //append new line
            sb.Append("\r\n");
        }
        Response.Output.Write(sb.ToString());
        Response.Flush();
        Response.End();
    }

    #region Metodo de Importación
    private void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            mvRevaloriza.ActiveViewIndex = 2;
            Site toolBar = (Site)Master;
            toolBar.MostrarCargar(true);
            toolBar.MostrarImportar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    private void btnCargar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        string error = "";
        try
        {
            if (!flpArchivo.HasFile)
            {
                VerError("Seleccione un archivo excel, verifique los datos.");
                return;
            }
            ExcelToGrid CargaEx = new ExcelToGrid();
            //CREACION DEL DATATABLE
            DataTable dt = new DataTable();

            String NombreArchivo = Path.GetFileName(this.flpArchivo.PostedFile.FileName);

            if (!validarExtension(flpArchivo))
            {
                VerError("Ingrese un Archivo valido (Excel).");
                return;
            }
            //GUARDANDO EL ARCHIVO EN EL SERVIDOR
            flpArchivo.PostedFile.SaveAs(Server.MapPath("Archivos\\") + NombreArchivo);
            flpArchivo.Dispose();

            //LEENDO LOS DATOS EN EL DATATABLE
            dt = CargaEx.leerExcel(Server.MapPath("Archivos\\") + NombreArchivo, "Datos");

            // ELIMINANDO ARCHIVOS GENERADOS
            try
            {
                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
                foreach (string ficheroActual in ficherosCarpeta)
                    File.Delete(ficheroActual);
            }
            catch
            { }

            if (dt.Rows.Count <= 0)
            {
                error = "No se pudo cargar el archivo o el archivo no contiene datos (Hoja: Datos)";
                VerError(error);
                return;
            }
            else
            {
                //DECLARACION DE VARIABLES
                List<RevalorizacionAportes> lstImport = new List<RevalorizacionAportes>();
                string pError = string.Empty;
                Boolean rpta = RealizarCargaArvhico(ref lstImport, dt, ref pError);
                if (!rpta)
                {
                    if (string.IsNullOrEmpty(pError))
                    {
                        VerError(pError);
                        return;
                    }
                }

                Site toolBar = (Site)this.Master;
                if (lstImport.Count > 0)
                {
                    toolBar.MostrarGuardar(true);
                    toolBar.MostrarCargar(false);
                    toolBar.MostrarImportar(true);
                    gvConsultaDatos.DataSource = lstImport;
                    gvConsultaDatos.DataBind();
                    lblTotalRev.Visible = true;
                    lblTotalRev.Text = "<br/> Registros generados : " + lstImport.Count;
                    Session["RevalorizacionAportes"] = lstImport;
                    mvRevaloriza.ActiveViewIndex = 0;
                    txtporcdistribuir.Text = "";
                    txtporcdistribuir.Enabled = false;
                    ViewState["Validacion"] = 1;
                }
                else
                {
                    Session["RevalorizacionAportes"] = null;
                    lblTotalRev.Visible = false;
                    toolBar.MostrarGuardar(true);
                    gvConsultaDatos.Visible = false;
                }
                TotalizarRevalorizacion();
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    bool validarExtension(FileUpload file)
    {
        bool rutaOk = false;
        string extension = Path.GetExtension(file.FileName).ToLower();

        rutaOk = extension == ".xls" || extension == ".xlsx" ? true : false; 
        
        return rutaOk;
    }


    protected Boolean RealizarCargaArvhico(ref List<RevalorizacionAportes> lstArchivo, DataTable dt, ref string pError)
    {
        int contador = 0;
        int rowIndex = 0;
        try
        {
            //REALIZANDO CARGA DEL BALANCE INICIAL
            //RECORRIENDO LOS DATOS DEL EXCEL LEIDO
            RevalorizacionAportes pEntidad;
            foreach (DataRow rData in dt.Rows)
            {
                rowIndex++;
                pEntidad = new RevalorizacionAportes();

                //ORDEN DE CARGA : Código Asociado, Identificación, Nombre (Opcional), Estado ( 1 = Activo, 2 = Inactivo, 3 = Cerrado), Numero de Aporte, 
                //Saldo Base, Valor a Revalorizar
                for (int i = 0; i <= 8; i++)
                {
                    if (i == 0)
                    {
                        if (rData.ItemArray[i].ToString().Trim() != "" && rData.ItemArray[i].ToString() != "&nbsp;")
                            pEntidad.codigo = Convert.ToInt64(rData.ItemArray[i].ToString().Trim());
                    }
                    if (i == 1)
                    {
                        if (rData.ItemArray[i].ToString().Trim() != "" && rData.ItemArray[i].ToString() != "&nbsp;")
                            pEntidad.identificacion = rData.ItemArray[i].ToString().Trim();
                    }
                    if (i == 2)
                    {
                        if (rData.ItemArray[i].ToString().Trim() != "" && rData.ItemArray[i].ToString() != "&nbsp;")
                            pEntidad.nombres = rData.ItemArray[i].ToString().Trim();
                    }
                    if (i == 3)
                    {
                        if (rData.ItemArray[i].ToString().Trim() != "" && rData.ItemArray[i].ToString() != "&nbsp;")
                            pEntidad.estado = rData.ItemArray[i].ToString().Trim();
                    }
                    if (i == 4)
                    {
                        if (rData.ItemArray[i].ToString().Trim() != "" && rData.ItemArray[i].ToString() != "&nbsp;")
                            pEntidad.num_aporte = Convert.ToInt64(rData.ItemArray[i].ToString().Trim());
                    }
                    if (i == 5)
                    {
                        if (rData.ItemArray[i].ToString().Trim() != "" && rData.ItemArray[i].ToString() != "&nbsp;")
                            pEntidad.saldo_base = Convert.ToDecimal(rData.ItemArray[i].ToString().Trim());
                    }
                    if (i == 6)
                    {
                        if (rData.ItemArray[i].ToString().Trim() != "" && rData.ItemArray[i].ToString() != "&nbsp;")
                            pEntidad.valordist = Convert.ToDecimal(rData.ItemArray[i].ToString().Trim());
                    }
                    contador++;
                }
                lstArchivo.Add(pEntidad);
            }

            return true;
        }
        catch (Exception ex)
        {
            pError = "Se generó un error en la fila " + rowIndex.ToString() + " - " + ex.Message;
            return false;
        }
    }

    #endregion
}
