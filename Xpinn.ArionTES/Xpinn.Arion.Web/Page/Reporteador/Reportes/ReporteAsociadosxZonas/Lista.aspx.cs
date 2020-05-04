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
using Xpinn.Riesgo.Services;
using Xpinn.Riesgo.Entities;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{

    private Xpinn.Riesgo.Services.ReporteProductoServices ReporteService = new Xpinn.Riesgo.Services.ReporteProductoServices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            //if (Session[ReporteService.CodigoPrograma + ".id"] != null)
            //    VisualizarOpciones(ReporteService.CodigoPrograma, "E");
            //else
            //    VisualizarOpciones(ReporteService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;           
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {           
            if (!IsPostBack)
            {
                txtFechaIni.Text = "";
                txtFechaFin.Text = "";
                LlenarComboOficinas(ddlOficinas);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void LlenarComboOficinas(DropDownList ddlOficinas)
    {
        Xpinn.FabricaCreditos.Services.OficinaService oficinaService = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        Usuario usuap = (Usuario)Session["usuario"];
        ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        ddlOficinas.Items.Insert(1, new ListItem(Convert.ToString(usuap.nombre_oficina), Convert.ToString(usuap.cod_oficina)));
        ddlOficinas.DataBind();
        ddlOficinas.SelectedValue = Convert.ToString(usuap.cod_oficina);
        ddlOficinas.Enabled = false;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.GetType().Name + "L", "btnConsultar_Click", ex);
        }
    }


    private void Actualizar()
    {
        try
        {
            Configuracion conf = new Configuracion();
            List<ReporteProducto> lstConsulta = new List<ReporteProducto>();
            ReporteProducto entidad = new ReporteProducto();
            DateTime? pFechaIni = null, pFechaFin = null;
            if (txtFechaIni.TieneDatos) pFechaIni = txtFechaIni.ToDateTime;
            if (txtFechaFin.TieneDatos) pFechaFin = txtFechaFin.ToDateTime;
            string filtro = "";
            if (txtIdentificacion.Text.Trim() != "")
                filtro += " AND p.identificacion = '" + txtIdentificacion.Text.Trim() + "' ";
            if (ddlOficinas.SelectedValue != "0")
                filtro += " AND p.cod_oficina = " + ddlOficinas.SelectedValue;
            if (ddlEstado.SelectedValue != "")
                filtro += " AND f.estado = '" + ddlEstado.SelectedValue + "'";

            lstConsulta = ReporteService.ListarReporteProducto(entidad, pFechaIni, pFechaFin, filtro, (Usuario)Session["usuario"]);
            if (lstConsulta.Count > 0)
            {
                pListado.Visible = true;
                gvProductos.DataSource = lstConsulta;
                gvProductos.DataBind();
                lblInfo.Visible = false;
                Session["LSTCONSULTA"] = lstConsulta;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
            }
            else
            {
                pListado.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(ReporteService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteService.GetType().Name + "L", "Actualizar", ex);
        }
    }



    protected Boolean ValidarDatos()
    {
        VerError("");
        if (txtFechaIni.Text == "")
        {
            VerError("Seleccione la fecha Inicial");
            return false;
        }
        if (txtFechaFin.Text == "")
        {
            VerError("Seleccione la fecha Final");
            return false;
        }
        if (txtFechaIni.Text != "" && txtFechaFin.Text != "")
        {
            if (Convert.ToDateTime(txtFechaIni.Text) > Convert.ToDateTime(txtFechaFin.Text))
            {
                VerError("No puede Ingresar una Fecha inicial mayor a la fecha final");
                return false;
            }
        }
        return true;
    }


    protected string ReemplazarTextos(String pTexto)
    {
        return pTexto.Replace(";", "").Replace("&#201", "E").Replace("&#193", "A").Replace("&#211", "O").Replace("&#243", "o").Replace("&#205", "I").Replace("&nbsp", "").Replace("&#209", "N");
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvProductos.Rows.Count > 0 && Session["LSTCONSULTA"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvProductos);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteProductos.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void gvProductos_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Int64? cod_persona = null;
            List<producto> lstProductos = null;
            cod_persona = ConvertirStringToInt(e.Row.Cells[0].Text);
            if (cod_persona > 0)
            {
                DateTime? pFechaIni = null;
                DateTime? pFechaFin = null;

                lstProductos = ReporteService.ListarProductoSaldoXAsociado(Convert.ToInt64(cod_persona), pFechaIni, pFechaFin, (Usuario)Session["usuario"]);
                if (lstProductos != null)
                {
                    foreach (producto pProducto in lstProductos)
                    {
                        if (pProducto.tipoproducto == "Aportes")
                            e.Row.Cells[14].Text = pProducto.monto.ToString("C");
                        if (pProducto.tipoproducto == "Aportes")
                            e.Row.Cells[15].Text = pProducto.cuota.ToString("C");
                        if (pProducto.tipoproducto == "Créditos")
                            e.Row.Cells[16].Text = pProducto.monto.ToString("C");
                        if (pProducto.tipoproducto == "Créditos")
                            e.Row.Cells[17].Text = pProducto.cuota.ToString("C");
                        if (pProducto.tipoproducto == "Ahorros Vista")
                            e.Row.Cells[18].Text = pProducto.monto.ToString("C");
                        if (pProducto.tipoproducto == "Ahorros Vista")
                            e.Row.Cells[19].Text = pProducto.cuota.ToString("C");
                        if (pProducto.tipoproducto == "CDATS")
                            e.Row.Cells[20].Text = pProducto.monto.ToString("C");
                        if (pProducto.tipoproducto == "CDATS")
                            e.Row.Cells[21].Text = pProducto.cuota.ToString("C");
                        if (pProducto.tipoproducto == "Ahorros Programado")
                            e.Row.Cells[22].Text = pProducto.monto.ToString("C");
                        if (pProducto.tipoproducto == "Ahorros Programado")
                            e.Row.Cells[23].Text = pProducto.cuota.ToString("C");
                        if (pProducto.tipoproducto == "Servicios")
                            e.Row.Cells[24].Text = pProducto.monto.ToString("C");
                        if (pProducto.tipoproducto == "Servicios")
                            e.Row.Cells[25].Text = pProducto.cuota.ToString("C");
                    }
                }
                /*GridView gvSaldos = (GridView)e.Row.FindControl("gvSaldos");
                if (gvSaldos != null)
                { 
                    
                    gvSaldos.DataSource = ReporteService.ListarProductoSaldo(Convert.ToInt64(cod_persona), pFechaIni, pFechaFin, (Usuario)Session["usuario"]);
                    gvSaldos.DataBind();
                }*/
            }
        }
    }

}