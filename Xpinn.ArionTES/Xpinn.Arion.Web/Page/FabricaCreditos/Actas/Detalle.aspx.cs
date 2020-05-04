using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.IO;

partial class Detalle : GlobalWeb
{

    private Xpinn.FabricaCreditos.Services.ActasService ActasServicio = new Xpinn.FabricaCreditos.Services.ActasService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ActasServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, ActasServicio.CodigoPrograma);
                if (Session[ActasServicio.CodigoPrograma + ".consulta"] != null)
                    idObjeto = Session[ActasServicio.CodigoPrograma + ".id"].ToString();
                txtacta.Text = idObjeto;
                String idObjeto2 = Session[ActasServicio.CodigoPrograma + ".fecha"].ToString();
                txtFechaacta.Text = idObjeto2;
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActasServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        gvLista.Visible = true;

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, ActasServicio.CodigoPrograma);
            Actualizar();
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/FabricaCreditos/Actas/Lista.aspx");
    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ActasServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;

        Session[ActasServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
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
            BOexcepcion.Throw(ActasServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    private void Actualizar()
    {
        try
        {
            ObtenerDatosAprobador(Convert.ToInt64(txtacta.Text));
            List<Xpinn.FabricaCreditos.Entities.Credito> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            String filtro = obtFiltro(ObtenerValores());
            lstConsulta = ActasServicio.ListarCreditosActas(ObtenerValores(), (Usuario)Session["usuario"], filtro);

            Session["Reporte"] = lstConsulta;
            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();

            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ActasServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActasServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Credito ObtenerValores()
    {
        Configuracion conf = new Configuracion();
        Credito vCredito = new Credito();
        String Fechaacta = txtFechaacta.Text;

        if (txtacta.Text.Trim() != "")
            vCredito.acta = (Convert.ToInt64(txtacta.Text));

        if (txtFechaacta.Text.Trim() != "")
            vCredito.fechaacta = Convert.ToString(Fechaacta);

        return vCredito;
    }

    private string obtFiltro(Credito credito)
    {
        String filtro = String.Empty;
        String Fechaacta = txtFechaacta.Text;

       if (txtacta.Text.Trim() != "")
            filtro += " and a.CODACTA = " + credito.acta + " ";

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = " and " + filtro;
        }
        return filtro;
    }
    protected void ObtenerDatosAprobador(Int64 pIdObjeto)
    {
        try
        {
            Credito usuario = new Credito();
            if (pIdObjeto != Int64.MinValue)
            {
                pIdObjeto = Convert.ToInt64(txtacta.Text);
                usuario = ActasServicio.ConsultarAprobadorActa(pIdObjeto, (Usuario)Session["usuario"]);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActasServicio.CodigoPrograma, "ObtenerDatosAprobador", ex);
        }
    }

    protected void btnInforme_Click(object sender, EventArgs e)
    {
        Usuario usuap = (Usuario)Session["usuario"];
        String Elaboro = Convert.ToString(usuap.nombre);
        String Fecha1 = Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy"));
        String Cargo = Convert.ToString(usuap.nombreperfil);

        ReportParameter[] param = new ReportParameter[9];
        param[0] = new ReportParameter("PFecha", Convert.ToString(DateTime.Now));
        param[1] = new ReportParameter("PActa", Convert.ToString(txtacta.Text).ToString());
        param[2] = new ReportParameter("PFechaActa", Convert.ToString(txtFechaacta.Text).ToString());
        param[3] = new ReportParameter("pEntidad", usuap.empresa);
        param[4] = new ReportParameter("pElaborado", usuap.representante_legal);
        param[5] = new ReportParameter("pCargo", "GERENTE GENERAL");
        param[6] = new ReportParameter("pfacultad", "dentro de sus facultades  otorgadas por el Consejo de Administración");
        param[7] = new ReportParameter("pAprobado", "Gerencia");
        param[8] = new ReportParameter("ImagenReport", ImagenReporte());
        mvReporte.Visible = true;
        ReportViewActa.LocalReport.EnableExternalImages = true;
        ReportViewActa.LocalReport.SetParameters(param);
        ReportViewActa.LocalReport.DataSources.Clear();
        ReportDataSource rdscreditos = new ReportDataSource("DataSetCreditosReporte", CrearDataTableCreditos());
        ReportViewActa.LocalReport.DataSources.Add(rdscreditos);
        ReportViewActa.LocalReport.Refresh();
        mvReporte.ActiveViewIndex = 0;
        mvReporte.Visible = true;

        //Genera el pdf automaticamente 

        Warning[] warnings;
        string[] streamids;
        string mimetype;
        string encoding;
        string extension;
        string _sSuggestedName = string.Empty;
        byte[] bytes = this.ReportViewActa.LocalReport.Render("PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);
        MemoryStream ms = new MemoryStream(bytes);
        string ruta = Server.MapPath("~/Archivos/Actas/");
        if (!Directory.Exists(ruta))
            Directory.CreateDirectory(ruta);
        if (Directory.Exists(ruta))
        {
            String Fecha = Convert.ToString(DateTime.Now.ToString("MMddyyyy"));
            String fileName = "Acta-" + Fecha + ".pdf";
            string savePath = ruta + fileName;
            FileStream fs = new FileStream(savePath, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
            FileInfo file = new FileInfo(savePath);
            Response.Clear();
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.TransmitFile(file.FullName);
            Response.End();
        }
    }


    public DataTable CrearDataTableCreditos()
    {
        Int64 pIdObjeto = Convert.ToInt64(txtacta.Text);

        List<Credito> LstConsulta = new List<Credito>();
        Credito credito = new Credito();
        string numacta = Convert.ToString(pIdObjeto);
        LstConsulta = ActasServicio.ListarCreditosReporte(credito, (Usuario)Session["usuario"], numacta);
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("NUMERO_RADICACION");
        table.Columns.Add("IDENTIFICACION");
        table.Columns.Add("TIPO_IDENTIFICACION");
        table.Columns.Add("NOMBRES");
        table.Columns.Add("LINEA");
        table.Columns.Add("OFICINA");
        table.Columns.Add("MONTO_APROBADO");
        table.Columns.Add("PLAZO");
        table.Columns.Add("PERIODICIDAD");
        table.Columns.Add("VALOR_CUOTA");
        table.Columns.Add("NUMERO_CUOTAS");
        table.Columns.Add("TASA");
        table.Columns.Add("ASESOR");
        table.Columns.Add("DESCRIPCION_TASA");
        table.Columns.Add("IDEN_CODEUDOR");
        table.Columns.Add("NOM_CODEUDOR");
        table.Columns.Add("ESTADO");

        foreach (Credito item in LstConsulta)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.numero_radicacion;
            datarw[1] = item.identificacion;
            datarw[2] = item.tipo_identificacion;
            datarw[3] = item.nombre;
            datarw[4] = item.linea_credito;
            datarw[5] = item.oficina;
            datarw[6] = item.monto.ToString("##,##0");
            datarw[7] = item.plazo;
            datarw[8] = item.periodicidad;
            datarw[9] = item.valor_cuota.ToString("##,##0");
            datarw[10] = item.numero_cuotas;
            datarw[11] = item.tasa;
            datarw[12] = item.NombreAsesor;
            datarw[13] = item.desc_tasa;
            datarw[14] = item.Codeudor;
            datarw[15] = item.NombreCodeudor;
            datarw[16] = item.estado;
            table.Rows.Add(datarw);
        }
        return table;
    }
}