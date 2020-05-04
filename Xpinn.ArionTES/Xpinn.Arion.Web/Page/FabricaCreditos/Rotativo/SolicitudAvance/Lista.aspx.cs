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
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using System.Text;
using System.Web.UI.HtmlControls;
using System.IO;

using System.Web.UI.WebControls.WebParts;

partial class Lista : GlobalWeb
{
    AvanceService AvanServicios = new AvanceService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AvanServicios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvanServicios.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvanServicios.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtNumCred.Text = ""; txtFecha.Text = ""; ddlLinea.SelectedIndex = 0; txtIdentificacion.Text = ""; txtNombre.Text = ""; txtCodigoNomina.Text = "";
        Actualizar();
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }


    void cargarDropdown()
    {
        //PoblarLista("lineascredito", ddlLinea);

        Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
        ddlOficina.DataSource = oficinaServicio.ListarOficina(oficina, (Usuario)Session["usuario"]);
        ddlOficina.DataTextField = "nombre";
        ddlOficina.DataValueField = "cod_oficina";
        ddlOficina.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlOficina.SelectedIndex = 0;
        ddlOficina.DataBind();


        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        LineasCredito eLinea = new LineasCredito();
        eLinea.tipo_linea = 2;
        eLinea.estado = 1;
        ddlLinea.DataSource = LineaCreditoServicio.ListarLineasCredito(eLinea, (Usuario)Session["usuario"]);
        ddlLinea.DataTextField = "nom_linea_credito";
        ddlLinea.DataValueField = "Codigo";
        ddlLinea.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlLinea.SelectedIndex = 0;
        ddlLinea.DataBind();
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


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
            gvLista.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvanServicios.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    //protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Cargar los datos del crédito rotativo seleccionado
        AvanceService AvancServices = new AvanceService();
        Avance vDetalle = new Avance();
        String id = gvLista.SelectedRow.Cells[1].Text; //gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        if (id == "")
        { 
            VerError("No pudo seleccionar el rotativo");
            return;
        }        
        Session[AvanServicios.CodigoPrograma + ".id"] = id;
        Session["codigocliente"] = gvLista.SelectedRow.Cells[4].Text; // Convert.ToInt64(gvLista.Rows[e.NewEditIndex].Cells[4].Text);
        // Consultar parámetro general para saber si requiere aprobación los rotativos
        Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
        pData = ConsultaData.ConsultarGeneral(41, (Usuario)Session["usuario"]);       
        if (pData.valor != "" )
        {
            vDetalle = AvancServices.ConsultarCredRotativoXaprobarXCredito(Convert.ToInt64(id), (Usuario)Session["usuario"]);
            Int64 numdias = Convert.ToInt16(pData.valor);
            Int64 saldocapital = Convert.ToInt64(vDetalle.saldo_capital);
            DateTime fechahoy = DateTime.Now;
            DateTime fecha = Convert.ToDateTime(vDetalle.fecha_proximo_pago);
            DateTime fecha2 = Convert.ToDateTime(fecha.AddDays(numdias));            
            if (saldocapital == 0)
            {
               Navegar(Pagina.Detalle);                
            }
            if (saldocapital > 0)
            {
                VerError("fecha2 " + fecha2.ToString() + " fechahoy" + fechahoy.ToString());
                if (fecha2 <= fechahoy)
                {
                    VerError("No puede Solicitar avances. Verifique que el Crédito este al dia");
                    //e.NewEditIndex = -1;
                    return;
                }
                Navegar(Pagina.Detalle);
            }
        }
        else
        {
            VerError("Ir a Detalle");
            Navegar(Pagina.Detalle);
        }


    }



    private void Actualizar()
    {
        try
        {
            List<Avance> lstConsulta = new List<Avance>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFecha;
            pFecha = txtFecha.ToDateTime == null ? DateTime.MinValue : txtFecha.ToDateTime;

            lstConsulta = AvanServicios.ListarCreditoRotativos(ObtenerValores(), pFecha, (Usuario)Session["usuario"], filtro);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                //panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();

            }
            else
            {
                // panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
            }

            Session.Add(AvanServicios.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AvanServicios.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Avance ObtenerValores()
    {
        Avance vAvance = new Avance();
        if (txtNumCred.Text.Trim() != "")
            vAvance.numero_radicacion = Convert.ToInt64(txtNumCred.Text.Trim());
        if (txtIdentificacion.Text != "")
            vAvance.identificacion = txtIdentificacion.Text;
        if (txtNombre.Text.Trim() != "")
            vAvance.nombre = txtNombre.Text.Trim().ToUpper();
        if (ddlLinea.SelectedIndex != 0)
            vAvance.cod_linea_credito = ddlLinea.SelectedValue;
        if (ddlOficina.SelectedIndex != 0)
            vAvance.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);
        return vAvance;
    }



    private string obtFiltro(Avance credit)
    {
        String filtro = String.Empty;

        if (txtNumCred.Text.Trim() != "")
            filtro += " and c.numero_radicacion = " + credit.numero_radicacion;
        if (txtIdentificacion.Text != "")
            filtro += " and p.identificacion like '%" + credit.identificacion + "%'";
        if (txtNombre.Text.Trim() != "")
            filtro += " and p.primer_nombre ||' '|| p.segundo_nombre||' '|| p.primer_apellido||' '||p.segundo_apellido like '%" + credit.nombre + "%'";
        if (txtCodigoNomina.Text.Trim() != "")
            filtro += " and p.cod_nomina = '" + txtCodigoNomina.Text + "'";

        if (ddlLinea.SelectedIndex != 0)
            filtro += " and c.COD_LINEA_CREDITO = '" + credit.cod_linea_credito + "'";
        //  else
        filtro += " and l.tipo_linea = 2 ";
        filtro += " and l.estado= 1 ";
        filtro += " and c.estado in ('C','G') ";
        filtro += " and c.estado not in ('T') ";
        //filtro += " and c.saldo_capital>0  ";
        //filtro += " and c.estado= 'G' ";

        if (ddlOficina.SelectedIndex != 0)
            filtro += " and c.cod_oficina = " + credit.cod_oficina;

        //filtro += " and cupodisponible>0 ";
        return filtro;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ////if (gvLista.Rows.Count > 0)
        ////{
        ////    Response.Clear();
        ////    Response.Buffer = true;
        ////    Response.ContentType = "application/vnd.ms-excel";
        ////    Response.AddHeader("content - disposition", "attachment; filename = GridViewExport.xls");

        ////    Response.Charset = "";
        ////    Response.ContentType = "application / vnd.ms - excel";
        ////    Response.ContentEncoding = System.Text.Encoding.UTF8;
        ////    Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
        ////    using (StringWriter sw = new StringWriter())
        ////    {
        ////        HtmlTextWriter hw = new HtmlTextWriter(sw);

        ////        //Exportar todas las paginas

        ////        gvLista.AllowPaging = false;
        ////        //llenaGrid(false);

        ////        gvLista.HeaderRow.BackColor = System.Drawing.Color.White;
        ////        foreach (TableCell cell in gvLista.HeaderRow.Cells)
        ////        {
        ////            cell.BackColor = gvLista.HeaderStyle.BackColor;
        ////        }
        ////        foreach (GridViewRow row in gvLista.Rows)
        ////        {
        ////            row.BackColor = System.Drawing.Color.White;
        ////            foreach (TableCell cell in row.Cells)
        ////            {
        ////                cell.CssClass = "textmode";
        ////            }
        ////        }


        ////        gvLista.RenderBeginTag(hw);
        ////        gvLista.HeaderRow.RenderControl(hw);
        ////        foreach (GridViewRow row in gvLista.Rows)
        ////        {
        ////            row.RenderControl(hw);
        ////        }
        ////        gvLista.FooterRow.RenderControl(hw);
        ////        gvLista.RenderEndTag(hw);


        ////       // gvLista.RenderControl(hw);

        ////        string style = @"< style > .textmode { } </ style >"    ;
        ////        Response.Write(style);
        ////        Response.Output.Write(sw.ToString());
        ////        Response.Flush();
        ////        Response.End();
        ////    }
        ////}

        //Response.Clear();
        //Response.AddHeader("content-disposition", "attachment; filename = FileName.xls");

        //Response.Charset = "";

        //// If you want the option to open the Excel file without saving than

        //// comment out the line below

        //// Response.Cache.SetCacheability(HttpCacheability.NoCache);

        ////Response.ContentType = "application/vnd.xls";
        ////System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        ////System.Web.UI.HtmlTextWriter htmlWrite =
        ////new HtmlTextWriter(stringWrite);

        ////gvLista.RenderControl(htmlWrite);

        ////Response.Write(stringWrite.ToString());

        ////Response.End();


        //Response.Clear();
        //Response.Buffer = true;
        //Response.ContentType = "application/vnd.ms-excel";
        //Response.Charset = "";

        //this.EnableViewState = false;
        //System.IO.StringWriter stringWrite1 = new System.IO.StringWriter();
        //  System.Web.UI.HtmlTextWriter htmlWrite1 =
        //new HtmlTextWriter(stringWrite1);

        ////render html content to textwriter
        //gvLista.RenderControl(htmlWrite1);
        //Response.Write(stringWrite1.ToString());
        //Response.End();


        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.IO.StringWriter sw = new System.IO.StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page page = new Page();
        System.Web.UI.HtmlControls.HtmlForm form = new System.Web.UI.HtmlControls.HtmlForm();

        gvLista.AllowPaging = false;
        Actualizar();
        gvLista.EnableViewState = false;
        // Deshabilitar la validación de eventos, sólo asp.net 2
        page.EnableEventValidation = false;
        // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
        page.DesignerInitialize();
        page.Controls.Add(form);
        form.Controls.Add(gvLista);
        page.RenderControl(htw);
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=datoscreditos.xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }



}