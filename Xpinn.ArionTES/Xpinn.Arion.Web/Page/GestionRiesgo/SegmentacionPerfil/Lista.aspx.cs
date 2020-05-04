using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    SegmentacionPerfilService _SegmentacionPerfil = new SegmentacionPerfilService();
    Thread _tareaEjecucion;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_SegmentacionPerfil.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            //ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_SegmentacionPerfil.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, _SegmentacionPerfil.CodigoPrograma);
                //Actualizar();
                Control();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_SegmentacionPerfil.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, _SegmentacionPerfil.CodigoPrograma);
        Actualizar();
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["DTSEGMENTACIONPERFIL"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            GridView gvExportar = CopiarGridViewParaExportar(gvLista, "DTSEGMENTACIONPERFIL");
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTSEGMENTACIONPERFIL"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=SegmentacionPerfiles.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
            gvLista.AllowPaging = true;
            gvLista.DataBind();

        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, _SegmentacionPerfil.CodigoPrograma);
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[_SegmentacionPerfil.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[_SegmentacionPerfil.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(_SegmentacionPerfil.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Segmentacion()
    {
        try
        {

            List<PerfilRiesgoSeg> lstConsultaperfiles = _SegmentacionPerfil.ListarPerfilPesonaRiesgo(ObtenerValores(), Usuario);
            if (lstConsultaperfiles.Count > 0)
            {
                //si hay datos se  borra todo el contenido  de la tabla  para no repetir resultados
                Xpinn.Riesgo.Data.SegmentacionPerfilData limpiarperfiles = new Xpinn.Riesgo.Data.SegmentacionPerfilData();
                limpiarperfiles.limpiarperfilesRiesgo(Usuario);

                bool insertPerfil = false;
                //consultar  personas
                SegmentacionPerfil vSegmentacion = new SegmentacionPerfil();
                List<SegmentacionPerfil> lstConsulta = _SegmentacionPerfil.ListarSegmentacionPerfil(vSegmentacion, Usuario);
                Xpinn.Riesgo.Data.PerfilData califacar = new Xpinn.Riesgo.Data.PerfilData();
                //calificar persona
                List<SegmentacionPerfil> lsCalificada = califacar.Calificarpersona(lstConsulta, Usuario);
                // insertar persona calificada
                Xpinn.Riesgo.Data.SegmentacionPerfilData insertarperfil = new Xpinn.Riesgo.Data.SegmentacionPerfilData();
                insertPerfil = insertarperfil.Insertarcalificacionperfiles(lsCalificada, Usuario);
                //consulta persona calificada
                if (insertPerfil)
                {
                    Actualizar();
                }
            }
            else
            {
                bool insertPerfil = false;
                //consultar  personas
                SegmentacionPerfil vSegmentacion = new SegmentacionPerfil();
                List<SegmentacionPerfil> lstConsulta = _SegmentacionPerfil.ListarSegmentacionPerfil(vSegmentacion, Usuario);
                Xpinn.Riesgo.Data.PerfilData califacar = new Xpinn.Riesgo.Data.PerfilData();
                //calificar persona
                List<SegmentacionPerfil> lsCalificada = califacar.Calificarpersona(lstConsulta, Usuario);
                // insertar persona calificada
                Xpinn.Riesgo.Data.SegmentacionPerfilData insertarperfil = new Xpinn.Riesgo.Data.SegmentacionPerfilData();
                insertPerfil = insertarperfil.Insertarcalificacionperfiles(lsCalificada, Usuario);
                //consulta persona calificada
                if (insertPerfil)
                {
                    Actualizar();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_SegmentacionPerfil.CodigoPrograma, "Actualizar", ex);
        }
    }
    private void Actualizar()
    {
        try
        {

            PerfilRiesgoSeg segmentacion = new PerfilRiesgoSeg();
            int count = _SegmentacionPerfil.ExistePerfilPesonaRiesgo(segmentacion, Usuario);
            if (count > 0)
            {
                pFinal.Visible = true;
                List<PerfilRiesgoSeg> lstConsulta = _SegmentacionPerfil.ListarPerfilPesonaRiesgo(ObtenerValores(), Usuario);

                gvLista.PageSize = pageSize;
                gvLista.EmptyDataText = emptyQuery;
                Session["DTSEGMENTACIONPERFIL"] = lstConsulta;
                gvLista.DataSource = lstConsulta;

                if (lstConsulta.Count > 0)
                {
                    gvLista.Visible = true;
                    lblTotalRegs.Visible = true;
                    lblInfo.Visible = false;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    gvLista.DataBind();
                }
                else
                {
                    gvLista.Visible = false;
                    lblTotalRegs.Visible = false;
                    lblInfo.Visible = true;
                }

            }
            else
            {
                lblConfi.Visible = true;
            }
            Session.Add(_SegmentacionPerfil.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_SegmentacionPerfil.CodigoPrograma, "Actualizar", ex);
        }

    }

    PerfilRiesgoSeg ObtenerValores()
    {
        PerfilRiesgoSeg vPerfilRiesgo = new PerfilRiesgoSeg();

        if (txtNomdep.Text.Trim() != "")
            vPerfilRiesgo.primer_nombre = Convert.ToString(txtNomdep.Text.Trim());
        if (Textident.Text.Trim() != "")
            vPerfilRiesgo.identificacion = Convert.ToString(Textident.Text.Trim());
        if (txtValoracion.SelectedValue != "")
            vPerfilRiesgo.valoracion = Convert.ToInt64(txtValoracion.SelectedValue);
        if (txtPerfilderiesgo.SelectedValue != "")
            vPerfilRiesgo.perfil = Convert.ToString(txtPerfilderiesgo.SelectedValue);
        if (ddlTipoRol.SelectedItem.Value != "")
            vPerfilRiesgo.tipo_rol = ddlTipoRol.SelectedItem.Value;



        return vPerfilRiesgo;
    }

    //protected void btnContinuarMen_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Session["ID"].ToString() != "")
    //        {
    //            JurisdiccionDepa pJurisdiccionDepa = new JurisdiccionDepa();
    //            pJurisdiccionDepa.Cod_Depa = Convert.ToInt64(Session["ID"]);
    //            _SegmentacionPerfil.EliminarJurisdiccionDepa(pJurisdiccionDepa, Usuario);
    //            Actualizar();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        BOexcepcion.Throw(_SegmentacionPerfil.CodigoPrograma, "btnContinuarMen_Click", ex);
    //    }
    //}

    protected void btnEjecutar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Show();

    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        IniciarProceso();

        _tareaEjecucion = new Thread(new ThreadStart(EjecutaProceso));
        _tareaEjecucion.Start();


    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
        mpeProcesando.Hide();
    }


    #region Metodos de Ayuda

    public void IniciarProceso()
    {
        btnContinuar.Enabled = false;
        btnCancelar.Enabled = false;
        lblespera.Visible = true;
        lblConfi.Visible = false;
        mpeNuevo.Hide();
        mpeProcesando.Show();
        Image1.Visible = true;
        Session["Proceso"] = "INICIO";
        Timer1.Enabled = true;
    }

    public void TerminarProceso()
    {
        mpeProcesando.Hide();
        Image1.Visible = false;
        pFinal.Visible = true;
        lblespera.Visible = false;
        Session.Remove("Proceso");
        Timer1.Enabled = false;
        if (Session["Error"] != null)
        {
            if (string.IsNullOrWhiteSpace(Session["Error"].ToString()))
            {
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                lblConfi.Text = "Error: " + Session["Error"].ToString();
                lblError.Text = Session["Error"].ToString();
                Session.Remove("Error");
            }
        }
    }

    public void EjecutaProceso()
    {
        string sError = string.Empty;

        try
        {
            Segmentacion();
        }
        catch (Exception ex)
        {
            sError = ex.Message;
        }
        finally
        {
            Session["Error"] = sError;
            Session["Proceso"] = "FINAL";
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (Session["Proceso"] != null)
            if (Session["Proceso"].ToString() == "FINAL")
                TerminarProceso();
            else
                mpeProcesando.Show();
        else
            mpeProcesando.Hide();
    }


    #endregion


    /// <summary>
    /// Metodo para exportar la gridview de la cartera vencida
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ExportarSegmentoPerfiles(object sender, EventArgs e)
    {
        if (Session["DTSEGMENTACIONPERFIL"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            GridView gvExportar = CopiarGridViewParaExportar(gvLista, "DTSEGMENTACIONPERFIL");
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTSEGMENTACIONPERFIL"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=SegmentacionPerfiles.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
            gvLista.AllowPaging = true;
            gvLista.DataBind();

        }
    }

    private void Control()
    {
        try
        {

            PerfilRiesgoSeg segmentacion = new PerfilRiesgoSeg();
            int count = _SegmentacionPerfil.ExistePerfilPesonaRiesgo(segmentacion, Usuario);
            if (count > 0)
            {
                pFinal.Visible = true;
                lblConfi.Visible = false;
            }
            else
            {
                pFinal.Visible = false;
                lblConfi.Visible = true;
            }
            Session.Add(_SegmentacionPerfil.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_SegmentacionPerfil.CodigoPrograma, "Actualizar", ex);
        }

    }


}