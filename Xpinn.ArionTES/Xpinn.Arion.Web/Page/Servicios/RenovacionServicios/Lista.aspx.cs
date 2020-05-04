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
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Services;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;



partial class Lista : GlobalWeb
{
    CausacionServiciosServices RenovaServicios = new CausacionServiciosServices();
    PoblarListas Poblar = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(RenovaServicios.CodigoProgramaRenova, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGrabar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RenovaServicios.CodigoProgramaRenova, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["DTRENOVACION"] = null;
                Session["NORENOVACION"] = null;
                
                cargarDropdown();
                Limpiar();                
                cpeDemo.CollapsedText = "(Listado de Servicios a no Renovar...)";
                cpeDemo.ExpandedText = "(Listado de Servicios a no Renovar...)";
                ddlTipoIncremento_SelectedIndexChanged(ddlTipoIncremento, null);
            }
            else
                CalcularTotal();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RenovaServicios.CodigoProgramaRenova, "Page_Load", ex);
        }
    }


    void Limpiar()
    {
        VerError("");
        gvLista.DataSource = null;
        lblTexto.Visible = false;
        txtVrTotal.Visible = false;
        lblInfo.Visible = false;
        lblTotalRegs.Visible = false;

        string fechaIni = "01/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
        txtFechaInicial.Text = fechaIni;
        txtFechaFinal.Text = DateTime.Now.ToShortDateString();

        ddlLinea.SelectedIndex = 0;
        panelGrilla.Visible = false;
        pNoRenovar.Visible = false;
        gvNoRenovacion.DataSource = null;


        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarExportar(false);
        toolBar.MostrarConsultar(true);

        //
        txtFIniVigencia.Text = "";
        txtFFinVigencia.Text = "";
        ddlTipoIncremento.SelectedIndex = 0;
        ddlTipoIncremento_SelectedIndexChanged(ddlTipoIncremento, null);
        txtPlazo.Text = "";
        txtVrCta.Text = "";
        txtVrCuota.Text = "0";
    }


    protected void InicializargvNoRenovar()
    {
        List<Servicio> lstDeta = new List<Servicio>();
        for (int i = 0; i < 1; i++)
        {
            Servicio eServi = new Servicio();
            eServi.numero_servicio = 0;
            eServi.cod_persona = -1;
            eServi.identificacion = "";
            eServi.nombre = "";
            eServi.nom_linea = "";
            eServi.nom_plan = "";
            eServi.num_poliza = "";
            eServi.fecha_aprobacion = null;
            eServi.fecha_proximo_pago = null;
            eServi.fecha_inicio_vigencia = null;
            eServi.fecha_final_vigencia = null;
            eServi.valor_total = null;
            eServi.numero_cuotas = null;
            eServi.valor_cuota = null;
            eServi.saldo = null;
            eServi.nom_periodicidad = "";
            eServi.forma_pago = "";
            eServi.nombre_proveedor = "";
            lstDeta.Add(eServi);
        }
        gvNoRenovacion.DataSource = lstDeta;
        gvNoRenovacion.DataBind();

        Session["NORENOVACION"] = lstDeta;
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        Limpiar();
    }

    Boolean ValidarSeleccionados()
    {
        if (txtFIniVigencia.Text == "")
        {
            VerError("Ingrese la fecha Inicial de vigencia");
            txtFIniVigencia.Focus();
            return false;
        }
        if (txtFFinVigencia.Text == "")
        {
            VerError("Ingrese la fecha Final de vigencia");
            txtFFinVigencia.Focus();
            return false;
        }
        if (Convert.ToDateTime(txtFIniVigencia.Text) > Convert.ToDateTime(txtFFinVigencia.Text))
        {
            VerError("La fecha inicial de vigencia no puede ser superior a la fecha Final de vigencia, verifique por favor");
            txtFIniVigencia.Focus();
            return false;
        }
        if (txtPlazo.Text == "")
        {
            VerError("Ingrese el plazo correspondiente, verifique por favor");
            txtPlazo.Focus();
            return false;
        }
        int cont = 0;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            cont++;
        }
        if (cont == 0)
        {
            VerError("No existen ningún registro para realizar la renovación");
            return false;
        }
        if (ddlTipoIncremento.SelectedIndex == 0)
        {
            if (txtVrCta.Text == "0" || txtVrCta.Text == "")
            {
                VerError("Ingrese el valor de la cuota");
                txtVrCta.Focus();
                return false;
            }
            if (Convert.ToDecimal(txtVrCta.Text) >= 100)
            {
                VerError("Ingrese un valor de porcentaje menor al 100%");
                txtVrCta.Focus();
                return false;
            }
        }
        else
        {
            if (txtVrCuota.Text == "0")
            {
                VerError("Ingrese el valor de la cuota");
                txtVrCuota.Focus();
                return false;
            }
        }

        return true;
    }

    Boolean ValidarDatos()
    {
        if (txtFechaInicial.Text == "")
        {
            VerError("Ingrese la fecha Inicial para generar el filtro");
            txtFechaInicial.Focus();
            return false;
        }
        if (txtFechaFinal.Text == "")
        {
            VerError("Ingrese la fecha Final para generar el filtro");
            txtFechaFinal.Focus();
            return false;
        }
        if (Convert.ToDateTime(txtFechaInicial.Text) > Convert.ToDateTime(txtFechaFinal.Text))
        {
            VerError("La fecha inicial no puede ser superior a la fecha Final para realizar el filtro, verifique por favor");
            txtFechaInicial.Focus();
            return false;
        }
        if (ddlLinea.SelectedIndex == 0)
        {
            VerError("Seleccione la Linea de servicio");
            ddlLinea.Focus();
            return false;
        }
        return true;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            if (ValidarDatos())
            { 
                Actualizar();
                CalcularTotal();
            }
        }
    }


    void cargarDropdown()
    {
        Poblar.PoblarListaDesplegable("lineasservicios", ddlLinea,(Usuario)Session["usuario"]);

        ddlTipoIncremento.Items.Insert(0,new ListItem("Porcentaje de la cuota", "1"));
        ddlTipoIncremento.Items.Insert(1,new ListItem("Valor de la cuota", "2"));
        ddlTipoIncremento.Items.Insert(2, new ListItem("Cuota General", "3"));
        ddlTipoIncremento.SelectedIndex = 0;
        ddlTipoIncremento.DataBind();
    }


    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        if (ValidarSeleccionados())
            ctlMensaje.MostrarMensaje("Desea registrar las causaciones de los servicios seleccionados?");
    }

    void CalcularTotal()
    {
        decimal valor = 0, vrTotal = 0;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            valor = rFila.Cells[12].Text != "&nbsp;" ? Convert.ToDecimal(rFila.Cells[12].Text.Replace(gSeparadorMiles, "")) : 0;
            vrTotal += valor;
        }

        txtVrTotal.Text = vrTotal.ToString("n0");
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

            Servicio Servi = new Servicio();
            Servi.fecha_inicio_vigencia = Convert.ToDateTime(txtFIniVigencia.Text);
            Servi.fecha_final_vigencia = Convert.ToDateTime(txtFFinVigencia.Text);
            Servi.numero_cuotas = Convert.ToInt32(txtPlazo.Text);

            RenovacionServicios pRenovacion = new RenovacionServicios();
            pRenovacion.tipo = Convert.ToInt32(ddlTipoIncremento.SelectedValue);
            if (ddlTipoIncremento.SelectedIndex == 0)
                pRenovacion.valor_cuota = Convert.ToDecimal(txtVrCta.Text);
            else
                pRenovacion.valor_cuota = Convert.ToDecimal(txtVrCuota.Text);

            List<Servicio> lstServicios = new List<Servicio>();
            lstServicios = ObtenerListaRenovacion();
            
            //DATOS DE LA OPERACION
            Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
            vOpe.cod_ope = 0;
            vOpe.tipo_ope = 28;
            vOpe.cod_caja = 0;
            vOpe.cod_cajero = 0;
            vOpe.observacion = "Operacion-Renovacion ";
            vOpe.cod_proceso = null;
            vOpe.fecha_oper = DateTime.Now;
            vOpe.fecha_calc = DateTime.Now;
            vOpe.cod_ofi = pUsuario.cod_oficina;

            Int64 COD_OPE = 0;
            RenovaServicios.CrearRenovacionServicios(ref COD_OPE, vOpe, lstServicios, Servi,pRenovacion, (Usuario)Session["usuario"]);

            if (COD_OPE != 0)
            {
                Actualizar();

                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = COD_OPE;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 28;
                Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.ParseExact(DateTime.Now.ToShortDateString(), gFormatoFecha, null);
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = pUsuario.codusuario;
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RenovaServicios.CodigoProgramaRenova, "btnContinuarMen_Click", ex);
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
            BOexcepcion.Throw(RenovaServicios.CodigoProgramaRenova, "gvLista_PageIndexChanging", ex);
        }
    }
    
    private void Actualizar()
    {
        try
        {
            List<Servicio> lstConsulta = new List<Servicio>();
            String filtro = obtFiltro();
            DateTime pFechaIni,pFechaFin;
            pFechaIni = txtFechaInicial.ToDateTime == null ? DateTime.MinValue : txtFechaInicial.ToDateTime;
            pFechaFin = txtFechaFinal.ToDateTime == null ? DateTime.MinValue : txtFechaFinal.ToDateTime;
            lstConsulta = RenovaServicios.ListarServiciosRenovacion(filtro, pFechaIni,pFechaFin, (Usuario)Session["usuario"]);

            gvLista.PageSize = 20;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataBind();
                panelGrilla.Visible = true;
                pNoRenovar.Visible = true;
                gvNoRenovacion.DataSource = null;
                InicializargvNoRenovar();

                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTRENOVACION"] = lstConsulta;
                toolBar.MostrarGuardar(true);
                toolBar.MostrarExportar(true);
                toolBar.MostrarConsultar(false);
                lblTexto.Visible = true;
                txtVrTotal.Visible = true;
            }
            else
            {
                gvNoRenovacion.DataSource = null;
                panelGrilla.Visible = false;
                pNoRenovar.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
                
                Session["DTRENOVACION"] = null;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarExportar(false);
                toolBar.MostrarConsultar(true);
                lblTexto.Visible = false;
                txtVrTotal.Visible = false;
            }

            Session.Add(RenovaServicios.CodigoProgramaRenova + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RenovaServicios.CodigoProgramaRenova, "Actualizar", ex);
        }
    }

    
    private string obtFiltro()
    {
        String filtro = String.Empty;

        if (ddlLinea.SelectedIndex != 0)
            filtro += " AND S.COD_LINEA_SERVICIO = '" + ddlLinea.SelectedValue + "'";
        filtro += " AND S.ESTADO = 'C'";
        return filtro;
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["DTRENOVACION"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.Columns[0].Visible = false;
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTRENOVACION"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=RenovacionServicios.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }

    protected void ddlTipoIncremento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoIncremento.SelectedIndex == 0)
        {
            txtVrCta.Visible = true;
            txtVrCuota.Visible = false;
        }
        else
        {
            txtVrCta.Visible = false;
            txtVrCuota.Visible = true;
        }
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());

        //LISTA GRILLA RENOVACION
        
        List<Servicio> LstDeta;
        LstDeta = ObtenerListaRenovacion();
        int Posicion = (gvLista.PageIndex * gvLista.PageSize) + e.RowIndex;

        //LISTA GRILLA NO RENOVACION
        List<Servicio> LstNoReno;
        LstNoReno = ObtenerListaNoRenovacion();

        Servicio Agregar = new Servicio();
        Agregar = ObtenerRenovacionPosicion(Posicion, LstDeta);
        LstNoReno.Add(Agregar);

        LstDeta.RemoveAt((gvLista.PageIndex * gvLista.PageSize) + e.RowIndex);
        Session["DTRENOVACION"] = LstDeta;
        gvLista.DataSourceID = null;
        gvLista.DataBind();
        gvLista.DataSource = LstDeta;
        gvLista.DataBind();

        //CARGANDO NO RENOVACION
        Session["NORENOVACION"] = LstNoReno;
        gvNoRenovacion.DataSourceID = null;
        gvNoRenovacion.DataBind();
        gvNoRenovacion.DataSource = LstNoReno;
        gvNoRenovacion.DataBind();

        CalcularTotal();

        if (gvLista.Rows.Count == 0)
        {
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            toolBar.MostrarConsultar(true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        }
    }

    protected void gvNoRenovacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvNoRenovacion.DataKeys[e.RowIndex].Values[0].ToString());
        
        List<Servicio> LstNoReno;
        LstNoReno = ObtenerListaNoRenovacion();
        int Posicion = (gvNoRenovacion.PageIndex * gvNoRenovacion.PageSize) + e.RowIndex;

        List<Servicio> LstDeta;
        LstDeta = ObtenerListaRenovacion();


        Servicio Agregar = new Servicio();
        Agregar = ObtenerRenovacionPosicion(Posicion, LstNoReno);
        LstDeta.Add(Agregar);

        LstNoReno.RemoveAt((gvNoRenovacion.PageIndex * gvNoRenovacion.PageSize) + e.RowIndex);

        Session["DTRENOVACION"] = LstDeta;
        gvLista.DataSourceID = null;
        gvLista.DataBind();
        gvLista.DataSource = LstDeta;
        gvLista.DataBind();

        //CARGANDO NO RENOVACION
        Session["NORENOVACION"] = LstNoReno;
        gvNoRenovacion.DataSourceID = null;
        gvNoRenovacion.DataBind();
        gvNoRenovacion.DataSource = LstNoReno;
        gvNoRenovacion.DataBind();

        CalcularTotal();
        if (gvNoRenovacion.Rows.Count == 0)
        {
            InicializargvNoRenovar();
        }

        if (gvLista.Rows.Count != 0)
        {
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(true);
            toolBar.MostrarExportar(true);
            toolBar.MostrarConsultar(false);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        }
    }


    protected List<Servicio> ObtenerListaRenovacion()
    {
        List<Servicio> lstServicios = new List<Servicio>();
        List<Servicio> lista = new List<Servicio>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            Servicio eServi = new Servicio();

            eServi.numero_servicio = Convert.ToInt32(rfila.Cells[1].Text);

            String cod_persona = gvLista.DataKeys[rfila.RowIndex].Values[1].ToString();
            if (cod_persona != "&nbsp;" && cod_persona != "")
                eServi.cod_persona = Convert.ToInt64(cod_persona);

            if (rfila.Cells[3].Text != "&nbsp;") //IDENTIFICACION
                eServi.identificacion = rfila.Cells[3].Text.Trim();

            if (rfila.Cells[4].Text != "&nbsp;") //NOMBRE
                eServi.nombre = rfila.Cells[4].Text.Trim();

            if (rfila.Cells[5].Text != "&nbsp;") //NOMLINEA
                eServi.nom_linea = rfila.Cells[5].Text.Trim();

            if (rfila.Cells[6].Text != "&nbsp;") //PLAN SERVICIO
                eServi.nom_plan = rfila.Cells[6].Text.Trim();

            if (rfila.Cells[7].Text != "&nbsp;") //NUM POLIZA
                eServi.num_poliza = rfila.Cells[7].Text.Trim();

            if (rfila.Cells[8].Text != "&nbsp;") //FECHA APROBACION
                eServi.fecha_aprobacion = Convert.ToDateTime(rfila.Cells[8].Text.Trim());

            if (rfila.Cells[9].Text != "&nbsp;") //FECHA PROX PAGO
                eServi.fecha_proximo_pago = Convert.ToDateTime(rfila.Cells[9].Text.Trim());

            if (rfila.Cells[10].Text != "&nbsp;") //FECHA INICIAL VIGENCIA
                eServi.fecha_inicio_vigencia = Convert.ToDateTime(rfila.Cells[10].Text.Trim());

            if (rfila.Cells[11].Text != "&nbsp;") //FECHA FINAL VIGENCIA
                eServi.fecha_final_vigencia = Convert.ToDateTime(rfila.Cells[11].Text.Trim());

            if (rfila.Cells[12].Text != "&nbsp;") //VALOR TOTAL
                eServi.valor_total = Convert.ToDecimal(rfila.Cells[12].Text.Trim().Replace(gSeparadorMiles, ""));
            
            if (rfila.Cells[13].Text != "&nbsp;") //NUMERO CUOTA
                eServi.numero_cuotas = Convert.ToInt32(rfila.Cells[13].Text.Trim());

            if (rfila.Cells[14].Text != "&nbsp;") //VR CUOTA
                eServi.valor_cuota = Convert.ToDecimal(rfila.Cells[14].Text.Replace(gSeparadorMiles,""));

            if (rfila.Cells[15].Text != "&nbsp;") //SALDO
                eServi.saldo = Convert.ToDecimal(rfila.Cells[15].Text.Replace(gSeparadorMiles, ""));

            if (rfila.Cells[16].Text != "&nbsp;") //PERIODICIDAD
                eServi.nom_periodicidad = rfila.Cells[16].Text.Trim();

            if (rfila.Cells[17].Text != "&nbsp;") //FORMA DE PAGO
                eServi.forma_pago = rfila.Cells[17].Text.Trim();

            if (rfila.Cells[18].Text != "&nbsp;") //NOMBRE PROVEEDOR
                eServi.nombre_proveedor = rfila.Cells[18].Text.Trim();

            lista.Add(eServi);
            Session["DTRENOVACION"] = lista;

            if (eServi.numero_servicio != 0)
            {
                lstServicios.Add(eServi);
            }
        }
        return lstServicios;
    }





    protected Servicio ObtenerRenovacionPosicion(int Posicion,List<Servicio> Lista)
    {
        Servicio entidad = new Servicio();
        int cont = 0;
        foreach (Servicio nServ in Lista)
        {
            if (cont == Posicion)
            {
                entidad.numero_servicio = nServ.numero_servicio;
                entidad.cod_persona = nServ.cod_persona;
                entidad.identificacion = nServ.identificacion;
                entidad.nombre = nServ.nombre;
                entidad.nom_linea = nServ.nom_linea;
                entidad.nom_plan = nServ.nom_plan;
                entidad.num_poliza = nServ.num_poliza;
                entidad.fecha_aprobacion = nServ.fecha_aprobacion;
                entidad.fecha_proximo_pago = nServ.fecha_proximo_pago;
                entidad.fecha_inicio_vigencia = nServ.fecha_inicio_vigencia;
                entidad.fecha_final_vigencia = nServ.fecha_final_vigencia;
                entidad.valor_total = nServ.valor_total;
                entidad.numero_cuotas = nServ.numero_cuotas;
                entidad.valor_cuota = nServ.valor_cuota;
                entidad.saldo = nServ.saldo;
                entidad.nom_periodicidad = nServ.nom_periodicidad;
                entidad.forma_pago = nServ.forma_pago;
                entidad.nombre_proveedor = nServ.nombre_proveedor;
                break;
            }
            cont++; 
        }
        return entidad;
    }


    protected List<Servicio> ObtenerListaNoRenovacion()
    {
        List<Servicio> lstServicios = new List<Servicio>();

        foreach (GridViewRow rfila in gvNoRenovacion.Rows)
        {
            Servicio eServi = new Servicio();

            eServi.numero_servicio = Convert.ToInt32(rfila.Cells[1].Text);

            String cod_persona = gvNoRenovacion.DataKeys[rfila.RowIndex].Values[1].ToString();
            if (cod_persona != "&nbsp;" && cod_persona != "")
                eServi.cod_persona = Convert.ToInt64(cod_persona);

            if (rfila.Cells[3].Text != "&nbsp;") //IDENTIFICACION
                eServi.identificacion = rfila.Cells[3].Text.Trim();

            if (rfila.Cells[4].Text != "&nbsp;") //NOMBRE
                eServi.nombre = rfila.Cells[4].Text.Trim();

            if (rfila.Cells[5].Text != "&nbsp;") //NOMLINEA
                eServi.nom_linea = rfila.Cells[5].Text.Trim();

            if (rfila.Cells[6].Text != "&nbsp;") //PLAN SERVICIO
                eServi.nom_plan = rfila.Cells[6].Text.Trim();

            if (rfila.Cells[7].Text != "&nbsp;") //NUM POLIZA
                eServi.num_poliza = rfila.Cells[7].Text.Trim();

            if (rfila.Cells[8].Text != "&nbsp;") //FECHA APROBACION
                eServi.fecha_aprobacion = Convert.ToDateTime(rfila.Cells[8].Text.Trim());

            if (rfila.Cells[9].Text != "&nbsp;") //FECHA PROX PAGO
                eServi.fecha_proximo_pago = Convert.ToDateTime(rfila.Cells[9].Text.Trim());

            if (rfila.Cells[10].Text != "&nbsp;") //FECHA INICIAL VIGENCIA
                eServi.fecha_inicio_vigencia = Convert.ToDateTime(rfila.Cells[10].Text.Trim());

            if (rfila.Cells[11].Text != "&nbsp;") //FECHA FINAL VIGENCIA
                eServi.fecha_final_vigencia = Convert.ToDateTime(rfila.Cells[11].Text.Trim());

            if (rfila.Cells[12].Text != "&nbsp;") //VALOR TOTAL
                eServi.valor_total = Convert.ToDecimal(rfila.Cells[12].Text.Trim().Replace(gSeparadorMiles, ""));

            if (rfila.Cells[13].Text != "&nbsp;") //NUMERO CUOTA
                eServi.numero_cuotas = Convert.ToInt32(rfila.Cells[13].Text.Trim());

            if (rfila.Cells[14].Text != "&nbsp;") //VR CUOTA
                eServi.valor_cuota = Convert.ToDecimal(rfila.Cells[14].Text.Replace(gSeparadorMiles, ""));

            if (rfila.Cells[15].Text != "&nbsp;") //SALDO
                eServi.saldo = Convert.ToDecimal(rfila.Cells[15].Text.Replace(gSeparadorMiles, ""));

            if (rfila.Cells[16].Text != "&nbsp;") //PERIODICIDAD
                eServi.nom_periodicidad = rfila.Cells[16].Text.Trim();

            if (rfila.Cells[17].Text != "&nbsp;") //FORMA DE PAGO
                eServi.forma_pago = rfila.Cells[17].Text.Trim();

            if (rfila.Cells[18].Text != "&nbsp;") //NOMBRE PROVEEDOR
                eServi.nombre_proveedor = rfila.Cells[18].Text.Trim();

            if (eServi.numero_servicio  != 0)
            {
                lstServicios.Add(eServi);
                Session["NORENOVACION"] = lstServicios;
            }
        }
        return lstServicios;
    }

    
}