using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;
using Microsoft.Reporting.WebForms;
using System.Web;
using System.Globalization;

public partial class Page_Asesores_Colocacion_Lista : GlobalWeb
{

    ExtractoService clienteExtractoServicio = new ExtractoService();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(clienteExtractoServicio.CodigoProgramaCertifAnual, "L");
            Site toolBar = (Site)this.Master;

            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarCancelar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteExtractoServicio.CodigoProgramaCertifAnual, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!Page.IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                _usuario = (Usuario)Session["usuario"];
                LlenarCombos();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteExtractoServicio.CodigoProgramaCertifAnual, "Page_Load", ex);
        }
    }
    protected void LlenarCombos()
    {
        
        // Llenar el DDL de la fecha de corte 
        Configuracion conf = new Configuracion();
        List<Xpinn.Contabilidad.Entities.BalanceGeneral> lstFechaCierre = new List<Xpinn.Contabilidad.Entities.BalanceGeneral>();
       
        Xpinn.Contabilidad.Services.BalanceGeneralService BalancePruebaService = new Xpinn.Contabilidad.Services.BalanceGeneralService();
        Xpinn.Contabilidad.Entities.BalanceGeneral BalancePrueba = new Xpinn.Contabilidad.Entities.BalanceGeneral();
        lstFechaCierre = BalancePruebaService.ListarFechaCorte(_usuario);

        /*List<Xpinn.Contabilidad.Entities.BalanceGeneral> lstFechaCierreFinal = new List<Xpinn.Contabilidad.Entities.BalanceGeneral>();

        foreach (Xpinn.Contabilidad.Entities.BalanceGeneral rEntidad in lstFechaCierre)
        {
            Xpinn.Contabilidad.Entities.BalanceGeneral nfecha = new Xpinn.Contabilidad.Entities.BalanceGeneral();
            if (rEntidad.fecha != null)
            {
                nfecha = rEntidad;
                DateTime fecha = nfecha.fecha.Value;
                DateTime mes = Convert.ToDateTime(fecha.Month);
                String mesdiciembre = Convert.ToString(mes);
                if (mesdiciembre == "12")
                {
                    lstFechaCierreFinal.Add(nfecha);
                }
            }
        }
        */
    

        ddlFechaCorte.DataSource = lstFechaCierre;
        ddlFechaCorte.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataBind();

    }


    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (ValidarDatos())
        {
            Actualizar();
            if (gvListaClientesExtracto.Rows.Count > 0)
            {
                mvPrincipal.ActiveViewIndex = 1;
                Site toolBar = (Site)Master;
                toolBar.MostrarCancelar(true);
                toolBar.MostrarLimpiar(false);
                toolBar.MostrarConsultar(false);
            }
        }
    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Site toolBar = (Site)Master;
        if (mvPrincipal.ActiveViewIndex == 1)
        {
            mvPrincipal.ActiveViewIndex = 0;

            toolBar.MostrarCancelar(false);
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarConsultar(true);
        }
        else if (mvPrincipal.ActiveViewIndex == 2)
        {
            mvPrincipal.ActiveViewIndex = 1;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarConsultar(false);
        }
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarControles();
        LimpiarPanel(pConsulta);
    }
    public string MonthName(int month)
    {
        DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", true).DateTimeFormat;
        return dtinfo.GetMonthName(month);
    }

    protected void gvListaClientesExtracto_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //vCreditoSeleccionado codigo seleccionado NUMERO CREDITO

        if (ValidarDatos())
        {
            StringHelper stringHelper = new StringHelper();
            GridViewRow rFila = gvListaClientesExtracto.Rows[e.NewEditIndex];
            DateTime fechaCorte = Convert.ToDateTime(ddlFechaCorte.SelectedValue);
            e.NewEditIndex = -1;

            int cod_Persona = Convert.ToInt32(rFila.Cells[1].Text);
            string identificacion = rFila.Cells[2].Text;
            string nombres = rFila.Cells[4].Text;

            // Traer información de productos del cliente desde la base de datos
            Extracto extracto = clienteExtractoServicio.BuscarExtractoAnualPersona(cod_Persona, fechaCorte, Usuario);

            // Tabla de datos de los productos del cliente
            DataTable table = new DataTable();
            table.Columns.Add("TipoProducto");
            table.Columns.Add("NombreLinea");
            table.Columns.Add("SaldoInicial");
            table.Columns.Add("Movimientos");
            table.Columns.Add("Intereses");
            table.Columns.Add("RetencionPracticada");
            table.Columns.Add("InteresPago");
            table.Columns.Add("OtrosPago");
            table.Columns.Add("Abonos");
            table.Columns.Add("SaldoFinal");
            table.Columns.Add("MovimientosC");
            table.Columns.Add("MovimientosD");
            table.Columns.Add("numero_radicacion");

            /* if (extracto == null)
             {
                 VerError("No se pudo obtener la informacion de extracto de un registro, identificacion: " + identificacion);
                 return;
             }
            */

            DataTable tableAporte = tableAporte = CreateDataRow(extracto.lista_extracto_aportes.OrderBy(x=> x.numero_radicacion).ToList());
            DataTable tableAhorro = tableAhorro = CreateDataRow(extracto.lista_extracto_ahorros.OrderBy(x => x.numero_radicacion).ToList());
            DataTable tableCreditos = tableCreditos = CreateDataRow(extracto.lista_extracto_creditos.OrderBy(x => x.numero_radicacion).ToList());
            DataTable tableCDAT = tableCDAT = CreateDataRow(extracto.lista_extracto_cdats.OrderBy(x => x.numero_radicacion).ToList());
            DataTable tableProgramado = tableProgramado = CreateDataRow(extracto.lista_extracto_programado.OrderBy(x => x.numero_radicacion).ToList());

            Xpinn.Seguridad.Entities.Proceso vProceso = new Xpinn.Seguridad.Entities.Proceso();
            Xpinn.Seguridad.Services.ProcesoService ProcesoServicio = new Xpinn.Seguridad.Services.ProcesoService();
            vProceso = ProcesoServicio.ConsultarProceso(2202, (Usuario)Session["usuario"]);

            Usuario pUsu = Usuario;
            rvExtracto.LocalReport.DataSources.Clear();

            DateTime fecha = Convert.ToDateTime(ddlFechaCorte.SelectedValue);          
            String mes = MonthName(fecha.Month).ToUpper();
            
            ReportParameter[] param = new ReportParameter[14]
            {
                new ReportParameter("NombreCliente", HttpUtility.HtmlDecode(nombres)),
                new ReportParameter("Identificacion", identificacion),
                new ReportParameter("NombreEntidad", pUsu.empresa),
                new ReportParameter("NITEmpresa", pUsu.nitempresa),
                new ReportParameter("YearPeriodo", fechaCorte.Year.ToString()),
                new ReportParameter("RutaImagen", new Uri(Server.MapPath("~/Images/LogoEmpresa.jpg")).AbsoluteUri),
                new ReportParameter("FechaGeneracion", DateTime.Today.ToShortDateString()),
                new ReportParameter("ReportAporte", extracto.lista_extracto_aportes != null && extracto.lista_extracto_aportes.Count > 0 ? "False" : "True"),
                new ReportParameter("ReportCredito", extracto.lista_extracto_creditos != null && extracto.lista_extracto_creditos.Count > 0 ? "False" : "True"),
                new ReportParameter("ReportAhorro",  extracto.lista_extracto_ahorros != null && extracto.lista_extracto_ahorros.Count > 0 ? "False" : "True"),
                new ReportParameter("ReportProgramado", extracto.lista_extracto_programado != null && extracto.lista_extracto_programado.Count > 0 ? "False" : "True"),
                new ReportParameter("ReportCDAT", extracto.lista_extracto_cdats != null && extracto.lista_extracto_cdats.Count > 0 ? "False" : "True"),
                new ReportParameter("NombreAhorro", vProceso.nombre),
                new ReportParameter("Mes", mes)/*,
                new ReportParameter("Revalorizacion", extracto.revalorizacion.ToString())*/
            };

            rvExtracto.LocalReport.EnableExternalImages = true;
            rvExtracto.LocalReport.SetParameters(param);

            if (tableCreditos != null)
            {
                ReportDataSource rds1 = new ReportDataSource("DataSetCreditos", tableCreditos);
                rvExtracto.LocalReport.DataSources.Add(rds1);
            }

            if (tableAporte != null)
            {
                ReportDataSource rds2 = new ReportDataSource("DataSetAportes", tableAporte);
                rvExtracto.LocalReport.DataSources.Add(rds2);
            }

            if (tableAhorro != null)
            {
                ReportDataSource rds3 = new ReportDataSource("DataSetAhorrosVista", tableAhorro);
                rvExtracto.LocalReport.DataSources.Add(rds3);
            }

            //Llenar DataSet de CDATS

            if (tableCDAT != null)
            {
                ReportDataSource rds4 = new ReportDataSource("DataSetCdats", tableCDAT);
                rvExtracto.LocalReport.DataSources.Add(rds4);
            }

            if (tableProgramado != null)
            {
                ReportDataSource rds5 = new ReportDataSource("DataSetAhorrosProgramado", tableProgramado);
                rvExtracto.LocalReport.DataSources.Add(rds5);
            }

            rvExtracto.LocalReport.Refresh();

            mvPrincipal.SetActiveView(vReporteExtracto);
            Site toolBar = (Site)Master;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarConsultar(false);
        }
    }

    protected void LimpiarControles()
    {
        mvPrincipal.ActiveViewIndex = 0;
        ddlFechaCorte.SelectedValue = "";
        txtFechaDetaIni.Text = "";
        txtFechaDetaFin.Text = "";

        gvListaClientesExtracto.DataSource = null;
        gvListaClientesExtracto.DataBind();
    }

    String sTipo = "EAN128";
    private DataTable CreateDataRow(List<Extracto> extracto)
    {
        string separadorDecimal = System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
        Configuracion conf = new Configuracion();
        StringHelper stringHelper = new StringHelper();
        DataTable table = new DataTable();
        table.Columns.Add("NombreLinea");
        table.Columns.Add("SaldoInicial");
        table.Columns.Add("Movimientos");       
        table.Columns.Add("Intereses");
        table.Columns.Add("RetencionPracticada");
        table.Columns.Add("InteresPago");
        table.Columns.Add("OtrosPago");
        table.Columns.Add("Abonos");
        table.Columns.Add("SaldoFinal");
        table.Columns.Add("MovimientosC");
        table.Columns.Add("MovimientosD");
        table.Columns.Add("numero_radicacion");

        foreach (Extracto fila in extracto)
        {
            DataRow datos = table.NewRow();

          
            datos[0] = fila.nom_linea;
            datos[1] = fila.saldo_inicial.ToString().Replace(gSeparadorDecimal, separadorDecimal);
            datos[2] = fila.movimiento.ToString().Replace(gSeparadorDecimal, separadorDecimal);
            datos[3] = fila.interes_corriente.ToString().Replace(gSeparadorDecimal, separadorDecimal);
            datos[4] = fila.retencion.ToString().Replace(gSeparadorDecimal, separadorDecimal);
              Decimal Totalinteres = (fila.interes_corriente + fila.interes_mora);
            datos[5] = Totalinteres.ToString().Replace(gSeparadorDecimal, separadorDecimal);
            datos[6] = fila.otros_pagos.ToString().Replace(gSeparadorDecimal, separadorDecimal);
            datos[7] = fila.saldo_pagado.ToString().Replace(gSeparadorDecimal, separadorDecimal);
            datos[8] = fila.saldo_final.ToString().Replace(gSeparadorDecimal, separadorDecimal);
            datos[9] = fila.movimientoC.ToString().Replace(gSeparadorDecimal, separadorDecimal);
            datos[10] = fila.movimientoD.ToString().Replace(gSeparadorDecimal, separadorDecimal);
            datos[11] = fila.numero_radicacion;

            table.Rows.Add(datos);
        }

        if (table.Rows.Count == 0)
        {
            CreateEmptyDataRow(table);
        }

        return table;
    }

    void CreateEmptyDataRow(DataTable table)
    {
        DataRow datos = table.NewRow();

        datos[0] = "";
        datos[1] = "0";
        datos[2] = "0";
        datos[3] = "0";
        datos[4] = "0";
        datos[5] = "0";
        datos[6] = "0";
        datos[7] = "0";
        datos[8] = "0";
        datos[9] = "0";
        datos[10] = "0";
        datos[11] = "0";
        table.Rows.Add(datos);
    }

    private string obtFiltro()
    {
        String filtro = " and (perafi.cod_persona Is Not Null OR per.empleado_entidad = 1) ";

        if (!string.IsNullOrWhiteSpace(txtCodigoDesde.Text))
        {
            if (!string.IsNullOrWhiteSpace(txtCodigoDesde.Text) && !string.IsNullOrWhiteSpace(txtCodigoHasta.Text))
                filtro += " and per.cod_Persona between " + txtCodigoDesde.Text.Trim() + " and " + txtCodigoHasta.Text.Trim();
            else
                filtro += " and per.cod_Persona >= " + txtCodigoDesde.Text.Trim();
        }
        else if (!string.IsNullOrWhiteSpace(txtCodigoHasta.Text))
        {
            filtro += " and per.cod_Persona <= " + txtCodigoHasta.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtIdentDesde.Text))
        {
            if (!string.IsNullOrWhiteSpace(txtIdentDesde.Text) && !string.IsNullOrWhiteSpace(txtIdentHasta.Text))
                filtro += " and per.identificacion between '" + txtIdentDesde.Text.Trim() + "' and '" + txtIdentHasta.Text.Trim() + "'";
            else
                filtro += " and per.Identificacion >= '" + txtIdentDesde.Text.Trim() + "' ";
        }
        else if (!string.IsNullOrWhiteSpace(txtIdentHasta.Text))
        {
            filtro += " and per.identificacion <= '" + txtIdentHasta.Text.Trim() + "' ";
        }

        if (!string.IsNullOrWhiteSpace(txtFechaDetaIni.Text))
        {
            if (!string.IsNullOrWhiteSpace(txtIdentDesde.Text) && !string.IsNullOrWhiteSpace(txtIdentHasta.Text))
                filtro += " and TRUNC(perafi.FECHA_AFILIACION) BETWEEN to_date('" + Convert.ToDateTime(txtFechaDetaIni.Text).ToShortDateString() + "', 'dd/MM/yyyy') and to_date('" + Convert.ToDateTime(txtFechaDetaFin.Text).ToShortDateString() + "', 'dd/MM/yyyy')";
            else
                filtro += " and TRUNC(perafi.FECHA_AFILIACION) = to_date('" + Convert.ToDateTime(txtFechaDetaIni.Text).ToShortDateString() + "', 'dd/MM/yyyy')";
        }
        else if (!string.IsNullOrWhiteSpace(txtFechaDetaFin.Text))
        {
            filtro += " and TRUNC(perafi.FECHA_AFILIACION) = to_date('" + Convert.ToDateTime(txtFechaDetaFin.Text).ToShortDateString() + "', 'dd/MM/yyyy')";
        }

        if (!string.IsNullOrWhiteSpace(txtNombres.Text))
            filtro += " and per.primer_nombre like '%" + txtNombres.Text.Trim() + "%'";

        if (!string.IsNullOrWhiteSpace(txtApellidos.Text))
            filtro += " and per.primer_apellido like '%" + txtApellidos.Text.Trim() + "%'";

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            StringHelper stringHelper = new StringHelper();
            filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);
        }

        return filtro;
    }



    private void Actualizar()
    {
        try
        {
            VerError("");
            String emptyQuery = "Fila de datos vacia";

            String filtro = obtFiltro();

            List<Xpinn.Reporteador.Entities.Persona1Ext> lstPersonasAfiliadas = clienteExtractoServicio.ConsultarPersonasAfiliadasExt(filtro, Usuario);

            gvListaClientesExtracto.EmptyDataText = emptyQuery;
            gvListaClientesExtracto.DataSource = lstPersonasAfiliadas;
            if (lstPersonasAfiliadas.Count > 0)
            {
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstPersonasAfiliadas.Count.ToString();
                gvListaClientesExtracto.DataBind();
            }
            else
            {
                VerError("Su consulta no obtuvo ningun resultado.");
                lblTotalRegs.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteExtractoServicio.CodigoProgramaCertifAnual, "Actualizar", ex);
        }
    }


    Boolean ValidarDatos()
    {
        VerError("");

        if (string.IsNullOrWhiteSpace(ddlFechaCorte.SelectedValue))
        {
            VerError("Ingrese la Fecha de Corte");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(txtFechaDetaIni.Text) && !string.IsNullOrWhiteSpace(txtFechaDetaFin.Text))
        {
            if (Convert.ToDateTime(txtFechaDetaIni.Text) > Convert.ToDateTime(txtFechaDetaFin.Text))
            {
                VerError("El Rango de Fechas fue ingresadas de forma erronea. (Detalle de Pago)");
                return false;
            }
        }

        if (!string.IsNullOrWhiteSpace(txtCodigoDesde.Text) && !string.IsNullOrWhiteSpace(txtCodigoHasta.Text))
        {
            if (Convert.ToInt32(txtCodigoDesde.Text) > Convert.ToInt32(txtCodigoHasta.Text))
            {
                VerError("El código Inicial debe ser menor que el código Final");
                return false;
            }
        }

        return true;
    }


    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEncabezado = (CheckBox)sender;
        if (cbSeleccionarEncabezado != null)
        {
            foreach (GridViewRow rFila in gvListaClientesExtracto.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;
            }
        }
    }


    protected void gvListaClientesExtracto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListaClientesExtracto.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteExtractoServicio.CodigoProgramaCertifAnual, "gvLista_PageIndexChanging", ex);
        }
    }
}