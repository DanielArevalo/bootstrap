using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Xpinn.Util;


public delegate void gvLista_SelectedIndexChanged_ActionsDelegate(object sender, EventArgs e);
public delegate void txtNumeIdentificacion_TextChanged_ActionsDelegate(object sender, EventArgs e);


public partial class ctlSeleccionarPersona : System.Web.UI.UserControl
{
    public event gvLista_SelectedIndexChanged_ActionsDelegate eventoEditar;
    public event txtNumeIdentificacion_TextChanged_ActionsDelegate eventoIdentificacion;

    Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                ViewState["CurrentAlphabet"] = "TODO";
                GenerateAlphabets();
                LlenarComboPageSize();
            }

            txtNumeIdentificacion.AutoPostBack = eventoIdentificacion != null;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    private void CargarListas()
    {
        try
        {
            // Llenar las listas que tienen que ver con ciudades
            ddlCiudad.DataTextField = "ListaDescripcion";
            ddlCiudad.DataValueField = "ListaIdStr";
            ddlCiudad.DataSource = TraerResultadosLista("Ciudades");
            ddlCiudad.DataBind();

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    #region generar y llenar la tabla
    public void Actualizar(int actualizar = 0)
    {
        lblMensaje.Text = "";
        string sLetra = ViewState["CurrentAlphabet"].ToString();
        string Filtro ="";
        if (ViewState["Filtro"] != null)
            Filtro = ViewState["Filtro"].ToString();
        string sOrdenar = "";
        string sFiltro = "";
        if (sLetra != "TODO" && sLetra.Trim() != "")
        {
            sFiltro = " (primer_apellido Like '" + sLetra + "%' OR razon_social Like '" + sLetra + "%') ";
        }
        if (Filtro != null)
        {
            if (Filtro.Trim() != "")
            {
                sFiltro += sFiltro.Trim() != "" && sFiltro != null ? " And " + Filtro : Filtro;
            }
        }

        //Código nómina
        if (!string.IsNullOrWhiteSpace(txtCodigoNomina.Text))
            sFiltro += (!string.IsNullOrWhiteSpace(sFiltro) ? " and " : "") + (" cod_nomina = " + txtCodigoNomina.Text);

        if (ddlOrdenar.SelectedValue != null)
            sOrdenar = ddlOrdenar.SelectedValue;

        try
        {
            if (actualizar == 6) { ddlTipoRol.SelectedItem.Value = Convert.ToString("R"); sFiltro += "persona.estado='R'"; }
            List<Xpinn.Contabilidad.Entities.Tercero> lstConsulta = new List<Xpinn.Contabilidad.Entities.Tercero>();
            if (ddlTipoRol.SelectedItem.Value == "A")
                lstConsulta = TerceroServicio.ListarTerceroSoloAfiliados(ObtenerValores(), sFiltro, (Usuario)Session["usuario"], sOrdenar);
            else if (ddlTipoRol.SelectedItem.Value == "T")
                lstConsulta = TerceroServicio.ListarTerceroNoAfiliados(ObtenerValores(), sFiltro, (Usuario)Session["usuario"], sOrdenar);
            else 
                lstConsulta = TerceroServicio.ListarTercero(ObtenerValores(), sFiltro, sOrdenar, (Usuario)Session["usuario"]);

            gvLista.PageSize = Convert.ToInt32(ddlPageSize.SelectedItem.Text);
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
                lblInfo.Visible = true;
                Session["DTPERSONAS"] = null;
            }
            Session.Add(TerceroServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    private Xpinn.Contabilidad.Entities.Tercero ObtenerValores()
    {                
        Xpinn.Contabilidad.Entities.Tercero vTercero = new Xpinn.Contabilidad.Entities.Tercero();
        try
        {
            if (ddlTipoPersona.SelectedValue.Trim() != "")
                vTercero.tipo_persona = Convert.ToString(ddlTipoPersona.SelectedValue.Trim()).ToUpper();
            if (txtCodigo.Text.Trim() != "")
                vTercero.cod_persona = Convert.ToInt64(txtCodigo.Text.Trim());
            if (txtNumeIdentificacion.Text.Trim() != "")
                vTercero.identificacion = Convert.ToString(txtNumeIdentificacion.Text.Trim());
            if (txtNombres.Text.Trim() != "")
                vTercero.primer_nombre = Convert.ToString(txtNombres.Text.Trim()).ToUpper();
            if (txtSegundoNombre.Text.Trim() != "")
                vTercero.segundo_nombre = Convert.ToString(txtSegundoNombre.Text.Trim()).ToUpper();
            if (txtApellidos.Text.Trim() != "")
                vTercero.primer_apellido = Convert.ToString(txtApellidos.Text.Trim()).ToUpper();
            if (txtSegundoApellido.Text.Trim() != "")
                vTercero.segundo_apellido = Convert.ToString(txtSegundoApellido.Text.Trim()).ToUpper();
            if (txtRazonSocial.Text.Trim() != "")
                vTercero.razon_social = Convert.ToString(txtRazonSocial.Text.Trim()).ToUpper();
            if (ddlCiudad.SelectedValue.Trim() != "")
                vTercero.codciudadexpedicion = Convert.ToInt64(ddlCiudad.SelectedValue.Trim());
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "ObtenerValores(). " + ex.Message;
        }
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
        rptAlphabets.DataSource = alphabets;
        rptAlphabets.DataBind();
    }

    protected void Alphabet_Click(object sender, EventArgs e)
    {
        LinkButton lnkAlphabet = (LinkButton)sender;
        ViewState["CurrentAlphabet"] = lnkAlphabet.Text;
        this.GenerateAlphabets();
        gvLista.PageIndex = 0;
        Actualizar();
    }

    protected void LlenarComboPageSize()
    {
        int tamaño = 50;
        int contador = 1;

        ddlPageSize.Items.Clear();
        ddlPageSize.Items.Insert(0, "1");
        if (tamaño < 5)
            ddlPageSize.Items.Insert(0, tamaño.ToString());
        for (int i = 5; i <= tamaño; i = i + 5)
        {
            if (i == pageSize)
            {
                ddlPageSize.Items.Insert(contador, i.ToString());
                ddlPageSize.SelectedValue = pageSize.ToString();
            }
            else
            {
                ddlPageSize.Items.Insert(contador, i.ToString());
            }

            contador = contador + 1;
        }
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void ddlOrdenar_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }
    #endregion

    public int pageSize
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]); }
    }

    public String emptyQuery
    {
        get { return ConfigurationManager.AppSettings["EmptyQuery"]; }
    }


    #region eventos del listado
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
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
            lblMensaje.Text = ex.Message;
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {          
        if (eventoEditar != null)
        {
            eventoEditar(sender, e);                        
        }
        else
        {
            String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Values[0].ToString(); 
            Session[CodigoPrograma + ".id"] = id;
            Navegar(Pagina.Nuevo);
        }
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
            try { pOrden = Convert.ToInt32(e.CommandName); Actualizar(); }
            catch { }
        }
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            try
            {
                TerceroServicio.EliminarTercero(id, (Usuario)Session["usuario"]);
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            lblMensaje.Text = ex.Message;
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
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
            lblMensaje.Text = ex.Message;
        }
    }
    #endregion

    #region propiedades del control
    public string Identificacion
    {
        set { txtNumeIdentificacion.Text = value; }
        get { return txtNumeIdentificacion.Text; }

    }

    public GridView gvListado
    {
        get { return gvLista; }
    }

    public Panel pBusquedaDatos
    {
        get { return pBusqueda; }
    }

    #endregion

    protected enum Pagina { Nuevo = 1, Lista = 2, Detalle = 3, Editar = 4, Modificar = 5 };
    protected void Navegar(Pagina page)
    {
        try
        {
            switch (page.ToString())
            {
                case "Nuevo":
                    Response.Redirect("Nuevo.aspx", false);
                    break;
                case "Detalle":
                    Response.Redirect("Detalle.aspx", false);
                    break;
                case "Editar":
                    Response.Redirect("Nuevo.aspx?o=E", false);
                    break;
                case "Modificar":
                    Response.Redirect("Lista.aspx?modificar=1", false);
                    break;
                default:
                    Response.Redirect("Lista.aspx", false);
                    break;
            }
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void Navegar(String pPath)
    {
        Response.Redirect(pPath, false);
    }

    public string Filtro 
    {
        set { ViewState["Filtro"] = value; }
        get { return ViewState["Filtro"].ToString(); }
    }

    public string CodigoPrograma 
    {
        set { ViewState["CodigoPrograma"] = value; }
        get
        {
            if (ViewState["CodigoPrograma"] != null)
                return ViewState["CodigoPrograma"].ToString();
            else
                return null;
            }
    }

    protected void txtNumeIdentificacion_TextChanged(object sender, EventArgs e)
    {
        if (eventoIdentificacion != null)
        {
            eventoIdentificacion(sender, e);
        }
    }

    public void Collapsed(bool pCollapsed)
    {
        cpeDemo.Collapsed = pCollapsed;
    }
}