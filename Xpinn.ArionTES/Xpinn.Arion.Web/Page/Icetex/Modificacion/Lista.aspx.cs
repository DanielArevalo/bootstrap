using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Icetex.Services;
using Xpinn.Icetex.Entities;

public partial class Lista : GlobalWeb
{
    ConvocatoriaServices ConvocatoriaServ = new ConvocatoriaServices();
    AprobacionServices ModificacionIctx = new AprobacionServices();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ModificacionIctx.CodigoProgramaModifi, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModificacionIctx.CodigoProgramaModifi, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                panelGrid.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModificacionIctx.CodigoProgramaModifi, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, ModificacionIctx.CodigoProgramaModifi);
            Actualizar();
        }
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ModificacionIctx.CodigoProgramaModifi);
        txtFecIni.Text = "";
        txtFecFin.Text = "";
    }
    

    private void Actualizar()
    {
        try
        {
            List<CreditoIcetex> lstConsulta = new List<CreditoIcetex>();
            String pFiltro = obtFiltro();
            lstConsulta = ConvocatoriaServ.ListarCreditosIcetex(pFiltro, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                panelGrid.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                lblInfo.Visible = false;
                gvLista.DataBind();
            }
            else
            {
                panelGrid.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }
            Session.Add(ModificacionIctx.CodigoProgramaModifi + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModificacionIctx.CodigoProgramaModifi, "Actualizar", ex);
        }
    }

    private string obtFiltro()
    {
        ConnectionDataBase conexion = new ConnectionDataBase();
        string sFiltro = string.Empty;
        if (txtNumCredito.Text.Trim() != "")
        {
            sFiltro += " AND C.NUMERO_CREDITO = " + txtNumCredito.Text.Trim() ;
        }
        if (txtIdentificacion.Text.Trim() != "")
        {
            sFiltro += " AND C.IDENTIFICACION = '" + txtIdentificacion.Text.Trim() + "'";
        }
        if (txtNombre.Text.Trim() != "")
        {
            sFiltro += " AND UPPER(Trim(Substr(C.primer_nombre || ' ' || C.segundo_nombre || ' ' ||  C.primer_apellido || ' ' || C.segundo_apellido, 0, 240))) like '" + txtNombre.Text.Trim().ToUpper() + "'";
        }
        if (txtFecIni.TieneDatos)
            if (txtFecIni.ToDate.Trim() != "")
                if (conexion.TipoConexion() == "ORACLE")
                    sFiltro += " And C.FECHA_SOLICITUD >= To_Date('" + txtFecIni.ToDate.Trim() + "', '" + gFormatoFecha + "')";
                else
                    sFiltro += " And C.FECHA_SOLICITUD <= '" + txtFecIni.ToDate.Trim() + "', '" + gFormatoFecha + "'";
        if (txtFecFin.TieneDatos)
            if (txtFecFin.ToDate.Trim() != "")
                if (conexion.TipoConexion() == "ORACLE")
                    sFiltro += " And C.FECHA_SOLICITUD <= To_Date('" + txtFecFin.ToDate.Trim() + "', '" + gFormatoFecha + "')";
                else
                    sFiltro += " And C.FECHA_SOLICITUD <= '" + txtFecFin.ToDate.Trim() + "', '" + gFormatoFecha + "'";
        if (ddlEstado.SelectedIndex > 0)
            sFiltro += " AND C.ESTADO = '" + ddlEstado.SelectedValue + "'";

        if (!string.IsNullOrEmpty(sFiltro))
        {
            sFiltro = sFiltro.Substring(4);
            sFiltro = " WHERE " + sFiltro;
        }
        return sFiltro;
    }

    #region EVENTOS DEL GRIDVIEW

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModificacionIctx.CodigoProgramaModifi, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        string pFecha = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        string pConvocatoria = gvLista.DataKeys[e.NewEditIndex].Values[2].ToString();
        e.NewEditIndex = -1;
        Session[ModificacionIctx.CodigoProgramaModifi + ".id"] = id;
        Session[ModificacionIctx.CodigoProgramaModifi + ".fecha"] = pFecha;
        Session[ModificacionIctx.CodigoProgramaModifi + ".cod_convocatoria"] = pConvocatoria;
        Navegar(Pagina.Nuevo);
    }


    #endregion

}