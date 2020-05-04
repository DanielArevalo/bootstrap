using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Indicadores.Services;
using Xpinn.Indicadores.Entities;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Drawing.Printing;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Indicadores.Services.GestionDiariaService Gestionservicio = new Xpinn.Indicadores.Services.GestionDiariaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[Gestionservicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(Gestionservicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(Gestionservicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Gestionservicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session[Gestionservicio.CodigoPrograma + ".id"] != null)
            {
                Usuario usuap = (Usuario)Session["usuario"];
            }
            ReportViewer1.Visible = false;
            btnInforme_Click(null, null);
        }
    }


    public DataTable CrearDataTableMovimientos()
    {
        List<GestionDiaria> LstDetalleComprobante = new List<GestionDiaria>();
        GestionDiaria detalle = new GestionDiaria();
        LstDetalleComprobante = Gestionservicio.ReporteGestionDiaria((Usuario)Session["Usuario"]);
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("COD_OFICINA", typeof(Int64));
        table.Columns["COD_OFICINA"].AllowDBNull = true;
        table.Columns.Add("OFICINA", typeof(string));
        table.Columns["OFICINA"].AllowDBNull = true;
        table.Columns.Add("COD_ASESOR_COM", typeof(Int64));
        table.Columns["COD_ASESOR_COM"].AllowDBNull = true;
        table.Columns.Add("NOMBRE", typeof(string));
        table.Columns["NOMBRE"].AllowDBNull = true;
        table.Columns.Add("NUM_CRED_CIERRE", typeof(decimal));
        table.Columns.Add("SALDO_CAPITAL_CIERRE", typeof(decimal));
        table.Columns.Add("NUM_CRED_ACTUAL", typeof(decimal));
        table.Columns.Add("SALDO_CAPITAL_ACTUAL", typeof(decimal));
        table.Columns.Add("NO_COLOCACION_CIERRE", typeof(decimal));
        table.Columns.Add("MONTO_COLOCACION_CIERRE", typeof(decimal));
        table.Columns.Add("NO_COLOCACION_ACTUAL", typeof(decimal));
        table.Columns.Add("MONTO_COLOCACION_ACTUAL", typeof(decimal));
        table.Columns.Add("META_COLOCACIONES", typeof(decimal));
        table.Columns.Add("CUMPLIMIENTO_COLOCACIONES", typeof(decimal));
        table.Columns.Add("NUM_CREDMORA_CIERRE", typeof(decimal));
        table.Columns.Add("SALDO_MORA_CIERRE", typeof(decimal));
        table.Columns.Add("NUM_CREDMORA_ACTUAL", typeof(decimal));
        table.Columns.Add("SALDO_MORA_ACTUAL", typeof(decimal));
        table.Columns.Add("SALDO_GCOMUNITARIA_CIERRE", typeof(decimal));
        table.Columns.Add("SALDO_GCOMUNITARIA_ACTUAL", typeof(decimal));
        table.Columns.Add("NUM_CREDMORAMAYOR30_ACTUAL", typeof(decimal));
        table.Columns.Add("SALDO_MORAMAYOR30_ACTUAL", typeof(decimal));
        table.Columns.Add("META_MORAMENOR30", typeof(decimal));
        table.Columns.Add("CUMPLIMIENTO_MORAMENOR30", typeof(decimal));
        table.Columns.Add("NUM_CREDMORAMAYOR60_ACTUAL", typeof(decimal));
        table.Columns.Add("SALDO_MORAMAYOR60_ACTUAL", typeof(decimal));
        table.Columns.Add("NUM_CREDMORAMENOR30_ACTUAL", typeof(decimal));
        table.Columns.Add("SALDO_MORAMENOR30_ACTUAL", typeof(decimal));
        table.Columns.Add("META_MORAMAYOR30", typeof(decimal));
        table.Columns.Add("CUMPLIMIENTO_MORAMAYOR30", typeof(decimal));
        table.Columns.Add("FECHA_HISTORICO", typeof(DateTime));
        table.Columns["FECHA_HISTORICO"].AllowDBNull = true;

        Int64 COD_OFICINA = 0;
        string OFICINA = "";
        Int64 COD_ASESOR_COM = 0;
        string NOMBRE = "";
        decimal NUM_CRED_CIERRE = 0m;
        decimal SALDO_CAPITAL_CIERRE = 0m;
        decimal NUM_CRED_ACTUAL = 0m;
        decimal SALDO_CAPITAL_ACTUAL = 0m;
        decimal NO_COLOCACION_CIERRE = 0m;
        decimal MONTO_COLOCACION_CIERRE = 0m;
        decimal NO_COLOCACION_ACTUAL = 0m;
        decimal MONTO_COLOCACION_ACTUAL = 0m;
        decimal META_COLOCACIONES = 0m;
        decimal CUMPLIMIENTO_COLOCACIONES = 0m;
        decimal NUM_CREDMORA_CIERRE = 0m;
        decimal SALDO_MORA_CIERRE = 0m;
        decimal NUM_CREDMORA_ACTUAL = 0m;
        decimal SALDO_MORA_ACTUAL = 0m;
        decimal SALDO_GCOMUNITARIA_CIERRE = 0m;
        decimal SALDO_GCOMUNITARIA_ACTUAL = 0m;
        decimal NUM_CREDMORAMAYOR30_ACTUAL = 0m;
        decimal SALDO_MORAMAYOR30_ACTUAL = 0m;
        decimal META_MORAMENOR30 = 0m;
        decimal CUMPLIMIENTO_MORAMENOR30 = 0m;
        decimal NUM_CREDMORAMAYOR60_ACTUAL = 0m;
        decimal SALDO_MORAMAYOR60_ACTUAL = 0m;
        decimal NUM_CREDMORAMENOR30_ACTUAL = 0m;
        decimal SALDO_MORAMENOR30_ACTUAL = 0m;
        decimal META_MORAMAYOR30 = 0m;
        decimal CUMPLIMIENTO_MORAMAYOR30 = 0m;

        decimal TNUM_CRED_CIERRE = 0m;
        decimal TSALDO_CAPITAL_CIERRE = 0m;
        decimal TNUM_CRED_ACTUAL = 0m;
        decimal TSALDO_CAPITAL_ACTUAL = 0m;
        decimal TNO_COLOCACION_CIERRE = 0m;
        decimal TMONTO_COLOCACION_CIERRE = 0m;
        decimal TNO_COLOCACION_ACTUAL = 0m;
        decimal TMONTO_COLOCACION_ACTUAL = 0m;
        decimal TMETA_COLOCACIONES = 0m;
        decimal TCUMPLIMIENTO_COLOCACIONES = 0m;
        decimal TNUM_CREDMORA_CIERRE = 0m;
        decimal TSALDO_MORA_CIERRE = 0m;
        decimal TNUM_CREDMORA_ACTUAL = 0m;
        decimal TSALDO_MORA_ACTUAL = 0m;
        decimal TSALDO_GCOMUNITARIA_CIERRE = 0m;
        decimal TSALDO_GCOMUNITARIA_ACTUAL = 0m;
        decimal TNUM_CREDMORAMAYOR30_ACTUAL = 0m;
        decimal TSALDO_MORAMAYOR30_ACTUAL = 0m;
        decimal TMETA_MORAMENOR30 = 0m;
        decimal TCUMPLIMIENTO_MORAMENOR30 = 0m;
        decimal TNUM_CREDMORAMAYOR60_ACTUAL = 0m;
        decimal TSALDO_MORAMAYOR60_ACTUAL = 0m;
        decimal TNUM_CREDMORAMENOR30_ACTUAL = 0m;
        decimal TSALDO_MORAMENOR30_ACTUAL = 0m;
        decimal TMETA_MORAMAYOR30 = 0m;
        decimal TCUMPLIMIENTO_MORAMAYOR30 = 0m;

        foreach (GestionDiaria item in LstDetalleComprobante)
        {
            DataRow datarw;
            datarw = table.NewRow();

            // Si se paso a la siguiente oficina entonces insertar una fila con los totales
            if (OFICINA != item.OFICINA && OFICINA != "")
            {
                DataRow datarw3;
                datarw3 = table.NewRow();
                datarw3[0] = DBNull.Value;
                datarw3[1] = DBNull.Value;
                datarw3[2] = DBNull.Value;
                datarw3[3] = "TOTAL " + OFICINA;
                datarw3[4] = NUM_CRED_CIERRE;
                datarw3[5] = SALDO_CAPITAL_CIERRE;
                datarw3[6] = NUM_CRED_ACTUAL;
                datarw3[7] = SALDO_CAPITAL_ACTUAL;
                datarw3[8] = NO_COLOCACION_CIERRE;
                datarw3[9] = MONTO_COLOCACION_CIERRE;
                datarw3[10] = NO_COLOCACION_ACTUAL;
                datarw3[11] = MONTO_COLOCACION_ACTUAL;
                datarw3[12] = META_COLOCACIONES;
                datarw3[13] = CUMPLIMIENTO_COLOCACIONES;
                datarw3[14] = NUM_CREDMORA_CIERRE;
                datarw3[15] = SALDO_MORA_CIERRE;
                datarw3[16] = NUM_CREDMORA_ACTUAL;
                datarw3[17] = SALDO_MORA_ACTUAL;
                datarw3[18] = SALDO_GCOMUNITARIA_CIERRE;
                datarw3[19] = SALDO_GCOMUNITARIA_ACTUAL;
                datarw3[20] = NUM_CREDMORAMAYOR30_ACTUAL;
                datarw3[21] = SALDO_MORAMAYOR30_ACTUAL;
                datarw3[22] = META_MORAMENOR30;
                datarw3[23] = CUMPLIMIENTO_MORAMENOR30;
                datarw3[24] = NUM_CREDMORAMAYOR60_ACTUAL;
                datarw3[25] = SALDO_MORAMAYOR60_ACTUAL;
                datarw3[26] = NUM_CREDMORAMENOR30_ACTUAL;
                datarw3[27] = SALDO_MORAMENOR30_ACTUAL;
                datarw3[28] = META_MORAMAYOR30;
                datarw3[29] = CUMPLIMIENTO_MORAMAYOR30;
                datarw3[30] = DBNull.Value;
                table.Rows.Add(datarw3);

                NUM_CRED_CIERRE = 0m;
                SALDO_CAPITAL_CIERRE = 0m;
                NUM_CRED_ACTUAL = 0m;
                SALDO_CAPITAL_ACTUAL = 0m;
                NO_COLOCACION_CIERRE = 0m;
                MONTO_COLOCACION_CIERRE = 0m;
                NO_COLOCACION_ACTUAL = 0m;
                MONTO_COLOCACION_ACTUAL = 0m;
                META_COLOCACIONES = 0m;
                CUMPLIMIENTO_COLOCACIONES = 0m;
                NUM_CREDMORA_CIERRE = 0m;
                SALDO_MORA_CIERRE = 0m;
                NUM_CREDMORA_ACTUAL = 0m;
                SALDO_MORA_ACTUAL = 0m;
                SALDO_GCOMUNITARIA_CIERRE = 0m;
                SALDO_GCOMUNITARIA_ACTUAL = 0m;
                NUM_CREDMORAMAYOR30_ACTUAL = 0m;
                SALDO_MORAMAYOR30_ACTUAL = 0m;
                META_MORAMENOR30 = 0m;
                CUMPLIMIENTO_MORAMENOR30 = 0m;
                NUM_CREDMORAMAYOR60_ACTUAL = 0m;
                SALDO_MORAMAYOR60_ACTUAL = 0m;
                NUM_CREDMORAMENOR30_ACTUAL = 0m;
                SALDO_MORAMENOR30_ACTUAL = 0m;
                META_MORAMAYOR30 = 0m;
                CUMPLIMIENTO_MORAMAYOR30 = 0m;
            }

            TNUM_CRED_CIERRE += item.NUM_CRED_CIERRE;
            TSALDO_CAPITAL_CIERRE += item.SALDO_CAPITAL_CIERRE;
            TNUM_CRED_ACTUAL += item.NUM_CRED_ACTUAL;
            TSALDO_CAPITAL_ACTUAL += item.SALDO_CAPITAL_ACTUAL;
            TNO_COLOCACION_CIERRE += item.NO_COLOCACION_CIERRE;
            TMONTO_COLOCACION_CIERRE += item.MONTO_COLOCACION_CIERRE;
            TNO_COLOCACION_ACTUAL += item.NO_COLOCACION_ACTUAL;
            TMONTO_COLOCACION_ACTUAL += item.MONTO_COLOCACION_ACTUAL;
            TMETA_COLOCACIONES += item.META_COLOCACIONES;
            TCUMPLIMIENTO_COLOCACIONES += item.CUMPLIMIENTO_COLOCACIONES;
            TNUM_CREDMORA_CIERRE += item.NUM_CREDMORA_CIERRE;
            TSALDO_MORA_CIERRE += item.SALDO_MORA_CIERRE;
            TNUM_CREDMORA_ACTUAL += item.NUM_CREDMORA_ACTUAL;
            TSALDO_MORA_ACTUAL += item.SALDO_MORA_ACTUAL;
            TSALDO_GCOMUNITARIA_CIERRE += item.SALDO_GCOMUNITARIA_CIERRE;
            TSALDO_GCOMUNITARIA_ACTUAL += item.SALDO_GCOMUNITARIA_ACTUAL;
            TNUM_CREDMORAMAYOR30_ACTUAL += item.NUM_CREDMORAMAYOR30_ACTUAL;
            TSALDO_MORAMAYOR30_ACTUAL += item.SALDO_MORAMAYOR30_ACTUAL;
            TMETA_MORAMENOR30 += item.META_MORAMENOR30;
            TCUMPLIMIENTO_MORAMENOR30 += item.CUMPLIMIENTO_MORAMENOR30;
            TNUM_CREDMORAMAYOR60_ACTUAL += item.NUM_CREDMORAMAYOR60_ACTUAL;
            TSALDO_MORAMAYOR60_ACTUAL += item.SALDO_MORAMAYOR60_ACTUAL;
            TNUM_CREDMORAMENOR30_ACTUAL += item.NUM_CREDMORAMENOR30_ACTUAL;
            TSALDO_MORAMENOR30_ACTUAL += item.SALDO_MORAMENOR30_ACTUAL;
            TMETA_MORAMAYOR30 += item.META_MORAMAYOR30;
            TCUMPLIMIENTO_MORAMAYOR30 += item.CUMPLIMIENTO_MORAMAYOR30;
            // Inserta una fila en el datatable con los datos
            datarw[0] = item.COD_OFICINA;
            datarw[1] = item.OFICINA;
            datarw[2] = item.COD_ASESOR_COM;
            datarw[3] = item.NOMBRE;
            datarw[4] = item.NUM_CRED_CIERRE;
            datarw[5] = item.SALDO_CAPITAL_CIERRE;
            datarw[6] = item.NUM_CRED_ACTUAL;
            datarw[7] = item.SALDO_CAPITAL_ACTUAL;
            datarw[8] = item.NO_COLOCACION_CIERRE;
            datarw[9] = item.MONTO_COLOCACION_CIERRE;
            datarw[10] = item.NO_COLOCACION_ACTUAL;
            datarw[11] = item.MONTO_COLOCACION_ACTUAL;
            datarw[12] = item.META_COLOCACIONES;
            datarw[13] = item.CUMPLIMIENTO_COLOCACIONES;
            datarw[14] = item.NUM_CREDMORA_CIERRE;
            datarw[15] = item.SALDO_MORA_CIERRE;
            datarw[16] = item.NUM_CREDMORA_ACTUAL;
            datarw[17] = item.SALDO_MORA_ACTUAL;
            datarw[18] = item.SALDO_GCOMUNITARIA_CIERRE;
            datarw[19] = item.SALDO_GCOMUNITARIA_ACTUAL;
            datarw[20] = item.NUM_CREDMORAMAYOR30_ACTUAL;
            datarw[21] = item.SALDO_MORAMAYOR30_ACTUAL;
            datarw[22] = item.META_MORAMENOR30;
            datarw[23] = item.CUMPLIMIENTO_MORAMENOR30;
            datarw[24] = item.NUM_CREDMORAMAYOR60_ACTUAL;
            datarw[25] = item.SALDO_MORAMAYOR60_ACTUAL;
            datarw[26] = item.NUM_CREDMORAMENOR30_ACTUAL;
            datarw[27] = item.SALDO_MORAMENOR30_ACTUAL;
            datarw[28] = item.META_MORAMAYOR30;
            datarw[29] = item.CUMPLIMIENTO_MORAMAYOR30;
            datarw[30] = item.FECHA_HISTORICO;
            table.Rows.Add(datarw);

            // Actualiza variables de totales por oficina
            COD_OFICINA = item.COD_OFICINA;
            OFICINA = item.OFICINA;
            COD_ASESOR_COM = item.COD_ASESOR_COM;
            NOMBRE = item.NOMBRE;
            NUM_CRED_CIERRE = NUM_CRED_CIERRE + item.NUM_CRED_CIERRE;
            SALDO_CAPITAL_CIERRE = SALDO_CAPITAL_CIERRE + item.SALDO_CAPITAL_CIERRE;
            NUM_CRED_ACTUAL = NUM_CRED_ACTUAL + item.NUM_CRED_ACTUAL;
            SALDO_CAPITAL_ACTUAL = SALDO_CAPITAL_ACTUAL + item.SALDO_CAPITAL_ACTUAL;
            NO_COLOCACION_CIERRE = NO_COLOCACION_CIERRE + item.NO_COLOCACION_CIERRE;
            MONTO_COLOCACION_CIERRE = MONTO_COLOCACION_CIERRE + item.MONTO_COLOCACION_CIERRE;
            NO_COLOCACION_ACTUAL = NO_COLOCACION_ACTUAL + item.NO_COLOCACION_ACTUAL;
            MONTO_COLOCACION_ACTUAL = MONTO_COLOCACION_ACTUAL + item.MONTO_COLOCACION_ACTUAL;
            META_COLOCACIONES = META_COLOCACIONES + item.META_COLOCACIONES;
            CUMPLIMIENTO_COLOCACIONES = CUMPLIMIENTO_COLOCACIONES + item.CUMPLIMIENTO_COLOCACIONES;
            NUM_CREDMORA_CIERRE = NUM_CREDMORA_CIERRE + item.NUM_CREDMORA_CIERRE;
            SALDO_MORA_CIERRE = SALDO_MORA_CIERRE + item.SALDO_MORA_CIERRE;
            NUM_CREDMORA_ACTUAL = NUM_CREDMORA_ACTUAL + item.NUM_CREDMORA_ACTUAL;
            SALDO_MORA_ACTUAL = SALDO_MORA_ACTUAL + item.SALDO_MORA_ACTUAL;
            SALDO_GCOMUNITARIA_CIERRE = SALDO_GCOMUNITARIA_CIERRE + item.SALDO_GCOMUNITARIA_CIERRE;
            SALDO_GCOMUNITARIA_ACTUAL = SALDO_GCOMUNITARIA_ACTUAL + item.SALDO_GCOMUNITARIA_ACTUAL;
            NUM_CREDMORAMAYOR30_ACTUAL = NUM_CREDMORAMAYOR30_ACTUAL + item.NUM_CREDMORAMAYOR30_ACTUAL;
            SALDO_MORAMAYOR30_ACTUAL = SALDO_MORAMAYOR30_ACTUAL + item.SALDO_MORAMAYOR30_ACTUAL;
            META_MORAMENOR30 = META_MORAMENOR30 + item.META_MORAMENOR30;
            CUMPLIMIENTO_MORAMENOR30 = CUMPLIMIENTO_MORAMENOR30 + item.CUMPLIMIENTO_MORAMENOR30;
            NUM_CREDMORAMAYOR60_ACTUAL = NUM_CREDMORAMAYOR60_ACTUAL + item.NUM_CREDMORAMAYOR60_ACTUAL;
            SALDO_MORAMAYOR60_ACTUAL = SALDO_MORAMAYOR60_ACTUAL + item.SALDO_MORAMAYOR60_ACTUAL;
            NUM_CREDMORAMENOR30_ACTUAL = NUM_CREDMORAMENOR30_ACTUAL + item.NUM_CREDMORAMENOR30_ACTUAL;
            SALDO_MORAMENOR30_ACTUAL = SALDO_MORAMENOR30_ACTUAL + item.SALDO_MORAMENOR30_ACTUAL;
            META_MORAMAYOR30 = META_MORAMAYOR30 + item.META_MORAMAYOR30;
            CUMPLIMIENTO_MORAMAYOR30 = CUMPLIMIENTO_MORAMAYOR30 + item.CUMPLIMIENTO_MORAMAYOR30;

        }

        DataRow datarw1;
        datarw1 = table.NewRow();
        datarw1[0] = DBNull.Value;
        datarw1[1] = DBNull.Value;
        datarw1[2] = DBNull.Value;
        datarw1[3] = "TOTAL GENERAL";
        datarw1[4] = TNUM_CRED_CIERRE;
        datarw1[5] = TSALDO_CAPITAL_CIERRE;
        datarw1[6] = TNUM_CRED_ACTUAL;
        datarw1[7] = TSALDO_CAPITAL_ACTUAL;
        datarw1[8] = TNO_COLOCACION_CIERRE;
        datarw1[9] = TMONTO_COLOCACION_CIERRE;
        datarw1[10] = TNO_COLOCACION_ACTUAL;
        datarw1[11] = TMONTO_COLOCACION_ACTUAL;
        datarw1[12] = TMETA_COLOCACIONES;
        datarw1[13] = TCUMPLIMIENTO_COLOCACIONES;
        datarw1[14] = TNUM_CREDMORA_CIERRE;
        datarw1[15] = TSALDO_MORA_CIERRE;
        datarw1[16] = TNUM_CREDMORA_ACTUAL;
        datarw1[17] = TSALDO_MORA_ACTUAL;
        datarw1[18] = TSALDO_GCOMUNITARIA_CIERRE;
        datarw1[19] = TSALDO_GCOMUNITARIA_ACTUAL;
        datarw1[20] = TNUM_CREDMORAMAYOR30_ACTUAL;
        datarw1[21] = TSALDO_MORAMAYOR30_ACTUAL;
        datarw1[22] = TMETA_MORAMENOR30;
        datarw1[23] = TCUMPLIMIENTO_MORAMENOR30;
        datarw1[24] = TNUM_CREDMORAMAYOR60_ACTUAL;
        datarw1[25] = TSALDO_MORAMAYOR60_ACTUAL;
        datarw1[26] = TNUM_CREDMORAMENOR30_ACTUAL;
        datarw1[27] = TSALDO_MORAMENOR30_ACTUAL;
        datarw1[28] = TMETA_MORAMAYOR30;
        datarw1[29] = TCUMPLIMIENTO_MORAMAYOR30;
        datarw1[30] = DBNull.Value;
        table.Rows.Add(datarw1);
        return table;
    }

    protected void btnInforme_Click(object sender, EventArgs e)
    {
        string valor = lablerror0.Text;

        ReportParameter[] param = new ReportParameter[1];
        param[0] = new ReportParameter("ImagenReport", ImagenReporte());

        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTableMovimientos());
        ReportViewer1.LocalReport.DataSources.Add(rds);
        ReportViewer1.LocalReport.Refresh();
        ReportViewer1.Visible = true;
    }



}
