using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{

    xpinnWSLogin.Persona1 pPersona;
    xpinnWSDeposito.WSDepositoSoapClient BODeposito = new xpinnWSDeposito.WSDepositoSoapClient();
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient BOEstadoCuenta = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            ValidarSession();
            AdicionarTitulo("Renovación de Cdat", "L");
            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("PlanPagos", "Page_PreInit", ex);
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            panelGeneral.Visible = true;
            panelFinal.Visible = false;
            panelGrid.Visible = false;
            ViewState["DatosGrid"] = null;
            Actualizar();
        }
    }


    private void Actualizar()
    {
        try
        {
            VerError("");
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            List<xpinnWSDeposito.Cdat> lstCdats = new List<xpinnWSDeposito.Cdat>();
            string pFiltro = obtFiltro(pPersona);
            lstCdats = BODeposito.ListarCdats(pFiltro, DateTime.MinValue, pPersona.identificacion.Trim(), pPersona.clavesinecriptar, Session["sec"].ToString());
            if (lstCdats.Count > 0)
            {
                ViewState.Add("DatosGrid", lstCdats);
                panelGrid.Visible = true;
                gvLista.DataSource = lstCdats;
                gvLista.DataBind();
                lblInfo.Visible = false;
                lblTotReg.Visible = true;
                lblTotReg.Text = "<br/> Registros encontrados " + lstCdats.Count().ToString();
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotReg.Visible = false;
                panelGrid.Visible = false;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    private string obtFiltro(xpinnWSLogin.Persona1 pPersona)
    {
        string pFiltro = string.Empty;
        pFiltro += " AND C.ESTADO IN (1,2) AND T.COD_PERSONA = " + pPersona.cod_persona;
        pFiltro += " AND C.FECHA_VENCIMIENTO BETWEEN C.FECHA_VENCIMIENTO - NVL(BUSCARGENERAL(91,2),30) AND C.FECHA_VENCIMIENTO ";
        
        return pFiltro;
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex >= 0)
            {
                gvLista.PageIndex = e.NewPageIndex;
                if (ViewState["DatosGrid"] != null)
                {
                    List<xpinnWSDeposito.Cdat> lstConsulta = new List<xpinnWSDeposito.Cdat>();
                    lstConsulta = (List<xpinnWSDeposito.Cdat>)ViewState["DatosGrid"];
                    gvLista.DataSource = lstConsulta;
                    gvLista.DataBind();
                }
                else
                {
                    Actualizar();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected List<xpinnWSEstadoCuenta.ListaDesplegable> ListarLineas()
    {

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstLineas = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstLineas = BOEstadoCuenta.PoblarListaDesplegable("LINEACDAT", "COD_LINEACDAT, DESCRIPCION", "ESTADO = 1", "1", Session["sec"].ToString());
        return lstLineas.Count > 0 ? lstLineas : null;
    }


    private bool validarDatos()
    {
        int cantCheck = gvLista.Rows.OfType<GridViewRow>().Where(x => ((CheckBoxGrid)x.FindControl("chkSeleccion")).Checked).Count();
        if (cantCheck == 0)
        {
            VerError("No existen registros seleccionados para realizar la solicitud de renovación");
            return false;
        }
        else
        {
            bool rowSelect = gvLista.Rows.OfType<GridViewRow>()
                .Where(x => ((CheckBoxGrid)x.FindControl("chkSeleccion")).Checked)
                .Where(x => string.IsNullOrWhiteSpace(((TextBox)x.FindControl("txtPlazo")).Text))
                .Select(y => y).Any();

            if (rowSelect)
            {
                VerError("No se ingresó el plazo deseado en la fila seleccionada, verifique nuevamente los datos por favor");
                return false;
            }
        }

        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (!validarDatos())
            return;
        ctlMensaje.MostrarMensaje("Desea realizar la solicitud de renovación?");
    }


    private void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            List<xpinnWSDeposito.SolicitudRenovacion> lstRenovacion = new List<xpinnWSDeposito.SolicitudRenovacion>();
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBoxGrid chkSeleccion = (CheckBoxGrid)rFila.FindControl("chkSeleccion");
                if (chkSeleccion != null)
                {
                    if (chkSeleccion.Checked)
                    {
                        xpinnWSDeposito.SolicitudRenovacion pEntidad = new xpinnWSDeposito.SolicitudRenovacion();

                        Label lblFecVenc = (Label)rFila.FindControl("lblFecVenc");
                        DropDownListGrid ddlLinea = (DropDownListGrid)rFila.FindControl("ddlLinea");
                        TextBox txtPlazo = (TextBox)rFila.FindControl("txtPlazo");
                        TextBox txtObservacion = (TextBox)rFila.FindControl("txtObservacion");

                        pEntidad.idrenovacion = 0;
                        pEntidad.codigo_cdat = Convert.ToInt64(gvLista.DataKeys[rFila.RowIndex].Values[0].ToString());
                        if (!string.IsNullOrWhiteSpace(lblFecVenc.Text))
                            pEntidad.fecha_vencimiento = Convert.ToDateTime(lblFecVenc.Text);
                        else
                            pEntidad.fecha_vencimiento = null;
                        pEntidad.fecha_solicitud = DateTime.Now;
                        pEntidad.cod_lineacdat = ddlLinea.SelectedValue;
                        pEntidad.plazo = Convert.ToInt32(txtPlazo.Text);
                        pEntidad.observacion = !string.IsNullOrWhiteSpace(txtObservacion.Text) ? txtObservacion.Text : null;
                        pEntidad.estado = 1;
                        lstRenovacion.Add(pEntidad);
                    }
                }
            }

            if (lstRenovacion.Count > 0)
            {
                xpinnWSDeposito.RespuestaApp pResult = new xpinnWSDeposito.RespuestaApp();
                pResult = BODeposito.SolicitarRenovacionCdat(lstRenovacion, pPersona.identificacion, pPersona.clavesinecriptar, Session["sec"].ToString());
                if (pResult != null)
                {
                    if (pResult.rpta == false)
                    {
                        VerError(pResult.Mensaje);
                    }
                    else
                    {
                        Site toolBar = (Site)Master;
                        toolBar.MostrarGuardar(false);
                        panelGeneral.Visible = false;
                        panelFinal.Visible = true;
                        Session.Remove("rptaIngreso" + pPersona.identificacion);
                        toolBar.LoadNotications();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            VerError(ex.Message);            
        }
    }

    protected void btnInicio_Click(object sender, EventArgs e)
    {
        Navegar("~/Index.aspx");
    }

}