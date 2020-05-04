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
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;

using Cantidad_a_Letra;
using Microsoft.Reporting.WebForms;
using System.IO;

partial class Lista : GlobalWeb
{
    CuentasPorPagarService CuentaService = new CuentasPorPagarService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {       
            VisualizarOpciones(CuentaService.CodigoProgramaReporte, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaReporte, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaReporte, "Page_Load", ex);
        }
    }

    bool validarIngresoDefechas()
    {
        if (txtVencimientoIni.Text != "" && txtVencimientoFin.Text != "")
        {
            if (Convert.ToDateTime(txtVencimientoIni.Text) > Convert.ToDateTime(txtVencimientoFin.Text))
            {
                VerError("Datos erroneos en las Fechas de Vencimiento.");
                return false;
            }
        }
        return true;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (validarIngresoDefechas())
        {
            Page.Validate();
            gvLista.Visible = true;
            if (Page.IsValid)
            {
                Actualizar();
            }
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    void cargarDropdown()
    {       
        ddlEstado.Items.Insert(0, new ListItem("Seleccione un item","0"));
        ddlEstado.Items.Insert(1, new ListItem("Pendiente", "1"));
        ddlEstado.Items.Insert(2, new ListItem("Pagado", "2"));
        ddlEstado.Items.Insert(3, new ListItem("Anulado", "3"));
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
            BOexcepcion.Throw(CuentaService.CodigoProgramaReporte, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();        
        Session[CuentaService.CodigoProgramaReporte + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea realizar la eliminación de la cuenta?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            CuentaService.EliminarCuentasXpagar(Convert.ToInt32(Session["ID"]), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaReporte, "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<CuentasPorPagar> lstConsulta = new List<CuentasPorPagar>();
            String filtro = obtFiltro(ObtenerValores());
            DateTime pFechaIni, pFechaFin, pVencIni, pVencFin;
            pFechaIni = DateTime.MinValue;
            pFechaFin = DateTime.MinValue;
            pVencIni = txtVencimientoIni.ToDateTime == null ? DateTime.MinValue : txtVencimientoIni.ToDateTime;
            pVencFin = txtVencimientoFin.ToDateTime == null ? DateTime.MinValue : txtVencimientoFin.ToDateTime;

            lstConsulta = CuentaService.ListarCuentasXpagar(ObtenerValores(), pFechaIni, pFechaFin, pVencIni, pVencFin, (Usuario)Session["usuario"],filtro);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
            }
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString(); 

            Session.Add(CuentaService.CodigoProgramaReporte + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaReporte, "Actualizar", ex);
        }
    }

    private CuentasPorPagar ObtenerValores()
    {
        CuentasPorPagar vCuentas = new CuentasPorPagar();
        if (txtCodigo.Text.Trim() != "")
            vCuentas.codigo_factura = Convert.ToInt32(txtCodigo.Text.Trim());
        
        return vCuentas;
    }
   
    private string obtFiltro(CuentasPorPagar Cuenta)
    {        
        String filtro = String.Empty;

        if (txtCodigo.Text.Trim() != "")
            filtro += " and c.CODIGO_FACTURA = " + txtCodigo.Text;
        if (ddlEstado.SelectedIndex != 0)
            filtro += " and c.estado = " + ddlEstado.SelectedValue;

        return filtro;
    }






    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        VerError("");
        try
        {
            if (e.CommandName == "Imprimir")
            {
                int Index = Convert.ToInt32(e.CommandArgument);
                int cod = Convert.ToInt32(gvLista.Rows[Index].Cells[1].Text);

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

                //RECUPERAR DATOS
                CuentasPorPagar vDatos = new CuentasPorPagar();
                vDatos.codigo_factura = cod;
                vDatos = CuentaService.ConsultarDatosReporte(vDatos, (Usuario)Session["usuario"]);

                CuentaXpagar_Detalle vData = new CuentaXpagar_Detalle();
                vData.codigo_factura = cod;
                List<CuentaXpagar_Detalle> lstDetalle = new List<CuentaXpagar_Detalle>();
                lstDetalle = CuentaService.ConsultarDetalleCuentasXpagar(vData, (Usuario)Session["usuario"]);

                List<CuentaXpagar_Pago> lstFormaPago = new List<CuentaXpagar_Pago>();
                CuentaXpagar_Pago vForm = new CuentaXpagar_Pago();
                vForm.codigo_factura = cod;
                lstFormaPago = CuentaService.ConsultarDetalleFormaPago(vForm, (Usuario)Session["usuario"]);

                //CREACION DE LA TABLA ENCABEZADO
                System.Data.DataTable table = new System.Data.DataTable();
                table.Columns.Add("codigo");
                table.Columns.Add("num_factura");
                table.Columns.Add("fechaingreso");
                table.Columns.Add("fechavencimiento");
                table.Columns.Add("identificacion");
                table.Columns.Add("nombre");
                table.Columns.Add("vrtotal");
                table.Columns.Add("por_desc");
                table.Columns.Add("por_iva");
                table.Columns.Add("por_reten");
                table.Columns.Add("por_retenIva");
                table.Columns.Add("por_retenTim");
                table.Columns.Add("vr_neto");

                //TABLA DETALLE
                System.Data.DataTable deta = new System.Data.DataTable();
                deta.Columns.Add("iddetalle");
                deta.Columns.Add("nom_deta");
                deta.Columns.Add("cantidad");
                deta.Columns.Add("vr_unitario");
                deta.Columns.Add("codigo");

                //FORMA PAGO
                System.Data.DataTable fpago = new System.Data.DataTable();
                fpago.Columns.Add("al30");
                fpago.Columns.Add("al60");
                fpago.Columns.Add("mayor60");

                //LLENAR LAS TABLAS CON LOS DATOS CORRESPONDIENTES

                DataRow datarw;
                datarw = table.NewRow();

                datarw[0] = " " + vDatos.codigo_factura;
                datarw[1] = " " + vDatos.numero_factura;
                datarw[2] = " " + vDatos.fecha_ingreso.ToShortDateString();
                if (vDatos.fecha_vencimiento != null)
                    datarw[3] = " " + Convert.ToDateTime(vDatos.fecha_vencimiento).ToShortDateString();
                else
                    datarw[3] = " ";
                datarw[4] = " " + vDatos.identificacion;
                datarw[5] = " " + vDatos.nombre;
                datarw[6] = " " + vDatos.valor_total.ToString("n");
                datarw[7] = " " + vDatos.porc_descuento;
                datarw[8] = " " + vDatos.porc_iva;
                datarw[9] = " " + vDatos.porc_retencion;
                datarw[10] = " " + vDatos.porc_reteiva;
                datarw[11] = " " + vDatos.porc_timbre;
                datarw[12] = " " + vDatos.valor_neto.ToString("n");
                table.Rows.Add(datarw);

                if (lstDetalle.Count > 0)
                {
                    foreach (CuentaXpagar_Detalle rFila in lstDetalle)
                    {
                        DataRow dr;
                        dr = deta.NewRow();
                        dr[0] = " " + rFila.coddetallefac;
                        dr[1] = " " + rFila.detalle;
                        dr[2] = " " + rFila.cantidad;
                        dr[3] = " " + rFila.valor_unitario;
                        dr[4] = " " + rFila.codigo_factura;
                        deta.Rows.Add(dr);
                    }
                }

                decimal al30 = 0, al60 = 0, masD60 = 0;
                if (lstFormaPago.Count > 0)
                {
                    DateTime fechaMaxima = DateTime.Now.AddYears(70), fechaMinima = DateTime.MinValue, fechaal30, fecha31al60;


                    foreach (CuentaXpagar_Pago rFila in lstFormaPago) //Obteniendo la fecha minima y rangos de fechas
                    {
                        if (rFila.fecha != DateTime.MinValue && rFila.fecha != null)
                        {
                            if (rFila.fecha < fechaMaxima && rFila.fecha != DateTime.MinValue && rFila.fecha != null)
                            {
                                fechaMaxima = Convert.ToDateTime(rFila.fecha);
                                fechaMinima = Convert.ToDateTime(rFila.fecha);
                            }
                        }
                    }
                    fechaal30 = fechaMinima.AddMonths(1);
                    fecha31al60 = fechaal30.AddMonths(1);

                    foreach (CuentaXpagar_Pago fila in lstFormaPago) //Asignando los Valores
                    {
                        if (fila.fecha >= fechaMinima && fila.fecha <= fechaal30)
                        {
                            if (fila.valor != 0)
                                al30 += Convert.ToDecimal(fila.valor);
                        }
                        else if (fila.fecha > fechaal30 && fila.fecha <= fecha31al60)
                        {
                            if (fila.valor != 0)
                                al60 += Convert.ToDecimal(fila.valor);
                        }
                        else
                        {
                            if (fila.valor != 0)
                                masD60 += Convert.ToDecimal(fila.valor);
                        }
                    }
                }
                //PASANDO DATOS A LA TABLA 
                DataRow dtr;
                dtr = fpago.NewRow();
                dtr[0] = al30.ToString("n");
                dtr[1] = al60.ToString("n");
                dtr[2] = masD60.ToString("n");
                fpago.Rows.Add(dtr);



                //PASAR LOS DATOS AL REPORTE
                Usuario pUsuario = new Usuario();
                pUsuario = (Usuario)Session["Usuario"];

                ReportParameter[] param = new ReportParameter[3];
                param[0] = new ReportParameter("entidad", pUsuario.empresa);
                param[1] = new ReportParameter("nit", pUsuario.nitempresa);
                param[2] = new ReportParameter("ImagenReport", ImagenReporte());

                rvCuentasXpagar.LocalReport.EnableExternalImages = true;
                rvCuentasXpagar.LocalReport.SetParameters(param);

                ReportDataSource rds = new ReportDataSource("DataSet1", table);
                ReportDataSource rds1 = new ReportDataSource("DataSet2", deta);
                ReportDataSource rds2 = new ReportDataSource("DataSet3", fpago);
                rvCuentasXpagar.LocalReport.DataSources.Clear();
                rvCuentasXpagar.LocalReport.DataSources.Add(rds);
                rvCuentasXpagar.LocalReport.DataSources.Add(rds1);
                rvCuentasXpagar.LocalReport.DataSources.Add(rds2);
                rvCuentasXpagar.LocalReport.Refresh();


                // MOSTRAR REPORTE EN PANTALLA
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                byte[] bytes = rvCuentasXpagar.LocalReport.Render("PDF", null, out mimeType,
                               out encoding, out extension, out streamids, out warnings);
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output.pdf"),
                FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                Session["Archivo" + Usuario.codusuario] = Server.MapPath("output.pdf");
                frmPrint.Visible = true;
                rvCuentasXpagar.Visible = false;


                mvPrincipal.ActiveViewIndex = 1;

            }
        }
        catch (Exception ex)
        { VerError(ex.Message); }
    }


    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 0;       
    }
}