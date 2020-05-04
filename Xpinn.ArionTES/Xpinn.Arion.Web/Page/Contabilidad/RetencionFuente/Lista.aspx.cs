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
using Cantidad_a_Letra;

partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.ImpuestoService retenFuente = new Xpinn.Contabilidad.Services.ImpuestoService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(retenFuente.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarImprimir(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(retenFuente.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                btnExportar.Visible = false;
                panelGrilla.Visible = false;
                Session["Impuestos"] = null;
                CargarDropDown();
                VerError("");
                Configuracion conf = new Configuracion();
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
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(retenFuente.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void CargarDropDown()
    {
        Xpinn.Contabilidad.Services.ReporteImpuestosService reporteImp = new Xpinn.Contabilidad.Services.ReporteImpuestosService();
        ReporteImpuestos pReport = new ReporteImpuestos();
        List<ReporteImpuestos> lstDatos = new List<ReporteImpuestos>();
        lstDatos = reporteImp.ListarImpuestosCombo(pReport, (Usuario)Session["usuario"]);
        Session["Impuestos"] = lstDatos;

        if (lstDatos.Count > 0)
        {
            ddlTipoImpuesto.DataSource = lstDatos.Where(x => Convert.ToInt32(x.principal) != 0 || x.principal == null);
            ddlTipoImpuesto.DataTextField = "nombre_impuesto";
            ddlTipoImpuesto.DataValueField = "cod_tipo_impuesto";
            ddlTipoImpuesto.DataBind();
        }
    }


    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona");
    }


    protected Boolean ValidarDatos(bool pvalidardetalle)
    {
        VerError("");
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

        if (panelGrilla.Visible == true && pvalidardetalle)
        {
            int val = 0;
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                if (cbSeleccionar.Checked)
                    val = 1;
            }
            if (val == 0)
            {
                VerError("Seleccione un Registro en el Detalle");
                return false;
            }
        }
        return true;
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (ValidarDatos(false))
        {
            GuardarValoresConsulta(pConsulta, retenFuente.CodigoPrograma);
            Actualizar();
            //if (txtCodPersona.Text == "" && txtNomPersona.Text == "")// SI Se ha seleccionado una persona no dar opcion a seleccionar varias.
            //    gvLista.Columns[0].Visible = true;
            //else
            //    gvLista.Columns[0].Visible = false;
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, retenFuente.CodigoPrograma);
        DataTable dtVacio = new DataTable();
        gvLista.DataSource = null;
        gvLista.DataBind();
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
            BOexcepcion.Throw(retenFuente.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    private string obtFiltro()
    {
        String filtro = String.Empty;
        // Filtro por persona
        if (txtIdPersona.Text.Trim() != "")
        {
            filtro += " and e.iden_benef = '" + txtIdPersona.Text.Trim() + "' | " + " and d.identificacion = '" + txtIdPersona.Text.Trim() + "' ";
        }
        return filtro;
    }


    private void Actualizar()
    {
        VerError("");

        try
        {
            List<Impuesto> lstConsulta = new List<Impuesto>();
            List<ReporteImpuestos> lstDatos = new List<ReporteImpuestos>();
            DateTime pFechaIni, pFechaFin;
            string result = "";
            pFechaIni = txtFecIni.ToDateTime == null ? DateTime.MinValue : txtFecIni.ToDateTime;
            pFechaFin = txtFecFin.ToDateTime == null ? DateTime.MinValue : txtFecFin.ToDateTime;
            lstDatos = (List<ReporteImpuestos>)Session["Impuestos"];
            String filtro = obtFiltro();
            String pTipoIMpuesto = "";
            if (ddlTipoImpuesto.SelectedValue == "")
            {
                pTipoIMpuesto = "";
            }
            else
            {
                string[] datos = ddlTipoImpuesto.SelectedValue.Split(',');
                foreach (var items in datos)
                {
                    lstDatos = lstDatos.Where(x => Convert.ToInt32(x.depende_de) == Convert.ToInt32(items) || x.depende_de == null) .ToList();
                    var codigos = lstDatos.Select(x => x.cod_tipo_impuesto).ToList();
                    if (codigos.Count > 0)
                    {
                        foreach (var item in codigos)
                        {
                            result += "," + item;
                        }
                    }
                    pTipoIMpuesto = " and p.cod_tipo_impuesto In (" + ddlTipoImpuesto.SelectedValue + result + ") ";
                }

            }

            lstConsulta = retenFuente.ListarRetencion(pTipoIMpuesto, filtro, pFechaIni, pFechaFin, (Usuario)Session["Usuario"]);

            gvLista.PageSize = pageSize;

            gvLista.EmptyDataText = emptyQuery;
            Session["DTRETENCION"] = lstConsulta;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
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
                VerError("No se encontraron Datos");
                btnExportar.Visible = false;
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
            }
            Session.Add(retenFuente.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(retenFuente.CodigoPrograma, "Actualizar", ex);
        }
    }



    protected void btnExportar_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTRETENCION"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.Columns[0].Visible = false;
                gvLista.AllowPaging = false;
                gvLista.DataSource = Session["DTRETENCION"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=RetencionEnLaFuente.xls");
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


    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEncabezado = (CheckBox)sender;
        if (cbSeleccionarEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;
            }
        }
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        if (ValidarDatos(true))
        {
            VerError("");
            if (panelGrilla.Visible == false)
            {
                VerError("Genere la Consulta");
                return;
            }
            if (gvLista.Rows.Count == 0)
            {
                VerError("No existen Datos");
                return;
            }

            //CREACION DE LA TABLA TEMPORAL
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("identificacion");
            table.Columns.Add("nombre");
            table.Columns.Add("ciudad");
            table.Columns.Add("direccion");
            table.Columns.Add("telefono");
            table.Columns.Add("email");
            table.Columns.Add("Fechaexpedicion");
            table.Columns.Add("base", typeof(decimal));
            table.Columns.Add("porcentaje", typeof(decimal));
            table.Columns.Add("valor", typeof(decimal));
            table.Columns.Add("totalletras");
            table.Columns.Add("valorTotal");
            table.Columns.Add("nom_cuenta");

            string identificacion = "";
            string nombre = "";
            string ciudad = "";
            string dreccion = "";
            string telefono = "";
            string email = "";
            string fechaexpedicion = "";
            DateTime fechasd = new DateTime();
            decimal Valortotalbase = 0;
            decimal valortotalporcentaje = 0;
            decimal valorTotal = 0;
            string cod_cuenta = "";
            string nombrecuenta = "";

            //LLENANDO TABLA GENERAL FINAL
            foreach (GridViewRow rfila in gvLista.Rows)
            {
                identificacion = rfila.Cells[1].Text;
                nombre = HttpUtility.HtmlDecode(rfila.Cells[2].Text); //.Replace("&#209;", "Ñ");
                ciudad = rfila.Cells[3].Text;
                dreccion = rfila.Cells[4].Text;
                telefono = rfila.Cells[5].Text;
                email = rfila.Cells[6].Text;
                fechaexpedicion = rfila.Cells[7].Text;
                fechasd = Convert.ToDateTime(fechaexpedicion);
                Valortotalbase = Convert.ToDecimal(rfila.Cells[8].Text);
                valortotalporcentaje = Convert.ToDecimal(rfila.Cells[9].Text) / 100;
                valorTotal = Convert.ToDecimal(rfila.Cells[10].Text);
                cod_cuenta = rfila.Cells[11].Text;
                nombrecuenta = rfila.Cells[12].Text;

                // Determinar el valor en letras
                Cardinalidad numero = new Cardinalidad();
                string cardinal = " ";

              if (valorTotal > 0 )
                {                 
                    cardinal = numero.enletras(valorTotal.ToString());
                    int conta = cardinal.Length - 1;
                    int cont2 = conta - 7;
                    if (cont2 >= 0)
                    {
                        string c = cardinal.Substring(cont2);
                        if (cardinal.Substring(cont2) == "MILLONES" || cardinal.Substring(cont2 + 2) == "MILLON")
                            cardinal = cardinal + " DE PESOS M/CTE";
                        else
                            cardinal = cardinal + " PESOS M/CTE";
                    }
                }
           

                // Adicionar registro al datatable
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = identificacion;//IDENTIFICACION
                datarw[1] = nombre;//NOMBRE
                datarw[2] = ciudad;//CIUDAD
                datarw[3] = dreccion; // DIRECCION
                datarw[4] = telefono;//TELEFONO
                datarw[5] = email;//EMAIL
                datarw[7] = Valortotalbase;//BASE
                datarw[8] = valortotalporcentaje; //PORCENTAJE
                datarw[9] = valorTotal; //VALORTOTAL
                datarw[10] = fechaexpedicion.ToString(); ;//FECHA EXPEDICION
                datarw[11] = valorTotal; //VALORTOTAL
                datarw[12] = nombrecuenta; //NOMBRE CUENTA 
                table.Rows.Add(datarw);
            }


            // ---------------------------------------------------------------------------------------------------------
            // Pasar datos al reporte
            // ---------------------------------------------------------------------------------------------------------
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es-CO");

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[9];
            param[0] = new ReportParameter("entidad", pUsuario.empresa + " " + pUsuario.sigla_empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            Impuesto vData = new Impuesto();
            vData = retenFuente.ConsultaTelefonoEmpresa(Convert.ToInt32(pUsuario.idEmpresa), (Usuario)Session["usuario"]);
            param[2] = new ReportParameter("telefono", vData.telefono);
            param[3] = new ReportParameter("year", txtFecFin.ToDateTime.Year.ToString());
            param[4] = new ReportParameter("direccion", pUsuario.direccion_empresa);
            param[5] = new ReportParameter("ImagenReport", ImagenReporte());
            param[7] = new ReportParameter("representante_legal", pUsuario.representante_legal);
            param[6] = new ReportParameter("direccionempresa", vData.direccion);
            param[8] = new ReportParameter("ciudad", vData.ciudad);
            
            rvRetencion.LocalReport.EnableExternalImages = true;
            rvRetencion.LocalReport.SetParameters(param);
            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvRetencion.LocalReport.DataSources.Clear();
            rvRetencion.LocalReport.DataSources.Add(rds);
            rvRetencion.LocalReport.Refresh();

            frmPrint.Visible = false;
            rvRetencion.Visible = true;

            mvImpuestos.ActiveViewIndex = 1;
        }
    }

    protected void btnImprime_Click(object sender, EventArgs e)
    {
        if (rvRetencion.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = rvRetencion.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output.pdf"),
            FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            frmPrint.Visible = true;
            rvRetencion.Visible = false;

        }
    }




}