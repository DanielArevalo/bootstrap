using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Data;
using Microsoft.Reporting.WebForms;


public partial class Detalle : GlobalWeb
{
    public int ancho = 900;
    Usuario usuario = new Usuario();
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient RecaudosMasivosServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();

    string estadoNom = "";

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarTitulo(OptionsUrl.ExtractoRecaudo, "Inf");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarCancelar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //captura la variable de seccion 
        xpinnWSEstadoCuenta.RecaudosMasivos vRecaudosMasivosR = new xpinnWSEstadoCuenta.RecaudosMasivos();
        vRecaudosMasivosR = (xpinnWSEstadoCuenta.RecaudosMasivos)Session["RvRecaudosMasivos"];
        xpinnWSEstadoCuenta.RecaudosMasivos Estados = new xpinnWSEstadoCuenta.RecaudosMasivos();
        Estados = (xpinnWSEstadoCuenta.RecaudosMasivos)Session["Estadomasi"];
        if (Estados != null)
        {
            estadoNom = Estados.estado;
        }
        try
        {
            if (vRecaudosMasivosR == null)
            {
                ancho = cbDetallado.Checked ? 900 : 900;
                if (!IsPostBack)
                {
                    CargarEmpresaRecaudo();
                    mvAplicar.ActiveViewIndex = 0;
                    if (Session["180103" + ".id"] != null)
                    {
                        idObjeto = Session["180103" + ".id"].ToString();
                        ObtenerDatos(idObjeto);
                    }
                }
                msg.Text = "";
            }
            else
            {
                if (!IsPostBack)
                {

                    Actualizar2();
                }

            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.GetType().Name + "L", "Page_Load", ex);
        }

    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("180103" + "L", "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        if (evt.CommandName == "Editar")
        {
            String[] tmp = evt.CommandArgument.ToString().Split('|');
            xpinnWSEstadoCuenta.RecaudosMasivos ejeMeta = new xpinnWSEstadoCuenta.RecaudosMasivos();
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            ancho = cbDetallado.Checked ? 900 : 900;
            List<xpinnWSEstadoCuenta.RecaudosMasivos> lstConsulta = new List<xpinnWSEstadoCuenta.RecaudosMasivos>();
            lstConsulta = RecaudosMasivosServicio.ListarDetalleRecaudoConsultaExtracto(Convert.ToInt32(txtNumeroLista.Text), estadoNom, cbDetallado.Checked, Session["sec"].ToString());
            gvLista.DataSource = lstConsulta;
            gvLista.DataBind();
            Session["DatosGrilla"] = lstConsulta;
            TotalizarGridView(lstConsulta);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private void Actualizar2(string fecha = "")
    {
        //captura la variable de sesión 
        xpinnWSEstadoCuenta.RecaudosMasivos vRecaudosMasivosR = new xpinnWSEstadoCuenta.RecaudosMasivos();
        vRecaudosMasivosR = (xpinnWSEstadoCuenta.RecaudosMasivos)Session["RvRecaudosMasivos"];
        if (estadoNom != "")
        {
            vRecaudosMasivosR.estado = estadoNom;
        }else
        {
            vRecaudosMasivosR.estado = "2";
        }
        try
        {
            ancho = cbDetallado.Checked ? 900 : 900;
            List<xpinnWSEstadoCuenta.RecaudosMasivos> lstConsulta = new List<xpinnWSEstadoCuenta.RecaudosMasivos>();
            lstConsulta = RecaudosMasivosServicio.ListarDetalleRecaudoConsultaExtractoxPersona(vRecaudosMasivosR, Session["sec"].ToString());
            if (!string.IsNullOrWhiteSpace(fecha))
            {
                lstConsulta = lstConsulta.Where(x => x.fechacreacion.ToString("dd/MM/yyyy") == fecha).ToList();
            }
            gvLista.DataSource = lstConsulta;
            gvLista.DataBind();
            Session["DatosGrilla"] = lstConsulta;
            TotalizarGridView(lstConsulta);
            tabladesc.Visible = false;
            if (!Page.IsPostBack)
            {
                ddlFecha.Items.Add(new ListItem("Seleccione",""));
                foreach (xpinnWSEstadoCuenta.RecaudosMasivos item in lstConsulta)
                {
                    ddlFecha.Items.Add(new ListItem(item.fechacreacion.ToString("dd/MM/yyyy"), item.fechacreacion.ToString("dd/MM/yyyy")));
                }
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void TotalizarGridView(List<xpinnWSEstadoCuenta.RecaudosMasivos> lstInfo)
    {
        decimal totalRev = 0;
        if (lstInfo != null && lstInfo.Count > 0)
        {
            totalRev = lstInfo.Sum(x => x.valor); ;
        }

    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            xpinnWSEstadoCuenta.RecaudosMasivos vRecaudos = new xpinnWSEstadoCuenta.RecaudosMasivos();
            vRecaudos = RecaudosMasivosServicio.ConsultarRecaudo(pIdObjeto, Session["sec"].ToString());

            if (!string.IsNullOrEmpty(vRecaudos.numero_recaudo.ToString()))
                txtNumeroLista.Text = HttpUtility.HtmlDecode(vRecaudos.numero_recaudo.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.fecha_aplicacion.ToString()))
                ucFechaAplicacion.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vRecaudos.fecha_aplicacion.ToString()));
            if (!string.IsNullOrEmpty(vRecaudos.cod_empresa.ToString()))
                ddlEntidad.SelectedValue = HttpUtility.HtmlDecode(vRecaudos.cod_empresa.ToString());
            if (!string.IsNullOrEmpty(vRecaudos.numero_novedad.ToString()))
                txtNumeroNovedad.Text = HttpUtility.HtmlDecode(vRecaudos.numero_novedad.ToString().Trim());
            if (!string.IsNullOrEmpty(vRecaudos.periodo_corte.ToString()))
                txtPeriodo.Text = HttpUtility.HtmlDecode(vRecaudos.periodo_corte.ToString().Trim());
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("180103", "ObtenerDatos", ex);
            // BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta, "ObtenerDatos", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session.Remove("RvRecaudosMasivos");
        Navegar(Pagina.Lista);
    }

    protected void CargarEmpresaRecaudo()
    {
        try
        {
            //Xpinn.Tesoreria.Services.RecaudosMasivosService recaudoServicio = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient recaudoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
            List<xpinnWSEstadoCuenta.EmpresaRecaudo> lstModulo = new List<xpinnWSEstadoCuenta.EmpresaRecaudo>();

            lstModulo = recaudoServicio.ListarEmpresaRecaudo(null, Session["sec"].ToString());

            ddlEntidad.DataSource = lstModulo;
            ddlEntidad.DataTextField = "nom_empresa";
            ddlEntidad.DataValueField = "cod_empresa";
            ddlEntidad.DataBind();

            ddlEntidad.Items.Insert(0, "");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("180103", "CargarEmpresaRecaudo", ex);
            //BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta, "CargarEmpresaRecaudo", ex);
        }
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=AplicacionRecaudosMasivos.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        //  ExpGrilla expGrilla = new ExpGrilla();

        // sw = expGrilla.ObtenerGrilla(GridView1, null);

        // Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvAplicar.ActiveViewIndex = 0;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarCancelar(false);
    }

    protected void btnReporte_Click(object sender, EventArgs e)
    {
        VerError("");

        List<xpinnWSEstadoCuenta.RecaudosMasivos> lstConsulta = new List<xpinnWSEstadoCuenta.RecaudosMasivos>();
        lstConsulta = (List<xpinnWSEstadoCuenta.RecaudosMasivos>)Session["DatosGrilla"];

        if (gvLista.Rows.Count > 0 && lstConsulta.Count > 0)
        {
            //CREACION DE LA TABLA
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("cedula");
            table.Columns.Add("nombre");
            table.Columns.Add("tipo_producto");
            table.Columns.Add("num_producto");
            table.Columns.Add("tipo_aplicacion");
            table.Columns.Add("nro_cuotas");
            table.Columns.Add("valor");
            table.Columns.Add("capital");
            table.Columns.Add("int_cte");
            table.Columns.Add("int_mora");
            table.Columns.Add("seguro");
            table.Columns.Add("ley_mypime");
            table.Columns.Add("ivaley_mypime");
            table.Columns.Add("devolucion");

            //LLENAR LAS TABLAS CON LOS DATOS CORRESPONDIENTES

            foreach (xpinnWSEstadoCuenta.RecaudosMasivos rFila in lstConsulta)
            {
                DataRow dr;
                dr = table.NewRow();
                dr[0] = " " + rFila.identificacion;
                dr[1] = " " + rFila.nombre.ToUpper();
                dr[2] = " " + rFila.tipo_producto.ToUpper();
                dr[3] = " " + rFila.numero_producto;
                dr[4] = " " + rFila.tipo_aplicacion;
                dr[5] = " " + rFila.num_cuotas;
                dr[6] = " " + rFila.valor.ToString("n");
                dr[7] = " " + rFila.capital;
                dr[8] = " " + rFila.intcte;
                dr[9] = " " + rFila.intmora;
                dr[10] = " " + rFila.seguro;
                dr[11] = " " + rFila.leymipyme;
                dr[12] = " " + rFila.ivaleymipyme;
                dr[13] = " " + rFila.devolucion.ToString("n");

                table.Rows.Add(dr);
            }

            //PASAR LOS DATOS AL REPORTE
            xpinnWSLogin.Persona1 pUsuario = new xpinnWSLogin.Persona1();
            pUsuario = (xpinnWSLogin.Persona1)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[3];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nit);
            param[2] = new ReportParameter("fecha", DateTime.Now.ToShortDateString());
            rvReporte.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", table);

            rvReporte.LocalReport.DataSources.Clear();
            rvReporte.LocalReport.DataSources.Add(rds);
            rvReporte.LocalReport.Refresh();

            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(false);

            mvAplicar.ActiveViewIndex = 1;
            rvConsultaRecaudo.Visible = false;
            rvReporte.Visible = true;
            rvConsolidado.Visible = false;
        }
        else
        {
            VerError("No existen Datos");
        }
    }

    protected void btnRptConsolidado_Click(object sender, EventArgs e)
    {
        VerError("");

        if (txtNumeroLista.Text == "")
        {
            VerError("Error al generar el reporte consolidado, Verifique los datos.");
            return;
        }

        List<xpinnWSEstadoCuenta.RecaudosMasivos> lstData = new List<xpinnWSEstadoCuenta.RecaudosMasivos>();
        xpinnWSEstadoCuenta.RecaudosMasivos pEntidad = new xpinnWSEstadoCuenta.RecaudosMasivos();
        pEntidad.numero_recaudo = Convert.ToInt64(txtNumeroLista.Text);

        string pError = "";
        // lstData = RecaudosMasivosServicio.ListarTEMP_Consolidado(pEntidad, ref pError);
        if (pError != "")
        {
            VerError(pError);
            return;
        }

        DataTable table = new DataTable();
        table.Columns.Add("Producto");
        table.Columns.Add("Concepto");
        table.Columns.Add("NombreConc");
        table.Columns.Add("Atributo");
        table.Columns.Add("Oficina");
        table.Columns.Add("Valor");

        if (lstData.Count > 0)
        {
            //LLENANDO EL DATATABLE
            foreach (xpinnWSEstadoCuenta.RecaudosMasivos pData in lstData)
            {
                DataRow dr;
                dr = table.NewRow();
                dr[0] = " " + pData.nom_tipo_producto;
                dr[1] = " " + pData.nombre;
                dr[2] = " " + pData.nombres;
                dr[3] = " " + pData.nom_atr;
                dr[4] = " " + pData.nom_oficina;
                dr[5] = pData.valor.ToString("n0");
                table.Rows.Add(dr);
            }

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[8];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("ImagenReport", ImagenReporte());
            param[3] = new ReportParameter("NroLista", " " + txtNumeroLista.Text);
            param[4] = new ReportParameter("Periodo", " " + txtPeriodo.Text.Substring(0, 10));
            if (ddlEntidad.SelectedItem != null)
                param[5] = new ReportParameter("Pagaduria", " " + ddlEntidad.SelectedItem.Text);
            else
                param[5] = new ReportParameter("Pagaduria", " ");
            param[6] = new ReportParameter("FechaAplicacion", " " + ucFechaAplicacion.Text);
            param[7] = new ReportParameter("nomUsuario", " " + pUsuario.nombre);

            rvConsolidado.LocalReport.DataSources.Clear();
            rvConsolidado.LocalReport.EnableExternalImages = true;
            rvConsolidado.LocalReport.SetParameters(param);
            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvConsolidado.LocalReport.DataSources.Add(rds);
            rvConsolidado.LocalReport.Refresh();

            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(false);

            mvAplicar.ActiveViewIndex = 1;
            rvConsultaRecaudo.Visible = false;
            rvReporte.Visible = false;
            rvConsolidado.Visible = true;
        }
        else
        {
            VerError("No existen Datos");
        }
    }

    protected void btnConciliacion_Click(object sender, EventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0)
        {
            //RECUPERAR DATOS

            List<xpinnWSEstadoCuenta.RecaudosMasivos> lstDetalle = new List<xpinnWSEstadoCuenta.RecaudosMasivos>();
            if (txtNumeroNovedad.Text == "")
                txtNumeroNovedad.Text = "0";
            // lstDetalle = RecaudosMasivosServicio.ListarDetalleReporte(Convert.ToInt32(txtNumeroLista.Text), Convert.ToInt32(txtNumeroNovedad.Text));

            //CREACION DE LA TABLA ENCABEZADO
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("cedula");
            table.Columns.Add("nombre");
            table.Columns.Add("tipo_producto");
            table.Columns.Add("num_producto");
            table.Columns.Add("valor_aplicado");
            table.Columns.Add("valor_novedad");
            table.Columns.Add("diferencia");

            //LLENAR LAS TABLAS CON LOS DATOS CORRESPONDIENTES
            decimal totalpagado = 0, totalCobrado = 0, TotalDiferencia = 0;
            if (lstDetalle.Count > 0)
            {
                foreach (xpinnWSEstadoCuenta.RecaudosMasivos rFila in lstDetalle)
                {
                    DataRow dr;
                    dr = table.NewRow();
                    dr[0] = " " + rFila.identificacion;
                    dr[1] = " " + rFila.nombre;
                    dr[2] = " " + rFila.tipo_producto;
                    dr[3] = " " + rFila.numero_producto;
                    dr[4] = " " + rFila.valor_aplicado.ToString("n");
                    dr[5] = " " + rFila.valor_novedad.ToString("n");
                    dr[6] = " " + (rFila.valor_novedad - rFila.valor_aplicado).ToString("n");
                    totalpagado += rFila.valor_aplicado;
                    totalCobrado += rFila.valor_novedad;
                    TotalDiferencia += rFila.valor_novedad - rFila.valor_aplicado;
                    table.Rows.Add(dr);
                }
            }


            //PASAR LOS DATOS AL REPORTE
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["persona"];

            ReportParameter[] param = new ReportParameter[6];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("fecha", DateTime.Now.ToShortDateString());
            param[3] = new ReportParameter("TotalPagado", totalpagado.ToString("N2"));
            param[4] = new ReportParameter("TotalCobrado", totalCobrado.ToString("N2"));
            param[5] = new ReportParameter("TotalDiferencia", TotalDiferencia.ToString("N2"));
            rvConsultaRecaudo.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", table);

            rvConsultaRecaudo.LocalReport.DataSources.Clear();
            rvConsultaRecaudo.LocalReport.DataSources.Add(rds);
            rvConsultaRecaudo.LocalReport.Refresh();

            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(false);

            mvAplicar.ActiveViewIndex = 1;
            rvConsultaRecaudo.Visible = true;
            rvReporte.Visible = false;
            rvConsolidado.Visible = false;
        }
        else
        {
            VerError("No existen Datos");
        }
    }

    protected void cbDetallado_CheckedChanged(object sender, EventArgs e)
    {
        // Actualizar();
    }

    protected void cbNoGeneradas_CheckedChanged(object sender, EventArgs e)
    {
        if (NomGene.Checked == true)
        {
            estadoNom = "1";
            Actualizar2();
        }
        else
        {
            estadoNom = "2";
            Actualizar2();
        }
    }

    protected void Check_Clicked(object sender, EventArgs e)
    {
        CheckBox chkHeader = sender as CheckBox;

        if (chkHeader.Checked == true)
        {
            if (gvLista.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvLista.Rows)
                {
                    CheckBox CheckBoxgv = row.FindControl("CheckBoxgv") as CheckBox;
                    CheckBoxgv.Checked = true;

                }

            }
        }
        else
        {
            foreach (GridViewRow row in gvLista.Rows)
            {
                CheckBox CheckBoxgv = row.FindControl("CheckBoxgv") as CheckBox;
                CheckBoxgv.Checked = false;

            }
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        int fila = e.NewEditIndex;        
        e.NewEditIndex = -1;

        string identificacion = "";
        string nombre = "";
        string fecha_corte = "";
        string fecha_apli = "";
        string Cod_Nomina = "";
        decimal totalpagado = 0;

        xpinnWSLogin.Persona1 pUsuario = new xpinnWSLogin.Persona1();
        pUsuario = (xpinnWSLogin.Persona1)Session["persona"];
        
        //tabla general 
        DataTable tablegeneral = new DataTable();
        tablegeneral.Columns.Add("nomUsuario");
        tablegeneral.Columns.Add("identificacion");
        tablegeneral.Columns.Add("Nombre");
        tablegeneral.Columns.Add("FechaCorte");
        tablegeneral.Columns.Add("FechaAplicacion");
        tablegeneral.Columns.Add("Cod_Nomina");
        tablegeneral.Columns.Add("Producto");
        tablegeneral.Columns.Add("Num_Producto");
        tablegeneral.Columns.Add("Capital");
        tablegeneral.Columns.Add("Int_cte");
        tablegeneral.Columns.Add("Int_Mora");
        tablegeneral.Columns.Add("Otros");
        tablegeneral.Columns.Add("Saldo_Fnl");
        tablegeneral.Columns.Add("Valor");
        tablegeneral.Columns.Add("TotalPagado");
        tablegeneral.Columns.Add("cont");

        int contp = 0;
        int cont = 0;

        totalpagado = 0;
        xpinnWSEstadoCuenta.RecaudosMasivos pEntidad = new xpinnWSEstadoCuenta.RecaudosMasivos();        
        pEntidad.numero_recaudo = Convert.ToUInt32(gvLista.Rows[fila].Cells[1].Text); //Para obtener el Número de desembolso
        pEntidad.identificacion = gvLista.Rows[fila].Cells[2].Text; ; //Para obtener el # de la identificación

        if (NomGene.Checked == true)
        {
            pEntidad.estado = "1";
        }
        else
        {
            pEntidad.estado = "2";
        }

                    string pError = "";
                    List<xpinnWSEstadoCuenta.RecaudosMasivos> lstData = new List<xpinnWSEstadoCuenta.RecaudosMasivos>();
                    lstData = RecaudosMasivosServicio.ListarDeduccionesxPersona(pEntidad, ref pError, Session["sec"].ToString());

                    //CREACION DE LA TABLA ENCABEZADO
                    DataTable table = new DataTable();
                    table.Columns.Add("Producto");
                    table.Columns.Add("Num_Producto");
                    table.Columns.Add("Capital");
                    table.Columns.Add("Int_cte");
                    table.Columns.Add("Int_Mora");
                    table.Columns.Add("Otros");
                    table.Columns.Add("Saldo_Fnl");
                    table.Columns.Add("Valor");

                    if (lstData.Count > 0)
                    {

                        identificacion = "";
                        nombre = "";
                        fecha_corte = "";
                        fecha_apli = "";
                        Cod_Nomina = "";

                        //LLENANDO EL DATATABLE
                        foreach (xpinnWSEstadoCuenta.RecaudosMasivos pData in lstData)
                        {
                            identificacion = pData.identificacion;
                            nombre = pData.nombre;
                            fecha_corte = Convert.ToString(pData.periodo_corte.Value.ToShortDateString());
                            fecha_apli = Convert.ToString(pData.fecha_aplicacion.Value.ToShortDateString());
                            Cod_Nomina = Convert.ToString(pData.cod_nomina_empleado);

                            DataRow dr;
                            dr = table.NewRow();
                            dr[0] = " " + pData.tipo_producto.ToUpper();
                            dr[1] = " " + pData.numero_producto;
                            dr[2] = " " + pData.capital.ToString("n0");
                            dr[3] = " " + pData.intcte.ToString("n0");
                            dr[4] = " " + pData.intmora.ToString("n0");
                            dr[5] = " " + pData.otros.ToString("n0");
                            dr[6] = " " + pData.valor.ToString("n0");
                            dr[7] = pData.valor_aplicado.ToString("n0");
                            totalpagado += pData.valor_aplicado;
                            table.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        // Si el cliente no tiene productos para el extracto entonces registrar un registro vacio.
                        DataRow datos;
                        datos = table.NewRow();
                        datos[0] = "";
                        datos[1] = "";
                        datos[2] = "";
                        datos[3] = "";
                        datos[4] = "";
                        datos[5] = "";
                        datos[6] = "";
                        datos[7] = "";
                        table.Rows.Add(datos);
                    }

                    cont++;
                    foreach (DataRow rdata in table.Rows)
                    {
                        DataRow datarw;
                        datarw = tablegeneral.NewRow();
                        datarw[0] = pUsuario.nombre;
                        datarw[1] = identificacion;
                        datarw[2] = nombre;
                        datarw[3] = fecha_corte;
                        datarw[4] = fecha_apli;
                        datarw[5] = Cod_Nomina;
                        datarw[6] = rdata[0].ToString();
                        datarw[7] = rdata[1].ToString();
                        datarw[8] = rdata[2].ToString();
                        datarw[9] = rdata[3].ToString();
                        datarw[10] = rdata[4].ToString();
                        datarw[11] = rdata[5].ToString();
                        datarw[12] = rdata[6].ToString();
                        datarw[13] = rdata[7].ToString();
                        datarw[14] = totalpagado;
                        datarw[15] = cont;
                        tablegeneral.Rows.Add(datarw);
        }

        ReportParameter[] param = new ReportParameter[3];
        param[0] = new ReportParameter("entidad", pUsuario.empresa);
        param[1] = new ReportParameter("nit", pUsuario.nit);
        param[2] = new ReportParameter("ImagenReport", ImagenReporte());


        rvConsolidado.LocalReport.EnableExternalImages = true;
        rvConsolidado.LocalReport.SetParameters(param);

        rvConsolidado.LocalReport.DataSources.Clear();
        ReportDataSource rds = new ReportDataSource("DataSet1", tablegeneral);
        rvConsolidado.LocalReport.DataSources.Add(rds);
        rvConsolidado.LocalReport.Refresh();

        Site toolBar = (Site)this.Master;
        // toolBar.MostrarImagenLoading(false);
        mvAplicar.ActiveViewIndex = 1;
        rvConsultaRecaudo.Visible = false;
        rvReporte.Visible = false;
        rvConsolidado.Visible = true;        
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {

        VerError("");
        string identificacion = "";
        string nombre = "";
        string fecha_corte = "";
        string fecha_apli = "";
        string Cod_Nomina = "";
        decimal totalpagado = 0;

        xpinnWSLogin.Persona1 pUsuario = new xpinnWSLogin.Persona1();
        pUsuario = (xpinnWSLogin.Persona1)Session["persona"];

        if (gvLista.Rows.Count > 0)
        {
            //tabla general 
            DataTable tablegeneral = new DataTable();
            tablegeneral.Columns.Add("nomUsuario");
            tablegeneral.Columns.Add("identificacion");
            tablegeneral.Columns.Add("Nombre");
            tablegeneral.Columns.Add("FechaCorte");
            tablegeneral.Columns.Add("FechaAplicacion");
            tablegeneral.Columns.Add("Cod_Nomina");
            tablegeneral.Columns.Add("Producto");
            tablegeneral.Columns.Add("Num_Producto");
            tablegeneral.Columns.Add("Capital");
            tablegeneral.Columns.Add("Int_cte");
            tablegeneral.Columns.Add("Int_Mora");
            tablegeneral.Columns.Add("Otros");
            tablegeneral.Columns.Add("Saldo_Fnl");
            tablegeneral.Columns.Add("Valor");
            tablegeneral.Columns.Add("TotalPagado");
            tablegeneral.Columns.Add("cont");


            int contp = 0;
            int cont = 0;

            foreach (GridViewRow row in gvLista.Rows)
            {
                CheckBox CheckBoxgv = row.FindControl("CheckBoxgv") as CheckBox;
                if (CheckBoxgv != null)
                {
                    if (CheckBoxgv.Checked == true)
                    {
                        totalpagado = 0;
                        xpinnWSEstadoCuenta.RecaudosMasivos pEntidad = new xpinnWSEstadoCuenta.RecaudosMasivos();
                        pEntidad.numero_recaudo = Convert.ToUInt32(gvLista.Rows[contp].Cells[1].Text); //Para obtener el Número de desembolso
                        pEntidad.identificacion = gvLista.Rows[contp].Cells[2].Text; //Para obtener el # de la identificación

                        if (NomGene.Checked == true)
                        {
                            pEntidad.estado = "1";                           
                        }
                        else
                        {
                            pEntidad.estado = "2";                         
                        }                         

                        string pError = "";
                        List<xpinnWSEstadoCuenta.RecaudosMasivos> lstData = new List<xpinnWSEstadoCuenta.RecaudosMasivos>();
                        lstData = RecaudosMasivosServicio.ListarDeduccionesxPersona(pEntidad, ref pError, Session["sec"].ToString());

                        //CREACION DE LA TABLA ENCABEZADO
                        DataTable table = new DataTable();
                        table.Columns.Add("Producto");
                        table.Columns.Add("Num_Producto");
                        table.Columns.Add("Capital");
                        table.Columns.Add("Int_cte");
                        table.Columns.Add("Int_Mora");
                        table.Columns.Add("Otros");
                        table.Columns.Add("Saldo_Fnl");
                        table.Columns.Add("Valor");

                        if (lstData.Count > 0)
                        {

                            identificacion = "";
                            nombre = "";
                            fecha_corte = "";
                            fecha_apli = "";
                            Cod_Nomina = "";

                            //LLENANDO EL DATATABLE
                            foreach (xpinnWSEstadoCuenta.RecaudosMasivos pData in lstData)
                            {
                                identificacion = pData.identificacion;
                                nombre = pData.nombre;
                                fecha_corte = Convert.ToString(pData.periodo_corte.Value.ToShortDateString());
                                fecha_apli = Convert.ToString(pData.fecha_aplicacion.Value.ToShortDateString());
                                Cod_Nomina = Convert.ToString(pData.cod_nomina_empleado);

                                DataRow dr;
                                dr = table.NewRow();
                                dr[0] = " " + pData.tipo_producto;
                                dr[1] = " " + pData.numero_producto;
                                dr[2] = " " + pData.capital.ToString("n0");
                                dr[3] = " " + pData.intcte.ToString("n0");
                                dr[4] = " " + pData.intmora.ToString("n0");
                                dr[5] = " " + pData.otros.ToString("n0");
                                dr[6] = " " + pData.valor.ToString("n0");
                                dr[7] = pData.valor_aplicado.ToString("n0");
                                totalpagado += pData.valor_aplicado;
                                table.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            // Si el cliente no tiene productos para el extracto entonces registrar un registro vacio.
                            DataRow datos;
                            datos = table.NewRow();
                            datos[0] = "";
                            datos[1] = "";
                            datos[2] = "";
                            datos[3] = "";
                            datos[4] = "";
                            datos[5] = "";
                            datos[6] = "";
                            datos[7] = "";
                            table.Rows.Add(datos);
                        }

                        cont++;
                        foreach (DataRow rdata in table.Rows)
                        {
                            DataRow datarw;
                            datarw = tablegeneral.NewRow();
                            datarw[0] = pUsuario.nombre;
                            datarw[1] = identificacion;
                            datarw[2] = nombre;
                            datarw[3] = fecha_corte;
                            datarw[4] = fecha_apli;
                            datarw[5] = Cod_Nomina;
                            datarw[6] = rdata[0].ToString();
                            datarw[7] = rdata[1].ToString();
                            datarw[8] = rdata[2].ToString();
                            datarw[9] = rdata[3].ToString();
                            datarw[10] = rdata[4].ToString();
                            datarw[11] = rdata[5].ToString();
                            datarw[12] = rdata[6].ToString();
                            datarw[13] = rdata[7].ToString();
                            datarw[14] = totalpagado;
                            datarw[15] = cont;
                            tablegeneral.Rows.Add(datarw);
                        }
                    }
                }
                contp++;
            }

            ReportParameter[] param = new ReportParameter[3];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nit);
            param[2] = new ReportParameter("ImagenReport", ImagenReporte());


            rvConsolidado.LocalReport.EnableExternalImages = true;
            rvConsolidado.LocalReport.SetParameters(param);

            rvConsolidado.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet1", tablegeneral);
            rvConsolidado.LocalReport.DataSources.Add(rds);
            rvConsolidado.LocalReport.Refresh();

            Site toolBar = (Site)this.Master;
            // toolBar.MostrarImagenLoading(false);
            toolBar.MostrarCancelar(true);

            mvAplicar.ActiveViewIndex = 1;
            rvConsultaRecaudo.Visible = false;
            rvReporte.Visible = false;
            rvConsolidado.Visible = true;

        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if(ddlFecha.SelectedValue != "")
        {
            Actualizar2(ddlFecha.SelectedValue);
        }else
        {
            Actualizar2("");
        }
    }
}

