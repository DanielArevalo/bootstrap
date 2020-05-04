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
    CausacionServiciosServices CausaServicios = new CausacionServiciosServices();
    PoblarListas Poblar = new PoblarListas();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CausaServicios.CodigoProgramaCausa, "L");

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
            BOexcepcion.Throw(CausaServicios.CodigoProgramaCausa, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                Limpiar();
            }
            //else
            //    CalcularTotal();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CausaServicios.CodigoProgramaCausa, "Page_Load", ex);
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
        if (txtFecha.Text != "")
            txtFecha.Text = "";
        ddlLinea.SelectedIndex = 0;
        panelGrilla.Visible = false;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarExportar(false);
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        Limpiar();
    }

    Boolean ValidarSeleccionados()
    {
        int cont = 0;
        cont = gvLista.Rows.OfType<GridViewRow>().Where(x => ((CheckBox)x.FindControl("chkCausacion")).Checked).Count();
        if (cont == 0)
        {
            VerError("No existen ningún registro seleccionado para realizar la causación");
            return false;
        }
        return true;
    }

    Boolean ValidarDatos()
    {
        if (txtFecha.Text == "")
        {
            VerError("Seleccione la fecha de causación");
            return false;
        }
        if (ddlLinea.SelectedIndex == 0)
        {
            VerError("Seleccione la línea de servicio");
            ddlLinea.Focus();
            return false;
        }
        bool result = ValidarProcesoContable(Convert.ToDateTime(txtFecha.Texto), 18);
        if (!result)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 18 = Causación de Servicios");
            return false;
        }

        //Agregado para validar que solo se cause una vez en el mes la línea seleccionada
        string validacion = CausaServicios.ValidarCausacionXFecha(Convert.ToDateTime(txtFecha.Texto), Convert.ToInt64(ddlLinea.SelectedValue), (Usuario)Session["usuario"]);
        if (validacion != "")
        {
            VerError(validacion);
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
            if(ValidarDatos())
                Actualizar();
        }
    }


    void cargarDropdown()
    {
        Poblar.PoblarListaDesplegable("lineasservicios", "COD_LINEA_SERVICIO, NOMBRE", "MANEJA_CAUSACION = 1","1", ddlLinea, Usuario);
        txtFecha.Text = DateTime.Now.ToShortDateString();
        /*List<Xpinn.Comun.Entities.Cierea> lstFechaCierre = new List<Xpinn.Comun.Entities.Cierea>();
        lstFechaCierre = CausaServicios.ListarFechaCierreCausacion(Usuario);
        ddlFechaCorte.DataSource = lstFechaCierre;
        ddlFechaCorte.DataTextFormatString = "{0:" + gFormatoFecha + "}";
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataBind();*/
    }


    protected void btnGrabar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            if(ValidarSeleccionados())
                ctlMensaje.MostrarMensaje("Desea registrar las causaciones de los servicios seleccionados?");
        }
    }

    void CalcularTotal()
    {
        decimal valor = 0, vrTotal = 0;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox chkCausacion = (CheckBox)rFila.FindControl("chkCausacion");
            if (chkCausacion.Checked)
            {
                valor = rFila.Cells[12].Text != "&nbsp;" ? Convert.ToDecimal(rFila.Cells[12].Text.Replace(gSeparadorMiles, "")) : 0;
                vrTotal += valor;
            }
        }
        txtVrTotal.Text = vrTotal.ToString("n0");
    }

    protected List<CausacionServicios> obtenerListaCausar()
    {
        List<CausacionServicios> lstCausar = new List<CausacionServicios>();
        CausacionServicios pCausa;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox chkCausacion = (CheckBox)rFila.FindControl("chkCausacion");
            if (chkCausacion.Checked)
            {
                pCausa = new CausacionServicios();
                pCausa.idcausacion = 0;
                pCausa.fecha_causacion = DateTime.ParseExact(txtFecha.Texto, gFormatoFecha, null);
                pCausa.numero_servicio = rFila.Cells[1].Text != "&nbsp;" ? Convert.ToInt32(rFila.Cells[1].Text) : 0;
                decimal pVrCausado = Convert.ToDecimal(gvLista.DataKeys[rFila.RowIndex].Values[1].ToString());
                pCausa.valor_causado = pVrCausado;
                pCausa.codusuario = Convert.ToInt32(Usuario.codusuario);
                lstCausar.Add(pCausa);
            }
        }
        return lstCausar;
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            List<CausacionServicios> lstCausar = obtenerListaCausar();

            //DATOS DE LA OPERACION
            Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
            vOpe.cod_ope = 0;
            vOpe.tipo_ope = 18;
            vOpe.cod_caja = 0;
            vOpe.cod_cajero = 0;
            vOpe.observacion = "Operacion - Causacion Operacion";
            vOpe.cod_proceso = null;
            vOpe.fecha_oper = DateTime.ParseExact(txtFecha.Texto, gFormatoFecha, null);
            vOpe.fecha_calc = DateTime.Now;
            vOpe.cod_ofi = Usuario.cod_oficina;

            Int64 COD_OPE = 0;
            string pError = string.Empty;
            CausaServicios.CrearCausacionServicios(ref COD_OPE, ref pError, vOpe, ddlLinea.SelectedValue, lstCausar, Usuario);

            if (!string.IsNullOrEmpty(pError))
            {
                VerError(pError.ToString());
                return;
            }

            if (COD_OPE != 0)
            {
                Actualizar();

                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = COD_OPE;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 18;
                Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.ParseExact(txtFecha.Texto, gFormatoFecha, null);
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = lblCodProveedor.Text != "" ? lblCodProveedor.Text : Usuario.codusuario.ToString();
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CausaServicios.CodigoProgramaCausa, "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<Servicio> lstConsulta = new List<Servicio>();
            String filtro = obtFiltro();
            DateTime pFechaCausa;
            pFechaCausa = txtFecha.Text == "" ? DateTime.MinValue : DateTime.ParseExact(txtFecha.Texto, gFormatoFecha, null);

            lstConsulta = CausaServicios.ListarServiciosCausacion(filtro, pFechaCausa, (Usuario)Session["usuario"]);

            gvLista.PageSize = 20;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;

            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTCAUSACION"] = lstConsulta;
                toolBar.MostrarGuardar(true);
                toolBar.MostrarExportar(true);
                lblTexto.Visible = true;
                txtVrTotal.Visible = true;
                CalcularTotal();
            }
            else
            {
                panelGrilla.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
                Session["DTCAUSACION"] = null;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarExportar(false);
                lblTexto.Visible = false;
                txtVrTotal.Visible = false;
            }

            Session.Add(CausaServicios.CodigoProgramaCausa + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CausaServicios.CodigoProgramaCausa, "Actualizar", ex);
        }
    }


    private string obtFiltro()
    {
        String filtro = String.Empty;

        if (ddlLinea.SelectedIndex != 0)
            filtro += " AND S.COD_LINEA_SERVICIO = '" + ddlLinea.SelectedValue + "'";
        filtro += " AND S.VALOR_CUOTA != 0 AND S.ESTADO = 'C' ";
        return filtro;
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (txtFecha.Texto == "")
        {
            VerError("Ingrese la fecha de causación");
            return;
        }
        if (gvLista.Rows.Count > 0 && Session["DTCAUSACION"] != null)
        {
            string Fecha = "";
            if (txtFecha.Texto != null)
                Fecha = txtFecha.Texto.Replace("/", "");

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.Columns[0].Visible = false;
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTCAUSACION"];
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
            Response.AddHeader("Content-Disposition", "attachment;filename=CausacionServicio_ "+Fecha+".xls");
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

    protected void chkEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkEncabezado = (CheckBox)sender;
        if (chkEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("chkCausacion");
                cbSeleccionar.Checked = chkEncabezado.Checked;
            }
            CalcularTotal();
        }
    }


    protected void chkCausacion_CheckedChanged(object sender, EventArgs e)
    {
        ValidarChecked();
        CalcularTotal();
    }

    protected void ValidarChecked()
    {
        int CantReg = gvLista.Rows.Count;
        int CantSelect = gvLista.Rows.OfType<GridViewRow>().Where(x => ((CheckBox)x.FindControl("chkCausacion")).Checked).Count();
        CheckBox chkEncabezado = (CheckBox)gvLista.HeaderRow.FindControl("chkEncabezado");
        chkEncabezado.Checked = CantReg == CantSelect ? true : false;
    }



    protected void ddlLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        LineaServiciosServices BOLineaServ = new LineaServiciosServices();
        if (ddlLinea.SelectedItem != null)
        {
            lblCodProveedor.Text = "";
            if (ddlLinea.SelectedIndex != 0)
            {
                LineaServicios vDetalle = new LineaServicios();
                vDetalle = BOLineaServ.ConsultarLineaSERVICIO(ddlLinea.SelectedValue, (Usuario)Session["usuario"]);
                if (vDetalle != null)
                    lblCodProveedor.Text = vDetalle.cod_proveedor != null ? vDetalle.cod_proveedor.ToString() : "";
            }
        }
    }
}