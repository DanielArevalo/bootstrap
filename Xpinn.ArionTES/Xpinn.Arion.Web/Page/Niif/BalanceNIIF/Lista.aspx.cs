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
using Xpinn.Comun.Services;
using Xpinn.NIIF.Services;
using Xpinn.NIIF.Entities;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{    
    private Xpinn.NIIF.Services.BalanceNIIFService BalanceServicio = new Xpinn.NIIF.Services.BalanceNIIFService();
    private Xpinn.Comun.Services.CiereaService Cierea = new Xpinn.Comun.Services.CiereaService();
       
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(BalanceServicio.CodigoProgramaoriginal, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoImportar += btnImportar_Click;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalanceServicio.CodigoProgramaoriginal, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            if (!IsPostBack)
            {
                ViewState["DTBALANCEGENERAL"] = null;
                Session["FORMA"] = null;
                ViewState["Index"] = null;
                mpeVisualizarDatos.Hide();
                CargarValoresConsulta(pConsulta, BalanceServicio.CodigoProgramaoriginal);
                
                panelCarga.Visible = false;

                ddlTipo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlTipo.Items.Insert(1, new ListItem("Reclasificación", "1"));
                ddlTipo.Items.Insert(2, new ListItem("Ajuste", "2"));
                ddlTipo.SelectedIndex = 0;
                ddlTipo.DataBind();

                DesabilitarText();
                Cargar_Fecha();
                lblMenError.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalanceServicio.CodigoProgramaoriginal, "Page_Load", ex);
        }
    }

    
    void CargarDropDown()
    {
        ddlCuenta_Dest.Items.Clear();
        PlanCuentasNIIF pPlanCuentas = new PlanCuentasNIIF();
        ddlCuenta_Dest.DataSource= BalanceServicio.ListaPlan_Cuentas(pPlanCuentas,(Usuario)Session["Usuario"]);
        ddlCuenta_Dest.DataTextField = "nombre";
        ddlCuenta_Dest.DataValueField= "cod_cuenta_niif";
        ddlCuenta_Dest.DataBind();
    }    

    protected void Cargar_Fecha()
    {
        try
        {
            Xpinn.Comun.Services.CiereaService cierreServicio = new Xpinn.Comun.Services.CiereaService();
            txtFechaCorte.ToDateTime = cierreServicio.FechaUltimoCierre("C", (Usuario)Session["Usuario"]);
        }
        catch
        {
            VerError("No se pudo determinar fecha de cierre inicial");
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Site toolBar = (Site)Master;
            if (panelCarga.Visible == false)
            {
                toolBar.MostrarImportar(true);
                panelCarga.Visible = false;
                Actualizar();
            }
            else
            {
                toolBar.MostrarImportar(true);
                Boolean pVisi = gvLista.Rows.Count == 0 ? false : true;                 
                toolBar.MostrarExportar(pVisi);
                toolBar.MostrarGuardar(pVisi);
                btnGenerarSaldos.Visible = true;
                pDatos.Visible = true;
                panelCarga.Visible = false;
            }
        }
        catch (Exception ex) 
        {
            BOexcepcion.Throw(BalanceServicio.CodigoProgramaoriginal, "btnConsultar_Click", ex);
        }
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        string pMensaje = string.Empty;
        if (panelCarga.Visible == false)
        {
            ViewState["Opcion"] = "GUARDAR";
            pMensaje = "Desea guardar los balances seleccionados?";
        }
        else
        {
            ViewState["Opcion"] = "AJUSTES";
            pMensaje = "Desea guardar los ajustes de los balances cargados?";
        }
        ctlMensaje.MostrarMensaje(pMensaje);
    }

    int tipo = 0;
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Opcion"].ToString() == "VOLVER")
            { //Opcion volver a generar un balance.
                tipo = 2;
                string pError = "";
                BalanceServicio.EliminarFechaBalanceGeneradoNIIF(Convert.ToDateTime(txtFechaCorte.Text), (Usuario)Session["usuario"]);
                if (BalanceServicio.GenerarBalance_NIIF(Convert.ToDateTime(txtFechaCorte.Text), ref opcion, ref pError, (Usuario)Session["usuario"]) == false)
                {
                    BalanceServicio.GenerarBalance_NIIF(Convert.ToDateTime(txtFechaCorte.Text), ref opcion, ref pError, (Usuario)Session["usuario"]);
                }
                if (pError != "")
                {
                    VerError(pError);
                    return;
                }
                BalanceServicio.ModificarFechaNIIF(Convert.ToDateTime(txtFechaCorte.Text), tipo, (Usuario)Session["usuario"]);
                Actualizar();
            }
            else if (ViewState["Opcion"].ToString() == "GUARDAR")
            {  //Opcion Grabar
                txtFechaCorte.Enabled = false;
                //Boolean rpta = false;
                if (Session["FORMA"] != null)
                {
                    if (Session["FORMA"].ToString() == "CARGA")
                    {
                        //GRABAR LOS DATOS CARGADOS
                        List<BalanceNIIF> lstBalance = new List<BalanceNIIF>();
                        lstBalance = ObtenerListaCarga();
                        string pError = "";
                        DateTime pFechaCorte = txtFechaCorte.ToDateTime;
                        BalanceServicio.CrearBalanceNIIF(ref pError, pFechaCorte, lstBalance, (Usuario)Session["usuario"]);
                        if (pError.Trim() != "")
                        {
                            VerError(pError);
                            return;
                        }
                        //rpta = true;
                    }
                }
                tipo = 1;
                BalanceServicio.ModificarFechaNIIF(Convert.ToDateTime(txtFechaCorte.Text), tipo, (Usuario)Session["usuario"]);
                gvLista.DataSource = null;
                gvLista.DataBind();
                pDatos.Visible = false;
                lblTotalRegs.Visible = false;
                Session.Remove("FORMA");
                Session.Remove("Opcion");
            }
            else
            {
                //OPCION DE GRABAR AJUSTES MASIVOS
                string pError = string.Empty;
                Boolean pResult = false;
                List<BalanceNIIF> lstBalance = new List<BalanceNIIF>();
                lstBalance = (List<BalanceNIIF>)ViewState["DTBALANCEGENERAL"];
                pResult = BalanceServicio.ReclasificarBalancesNIIF(lstBalance, ref pError, Usuario);
                if (!pResult)
                {
                    if (!string.IsNullOrEmpty(pError))
                    {
                        VerError(pError);
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected List<BalanceNIIF> ObtenerListaCarga()
    {
        List<BalanceNIIF> lstData = new List<BalanceNIIF>();
        if (ViewState["DTBALANCEGENERAL"] != null)
        {
            lstData = (List<BalanceNIIF>)ViewState["DTBALANCEGENERAL"];
        }
        int contador = 0;
        gvLista.AllowPaging = false;
        gvLista.DataSource = lstData;
        gvLista.DataBind();
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            BalanceNIIF pEntidad = new BalanceNIIF();
            int rowIndex = ((gvLista.PageIndex * gvLista.PageSize) + contador);
            pEntidad = lstData[rowIndex];

            if (rFila.Cells[1].Text.Trim() != "" && rFila.Cells[1].Text != "&nbsp;")
                pEntidad.cod_cuenta_niif = rFila.Cells[1].Text.Trim();

            if (rFila.Cells[2].Text.Trim() != "" && rFila.Cells[2].Text != "&nbsp;")
                pEntidad.nombre = rFila.Cells[2].Text.Trim();

            if (rFila.Cells[10].Text.Trim() != "" && rFila.Cells[10].Text != "&nbsp;")
                pEntidad.centro_costo = Convert.ToInt32(rFila.Cells[10].Text.Trim());

            if (rFila.Cells[3].Text.Trim() != "" && rFila.Cells[3].Text != "&nbsp;")
                pEntidad.tipo_moneda = 1; //Convert.ToInt32(rFila.Cells[3].Text.Trim());
            else
            {
                if (pEntidad.tipo != null)
                    pEntidad.tipo_moneda = Convert.ToInt32(pEntidad.tipo);
                else
                    pEntidad.tipo_moneda = null;
            }
            Int64 pCod_Persona = 0;
            if (gvLista.DataKeys[rFila.RowIndex].Value != null)
                pCod_Persona = Convert.ToInt64(gvLista.DataKeys[rFila.RowIndex].Value.ToString());

            if (pCod_Persona != 0)
                pEntidad.cod_persona = pCod_Persona;
            else
                pEntidad.cod_persona = null;

            if (rFila.Cells[9].Text.Trim() != "" && rFila.Cells[9].Text != "&nbsp;")
                pEntidad.saldo = Convert.ToDecimal(rFila.Cells[9].Text.Trim().Replace(".",""));            
            contador++;
        }
        gvLista.AllowPaging = true;
        gvLista.DataSource = lstData;
        gvLista.DataBind();
        return lstData;
    }
   
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (Session["FORMA"] != null)
        {
            if (Session["FORMA"].ToString() == "CARGA")
            {
                int Index = (gvLista.PageSize * gvLista.PageIndex) + e.NewEditIndex;
                ViewState.Add("Index", Index);
            }
        }
        String id = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        lblMenError.Text = "";   
        txtcod_cuenta.Text = gvLista.Rows[e.NewEditIndex].Cells[1].Text;
        txtnombre.Text = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        txtsaldo.Text = gvLista.Rows[e.NewEditIndex].Cells[9].Text;
        
        CargarDropDown();       
        Session[BalanceServicio.CodigoProgramaoriginal + ".id"] = id;
        e.NewEditIndex = -1;
        limpiatext();
        mpeVisualizarDatos.Show();
    }


    /// <summary>
    /// Generar las condiciones de acuerdo a los filtros ingresados
    /// </summary>
    /// <param name="credito"></param>
    /// <returns></returns>
    ///       
    private void Actualizar()
    {       
        // Mostrar los datos
        try
        {
            List<Xpinn.NIIF.Entities.BalanceNIIF> lstConsulta = new List<Xpinn.NIIF.Entities.BalanceNIIF>();
            DateTime pFecha;
            pFecha = txtFechaCorte.TieneDatos ? txtFechaCorte.ToDateTime : DateTime.MinValue;

            lstConsulta = BalanceServicio.ListarBalance_NIIF(pFecha, (Usuario)Session["usuario"]);
                        
            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                toolBar.MostrarGuardar(true);
                toolBar.MostrarExportar(true);
                pDatos.Visible = true;
                gvLista.DataBind();
                ViewState["DTBALANCEGENERAL"] = lstConsulta;
            }
            else
            {
                toolBar.MostrarGuardar(false);
                toolBar.MostrarExportar(false);
                ViewState["DTBALANCEGENERAL"] = null;
                pDatos.Visible = false;
                VerError("No se encontraron Datos");
            }

            // Mostrar el número de registros
            lblTotalRegs.Visible = true;
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();

            Session.Add(BalanceServicio.CodigoProgramaoriginal + ".consulta", 1);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalanceServicio.CodigoProgramaoriginal, "Actualizar", ex);
        }
    }

    int opcion = 0;
    protected void btnGenerarSaldos_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        if (txtFechaCorte.Text == "")
        {
            VerError("Ingrese la fecha de corte, verifique los datos.");
            return;
        }
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, BalanceServicio.CodigoProgramaoriginal);
            DateTime pFecha;
            pFecha = txtFechaCorte.TieneDatos ? txtFechaCorte.ToDateTime : DateTime.MinValue;            
            // Generar el balance de iniciación
            try
            {
                string pError = "";
                if(BalanceServicio.GenerarBalance_NIIF(pFecha, ref opcion,ref pError,(Usuario)Session["usuario"]) == false)
                {
                    if (opcion == 0)
                    {
                        ViewState["Opcion"] = "VOLVER";
                        ctlMensaje.MostrarMensaje("Ya existe un balance generado para esta fecha. Desea Volver a generar?");
                        return;
                    }
                }
                else
                {   
                    BalanceServicio.GenerarBalance_NIIF(pFecha, ref opcion,ref pError, (Usuario)Session["usuario"]);
                    if (pError != "")
                    {
                        VerError(pError);
                        return;
                    }
                    Actualizar();
                }

            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }           
        }
    }


    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {        
        if (ddlTipo.SelectedItem.Text == "Ajuste")
        {
            lblcuenta_dest.Visible = false;
            ddlCuenta_Dest.Visible = false;
        }
        else 
        {
            lblcuenta_dest.Visible = true;
            ddlCuenta_Dest.Visible = true;
        }
    }


    bool validarExtension(FileUpload f)
    {
        bool rutaOk = false;
        String extension = Path.GetExtension(f.FileName);

        if (extension == ".xls")
        {
            rutaOk = true;
        }
        else if (extension == ".xlsx")
        {
            rutaOk = true;
        }

        return rutaOk;
    }


    protected void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Site toolBar = (Site)Master;
            toolBar.MostrarImportar(false);
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            panelCarga.Visible = true;
            pDatos.Visible = false;
            panelAjustes.Visible = false;
            gvCargaAjuste.DataSource = null;
            gvCargaAjuste.DataBind();
            rblTipoCarga_SelectedIndexChanged(rblTipoCarga, null);
            btnGenerarSaldos.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(BalanceServicio.CodigoProgramaoriginal, "btnImportar_Click", ex);
        }
    }


    protected void btnCargaDatos_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");        
        string error = "";
        try
        {
            if (txtFechaCorte.Text == "")
            {
                VerError("Ingrese la fecha de corte, verifique los datos.");
                return;
            }
            if (fupArchivoCarga.HasFile)
            {
                ExcelToGrid CargaEx = new ExcelToGrid();
                //CREACION DEL DATATABLE
                DataTable dt = new DataTable();

                String NombreArchivo = Path.GetFileName(this.fupArchivoCarga.PostedFile.FileName);

                if(validarExtension(fupArchivoCarga))
                {
                    //GUARDANDO EL ARCHIVO EN EL SERVIDOR
                    fupArchivoCarga.PostedFile.SaveAs(Server.MapPath("Archivos\\") + NombreArchivo);
                    fupArchivoCarga.Dispose();

                    //LEENDO LOS DATOS EN EL DATATABLE
                    dt = CargaEx.leerExcel(Server.MapPath("Archivos\\") + NombreArchivo , "Datos");

                    // ELIMINANDO ARCHIVOS GENERADOS
                    try
                    {
                        string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
                        foreach (string ficheroActual in ficherosCarpeta)
                            File.Delete(ficheroActual);
                    }
                    catch
                    { }

                    if (dt.Rows.Count <= 0)
                    {
                        error = "No se pudo cargar el archivo o el archivo no contiene datos (Hoja: Datos)";
                        VerError(error);
                        return;
                    }
                    else
                    {
                        //DECLARACION DE VARIABLES
                        List<BalanceNIIF> lstBalanceNiif = new List<BalanceNIIF>();
                        string pError = string.Empty;
                        Boolean rpta = RealizarCargaArvhico(ref lstBalanceNiif, dt, rblTipoCarga.SelectedIndex, ref pError);
                        if (!rpta)
                        {
                            if (string.IsNullOrEmpty(pError))
                            {
                                VerError(pError);
                                return;
                            }
                        }

                        ViewState["DTBALANCEGENERAL"] = null;
                        if (lstBalanceNiif.Count > 0)
                        {
                            Site toolBar = (Site)this.Master;
                            toolBar.MostrarGuardar(true);
                            if (rblTipoCarga.SelectedIndex == 0)
                            {
                                toolBar.MostrarExportar(true);
                                panelCarga.Visible = false;
                                pDatos.Visible = true;
                                gvLista.DataSource = lstBalanceNiif;
                                gvLista.DataBind();
                                lblTotalRegs.Visible = true;
                                lblTotalRegs.Text = "<br/> Registros encontrados " + lstBalanceNiif.Count.ToString();
                            }
                            else
                            {
                                panelCarga.Visible = true;
                                pDatos.Visible = false;
                                gvCargaAjuste.DataSource = lstBalanceNiif;
                                gvCargaAjuste.DataBind();
                                panelAjustes.Visible = true;
                            }
                            //CARGANDO LOS DATOS A LA GRIDVIEW                            
                            ViewState["DTBALANCEGENERAL"] = lstBalanceNiif;
                            Session["FORMA"] = "CARGA";
                        }
                    }
                }
                else
                {
                    VerError("Ingrese un Archivo valido (Excel).");
                }                
            }
            else
            {
                VerError("Seleccione un archivo excel, verifique los datos.");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected Boolean RealizarCargaArvhico(ref List<BalanceNIIF> lstArchivo, DataTable dt, int pOpcion, ref string pError)
    {
        int contador = 0;
        int rowIndex = 0;
        try
        {
            //REALIZANDO CARGA DEL BALANCE INICIAL
            //RECORRIENDO LOS DATOS DEL EXCEL LEIDO
            foreach (DataRow rData in dt.Rows)
            {
                rowIndex++;
                BalanceNIIF pEntidad = new BalanceNIIF();
                if (pOpcion == 0)
                {
                    //ORDEN DE CARGA (1 = Cod Cuenta Niif, 2 = Nombre Cuenta(opcional), 3 = Cod Moneda, 4 = Cod Persona(opcional), 5 = Identificacion(opcional), 6 = Nombre(opcional), 7 = Saldo, 8 = Centro Costo)
                    for (int i = 0; i <= 8; i++)
                    {
                        if (i == 0)
                        {
                            string sFormatoFecha = "dd/MM/yyyy";
                            pEntidad.fecha = DateTime.ParseExact(txtFechaCorte.ToDateTime.ToShortDateString(), sFormatoFecha, null);
                        }
                        if (i == 1)
                        {
                            pEntidad.cod_cuenta_niif = rData.ItemArray[i - 1].ToString().Trim();
                        }
                        if (i == 2)
                        {
                            if (rData.ItemArray[i - 1].ToString().Trim() != "" && rData.ItemArray[i - 1].ToString() != "&nbsp;")
                                pEntidad.nombre = rData.ItemArray[i - 1].ToString().Trim();
                        }
                        if (i == 3)
                        {
                            pEntidad.tipo = Convert.ToString(rData.ItemArray[i - 1].ToString().Trim());
                        }
                        if (i == 4)
                        {
                            if (rData.ItemArray[i - 1].ToString().Trim() != "" && rData.ItemArray[i - 1].ToString() != "&nbsp;")
                                pEntidad.cod_persona = Convert.ToInt64(rData.ItemArray[i - 1].ToString().Trim());
                        }
                        if (i == 5)
                        {
                            if (rData.ItemArray[i - 1].ToString().Trim() != "" && rData.ItemArray[i - 1].ToString() != "&nbsp;")
                                pEntidad.identificacion = Convert.ToString(rData.ItemArray[i - 1].ToString().Trim());
                        }
                        if (i == 6)
                        {
                            if (rData.ItemArray[i - 1].ToString().Trim() != "" && rData.ItemArray[i - 1].ToString() != "&nbsp;")
                                pEntidad.nombre = rData.ItemArray[i - 1].ToString().Trim();
                        }
                        if (i == 7)
                        {
                            pEntidad.saldo = Convert.ToDecimal(rData.ItemArray[i - 1].ToString().Trim());
                        }
                        if (i == 8)
                        {
                            pEntidad.centro_costo = Convert.ToInt32(rData.ItemArray[i - 1].ToString().Trim());
                        }
                        contador++;
                    }
                    lstArchivo.Add(pEntidad);
                }
                else
                {
                    //REALIZANDO CARGA DE AJUSTES DEL BALANCE
                    for (int i = 0; i <= 6; i++)
                    {
                        //ORDEN DE CARGA => 1 Cod Cuenta Niif, 2 Centro Costo, 3 Tipo Ajuste( 1 = Reclasificación, 2 = Ajuste), 4 Cod Cuenta Destino(Solo si es Reclasificación), 5 Valor, 6 Observación.
                        if (i == 0)
                        {
                            string sFormatoFecha = "dd/MM/yyyy";
                            pEntidad.fecha = DateTime.ParseExact(txtFechaCorte.ToDateTime.ToShortDateString(), sFormatoFecha, null);
                        }
                        if (i == 1)
                        {
                            pEntidad.cod_cuentaOrigen_niif = rData.ItemArray[i - 1].ToString().Trim();
                        }
                        if (i == 2)
                        {
                            if (rData.ItemArray[i - 1].ToString().Trim() != "" && rData.ItemArray[i - 1].ToString() != "&nbsp;")
                                pEntidad.centro_costo = Convert.ToInt32(rData.ItemArray[i - 1].ToString().Trim());
                        }
                        if (i == 3)
                        {
                            if (rData.ItemArray[i - 1].ToString().Trim() != "" && rData.ItemArray[i - 1].ToString() != "&nbsp;")
                                pEntidad.ajuste = Convert.ToInt32(rData.ItemArray[i - 1].ToString().Trim());
                            else
                                pEntidad.ajuste = 2;
                        }
                        if (i == 4)
                        {
                            if (rData.ItemArray[i - 1].ToString().Trim() != "" && rData.ItemArray[i - 1].ToString() != "&nbsp;")
                                pEntidad.cod_cuenta_niif = Convert.ToString(rData.ItemArray[i - 1].ToString().Trim());
                        }
                        if (i == 5)
                        {
                            if (rData.ItemArray[i - 1].ToString().Trim() != "" && rData.ItemArray[i - 1].ToString() != "&nbsp;")
                                pEntidad.saldo = Convert.ToDecimal(rData.ItemArray[i - 1].ToString().Trim());
                        }
                        if (i == 6)
                        {
                            if (rData.ItemArray[i - 1].ToString().Trim() != "" && rData.ItemArray[i - 1].ToString() != "&nbsp;")
                                pEntidad.nombre = Convert.ToString(rData.ItemArray[i - 1].ToString().Trim());
                        }
                    }
                    lstArchivo.Add(pEntidad);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            pError = "Se generó un error en la fila " + rowIndex.ToString() + " - " + ex.Message;
            return false;
        }
    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[BalanceServicio.CodigoProgramaoriginal + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        List<Xpinn.NIIF.Entities.BalanceNIIF> lstConsulta = new List<Xpinn.NIIF.Entities.BalanceNIIF>();
        gvLista.PageIndex = e.NewPageIndex;
        if (Session["FORMA"] != null)
        {
            if (Session["FORMA"].ToString() == "CARGA")
            {
                lstConsulta = (List<Xpinn.NIIF.Entities.BalanceNIIF>)ViewState["DTBALANCEGENERAL"];
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
            }
        }
        else
            Actualizar();
    }

    void DesabilitarText()
    {
        txtcod_cuenta.Enabled = false;
        txtsaldo.Enabled = false;
        txtnombre.Enabled = false;       
    }

    void limpiatext()
    {
        txtValor.Text = "";
        txtObserv.Text = "";
    }


    protected void btnModificar_Click1(object sender, EventArgs e)
    {
        Configuracion conf = new Configuracion();

        if (txtValor.Text.Trim() != "")
        {
            if (Convert.ToDecimal(txtValor.Text.Trim().Replace(".", "")) <= Convert.ToDecimal(txtsaldo.Text.Trim().Replace(".", "")))
            {
                if (ddlTipo.SelectedIndex != 0)
                {
                    lblMenError.Text = "";
                    lblMenError.Visible = false;
                    if (Session["FORMA"] != null)
                    {
                        if (Session["FORMA"].ToString() == "CARGA")
                        {
                            int Index = (int)ViewState["Index"];
                            List<Xpinn.NIIF.Entities.BalanceNIIF> lstConsulta = new List<Xpinn.NIIF.Entities.BalanceNIIF>();
                            lstConsulta = (List<Xpinn.NIIF.Entities.BalanceNIIF>)ViewState["DTBALANCEGENERAL"];

                            BalanceNIIF pEntidad = new BalanceNIIF();
                            pEntidad = lstConsulta[Index];
                            pEntidad.saldo = Convert.ToDecimal(txtValor.Text.Replace(".", ""));

                            ViewState["DTBALANCEGENERAL"] = lstConsulta;
                            gvLista.DataSource = lstConsulta;
                            gvLista.DataBind();
                        }
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                    }
                    else
                    {


                        string CodOrigen, Cod_Destino, Observacion;
                        decimal Valor;
                        int TipoAjuste;
                        Int64 CentroCosto = 1;

                        CodOrigen = txtcod_cuenta.Text;
                        TipoAjuste = Convert.ToInt32(ddlTipo.SelectedValue);
                        if (ddlTipo.SelectedItem.Text == "Reclasificación")
                        {
                            Cod_Destino = ddlCuenta_Dest.SelectedValue.ToString();
                        }
                        else
                        {
                            Cod_Destino = null;
                        }
                        Valor = Convert.ToDecimal(txtValor.Text.Replace(".", ""));
                        Observacion = txtObserv.Text;

                        string pError = string.Empty;
                        Boolean rpta = BalanceServicio.ReclasificarBalanceNIIF(CodOrigen, Cod_Destino, Valor, TipoAjuste, Observacion, Convert.ToDateTime(txtFechaCorte.Text), CentroCosto, ref pError, (Usuario)Session["usuario"]);
                        if (!string.IsNullOrEmpty(pError))
                        {
                            VerError(pError);
                            return;
                        }
                        //tipo = 1;
                        //BalanceServicio.ModificarFechaNIIF(Convert.ToDateTime(txtFechaCorte.Text), tipo, (Usuario)Session["usuario"]);
                        limpiatext();
                        Actualizar();
                        mpeVisualizarDatos.Hide();
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                    }
                }
                else
                {
                    lblMenError.Visible = true;
                    lblMenError.Text = ("Seleccione el tipo.");
                }
            }
            else
            {
                lblMenError.Visible = true;
                lblMenError.Text = ("Ingrese un valor menor al saldo actual.");
            }
        }
        else
        {
            lblMenError.Visible = true;
            lblMenError.Text = ("Ingrese el Valor.");
        }
    }


    protected void btnCloseReg1_Click1(object sender, EventArgs e)
    {
        mpeVisualizarDatos.Hide();
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && ViewState["DTBALANCEGENERAL"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.Columns[0].Visible = false;
                gvLista.DataSource = ViewState["DTBALANCEGENERAL"];
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
                Response.AddHeader("Content-Disposition", "attachment;filename=BalanceIniciacion.xls");
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

    protected void rblTipoCarga_SelectedIndexChanged(object sender, EventArgs e)
    {
        Boolean rpta = true;
        rpta = rblTipoCarga.SelectedIndex == 0 ? true : false;
        lblEstBalance.Visible = rpta;
        lblEstAjuste.Visible = !rpta;
    }

}