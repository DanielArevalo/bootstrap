using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TerceroServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
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
                if (Session[TerceroServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
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
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, TerceroServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
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
            Navegar(Pagina.Detalle);
        else
            Navegar("../Personas/Nuevo.aspx");
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
            Navegar("../Personas/Nuevo.aspx");
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            VerError("");
            Int64 id = Convert.ToInt64(e.Keys[0]);
            try
            {
                TerceroServicio.EliminarTercero(id, (Usuario)Session["usuario"]);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            Actualizar();
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
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Contabilidad.Entities.Tercero> lstConsulta = new List<Xpinn.Contabilidad.Entities.Tercero>();
            lstConsulta = TerceroServicio.ListarTercero(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(TerceroServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "Actualizar", ex);
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
        if (txtRazonSocial.Text.Trim() != "")
            vTercero.razon_social = Convert.ToString(txtRazonSocial.Text.Trim());
        if (ddlCiudad.SelectedValue.Trim() != "")
            vTercero.codciudadexpedicion = Convert.ToInt64(ddlCiudad.SelectedValue.Trim());
        return vTercero;
    }

    private void CargarListas()
    {
        try
        {
            // Llenar las listas que tienen que ver con ciudades
            ddlCiudad.DataTextField = "ListaDescripcion";
            ddlCiudad.DataValueField = "ListaId";
            ddlCiudad.DataSource = TraerResultadosLista("Ciudades");
            ddlCiudad.DataBind();

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

}