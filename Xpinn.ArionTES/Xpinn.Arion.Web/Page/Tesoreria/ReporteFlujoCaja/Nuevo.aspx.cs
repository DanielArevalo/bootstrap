using System;
using System.Collections.Generic;
using System.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;
using Xpinn.Util;
using Microsoft.Reporting.WebForms;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

public partial class Nuevo : GlobalWeb
{
    ParametrosFlujoCajaService ParamFlujoCajaServicio = new ParametrosFlujoCajaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ParamFlujoCajaServicio.CodigoProgramaR + ".id"] != null)
                VisualizarOpciones(ParamFlujoCajaServicio.CodigoProgramaR, "E");
            else
                VisualizarOpciones(ParamFlujoCajaServicio.CodigoProgramaR, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParamFlujoCajaServicio.CodigoProgramaR, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarConsultar(true);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParamFlujoCajaServicio.CodigoProgramaR, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        List<string> titulos = new List<string>();
        string valor = "";

        titulos = ParamFlujoCajaServicio.ListarTitulos(Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFinal.Text), (Usuario)Session["usuario"]);

        DataTable dtDatos = new DataTable();
        DataRow drDatos;

        gvConceptos.Columns.Clear();
        BoundField ColumnBoundT = new BoundField();
        ColumnBoundT.HeaderText = "Tipo";
        ColumnBoundT.DataField = "Tipo";
        ColumnBoundT.DataFormatString = "";
        ColumnBoundT.ItemStyle.Width = 100;
        ColumnBoundT.ControlStyle.Width = 100;
        ColumnBoundT.HeaderStyle.Width = 100;
        gvConceptos.Columns.Add(ColumnBoundT);
        dtDatos.Columns.Add("Tipo", typeof(string));
        dtDatos.Columns["Tipo"].AllowDBNull = true;
        dtDatos.Columns["Tipo"].DefaultValue = "";

        BoundField ColumnBound = new BoundField();
        ColumnBound.HeaderText = "Conceptos";
        ColumnBound.DataField = "Conceptos";
        ColumnBound.DataFormatString = "";
        ColumnBound.ItemStyle.Width = 100;
        ColumnBound.ControlStyle.Width = 100;
        ColumnBound.HeaderStyle.Width = 100;
        gvConceptos.Columns.Add(ColumnBound);
        dtDatos.Columns.Add("Conceptos", typeof(string));
        dtDatos.Columns["Conceptos"].AllowDBNull = true;
        dtDatos.Columns["Conceptos"].DefaultValue = "";

        string[] totalesI = new string[titulos.Count];
        string[] totalesE = new string[titulos.Count];
        string[] totalesO = new string[titulos.Count];
        string[] totalesC = new string[titulos.Count];

        //Agregar columnas de titulos
        foreach (string titulo in titulos)
        {
            if (titulo != "" && titulo != null)
            {
                BoundField ColumnBoundV = new BoundField();
                ColumnBoundV.HeaderText = titulo;
                ColumnBoundV.DataField = titulo;
                ColumnBoundV.DataFormatString = "{0:N}";
                ColumnBoundV.ItemStyle.Width = 100;
                ColumnBoundV.ControlStyle.Width = 100;
                ColumnBoundV.HeaderStyle.Width = 100;
                gvConceptos.Columns.Add(ColumnBoundV);
                dtDatos.Columns.Add(titulo, typeof(string));
                dtDatos.Columns[titulo].AllowDBNull = true;
                dtDatos.Columns[titulo].DefaultValue = "";
            }
        }
        List<ParametrosFlujoCaja> listaC = new List<ParametrosFlujoCaja>();
        listaC = ParamFlujoCajaServicio.ListarConceptosReporte(Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFinal.Text), (Usuario)Session["usuario"]);
        Decimal sumaI, sumaE, sumaO, sumaC;
        int m = 0;
        int d = 0;
        int p = 0;
        //Agregar saldo inicial de la caja
        foreach (ParametrosFlujoCaja parametro in listaC)
        {
            if (parametro.descripcion == "Saldo Inicial Caja")
            {
                drDatos = dtDatos.NewRow();
                drDatos[0] = parametro.nom_tipo_concepto;
                drDatos[1] = parametro.descripcion;
                string[] v = parametro.valores.Split('/');
                string[] fc = parametro.fechas.Split(',');
                int j = 2;
                for (int i = 0; i < v.Length; i++)
                {
                    drDatos[j] = v[i];
                    j++;
                }
                dtDatos.Rows.Add(drDatos);
                dtDatos.AcceptChanges();
            }
        }

        //Insertar una fila vacia
        drDatos = dtDatos.NewRow();
        for (int i = 0; i < gvConceptos.Columns.Count; i++)
        {
            drDatos[i] = "";
        }
        dtDatos.Rows.Add(drDatos);
        dtDatos.AcceptChanges();
        //Agregar valores de conceptos de ingresos
        foreach (ParametrosFlujoCaja parametro in listaC)
        {
            if (parametro.nom_tipo_concepto == "Ingreso")
            {
                drDatos = dtDatos.NewRow();
                drDatos[0] = parametro.nom_tipo_concepto;
                drDatos[1] = parametro.descripcion;
                string[] v = parametro.valores.Split('/');
                string[] fc = parametro.fechas.Split(',');
                int j = 2;
                for (int i = 0; i < v.Length; i++)
                {
                    drDatos[j] = v[i];
                    j++;
                }
                dtDatos.Rows.Add(drDatos);
                dtDatos.AcceptChanges();
            }
        }
        sumaI = 0;
        m = d = 0;
        valor = "";
        for (m = 2; m < dtDatos.Columns.Count; m++)
        {
            sumaI = 0;
            foreach (DataRow fila in dtDatos.Rows) //(n = 1; n < dtDatos.Rows.Count; n++)
            {
                if (fila[0].ToString() == "Ingreso")
                {
                    valor = fila[m].ToString().Replace(".", ",");
                    sumaI += Convert.ToDecimal(valor);
                }
            }
            totalesI[d] = sumaI.ToString();
            d++;
        }
        p = 0;
        //Agregar valores total Ingresos
        drDatos = dtDatos.NewRow();
        drDatos[0] = "Ingresos";
        drDatos[1] = "Total Ingresos";
        p = 2;
        for (int i = 0; i < totalesI.Length; i++)
        {
            drDatos[p] = totalesI[i];
            p++;
        }

        dtDatos.Rows.Add(drDatos);
        dtDatos.AcceptChanges();

        //Insertar una fila vacia
        drDatos = dtDatos.NewRow();
        for (int i = 0; i < gvConceptos.Columns.Count; i++)
        {
            drDatos[i] = "";
        }
        dtDatos.Rows.Add(drDatos);
        dtDatos.AcceptChanges();

        //Agregar valores de conceptos de egresos
        foreach (ParametrosFlujoCaja parametro in listaC)
        {
            if (parametro.nom_tipo_concepto == "Egreso")
            {
                drDatos = dtDatos.NewRow();
                drDatos[0] = parametro.nom_tipo_concepto;
                drDatos[1] = parametro.descripcion;
                string[] v = parametro.valores.Split('/');
                string[] fc = parametro.fechas.Split(',');
                int j = 2;
                for (int i = 0; i < v.Length; i++)
                {
                    drDatos[j] = v[i];
                    j++;
                }
                dtDatos.Rows.Add(drDatos);
                dtDatos.AcceptChanges();
            }
        }
        sumaE = 0;
        m = d = 0;
        for (m = 2; m < dtDatos.Columns.Count; m++)
        {
            sumaE = 0;
            foreach (DataRow fila in dtDatos.Rows) //(n = 1; n < dtDatos.Rows.Count; n++)
            {
                if (fila[0].ToString() == "Egreso")
                {
                    valor = fila[m].ToString().Replace(".", ",");
                    sumaE += Convert.ToDecimal(valor);
                }
            }
            totalesE[d] = sumaE.ToString();
            d++;
        }
        p = 0;
        //Agregar valores total Ingresos
        //Agregar valores total Egresos
        drDatos = dtDatos.NewRow();
        drDatos[0] = "Egresos";
        drDatos[1] = "Total Egresos";
        p = 2;
        for (int i = 0; i < totalesE.Length; i++)
        {
            drDatos[p] = totalesE[i];
            p++;
        }
        dtDatos.Rows.Add(drDatos);
        dtDatos.AcceptChanges();

        //Insertar una fila vacia
        drDatos = dtDatos.NewRow();
        for (int i = 0; i < gvConceptos.Columns.Count; i++)
        {
            drDatos[i] = "";
        }
        dtDatos.Rows.Add(drDatos);
        dtDatos.AcceptChanges();

        //Agregar valores de conceptos de otros egresos
        foreach (ParametrosFlujoCaja parametro in listaC)
        {
            if (parametro.nom_tipo_concepto == "Otros Egresos")
            {
                drDatos = dtDatos.NewRow();
                drDatos[0] = parametro.nom_tipo_concepto;
                drDatos[1] = parametro.descripcion;
                string[] v = parametro.valores.Split('/');
                string[] fc = parametro.fechas.Split(',');
                int j = 2;
                for (int i = 0; i < v.Length; i++)
                {
                    drDatos[j] = v[i];
                    j++;
                }
                dtDatos.Rows.Add(drDatos);
                dtDatos.AcceptChanges();
            }
        }
        sumaO = 0;
        m = d = 0;
        valor = "";
        for (m = 2; m < dtDatos.Columns.Count; m++)
        {
            sumaO = 0;
            foreach (DataRow fila in dtDatos.Rows) //(n = 1; n < dtDatos.Rows.Count; n++)
            {
                if (fila[0].ToString() == "Otros Egresos")
                {
                    valor = fila[m].ToString().Replace(".", ",");
                    sumaO += Convert.ToDecimal(valor);
                }
            }
            totalesO[d] = sumaO.ToString();
            d++;
        }
        p = 0;
        //Agregar valores total Otros Egresos
        drDatos = dtDatos.NewRow();
        drDatos[0] = "Otros Egresos";
        drDatos[1] = "Total Otros Egresos";
        p = 2;
        for (int i = 0; i < totalesE.Length; i++)
        {
            drDatos[p] = totalesO[i];
            p++;
        }
        dtDatos.Rows.Add(drDatos);
        dtDatos.AcceptChanges();


        //Insertar una fila vacia
        drDatos = dtDatos.NewRow();
        for (int i = 0; i < gvConceptos.Columns.Count; i++)
        {
            drDatos[i] = "";
        }
        dtDatos.Rows.Add(drDatos);
        dtDatos.AcceptChanges();

        //Agregar valores de conceptos de saldo caja
        foreach (ParametrosFlujoCaja parametro in listaC)
        {
            if (parametro.nom_tipo_concepto == "Saldo Caja" && parametro.descripcion != "Saldo Inicial Caja")
            {
                drDatos = dtDatos.NewRow();
                drDatos[0] = parametro.nom_tipo_concepto;
                drDatos[1] = parametro.descripcion;
                string[] v = parametro.valores.Split('/');
                string[] fc = parametro.fechas.Split(',');
                int j = 2;
                for (int i = 0; i < v.Length; i++)
                {
                    drDatos[j] = v[i];
                    j++;
                }
                dtDatos.Rows.Add(drDatos);
                dtDatos.AcceptChanges();
            }
        }
        sumaC = 0;
        m = d = 0;
        valor = "";
        for (m = 2; m < dtDatos.Columns.Count; m++)
        {
            sumaC = 0;
            foreach (DataRow fila in dtDatos.Rows) //(n = 1; n < dtDatos.Rows.Count; n++)
            {
                if (fila[0].ToString() == "Saldo Caja")
                {
                    valor = fila[m].ToString().Replace(".", ",");
                    sumaC += Convert.ToDecimal(valor);
                }
            }
            totalesC[d] = sumaC.ToString();
            d++;
        }
        p = 0;
        //Agregar valores total Otros Egresos
        drDatos = dtDatos.NewRow();
        drDatos[0] = "Saldo Caja";
        drDatos[1] = "Total Saldo Caja";
        p = 2;
        for (int i = 0; i < totalesC.Length; i++)
        {
            drDatos[p] = totalesC[i];
            p++;
        }
        dtDatos.Rows.Add(drDatos);
        dtDatos.AcceptChanges();

        //Insertar una fila vacia
        drDatos = dtDatos.NewRow();
        for (int i = 0; i < gvConceptos.Columns.Count; i++)
        {
            drDatos[i] = "";
        }
        dtDatos.Rows.Add(drDatos);
        dtDatos.AcceptChanges();

        Session["datosGrilla"] = dtDatos;
        gvConceptos.DataSource = dtDatos;
        gvConceptos.DataBind();
        btnExportar.Visible = true;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page pagina = new Page();
        dynamic form = new HtmlForm();
        GridView gvExportar = CopiarGridViewParaExportar(gvConceptos, "datosGrilla");
        pagina.EnableEventValidation = false;
        pagina.DesignerInitialize();
        pagina.Controls.Add(form);
        form.Controls.Add(gvExportar);
        pagina.RenderControl(htw);
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=ReporteFlujoCaja.xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

}