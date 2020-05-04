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
using Xpinn.Programado.Entities;
using Xpinn.Programado.Services;
using System.IO;
using System.Text;

partial class Lista : GlobalWeb
{
    MovimientoCuentasServices MovimientoCtaService = new MovimientoCuentasServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {       
            VisualizarOpciones(MovimientoCtaService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MovimientoCtaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarDropdown();
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MovimientoCtaService.CodigoPrograma, "Page_Load", ex);
        }
    }

    
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtFechaApertura.Text = "";
        LimpiarValoresConsulta(pConsulta, MovimientoCtaService.CodigoPrograma);
        panelGrilla.Visible = false;
        gvLista.DataSource = null;
        lblInfo.Visible = false;
        lblTotalRegs.Visible = false;
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate();
        if (Page.IsValid)
        {
            Actualizar();
        }
    }

    
    void cargarDropdown()
    {
        ddlFormaPago.Items.Insert(0, new ListItem("Seleccione un item","0"));
        ddlFormaPago.Items.Insert(1, new ListItem("CAJA", "1"));
        ddlFormaPago.Items.Insert(2, new ListItem("NÓMINA", "2"));
        ddlFormaPago.SelectedIndex = 0;
        ddlFormaPago.DataBind();

        ddlEstado.Items.Insert(0, new ListItem("Seleccione un item",""));
        ddlEstado.Items.Insert(1, new ListItem("Inactiva", "0"));
        ddlEstado.Items.Insert(2, new ListItem("Activa", "1"));
        ddlEstado.Items.Insert(3, new ListItem("Terminada", "2"));
        ddlEstado.Items.Insert(4, new ListItem("Anulada", "3"));
        ddlEstado.SelectedIndex = 0;
        ddlEstado.DataBind();

        Xpinn.Asesores.Data.OficinaData vDatosOficina = new Xpinn.Asesores.Data.OficinaData();
        Xpinn.Asesores.Entities.Oficina pOficina = new Xpinn.Asesores.Entities.Oficina();
        List<Xpinn.Asesores.Entities.Oficina> lstOficina = new List<Xpinn.Asesores.Entities.Oficina>();
        pOficina.Estado = 1;
        lstOficina = vDatosOficina.ListarOficina(pOficina, (Usuario)Session["usuario"]);
        if (lstOficina.Count > 0)
        {
            ddlOficina.DataSource = lstOficina;
            ddlOficina.DataTextField = "NombreOficina";
            ddlOficina.DataValueField = "IdOficina";
            ddlOficina.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlOficina.AppendDataBoundItems = true;
            ddlOficina.SelectedIndex = 0;
            ddlOficina.DataBind();
        }

        Xpinn.Programado.Data.LineasProgramadoData vDatosLinea = new Xpinn.Programado.Data.LineasProgramadoData();
        LineasProgramado pLineas = new LineasProgramado();

        List<LineasProgramado> lstConsulta = new List<LineasProgramado>();
        pLineas.estado = 1;
        lstConsulta = vDatosLinea.ListarComboLineas(pLineas, (Usuario)Session["usuario"]);

        if (lstConsulta.Count > 0)
        {
            ddlLinea.DataSource = lstConsulta;
            ddlLinea.DataTextField = "nombre";
            ddlLinea.DataValueField = "cod_linea_programado";
            ddlLinea.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlLinea.AppendDataBoundItems = true;
            ddlLinea.SelectedIndex = 0;
            ddlLinea.DataBind();
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
            BOexcepcion.Throw(MovimientoCtaService.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();         
        Session[MovimientoCtaService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }
    
    
    private void Actualizar()
    {
        try
        {
            List<CuentasProgramado> lstConsulta = new List<CuentasProgramado>();
            String filtro = obtFiltro();
            DateTime pFechaApert;
            pFechaApert = txtFechaApertura.ToDateTime == null ? DateTime.MinValue : txtFechaApertura.ToDateTime;

            lstConsulta = MovimientoCtaService.ListarAhorrosProgramado(filtro, pFechaApert,(Usuario)Session["usuario"]);

            gvLista.PageSize = 20;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            ViewState[Usuario.codusuario + "DTCuentasProgramado"] = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();               
            }
            else
            {
                panelGrilla.Visible = false;
                gvLista.DataSource = null;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }

            Session.Add(MovimientoCtaService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MovimientoCtaService.CodigoPrograma, "Actualizar", ex);
        }
    }
       
   
    private string obtFiltro()
    {
        String filtro = String.Empty;

        if (txtNumCta.Text.Trim() != "")
            filtro += "AND A.NUMERO_PROGRAMADO = '" + txtNumCta.Text.Trim() + "'";
        if (ddlLinea.SelectedIndex != 0)
            filtro += "AND A.COD_LINEA_PROGRAMADO = '" + ddlLinea.SelectedValue + "'";
        if (ddlFormaPago.SelectedIndex != 0)
            filtro += "AND A.FORMA_PAGO = " + ddlFormaPago.SelectedValue;
        if (ddlEstado.SelectedIndex != 0)
            filtro += "AND A.ESTADO = " + ddlEstado.SelectedValue;
        if (txtCodPersona.Text != "")
            filtro += "AND A.COD_PERSONA = " + txtCodPersona.Text.Trim();
        if(txtIdentificacion.Text != "")
            filtro += "AND V.IDENTIFICACION = '" + txtIdentificacion.Text.Trim() + "'";
        if(txtNombre.Text != "")
            filtro += "AND V.NOMBRE LIKE '%" + txtNombre.Text.Trim() + "%'";
        if (txtCodigoNomina.Text != "")
            filtro += "AND V.COD_NOMINA LIKE '%" + txtCodigoNomina.Text.Trim() + "%'";
        if (ddlOficina.SelectedIndex != 0)
            filtro += "AND A.COD_OFICINA = " + ddlOficina.SelectedValue;

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "WHERE " + filtro;
        }
        return filtro;
    }


    private void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        if (gvLista.Rows.Count > 0)
            ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=MovimientosCuentasAhoProgramado.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.Default;
        StringWriter sw = new StringWriter();
        ExpGrilla expGrilla = new ExpGrilla();

        sw = expGrilla.ObtenerGrilla(GridView1, (List<CuentasProgramado>)ViewState[Usuario.codusuario + "DTCuentasProgramado"]);

        Response.Write(expGrilla.style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

}