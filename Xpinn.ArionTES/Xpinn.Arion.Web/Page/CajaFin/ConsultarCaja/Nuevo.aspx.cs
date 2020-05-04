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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;


public partial class Nuevo : GlobalWeb
{
    Xpinn.Caja.Services.ArqueoCajaService arqueoCajaService = new Xpinn.Caja.Services.ArqueoCajaService();
    Xpinn.Caja.Entities.ArqueoCaja arqueoCaja = new Xpinn.Caja.Entities.ArqueoCaja();

    Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();

    Xpinn.Caja.Services.HorarioOficinaService HorarioService = new Xpinn.Caja.Services.HorarioOficinaService();
    Xpinn.Caja.Entities.HorarioOficina horario = new Xpinn.Caja.Entities.HorarioOficina();

    Xpinn.Caja.Services.MovimientoCajaService movCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();
    Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();

    Xpinn.Caja.Entities.SaldoCaja saldo = new Xpinn.Caja.Entities.SaldoCaja();
    Xpinn.Caja.Services.SaldoCajaService saldoService = new Xpinn.Caja.Services.SaldoCajaService();

    List<Xpinn.Caja.Entities.MovimientoCaja> lstCheques = new List<Xpinn.Caja.Entities.MovimientoCaja>();
    List<Xpinn.Caja.Entities.MovimientoCaja> lstSaldos = new List<Xpinn.Caja.Entities.MovimientoCaja>();
    List<Xpinn.Caja.Entities.MovimientoCaja> lstcomprobante = new List<Xpinn.Caja.Entities.MovimientoCaja>();
    Usuario user = new Usuario();



    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(arqueoCajaService.CodigoProgramaConsultasArqueos, "A");
            Site toolBar = (Site)this.Master;



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


            long cod_cajero = Convert.ToInt64(Session["codigo_cajero"]);



            ObtenerDatos();

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

    private void actualizar()
    {
      
        Int64 cajero = Convert.ToInt64(Session["codigo_cajero"]);
        DateTime fecha = Convert.ToDateTime(Session["fecha"]);
        lstSaldos = movCajaServicio.Listararqueodetalle(cajero, fecha, (Usuario)Session["usuario"]);

        gvSaldos.DataSource = lstSaldos;
        if (lstSaldos.Count > 0)
        {
            gvSaldos.Visible = true;
            gvSaldos.DataBind();
            ValidarPermisosGrilla(gvSaldos);
        }
        else
        {
            gvSaldos.Visible = false;
        }



    }

    protected void ObtenerDatos()
    {
        Usuario pUsu = new Usuario();
        pUsu.codusuario = Convert.ToInt64(Session["codigo_cajero"]);
      

        try
        {
            arqueoCaja = arqueoCajaService.ConsultarCajero(pUsu);//se consulta la informacion del cajero que se encuentra conectado
            if (!string.IsNullOrEmpty(arqueoCaja.nom_oficina.ToString()))
                txtOficina.Text = arqueoCaja.nom_oficina.ToString();
            if (!string.IsNullOrEmpty(arqueoCaja.nom_caja.ToString()))
                txtCaja.Text = arqueoCaja.nom_caja.ToString();
            if (!string.IsNullOrEmpty(arqueoCaja.nom_cajero.ToString()))
                txtCajero.Text = arqueoCaja.nom_cajero.ToString().Trim();
            if (!string.IsNullOrEmpty(arqueoCaja.fecha_cierre.ToString()))
                txtFechaArqueo.Text = Convert.ToDateTime(Session["fecha"]).ToString("dd/MM/yyyy");

            actualizar();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(arqueoCajaService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }





    protected void btnRegresar_Click(object sender, EventArgs e)
    {

        Navegar("~/General/Global/inicio.aspx");


    }


    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        string printScript =
               @"function PrintGridView()
            {
         
           div = document.getElementById('DivButtons');
             div.style.display='none';
            var gridInsideDiv = document.getElementById('gvDiv');
            var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');
            printWindow.document.write(gridInsideDiv.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();}";
        this.ClientScript.RegisterStartupScript(Page.GetType(), "PrintGridView", printScript.ToString(), true);
        btnImprimir.Attributes.Add("onclick", "PrintGridView();");
    }
}