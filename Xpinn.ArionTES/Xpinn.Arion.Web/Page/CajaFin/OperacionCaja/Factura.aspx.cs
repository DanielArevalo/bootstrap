using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Xpinn.Util;
using System.Configuration;

public partial class Factura : GlobalWeb
{

    Xpinn.Caja.Services.PersonaService peopleServicio = new Xpinn.Caja.Services.PersonaService();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            frmPrint.Visible = false;

            if (!IsPostBack)
            {
                ObtenerDatos();

                if (ConfigurationManager.AppSettings["impresionValidadoraPorRDLC"] == "1")
                {
                    GenerarReporte();
                    DivFactura.Visible = false;
                    DivButtons.Visible = false;
                }
                else if (ConfigurationManager.AppSettings["impresionValidadoraPorRDLC"] == "2")
                {
                    Navegar("ReciboPago.aspx");
                }
                else
                {
                    DivReporte.Visible = false;
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(peopleServicio.GetType().Name + "D", "Page_Load", ex);
        }
    }

    protected void ObtenerDatos()
    {
        long cod_ope = long.Parse(Session[Usuario.codusuario + "codOpe"].ToString());
        Xpinn.Caja.Services.TipoOperacionService tipOpeService = new Xpinn.Caja.Services.TipoOperacionService();
        Xpinn.Caja.Entities.TipoOperacion tipOpe = new Xpinn.Caja.Entities.TipoOperacion();

        try
        {
            Xpinn.Caja.Entities.Persona people = new Xpinn.Caja.Entities.Persona();
            people = peopleServicio.ConsultarEmpresa(people, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(people.nom_empresa))
                lblEmpresa.Text = people.nom_empresa;
            if (!string.IsNullOrEmpty(people.nit))
                lblNit.Text = people.nit.Trim();
            if (!string.IsNullOrEmpty(people.direccion))
                lblDir.Text = people.direccion;
            if (!string.IsNullOrEmpty(people.telefono))
                lblTel.Text = people.telefono;
            if (!string.IsNullOrEmpty(people.identificacion))
                lblIdentific.Text = people.identificacion;

            // Consultar datos de la operación y listado de movimientos.
            tipOpe.cod_operacion = cod_ope.ToString();
            List<Xpinn.Caja.Entities.TipoOperacion> lstConsulta = new List<Xpinn.Caja.Entities.TipoOperacion>();
            lstConsulta = tipOpeService.ConsultarTranCred(tipOpe, (Usuario)Session["usuario"]);
            Xpinn.Caja.Entities.TipoOperacion operacion = lstConsulta.First();
            lblCajero.Text = operacion.nombre_cajero;
            lblOficina.Text = operacion.nombre_oficina;
            lblCaja.Text = operacion.nombre_caja;
            lblFecha.Text = operacion.fecha_operacion.HasValue ? operacion.fecha_operacion.Value.ToShortDateString() : " ";
            txtObservaciones.Text = operacion.observaciones;

            people.cod_persona = long.Parse(operacion.cod_persona);
            people = peopleServicio.ConsultarPersonaXCodigo(people, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(people.identificacion))
                lblIdentific.Text = people.identificacion;
            if (!string.IsNullOrEmpty(people.nom_persona))
                lblCliente.Text = people.nom_persona;
            if (!string.IsNullOrEmpty(people.ciudad))
                lblCiudad.Text = people.ciudad;
            if (!string.IsNullOrEmpty(people.identificacion))
                lblIdentific.Text = people.identificacion;

            lblCodOpera.Text = cod_ope.ToString();

            // Consultar el valor en efectivo
            people.cod_ope = cod_ope;
            people = peopleServicio.ConsultarValorEfectivo(people, (Usuario)Session["usuario"]);
            lblEfectivo.Text = people.valor_total_efectivo.ToString("N0");

            // Consultar el valor en cheque
            people.cod_ope = cod_ope;
            people = peopleServicio.ConsultarValorChequeCaja(people, (Usuario)Session["usuario"]);
            lblCheque.Text = people.valor_total_cheques.ToString("N0");

            // Consultar el valor por otras formas de pago
            people.cod_ope = cod_ope;
            people = peopleServicio.ConsultarValorOtros(people, (Usuario)Session["usuario"]);
            lblOtros.Text = people.valor_total_otros.ToString("N0");

            // Mostrar el detalle de las transacciones                
            decimal acum = 0;
            gvDetalle.Visible = true;
            gvDetalle.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvDetalle.Visible = true;
                gvDetalle.DataBind();
            }
            else
            {
                gvDetalle.Visible = false;
            }

            // Despues de llenado la grilla de detalles me interesa sumar los datos de valores
            foreach (GridViewRow fila in gvDetalle.Rows)
            {
                decimal valor = 0;
                decimal.TryParse(fila.Cells[3].Text, out valor);
                acum += valor;
            }

            // Determinar el valor del IVA
            tipOpe.cod_operacion = cod_ope.ToString();
            tipOpe = tipOpeService.ConsultarValIva(tipOpe, (Usuario)Session["usuario"]);

            // Determinar totales y subtotales
            lblSubTotal.Text = acum.ToString("N0");
            lblBaseIva.Text = tipOpe.valor_base.ToString("N0");
            lblIva.Text = tipOpe.valor_iva.ToString("N0");

            decimal valTotal = 0;
            decimal valIva = 0;
            decimal subTotal = 0;

            subTotal = !string.IsNullOrWhiteSpace(lblSubTotal.Text) ? 0 : decimal.Parse(lblSubTotal.Text);
            valIva = !string.IsNullOrWhiteSpace(lblIva.Text) ? 0 : decimal.Parse(lblIva.Text);
            valTotal = subTotal + valIva;
            lblTotal.Text = valTotal.ToString("N0");

            // Determinar el número de factura
            tipOpe.cod_operacion = cod_ope.ToString();
            if (Session["vengoDeTesoreria"] != null && (bool)Session["vengoDeTesoreria"])
            {
                tipOpe.num_factura = tipOpeService.ConsultarFactura(Convert.ToInt64(tipOpe.cod_operacion), true, (Usuario)Session["usuario"]);
            }
            else
            {
                tipOpe = tipOpeService.InsertarFactura(tipOpe, (Usuario)Session["usuario"]);
                tipOpe.num_factura = tipOpeService.ConsultarFactura(Convert.ToInt64(tipOpe.cod_operacion), true, (Usuario)Session["usuario"]);
            }
            lblFactura.Text = tipOpe.num_factura;
            fechahoy.Text = Convert.ToString(DateTime.Now);

            //RECUPERAR  parametro general
            Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
            Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
            pData = ConsultaData.ConsultarGeneral(19, (Usuario)Session["usuario"]);
            Int64 parametro = Convert.ToInt64(pData.valor);

            if (parametro == 1)
            {
                lblsaldos.Visible = false;
                // Consultar datos de saldos de productos
                gvSaldos.Visible = false;
                List<Xpinn.Caja.Entities.TipoOperacion> lstProductos = new List<Xpinn.Caja.Entities.TipoOperacion>();
                lstProductos = tipOpeService.ConsultarSaldoProductos(Convert.ToInt64(tipOpe.cod_operacion), (Usuario)Session["usuario"]);
                if (lstConsulta != null)
                {
                    if (lstConsulta.Count > 0)
                    {
                        lblsaldos.Visible = true;
                        gvSaldos.Visible = true;
                        gvSaldos.DataSource = lstProductos;
                        gvSaldos.DataBind();
                    }
                }
            }
            else
            {
                gvSaldos.Visible = true;
            }
        }
        catch (Exception ex)
        {
            VerError("No se pudo determinar datos de la operación, Error:" + ex.Message);
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["vengoDeTesoreria"] != null && (bool)Session["vengoDeTesoreria"])
        {
            Navegar("~/Page/Tesoreria/AnulacionOperaciones/Nuevo.aspx");
        }
        else
        {
            Navegar("~/Page/CajaFin/OperacionCaja/Nuevo.aspx");
        }
    }

    protected void GenerarReporte()
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        //CREAR TABLA TRANSACCIONES;
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("concepto");
        table.Columns.Add("nroref");
        table.Columns.Add("valor");
        foreach (GridViewRow rfila in gvDetalle.Rows)
        {
            System.Data.DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = HttpUtility.HtmlDecode(rfila.Cells[0].Text.Replace("&#233;", "e").Replace("&nbsp;", "").ToUpperInvariant());
            datarw[1] = HttpUtility.HtmlDecode(rfila.Cells[1].Text.Replace("&nbsp;", ""));
            datarw[2] = HttpUtility.HtmlDecode(rfila.Cells[3].Text.Replace("&nbsp;", ""));
            table.Rows.Add(datarw);
        }

        //CREAR TABLA SALDOS;
        System.Data.DataTable tableSaldos = new System.Data.DataTable();
        tableSaldos.Columns.Add("producto");
        tableSaldos.Columns.Add("nroref");
        tableSaldos.Columns.Add("saldo");
        foreach (GridViewRow rfila in gvSaldos.Rows)
        {
            System.Data.DataRow datarw;
            datarw = tableSaldos.NewRow();
            datarw[0] = "Saldo " + HttpUtility.HtmlDecode(rfila.Cells[0].Text.Replace("&#233;", "e"));
            datarw[1] = HttpUtility.HtmlDecode(rfila.Cells[1].Text);
            datarw[2] = "$  " + HttpUtility.HtmlDecode(rfila.Cells[2].Text);
            tableSaldos.Rows.Add(datarw);
        }

        ReportParameter[] param = new ReportParameter[23];
        param[0] = new ReportParameter("logo", new Uri(Server.MapPath("~/Images/LogoEmpresa.jpg")).AbsoluteUri);
        param[1] = new ReportParameter("empresa", pUsuario.empresa);
        param[2] = new ReportParameter("nit", pUsuario.nitempresa);
        param[3] = new ReportParameter("factura", lblFactura.Text);
        param[4] = new ReportParameter("fechahoy", fechahoy.Text);
        param[5] = new ReportParameter("direccion", lblDir.Text);
        param[6] = new ReportParameter("telefono", lblTel.Text);
        param[7] = new ReportParameter("codoperac", lblCodOpera.Text);
        param[8] = new ReportParameter("oficina", lblOficina.Text);
        param[9] = new ReportParameter("caja", lblCaja.Text);
        param[10] = new ReportParameter("cajero", lblCajero.Text);
        param[11] = new ReportParameter("ciudad", lblCiudad.Text);
        param[12] = new ReportParameter("fechaoper", lblFecha.Text);
        param[13] = new ReportParameter("cliente", lblCliente.Text);
        param[14] = new ReportParameter("identificacion", lblIdentific.Text);
        param[15] = new ReportParameter("subtotal", lblSubTotal.Text);
        param[16] = new ReportParameter("baseiva", lblBaseIva.Text);
        param[17] = new ReportParameter("iva", lblIva.Text);
        param[18] = new ReportParameter("total", lblTotal.Text);
        param[19] = new ReportParameter("efectivo", lblEfectivo.Text);
        param[20] = new ReportParameter("cheque", lblCheque.Text);
        param[21] = new ReportParameter("otros", lblOtros.Text);
        param[22] = new ReportParameter("Observaciones", txtObservaciones.Text);

        Rpview.LocalReport.EnableExternalImages = true;
        Rpview.LocalReport.SetParameters(param);
        var sa = Rpview.LocalReport.GetDefaultPageSettings();
        Rpview.LocalReport.DataSources.Clear();
        ReportDataSource rds = new ReportDataSource("dsDetalle", table);
        Rpview.LocalReport.DataSources.Add(rds);
        ReportDataSource rdsSaldos = new ReportDataSource("dsSaldos", tableSaldos);
        Rpview.LocalReport.DataSources.Add(rdsSaldos);
        Rpview.LocalReport.Refresh();
    }

    protected void btnImprimirRep_Click(object sender, EventArgs e)
    {
        if (Rpview.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Usuario pUsuario = (Usuario)Session["Usuario"];
            string cod_usuario = pUsuario.codusuario.ToString();
            long cod_ope = long.Parse(Session[Usuario.codusuario + "codOpe"].ToString());

            byte[] bytes = Rpview.LocalReport.Render("PDF");
            string ruta = HttpContext.Current.Server.MapPath("Archivos\\output" + cod_usuario + cod_ope + ".pdf");

            if (File.Exists(ruta))
            {
                File.Delete(ruta);
            }

            FileStream fs = new FileStream(ruta, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

            // LINEAS POR COMENTAREAR
            // --::::::::::::::::::
            frmPrint.Visible = true;
            Session["Archivo" + Usuario.codusuario] = Server.MapPath("Archivos\\output" + cod_usuario + cod_ope + ".pdf");
            // --::::::::::::::::::

            /* 
             * EVITAR USO DE LIBRERIA ITEXTSHARP YA QUE EN ALGUNOS CASOS NO SE ENCUENTRA LA RUTA DE IMPRESION EN LAS VALIDADORAS
             * PARA EL USO DE ESTA FORMA TOCA COMENTAREAR LAS LINEAS SUPERIORES
            FileInfo file = new FileInfo(ruta);
            Response.Clear();
            Response.AppendHeader("Content-Disposition", "attachment; filename=FACTURA" + cod_ope + "_" + cod_usuario + ".pdf");
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.TransmitFile(file.FullName);
            Response.End(); 
             */
        }
    }


}