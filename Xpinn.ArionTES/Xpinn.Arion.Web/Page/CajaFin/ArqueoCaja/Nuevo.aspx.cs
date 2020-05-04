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

    List<Xpinn.Caja.Entities.MovimientoCaja> lstSaldos = new List<Xpinn.Caja.Entities.MovimientoCaja>();
    Usuario user = new Usuario();
    System.Data.DataTable table = new System.Data.DataTable();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(arqueoCajaService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
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
            if (!Page.IsPostBack)
            {
                ReportViewer1.Visible = false;
                Session["listSaldos2"] = null;
                Session["lstSaldos"] = null;
                gvSaldos.DataSource = null;
                gvSaldos.DataBind();
                btnReporte.Visible = false;
                user = (Usuario)Session["usuario"];
                cajero = cajeroService.ConsultarIfUserIsntCajeroPrincipal(user.codusuario, (Usuario)Session["usuario"]);
                Session["estadoCaj"] = cajero.estado;//estado Cajero
                Session["estadoOfi"] = cajero.estado_ofi;// estado Oficina
                Session["estadoCaja"] = cajero.estado_caja;// estado Caja

                horario = HorarioService.VerificarHorarioOficina(user.cod_oficina, (Usuario)Session["usuario"]);
                Session["conteoOfiHorario"] = horario.conteo;

                horario = HorarioService.getHorarioOficina(user.cod_oficina, (Usuario)Session["usuario"]);

                Session["Resp1"] = 0;
                Session["Resp2"] = 0;

                //si la hora actual es mayor que de la hora inicial
                if (TimeSpan.Compare(horario.fecha_hoy.TimeOfDay, horario.hora_inicial.TimeOfDay) > 0)
                    Session["Resp1"] = 1;
                //si la hora actual es menor que la hora final
                if (TimeSpan.Compare(horario.fecha_hoy.TimeOfDay, horario.hora_final.TimeOfDay) < 0)
                    Session["Resp2"] = 1;

                if (long.Parse(Session["estadoOfi"].ToString()) == 2)
                    VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
                else
                {
                    if (long.Parse(Session["estadoCaja"].ToString()) == 0)
                        VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
                    else
                    {
                        if (long.Parse(Session["conteoOfiHorario"].ToString()) == 0)
                            VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
                        else
                        {
                            if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                            {
                                if (long.Parse(Session["estadoCaj"].ToString()) == 0)
                                    VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                            }
                            else
                                VerError("La Oficina se encuentra por fuera del horario configurado");
                        }

                    }
                }
                ImprimirGrilla();
         
                Session["FechaArqueo"] = "01/01/1900";

                //se inicializa la informacion ppal del Cierre: Caja, Cajero, Oficina, Fecha Cierre
                ObtenerDatos();
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


    protected void ObtenerDatos()
    {
        try
        {
            arqueoCaja = arqueoCajaService.ConsultarCajero((Usuario)Session["usuario"]);//se consulta la informacion del cajero que se encuentra conectado

            if (!string.IsNullOrEmpty(arqueoCaja.nom_oficina.ToString()))
                txtOficina.Text = arqueoCaja.nom_oficina.ToString();
            if (!string.IsNullOrEmpty(arqueoCaja.nom_caja.ToString()))
                txtCaja.Text = arqueoCaja.nom_caja.ToString();
            if (!string.IsNullOrEmpty(arqueoCaja.nom_cajero.ToString()))
                txtCajero.Text = arqueoCaja.nom_cajero.ToString().Trim();
            if (!string.IsNullOrEmpty(arqueoCaja.fecha_cierre.ToString()))
                txtFechaArqueo.Text = arqueoCaja.fecha_cierre.ToShortDateString().Trim();
            if (!string.IsNullOrEmpty(arqueoCaja.cod_oficina.ToString()))
                Session["Oficina"] = arqueoCaja.cod_oficina.ToString().Trim();
            if (!string.IsNullOrEmpty(arqueoCaja.cod_caja.ToString()))
                Session["Caja"] = arqueoCaja.cod_caja.ToString().Trim();
            if (!string.IsNullOrEmpty(arqueoCaja.cod_cajero.ToString()))
                Session["Cajero"] = arqueoCaja.cod_cajero.ToString().Trim();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(arqueoCajaService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }


    private Xpinn.Caja.Entities.MovimientoCaja ObtenerValoresSaldos(Xpinn.Caja.Entities.ArqueoCaja pArqueo)
    {
        Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();

        movCaja.cod_caja = long.Parse(Session["Caja"].ToString());
        movCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
        movCaja.fechaCierre = pArqueo.fecha_cierre;
        Session["FechaArqueo"] = movCaja.fechaCierre;
        return movCaja;
    }

    protected void btnGenerarArqueo_Click(object sender, EventArgs e)
    {

        if (long.Parse(Session["estadoOfi"].ToString()) == 1)
        {
            if (long.Parse(Session["estadoCaja"].ToString()) == 1)
            {
                if (long.Parse(Session["conteoOfiHorario"].ToString()) == 1)
                {
                    if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                    {
                        if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                        {
                            ActualizarSaldos();
                        }
                        else
                            VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                    }
                    else
                        VerError("La Oficina se encuentra por fuera del horario configurado");
                }
                else
                    VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
            }
            else
                VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
        }
        else
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");

        btnReporte.Visible = true;
    }

    public void ActualizarSaldos()
    {
        try
        {
            arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
            arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaArqueo.Text);
            arqueoCaja = arqueoCajaService.ConsultarUltFechaArqueoCaja(arqueoCaja, (Usuario)Session["usuario"]);

            //aqui va el codigo para borrar todos los datos de la tabla TempArqueoCaja
            arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
            arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaArqueo.Text);
            movCajaServicio.EliminarTempArqueoCaja(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);

            //aqui va el metodo para realizar la insercion
            arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
            arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaArqueo.Text);            
            movCajaServicio.CrearTempArqueoCaja(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);

            //este es el metodo que sirve para consultar los datos que hay en ese momento en la tabla de TempArqueoCaja
            arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
            arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaArqueo.Text);            
            lstSaldos = movCajaServicio.ListarSaldos(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);

            //este guarda los datos de la tabla temarqueocaja y los guarda en una table para el reporte
            Xpinn.Caja.Entities.MovimientoCaja refe = new Xpinn.Caja.Entities.MovimientoCaja();
           
            table.Columns.Add("moneda");
            table.Columns.Add("concepto");
            table.Columns.Add("efectivo");         
            table.Columns.Add("cheque");
            table.Columns.Add("total");
            table.Columns.Add("consignacion");
            table.Columns.Add("datafono");



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
                    datarw[4] = " " + refe.total.ToString("0,0");
                    datarw[5] = " " + refe.consignacion.ToString("0,0");
                    datarw[6] = " " + refe.datafono.ToString("0,0");

                    table.Rows.Add(datarw);
                }
            }
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
            ReportViewer1.LocalReport.DataSources.Add(rds1);
            ReportViewer1.LocalReport.Refresh();

            arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
            arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaArqueo.Text);

            //aqui va el codigo para borrar todos los datos de la tabla TempArqueoCaja
            movCajaServicio.EliminarTempArqueoCaja(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);

            gvSaldos.DataSource = lstSaldos;

            Session["lstSaldos"] = lstSaldos;

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

            Session.Add(movCajaServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void btnVerMovimientos_Click(object sender, EventArgs e)
    {
        Session["FechaArqueo"] = txtFechaArqueo.Text;
        Session["listSaldos"] = Session["lstSaldos"];

        if (long.Parse(Session["estadoOfi"].ToString()) == 1)
        {
            if (long.Parse(Session["estadoCaja"].ToString()) == 1)
            {
                if (long.Parse(Session["conteoOfiHorario"].ToString()) == 1)
                {
                    if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                    {
                        if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                        {
                            if (Convert.ToDateTime(Session["FechaArqueo"]).ToShortDateString() != "01/01/1900")
                            {
                                Session["OficinaNom"] = txtOficina.Text;
                                Session["OficinaId"] = Session["Oficina"];
                                Session["CajaNom"] = txtCaja.Text;
                                Session["CajaId"] = Session["Caja"];
                                Session["CajeroNom"] = txtCajero.Text;
                                Session["CajeroId"] = Session["Cajero"];
                                Session["StateMov"] = 2;//Arqueo de Caja
                                Navegar("../../CajaFin/MovimientoCaja/Lista.aspx");
                            }
                            else
                                VerError("Se debe consultar primero el Arqueo para despues consultar el detalle de los movimientos");
                        }
                        else
                            VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                    }
                    else
                        VerError("La Oficina se encuentra por fuera del horario configurado");
                }
                else
                    VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
            }
            else
                VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
        }
        else
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
    }

    protected void ImprimirGrilla()
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
                                printWindow.close();
                            }";
                            this.ClientScript.RegisterStartupScript(Page.GetType(), "PrintGridView", printScript.ToString(), true);

                            btnImprimirArqueo.Attributes.Add("onclick", "PrintGridView();");

        // Validar datos nulos
        if (Session["estadoOfi"] == null)
            return;
        // Validar estado de caja

        if (long.Parse(Session["estadoOfi"].ToString()) == 1)
        {
            if (long.Parse(Session["estadoCaja"].ToString()) == 1)
            {
                if (long.Parse(Session["conteoOfiHorario"].ToString()) == 1)
                {
                    if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                    {
                        if (long.Parse(Session["estadoCaj"].ToString()) == 1)
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
                        }
                        else
                            VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                    }
                    else
                        VerError("La Oficina se encuentra por fuera del horario configurado");
                }
                else
                    VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
            }
            else
                VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
        }
        else
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");

    }

    protected void btnVerCheques_Click(object sender, EventArgs e)
    {
        Session["FechaArqueo"] = txtFechaArqueo.Text;
        Session["listSaldos"] = Session["lstSaldos"];

        if (long.Parse(Session["estadoOfi"].ToString()) == 1)
        {
            if (long.Parse(Session["estadoCaja"].ToString()) == 1)
            {
                if (long.Parse(Session["conteoOfiHorario"].ToString()) == 1)
                {
                    if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                    {

                        if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                        {
                            if (Convert.ToDateTime(Session["FechaArqueo"]).ToShortDateString() != "01/01/1900")
                            {
                                Session["OficinaNom"] = txtOficina.Text;
                                Session["OficinaId"] = Session["Oficina"];
                                Session["CajaNom"] = txtCaja.Text;
                                Session["CajaId"] = Session["Caja"];
                                Session["CajeroNom"] = txtCajero.Text;
                                Session["CajeroId"] = Session["Cajero"];
                                Navegar("../../CajaFin/ChequeCaja/Lista.aspx");
                            }
                            else
                                VerError("Se debe consultar primero el Arqueo para despues consultar el detalle de los Cheques");
                        }
                        else
                            VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                    }
                    else
                        VerError("La Oficina se encuentra por fuera del horario configurado");
                }
                else
                    VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
            }
            else
                VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
        }
        else
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
    }

    protected void btnReporte_Click(object sender, EventArgs e)
    {
        arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
        arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
        arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaArqueo.Text);
        arqueoCaja = arqueoCajaService.ConsultarUltFechaArqueoCaja(arqueoCaja, (Usuario)Session["usuario"]);
        arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
        arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
        arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaArqueo.Text);

        //aqui va el codigo para borrar todos los datos de la tabla TempArqueoCaja
        movCajaServicio.EliminarTempArqueoCaja(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);

        arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
        arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
        arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaArqueo.Text);

        //aqui va el metodo para realizar la insercion
        movCajaServicio.CrearTempArqueoCaja(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);

        arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
        arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
        arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaArqueo.Text);

        //este es el metodo que sirve para consultar los datos que hay en ese momento en la tabla de TempArqueoCaja
        lstSaldos = movCajaServicio.ListarSaldos(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);

        //este guarda los datos de la tabla temarqueocaja y los guarda en una table para el reporte
        Xpinn.Caja.Entities.MovimientoCaja refe = new Xpinn.Caja.Entities.MovimientoCaja();

        table.Columns.Add("moneda");
        table.Columns.Add("concepto");
        table.Columns.Add("efectivo");
      
        table.Columns.Add("cheque");
        table.Columns.Add("total");
        table.Columns.Add("consignacion");
        table.Columns.Add("datafono");
        DataRow datarw;
        if (lstSaldos.Count == 0)
        {
            datarw = table.NewRow();
            datarw[0] = " ";
            datarw[1] = " ";
            datarw[2] = " ";
            datarw[3] = " ";
            datarw[4] = " ";

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
                datarw[4] = " " + refe.total.ToString("0,0");

                datarw[5] = " " + refe.consignacion.ToString("0,0");
                datarw[6] = " " + refe.datafono.ToString("0,0");
                table.Rows.Add(datarw);
            }
        }

        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";

        ReportParameter[] param = new ReportParameter[5];
        param[0] = new ReportParameter("fecha", Convert.ToDateTime(txtFechaArqueo.Text).ToString());
        param[1] = new ReportParameter("cajero", txtCajero.Text);
        param[2] = new ReportParameter("caja", txtCaja.Text);
        param[3] = new ReportParameter("ImagenReport", cRutaDeImagen);
        param[4] = new ReportParameter("fecha_impresion", DateTime.Now.ToString());

        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.LocalReport.SetParameters(param);               
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportDataSource rds1 = new ReportDataSource("DataSet2", table);
        ReportViewer1.LocalReport.DataSources.Add(rds1);
        ReportViewer1.LocalReport.Refresh();
        ReportViewer1.Visible = true;
        mpeNuevo.Show();

    }

    protected void btnImprimirArqueo_Click(object sender, EventArgs e)
    {
        btnReporte_Click(btnReporte, null);
   }
}