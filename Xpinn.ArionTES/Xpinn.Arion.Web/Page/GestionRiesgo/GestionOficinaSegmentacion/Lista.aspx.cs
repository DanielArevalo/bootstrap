using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.NIIF.Entities;
using Xpinn.NIIF.Services;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;
using Xpinn.Util;
using System.Web;
using System.Reflection;
using System.Linq;

public partial class Lista : GlobalWeb
{
    HistoricoSegmentacionService _historicoService = new HistoricoSegmentacionService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(_historicoService.CodigoPrograma2, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoExportar += btnExportar_Click;

            toolBar.MostrarGuardar(false);
            toolBar.MostrarRegresar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.GetType().Name + "L", "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.Oficinas, ddloficina);
        LlenarListasDesplegables(TipoLista.Segmentos, ddlSegmentoActual);

        List<HistoricoSegmentacion> listaFechasCerras = _historicoService.ListarFechaCierreYaHechas("", Usuario);

        ddlFechaCierre.DataSource = listaFechasCerras;
        ddlFechaCierre.DataValueField = "fechacierre";
        ddlFechaCierre.DataTextField = "fechacierre";
        Configuracion conf = new Configuracion();
        ddlFechaCierre.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCierre.DataBind();
    }


    #endregion


    #region Eventos CheckBox


    protected void chkVolverConsultar_CheckedChanged(object sender, EventArgs e)
    {
        ObtenerDatos();
    }


    #endregion


    #region Eventos Botones


    void btnConsultar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ObtenerDatos();
    }

    void btnGuardar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(txtAnalisisOficial.Text))
            {
                HistoricoSegmentacion historico = new HistoricoSegmentacion
                {
                    consecutivo = Convert.ToInt32(idObjeto),
                    analisisoficialcumplimiento = txtAnalisisOficial.Text,
                };

                _historicoService.GuardarAnalisisOficialDeHistoricoSegmentacion(historico, Usuario);

                mvPrincipal.SetActiveView(mvFinal);

                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
            }
            else
            {
                VerError("El analisis efectuado no puede estar vacio!.");
            }
        }
        catch (Exception ex)
        {
            VerError("Error al guardar el analisis efectuado, " + ex.Message);
        }
    }

    protected void btnRegresarGuardar_Click(object sender, EventArgs e)
    {
        LimpiarPantallaDetalleYRegresar();
    }

    protected void btnRegresar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        LimpiarPantallaDetalleYRegresar();
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["DTACTUALIZACION"] != null)
        {
            string fic = "ProcesoOficina.csv";
            try
            {
                File.Delete(fic);
            }
            catch
            {
            }
            // Generar el archivo
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("") + fic, true);
            string filtro = ObtenerFiltro();
            List<HistoricoSegmentacion> lstConsulta = _historicoService.ListarHistoricosSegmentacion(filtro, Usuario);

            string texto = "";
            texto = "consecutivo;codigopersona;fechacierre;usuariocierre;identificacion_persona;nombre_persona;cod_oficina;nombre_oficina;calificacion;segmentoaso;segmentopro;segmentocan;segmentojur;tipo_rol;calificacion_anterior";
            sw.WriteLine(texto);

            foreach (HistoricoSegmentacion item in lstConsulta)
            {                
                texto = "";
                texto = item.consecutivo+";"+item.codigopersona + ";" +item.fechacierre + ";" +item.usuariocierre + ";" +item.identificacion_persona + ";" +item.nombre_persona + ";" +item.cod_oficina + ";" +item.nombre_oficina + ";" +item.calificacion + ";" +item.segmentoaso + ";" +item.segmentopro + ";" +item.segmentocan + ";" +item.segmentojur + ";" +item.tipo_rol + ";" +item.calificacion_anterior;
                sw.WriteLine(texto);
            }
            sw.Close();
            System.IO.StreamReader sr;
            sr = File.OpenText(Server.MapPath("") + fic);
            string texo = sr.ReadToEnd();
            sr.Close();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(texo);
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
            HttpContext.Current.Response.Flush();
            File.Delete(Server.MapPath("") + fic);
            HttpContext.Current.Response.End();

            /*StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            GridView gvExportar = CopiarGridViewParaExportar(gvLista, "DTACTUALIZACION");
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTACTUALIZACION"];
            gvLista.DataBind();
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            form.Controls.Add(gvExportar);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=ProcesoOficina.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
            gvLista.AllowPaging = true;
            gvLista.DataBind();*/

        }
    }


    #endregion


    #region Eventos GridView


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            if (Session["DTACTUALIZACION"] != null)
            { 
                List<HistoricoSegmentacion> listaSegmentacion = new List<HistoricoSegmentacion>();
                Session["DTACTUALIZACION"] = listaSegmentacion;
                return;
            }
            ObtenerDatos();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma2, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {            
            GridViewRow row = gvLista.SelectedRow;
            string consecutivo = gvLista.DataKeys[row.RowIndex].Value.ToString();
            Int64 cod_persona = Convert.ToInt64(row.Cells[2].Text);

            if (Session["DTACTUALIZACION"] != null)
            {
                List<HistoricoSegmentacion> listaSegmentacion = Session["DTACTUALIZACION"] as List<HistoricoSegmentacion>;
                listaSegmentacion = listaSegmentacion.Where(x => x.codigopersona.ToString() == cod_persona.ToString()
                                                            && x.consecutivo.ToString() == consecutivo).ToList();
                HistoricoSegmentacion hist = listaSegmentacion.ElementAt(0);
                lblAnterior.Text = " - " + hist.calificacion_anterior;
                lblactual.Text = " - " + hist.calificacion;
                Session["DTLISTA"] = listaSegmentacion;
                gvDetalle.DataSource = listaSegmentacion;
                gvDetalle.DataBind();
            }

            // Configuro los botones para esta View
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
            toolBar.MostrarRegresar(true);
            toolBar.MostrarConsultar(false);

            //Cargar datos de la persona
            ctlPersona.DatosPersona(cod_persona);

            // Busco el historico seleccionado y su detalle del segmento
            Usuario _Usuario = (Usuario)Session["Usuario"];
            HistoricoSegmentacion historicoActual = _historicoService.ConsultarHistoricoSegmentoActual(consecutivo, _Usuario);

            List<Segmento_Detalles> segmentoDetalleActual = null;
            if (historicoActual.segmentoactual.HasValue)
            {
                segmentoDetalleActual = _historicoService.ListarDetalleSegmentos(historicoActual.segmentoactual.Value, _Usuario);
            }

            // Lleno la informacion del historico que estoy viendo
            txtAnalisisOficial.Text = historicoActual.analisisoficialcumplimiento;
            txtFechaAnalisis.Text = historicoActual.fechaanalisis.HasValue ? historicoActual.fechaanalisis.Value.ToShortDateString() : DateTime.Now.ToShortDateString();
            txtUsuarioAnalisis.Text = historicoActual.nombre_usuario_analisis != null ? (historicoActual.nombre_usuario_analisis.Trim() != "" ? historicoActual.nombre_usuario_analisis : _Usuario.nombre) : _Usuario.nombre;

            // Si el historico que seleccione tiene un segmento anterior, 
            // Busco ese historico y sus detalles
            // Si no tengo segmento anterior entonces no busco nada, 
            // Oculto el boton de guardar, pongo el textbox de analisis en modo ReadOnly y muestro el mensaje que no hay segmento anterior
            HistoricoSegmentacion historicoAnterior = null;
            List<Segmento_Detalles> segmentoDetalleAnterior = null;
            if (historicoActual.segmentoanterior.HasValue && historicoActual.segmentoanterior > 0)
            {
                // Se usa el consecutivo del historico actual para poder apartir de ese historico buscar el inmediatamente anterior
                historicoAnterior = _historicoService.ConsultarHistoricoSegmentoAnterior(consecutivo, Usuario);

                if (historicoAnterior.segmentoactual.HasValue)
                {
                    segmentoDetalleAnterior = _historicoService.ListarDetalleSegmentos(historicoAnterior.segmentoactual.Value, Usuario);
                    lblNoExisteSegmentoAnterior.Visible = false;
                    //txtAnalisisOficial.ReadOnly = false;
                }
                else
                {
                    toolBar.MostrarGuardar(false);
                    //txtAnalisisOficial.ReadOnly = true;
                    lblNoExisteSegmentoAnterior.Visible = true;
                    gvSegmentoAnterior.Visible = false;
                }
            }
            else
            {
                toolBar.MostrarGuardar(false);
                //txtAnalisisOficial.ReadOnly = true;
                lblNoExisteSegmentoAnterior.Visible = true;
                gvSegmentoAnterior.Visible = false;
            }

            // Si no tengo segmento actual, oculto la gridview y muestro el mensaje alusivo
            // Si tengo segmento actual entonces muestro la gridview
            if (segmentoDetalleActual != null)
            {
                gvSegmentoActual.Visible = true;

                // Recorro cada detalle del segmento actual para formatearlo y llenar propiedades necesarias para la visualizacion
                foreach (var detalleActual in segmentoDetalleActual)
                {
                    // Segun la variable del detalle obtengo el valor del historico para mostrarlo mas facil en la GV
                    ObtenerValorHistoricoSegunVariableDelDetalle(historicoActual, detalleActual);

                    // Si tengo historico y segmento anterior entonces comparo el detalle con el que trabajo con el historico anterior en busca de cambios
                    // Marco los cambios para posterior visualizacion
                    if (historicoAnterior != null && segmentoDetalleAnterior != null)
                    {
                        CompararValoresYDeterminarSiLlevaMarcaDeCambio(historicoAnterior, detalleActual);
                    }
                }
            }
            else
            {
                lblNoExisteSegmentoActual.Visible = true;
                gvSegmentoActual.Visible = false;
            }

            // Si tengo historico y segmento anterior tambien lo formateo
            if (historicoAnterior != null && segmentoDetalleAnterior != null)
            {
                // Recorro cada detalle del segmento anterior para formatearlo y llenar propiedades necesarias para la visualizacion
                foreach (var detalleAnterior in segmentoDetalleAnterior)
                {
                    // Segun la variable del detalle obtengo el valor del historico anterior para mostrarlo mas facil en la GV
                    ObtenerValorHistoricoSegunVariableDelDetalle(historicoAnterior, detalleAnterior);

                    // Comparo el detalle con el que estoy trabajando con el historico actual en busca de cambios
                    // Marco los cambios para posterior visualizacion
                    CompararValoresYDeterminarSiLlevaMarcaDeCambio(historicoActual, detalleAnterior);
                }

                // Bindeo la gridview con la nueva lista formateada
                gvSegmentoAnterior.Visible = true;
                gvSegmentoAnterior.DataSource = segmentoDetalleAnterior;
                gvSegmentoAnterior.DataBind();
            }

            // Bindeo la gridview con la nueva lista formateada
            gvSegmentoActual.DataSource = segmentoDetalleActual;
            gvSegmentoActual.DataBind();

            mvPrincipal.SetActiveView(viewDetalle);
            idObjeto = consecutivo;
            gvLista.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar el detalle del segmento, " + ex.Message);
        }
    }

    protected void gvSegmentoAnterior_RowDataBoundA(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlCondicion = (DropDownList)e.Row.FindControl("ddlCondicion");
            DropDownList ddlOperador = (DropDownList)e.Row.FindControl("ddlOperador");

            if (ddlCondicion != null)
            {
                /*TasaMercadoNIFService Tasa = new TasaMercadoNIFService();
                List<TasaMercadoNIF> lstTasa = Tasa.DatosCondicionNIIF(new TasaMercadoNIF(), Usuario);*/

                List<Segmento_Detalles> lstVariables = new List<Segmento_Detalles>();  // Tasa.DatosCondicionNIIF(new TasaMercadoNIF(), Usuario);
                lstVariables.Add(new Segmento_Detalles() { variable = "A", descripcion = "Puntaje Calificación" });
                lstVariables.Add(new Segmento_Detalles() { variable = "B", descripcion = "Endeudamiento" });
                lstVariables.Add(new Segmento_Detalles() { variable = "C", descripcion = "Ingresos Mensuales" });
                lstVariables.Add(new Segmento_Detalles() { variable = "D", descripcion = "Edad" });
                lstVariables.Add(new Segmento_Detalles() { variable = "E", descripcion = "Personas a Cargo" });
                lstVariables.Add(new Segmento_Detalles() { variable = "F", descripcion = "Tipo de vivienda" });
                lstVariables.Add(new Segmento_Detalles() { variable = "G", descripcion = "Estrato" });
                lstVariables.Add(new Segmento_Detalles() { variable = "H", descripcion = "Nivel Academico" });
                lstVariables.Add(new Segmento_Detalles() { variable = "I", descripcion = "Ciudad Residencia" });
                lstVariables.Add(new Segmento_Detalles() { variable = "J", descripcion = "Actividad Economica" });
                lstVariables.Add(new Segmento_Detalles() { variable = "K", descripcion = "Saldo Promedio Ahorros" });
                lstVariables.Add(new Segmento_Detalles() { variable = "L", descripcion = "Saldo Promedio Aportes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "M", descripcion = "Monto del Crédito SMLMV" });
                lstVariables.Add(new Segmento_Detalles() { variable = "N", descripcion = "Saldo Creditos Activos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "O", descripcion = "Numero Creditos Activos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "P", descripcion = "# Operaciones productos al mes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "R", descripcion = "Monto operaciones en el mes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "S", descripcion = "Sexo" });
                lstVariables.Add(new Segmento_Detalles() { variable = "T", descripcion = "Tipo de persona" });
                lstVariables.Add(new Segmento_Detalles() { variable = "U", descripcion = "Tipo de Cliente" });
                lstVariables.Add(new Segmento_Detalles() { variable = "V", descripcion = "Saldo Aportes Activos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "W", descripcion = "Jurisdiccion" });
                lstVariables.Add(new Segmento_Detalles() { variable = "X", descripcion = "Valoracion Jurisdiccion" });
                lstVariables.Add(new Segmento_Detalles() { variable = "Y", descripcion = "Saldo total de captaciones" });
                lstVariables.Add(new Segmento_Detalles() { variable = "Z", descripcion = "Monto de operaciones en el mes ahorro vista" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AB", descripcion = "Monto de operaciones en el mes aportes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AC", descripcion = "Monto de operaciones en el mes créditos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AD", descripcion = "Monto de operaciones en el mes ahorro programado" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AE", descripcion = "Monto de operaciones en el mes cdta" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AF", descripcion = "# de operaciones en el mes ahorro vista" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AG", descripcion = "# de operaciones en el mes aportes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AH", descripcion = "# de operaciones en el mes créditos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AI", descripcion = "# de operaciones en el mes ahorro permanente" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AJ", descripcion = "# de operaciones en el mes cdta" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AK", descripcion = "Antigüedad asociado sea: en fecha o en numero de meses" });

                ddlCondicion.DataSource = lstVariables;
                ddlCondicion.DataTextField = "descripcion";
                ddlCondicion.DataValueField = "variable";
                ddlCondicion.DataBind();
            }

            if (ddlOperador != null)
            {
                ddlOperador.Items.Insert(0, new ListItem("<Seleccione un Item>", " "));
                ddlOperador.Items.Insert(1, new ListItem("IGUAL", "1"));
                ddlOperador.Items.Insert(2, new ListItem("MAYOR", "2"));
                ddlOperador.Items.Insert(3, new ListItem("MENOR", "3"));
                ddlOperador.Items.Insert(4, new ListItem("MAYOR o IGUAL", "4"));
                ddlOperador.Items.Insert(5, new ListItem("MENOR o IGUAL", "5"));
                ddlOperador.Items.Insert(6, new ListItem("DIFERENTE", "6"));
                ddlOperador.Items.Insert(7, new ListItem("COMIENZA POR", "7"));
                ddlOperador.Items.Insert(8, new ListItem("CONTIENE", "8"));
                ddlOperador.Items.Insert(9, new ListItem("RANGO", "9"));
                ddlOperador.Items.Insert(9, new ListItem("CONJUNTO", "10"));
                ddlOperador.DataBind();
            }

            Label lblCondicion = (Label)e.Row.FindControl("lblCondicion");
            if (lblCondicion != null)
            {
                ddlCondicion.SelectedValue = lblCondicion.Text;
            }

            Label lblOperador = (Label)e.Row.FindControl("lblOperador");
            if (lblOperador != null)
            {
                ddlOperador.SelectedValue = lblOperador.Text;

                // Configuro la visibilidad de los controles en la fila
                switch (ddlCondicion.SelectedValue)
                {
                    case "H": // Nivel Academico
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.NivelEscolaridad);
                        break;
                    case "I": // Ciudad Residencia
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.Ciudades);
                        break;
                    case "J": // Actividad Economica
                        if (Convert.ToInt32(lblOperador.Text) == 10)
                        {
                            List<listaMultiple> lstConsultaAct = new List<listaMultiple>();
                            lstConsultaAct = _historicoService.ListarActividadesMultiple((Usuario)Session["usuario"]);
                            GridView listaAct = (GridView)e.Row.FindControl("gvRecogerA");
                            TextBox txtRecoger = (TextBox)e.Row.FindControl("txtRecogerA");
                            DropDownListGrid dropDownValorA = e.Row.FindControl("ddlValor") as DropDownListGrid;

                            listaAct.DataSource = lstConsultaAct;
                            if (lstConsultaAct.Count > 0)
                            {
                                txtRecoger.Visible = true;
                                dropDownValorA.Visible = false;
                                listaAct.DataBind();
                                TextBox txtValor = (TextBox)e.Row.FindControl("txtValor");
                                if (txtValor != null)
                                    cargarconjutas(listaAct, Convert.ToString(txtValor.Text), txtRecoger);
                            }
                            else
                            {
                                listaAct.Visible = false;
                            }
                        }
                        else { AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.Actividad_Laboral); }
                        break;
                    case "S": // Sexo
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.Sexo);
                        break;
                    default: // Si es cualquier otro no permito dropdowns
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, false);
                        break;
                }

                // Configurar columna de segundo valor
                TextBox txtSegundoValor = (TextBox)e.Row.FindControl("txtSegundoValor");
                DropDownListGrid dropDownSegundoValor = e.Row.FindControl("ddlSegundoValor") as DropDownListGrid;
                if (dropDownSegundoValor != null && txtSegundoValor != null)
                {
                    if (!string.IsNullOrWhiteSpace(txtSegundoValor.Text) && !txtSegundoValor.Visible)
                    {
                        dropDownSegundoValor.SelectedValue = txtSegundoValor.Text;
                    }
                }
            }

            // Configurar columna de valor
            TextBox textBoxValor = e.Row.FindControl("txtValor") as TextBox;
            DropDownListGrid dropDownValor = e.Row.FindControl("ddlValor") as DropDownListGrid;
            if (textBoxValor != null && dropDownValor != null && !textBoxValor.Visible && !string.IsNullOrWhiteSpace(textBoxValor.Text))
            {
                dropDownValor.SelectedValue = textBoxValor.Text;
            }

            // Configurar columna de valor historico
            TextBox textBoxValorHistorico = e.Row.FindControl("txtValorHistorico") as TextBox;
            DropDownListGrid dropDownValorHistorico = e.Row.FindControl("ddlValorHistorico") as DropDownListGrid;
            if (textBoxValorHistorico != null && dropDownValorHistorico != null && !textBoxValorHistorico.Visible && !string.IsNullOrWhiteSpace(textBoxValorHistorico.Text))
            {
                dropDownValorHistorico.SelectedValue = textBoxValorHistorico.Text;
            }

            // Marco los campos cambiados con background Rojo
            TransicionDetalle itemBound = e.Row.DataItem as TransicionDetalle;
            if (itemBound != null && itemBound.llevaMarcaCambio)
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void gvSegmentoAnterior_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlCondicion = (DropDownList)e.Row.FindControl("ddlCondicion");
            DropDownList ddlOperador = (DropDownList)e.Row.FindControl("ddlOperador");

            if (ddlCondicion != null)
            {
                /*TasaMercadoNIFService Tasa = new TasaMercadoNIFService();
                List<TasaMercadoNIF> lstTasa = Tasa.DatosCondicionNIIF(new TasaMercadoNIF(), Usuario);*/

                List<Segmento_Detalles> lstVariables = new List<Segmento_Detalles>();  // Tasa.DatosCondicionNIIF(new TasaMercadoNIF(), Usuario);
                lstVariables.Add(new Segmento_Detalles() { variable = "A", descripcion = "Puntaje Calificación" });
                lstVariables.Add(new Segmento_Detalles() { variable = "B", descripcion = "Endeudamiento" });
                lstVariables.Add(new Segmento_Detalles() { variable = "C", descripcion = "Ingresos Mensuales" });
                lstVariables.Add(new Segmento_Detalles() { variable = "D", descripcion = "Edad" });
                lstVariables.Add(new Segmento_Detalles() { variable = "E", descripcion = "Personas a Cargo" });
                lstVariables.Add(new Segmento_Detalles() { variable = "F", descripcion = "Tipo de vivienda" });
                lstVariables.Add(new Segmento_Detalles() { variable = "G", descripcion = "Estrato" });
                lstVariables.Add(new Segmento_Detalles() { variable = "H", descripcion = "Nivel Academico" });
                lstVariables.Add(new Segmento_Detalles() { variable = "I", descripcion = "Ciudad Residencia" });
                lstVariables.Add(new Segmento_Detalles() { variable = "J", descripcion = "Actividad Economica" });
                lstVariables.Add(new Segmento_Detalles() { variable = "K", descripcion = "Saldo Promedio Ahorros" });
                lstVariables.Add(new Segmento_Detalles() { variable = "L", descripcion = "Saldo Promedio Aportes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "M", descripcion = "Monto del Crédito SMLMV" });
                lstVariables.Add(new Segmento_Detalles() { variable = "N", descripcion = "Saldo Creditos Activos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "O", descripcion = "Numero Creditos Activos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "P", descripcion = "# Operaciones productos al mes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "R", descripcion = "Monto operaciones en el mes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "S", descripcion = "Sexo" });
                lstVariables.Add(new Segmento_Detalles() { variable = "T", descripcion = "Tipo de persona" });
                lstVariables.Add(new Segmento_Detalles() { variable = "U", descripcion = "Tipo de Cliente" });
                lstVariables.Add(new Segmento_Detalles() { variable = "V", descripcion = "Saldo Aportes Activos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "W", descripcion = "Jurisdiccion" });
                lstVariables.Add(new Segmento_Detalles() { variable = "X", descripcion = "Valoracion Jurisdiccion" });
                lstVariables.Add(new Segmento_Detalles() { variable = "Y", descripcion = "Saldo total de captaciones" });
                lstVariables.Add(new Segmento_Detalles() { variable = "Z", descripcion = "Monto de operaciones en el mes ahorro vista" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AB", descripcion = "Monto de operaciones en el mes aportes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AC", descripcion = "Monto de operaciones en el mes créditos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AD", descripcion = "Monto de operaciones en el mes ahorro programado" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AE", descripcion = "Monto de operaciones en el mes cdta" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AF", descripcion = "# de operaciones en el mes ahorro vista" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AG", descripcion = "# de operaciones en el mes aportes" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AH", descripcion = "# de operaciones en el mes créditos" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AI", descripcion = "# de operaciones en el mes ahorro permanente" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AJ", descripcion = "# de operaciones en el mes cdta" });
                lstVariables.Add(new Segmento_Detalles() { variable = "AK", descripcion = "Antigüedad asociado sea: en fecha o en numero de meses" });

                ddlCondicion.DataSource = lstVariables;
                ddlCondicion.DataTextField = "descripcion";
                ddlCondicion.DataValueField = "variable";
                ddlCondicion.DataBind();
            }

            if (ddlOperador != null)
            {
                ddlOperador.Items.Insert(0, new ListItem("<Seleccione un Item>", " "));
                ddlOperador.Items.Insert(1, new ListItem("IGUAL", "1"));
                ddlOperador.Items.Insert(2, new ListItem("MAYOR", "2"));
                ddlOperador.Items.Insert(3, new ListItem("MENOR", "3"));
                ddlOperador.Items.Insert(4, new ListItem("MAYOR o IGUAL", "4"));
                ddlOperador.Items.Insert(5, new ListItem("MENOR o IGUAL", "5"));
                ddlOperador.Items.Insert(6, new ListItem("DIFERENTE", "6"));
                ddlOperador.Items.Insert(7, new ListItem("COMIENZA POR", "7"));
                ddlOperador.Items.Insert(8, new ListItem("CONTIENE", "8"));
                ddlOperador.Items.Insert(9, new ListItem("RANGO", "9"));
                ddlOperador.Items.Insert(9, new ListItem("CONJUNTO", "10"));
                ddlOperador.DataBind();
            }

            Label lblCondicion = (Label)e.Row.FindControl("lblCondicion");
            if (lblCondicion != null)
            {
                ddlCondicion.SelectedValue = lblCondicion.Text;
            }

            Label lblOperador = (Label)e.Row.FindControl("lblOperador");
            if (lblOperador != null)
            {
                ddlOperador.SelectedValue = lblOperador.Text;

                // Configuro la visibilidad de los controles en la fila
                switch (ddlCondicion.SelectedValue)
                {
                    case "H": // Nivel Academico
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.NivelEscolaridad);
                        break;
                    case "I": // Ciudad Residencia
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.Ciudades);
                        break;
                    case "J": // Actividad Economica
                        if (Convert.ToInt32(lblOperador.Text) == 10)
                        {
                            List<listaMultiple> lstConsultaAct = new List<listaMultiple>();
                            lstConsultaAct = _historicoService.ListarActividadesMultiple((Usuario)Session["usuario"]);
                            GridView listaAct = (GridView)e.Row.FindControl("gvRecoger");
                            TextBox txtRecoger = (TextBox)e.Row.FindControl("txtRecoger");
                            DropDownListGrid dropDownValorA = e.Row.FindControl("ddlValor") as DropDownListGrid;

                            listaAct.DataSource = lstConsultaAct;
                            if (lstConsultaAct.Count > 0)
                            {
                                txtRecoger.Visible = true;
                                dropDownValorA.Visible = false;
                                listaAct.DataBind();
                                TextBox txtValor = (TextBox)e.Row.FindControl("txtValor");
                                if (txtValor != null)
                                    cargarconjutas(listaAct, Convert.ToString(txtValor.Text), txtRecoger);
                            }
                            else
                            {
                                listaAct.Visible = false;
                            }
                        }
                        else { AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.Actividad_Laboral); }
                        break;
                    case "S": // Sexo
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, true, TipoLista.Sexo);
                        break;
                    default: // Si es cualquier otro no permito dropdowns
                        AlternarVisibilidadDropDownsDeUnaFila(e.Row, false);
                        break;
                }

                // Configurar columna de segundo valor
                TextBox txtSegundoValor = (TextBox)e.Row.FindControl("txtSegundoValor");
                DropDownListGrid dropDownSegundoValor = e.Row.FindControl("ddlSegundoValor") as DropDownListGrid;
                if (dropDownSegundoValor != null && txtSegundoValor != null)
                {
                    if (!string.IsNullOrWhiteSpace(txtSegundoValor.Text) && !txtSegundoValor.Visible)
                    {
                        dropDownSegundoValor.SelectedValue = txtSegundoValor.Text;
                    }
                }
            }

            // Configurar columna de valor
            TextBox textBoxValor = e.Row.FindControl("txtValor") as TextBox;
            DropDownListGrid dropDownValor = e.Row.FindControl("ddlValor") as DropDownListGrid;
            if (textBoxValor != null && dropDownValor != null && !textBoxValor.Visible && !string.IsNullOrWhiteSpace(textBoxValor.Text))
            {
                dropDownValor.SelectedValue = textBoxValor.Text;
            }

            // Configurar columna de valor historico
            TextBox textBoxValorHistorico = e.Row.FindControl("txtValorHistorico") as TextBox;
            DropDownListGrid dropDownValorHistorico = e.Row.FindControl("ddlValorHistorico") as DropDownListGrid;
            if (textBoxValorHistorico != null && dropDownValorHistorico != null && !textBoxValorHistorico.Visible && !string.IsNullOrWhiteSpace(textBoxValorHistorico.Text))
            {
                dropDownValorHistorico.SelectedValue = textBoxValorHistorico.Text;
            }

            // Marco los campos cambiados con background Rojo
            TransicionDetalle itemBound = e.Row.DataItem as TransicionDetalle;
            if (itemBound != null && itemBound.llevaMarcaCambio)
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
        }
    }


    #endregion


    #region Metodos Ayuda
    void cargarconjutas(GridView lista, String val, TextBox txtRecoger)
    {
        string[] arrAct = val.Split(';');
        foreach (string act in arrAct)
        {
            if (act != "")
            {
                foreach (GridViewRow rfila in lista.Rows)
                {
                    Label lbl_destino = (Label)rfila.FindControl("lbl_destino");
                    if (Convert.ToString(lbl_destino.Text) == act)
                    {
                        CheckBox cbListado = (CheckBox)rfila.FindControl("cbListado");
                        Label lbl_descripcion = (Label)rfila.FindControl("lbl_descripcion");
                        cbListado.Checked = true;
                        txtRecoger.Text = lbl_descripcion.Text;
                    }
                }
            }
        }
    }

    void ObtenerDatos()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();

            List<HistoricoSegmentacion> listaSegmentacion = _historicoService.ListarHistoricosSegmentacion(filtro, Usuario);
            Session["DTACTUALIZACION"] = listaSegmentacion;
            if (listaSegmentacion != null)
            {
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "Se encontraron " + listaSegmentacion.Count + " registros!.";
                gvLista.DataSource = listaSegmentacion;
                gvLista.DataBind();
            }
            else
            {
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }
        }
        catch (Exception ex)
        {
            VerError("Error al obtener los datos: " + ex.Message);
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and per.identificacion LIKE '%" + txtIdentificacion.Text.Trim() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(txtCodigoPersona.Text))
        {
            filtro += " and per.cod_persona = " + txtCodigoPersona.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(ddlFechaCierre.SelectedValue))
        {
            filtro += " and hist.fechacierre = to_date('" + ddlFechaCierre.SelectedItem.Text.Trim() + "', 'dd/MM/yy') ";
        }

        if (!string.IsNullOrWhiteSpace(ddloficina.SelectedValue))
        {
            filtro += " and per.cod_oficina = " + ddloficina.SelectedValue.Trim();
        }

        if (!string.IsNullOrWhiteSpace(ddlSegmentoActual.SelectedValue))
        {
            filtro += " and hist.segmentoActual = " + ddlSegmentoActual.SelectedValue.Trim();
        }

        if (chkCambiaronSegmento.Checked)
        {
            //filtro += " and (hist.segmentoAnterior IS NOT NULL AND hist.segmentoactual != hist.segmentoAnterior) ";
            filtro += " and (hist.calificacion IS NOT NULL AND hist.calificacion != hist.calificacion_anterior) ";
        }

        if (chkFaltanPorAnalizar.Checked)
        {
            filtro += " and hist.FECHAANALISIS IS NULL AND hist.USUARIOANALISIS IS NULL ";
        }

        if (!string.IsNullOrWhiteSpace(ddlCalificacion.SelectedValue))
        {
            if (ddlCalificacion.SelectedValue.Trim() != "0")
                filtro += " and hist.calificacion = " + ddlCalificacion.SelectedValue.Trim();
        }
        if (ddlTipoRol.SelectedValue!="")
        {
            if (ddlTipoRol.SelectedValue == "T")
                filtro += " and NVL(hper.estado, NVL(hist.tipocliente, per.estado)) Not In ('A', 'R') ";
            else
                filtro += " and NVL(hper.estado, NVL(hist.tipocliente, per.estado)) = '" + ddlTipoRol.SelectedValue + "' ";
        }
        StringHelper stringHelper = new StringHelper();

        return stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);
    }

    void CompararValoresYDeterminarSiLlevaMarcaDeCambio(HistoricoSegmentacion historicoParaComparar, Segmento_Detalles detalleActual)
    {
        switch (detalleActual.variable)
        {
            case "A": // Puntaje de Calificación
                detalleActual.llevaMarcaCambio = historicoParaComparar.puntajecalificacion.HasValue ? historicoParaComparar.puntajecalificacion.ToString() != detalleActual.valor_historico : false;
                break;
            case "B": // Endeudamiento
                detalleActual.llevaMarcaCambio = historicoParaComparar.endeudamiento.HasValue ? historicoParaComparar.endeudamiento.ToString() != detalleActual.valor_historico : false;
                break;
            case "C": // Ingresos Mensuales
                detalleActual.llevaMarcaCambio = historicoParaComparar.ingresosmensuales.HasValue ? historicoParaComparar.ingresosmensuales.ToString() != detalleActual.valor_historico : false;
                break;
            case "D": // Edad
                detalleActual.llevaMarcaCambio = historicoParaComparar.edad.HasValue ? historicoParaComparar.edad.ToString() != detalleActual.valor_historico : false;
                break;
            case "E": // Personas a cargo
                detalleActual.llevaMarcaCambio = historicoParaComparar.personasacargo.HasValue ? historicoParaComparar.personasacargo.ToString() != detalleActual.valor_historico : false;
                break;
            case "F": // Tipo de Vivienda
                detalleActual.llevaMarcaCambio = historicoParaComparar.tipovivienda.HasValue ? historicoParaComparar.tipovivienda.ToString() != detalleActual.valor_historico : false;
                break;
            case "G": // Estrato
                detalleActual.llevaMarcaCambio = historicoParaComparar.estrato.HasValue ? historicoParaComparar.estrato.ToString() != detalleActual.valor_historico : false;
                break;
            case "H": // Nivel Academico
                detalleActual.llevaMarcaCambio = historicoParaComparar.nivelacademico.HasValue ? historicoParaComparar.nivelacademico.ToString() != detalleActual.valor_historico : false;
                break;
            case "I": // Actividad Economica
                detalleActual.llevaMarcaCambio = historicoParaComparar.actividadeconomica.HasValue ? historicoParaComparar.actividadeconomica.ToString() != detalleActual.valor_historico : false;
                break;
            case "K": // Saldo Promedio Ahorros
                detalleActual.llevaMarcaCambio = historicoParaComparar.saldopromedioahorros.HasValue ? historicoParaComparar.saldopromedioahorros.ToString() != detalleActual.valor_historico : false;
                break;
            case "L": // Saldo Promedio Aportes
                detalleActual.llevaMarcaCambio = historicoParaComparar.saldopromedioaportes.HasValue ? historicoParaComparar.saldopromedioaportes.ToString() != detalleActual.valor_historico : false;
                break;
            case "M": // Monto del Crédito SMLMV
                detalleActual.llevaMarcaCambio = historicoParaComparar.montodelcreditosmlv.HasValue ? historicoParaComparar.montodelcreditosmlv.ToString() != detalleActual.valor_historico : false;
                break;
            case "N": // Saldo Creditos Activos
                detalleActual.llevaMarcaCambio = historicoParaComparar.saldocreditosactivos.HasValue ? historicoParaComparar.saldocreditosactivos.ToString() != detalleActual.valor_historico : false;
                break;
            case "O": // Numero Creditos Activos
                detalleActual.llevaMarcaCambio = historicoParaComparar.numerocreditosactivos.HasValue ? historicoParaComparar.numerocreditosactivos.ToString() != detalleActual.valor_historico : false;
                break;
            case "P": // # Operaciones productos al mes
                detalleActual.llevaMarcaCambio = historicoParaComparar.operacionesproductosalmes.HasValue ? historicoParaComparar.operacionesproductosalmes.ToString() != detalleActual.valor_historico : false;
                break;
            case "S": // Sexo
                detalleActual.llevaMarcaCambio = historicoParaComparar.sexo.HasValue ? historicoParaComparar.sexo.ToString() != detalleActual.valor_historico : false;
                break;
            case "R": //Monto de operaciones en el mes
                detalleActual.llevaMarcaCambio = historicoParaComparar.montooperacioneslmes.HasValue ? historicoParaComparar.montooperacioneslmes.ToString() != detalleActual.valor_historico : false;
                break;
        }
    }

    void ObtenerValorHistoricoSegunVariableDelDetalle(HistoricoSegmentacion historico, Segmento_Detalles detalleActual)
    {
        switch (detalleActual.variable)
        {
            case "A": // Puntaje de Calificación
                detalleActual.valor_historico = historico.puntajecalificacion.HasValue ? historico.puntajecalificacion.ToString() : string.Empty;
                break;
            case "B": // Endeudamiento
                detalleActual.valor_historico = historico.endeudamiento.HasValue ? historico.endeudamiento.ToString() : string.Empty;
                break;
            case "C": // Ingresos Mensuales
                detalleActual.valor_historico = historico.ingresosmensuales.HasValue ? historico.ingresosmensuales.ToString() : string.Empty;
                break;
            case "D": // Edad
                detalleActual.valor_historico = historico.edad.HasValue ? historico.edad.ToString() : string.Empty;
                break;
            case "E": // Personas a cargo
                detalleActual.valor_historico = historico.personasacargo.HasValue ? historico.personasacargo.ToString() : string.Empty;
                break;
            case "F": // Tipo de Vivienda
                detalleActual.valor_historico = historico.tipovivienda.HasValue ? historico.tipovivienda.ToString() : string.Empty;
                break;
            case "G": // Estrato
                detalleActual.valor_historico = historico.estrato.HasValue ? historico.estrato.ToString() : string.Empty;
                break;
            case "H": // Nivel Academico
                detalleActual.valor_historico = historico.nivelacademico.HasValue ? historico.nivelacademico.ToString() : string.Empty;
                break;
            case "I": // Actividad Economica
                detalleActual.valor_historico = historico.actividadeconomica.HasValue ? historico.actividadeconomica.ToString() : string.Empty;
                break;
            case "K": // Saldo Promedio Ahorros
                detalleActual.valor_historico = historico.saldopromedioahorros.HasValue ? historico.saldopromedioahorros.ToString() : string.Empty;
                break;
            case "L": // Saldo Promedio Aportes
                detalleActual.valor_historico = historico.saldopromedioaportes.HasValue ? historico.saldopromedioaportes.ToString() : string.Empty;
                break;
            case "M": // Monto del Crédito SMLMV
                detalleActual.valor_historico = historico.montodelcreditosmlv.HasValue ? historico.montodelcreditosmlv.ToString() : string.Empty;
                break;
            case "N": // Saldo Creditos Activos
                detalleActual.valor_historico = historico.saldocreditosactivos.HasValue ? historico.saldocreditosactivos.ToString() : string.Empty;
                break;
            case "O": // Numero Creditos Activos
                detalleActual.valor_historico = historico.numerocreditosactivos.HasValue ? historico.numerocreditosactivos.ToString() : string.Empty;
                break;
            case "P": // # Operaciones productos al mes
                detalleActual.valor_historico = historico.operacionesproductosalmes.HasValue ? historico.operacionesproductosalmes.ToString() : string.Empty;
                break;
            case "S": // Sexo
                detalleActual.valor_historico = historico.sexo.HasValue ? historico.sexo.ToString() : string.Empty;
                break;
            case "R": // Monto de operaciones en el mes
                detalleActual.valor_historico = historico.montooperacioneslmes.HasValue ? historico.sexo.ToString() : string.Empty;
                break;
        }
    }

    void AlternarVisibilidadDropDownsDeUnaFila(GridViewRow row, bool permitirDropDown, TipoLista tipoListaParaLlenar = TipoLista.SinTipoLista)
    {
        TextBox textBoxValor = row.FindControl("txtValor") as TextBox;
        TextBox textBoxSegundoValor = row.FindControl("txtSegundoValor") as TextBox;
        TextBox textBoxValorHistorico = row.FindControl("txtValorHistorico") as TextBox;

        DropDownListGrid dropDownValor = row.FindControl("ddlValor") as DropDownListGrid;
        DropDownListGrid dropDownSegundoValor = row.FindControl("ddlSegundoValor") as DropDownListGrid;
        DropDownListGrid dropDownValorHistorico = row.FindControl("ddlValorHistorico") as DropDownListGrid;

        DropDownListGrid dropDownOperador = row.FindControl("ddlOperador") as DropDownListGrid;
        bool modificarOperador = false;

        textBoxValor.Visible = !permitirDropDown;
        textBoxSegundoValor.Visible = !permitirDropDown;
        textBoxValorHistorico.Visible = !permitirDropDown;
        dropDownValor.Visible = permitirDropDown;
        dropDownSegundoValor.Visible = permitirDropDown && dropDownOperador.SelectedValue == "9"; /// Si no es operador rango no lo muestro
        dropDownValorHistorico.Visible = permitirDropDown;

        // Switch se encargara de alternar campos entre textbox y dropdownlist
        if (permitirDropDown && tipoListaParaLlenar != TipoLista.SinTipoLista)
        {
            switch (tipoListaParaLlenar)
            {
                case TipoLista.NivelEscolaridad:
                    LlenarListasDesplegables(TipoLista.NivelEscolaridad, dropDownValor, dropDownSegundoValor, dropDownValorHistorico);
                    modificarOperador = true;
                    break;
                case TipoLista.Ciudades:
                    LlenarListasDesplegables(TipoLista.Ciudades, dropDownValor, dropDownSegundoValor, dropDownValorHistorico);
                    modificarOperador = true;
                    break;
                case TipoLista.Actividad_Laboral:
                    LlenarListasDesplegables(TipoLista.Actividad_Laboral, dropDownValor, dropDownSegundoValor, dropDownValorHistorico);
                    modificarOperador = true;
                    break;
                case TipoLista.Sexo:
                    LlenarListasDesplegables(TipoLista.Sexo, dropDownValor, dropDownSegundoValor, dropDownValorHistorico);
                    modificarOperador = true;
                    break;
            }
            if (modificarOperador)
            {
                dropDownOperador.Items.Clear();
                dropDownOperador.Items.Insert(0, new ListItem("<Seleccione un Item>", " "));
                dropDownOperador.Items.Insert(1, new ListItem("IGUAL", "1"));
                dropDownOperador.Items.Insert(2, new ListItem("DIFERENTE", "6"));
                dropDownOperador.Items.Insert(3, new ListItem("RANGO", "9"));
                dropDownOperador.DataBind();
            }
        }
    }

    void LimpiarPantallaDetalleYRegresar()
    {
        Site toolBar = (Site)this.Master;

        toolBar.MostrarGuardar(false);
        toolBar.MostrarRegresar(false);
        toolBar.MostrarConsultar(true);

        gvSegmentoActual.Visible = true;
        gvSegmentoActual.DataSource = null;
        gvSegmentoActual.DataBind();

        gvSegmentoAnterior.Visible = true;
        gvSegmentoAnterior.DataSource = null;
        gvSegmentoAnterior.DataBind();

        txtAnalisisOficial.Text = string.Empty;
        txtFechaAnalisis.Text = string.Empty;
        txtUsuarioAnalisis.Text = string.Empty;

        txtAnalisisOficial.ReadOnly = false;
        lblNoExisteSegmentoAnterior.Visible = false;

        idObjeto = string.Empty;

        mvPrincipal.SetActiveView(viewPrincipal);
    }


    #endregion

    protected void gvDetalle_RowCreated(object sender, GridViewRowEventArgs e)
    {
        List<HistoricoSegmentacion> titulos = Session["DTLISTA"] as List<HistoricoSegmentacion>;        

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow gvHeaderRow = e.Row;
            GridViewRow gvHeaderRowCopy = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            this.gvDetalle.Controls[0].Controls.AddAt(0, gvHeaderRowCopy);

            TableCell tcFirst = e.Row.Cells[0];
            tcFirst.RowSpan = 2;
            gvHeaderRowCopy.Cells.AddAt(0, tcFirst);

            TableCell tcSecond = e.Row.Cells[0];
            tcSecond.RowSpan = 2;
            gvHeaderRowCopy.Cells.AddAt(1, tcSecond);

            TableCell tcMergePeriodo = new TableCell();
            tcMergePeriodo.Text = "Actual";
            tcMergePeriodo.ColumnSpan = 5;
            gvHeaderRowCopy.Cells.AddAt(2, tcMergePeriodo);
            
            TableCell tcMergeAcumulado = new TableCell();
            tcMergeAcumulado.Text = "Anterior";
            tcMergeAcumulado.ColumnSpan = 5;
            gvHeaderRowCopy.Cells.AddAt(3, tcMergeAcumulado);

            TableCell tcLast = e.Row.Cells[10];
            tcLast.RowSpan = 2;
            gvHeaderRowCopy.Cells.AddAt(4, tcLast);

        }
    }





}