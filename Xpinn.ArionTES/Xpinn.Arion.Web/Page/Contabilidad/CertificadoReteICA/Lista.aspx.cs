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
            VisualizarOpciones(retenFuente.CodPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(retenFuente.CodPrograma, "Page_PreInit", ex);
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
                VerError("");
                Configuracion conf = new Configuracion();
                try
                {
                    Xpinn.Comun.Services.CiereaService cierreServicio = new Xpinn.Comun.Services.CiereaService();                    
                    txtFecIni.ToDateTime = cierreServicio.FechaUltimoCierre("C", (Usuario)Session["usuario"]).AddDays(1);
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
            BOexcepcion.Throw(retenFuente.CodPrograma, "Page_Load", ex);
        }
    }


    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodPersona", "txtIdPersona", "txtNomPersona");
    }


    protected Boolean ValidarDatos()
    {
        VerError("");              
        if (txtFecIni.Text.Trim() == "" || txtFecFin.Text.Trim() == "")
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
            GuardarValoresConsulta(pConsulta, retenFuente.CodigoPrograma);
            Actualizar();
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, retenFuente.CodPrograma);
        DataTable dtVacio = new DataTable();
        gvLista.DataSource = null;
        gvLista.DataBind();
        panelGrilla.Visible = false;
        try
        {
            Xpinn.Comun.Services.CiereaService cierreServicio = new Xpinn.Comun.Services.CiereaService();
            txtFecIni.ToDateTime = cierreServicio.FechaUltimoCierre("C", (Usuario)Session["usuario"]).AddDays(1);
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
            BOexcepcion.Throw(retenFuente.CodPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    private string obtFiltro()
    {
        String filtro = " and d.cod_cuenta In (Select im.cod_cuenta From plan_cuentas_impuesto im Where cod_tipo_impuesto = 3) ";
        if (txtCodPersona.Text.Trim()!= "")
        {
            filtro += " | and d.cod_tipo_impuesto = 3 and e.iden_benef = '" + txtCodPersona.Text + "' | " + " and d.cod_tipo_impuesto = 3 and d.identificacion = '" + txtCodPersona.Text + "' ";
        }
        return filtro;
    }


    private void Actualizar()
    {
        VerError("");

        try
        {
            List<Impuesto> lstConsulta = new List<Impuesto>();
            
            DateTime pFechaIni, pFechaFin;
            pFechaIni =  txtFecIni.ToDateTime;
            pFechaFin =  txtFecFin.ToDateTime;

            lstConsulta = retenFuente.getListaGridvServices(obtFiltro(), pFechaIni, pFechaFin, (Usuario)Session["usuario"]);
            var estrin = "";
            foreach (var item in lstConsulta)
            {
                estrin = item.porcentaje.ToString() + " %";
                item.nombre_impuesto = estrin;
            }

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
            }
            else
            {
                VerError("No se encontraron Datos");               
                btnExportar.Visible = false;
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
            }
            Session.Add(retenFuente.CodPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(retenFuente.CodPrograma, "Actualizar", ex);
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


    protected void cbSeleccionarEncabezado_CheckedChanged(object sender,EventArgs e)
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
        if (ValidarDatos())
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
            table.Columns.Add("base");
            table.Columns.Add("porcentaje");
            table.Columns.Add("valor");
            table.Columns.Add("totalletras");
            table.Columns.Add("Fechaexpedicion");
            table.Columns.Add("valorTotal");
            table.Columns.Add("nom_cuenta");

            //TABLA DETALLE
            System.Data.DataTable tabDeta = new System.Data.DataTable();
            tabDeta.Columns.Add("identificacion");
            tabDeta.Columns.Add("nombre");
            tabDeta.Columns.Add("ciudad");
            tabDeta.Columns.Add("direccion");
            tabDeta.Columns.Add("telefono");
            tabDeta.Columns.Add("email");
            tabDeta.Columns.Add("base");
            tabDeta.Columns.Add("porcentaje");
            tabDeta.Columns.Add("valor");
            tabDeta.Columns.Add("Fechaexpedicion");
            tabDeta.Columns.Add("nom_cuenta");

            decimal valorTotal = 0, cont = 0;
            string identificacion = "", identif_ant = "";


            foreach (GridViewRow rfila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rfila.FindControl("cbSeleccionar");
                if (cbSeleccionar.Checked == true)
                {
                    identif_ant = rfila.Cells[1].Text;
                    if (cont == 0)
                        identificacion = rfila.Cells[1].Text;

                    if (identif_ant == identificacion)
                    {
                        valorTotal += Convert.ToDecimal(rfila.Cells[10].Text);

                        DataRow dr;
                        dr = tabDeta.NewRow();
                        dr[0] = " " + rfila.Cells[1].Text;//IDENTIFICACION
                        dr[1] = " " + rfila.Cells[2].Text;//NOMBRE
                        dr[2] = " " + rfila.Cells[3].Text;//CIUDAD
                        dr[3] = " " + rfila.Cells[4].Text; // DIRECCION
                        dr[4] = " " + rfila.Cells[5].Text;//TELEFONO
                        dr[5] = " " + rfila.Cells[6].Text;//EMAIL                    
                        dr[6] = " " + rfila.Cells[8].Text;//BASE
                        dr[7] = " " + rfila.Cells[9].Text;//PORCENTAJE
                        dr[8] = " " + rfila.Cells[10].Text; //VALOR
                        DateTime fecha = DateTime.Now;// Convert.ToDateTime(rfila.Cells[7].Text); //Fecha Emision
                        dr[9] = " " + fecha.ToLongDateString(); //Fecha Emision
                        dr[10] = " " +  rfila.Cells[7].Text; //NOMBRE CUENTA
                        tabDeta.Rows.Add(dr);
                    }
                    else
                    {
                        //LLENANDO TABLA GENERAL 
                        foreach (DataRow fila in tabDeta.Rows)
                        {
                            DataRow datarw;
                            datarw = table.NewRow();
                            datarw[0] = " " + fila["identificacion"];//IDENTIFICACION
                            datarw[1] = " " + fila["nombre"];//NOMBRE
                            datarw[2] = " " + fila["ciudad"];//CIUDAD
                            datarw[3] = " " + fila["direccion"]; // DIRECCION
                            datarw[4] = " " + fila["telefono"];//TELEFONO
                            datarw[5] = " " + fila["email"];//EMAIL                    
                            datarw[6] = " " + fila["base"];//BASE
                            datarw[7] = " " + fila["porcentaje"];//PORCENTAJE
                            datarw[8] = " " + fila["valor"]; //VALOR
                            Cardinalidad numero = new Cardinalidad();
                            string cardinal = " ";
                            if (valorTotal != 0)
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
                            datarw[9] = "" + cardinal; // VALOR EN LETRAS

                            datarw[10] = " " + fila["Fechaexpedicion"];//PORCENTAJE
                            datarw[11] = valorTotal.ToString("N0"); //VALORTOTAL 
                            datarw[12] = " " + fila["nom_cuenta"]; //NOMBRE CUENTA 
                            table.Rows.Add(datarw);
                        }
                        //LIMPIANDO TABLA DETALLE
                        tabDeta.Clear();
                        valorTotal = 0;
                        valorTotal += Convert.ToDecimal(rfila.Cells[10].Text);
                        //LLENANDO EL SIGUIENTE DATO SELECCIONADO
                        DataRow dr;
                        dr = tabDeta.NewRow();
                        dr[0] = " " + rfila.Cells[1].Text;//IDENTIFICACION
                        dr[1] = " " + rfila.Cells[2].Text;//NOMBRE
                        dr[2] = " " + rfila.Cells[3].Text;//CIUDAD
                        dr[3] = " " + rfila.Cells[4].Text; // DIRECCION
                        dr[4] = " " + rfila.Cells[5].Text;//TELEFONO
                        dr[5] = " " + rfila.Cells[6].Text;//EMAIL                    
                        dr[6] = " " + rfila.Cells[8].Text;//BASE
                        dr[7] = " " + rfila.Cells[9].Text;//PORCENTAJE
                        dr[8] = " " + rfila.Cells[10].Text; //VALOR
                        DateTime fecha = DateTime.Now;// Convert.ToDateTime(rfila.Cells[7].Text); //Fecha Emision
                        dr[9] = " " + fecha.ToLongDateString(); //Fecha Emision
                        dr[10] = " " + rfila.Cells[7].Text; //NOMBRE CUENTA
                        tabDeta.Rows.Add(dr);
                    }
                    identificacion = identif_ant;
                    cont++;
                }
            }

            //LLENANDO TABLA GENERAL FINAL
            foreach (DataRow fila in tabDeta.Rows)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = " " + fila["identificacion"];//IDENTIFICACION
                datarw[1] = " " + fila["nombre"];//NOMBRE
                datarw[2] = " " + fila["ciudad"];//CIUDAD
                datarw[3] = " " + fila["direccion"]; // DIRECCION
                datarw[4] = " " + fila["telefono"];//TELEFONO
                datarw[5] = " " + fila["email"];//EMAIL                    
                datarw[6] = " " + fila["base"];//BASE
                datarw[7] = " " + fila["porcentaje"];//PORCENTAJE
                datarw[8] = " " + fila["valor"]; //VALOR
                Cardinalidad numero = new Cardinalidad();
                string cardinal = " ";
                if (valorTotal != 0)
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
                datarw[9] = "" + cardinal; // VALOR EN LETRAS

                datarw[10] = " " + fila["Fechaexpedicion"];//PORCENTAJE
                datarw[11] = valorTotal.ToString("N0"); //VALORTOTAL
                datarw[12] = " " + fila["nom_cuenta"]; //NOMBRE CUENTA 
                table.Rows.Add(datarw);
            }
            //
            

            // ---------------------------------------------------------------------------------------------------------
            // Pasar datos al reporte
            // ---------------------------------------------------------------------------------------------------------

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

            ReportParameter[] param = new ReportParameter[5];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            DateTime fecha1 = Convert.ToDateTime(txtFecIni.Text), fecha2 = Convert.ToDateTime(txtFecFin.Text);
            param[2] = new ReportParameter("year",fecha1.ToLongDateString() +" a "+ fecha2.ToLongDateString());
            param[3] = new ReportParameter("ImagenReport", ImagenReporte());
            param[4] = new ReportParameter("fechaExpedicion", DateTime.Now.ToLongDateString());
            rvRetencion.LocalReport.EnableExternalImages = true;
            //rptRetencionFuente.rdlc
            rvRetencion.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvRetencion.LocalReport.DataSources.Clear();
            rvRetencion.LocalReport.DataSources.Add(rds);
            rvRetencion.LocalReport.Refresh();

            //frmPrint.Visible = false;
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
            rvRetencion.Visible = false;

        }
    }

       
}