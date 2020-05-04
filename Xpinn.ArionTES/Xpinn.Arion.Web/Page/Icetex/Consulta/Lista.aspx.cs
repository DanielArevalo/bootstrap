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
            VisualizarOpciones(AprobacionIctx.CodigoProgramaConsul, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionIctx.CodigoProgramaConsul, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                panelGrid.Visible = false;
                CargarDropDown();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionIctx.CodigoProgramaConsul, "Page_Load", ex);
        }
    }

    protected void CargarDropDown()
    {
        var lstConvocatoria = ConvocatoriaServ.ListarConvocatoriaIcetex("", Usuario);
        ctllistar.ValueField = "cod_convocatoria";
        ctllistar.TextField = "descripcion";
        ctllistar.BindearControl(lstConvocatoria);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, AprobacionIctx.CodigoProgramaConsul);
            Actualizar();
        }
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, AprobacionIctx.CodigoProgramaConsul);
        txtFecIni.Text = "";
        txtFecFin.Text = "";
    }
    

    private void Actualizar()
    {
        try
        {
            if (ctllistar.Codigo == null || string.IsNullOrWhiteSpace(ctllistar.Codigo))
            {
                VerError("Seleccione una convocatoria");
                return;
            }

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
            Session.Add(AprobacionIctx.CodigoProgramaConsul + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionIctx.CodigoProgramaConsul, "Actualizar", ex);
        }
    }

    private string obtFiltro()
    {
        ConnectionDataBase conexion = new ConnectionDataBase();
        string sFiltro = string.Empty;
        if (!string.IsNullOrWhiteSpace(ctllistar.Codigo))
            sFiltro += " AND C.COD_CONVOCATORIA = " + ctllistar.Codigo;

        string sRestric = @" AND A.IDAPROBACION IN (SELECT X.IDAPROBACION FROM creditoicetexaprobacion X
                                    WHERE X.numero_credito = C.NUMERO_CREDITO and rownum <= 1)";
        if (txtNumCredito.Text.Trim() != "")
        {
            sFiltro += " AND C.NUMERO_CREDITO = " + txtNumCredito.Text.Trim();
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
        e.NewEditIndex = -1;
        Session[AprobacionIctx.CodigoProgramaConsul + ".id"] = id;
        Session[AprobacionIctx.CodigoProgramaConsul + ".fecha"] = pFecha;
        Session[AprobacionIctx.CodigoProgramaConsul + ".estado"] = pEstado;
        Session[AprobacionIctx.CodigoProgramaConsul + ".cod_convocatoria"] = pConvocatoria;
        Session[AprobacionIctx.CodigoProgramaConsul + ".cod_persona"] = pCod_Persona;
        Navegar(Pagina.Nuevo);
    }


    #endregion

}