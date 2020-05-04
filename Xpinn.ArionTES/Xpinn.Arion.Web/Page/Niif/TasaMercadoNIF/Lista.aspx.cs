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
    private Xpinn.NIIF.Services.TasaMercadoNIFService TasaMercado = new Xpinn.NIIF.Services.TasaMercadoNIFService();
    

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TasaMercado.CodigoProgramaoriginal, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;

            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.eventoExportar += btnExportar_Click;
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TasaMercado.CodigoProgramaoriginal, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {   
            if (!IsPostBack)
            {
                //btnExportar.Visible = false;
                //CargarValoresConsulta(pConsulta, TasaMercado.CodigoProgramaoriginal);
                pDatos.Visible = false;
                Actualizar();

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TasaMercado.CodigoProgramaoriginal, "Page_Load", ex);
        }
    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session[TasaMercado.CodigoProgramaoriginal + ".id"] = null;
        Navegar(Pagina.Nuevo);
    }




    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int cod_tasa_mercado = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values["cod_tasa_mercado"].ToString());

            Session["COD_TASA"] = cod_tasa_mercado;

            ctlMensaje.MostrarMensaje("Desea eliminar la tasa seleccionada?");

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TasaMercado.CodigoProgramaoriginal, "ELiminarFila", ex);
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            TasaMercadoNIF vTasa = new TasaMercadoNIF();
            vTasa.cod_tasa_mercado = Convert.ToInt32(Session["COD_TASA"].ToString());

            TasaMercado.EliminarTasaMercadoNIIF(vTasa, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch(Exception ex)
        {
            BOexcepcion.Throw(TasaMercado.CodigoProgramaoriginal, "btnContinuarMen_Click", ex);
        }
    }
       

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;

        Session[TasaMercado.CodigoProgramaoriginal + ".id"] = id;
        
        Session["CODIGO"] = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session["FECHAINI"] = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        Session["FECHAFIN"] = gvLista.Rows[e.NewEditIndex].Cells[4].Text;
        Session["TASA"] = gvLista.Rows[e.NewEditIndex].Cells[5].Text;
        Session["OBSERVACIONES"] = gvLista.Rows[e.NewEditIndex].Cells[6].Text;
        e.NewEditIndex = -1;
        Navegar(Pagina.Nuevo);
    }


    
    private void Actualizar()
    {
        try
        {
            List<TasaMercadoNIF> lstConsulta = new List<TasaMercadoNIF>();
            TasaMercadoNIF eTasa = new TasaMercadoNIF();

            lstConsulta = TasaMercado.ListarTasaMercadoNIIF(eTasa, (Usuario)Session["usuario"]);
             
            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                Site toolBar = (Site)this.Master;  
                //btnExportar.Visible = true;
               

                pDatos.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
                Session["DTTASAMERCADO"] = lstConsulta;
                
                VerError("");
            }
            else
            {
                Session["DTTASAMERCADO"] = null;
                pDatos.Visible = false;
                gvLista.DataSource = null;
                gvLista.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                //btnExportar.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                VerError("No se encontraron Datos");
            }


            Session.Add(TasaMercado.CodigoProgramaoriginal + ".consulta", 1);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TasaMercado.CodigoProgramaoriginal, "ACTUALIZAR", ex);
                
        }
    }



    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[TasaMercado.CodigoProgramaoriginal + ".id"] = id;
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
            if (gvLista.Rows.Count > 0 && Session["DTTASAMERCADO"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.Columns[0].Visible = false;
                gvLista.Columns[1].Visible = false;
                gvLista.DataSource = Session["DTTASAMERCADO"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=TasaMercadoNIF.xls");
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


    protected string FormatoFecha()
    {
        Configuracion conf = new Configuracion();
        return conf.ObtenerFormatoFecha();
    }
   
}