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
using Microsoft.Reporting.WebForms;
using Microsoft.CSharp;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Contabilidad.Entities;

partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.ReporteImpuestosService reporteImp = new Xpinn.Contabilidad.Services.ReporteImpuestosService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            //VisualizarOpciones(reporteImp.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImprimir += btnInforme_Click;
            toolBar.MostrarImprimir(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteImp.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                btnInforme.Visible = false;
                btnExportar.Visible = false;
                rbAgrupacion.ClearSelection();
                panelGrilla.Visible = false;
                CargarDropDown();
                VerError("");
                Configuracion conf = new Configuracion();
                try
                {
                    Xpinn.Comun.Services.CiereaService cierreServicio = new Xpinn.Comun.Services.CiereaService();
                    txtFecIni.ToDateTime = cierreServicio.FechaUltimoCierre("C", (Usuario)Session["Usuario"]).AddDays(1);
                    rbAgrupacion.SelectedIndex = 0;
                }
                catch
                {
                    VerError("No se pudo determinar fecha de cierre inicial");
                }
                txtFecFin.ToDateTime = System.DateTime.Now;
               
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteImp.CodigoPrograma, "Page_Load", ex);
        }
    }


    private void CargarDropDown()
    { 
        ReporteImpuestos pReport = new ReporteImpuestos();
        List<ReporteImpuestos> lstDatos = new List<ReporteImpuestos>();
        lstDatos = reporteImp.ListarImpuestosCombo(pReport, (Usuario)Session["usuario"]);

        if (lstDatos.Count > 0)
        {
            ddlTipoImpuesto.DataSource = lstDatos;
            ddlTipoImpuesto.DataTextField = "nombre_impuesto";
            ddlTipoImpuesto.DataValueField = "cod_tipo_impuesto";
            ddlTipoImpuesto.DataBind();
        }

        ddlTipoImpuesto_SelectedIndexChanged(null, null);
    }


    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona");
    }



    Boolean ValidarDatos()
    {
        VerError("");
        //if (ddlCodCuenta.SelectedIndex == 0)
        //{
        //    VerError("Ingrese o seleccione una Cuenta Contable");
        //    return false;
        //}
        if (txtFecIni.Text == "" && txtFecFin.Text == "")
        {
            VerError("Ingrese las fechas de Periodo");
            return false;
        }
        if (Convert.ToDateTime(txtFecFin.Text) < Convert.ToDateTime(txtFecIni.Text))
        {
            VerError("Ingrese una fecha inicial menor a la fecha final");
            return false;
        }
        return true;
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (ValidarDatos())
        {
            GuardarValoresConsulta(pConsulta, reporteImp.CodigoPrograma);
            Actualizar();
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, reporteImp.CodigoPrograma);
        DataTable dtVacio = new DataTable();
        panelGrilla.Visible = false;
        try
        {
            Xpinn.Comun.Services.CiereaService cierreServicio = new Xpinn.Comun.Services.CiereaService();
            txtFecIni.ToDateTime = cierreServicio.FechaUltimoCierre("C", (Usuario)Session["Usuario"]).AddDays(1);
        }
        catch
        {
            VerError("No se pudo determinar fecha de cierre inicial");
        }
        txtFecFin.ToDateTime = System.DateTime.Now;
        rbAgrupacion.ClearSelection();
        mvImpuestos.ActiveViewIndex = 0;
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
            BOexcepcion.Throw(reporteImp.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    private string obtFiltro()
    {
        String filtro = String.Empty;

        if(ddlTipoImpuesto.SelectedIndex != 0)
            filtro += " and Pi.Cod_Tipo_Impuesto = " + ddlTipoImpuesto.SelectedValue.Trim();
        if (ddlCodCuenta.SelectedIndex != 0)
            filtro += " and d.cod_cuenta = '" + ddlCodCuenta.SelectedValue.Trim() + "' ";
        if (txtIdPersona.Text != "")
            filtro += " and d.identificacion like '%" + txtIdPersona.Text + "%'";

        filtro += " and d.tipo = d.naturaleza";

        return filtro;
    }


    private void Actualizar()
    {
        VerError("");

        try
        {
            List<Xpinn.Contabilidad.Entities.ReporteImpuestos> lstConsulta = new List<Xpinn.Contabilidad.Entities.ReporteImpuestos>();
            string agrupacion = "";
            if (rbAgrupacion.SelectedIndex != -1)
                if (rbAgrupacion.SelectedIndex == 0)
                    agrupacion = "d.cod_cuenta";
                else
                    agrupacion = "nombre";

            DateTime pFechaIni, pFechaFin;
            pFechaIni = txtFecIni.ToDateTime == null ? DateTime.MinValue : txtFecIni.ToDateTime;
            pFechaFin = txtFecFin.ToDateTime == null ? DateTime.MinValue : txtFecFin.ToDateTime;

            String filtro = obtFiltro();
                       
            lstConsulta = reporteImp.ListarImpuestos(filtro, pFechaIni, pFechaFin, agrupacion, (Usuario)Session["Usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            Session["DTIMPUESTOS"] = lstConsulta;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                btnInforme.Visible = true;
                btnExportar.Visible = true;
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                Site toolBar = (Site)this.Master;
                toolBar.MostrarImprimir(true);
            }
            else
            {
                btnInforme.Visible = false;
                btnExportar.Visible = false;
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
                Site toolBar = (Site)this.Master;
                toolBar.MostrarImprimir(false);
            }

            Session.Add(reporteImp.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reporteImp.CodigoPrograma, "Actualizar", ex);
        }
    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTIMPUESTOS"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTIMPUESTOS"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=Impuestos.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }            
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }
    }


    protected void btnInforme_Click(object sender, EventArgs e)
    {
        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTIMPUESTOS"] == null)
        {
            VerError("No ha generado el Impuesto para poder imprimir el reporte");
        }

        List<Xpinn.Contabilidad.Entities.ReporteImpuestos> lstConsulta = new List<Xpinn.Contabilidad.Entities.ReporteImpuestos>();
        lstConsulta = (List<Xpinn.Contabilidad.Entities.ReporteImpuestos>)Session["DTIMPUESTOS"];

        // LLenar data table con los datos a recoger
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("identificacion");
        table.Columns.Add("nombre");
        table.Columns.Add("ciudad");
        table.Columns.Add("direccion");
        table.Columns.Add("telefono");
        table.Columns.Add("email");
        table.Columns.Add("fecha");
        table.Columns.Add("num_comprobante");
        table.Columns.Add("tipo_comprobante");
        table.Columns.Add("cod_cuenta");
        table.Columns.Add("base");
        table.Columns.Add("porcentaje");
        table.Columns.Add("valor");

        DataRow datarw;
        if (lstConsulta.Count == 0)
        {
            datarw = table.NewRow();
            for (int i = 0; i <= 12; i++)
            {
                datarw[i] = " ";
            }
            table.Rows.Add(datarw);
        }
        else
        {
            foreach(Xpinn.Contabilidad.Entities.ReporteImpuestos refe in lstConsulta)
            {
                datarw = table.NewRow();
                
                datarw[0] = " " + refe.identificacion;
                datarw[1] = " " + refe.nombre;
                datarw[2] = " " + refe.ciudad;
                datarw[3] = " " + refe.direccion;
                datarw[4] = " " + refe.telefono;
                datarw[5] = " " + refe.email;
                datarw[6] = " " + refe.fecha;
                datarw[7] = " " + refe.num_comprobante;
                datarw[8] = " " + refe.tipo_comprobante;
                datarw[9] = " " + refe.cod_cuenta + " " + refe.nombre_cuenta;
                datarw[10] = " " + refe.baseimp;
                datarw[11] = " " + refe.porcentaje;
                datarw[12] = " " + refe.valor;               
                table.Rows.Add(datarw);
            }
        }


        // ---------------------------------------------------------------------------------------------------------
        // Pasar datos al reporte
        // ---------------------------------------------------------------------------------------------------------

        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        
        ReportParameter[] param = new ReportParameter[5];
        param[0] = new ReportParameter("entidad", pUsuario.empresa);
        param[1] = new ReportParameter("nit", pUsuario.nitempresa);
        param[2] = new ReportParameter("fecha_inicial", txtFecIni.Texto);
        param[3] = new ReportParameter("fecha_final", txtFecFin.Texto);
        param[4] = new ReportParameter("ImagenReport", ImagenReporte());

        rvImpuestos.LocalReport.EnableExternalImages = true;
        rvImpuestos.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", table);
        rvImpuestos.LocalReport.DataSources.Clear();
        rvImpuestos.LocalReport.DataSources.Add(rds);
        rvImpuestos.LocalReport.Refresh();

        // Mostrar el reporte en pantalla.
        mvImpuestos.ActiveViewIndex = 1;

    }


    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvImpuestos.ActiveViewIndex = 0;
    }

    protected void txtIdPersona_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtIdPersona.Text == "")
                txtNomPersona.Text = "";

            Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            Persona1 DatosPersona = new Persona1();

            DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdPersona.Text, (Usuario)Session["usuario"]);
            if (DatosPersona.cod_persona != 0)
                txtCodPersona.Text = DatosPersona.cod_persona.ToString();
            if (DatosPersona.identificacion != "")
                txtIdPersona.Text = DatosPersona.identificacion;
            if (DatosPersona.nombre != "")
                txtNomPersona.Text = DatosPersona.nombre;
        }
        catch { }
    }

    protected void ddlTipoImpuesto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoImpuesto.SelectedIndex >= 0)
        {
            ddlCodCuenta.ClearSelection();
            ddlCodCuenta.Items.Clear();
            ReporteImpuestos pReport = new ReporteImpuestos();
            pReport.cod_tipo_impuesto = Convert.ToInt32(ddlTipoImpuesto.SelectedItem.Value);
            List<ReporteImpuestos> lstDatos = new List<ReporteImpuestos>();
            lstDatos = reporteImp.ListarCuentasImpuesto(pReport, (Usuario)Session["usuario"]);
            if (lstDatos.Count > 0)
            {
                ddlCodCuenta.DataSource = lstDatos;
                ddlCodCuenta.DataTextField = "cod_cuenta";
                ddlCodCuenta.DataValueField = "cod_cuenta";
                ddlCodCuenta.AppendDataBoundItems = true;             
            }
            ddlCodCuenta.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlCodCuenta.SelectedIndex = 0;
            ddlCodCuenta.DataBind();
        }
        else
        {
            ddlCodCuenta.SelectedIndex = 0;
        }
    }    

    protected void ddlCodCuenta_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCodCuenta.SelectedIndex != 0)
        {
            // Determinar los datos de la cuenta contable
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(ddlCodCuenta.SelectedValue, (Usuario)Session["usuario"]);

            // Mostrar el nombre de la cuenta            
            if (txtNomCuenta != null)
                txtNomCuenta.Text = PlanCuentas.nombre;
        }
        else
        {
            txtNomCuenta.Text = "";
        }
    }
}