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
using Xpinn.Caja.Services;
using Xpinn.Caja.Entities;

partial class Lista : GlobalWeb
{
    private Xpinn.Caja.Services.MovimientoCajaService MovimientoCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();
    Xpinn.Caja.Services.MotivoDevCheService MotivoService = new Xpinn.Caja.Services.MotivoDevCheService();
    Xpinn.Caja.Entities.MotivoDevChe Motivo = new Xpinn.Caja.Entities.MotivoDevChe();
    Xpinn.Caja.Services.ConsignacionService ConsignacionChequeService = new ConsignacionService();
    Xpinn.Caja.Entities.Consignacion Consignacioncheque = new Consignacion();
    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
    private PoblarListas poblarLista = new PoblarListas();
    Int64 tipoOpe = 129;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(MovimientoCajaServicio.CodigoPrograma6, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;

            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MovimientoCajaServicio.CodigoPrograma6, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtFechaRealiza.Text = DateTime.Now.ToString(gFormatoFecha);
                LlenarComboBancos(ddlBancos);
                poblarLista.PoblarListaDesplegable("cuenta_bancaria", "idctabancaria,num_cuenta", "", "2", ddlcuentas, (Usuario)Session["usuario"]);

            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MovimientoCajaServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Site toolBar = (Site)this.Master;

        if (txtFechaRealiza.Text != "")
        {
            if (txtFechaInicial.Text == "" && txtFechaFinal.Text != "")
            {
                VerError("Digite el rango de la fecha inicial");
                return;
            }
            toolBar.MostrarGuardar(true);
            Actualizar();
            VerError("");
        }
        else
        {
            VerError("Digite la fecha establecida");
            return;
        }


    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtFechaRealiza.Text == "")
        {
            VerError("Digite la fecha establecida");
            return;
        }
        Int64 total_canje = 0;
        // Validar datos del gridview 
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox chkCanjear = (CheckBox)rFila.FindControl("chkCanjear");
            Label Txtestadocheque = (Label)rFila.FindControl("Lblestadocheque");

            if (chkCanjear != null)
            { 
                if (chkCanjear.Checked == true)
                {
                    // Validar estados y motivos
                    DropDownListGrid ddlestado = (DropDownListGrid)rFila.FindControl("ddlestado");
                    DropDownListGrid ddlmotivo = (DropDownListGrid)rFila.FindControl("ddlmotivo");
                    if (Txtestadocheque.Text == "CONSIGNADO")
                    {
                        if (ddlestado.SelectedValue == "3")
                        {
                            if (ddlmotivo.SelectedValue == "0")
                            {
                                ddlmotivo.Focus();
                                VerError("Seleccione el motivo de devolucion correspondiente a la consignacion del cheque");
                                return;
                            }
                            else
                            {
                                VerError("");
                            }

                        }
                    }
                    else
                    {
                        VerError("Este cheque aún no ha sido consignado");
                    }
                }
            }
            total_canje += 1;

        }
        if (total_canje == 0)
        {
            VerError("No hay canje seleccionado para guardar o este cheque aún no ha sido consignado");
        }

        else
        {
            VerError("");
            ctlMensaje.MostrarMensaje("Desea realizar el canje de consignaciones de cheques seleccionados?");
        }
    }
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Usuario pUsuario = (Usuario)Session["Usuario"];

        // Determinar el proceso del guardar canje del cheque

        // Generar el comprobante
        pOperacion.cod_ope = 0;
        pOperacion.tipo_ope = tipoOpe;
        pOperacion.cod_caja = 0;
        pOperacion.cod_cajero = 0;
        pOperacion.fecha_oper = Convert.ToDateTime(txtFechaRealiza.Text);
        pOperacion.fecha_calc = Convert.ToDateTime(txtFechaRealiza.Text);
        pOperacion.observacion = "Devolución de cheques en canje";

        Xpinn.Tesoreria.Entities.Operacion Operacion = new Xpinn.Tesoreria.Entities.Operacion();
        Xpinn.Tesoreria.Services.OperacionServices operacionservice = new Xpinn.Tesoreria.Services.OperacionServices();
        operacionservice.GrabarOperacion(pOperacion, pUsuario);
        Consignacioncheque.cod_ope = pOperacion.cod_ope;
        Int64 motivo = 0;

        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox chkCanjear = (CheckBox)rFila.FindControl("chkCanjear");
            if (chkCanjear != null)
                if (chkCanjear.Checked == true)
                {
                    // Canjear Cheque
                    DropDownListGrid ddlestado = (DropDownListGrid)rFila.FindControl("ddlestado");
                    DropDownListGrid ddlmotivo = (DropDownListGrid)rFila.FindControl("ddlmotivo");
                    TextBox txtobservaciones = (TextBox)rFila.FindControl("txtobservaciones");
                    Xpinn.Caja.Services.MotivoDevCheService MotivoService = new Xpinn.Caja.Services.MotivoDevCheService();
                    Xpinn.Caja.Entities.MotivoDevChe Motivo = new Xpinn.Caja.Entities.MotivoDevChe();
                    Usuario usuap = (Usuario)Session["usuario"];
                    Consignacioncheque.cod_consignacion = Convert.ToInt64(rFila.Cells[0].Text);
                    
                    if (ddlmotivo.SelectedValue == "Seleccionar motivo")
                    {
                        Motivo.cod_motivo = 0;
                        Motivo.descripcion = ddlmotivo.Text;

                    }
                    else
                    {
                        Motivo.cod_motivo = Convert.ToInt32(ddlmotivo.SelectedValue);
                        Motivo.descripcion = ddlmotivo.Text;
                    }
                    motivo = Motivo.cod_motivo;
                    Consignacioncheque.fecha_consignacion = Convert.ToDateTime(txtFechaRealiza.Text);
                    Consignacioncheque.estado = Convert.ToInt64(ddlestado.SelectedValue);
                    Consignacioncheque.observaciones = txtobservaciones.Text;

                    Consignacioncheque = ConsignacionChequeService.GrabarCanje(Consignacioncheque, Motivo, usuap);
                }
        }

        if (Consignacioncheque.cod_ope > 0 && motivo != 0)
        {
            // Se genera el comprobante
            DateTime fecha = Convert.ToDateTime(txtFechaRealiza.Text);
            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = Consignacioncheque.cod_ope;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = tipoOpe;
            Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(txtFechaRealiza.Text);
            if(pUsuario.cod_persona != null && pUsuario.cod_persona != 0)
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = pUsuario.cod_persona;
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = pUsuario.cod_oficina;
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
        }
        else
        {
            Actualizar();
        }

    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        Site toolBar = (Site)this.Master;
        VerError("");
        txtFechaRealiza.Text = DateTime.Now.ToString(gFormatoFecha);
        txtFechaInicial.Text = "";
        txtFechaFinal.Text = "";
        toolBar.MostrarGuardar(false);
        gvLista.DataSource = null;
        gvLista.DataBind();
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
        ddlBancos.SelectedIndex = 0;

    }
    private void Actualizar()
    {
        try
        {
            Configuracion conf = new Configuracion();

            string[] data = new string[4];
            data[0] = ddlBancos.SelectedValue.Trim();
            data[1] = txtFechaInicial.Text;
            data[2] = txtFechaFinal.Text;
            data[3] = ddlcuentas.SelectedItem.Text.Trim();

            Usuario pUsuario = (Usuario)Session["Usuario"];
            List<Xpinn.Caja.Entities.MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();
            lstMovimientoCaja = MovimientoCajaServicio.ListarChequesCanje(data, pUsuario);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstMovimientoCaja;
            if (lstMovimientoCaja.Count > 0)
            {
                pDatos.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstMovimientoCaja.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                pDatos.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;

            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MovimientoCajaServicio.CodigoPrograma6, "Actualizar", ex);
        }
    }
    private void Actualizar2()
    {
        try
        {
            Configuracion conf = new Configuracion();

            string[] data = new string[3];
            data[0] = ddlBancos.SelectedValue.Trim();
            data[1] = txtFechaInicial.Text;
            data[2] = txtFechaFinal.Text;

            Usuario pUsuario = (Usuario)Session["Usuario"];
            List<Xpinn.Caja.Entities.MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();
            lstMovimientoCaja = MovimientoCajaServicio.ListarChequesNoconsignados(data, pUsuario);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstMovimientoCaja;
            if (lstMovimientoCaja.Count > 0)
            {
                pDatos.Visible = true;
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstMovimientoCaja.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
                pDatos.Visible = false;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;

            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MovimientoCajaServicio.CodigoPrograma6, "Actualizar", ex);
        }
    }
    protected void LlenarComboBancos(DropDownList ddlBancos)
    {

        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        Usuario usuario = new Usuario();
        ddlBancos.Items.Add("Seleccionar item");
        ddlBancos.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlBancos.DataTextField = "nombrebanco";
        ddlBancos.DataValueField = "cod_banco";
        ddlBancos.DataBind();
    }

  


    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownListGrid ddlestado = (DropDownListGrid)e.Row.FindControl("ddlestado");

        Usuario usuario = new Usuario();
        DropDownListGrid ddlmotivo = (DropDownListGrid)e.Row.FindControl("ddlmotivo");

        if (ddlestado != null)
        {
            ddlestado.Items.Insert(0, new ListItem("Canje", "2"));
            ddlestado.Items.Insert(1, new ListItem("Devuelto", "3"));
            ddlestado.SelectedIndex = 0;
            ddlestado.DataBind();
        }
        if (ddlmotivo != null)
        {
            ddlmotivo.Items.Add("Seleccionar motivo");
            ddlmotivo.DataSource = MotivoService.ListarMotivoDevChe(Motivo, (Usuario)Session["usuario"]);
            ddlmotivo.DataBind();
            ddlmotivo.Enabled = false;
        }

    }

    protected void ddlestado_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlestado = (DropDownListGrid)sender;
        int column_id = int.Parse(ddlestado.CommandArgument);
        DropDownListGrid ddlmotivo = (DropDownListGrid)gvLista.Rows[column_id].FindControl("ddlmotivo");
        if (ddlestado.SelectedValue == "3")
        {
            ddlmotivo.Enabled = true;
            // ddlmotivo.Items.Add("Seleccionar motivo");
            //ddlmotivo.DataSource = MotivoService.ListarMotivoDevChe(Motivo, (Usuario)Session["usuario"]);
            //ddlmotivo.DataBind();

        }
        else
        {
            ddlestado.SelectedIndex = 0;
            ddlmotivo.Enabled = false;

        }
    }
    protected void chkChequesSinConsignar_CheckedChanged(object sender, EventArgs e)
    {
        if (chkChequesSinConsignar.Checked)
            Actualizar2();
        else
            Actualizar();
    }
}

