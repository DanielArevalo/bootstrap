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
    Xpinn.NIIF.Services.BalanceNIIFService _cierreMensualSer = new Xpinn.NIIF.Services.BalanceNIIFService();
    static ArrayList ar = new ArrayList();
    Xpinn.NIIF.Entities.BalanceNIIF datosApp = new Xpinn.NIIF.Entities.BalanceNIIF();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_cierreMensualSer.CodigoProgramaCierreNiif, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cierreMensualSer.CodigoProgramaCierreNiif, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                LlenarCombos();
                Actualizar();
                VerificarComprobantesYCuentasNIIF();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_cierreMensualSer.CodigoProgramaCierreNiif, "Page_Load", ex);
        }
    }

    void VerificarComprobantesYCuentasNIIF()
    {
        if (!string.IsNullOrWhiteSpace(ddlFechaCorte.SelectedValue))
        {
            DateTime dateCorte = Convert.ToDateTime(ddlFechaCorte.SelectedValue);

            if (dateCorte != DateTime.MinValue)
            {
                string mensajeDescuadre = _cierreMensualSer.VerificarComprobantesYCuentasNIIF(dateCorte, _usuario);

                if (!string.IsNullOrWhiteSpace(mensajeDescuadre))
                {
                    VerError(mensajeDescuadre);
                }
            }
        }
    }

    /// <summary>
    /// Método para llenar los DDLs requeridos para las consultas
    /// </summary>
    protected void LlenarCombos()
    {
        Xpinn.Confecoop.Services.ConfecoopService ConfecoopServ = new Xpinn.Confecoop.Services.ConfecoopService();
        ddlFechaCorte.DataSource = ConfecoopServ.ListarFechaCierre(_usuario);
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataTextFormatString = "{0:" + gFormatoFecha + "}";
        ddlFechaCorte.DataValueField = "fecha";
        ddlFechaCorte.DataBind();
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, _cierreMensualSer.CodigoProgramaCierreNiif);
        Guardar(idObjeto);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, _cierreMensualSer.CodigoProgramaCierreNiif);
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
            BOexcepcion.Throw(_cierreMensualSer.CodigoProgramaCierreNiif + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[_cierreMensualSer.CodigoProgramaCierreNiif + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[_cierreMensualSer.CodigoProgramaCierreNiif + ".id"] = id;
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
            BOexcepcion.Throw(_cierreMensualSer.CodigoProgramaCierreNiif, "gvLista_PageIndexChanging", ex);
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
            LstCentroCosto = CentroCostoService.ListarCentroCosto(_usuario, sFiltro);
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = LstCentroCosto;
            if (LstCentroCosto.Count > 0)
            {
                mvCierreMensual.ActiveViewIndex = 0;
                gvLista.DataBind();
                foreach (GridViewRow gv in gvLista.Rows)
                {
                    CheckBox chi = (CheckBox)(gv.FindControl("chkSeleccionarcentro"));
                    chi.Checked = true;
                    ar.Add(gv.Cells[1].Text);
                }
            }
            else
            {
                mvCierreMensual.ActiveViewIndex = -1;
            }
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(_cierreMensualSer.CodigoProgramaCierreNiif, "Actualizar", ex);
        }
    }

    private void Guardar(String pIdObjeto)
    {
        try
        {
            CheckBox chkseleccionar;
            Label lblcodigo;
            Label lblmensaje;
            Xpinn.NIIF.Entities.BalanceNIIF datosApp = new Xpinn.NIIF.Entities.BalanceNIIF();

            Configuracion conf = new Configuracion();
            String format = conf.ObtenerFormatoFecha();
            datosApp.fecha = DateTime.ParseExact(ddlFechaCorte.SelectedItem.Text, format, CultureInfo.InvariantCulture);
            datosApp.estado = RadioButtonList1.SelectedItem.Value;
            datosApp.tipo = "1";  // Esto se coloco porque no estaba haciendo el cierre por terceros
            foreach (GridViewRow wrow in gvLista.Rows)
            {
                chkseleccionar = (CheckBox)wrow.FindControl("chkSeleccionarcentro");
                lblcodigo = (Label)wrow.FindControl("lblcodigo");
                lblmensaje = (Label)wrow.FindControl("lblmensaje");
                datosApp.centro_costo = Convert.ToInt32(lblcodigo.Text);
                if (chkseleccionar.Checked)
                {
                    Xpinn.NIIF.Entities.BalanceNIIF lstConsultaCierreMensual = new Xpinn.NIIF.Entities.BalanceNIIF();
                    try
                    {
                        lstConsultaCierreMensual = _cierreMensualSer.CrearCierreMensual(datosApp, _usuario);
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
            BOexcepcion.Throw(_cierreMensualSer.CodigoProgramaCierreNiif, "Actualizar", ex);
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