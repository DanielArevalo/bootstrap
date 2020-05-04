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
    private Xpinn.Aportes.Services.PersonaAutorizacionService TerceroServicio = new Xpinn.Aportes.Services.PersonaAutorizacionService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TerceroServicio.CodigoPrograma, "L");
                
            Site toolBar = (Site)this.Master;

            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                CargarValoresConsulta(pConsulta, TerceroServicio.CodigoPrograma);
                ViewState["CurrentAlphabet"] = "TODO";
                GenerateAlphabets();
                if (Session[TerceroServicio.CodigoPrograma + ".nid"] != null)
                {
                    txtNumeIdentificacion.Text = Session[TerceroServicio.CodigoPrograma + ".nid"].ToString();
                    Session.Remove(TerceroServicio.CodigoPrograma + ".nid");                    
                   
                }
               
                   
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

   

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(TerceroServicio.CodigoPrograma + ".id");
        GuardarValoresConsulta(pConsulta, TerceroServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, TerceroServicio.CodigoPrograma);
        Actualizar(0);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtNumeIdentificacion.Text = "";
        LimpiarValoresConsulta(pBusqueda, TerceroServicio.CodigoPrograma);
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
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[TerceroServicio.CodigoPrograma + ".id"] = id;
        Session[TerceroServicio.CodigoPrograma + ".modificar"] = 1;
        String tipo_persona = gvLista.Rows[gvLista.SelectedRow.RowIndex].Cells[4].Text;
        if (tipo_persona == "Juridica")
            Navegar(Pagina.Nuevo);
        else
            Navegar(Pagina.Nuevo);
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
        Session[TerceroServicio.CodigoPrograma + ".id"] = id;
        Session[TerceroServicio.CodigoPrograma + ".modificar"] = 0;
        String tipo_persona = gvLista.Rows[e.NewEditIndex].Cells[4].Text;
        if (tipo_persona == "Juridica")
            Navegar(Pagina.Nuevo);
        else
            Navegar(Pagina.Nuevo); ;
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            try
            {
               
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
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar(int pOrden)
    {
        string sLetra = ViewState["CurrentAlphabet"].ToString();
        obtFiltro();
        
        List<Xpinn.Aportes.Entities.PersonaAutorizacion> lstConsulta = new List<Xpinn.Aportes.Entities.PersonaAutorizacion>();
        Xpinn.Aportes.Entities.PersonaAutorizacion valores = new Xpinn.Aportes.Entities.PersonaAutorizacion();
        if (txtId.Text != "")
        {
            valores.idautorizacion = Convert.ToInt64(txtId.Text);
        }
        
        lstConsulta = TerceroServicio.ListarPersonaAutorizacion(valores, (Usuario)Session["usuario"], obtFiltro());

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
        return vTercero;
    }

    private void CargarListas()
    {
        try
        {
            // Llenar las listas que tienen que ver con ciudades
           

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.GetType().Name + "L", "CargarListas", ex);
        }
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

    private string obtFiltro()
    {
        Configuracion conf = new Configuracion();

        String filtro = String.Empty;

        if (txtCodigo.Text.Trim() != "")
            filtro += " and Persona.cod_persona = " + txtCodigo.Text;
        if (txtNombres.Text.Trim() != "")
            filtro += " and Persona.Nombres like '%" + txtNombres.Text + "%'";
        if (txtNumeIdentificacion.Text != "")
            filtro += " and Persona.identificacion = " + "'"+txtNumeIdentificacion.Text+"'";
        if (txtCodigoNomina.Text != "")
            filtro += " and Persona.cod_nomina = '" + txtCodigoNomina.Text + "'";

        filtro += " and persona_autorizacion.estado= 1";
        return filtro;
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        Actualizar(0);
    }

    protected void ddlOrdenar_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar(0);
    }
}