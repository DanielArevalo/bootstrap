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
using Xpinn.Comun.Services;
using Xpinn.NIIF.Services;
using Xpinn.NIIF.Entities;
using System.IO;
using Microsoft.Win32;
using System.Text;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    private Xpinn.NIIF.Services.CarteraNIFService CarteraServicio = new Xpinn.NIIF.Services.CarteraNIFService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {

            VisualizarOpciones(CarteraServicio.CodigoProgramaoriginal, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;           
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CarteraServicio.CodigoProgramaoriginal, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {          

            if (!IsPostBack)
            {
                btnExportar.Visible = false;
                txtFecha.Text = DateTime.Today.ToShortDateString();
                CargarValoresConsulta(pConsulta, CarteraServicio.CodigoProgramaoriginal);
                pDatos.Visible = false;
                txtFecha.Text = DateTime.Today.ToShortDateString();

                CargarDropDawn();

                Session["Orden"] = "SIN_DATA";
                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CarteraServicio.CodigoProgramaoriginal, "Page_Load", ex);
        }
    }


   
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Page.Validate();
            gvLista.Visible = true;
            if (Page.IsValid)
            {
                GuardarValoresConsulta(pConsulta, CarteraServicio.CodigoProgramaoriginal);
                gvLista.DataSource = null;
                DateTime pFecha;
                pFecha = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;
                Session["Fecha"] = pFecha;
                CarteraServicio.ConsultarCarteraNIIF(pFecha, (Usuario)Session["Usuario"]);
                Actualizar();
            }
        }
        catch        
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            btnExportar.Visible = false;
            gvLista.DataSource = null;
            gvLista.Visible = false;
            lblTotalRegs.Visible = true;
            lblTotalRegs.Text = "<br/>No se encontraron Registros ";
            VerError("No existe datos para la fecha ingresada");          
        }
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        Session["Opcion"] = "GRABAR";
        ctlMensaje.MostrarMensaje("Desea realizar el Costo de Cartera para los créditos seleccionados?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["Opcion"] == "GRABAR")
            {

                CarteraServicio.ModificarEstadoFechaNIIF(Convert.ToDateTime(Session["Fecha"].ToString()), (Usuario)Session["usuario"]);

                txtFecha.Enabled = false;

                Session["FechaComprobante"] = txtFecha.Text;
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                //Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = 0;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 116;
                //Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(txtFecha.Text);
                //Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = -1;
                //Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = Session["Oficina"];
                //Session["OrigenComprobante"] = "../../Niif/CarteraNIF/Lista.aspx";
                //Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");


                VerError("Datos Grabados Correctamente");
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                btnExportar.Visible = false;
                gvLista.DataSource = null;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }
            if (Session["Opcion"] == "ELIMINAR")
            {
                CarteraNIF vCartera = new CarteraNIF();
                vCartera.numero_radicacion = Convert.ToInt32(Session["NUM_RADICACION"].ToString());
                DateTime vfecha = Convert.ToDateTime(Session["Fecha"].ToString());

                CarteraServicio.EliminarCarteraNIIF(vCartera, vfecha, (Usuario)Session["usuario"]);
                Actualizar();
                //Session.Remove(CarteraServicio.CodigoProgramaoriginal + ".id");
            }
        }
        catch
        {           
        
        }
    }
       

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;

        Session[CarteraServicio.CodigoProgramaoriginal + ".id"] = id;
        e.NewEditIndex = -1;       
    }


    void CargarDropDawn()
    {
        try
        {
            ddlOrdenado.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlOrdenado.Items.Insert(1, new ListItem("Numero de Cuenta", "numero_radicacion"));
            ddlOrdenado.Items.Insert(2, new ListItem("Cliente", "nombre"));
            ddlOrdenado.Items.Insert(3, new ListItem("Tipo", "tipo"));
            ddlOrdenado.Items.Insert(4, new ListItem("Monto", "monto"));
            ddlOrdenado.Items.Insert(5, new ListItem("Saldo Capital", "saldo"));
            ddlOrdenado.Items.Insert(6, new ListItem("Plazo ", "plazo"));
            ddlOrdenado.Items.Insert(7, new ListItem("Peridiocidad", "cod_periodicidad"));
            ddlOrdenado.Items.Insert(8, new ListItem("Cuota ", "cuota"));
            ddlOrdenado.Items.Insert(9, new ListItem("Fecha del Proximo Pago", "fecha_proximo_pago"));
            ddlOrdenado.Items.Insert(10, new ListItem("Dias de Mora", "dias_mora"));
            ddlOrdenado.Items.Insert(11, new ListItem("Valor del Credito", "valor_total"));

            ddlOrdenado.DataBind();
            ddlOrdenado.SelectedIndex = 0;
        }
        catch
        { }
    
    }

    
    private void Actualizar()
    {
        try
        {
            List<Xpinn.NIIF.Entities.CarteraNIF> lstConsulta = new List<Xpinn.NIIF.Entities.CarteraNIF>();

            lstConsulta = CarteraServicio.ListarCarteraNIIF(Convert.ToDateTime(Session["Fecha"].ToString()), Session["Orden"].ToString(), (Usuario)Session["usuario"]);
                        
            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                Site toolBar = (Site)this.Master;   
                toolBar.MostrarGuardar(true);
                btnExportar.Visible = true;
                //ddlOrdenado.Enabled = true;

                pDatos.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
                Session["DTCOSTONIF"] = lstConsulta;
                
                VerError("");
            }
            else
            {
                Session["DTCOSTONIF"] = null;
                pDatos.Visible = false;
                gvLista.DataSource = null;
                gvLista.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;                
                btnExportar.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                VerError("No existen Datos para la fecha seleccionada");
            }
                       

            Session.Add(CarteraServicio.CodigoProgramaoriginal + ".consulta", 1);

        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(CarteraServicio.CodigoProgramaoriginal, "ACTUALIZAR", ex);
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            btnExportar.Visible = false;
            gvLista.DataSource = null;
            gvLista.Visible = false;
            lblTotalRegs.Visible = true;
            lblTotalRegs.Text = "<br/>No se encontraron Registros ";
            VerError("No existe un cierre definido para la fecha ingresada");            
        }
    }



    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[CarteraServicio.CodigoProgramaoriginal + ".id"] = id;
        Navegar(Pagina.Detalle);
    }
    
    protected void gvLista_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gvLista.PageIndex = e.NewPageIndex;
        Actualizar();
    }               

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTCOSTONIF"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTCOSTONIF"];
                gvLista.DataBind();
                gvLista.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvLista);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=Data_CarteraNIF.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
            else 
            {
                VerError("No existen Datos");
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    decimal SumTotal = 0;    

    protected void gvLista_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SumTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total_ajuste"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[17].Text = "Total:";
            e.Row.Cells[17].Font.Bold = true;
            e.Row.Cells[18].Text = SumTotal.ToString("c");
            e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[18].Font.Bold = true;            
        }

    }


   
    protected void ddlOrdenado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrdenado.SelectedIndex != 0)
        {
            Session["Orden"] = ddlOrdenado.SelectedValue;
        }
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Session["Opcion"] = "ELIMINAR";
            int num_radicacion = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values["numero_radicacion"].ToString());

             Session["NUM_RADICACION"] = num_radicacion;
           
            ctlMensaje.MostrarMensaje("Desea eliminar el créditos seleccionados?");
                        
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CarteraServicio.CodigoProgramaoriginal, "ELiminarFila", ex);
        }


    }
}