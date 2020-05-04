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
    private Xpinn.Ahorros.Services.AhorroVistaServices AhorroVistaServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    private Xpinn.Ahorros.Services.LineaAhorroServices LineaAhorroServicio = new Xpinn.Ahorros.Services.LineaAhorroServices();
    PoblarListas Poblar = new PoblarListas();
    private Xpinn.Ahorros.Entities.AhorroVista AhorrosVista = new Xpinn.Ahorros.Entities.AhorroVista();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AhorroVistaServicio.CodigoProgramaCie, "L");

            Site toolBar = (Site)this.Master;
           // toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnImprimir_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaCie, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
          
            if (!IsPostBack)
            {
                cargarDropdown(); 
                //CargarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaCie);
                if (Session[AhorroVistaServicio.CodigoProgramaCie + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaCie, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session[AhorroVistaServicio.CodigoProgramaCie + ".id"] = null;
        GuardarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaCie);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaCie);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        LimpiarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaCie);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaCie + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoProgramaCie + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoProgramaCie + ".id"] = id;
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
            //BOexcepcion.Throw(AhorroVistaervicio.CodigoProgramaCie, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaCie, "gvLista_PageIndexChanging", ex);
        }
    }

    private string obtFiltro()
    {
        String filtro = String.Empty;

        if (txtCodigo.Text.Trim() != "")
            filtro += " AND A.NUMERO_CUENTA = '" + txtCodigo.Text + "'";

        if (txtCliente.Text.Trim() != "")
            filtro += " AND  p.nombre  like  '%" + txtCliente.Text + "'";

        if (txtIdentificacion.Text.Trim() != "")
            filtro += " AND  p.IDENTIFICACION  = '" + txtIdentificacion.Text + "'";

        if (this.ddlTipoLinea.SelectedIndex != 0)
            filtro += " AND  a.cod_linea_ahorro = " + ddlTipoLinea.SelectedValue ;
       
        if (this.ddlOficina.SelectedIndex != 0)
            filtro += " AND  a.cod_oficina = " + ddlOficina.SelectedValue;

        if (txtCodigoNomina.Text.Trim() != "")
            filtro += " AND  p.cod_nomina  = '" + txtCodigoNomina.Text + "'";
        if (chkTarjeta.Checked)
            filtro += "AND exists (select * from TARJETA T where T.NUMERO_CUENTA=a.NUMERO_CUENTA)";
        if (chkCtaCerradas.Checked)
            filtro += "AND A.ESTADO =3";

        filtro += " AND A.estado !=2";


        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "WHERE " + filtro;
        }

        return filtro;
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            List<Xpinn.Ahorros.Entities.AhorroVista> lstConsulta = new List<Xpinn.Ahorros.Entities.AhorroVista>();
            string pFiltro = obtFiltro();

            lstConsulta = AhorroVistaServicio.ListarAhorroVista(pFiltro,DateTime.MinValue, (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTAhorroVista"] = lstConsulta;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(AhorroVistaServicio.CodigoProgramaCie + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaCie, "Actualizar", ex);
        }
    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Ahorros.xls");
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }


    void cargarDropdown()
    {
         Poblar.PoblarListaDesplegable("lineaahorro", " COD_LINEA_AHORRO,DESCRIPCION", "", "1", ddlTipoLinea, (Usuario)Session["usuario"]);
        Poblar.PoblarListaDesplegable("OFICINA", " COD_OFICINA,NOMBRE", " estado = 1", "1", ddlOficina, (Usuario)Session["usuario"]);
    }
    
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
        Response.Charset = "UTF-16";
        Response.ContentEncoding = Encoding.Default;
        Response.ContentType = "application/vnd.ms-excel";
        gvLista.Columns[0].Visible = false;
        gvLista.Columns[1].Visible = false;
        gvLista.Columns[2].Visible = false;
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            StringBuilder sb = new StringBuilder();
            //To Export all pages
            gvLista.AllowPaging = false;
            this.Actualizar();
        
          

            gvLista.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(sb);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

}