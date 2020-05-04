using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Reporteador.Services;
using Xpinn.Reporteador.Entities;
using Xpinn.Programado.Services;
using Xpinn.Programado.Entities;

public partial class Lista : GlobalWeb
{
    Xpinn.Reporteador.Services.UIAFService reporteServicio = new Xpinn.Reporteador.Services.UIAFService();
    CuentasProgramadoServices cuentaProgramadoServices = new CuentasProgramadoServices();
    
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(cuentaProgramadoServices.CodigoProgramaReporte, "L");

            Site toolBar = (Site)this.Master;
            //toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
            //ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentaProgramadoServices.CodigoProgramaReporte + "L", "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            if (!IsPostBack)
            {

                var fechain = DateTime.Now;
                txtFechaFin.Text = fechain.ToString();
                DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                txtFechaIni.Text = fecha.ToString();
                cargarListaddl();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentaProgramadoServices.CodigoProgramaReporte + "L", "Page_Load", ex);
        }
    }


	private void Actualizar()
	{
		List<ReporteCuentasPeriodico> listagr;
		try
		{
			Site toolBar = (Site)this.Master;

			if (ddlLinea.SelectedIndex <= 0 || txtFechaIni.Text.Length < 0 || txtFechaFin.Text.Length < 0)
			{
				lblInfo.Text = "Debe Seleccionar por lo menos los 3 primeros datos";
				lblInfo.Visible = true;
			}
			else
			{
				var fechaini = Convert.ToDateTime(txtFechaIni.Text);
				var fechaFinal = Convert.ToDateTime(txtFechaFin.Text);
				if (fechaini > fechaFinal)
				{
					lblInfo.Text = "<br/> la fecha inicial no puede ser mayor que la final ";
					return;
				}


				if (ddlOficina.SelectedIndex <= 0)
					listagr = cuentaProgramadoServices.ReportePeriodico(ddlLinea.SelectedValue.ToString(), fechaini, fechaFinal, 0, (Usuario)Session["usuario"]);
				 else
					listagr = cuentaProgramadoServices.ReportePeriodico(ddlLinea.SelectedValue.ToString(), fechaini, fechaFinal, Convert.ToInt64(ddlOficina.SelectedValue), (Usuario)Session["usuario"]);

                        gvLista.PageSize = 20;
                        String emptyQuery = "Fila de datos vacia";
                        gvLista.EmptyDataText = emptyQuery;
                        gvLista.DataSource = listagr;

					if (listagr.Count > 0)
					{
                        Listado.Visible = true;
                        gvLista.Visible = true;
						lblInfo.Text = "<br/> Registros encontrados " + listagr.Count.ToString();
						lblInfo.Visible = true;
						lblTotalRegs.Visible = true;
						toolBar.MostrarExportar(true);
						gvLista.DataBind();
					}
					else
					{
						gvLista.Visible = false;
						lblTotalRegs.Visible = false;
						lblInfo.Text = "<br/> Registros encontrados " + listagr.Count.ToString();
						lblInfo.Visible = true;
						toolBar.MostrarExportar(false);
					}
				 }
			
		}
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentaProgramadoServices.CodigoProgramaReporte + "L", "btnConsultar_Click", ex);
        }
    }


    Boolean ValidarDatos()
    {
        VerError("");

        if (txtFechaIni.Text != "" && txtFechaFin.Text != "")
        {
            if (Convert.ToDateTime(txtFechaIni.Text) > Convert.ToDateTime(txtFechaFin.Text))
            {
                VerError("No puede Ingresar una Fecha inicial mayor a la fecha final");
                return false;
            }
        }

        return true;
    }



    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtFechaIni.Text = "";
        txtFechaFin.Text = "";
        ddlLinea.ClearSelection();
        ddlOficina.ClearSelection();
        gvLista.Visible = false;
        lblInfo.Visible = false;
        var fechain = DateTime.Now;
        txtFechaFin.Text = fechain.ToString();
        DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        txtFechaIni.Text = fecha.ToString();
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
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
            BOexcepcion.Throw(reporteServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }



    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id;
        id = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        Session[reporteServicio.CodigoPrograma + ".id"] = id;

        Navegar(Pagina.Nuevo);
    }


    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvLista.Visible==true)
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=reporte.xls");
            Response.ContentType = "application/excel";

            System.IO.StringWriter stringwriter = new System.IO.StringWriter();
            HtmlTextWriter htmlte = new HtmlTextWriter(stringwriter);
            gvLista.HeaderRow.Style.Add("background-color", "#ffffff");

            foreach (TableCell tablecell in gvLista.HeaderRow.Cells)
            {
                tablecell.Style["background-color"] = "#a555129";
            }
            foreach (GridViewRow gridviewros in gvLista.Rows)
            {
                gridviewros.BackColor = System.Drawing.Color.White;

                foreach (TableCell tablecel in gridviewros.Cells)
                {
                    tablecel.Style["background-color"] = "#fff7e7";
                }
            }
            gvLista.RenderControl(htmlte);
            Response.Write(stringwriter.ToString());
            Response.End(); 
        }
    }

  

    void cargarListaddl() 
    {
        Xpinn.Asesores.Data.OficinaData listaOficina = new Xpinn.Asesores.Data.OficinaData();
        Xpinn.Asesores.Entities.Oficina oficina = new Xpinn.Asesores.Entities.Oficina();
        oficina.Estado = 1;
        var lista = listaOficina.ListarOficina(oficina,(Usuario)Session["usuario"]);
       
        if (lista!=null)
        {
            lista.Insert(0, new Xpinn.Asesores.Entities.Oficina { NombreOficina= "Seleccione un Item", IdOficina=0 });
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