using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Programado.Services;
using Xpinn.Programado.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Lista : GlobalWeb
{
    LineasProgramadoServices LineasProgramado = new LineasProgramadoServices();
    CuentasProgramadoServices  cuentasProgramado =new CuentasProgramadoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(cuentasProgramado.CodigoProgramaCierreCuenta, "L");
           // VisualizarOpciones(cuentasProgramado.CodigoProgramaCierreCuentaCierreCuenta, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentasProgramado.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargarListaddl();
                panelGrilla.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentasProgramado.GetType().Name + "L", "Page_Load", ex);
        }

    }



    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, cuentasProgramado.CodigoProgramaCierreCuenta);
        gvLista.DataSource = null;
        panelGrilla.Visible = false;
        lblTotalRegs.Visible = false;
        lblInfo.Visible = false;
        txtCuenta.Text = string.Empty;
        txtIdentificacion.Text = string.Empty;
        txtNombre.Text = string.Empty;
        txtFecha.Text = string.Empty;
        ddlLinea.ClearSelection();
        ddlOficina.ClearSelection();

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

    private void Actualizar()
    {
        DateTime fechaApertura;
        try
        {
            String filtro = obtFiltro();
            List<CierreCuentaAhorroProgramado> listaConsulta = new List<CierreCuentaAhorroProgramado>();

            if (txtFecha.Text != "")
                fechaApertura = Convert.ToDateTime(txtFecha.Text);
            else
                fechaApertura = DateTime.MinValue;

            listaConsulta = new CuentasProgramadoServices().cerrarCuentaProgramadoService(fechaApertura, filtro, (Usuario)Session["usuario"]);
            
            gvLista.PageSize = 20;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = listaConsulta;

            if (listaConsulta.Count > 0)
            {
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + listaConsulta.Count.ToString();
                lblInfo.Visible = false;
                gvLista.DataBind();
            }
            else
            {
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }
            Session.Add(cuentasProgramado.CodigoProgramaCierreCuenta + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentasProgramado.CodigoProgramaCierreCuenta, "Actualizar", ex);
        }
    }

    private string obtFiltro()
    {
        String filtro = String.Empty;
        if (txtCuenta.Text.Trim() != "")
            filtro += " and ap.numero_programado = " + txtCuenta.Text;
        if (ddlLinea.SelectedIndex!=0)
            filtro+=" and lp.COD_LINEA_PROGRAMADO = "+ddlLinea.SelectedValue;
        if (ddlOficina.SelectedIndex != 0)
            filtro += " and o.cod_oficina = " + ddlOficina.SelectedValue;
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and vp.identificacion = " + txtIdentificacion.Text;
        if (txtNombre.Text.Trim() != "")
            filtro += " and vp.nombre LIKE '%" + txtNombre.Text.Trim() + "%'";
        if (txtCodigoNomina.Text != "")
            filtro += "and vp.cod_nomina LIKE '%" + txtCodigoNomina.Text.Trim() + "%'";

        return filtro;
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        //Session["CodigoLinea"] = id;
        Session[cuentasProgramado.CodigoProgramaCierreCuenta + ".id"] = id;
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
            BOexcepcion.Throw(cuentasProgramado.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }
    void cargarListaddl()
    {
        Xpinn.Asesores.Data.OficinaData listaOficina = new Xpinn.Asesores.Data.OficinaData();
        Xpinn.Asesores.Entities.Oficina oficina = new Xpinn.Asesores.Entities.Oficina();
        oficina.Estado = 1;
        var lista = listaOficina.ListarOficina(oficina, (Usuario)Session["usuario"]);

        if (lista != null)
        {
            lista.Insert(0, new Xpinn.Asesores.Entities.Oficina { NombreOficina = "Seleccione un Item", IdOficina = 0 });
            ddlOficina.DataSource = lista;
            ddlOficina.DataTextField = "NombreOficina";
            ddlOficina.DataValueField = "IdOficina";
            ddlOficina.DataBind();
        }

        //LINEAS DE AHORRO PROGRAMADO
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


}