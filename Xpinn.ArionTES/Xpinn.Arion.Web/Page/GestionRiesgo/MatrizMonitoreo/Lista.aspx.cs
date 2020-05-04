using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Riesgo.Services;
using Xpinn.Riesgo.Entities;
using System.IO;
using System.Text;

public partial class Lista : GlobalWeb
{
    IdentificacionServices identificacionServicio = new IdentificacionServices();
    SeguimientoServices seguimientoServicio = new SeguimientoServices();
    MatrizServices matrizServicio = new MatrizServices();
    PoblarListas poblar = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(matrizServicio.CodigoProgramaM, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            ctlmensaje.eventoClick += btnContinuar_Click;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(matrizServicio.CodigoProgramaM, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarLista();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(matrizServicio.CodigoProgramaM, "Page_Load", ex);
        }
    }


    #region Métodos de carga de datos

    private void CargarLista()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("GR_RIESGO_GENERAL", "COD_RIESGO, DESCRIPCION ||'-'|| SIGLA AS DESCRIPCION", "", "1", ddlSistemaRiesgo, (Usuario)Session["usuario"]);
    }


    protected List<Seguimiento> ListaTipoMonitoreo()
    {
        List<Seguimiento> lstMonitoreo = new List<Seguimiento>();
        Seguimiento pMonitoreo = new Seguimiento();        
        lstMonitoreo = seguimientoServicio.ListarTiposMonitoreo(pMonitoreo, (Usuario)Session["usuario"]);
        return lstMonitoreo;
    }

    /// <summary>
    /// Validar que no hayan campos vacios
    /// </summary>
    /// <returns></returns>
    private bool ValidarMatriz()
    {
        List<Matriz> lstMatriz = new List<Matriz>();
        ObtenerLista();
        lstMatriz = ViewState["lstMatriz"] != null ? (List<Matriz>)ViewState["lstMatriz"] : null;
        if (lstMatriz != null)
        {
            //Validar que cada registro tenga todos los valores
            foreach (Matriz pMatriz in lstMatriz)
            {
                if (pMatriz.cod_monitoreo == 0)
                {
                    VerError("Debe ingresar el parámetro de monitoreo en cada registro");
                    return false;
                }
            }
        }
        else
        {
            VerError("No hay datos para guardar");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Cargar datos si ya hay una matriz existente
    /// </summary>
    private void Actualizar()
    {
        List<Matriz> lstMatriz = new List<Matriz>();

        if (ddlSistemaRiesgo.SelectedValue != "")
        {
            lstMatriz = matrizServicio.ListarMatrizMonitoreo(Convert.ToInt64(ddlSistemaRiesgo.SelectedValue), "", (Usuario)Session["usuario"]);
            if (lstMatriz.Count > 0)
            {
                ViewState["lstMatriz"] = lstMatriz;
                gvMatriz.DataSource = lstMatriz;
                gvMatriz.DataBind();
            }
            else
            {
                ViewState["lstMatriz"] = null;
                gvMatriz.DataSource = null;
                gvMatriz.DataBind();
                VerError("No se encuentra registrada la matriz de riesgo residual");
            }
        }
        if (gvMatriz.Rows.Count > 0)
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
            toolBar.MostrarExportar(true);
        }
    }

    /// <summary>
    /// Obtener los registros de la matriz
    /// </summary>
    /// <returns>Lista con los registros</returns>
    private void ObtenerLista()
    {
        try
        {
            List<Matriz> lstMatriz = new List<Matriz>();
            bool modificar = false;
            int contador = 0;

            //Verificar que la sesion no se encuentre vacia
            if (ViewState["lstMatriz"] != null)
            {
                modificar = true;
                lstMatriz = (List<Matriz>)ViewState["lstMatriz"];
            }

            //Verificar página actual de la grilla
            foreach (GridViewRow rFila in gvMatriz.Rows)
            {
                int rowindex = -1;
                Matriz objDetalle = new Matriz();

                //Obtener el objeto correspondiente a la fila y actualizarlo con los datos
                if (modificar == true)
                {
                    rowindex = (gvMatriz.PageIndex * gvMatriz.PageSize) + contador;
                    if (rowindex < lstMatriz.Count)
                        objDetalle = lstMatriz[rowindex];
                    else
                        objDetalle = null;
                }
                if (modificar == false || (modificar == true && objDetalle != null))
                {

                    Int64 cod = Convert.ToInt32(gvMatriz.DataKeys[rFila.RowIndex].Values[0]);
                    DropDownListGrid ddlTipoM = (DropDownListGrid)rFila.FindControl("ddlTipoM");
                    Label lbRiesgoI = (Label)rFila.FindControl("lbRiesgoI");
                    Label lbValorC = (Label)rFila.FindControl("lbValorC");
                    Label lbRiesgoR = (Label)rFila.FindControl("lbRiesgoR");
                    Label lbEstadoA = (Label)rFila.FindControl("lbEstadoA");

                    objDetalle.cod_matriz = Convert.ToInt32(gvMatriz.DataKeys[rFila.RowIndex].Values[0]);
                    objDetalle.cod_factor = Convert.ToInt64(rFila.Cells[0].Text);
                    objDetalle.desc_factor = HttpUtility.HtmlDecode(rFila.Cells[1].Text);
                    objDetalle.valor_rinherente = Convert.ToInt64(lbRiesgoI.Text);
                    objDetalle.valor_control = lbValorC.Text != null && lbValorC.Text != "" ? Convert.ToInt32(lbValorC.Text) : 0;
                    objDetalle.valor_rresidual = lbRiesgoR.Text != null && lbRiesgoR.Text != "" ? Convert.ToInt32(lbRiesgoR.Text) : 0;
                    objDetalle.cod_monitoreo = ddlTipoM.SelectedValue != "0" ? Convert.ToInt64(ddlTipoM.SelectedValue) : 0;
                    objDetalle.cod_alerta = lbEstadoA.Text != "" && lbEstadoA.Text != null ? Convert.ToInt64(lbEstadoA.Text) : 0;
                    objDetalle.descripcion = HttpUtility.HtmlDecode(rFila.Cells[7].Text);
                    objDetalle.desc_area = HttpUtility.HtmlDecode(rFila.Cells[8].Text);
                    objDetalle.desc_cargo = HttpUtility.HtmlDecode(rFila.Cells[9].Text);
                    objDetalle.cod_riesgo = Convert.ToInt64(ddlSistemaRiesgo.SelectedValue);

                    if (!modificar)
                        lstMatriz.Add(objDetalle);
                }
                contador += 1;
            }

            ViewState["lstMatriz"] = lstMatriz;
        }
        catch (Exception ex)
        {
            VerError("Error al obtener la lista de datos " + ex.Message);
        }

    }
    #endregion

    #region Eventos de botones

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarMatriz())
                ctlmensaje.MostrarMensaje("¿Desea registrar la matriz?");
        }
        catch (Exception ex)
        {
            VerError(matrizServicio.CodigoProgramaM + ex.Message);
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            List<Matriz> lstMatriz = new List<Matriz>();
            ObtenerLista();
            lstMatriz = ViewState["lstMatriz"] != null ? (List<Matriz>)ViewState["lstMatriz"] : null;
            if (lstMatriz.Count > 0)
            {
                bool exitoso = matrizServicio.MatrizParametroMonitoreo(lstMatriz, (Usuario)Session["usuario"]);
                if (exitoso)
                {
                    lblMensaje.Visible = true;
                    lblMensaje.Text = "Matriz registrada correctamente";
                }
            }
        }
        catch (Exception ex)
        {
            VerError(matrizServicio.CodigoProgramaM + ex.Message);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    /// <summary>
    /// Evento para exportar a Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvMatriz.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=MatrizRResidual.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            ObtenerLista();
            GridView gvExportar = gvMatriz;
            gvExportar.AllowPaging = false;
            gvExportar.DataSource = (List<Matriz>)ViewState["lstMatriz"];
            gvExportar.DataBind();
            sw = ObtenerGrilla(gvExportar);
            Response.Output.Write("<div>" + sw.ToString() + "</div>");
            Response.Flush();
            Response.End();
        }
    }
    #endregion

    #region Eventos GridView

    protected void gvMatriz_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            List<Matriz> lstMatriz = ViewState["lstMatriz"] != null ? (List<Matriz>)ViewState["lstMatriz"] : null;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int cod = Convert.ToInt32(gvMatriz.DataKeys[e.Row.RowIndex].Values[0]);
                Label lbRiesgoI = (Label)e.Row.FindControl("lbRiesgoI");
                Label lbRiesgoR = (Label)e.Row.FindControl("lbRiesgoR");

                //Cargar descripción y color para la calificación del riesgo inherente
                Matriz dataItem = e.Row.DataItem as Matriz;
                if (dataItem.valor_rinherente > 0)
                {
                    TextBox txtRiesgoIn = (TextBox)e.Row.FindControl("txtRiesgoI");
                    txtRiesgoIn.Text = matrizServicio.ColoresRInherente[dataItem.valor_rinherente].descripcion;
                    txtRiesgoIn.BackColor = System.Drawing.Color.FromName(matrizServicio.ColoresRInherente[dataItem.valor_rinherente].nivel);
                }

                //Cargar valoración control
                if (dataItem.valor_control > 0)
                {
                    TextBox txtValorC = (TextBox)e.Row.FindControl("txtValorC");
                    txtValorC.Text = matrizServicio.ColoresVControl[dataItem.valor_control].descripcion;
                    txtValorC.BackColor = System.Drawing.Color.FromName(matrizServicio.ColoresVControl[dataItem.valor_control].nivel);
                }

                //Cargar riesgo residual
                if (dataItem.valor_rresidual > 0)
                {
                    TextBox txtRiesgoR = (TextBox)e.Row.FindControl("txtRiesgoR");
                    Matriz pValorResidual = new Matriz();
                    pValorResidual = matrizServicio.ColoresRResidual.Where(x => dataItem.valor_rresidual <= x.valor_rresidual && x.valor_rresidual > 0).FirstOrDefault();
                    txtRiesgoR.Text = pValorResidual.descripcion;
                    txtRiesgoR.BackColor = System.Drawing.Color.FromName(pValorResidual.nivel);
                }

                //Cargar alerta
                if (dataItem.cod_alerta > 0)
                {
                    TextBox txtEstadoA = (TextBox)e.Row.FindControl("txtEstadoA");
                    Label lbEstadoA = (Label)e.Row.FindControl("lbEstadoA");

                    txtEstadoA.Text = matrizServicio.ColoresAlerta[dataItem.cod_alerta].descripcion;
                    txtEstadoA.BackColor = System.Drawing.Color.FromName(matrizServicio.ColoresAlerta[dataItem.cod_alerta].nivel);
                }
            }
        }
        catch (Exception ex)
        {
            VerError(matrizServicio.CodigoProgramaM + "gvMatriz_RowDataBound" + ex.Message);
        }
    }

    protected void gvMatriz_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            List<Matriz> lstMatriz = new List<Matriz>();
            ObtenerLista();
            lstMatriz = ViewState["lstMatriz"] != null ? (List<Matriz>)ViewState["lstMatriz"] : null;
            if (lstMatriz != null)
            {
                gvMatriz.PageIndex = e.NewPageIndex;
                gvMatriz.DataSource = lstMatriz;
                gvMatriz.DataBind();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(matrizServicio.CodigoProgramaM, "gvMatriz_PageIndexChanging", ex);
        }
    }

    #endregion

    #region Eventos DropDownList

    protected void ddlTipoM_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlTipoM = (DropDownList)sender;
        GridViewRow rFila = (GridViewRow)ddlTipoM.NamingContainer;
        Label lbEstadoA = (Label)rFila.FindControl("lbEstadoA");
        TextBox txtEstadoA = (TextBox)rFila.FindControl("txtEstadoA");

        Seguimiento pMonitoreo = new Seguimiento();
        pMonitoreo.cod_monitoreo = Convert.ToInt64(ddlTipoM.SelectedValue);
        pMonitoreo = seguimientoServicio.ListarTiposMonitoreo(pMonitoreo, (Usuario)Session["usuario"]).FirstOrDefault();
        lbEstadoA.Text = pMonitoreo.cod_alerta.ToString();
        rFila.Cells[7].Text = pMonitoreo.nom_periodicidad;
        rFila.Cells[8].Text = pMonitoreo.nom_area;
        rFila.Cells[9].Text = pMonitoreo.nom_cargo;
        
        txtEstadoA.Text = matrizServicio.ColoresAlerta[pMonitoreo.cod_alerta].descripcion;
        txtEstadoA.BackColor = System.Drawing.Color.FromName(matrizServicio.ColoresAlerta[pMonitoreo.cod_alerta].nivel);
    }


    #endregion



    /// <summary>
    /// Cargar grilla sin controles internos
    /// </summary>
    /// <param name="GridView1">Grilla a cargar</param>
    /// <returns></returns>             
    public StringWriter ObtenerGrilla(GridView GridView1)
    {
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            foreach (GridViewRow row in GridView1.Rows)
            {
                //row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    List<Control> lstControls = new List<Control>();

                    //Agregar controles para luego removerlos
                    foreach (Control control in cell.Controls)
                    {
                        lstControls.Add(control);
                    }
                    //Consultar controles para reemplazarlos
                    foreach (Control control in lstControls)
                    {
                        switch (control.GetType().Name)
                        {
                            case "TextBox":
                                cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                cell.BackColor = (control as TextBox).BackColor;
                                break;
                            case "DropDownListGrid":
                                cell.Controls.Add(new Literal { Text = (control as DropDownListGrid).SelectedItem.Text });
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
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }



}