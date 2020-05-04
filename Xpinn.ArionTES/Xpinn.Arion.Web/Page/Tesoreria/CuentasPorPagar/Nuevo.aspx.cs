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
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Seguridad.Services;
using Xpinn.Seguridad.Entities;
using Microsoft.Reporting.WebForms;
using System.IO;

using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
partial class Nuevo : GlobalWeb
{
    EmpresaService _empresaService = new EmpresaService();


    CuentasPorPagarService CuotasXpagar = new CuentasPorPagarService();
    int tipoOpe = 24;
    string anteriorTipo = "";
    int contador = 0;
    private int index = 0;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[CuotasXpagar.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(CuotasXpagar.CodigoPrograma, "E");
            else
                VisualizarOpciones(CuotasXpagar.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasXpagar.CodigoPrograma, "Page_PreInit", ex);
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {
                ctlGiro.Inicializar();

                Session["DatosDetalle"] = null;
                Session["DatosFormaPago"] = null;
                Session["lstImpuesto"] = null;
                mvCuentasxPagar.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                txtFechaIngreso.Text = DateTime.Today.ToShortDateString();
                cargarDropdown();


                if (Session[CuotasXpagar.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[CuotasXpagar.CodigoPrograma + ".id"].ToString();
                    Session.Remove(CuotasXpagar.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    lblMsj.Text = " modificada ";
                }
                else
                {
                    lblMsj.Text = " grabada ";
                    InicializarDetalle();
                    InicializarFormaPago();
                    txtEstado.Visible = false;
                    lblEstado.Visible = false;

                    txtCodigo.Text = CuotasXpagar.ObtenerSiguienteCodigo((Usuario)Session["usuario"]).ToString();
                    Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
                    Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
                    try
                    {
                        pData = ConsultaData.ConsultarGeneral(90175, (Usuario)Session["usuario"]);
                        if (pData.valor != "")
                        {
                            Int32 parametro = Convert.ToInt32(pData.valor);
                            if (parametro == 1)
                            {
                                txtDocEquiva.Text = CuotasXpagar.ObtenerSiguienteEquivalente((Usuario)Session["usuario"]).ToString();
                            }
                        }
                    }
                    catch
                    { VerError("Se presento error al consultar el parámetro general 90175"); }
                    cbManejaAnti.Checked = false;
                }
                cbManejaAnti_CheckedChanged(cbManejaAnti, null);
                cbDescuentos_CheckedChanged(cbDescuentos, null);
                ddlTipoCuenta_SelectedIndexChanged(ddlTipoCuenta, null);
            }
            else
            {
                //RECALCULANDO EL VALOR NETO CUANDO SE DIGITA EN ALGUN IMPUESTO DINAMICO
                /*foreach (GridViewRow rFila in gvDetalle.Rows)
                {
                    TextBoxGrid txtVrTotal = (TextBoxGrid)rFila.FindControl("txtVrTotal");
                    txtVrTotal_TextChanged(txtVrTotal, null);
                }*/
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasXpagar.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void cargarDropdown()
    {
        LlenarListasDesplegables(TipoLista.TipoCuentasXpagar, ddlTipoCuenta);
        if (ddlTipoCuenta.Items.Count == 0)
        {
            ddlTipoCuenta.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoCuenta.Items.Insert(1, new ListItem("Factura", "1"));
            ddlTipoCuenta.Items.Insert(2, new ListItem("Orden de Pago", "2"));
            ddlTipoCuenta.Items.Insert(3, new ListItem("Orden de Compra", "3"));
            ddlTipoCuenta.Items.Insert(4, new ListItem("Orden de Servicio", "4"));
            ddlTipoCuenta.Items.Insert(5, new ListItem("Contrato de Servicio", "5"));
        }
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodProveedor", "txtIdProveedor", "txtNomProveedor");
    }




    protected void ObtenerDatos(String pIdObjeto)
    {
        //  ctlGiro.Visible = false;

        try
        {
            CuentasPorPagar cuentas = new CuentasPorPagar();
            cuentas.codigo_factura = Convert.ToInt32(idObjeto);
            CuentasPorPagar vCuentas = new CuentasPorPagar();
            vCuentas = CuotasXpagar.ConsultarCuentasXpagar(cuentas, (Usuario)Session["usuario"]);

            if (vCuentas.codigo_factura != 0)
            {
                txtCodigo.Text = vCuentas.codigo_factura.ToString();
                gvDetalle.Enabled = false;
                gvFormaPago.Enabled = false;
                btnAgregar.Enabled = false;
                btnAddRow.Enabled = false;
                Site toolBar2 = (Site)Master;
                toolBar2.MostrarGuardar(false);
            }
            if (vCuentas.numero_factura != "")
                txtNumFactura.Text = Convert.ToString(vCuentas.numero_factura);
            if (vCuentas.fecha_ingreso != DateTime.MinValue)
                txtFechaIngreso.Text = vCuentas.fecha_ingreso.ToString(gFormatoFecha);
            if (vCuentas.fecha_factura != DateTime.MinValue)
                txtFechaFact.Text = vCuentas.fec_fact.ToString(gFormatoFecha);
            if (vCuentas.fecha_radicacion != DateTime.MinValue)
                txtFechaRadia.Text = vCuentas.fec_radi.ToString(gFormatoFecha);
            if (vCuentas.fecha_vencimiento != null)
                if (vCuentas.fecha_vencimiento != DateTime.MinValue)
                    txtFechaVenci.Text = Convert.ToDateTime(vCuentas.fecha_vencimiento).ToString(gFormatoFecha);
            if (vCuentas.idtipo_cta_por_pagar != 0)
                ddlTipoCuenta.SelectedValue = Convert.ToString(vCuentas.idtipo_cta_por_pagar);
            if (vCuentas.doc_equivalente != 0)
                txtDocEquiva.Text = Convert.ToString(vCuentas.doc_equivalente);
            if (vCuentas.num_contrato != "")
                txtNroContra.Text = vCuentas.num_contrato;
            if (vCuentas.poliza != "")
                txtPoliza.Text = vCuentas.poliza;
            if (vCuentas.vence_contrato != DateTime.MinValue)
                txtVence.Text = vCuentas.vence_contrato.ToString(gFormatoFecha);
            if (vCuentas.cod_persona != 0)
            {
                txtCodProveedor.Text = Convert.ToString(vCuentas.cod_persona);
                txtIdProveedor.Text = vCuentas.identificacion;
                txtNomProveedor.Text = vCuentas.nombre;
            }
            if (vCuentas.estado != 0)
            {
                txtEstado.Text = Convert.ToString(vCuentas.nomestado);
            }

            if (vCuentas.estado == 4)
            {
                Site toolBar1 = (Site)Master;
                toolBar1.MostrarGuardar(false);
                toolBar1.MostrarImprimir(false);

            }

            if (vCuentas.cod_ope != 0)
                lblCod_Ope.Text = vCuentas.cod_ope.ToString();

            //RECUPERAR GRILLA DETALLE 
            List<CuentaXpagar_Detalle> lstDetalle = new List<CuentaXpagar_Detalle>();
            CuentaXpagar_Detalle Deta = new CuentaXpagar_Detalle();
            Deta.codigo_factura = vCuentas.codigo_factura;
            lstDetalle = CuotasXpagar.ConsultarDetalleCuentasXpagar(Deta, (Usuario)Session["usuario"]);

            if (lstDetalle.Count > 0)
            {
                gvDetalle.DataSource = lstDetalle;
                gvDetalle.DataBind();
                ActualizarImpuestoXConcepto();
            }
            else
            {
                InicializarDetalle();
            }

            //RECUPERAR GRILLA FORMA DE PAGO
            List<CuentaXpagar_Pago> lstFormaPago = new List<CuentaXpagar_Pago>();
            CuentaXpagar_Pago vForm = new CuentaXpagar_Pago();
            vForm.codigo_factura = vCuentas.codigo_factura;
            lstFormaPago = CuotasXpagar.ConsultarDetalleFormaPago(vForm, (Usuario)Session["usuario"]);
            if (lstFormaPago.Count > 0)
            {
                gvFormaPago.DataSource = lstFormaPago;
                gvFormaPago.DataBind();
            }
            else
            {
                InicializarFormaPago();
            }

            if (vCuentas.maneja_anticipos != 0)
                cbManejaAnti.Checked = true;

            if (vCuentas.maneja_descuentos != 0)
                cbDescuentos.Checked = true;

            if (vCuentas.valor_anticipo != 0)
                txtValorAnti.Text = Convert.ToString(vCuentas.valor_anticipo);


            if (vCuentas.observaciones != "")
                txtObservacion.Text = vCuentas.observaciones;

            // CuentasPorPagar cuentas = new CuentasPorPagar();
            cuentas.cod_ope = Convert.ToInt32(lblCod_Ope.Text);
            CuentasPorPagar vGiro = new CuentasPorPagar();
            vGiro = CuotasXpagar.ConsultarGiro(cuentas, (Usuario)Session["usuario"]);
            panelGiro.Enabled = false;
            ctlGiro.ValueFormaDesem = Convert.ToString(vGiro.forma_pago);

            ctlGiro.ValueEntidadOrigen = Convert.ToString(vGiro.cod_banco);
            if (vGiro.cod_bancodestino > 0)
            {
                ctlGiro.ValueEntidadDest = Convert.ToString(vGiro.cod_bancodestino);
                ctlGiro.ValueTipoCta = Convert.ToString(vGiro.tipo_cuenta);
                ctlGiro.TextNumCuenta = Convert.ToString(vGiro.num_cuenta_destino);
            }


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasXpagar.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected List<CuentaXpagar_Detalle> ObtenerListaDetalle()
    {
        List<CuentaXpagar_Detalle> lstDetalle = new List<CuentaXpagar_Detalle>();
        List<CuentaXpagar_Detalle> lista = new List<CuentaXpagar_Detalle>();

        foreach (GridViewRow rfila in gvDetalle.Rows)
        {
            CuentaXpagar_Detalle eCuen = new CuentaXpagar_Detalle();

            Label lblcodigo = (Label)rfila.FindControl("lblcodigo");
            if (lblcodigo != null)
                eCuen.coddetallefac = Convert.ToInt32(lblcodigo.Text);

            DropDownListGrid ddlConcepto = (DropDownListGrid)rfila.FindControl("ddlConcepto");
            if (ddlConcepto.SelectedValue != "" || ddlConcepto.SelectedIndex != 0)
                eCuen.cod_concepto_fac = Convert.ToInt32(ddlConcepto.SelectedValue);



            TextBox txtDetalle = (TextBox)rfila.FindControl("txtDetalle");
            if (txtDetalle.Text != "")
                eCuen.detalle = Convert.ToString(txtDetalle.Text);
            else
                eCuen.detalle = null;

            DropDownListGrid ddlCentroCosto = (DropDownListGrid)rfila.FindControl("ddlCentroCosto");
            if (ddlCentroCosto.SelectedValue != "" || ddlCentroCosto.SelectedIndex != 0)
                eCuen.centro_costo = Convert.ToInt32(ddlCentroCosto.SelectedValue);

            TextBoxGrid txtCantidad = (TextBoxGrid)rfila.FindControl("txtCantidad");
            if (txtCantidad.Text != "")
                eCuen.cantidad = Convert.ToInt32(txtCantidad.Text);

            TextBoxGrid txtVrUnitario = (TextBoxGrid)rfila.FindControl("txtVrUnitario");
            if (txtVrUnitario.Text != "")
                eCuen.valor_unitario = Convert.ToDecimal(txtVrUnitario.Text);

            TextBoxGrid txtVrTotal = (TextBoxGrid)rfila.FindControl("txtVrTotal");
            if (txtVrTotal.Text != "" && txtVrTotal.Text != "0")
                eCuen.valor_total = Convert.ToDecimal(txtVrTotal.Text);
            else
                eCuen.valor_total = null;

            TextBoxGrid txtPorDesc = (TextBoxGrid)rfila.FindControl("txtPorDesc");
            if (txtPorDesc.Text != "" && txtPorDesc.Text != "0")
                eCuen.porc_descuento = Convert.ToDecimal(txtPorDesc.Text);
            else
                eCuen.porc_descuento = null;

            decimal porcentajeImpuestos = 0;
            DataList dtImpuestos = (DataList)rfila.FindControl("dtImpuestos");
            if (dtImpuestos != null)
            {
                eCuen.lstImpuesto = new List<Concepto_CuentasXpagarImp>();

                foreach (DataListItem rRow in dtImpuestos.Items)
                {
                    DropDownListGrid ddlPorcentaje = (DropDownListGrid)rRow.FindControl("ddlPorcentaje");
                    if (Convert.ToInt64(ddlPorcentaje.SelectedItem.Value) > 0)
                    {
                        Concepto_CuentasXpagarImp entidad = new Concepto_CuentasXpagarImp();

                        entidad.coddetalleimp = Convert.ToInt32(ddlPorcentaje.SelectedItem.Value);

                        Label lblCodImp = (Label)rRow.FindControl("lblCodImp");
                        if (lblCodImp.Text != "")
                            entidad.cod_tipo_impuesto = Convert.ToInt32(lblCodImp.Text);

                        Label lblTipo = (Label)rRow.FindControl("lblTipo");
                        if (lblTipo.Text != "")
                            entidad.naturaleza = lblTipo.Text;

                        Label lblTitulo = (Label)rRow.FindControl("lblTitulo");
                        if (lblTitulo.Text != "")
                            entidad.nom_tipo_impuesto = lblTitulo.Text;


                        Label lblCuenta1 = (Label)rRow.FindControl("lblCuenta1");
                        if (lblCuenta1.Text != "")
                            entidad.cod_cuenta_imp = lblCuenta1.Text;

                        Label lblBase = (Label)rRow.FindControl("lblBase");


                        if (ddlPorcentaje.SelectedItem.Text != null)
                        {
                            entidad.porcentaje_impuesto = Convert.ToDecimal(ddlPorcentaje.SelectedItem.Text);
                            entidad.base_minima = Convert.ToDecimal(lblBase.Text);
                            if (eCuen.valor_total >= entidad.base_minima)
                            {
                                //Totalizar porcentaje de Impuestos para calcular el Vr_IMPUESTOS
                                porcentajeImpuestos += Convert.ToDecimal(entidad.porcentaje_impuesto);
                            }
                        }

                        eCuen.lstImpuesto.Add(entidad);
                    }
                }
            }

            Label txtVrNeto = (Label)rfila.FindControl("txtVrNeto");
            if (txtVrNeto != null)
                eCuen.valor_neto = Convert.ToDecimal(txtVrNeto.Text.Replace('$', ' '));

            decimal vrImpuestos = 0, vrTotal = 0;
            vrTotal = eCuen.valor_total != null ? Convert.ToDecimal(eCuen.valor_total) : 0;

            vrImpuestos = Math.Round(vrTotal * (porcentajeImpuestos / 100));
            eCuen.valor_impuesto = vrImpuestos;


            lista.Add(eCuen);
            Session["DatosDetalle"] = lista;

            if (eCuen.cod_concepto_fac != null && eCuen.detalle != null && eCuen.centro_costo != null && eCuen.valor_total != null)
            {
                lstDetalle.Add(eCuen);
            }
        }
        return lstDetalle;
    }


    protected void ActualizarImpuestoXConcepto()
    {
        foreach (GridViewRow rfila in gvDetalle.Rows)
        {
            DropDownListGrid ddlConcepto = (DropDownListGrid)rfila.FindControl("ddlConcepto");
            if (ddlConcepto != null)
            {
                if (idObjeto == "")
                {
                    //SI ES REGISTRO NUEVO CARGA AUTOMATICAMENTE LOS IMPUESTOS PREDETERMINADOS
                    if (ddlConcepto.SelectedIndex != 0)
                        ddlConcepto_SelectedIndexChanged(ddlConcepto, null);
                }
                else
                {
                    //SI ES REGISTRO NO ES NUEVO VERIFICA QUE EXISTAN IMPUESTOS X CONCEPTO
                    DataList dtImpuestos = (DataList)rfila.FindControl("dtImpuestos");

                    dtImpuestos.DataSource = null;
                    List<CuentasXpagarImpuesto> lstData = new List<CuentasXpagarImpuesto>();
                    CuentasXpagarImpuesto entidad = new CuentasXpagarImpuesto();

                    Label lblcodigo = (Label)rfila.FindControl("lblcodigo");
                    if (lblcodigo.Text != "")
                    {
                        //Si es un registro Existente capture el codigo
                        if (Convert.ToInt32(lblcodigo.Text) > 0)
                        {
                            entidad.coddetallefac = Convert.ToInt32(lblcodigo.Text);

                            //Cargar datos del DataList
                            List<Concepto_CuentasXpagarImp> lstImpuesto = new List<Concepto_CuentasXpagarImp>();
                            Concepto_CuentasXpagarImp pImpuesto = new Concepto_CuentasXpagarImp();
                            ConceptoCtaService ConceptoCtaServicio = new ConceptoCtaService();
                            pImpuesto.cod_factura = Convert.ToInt32(txtCodigo.Text);

                            lstImpuesto = ConceptoCtaServicio.ListarConceptoImpuestoDetalleCxp(pImpuesto, (Usuario)Session["usuario"]). Where(x=>x.CodDetalleFac == Convert.ToInt32(lblcodigo.Text)).ToList();
                            Session["lstImpuesto"] = lstImpuesto;

                            if (lstImpuesto.Count > 0)
                            {
                                dtImpuestos.DataSource = lstImpuesto;
                                dtImpuestos.DataBind();
                            }

                            lstData = CuotasXpagar.ConsultarDetImpuestosXConcepto(entidad, (Usuario)Session["usuario"]);
                            ActualizarPorcentajesCxP(lstData, lstImpuesto, rfila.RowIndex);


                            TextBoxGrid txtVrTotal = (TextBoxGrid)rfila.FindControl("txtVrTotal");
                            txtVrTotal_TextChanged(txtVrTotal, null);
                        }
                        else //carga Impuestos predeterminados
                            ddlConcepto_SelectedIndexChanged(ddlConcepto, null);
                    }
                    CalculaTotalXColumna();
                }
            }

            index++;
        }
    }

    //Agregado para indicar el porcentaje correspondiente al impuesto y cargar la base minima
    private void ActualizarPorcentajes(List<CuentasXpagarImpuesto> lstImpuestos, List<Concepto_CuentasXpagarImp> lstConceptos, int index)
    {
        GridViewRow rfila = gvDetalle.Rows[index];
        DataList dtImpuestos = (DataList)rfila.FindControl("dtImpuestos");
        DropDownListGrid ddlConcepto = (DropDownListGrid)rfila.FindControl("ddlConcepto");
        if (lstImpuestos != null)
        {
            foreach (DataListItem item in dtImpuestos.Items)
            {
                Label lblID = (Label)item.FindControl("lblID"); //Cod detalle impuesto
                Label lblCodImp = (Label)item.FindControl("lblCodImp"); //Código del impuesto
                Label lblBase = (Label)item.FindControl("lblBase");
                Label lblCuenta1 = (Label)item.FindControl("lblCuenta1");
                DropDownListGrid ddlPorcentaje = (DropDownListGrid)item.FindControl("ddlPorcentaje");

                foreach (CuentasXpagarImpuesto impuesto in lstImpuestos)
                {
                    if (lblCodImp.Text == impuesto.cod_tipo_impuesto.ToString())
                    {
                        lblID.Text = impuesto.coddetalleimp.ToString();
                        ddlPorcentaje.SelectedValue = impuesto.coddetalleimp.ToString();
                        Concepto_CuentasXpagarImp concepto = lstConceptos.Where(x => x.cod_tipo_impuesto == impuesto.cod_tipo_impuesto).FirstOrDefault();
                        List<Concepto_CuentasXpagarImp> lista = concepto.lstPorcentaje;
                        try
                        {
                            Decimal baseim = (from valor in concepto.lstPorcentaje
                                              where valor.porcentaje_impuesto == impuesto.porcentaje_impuesto
                                              select valor.base_minima).FirstOrDefault().Value;
                            lblBase.Text = baseim == 0 ? "0" : baseim.ToString();




                        }
                        catch { }


                    }
                }
            }
        }
        else
        {
            ConceptoCtaService ConceptoServicio = new ConceptoCtaService();
            Concepto_CuentasXpagarImp concepto = null;

            foreach (DataListItem item in dtImpuestos.Items)
            {
                Label lblID = (Label)item.FindControl("lblID"); //Cod detalle impuesto
                Label lblCodImp = (Label)item.FindControl("lblCodImp"); //Código del impuesto
                Label lblTipo = (Label)item.FindControl("lblTipo");
                Label lblBase = (Label)item.FindControl("lblBase"); //Base minima
                DropDownListGrid ddlPorcentaje = (DropDownListGrid)item.FindControl("ddlPorcentaje");
                Label lblCuenta1 = (Label)item.FindControl("lblCuenta1"); //Base minima


                concepto = new Concepto_CuentasXpagarImp();
                concepto.cod_tipo_impuesto = Convert.ToInt32(lblCodImp.Text);
                concepto.idconceptoimp = Convert.ToInt32(ddlPorcentaje.SelectedValue);
                concepto.cod_concepto_fac = Convert.ToInt32(ddlConcepto.SelectedValue);
                concepto = ConceptoServicio.ListarConceptoImpuesto(concepto, (Usuario)Session["usuario"]).FirstOrDefault();

                lblBase.Text = concepto.base_minima.ToString();
                lblTipo.Text = concepto.naturaleza;

                lblCuenta1.Text = concepto.cod_cuenta_imp;
            }
        }
    }
    private void ActualizarPorcentajesCxP(List<CuentasXpagarImpuesto> lstImpuestos, List<Concepto_CuentasXpagarImp> lstConceptos, int index)
    {
        GridViewRow rfila = gvDetalle.Rows[index];
        DataList dtImpuestos = (DataList)rfila.FindControl("dtImpuestos");
        DropDownListGrid ddlConcepto = (DropDownListGrid)rfila.FindControl("ddlConcepto");
        if (lstImpuestos.Count>0)
        {
            foreach (DataListItem item in dtImpuestos.Items)
            {
                Label lblID = (Label)item.FindControl("lblID"); //Cod detalle impuesto
                Label lblCodImp = (Label)item.FindControl("lblCodImp"); //Código del impuesto
                Label lblBase = (Label)item.FindControl("lblBase");
                Label lblCuenta1 = (Label)item.FindControl("lblCuenta1");
                DropDownListGrid ddlPorcentaje = (DropDownListGrid)item.FindControl("ddlPorcentaje");

                foreach (CuentasXpagarImpuesto impuesto in lstImpuestos)
                {
                    if (lblCodImp.Text == impuesto.cod_tipo_impuesto.ToString())
                    {
                        lblID.Text = impuesto.coddetalleimp.ToString();
                        ddlPorcentaje.SelectedValue = impuesto.coddetalleimp.ToString();
                        Concepto_CuentasXpagarImp concepto = lstConceptos.Where(x => x.cod_tipo_impuesto == impuesto.cod_tipo_impuesto).FirstOrDefault();
                        List<Concepto_CuentasXpagarImp> lista = concepto.lstPorcentaje;
                        try
                        {
                            Decimal baseim = (from valor in concepto.lstPorcentaje
                                              where valor.porcentaje_impuesto == impuesto.porcentaje_impuesto
                                              select valor.base_minima).FirstOrDefault().Value;
                            lblBase.Text = baseim == 0 ? "0" : baseim.ToString();




                        }
                        catch { }


                    }

                    if (impuesto.cod_cuenta_imp != null)
                    {
                        lblCuenta1.Text = impuesto.cod_cuenta_imp;
                    }
                }

            }

        }
      
  
 }

    protected void InicializarDetalle()
    {
        List<CuentaXpagar_Detalle> lstDetalle = new List<CuentaXpagar_Detalle>();
        for (int i = gvDetalle.Rows.Count; i < 20; i++)
        {
            CuentaXpagar_Detalle eActi = new CuentaXpagar_Detalle();
            eActi.coddetallefac = -1;
            eActi.cod_concepto_fac = null;
            eActi.detalle = "";
            eActi.centro_costo = null;
            eActi.cantidad = null;
            eActi.valor_unitario = null;
            eActi.valor_total = null;
            eActi.porc_descuento = null;
            eActi.valor_neto = null;
            lstDetalle.Add(eActi);
        }
        gvDetalle.DataSource = lstDetalle;
        gvDetalle.DataBind();
        Session["DatosDetalle"] = lstDetalle;
    }

    protected void InicializarFormaPago()
    {
        List<CuentaXpagar_Pago> lstDetalle = new List<CuentaXpagar_Pago>();
        for (int i = gvFormaPago.Rows.Count; i < 3; i++)
        {
            CuentaXpagar_Pago eActi = new CuentaXpagar_Pago();
            if (i == 0)
            {
                eActi.codpagofac = -1;
                eActi.numero = i + 1;
                eActi.fecha = Convert.ToDateTime(txtFechaIngreso.Text);
                eActi.porcentaje = 100;
                eActi.valor = null;
                eActi.porc_descuento = null;
                eActi.fecha_descuento = null;
                eActi.valor_descuento = null;
                eActi.vr_ConDescuento = null;
                lstDetalle.Add(eActi);
            }
            else
            {
                eActi.codpagofac = -1;
                eActi.numero = null;
                eActi.fecha = null;
                eActi.porcentaje = null;
                eActi.valor = null;
                eActi.porc_descuento = null;
                eActi.fecha_descuento = null;
                eActi.valor_descuento = null;
                eActi.vr_ConDescuento = null;
                lstDetalle.Add(eActi);
            }
        }
        gvFormaPago.DataSource = lstDetalle;
        gvFormaPago.DataBind();
        Session["DatosFormaPago"] = lstDetalle;
    }


    protected List<CuentaXpagar_Pago> ObtenerListaFormaPago()
    {
        List<CuentaXpagar_Pago> lstFormaPago = new List<CuentaXpagar_Pago>();
        List<CuentaXpagar_Pago> lista = new List<CuentaXpagar_Pago>();

        int cantRow = 0;
        decimal vrtotal = 0, vrReemplazo = 0, neto = 0, totalPorc = 0;
        if (gvFormaPago.Rows.Count > 0)
            cantRow = gvFormaPago.Rows.Count - 1;
        Label lblVrNeto = (Label)gvDetalle.FooterRow.FindControl("lblVrNeto");
        neto = Convert.ToDecimal(lblVrNeto.Text.Replace("$", "").Replace(",", "").Trim());
        neto = ConvertirStringToDecimal(lblVrNeto.Text);
        foreach (GridViewRow rfila in gvFormaPago.Rows)
        {
            CuentaXpagar_Pago eForma = new CuentaXpagar_Pago();

            Label lblCodPago = (Label)rfila.FindControl("lblCodPago");
            if (lblCodPago != null)
                eForma.codpagofac = Convert.ToInt32(lblCodPago.Text);

            TextBox txtnumero = (TextBox)rfila.FindControl("txtnumero");
            if (txtnumero.Text != "")
                eForma.numero = Convert.ToInt32(txtnumero.Text);

            fecha txtfecha = (fecha)rfila.FindControl("txtfecha");
            if (txtfecha.Text != "")
                eForma.fecha = Convert.ToDateTime(txtfecha.Text);

            TextBoxGrid txtPorcentaje = (TextBoxGrid)rfila.FindControl("txtPorcentaje");
            if (txtPorcentaje.Text != "")
                eForma.porcentaje = Convert.ToDecimal(txtPorcentaje.Text);

            TextBoxGrid txtValor = (TextBoxGrid)rfila.FindControl("txtValor");
            if (txtValor.Text != "" && txtValor.Text != "0")
                eForma.valor = Convert.ToDecimal(txtValor.Text);

            totalPorc += Convert.ToDecimal(eForma.porcentaje);
            vrtotal += Convert.ToDecimal(eForma.valor);
            if (cantRow == rfila.RowIndex)
            {
                //---validando que cuadre el valor dividido con el monto neto.
                if (lblVrNeto.Text != "" && neto != null && neto != 0 && totalPorc == 100)
                {
                    Int64 cuota = Convert.ToInt64(neto.ToString().Replace(",", "")); //.ToString().Replace(".", "")
                    if (vrtotal > cuota)
                    {
                        vrReemplazo = vrtotal - cuota;
                        eForma.valor = eForma.valor - vrReemplazo;
                    }
                    else if (vrtotal < cuota)
                    {
                        vrReemplazo = cuota - vrtotal;
                        eForma.valor = eForma.valor + vrReemplazo;
                    }
                }
            }

            TextBoxGrid txtPorDsctoPago = (TextBoxGrid)rfila.FindControl("txtPorDsctoPago");
            if (txtPorDsctoPago.Text != "")
                eForma.porc_descuento = Convert.ToDecimal(txtPorDsctoPago.Text);

            if (cbDescuentos.Checked)
            {
                fecha txtfechaDscto = (fecha)rfila.FindControl("txtfechaDscto");
                if (txtfechaDscto.Text != "")
                    eForma.fecha_descuento = Convert.ToDateTime(txtfechaDscto.Text);

                //decimalesGridRow txtValorDscto = (decimalesGridRow)rfila.FindControl("txtValorDscto");
                Label txtValorDscto = (Label)rfila.FindControl("txtValorDscto");
                if (txtValorDscto.Text != "")
                    eForma.vr_ConDescuento = Convert.ToDecimal(txtValorDscto.Text);

                decimal total = 0, dscto = 0;
                if (eForma.porc_descuento != null)
                    dscto = Convert.ToDecimal(eForma.porc_descuento);
                if (txtValor.Text != "")
                    total = Convert.ToDecimal(txtValor.Text);

                eForma.valor_descuento = total * (dscto / 100);
            }
            else
            {
                eForma.fecha_descuento = null;
                eForma.valor_descuento = null;
            }
            lista.Add(eForma);
            Session["DatosFormaPago"] = lista;

            if (eForma.numero != null && eForma.fecha != null && eForma.porcentaje != null && eForma.valor != null)
            {
                lstFormaPago.Add(eForma);
            }
        }
        return lstFormaPago;
    }


    decimal sumVrTotal = 0;
    decimal sumDescuen = 0;
    decimal sumVrNeto = 0;

    protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlCentroCosto = (DropDownListGrid)e.Row.FindControl("ddlCentroCosto");
            if (ddlCentroCosto != null)
                PoblarLista("centro_costo", ddlCentroCosto);

            DropDownListGrid ddlConcepto = (DropDownListGrid)e.Row.FindControl("ddlConcepto");
            if (ddlConcepto != null)
                PoblarLista("CONCEPTO_CUENTAXPAGAR", ddlConcepto);


            Label lblCentroCosto = (Label)e.Row.FindControl("lblCentroCosto");
            if (lblCentroCosto != null)
                ddlCentroCosto.SelectedValue = lblCentroCosto.Text;

            Label lblConcepto = (Label)e.Row.FindControl("lblConcepto");
            if (lblConcepto != null)
                ddlConcepto.SelectedValue = lblConcepto.Text;

            //calcular valor neto
            decimal neto, total = 0, pordes = 0; //poriva = 0, porreten = 0, reteniva = 0, retentimbre = 0
            TextBoxGrid txtVrTotal = (TextBoxGrid)e.Row.FindControl("txtVrTotal");
            if (txtVrTotal.Text != "")
                total = Convert.ToDecimal(txtVrTotal.Text);
            TextBoxGrid txtPorDesc = (TextBoxGrid)e.Row.FindControl("txtPorDesc");
            if (txtPorDesc.Text != "")
                pordes = Convert.ToDecimal(txtPorDesc.Text);

            Label txtVrNeto = (Label)e.Row.FindControl("txtVrNeto");
            if (txtVrNeto != null)
            {
                neto = (total - (total * (pordes / 100)));  //- (total * (poriva / 100)) - (total * (porreten / 100)) - (total * (reteniva / 100)) - (total * (retentimbre / 100))
                txtVrNeto.Text = neto.ToString("n0");
                sumVrNeto += neto;
            }

            sumVrTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor_total"));
            sumDescuen += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "porc_descuento"));

            //SI EL VR TOTAL TIENE DATOS SE ACTIVAN EL CAMPO DE PORCENTAJE
            if (txtVrTotal.Text != "")
            {
                txtPorDesc.Enabled = true;
            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Totales:";
            e.Row.Cells[7].Text = sumVrTotal.ToString("c0");
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[8].Text = sumDescuen.ToString();
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            //e.Row.Cells[10].Text = sumVrNeto.ToString("c0");
            //e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Font.Bold = true;
        }
    }




    protected void gvDetalle_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvDetalle.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaDetalle();

        List<CuentaXpagar_Detalle> LstDeta;
        LstDeta = (List<CuentaXpagar_Detalle>)Session["DatosDetalle"];

        if (conseID > 0)
        {
            try
            {
                foreach (CuentaXpagar_Detalle Deta in LstDeta)
                {
                    if (Deta.coddetallefac == conseID)
                    {
                        CuotasXpagar.EliminarCuentasXpagarDetalles(conseID, (Usuario)Session["usuario"], 1); //OPCION 1 Eliminar detalle
                        LstDeta.Remove(Deta);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstDeta.RemoveAt((gvDetalle.PageIndex * gvDetalle.PageSize) + e.RowIndex);
        }

        gvDetalle.DataSourceID = null;
        gvDetalle.DataBind();

        gvDetalle.DataSource = LstDeta;
        gvDetalle.DataBind();

        //RECUPERAR DATOS DE IMPUESTOS POR CONCEPTO
        RecuperarDatosImpuestosXConcepto(LstDeta);

        Session["DatosDetalle"] = LstDeta;
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

    Boolean ValidarDatos()
    {
        CalculaTotalXColumna();
        VerError("");
        if (txtFechaIngreso.Text == "")
        {
            VerError("Seleccione la fecha de Ingreso");
            return false;
        }
        if (txtFechaVenci.Visible == true)
        {
            if (txtFechaVenci.Text == "")
            {
                VerError("Seleccione una fecha de Vencimiento");
                return false;
            }
        }
        if (ddlTipoCuenta.SelectedValue == "0")
        {
            VerError("Seleccione un tipo de Cuenta a Pagar");
            return false;
        }
        if (txtIdProveedor.Text == "")
        {
            VerError("Ingrese la Identificación del Proveedor");
            return false;
        }
        if (gvDetalle.Rows.Count == 0)
        {
            VerError("Ingrese Un valor en el detalle");
            return false;
        }

        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(Convert.ToDateTime(txtFechaIngreso.Text), tipoOpe) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación " + tipoOpe + "= Pago facturas de proveedores");
            return false;
        }

        List<CuentaXpagar_Detalle> LstDetalle = new List<CuentaXpagar_Detalle>();
        LstDetalle = ObtenerListaDetalle();
        if (cbManejaAnti.Checked == true)
            if (txtValorAnti.Text == "")
            {
                VerError("Ingrese el valor de Anticipo");
                return false;
            }

        for (int i = 0; i < LstDetalle.Count; i++)
        {
            if (LstDetalle[i].valor_neto < 0)
            {
                VerError("El Valor Neto no puede ser negativo. Error en el Detalle - Fila " + i + 1);
                return false;
            }
        }


        List<CuentaXpagar_Pago> lstFormaPago = new List<CuentaXpagar_Pago>();
        lstFormaPago = ObtenerListaFormaPago();
        if (lstFormaPago.Count == 0)
        {
            VerError("Debe Ingresar al menos un registro en la Forma de Pago");
            return false;
        }
        else
        {
            int cont = 0;
            decimal Vrtotal = 0, val = 0;

            //FALTA VALIDAR QUE LA SUMA DE LOS VALORES EN FORMA DE PAGO SEA IGUAL AL VALOR TOTAL NETO EN LA GRILLA DE DETALLE
            Label lblVrNeto = (Label)gvDetalle.FooterRow.FindControl("lblVrNeto");
            decimal vrNetoFac = 0;
            if (lblVrNeto.Text != "")
                vrNetoFac = Convert.ToDecimal(lblVrNeto.Text.Replace("$", "").Replace(",", ""));

            foreach (CuentaXpagar_Pago rFila in lstFormaPago)
            {
                cont++;
                if (rFila.valor == null)
                    val = 0;
                else
                    val = Convert.ToDecimal(rFila.valor);
                Vrtotal += val;

                if (rFila.fecha != null && rFila.fecha != DateTime.MinValue)
                {
                    if (rFila.fecha < Convert.ToDateTime(txtFechaIngreso.Text))
                    {
                        VerError("La fecha de forma de pago no puede ser menor a la fecha de Ingreso");
                        return false;
                    }

                    if (cbDescuentos.Checked && cbDescuentos.Visible == true)
                    {
                        if (rFila.fecha_descuento == null)
                        {
                            VerError("Error en los datos de Forma de Pago. Fila " + cont.ToString() + " debe ingresar la fecha de pago con descuento");
                            return false;
                        }
                        if (rFila.porc_descuento == null)
                        {
                            VerError("Error en los datos de Forma de Pago. Fila " + cont.ToString() + " debe ingresar el porcentaje de descuento");
                            return false;
                        }
                    }
                }
            }

            if (vrNetoFac != Vrtotal)
            {
                VerError("La suma de los Valores a pagar en Forma de Pago debe ser igual al total del valor neto");
                return false;
            }
        }


        return true;
    }

    Boolean ValidarGiro()
    {
        bool pGenerarGiro = false;
        CuentaXpagar_Detalle pVar = new CuentaXpagar_Detalle();

        //Validando datos del control de Giro
        if (ctlGiro.IndiceFormaDesem == 0)
        {
            VerError("Seleccione una forma de desembolso");
            return false;
        }
        else
        {
            if (ctlGiro.IndiceFormaDesem == 2 || ctlGiro.IndiceFormaDesem == 3)
            {
                if (ctlGiro.IndiceEntidadOrigen == 0)
                {
                    VerError("Seleccione un Banco de donde se girará");
                    return false;
                }
                if (ctlGiro.IndiceFormaDesem == 3)
                {
                    if (ctlGiro.IndiceEntidadDest == 0)
                    {
                        VerError("Seleccione la Entidad de destino");
                        return false;
                    }
                    if (ctlGiro.TextNumCuenta == "")
                    {
                        VerError("Ingrese el número de la cuenta");
                        return false;
                    }
                }
                if (ctlGiro.IndiceFormaDesem == 4)
                {
                    pGenerarGiro = false;

                    pVar.numero_cuenta_ahorro_vista = !string.IsNullOrWhiteSpace(ctlGiro.ValueCuentaAhorro) ? Convert.ToInt64(ctlGiro.ValueCuentaAhorro) : default(long?);

                    if (!pVar.numero_cuenta_ahorro_vista.HasValue)
                    {
                        VerError("No tiene una cuenta de ahorro a la vista asociada para esta forma de pago!.");

                    }
                }
            }
        }

        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarDatos() && ValidarGiro())
                ctlMensaje.MostrarMensaje("Desea grabar los datos ingresados?");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasXpagar.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            // Determinar código de proceso contable para generar el comprobante
            Int64? rpta = 0;
            if (!panelProceso.Visible && panelGeneral.Visible)
            {
                rpta = ctlproceso.Inicializar(tipoOpe, Convert.ToDateTime(txtFechaIngreso.Text), (Usuario)Session["Usuario"]);
                if (rpta > 1)
                {
                    Site toolBar = (Site)Master;
                    toolBar.MostrarGuardar(false);
                    toolBar.MostrarCancelar(false);
                    toolBar.MostrarConsultar(false);
                    mvCuentasxPagar.Visible = false;
                    panelGeneral.Visible = false;
                    panelProceso.Visible = true;
                }

                else
                {
                    // pGiro.valor = Convert.ToInt64(this.txtTotalPagar.Text.Replace(".", ""));
                    if (GenerarCuentaXpagar())
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasXpagar.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }


    protected bool GenerarCuentaXpagar()
    {

        Usuario usuap = (Usuario)Session["usuario"];

        Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
        Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        bool pGenerarGiro = true;
        CuentaXpagar_Detalle pVar = new CuentaXpagar_Detalle();
        // CREAR OPERACION
        pOperacion.cod_ope = 0;
        pOperacion.tipo_ope = tipoOpe;
        pOperacion.cod_caja = 0;
        pOperacion.cod_cajero = 0;
        pOperacion.observacion = "Operacion-Cuentas Por Pagar";
        pOperacion.cod_proceso = null;
        pOperacion.fecha_oper = Convert.ToDateTime(txtFechaIngreso.Text);
        pOperacion.fecha_calc = DateTime.Now;
        pOperacion.cod_ofi = usuap.cod_oficina;

        CuentasPorPagar vCuentas = new CuentasPorPagar();

        if (idObjeto != "")
            vCuentas.codigo_factura = Convert.ToInt32(txtCodigo.Text);
        else
            vCuentas.codigo_factura = 0;

        if (txtNumFactura.Visible == true)
        {
            if (txtNumFactura.Text != "")
                vCuentas.numero_factura = txtNumFactura.Text;
            else
                vCuentas.numero_factura = null;
        }
        else
            vCuentas.numero_factura = null;

        vCuentas.fecha_ingreso = Convert.ToDateTime(txtFechaIngreso.Text);

        if (txtFechaFact.Visible == true)
        {
            if (txtFechaFact.Text != "")
                vCuentas.fecha_factura = Convert.ToDateTime(txtFechaFact.Text);
            else
                vCuentas.fecha_factura = DateTime.MinValue;
        }
        else
            vCuentas.fecha_factura = DateTime.MinValue;

        if (txtFechaRadia.Visible == true)
        {
            if (txtFechaRadia.Text != "")
                vCuentas.fecha_radicacion = Convert.ToDateTime(txtFechaRadia.Text);
            else
                vCuentas.fecha_radicacion = DateTime.MinValue;
        }
        else
            vCuentas.fecha_radicacion = DateTime.MinValue;

        if (txtFechaVenci.Visible == true)
            vCuentas.fecha_vencimiento = Convert.ToDateTime(txtFechaVenci.Text);
        else
            vCuentas.fecha_vencimiento = DateTime.MinValue;

        vCuentas.idtipo_cta_por_pagar = Convert.ToInt32(ddlTipoCuenta.SelectedValue);

        if (txtDocEquiva.Text != "")
            vCuentas.doc_equivalente = Convert.ToDecimal(txtDocEquiva.Text);
        else
            vCuentas.doc_equivalente = 0;

        if (txtNroContra.Visible == true)
        {
            if (txtNroContra.Text != "")
                vCuentas.num_contrato = txtNroContra.Text;
            else
                vCuentas.num_contrato = null;
        }
        else
            vCuentas.num_contrato = null;

        if (txtPoliza.Visible == true)
        {
            if (txtPoliza.Text != "")
                vCuentas.poliza = txtPoliza.Text;
            else
                vCuentas.poliza = null;
        }
        else
            vCuentas.poliza = null;

        if (txtVence.Visible == true)
        {
            if (txtVence.Text != "")
                vCuentas.vence_contrato = Convert.ToDateTime(txtVence.Text);
            else
                vCuentas.vence_contrato = DateTime.MinValue;
        }
        else
            vCuentas.vence_contrato = DateTime.MinValue;

        vCuentas.cod_persona = Convert.ToInt64(txtCodProveedor.Text);
        vCuentas.nombre = txtNomProveedor.Text.ToUpper();

        if (cbDescuentos.Visible == true)
        {
            if (cbDescuentos.Checked)
                vCuentas.maneja_descuentos = 1;
            else
                vCuentas.maneja_descuentos = 0;
        }
        else
            vCuentas.maneja_descuentos = 0;

        if (cbManejaAnti.Checked)
            vCuentas.maneja_anticipos = 1;
        else
            vCuentas.maneja_anticipos = 0;

        if (txtValorAnti.Enabled == true)
            vCuentas.valor_anticipo = Convert.ToDecimal(txtValorAnti.Text);
        else
            vCuentas.valor_anticipo = 0;

        if (txtObservacion.Text != "")
            vCuentas.observaciones = txtObservacion.Text;
        else
            vCuentas.observaciones = null;
        vCuentas.estado = 1;

        if (txtNumFactura.Text != "" && txtNumFactura.Text != "0")
            vCuentas.numero_factura = txtNumFactura.Text;
        else vCuentas.numero_factura = null;
        //GRABACION DEL GIRO A REALIZAR
        Xpinn.FabricaCreditos.Services.AvanceService AvancServices = new Xpinn.FabricaCreditos.Services.AvanceService();
        Usuario pusu = (Usuario)Session["usuario"];
        Xpinn.Tesoreria.Entities.Giro pGiro = new Xpinn.Tesoreria.Entities.Giro();
        pGiro.idgiro = 0;
        pGiro.cod_persona = Convert.ToInt64(txtCodProveedor.Text);
        pGiro.forma_pago = Convert.ToInt32(ctlGiro.ValueFormaDesem);
        pGiro.tipo_acto = 11;
        pGiro.fec_reg = Convert.ToDateTime(txtFechaIngreso.Texto);
        pGiro.fec_giro = DateTime.Now;
        pGiro.numero_radicacion = 0;
        pGiro.usu_gen = pusu.nombre;
        pGiro.usu_apli = null;
        pGiro.estadogi = 0;
        pGiro.usu_apro = null;


        if (ctlGiro.IndiceFormaDesem == 1)          //Efectivo
        {
            pGiro.idctabancaria = 0;
            pGiro.cod_banco = 0;
            pGiro.num_cuenta = "";
            pGiro.tipo_cuenta = -1;
        }
        if (ctlGiro.IndiceFormaDesem != 1 && ctlGiro.IndiceFormaDesem != 4)
        {
            //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
            CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ctlGiro.ValueEntidadOrigen), ctlGiro.TextCuentaOrigen, (Usuario)Session["usuario"]);
            Int64 idCta = CuentaBanc.idctabancaria;
            //DATOS DE FORMA DE PAGO
            if (ctlGiro.IndiceFormaDesem == 3)      //Transferencia Bancaria
            {
                pGiro.idctabancaria = idCta;
                pGiro.cod_banco = Convert.ToInt32(ctlGiro.ValueEntidadDest);
                pGiro.num_cuenta = ctlGiro.TextNumCuenta;
                pGiro.tipo_cuenta = Convert.ToInt32(ctlGiro.ValueTipoCta);
            }
            else if (ctlGiro.IndiceFormaDesem == 2) //Cheque
            {
                pGiro.idctabancaria = idCta;
                pGiro.cod_banco = 0;
                pGiro.num_cuenta = null;
                pGiro.tipo_cuenta = -1;
            }
            else if (ctlGiro.IndiceFormaDesem == 4)  //Transferencia a Cuenta de Ahorros
            {
                pGenerarGiro = false;
                pVar.numero_cuenta_ahorro_vista = !string.IsNullOrWhiteSpace(ctlGiro.ValueCuentaAhorro) ? Convert.ToInt64(ctlGiro.ValueCuentaAhorro) : default(long?);
                if (!pVar.numero_cuenta_ahorro_vista.HasValue)
                {
                    VerError("No tiene una cuenta de ahorro a la vista asociada para esta forma de pago!.");
                }
            }
            else
            {
                pGiro.idctabancaria = 0;
                pGiro.cod_banco = 0;
                pGiro.num_cuenta = "";
                pGiro.tipo_cuenta = -1;
            }
        }
        if (ctlGiro.Visible == true)
        {
            pGenerarGiro = true;
        }


        pGiro.fec_apro = DateTime.MinValue;
        pGiro.cob_comision = 0;
        vCuentas.saldo = 1;
        vCuentas.lstDetalle = new List<CuentaXpagar_Detalle>();
        vCuentas.lstDetalle = ObtenerListaDetalle();
        vCuentas.lstFormaPago = new List<CuentaXpagar_Pago>();
        vCuentas.lstFormaPago = ObtenerListaFormaPago();

        if (idObjeto != "")
        {
            if (lblCod_Ope.Text != "")
                vCuentas.cod_ope = Convert.ToInt64(lblCod_Ope.Text.Trim());
            CuotasXpagar.ModificarCuentasXpagar(vCuentas, pOperacion, (Usuario)Session["usuario"]);
            Navegar(Pagina.Lista);
        }
        else
        {
            CuotasXpagar.CrearCuentasXpagar(vCuentas, pOperacion, pGenerarGiro, Convert.ToInt64(ctlGiro.IndiceFormaDesem), pGiro, (Usuario)Session["usuario"]);
        }


        if (pOperacion.cod_ope != 0)
        {
            ctlproceso.CargarVariables(pOperacion.cod_ope, tipoOpe, Convert.ToDateTime(txtFechaIngreso.Text), Convert.ToInt64(txtCodProveedor.Text.ToString().Trim()), ((Usuario)Session["usuario"]).cod_oficina, (Usuario)Session["usuario"]);
        }
        else
        {
            return false;
        }

        return true;

    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        if (mvCuentasxPagar.ActiveViewIndex == 1)
        {
            mvCuentasxPagar.ActiveViewIndex = 0;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
            toolBar.MostrarImprimir(true);
        }
        else if (mvCuentasxPagar.ActiveViewIndex == 0)
            Navegar(Pagina.Lista);
        else if (panelProceso.Visible == true && mvCuentasxPagar.Visible == false)
        {
            mvCuentasxPagar.Visible = true;
            mvCuentasxPagar.ActiveViewIndex = 0;
            panelProceso.Visible = false;
        }
    }


    protected void txtIdProveedor_TextChanged(object sender, EventArgs e)
    {

        Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
        Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();

        DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdProveedor.Text, (Usuario)Session["usuario"]);

        if (DatosPersona.cod_persona != 0)
        {
            if (DatosPersona.cod_persona != 0)
                txtCodProveedor.Text = DatosPersona.cod_persona.ToString();
            if (DatosPersona.identificacion != "")
                txtIdProveedor.Text = DatosPersona.identificacion;
            if (DatosPersona.nombre != "")
                txtNomProveedor.Text = DatosPersona.nombre;
        }
        else
        {
            txtNomProveedor.Text = "";
            txtCodProveedor.Text = "";
        }
    }


    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        ObtenerListaDetalle();

        List<CuentaXpagar_Detalle> lstDetalle = new List<CuentaXpagar_Detalle>();

        if (Session["DatosDetalle"] != null)
        {
            lstDetalle = (List<CuentaXpagar_Detalle>)Session["DatosDetalle"];

            for (int i = 1; i <= 1; i++)
            {
                CuentaXpagar_Detalle eActi = new CuentaXpagar_Detalle();
                eActi.coddetallefac = -1;
                eActi.cod_concepto_fac = null;
                eActi.detalle = "";
                eActi.centro_costo = null;
                eActi.cantidad = null;
                eActi.valor_unitario = null;
                eActi.valor_total = null;
                eActi.porc_descuento = null;
                eActi.valor_neto = null;
                lstDetalle.Add(eActi);
            }
            gvDetalle.DataSource = lstDetalle;
            gvDetalle.DataBind();

            //RECUPERAR DATOS DE IMPUESTOS POR CONCEPTO
            RecuperarDatosImpuestosXConcepto(lstDetalle);

            Session["DatosDetalle"] = lstDetalle;
        }
    }

    protected void RecuperarDatosImpuestosXConcepto(List<CuentaXpagar_Detalle> lstDetalle)
    {
        int cont = 0;
        foreach (CuentaXpagar_Detalle Impuesto in lstDetalle)
        {
            if (Impuesto.lstImpuesto != null && Impuesto.lstImpuesto.Count != 0)
            {
                DataList dtImpuestos = (DataList)gvDetalle.Rows[cont].FindControl("dtImpuestos");
                dtImpuestos.DataSource = Impuesto.lstImpuesto;
                dtImpuestos.DataBind();
            }

            //RECUPERANDO EL VR TOTAL PARA QUE NO SE PIERDA LOS DATOS DEL VALOR NETO CALCULADO
            TextBoxGrid txtVrTotal = (TextBoxGrid)gvDetalle.Rows[cont].FindControl("txtVrTotal");
            if (txtVrTotal != null)
                txtVrTotal_TextChanged(txtVrTotal, null);
            cont++;
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        ObtenerListaFormaPago();

        List<CuentaXpagar_Pago> lstDetalle = new List<CuentaXpagar_Pago>();

        if (Session["DatosFormaPago"] != null)
        {
            lstDetalle = (List<CuentaXpagar_Pago>)Session["DatosFormaPago"];

            for (int i = 1; i <= 1; i++)
            {
                CuentaXpagar_Pago eActi = new CuentaXpagar_Pago();
                eActi.codpagofac = -1;
                eActi.numero = null;
                eActi.fecha = null;
                eActi.porcentaje = null;
                eActi.valor = null;
                eActi.porc_descuento = null;
                eActi.fecha_descuento = null;
                eActi.valor_descuento = null;
                eActi.vr_ConDescuento = null;
                lstDetalle.Add(eActi);
            }
            gvFormaPago.DataSource = lstDetalle;
            gvFormaPago.DataBind();

            Session["DatosDetalle"] = lstDetalle;
        }
        CalculaTotalXColumna();
    }



    protected void gvFormaPago_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvFormaPago.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaFormaPago();

        List<CuentaXpagar_Pago> LstDeta;
        LstDeta = (List<CuentaXpagar_Pago>)Session["DatosFormaPago"];

        if (conseID > 0)
        {
            try
            {
                foreach (CuentaXpagar_Pago Deta in LstDeta)
                {
                    if (Deta.codpagofac == conseID)
                    {
                        CuotasXpagar.EliminarCuentasXpagarDetalles(conseID, (Usuario)Session["usuario"], 2); //OPCION 1 Eliminar Forma de Pago
                        LstDeta.Remove(Deta);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstDeta.RemoveAt((gvFormaPago.PageIndex * gvFormaPago.PageSize) + e.RowIndex);
        }

        gvFormaPago.DataSourceID = null;
        gvFormaPago.DataBind();

        gvFormaPago.DataSource = LstDeta;
        gvFormaPago.DataBind();

        Session["DatosFormaPago"] = LstDeta;
    }

    protected void cbManejaAnti_CheckedChanged(object sender, EventArgs e)
    {
        if (cbManejaAnti.Checked)
        {
            if (gvDetalle.Rows.Count > 0)
                CalculaTotalXColumna();
            txtValorAnti.Enabled = true;
        }
        else
        {
            txtValorAnti.Text = "";
            txtValorAnti.Enabled = false;
        }
    }

    protected void cbDescuentos_CheckedChanged(object sender, EventArgs e)
    {
        if (cbDescuentos.Checked)
        {
            if (gvDetalle.Rows.Count > 0)
                CalculaTotalXColumna();
            for (int i = 0; i < gvFormaPago.Rows.Count; i++)
            {
                Label txtValorDscto = (Label)gvFormaPago.Rows[i].FindControl("txtValorDscto");
                if (txtValorDscto.Text == "" || txtValorDscto.Text == "0")
                    txtValorDscto.Text = null;
                gvFormaPago.Columns[7].Visible = true;
                gvFormaPago.Columns[8].Visible = true;
            }
            CalcularVrPagarDscto();
        }
        else
        {
            for (int i = 0; i < gvFormaPago.Rows.Count; i++)
            {
                gvFormaPago.Columns[7].Visible = false;
                gvFormaPago.Columns[8].Visible = false;
            }

        }
    }



    protected void txtVrTotal_TextChanged(object sender, EventArgs e)
    {
        decimal total = 0, pordes = 0;
        TextBoxGrid txtVrTotal = (TextBoxGrid)sender;
        int rowIndex = Convert.ToInt32(txtVrTotal.CommandArgument);

        //recuperar todos los datos
        TextBoxGrid txtPorDesc = (TextBoxGrid)gvDetalle.Rows[rowIndex].FindControl("txtPorDesc");
        Label txtVrNeto = (Label)gvDetalle.Rows[rowIndex].FindControl("txtVrNeto");

        if (txtVrTotal.Text != "")
        {
            total = Convert.ToDecimal(txtVrTotal.Text);

            if (txtPorDesc.Text != "")
                pordes = Convert.ToDecimal(txtPorDesc.Text);

            decimal totalPorcentaje = 0;
            totalPorcentaje = calcularTotalDePorcentajeImpuesto(rowIndex);

            if (txtVrNeto != null)
                txtVrNeto.Text = (total - (total * (pordes / 100)) - Math.Round((total * (totalPorcentaje / 100)))).ToString("n0");

            txtPorDesc.Enabled = true;

            CalculaTotalXColumna();
        }
        else
        {
            txtPorDesc.Enabled = false;
            txtVrNeto.Text = "0";
            CalculaTotalXColumna();
        }

        foreach (GridViewRow rFila in gvFormaPago.Rows)
        {
            TextBoxGrid txtPorcentaje = (TextBoxGrid)rFila.FindControl("txtPorcentaje");
            txtPorcentaje_TextChanged(txtPorcentaje, null);
        }
    }


    protected void CalculaTotalXColumna()
    {
        decimal Fneto = 0, Ftotal = 0, Fpordes = 0;  // Fporiva = 0, Fporreten = 0, Freteniva = 0, Fretentimbre = 0;
        Label lblVrNeto = (Label)gvDetalle.FooterRow.FindControl("lblVrNeto");
        Label lblVrTotal = (Label)gvDetalle.FooterRow.FindControl("lblVrTotal");
        Label lblPorDesc = (Label)gvDetalle.FooterRow.FindControl("lblPorDesc");

        foreach (GridViewRow rfila in gvDetalle.Rows)
        {
            TextBoxGrid tVrTotal = (TextBoxGrid)rfila.FindControl("txtVrTotal");
            if (tVrTotal.Text != "")
                Ftotal += Convert.ToDecimal(tVrTotal.Text);
            TextBoxGrid tPorDesc = (TextBoxGrid)rfila.FindControl("txtPorDesc");
            if (tPorDesc.Text != "")
                Fpordes += Convert.ToDecimal(tPorDesc.Text);
            Label tVrNeto = (Label)rfila.FindControl("txtVrNeto");
            if (tVrNeto.Text != "")
                Fneto += Convert.ToDecimal(tVrNeto.Text.Replace('$', ' '));
        }
        if (lblVrTotal != null)
            lblVrTotal.Text = Ftotal.ToString("c0");
        if (lblPorDesc != null)
            lblPorDesc.Text = Fpordes.ToString();

        if (txtValorAnti.Text != "")
        {
            decimal valoranticipo = 0;
            valoranticipo = Convert.ToDecimal(txtValorAnti.Text.Replace('$', ' '));
            if (valoranticipo > 0)
            {
                Fneto = Fneto - valoranticipo;
            }

        }
        if (lblVrNeto != null)
            lblVrNeto.Text = Fneto.ToString("c0");
    }


    protected void txtPorDesc_TextChanged(object sender, EventArgs e)
    {
        decimal total = 0, pordes = 0; // poriva = 0, porreten = 0, reteniva = 0, retentimbre = 0;
        TextBoxGrid txtPorDesc = (TextBoxGrid)sender;
        int rowIndex = Convert.ToInt32(txtPorDesc.CommandArgument);

        TextBoxGrid txtVrTotal = (TextBoxGrid)gvDetalle.Rows[rowIndex].FindControl("txtVrTotal");

        Label txtVrNeto = (Label)gvDetalle.Rows[rowIndex].FindControl("txtVrNeto");

        total = Convert.ToDecimal(txtVrTotal.Text);

        if (txtPorDesc.Text != "")
            pordes = Convert.ToDecimal(txtPorDesc.Text);

        decimal totalPorcentaje = 0;
        totalPorcentaje = calcularTotalDePorcentajeImpuesto(rowIndex);

        if (txtVrNeto != null)
            txtVrNeto.Text = (total - (total * (pordes / 100)) - Math.Round((total * (totalPorcentaje / 100)))).ToString("n0");

        CalculaTotalXColumna();

    }

    protected decimal calcularTotalDePorcentajeImpuesto(int Indice)
    {
        string tipoMov = "";
        ConceptoCtaService ConceptoCtaServicio = new ConceptoCtaService();
        DropDownListGrid ddlConcepto = (DropDownListGrid)gvDetalle.Rows[Indice].FindControl("ddlConcepto");
        ////////////////Inicializa variables de impuestos segun el concepto seleccionado///////////////
        List<Concepto_CuentasXpagarImp> lstImpuesto = new List<Concepto_CuentasXpagarImp>();
        Concepto_CuentasXpagarImp pImpuesto = new Concepto_CuentasXpagarImp();
        /////////////////////Termina Inicializacion Variables Impuestos ////////////////
        /////////////////////Llena entiti para consultar y traer impuestos parametrizados del concepto seleccionado///////////////
        pImpuesto.cod_concepto_fac = Convert.ToInt32(ddlConcepto.SelectedValue);
        lstImpuesto = ConceptoCtaServicio.ListarConceptoImpuestoDetalle(pImpuesto, (Usuario)Session["usuario"]);
        ///////////////////// Trae informacion del conepto////////////////////
        ConceptoCtaService conceptoServicio = new ConceptoCtaService();
        ConceptoCta econcepto = new ConceptoCta();
        econcepto = conceptoServicio.ConsultarConceptoCta(Convert.ToInt64(ddlConcepto.SelectedValue), (Usuario)Session["Usuario"]);
        if (econcepto != null)
            if (econcepto.tipo_mov != null)
                tipoMov = econcepto.tipo_mov == 1 ? "D" : "C";

        decimal totalPorcentaje = 0;
        DataList dtImpuestos = (DataList)gvDetalle.Rows[Indice].FindControl("dtImpuestos");
        if (dtImpuestos != null)
        {
            foreach (DataListItem rItem in dtImpuestos.Items)
            {
                DropDownListGrid ddlPorcentaje = (DropDownListGrid)rItem.FindControl("ddlPorcentaje");
                Label lblTipo = (Label)rItem.FindControl("lblTipo");
                Label lblCodImp = (Label)rItem.FindControl("lblCodImp");
                TextBoxGrid txtValor = (TextBoxGrid)gvDetalle.Rows[Indice].FindControl("txtVrTotal");

                if (totalPorcentaje > 0) continue;

                if (lblCodImp.Text != "" && lblCodImp.Text == lstImpuesto[Indice].cod_tipo_impuesto.ToString())
                {
                    foreach (Concepto_CuentasXpagarImp item in lstImpuesto[Indice].lstPorcentaje)
                    {

                      if (ddlPorcentaje.SelectedValue!="")
                        { 
                        //Valida si el impuesto es igual a seleccionado 
                        if (Convert.ToDecimal(ddlPorcentaje.SelectedValue) == item.coddetalleimp)
                            //Valida que el impuesto seleccionado no sea menor de 0 y ademas compara si el valor Unitario con la base que tenga parametrizada el concepto
                            if (Convert.ToDecimal(item.porcentaje_impuesto) > 0 && Convert.ToDecimal(txtValor.Text.Replace(".", "")) >= Convert.ToDecimal(item.base_minima))
                                if (lblTipo.Text == tipoMov)
                                    totalPorcentaje = totalPorcentaje - Convert.ToDecimal(item.porcentaje_impuesto);
                                else
                                    totalPorcentaje = totalPorcentaje + Convert.ToDecimal(item.porcentaje_impuesto);

                        }
                      else
                       {
                            ddlPorcentaje.SelectedIndex = 0;
                        }
                    }
                }
                if (totalPorcentaje == 0 && ddlPorcentaje.SelectedValue != "")
                {
                    ddlPorcentaje.SelectedIndex = 0;
                }

             
            }


        }
       

        return totalPorcentaje;
    }

    protected void txtCantidad_TextChanged(object sender, EventArgs e)
    {
        decimal Vrtotal = 0;

        TextBoxGrid txtCantidad = (TextBoxGrid)sender;
        int rowIndex = Convert.ToInt32(txtCantidad.CommandArgument);

        //recuperar todos los datos
        TextBoxGrid txtVrUnitario = (TextBoxGrid)gvDetalle.Rows[rowIndex].FindControl("txtVrUnitario");
        TextBoxGrid txtVrTotal = (TextBoxGrid)gvDetalle.Rows[rowIndex].FindControl("txtVrTotal");

        if (txtCantidad.Text != "")
        {
            if (txtVrUnitario.Text != "")
                Vrtotal = (Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtVrUnitario.Text));
        }
        if (Vrtotal != 0)
            txtVrTotal.Text = Vrtotal.ToString("n0");
        else
            txtVrTotal.Text = "";
        txtVrTotal_TextChanged(txtVrTotal, null);
    }


    protected void txtVrUnitario_TextChanged(object sender, EventArgs e)
    {
        decimal Vrtotal = 0;

        TextBoxGrid txtVrUnitario = (TextBoxGrid)sender;
        int rowIndex = Convert.ToInt32(txtVrUnitario.CommandArgument);

        //recuperar todos los datos
        TextBoxGrid txtCantidad = (TextBoxGrid)gvDetalle.Rows[rowIndex].FindControl("txtCantidad");
        TextBoxGrid txtVrTotal = (TextBoxGrid)gvDetalle.Rows[rowIndex].FindControl("txtVrTotal");

        if (txtVrUnitario.Text != "")
        {
            if (txtCantidad.Text != "")
                Vrtotal = (Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtVrUnitario.Text));
        }
        if (Vrtotal != 0)
            txtVrTotal.Text = Vrtotal.ToString("n0");
        else
            txtVrTotal.Text = "";
        txtVrTotal_TextChanged(txtVrTotal, null);
    }

    //FORMA PAGO CALCULAR VR_CON DESCUENTO
    protected void txtValor_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        decimal Vrtotal = 0;
        decimal porcent = 0, total = 0;
        TextBoxGrid txtValor = (TextBoxGrid)sender;
        int rowIndex = Convert.ToInt32(txtValor.CommandArgument);
        if (cbDescuentos.Checked)
        {
            //recuperar todos los datos
            TextBoxGrid txtPorDsctoPago = (TextBoxGrid)gvFormaPago.Rows[rowIndex].FindControl("txtPorDsctoPago");
            Label txtValorDscto = (Label)gvFormaPago.Rows[rowIndex].FindControl("txtValorDscto");

            if (txtValor.Text != "")
                total = Convert.ToDecimal(txtValor.Text);

            if (txtPorDsctoPago.Text != "")
                porcent = Convert.ToDecimal(txtPorDsctoPago.Text);

            decimal vrPorc = 0;
            vrPorc = Math.Round(Convert.ToDecimal(total) * (Convert.ToDecimal(porcent) / 100));
            Vrtotal = total - vrPorc;

            txtValorDscto.Text = Vrtotal.ToString("n0");

        }
        CalculaTotalXColumna();
    }


    protected void txtPorDsctoPago_TextChanged(object sender, EventArgs e)
    {
        decimal Vrtotal = 0;
        decimal porcent = 0, total = 0;
        TextBoxGrid txtPorDsctoPago = (TextBoxGrid)sender;
        int rowIndex = Convert.ToInt32(txtPorDsctoPago.CommandArgument);
        if (txtPorDsctoPago.Text != "")
            if (Convert.ToDecimal(txtPorDsctoPago.Text) >= 100)
            {
                txtPorDsctoPago.Text = "";
            }
        if (cbDescuentos.Checked)
        {
            TextBoxGrid txtValor = (TextBoxGrid)gvFormaPago.Rows[rowIndex].FindControl("txtValor");
            Label txtValorDscto = (Label)gvFormaPago.Rows[rowIndex].FindControl("txtValorDscto");

            if (txtValor.Text != "")
                total = Convert.ToDecimal(txtValor.Text);

            if (txtPorDsctoPago.Text != "")
                porcent = Convert.ToDecimal(txtPorDsctoPago.Text);

            decimal vrPorc = 0;
            vrPorc = Math.Round(Convert.ToDecimal(total) * (Convert.ToDecimal(porcent) / 100));
            Vrtotal = total - vrPorc;

            txtValorDscto.Text = Vrtotal.ToString("n0");
        }
        CalculaTotalXColumna();
    }

    protected void CalcularVrPagarDscto()
    {
        foreach (GridViewRow rFila in gvFormaPago.Rows)
        {
            decimal Total = 0;
            decimal porcent = 0, total = 0;
            TextBoxGrid txtValor = (TextBoxGrid)rFila.FindControl("txtValor");
            TextBoxGrid txtPorDsctoPago = (TextBoxGrid)rFila.FindControl("txtPorDsctoPago");
            Label txtValorDscto = (Label)rFila.FindControl("txtValorDscto");

            if (txtValor.Text != "")
                total = Convert.ToDecimal(txtValor.Text);

            if (txtPorDsctoPago.Text != "")
                porcent = Convert.ToDecimal(txtPorDsctoPago.Text);

            decimal vrPorc = 0;
            vrPorc = Math.Round(Convert.ToDecimal(total) * (Convert.ToDecimal(porcent) / 100));
            Total = total - vrPorc;

            txtValorDscto.Text = Total.ToString("n0");
        }
    }


    protected void habilitar_1eraFila(Boolean rpta)
    {
        lblNumFactura.Visible = rpta;
        txtNumFactura.Visible = rpta;
        lblFechaFact.Visible = rpta;
        txtFechaFact.Visible = rpta;
        lblFechaRadia.Visible = rpta;
        txtFechaRadia.Visible = rpta;
        lblFechaVenci.Visible = rpta;
        txtFechaVenci.Visible = rpta;
    }

    protected void habilitar_2daFila(Boolean rpta)
    {
        lblNroContra.Visible = rpta;
        txtNroContra.Visible = rpta;
        lblPoliza.Visible = rpta;
        txtPoliza.Visible = rpta;
        lblVence.Visible = rpta;
        txtVence.Visible = rpta;
    }

    protected void ddlTipoCuenta_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //1ERA FILA
            habilitar_1eraFila(true);

            //2DA FILA 
            habilitar_2daFila(false);
            cbDescuentos.Visible = true;
            if (ddlTipoCuenta.SelectedValue == "5")
            {
                habilitar_2daFila(true);
            }
            if (ddlTipoCuenta.SelectedValue == "2")
            {
                habilitar_1eraFila(false);
                habilitar_2daFila(false);
                cbDescuentos.Checked = false;
                cbDescuentos.Visible = false;
                cbDescuentos_CheckedChanged(cbDescuentos, null);
            }
            CalculaTotalXColumna();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void ddlConcepto_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Tesoreria.Services.ConceptoCtaService ConceptoCtaServicio = new Xpinn.Tesoreria.Services.ConceptoCtaService();

            DropDownListGrid ddlConcepto = (DropDownListGrid)sender;
            int rowIndex = Convert.ToInt32(ddlConcepto.CommandArgument);

            //LLENANDO EL DATALIST CON LOS IMPUESTOS POR CONCEPTO
            DataList panelGrid = (DataList)gvDetalle.Rows[rowIndex].FindControl("dtImpuestos");

            panelGrid.DataSource = null;

            if (ddlConcepto.SelectedIndex != 0)
            {
                List<Concepto_CuentasXpagarImp> lstImpuesto = new List<Concepto_CuentasXpagarImp>();
                Concepto_CuentasXpagarImp pImpuesto = new Concepto_CuentasXpagarImp();
                pImpuesto.cod_concepto_fac = Convert.ToInt32(ddlConcepto.SelectedValue);

                lstImpuesto = ConceptoCtaServicio.ListarConceptoImpuestoDetalle(pImpuesto, (Usuario)Session["usuario"]);
                Session["lstImpuesto"] = lstImpuesto;

                if (lstImpuesto.Count > 0)
                {
                    panelGrid.DataSource = lstImpuesto;
                    panelGrid.DataBind();
                }
                else
                    panelGrid.DataBind();
            }
            else
                panelGrid.DataBind();

            //CALCULANDO EL VALOR NETO
            //recuperar todos los datos
            TextBoxGrid txtVrTotal = (TextBoxGrid)gvDetalle.Rows[rowIndex].FindControl("txtVrTotal");
            txtVrTotal_TextChanged(txtVrTotal, null);

            //CALCULANDO EL VALOR TOTAL POR COLUMNA
            CalculaTotalXColumna();

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void ddlPorcentaje_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownListGrid ddlPorcentaje = (DropDownListGrid)sender;
            DataListItem dtImpuestosItem = (DataListItem)ddlPorcentaje.NamingContainer;
            DataList dtImpuestos = (DataList)dtImpuestosItem.NamingContainer;
            GridViewRow rFila = (GridViewRow)dtImpuestos.NamingContainer;
            int index = Convert.ToInt32(rFila.RowIndex);
            ddlPorcentaje.SelectedIndex = Convert.ToInt32(ddlPorcentaje.SelectedIndex);
            ActualizarPorcentajes(null, null, index);
            var s = ddlPorcentaje.SelectedItem.Text;
            TextBoxGrid txtVrTotal = (TextBoxGrid)rFila.FindControl("txtVrTotal");
            txtVrTotal_TextChanged(txtVrTotal, null);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    //Cargar datasource para porcentaje en caso de que un impuesto tenga varios porcentajes
    protected void dtImpuestos_ItemDataBound(object sender, DataListItemEventArgs e)
    {


        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            List<Concepto_CuentasXpagarImp> lstImpuestos = new List<Concepto_CuentasXpagarImp>();
            lstImpuestos = (List<Concepto_CuentasXpagarImp>)Session["lstImpuesto"];
            Label lblID = (Label)e.Item.FindControl("lblID"); //Cod detalle impuesto

            foreach (Concepto_CuentasXpagarImp impuesto in lstImpuestos.Where(x => x.coddetalleimp == Convert.ToInt64(lblID.Text)))
            {
                Label lblCodImp = e.Item.FindControl("lblCodImp") as Label;
                Label lblBase = e.Item.FindControl("lblBase") as Label;
                Label lblTipo = e.Item.FindControl("lblTipo") as Label;
                Label lblTitulo = e.Item.FindControl("lblTitulo") as Label;


                Label lblCuenta1 = e.Item.FindControl("lblCuenta1") as Label;

                if (lblCodImp.Text == impuesto.cod_tipo_impuesto.ToString())
                {
                    DropDownList ddlPorcentaje = e.Item.FindControl("ddlPorcentaje") as DropDownList;
                    if (impuesto.lstPorcentaje != null)
                    {
                        ddlPorcentaje.DataTextField = "porcentaje_impuesto";
                        ddlPorcentaje.DataValueField = "coddetalleimp";
                        ddlPorcentaje.DataSource = impuesto.lstPorcentaje;

                        ddlPorcentaje.DataBind();
                        ddlPorcentaje.Items.Insert(0, "0");
                        ddlPorcentaje.SelectedIndex = 0;
                    }
                    else
                    {
                        contador = contador == lstImpuestos.Count ? 0 : contador;
                        if (contador < 1)
                        {
                            ddlPorcentaje.DataTextField = "porcentaje_impuesto";
                            ddlPorcentaje.DataValueField = "coddetalleimp";
                            ddlPorcentaje.DataSource = lstImpuestos;
                            ddlPorcentaje.DataBind();

                            ddlPorcentaje.Visible = true;
                            lblTitulo.Visible = true;
                            lblCuenta1.Visible = true;
                        }

                    }

                    if (impuesto.lstPorcentaje != null)
                    {
                        if (anteriorTipo != impuesto.nom_tipo_impuesto)
                        {
                            ddlPorcentaje.Visible = true;
                            lblTitulo.Visible = true;
                            lblCuenta1.Visible = true;
                        }
                    }


                    //Cargar la base mínina y el movimiento según el porcentaje del impuesto
                    Concepto_CuentasXpagarImp imp = new Concepto_CuentasXpagarImp();
                    if (impuesto.lstPorcentaje != null)
                        imp = impuesto.lstPorcentaje
                            .Where(x => x.porcentaje_impuesto == Convert.ToDecimal(ddlPorcentaje.SelectedValue))
                            .FirstOrDefault();
                    else
                        imp = impuesto;
                    if (imp != null)
                    {
                        if (imp.base_minima != null)
                        {
                            lblBase.Text = imp.base_minima.ToString();
                        }
                        if (imp.naturaleza != null)
                        {
                            lblTipo.Text = imp.naturaleza;
                        }
                        if (imp.cod_cuenta_imp != null)
                        {
                            lblCuenta1.Text = imp.cod_cuenta_imp;

                        }
                    }

                    anteriorTipo = lblTitulo.Text;
                }
            }
        }
        contador++;
    }

    protected void txtPorcentaje_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBoxGrid txtPorcentaje = (TextBoxGrid)sender;
            int rowIndex = Convert.ToInt32(txtPorcentaje.CommandArgument);
            if (txtPorcentaje != null)
            {
                //VALOR NETO
                Label lblVrNeto = (Label)gvDetalle.FooterRow.FindControl("lblVrNeto");
                if (lblVrNeto != null)
                {
                    TextBoxGrid txtValor = (TextBoxGrid)gvFormaPago.Rows[rowIndex].FindControl("txtValor");
                    if (txtValor != null)
                    {
                        if (txtPorcentaje.Text != "")
                        {
                            decimal neto = 0, total = 0, pordes = 0;
                            neto = Convert.ToDecimal(lblVrNeto.Text.Replace("$", "").Replace(".", "").Trim());
                            pordes = Convert.ToDecimal(txtPorcentaje.Text);
                            total = neto * (pordes / 100);
                            txtValor.Text = total.ToString("n0");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        try
        {
            //VALIDANDO SI EXISTEN DATOS POR IMPRIMIR
            List<CuentaXpagar_Detalle> lstDetalle = new List<CuentaXpagar_Detalle>();
            lstDetalle = ObtenerListaDetalle();

            List<CuentaXpagar_Pago> lstFormaPago = new List<CuentaXpagar_Pago>();
            lstFormaPago = ObtenerListaFormaPago();
            if (lstDetalle.Count == 0)
            {
                VerError("No existen datos para imprimir en el detalle de cuentas por Pagar, Verifique los datos.");
                return;
            }
            if (lstFormaPago.Count == 0)
            {
                VerError("No existen datos para imprimir en el detalle de forma de pago, Verifique los datos.");
                return;
            }
            Label lblVrNeto = (Label)gvDetalle.FooterRow.FindControl("lblVrNeto");
            Label lblVrTotal = (Label)gvDetalle.FooterRow.FindControl("lblVrTotal");
            int vrNeto = 0, vrTotal = 0, anticipo = 0;
            vrNeto = Convert.ToInt32(lblVrNeto.Text.Replace("$", "").Replace(",", "").Replace(".", "").Trim());
            vrTotal = Convert.ToInt32(lblVrTotal.Text.Replace("$", "").Replace(",", "").Replace(".", "").Trim());
            if (txtValorAnti.Text != "")
                anticipo = Convert.ToInt32(txtValorAnti.Text.Replace("$", "").Replace(",", "").Replace(".", "").Trim());
            if (vrNeto == 0)
            {
                VerError("No existen datos para imprimir en el detalle de cuentas por Pagar, Verifique los datos.");
                return;
            }
            if (vrTotal == 0)
            {
                VerError("No existen datos para imprimir en el detalle de cuentas por Pagar, Verifique los datos.");
                return;
            }

            if (txtIdProveedor.Text == "")
            {
                VerError("Ingrese la identificacion de la persona.");
                txtIdProveedor.Focus();
                return;
            }
            //CREAR TABLA DETALLE;
            System.Data.DataTable tbDetalle = new System.Data.DataTable();
            tbDetalle.Columns.Add("concecutivo");
            tbDetalle.Columns.Add("concepto");
            tbDetalle.Columns.Add("descripcion");
            tbDetalle.Columns.Add("cantidad");
            tbDetalle.Columns.Add("vrUnitario");
            tbDetalle.Columns.Add("vrParcial");

            //CREAR TABLA IMPUESTOS;
            System.Data.DataTable tbImpuestos = new System.Data.DataTable();
            tbImpuestos.Columns.Add("descripcion");
            tbImpuestos.Columns.Add("porcentaje");
            tbImpuestos.Columns.Add("valor");
            //tbImpuestos.Constraints.Add("PK_IMPUESTOS", tbImpuestos.Columns["descripcion"], true);

            //RECORRIENDO EL DETALLE
            int cont = 0;
            foreach (CuentaXpagar_Detalle rItemRow in lstDetalle)
            {
                DataRow datarw;
                datarw = tbDetalle.NewRow();
                datarw[0] = cont + 1;
                datarw[1] = " ";
                datarw[2] = " ";
                datarw[3] = " ";
                datarw[4] = " ";
                datarw[5] = " ";
                DropDownListGrid ddlConcepto = (DropDownListGrid)gvDetalle.Rows[cont].FindControl("ddlConcepto");
                if (ddlConcepto != null)
                {
                    ddlConcepto.SelectedIndex = 0;
                    if (rItemRow.cod_concepto_fac != null)
                    {
                        ddlConcepto.SelectedValue = rItemRow.cod_concepto_fac.ToString();
                        datarw[1] = ddlConcepto.SelectedItem.Text;
                    }
                }

                TextBox txtDetalle = (TextBox)gvDetalle.Rows[cont].FindControl("txtDetalle");
                if (txtDetalle.Text != "")
                    datarw[2] = txtDetalle.Text;

                TextBoxGrid txtCantidad = (TextBoxGrid)gvDetalle.Rows[cont].FindControl("txtCantidad");
                if (txtCantidad.Text != "")
                    datarw[3] = txtCantidad.Text.Trim();

                TextBoxGrid txtVrUnitario = (TextBoxGrid)gvDetalle.Rows[cont].FindControl("txtVrUnitario");
                if (txtVrUnitario.Text != "")
                    datarw[4] = txtVrUnitario.Text;

                TextBoxGrid txtVrTotal = (TextBoxGrid)gvDetalle.Rows[cont].FindControl("txtVrTotal");
                if (txtVrTotal.Text != "" && txtVrTotal.Text != "0")
                    datarw[5] = txtVrTotal.Text;
                //ADICIONANDO LOS DATOS A DATASET DE IMPUESTOS
                tbDetalle.Rows.Add(datarw);

                decimal vTotal = 0;
                vTotal = txtVrTotal.Text != "" && txtVrTotal.Text != "0" ? Convert.ToDecimal(txtVrTotal.Text.Replace(".", "")) : 0;
                //ADICIONANDO DESCUENTO SI EXISTE
                TextBoxGrid txtPorDesc = (TextBoxGrid)gvDetalle.Rows[cont].FindControl("txtPorDesc");
                if (txtPorDesc != null && txtPorDesc.Text != "" && txtPorDesc.Text != "0")
                {
                    decimal porc = 0, vValor = 0;
                    DataRow drImp;
                    drImp = tbImpuestos.NewRow();
                    drImp[0] = "% Descuento";
                    drImp[1] = txtPorDesc.Text.Trim() + " %";
                    porc = Convert.ToDecimal(txtPorDesc.Text.Trim());
                    vValor = vTotal * porc / 100;
                    drImp[2] = vValor.ToString("n0");
                    tbImpuestos.Rows.Add(drImp);
                }

                if (rItemRow.lstImpuesto.Count > 0)
                {
                    foreach (Concepto_CuentasXpagarImp ImpDeta in rItemRow.lstImpuesto)
                    {
                        //Buscando si existe ya insertado el impuesto
                        decimal porc = ImpDeta.porcentaje_impuesto != null ? Convert.ToDecimal(ImpDeta.porcentaje_impuesto) : 0;
                        string nom_impuesto = ImpDeta.nom_tipo_impuesto != null ? ImpDeta.nom_tipo_impuesto : " ";
                        decimal vValor = 0;
                        if (porc != 0)
                        {
                            DataRow drImp;
                            drImp = tbImpuestos.NewRow();
                            drImp[0] = nom_impuesto;
                            drImp[1] = porc + " %";
                            vValor = vTotal * porc / 100;
                            drImp[2] = vValor.ToString("n0");
                            tbImpuestos.Rows.Add(drImp);
                        }
                        //drImp = tbImpuestos.Rows.Find(nom_impuesto);
                        //if (drImp == null)
                        //{                            
                        //}
                        //else
                        //{
                        //    drImp.BeginEdit();
                        //    //SI YA EXISTE NO SE VUELVE A INGRESAR EL PORCENTAJE                            
                        //    drImp.EndEdit();
                        //}
                        //tbImpuestos.AcceptChanges();
                    }
                }
                cont++;
            }

            for (int i = cont; i < 10; i++)
            {
                DataRow datarw;
                datarw = tbDetalle.NewRow();
                datarw[0] = " ";
                datarw[1] = " ";
                datarw[2] = " ";
                datarw[3] = " ";
                datarw[4] = " ";
                datarw[5] = " ";
                tbDetalle.Rows.Add(datarw);
            }

            //CONSULTANDO DATOS DE LA PERSONA
            Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            vPersona1.soloPersona = 1;
            vPersona1.seleccionar = "Identificacion";
            vPersona1.identificacion = txtIdProveedor.Text.Trim();
            vPersona1 = DatosClienteServicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
            if (vPersona1.nombre == "errordedatos")
            {
                VerError("Error al realizar la consulta de la persona con identificación " + txtIdProveedor.Text.Trim());
                return;
            }
            string pDireccion = "", pTelefono = "";
            pDireccion = vPersona1.direccion != null ? vPersona1.direccion : " ";
            pTelefono = vPersona1.celular != null ? vPersona1.celular : " ";
            if (pTelefono == " ")
                pTelefono = vPersona1.telefono != null ? vPersona1.telefono : " ";

            // PARAMETROS DEL REPORTE
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

            Xpinn.Seguridad.Entities.Empresa empresa = _empresaService.ConsultarEmpresa(pUsuario);
            string resol_facturacion = "";
            string regimen = "";
            if (empresa.resol_facturacion != null)
                resol_facturacion = empresa.resol_facturacion;
            if (empresa.desc_regimen != null)
                regimen = empresa.desc_regimen;

            if (empresa.resol_facturacion == null)
            {
                resol_facturacion = "Factura Equivalente No.";
            }

            if (empresa.desc_regimen == null)
            {
                regimen = "RESPONSABLE DE IVA REGIMEN COMUN";
            }

            ReportParameter[] param = new ReportParameter[15];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", "NIT: " + pUsuario.nitempresa);
            param[2] = new ReportParameter("ImagenReport", ImagenReporte());
            param[3] = new ReportParameter("nombre", " " + txtNomProveedor.Text);
            param[4] = new ReportParameter("identificacion", " " + txtIdProveedor.Text);
            param[5] = new ReportParameter("direccion", pDireccion);
            param[6] = new ReportParameter("telefono", pTelefono);
            param[7] = new ReportParameter("nroEquivalente", txtDocEquiva.Text);
            param[8] = new ReportParameter("vrParcial", vrTotal.ToString("n0"));
            param[9] = new ReportParameter("vrNeto", vrNeto.ToString("n0"));
            param[10] = new ReportParameter("tipoCtaxPagar", " " + ddlTipoCuenta.SelectedItem.Text);
            param[11] = new ReportParameter("codtipoCtaxPagar", " " + ddlTipoCuenta.SelectedValue);
            param[12] = new ReportParameter("p_regimen", " " + regimen.ToString());
            param[13] = new ReportParameter("p_resolucion_facturacion", " " + resol_facturacion.ToString());
            param[14] = new ReportParameter("anticipo", anticipo.ToString("n0"));


            rvCuentaXpagar.LocalReport.EnableExternalImages = true;
            rvCuentaXpagar.LocalReport.SetParameters(param);
            rvCuentaXpagar.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet1", tbImpuestos);
            rvCuentaXpagar.LocalReport.DataSources.Add(rds);
            ReportDataSource rds1 = new ReportDataSource("DataSet2", tbDetalle);
            rvCuentaXpagar.LocalReport.DataSources.Add(rds1);
            rvCuentaXpagar.LocalReport.Refresh();

            mvCuentasxPagar.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarImprimir(false);
            rvCuentaXpagar.Visible = true;
            frmPrint.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasXpagar.CodigoPrograma, "btnImprimir_Click", ex);
        }
    }


    protected void btnImprime_Click(object sender, EventArgs e)
    {
        if (rvCuentaXpagar.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = rvCuentaXpagar.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
            Usuario pUsuario = new Usuario();
            string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_" + pUsuario.nombre : "";
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output" + pNomUsuario + ".pdf"),
            FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            Session["Archivo" + Usuario.codusuario] = Server.MapPath("output" + pNomUsuario + ".pdf");
            frmPrint.Visible = true;
            rvCuentaXpagar.Visible = false;
        }
    }

    protected void btnVerData_Click(object sender, EventArgs e)
    {
        mvCuentasxPagar.ActiveViewIndex = 0;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarImprimir(true);
    }

    protected void preRender(GridView pGrid)
    {
        foreach (GridViewRow ins in pGrid.Rows)
        {
            TextBoxGrid txtValor;
            if (pGrid.ID == "gvFormaPago")
                txtValor = (TextBoxGrid)ins.FindControl("txtValor");
            else
                txtValor = (TextBoxGrid)ins.FindControl("txtVrUnitario");

            string str = txtValor.Text;
            string strDec = "";
            int posDec = 0;
            string formateado = "";

            string s = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (s == ".")
                str = str.Replace(",", "");
            else
            {
                str = str.Replace(".", "");
                str = str.Replace(",", ".");
            }

            try
            {
                //posDec = str.IndexOf(",");
                posDec = s == "." ? str.IndexOf(",") : str.IndexOf(".");
                if (posDec > 0)
                {
                    strDec = str.Substring(posDec + 1, str.Length - (posDec + 1));
                    str = str.Substring(0, posDec);
                }
                if (str != "" && Convert.ToInt64(str) > 0)
                {
                    var strI = Convert.ToInt64(str);  //Convierte a entero y luego a string para quitar ceros a la izquierda
                    str = strI.ToString();

                    if (str.Length > 10)
                    { str = str.Substring(0, 10); }

                    int longi = str.Length;
                    string mill = "";
                    string mil = "";
                    string cen = "";


                    if (longi > 0 && longi <= 3)
                    {
                        cen = str.Substring(0, longi);
                        formateado = Convert.ToInt64(cen).ToString();
                    }
                    else if (longi > 3 && longi <= 6)
                    {
                        mil = str.Substring(0, longi - 3);
                        cen = str.Substring(longi - 3, 3);
                        formateado = Convert.ToInt64(mil) + "." + cen;
                    }
                    else if (longi > 6 && longi <= 10)
                    {
                        mill = str.Substring(0, longi - 6);
                        mil = str.Substring(longi - 6, 3);
                        cen = str.Substring(longi - 3, 3);
                        formateado = Convert.ToInt64(mill) + "." + mil + "." + cen;
                    }
                    else
                    { formateado = ""; }

                    if (posDec > 0 && formateado != "")
                    {
                        formateado = formateado + "," + strDec;
                    }

                }
                else { formateado = ""; }
                txtValor.Text = formateado.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }

    protected void txtVrUnitario_PreRender(object sender, EventArgs e)
    {
        preRender(gvDetalle);
    }

    protected void txtValor_PreRender(object sender, EventArgs e)
    {
        preRender(gvFormaPago);
    }


    #region VALIDACION DE PROCESO

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            if (!GenerarCuentaXpagar())
                return;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    #endregion



    protected void txtCodProveedor_TextChanged(object sender, EventArgs e)
    {
        Int64 cod_deudor = 0;
        if (txtCodProveedor.Text != null)
        {
            cod_deudor = Convert.ToInt64(txtCodProveedor.Text);
            ctlGiro.cargarCuentasAhorro(cod_deudor);

        }
    }

    protected void txtValorAnti_TextChanged(object sender, EventArgs e)
    {
        CalculaTotalXColumna();
    }


}