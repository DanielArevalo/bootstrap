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

partial class Lista : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices AhorroVistaServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    PoblarListas lista = new PoblarListas();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AhorroVistaServicio.CodigoProgramaReportePeriodico, "L");

            Site toolBar = (Site)this.Master;

            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarExportar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaReportePeriodico, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargar();
                CargarListar();
                CargarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaReportePeriodico);
                if (Session[AhorroVistaServicio.CodigoProgramaReportePeriodico + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaReportePeriodico, "Page_Load", ex);
        }
    }

    protected void cargar()
    {

        lista.PoblarListaDesplegable("oficina", " COD_OFICINA,NOMBRE", " estado = 1", "1", ddlOficina, (Usuario)Session["usuario"]);

    }
    private void CargarListar()
    {
        Xpinn.Ahorros.Services.LineaAhorroServices linahorroServicio = new Xpinn.Ahorros.Services.LineaAhorroServices();
        Xpinn.Ahorros.Entities.LineaAhorro linahorroVista = new Xpinn.Ahorros.Entities.LineaAhorro();
        List<Xpinn.Ahorros.Entities.LineaAhorro> lstAhorro = linahorroServicio.ListarLineaAhorro(linahorroVista, (Usuario)Session["usuario"]);
        linahorroVista.cod_linea_ahorro = null;
        linahorroVista.descripcion = "";
        lstAhorro.Insert(0, linahorroVista);
        ddlLineaAhorro.DataTextField = "descripcion";
        ddlLineaAhorro.DataValueField = "cod_linea_ahorro";
        ddlLineaAhorro.DataSource = lstAhorro;
        ddlLineaAhorro.DataBind();
  
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        ///inicializa en 0 los campos vacios de la grilla
     
        if (fechainicial.Text == "")
        {
            VerError("Ingrese una fecha inicial");
            return;
        }
        if (fechafinal.Text == "")
        {
            VerError("Ingrese una fecha final");
            return;
        }
        if (fechainicial.ToDateTime >= fechafinal.ToDateTime)
        {
            VerError("La fecha inicial no puede ser menor ni igual a la fecha final");
            return;
        }
        GuardarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaReportePeriodico);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        fechainicial.Text = ("");
        lblTotalRegs.Text = ("");
        fechafinal.Text = ("");
        gvLista.Visible = false;
        LimpiarValoresConsulta(pConsulta, AhorroVistaServicio.CodigoProgramaReportePeriodico);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaReportePeriodico + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoProgramaReportePeriodico + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[AhorroVistaServicio.CodigoProgramaReportePeriodico + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //try
        //{
            string id = Convert.ToString(e.Keys[0]);
            AhorroVistaServicio.EliminarAhorroVista(id, (Usuario)Session["usuario"]);
            Actualizar();
        //}
        //catch (Xpinn.Util.ExceptionBusiness ex)
        //{
        //    VerError(ex.Message);
        //}
        //catch (Exception ex)
        //{
        //    VerError(ex.Message);
        //}
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
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaReporteGMF, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        VerError("");
        try
        {
            ///Guarda en una variable de la entidad ahorrovista
            
            Xpinn.Ahorros.Entities.AhorroVista ahorro = new Xpinn.Ahorros.Entities.AhorroVista();
            if(ddlOficina.SelectedIndex != 0)
                if (ddlOficina == null)
                {
                    ahorro.cod_oficina = Convert.ToInt32(ddlOficina.SelectedIndex == 0);
                }
                else
                {
                    ahorro.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);
                }
            ahorro.cod_linea_ahorro = Convert.ToString(ddlLineaAhorro.SelectedValue);
            ahorro.fecha_apertura = Convert.ToDateTime(fechainicial.ToDate);
            ahorro.fecha_cierre = Convert.ToDateTime(fechafinal.ToDate);

            List<Xpinn.Ahorros.Entities.AhorroVista> lstConsulta = new List<Xpinn.Ahorros.Entities.AhorroVista>();

            lstConsulta = AhorroVistaServicio.ReportePeriodico(ahorro, (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            Site toolBar = (Site)this.Master;
            if (lstConsulta.Count > 0)
            {
               ///Si la consulta es mayor a 0  rellena la grilla
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["DTAhorroVista"] = lstConsulta;
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
                toolBar.MostrarExportar(true);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                toolBar.MostrarExportar(false);
            }

            Session.Add(AhorroVistaServicio.CodigoProgramaReportePeriodico + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AhorroVistaServicio.CodigoProgramaReportePeriodico, "Actualizar", ex);
        }
    }

    private Xpinn.Ahorros.Entities.AhorroVista ObtenerValores()
    {
        Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
        if (ddlOficina.Text.Trim() != "")
            vAhorroVista.cod_oficina = Convert.ToInt32(ddlOficina.Text.Trim());
        ///numero de oficina actualizacion

        if (ddlLineaAhorro.Text.Trim() != "")
            vAhorroVista.cod_linea_ahorro = Convert.ToString(ddlLineaAhorro.Text.Trim());

        if (fechafinal.ToDate.Trim() != "")
            vAhorroVista.fecha_cierre = Convert.ToDateTime(fechafinal.ToDate.Trim());

        ///fecha final
            if (fechainicial.ToDate.Trim() != "")
                vAhorroVista.fecha_apertura = Convert.ToDateTime(fechainicial.ToDate.Trim());
        ///fecha inicial

        return vAhorroVista;
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportToExcel(gvLista);
    }

    protected void ExportToExcel(GridView GridView1)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GMF.xls");
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

}