using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Xpinn.Util;
using Microsoft.Reporting.WebForms;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;
using System.Linq;
using System.Globalization;


public partial class Lista : GlobalWeb
{
    CosechasService CosechaServices = new CosechasService();
    List<Cosechas> ListColocacion = new List<Cosechas>();

    /// <summary>
    /// Metodo de inicio de la pagina
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(CosechaServices.CodigoProgramaR, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CosechaServices.CodigoProgramaR, "Page_PreInit", ex);
        }
    }

    /// <summary>
    /// Metodo de carga de los componentes de la pagina
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarConsultar(true);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CosechaServices.CodigoProgramaR, "Page_Load", ex);
        }

    }

    /// <summary>
    /// Metodo usado para consultar los metodos que listan la información de la cosecha
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFechaInicio.Text != "" && txtFechaFinal.Text != "")
            {
                Colocacion();
                CarteraVencida();
                CalidadCosecha();
            }
            else
            {
                VerError("Debe ingresar las fechas");
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CosechaServices.CodigoProgramaR, "btnConsultar_Click", ex);
        }
    }

    protected void gvColocacion_OnRowDataBound(object sender, EventArgs e)
    {
        DataBoundTable(gvColocacion, 0);
    }

    protected void gvConceptos_OnDataBound(object sender, EventArgs e)
    {
        DataBoundTable(gvConceptos, 0);

    }

    protected void gvConceptos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        List<Cosechas> titulos = (List<Cosechas>)ViewState["DTCarteraVenci"];

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow gvHeaderRow = e.Row;
            //Creacion de la fila
            int cont = 1;
            int cont1 = 0;
            int pru = 0;
            GridViewRow gvHeaderRowCopy = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            this.gvConceptos.Controls[0].Controls.AddAt(0, gvHeaderRowCopy);
            TableCell tcMergeFec = new TableCell();
            tcMergeFec.Text = "Fechas";
            tcMergeFec.ColumnSpan = 1;
            gvHeaderRowCopy.Cells.AddAt(cont1, tcMergeFec);


            foreach (var fecha in titulos.Select(x => x.comportamiento.ToString("MMMM-yyyy")).Distinct().ToList())
            {

                if (pru == 0)
                {
                    TableCell tcMergeVrs = new TableCell();
                    tcMergeVrs.Text = Session["Grupos"].ToString();
                    tcMergeVrs.ColumnSpan = 1;
                    gvHeaderRowCopy.Cells.AddAt(cont, tcMergeVrs);
                    cont += 1;
                    pru++;
                }
                TableCell tcMergeVr = new TableCell();
                tcMergeVr.Text = fecha.ToString(new CultureInfo("es-MX"));
                tcMergeVr.ColumnSpan = 2;
                gvHeaderRowCopy.Cells.AddAt(cont, tcMergeVr);
                cont += 1;
            }

        }
    }

    protected void gvCalidad_OnDataBound(object sender, EventArgs e)
    {
        DataBoundTable(gvCalidad, 0);
    }

    protected void gvCalidad_RowCreated(object sender, GridViewRowEventArgs e)
    {
        List<Cosechas> titulos = (List<Cosechas>)ViewState["DTCarteraVenci"];

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow gvHeaderRow = e.Row;
            //Creacion de la fila
            int cont = 1;
            int cont1 = 0, pru = 0;
            GridViewRow gvHeaderRowCopy = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            this.gvCalidad.Controls[0].Controls.AddAt(0, gvHeaderRowCopy);
            TableCell tcMergeFec = new TableCell();
            tcMergeFec.Text = "Fechas";
            tcMergeFec.ColumnSpan = 1;
            gvHeaderRowCopy.Cells.AddAt(cont1, tcMergeFec);

            foreach (var fecha in titulos.Select(x => x.comportamiento.ToString("MMMM-yyyy")).Distinct().ToList())
            {
                if (pru == 0)
                {
                    TableCell tcMergeVrs = new TableCell();
                    tcMergeVrs.Text = Session["Grupos"].ToString();
                    tcMergeVrs.ColumnSpan = 1;
                    gvHeaderRowCopy.Cells.AddAt(cont, tcMergeVrs);
                    cont += 1;
                    pru++;
                }
                TableCell tcMergeVr = new TableCell();
                tcMergeVr.Text = fecha.ToString(new CultureInfo("es-MX"));
                tcMergeVr.ColumnSpan = 2;
                gvHeaderRowCopy.Cells.AddAt(cont, tcMergeVr);
                cont += 1;
            }
        }
    }


    #region Consultas

    /// <summary>
    /// Metodo que genera la lista del primer reporte de analisis de cosechas
    /// </summary>
    public void Colocacion()
    {
        DataTable dtDatos = new DataTable();
        DataRow drDatos;
        int column = 0;
        List<Cosechas> lstiColocacion = new List<Cosechas>();
        ListColocacion = CosechaServices.Colocacion(Convert.ToDateTime(txtFechaInicio.Text),
        Convert.ToDateTime(txtFechaFinal.Text), (Usuario)Session["usuario"]);
        foreach (Cosechas item in ListColocacion)
        {
            Cosechas cos = new Cosechas();
            cos.valor = item.valor;
            cos.stg_comportamiento = Convert.ToDateTime(item.stg_comportamiento).ToString("MMMM-yyyy");
            cos.cantidad = item.cantidad;
            cos.GrupoLinea = item.GrupoLinea;
            cos.Participacion_Cantidad = item.Participacion_Cantidad;
            cos.Participacion_valor = item.Participacion_valor;

            lstiColocacion.Add(cos);
        }
        if (ListColocacion.Count > 0)
        {
            #region CrearColumnasColocacion

            gvColocacion.Columns.Clear();
            BoundField Comportamiento = new BoundField();
            Comportamiento.HeaderText = "Comportamiento";
            Comportamiento.DataField = "stg_comportamiento";
            Comportamiento.DataFormatString = "";
            Comportamiento.ItemStyle.Width = 100;
            Comportamiento.ControlStyle.Width = 100;
            Comportamiento.HeaderStyle.Width = 100;
            gvColocacion.Columns.Add(Comportamiento);
            dtDatos.Columns.Add("Comportamiento", typeof(string));
            dtDatos.Columns["Comportamiento"].AllowDBNull = true;
            dtDatos.Columns["Comportamiento"].DefaultValue = "";

            BoundField GrupoLinea = new BoundField();
            GrupoLinea.HeaderText = "Grupo Linea";
            GrupoLinea.DataField = "GrupoLinea";
            GrupoLinea.DataFormatString = "";
            GrupoLinea.ItemStyle.Width = 100;
            GrupoLinea.ControlStyle.Width = 100;
            GrupoLinea.HeaderStyle.Width = 100;
            gvColocacion.Columns.Add(GrupoLinea);
            dtDatos.Columns.Add("GrupoLinea", typeof(string));
            dtDatos.Columns["GrupoLinea"].AllowDBNull = true;
            dtDatos.Columns["GrupoLinea"].DefaultValue = "";

            BoundField cantidad = new BoundField();
            cantidad.HeaderText = "Cantidad";
            cantidad.DataField = "cantidad";
            cantidad.DataFormatString = "";
            cantidad.ItemStyle.Width = 100;
            cantidad.ControlStyle.Width = 100;
            cantidad.HeaderStyle.Width = 100;
            gvColocacion.Columns.Add(cantidad);
            dtDatos.Columns.Add("cantidad", typeof(string));
            dtDatos.Columns["cantidad"].AllowDBNull = true;
            dtDatos.Columns["cantidad"].DefaultValue = "";

            BoundField porcentajeCantidad = new BoundField();
            porcentajeCantidad.HeaderText = "Par.Cantidad";
            porcentajeCantidad.DataField = "Participacion_Cantidad";
            porcentajeCantidad.DataFormatString = "";
            porcentajeCantidad.ItemStyle.Width = 100;
            porcentajeCantidad.ControlStyle.Width = 100;
            porcentajeCantidad.HeaderStyle.Width = 100;
            gvColocacion.Columns.Add(porcentajeCantidad);
            dtDatos.Columns.Add("Par.Cantidad", typeof(string));
            dtDatos.Columns["Par.Cantidad"].AllowDBNull = true;
            dtDatos.Columns["Par.Cantidad"].DefaultValue = "";

            BoundField valor = new BoundField();
            valor.HeaderText = "Valor (Millones)";
            valor.DataField = "valor";
            valor.DataFormatString = "{0:c0}";
            valor.ItemStyle.Width = 100;
            valor.ControlStyle.Width = 100;
            valor.HeaderStyle.Width = 100;
            gvColocacion.Columns.Add(valor);
            dtDatos.Columns.Add("valor", typeof(string));
            dtDatos.Columns["valor"].AllowDBNull = true;
            dtDatos.Columns["valor"].DefaultValue = "";

            BoundField porcentajeValor = new BoundField();
            porcentajeValor.HeaderText = "Par.Valor";
            porcentajeValor.DataField = "Participacion_valor";
            porcentajeValor.DataFormatString = "{0:c0}";
            porcentajeValor.ItemStyle.Width = 100;
            porcentajeValor.ControlStyle.Width = 100;
            porcentajeValor.HeaderStyle.Width = 100;
            gvColocacion.Columns.Add(porcentajeValor);
            dtDatos.Columns.Add("Par.Valor", typeof(string));
            dtDatos.Columns["Par.Valor"].AllowDBNull = true;
            dtDatos.Columns["Par.Valor"].DefaultValue = "";

            #endregion

            gvColocacion.Visible = true;
            ViewState["DTColocacion"] = totales(lstiColocacion);
            gvColocacion.DataSource = totales(lstiColocacion);
            gvColocacion.DataBind();
            BtnExpColo.Visible = true;
            lblTitulo1.Visible = true;
            BtnDetallado.Visible = true;
        }
        else
        {
            gvColocacion.Visible = false;
            BtnExpColo.Visible = false;
            lblTitulo1.Visible = false;
        }

    }

    /// <summary>
    /// Metodo usado para conocer el estado de la cartera Vencida
    /// </summary>
    public void CarteraVencida()
    {
        List<string> titulos = new List<string>();
        int conts = 0, contador = 0;
        titulos = CosechaServices.GenerarTitulos(Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFinal.Text), 2, (Usuario)Session["usuario"]);
        List<Cosechas> LtsCarteraVen = new List<Cosechas>();
        List<string> grupos = new List<string>();
        LtsCarteraVen = CosechaServices.ListarCarteraVencida(Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFinal.Text), (Usuario)Session["usuario"]);
        foreach (var item in LtsCarteraVen.Select(x => x.GrupoLinea).Distinct().ToList())
        {
            if (item != null)
            {
                grupos.Add(item);
            }
        }

        DataTable dtDatos = new DataTable();
        DataRow drDatos, drDats, dtLimpio;


        gvConceptos.Columns.Clear();
        BoundField ColumnBoundT = new BoundField();
        ColumnBoundT.HeaderText = "Comportamiento";
        ColumnBoundT.DataField = "comportamiento";
        ColumnBoundT.DataFormatString = "";
        ColumnBoundT.ItemStyle.Width = 100;
        ColumnBoundT.ControlStyle.Width = 100;
        ColumnBoundT.HeaderStyle.Width = 100;
        gvConceptos.Columns.Add(ColumnBoundT);
        dtDatos.Columns.Add("Comportamiento", typeof(string));
        dtDatos.Columns["Comportamiento"].AllowDBNull = true;
        dtDatos.Columns["Comportamiento"].DefaultValue = "";


        BoundField GrupoLinea = new BoundField();
        GrupoLinea.HeaderText = "Linea";
        GrupoLinea.DataField = "GrupoLinea";
        GrupoLinea.DataFormatString = "";
        GrupoLinea.ItemStyle.Width = 100;
        GrupoLinea.ControlStyle.Width = 100;
        GrupoLinea.HeaderStyle.Width = 100;
        gvConceptos.Columns.Add(GrupoLinea);
        dtDatos.Columns.Add("GrupoLinea", typeof(string));
        dtDatos.Columns["GrupoLinea"].AllowDBNull = true;
        dtDatos.Columns["GrupoLinea"].DefaultValue = "";

        //Agregar 2 columnas por cada titulo
        int con = 0;
        foreach (var titulo in titulos)
        {
            if (titulo != "" && titulo != null)
            {


                BoundField ColumnBoundV = new BoundField();
                ColumnBoundV.HeaderText = "Valor";
                ColumnBoundV.DataField = "VLR" + con;
                ColumnBoundV.DataFormatString = "{0:c0}";
                ColumnBoundV.ItemStyle.Width = 100;
                ColumnBoundV.ControlStyle.Width = 100;
                ColumnBoundV.HeaderStyle.Width = 100;
                gvConceptos.Columns.Add(ColumnBoundV);
                dtDatos.Columns.Add("VLR" + con, typeof(string));
                dtDatos.Columns["VLR" + con].DefaultValue = "";

                BoundField ColumnBoundA = new BoundField();
                ColumnBoundA.HeaderText = "Cant";
                ColumnBoundA.DataField = "CANT" + con;
                ColumnBoundA.DataFormatString = "{0:c0}";
                ColumnBoundA.ItemStyle.Width = 100;
                ColumnBoundA.ControlStyle.Width = 100;
                ColumnBoundA.HeaderStyle.Width = 100;
                gvConceptos.Columns.Add(ColumnBoundA);
                dtDatos.Columns.Add("CANT" + con, typeof(string));
                dtDatos.Columns["CANT" + con].DefaultValue = "";
                con++;
            }

        }
        Session["titulos"] = titulos;
        Session["Grupos"] = "Grupo";
        //Se realiza la consulta de la información de la cartera vencida
        CultureInfo _CultureInfo = new CultureInfo("es-ES", false);

        if (LtsCarteraVen.Count > 0)
        {
            foreach (string item in titulos)
            {
                DateTime MyDateTime = DateTime.Parse("01" + item, _CultureInfo);
                List<Cosechas> LtsComportamiento = new List<Cosechas>();
                LtsComportamiento = LtsCarteraVen.Where(x => x.comportamiento == MyDateTime).ToList();

                int cont = 0, previus = 0, existe = 0;
                if (LtsComportamiento.Count > 0)
                {
                    dtLimpio = dtDatos.NewRow();
                    drDats = dtDatos.NewRow();
                    foreach (var grupo in grupos)
                    {
                        drDatos = dtDatos.NewRow();
                        if (LtsComportamiento.Where(x => x.GrupoLinea == grupo).ToList().Count > 0)
                        {
                            cont = 0;
                            foreach (Cosechas comp in LtsComportamiento.Where(x => x.GrupoLinea == grupo).ToList())
                            {
                                drDatos["GrupoLinea"] = comp.GrupoLinea;
                                drDatos["VLR" + cont] = comp.valor.ToString();
                                drDatos["CANT" + cont] = comp.cantidad.ToString();
                                drDatos["comportamiento"] = MyDateTime.ToString("MMMM-yyyy");
                                cont++;

                            }
                            dtDatos.Rows.Add(drDatos);
                        }
                    }
                    if (LtsComportamiento.Count <= 1)
                    {
                        var s = LtsComportamiento.Select(x => x.GrupoLinea).FirstOrDefault();
                        if (s == null)
                        {
                            dtLimpio["comportamiento"] = MyDateTime.ToString("MMMM-yyyy");
                            dtDatos.Rows.Add(dtLimpio);
                        }

                    }
                    drDats["comportamiento"] = MyDateTime.ToString("MMMM-yyyy");
                    dtDatos.Rows.Add(drDats);
                    dtDatos.AcceptChanges();
                }
            }
        }

        if (dtDatos != null)
        {
            gvConceptos.Visible = true;
            ViewState["DTCarteraVenci"] = LtsCarteraVen;
            gvConceptos.DataSource = dtDatos;
            gvConceptos.DataBind();
            BtnExpCarVen.Visible = true;
            lblTitulo2.Visible = true;
        }
        else
        {
            gvConceptos.Visible = false;
            BtnExpCarVen.Visible = false;
            lblTitulo2.Visible = false;
        }


    }

    /// <summary>
    /// Metodo implementado para conocer la calidad de la cosecha
    /// </summary>
    public void CalidadCosecha()
    {
        List<string> titulos = new List<string>();

        titulos = CosechaServices.GenerarTitulos(Convert.ToDateTime(txtFechaInicio.Text),
            Convert.ToDateTime(txtFechaFinal.Text), 2, (Usuario)Session["usuario"]);
        List<Cosechas> LtsCalidadCosecha = new List<Cosechas>();
        List<string> grupos = new List<string>();
        LtsCalidadCosecha = CosechaServices.ListarCalidadCosecha(Convert.ToDateTime(txtFechaInicio.Text),
            Convert.ToDateTime(txtFechaFinal.Text), (Usuario)Session["usuario"]);
        foreach (var item in LtsCalidadCosecha.Select(x => x.GrupoLinea).Distinct().ToList())
        {
            if (item != null)
            {
                grupos.Add(item);
            }
        }

        DataTable dtDatos = new DataTable();
        DataRow drDatos, drDats, dtLimpio;


        gvCalidad.Columns.Clear();
        BoundField ColumnBoundT = new BoundField();
        ColumnBoundT.HeaderText = "Comportamiento";
        ColumnBoundT.DataField = "comportamiento";
        ColumnBoundT.DataFormatString = "";
        ColumnBoundT.ItemStyle.Width = 100;
        ColumnBoundT.ControlStyle.Width = 100;
        ColumnBoundT.HeaderStyle.Width = 100;
        gvCalidad.Columns.Add(ColumnBoundT);
        dtDatos.Columns.Add("Comportamiento", typeof(string));
        dtDatos.Columns["Comportamiento"].AllowDBNull = true;
        dtDatos.Columns["Comportamiento"].DefaultValue = "";

        BoundField GrupoLinea = new BoundField();
        GrupoLinea.HeaderText = "Grupo Linea";
        GrupoLinea.DataField = "GrupoLinea";
        GrupoLinea.DataFormatString = "";
        GrupoLinea.ItemStyle.Width = 100;
        GrupoLinea.ControlStyle.Width = 100;
        GrupoLinea.HeaderStyle.Width = 100;
        gvCalidad.Columns.Add(GrupoLinea);
        dtDatos.Columns.Add("GrupoLinea", typeof(string));
        dtDatos.Columns["GrupoLinea"].AllowDBNull = true;
        dtDatos.Columns["GrupoLinea"].DefaultValue = "";


        //Agregar 2 columnas por cada titulo
        int con = 0;
        foreach (string titulo in titulos)
        {

            if (titulo != "" && titulo != null)
            {

                BoundField ColumnBoundV = new BoundField();
                ColumnBoundV.HeaderText = "Valor";
                ColumnBoundV.DataField = "VLR" + con;
                ColumnBoundV.DataFormatString = "{0:c0}";
                ColumnBoundV.ItemStyle.Width = 100;
                ColumnBoundV.ControlStyle.Width = 100;
                ColumnBoundV.HeaderStyle.Width = 100;
                gvCalidad.Columns.Add(ColumnBoundV);
                dtDatos.Columns.Add("VLR" + con, typeof(string));
                dtDatos.Columns["VLR" + con].DefaultValue = "";

                BoundField ColumnBoundA = new BoundField();
                ColumnBoundA.HeaderText = "Cant";
                ColumnBoundA.DataField = "CANT" + con;
                ColumnBoundA.DataFormatString = "{0:c0}";
                ColumnBoundA.ItemStyle.Width = 100;
                ColumnBoundA.ControlStyle.Width = 100;
                ColumnBoundA.HeaderStyle.Width = 100;
                gvCalidad.Columns.Add(ColumnBoundA);
                dtDatos.Columns.Add("CANT" + con, typeof(string));
                dtDatos.Columns["CANT" + con].DefaultValue = "";
                con++;
            }
        }
        Session["titulos"] = titulos;
        Session["Grupos"] = "Grupo Linea";
        //Se realiza la consulta de la información de la clidad de la cosecha
        CultureInfo _CultureInfo = new CultureInfo("es-ES", false);

        if (LtsCalidadCosecha.Count > 0)
        {
            foreach (string item in titulos)
            {
                DateTime MyDateTime = DateTime.Parse("01" + item, _CultureInfo);
                List<Cosechas> LtsComportamiento = new List<Cosechas>();
                LtsComportamiento = LtsCalidadCosecha.Where(x => x.comportamiento == MyDateTime).ToList();

                int cont = 0, previus = 0, existe = 0;
                if (LtsComportamiento.Count > 0)
                {
                    dtLimpio = dtDatos.NewRow();
                    drDats = dtDatos.NewRow();
                    foreach (var grupo in grupos)
                    {
                        drDatos = dtDatos.NewRow();
                        if (LtsComportamiento.Where(x => x.GrupoLinea == grupo).ToList().Count > 0)
                        {
                            cont = 0;
                            foreach (Cosechas comp in LtsComportamiento.Where(x => x.GrupoLinea == grupo).ToList())
                            {
                                drDatos["GrupoLinea"] = comp.GrupoLinea;
                                drDatos["VLR" + cont] = comp.Participacion_valor.ToString();
                                drDatos["CANT" + cont] = comp.Participacion_Cantidad.ToString();
                                drDatos["comportamiento"] = MyDateTime.ToString("MMMM-yyyy");
                                cont++;

                            }
                            dtDatos.Rows.Add(drDatos);
                        }
                    }
                    if (LtsComportamiento.Count <= 1)
                    {
                        var s = LtsComportamiento.Select(x => x.GrupoLinea).FirstOrDefault();
                        if (s == null)
                        {
                            dtLimpio["comportamiento"] = MyDateTime.ToString("MMMM-yyyy");
                            dtDatos.Rows.Add(dtLimpio);
                        }

                    }
                    drDats["comportamiento"] = MyDateTime.ToString("MMMM-yyyy");
                    dtDatos.Rows.Add(drDats);
                    dtDatos.AcceptChanges();
                }
            }

            if (dtDatos != null)
            {
                gvCalidad.Visible = true;
                ViewState["DTCaliCart"] = dtDatos;
                gvCalidad.DataSource = dtDatos;
                gvCalidad.DataBind();
                BtnExpCalidad.Visible = true;
                lblTitulo3.Visible = true;
            }
            else
            {
                gvCalidad.Visible = false;
                BtnExpCalidad.Visible = false;
                lblTitulo3.Visible = false;
            }
        }
    }

    #endregion

    #region exportar

    /// <summary>
    /// Metodo para exportar la gridview de la colocación
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ExportarColo(object sender, EventArgs e)
    {
        VerError("");
        if (gvColocacion.Rows.Count > 0 && ViewState["DTColocacion"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvColocacion.AllowPaging = false;
            gvColocacion.DataSource = ViewState["DTColocacion"];
            gvColocacion.DataBind();
            gvColocacion.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvColocacion);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Colocacion.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            gvColocacion.AllowPaging = true;
            gvColocacion.DataSource = ViewState["DTColocacion"];
            gvColocacion.DataBind();
            Response.End();
        }
    }

    /// <summary>
    /// Metodo para exportar la gridview de la cartera vencida
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ExportarCarVen(object sender, EventArgs e)
    {
        VerError("");
        if (gvConceptos.Rows.Count > 0 && ViewState["DTCarteraVenci"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvConceptos.AllowPaging = false;
            gvConceptos.DataSource = ViewState["DTCarteraVenci"];
            gvConceptos.DataBind();
            gvConceptos.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvConceptos);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=CarteraVencida.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            gvConceptos.AllowPaging = true;
            gvConceptos.DataSource = ViewState["DTCarteraVenci"];
            gvConceptos.DataBind();
            Response.End();
        }

    }

    /// <summary>
    /// Metodo para exportar la gridview de la calidad de la cosecha
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ExpCalidadCart(object sender, EventArgs e)
    {
        VerError("");
        if (gvCalidad.Rows.Count > 0 && ViewState["DTCaliCart"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvCalidad.AllowPaging = false;
            gvCalidad.DataSource = ViewState["DTCaliCart"];
            gvCalidad.DataBind();
            gvCalidad.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvCalidad);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=CalidadCosecha.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            gvCalidad.AllowPaging = true;
            gvCalidad.DataSource = ViewState["DTCaliCart"];
            gvCalidad.DataBind();
            Response.End();
        }

    }

    public void ExportarDet(object sender, EventArgs e)
    {

        ExportarExcel.ExportToExcelXls(CosechaServices.Colocacion(Convert.ToDateTime(txtFechaInicio.Text),
        Convert.ToDateTime(txtFechaFinal.Text), (Usuario)Session["usuario"]), "Cosechas", 1);
        ExportarExcel.AgregarNuevoSheet(CosechaServices.Colocacion(Convert.ToDateTime(txtFechaInicio.Text),
        Convert.ToDateTime(txtFechaFinal.Text), (Usuario)Session["usuario"]), "Cosechas.xlsx");
    }
    #endregion

    #region MetodosDinamicos

    public void styles(GridViewRow row)
    {
        for (var i = 1; i < row.Cells.Count; i++)
        {
            row.Cells[i].BackColor = Color.CornflowerBlue;
            row.Cells[i].Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
        }
    }

    public void DataBoundTable(GridView table, int ponerstylo)
    {
        for (int rowIndex = table.Rows.Count - 1; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = table.Rows[rowIndex];
            if (table.Rows.Count != rowIndex + 1)
            {
                GridViewRow previusrow = table.Rows[rowIndex + 1];
                if (row.Cells[0].Text == previusrow.Cells[0].Text)
                {
                    row.Cells[0].RowSpan = previusrow.Cells[0].RowSpan < 2 ? 2 : previusrow.Cells[0].RowSpan + 1;
                    previusrow.Cells[0].Visible = false;
                }
                else if (ponerstylo != 1)
                {
                    styles(row);
                }
            }
            else if (ponerstylo != 1)
            {
                styles(row);
            }

        }
    }

    public List<Cosechas> totales(List<Cosechas> List)
    {
        //trae los datos en la lista datos
        List<Cosechas> nuevaLista = new List<Cosechas>();
        Cosechas cost = new Cosechas();
        string fechaTemp;
        var filaInicio = 0;
        fechaTemp = List[0].stg_comportamiento;
        List.Add(cost);
        for (int i = 0; i < List.Count; i++)
        {
            if (fechaTemp != List[i].stg_comportamiento)
            {
                decimal totalCantidad = 0, totalValor = 0, totalPatiCan = 0, TotalPartiVal = 0;
                //Suma datos de filas anteriores
                for (int j = filaInicio; j < i; j++)
                {
                    totalCantidad = totalCantidad + List[j].cantidad;
                    totalValor = totalValor + List[j].valor;
                    totalPatiCan = List[j].Participacion_Cantidad == null ? 0 : totalPatiCan + Convert.ToInt32(List[j].Participacion_Cantidad.Replace("%", ""));
                    TotalPartiVal = List[j].Participacion_valor == null ? 0 : TotalPartiVal + Convert.ToInt32(List[j].Participacion_valor.Replace("%", ""));
                }

                Cosechas cos = new Cosechas();
                cos.cantidad = totalCantidad;
                cos.stg_comportamiento = List[i - 1].stg_comportamiento;
                cos.valor = totalValor;
                cos.GrupoLinea = "Total";
                cos.Participacion_Cantidad = totalPatiCan + "%";
                cos.Participacion_valor = TotalPartiVal + "%";
                nuevaLista.Add(cos);
                //agrega fila de total
                fechaTemp = List[i].stg_comportamiento;
                //Agrega la primera fila del nuevo grupo
                if (i < List.Count - 1)
                    if (List[i].stg_comportamiento != null)
                        nuevaLista.Add(List[i]);
                filaInicio = i;
            }
            else
            {
                if (List[i].stg_comportamiento != null)
                    nuevaLista.Add(List[i]);
            }
        }
        return nuevaLista;
    }

    #endregion


}