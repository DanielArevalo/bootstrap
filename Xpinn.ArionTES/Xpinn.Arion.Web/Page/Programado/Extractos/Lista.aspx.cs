using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using Xpinn.Programado.Services;
using Xpinn.Programado.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;

partial class Lista : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.Programado.Services.CuentasProgramadoServices CuentasPrograServicios = new Xpinn.Programado.Services.CuentasProgramadoServices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[CuentasPrograServicios.CodigoProgramaExtractos + ".id"] != null)
                VisualizarOpciones(CuentasPrograServicios.CodigoProgramaExtractos, "E");
            else
                VisualizarOpciones(CuentasPrograServicios.CodigoProgramaExtractos, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarConsultar(true);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarImprimir(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentasPrograServicios.CodigoProgramaExtractos, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
               // Dim fecha as new datetime(Date.Now.Year,Date.Now.Month,1)
                DateTime fechafin= new DateTime(DateTime.Now.Year,DateTime.Now.Month-1,DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month));
                DateTime fechain = new DateTime(DateTime.Now.Year, DateTime.Now.Month-1,1);
                Txtfechaperiodo_final.Text = Convert.ToString(fechafin);
                txtFecha_periodo.Text= Convert.ToString(fechain);
                txtObservacionesExtracto.Text = "Periodo Comprendido entre " + fechain.ToShortDateString() + "  y  " + fechafin.ToShortDateString();
                CargaDropDown();
                mvPrincipal.ActiveViewIndex = 0;
                txtFecha_corte.ToDateTime = DateTime.Now;                               
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentasPrograServicios.CodigoProgramaExtractos, "Page_Load", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 0;
        txtFecha_corte.Text = "";
        txtFecha_periodo.Text = "";
        txtCodigo.Text = "";
        txtApellidos.Text = "";
        txtcodigo_final.Text = "";
        txtidentificacion.Text = "";
        Txtfechaperiodo_final.Text = "";
        txtidentificacion_final.Text = "";

        gvLista.DataSource = null;
        gvLista.DataBind();
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
   
        VerError("");


        if (txtFecha_periodo.ToDateTime >  Txtfechaperiodo_final.ToDateTime)
        {
            VerError("La fecha posterior no puede ser superior a la fecha inciial.");
            return;
        }

        mvPrincipal.ActiveViewIndex = 1;
        Actualizar();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");

        if (txtNumeroCuenta.Text == txtNumeroCuenta.Text)
        {
            VerError("Coloque un numero de cuenta valido");
            return;
        }

        VerError("Desea guardar los datos de el traslado de la cuenta?");

    }

    protected void btnCancelar_Click(object sender, EventArgs e)
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
           // ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlEmpresa.SelectedIndex = 0;
            ddlEmpresa.DataBind();
        }
        //LINEAS DE AHORRO PROGRAMADO
        Xpinn.Programado.Data.LineasProgramadoData vDatosLinea = new Xpinn.Programado.Data.LineasProgramadoData();
        LineasProgramado pLineas = new LineasProgramado();
        List<LineasProgramado> lstConsulta = new List<LineasProgramado>();
        pLineas.estado = 1;
        lstConsulta = vDatosLinea.ListarComboLineas(pLineas, (Usuario)Session["usuario"]);
        if (lstConsulta.Count > 0)
        {
            ddllineaahorro.DataSource = lstConsulta;
            ddllineaahorro.DataTextField = "nombre";
            ddllineaahorro.DataValueField = "cod_linea_programado";
           // ddllineaahorro.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddllineaahorro.AppendDataBoundItems = true;
            ddllineaahorro.SelectedIndex = 0;
            ddllineaahorro.DataBind();

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



    
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[CuentasPrograServicios.CodigoProgramaExtractos + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[CuentasPrograServicios.CodigoProgramaExtractos + ".id"] = id;
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(CuentasPrograServicios.CodigoProgramaExtractos, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(true);
            toolBar.MostrarImprimir(true);
            String filtro = obtFiltro(ObtenerValores());
            List<Xpinn.Programado.Entities.CuentasProgramado> lstConsulta = new List<Xpinn.Programado.Entities.CuentasProgramado>();
             
            DateTime pFechaIni, pFechaFin;
            pFechaIni = txtFecha_periodo.ToDateTime == null ? DateTime.MinValue : txtFecha_periodo.ToDateTime;
            pFechaFin = Txtfechaperiodo_final.ToDateTime == null ? DateTime.MinValue : Txtfechaperiodo_final.ToDateTime;
            lstConsulta = CuentasPrograServicios.ListarAhorroExtractos(ObtenerValores(), (Usuario)Session["usuario"], filtro);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTAhorroVista"] = lstConsulta;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(CuentasPrograServicios.CodigoProgramaExtractos + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentasPrograServicios.CodigoProgramaExtractos, "Actualizar", ex);
        }
    }
    private string obtFiltro(CuentasProgramado ahorro)
    {
        String filtro = String.Empty;

        if (txtCodigo.Text != "")
            if (txtCodigo.Text != "" && txtcodigo_final.Text != "")
                filtro += " and p.Cod_Persona between " + txtCodigo.Text + " and " + txtcodigo_final.Text;
            else
                filtro += " and p.Cod_Persona  = " + txtCodigo.Text;
        else if (txtcodigo_final.Text != "")
            filtro += " and p.Cod_Persona  = " + txtcodigo_final.Text;

        if (txtidentificacion.Text != "")
            if (txtidentificacion.Text != "" && txtidentificacion_final.Text != "")
                filtro += " and p.Identificacion between '" + txtidentificacion.Text + "' and '" + txtidentificacion_final.Text + "'";
            else
                filtro += " and p.Identificacion = '" + txtidentificacion.Text + "' ";
        else if (txtidentificacion_final.Text != "")
            filtro += " and p.Identificacion <= '" + txtidentificacion_final.Text + "' ";
        
        if (txtNombres.Text != "")
            filtro += " and p.Nombres like '%" + txtNombres.Text + "%'";

        if (txtApellidos.Text != "")
            filtro += " and p.Apellidos like '%" + txtApellidos.Text + "%'";
       
        
        if (txtNumeroCuenta.Text != "")
            if (txtNumeroCuenta.Text != "" && this.txtNumCuenta_final.Text != "")
                filtro += " and a.numero_programado between " + txtNumeroCuenta.Text + " and " + txtNumCuenta_final.Text + "";
            else
                filtro += " and  a.numero_programado  = '" + txtNumeroCuenta.Text + "' ";
        else if (txtNumCuenta_final.Text != "")
            filtro += " and a.numero_programado <= '" + txtNumCuenta_final.Text + "' ";


        if (ddllineaahorro.SelectedValue != "0")
        {
            filtro += " and a.COD_LINEA_PROGRAMADO=" + ddllineaahorro.SelectedValue;

        }

        return filtro;
    }

    private CuentasProgramado ObtenerValores()
    {
        Xpinn.Programado.Entities.CuentasProgramado vAhorroVista = new Xpinn.Programado.Entities.CuentasProgramado();

        if (txtFecha_corte.ToDate.Trim() != "")
            vAhorroVista.fec_realiza = Convert.ToDateTime(txtFecha_corte.ToDate.Trim());
        //fecha de aprovacion

        if (txtFecha_periodo.ToDate.Trim() != "")
            vAhorroVista.fecha_apertura = Convert.ToDateTime(txtFecha_periodo.ToDate.Trim());
        //fecha de aprovacion

        if (Txtfechaperiodo_final.ToDate.Trim() != "")
            vAhorroVista.fecha_cierre = Convert.ToDateTime(Txtfechaperiodo_final.ToDate.Trim());
        //fecha de aprovacion

        if (txtCodigo.Text.Trim() != "")
            vAhorroVista.codigo_inicial = Convert.ToInt32(txtCodigo.Text.Trim());
        //codigo
        if (txtcodigo_final.Text.Trim() != "")
            vAhorroVista.codigo_final = Convert.ToInt32(txtcodigo_final.Text.Trim());
        //codigo
        if (txtidentificacion.Text.Trim() != "")
            vAhorroVista.identificacion = Convert.ToString(txtidentificacion.Text.Trim());
        //identificacion
        if (txtidentificacion_final.Text.Trim() != "")
            vAhorroVista.identificacion_final = Convert.ToString(txtidentificacion_final.Text.Trim());
        //identificacion

        if (txtNombres.Text.Trim() != "")
            vAhorroVista.nombres = Convert.ToString(txtNombres.Text.Trim());
        //nombres
        if (txtApellidos.Text.Trim() != "")
            vAhorroVista.apellidos = Convert.ToString(txtApellidos.Text.Trim());
        //apellidos


        if (ddlEmpresa.Text.Trim() != "")
            vAhorroVista.empresa = Convert.ToString(ddlEmpresa.Text.Trim());
        //linea de ahorro

        if (ddlCiudad.SelectedIndex != 0)
            vAhorroVista.CiudadResidencia = Convert.ToString(ddlCiudad.SelectedValue.Trim());
        //Nombre oficina

        if (txtNumeroCuenta.Text.Trim() != "")
            vAhorroVista.numero_cuenta = Convert.ToString(txtNumeroCuenta.Text.Trim());
        //numero cuenta
        if (txtNumCuenta_final.Text.Trim() != "")
            vAhorroVista.numero_cuenta_final = Convert.ToString(txtNumCuenta_final.Text.Trim());
        //numero cuenta
        if (ddllineaahorro.SelectedIndex != 0)
            vAhorroVista.cod_linea_ahorro = Convert.ToString(ddllineaahorro.SelectedValue.Trim());
        //Nombre oficina

        return vAhorroVista;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox check = (CheckBox)rFila.FindControl("check");
            if (check != null)
            {
                if (check.Checked == true)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=AhorroVista.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.ContentEncoding = Encoding.Default;
                    StringWriter sw = new StringWriter();
                    ExpGrilla expGrilla = new ExpGrilla();

                    sw = expGrilla.ObtenerGrilla(GridView1, null);

                    Response.Write(expGrilla.style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void gvLista_SelectedIndexChanged1(object sender, System.EventArgs e)
    {

    }

    Boolean ValidarDatos()
    {
        VerError("");

        if (txtFecha_corte.Text == "")
        {
            VerError("Ingrese la Fecha de Corte");
            return false;
        }
        if (txtFecha_periodo.Text == "")
        {
            VerError("Ingrese la Fecha del periodo");
            return false;
        }
        if (Txtfechaperiodo_final.Text == "")
        {
            VerError("Ingrese la Fecha del periodo final");
            return false;
        }


        if (txtFecha_periodo.Text != "" && Txtfechaperiodo_final.Text != "")
            if (Convert.ToDateTime(txtFecha_periodo.Text) > Convert.ToDateTime(Txtfechaperiodo_final.Text))
            {
                VerError("El Rango de Fechas fue ingresadas de forma erronea. (Detalle de Pago)");
                return false;
            }

        if (txtCodigo.Text != "" && txtcodigo_final.Text != "")
            if (Convert.ToInt32(txtCodigo.Text) > Convert.ToInt32(txtcodigo_final.Text))
            {
                VerError("El código Inicial debe ser menor que el código Final");
                return false;
            }

        if (txtNumeroCuenta.Text != "" && txtNumCuenta_final.Text != "")
            if (Convert.ToInt64(txtNumeroCuenta.Text) > Convert.ToInt64(txtNumCuenta_final.Text))
            {
                VerError("El número de cuenta Inicial debe ser menor que el número Final");
                return false;
            }

        if (txtidentificacion.Text != "" && txtidentificacion_final.Text != "")
            if (Convert.ToInt64(txtidentificacion.Text) > Convert.ToInt64(txtidentificacion_final.Text))
            {
                VerError("El número de identificacion Inicial debe ser menor que el número de Identificacion Final");
                return false;
            }

        if (txtObservacionesExtracto.Text == "")
        {
            VerError("ponga una observacion al extracto");
            return false;
        }



        return true;
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
            tablegeneral.Columns.Add("nombres");
            tablegeneral.Columns.Add("ciudad");
            tablegeneral.Columns.Add("direccion");
            tablegeneral.Columns.Add("fecha_oper");
            tablegeneral.Columns.Add("nomtipo_ope");
            tablegeneral.Columns.Add("cod_ope");
            tablegeneral.Columns.Add("nombre");
            tablegeneral.Columns.Add("linea");
            tablegeneral.Columns.Add("saldo_total");
            tablegeneral.Columns.Add("tipo_mov");
            tablegeneral.Columns.Add("valor");
            tablegeneral.Columns.Add("saldo");
            tablegeneral.Columns.Add("rutaImagen");
            tablegeneral.Columns.Add("iddetalle");
            tablegeneral.Columns.Add("idextracto");
            tablegeneral.Columns.Add("cuenta");
            tablegeneral.Columns.Add("fecha_proximo_pago");
            tablegeneral.Columns.Add("valor_total");
            int contDetalle = 0;

            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox check = (CheckBox)rFila.FindControl("check");
                if (check != null)
                {
                    if (check.Checked == true)
                    {

                        String vartipo_operacion = Convert.ToString(rFila.Cells[2].Text);//NUMERO DE CUENTA Seleccionado;
                        string identificacionn = rFila.Cells[6].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[6].Text) : "";
                        string NombreCompleto, nombres, apellidos;
                        nombres = rFila.Cells[7].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[7].Text) : "";
                        apellidos = txtApellidos.Text;
                        NombreCompleto = (nombres + ' ' + apellidos).Trim();
                        string ciudad = rFila.Cells[5].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[5].Text) : "";
                        string direccion = rFila.Cells[8].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[8].Text) : "";
                        string cuenta = rFila.Cells[2].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[2].Text) : "";
                        string saldo_total = rFila.Cells[9].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[9].Text) : "";
                        string linea = rFila.Cells[3].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[3].Text) : "";
                        string fecha_proximo_pago = rFila.Cells[10].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[10].Text) : "";
                        string valor_total = rFila.Cells[11].Text != "&nbsp;" ? Convert.ToString(rFila.Cells[11].Text) : "";
                      


                        #region Trayendo datos desde BD para el informe

                        List<ReporteMovimiento> lstExtractos = new List<ReporteMovimiento>();
                        lstExtractos = CuentasPrograServicios.ListarDetalleExtracto(vartipo_operacion, Convert.ToDateTime(txtFecha_corte.Text), (Usuario)Session["usuario"]);

                        //Asignando parametros                        
                        #endregion Trayendo datos desde BD para el informe

                        string cRutaDeImagen;

                        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";

                        //TABLA POR CLIENTE
                        DataTable table = new DataTable();
                        table.Columns.Add("fecha_oper");
                        table.Columns.Add("nomtipo_ope");
                        table.Columns.Add("cod_ope");
                        table.Columns.Add("cod_ofi");
                        table.Columns.Add("tipo_mov");
                        table.Columns.Add("valor");
                        table.Columns.Add("saldo");
                        table.Columns.Add("nombre");
                        

                        if (lstExtractos.Count > 0)
                        {
                            foreach (ReporteMovimiento fila in lstExtractos)
                            {
                                DataRow datarw;
                                datarw = table.NewRow();
                                datarw[0] = fila.fecha_oper.ToShortDateString();
                                datarw[1] = fila.nomtipo_ope;
                                datarw[2] = fila.cod_ope;
                                datarw[3] = fila.nombre;
                                datarw[4] = fila.tipo_mov;
                                datarw[5] = fila.valor.ToString("n0");
                                datarw[6] = fila.saldo.ToString("n0");
                                datarw[7] = fila.nombre;
                                

                                table.Rows.Add(datarw);
                            }
                        }
                        else
                        {
                            DataRow datos;
                            datos = table.NewRow();
                            datos[0] = "";
                            datos[1] = "";
                            datos[2] = "";
                            datos[3] = "";
                            datos[4] = "";
                            datos[5] ="";
                            datos[6] = "";
                            datos[7] = "";
                          
                            table.Rows.Add(datos);
                        }


                        //LLena el datatable general
                        for (int x = 0; x < 1; x++)
                        {
                            contDetalle++;
                            foreach (DataRow rData in table.Rows)
                            {
                                DataRow datarw;
                                
                                datarw = tablegeneral.NewRow();
                                datarw[0] = vartipo_operacion;
                                datarw[1] = identificacionn;
                                datarw[2] = NombreCompleto;
                                datarw[3] = ciudad;
                                datarw[4] = direccion;
                                datarw[5] = rData[0].ToString();
                                datarw[6] = rData[1].ToString();
                                datarw[7] = rData[2].ToString() ;
                                datarw[8] = rData[3].ToString();
                                datarw[9] = linea;
                                datarw[10] = saldo_total;
                                datarw[11] = rData[4].ToString();
                                datarw[12] = rData[5].ToString();
                                datarw[13] = rData[6].ToString();
                                datarw[14] = cRutaDeImagen;
                                datarw[15] = contDetalle;
                                datarw[16] = 1;
                                datarw[17] = cuenta;
                                datarw[18] = fecha_proximo_pago;
                                datarw[19] = valor_total;
                                tablegeneral.Rows.Add(datarw);
                            }

                            Site toolBar = (Site)Master;
                            toolBar.MostrarLimpiar(false);
                            toolBar.MostrarConsultar(false);
                        }
                    }
                }

                Usuario pUsu = (Usuario)Session["usuario"];

                    rvExtracto.LocalReport.DataSources.Clear();
                    ReportParameter[] param = new ReportParameter[4];
                    param[0] = new ReportParameter("entidad", pUsu.empresa);
                    param[1] = new ReportParameter("nit", pUsu.nitempresa);
                    param[2] = new ReportParameter("fecha_corte", Convert.ToDateTime(txtFecha_corte.Text).ToShortDateString());
                    param[3] = new ReportParameter("observaciones", txtObservacionesExtracto.Text);


                    rvExtracto.LocalReport.EnableExternalImages = true;
                    rvExtracto.LocalReport.SetParameters(param);

                    ReportDataSource rds1 = new ReportDataSource("DataSet1", tablegeneral);
                    rvExtracto.LocalReport.DataSources.Add(rds1);
                    rvExtracto.LocalReport.Refresh();
                 
                

                if (tablegeneral.Rows.Count > 0)
                {

                    Site toolBar = (Site)Master;
                    mvPrincipal.ActiveViewIndex = 2;
                    toolBar.MostrarCancelar(true);
                    toolBar.MostrarExportar(false);
                    toolBar.MostrarImprimir(false);
                    toolBar.MostrarLimpiar(false);
                    toolBar.MostrarConsultar(false);
                }
            }
        }
    }
}




#region Titulares

/// <summary>
/// Método para instar un detalle en blanco para cuando la grilla no tiene datos
/// </summary>
/// <param name="consecutivo"></param>


/// <summary>
/// Método para cambio de página
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>






/// <summary>
/// Método para borrar un registro de la grilla
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>




#endregion
