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
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;
using Microsoft.Reporting.WebForms;


partial class Nuevo : GlobalWeb
{
    ConciliacionBancariaServices ConciliacionServ = new ConciliacionBancariaServices();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            //if (Session[ConciliacionServ.CodigoPrograma + ".id"] != null)
            //    VisualizarOpciones(ConciliacionServ.CodigoPrograma, "E");
            //else
            //    VisualizarOpciones(ConciliacionServ.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarImprimir(false);
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConciliacionServ.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvPrincipal.Visible = true;
                mvPrincipal.ActiveViewIndex = 0;
                mvACH.Visible = false;
                mvReporte.Visible = false;
                //mvACH.ActiveViewIndex = 1;
                CargarDropDown();
                txtEntidad.Enabled = false;
                txtTipoCuenta.Enabled = false;
                txtCodCuenta.Enabled = false;
                txtNombreCta.Enabled = false;
                Usuario vUsu = (Usuario)Session["usuario"];
                txtUsuElabora.Text = vUsu.nombre;

                Session["LstRegistros"] = null;
                if (Session[ConciliacionServ.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[ConciliacionServ.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ConciliacionServ.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }                
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConciliacionServ.CodigoPrograma, "Page_Load", ex);
        }
    }

    
    void CargarDropDown()
    {
        ConciliacionBancaria vConci = new ConciliacionBancaria();
        List<ConciliacionBancaria> lstCuentas = new List<ConciliacionBancaria>();

        lstCuentas = ConciliacionServ.ListarCuentasBancarias(vConci, (Usuario)Session["usuario"]);
        if (lstCuentas.Count > 0)
        {
            ddlCuentaBanc.DataSource = lstCuentas;
            ddlCuentaBanc.DataTextField = "NUM_CUENTA";
            ddlCuentaBanc.DataValueField = "IDCTABANCARIA";
            ddlCuentaBanc.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlCuentaBanc.SelectedIndex = 0;
            ddlCuentaBanc.DataBind();
        }

        List<ConciliacionBancaria> lstExtracto = new List<ConciliacionBancaria>();

        lstExtracto = ConciliacionServ.ListarExtracto(vConci, (Usuario)Session["usuario"]);
        if (lstExtracto.Count > 0)
        {
            ddlExtracto.DataSource = lstExtracto;
            ddlExtracto.DataTextField = "IDEXTRACTO";
            ddlExtracto.DataValueField = "IDEXTRACTO";
            ddlExtracto.Items.Insert(0, new ListItem("Seleccione un item","0"));
            ddlExtracto.SelectedIndex = 0;
            ddlExtracto.DataBind();
        }
    }


    Boolean validarDatos()
    {
        if (ddlCuentaBanc.SelectedIndex == 0)
        {
            VerError("Seleccione la Cuenta Bancaria");
            return false;
        }
        if (txtFechaIni.Text == "")
        {
            VerError("Ingrese la fecha Inicial");
            return false;
        }
        if (txtFechaFin.Text == "")
        {
            VerError("Ingrese la fecha Final");
            return false;
        }
        if (Convert.ToDateTime(txtFechaIni.Text) > Convert.ToDateTime(txtFechaFin.Text))
        {
            VerError("No Puede Ingresar una fecha inicial mayor a la fecha final");
            return false;
        }
        if (ddlExtracto.SelectedIndex == 0)
        {
            VerError("Seleccione el Extracto");
            return false;
        }
        if (ddlExtracto.SelectedValue == "")
        {
            VerError("Seleccione el Extracto");
            return false;
        }

        return true;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();

        if (Page.IsValid)
        {
            if (validarDatos())
            {
                ConciliacionBancaria vConci = new ConciliacionBancaria();
                vConci.idctabancaria = Convert.ToInt32(ddlCuentaBanc.SelectedValue);
                vConci.cod_banco = Convert.ToInt32(lblCodEntidad.Text);
                vConci.cod_cuenta = txtCodCuenta.Text;
                vConci.idextracto = Convert.ToInt32(ddlExtracto.SelectedValue);
                vConci.fecha_inicial = Convert.ToDateTime(txtFechaIni.Text);
                vConci.fecha_final = Convert.ToDateTime(txtFechaFin.Text);

                ConciliacionServ.GenerarConciliacion(vConci, (Usuario)Session["usuario"]);
                Actualizar();
                if (gvLista.Rows.Count > 0)
                {
                    List<CONCBANCARIA_DETALLE> lstDetalle = new List<CONCBANCARIA_DETALLE>();
                    CONCBANCARIA_DETALLE vdeta = new CONCBANCARIA_DETALLE();
                    int opcion;

                    #region GRILLAS PARTIDAS CONTABILIDAD

                    opcion = 3; // filtro de grilla Cheque Pendientes
                    lstDetalle = ConciliacionServ.ListarTemporalDetalle(vdeta, opcion, (Usuario)Session["usuario"]);
                    if (lstDetalle.Count > 0)
                    {
                        panel1.Visible = true;
                        lblInfoCheque.Visible = false;
                        gvChequePend1.DataSource = lstDetalle;
                        gvChequePend1.DataBind();
                    }
                    else
                    {
                        panel1.Visible = false;
                        lblInfoCheque.Visible = true;
                    }

                    opcion = 4; // filtro de grilla Consignaciones Pendientes
                    lstDetalle = ConciliacionServ.ListarTemporalDetalle(vdeta, opcion, (Usuario)Session["usuario"]);
                    if (lstDetalle.Count > 0)
                    {
                        panel2.Visible = true;
                        lblInfoConsig.Visible = false;
                        gvConsigPend1.DataSource = lstDetalle;
                        gvConsigPend1.DataBind();
                    }
                    else
                    {
                        panel2.Visible = false;
                        lblInfoConsig.Visible = true;
                    }

                    opcion = 5; // filtro de grilla notas Credito
                    lstDetalle = ConciliacionServ.ListarTemporalDetalle(vdeta, opcion, (Usuario)Session["usuario"]);
                    if (lstDetalle.Count > 0)
                    {
                        panel3.Visible = true;
                        lblInfoCredito.Visible = false;
                        gvNotasCred1.DataSource = lstDetalle;
                        gvNotasCred1.DataBind();
                    }
                    else
                    {
                        panel3.Visible = false;
                        lblInfoCredito.Visible = true;
                    }

                    opcion = 6; // filtro de grilla notas Debito
                    lstDetalle = ConciliacionServ.ListarTemporalDetalle(vdeta, opcion, (Usuario)Session["usuario"]);
                    if (lstDetalle.Count > 0)
                    {
                        panel4.Visible = true;
                        lblInfoDebito.Visible = false;
                        gvNotasDeb1.DataSource = lstDetalle;
                        gvNotasDeb1.DataBind();
                    }
                    else
                    {
                        panel4.Visible = false;
                        lblInfoDebito.Visible = true;
                    }


                    #endregion


                    #region GRILLAS PARTIDAS EXTRACTO

                    opcion = 8; // filtro de grilla Cheque Pendientes
                    lstDetalle = ConciliacionServ.ListarTemporalDetalle(vdeta, opcion, (Usuario)Session["usuario"]);
                    if (lstDetalle.Count > 0)
                    {
                        panel5.Visible = true;
                        lblInfoCheque2.Visible = false;
                        gvChequePend2.DataSource = lstDetalle;
                        gvChequePend2.DataBind();
                    }
                    else
                    {
                        panel5.Visible = false;
                        lblInfoCheque2.Visible = true;
                    }

                    opcion = 9; // filtro de grilla Consignaciones Pendientes
                    lstDetalle = ConciliacionServ.ListarTemporalDetalle(vdeta, opcion, (Usuario)Session["usuario"]);
                    if (lstDetalle.Count > 0)
                    {
                        panel6.Visible = true;
                        lblInfoConsig2.Visible = false;
                        gvConsigPend2.DataSource = lstDetalle;
                        gvConsigPend2.DataBind();
                    }
                    else
                    {
                        panel6.Visible = false;
                        lblInfoConsig2.Visible = true;
                    }

                    opcion = 10; // filtro de grilla notas Credito
                    lstDetalle = ConciliacionServ.ListarTemporalDetalle(vdeta, opcion, (Usuario)Session["usuario"]);
                    if (lstDetalle.Count > 0)
                    {
                        panel7.Visible = true;
                        lblInfoCredito2.Visible = false;
                        gvNotasCred2.DataSource = lstDetalle;
                        gvNotasCred2.DataBind();
                    }
                    else
                    {
                        panel7.Visible = false;
                        lblInfoCredito2.Visible = true;
                    }

                    opcion = 11; // filtro de grilla notas Debito
                    lstDetalle = ConciliacionServ.ListarTemporalDetalle(vdeta, opcion, (Usuario)Session["usuario"]);
                    if (lstDetalle.Count > 0)
                    {
                        panel8.Visible = true;
                        lblInfoDebito2.Visible = false;
                        gvNotasDeb2.DataSource = lstDetalle;
                        gvNotasDeb2.DataBind();
                    }
                    else
                    {
                        panel8.Visible = false;
                        lblInfoDebito2.Visible = true;
                    }


                    #endregion

                }
            }
        }
    }


    


    private void Actualizar()
    {
        try
        {
            List<CONCBANCARIA_RESUMEN> lstConsulta = new List<CONCBANCARIA_RESUMEN>();
            CONCBANCARIA_RESUMEN vConci = new CONCBANCARIA_RESUMEN();

            lstConsulta = ConciliacionServ.ListarTemporalResumen(vConci, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(true);
                toolBar.MostrarImprimir(true);
                Session["DTCONCILIACION"] = lstConsulta;
                mvACH.Visible = true;
                mvACH.ActiveViewIndex = 0;                
            }
            else
            {
                mvACH.Visible = false;                
            }
            Session.Add(ConciliacionServ.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConciliacionServ.CodigoPrograma, "Actualizar", ex);
        }
    }



    protected void ddlCuentaBanc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCuentaBanc.SelectedIndex != 0)
        {
            ConciliacionBancaria vDatos = new ConciliacionBancaria();
            vDatos.idctabancaria = Convert.ToInt32(ddlCuentaBanc.SelectedValue);
            vDatos = ConciliacionServ.ConsultarCuentasBancarias(vDatos, (Usuario)Session["usuario"]);

            if (vDatos.cod_banco != 0)
                lblCodEntidad.Text = vDatos.cod_banco.ToString();
            if (vDatos.nombrebanco != "")
                txtEntidad.Text = vDatos.nombrebanco;
            if (vDatos.cod_cuenta != "")
                txtCodCuenta.Text = vDatos.cod_cuenta;
            if (vDatos.nombre != "")
                txtNombreCta.Text = vDatos.nombre;
        }
        else
        {
            lblCodEntidad.Text = ""; txtEntidad.Text = ""; txtCodCuenta.Text = ""; txtNombreCta.Text = "";
        }
    }


    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        mvACH.ActiveViewIndex = 1;
    }

    protected void btnVerResumen_Click(object sender, EventArgs e)
    {
        mvACH.ActiveViewIndex = 0;
    }



    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (validarDatos())
                ctlMensaje.MostrarMensaje("Desea realizar la Grabación?");
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConciliacionServ.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }



    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {

            ConciliacionBancaria vConci = new ConciliacionBancaria();
            if (idObjeto != "")
                vConci.idconciliacion = Convert.ToInt32(txtCodigo.Text);
            else
                vConci.idconciliacion = 0;
            vConci.idctabancaria = Convert.ToInt32(ddlCuentaBanc.SelectedValue);
            vConci.cod_banco = Convert.ToInt32(lblCodEntidad.Text);
            vConci.fecha_inicial = Convert.ToDateTime(txtFechaIni.Text);
            vConci.fecha_final = Convert.ToDateTime(txtFechaFin.Text);
            vConci.saldo_contable = 0;
            vConci.saldo_extracto = 0;
            //vConci.codusuario_aprueba = 0; NULO
            vConci.num_extracto = Convert.ToInt32(ddlExtracto.SelectedValue);
            vConci.estado = 1 ;

            vConci.lstResumen = new List<CONCBANCARIA_RESUMEN>();
            vConci.lstResumen = ObtenerListaResumen();

            vConci.lstDetalle = new List<CONCBANCARIA_DETALLE>();
            vConci.lstDetalle = ObtenerListaDetalle();

            if (idObjeto != "")
                ConciliacionServ.ModificarConciliacion(vConci, (Usuario)Session["usuario"]);
            else
                ConciliacionServ.CrearConciliacion(vConci, (Usuario)Session["usuario"]);

            Navegar(Pagina.Lista);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConciliacionServ.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }


    protected List<CONCBANCARIA_RESUMEN> ObtenerListaResumen()
    {
        List<CONCBANCARIA_RESUMEN> lstResumen = new List<CONCBANCARIA_RESUMEN>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            CONCBANCARIA_RESUMEN eResu = new CONCBANCARIA_RESUMEN();

            Label lblCodigo = (Label)rfila.FindControl("lblCodigo");
            if (lblCodigo.Text != "")
                eResu.idresumen = Convert.ToInt32(lblCodigo.Text);
            else
                eResu.idresumen = 0;
            
            Label lblItem = (Label)rfila.FindControl("lblItem");
            if (lblItem.Text != "" && lblItem.Text != null)
                eResu.descripcion = lblItem.Text;
            else
                eResu.descripcion = null;

            Label lblItemValor = (Label)rfila.FindControl("lblItemValor");
            if (lblItemValor.Text != "" && lblItemValor.Text != null)
                eResu.valor = Convert.ToDecimal(lblItemValor.Text);
            else
                eResu.valor = 0;

            if (eResu.idresumen != 0 && eResu.idresumen > 0)
                lstResumen.Add(eResu);

        }

        return lstResumen;
    }



    protected List<CONCBANCARIA_DETALLE> RecuperaDetalle(GridView nombre)
    {
        List<CONCBANCARIA_DETALLE> lstResumen = new List<CONCBANCARIA_DETALLE>();

        foreach (GridViewRow rfila in nombre.Rows)
        {
            CONCBANCARIA_DETALLE eDeta = new CONCBANCARIA_DETALLE();

            Label lblidDeta = (Label)rfila.FindControl("lblCodigo");
            if (idObjeto != "")//codigo
                if(lblidDeta.Text != "")
                    eDeta.iddetalle = Convert.ToInt32(lblidDeta.Text);
            else
                eDeta.iddetalle = 0;

            if (rfila.Cells[1].Text != "" && rfila.Cells[1].Text != null)//Fecha
                eDeta.fecha = Convert.ToDateTime(rfila.Cells[1].Text);
            else
                eDeta.fecha = DateTime.MinValue;

            if (rfila.Cells[2].Text != "" && rfila.Cells[2].Text != null)//Referencia
                eDeta.referencia = rfila.Cells[2].Text;
            else
                eDeta.referencia = null;

            if (rfila.Cells[3].Text != "" && rfila.Cells[3].Text != null)//Beneficiario
                eDeta.beneficiario = rfila.Cells[3].Text;
            else
                eDeta.beneficiario = null;

            if (rfila.Cells[4].Text != "" && rfila.Cells[4].Text != null)//Valor
                eDeta.valor = Convert.ToDecimal(rfila.Cells[4].Text);
            else
                eDeta.valor = 0;

            if (rfila.Cells[5].Text != "" && rfila.Cells[5].Text != null)//Dias
                eDeta.dias = Convert.ToInt32(rfila.Cells[5].Text);
            else
                eDeta.dias = 0;
            
            if (rfila.Cells[6].Text != "" && rfila.Cells[6].Text != null)//Num. Comp
                eDeta.num_comp = Convert.ToInt32(rfila.Cells[6].Text);
            else
                eDeta.num_comp = 0;

            if (rfila.Cells[7].Text != "" && rfila.Cells[7].Text != null)//Tipo Comp
                eDeta.tipo_comp = Convert.ToInt32(rfila.Cells[7].Text);
            else
                eDeta.tipo_comp = 0;

            TextBox txtObservacion = (TextBox)rfila.FindControl("txtObservacion");
            if (txtObservacion.Text != "")
                eDeta.observacion = txtObservacion.Text;
            else
                eDeta.observacion = null;

            if (nombre.Rows.Count != 0)
                lstResumen.Add(eDeta);
        }

        return lstResumen;
    }


    protected List<CONCBANCARIA_DETALLE> ObtenerListaDetalle()
    {
        List<CONCBANCARIA_DETALLE> lstResumen = new List<CONCBANCARIA_DETALLE>();
        List<CONCBANCARIA_DETALLE> lstResumenGeneral = new List<CONCBANCARIA_DETALLE>();
         
        //CONTABILIDAD
        lstResumen = RecuperaDetalle(gvChequePend1);
        foreach (CONCBANCARIA_DETALLE rfila in lstResumen)
        {
            rfila.tipo = 3;
            lstResumenGeneral.Add(rfila);          
        }
        lstResumen = RecuperaDetalle(gvConsigPend1);
        foreach (CONCBANCARIA_DETALLE rfila in lstResumen)
        {
            rfila.tipo = 4;
            lstResumenGeneral.Add(rfila);
        }
        lstResumen = RecuperaDetalle(gvNotasCred1);
        foreach (CONCBANCARIA_DETALLE rfila in lstResumen)
        {
            rfila.tipo = 5;
            lstResumenGeneral.Add(rfila);
        }
        lstResumen = RecuperaDetalle(gvNotasDeb1);
        foreach (CONCBANCARIA_DETALLE rfila in lstResumen)
        {
            rfila.tipo = 6;
            lstResumenGeneral.Add(rfila);
        }

        //EXTRACTO
        lstResumen = RecuperaDetalle(gvChequePend2);
        foreach (CONCBANCARIA_DETALLE rfila in lstResumen)
        {
            rfila.tipo = 8;
            lstResumenGeneral.Add(rfila);
        }
        lstResumen = RecuperaDetalle(gvConsigPend2);
        foreach (CONCBANCARIA_DETALLE rfila in lstResumen)
        {
            rfila.tipo = 9;
            lstResumenGeneral.Add(rfila);
        }
        lstResumen = RecuperaDetalle(gvNotasCred2);
        foreach (CONCBANCARIA_DETALLE rfila in lstResumen)
        {
            rfila.tipo = 10;
            lstResumenGeneral.Add(rfila);
        }
        lstResumen = RecuperaDetalle(gvNotasDeb2);
        foreach (CONCBANCARIA_DETALLE rfila in lstResumen)
        {
            rfila.tipo = 11;
            lstResumenGeneral.Add(rfila);
        }

        return lstResumenGeneral;
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            ConciliacionBancaria vConci = new ConciliacionBancaria();
            vConci.idconciliacion = Convert.ToInt32(pIdObjeto);
            vConci = ConciliacionServ.ConsultarConciliacion(vConci, (Usuario)Session["usuario"]);

            if (vConci.idconciliacion != 0)
                txtCodigo.Text = vConci.idconciliacion.ToString();
            if (vConci.idctabancaria != 0)
                ddlCuentaBanc.SelectedValue = vConci.idctabancaria.ToString();
            ddlCuentaBanc_SelectedIndexChanged(ddlCuentaBanc, null);

            if (vConci.fecha_inicial != DateTime.MinValue)
                txtFechaIni.Text = vConci.fecha_inicial.ToShortDateString();

            if (vConci.fecha_final != DateTime.MinValue)
                txtFechaFin.Text = vConci.fecha_final.ToShortDateString();

            if (vConci.num_extracto != 0)
                ddlExtracto.SelectedValue = vConci.num_extracto.ToString();

            ddlExtracto_SelectedIndexChanged(ddlExtracto, null);

            List<CONCBANCARIA_RESUMEN> lstResumen = new List<CONCBANCARIA_RESUMEN>();
            List<CONCBANCARIA_DETALLE> lstDetalle = new List<CONCBANCARIA_DETALLE>();

            lstResumen = ConciliacionServ.ListarResumenConciliacion(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);
            if (lstResumen.Count > 0)
            {
                mvACH.Visible = true;
                mvACH.ActiveViewIndex = 0;
                gvLista.DataSource = lstResumen;
                gvLista.DataBind();
                Site toolBar = (Site)this.Master;
                toolBar.MostrarImprimir(true);
                toolBar.MostrarConsultar(false);
                toolBar.MostrarGuardar(true);
                Session["DTCONCILIACION"] = lstResumen;
            }

            lstDetalle = ConciliacionServ.ListarDetalleConciliacion(Convert.ToInt32(pIdObjeto), (Usuario)Session["usuario"]);

            if (lstDetalle.Count > 0)
            {
                List<CONCBANCARIA_DETALLE> lstData3 = new List<CONCBANCARIA_DETALLE>();
                List<CONCBANCARIA_DETALLE> lstData4 = new List<CONCBANCARIA_DETALLE>();
                List<CONCBANCARIA_DETALLE> lstData5 = new List<CONCBANCARIA_DETALLE>();
                List<CONCBANCARIA_DETALLE> lstData6 = new List<CONCBANCARIA_DETALLE>();
                List<CONCBANCARIA_DETALLE> lstData8 = new List<CONCBANCARIA_DETALLE>();
                List<CONCBANCARIA_DETALLE> lstData9 = new List<CONCBANCARIA_DETALLE>();
                List<CONCBANCARIA_DETALLE> lstData10 = new List<CONCBANCARIA_DETALLE>();
                List<CONCBANCARIA_DETALLE> lstData11 = new List<CONCBANCARIA_DETALLE>();
                foreach (CONCBANCARIA_DETALLE rFila in lstDetalle)
                {                    
                    if (rFila.tipo == 3)
                    {
                        lstData3.Add(rFila);
                        gvChequePend1.DataSource = lstData3;
                        gvChequePend1.DataBind();                        
                    }
                    if (rFila.tipo == 4)
                    {
                        lstData4.Add(rFila);
                        gvConsigPend1.DataSource = lstData4;
                        gvConsigPend1.DataBind();                        
                    }
                    if (rFila.tipo == 5)
                    {
                        lstData5.Add(rFila);
                        gvNotasCred1.DataSource = lstData5;
                        gvNotasCred1.DataBind();                        
                    }
                    if (rFila.tipo == 6)
                    {
                        lstData6.Add(rFila);
                        gvNotasDeb1.DataSource = lstData6;
                        gvNotasDeb1.DataBind();                       
                    }

                    if (rFila.tipo == 8)
                    {
                        lstData8.Add(rFila);
                        gvChequePend2.DataSource = lstData8;
                        gvChequePend2.DataBind();                        
                    }
                    if (rFila.tipo == 9)
                    {
                        lstData9.Add(rFila);
                        gvConsigPend2.DataSource = lstData9;
                        gvConsigPend2.DataBind();                        
                    }
                    if (rFila.tipo == 10)
                    {
                        lstData10.Add(rFila);
                        gvNotasCred2.DataSource = lstData10;
                        gvNotasCred2.DataBind();                        
                    }
                    if (rFila.tipo == 11)
                    {
                        lstData11.Add(rFila);
                        gvNotasDeb2.DataSource = lstData11;
                        gvNotasDeb2.DataBind();                        
                    }
                }
                if (gvChequePend1.Rows.Count == 0) {lblInfoCheque.Visible = true; panel1.Visible = false; }else panel1.Visible=true;
                if (gvConsigPend1.Rows.Count == 0) { lblInfoConsig.Visible = true; panel2.Visible = false; } else panel2.Visible = true;
                if (gvNotasCred1.Rows.Count == 0) {lblInfoCredito.Visible = true; panel3.Visible = false;}else panel3.Visible = true;
                if (gvNotasDeb1.Rows.Count == 0) {lblInfoDebito.Visible = true;  panel4.Visible = false; } else panel4.Visible = true;

                if (gvChequePend2.Rows.Count == 0) {lblInfoCheque2.Visible = true; panel5.Visible =false;} else panel5.Visible = true;
                if (gvConsigPend2.Rows.Count == 0) {lblInfoConsig2.Visible = true;panel6.Visible = false; } else panel6.Visible = true;
                if (gvNotasCred2.Rows.Count == 0) {lblInfoCredito2.Visible = true;panel7.Visible = false; } else panel7.Visible = true;
                if (gvNotasDeb2.Rows.Count == 0) { lblInfoDebito2.Visible = true; panel8.Visible = false; } else panel8.Visible = true;
            }


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConciliacionServ.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void ddlExtracto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExtracto.SelectedIndex != 0)
        {
            ConciliacionBancaria vConc = new ConciliacionBancaria();
            vConc = ConciliacionServ.ConsultarExtracto(vConc, Convert.ToInt32(ddlExtracto.SelectedValue), (Usuario)Session["usuario"]);
            if (vConc.fecha != DateTime.MinValue)
                txtFechaExtrac.Text = vConc.fecha.ToShortDateString(); 
        }
        else
        {
            txtFechaExtrac.Text = "";
        }
    }



    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTCONCILIACION"] == null)
        {
            VerError("No ha generado el Reporte para poder imprimir informacion");
        }
        else
        {
            mvPrincipal.Visible = false;
            mvReporte.Visible = true;
            mvReporte.ActiveViewIndex = 0;

            List<CONCBANCARIA_RESUMEN> lstConsulta = new List<CONCBANCARIA_RESUMEN>();
            lstConsulta = (List<CONCBANCARIA_RESUMEN>)Session["DTCONCILIACION"];

            // LLenar data table con los datos a recoger
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("Item");
            table.Columns.Add("Valor");

            foreach (CONCBANCARIA_RESUMEN item in lstConsulta)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = item.descripcion;
                datarw[1] = item.valor.ToString("n");                
                table.Rows.Add(datarw);
            }
            // ---------------------------------------------------------------------------------------------------------
            // Pasar datos al reporte
            // ---------------------------------------------------------------------------------------------------------

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[11];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);            
            param[2] = new ReportParameter("Usuario", pUsuario.nombre);
            param[3] = new ReportParameter("Oficina", pUsuario.nombre_oficina);
            param[4] = new ReportParameter("fecha_ini", txtFechaIni.Text);
            param[5] = new ReportParameter("fecha_fin", txtFechaFin.Text);
            param[6] = new ReportParameter("ctabancaria", ddlCuentaBanc.SelectedItem.Text);
            param[7] = new ReportParameter("banco", txtEntidad.Text);
            param[8] = new ReportParameter("nomcuenta", txtNombreCta.Text);
            param[9] = new ReportParameter("cuenta", txtCodCuenta.Text);
            param[10] = new ReportParameter("ImagenReport", ImagenReporte());


            rvReporteMensajeria.LocalReport.EnableExternalImages = true;
            rvReporteMensajeria.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvReporteMensajeria.LocalReport.DataSources.Clear();
            rvReporteMensajeria.LocalReport.DataSources.Add(rds);
            rvReporteMensajeria.LocalReport.Refresh();
                        
        }
    }


    protected void btnDatos_Click(object sender, EventArgs e)
    {
        mvReporte.Visible = false;
        mvPrincipal.Visible = true;
        mvPrincipal.ActiveViewIndex = 0;
        mvACH.ActiveViewIndex = 0;
    }

}