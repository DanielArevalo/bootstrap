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

public partial class Nuevo : GlobalWeb
{
    Xpinn.Caja.Services.ArqueoCajaService arqueoCajaService = new Xpinn.Caja.Services.ArqueoCajaService();
    Xpinn.Caja.Entities.ArqueoCaja arqueoCaja = new Xpinn.Caja.Entities.ArqueoCaja();

    List<Xpinn.Caja.Entities.MovimientoCaja> lstSaldos = new List<Xpinn.Caja.Entities.MovimientoCaja>();
    Usuario user = new Usuario();
    System.Data.DataTable table = new System.Data.DataTable();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(arqueoCajaService.CodigoProgramaTesoreria, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.MostrarCancelar(false);
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(arqueoCajaService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void txtFechaArqueo_eventoCambiar(object sender, EventArgs e)
    {      
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {            
            if (!Page.IsPostBack)
            {
                user = (Usuario)Session["usuario"];
                Session["lstSaldos"] = null;
                mvPrincipal.ActiveViewIndex = 0;
                ImprimirGrilla();

              //  Session["FechaArqueo"] = "01/01/1900";
                //se inicializa la informacion ppal del Cierre: Caja, Cajero, Oficina, Fecha Cierre                
                Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
                Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
                oficina = oficinaServicio.ConsultarOficina(user.cod_oficina, user);
                this.txtOficina.Text = Convert.ToString(user.cod_oficina);
                this.txtNomOficina.Text = oficina.nombre;
                this.txtFechaArqueo.Text = Convert.ToString(DateTime.Now.ToShortDateString());
                this.txtCodUsuario.Text = user.codusuario.ToString();
                this.txtUsuario.Text = user.nombre;
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


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        ctlMensaje.MostrarMensaje("Desea Guardar Los Datos Del Arqueo?");
    }


    protected string obtfiltro()
    {
        string filtrar = "";
        Configuracion conf = new Configuracion();

        
        if (txtFechaArqueo.Text != "" && txtFechaArqueo.Text != "")
            filtrar += " and ARQUEOCAJA_DETALLE.cod_cajero= " + txtCodUsuario.Text + "and ARQUEOCAJA_DETALLE.fecha =To_Date('" + txtFechaArqueo.Text + " ','" + conf.ObtenerFormatoFecha() + "')";
        
        return filtrar;
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Site toolBar = (Site)this.Master;
          //  arqueoCajaService.EliminarArqueo(Convert.ToDateTime(txtFechaArqueo.Text),(Usuario)Session["usuario"]);
            string Filtro = obtfiltro();
           
            Xpinn.Caja.Entities.ArqueoCaja Arqueos = new Xpinn.Caja.Entities.ArqueoCaja();
            Arqueos.fecha_cierre = Convert.ToDateTime(txtFechaArqueo.Text);
            Arqueos.cod_caja = 0;
            Arqueos.cod_cajero = Convert.ToInt32(txtCodUsuario.Text);

            arqueoCajaService.ArqueosGuardarEnDetalle(Arqueos, gvSaldos, null, (Usuario)Session["usuario"]);
            mvPrincipal.ActiveViewIndex = 3;
             toolBar.MostrarCancelar(true);
             toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            Site toolBar = (Site)this.Master;
            string Filtro = obtfiltro();

            Xpinn.Caja.Entities.ArqueoCaja Arqueos = new Xpinn.Caja.Entities.ArqueoCaja();
            Arqueos.fecha_cierre = Convert.ToDateTime(txtFechaArqueo.Text);
            Arqueos.cod_caja = 0;
            Arqueos.cod_cajero = Convert.ToInt32(txtCodUsuario.Text);

            arqueoCajaService.ArqueosGuardarEnDetalle(Arqueos, gvSaldos, null, (Usuario)Session["usuario"]);
            mvPrincipal.ActiveViewIndex = 3;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarGuardar(false);
        }
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
            mvPrincipal.ActiveViewIndex = 0;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(false);
    }

    
    private Xpinn.Caja.Entities.MovimientoCaja ObtenerValoresSaldos()
    {
        user = (Usuario)Session["Usuario"];
        Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();
        //movCaja.cod_usuario = user.codusuario;
        movCaja.cod_usuario = Convert.ToInt64(txtCodUsuario.Text);
        movCaja.fechaCierre = Convert.ToDateTime(txtFechaArqueo.Text); 
        //if (txtFechaArqueo.TieneDatos)
        //    movCaja.fechaCierre = txtFechaArqueo.ToDateTime; 
        //Session["FechaArqueo"] = movCaja.fechaCierre;
        return movCaja;
    }

    protected void btnGenerarArqueo_Click(object sender, EventArgs e)
    {
        VerError("");
        ActualizarSaldos();
        btnReporte.Visible = true;
    }

    public void ActualizarSaldos()
    {
        try
        {
            Site toolBar = (Site)this.Master;
            Xpinn.Caja.Services.MovimientoCajaService movCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();
            movCajaServicio.EliminarTempArqueoTesoreria(ObtenerValoresSaldos(), (Usuario)Session["usuario"]);

            //aqui va el metodo para realizar la insercion     
            movCajaServicio.CrearTempArqueoTesoreria(ObtenerValoresSaldos(), (Usuario)Session["usuario"]);

            //este es el metodo que sirve para consultar los datos que hay en ese momento en la tabla de TempArqueoCaja
            lstSaldos = movCajaServicio.ListarSaldosTesoreria(ObtenerValoresSaldos(), (Usuario)Session["usuario"]);

            //este guarda los datos de la tabla temarqueocaja y los guarda en una table para el reporte
            Xpinn.Caja.Entities.MovimientoCaja refe = new Xpinn.Caja.Entities.MovimientoCaja();
           
            table.Columns.Add("moneda");
            table.Columns.Add("concepto");
            table.Columns.Add("efectivo");
            table.Columns.Add("cheque");
            table.Columns.Add("consignacion");
            table.Columns.Add("datafono");
            table.Columns.Add("total");
           
            DataRow datarw;
            if (lstSaldos.Count == 0)
            {
                datarw = table.NewRow();
                datarw[0] = " ";
                datarw[1] = " ";
                datarw[2] = " ";
                datarw[3] = " ";
                datarw[4] = " ";
                datarw[5] = " ";
                datarw[6] = " ";
              
                table.Rows.Add(datarw);
            }
            else
            {
                for (int i = 0; i < lstSaldos.Count; i++)
                {
                    datarw = table.NewRow();
                    refe = lstSaldos[i];
                    datarw[0] = " " + refe.nom_moneda;
                    datarw[1] = " " + refe.concepto;
                    datarw[2] = " " + refe.efectivo.ToString("0,0");
                    datarw[3] = " " + refe.cheque.ToString("0,0");
                    datarw[4] = " " + refe.consignacion.ToString("0,0");
                    datarw[5] = " " + refe.datafono.ToString("0,0");                  
                    datarw[6] = " " + refe.total.ToString("0,0");
                    
                    table.Rows.Add(datarw);
                }
            }
            rptArqueoPagos.LocalReport.DataSources.Clear();
            ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
            rptArqueoPagos.LocalReport.DataSources.Add(rds1);
            rptArqueoPagos.LocalReport.Refresh();

            arqueoCaja.cod_cajero = user.codusuario;
            arqueoCaja.fecha_cierre =Convert.ToDateTime(txtFechaArqueo.Text);

            gvSaldos.DataSource = lstSaldos;
            Session["lstSaldos"] = lstSaldos;
            if (lstSaldos.Count > 0)
            {
                
                gvSaldos.Visible = true;
                gvSaldos.DataBind();
                toolBar.MostrarGuardar(true);
            }
            else
            {
                toolBar.MostrarGuardar(false);
                gvSaldos.Visible = false;
            }

            Session.Add(movCajaServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(arqueoCajaService.GetType().Name + "L", "Actualizar", ex);
        }
    }


    public void ActualizarCheques()
    {
        try
        {
            Xpinn.Caja.Services.MovimientoCajaService movCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();
            List<Xpinn.Caja.Entities.MovimientoCaja> lstChequePendiente = new List<Xpinn.Caja.Entities.MovimientoCaja>();
            List<Xpinn.Caja.Entities.MovimientoCaja> lstChequeAsignado = new List<Xpinn.Caja.Entities.MovimientoCaja>();
            Xpinn.Caja.Entities.MovimientoCaja pEntidad = new Xpinn.Caja.Entities.MovimientoCaja();
            pEntidad.cod_caja = 0;
            pEntidad.cod_cajero = Convert.ToInt64(txtCodUsuario.Text);
            lstChequePendiente = movCajaServicio.ListarChequesPendientes(pEntidad, (Usuario)Session["usuario"]);
            lstChequeAsignado = movCajaServicio.ListarChequesAsignados(pEntidad, (Usuario)Session["usuario"]);
            
            gvChequePendiente.DataSource = lstChequePendiente;
            gvChequeAsignado.DataSource = lstChequeAsignado;

            if (lstChequePendiente.Count > 0)
            {
                lblInfoPendiente.Visible = false;
                lblTotPendiente.Visible = true;
                lblTotPendiente.Text = "Registros encontrados " + lstChequePendiente.Count;
                gvChequePendiente.Visible = true;
                gvChequePendiente.DataBind();
                ValidarPermisosGrilla(gvChequePendiente);
            }
            else
            {
                lblInfoPendiente.Visible = true;
                lblTotPendiente.Visible = false;
                gvChequePendiente.Visible = false;
            }
            if (lstChequeAsignado.Count > 0)
            {
                lblInfoAsignado.Visible = false;
                lblTotAsignado.Visible = true;
                lblTotAsignado.Text = "Registros encontrados " + lstChequeAsignado.Count;
                gvChequeAsignado.Visible = true;
                gvChequeAsignado.DataBind();
                ValidarPermisosGrilla(gvChequeAsignado);
            }
            else
            {
                gvChequeAsignado.Visible = false;
                lblInfoAsignado.Visible = true;
                lblTotAsignado.Visible = false;
            }

            Session.Add(movCajaServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(arqueoCajaService.GetType().Name + "L", "Actualizar", ex);
        }
    }


    protected void ImprimirGrilla()
    {
        if (Session["listSaldos2"] != null)
        {
            Session["lstSaldos"] = Session["listSaldos2"];
            List<Xpinn.Caja.Entities.MovimientoCaja> lstSaldos = new List<Xpinn.Caja.Entities.MovimientoCaja>();

            lstSaldos = (List<Xpinn.Caja.Entities.MovimientoCaja>)Session["lstSaldos"];
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
            printWindow.close();
        }";
        this.ClientScript.RegisterStartupScript(Page.GetType(), "PrintGridView", printScript.ToString(), true);

        btnImprimirArqueo.Attributes.Add("onclick", "PrintGridView();");
    }

    protected void btnVerCheques_Click(object sender, EventArgs e)
    {        
        VerError("");
        if (Convert.ToDateTime(Session["FechaArqueo"]).ToShortDateString() != "01/01/1900")
        {
            ActualizarCheques();
            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(true);
            mvPrincipal.ActiveViewIndex = 1;
        }
        else
            VerError("Se debe consultar primero el Arqueo para despues consultar el detalle de los Cheques");

    }

    protected void btnReporte_Click(object sender, EventArgs e)
    {
        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";

        Xpinn.Caja.Services.MovimientoCajaService movCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();

        //aqui va el codigo para borrar todos los datos de la tabla TempArqueoCaja
        movCajaServicio.EliminarTempArqueoTesoreria(ObtenerValoresSaldos(), (Usuario)Session["usuario"]);
        movCajaServicio.CrearTempArqueoTesoreria(ObtenerValoresSaldos(), (Usuario)Session["usuario"]);

        //este es el metodo que sirve para consultar los datos que hay en ese momento en la tabla de TempArqueoCaja
        lstSaldos = movCajaServicio.ListarSaldosTesoreria(ObtenerValoresSaldos(), (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);

        //este guarda los datos de la tabla temarqueocaja y los guarda en una table para el reporte
        Xpinn.Caja.Entities.MovimientoCaja refe = new Xpinn.Caja.Entities.MovimientoCaja();

        table.Columns.Add("moneda");
        table.Columns.Add("concepto");
        table.Columns.Add("efectivo");
        table.Columns.Add("cheque");
        table.Columns.Add("consignacion");
        table.Columns.Add("datafono");
        table.Columns.Add("total");
      
        DataRow datarw;
        if (lstSaldos.Count == 0)
        {
            datarw = table.NewRow();
            datarw[0] = " ";
            datarw[1] = " ";
            datarw[2] = " ";
            datarw[3] = " ";
            datarw[4] = " ";
            datarw[5] = " ";
            datarw[6] = " ";

            table.Rows.Add(datarw);
        }
        else
        {
            for (int i = 0; i < lstSaldos.Count; i++)
            {
                datarw = table.NewRow();
                refe = lstSaldos[i];
                datarw[0] = " " + refe.nom_moneda;
                datarw[1] = " " + refe.concepto;
                datarw[2] = " " + refe.efectivo.ToString("0,0");
                datarw[3] = " " + refe.cheque.ToString("0,0");
                datarw[4] = " " + refe.consignacion.ToString("0,0");
                datarw[5] = " " + refe.datafono.ToString("0,0");
                datarw[6] = " " + refe.total.ToString("0,0");

                table.Rows.Add(datarw);
            }
        }

        Usuario pUsu = (Usuario)Session["usuario"];

        ReportParameter[] param = new ReportParameter[6];
        param[0] = new ReportParameter("entidad", pUsu.empresa);
        param[1] = new ReportParameter("nit", pUsu.nitempresa);
        param[2] = new ReportParameter("fecha", Convert.ToDateTime(txtFechaArqueo.Text).ToShortDateString());
        param[3] = new ReportParameter("usuario", txtUsuario.Text);
        param[4] = new ReportParameter("oficina", txtOficina.Text);
        param[5] = new ReportParameter("ImagenReport", cRutaDeImagen);
        rptArqueoPagos.LocalReport.EnableExternalImages = true;
        rptArqueoPagos.LocalReport.SetParameters(param);
        
        rptArqueoPagos.LocalReport.DataSources.Clear();
        ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
        rptArqueoPagos.LocalReport.DataSources.Add(rds1);
        rptArqueoPagos.LocalReport.Refresh();
        rptArqueoPagos.Visible = true;
        frmPrint.Visible = false;
        mvPrincipal.ActiveViewIndex = 2;

    }

    protected void btnImprime_Click(object sender, EventArgs e)
    {
        if (rptArqueoPagos.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = rptArqueoPagos.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
            Usuario pUsuario = new Usuario();
            string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_" + pUsuario.nombre : "";
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output" + pNomUsuario + ".pdf"),
            FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            Session["Archivo" + Usuario.codusuario] = Server.MapPath("output" + pNomUsuario + ".pdf");
            frmPrint.Visible = true;
            rptArqueoPagos.Visible = false;

        }
    }

    protected void btnCloseReg_Click(object sender, EventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 0;
    }

    protected void btnImprimirArqueo_Click(object sender, EventArgs e)
    {

    }
}