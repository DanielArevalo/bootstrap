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
using Xpinn.ActivosFijos.Entities;
using System.Drawing;

partial class Lista : GlobalWeb
{
    private Xpinn.ActivosFijos.Services.ActivosFijoservices ActivosFijoservicio = new Xpinn.ActivosFijos.Services.ActivosFijoservices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ActivosFijoservicio.CodigoProgramaDeterioroNif, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;

            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaDeterioroNif, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                txtFecha.Text = DateTime.Today.ToShortDateString();
                LimpiarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaDeterioroNif);
                btnExportar.Visible = false;
                CargarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaDeterioroNif);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaDeterioroNif, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaDeterioroNif);
        Actualizar();
    }


    protected List<ActivoFijo> ObtenerListaDeterioro()
    {
        List<ActivoFijo> lstDete = new List<ActivoFijo>();

        foreach (GridViewRow rFila in gvLista.Rows)
        {
            decimalesGridRow txtnewvalor = (decimalesGridRow)rFila.FindControl("txtnuevo");
            TextBox txtobservaciones = (TextBox)rFila.FindControl("txtObser");

            string valor = gvLista.DataKeys[rFila.RowIndex].Values[1].ToString(); //capturando el valor de activo niif
            decimal valorActivo = ConvertirStringToDecimal(valor);

            if (Convert.ToDecimal(txtnewvalor.Text.Replace(gSeparadorMiles, "")) <= valorActivo && txtnewvalor.Text.Trim() != "0")
            {
                ActivoFijo pActivoFijo = new ActivoFijo();
                pActivoFijo.consecutivo = Convert.ToInt32(rFila.Cells[0].Text.ToString());
                pActivoFijo.codigo_act = Convert.ToInt32(rFila.Cells[1].Text.ToString());
                pActivoFijo.valor_deterioro = Convert.ToDecimal(txtnewvalor.Text.Replace(gSeparadorMiles,""));
                pActivoFijo.observaciones = txtobservaciones.Text;
                lstDete.Add(pActivoFijo);
            }
        }
        return lstDete;
    }


    protected Boolean validarDatos()
    {
        Boolean rpta = ValidarProcesoContable(DateTime.Now, 106);
        if (rpta == false)
        {
            VerError("No existen comprobantes parametrizados para esta operación (Tipo 106 = Deterioro Activos Fijos)");
            return false;
        }
        string pMensaje = string.Empty;
        pMensaje = ActivosFijoservicio.ValidarDeterioroNiif(Convert.ToDateTime(txtFecha.Text), (Usuario)Session["usuario"]);
        if (!string.IsNullOrEmpty(pMensaje))
        {
            VerError(pMensaje);
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if(validarDatos())
            ctlMensaje.MostrarMensaje("Desea guardar los datos de la depreciación de activos fijos?");        
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {

        try
        {
            // Aplicando las depreciaciones
            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            // Valida que exista parametrización contable para la operación        
            List<Xpinn.Contabilidad.Entities.ProcesoContable> LstProcesoContable;
            LstProcesoContable = ComprobanteServicio.ConsultaProceso(0, 106, txtFecha.ToDateTime, pUsuario);
            if (LstProcesoContable.Count() == 0)
            {
                VerError("No existen comprobantes parametrizados para esta operación (Tipo 106 = Deterioro Activos Fijos)");
                return;
            }

            Site toolBar = (Site)Master;
            // Determinar código de proceso contable para generar el comprobante
            Int64? rpta = 0;
            if (!panelProceso.Visible && MultiView1.Visible)
            {
                rpta = ctlproceso.Inicializar(106, Convert.ToDateTime(txtFecha.Text), (Usuario)Session["Usuario"]);
                if (rpta > 1)
                {
                    
                    toolBar.MostrarGuardar(false);
                    toolBar.MostrarConsultar(false);
                    MultiView1.Visible = false;
                    panelProceso.Visible = true;
                }
                else
                {
                    if (GenerarDeterioro())
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaDeterioroNif, "btnContinuarMen_Click", ex);
        }
    }

    protected bool GenerarDeterioro()
    {
        List<ActivoFijo> lstDete = new List<ActivoFijo>();
        lstDete = ObtenerListaDeterioro();
        
        string pError = "";
        Int64 pCodOpe = 0;

        ActivosFijoservicio.CrearDeterioroNIF(txtFecha.ToDateTime, lstDete,ref pCodOpe, ref pError, (Usuario)Session["Usuario"]);
        if (pError != "")
        {
            VerError(pError.ToString());
            return false;
        }
        // Guardar los datos de la cuenta del cliente y generar el comprobante si se pudo crear la operaciòn.
        if (pCodOpe != 0)
        {
            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = pCodOpe;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 106;            
        }
        return true;
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ActivosFijoservicio.CodigoProgramaDeterioroNif);
        List<Xpinn.ActivosFijos.Entities.ActivoFijo> lstConsulta = new List<Xpinn.ActivosFijos.Entities.ActivoFijo>();
        gvLista.DataSource = lstConsulta;
        gvLista.DataBind();
        btnExportar.Visible = false;
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Values[0].ToString();
        Session[ActivosFijoservicio.CodigoProgramaDeterioroNif + ".id"] = id;
        Navegar(Pagina.Detalle);
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
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaDeterioroNif, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        if (txtFecha.TieneDatos == false)
        {
            VerError("Debe ingresar la fecha de deterioro");
            return;
        }
        try
        {
            DateTime pFecha = new DateTime();
            pFecha = txtFecha.ToDateTime;
            List<Xpinn.ActivosFijos.Entities.ActivoFijo> lstConsulta = new List<Xpinn.ActivosFijos.Entities.ActivoFijo>();
            lstConsulta = ActivosFijoservicio.ListarActivoDeterioroNif(pFecha, (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                btnExportar.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                toolBar.MostrarGuardar(true);
            }
            else
            {
                btnExportar.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                toolBar.MostrarGuardar(false);
            }

            Session.Add(ActivosFijoservicio.CodigoProgramaDepre + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActivosFijoservicio.CodigoProgramaDepre, "Actualizar", ex);
        }
    }

    private Xpinn.ActivosFijos.Entities.ActivoFijo ObtenerValores()
    {
        Xpinn.ActivosFijos.Entities.ActivoFijo vActivoFijo = new Xpinn.ActivosFijos.Entities.ActivoFijo();
        return vActivoFijo;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=DetActivosFijos.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();
        
        //sw = expGrilla.ObtenerGrilla(GridView1,null);
        sw = ObtenerGrilla(GridView1);
        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public StringWriter ObtenerGrilla(GridView GridView1)
    {
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView1.RowStyle.BackColor;
                    }
                    cell.CssClass = "gridItem";
                    List<Control> lstControls = new List<Control>();

                    //Add controls to be removed to Generic List
                    foreach (Control control in cell.Controls)
                    {
                        lstControls.Add(control);
                    }

                    //Loop through the controls to be removed and replace then with Literal
                    foreach (Control control in lstControls)
                    {
                        switch (control.GetType().Name)
                        {
                            case "TextBox":
                                cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                break;
                            case "general_controles_decimalesgridrow_ascx":
                                cell.Controls.Add(new Literal { Text = (control as decimalesGridRow).Text });
                                break;
                        }
                        cell.Controls.Remove(control);
                    }
                }
            }

            GridView1.RenderControl(hw);

            return sw;
        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarConsultar(true);
        MultiView1.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.Visible = true;
            panelProceso.Visible = false;
            GenerarDeterioro();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


}