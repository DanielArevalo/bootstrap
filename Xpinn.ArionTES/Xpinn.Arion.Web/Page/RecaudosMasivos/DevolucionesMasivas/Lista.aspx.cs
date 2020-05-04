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
    int tipoOpe = 117;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TrasladoServicios.CodigoProgramaAplMasivo, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
            toolBar.MostrarExportar(true);
            toolBar.eventoExportar += btnExportar_Click;

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
                CargarEmpresa();//txtFecha.ToDateTime = DateTime.Now;
                Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaMenor, "Page_Load", ex);
        }
    }


    private void CargarEmpresa()
    {
        try
        {
            Xpinn.Tesoreria.Services.RecaudosMasivosService recaudoServicio = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstModulo = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();

            lstModulo = recaudoServicio.ListarEmpresaRecaudo(null, (Usuario)Session["usuario"]);

            ddlEmpresa.DataSource = lstModulo;
            ddlEmpresa.DataTextField = "nom_empresa";
            ddlEmpresa.DataValueField = "cod_empresa";
            ddlEmpresa.DataBind();

            ddlEmpresa.Items.Insert(0, "");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaAplMasivo, "CargarEmpresa", ex);
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

        if (txtFec_Apli.Text == "")
        {
            VerError("Seleccione Fecha de Aplicacion de Operaciones");
            return false;
        }

        if (Convert.ToDateTime(txtFec_Apli.Text) > DateTime.Now)
        {
            VerError("No puede aplicar devoluciones con fecha mayor al dia de hoy ");
            return false;
        }
        // Validar que exista la parametrización contable por procesos
        //if (ValidarProcesoContable(txtFecha.ToDateTime, tipoOpe) == false)
        //{
        //    VerError("No se encontró parametrización contable por procesos para el tipo de operación " + tipoOpe + "=Traslado de Devoluciones");
        //    return false;
        //}

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
            rpta = ctlproceso.Inicializar(tipoOpe, DateTime.Now, (Usuario)Session["Usuario"]);
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
                        string cod_persona = rFila.Cells[1].Text.ToString();
                        String filtro = obtFiltro();
                        filtro += " AND d.cod_persona = " + cod_persona;
                        filtro += " AND d.num_devolucion = " + long.Parse(rFila.Cells[2].Text.ToString());

                        String orden = "";
                        lstDevoluciones = TrasladoServicios.ListarDevolucionesMasivas(orden, filtro, (Usuario)Session["usuario"]);
                        foreach (TrasladoDevolucion devol in lstDevoluciones)
                        {
                            //devol.cod_persona = long.Parse(rFila.Cells[1].Text.ToString());
                            devol.num_devolucion = long.Parse(rFila.Cells[2].Text.ToString());
                            devol.fecha = DateTime.Now;
                            devol.valor = Decimal.Parse(rFila.Cells[6].Text.ToString());
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
                if (!TrasladoServicios.AplicacionMasivaDevoluciones(Convert.ToDateTime(txtFec_Apli.Text), lstTotDevoluciones, tipoOpe, ref codOpe, ref Error, usuario))
                {
                    VerError("Se presentaron errores al realizar la aplicacion de devoluciones Masivas. Error: " + Error);
                    return false;
                }
                ctlproceso.CargarVariables(codOpe, tipoOpe, 0, usuario);
            }
            else
            {
                VerError("No hay devoluciones para aplicar ");
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
            lstConsulta = TrasladoServicios.ListarDevolucionesMasivas(orden, filtro, (Usuario)Session["usuario"]);
            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            Site toolBar = (Site)this.Master;
            if (lstConsulta != null)
            {
                if (lstConsulta.Count > 0)
                {
                    gvLista.DataSource = lstConsulta;
                    Session["DTDETALLE"] = lstConsulta;
                    gvLista.DataBind();
                    panelGrilla.Visible = true;
                    lblTotalRegs.Visible = true;
                    toolBar.MostrarGuardar(true);
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    Session.Add(TrasladoServicios.CodigoProgramaMenor + ".consulta", 1);
                }
                else
                {
                    panelGrilla.Visible = false;
                    lblTotalRegs.Visible = true;
                    toolBar.MostrarGuardar(false);
                    lblTotalRegs.Text = "<br/> No se encontraron registros ";
                    Session.Add(TrasladoServicios.CodigoProgramaMenor + ".consulta", 1);
                }
            }
            else
            {
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = true;
                toolBar.MostrarGuardar(false);
                lblTotalRegs.Text = "<br/> No se encontraron registros ";
                Session.Add(TrasladoServicios.CodigoProgramaMenor + ".consulta", 1);
            }
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoProgramaMenor, "Actualizar", ex);
        }
    }


    private string obtFiltro()
    {
        String filtro = " ";


        if (ddlEmpresa.SelectedValue != "")
        {
            filtro += " AND rm.cod_empresa = " + ddlEmpresa.SelectedValue;
        }
        if (txtFechaPeriodo.Text != "")
        {
            Configuracion conf = new Configuracion();
            filtro += " AND rm.fecha_recaudo = '" + Convert.ToDateTime(txtFechaPeriodo.Text).ToShortDateString() + "' ";
        }
        if (txtNumeroRecaudo.Text != "")
        {
            filtro += " AND rm.numero_novedad = " + txtNumeroRecaudo.Text;
        }
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

    protected void ExportToExcel(GridView GridView1)
    {
        foreach (GridViewRow rFila in gvLista.Rows)
        {
            CheckBox check = (CheckBox)rFila.FindControl("cbSeleccionar");
            if (check != null)
            {
                if (check.Checked == true)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Devoluciones.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.ContentEncoding = Encoding.Default;
                    StringWriter sw = new StringWriter();
                    ExpGrilla expGrilla = new ExpGrilla();

                    sw = expGrilla.ObtenerGrilla(GridView1, null);

                    Response.Write(expGrilla.style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            //   ExportToExcel(gvLista);


            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=AplicacionMasivaDevoluciones.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTDETALLE"];
            gvLista.DataBind();
            StringBuilder sb = ExportarGridCSV(gvLista);
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
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
