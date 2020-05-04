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
using Xpinn.NIIF.Services;
using Xpinn.NIIF.Entities;
using System.IO;
using System.Data.OleDb;
using System.Text;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    private Xpinn.NIIF.Services.BalanceNIIFService PlanCuentasServicio = new Xpinn.NIIF.Services.BalanceNIIFService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(PlanCuentasServicio.CodigoPrograma, "L");
            txtAprobacion_fin.ToDateTime = DateTime.Now;
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarConsultar(true);
            toolBar.MostrarCancelar(false);
            toolBar.eventoCancelar += btnRegresar_Click;
            toolBar.eventoCargar += btnCargar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCopiar += btnEditar_click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;

            mvPlanCuentasNIIF.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                gvExport.Visible = false;
                Session["DTPLAN"] = null;
                Label1.Visible = false;
                Session["LISTA-NIFF"] = null;
                Session["Recaudos"] = null;
                pErrores.Visible = false;
                pErroresGrabar.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected List<PlanCuentasNIIF> ObtenerListaNiif()
    {
        List<PlanCuentasNIIF> lstReturn = new List<PlanCuentasNIIF>();
        if (gvLista.Rows.Count > 0)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                PlanCuentasNIIF pEntidad = new PlanCuentasNIIF();
                if (rFila.Cells[1].Text != "&nbsp;")
                    pEntidad.cod_cuenta_niif = rFila.Cells[1].Text.Trim();
                if (rFila.Cells[2].Text != "&nbsp;" && rFila.Cells[2].Text != "")
                    pEntidad.nombre = rFila.Cells[2].Text.Trim();
                if (rFila.Cells[3].Text != "&nbsp;" && rFila.Cells[3].Text != "")
                    pEntidad.tipo = rFila.Cells[3].Text.Trim();
                if (rFila.Cells[4].Text != "&nbsp;" && rFila.Cells[4].Text != "")
                    pEntidad.nivel = Convert.ToInt64(rFila.Cells[4].Text.Trim());
                if (rFila.Cells[5].Text != "&nbsp;" && rFila.Cells[5].Text != "")
                    pEntidad.depende_de = rFila.Cells[5].Text.Trim();
                if (rFila.Cells[6].Text != "&nbsp;" && rFila.Cells[6].Text != "")
                    pEntidad.moneda = rFila.Cells[6].Text.Trim();
                if (rFila.Cells[7].Text != "&nbsp;" && rFila.Cells[7].Text != "")
                    pEntidad.cod_moneda = Convert.ToInt64(rFila.Cells[7].Text.Trim());
                if (rFila.Cells[8].Text != "&nbsp;" && rFila.Cells[8].Text != "")
                    pEntidad.estado = Convert.ToInt64(rFila.Cells[8].Text);
                CheckBox chkTerceros = (CheckBox)rFila.FindControl("chkTerceros");
                if (chkTerceros != null)
                    pEntidad.maneja_ter = chkTerceros.Checked ? 1 : 0;

                CheckBox chkManejaCC = (CheckBox)rFila.FindControl("chkManejaCC");
                if (chkManejaCC != null)
                    pEntidad.maneja_cc = chkManejaCC.Checked ? 1 : 0;

                CheckBox chkManejaCG = (CheckBox)rFila.FindControl("chkManejaCG");
                if (chkManejaCG != null)
                    pEntidad.maneja_sc = chkManejaCG.Checked ? 1 : 0;

                CheckBox chkManejaImp = (CheckBox)rFila.FindControl("chkManejaImp");
                if (chkManejaImp != null)
                    pEntidad.impuesto = chkManejaImp.Checked ? 1 : 0;

                CheckBox chkManejaCP = (CheckBox)rFila.FindControl("chkManejaCP");
                if (chkManejaCP != null)
                    pEntidad.maneja_gir = chkManejaCP.Checked ? 1 : 0;

                if (rFila.Cells[13].Text != "&nbsp;" && rFila.Cells[13].Text != "")
                    pEntidad.base_minima = Convert.ToDecimal(rFila.Cells[13].Text);
                if (rFila.Cells[14].Text != "&nbsp;" && rFila.Cells[14].Text != "")
                    pEntidad.porcentaje_impuesto = Convert.ToDecimal(rFila.Cells[14].Text);
                if (rFila.Cells[15].Text != "&nbsp;" && rFila.Cells[15].Text != "")
                    pEntidad.cod_cuenta = rFila.Cells[15].Text.Trim();
                
                lstReturn.Add(pEntidad);                
            }
            Session["DTPLAN"] = lstReturn;
        }
        return lstReturn;
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["DTPLAN"] != null)
        {
            gvExport.Visible = true;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.AllowPaging = false;
            gvExport.AllowPaging = false;
            gvExport.DataSource = Session["DTPLAN"];
            gvExport.DataBind();
            gvExport.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvExport);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=PlanCuentasNiif.xls");
            Response.Charset = "";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }


    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        mvPlanCuentasNIIF.ActiveViewIndex = 0;
        Actualizar();
        Site toolBar = (Site)this.Master;
        lblTotalRegs.Visible = false;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarCopiar(true);
        toolBar.MostrarCargar(true);
        toolBar.MostrarCancelar(false);        
        Label1.Visible = false;
        pErrores.Visible = false;
        VerError("");
        Session["LISTA-NIFF"] = null;
        Session["Recaudos"] = null;
    }

    protected void ActualizarLocal()
    {
        try
        {
            Site toolBar = (Site)this.Master;

            List<PlanCuentasNIIF> lstConsulta = new List<PlanCuentasNIIF>();
            string filtro = " plan_cuentas.cod_cuenta Not In (Select x.cod_cuenta_niif From plan_cuentas_niif x) ";
            lstConsulta = PlanCuentasServicio.ListarPlanCuentasLocal(ObtenerValores(), (Usuario)Session["usuario"], filtro);
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                Session["Recaudos"] = lstConsulta;
                toolBar.MostrarGuardar(true);
                toolBar.MostrarCopiar(false);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Text = "<br/> No se encontraron cuentas contables para generar ";
                lblTotalRegs.Visible = true;
            }

            Session.Add(PlanCuentasServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected void btnEditar_click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            Session["LISTA-NIFF"] = "false";
            Label1.Visible = false;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarCopiar(true);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarCargar(false);
            toolBar.MostrarCancelar(true);
            mvPlanCuentasNIIF.ActiveViewIndex = 0;
            ActualizarLocal();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected void btnGuardarReg_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        VerError("");
        List<PlanCuentasNIIF> lstRecaudos = new List<PlanCuentasNIIF>();
        string error = "";

        try
        {
            if (FileUpload2.HasFile)
            {
                Stream stream = FileUpload2.FileContent;
                DateTime fecha_aplicacion = DateTime.Now;

                List<PlanCuentasNIIF> plstErrores = new List<PlanCuentasNIIF>();
                lstRecaudos = PlanCuentasServicio.CargarArchivo(fecha_aplicacion, stream, ref error, ref plstErrores, (Usuario)Session["usuario"]);
                Session["Recaudos"] = lstRecaudos;
                if (error.Trim() != "")
                {
                    VerError(error);
                    return;
                }
                if (plstErrores.Count() > 0)
                {
                    pErrores.Visible = true;
                    Label1.Visible = true;
                    gvErrores.DataSource = plstErrores;
                    gvErrores.DataBind();                    
                }
                if (lstRecaudos.Count > 0)
                {
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros Cargados " + lstRecaudos.Count.ToString();
                    gvLista.DataSource = lstRecaudos;
                    gvLista.DataBind();
                    gvLista.Visible = true;
                    txtAprobacion_fin.Visible = true;
                    mvPlanCuentasNIIF.ActiveViewIndex = 0;

                    FileUpload2.Visible = true;
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(true);
                    toolBar.MostrarConsultar(false);
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "Archivo No Valido";
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }

    }

    protected void btnCargar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Label1.Visible = false;
        mvPlanCuentasNIIF.ActiveViewIndex = 1;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(false);
        toolBar.MostrarCopiar(false);
        toolBar.MostrarGuardar(false);
        toolBar.MostrarCargar(false);
        toolBar.MostrarExportar(false);
        lblTotalRegs.Visible = false;

    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Label1.Visible = false;
        VerError("");
        Session["LISTA-NIFF"] = "true";
        Actualizar();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarCopiar(true);
        toolBar.MostrarCargar(true);
    }


    protected Boolean ValidarData()
    {
        if (gvLista.Rows.Count == 0)
        {
            VerError("No existen plan de cuentas por registrar, Verifique los datos.");
            return false;
        }
        //validacion por linea
        if (gvLista.Rows.Count > 0)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                string cod_cuenta_niff = "", nombre = "", tipo = "", depende_de = "", cod_cuenta = "", nivel = "";
                if (rFila.Cells[1].Text != "&nbsp;")
                    cod_cuenta_niff = rFila.Cells[1].Text.Trim();
                if (cod_cuenta_niff == "")
                {
                    VerError("No existe el código de cuenta Niif en la Fila " + (rFila.RowIndex + 1) + " , Verifique los datos." );
                    return false;
                }
                if (rFila.Cells[2].Text != "&nbsp;")
                    nombre = rFila.Cells[2].Text.Trim();
                if (nombre == "")
                {
                    VerError("No existe el nombre de cuenta Niif en la Fila " + (rFila.RowIndex + 1) + " , Verifique los datos.");
                    return false;
                }
                if (rFila.Cells[3].Text != "&nbsp;")
                    tipo = rFila.Cells[3].Text.Trim();
                if (tipo == "")
                {
                    VerError("No existe el tipo de cuenta Niif en la Fila " + (rFila.RowIndex + 1) + " , Verifique los datos.");
                    return false;
                } 
                if (rFila.Cells[4].Text != "&nbsp;")
                    nivel = rFila.Cells[4].Text;
                if (nivel == "")
                {
                    VerError("Error en la Fila " + (rFila.RowIndex + 1) + " : Debe ingresar un Nivel para la cuenta contable Niif.");
                    return false;
                }                
                if (nivel != "1")
                {
                    if (cod_cuenta_niff.Contains(depende_de) == false)
                    {
                        VerError("Error en la Fila " + (rFila.RowIndex + 1) + " : La cuenta contable Niif no coincide con la cuenta que depende.");
                        return false;
                    }
                }
                if (rFila.Cells[5].Text != "&nbsp;")
                    depende_de = rFila.Cells[5].Text.Trim();
                //if (depende_de == "" && cod_cuenta_niff.Length > 1)
                //{
                //    VerError("Error en la Fila " + (rFila.RowIndex + 1) + " : No se ingreso a que cuenta pertenece el código contable Niif.");
                //    return false;
                //}
                if (rFila.Cells[15].Text != "&nbsp;")
                    cod_cuenta = rFila.Cells[15].Text.Trim();
            }
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Label1.Visible = false;
        if (Session["Recaudos"] == null)
        {
            VisualizarOpciones(PlanCuentasServicio.CodigoPrograma, "L");
        }
        if (ValidarData())
            ctlMensaje.MostrarMensaje("Desea cargar el plan de cuentas a NIIF?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkCambiarArchivoConsulta.Checked)
            {
                int Id = 0;
                PlanCuentasServicio.Eliminarniff(Id, (Usuario)Session["usuario"]);
            }
            List<PlanCuentasNIIF> LstPlan = new List<PlanCuentasNIIF>();
            List<PlanCuentasNIIF> lstPlanNoRegistrados = new List<PlanCuentasNIIF>();

            if (Session["Recaudos"] != null)
                LstPlan = (List<PlanCuentasNIIF>)Session["Recaudos"];
            if (LstPlan.Count > 0)
            {
                PlanCuentasServicio.GenerarPlanCuentasNIIF(LstPlan, ref lstPlanNoRegistrados, (Usuario)Session["usuario"]);
            }
            
            if (lstPlanNoRegistrados.Count > 0)
            {
                pErroresGrabar.Visible = true;
                gvDatosErrados.DataSource = lstPlanNoRegistrados;
                gvDatosErrados.DataBind();
            }

            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarConsultar(false);
            Session.Remove("LISTA-NIFF");
            Session.Remove("Recaudos");
            Session.Remove("DTPLAN");
            mvPlanCuentasNIIF.ActiveViewIndex = 2;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id;
        id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[PlanCuentasServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (Session["LISTA-NIFF"] != null)
            {
                gvLista.PageIndex = e.NewPageIndex;
                if (Session["LISTA-NIFF"].ToString() == "true")
                    Actualizar();
                else
                    ActualizarLocal();
            }
            else
            {
                if (Session["Recaudos"] != null)
                {
                    gvLista.PageIndex = e.NewPageIndex;
                    List<PlanCuentasNIIF> lstRecaudos = new List<PlanCuentasNIIF>();
                    lstRecaudos = (List<PlanCuentasNIIF>)Session["Recaudos"];
                    gvLista.DataSource = null;
                    gvLista.DataBind();
                    gvLista.DataSource = lstRecaudos;
                    gvLista.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try 
        {
            mvPlanCuentasNIIF.ActiveViewIndex = 0;
            List<PlanCuentasNIIF> lstConsulta = new List<PlanCuentasNIIF>();
            string filtro = " ";
            lstConsulta = PlanCuentasServicio.ListarPlanCuentasNIIF(ObtenerValores(), (Usuario)Session["usuario"], filtro);
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Session["DTPLAN"] = null;
            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                Session["DTPLAN"] = lstConsulta;
                toolBar.MostrarExportar(true);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Text = "<br/> No se encontraron cuentas contables para generar ";
                lblTotalRegs.Visible = true;
                toolBar.MostrarExportar(false);
            }

            Session.Add(PlanCuentasServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private PlanCuentasNIIF ObtenerValores()
    {
        PlanCuentasNIIF vPlanCuentas = new PlanCuentasNIIF();
        return vPlanCuentas;
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string conseID = gvLista.DataKeys[e.RowIndex].Values[0].ToString();
        List<PlanCuentasNIIF> LstPlan = new List<PlanCuentasNIIF>();
        if (Session["Recaudos"] != null)
            LstPlan = (List<PlanCuentasNIIF>)Session["Recaudos"];
        int i = -1;
        int contador = 0;
        foreach (PlanCuentasNIIF cuenta in LstPlan)
        {
            if (cuenta.cod_cuenta_niif == conseID)
            {
                i = contador;
                break;
            }
            contador += 1;
        }
        if (i >= 0)
            LstPlan.RemoveAt(i);
        gvLista.DataSource = LstPlan;
        gvLista.DataBind();
        Session["Recaudos"] = LstPlan;
        if (lblTotalRegs.Text.ToUpper().Contains("CARGADOS") == true)
            if (LstPlan.Count == 0)
                lblTotalRegs.Text = "";
            else
                lblTotalRegs.Text = "<br/> Registros Cargados " + LstPlan.Count.ToString();
        else
            if (LstPlan.Count == 0)
                lblTotalRegs.Text = "";
            else
                lblTotalRegs.Text = "<br/> Registros encontrados " + LstPlan.Count.ToString();
    }


    protected void btnExportarErroneos_Click(object sender, EventArgs e)
    {
        VerError("");
        if (gvDatosErrados.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            
            gvDatosErrados.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvDatosErrados);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=PlanCuenta.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        else
        {
            VerError("No existen datos por exportar, Verifique o intente nuevamente.");
        }
    }  

}