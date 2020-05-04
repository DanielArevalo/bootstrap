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
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;

partial class Lista : GlobalWeb
{

    CambioMonedaServices CAMBIOMONEDA = new CambioMonedaServices();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CAMBIOMONEDA.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CAMBIOMONEDA.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropDown();
                CargarValoresConsulta(pConsulta, CAMBIOMONEDA.CodigoPrograma);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CAMBIOMONEDA.CodigoPrograma, "Page_Load", ex);
        }
    }
    
    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Navegar(Pagina.Nuevo);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CAMBIOMONEDA.GetType().Name + "L", "btnNuevo_Click", ex);
        }
    }

  
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, CAMBIOMONEDA.GetType().Name);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CAMBIOMONEDA.GetType().Name + "L", "btnConsultar_Click", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        panelGrilla.Visible = false;
        lblTotalRegs.Visible = false;
        lblInfo.Visible = false;
        txtFecha.Text = "";
        LimpiarValoresConsulta(pConsulta, CAMBIOMONEDA.CodigoPrograma);
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
            BOexcepcion.Throw(CAMBIOMONEDA.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[CAMBIOMONEDA.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);            
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        String id = gvLista.DataKeys[e.RowIndex].Values[0].ToString();
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["ID"] != null)
            {
                CAMBIOMONEDA.EliminarCambioMoneda(Convert.ToInt64(Session["ID"]), (Usuario)Session["usuario"]);
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CAMBIOMONEDA.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }


    private void Actualizar()
    {
        try
        {            
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            List<CambioMoneda> lstConsulta = new List<CambioMoneda>();
            lstConsulta = CAMBIOMONEDA.ListarCambioMoneda(obtFiltro(), (Usuario)Session["Usuario"]);
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                panelGrilla.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(CAMBIOMONEDA.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CAMBIOMONEDA.CodigoPrograma, "Actualizar", ex);
        }
    }


    private string obtFiltro()
    {
        String filtro = String.Empty;       
        if (ddlOrigen.SelectedIndex != 0)
            filtro += " AND C.COD_MONEDA_INI = " + ddlOrigen.SelectedValue;
        if (ddlDestino.SelectedIndex != 0)
            filtro += " AND C.COD_MONEDA_INI = " + ddlDestino.SelectedValue;
        if (txtFecha.TieneDatos)
        {
            string sFecha = Convert.ToDateTime(txtFecha.ToDateTime.ToString()).ToString(GlobalWeb.gFormatoFecha);
            filtro += " AND FECHA = TO_DATE('" + sFecha + "', 'dd/MM/yyyy') ";
        }        

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "WHERE " + filtro;
        }
        return filtro;
    }


    void CargarDropDown()
    {
        PoblarLista("TIPOMONEDA", ddlOrigen);
        PoblarLista("TIPOMONEDA", ddlDestino);
    }

    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }

}

 