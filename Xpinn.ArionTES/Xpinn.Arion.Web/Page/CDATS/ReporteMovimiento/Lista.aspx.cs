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
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Services;


partial class Lista : GlobalWeb
{
    ReporteMovimientoServices ReporteMovService = new ReporteMovimientoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {       
            VisualizarOpciones(ReporteMovService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteMovService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteMovService.CodigoPrograma, "Page_Load", ex);
        }
    }

    
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtFechaApertura.Text = "";
        LimpiarValoresConsulta(pConsulta, ReporteMovService.CodigoPrograma);
        panelGrilla.Visible = false;
        gvLista.DataSource = null;
        lblInfo.Visible = false;
        lblTotalRegs.Visible = false;
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate();
        if (Page.IsValid)
        {
            Actualizar();
        }
    }

    
    void cargarDropdown()
    {
        ddlFormaPago.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlFormaPago.Items.Insert(1, new ListItem("CAJA", "1"));
        ddlFormaPago.Items.Insert(2, new ListItem("NÓMINA", "2"));
        ddlFormaPago.SelectedIndex = 0;
        ddlFormaPago.DataBind();

        ddlEstado.Items.Insert(0, new ListItem("Seleccione un item",""));
        ddlEstado.Items.Insert(1, new ListItem("Apertura", "1"));
        ddlEstado.Items.Insert(2, new ListItem("Activa", "2"));
        ddlEstado.Items.Insert(3, new ListItem("Terminado", "3"));
        ddlEstado.Items.Insert(4, new ListItem("Anulado", "4"));
        ddlEstado.Items.Insert(5, new ListItem("Embargado", "5"));
        ddlEstado.SelectedIndex = 0;
        ddlEstado.DataBind();

        Xpinn.Asesores.Data.OficinaData vDatosOficina = new Xpinn.Asesores.Data.OficinaData();
        Xpinn.Asesores.Entities.Oficina pOficina = new Xpinn.Asesores.Entities.Oficina();
        List<Xpinn.Asesores.Entities.Oficina> lstOficina = new List<Xpinn.Asesores.Entities.Oficina>();
        pOficina.Estado = 1;
        lstOficina = vDatosOficina.ListarOficina(pOficina, (Usuario)Session["usuario"]);
        if (lstOficina.Count > 0)
        {
            ddlOficina.DataSource = lstOficina;
            ddlOficina.DataTextField = "NombreOficina";
            ddlOficina.DataValueField = "IdOficina";
            ddlOficina.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlOficina.AppendDataBoundItems = true;
            ddlOficina.SelectedIndex = 0;
            ddlOficina.DataBind();
        }

        List<ReporteMovimiento> lstLineas = new List<ReporteMovimiento>();
        ReporteMovimiento pData = new ReporteMovimiento();
        pData.estado = 1;
        lstLineas = ReporteMovService.ListarDropDownLineasCdat(pData, (Usuario)Session["usuario"]);
        if (lstLineas.Count > 0)
        {
            ddlLinea.DataSource = lstLineas;
            ddlLinea.DataTextField = "descripcion";
            ddlLinea.DataValueField = "cod_linea_cdat";
            ddlLinea.AppendDataBoundItems = true;
            ddlLinea.Items.Insert(0, new ListItem("Seleccione un item","0"));
            ddlLinea.SelectedIndex = 0;
            ddlLinea.DataBind();
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
            BOexcepcion.Throw(ReporteMovService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();         
        Session[ReporteMovService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }
    
    
    private void Actualizar()
    {
        try
        {
            List<Cdat> lstConsulta = new List<Cdat>();
            String filtro = obtFiltro();
            DateTime pFechaApert;
            pFechaApert = txtFechaApertura.ToDateTime == null ? DateTime.MinValue : txtFechaApertura.ToDateTime;

            lstConsulta = ReporteMovService.ListarCdat(filtro, pFechaApert,(Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();                
               
            }
            else
            {
                panelGrilla.Visible = false;
                gvLista.DataSource = null;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(ReporteMovService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteMovService.CodigoPrograma, "Actualizar", ex);
        }
    }
       
   
    private string obtFiltro()
    {
        String filtro = String.Empty;

        if (txtNumCta.Text.Trim() != "")
            filtro += "AND A.numero_cdat = '" + txtNumCta.Text.Trim() + "'";
        if (ddlLinea.SelectedIndex != 0)
            filtro += "AND A.COD_LINEACDAT = '" + ddlLinea.SelectedValue + "'";
        //if (ddlFormaPago.SelectedIndex != 0)
        //    filtro += "AND A.COD_FORMA_PAGO = " + ddlFormaPago.SelectedValue;
        if (ddlEstado.SelectedIndex != 0)
            filtro += "AND A.ESTADO = " + ddlEstado.SelectedValue;
        if (txtCodPersona.Text != "")
            filtro += "AND T.COD_PERSONA = " + txtCodPersona.Text.Trim();
        if(txtIdentificacion.Text != "")
            filtro += "AND V.IDENTIFICACION = '" + txtIdentificacion.Text.Trim() + "'";
        if(txtNombre.Text != "")
            filtro += "AND V.NOMBRE LIKE '%" + txtNombre.Text.Trim() + "%'";
        if (txtCodigoNomina.Text != "")
            filtro += "AND V.COD_NOMINA LIKE '%" + txtCodigoNomina.Text.Trim() + "%'";
        if (ddlOficina.SelectedIndex != 0)
            filtro += "AND A.COD_OFICINA = " + ddlOficina.SelectedValue;

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "WHERE " + filtro;
        }
        return filtro;
    }


   
}