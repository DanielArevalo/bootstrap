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
using Xpinn.NIIF.Services;
using Xpinn.NIIF.Entities;
using System.IO;
using System.Text;

partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
    Xpinn.Contabilidad.Services.BalanceGeneralService BalanceGeneral = new Xpinn.Contabilidad.Services.BalanceGeneralService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            Site toolBar = (Site)this.Master;
            toolBar.eventoExportar += btnExportar_Click;
            if (Request.QueryString["modificar"] != null)
            {
                toolBar.MostrarExportar(false);
                if (Request.QueryString["modificar"].ToString().Trim() == "1")
                {
                    PlanCuentasServicio.CodigoPrograma = PlanCuentasServicio.CodigoProgramaModif;
                    toolBar.MostrarImportar(false);
                }
            }

            VisualizarOpciones(PlanCuentasServicio.CodigoPrograma, "L");
           
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImportar += btnImportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session[Usuario.codusuario + "DT_PLANCTAS"] = null;
                CargaFormatosFecha();
                Session["lstData"] = null;
                Session["lstDataBalance"] = null;
                rblTipoCarga_SelectedIndexChanged(rblTipoCarga, null);
                CargarValoresConsulta(pConsulta, PlanCuentasServicio.CodigoPrograma);
                if (Session[PlanCuentasServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(PlanCuentasServicio.CodigoPrograma + ".id");
        Session.Remove(PlanCuentasServicio.CodigoPrograma + ".id");
        Session.Remove(PlanCuentasServicio.CodigoProgramaModif + ".id");
        GuardarValoresConsulta(pConsulta, PlanCuentasServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (mvPrincipal.ActiveViewIndex == 0)
        {
            GuardarValoresConsulta(pConsulta, PlanCuentasServicio.CodigoPrograma);
            Actualizar();
        }
        else
        {
            Session["lstData"] = null;
            Session["lstDataBalance"] = null;
            mvPrincipal.ActiveViewIndex = 0;
            Site toolBar = (Site)Master;
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarImportar(true);
            toolBar.MostrarGuardar(false);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (mvPrincipal.ActiveViewIndex == 0)
            LimpiarValoresConsulta(pConsulta, PlanCuentasServicio.CodigoPrograma);
        else
            LimpiarDataImportacion();
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id;
        id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[PlanCuentasServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id;
        id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();        

        //String tipo = cbNif.Checked ? "N" : "L";
        Session[PlanCuentasServicio.CodigoPrograma + ".id"] = id;
        Session[PlanCuentasServicio.CodigoProgramaModif + ".id"] = id;
        //Session[PlanCuentasServicio.CodigoProgramaModif + ".tipo"] = tipo;
        e.NewEditIndex = -1;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        VerError("");
        try
        {
            string id = e.Keys[0].ToString();
            if (id.Trim() != "")
                try
                {
                    PlanCuentasServicio.EliminarPlanCuentas(id, (Usuario)Session["usuario"]);
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                }
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
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
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Contabilidad.Entities.PlanCuentas> lstConsulta = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
            string filtro = obtFiltro();
            lstConsulta = PlanCuentasServicio.ListarPlanCuentasLocal(ObtenerValores(), (Usuario)Session["usuario"], filtro);
            /*
            if (cbLocal.Checked)
            {
                
            }
            else if (cbNif.Checked)
            {
                lstConsulta = PlanCuentasServicio.ListarPlanCuentasNif(ObtenerValores(), (Usuario)Session["usuario"], filtro);                                    
            }
            else if (cbAmbos.Checked)
            {
                lstConsulta = PlanCuentasServicio.ListarPlanCuentasAmbos(ObtenerValores(), (Usuario)Session["usuario"], filtro);
            }
            else
            {
                lstConsulta = PlanCuentasServicio.ListarPlanCuentas(ObtenerValores(), (Usuario)Session["usuario"], filtro);
            }*/
                
            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Session[Usuario.codusuario + "DT_PLANCTAS"] = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(PlanCuentasServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    public string obtFiltro()
    { 
        string filtro = "";
        if (txtCodCuenta.Text != "")
            filtro += " cod_cuenta like '"+txtCodCuenta.Text+"%'";            
        return filtro;
    }

    private Xpinn.Contabilidad.Entities.PlanCuentas ObtenerValores()
    {
        Xpinn.Contabilidad.Entities.PlanCuentas vPlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        if (txtNombre.Text.Trim() != "")
            vPlanCuentas.nombre = Convert.ToString(txtNombre.Text.Trim());
        if (ddlDepende.Text.Trim() != "")
            vPlanCuentas.depende_de = ddlDepende.SelectedValue;
        
        if (txtNivel.Text.Trim() != "")
            vPlanCuentas.nivel = Convert.ToInt64(txtNivel.Text.Trim());        
        if (ddlEstado.Text.Trim() != "")
            vPlanCuentas.estado = Convert.ToInt64(ddlEstado.SelectedValue.ToString());
        if (chkTerceros.Checked == true)
            vPlanCuentas.maneja_ter = 1;
        if (chkCentroCosto.Checked == true)
            vPlanCuentas.maneja_cc = 1;
        if (chkCentroGestion.Checked == true)
            vPlanCuentas.maneja_sc = 1;
        if (chkImpuestos.Checked == true)
            vPlanCuentas.impuesto = 1;
        if (chkGiro.Checked == true)
            vPlanCuentas.maneja_gir = 1;
        if (ddlTipo.SelectedIndex != 0)
            vPlanCuentas.tipo = ddlTipo.SelectedValue;
        return vPlanCuentas;
    }

    /*
    protected void cbLocal_CheckedChanged(object sender, EventArgs e)
    {
        if (cbLocal.Checked)
        {
            cbNif.Checked = false;
            cbAmbos.Checked = false;
        }
    }

    protected void cbNif_CheckedChanged(object sender, EventArgs e)
    {
        if (cbNif.Checked)
        {
            cbLocal.Checked = false;
            cbAmbos.Checked = false;
        }
    }

    protected void cbAmbos_CheckedChanged(object sender, EventArgs e)
    {
        if (cbAmbos.Checked)
        {
            cbLocal.Checked = false;
            cbNif.Checked = false;
        }
    }
    */

    #region CODIGO DE EXPORTACION 

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        if (gvLista.Rows.Count > 0)
            ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=PlanCuentasLocal.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        List<Xpinn.Contabilidad.Entities.PlanCuentas> lstPlanCuenta = null;
        if (Session[Usuario.codusuario + "DT_PLANCTAS"] != null)
            lstPlanCuenta = (List<Xpinn.Contabilidad.Entities.PlanCuentas>)Session[Usuario.codusuario + "DT_PLANCTAS"];
        sw = expGrilla.ObtenerGrilla(GridView1, lstPlanCuenta);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    #endregion


    #region CODIGO DE IMPORTACION

    void CargaFormatosFecha()
    {
        ddlFormatoFecha.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
        ddlFormatoFecha.Items.Insert(1, new ListItem("dd/MM/yyyy", "dd/MM/yyyy"));
        ddlFormatoFecha.Items.Insert(2, new ListItem("yyyy/MM/dd", "yyyy/MM/dd"));
        ddlFormatoFecha.Items.Insert(3, new ListItem("MM/dd/yyyy", "MM/dd/yyyy"));
        ddlFormatoFecha.Items.Insert(4, new ListItem("ddMMyyyy", "ddMMyyyy"));
        ddlFormatoFecha.Items.Insert(5, new ListItem("yyyyMMdd", "yyyyMMdd"));
        ddlFormatoFecha.Items.Insert(6, new ListItem("MMddyyyy", "MMddyyyy"));
        ddlFormatoFecha.SelectedIndex = 0;
        ddlFormatoFecha.DataBind();
    }


    void LimpiarDataImportacion()
    {
        panelPlanCta2.Visible = false;
        panelBalance2.Visible = false;
        pErrores.Visible = false;
        gvDatos.DataSource = null;
        ucFecha.Text = DateTime.Now.ToShortDateString();
        ddlFormatoFecha.SelectedIndex = 0;
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(false);
    }


    protected void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        mvPrincipal.ActiveViewIndex = 1;       
        Site toolBar = (Site)Master;
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarImportar(false);
        LimpiarDataImportacion();
    }


    protected void btnCargarCuentas_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            string error = "";
            if (ddlFormatoFecha.SelectedIndex == 0)
            {
                VerError("Seleccione el tipo de fecha que se carga en el archivo.");
                ddlFormatoFecha.Focus();
                return;
            }
            if (ucFecha.Text == "")
            {
                VerError("Ingrese la fecha de carga");
                ucFecha.Focus();
                return;
            }
            if (rblTipoCarga.SelectedItem == null)
            {
                VerError("Seleccione un tipo de carga, verifique los datos.");
                return;
            }
            if (fupArchivoPersona.HasFile)
            {
                Stream stream = fupArchivoPersona.FileContent;
                List<Xpinn.Contabilidad.Entities.ErroresCargaContabil> plstErrores = new List<Xpinn.Contabilidad.Entities.ErroresCargaContabil>();
                List<Xpinn.Contabilidad.Entities.PlanCuentas> lstPlanCta = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
                List<Xpinn.Contabilidad.Entities.BalanceGeneral> lstBalance = new List<Xpinn.Contabilidad.Entities.BalanceGeneral>();

                //LLAMANDO AL METODO DE CAPTURA DE DATOS
                PlanCuentasServicio.CargaPlanCuentasYbalance(ref error, Convert.ToInt32(rblTipoCarga.SelectedValue), ddlFormatoFecha.SelectedValue, stream, ref lstPlanCta, ref lstBalance, ref plstErrores, (Usuario)Session["usuario"]);

                if (error.Trim() != "")
                {
                    VerError(error);
                    return;
                }
                if (plstErrores.Count() > 0)
                {
                    pErrores.Visible = true;
                    gvErrores.DataSource = plstErrores;
                    gvErrores.DataBind();
                    cpeDemo1.CollapsedText = "(Click Aqui para ver " + plstErrores.Count() + " errores...)";
                    cpeDemo1.ExpandedText = "(Click Aqui para ocultar listado de errores...)";
                }
                panelPlanCta2.Visible = false;
                panelBalance2.Visible = false;
                if (rblTipoCarga.SelectedValue == "1")
                {
                    if (lstPlanCta.Count > 0)
                    {
                        Session["lstData"] = lstPlanCta;
                        panelPlanCta2.Visible = true;
                        //CARGAR DATOS A GRILLA DE NATURALES
                        gvDatos.DataSource = lstPlanCta;
                        gvDatos.DataBind();
                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarGuardar(true);
                    }
                }
                else
                {
                    if (lstBalance.Count > 0)
                    {
                        Session["lstDataBalance"] = lstBalance; 
                        panelBalance2.Visible = true;
                        gvBalance.DataSource = lstBalance;
                        gvBalance.DataBind();
                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarGuardar(true);
                    }
                }

            }
            else
            {
                VerError("Seleccione el archivo a cargar, verifique los datos.");
                return;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "btnCargarAportes_Click", ex);
        }
    }

    protected void gvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            List<Xpinn.Contabilidad.Entities.PlanCuentas> lstPlanCta = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
            lstPlanCta = (List<Xpinn.Contabilidad.Entities.PlanCuentas>)Session["lstData"];

            lstPlanCta.RemoveAt((gvDatos.PageIndex * gvDatos.PageSize) + e.RowIndex);

            gvDatos.DataSourceID = null;
            gvDatos.DataBind();

            gvDatos.DataSource = lstPlanCta;
            gvDatos.DataBind();
            Session["lstData"] = lstPlanCta;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "gvDatos_RowDeleting", ex);
        }
    }

    protected void gvBalance_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            List<Xpinn.Contabilidad.Entities.BalanceGeneral> lstBalance = new List<Xpinn.Contabilidad.Entities.BalanceGeneral>();
            lstBalance = (List<Xpinn.Contabilidad.Entities.BalanceGeneral>)Session["lstDataBalance"];

            lstBalance.RemoveAt((gvBalance.PageIndex * gvBalance.PageSize) + e.RowIndex);

            gvBalance.DataSourceID = null;
            gvBalance.DataBind();

            gvBalance.DataSource = lstBalance;
            gvBalance.DataBind();
            Session["lstDataBalance"] = lstBalance;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "gvBalance_RowDeleting", ex);
        }
    }


    Boolean ValidarData()
    {
        if (ucFecha.Text == "")
        {
            VerError("Ingrese la fecha de carga");
            ucFecha.Focus();
            return false;
        }
        if (gvDatos.Rows.Count <= 0)
        {
            VerError("No existen datos por registrar, verifique los datos.");
            return false;
        }
        if (rblTipoCarga.SelectedValue == "1")
        {
            if (Session["lstData"] == null)
            {
                VerError("No existen datos por registrar, verifique los datos.");
                return false;
            }
        }
        else
        {
            if (Session["lstDataBalance"] == null)
            {
                VerError("No existen datos por registrar, verifique los datos.");
                return false;
            }
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarData())
                ctlMensaje.MostrarMensaje("Desea realizar la grabación de datos?");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            List<Xpinn.Contabilidad.Entities.PlanCuentas> lstPlanCta = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
            lstPlanCta = (List<Xpinn.Contabilidad.Entities.PlanCuentas>)Session["lstData"];
            List<Xpinn.Contabilidad.Entities.BalanceGeneral> lstBalance = new List<Xpinn.Contabilidad.Entities.BalanceGeneral>();
            lstBalance = (List<Xpinn.Contabilidad.Entities.BalanceGeneral>)Session["lstDataBalance"];

            string pError = "";
            PlanCuentasServicio.CrearPlanBalanceImportacion(ucFecha.ToDateTime, ref pError, Convert.ToInt32(rblTipoCarga.SelectedValue), lstPlanCta,lstBalance, (Usuario)Session["usuario"]);
            if (pError != "")
            {
                VerError(pError);
                return;
            }
            mvPrincipal.ActiveViewIndex = 2;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarLimpiar(false);
            Session.Remove("lstData");
            Session.Remove("lstDataBalance");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }


    protected void rblTipoCarga_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblTipoCarga.SelectedValue == "1")
        {
            panelPlanCta.Visible = true;
            panelBalance.Visible = false;
        }
        else
        {
            panelPlanCta.Visible = false;
            panelBalance.Visible = true;
        }
    }


    #endregion

}