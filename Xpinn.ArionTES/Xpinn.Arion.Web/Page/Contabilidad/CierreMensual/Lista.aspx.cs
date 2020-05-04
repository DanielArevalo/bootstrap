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
using Xpinn.Contabilidad.Entities;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using Microsoft.CSharp;
using System.Text;
using System.IO;

partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.CierreMensualService CierreMensualSer = new Xpinn.Contabilidad.Services.CierreMensualService();
    static ArrayList ar = new ArrayList();
    Cierremensual datosApp = new Cierremensual();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CierreMensualSer.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CierreMensualSer.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarCombos();
                Actualizar();
                CargarValoresConsulta(pConsulta, CierreMensualSer.CodigoPrograma);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CierreMensualSer.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Método para llenar los DDLs requeridos para las consultas
    /// </summary>
    protected void LlenarCombos()
    {
        // Llenar el DDL de la fecha de corte 
        Configuracion conf = new Configuracion();
        List<Cierremensual> lstFechaCierre = new List<Cierremensual>();
        Xpinn.Contabilidad.Services.CierreMensualService CierremensualService = new Xpinn.Contabilidad.Services.CierreMensualService();
        Cierremensual CierremensualPrueba = new Cierremensual();
        lstFechaCierre = CierremensualService.ListarFechaCorte((Usuario)Session["Usuario"]);
        ddlFechaCorte.DataSource = lstFechaCierre;
        ddlFechaCorte.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataBind();

    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, CierreMensualSer.CodigoPrograma);
        Guardar(idObjeto);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, CierreMensualSer.CodigoPrograma);
        LlenarCombos();
        Actualizar();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CierreMensualSer.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[CierreMensualSer.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[CierreMensualSer.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
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
            BOexcepcion.Throw(CierreMensualSer.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            // Generar consulta de centros de costo
            // LLenar el Gridview con centros de costo
            Xpinn.Contabilidad.Services.CentroCostoService CentroCostoService = new Xpinn.Contabilidad.Services.CentroCostoService();
            List<CentroCosto> LstCentroCosto = new List<CentroCosto>();
            string sFiltro = "";
            LstCentroCosto.Clear();
            LstCentroCosto = CentroCostoService.ListarCentroCosto((Usuario)Session["usuario"], sFiltro);
            gvLista.EmptyDataText = emptyQuery;            
            gvLista.DataSource = LstCentroCosto;
            if (LstCentroCosto.Count > 0)
            {
                mvCierreMensual.ActiveViewIndex = 0;
                gvLista.DataBind();
            }
            else
            {
                mvCierreMensual.ActiveViewIndex = -1;
            }
            Session.Add(CierreMensualSer.CodigoPrograma + ".consulta", 1);

        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(CierreMensualSer.CodigoPrograma, "Actualizar", ex);
        }
    }

    private void Guardar(String pIdObjeto)
    {
        try
        {
            CheckBox chkseleccionar;
            Label lblcodigo;
            Label lblmensaje;
            Cierremensual datosApp = new Cierremensual();

            Configuracion conf = new Configuracion();
            String format = conf.ObtenerFormatoFecha();
            datosApp.fecha = DateTime.ParseExact(ddlFechaCorte.SelectedValue, format, CultureInfo.InvariantCulture);

            if (RadioButtonList1.SelectedItem.Value == "Definitivo")
            {
                RadioButtonList1.SelectedItem.Value = "D";
            }
            if (RadioButtonList1.SelectedItem.Value == "Prueba")
            {
                RadioButtonList1.SelectedItem.Value = "P";
            }
            datosApp.estado = RadioButtonList1.SelectedItem.Value;
            if (chkcierreporterceros.Checked == true)
            {
                datosApp.terceros = 1;
            }
            else
            {
                datosApp.terceros = 0;
            }

            foreach (GridViewRow wrow in gvLista.Rows)
            {

                chkseleccionar = (CheckBox)wrow.FindControl("chkSeleccionarcentro");
                lblcodigo = (Label)wrow.FindControl("lblcodigo");
                lblmensaje = (Label)wrow.FindControl("lblmensaje");
                datosApp.centro_costo = Convert.ToInt64(lblcodigo.Text);
                if (chkseleccionar.Checked)
                {
                    Cierremensual lstConsultaCierreMensual = new Cierremensual();
                    try
                    {
                        lstConsultaCierreMensual = CierreMensualSer.CrearCierreMensual(datosApp, (Usuario)Session["usuario"]);
                        lblmensaje.Text = "Cierre Mensual Terminado Correctamente";
                    }
                    catch (Exception ex)
                    {
                        int n = 1;
                        if (ex.Message.Contains("ORA-20101:"))                        
                            n = ex.Message.IndexOf("ORA-20101:") + 10;
                        if (n > 0)
                        {
                            lblmensaje.Text = ex.Message.Substring(n, ex.Message.Length - n);
                        }
                        else
                        {
                            lblmensaje.Text = ex.Message;
                        }
                    }
                }
            }
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CierreMensualSer.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Util.Usuario ObtenerValores()
    {
        Xpinn.Util.Usuario vUsuario = new Xpinn.Util.Usuario();

        return vUsuario;
    }


    protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ch = (CheckBox)(sender);
        if (ch.Checked)
        {
            foreach (GridViewRow gv in gvLista.Rows)
            {
                CheckBox chi = (CheckBox)(gv.FindControl("chkSeleccionarcentro"));
                chi.Checked = true;
                ar.Add(gv.Cells[1].Text);
            }
        }
        else
        {
            foreach (GridViewRow gv in gvLista.Rows)
            {
                CheckBox chi = (CheckBox)(gv.FindControl("chkSeleccionarcentro"));
                chi.Checked = false;
                ar.Remove(gv.Cells[1].Text);
            }

        }
    }


}