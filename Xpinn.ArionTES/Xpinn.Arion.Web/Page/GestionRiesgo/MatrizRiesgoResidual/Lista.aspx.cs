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
            VisualizarOpciones(matrizServicio.CodigoProgramaC, "L");
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
            BOexcepcion.Throw(matrizServicio.CodigoProgramaC, "Page_PreInit", ex);
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
            BOexcepcion.Throw(matrizServicio.CodigoProgramaC, "Page_Load", ex);
        }
    }


    #region Métodos de carga de datos

    private void CargarLista()
    {
        PoblarListas poblar = new PoblarListas();
        poblar.PoblarListaDesplegable("GR_RIESGO_GENERAL", "COD_RIESGO, DESCRIPCION ||'-'|| SIGLA AS DESCRIPCION", "", "1", ddlSistemaRiesgo, (Usuario)Session["usuario"]);
    }


    protected List<Seguimiento> ListaControl()
    {
        List<Seguimiento> lstControl = new List<Seguimiento>();
        Seguimiento pControl = new Seguimiento();
        lstControl = seguimientoServicio.ListarFormasControl(pControl, (Usuario)Session["usuario"]);
        return lstControl;
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
                if (pMatriz.cod_control == 0)
                {
                    VerError("Debe ingresar el parámetro de control en cada registro");
                    return false;
                }
                if (pMatriz.forma == 0)
                {
                    VerError("Debe ingresar la forma de control en cada registro");
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
            lstMatriz = matrizServicio.ListarMatrizRResidual(Convert.ToInt64(ddlSistemaRiesgo.SelectedValue), "", (Usuario)Session["usuario"]);
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
                VerError("No se encuentra registrada la matriz de riesgo inherente");
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
                    DropDownListGrid ddlControl = (DropDownListGrid)rFila.FindControl("ddlControl");
                    DropDownListGrid ddlForma = (DropDownListGrid)rFila.FindControl("ddlForma");
                    Label lbRiesgoI = (Label)rFila.FindControl("lbRiesgoI");
                    Label lbValorC = (Label)rFila.FindControl("lbValorC");
                    Label lbClase = (Label)rFila.FindControl("lbClase");
                    Label lbRiesgoR = (Label)rFila.FindControl("lbRiesgoR");
                    DropDownListGrid ddlEjecucion = (DropDownListGrid)rFila.FindControl("ddlEjecucion");
                    DropDownListGrid ddlDocumentacion = (DropDownListGrid)rFila.FindControl("ddlDocumentacion");
                    DropDownListGrid ddlComplejidad = (DropDownListGrid)rFila.FindControl("ddlComplejidad");
                    DropDownListGrid ddlFallas = (DropDownListGrid)rFila.FindControl("ddlFallas");
                    Label lbNivelReduccion = (Label)rFila.FindControl("lbNivelReduccion");

                    objDetalle.cod_matriz = Convert.ToInt32(gvMatriz.DataKeys[rFila.RowIndex].Values[0]);
                    objDetalle.cod_factor = Convert.ToInt64(rFila.Cells[0].Text);
                    objDetalle.desc_factor = HttpUtility.HtmlDecode(rFila.Cells[1].Text);
                    objDetalle.cod_causa = Convert.ToInt64(rFila.Cells[2].Text);
                    objDetalle.desc_causa = HttpUtility.HtmlDecode(rFila.Cells[3].Text);
                    objDetalle.valor_rinherente = Convert.ToInt64(lbRiesgoI.Text);
                    objDetalle.cod_control = ddlControl.SelectedValue != "" ? Convert.ToInt64(ddlControl.SelectedValue) : 0;
                    objDetalle.desc_control = HttpUtility.HtmlDecode(rFila.Cells[6].Text);
                    objDetalle.desc_clase = HttpUtility.HtmlDecode(rFila.Cells[7].Text);
                    objDetalle.clase = lbClase.Text != "" ? Convert.ToInt64(lbClase.Text) : 0;
                    objDetalle.forma = ddlForma.SelectedValue != "" ? Convert.ToInt64(ddlForma.SelectedValue) : 0;
                    objDetalle.valor_control = lbValorC.Text != null && lbValorC.Text != "" ? Convert.ToInt32(lbValorC.Text) : 0;
                    objDetalle.valor_rresidual = lbRiesgoR.Text != null && lbRiesgoR.Text != "" ? Convert.ToInt32(lbRiesgoR.Text) : 0;
                    objDetalle.cod_riesgo = Convert.ToInt64(ddlSistemaRiesgo.SelectedValue);
                    objDetalle.ejecucion = ConvertirStringToInt32(ddlEjecucion.SelectedValue);
                    objDetalle.documentacion = ConvertirStringToInt32(ddlDocumentacion.SelectedValue);
                    objDetalle.complejidad = ConvertirStringToInt32(ddlComplejidad.SelectedValue);
                    objDetalle.fallas = ConvertirStringToInt32(ddlFallas.SelectedValue);
                    objDetalle.nivel_reduccion = ConvertirStringToInt32(lbNivelReduccion.Text);

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
            VerError(matrizServicio.CodigoProgramaC + ex.Message);
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
                bool exitoso = matrizServicio.CrearMatrizRResidual(lstMatriz, (Usuario)Session["usuario"]);
                if (exitoso)
                {
                    lblMensaje.Visible = true;
                    lblMensaje.Text = "Matriz registrada correctamente";
                }
            }
        }
        catch (Exception ex)
        {
            VerError(matrizServicio.CodigoProgramaC + ex.Message);
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
                // Mostrar calificaciones
                DropDownListGrid ddlForma = (DropDownListGrid)e.Row.FindControl("ddlForma");
                if (ddlForma != null) CalcularPuntajeForma(e.Row, ddlForma);
                DropDownListGrid ddlEjecucion = (DropDownListGrid)e.Row.FindControl("ddlEjecucion");
                if (ddlEjecucion != null) CalcularPuntajeForma(e.Row, ddlEjecucion);
                DropDownListGrid ddlDocumentacion = (DropDownListGrid)e.Row.FindControl("ddlDocumentacion");
                if (ddlDocumentacion != null) CalcularPuntajeForma(e.Row, ddlDocumentacion);
                DropDownListGrid ddlComplejidad = (DropDownListGrid)e.Row.FindControl("ddlComplejidad");
                if (ddlComplejidad != null) CalcularPuntajeForma(e.Row, ddlComplejidad);
                DropDownListGrid ddlFallas = (DropDownListGrid)e.Row.FindControl("ddlFallas");
                if (ddlFallas != null) CalcularPuntajeForma(e.Row, ddlFallas);

                // Calcular riesgo
                int cod = Convert.ToInt32(gvMatriz.DataKeys[e.Row.RowIndex].Values[0]);
                TextBox txtRiesgoIn = (TextBox)e.Row.FindControl("txtRiesgoI");
                Label lbRiesgoI = (Label)e.Row.FindControl("lbRiesgoI");
                Label lbRiesgoR = (Label)e.Row.FindControl("lbRiesgoR");

                //Cargar descripción y color para la calificación del riesgo inherente
                Matriz dataItem = e.Row.DataItem as Matriz;
                if (dataItem.valor_rinherente > 0)
                {
                    Int32 calificacion = 0;
                    if (cod == 0)
                        calificacion = Convert.ToInt32(dataItem.valor_rinherente);
                    else
                        calificacion = matrizServicio.calcularValoracionRango(Convert.ToInt32(dataItem.valor_rinherente), (Usuario)Session["usuario"]);
                    txtRiesgoIn.Text = matrizServicio.ColoresRInherente[calificacion].descripcion;
                    lbRiesgoI.Text = matrizServicio.ColoresRInherente[calificacion].valor_rinherente.ToString();
                    txtRiesgoIn.BackColor = System.Drawing.Color.FromName(matrizServicio.ColoresRInherente[calificacion].nivel);
                }

                //Cargar valoración control
                if (dataItem.forma > 0 && dataItem.clase > 0)
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

            }
        }
        catch (Exception ex)
        {
            VerError(matrizServicio.CodigoProgramaC + "gvMatriz_RowDataBound" + ex.Message);
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
            BOexcepcion.Throw(matrizServicio.CodigoProgramaC, "gvMatriz_PageIndexChanging", ex);
        }
    }

    #endregion

    #region Eventos DropDownList

    protected void ddlControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlControl = (DropDownList)sender;
        GridViewRow rFila = (GridViewRow)ddlControl.NamingContainer;
        Label lbClase = (Label)rFila.FindControl("lbClase");
        Label lbNivelReduccion = (Label)rFila.FindControl("lbNivelReduccion");
        DropDownList ddlForma = (DropDownList)rFila.FindControl("ddlForma");

        Seguimiento pControl = new Seguimiento();
        pControl.cod_control = Convert.ToInt64(ddlControl.SelectedValue);
        pControl = seguimientoServicio.ConsultarFormaControl(pControl, (Usuario)Session["usuario"]);
        rFila.Cells[6].Text = pControl.descripcion;
        rFila.Cells[7].Text = pControl.nom_clase;
        lbClase.Text = pControl.clase.ToString();
        lbNivelReduccion.Text = pControl.grado_aceptacion.ToString();

        //Si los valores cambiaron, calcular los valores de riesgo y control
        if (ddlForma.SelectedValue != "" && ddlForma.SelectedValue != "0" && lbClase.Text != "" && lbClase.Text != null && lbClase.Text != "0")
        {
            DeterminarEscala(Convert.ToInt32(lbClase.Text), Convert.ToInt64(ddlForma.SelectedValue), rFila.RowIndex);
            DeterminarRiesgoResidual(rFila);
        }
    }

    protected void ddlForma_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlForma = (DropDownListGrid)sender;
        string[] commandArgs = ddlForma.CommandArgument.ToString().Split(new char[] { ';' });
        Int32 indice = ConvertirStringToInt32(commandArgs[0]);
        Int32 cod_atr = ConvertirStringToInt32(commandArgs[1]);
        GridViewRow rFila = (GridViewRow)ddlForma.NamingContainer;
        GridView rGrid = (GridView)rFila.NamingContainer;
        Label lbClase = (Label)rFila.FindControl("lbClase");
        TextBox txtValorC = (TextBox)rFila.FindControl("txtValorC");        

        //Si los valores cambiaron, calcular los valores de riesgo y control
        if (cod_atr == 1)
        { 
            if (ddlForma.SelectedValue != "" && ddlForma.SelectedValue != "0" && lbClase.Text != "" && lbClase.Text != null && lbClase.Text != "0")
            {
                DeterminarEscala(Convert.ToInt32(lbClase.Text), Convert.ToInt64(ddlForma.SelectedValue), rFila.RowIndex);
                DeterminarRiesgoResidual(rFila);
            }
        }

        //Calcular el puntaje segùn la forma
        if (ddlForma.SelectedValue != "" && ddlForma.SelectedValue != "0")
        {
            Label lbForma = new Label();
            if (cod_atr == 1) lbForma = (Label)rFila.FindControl("lbForma");
            if (cod_atr == 2) lbForma = (Label)rFila.FindControl("lbEjecucion");
            if (cod_atr == 3) lbForma = (Label)rFila.FindControl("lbDocumentacion");
            if (cod_atr == 4) lbForma = (Label)rFila.FindControl("lbComplejidad");
            if (cod_atr == 5) lbForma = (Label)rFila.FindControl("lbFallas");
            FormaControl _formaControl = new FormaControl();
            _formaControl.cod_atributo = ConvertirStringToInt32(commandArgs[1]);
            _formaControl.cod_opcion = ConvertirStringToInt32(ddlForma.SelectedValue);
            if (_formaControl.cod_opcion > 0)
                lbForma.Text = matrizServicio.ConsultarPuntajeFormaControl(_formaControl, (Usuario)Session["usuario"]).ToString();
            else
                lbForma.Text = "0";
            // Calcular la calificación
            if (rFila != null)
                indice = rFila.RowIndex;
            DeterminarCalificacion(indice);
        }
    }

    public void CalcularPuntajeForma(GridViewRow rFila, DropDownListGrid ddlForma)
    {
        string[] commandArgs = ddlForma.CommandArgument.ToString().Split(new char[] { ';' });
        Int32 indice = ConvertirStringToInt32(commandArgs[0]);
        Int32 cod_atr = ConvertirStringToInt32(commandArgs[1]);
        Label lbForma = new Label();
        if (cod_atr == 1) lbForma = (Label)rFila.FindControl("lbForma");
        if (cod_atr == 2) lbForma = (Label)rFila.FindControl("lbEjecucion");
        if (cod_atr == 3) lbForma = (Label)rFila.FindControl("lbDocumentacion");
        if (cod_atr == 4) lbForma = (Label)rFila.FindControl("lbComplejidad");
        if (cod_atr == 5) lbForma = (Label)rFila.FindControl("lbFallas");
        FormaControl _formaControl = new FormaControl();
        _formaControl.cod_atributo = ConvertirStringToInt32(commandArgs[1]);
        _formaControl.cod_opcion = ConvertirStringToInt32(ddlForma.SelectedValue);
        if (_formaControl.cod_opcion > 0)
            lbForma.Text = matrizServicio.ConsultarPuntajeFormaControl(_formaControl, (Usuario)Session["usuario"]).ToString();
        else
            lbForma.Text = "0";
    }

    public void DeterminarCalificacion(int indice)
    {
        Label lbNivelReduccion = (Label)gvMatriz.Rows[indice].FindControl("lbNivelReduccion");
        Label lbClase = (Label)gvMatriz.Rows[indice].FindControl("lbClase");
        DropDownList ddlControl = (DropDownList)gvMatriz.Rows[indice].FindControl("ddlControl");
        DropDownList ddlForma = (DropDownList)gvMatriz.Rows[indice].FindControl("ddlForma");
        Seguimiento pControl = new Seguimiento();
        pControl.cod_control = Convert.ToInt64(ddlControl.SelectedValue);
        pControl = seguimientoServicio.ConsultarFormaControl(pControl, (Usuario)Session["usuario"]);
        gvMatriz.Rows[indice].Cells[6].Text = pControl.descripcion;
        gvMatriz.Rows[indice].Cells[7].Text = pControl.nom_clase;
        lbClase.Text = pControl.clase.ToString();
        lbNivelReduccion.Text = pControl.grado_aceptacion.ToString();

        Label lbForma = (Label)gvMatriz.Rows[indice].FindControl("lbForma");
        Label lbEjecucion = (Label)gvMatriz.Rows[indice].FindControl("lbEjecucion");
        Label lbDocumentacion = (Label)gvMatriz.Rows[indice].FindControl("lbDocumentacion");
        Label lbComplejidad = (Label)gvMatriz.Rows[indice].FindControl("lbComplejidad");
        Label lbFallas = (Label)gvMatriz.Rows[indice].FindControl("lbFallas");
        Label lbRiesgoI = (Label)gvMatriz.Rows[indice].FindControl("lbRiesgoI");
        Label lbRiesgoR = (Label)gvMatriz.Rows[indice].FindControl("lbRiesgoR");
        Label lbPuntajeTotal = (Label)gvMatriz.Rows[indice].FindControl("lbPuntajeTotal");
        Label lbValorC = (Label)gvMatriz.Rows[indice].FindControl("lbValorC");
        TextBox txtValorC = (TextBox)gvMatriz.Rows[indice].FindControl("txtValorC");
        TextBox txtRiesgoR = (TextBox)gvMatriz.Rows[indice].FindControl("txtRiesgoR");

        int _forma = ConvertirStringToInt32(lbForma.Text);
        int _ejecucion = ConvertirStringToInt32(lbEjecucion.Text);
        int _documentacion = ConvertirStringToInt32(lbDocumentacion.Text);
        int _complejidad = ConvertirStringToInt32(lbComplejidad.Text);
        int _fallas = ConvertirStringToInt32(lbFallas.Text);
        int _nivel_reduccion = ConvertirStringToInt32(lbNivelReduccion.Text);

        decimal puntaje_total = _forma + _ejecucion + _documentacion + _complejidad + _fallas + _nivel_reduccion;

        // Determinar la valoración del control
        int valoracion_control = 0;
        string desc_valoracion_control = matrizServicio.ConsultarValoracionFormaControl(Convert.ToInt32(puntaje_total), ref valoracion_control, (Usuario)Session["usuario"]);
        txtValorC.Text = desc_valoracion_control;
        Matriz pValorControl = new Matriz();
        pValorControl = matrizServicio.ColoresVControl.Where(x => valoracion_control <= x.valor_control && x.valor_control > 0).FirstOrDefault();
        if (pValorControl != null)
        {
            txtValorC.Text = pValorControl.descripcion;
            txtValorC.BackColor = System.Drawing.Color.FromName(pValorControl.nivel);
            lbValorC.Text = valoracion_control.ToString();
        }

        // Determinar el riesgo residual
        int riesgo_inherente = ConvertirStringToInt32(lbRiesgoI.Text);
        Int64 puntaje_maximo = matrizServicio.PuntajeMaximoMatrizRiesgo((Usuario)Session["usuario"]);
        decimal aux = 0;
        if (puntaje_maximo != 0) aux = (puntaje_total/puntaje_maximo); else aux = (puntaje_total/100);
        aux = riesgo_inherente * (1 - aux);
        int _residual_inherente = Convert.ToInt32(aux);
        int _riesgo_residual = matrizServicio.ConsultarRangoMatrizRiesgo(_residual_inherente, (Usuario)Session["usuario"]);
        Matriz pRiesgoResidual = new Matriz();
        pRiesgoResidual = matrizServicio.ColoresRResidual.Where(x => _riesgo_residual <= x.valor_rresidual && x.valor_rresidual > 0).FirstOrDefault();
        if (pRiesgoResidual != null)
        {
            lbPuntajeTotal.Text = puntaje_total.ToString();
            txtRiesgoR.Text = pRiesgoResidual.descripcion;
            txtRiesgoR.BackColor = System.Drawing.Color.FromName(pRiesgoResidual.nivel);
            lbRiesgoR.Text = _riesgo_residual.ToString();
        }
    }

    #endregion

    /// <summary>
    /// Determinar el valor de control 
    /// </summary>
    /// <param name="cod_clase">Nivel de probabilidad</param>
    /// <param name="cod_forma">Nivel de impacto</param>
    /// <param name="indice">Indice de la fila en la grilla</param>
    private void DeterminarEscala(Int64 cod_clase, Int64 cod_forma, int indice)
    {
        try
        {
            GridViewRow rfila = gvMatriz.Rows[indice];
            int calificacion = 0;

            //Consultar valor para cargar descripción y color según la matriz de calor para control
            calificacion = matrizServicio.ConsultarCalificacionControl(cod_clase, cod_forma, (Usuario)Session["usuario"]);
            TextBox txtValorC = (TextBox)rfila.FindControl("txtValorC");
            txtValorC.Text = matrizServicio.ColoresVControl[calificacion].descripcion;
            txtValorC.BackColor = System.Drawing.Color.FromName(matrizServicio.ColoresVControl[calificacion].nivel);

            //Cargar valor de la calificación en un hidden para poder guardar
            Label lbValorC = (Label)rfila.FindControl("lbValorC");
            lbValorC.Text = calificacion.ToString();
        }
        catch (Exception ex)
        {
            VerError("Error al definir la calificación del control" + ex.Message);
        }
    }

    /// <summary>
    /// Determinar el valor del riesgo inherente
    /// </summary>
    /// <param name="calificacion">Calificacion obtenida</param>
    private void DeterminarEscalaRiesgoI(Int64 calificacion, TextBox txtRiesgoIn)
    {

    }

    /// <summary>
    /// Determinar el valor del riesgo residual en base al valor de control y de riesgo inherente
    /// </summary>
    /// <param name="rFila"></param>
    private void DeterminarRiesgoResidual(GridViewRow rFila)
    {
        int valor_inherente = 0;
        int valor_control = 0;
        int valor_residual = 0;
        Label lbRiesgoI = (Label)rFila.FindControl("lbRiesgoIC");
        Label lbRiesgoR = (Label)rFila.FindControl("lbRiesgoR");
        Label lbValorC = (Label)rFila.FindControl("lbValorC");
        TextBox txtRiesgoR = (TextBox)rFila.FindControl("txtRiesgoR");

        if (lbRiesgoI.Text != null && lbRiesgoI.Text != "" && lbValorC.Text != "" && lbValorC.Text != null)
        {
            valor_inherente = Convert.ToInt32(lbRiesgoI.Text);
            valor_control = Convert.ToInt32(lbValorC.Text);
            if (valor_control != 0)
                valor_residual = Math.Abs(valor_inherente / valor_control);
            Matriz pValorResidual = new Matriz();
            Int32 residual = matrizServicio.calcularValoracionRango(Convert.ToInt32(valor_residual), (Usuario)Session["usuario"]);
            pValorResidual = matrizServicio.ColoresRResidual.Where(x => residual <= x.valor_rresidual && x.valor_rresidual > 0).FirstOrDefault();
            txtRiesgoR.Text = pValorResidual.descripcion;                
            txtRiesgoR.BackColor = System.Drawing.Color.FromName(pValorResidual.nivel);
            lbRiesgoR.Text = pValorResidual.valor_rresidual.ToString();
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
            GridView1.RenderControl(hw);
            return sw;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void gvMatriz_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow gvHeaderRow = e.Row;
        }
    }

    protected List<FormaControl> ListaForma(int pcod_atr)
    {
        //Forma Control
        List<FormaControl> lstForma = (List<FormaControl>)(from u in matrizServicio.ListaFormaControl() where u.cod_atributo == pcod_atr select new FormaControl { cod_opcion = u.cod_opcion, opcion = u.opcion }).ToList();
        lstForma.Add(new FormaControl {cod_atributo = pcod_atr, cod_opcion = 0, opcion = "Seleccione una opciòn" } );
        return lstForma;
    }


}