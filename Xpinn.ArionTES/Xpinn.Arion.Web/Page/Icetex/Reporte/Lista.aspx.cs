using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Xpinn.Util;
using Xpinn.Icetex.Services;
using Xpinn.Icetex.Entities;
using Microsoft.Reporting.WebForms;

public partial class Lista : GlobalWeb
{
    ConvocatoriaServices BOConvocatoria = new ConvocatoriaServices();
    AprobacionServices ReporteIctx = new AprobacionServices();
    Usuario pUsu;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ReporteIctx.CodigoProgramaReporte, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReporteIctx.CodigoProgramaReporte, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pUsu = (Usuario)Session["usuario"];
        if (!Page.IsPostBack)
        {
            CargarDropDown();
            rptIcetex.Visible = false;
        }
    }

    protected void CargarDropDown()
    {

        var lstConvocatoria = BOConvocatoria.ListarConvocatoriaIcetex("", pUsu);
        ctllistar.ValueField = "cod_convocatoria";
        ctllistar.TextField = "descripcion";
        ctllistar.BindearControl(lstConvocatoria);

        ddlTipoBeneficiario.Items.Insert(0, new ListItem("Seleccione un item", "-1"));
        ddlTipoBeneficiario.Items.Insert(1, new ListItem("Asociado", "0"));
        ddlTipoBeneficiario.Items.Insert(2, new ListItem("Hijo del Asociado", "1"));
        ddlTipoBeneficiario.Items.Insert(3, new ListItem("Nieto del Asociado", "2"));
        ddlTipoBeneficiario.Items.Insert(4, new ListItem("Empleado", "3"));
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ctllistar.Codigo == null || string.IsNullOrWhiteSpace(ctllistar.Codigo))
        {
            VerError("Seleccione una convocatoria");
            return;
        }
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtFecIni.Text = "";
        txtFecFin.Text = "";
        rptIcetex.Visible = false;
        LimpiarPanel(pConsulta);
    }

    protected string obtFiltro()
    {
        string pFiltro = string.Empty;

        if (!string.IsNullOrWhiteSpace(ctllistar.Codigo))
            pFiltro += " AND C.COD_CONVOCATORIA = " + ctllistar.Codigo;

        if (txtFecIni.TieneDatos)
            pFiltro += " AND A.FECHA_APROBACION >= TO_DATE('" + txtFecIni.ToDateTime.ToString(gFormatoFecha) + "', '" + gFormatoFecha + "')";

        if (txtFecFin.TieneDatos)
            pFiltro += " AND A.FECHA_APROBACION <= TO_DATE('" + txtFecIni.ToDateTime.ToString(gFormatoFecha) + "', '" + gFormatoFecha + "')";

        if (ddlTipoPrograma.SelectedIndex > 0)
            pFiltro += " AND C.TIPO_PROGRAMA = " + ddlTipoPrograma.SelectedValue;

        if (ddlTipoBeneficiario.SelectedIndex > 0)
            pFiltro += " AND C.TIPO_BENEFICIARIO = '" + ddlTipoBeneficiario.SelectedValue + "'";

        if (ddlEstrato.SelectedIndex > 0)
            pFiltro += " AND V.ESTRATO = " + ddlEstrato.SelectedValue;

        if (ddlPeriodos.SelectedIndex > 0)
            pFiltro += " AND C.PERIODOS = " + ddlPeriodos.SelectedValue;

        if(ddlEstado.SelectedIndex > 0)
            pFiltro += " AND C.ESTADO = '" + ddlEstado.SelectedValue + "'";

        if (!string.IsNullOrEmpty(pFiltro))
        {
            pFiltro = pFiltro.Substring(4);
            pFiltro = " WHERE " + pFiltro;
        }
        return pFiltro;
    }

    protected void Actualizar()
    {
        try
        {
            List<Reporte> lstCredito = new List<Reporte>();
            string pFiltro = obtFiltro();

            lstCredito = BOConvocatoria.ListarReporteCreditosIcetex(pFiltro, pUsu);

            DataTable dtReporte = new DataTable();
            dtReporte = lstCredito.ToDataTable();

            int totalAprobados = lstCredito.Where(x => x.estado.ToUpper() == "A").Count();
            int totalNegados = lstCredito.Where(x => x.estado.ToUpper() == "N").Count();
            int totalAplazados = lstCredito.Where(x => x.estado.ToUpper() == "Z").Count();
            int totalPreInscri = lstCredito.Where(x => x.estado.ToUpper() == "S").Count();
            if (lstCredito.Count > 0)
            {
                lblInfo.Visible = false;
                rptIcetex.Visible = true;
                ReportParameter[] param = new ReportParameter[10];
                param[0] = new ReportParameter("entidad", HttpUtility.HtmlDecode(pUsu.empresa));
                param[1] = new ReportParameter("nit", HttpUtility.HtmlDecode(pUsu.nitempresa));
                param[2] = new ReportParameter("nom_reporte", HttpUtility.HtmlDecode(pUsu.empresa));
                param[3] = new ReportParameter("ImagenReport", ImagenReporte());
                param[4] = new ReportParameter("nomUsuario", pUsu.nombre);
                param[5] = new ReportParameter("cant_Aprobados", " " + totalAprobados);
                param[6] = new ReportParameter("cant_Aplazados", " " + totalAplazados);
                param[7] = new ReportParameter("cant_Negados", " " + totalNegados);
                string pNomConv = ctllistar.Descripcion;
                param[8] = new ReportParameter("NomConvocatoria", " " + pNomConv);
                param[9] = new ReportParameter("cant_preinscritos", " " + totalPreInscri);

                rptIcetex.LocalReport.EnableExternalImages = true;
                rptIcetex.LocalReport.SetParameters(param);
                rptIcetex.LocalReport.DataSources.Clear();

                ReportDataSource rds = new ReportDataSource("DataSet1", dtReporte);
                rptIcetex.LocalReport.DataSources.Add(rds);
                rptIcetex.LocalReport.Refresh();
            }
            else
            {
                lblInfo.Visible = true;
                rptIcetex.Visible = false;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


}