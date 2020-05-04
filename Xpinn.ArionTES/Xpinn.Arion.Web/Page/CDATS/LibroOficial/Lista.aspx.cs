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
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

partial class Lista : GlobalWeb
{
    LibroOficialCDATService LibroService = new LibroOficialCDATService();
    AperturaCDATService ApertuService = new AperturaCDATService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(LibroService.CodigoProgramaLIB, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarImprimir(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LibroService.CodigoProgramaLIB, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {                 
            if (!Page.IsPostBack)
            {
                cargarDropdown();
                mvPrincipal.ActiveViewIndex = 0;
                
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LibroService.CodigoProgramaLIB, "Page_Load", ex);
        }
    }


    Boolean ValidaDatos()
    {
        VerError("");
        if (txtFechaIni.Text == "")
        {
            VerError("Ingrese la Fecha inicial");
            return false;
        }
        if (txtFechaFin.Text == "")
        {
            VerError("Ingrese la Fecha final");
            return false;
        }
        if (Convert.ToDateTime(txtFechaIni.Text) > Convert.ToDateTime(txtFechaFin.Text))
        {
            VerError("No puede ingresar una fecha inicial mayor a la fecha final.");
            return false;
        }
        return true;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            if(ValidaDatos())
                Actualizar();
        }
    }


    void cargarDropdown()
    {
        Cdat Data = new Cdat();
               

        List<Cdat> lstOficina = new List<Cdat>();

        lstOficina = ApertuService.ListarOficinas(Data, (Usuario)Session["usuario"]);
        if (lstOficina.Count > 0)
        {
            ddlOficina.DataSource = lstOficina;
            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "cod_oficina";
            ddlOficina.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlOficina.SelectedIndex = 0;
            ddlOficina.DataBind();
        }                
    }




    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (gvLista.Rows.Count > 0 && Session["DTCDAT"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTCDAT"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=LibroOficialCDATS.xls");
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

    void muestraInformeReporte()
    {
        VerError("");
        if (Session["DTCDAT"] == null)
        {
            VerError("No ha generado el Reporte para poder imprimir informacion");
        }
        else
        {
            List<AdministracionCDAT> lstConsulta = new List<AdministracionCDAT>();
            lstConsulta = (List<AdministracionCDAT>)Session["DTCDAT"];

            // LLenar data table con los datos a recoger
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("codigo");
            table.Columns.Add("num_cdat");
            table.Columns.Add("fecha_apertura");
            table.Columns.Add("fecha_vencimiento");
            table.Columns.Add("valor");
            table.Columns.Add("tasa_efectiva");
            table.Columns.Add("tasa_nominal");
            table.Columns.Add("modalidad");
            table.Columns.Add("periodicidad");
            table.Columns.Add("plazo");
            table.Columns.Add("identificacion");
            table.Columns.Add("nombres");
            table.Columns.Add("apellidos");
            table.Columns.Add("direccion");
            table.Columns.Add("telefono");
            table.Columns.Add("modalidad_int");
            table.Columns.Add("estado");
            table.Columns.Add("oficina");

            foreach (AdministracionCDAT item in lstConsulta)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = item.codigo_cdat;
                datarw[1] = item.numero_cdat;
                datarw[2] = item.fecha_apertura.ToShortDateString();
                datarw[3] = item.fecha_vencimiento.ToShortDateString();
                datarw[4] = item.valor.ToString("n");
                datarw[5] = item.tasa_efectiva;
                datarw[6] = item.tasa_nominal;
                datarw[7] = item.nommodalidadint;
                datarw[8] = item.nomperiodicidad;
                datarw[9] = item.plazo;
                datarw[10] = item.identificacion;
                datarw[11] = item.nombres;
                datarw[12] = item.apellidos;
                datarw[13] = item.direccion;
                datarw[14] = item.telefono;
                datarw[15] = item.modalidad;
                datarw[16] = item.nomestado;
                datarw[17] = item.nomoficina;
                table.Rows.Add(datarw);
            }
            // ---------------------------------------------------------------------------------------------------------
            // Pasar datos al reporte
            // ---------------------------------------------------------------------------------------------------------

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[5];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("Oficina", pUsuario.nombre_oficina);
            param[3] = new ReportParameter("Usuario", pUsuario.nombre);
            param[4] = new ReportParameter("ImagenReport", ImagenReporte());

            rvReporte.LocalReport.EnableExternalImages = true;
            rvReporte.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvReporte.LocalReport.DataSources.Clear();
            rvReporte.LocalReport.DataSources.Add(rds);
            rvReporte.LocalReport.Refresh();
        }
    }

    protected void btnDatos_Click(object sender,EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarExportar(true);
        toolBar.MostrarImprimir(true);
        toolBar.MostrarConsultar(true);   
        mvPrincipal.ActiveViewIndex = 0;            
    }


    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 1;
        Site toolBar = (Site)Master;
        toolBar.MostrarExportar(false);
        toolBar.MostrarImprimir(false);
        toolBar.MostrarConsultar(false);
        muestraInformeReporte();
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
            BOexcepcion.Throw(LibroService.CodigoProgramaLIB, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            //ctlMensaje.MostrarMensaje("Desea realizar la eliminación?");
            ApertuService.EliminarAperturaCdat(Convert.ToInt64(Session["ID"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LibroService.CodigoProgramaLIB, "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<AdministracionCDAT> lstConsulta = new List<AdministracionCDAT>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime FechaIni,FechaFin;

            FechaIni = txtFechaIni.ToDateTime == null ? DateTime.MinValue : txtFechaIni.ToDateTime;
            FechaFin = txtFechaFin.ToDateTime == null ? DateTime.MinValue : txtFechaFin.ToDateTime;

            lstConsulta = LibroService.ListarCdat(filtro, FechaIni,FechaFin, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;

            Site toolBar = (Site)Master;
            
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTCDAT"] = lstConsulta;

                toolBar.MostrarExportar(true);
                toolBar.MostrarImprimir(true);
            }
            else
            {
                toolBar.MostrarExportar(false);
                toolBar.MostrarImprimir(false);

                Session["DTCDAT"] = null;
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
            }

            Session.Add(LibroService.CodigoProgramaLIB + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LibroService.CodigoProgramaLIB, "Actualizar", ex);
        }
    }

    private AdministracionCDAT ObtenerValores()
    {
        AdministracionCDAT vApertu = new AdministracionCDAT();
       
        if (ddlOficina.SelectedIndex != 0)
            vApertu.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);
        
        return vApertu;
    }



    private string obtFiltro(AdministracionCDAT vApertu)
    {
        String filtro = String.Empty;

        if (ddlOficina.SelectedIndex != 0)
            filtro += " and C.cod_oficina = " + vApertu.cod_oficina;

        filtro += "and C.estado in (2) ";

        filtro += " and t.principal=1 ";

        return filtro;
    }


}