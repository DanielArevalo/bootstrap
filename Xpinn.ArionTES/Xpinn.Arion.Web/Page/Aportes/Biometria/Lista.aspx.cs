using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Xpinn.Util;

partial class Lista : GlobalWeb
{    
    private Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AfiliacionServicio.codigoprogramabiometria, "L");
                
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramabiometria, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboOficinas(ddlOficina);
                CargarValoresConsulta(pConsulta, AfiliacionServicio.codigoprogramabiometria);
                ViewState["CurrentAlphabet"] = "TODO";
                GenerateAlphabets();
                if (Session[AfiliacionServicio.CodigoPrograma + ".nid"] != null)
                {
                    txtNumeIdentificacion.Text = Session[AfiliacionServicio.codigoprogramabiometria + ".nid"].ToString();
                    Session.Remove(AfiliacionServicio.CodigoPrograma + ".nid");                    
                    Actualizar(0);
                }
                if (Session[AfiliacionServicio.codigoprogramabiometria + ".consulta"] != null)
                    Actualizar(0);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramabiometria, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AfiliacionServicio.codigoprogramabiometria);
        Actualizar(0);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtNumeIdentificacion.Text = "";
        LimpiarValoresConsulta(pConsulta, AfiliacionServicio.codigoprogramabiometria);
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["DTPERSONAS"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.Columns[0].Visible = false;
            gvLista.Columns[1].Visible = false;
            gvLista.Columns[2].Visible = false;
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTPERSONAS"];
            gvLista.DataBind();
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Personas.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
            Label lblBiometria = (Label)e.Row.FindControl("lblBiometria");
            if (lblBiometria != null)
            {
                if (lblBiometria.Text.Trim() != "")
                {
                    Int32 indicador = Convert.ToInt32(lblBiometria.Text);
                    if (indicador > 0)
                    {
                        Image imgBiometria = (Image)e.Row.FindControl("imgBiometria");
                        if (imgBiometria != null)
                        {
                            imgBiometria.Visible = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramabiometria + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AfiliacionServicio.codigoprogramabiometria + ".id"] = id;
        String tipo_persona = gvLista.Rows[gvLista.SelectedRow.RowIndex].Cells[4].Text;
        if (tipo_persona == "Juridica")
            Navegar(Pagina.Detalle);
        else
            Navegar("../Biometria/Nuevo.aspx");
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int pos = Convert.ToInt32(e.CommandArgument.ToString());
        }
        else
        {
            int pOrden = 0;
            try { pOrden = Convert.ToInt32(e.CommandName); Actualizar(pOrden); }
            catch { }
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[AfiliacionServicio.codigoprogramabiometria + ".id"] = id;
        String tipo_persona = gvLista.Rows[e.NewEditIndex].Cells[4].Text;
        if (tipo_persona == "Juridica")
            Navegar(Pagina.Nuevo);
        else
            Navegar("../Biometria/Nuevo.aspx");
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            try
            {
                //TerceroServicio.EliminarTercero(id, (Usuario)Session["usuario"]);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            Actualizar(0);
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramabiometria, "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar(0);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramabiometria, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar(int pOrden)
    {
        string sLetra = ViewState["CurrentAlphabet"].ToString();
        string sFiltro = "";
        if (sLetra != "TODO" && sLetra.Trim() != "")
        {
            sFiltro = " (primer_apellido Like '" + sLetra;
        }

        //Código nómina
        if (!string.IsNullOrWhiteSpace(txtCodigoNomina.Text))
            sFiltro += (!string.IsNullOrWhiteSpace(sFiltro) ? " and " : "") + (" cod_nomina = " + txtCodigoNomina.Text);

        try
        {
            Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
            List<Xpinn.Contabilidad.Entities.Tercero> lstConsulta = new List<Xpinn.Contabilidad.Entities.Tercero>();            
            lstConsulta = TerceroServicio.ListarTerceroSoloAfiliados(ObtenerValores(), sFiltro, (Usuario)Session["usuario"]);                                          
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                Session["DTPERSONAS"] = lstConsulta;
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                Session["DTPERSONAS"] = null;
            }
            Session.Add(TerceroServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.codigoprogramabiometria, "Actualizar", ex);
        }
    }

    private Xpinn.Contabilidad.Entities.Tercero ObtenerValores()
    {
        Xpinn.Contabilidad.Entities.Tercero vTercero = new Xpinn.Contabilidad.Entities.Tercero();
        if (ddlTipoPersona.SelectedValue.Trim() != "")
            vTercero.tipo_persona = Convert.ToString(ddlTipoPersona.SelectedValue.Trim());
        if (txtCodigo.Text.Trim() != "")
            vTercero.cod_persona = Convert.ToInt64(txtCodigo.Text.Trim());
        if (txtNumeIdentificacion.Text.Trim() != "")
            vTercero.identificacion = Convert.ToString(txtNumeIdentificacion.Text.Trim());
        if (txtNombres.Text.Trim() != "")
            vTercero.primer_nombre = Convert.ToString(txtNombres.Text.Trim());
        if (txtApellidos.Text.Trim() != "")
            vTercero.primer_apellido = Convert.ToString(txtApellidos.Text.Trim());
        if (ddlOficina.SelectedValue.Trim() != "")
            vTercero.cod_oficina = Convert.ToInt64(ddlOficina.SelectedValue.Trim());
        return vTercero;
    }



    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }

    private void GenerateAlphabets()
    {
        List<ListItem> alphabets = new List<ListItem>();
        ListItem alphabet = new ListItem();
        alphabet.Value = "TODO";
        alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
        alphabets.Add(alphabet);
        for (int i = 65; i <= 90; i++)
        {
            alphabet = new ListItem();
            alphabet.Value = Char.ConvertFromUtf32(i);
            alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
            alphabets.Add(alphabet);
        }
  
    }

    protected void Alphabet_Click(object sender, EventArgs e)
    {
        LinkButton lnkAlphabet = (LinkButton)sender;
        ViewState["CurrentAlphabet"] = lnkAlphabet.Text;
        this.GenerateAlphabets();
        gvLista.PageIndex = 0;
        Actualizar(0);
    }

    protected void ddlOrdenar_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar(0);
    }

    protected void LlenarComboOficinas(DropDownList ddlOficinas)
    {
        Xpinn.FabricaCreditos.Services.OficinaService oficinaService = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();

        ddlOficinas.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        ddlOficinas.DataTextField = "nombre";
        ddlOficinas.DataValueField = "codigo";
        ddlOficinas.DataBind();
        ddlOficinas.Enabled = true;

    }

}