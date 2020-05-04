using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using Xpinn.Caja.Services;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

public partial class Nuevo : GlobalWeb
{
    Xpinn.Caja.Services.ArqueoCajaService arqueoCajaService = new Xpinn.Caja.Services.ArqueoCajaService();
    Xpinn.Caja.Entities.ArqueoCaja arqueoCaja = new Xpinn.Caja.Entities.ArqueoCaja();

    Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();



    Xpinn.Caja.Services.MovimientoCajaService movCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();
    Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();

    Xpinn.Caja.Entities.SaldoCaja saldo = new Xpinn.Caja.Entities.SaldoCaja();
    Xpinn.Caja.Services.SaldoCajaService saldoService = new Xpinn.Caja.Services.SaldoCajaService();


    List<Xpinn.Caja.Entities.MovimientoCaja> lstSaldos = new List<Xpinn.Caja.Entities.MovimientoCaja>();

    Usuario user = new Usuario();



    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(arqueoCajaService.CodigoProgramaConsultasArqueos, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;



        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(arqueoCajaService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {



            if (!IsPostBack)
            {

                consultar_cajeros();

            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(arqueoCajaService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        actualizar();

    }
    private void actualizar()
    {
        Int64 cajero = Convert.ToInt64(ddlAsesores.SelectedValue);
         DateTime fecha;
         fecha = Convert.ToDateTime(txtFechaIni.Text);
        
        lstSaldos = movCajaServicio.Listararqueo(cajero, fecha, (Usuario)Session["usuario"]);

        gvdetalles.DataSource = lstSaldos;
        if (lstSaldos.Count > 0)
        {
            gvdetalles.Visible = true;

            lblTotalRegs.Visible = true;
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstSaldos.Count.ToString();
            gvdetalles.DataBind();
           // ValidarPermisosGrilla(gvdetalles);
        }
        else
        {
            gvdetalles.Visible = false;

            lblTotalRegs.Visible = false;
        }

    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {

        Navegar("~/General/Global/inicio.aspx");


    }
    protected void gvsingarantias_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void gvGarantias_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvdetalles.Rows[e.NewEditIndex].Cells[0].Text;
        Session["codigo_cajero"] = id;
        String fecha = gvdetalles.Rows[e.NewEditIndex].Cells[3].Text;
        Session["fecha"] = fecha;
        String caja = gvdetalles.Rows[e.NewEditIndex].Cells[4].Text;
        Session["caja"] = caja;
        String cajero = gvdetalles.Rows[e.NewEditIndex].Cells[5].Text;
        Session["cajero"] = cajero;
        Navegar(Pagina.Nuevo);
    }
    protected void gvGarantias_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvdetalles.PageIndex = e.NewPageIndex;
            actualizar();
        }
        catch 
        {
            //BOexcepcion.Throw(garantiasservicio.CodigoProgramaConsultasArqueos, "gvLista_PageIndexChanging", ex);
        }
    }
    protected void txtFechaIni_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ddlAsesores_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvGarantias_SelectedIndexChanged(object sender, GridViewEditEventArgs e)
    {
        //aqui
    }
    private void consultar_cajeros()
    {
        Usuario usuap = (Usuario)Session["usuario"];
        ddlAsesores.Visible = true;
        Labelejecutivos.Visible = true;
        Cajero ejec = new Cajero();
        long iOficina = (usuap.cod_oficina);
        ejec.cod_oficina = iOficina;
        ddlAsesores.DataSource = cajeroService.ListarCajero(ejec, (Usuario)Session["usuario"]);
        ddlAsesores.DataTextField = "nom_cajero";
        ddlAsesores.DataValueField = "cod_cajero";
        ddlAsesores.DataBind();
        ddlAsesores.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }
}