using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

public partial class Lista : GlobalWeb
{
    private Xpinn.Programado.Services.MovimientoCuentasServices RepVencimiento = new Xpinn.Programado.Services.MovimientoCuentasServices();
    PoblarListas poblar = new PoblarListas();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(RepVencimiento.CodigoProgramaVenci, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarImprimir(false);
            toolBar.MostrarLimpiar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RepVencimiento.CodigoProgramaVenci, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarCombos();
                VerError("");
                txt_fechainicial.ToDateTime = System.DateTime.Now.AddMonths(-1);
                txt_fechafinal.ToDateTime = System.DateTime.Now;
                pDatos.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RepVencimiento.CodigoProgramaVenci, "Page_Load", ex);
        }
    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, RepVencimiento.CodigoProgramaVenci);
        Actualizar();
    }
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, RepVencimiento.CodigoProgramaVenci);
        DataTable dtVacio = new DataTable();
        gvLista.DataSource = dtVacio;
        gvLista.DataBind();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarExportar(false);
        toolBar.MostrarImprimir(false);
        toolBar.MostrarLimpiar(false);
        ddlOficina.SelectedIndex = 0;
        txt_fechainicial.ToDateTime = DateTime.Now;
        txt_fechafinal.ToDateTime = DateTime.Now;
        pDatos.Visible = false;
        txtCuenta.Text = "";
    }
    protected void LlenarCombos()
    {
        poblar.PoblarListaDesplegable("OFICINA", "cod_oficina, nombre", "", "2", ddlOficina, Usuario);
        ddlEstado.Items.Insert(0, new ListItem("Seleccione un item", "-1"));
        ddlEstado.Items.Insert(1, new ListItem("ACTIVOS", "1"));
        //ddlEstado.Items.Insert(2, new ListItem("INACTIVOS", "0"));
       // ddlEstado.Items.Insert(3, new ListItem("TERMINADOS", "2"));
//ddlEstado.Items.Insert(4, new ListItem("ANULADOS", "3"));
        ddlEstado.DataBind();
    }
    private void Actualizar()
    {
        try
        {   
            Usuario pUsuario = (Usuario)Session["Usuario"];
            List<Xpinn.Programado.Entities.CuentasProgramado> lstRepVencimiento = null;
            string pfiltro = obtenerFiltro();
            lstRepVencimiento = RepVencimiento.ListarProgramadoAvencer(pfiltro, pUsuario);
            
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstRepVencimiento;
            Site toolBar = (Site)this.Master;
            decimal total_valor = 0;
            if (lstRepVencimiento.Count > 0)
            {
                total_valor = lstRepVencimiento.ToList().Select(x => x.saldo).Sum();
                
                pDatos.Visible = true;
                gvLista.DataBind();
                pDatos.Visible = true;
                Session["DATA"] = lstRepVencimiento;
                lblTotReg.Text = "<br/><b>Registros encontrados :</b> " + lstRepVencimiento.Count;
                toolBar.MostrarExportar(true);
                toolBar.MostrarImprimir(true);
                toolBar.MostrarLimpiar(true);
                toolBar.MostrarLimpiar(true);                
            }
            else
            {
                Session["DATA"] = null;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarExportar(false);
                toolBar.MostrarImprimir(false);
                toolBar.MostrarLimpiar(false);
                pDatos.Visible = false;
                lblTotReg.Text = "";
            }
            txtTotalSaldo.Text = total_valor.ToString("N2");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RepVencimiento.CodigoProgramaVenci, "Actualizar", ex);
        }
    }

    private string obtenerFiltro()
    {
        string pResult = string.Empty;

        if (txtCuenta.Text.Trim() != "")
            pResult +=  " and numero_programado = " + txtCuenta.Text;


        if (txt_fechainicial.Text.Trim() != "")
        {
            pResult += " and FECHA_VENCIMIENTO_PROGRAMADO(A.NUMERO_PROGRAMADO) >= to_date('" + txt_fechainicial.Text + "','" + gFormatoFecha + "') ";  
        }
        if(txt_fechafinal.Text.Trim() != "")
        {
            pResult += " and FECHA_VENCIMIENTO_PROGRAMADO(A.NUMERO_PROGRAMADO) <= to_date('" + txt_fechafinal.Text + "','" + gFormatoFecha + "') ";
        }
        if (ddlOficina.SelectedItem != null)
        {
            if (ddlOficina.SelectedIndex != 0)
            {
                pResult += " and p.cod_oficina = " + ddlOficina.SelectedValue;
            }
        }
       // if (ddlEstado.SelectedIndex != 0)
      //  {
            pResult += " and A.ESTADO = 1 " ;
       // }

        if (!string.IsNullOrEmpty(pResult))
        {
            pResult = pResult.Substring(4);
            pResult = " WHERE " + pResult;
        }
        return pResult;
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (gvLista.Rows.Count > 0 && Session["DATA"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DATA"];
                gvLista.DataBind();
                gvLista.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvLista);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=ProgramadosAvencer.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
            else
            {
                VerError("No existen Datos");
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 1;
        Site toolBar = (Site)Master;
        toolBar.MostrarExportar(false);
        toolBar.MostrarImprimir(false);
        toolBar.MostrarConsultar(false);
        toolBar.MostrarLimpiar(false);
        muestraInformeReporte();
    }

    void muestraInformeReporte()
    {
        VerError("");
        if (Session["DATA"] == null)
        {
            VerError("No ha generado el Reporte para poder imprimir informacion");
        }
        else
        {
            List<Xpinn.Programado.Entities.CuentasProgramado> lstConsulta = (List<Xpinn.Programado.Entities.CuentasProgramado>)Session["DATA"];

            // LLenar data table con los datos a recoger
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("numero_programado");
            table.Columns.Add("cod_linea_programado");
            table.Columns.Add("nombre");
            table.Columns.Add("identificacion");
            table.Columns.Add("nom_persona");
            table.Columns.Add("nom_oficina");
            table.Columns.Add("fecha_apertura");
            table.Columns.Add("fecha_vencimiento");
            table.Columns.Add("estado");
            table.Columns.Add("valor_cuota");
            table.Columns.Add("plazo");
            table.Columns.Add("saldo");

            foreach (Xpinn.Programado.Entities.CuentasProgramado item in lstConsulta)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = item.numero_programado;
                datarw[1] = item.cod_linea_programado;
                datarw[2] = item.nomlinea;
                datarw[3] = item.identificacion;
                datarw[4] = item.nombre;
                datarw[5] = item.nomoficina;
                datarw[6] = item.fecha_apertura.ToString("dd/MM/yyyy");
                datarw[7] = item.fecha_vencimiento.ToString("dd/MM/yyyy");
                datarw[8] = item.nom_estado;
                datarw[9] = item.valor_cuota.ToString("n");
                datarw[10] = item.plazo;
                datarw[11] = item.saldo.ToString("n");                
                table.Rows.Add(datarw);
            }
            // ---------------------------------------------------------------------------------------------------------
            // Pasar datos al reporte
            // ---------------------------------------------------------------------------------------------------------

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[3];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);            
            param[2] = new ReportParameter("ImagenReport", ImagenReporte());

            rvReporte.LocalReport.EnableExternalImages = true;
            rvReporte.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvReporte.LocalReport.DataSources.Clear();
            rvReporte.LocalReport.DataSources.Add(rds);
            rvReporte.LocalReport.Refresh();
        }
    }
    protected void btnDatos_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarExportar(true);
        toolBar.MostrarImprimir(true);
        toolBar.MostrarConsultar(true);
        mvPrincipal.ActiveViewIndex = 0;
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Xpinn.Programado.Services.CuentasProgramadoServices cuentasProgramado = new Xpinn.Programado.Services.CuentasProgramadoServices();
        //RECUPERAR  PERMITE CIERRE ANTICIPADO
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[cuentasProgramado.CodigoProgramaCierreCuenta + ".id"] = id;
        Navegar("../CierreCuentas/Nuevo.aspx");
        e.NewEditIndex = -1;        
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Xpinn.Programado.Services.CuentasProgramadoServices cuentasProgramado = new Xpinn.Programado.Services.CuentasProgramadoServices();

        string conseID = gvLista.DataKeys[e.RowIndex].Values[0].ToString();
        Session[cuentasProgramado.CodigoProgramaRenovacion + ".id"] = conseID;
        Navegar("../Renovacion/Nuevo.aspx");
  
       
    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        Xpinn.Programado.Services.CuentasProgramadoServices cuentasProgramado = new Xpinn.Programado.Services.CuentasProgramadoServices();

        String id = gvLista.SelectedRow.Cells[3].Text;
        Session[cuentasProgramado.CodigoProgramaProrrogaCuenta + ".id"] = id;
        Navegar("../Prorroga/Nuevo.aspx");
    }

}