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
    AprobacionServices AprobacionIctx = new AprobacionServices();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AprobacionIctx.CodigoPrograma, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionIctx.CodigoPrograma, "Page_PreInit", ex);
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
            BOexcepcion.Throw(AprobacionIctx.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, AprobacionIctx.CodigoPrograma);
            Actualizar();
        }
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, AprobacionIctx.CodigoPrograma);
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
            Session.Add(AprobacionIctx.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionIctx.CodigoPrograma, "Actualizar", ex);
        }
    }

    private string obtFiltro()
    {
        ConnectionDataBase conexion = new ConnectionDataBase();
        string sFiltro = string.Empty;
        string sRestric = string.Empty;
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

        if (ddlEstado.SelectedValue == "Y")
            sFiltro += " AND C.ESTADO  = 'Z'";
        else
            sFiltro += " AND C.ESTADO  = '" + ddlEstado.SelectedValue + "'";
        if (ddlEstado.SelectedValue == "I")
        {
            sRestric = @" AND C.NUMERO_CREDITO IN (SELECT X.numero_credito FROM creditoicetexaprobacion X
                                    WHERE X.numero_credito = C.NUMERO_CREDITO and X.tipo_aprobacion = 1 and rownum <= 1)";

            sFiltro += " AND C.NUMERO_CREDITO NOT IN (SELECT X.numero_credito FROM creditoicetexaprobacion X WHERE X.numero_credito = C.NUMERO_CREDITO AND X.tipo_aprobacion = 2)";
        }
        else if (ddlEstado.SelectedValue == "Z")
        {
            sRestric = @" AND C.NUMERO_CREDITO IN (SELECT X.numero_credito FROM creditoicetexaprobacion X
                                    WHERE X.numero_credito = C.NUMERO_CREDITO and X.tipo_aprobacion = 1 and rownum <= 1)";

            sFiltro += " AND C.NUMERO_CREDITO NOT IN (SELECT X.numero_credito FROM creditoicetexaprobacion X WHERE X.numero_credito = C.NUMERO_CREDITO AND X.tipo_aprobacion = 2)";
        }
        else if (ddlEstado.SelectedValue == "Y")
        {
            sRestric = @" AND C.NUMERO_CREDITO IN (SELECT X.numero_credito FROM creditoicetexaprobacion X
                                    WHERE X.numero_credito = C.NUMERO_CREDITO and X.tipo_aprobacion = 2 and rownum <= 1)";

            sFiltro += " AND C.NUMERO_CREDITO IN (SELECT X.numero_credito FROM creditoicetexaprobacion X WHERE X.numero_credito = C.NUMERO_CREDITO AND X.tipo_aprobacion = 2)";
        }

        if (!string.IsNullOrEmpty(sFiltro))
        {
            sFiltro = sFiltro.Substring(4);
            sFiltro = " WHERE " + sFiltro;
        }
        return string.IsNullOrEmpty(sRestric) ? sFiltro : sRestric + " " + sFiltro;
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
            BOexcepcion.Throw(AprobacionIctx.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        string pFecha = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        string pConvocatoria = gvLista.DataKeys[e.NewEditIndex].Values[2].ToString();
        string pCod_Persona = gvLista.DataKeys[e.NewEditIndex].Values[3].ToString();
        string pEstado = gvLista.DataKeys[e.NewEditIndex].Values[4].ToString();
        string pTipoAprobacion = gvLista.DataKeys[e.NewEditIndex].Values[5].ToString();
        e.NewEditIndex = -1;
        Session[AprobacionIctx.CodigoPrograma + ".id"] = id;
        Session[AprobacionIctx.CodigoPrograma + ".fecha"] = pFecha;
        Session[AprobacionIctx.CodigoPrograma + ".estado"] = pEstado;
        Session[AprobacionIctx.CodigoPrograma + ".cod_convocatoria"] = pConvocatoria;
        Session[AprobacionIctx.CodigoPrograma + ".cod_persona"] = pCod_Persona;
        Session[AprobacionIctx.CodigoPrograma + ".tipo_aprobacion"] = pTipoAprobacion;
        Navegar(Pagina.Nuevo);
    }


    #endregion

}