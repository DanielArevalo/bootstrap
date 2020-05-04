using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Page_Asesores_Colocacion_Lista : GlobalWeb
{

    ExtractoService clienteExtractoServicio = new ExtractoService();
    ExcelService excelServicio = new ExcelService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(clienteExtractoServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;

            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarImprimir(false);
            toolBar.MostrarCancelar(false);
        }        
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteExtractoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;                
                CargaDropDown();
                txtfechaCorte.Text = DateTime.Now.ToShortDateString();
                txtfechaPago.Text = DateTime.Now.ToShortDateString();                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteExtractoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void CargaDropDown()
    {
        PoblarLista("Ciudades", ddlCiudad);

        PersonaEmpresaRecaudoServices EmpresaRecaudoService = new PersonaEmpresaRecaudoServices();
        List<PersonaEmpresaRecaudo> lstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
        lstEmpresaRecaudo = EmpresaRecaudoService.ListarEmpresaRecaudo(false, (Usuario)Session["usuario"]);
        if (lstEmpresaRecaudo.Count > 0)
        {
            ddlEmpresa.DataSource = lstEmpresaRecaudo;
            ddlEmpresa.DataTextField = "descripcion";
            ddlEmpresa.DataValueField = "cod_empresa";
            ddlEmpresa.AppendDataBoundItems = true;
            ddlEmpresa.Items.Insert(0,new ListItem("Seleccione un item","0"));
            ddlEmpresa.SelectedIndex = 0;
            ddlEmpresa.DataBind();
        }
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
            String filtro;
            List<ClienteExtracto> lstClientesExtracto = new List<ClienteExtracto>();
            filtro = obtFiltro();
            Actualizar();
            if (gvListaClientesExtracto.Rows.Count > 0)
            {
                mvPrincipal.ActiveViewIndex = 1;
                Site toolBar = (Site)Master;
                toolBar.MostrarCancelar(true);
                toolBar.MostrarExportar(true);
                toolBar.MostrarImprimir(true);
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
            toolBar.MostrarExportar(false);
            toolBar.MostrarImprimir(false);
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarConsultar(true);
        }
        else if (mvPrincipal.ActiveViewIndex == 2)
        {
            mvPrincipal.ActiveViewIndex = 1;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarExportar(true);
            toolBar.MostrarImprimir(true);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarConsultar(false);
        }
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarControles();
        LimpiarPanel(pConsulta);
    }


    protected void LimpiarControles()
    {
        mvPrincipal.ActiveViewIndex = 0;
        txtfechaCorte.Text = "";
        txtfechaPago.Text = "";
        txtFechaDetaIni.Text = "";
        txtFechaDetaFin.Text = "";
        txtFecVencAporteIni.Text = "";
        txtFecVencAporteFin.Text = "";
        txtFecVencCredIni.Text = "";
        txtFecVencCredFin.Text = "";
                
        gvListaClientesExtracto.DataSource = null;
        gvListaClientesExtracto.DataBind();
    }


    String sTipo = "EAN128";
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        //vCreditoSeleccionado codigo seleccionado NUMERO CREDITO

        if (ValidarDatos())
        {
            //tabla general 
            DataTable tablegeneral = new DataTable();
            tablegeneral.Columns.Add("codigo");
            tablegeneral.Columns.Add("identificacion");
            tablegeneral.Columns.Add("nombre");
            tablegeneral.Columns.Add("ciudad");
            tablegeneral.Columns.Add("direccion");
            tablegeneral.Columns.Add("fecha_corte");
            tablegeneral.Columns.Add("fecha_pago");
            tablegeneral.Columns.Add("vr_total");
            tablegeneral.Columns.Add("tipo_producto");
            tablegeneral.Columns.Add("numero");
            tablegeneral.Columns.Add("linea");
            tablegeneral.Columns.Add("fec_prox_pago");
            tablegeneral.Columns.Add("vr_apagar");
            tablegeneral.Columns.Add("rutaImagen");
            tablegeneral.Columns.Add("iddetalle");
            tablegeneral.Columns.Add("idextracto");

            int contDetalle = 0;

            foreach (GridViewRow rFila in gvListaClientesExtracto.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                if (cbSeleccionar != null)
                {
                    if (cbSeleccionar.Checked == true)
                    {
                        // Cargar los datos de la persona
                        int varCod_Persona = Convert.ToInt32(rFila.Cells[1].Text);  //NUMERO DE CREDITO Seleccionado
                        string identificacion = Convert.ToString(rFila.Cells[2].Text);
                        string NombreCompleto, nombres, apellidos;
                        nombres = rFila.Cells[4].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[4].Text) : "";
                        apellidos = rFila.Cells[5].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[5].Text) : "";
                        NombreCompleto = (nombres + ' ' + apellidos).Trim();
                        string ciudad = rFila.Cells[6].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[6].Text) : "";
                        string direccion = rFila.Cells[7].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[7].Text) : "";
                        decimal totalaPagar = 0, totalaporte, totalcredito;
                        totalaporte = rFila.Cells[11].Text != "&nbsp;" ? Convert.ToDecimal(rFila.Cells[11].Text) : 0;
                        totalcredito = rFila.Cells[13].Text != "&nbsp;" ? Convert.ToDecimal(rFila.Cells[13].Text) : 0;

                        // Traer información de productos del cliente desde la base de datos
                        List<Extracto> lstExtractos = new List<Extracto>();
                        lstExtractos = clienteExtractoServicio.ListarDetalleExtracto(varCod_Persona,Convert.ToDateTime(txtfechaCorte.Text), (Usuario)Session["usuario"]);

                        // Ruta para el logo del extracto
                        string cRutaDeImagen;
                        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";

                        // Tabla de datos de los productos del cliente
                        DataTable table = new DataTable();
                        table.Columns.Add("tipo_producto");
                        table.Columns.Add("numero");
                        table.Columns.Add("linea");
                        table.Columns.Add("fec_prox_pago");
                        table.Columns.Add("vr_apagar");
                        if (lstExtractos.Count > 0)
                        {
                            // Cargar los datos del extracto
                            foreach (Extracto fila in lstExtractos)
                            {
                                DataRow datarw;
                                datarw = table.NewRow();
                                datarw[0] = fila.tipo_producto;
                                datarw[1] = fila.numero_radicacion;
                                datarw[2] = fila.linea;
                                datarw[3] = fila.fec_prox_pago.ToString(gFormatoFecha);
                                datarw[4] = (fila.vr_totalPagar_aporte);
                                totalaPagar += Convert.ToDecimal(fila.vr_apagar);
                                table.Rows.Add(datarw);
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
                            table.Rows.Add(datos);                        
                        }

                        // LLena el datatable general. Se hace tres veces por las tres copias del extracto.
                        for (int x = 0; x < 3; x++)
                        {
                            contDetalle++;
                            foreach (DataRow rData in table.Rows)
                            {
                                DataRow datarw;
                                datarw = tablegeneral.NewRow();
                                datarw[0] = varCod_Persona;
                                datarw[1] = identificacion;
                                datarw[2] = NombreCompleto;
                                datarw[3] = ciudad;
                                datarw[4] = direccion;
                                datarw[5] = Convert.ToDateTime(txtfechaCorte.Text).ToString(gFormatoFecha);
                                datarw[6] = Convert.ToDateTime(txtfechaPago.Text).ToString(gFormatoFecha);
                                datarw[7] = (totalaPagar).ToString("n0");
                                datarw[8] = rData[0].ToString();
                                datarw[9] = rData[1].ToString();
                                datarw[10] = rData[2].ToString();
                                datarw[11] = rData[3].ToString() != "" ? Convert.ToDateTime(rData[3].ToString()).ToString(gFormatoFecha) : DBNull.Value.ToString();
                                datarw[12] = rData[4].ToString();
                                datarw[13] = cRutaDeImagen;
                                datarw[14] = contDetalle;
                                datarw[15] = 1;
                                tablegeneral.Rows.Add(datarw);
                            }
                        }                       
                    }
                }
            }

            Usuario pUsu = (Usuario)Session["usuario"];
            rvExtracto.LocalReport.DataSources.Clear();
            ReportParameter[] param = new ReportParameter[5];
            param[0] = new ReportParameter("entidad", pUsu.empresa);
            param[1] = new ReportParameter("nit", pUsu.nitempresa);
            param[2] = new ReportParameter("fecha_corte", Convert.ToDateTime(txtfechaCorte.Text).ToShortDateString());
            param[3] = new ReportParameter("fecha_pago", Convert.ToDateTime(txtfechaPago.Text).ToShortDateString());
            param[4] = new ReportParameter("observaciones", txtObservaciones.Text);

            rvExtracto.LocalReport.EnableExternalImages = true;
            rvExtracto.LocalReport.SetParameters(param);

            ReportDataSource rds1 = new ReportDataSource("DataSet1", tablegeneral);
            rvExtracto.LocalReport.DataSources.Add(rds1);
            rvExtracto.LocalReport.Refresh();

            if (tablegeneral.Rows.Count > 0)
            {
                mvPrincipal.ActiveViewIndex = 2;
                Site toolBar = (Site)Master;
                toolBar.MostrarCancelar(true);
                toolBar.MostrarExportar(false);
                toolBar.MostrarImprimir(false);
                toolBar.MostrarLimpiar(false);
                toolBar.MostrarConsultar(false);
            }
        }
    }

    
    private string obtFiltro()
    {
        String filtro = String.Empty;

        if (txtCodigoDesde.Text != "")
            if (txtCodigoDesde.Text != "" && txtCodigoHasta.Text != "")
                filtro += " and V.Cod_Persona between " + txtCodigoDesde.Text + " and " + txtCodigoHasta.Text;
            else
                filtro += " and V.Cod_Persona  = " + txtCodigoDesde.Text;
        else if (txtCodigoHasta.Text != "")
            filtro += " and V.Cod_Persona  = " + txtCodigoHasta.Text;

        if(txtIdentDesde.Text != "")
            if (txtIdentDesde.Text != "" && txtIdentHasta.Text != "")
                filtro += " and V.Identificacion between '" + txtIdentDesde.Text + "' and '" + txtIdentHasta.Text+"'";
            else
                filtro += " and V.Identificacion = '" + txtIdentDesde.Text + "' ";
        else if (txtIdentHasta.Text != "")
            filtro += " and V.Identificacion <= '" + txtIdentHasta.Text + "' ";

        if(txtNombres.Text != "")
            filtro += " and V.Nombres like '%"+txtNombres.Text+"%'";

        if (txtApellidos.Text != "")
            filtro += " and V.Apellidos like '%" + txtApellidos.Text + "%'";

        if (ddlCiudad.SelectedIndex != 0)
            filtro += " and V.Codciudadresidencia = " + ddlCiudad.SelectedValue;

        if (txtNumAporteIni.Text != "")
            if (txtNumAporteIni.Text != "" && txtNumAporteFin.Text != "")
                filtro += " and P.Numero_Radicacion between " + txtNumAporteIni.Text + " and " + txtNumAporteFin.Text + " and Codtipoproducto = 1";
            else
                filtro += " and P.Numero_Radicacion  = " + txtNumAporteIni.Text + " and Codtipoproducto = 1";
        else if (txtNumAporteFin.Text != "")
            filtro += " and P.Numero_Radicacion  = " + txtNumAporteFin.Text + " and Codtipoproducto = 1";

        if (txtNumCredIni.Text != "")
            if (txtNumCredIni.Text != "" && txtNumCredFin.Text != "")
                filtro += " and P.Numero_Radicacion between " + txtNumCredIni.Text + " and " + txtNumCredFin.Text + " and Codtipoproducto = 2";
            else
                filtro += " and P.Numero_Radicacion  = " + txtNumCredIni.Text + " and Codtipoproducto = 2";
        else if (txtNumCredFin.Text != "")
            filtro += " and P.Numero_Radicacion  = " + txtNumCredFin.Text + " and Codtipoproducto = 2";

        return filtro;
    }



    private void Actualizar()
    {
        try
        {
            VerError("");
            String emptyQuery = "Fila de datos vacia";
            
            String filtro;            
            filtro = obtFiltro();
            DateTime pFechaCorte, pFechaPago, pFecDetaPagoIni, pFecDetaPagoFin, pFecVenAporIni, pFecVenAporFin, pFecVenCredIni, pFecVenCredFin;
            pFechaCorte = txtfechaCorte.ToDateTime == null ? DateTime.MinValue : txtfechaCorte.ToDateTime;
            pFechaPago = txtfechaPago.ToDateTime == null ? DateTime.MinValue : txtfechaPago.ToDateTime;
            pFecDetaPagoIni = txtFechaDetaIni.ToDateTime == null ? DateTime.MinValue : txtFechaDetaIni.ToDateTime;
            pFecDetaPagoFin = txtFechaDetaFin.ToDateTime == null ? DateTime.MinValue : txtFechaDetaFin.ToDateTime;
            pFecVenAporIni = txtFecVencAporteIni.ToDateTime == null ? DateTime.MinValue : txtFecVencAporteIni.ToDateTime;
            pFecVenAporFin = txtFecVencAporteFin.ToDateTime == null ? DateTime.MinValue : txtFecVencAporteFin.ToDateTime;
            pFecVenCredIni = txtFecVencCredIni.ToDateTime == null ? DateTime.MinValue : txtFecVencCredIni.ToDateTime;
            pFecVenCredFin = txtFecVencCredFin.ToDateTime == null ? DateTime.MinValue : txtFecVencCredFin.ToDateTime;

            List<Extracto> lstClientesExtracto = new List<Extracto>();
            
            lstClientesExtracto = clienteExtractoServicio.ListarExtracto(filtro, pFechaCorte, pFechaPago, pFecDetaPagoIni, pFecDetaPagoFin, 
                                     pFecVenAporIni, pFecVenAporFin, pFecVenCredIni, pFecVenCredFin, (Usuario)Session["usuario"]);
           
            gvListaClientesExtracto.EmptyDataText = emptyQuery;
            gvListaClientesExtracto.DataSource = lstClientesExtracto;
            if (lstClientesExtracto.Count > 0)
            {               
                lblTotalRegs.Visible = true;                  
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstClientesExtracto.Count.ToString();
                gvListaClientesExtracto.DataBind();
                Session["DTEXTRACTOS"] = lstClientesExtracto;
            }
            else
            {
                VerError("Su consulta no obtuvo ningun resultado.");
                lblTotalRegs.Visible = false;
                Session["DTEXTRACTOS"] = null;
            }
            Session.Add(clienteExtractoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteExtractoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }
    
   
    Boolean ValidarDatos()
    {
        VerError("");

        if (txtfechaCorte.Text == "")
        {
            VerError ("Ingrese la Fecha de Corte");
            return false;
        }
        if (txtfechaPago.Text == "")
        {
            VerError("Ingrese la Fecha de Pago");
            return false;
        }

        if (txtFechaDetaIni.Text != "" && txtFechaDetaFin.Text != "")
            if (Convert.ToDateTime(txtFechaDetaIni.Text) > Convert.ToDateTime(txtFechaDetaFin.Text))
            {
                VerError("El Rango de Fechas fue ingresadas de forma erronea. (Detalle de Pago)");
                return false;
            }

        if (txtCodigoDesde.Text != "" && txtCodigoHasta.Text != "")
            if (Convert.ToInt32(txtCodigoDesde.Text) > Convert.ToInt32(txtCodigoHasta.Text))
            {
                VerError("El código Inicial debe ser menor que el código Final");
                return false;
            }

        if(txtNumAporteIni.Text != "" && txtNumAporteFin.Text != "")
            if (Convert.ToInt32(txtNumAporteIni.Text) > Convert.ToInt32(txtNumAporteFin.Text))
            {
                VerError("El número de Aporte Inicial debe ser menor que el número Final");
                return false;
            }

        if(txtNumCredIni.Text != "" && txtNumCredFin.Text != "")
            if (Convert.ToInt32(txtNumCredIni.Text) > Convert.ToInt32(txtNumCredFin.Text))
            {
                VerError("El número de Crédito Inicial debe ser menor que el número de Crédito Final");
                return false;
            }

        if (txtFecVencAporteIni.Text != "" && txtFecVencAporteFin.Text != "")
            if (Convert.ToDateTime(txtFecVencAporteIni.Text) > Convert.ToDateTime(txtFecVencAporteFin.Text))
            {
                VerError("El Rango de Fechas fue ingresadas de forma erronea. (Fecha de vencimiento Aporte)");
                return false;
            }

        if (txtFecVencCredIni.Text != "" && txtFecVencCredFin.Text != "")
            if (Convert.ToDateTime(txtFecVencCredIni.Text) > Convert.ToDateTime(txtFecVencCredFin.Text))
            {
                VerError("El Rango de Fechas fue ingresadas de forma erronea. (Fecha de vencimiento Crédito)");
                return false;
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
            BOexcepcion.Throw(clienteExtractoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (Session["DTEXTRACTOS"] != null && gvListaClientesExtracto.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvListaClientesExtracto.AllowPaging = false;
            gvListaClientesExtracto.Columns[0].Visible = false;
            gvListaClientesExtracto.DataSource = Session["DTEXTRACTOS"];
            gvListaClientesExtracto.DataBind();
            gvListaClientesExtracto.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvListaClientesExtracto);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Extractos.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen Datos");
        }
    }

  
}