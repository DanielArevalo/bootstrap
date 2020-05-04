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
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;

partial class Lista : GlobalWeb
{
    TrasladoDevolucionServices TrasladoServicios = new TrasladoDevolucionServices();
    int tipoOpe = 59;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TrasladoServicios.CodigoProgramaMenor, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaMenor, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtFecha.ToDateTime = DateTime.Now;
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaMenor, "Page_Load", ex);
        }
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


    protected Boolean ValidarDatos()
    {
        VerError("");
        int cont = 0;
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
            if (cbSeleccionar != null)
            {
                if (cbSeleccionar.Checked == true)
                    cont++;
            }
        }
        if (cont == 0)
        {
            VerError("No existen personas con devoluciones seleccionadas");
            return false;
        }
        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(txtFecha.ToDateTime, tipoOpe) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación " + tipoOpe + "=Traslado de Devoluciones");
            return false;
        }

        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        try
        {
            if (ValidarDatos())
            {
                ctlMensaje.MostrarMensaje("Desea realizar el traslado de los registros seleccionados ?");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaMenor, "btnGuardar_Click", ex);
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!panelProceso.Visible && panelGeneral.Visible)
        {
            rpta = ctlproceso.Inicializar(tipoOpe, txtFecha.ToDateTime, (Usuario)Session["Usuario"]);
            if (rpta > 1)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                // Activar demás botones que se requieran
                panelGeneral.Visible = false;
                panelProceso.Visible = true;
            }
            else
            {
                // Crear la tarea de ejecución del proceso                
                if (Aplicar())
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }
        }
    }


    protected bool Aplicar()
     {
        // Variables
        Usuario usuario = new Usuario();
        if (Session["usuario"] == null)
            return false;
        usuario = (Usuario)Session["usuario"];
        string sError = "";

        // Determinar el proceso del traslado de devoluciones      
        Xpinn.Contabilidad.Entities.ProcesoContable eproceso = new Xpinn.Contabilidad.Entities.ProcesoContable();
        eproceso = ConsultarProcesoContable(tipoOpe, ref sError, usuario);
        if (eproceso == null && sError.Trim() != "")
        {
            VerError("No hay ningún proceso contable parametrizado para el traslado masivo de devoluciones");
            return false;
        }

        // Realizar el proceso para cada devolución seleccionada
        try
        {
            List<TrasladoDevolucion> lstTotDevoluciones = new List<TrasladoDevolucion>();
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                if (cbSeleccionar != null)
                {
                    if (cbSeleccionar.Checked == true)
                    {
                        List<TrasladoDevolucion> lstDevoluciones = new List<TrasladoDevolucion>();
                        string sIdentificacion = rFila.Cells[2].Text.ToString();
                        lstDevoluciones = TrasladoServicios.ListarDevolucionesPersona(sIdentificacion, usuario);
                        foreach (TrasladoDevolucion devol in lstDevoluciones)
                        {
                            devol.tipo_tran = 904;
                            devol.estado = 2;
                            devol.valor = devol.saldo;
                            lstTotDevoluciones.Add(devol);
                        }
                    }
                }
            }

            // Actualizar listado de devoluciones y mostrar los comprobantes generados
            Int64 codOpe = 0;
            string Error = "";
            if (lstTotDevoluciones.Count > 0)
            {
                if (!TrasladoServicios.TrasladoDevolucionesMenores(txtFecha.ToDateTime, lstTotDevoluciones, tipoOpe, ref codOpe, ref Error, usuario))
                {
                    VerError("Se presentaron errores al realizar el traslado de devoluciones. Error: " + Error);
                    return false;
                }
                ctlproceso.CargarVariables(codOpe, tipoOpe, 0, usuario);
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaMenor, "btnContinuar_Click", ex);
        }

        return true;
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
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaMenor, "gvLista_PageIndexChanging", ex);
        }
    }
    
    
    private void Actualizar()
    {
        try
        {
            List<TrasladoDevolucion> lstConsulta = new List<TrasladoDevolucion>();
            String filtro = obtFiltro();
            String orden = "";
            lstConsulta = TrasladoServicios.ListarDevolucionesMenores(orden, filtro, 2000, (Usuario)Session["usuario"]);
            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                toolBar.MostrarGuardar(true);
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
                toolBar.MostrarGuardar(false);
            }
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
            Session.Add(TrasladoServicios.CodigoProgramaMenor + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaMenor, "Actualizar", ex);
        }
    }
      

    private string obtFiltro()
    {        
        String filtro = " ";
        return filtro;
    }


    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEncabezado = (CheckBox)sender;
        if (cbSeleccionarEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;
            }
        }
    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                GridView gvExportar = gvLista;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvExportar);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=DevolucionesMenores.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();             

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }
    }

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
            // Aquí va la función que hace lo que se requiera grabar en la funcionalidad
            Aplicar();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


}
