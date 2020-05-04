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
using Microsoft.Reporting.WebForms;
partial class Lista : GlobalWeb
{
    CuentasProgramadoServices CuentasPrograServicios = new CuentasProgramadoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CuentasPrograServicios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentasPrograServicios.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)

            {
                mvReporte.Visible = false;
                btnDatos.Visible = false;
                panelGrilla.Visible = false;
                CargarDropdown();
                CargarValoresConsulta(pConsulta, CuentasPrograServicios.CodigoPrograma);                
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentasPrograServicios.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, CuentasPrograServicios.CodigoPrograma);
        gvLista.DataSource = null;
        panelGrilla.Visible = false;
        lblTotalRegs.Visible = false;
        txtFechaApert.Text = "";
        lblInfo.Visible = false;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }


    void CargarDropdown()
    {
        ddlEstado.Items.Insert(0, new ListItem("Seleccione un item", "-1"));
        ddlEstado.Items.Insert(1, new ListItem("ACTIVOS", "1"));
        ddlEstado.Items.Insert(2, new ListItem("INACTIVOS", "0"));
        ddlEstado.SelectedIndex = -1;
        ddlEstado.DataBind();

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
            ddlLinea.Items.Insert(0, new ListItem("Seleccione un item","0"));
            ddlLinea.AppendDataBoundItems = true;
            ddlLinea.SelectedIndex = 0;
            ddlLinea.DataBind();
        }

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
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        CuentasProgramadoServices cuentasProgramado = new CuentasProgramadoServices();

        Session[cuentasProgramado.CodigoPrograma + ".id"] = null;
        Session[cuentasProgramado.CodigoPrograma + ".renovacion"] = null;
        Session[cuentasProgramado.CodigoPrograma + ".renovacionsaldo"] = null;
        Session[cuentasProgramado.CodigoPrograma + ".renovacioninteresliq"] = null;
        Session[cuentasProgramado.CodigoPrograma + ".renovacioninteres"] = null;
        Session[cuentasProgramado.CodigoPrograma + ".num_programado_renovar"] = null;
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(CuentasPrograServicios.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string ID = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        Session[CuentasPrograServicios.CodigoPrograma + ".id"] = ID;

        CuentasProgramadoServices cuentasProgramado = new CuentasProgramadoServices();

        Session[cuentasProgramado.CodigoPrograma + ".renovacion"] = null;
        Session[cuentasProgramado.CodigoPrograma + ".renovacionsaldo"] = null;
        Session[cuentasProgramado.CodigoPrograma + ".renovacioninteresliq"] = null;
        Session[cuentasProgramado.CodigoPrograma + ".renovacioninteres"] = null;
        Session[cuentasProgramado.CodigoPrograma + ".num_programado_renovar"] = null;
        Navegar(Pagina.Nuevo);        
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string conseID =gvLista.DataKeys[e.RowIndex].Values[0].ToString();
        Session["ID"] = conseID;
        ctlMensaje.MostrarMensaje("Desea eliminar el registro seleccionado?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            CuentasPrograServicios.EliminarAhorroProgramado(Convert.ToInt64(Session["ID"]), (Usuario)Session["usuario"]);            
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentasPrograServicios.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    
    private void Actualizar()
    {
        try
        {
            List<CuentasProgramado> lstConsulta = new List<CuentasProgramado>();
            String filtro = obtFiltro();
            DateTime pFecha;
            pFecha = txtFechaApert.ToDateTime == null ? DateTime.MinValue : txtFechaApert.ToDateTime;

            lstConsulta = CuentasPrograServicios.ListarAhorrosProgramado(filtro, pFecha, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            //MvPrincipal.ActiveViewIndex = 0;
            btnDatos.Visible = false;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                lblInfo.Visible = false;
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }
            Session.Add(CuentasPrograServicios.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentasPrograServicios.CodigoPrograma, "Actualizar", ex);
        }
    }
    private string obtFiltro()
    {
        
        String filtro = String.Empty;

        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and V.IDENTIFICACION = '" + txtIdentificacion.Text.Trim() + "'";
       

        if (txtCuenta.Text.Trim() != "")
            filtro += " and A.NUMERO_PROGRAMADO = " + txtCuenta.Text.Trim();
       
        if (ddlLinea.SelectedIndex != 0)
            filtro += " and A.COD_LINEA_PROGRAMADO = '" + ddlLinea.SelectedValue +"'";
        if (ddlOficina.SelectedIndex != 0)
            filtro += " and A.COD_OFICINA = " + ddlOficina.SelectedValue;
        if (ddlEstado.SelectedIndex != 0)
            filtro += " and A.ESTADO = " + ddlEstado.SelectedValue;
        
        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }
        return filtro;
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        mvReporte.Visible = true;
        MvPrincipal.Visible = false;
        btnDatos.Visible = true;
        mvReporte.ActiveViewIndex = 0;
        
        // ---------------------------------------------------------------------------------------------------------
        // Pasar datos al reporte
        // ---------------------------------------------------------------------------------------------------------

        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        ReportParameter[] param = new ReportParameter[4];
        param[0] = new ReportParameter("entidad", pUsuario.empresa);
        param[1] = new ReportParameter("nit", pUsuario.nitempresa);
        param[2] = new ReportParameter("Oficina", pUsuario.nombre_oficina);     
        param[3] = new ReportParameter("ImagenReport", ImagenReporte());
       
        //mvReporte.Visible = true;
        //rvReporte.LocalReport.DataSources.Clear();
        //rvReporte.LocalReport.EnableExternalImages = true;
        //rvReporte.LocalReport.SetParameters(param);
        //mvReporte.Visible = true;
        //rvReporte.LocalReport.Refresh();
        //mvReporte.Visible = true;

        rvReporte.LocalReport.EnableExternalImages = true;
        rvReporte.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTable());
        rvReporte.LocalReport.DataSources.Clear();
        rvReporte.LocalReport.DataSources.Add(rds);
        rvReporte.LocalReport.Refresh();

    }
    public DataTable CrearDataTable()
    {
        String filtro = String.Empty;
        String orden = String.Empty;
        System.Data.DataTable table = new System.Data.DataTable();
        List<CuentasProgramado> lstConsulta = new List<CuentasProgramado>();
        filtro = obtFiltro();
        //orden = obtOrden();
      //  lstConsulta = (List<CuentasProgramado>)Session["DTCUENTAS"];
        DateTime pFecha;
        pFecha = txtFechaApert.ToDateTime == null ? DateTime.MinValue : txtFechaApert.ToDateTime;

        lstConsulta = CuentasPrograServicios.ListarAhorrosProgramado(filtro, pFecha, (Usuario)Session["usuario"]);

        table.Columns.Add("Cuenta");
        table.Columns.Add("Linea");
        table.Columns.Add("Oficina");
        table.Columns.Add("F_Apertura");
        table.Columns.Add("Motivo_Apertura");
        table.Columns.Add("Identificacion");
        table.Columns.Add("Nombre");
        table.Columns.Add("F_Prox_Pago");
        table.Columns.Add("Plazo");
        table.Columns.Add("Forma_Pago");
        table.Columns.Add("Estado");

        foreach (CuentasProgramado item in lstConsulta)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.numero_programado;
            datarw[1] = item.nomlinea;
            datarw[2] = item.nomoficina;
            datarw[3] = item.fecha_apertura.ToShortDateString();
            datarw[4] = item.nommotivo_progra;
            datarw[5] = item.identificacion;
            datarw[6] = item.nombre;
            datarw[7] = item.fecha_proximo_pago.ToShortDateString();
            datarw[8] = item.plazo;
            datarw[9] = item.nomforma_pago;
            datarw[10] = item.nom_estado;
            table.Rows.Add(datarw);
        }
        return table;
    }


    protected void btnDatos_Click(object sender, EventArgs e)
    {
        MvPrincipal.Visible = true;
        mvReporte.Visible = false;
        Actualizar();
    }
}