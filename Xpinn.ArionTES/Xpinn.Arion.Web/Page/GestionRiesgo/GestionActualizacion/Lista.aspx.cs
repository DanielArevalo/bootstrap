using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Services;

partial class Lista : GlobalWeb
{
    ActDatosServices _ActDatos = new ActDatosServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_ActDatos.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ActDatos.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CheckActualizados.Checked = true;
                txtFina.Text = DateTime.Now.ToString(GlobalWeb.gFormatoFecha);
                txtFina.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString(GlobalWeb.gFormatoFecha);
                CargarValoresConsulta(pConsulta, _ActDatos.CodigoPrograma);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ActDatos.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, _ActDatos.CodigoPrograma);

        if (CheckActualizados.Checked == true)
        {
            Actualizar();
        }
        if (ChecknoActu.Checked == true) {

            ConsultaNoActualizados();
        }      
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, _ActDatos.CodigoPrograma);
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[_ActDatos.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        Session[_ActDatos.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
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
            BOexcepcion.Throw(_ActDatos.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        string Fechaini = "";
        string fechafinal = "";
        Fechaini = String.Format("{0:M/d/yyyy}", txtfechaIni.Text);
        fechafinal = txtFina.Text;
        if (ValidarDatos())
        {
            try
            {
                List<ActDatos> lstConsulta = _ActDatos.ListarActDatos(Fechaini, fechafinal, ObtenerValores(), Usuario);

                gvLista.PageSize = pageSize;
                gvLista.EmptyDataText = emptyQuery;
                gvLista.DataSource = lstConsulta;

                if (lstConsulta.Count > 0)
                {
                    gvLista.Visible = true;
                    lblTotalRegs.Visible = true;
                    lblInfo.Visible = false;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    gvLista.DataBind();
                    CheckActualizados.Checked = true;
                    ChecknoActu.Checked = false;
                }
                else
                {
                    gvLista.Visible = false;
                    lblTotalRegs.Visible = false;
                    lblInfo.Visible = true;
                }
                Session["DTACTUALIZACION"] = lstConsulta;
                Session.Add(_ActDatos.CodigoPrograma + ".consulta", 1);
            }


            catch (Exception ex)
            {
                BOexcepcion.Throw(_ActDatos.CodigoPrograma, "Actualizar", ex);
            }

        }
    }

    private bool ValidarDatos()
    {

        if (txtfechaIni.Text == null || txtfechaIni.Text == "")
        {
            VerError("Determine una  fecha  inicial ");
            return false;
        }
        if (txtFina.Text == null || txtFina.Text == " ")
        {
            VerError("Determine una  fecha  Final ");
            return false;
        }
        return true;
    }

    private ActDatos ObtenerValores()
    {
        ActDatos vActDatos = new ActDatos();

        //if (txtCodigo.Text.Trim() != "")
        //    vActDatos.Id_update = Convert.ToInt64(txtCodigo.Text.Trim());
        if (txtidentificacion.Text.Trim() != "")
            vActDatos.Identificacion = Convert.ToString(txtidentificacion.Text.Trim());
        if (txtPrimerNom.Text.Trim() != "")
            vActDatos.Primer_nombre = Convert.ToString(txtPrimerNom.Text.Trim());
        if (txtSeguNom.Text.Trim() != "")
            vActDatos.Segundo_nombre = Convert.ToString(txtSeguNom.Text.Trim());
        if (txtPrimerape.Text.Trim() != "")
            vActDatos.Primer_Apellido = Convert.ToString(txtPrimerape.Text.Trim());

        return vActDatos;
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 id = Convert.ToInt32(e.Keys[0]);

            Session["ID"] = id;
            ctlMensaje.MostrarMensaje("Desea eliminar el registro seleccionado?");
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Session["ID"].ToString() != "")
            //{
            //    ActividadEco pactividad = new ActividadEco();
            //    pactividad.Cod_actividad = Convert.ToString(Session["ID"]);
            //    _ActDatos.EliminarActividad(pactividad, Usuario);
            //    Actualizar();
            //}
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ActDatos.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    protected void ConsultaNoActualizados()
    {
        try
        {
            List<ActDatos> lstConsulta = _ActDatos.ListarActDatosNoActualizado(ObtenerValores(), Usuario);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                CheckActualizados.Checked = false;
                ChecknoActu.Checked = true;
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(_ActDatos.CodigoPrograma + ".consulta", 1);
        }


        catch (Exception ex)
        {
            BOexcepcion.Throw(_ActDatos.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["DTACTUALIZACION"] != null)
        {
            StringBuilder sb = new StringBuilder();
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
            Response.AddHeader("Content-Disposition", "attachment;filename=GestionActualizacion.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
            gvLista.AllowPaging = true;
            gvLista.DataBind();

        }
    }


}