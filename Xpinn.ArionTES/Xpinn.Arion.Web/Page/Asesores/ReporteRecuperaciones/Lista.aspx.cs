using System;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Services;
using Xpinn.Caja.Services;
using Xpinn.Caja.Entities;
using Xpinn.Asesores.Entities;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Configuration;
using System.Reflection;

public partial class Page_Asesores_ReporteRecuperaciones_Lista : GlobalWeb
{
    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    ClienteService clienteServicio = new ClienteService();
    CreditoService creditoServicio = new CreditoService();
    CreditosService creditoServicios = new CreditosService();
    ExcelService excelServicio = new ExcelService();
    DiligenciaService reporteServicio = new DiligenciaService();
    Xpinn.FabricaCreditos.Services.UsuarioAtribucionesService UsuarioAtribucionesServicio = new Xpinn.FabricaCreditos.Services.UsuarioAtribucionesService();
    Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones vUsuarioAtribuciones = new Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones();
    TransaccionCajaService cajaServicio = new TransaccionCajaService();
    MovGralCreditoService movGrlService = new MovGralCreditoService();
    Usuario usuario = new Usuario();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(reporteServicio.CodigoProgramaReportes, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgramaReportes, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Panel3.Visible = false;
                Panel4.Visible = false;
                Panel1.Visible = false;
                Panel2.Visible = false;
                ddlAsesores.Visible = false;
                Labelusuario.Visible = false;
                lblZona.Visible = false;
                ddlZona.Visible = false;
                //Agregado para cargar zonas
                LlenarComboZonasvalidado(ddlZona);
                CargarValoresConsulta(pConsulta, reporteServicio.CodigoProgramaReportes);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgramaReportes, "Page_Load", ex);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    /// <summary>
    /// Método que permite generar el reporte segùn las condiciones dadas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, reporteServicio.CodigoProgramaReportes);

            switch (ddlConsultar.SelectedIndex)
            {
                case 1:
                    Session["op1"] = 1;
                    break;
                case 2:
                    Session["op1"] = 2;
                    break;
            }
            Actualizar();
            //CalcularTotal();
        }
    }

    /// <summary>
    /// Méotodo para limpiar datos de la conuslta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, reporteServicio.CodigoProgramaReportes);
        GvDiligencias.DataSource = null;
        GvDiligencias.DataBind();
        gvReportemovdiario.DataSource = null;
        gvReportemovdiario.DataBind();
        lblTotalRegs.Text = "";
        txtFechaInidilig.Text = "";
        txtFechaFinDilig.Text = "";
        txtFechaIni.Text = "";
        txtFechaFin.Text = "";
    }


    protected void gvReportemovdiario_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvReportemovdiario.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgramaReportes, "gvReportemovdiario_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para traer los datos según las condiciones ingresadas
    /// </summary>
    private void Actualizar()
    {
        try
        {
            Usuario usuap = (Usuario)Session["usuario"];

            // Determinar el còdigo del usuario y determinar si el usuario es un ejecutivo
            int cod = Convert.ToInt32(usuap.codusuario);
            List<Diligencia> lstConsulta = new List<Diligencia>();
            // Generar el reporte
            switch ((int)Session["op1"])
            {

                case 1:

                    if (ddlAsesores.SelectedValue == "0" || ddlAsesores.SelectedValue == "")
                    {
                        DateTime fechaini, fechafinal;
                        Int64 cod_zona = Convert.ToInt64(ddlZona.SelectedValue);

                        fechafinal = txtFechaFin.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaFin.Text);
                        fechaini = txtFechaIni.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaIni.Text);

                        //List<Diligencia> lstConsulta = new List<Diligencia>();
                        lstConsulta = reporteServicio.ListarReporteacuerdosTodosUsuarios((Usuario)Session["usuario"], fechaini, fechafinal, cod_zona);

                        gvReportemovdiario.EmptyDataText = emptyQuery;
                        gvReportemovdiario.DataSource = lstConsulta;

                        if (lstConsulta.Count > 0)
                        {
                            mvLista.ActiveViewIndex = 0;
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                            gvReportemovdiario.DataBind();
                            ValidarPermisosGrilla(gvReportemovdiario);
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                    }
                    else
                    {

                        if (txtFechaIni.Text == "" && txtFechaFin.Text == "")
                        {
                            Label3.Text = "falta fecha inicial";
                            Label4.Text = " falta fecha final";
                        }
                        else
                        {
                            DateTime fechaini, fechafinal;
                            Int64 usuarioacuerdo;
                            Int64 cod_zona = Convert.ToInt64(ddlZona.SelectedValue);
                            fechafinal = txtFechaFin.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaFin.Text);
                            fechaini = txtFechaIni.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaIni.Text);
                            usuarioacuerdo = Convert.ToInt64(ddlAsesores.SelectedValue);
                            //List<Diligencia> lstConsulta = new List<Diligencia>();
                            lstConsulta = reporteServicio.ListarReporteacuerdos((Usuario)Session["usuario"], fechaini, fechafinal, usuarioacuerdo);
                            gvReportemovdiario.EmptyDataText = emptyQuery;
                            gvReportemovdiario.DataSource = lstConsulta;
                            if (lstConsulta.Count > 0)
                            {
                                mvLista.ActiveViewIndex = 0;
                                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                                gvReportemovdiario.DataBind();
                                ValidarPermisosGrilla(gvReportemovdiario);
                            }
                            else
                            {
                                mvLista.ActiveViewIndex = -1;
                            }
                        }
                        Session.Add(reporteServicio.CodigoProgramaReportes + ".consulta", 1);
                    }
                    break;

                case 2:

                    if (ddlAsesores.SelectedValue == "0" || ddlAsesores.SelectedValue == "")
                    {
                        DateTime fechainidil, fechafinaldil;
                        Int64 cod_zona = Convert.ToInt64(ddlZona.SelectedValue);
                        fechafinaldil = txtFechaFinDilig.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaFinDilig.Text);
                        fechainidil = txtFechaInidilig.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaInidilig.Text);

                        //List<Diligencia> lstConsulta = new List<Diligencia>();
                        lstConsulta = reporteServicio.ListarReporteDiligenciaTodos((Usuario)Session["usuario"], fechainidil, fechafinaldil, cod_zona);

                        gvReportemovdiario.EmptyDataText = emptyQuery;
                        gvReportemovdiario.DataSource = lstConsulta;
                        if (lstConsulta.Count > 0)
                        {
                            mvLista.ActiveViewIndex = 0;
                            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                            gvReportemovdiario.DataBind();
                            ValidarPermisosGrilla(gvReportemovdiario);
                        }
                        else
                        {
                            mvLista.ActiveViewIndex = -1;
                        }
                    }
                    else
                    {
                        DateTime fechainidil, fechafinaldil;
                        Int64 usuario;
                        if (txtFechaInidilig.Text == "" && txtFechaFinDilig.Text == "")
                        {
                            Label3.Text = "falta fecha inicial";
                            Label4.Text = " falta fecha final";
                        }
                        else
                        {

                            fechafinaldil = txtFechaFinDilig.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaFinDilig.Text);
                            fechainidil = txtFechaInidilig.Text == "" ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(txtFechaInidilig.Text);
                            usuario = Convert.ToInt64(ddlAsesores.SelectedValue);
                            //List<Diligencia> lstConsulta = new List<Diligencia>();
                            lstConsulta = reporteServicio.ListarReporteDiligencia((Usuario)Session["usuario"], fechainidil, fechafinaldil, usuario);
                            GvDiligencias.EmptyDataText = emptyQuery;
                            GvDiligencias.DataSource = lstConsulta;
                            if (lstConsulta.Count > 0)
                            {
                                mvLista.SetActiveView(GvListaDiligencias);
                                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                                GvDiligencias.DataBind();
                                ValidarPermisosGrilla(GvDiligencias);
                            }
                            else
                            {
                                mvLista.ActiveViewIndex = -1;
                            }
                        }
                    }
                    break;

            }
            foreach (GridViewRow fila in GvDiligencias.Rows)
            {
                if (fila.Cells[6].Text == "01/01/0001")
                    fila.Cells[6].Text = "";
            }
            Session["DTCREDITOS"] = null;
            if (lstConsulta.Count > 0)
                Session["DTCREDITOS"] = lstConsulta;

        }
        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
    }

    protected void CalcularTotal()
    {

        decimal total = 0;
        List<ReportesCaja> LstReportesCaja = new List<ReportesCaja>();
        LstReportesCaja = (List<ReportesCaja>)Session["ReportesCaja"];


        foreach (GridViewRow fila in gvReportemovdiario.Rows)
        {
            total = decimal.Parse(fila.Cells[50].Text);

        }


    }


    /// <summary>
    /// Método para obtener datos del filtro
    /// </summary>
    /// <returns></returns>
    private string obtFiltro()
    {

        String filtro = String.Empty;

        return filtro;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvReportemovdiario.Rows.Count > 0)
        {
            //Modificado para generar formato de excel sin hipervinculos
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            ExpGrilla expGrilla = new ExpGrilla();
            List<Diligencia> lista = (List<Diligencia>)Session["DTCREDITOS"];
            sw = expGrilla.ObtenerGrilla(gvReportemovdiario, null);
            Response.Write("<div>" + expGrilla.style + "</div>");
            Response.Output.Write("<div>" + sw.ToString() + "</div>");
            Response.Flush();
            Response.End();

            //Response.Write(sb.ToString());
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //Page pagina = new Page();
            //dynamic form = new HtmlForm();
            //gvReportemovdiario.DataSource = Session["DTCREDITOS"];
            //gvReportemovdiario.DataBind();
            //gvReportemovdiario.EnableViewState = false;
            //pagina.EnableEventValidation = false;
            //pagina.DesignerInitialize();
            //pagina.Controls.Add(form);
            //form.Controls.Add(gvReportemovdiario);          
            //pagina.RenderControl(htw);
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            //Response.Charset = "UTF-8";
            //Response.ContentEncoding = Encoding.Default;
            //Response.Write(sb.ToString());
            //Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    protected void btnExportarpoliza_Click(object sender, EventArgs e)
    {
        if (gvReportemovdiario.Rows.Count > 0)
        {
            //Modificado para generar formato de excel sin hipervinculos
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            ExpGrilla expGrilla = new ExpGrilla();
            List<Diligencia> lista = (List<Diligencia>)Session["DTCREDITOS"];
            sw = expGrilla.ObtenerGrilla(gvReportemovdiario, null);
            Response.Write("<div>" + expGrilla.style + "</div>");
            Response.Output.Write("<div>" + sw.ToString() + "</div>");
            Response.Flush();
            Response.End();

            //StringBuilder sb = new StringBuilder();
            //StringWriter sw = new StringWriter(sb);
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //Page pagina = new Page();
            //dynamic form = new HtmlForm();
            //gvReportemovdiario.DataSource = Session["DTCREDITOS"];
            //gvReportemovdiario.DataBind();
            //gvReportemovdiario.EnableViewState = false;
            //pagina.EnableEventValidation = false;
            //pagina.DesignerInitialize();
            //pagina.Controls.Add(form);
            //form.Controls.Add(gvReportemovdiario);
            //pagina.RenderControl(htw);
            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            //Response.Charset = "UTF-8";
            //Response.ContentEncoding = Encoding.Default;
            //Response.Write(sb.ToString());
            //Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }




    protected void ddlConsultar_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlConsultar.SelectedIndex == 1)
        {

            Panel1.Visible = true;
            Panel2.Visible = true;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Usuario usuap = (Usuario)Session["usuario"];
            ddlAsesores.Visible = true;
            Labelusuario.Visible = true;
            //Agregado para consulta por zonas
            lblZona.Visible = true;
            ddlZona.Visible = true;
            EjecutivoService serviceEjecutivo = new EjecutivoService();
            ddlAsesores.DataSource = serviceEjecutivo.ListartodosUsuarios((Usuario)Session["usuario"]);
            ddlAsesores.DataTextField = "NombreCompleto";
            ddlAsesores.DataValueField = "IdEjecutivo";
            ddlAsesores.DataBind();
            ddlAsesores.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

        }

        if (ddlConsultar.SelectedIndex == 2)
        {
            Panel3.Visible = true;
            Panel4.Visible = true;
            Panel1.Visible = false;
            Panel2.Visible = false;
            Usuario usuap = (Usuario)Session["usuario"];
            ddlAsesores.Visible = true;
            Labelusuario.Visible = true;
            //Agregado para consulta por zonas
            lblZona.Visible = true;
            ddlZona.Visible = true;
            EjecutivoService serviceEjecutivo = new EjecutivoService();
            ddlAsesores.DataSource = serviceEjecutivo.ListartodosUsuarios((Usuario)Session["usuario"]);
            ddlAsesores.DataTextField = "NombreCompleto";
            ddlAsesores.DataValueField = "IdEjecutivo";
            ddlAsesores.DataBind();
            ddlAsesores.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

        }
    }

    Int64 subtotalvaloracuerdo = 0;

    protected void gvReportemovdiario_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            subtotalvaloracuerdo += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "valor_acuerdo"));


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[7].Text = "Total:";
            e.Row.Cells[8].Text = subtotalvaloracuerdo.ToString("c");
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Font.Bold = true;
        }
    }

    protected void GvDiligencias_PageIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GvDiligencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GvDiligencias.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteServicio.CodigoProgramaReportes, "GvDiligenciaso_PageIndexChanging", ex);
        }
    }

    protected void GvDiligencias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            subtotalvaloracuerdo += Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "valor_acuerdo"));


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[7].Text = "Total:";
            e.Row.Cells[8].Text = subtotalvaloracuerdo.ToString("c");
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Font.Bold = true;
        }
    }

    protected void BtnExportarDilig_Click(object sender, EventArgs e)
    {
        List<Diligencia> lstConsulta = (List<Diligencia>)Session["DTCREDITOS"];
        if (Session["DTCREDITOS"] != null)
        {
            string fic = "ReporteRecuperaciones.csv";
            try
            {
                File.Delete(fic);
            }
            catch
            {
            }
            // Generar el archivo
            bool bTitulos = false;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("") + fic, true);
            foreach (Diligencia item in lstConsulta)
            {
                string texto = "";
                FieldInfo[] propiedades = item.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                if (!bTitulos)
                {
                    foreach (FieldInfo f in propiedades)
                    {
                        try
                        {
                            texto += f.Name.Split('>').First().Replace("<", "") + ";";
                        }
                        catch { texto += ";"; };
                    }
                    sw.WriteLine(texto);
                    bTitulos = true;
                }
                texto = "";
                int i = 0;
                foreach (FieldInfo f in propiedades)
                {
                    i += 1;
                    object valorObject = f.GetValue(item);
                    // Si no soy nulo
                    if (valorObject != null)
                    {
                        string valorString = valorObject.ToString();
                        if (valorObject is DateTime)
                        {
                            DateTime? fechaValidar = valorObject as DateTime?;
                            if (fechaValidar.Value != DateTime.MinValue)
                            {
                                texto += f.GetValue(item) + ";";
                            }
                            else
                            {
                                texto += "" + ";";
                            }
                        }
                        else
                        {
                            texto += f.GetValue(item) + ";";
                            texto.Replace("\r", "").Replace(";", "");
                        }
                    }
                    else
                    {
                        texto += "" + ";";
                    }
                }
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

        }
    }

    protected void GvDiligencias_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void LlenarComboZonasvalidado(DropDownList ddlZona)
    {
        Usuario usuap = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = creditoServicios.UsuarioEsEjecutivo(cod, (Usuario)Session["Usuario"]);
        if (consulta >= 1)
        {
            LlenarComboZona(ddlZona);
        }
        else
        {
            LlenarComboZonaXasesor(ddlZona);
        }
    }


    protected void LlenarComboZona(DropDownList ddlZona)
    {
        Xpinn.Asesores.Services.CiudadService ciudadService = new Xpinn.Asesores.Services.CiudadService();
        Xpinn.Asesores.Entities.Ciudad zona = new Xpinn.Asesores.Entities.Ciudad();
        zona.tipo = 5;
        ddlZona.DataSource = ciudadService.ListarZonas((Usuario)Session["usuario"]);
        ddlZona.DataTextField = "NOMCIUDAD";
        ddlZona.DataValueField = "CODCIUDAD";
        ddlZona.DataBind();
        ddlZona.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboZonaXasesor(DropDownList ddlZona)
    {
        Xpinn.Asesores.Services.CiudadService ciudadService = new Xpinn.Asesores.Services.CiudadService();
        Xpinn.Asesores.Entities.Ciudad zona = new Xpinn.Asesores.Entities.Ciudad();
        zona.tipo = 5;
        ddlZona.DataSource = ciudadService.ListarZonas((Usuario)Session["usuario"]);
        ddlZona.DataTextField = "NOMCIUDAD";
        ddlZona.DataValueField = "CODCIUDAD";
        ddlZona.DataBind();
        ddlZona.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Seleccione un Item>", "0"));
    }

}
