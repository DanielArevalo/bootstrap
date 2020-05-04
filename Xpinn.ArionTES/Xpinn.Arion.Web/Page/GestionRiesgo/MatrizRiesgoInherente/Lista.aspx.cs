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
    MatrizServices matrizServicio = new MatrizServices();
    PoblarListas poblar = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(matrizServicio.CodigoProgramaI, "L");
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
            BOexcepcion.Throw(matrizServicio.CodigoProgramaI, "Page_PreInit", ex);
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
            BOexcepcion.Throw(matrizServicio.CodigoProgramaI, "Page_Load", ex);
        }
    }


    #region Métodos de carga de datos

    private void CargarLista()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("GR_RIESGO_GENERAL", "COD_RIESGO, DESCRIPCION ||'-'|| SIGLA AS DESCRIPCION", "", "1", ddlSistemaRiesgo, (Usuario)Session["usuario"]);
    }

    /// <summary>
    /// Cargar listado de factores para llenar DDL contenido en gridview
    /// </summary>
    /// <returns>Listado de factores</returns>
    protected List<Identificacion> ListaFactor()
    {
        List<Identificacion> lstFactores = new List<Identificacion>();
        Identificacion pFactor = new Identificacion();
        pFactor.cod_riesgo = Convert.ToInt64(ddlSistemaRiesgo.SelectedValue);
        lstFactores = identificacionServicio.ListarFactoresRiesgo(pFactor, "", (Usuario)Session["usuario"]);
        return lstFactores;
    }

    /// <summary>
    /// Cargar listado de causas para llenar DDL contenido en gridview
    /// </summary>
    /// <returns>Listado de causas</returns>
    protected List<Identificacion> ListaCausa()
    {
        List<Identificacion> lstCausas = new List<Identificacion>();
        lstCausas = identificacionServicio.ListarCausas(null, "", (Usuario)Session["usuario"]);
        return lstCausas;
    }

    /// <summary>
    /// Cargar listado de niveles de probabilidad para llenar DDL contenido en gridview
    /// </summary>
    /// <returns>Listado de niveles de probabilidad</returns>
    protected List<Matriz> ListaProbabilidad()
    {
        List<Matriz> lstProbabilidad = new List<Matriz>();
        lstProbabilidad = matrizServicio.ListarProbabilidad((Usuario)Session["usuario"]);
        return lstProbabilidad;
    }

    /// <summary>
    /// Cargar listado de niveles de impacto para llenar DDL contenido en gridview
    /// </summary>
    /// <returns>Listado de niveles de impacto</returns>
    protected List<Matriz> ListaImpacto()
    {
        List<Matriz> lstImpacto = new List<Matriz>();
        lstImpacto = matrizServicio.ListarImpacto((Usuario)Session["usuario"]);
        return lstImpacto;
    }

    /// <summary>
    /// Validar que no hayan campos repetidos o vacios
    /// </summary>
    /// <returns></returns>
    private bool ValidarMatriz()
    {
        ObtenerLista();
        List<Matriz> lstMatriz = new List<Matriz>();
        lstMatriz = (List<Matriz>)ViewState["lstMatriz"];

        //Validar que cada registro tenga todos los valores
        foreach (Matriz pRegistro in lstMatriz)
        {
            if (pRegistro.cod_factor == 0)
            {
                VerError("Debe ingresar el factor de riesgo en cada registro");
                return false;
            }
            if (pRegistro.cod_causa == 0)
            {
                VerError("Debe ingresar la causa de riesgo en cada registro");
                return false;
            }
            if(pRegistro.cod_probabilidad == 0)
            {
                VerError("Debe ingresar el nivel de probabilidad en cada registro");
                return false;
            }
            if(pRegistro.cod_impacto == 0)
            {
                VerError("Debe ingresar el nivel de impacto en cada registro");
                return false;
            }
        }

        //Validar que no existan combinaciones factor-causa repetidas
        
        Dictionary<Int64, List<Int64>> lista = new Dictionary<Int64, List<Int64>>();
        

        foreach (Matriz obj in lstMatriz)
        {
            if (!lista.ContainsKey(obj.cod_factor))
            {
                lista.Add(obj.cod_factor, new List<Int64> { obj.cod_causa });
            }
            else
            {
                bool causa = lista.FirstOrDefault(t => t.Key == obj.cod_factor).Value.Contains(obj.cod_causa);
                if (!causa)
                    lista.FirstOrDefault(t => t.Key == obj.cod_factor).Value.Add(obj.cod_causa);
                else
                {
                    VerError("El registro con factor: " + obj.cod_factor + " con causa: " + obj.cod_causa + " ya se encuentra ingresado");
                    return false;
                }
            }
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
            VerError("");
            lstMatriz = matrizServicio.ListarMatriz(Convert.ToInt64(ddlSistemaRiesgo.SelectedValue), "", (Usuario)Session["usuario"]);
            if (lstMatriz.Count > 0)
            {
                ViewState["lstMatriz"] = lstMatriz;
                gvMatriz.DataSource = lstMatriz;
                gvMatriz.DataBind();
                btnDetalle.Visible = true;
            }
            else
            {
                ViewState["lstMatriz"] = null;
                gvMatriz.DataSource = null;
                gvMatriz.DataBind();
                btnDetalle.Visible = false;
                VerError("No hay factores de riesgo asociados al sistema seleccionado");
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
                    DropDownListGrid ddlFactor = (DropDownListGrid)rFila.FindControl("ddlFactor");
                    DropDownListGrid ddlCausa = (DropDownListGrid)rFila.FindControl("ddlCausa");
                    DropDownListGrid ddlProbabilidad = (DropDownListGrid)rFila.FindControl("ddlProbabilidad");
                    DropDownListGrid ddlImpacto = (DropDownListGrid)rFila.FindControl("ddlImpacto");
                    HiddenField hbValorRiesgo = (HiddenField)rFila.FindControl("hdValorRiesgo");

                    objDetalle.cod_matriz = cod != 0 ? cod : 0;
                    objDetalle.cod_factor = ddlFactor.SelectedValue != "" ? Convert.ToInt64(ddlFactor.SelectedValue) : 0;
                    objDetalle.cod_causa = ddlCausa.SelectedValue != "" ? Convert.ToInt64(ddlCausa.SelectedValue) : 0;
                    objDetalle.cod_probabilidad = ddlProbabilidad.SelectedValue != "" ? Convert.ToInt64(ddlProbabilidad.SelectedValue) : 0;
                    objDetalle.cod_impacto = ddlImpacto.SelectedValue != "" ? Convert.ToInt64(ddlImpacto.SelectedValue) : 0;
                    objDetalle.valor_rinherente = hbValorRiesgo.Value != "" ? Convert.ToInt64(hbValorRiesgo.Value) : 0;
                    objDetalle.desc_factor = HttpUtility.HtmlDecode(rFila.Cells[2].Text);
                    objDetalle.descripcion = HttpUtility.HtmlDecode(rFila.Cells[4].Text);
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
            VerError(matrizServicio.CodigoProgramaI + ex.Message);
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            List<Matriz> lstMatriz = new List<Matriz>();
            ObtenerLista();
            lstMatriz = ViewState["lstMatriz"] != null ? (List<Matriz>)ViewState["lstMatriz"] : null;
            if (lstMatriz != null)
            {
                if (lstMatriz.Count > 0)
                {
                    bool exitoso = matrizServicio.CrearMatriz(lstMatriz, (Usuario)Session["usuario"]);
                    if (exitoso)
                    {
                        lblMensaje.Visible = true;
                        lblMensaje.Text = "Matriz registrada correctamente";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            VerError(matrizServicio.CodigoProgramaI + ex.Message);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Actualizar();
    }

    /// <summary>
    /// Evento para agregar un nuevo detalle a la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        List<Matriz> lstMatriz = new List<Matriz>();
        Matriz pMatriz = new Matriz();

        ObtenerLista();
        lstMatriz = ViewState["lstMatriz"] != null ? (List<Matriz>)ViewState["lstMatriz"] : null;
        lstMatriz.Add(pMatriz);
        ViewState["lstMatriz"] = lstMatriz;
        gvMatriz.DataSource = lstMatriz;
        gvMatriz.DataBind();
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
            Response.AddHeader("Content-Disposition", "attachment;filename=MatrizRInherente.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            ExpGrilla expGrilla = new ExpGrilla();
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
                DropDownListGrid ddlProbabilidad = (DropDownListGrid)e.Row.FindControl("ddlProbabilidad");
                DropDownListGrid ddlImpacto = (DropDownListGrid)e.Row.FindControl("ddlImpacto");

                if (ddlProbabilidad.SelectedValue != "0" && ddlImpacto.SelectedValue != "0")
                {
                    int calificacion = 0;

                    //Consultar valor para cargar descripción y color según la matriz de calor
                    calificacion = matrizServicio.ConsultarCalificacion(Convert.ToInt64(ddlProbabilidad.SelectedValue), Convert.ToInt64(ddlImpacto.SelectedValue), (Usuario)Session["usuario"]);
                    TextBox txtRiesgoI = (TextBox)e.Row.FindControl("txtRiesgoI");
                    txtRiesgoI.Text = matrizServicio.ColoresRInherente[calificacion].descripcion;
                    txtRiesgoI.BackColor = System.Drawing.Color.FromName(matrizServicio.ColoresRInherente[calificacion].nivel);

                    //Cargar valor de la calificación en un hidden para poder guardar
                    HiddenField hdValorRiesgo = (HiddenField)e.Row.FindControl("hdValorRiesgo");
                    hdValorRiesgo.Value = calificacion.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            VerError(matrizServicio.CodigoProgramaI + "gvMatriz_RowDataBound" + ex.Message);
        }
    }

    protected void gvMatriz_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Seguimiento pControl = new Seguimiento();
            MatrizServices matrizServicio = new MatrizServices();
            List<Matriz> lstMatriz = new List<Matriz>();

            Int64 cod_matriz = Convert.ToInt64(gvMatriz.DataKeys[e.RowIndex].Values[0]);
            DropDownListGrid ddlFactor = (DropDownListGrid)gvMatriz.Rows[e.RowIndex].FindControl("ddlFactor");
            string filtro = " WHERE COD_FACTOR = " + ddlFactor.SelectedValue + " AND VALOR_RRESIDUAL > 0";
            lstMatriz = matrizServicio.ListarMatrizRResidual(0, filtro, (Usuario)Session["usuario"]);
            if (lstMatriz.Count > 0 && cod_matriz > 0)
            {
                VerError("No es posible eliminar el registro, el factor se encuentra asociado en la matriz de  riesgo residual");
            }
            else if (cod_matriz > 0)
            {
                matrizServicio.EliminarMatrizRInherente(cod_matriz, (Usuario)Session["usuario"]);
                Actualizar();
            }
            else
            {
                //List<Matriz> lstMatriz = new List<Matriz>();
                lstMatriz = (List<Matriz>)ViewState["lstMatriz"];
                lstMatriz.RemoveAt((gvMatriz.PageIndex * gvMatriz.PageSize) + e.RowIndex);
                gvMatriz.DataSource = lstMatriz;
                gvMatriz.DataBind();
            }
        }
        catch (Exception ex)
        {
            VerError(matrizServicio.CodigoProgramaI + "gvCausaRiesgo_RowDeleting" + ex.Message);
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
            BOexcepcion.Throw(matrizServicio.CodigoProgramaI, "gvMatriz_PageIndexChanging", ex);
        }
    }

    #endregion

    #region Eventos DropDownList

    protected void ddlCausa_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlCausa = (DropDownList)sender;

        if (ddlCausa != null)
        {
            GridViewRow rFila = (GridViewRow)ddlCausa.NamingContainer;
            Identificacion pCausa = new Identificacion();
            pCausa.cod_causa = Convert.ToInt64(ddlCausa.SelectedValue);
            pCausa = identificacionServicio.ConsultarCausa(pCausa, (Usuario)Session["usuario"]);
            rFila.Cells[4].Text = pCausa.descripcion;
        }
    }

    protected void ddlFactor_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlFactor = (DropDownList)sender;

        if (ddlFactor != null)
        {
            GridViewRow rFila = (GridViewRow)ddlFactor.NamingContainer;
            Identificacion pFactor = new Identificacion();
            pFactor.cod_factor = Convert.ToInt64(ddlFactor.SelectedValue);
            pFactor = identificacionServicio.ConsultarFactorRiesgo(pFactor, (Usuario)Session["usuario"]);
            rFila.Cells[2].Text = pFactor.descripcion;
        }
    }

    protected void ddlImpacto_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlImpacto = (DropDownList)sender;
        GridViewRow rFila = (GridViewRow)ddlImpacto.NamingContainer;
        DropDownList ddlProbabilidad = (DropDownList)rFila.FindControl("ddlProbabilidad");
        
        if (ddlImpacto.SelectedValue != "" && ddlProbabilidad.SelectedValue != "")
        {
            DeterminarEscala(Convert.ToInt64(ddlImpacto.SelectedValue), Convert.ToInt64(ddlProbabilidad.SelectedValue), rFila.RowIndex);
        }
    }

    protected void ddlProbabilidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlProbabilidad = (DropDownList)sender;
        GridViewRow rFila = (GridViewRow)ddlProbabilidad.NamingContainer;
        DropDownList ddlImpacto = (DropDownList)rFila.FindControl("ddlImpacto");
        
        if (ddlImpacto.SelectedValue != "" && ddlProbabilidad.SelectedValue != "")
        {
            DeterminarEscala(Convert.ToInt64(ddlImpacto.SelectedValue), Convert.ToInt64(ddlProbabilidad.SelectedValue), rFila.RowIndex);
        }
    }

    #endregion

    /// <summary>
    /// Determinar el riesgo inherente 
    /// </summary>
    /// <param name="cod_probabilidad">Nivel de probabilidad</param>
    /// <param name="cod_impacto">Nivel de impacto</param>
    /// <param name="indice">Indice de la fila en la grilla</param>
    private void DeterminarEscala(Int64 cod_probabilidad, Int64 cod_impacto, int indice)
    {
        try
        {
            MatrizServices matrizServicio = new MatrizServices();
            GridViewRow rfila = gvMatriz.Rows[indice];
            int calificacion = 0;

            //Consultar valor para cargar descripción y color según la matriz de calor
            calificacion = matrizServicio.ConsultarCalificacion(cod_probabilidad, cod_impacto, (Usuario)Session["usuario"]);
            TextBox txtRiesgoI = (TextBox)rfila.FindControl("txtRiesgoI");
            txtRiesgoI.Text = matrizServicio.ColoresRInherente[calificacion].descripcion;
            txtRiesgoI.BackColor = System.Drawing.Color.FromName(matrizServicio.ColoresRInherente[calificacion].nivel);

            //Cargar valor de la calificación en un hidden para poder guardar
            HiddenField hdValorRiesgo = (HiddenField)rfila.FindControl("hdValorRiesgo");
            hdValorRiesgo.Value = calificacion.ToString();

        }
        catch (Exception ex)
        {
            VerError("Error al definir la calificación del riesgo" + ex.Message);
        }
    }

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
            //No visualizar columna de eliminación
            GridView1.Columns[0].Visible = false;
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