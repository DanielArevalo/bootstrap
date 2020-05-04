using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;


public partial class General_Controles_ctlCuotasExtras : UserControl

{
    #region Metodo inicial y variables
    private String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    private Persona1Service DatosClienteServicio = new Persona1Service();
    private List<Persona1> lstDatosSolicitud = new List<Persona1>();  //Lista de los menus desplegables    
    private DatosSolicitudService DatosSolicitudServicio = new DatosSolicitudService();
    CuotasExtrasService CuoExtServicio = new CuotasExtrasService();
    List<CuotasExtras> lstConsulta = new List<CuotasExtras>();
    private int _Contador = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #endregion

    #region Propiedades

    public string Monto
    {
        get { return txtMonto.Text.Trim().ToUpper(); }
        set { txtMonto.Text = value; }
    }
    public string Periodicidad
    {
        get { return txtCodPeriodicidad.Text.Trim().ToUpper(); }
        set { txtCodPeriodicidad.Text = value; }
    }
    public string PlazoTxt
    {
        get { return txtPlazoTxt.Text.Trim().ToUpper(); }
        set { txtPlazoTxt.Text = value; }
    }

    #endregion

    #region Metodo tabla
    public void gvCuoExt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            return;
        }
        //Footer
        Control ctrl = null;
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            ctrl = e.Row.FindControl("ddlformapago");
            if (ctrl != null)
            {
                DropDownList ddlformapago = ctrl as DropDownList;
            }
            ctrl = e.Row.FindControl("ddltipocuotagv");
            if (ctrl != null)
            {
                DropDownList ddltipocuota = ctrl as DropDownList;
                ListaSolicitada = "TipoCuotaExtra";
                TraerResultadosLista();
                ddltipocuota.DataSource = lstDatosSolicitud;
                ddltipocuota.DataTextField = "ListaDescripcion";
                ddltipocuota.DataValueField = "ListaIdStr";
                ddltipocuota.DataBind();
            }
            return;
        }
        //
        if (_Contador < lstConsulta.Count)
        {
            if (panelCuotasExtrasEdit.Visible)
            {
                ctrl = e.Row.FindControl("ddltipocuotagvs");
                Control ctrls = e.Row.FindControl("lblvalor");
                Control ctrlsq = e.Row.FindControl("ddlformapago");
                if (ctrl != null)
                {
                    DropDownList ddltipocuota = ctrl as DropDownList;
                    ListaSolicitada = "TipoCuotaExtra";
                    TraerResultadosLista();
                    ddltipocuota.DataSource = lstDatosSolicitud;
                    ddltipocuota.DataTextField = "ListaDescripcion";
                    ddltipocuota.DataValueField = "ListaIdStr";
                    ddltipocuota.DataBind();

                    try
                    {
                        string[] number = lstConsulta[_Contador].des_tipo_cuota.Split('-');
                        if (!string.IsNullOrEmpty(number[0]))
                        {
                            ddltipocuota.SelectedValue = number[0];
                            ddltipocuota.DataBind();
                        }

                    }
                    catch { }
                }
                if (ctrlsq != null)
                {
                    DropDownList ddlFormaPago = ctrlsq as DropDownList;
                    string number = lstConsulta[_Contador].forma_pago;
                    if (!string.IsNullOrEmpty(number))
                    {
                        ddlFormaPago.SelectedValue = number;
                        ddlFormaPago.DataBind();
                    }
                }
                if (ctrls != null)
                {
                    TextBox lblvalor = ctrls as TextBox;
                    if (lblvalor != null)
                    {
                        lblvalor.Text = null;
                        lblvalor.Text = Convert.ToString(lstConsulta[_Contador].valor);
                        lblvalor.DataBind();
                    }

                }
                _Contador++;
            }

        }
    }

    public void gvCuoExt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lstConsulta = (List<CuotasExtras>)Session["CuoExt"];
        if (lstConsulta.Count >= 1)
        {
            CuotasExtras eCuoExt = new CuotasExtras();
            int index = Convert.ToInt32(e.RowIndex);
            eCuoExt = lstConsulta[index];
            if (eCuoExt.valor != 0 || eCuoExt.valor == null)
            {
                decimal total = 0;
                if (Session["TotalCuoExt"] != null)
                    if (Session["TotalCuoExt"].ToString().Trim() != "")
                        total = Convert.ToDecimal(Session["TotalCuoExt"].ToString());
                if (total != 0)
                {
                    total = total - Convert.ToDecimal(eCuoExt.valor);
                    Session["TotalCuoExt"] = total;
                }
                lstConsulta.Remove(eCuoExt);
            }
        }
        e.Cancel = true;
        if (lstConsulta.Count <= 0)
        {
            CuotasExtras eCuoExt = new CuotasExtras();
            lstConsulta.Add(eCuoExt);
        }
        GridView _gvCuoExt = (GridView)sender;
        if (lstConsulta.Select(x => x.cod_cuota).FirstOrDefault() <= 0)
        {
            _gvCuoExt.DataSource = lstConsulta;
            _gvCuoExt.DataBind();
            Session["CuoExt"] = lstConsulta;
        }
        else
        {
            _gvCuoExt.DataSource = lstConsulta;
            _gvCuoExt.DataBind();
            Session["CuoExt"] = lstConsulta;
        }
    }

    public void gvCuoExt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var cadena = HttpContext.Current.Request.Url.AbsoluteUri;
        string Page = cadena.Split('/').Last();
        if (e.CommandName.Equals("AddNew"))
        {
            GridView _gvCuoExt = (GridView)sender;
            if (_gvCuoExt.FooterRow == null)
            {
                return;
            }
            TextBox txtfechapago = (TextBox)_gvCuoExt.FooterRow.FindControl("txtfechapago");
            DropDownList ddlformapago = (DropDownList)_gvCuoExt.FooterRow.FindControl("ddlformapago");
            TextBox txtvalor = (TextBox)_gvCuoExt.FooterRow.FindControl("txtvalor");
            DropDownList dlltipocuota = (DropDownList)_gvCuoExt.FooterRow.FindControl("ddltipocuotagv");

            List<CuotasExtras> lstCuoExt = new List<CuotasExtras>();
            lstCuoExt = (List<CuotasExtras>)Session["CuoExt"];

            if (lstCuoExt.Count == 1)
            {
                CuotasExtras gItem = new CuotasExtras();
                gItem = lstCuoExt[0];
                if (gItem.valor == 0 || gItem.valor == null)
                    lstCuoExt.Remove(gItem);
            }

            if (ValidaMonto(_gvCuoExt))
            {
                CuotasExtras gItemNew = new CuotasExtras();
                if (txtfechapago.Text.Trim() == "" || txtvalor.Text.Trim() == "")
                {
                    return;
                }
                gItemNew.fecha_pago = Convert.ToDateTime(txtfechapago.Text);
                gItemNew.forma_pago = ddlformapago.SelectedValue.ToString();
                gItemNew.des_forma_pago = ddlformapago.SelectedItem.ToString();
                gItemNew.valor = Convert.ToInt64(txtvalor.Text);
                gItemNew.tipo_cuota = Convert.ToInt32(dlltipocuota.SelectedValue);
                gItemNew.des_tipo_cuota = "-" + dlltipocuota.SelectedItem.ToString();
                lstCuoExt.Add(gItemNew);
                decimal total = Convert.ToDecimal(Session["TotalCuoExt"].ToString());
                total = total + Convert.ToDecimal(gItemNew.valor);
                Session["TotalCuoExt"] = total;
                lstConsulta = lstCuoExt;
                _gvCuoExt.DataSource = lstCuoExt;
                _gvCuoExt.DataBind();
                Session["CuoExt"] = lstCuoExt;
            }
        }
        if (e.CommandName.Equals("Delete"))
        {
            return;
        }
        var numero_radicado = Convert.ToString(Session["Numero_Radicacion"]);
        if (!string.IsNullOrEmpty(numero_radicado))
        {
            GuardarCuotas(Session["Numero_Radicacion"].ToString());
            Response.Redirect(Page);
        }
    }

    public void gvCuoExt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCuoExt.PageIndex = e.NewPageIndex;
        }
        catch (Exception ex)
        {
            //Ignore
        }
    }
    public void btnGenerarCuotaext_Click(object sender, EventArgs e)
    {
        List<CuotasExtras> lstCuoExt = new List<CuotasExtras>();
        lstCuoExt = (List<CuotasExtras>)Session["CuoExt"];
        if (lstCuoExt.Count == 1)
        {
            CuotasExtras gItem = new CuotasExtras();
            gItem = lstCuoExt[0];
            if (gItem.valor == 0 || gItem.valor == null)
                lstCuoExt.Remove(gItem);
        }
        if (txtPorcentaje.Text == "")
        {
            lblError.Text = "Debe digitar el porcentaje de cuota extra";
            return;
        }
        else if (txtNumeroCuotaExt.Text == "")
        {
            lblError.Text = "Debe digitar el numero de cuotas extras";
            return;
        }
        else if (txtFechaCuotaExt.Text == "")
        {
            lblError.Text = "Debe digitar la fecha inicial de cuota extra";
            return;
        }
        else if (txtValorCuotaExt.Text == "")
        {
            lblError.Text = "Debe digitar el valor de cuota extra";
            return;
        }

        lblError.Text = "";
        if (ValidaMonto(gvCuoExt))
        {
            int total_cuota = Convert.ToInt32(txtNumeroCuotaExt.Text);
            int dias_inclemento = 0;
            for (int i = 1; i <= total_cuota; i++)
            {
                CuotasExtras gItemNew = new CuotasExtras();
                gItemNew.fecha_pago = Convert.ToDateTime(txtFechaCuotaExt.Text);
                if (i > 1)
                {
                    dias_inclemento = dias_inclemento + Convert.ToInt32(ddlPeriodicidadCuotaExt.SelectedValue);
                    Xpinn.Comun.Services.FechasService fechaServicio = new Xpinn.Comun.Services.FechasService();
                    gItemNew.fecha_pago = fechaServicio.FecSumDia(Convert.ToDateTime(gItemNew.fecha_pago), dias_inclemento, 1);
                }
                gItemNew.forma_pago = ddlFormaPagoCuotaExt.SelectedValue;
                gItemNew.des_forma_pago = ddlFormaPagoCuotaExt.SelectedItem.ToString();
                gItemNew.valor = Convert.ToInt64(txtValorCuotaExt.Text);
                gItemNew.des_tipo_cuota = "-" + ddlCuotaExtTipo.SelectedItem;
                gItemNew.tipo_cuota = Convert.ToInt32(ddlCuotaExtTipo.SelectedValue);
                lstCuoExt.Add(gItemNew);
                decimal total = Convert.ToDecimal(Session["TotalCuoExt"].ToString());
                total = total + Convert.ToDecimal(gItemNew.valor);
                Session["TotalCuoExt"] = total;
                gvCuoExt.DataSource = lstCuoExt;
                gvCuoExt.DataBind();
                Session["CuoExt"] = lstCuoExt;
            }
        }
    }
    public void btnLimpiarCuotaext_Click(object sender, EventArgs e)
    {
        List<CuotasExtras> lstCuoExt = new List<CuotasExtras>();
        gvCuoExt.DataSource = lstCuoExt;
        gvCuoExt.DataBind();
        Session["CuoExt"] = lstCuoExt;
        Session["TotalCuoExt"] = 0;
        InicialCuoExt();
    }
    #endregion

    #region Metodos Externos

    private void TraerResultadosLista()
    {
        if (ListaSolicitada == null)
            return;
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }
    public void InicialCuoExt()
    {
        panelCuotasExtras.Visible = true;
        panelCuotasExtrasEdit.Visible = false;
        List<CuotasExtras> lstConsulta = new List<CuotasExtras>();
        CuotasExtras eCuoExt = new CuotasExtras();
        lstConsulta.Add(eCuoExt);
        Session["CuoExt"] = lstConsulta;
        gvCuoExt.DataSource = lstConsulta;
        gvCuoExt.DataBind();
        gvCuoExt.Visible = true;

        ListaSolicitada = "TipoCuotaExtra";
        TraerResultadosLista();
        ddlCuotaExtTipo.DataSource = lstDatosSolicitud;
        ddlCuotaExtTipo.DataTextField = "ListaDescripcion";
        ddlCuotaExtTipo.DataValueField = "ListaIdStr";
        ddlCuotaExtTipo.DataBind();

        ListaSolicitada = "PeriodicidadCuotaExt";
        TraerResultadosLista();
        ddlPeriodicidadCuotaExt.DataSource = lstDatosSolicitud;
        ddlPeriodicidadCuotaExt.DataTextField = "ListaDescripcion";
        ddlPeriodicidadCuotaExt.DataValueField = "ListaId";
        ddlPeriodicidadCuotaExt.DataBind();

    }

    public bool ValidaMonto(GridView pGridView)
    {
        DropDownList ddlperiod = new DropDownList();
        ddlperiod = ddlPeriodicidadCuotaExt;
        TextBox valor = (TextBox)pGridView.FooterRow.FindControl("txtvalor");
        if (string.IsNullOrEmpty(txtCodPeriodicidad.Text))
        {
            lblError.Text = @"No ha escogido periodicidad del crédito.";
            return false;
        }
        if (string.IsNullOrEmpty(txtMonto.Text))
        {
            lblError.Text = @"No hay monto solicitado del credito.";
            return false;
        }
        if (string.IsNullOrEmpty(txtPlazoTxt.Text))
        {
            lblError.Text = @"La solicitud no tiene plazo estimado.";
            return false;
        }
        int valor_diasPeriodicidad = 0;
        try { valor_diasPeriodicidad = Convert.ToInt32(ddlperiod.Items.FindByText(txtCodPeriodicidad.Text).Value); } catch { }
        var numeroCuota = !string.IsNullOrEmpty(txtNumeroCuotaExt.Text) ? txtNumeroCuotaExt.Text : "1";
        int plazo_CuotaExtra = 0;
        if (valor_diasPeriodicidad != 0)
            plazo_CuotaExtra = (Convert.ToInt32(ddlPeriodicidadCuotaExt.SelectedValue) * Convert.ToInt32(numeroCuota)) / valor_diasPeriodicidad;
        if (plazo_CuotaExtra > Convert.ToInt32(txtPlazoTxt.Text))
        {
            lblError.Text = @"El número de cuotas por la periodicidad excede el plazo";
            return false;
        }
        Decimal valor_limite = (Convert.ToDecimal(txtMonto.Text) * Convert.ToDecimal(!string.IsNullOrEmpty(txtPorcentaje.Text) ? txtPorcentaje.Text : "100")) / 100;
        Decimal valor_cuota = Convert.ToDecimal(!string.IsNullOrEmpty(txtValorCuotaExt.Text) ? txtValorCuotaExt.Text : valor.Text) * Convert.ToDecimal(numeroCuota);
        if (valor_cuota > valor_limite)
        {
            lblError.Text = @"La cantidad de cuotas extras por el valor excede el porcentaje del monto";
            return false;
        }
        return true;
    }

    public bool GuardarCuotas(string numeroRadicacion)
    {
        return GuardarCuotas(numeroRadicacion, 0);
    }

    public bool GuardarCuotas(string numeroRadicacion, int tipo = 0)
    {
        // Limpiar tabla temporal de cuotas extras
        if (tipo == 1)
        {
            try
            {
                CuoExtServicio.EliminarCuotasExtrasTemporales(Convert.ToInt64(numeroRadicacion), (Usuario)Session["usuario"]);
            }
            catch { }
        }
        // Grabar los datos de las cuotas extras
        try
        {
            if (gvCuoExt.Rows.Count > 0)
            {
                CuotasExtrasService CuoExtServicio = new CuotasExtrasService();
                foreach (GridViewRow rFila in gvCuoExt.Rows)
                {
                    //llama los datos de la tabla
                    CuotasExtras vCuotaExtra = new CuotasExtras();
                    Label lblfechapago = rFila.FindControl("lblfechapago") as Label;
                    Label lblformapago = rFila.FindControl("lblformapago") as Label;
                    Label lblvalor = rFila.FindControl("lblvalor") as Label;
                    Label lbltipocuota = rFila.FindControl("lbltipocuota") as Label;
                    Label lblcodtipocuota = rFila.FindControl("lblcodtipocuota") as Label;
                    //Validad de que la fecha de pago no este nulo
                    if (lblfechapago.Text == "")
                        break;

                    var valor = lblvalor.Text.Replace(",00", "").Replace(".", "");
                    //ingresa datos en la entity
                    vCuotaExtra.numero_radicacion = Convert.ToInt64(numeroRadicacion);
                    vCuotaExtra.fecha_pago = Convert.ToDateTime(lblfechapago.Text);
                    vCuotaExtra.forma_pago = lblformapago.Text == "Caja" ? "1" : "2";
                    vCuotaExtra.valor = Convert.ToDecimal(valor);
                    vCuotaExtra.valor_capital = Convert.ToDecimal(valor);
                    vCuotaExtra.tipo_cuota = Convert.ToInt32(lblcodtipocuota.Text);
                    if (tipo == -1)
                    {
                        vCuotaExtra.des_tipo_cuota = lblcodtipocuota.Text + lbltipocuota.Text;
                    }
                    else
                    {
                        vCuotaExtra.des_tipo_cuota = lbltipocuota.Text;
                    }
                    vCuotaExtra.saldo_capital = Convert.ToInt64(valor);
                    vCuotaExtra.saldo_interes = 0;
                    vCuotaExtra.valor_interes = 0;
                    if (tipo == -1)
                        CuoExtServicio.CrearSolicitudCuotasExtras(vCuotaExtra, (Usuario)Session["usuario"]);
                    else if (tipo == 0)
                        CuoExtServicio.CrearCuotasExtras(vCuotaExtra, (Usuario)Session["usuario"]);
                    else
                        CuoExtServicio.CrearSimulacionCuotasExtras(vCuotaExtra, (Usuario)Session["usuario"]);
                }
            }
            else
            {
                CuotasExtrasService CuoExtServicio = new CuotasExtrasService();
                foreach (GridViewRow rFila in gvCuoExtEdit.Rows)
                {

                    //llama los datos de la tabla
                    CuotasExtras vCuotaExtra = new CuotasExtras();
                    TextBox lblCodCuota = rFila.FindControl("CodCuota") as TextBox;
                    TextBox lblfechapago = rFila.FindControl("lblfechapago") as TextBox;
                    DropDownList lblformapago = rFila.FindControl("ddlformapago") as DropDownList;
                    TextBox lblvalor = rFila.FindControl("lblvalor") as TextBox;
                    DropDownList lblcodtipocuota = rFila.FindControl("ddltipocuotagvs") as DropDownList;
                    TextBox txtSaldoCapital = rFila.FindControl("txtSaldoCapital") as TextBox;
                    //Validad de que la fecha de pago no este nulo
                    if (lblfechapago.Text == "")
                        break;

                    var valor = lblvalor.Text.Replace(",00", "").Replace(".", "");
                    //ingresa datos en la entity
                    vCuotaExtra.cod_cuota = string.IsNullOrEmpty(lblCodCuota.Text) ? 0 : Convert.ToInt32(lblCodCuota.Text);
                    vCuotaExtra.numero_radicacion = Convert.ToInt64(numeroRadicacion);
                    vCuotaExtra.fecha_pago = Convert.ToDateTime(lblfechapago.Text);
                    vCuotaExtra.forma_pago = lblformapago.SelectedValue;
                    vCuotaExtra.valor = Convert.ToDecimal(valor);
                    vCuotaExtra.valor_capital = Convert.ToDecimal(valor);
                    vCuotaExtra.tipo_cuota = Convert.ToInt32(lblcodtipocuota.SelectedValue);
                    vCuotaExtra.des_tipo_cuota = lblcodtipocuota.Text;
                    vCuotaExtra.saldo_capital = Convert.ToInt64(txtSaldoCapital.Text);
                    vCuotaExtra.saldo_interes = 0;
                    vCuotaExtra.valor_interes = 0;
                    try
                    {
                        if (vCuotaExtra.cod_cuota > 0)
                        {
                            if (tipo == -1)
                                CuoExtServicio.ModificarCuotasExtras(vCuotaExtra, (Usuario)Session["usuario"]);
                            else if (tipo == 0)
                                CuoExtServicio.ModificarCuotasExtras(vCuotaExtra, (Usuario)Session["usuario"]);
                            else
                                CuoExtServicio.CrearSimulacionCuotasExtras(vCuotaExtra, (Usuario)Session["usuario"]);
                        }
                        else
                        {
                            if (tipo == -1)
                                CuoExtServicio.CrearSolicitudCuotasExtras(vCuotaExtra, (Usuario)Session["usuario"]);
                            else if (tipo == 0)
                                CuoExtServicio.CrearCuotasExtras(vCuotaExtra, (Usuario)Session["usuario"]);
                            else
                                CuoExtServicio.CrearSimulacionCuotasExtras(vCuotaExtra, (Usuario)Session["usuario"]);
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }

    }

    public void TablaCuoExt(string pIdObjeto, int pControl)
    {
        TablaCuoExt(pIdObjeto);
        if (pControl == 1) divGenerar.Visible = false;
    }

    public void TablaCuoExt(string pIdObjeto)
    {
        try
        {
            CuotasExtras eCuoExt = new CuotasExtras();

            eCuoExt.numero_radicacion = Convert.ToInt64(pIdObjeto);
            lstConsulta = CuoExtServicio.ListarCuotasExtras(eCuoExt, (Usuario)Session["Usuario"]);

            if (lstConsulta != null)
            {
                panelCuotasExtras.Visible = false;
                panelCuotasExtrasEdit.Visible = true;
                gvCuoExtEdit.PageSize = 5;
                gvCuoExtEdit.EmptyDataText = "No se encontraron registros";
                gvCuoExtEdit.DataSource = lstConsulta;
                if (lstConsulta.Count > 0)
                {
                    gvCuoExtEdit.Visible = true;
                    gvCuoExtEdit.DataBind();
                    Session["CuoExt"] = lstConsulta;
                    decimal? total = lstConsulta.Sum(p => p.valor);
                    Session["TotalCuoExt"] = total.ToString();
                }
                else
                {
                    panelCuotasExtras.Visible = true;
                    panelCuotasExtrasEdit.Visible = false;
                    InicialCuoExt();
                }

                Session.Add(DatosSolicitudServicio.CodigoPrograma + ".consulta", 1);
            }

        }
        catch (Exception ex)
        {

            // BOexcepcion.Throw(DatosSolicitudServicio.CodigoPrograma, "Actualizar", ex);
        }

    }

    public List<CuotasExtras> GetListCuotas(string numeroRadicacion)
    {
        try
        {
            List<CuotasExtras> lstCuotasExtras = new List<CuotasExtras>();
            if (gvCuoExt.Rows.Count > 0)
            {
                CuotasExtrasService CuoExtServicio = new CuotasExtrasService();
                foreach (GridViewRow rFila in gvCuoExt.Rows)
                {
                    //llama los datos de la tabla
                    CuotasExtras vCuotaExtra = new CuotasExtras();
                    Label lblfechapago = rFila.FindControl("lblfechapago") as Label;
                    Label lblformapago = rFila.FindControl("lblformapago") as Label;
                    Label lblvalor = rFila.FindControl("lblvalor") as Label;
                    Label lbltipocuota = rFila.FindControl("lbltipocuota") as Label;
                    Label lblcodtipocuota = rFila.FindControl("lblcodtipocuota") as Label;
                    //Validad de que la fecha de pago no este nulo
                    if (lblfechapago.Text == "")
                        break;

                    var valor = lblvalor.Text.Replace(",00", "").Replace(".", "");
                    //ingresa datos en la entity
                    vCuotaExtra.numero_radicacion = Convert.ToInt64(numeroRadicacion);
                    vCuotaExtra.fecha_pago = Convert.ToDateTime(lblfechapago.Text);
                    vCuotaExtra.forma_pago = lblformapago.Text == "Caja" ? "1" : "2";
                    vCuotaExtra.valor = Convert.ToDecimal(valor);
                    vCuotaExtra.valor_capital = Convert.ToDecimal(valor);
                    vCuotaExtra.tipo_cuota = Convert.ToInt32(lblcodtipocuota.Text);
                    vCuotaExtra.saldo_capital = Convert.ToInt64(valor);
                    vCuotaExtra.saldo_interes = 0;
                    vCuotaExtra.valor_interes = 0;
                    lstCuotasExtras.Add(vCuotaExtra);
                }
            }
            else
            {
                CuotasExtrasService CuoExtServicio = new CuotasExtrasService();
                foreach (GridViewRow rFila in gvCuoExtEdit.Rows)
                {

                    //llama los datos de la tabla
                    CuotasExtras vCuotaExtra = new CuotasExtras();
                    TextBox lblCodCuota = rFila.FindControl("CodCuota") as TextBox;
                    TextBox lblfechapago = rFila.FindControl("lblfechapago") as TextBox;
                    DropDownList lblformapago = rFila.FindControl("ddlformapago") as DropDownList;
                    TextBox lblvalor = rFila.FindControl("lblvalor") as TextBox;
                    DropDownList lblcodtipocuota = rFila.FindControl("ddltipocuotagvs") as DropDownList;
                    //Validad de que la fecha de pago no este nulo
                    if (lblfechapago.Text == "")
                        break;

                    var valor = lblvalor.Text.Replace(",00", "").Replace(".", "");
                    //ingresa datos en la entity
                    vCuotaExtra.cod_cuota = string.IsNullOrEmpty(lblCodCuota.Text) ? 0 : Convert.ToInt32(lblCodCuota.Text);
                    vCuotaExtra.numero_radicacion = Convert.ToInt64(numeroRadicacion);
                    vCuotaExtra.fecha_pago = Convert.ToDateTime(lblfechapago.Text);
                    vCuotaExtra.forma_pago = lblformapago.SelectedValue;
                    vCuotaExtra.valor = Convert.ToDecimal(valor);
                    vCuotaExtra.valor_capital = Convert.ToDecimal(valor);
                    vCuotaExtra.tipo_cuota = Convert.ToInt32(lblcodtipocuota.SelectedValue);
                    vCuotaExtra.saldo_capital = Convert.ToInt64(valor);
                    vCuotaExtra.saldo_interes = 0;
                    vCuotaExtra.valor_interes = 0;
                    lstCuotasExtras.Add(vCuotaExtra);
                }
            }

            return lstCuotasExtras;
        }
        catch
        {
            return null;
        }

    }

    #endregion

}