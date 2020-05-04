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
    Xpinn.Caja.Entities.ArqueoCaja arqueodetalle = new Xpinn.Caja.Entities.ArqueoCaja();

    Xpinn.Comun.Services.GeneralService generalService = new Xpinn.Comun.Services.GeneralService();
    Xpinn.Contabilidad.Services.ComprobanteService comprobanteService = new Xpinn.Contabilidad.Services.ComprobanteService();

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
    int tipoOpe = 34;


    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(arqueoCajaService.CodigoPrograma2, "A");
            Site toolBar = (Site)this.Master;
            btnGuardar.Visible = false;
            // Eventos para el control de generar el comprobante
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;           
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
            user = (Usuario)Session["usuario"];
            MultiView1.SetActiveView(View2);
            mvReporte.ActiveViewIndex = 0;

            cajero = cajeroService.ConsultarIfUserIsntCajeroPrincipal(user.codusuario, (Usuario)Session["usuario"]);
            Session["estadoCaj"] = cajero.estado;       //estado Cajero
            Session["estadoOfi"] = cajero.estado_ofi;   // estado Oficina
            Session["estadoCaja"] = cajero.estado_caja; // estado Caja

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

            if (!IsPostBack)
            {
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

    private bool ValidarProcesoCaja()
    {
        if (long.Parse(Session["estadoOfi"].ToString()) != 1)
        {
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
            return false;
        }
        if (long.Parse(Session["estadoCaja"].ToString()) != 1)
        {
            VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
            return false;
        }
        if (long.Parse(Session["conteoOfiHorario"].ToString()) != 1)
        {
            VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
            return false;
        }
        if (long.Parse(Session["Resp1"].ToString()) != 1 && long.Parse(Session["Resp2"].ToString()) != 1)
        {
            VerError("La Oficina se encuentra por fuera del horario configurado");
            return false;
        }
        if (long.Parse(Session["estadoCaj"].ToString()) != 1)
        {
            VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
            return false;
        }
        if (gvSaldos.Rows.Count > 0)
        {
            Xpinn.Caja.Entities.ArqueoCaja arqueoCaja = DatosArqueo();
            string Error = "";
            Boolean bValidar = false;
            bValidar = arqueoCajaService.ValidarArqueo(arqueoCaja, (Usuario)Session["usuario"], ref Error);
            if (bValidar == false)
            {
                VerError(Error);
                return false;
            }
        }
        else
        {
            VerError("Para Realizar el Cierre se hace Necesario que se haga la Generacion de Arqueo");
            return false;
        }
        Xpinn.Caja.Entities.Traslado pTraslado = new Xpinn.Caja.Entities.Traslado();
        Xpinn.Caja.Entities.Recepcion pRecepcion = new Xpinn.Caja.Entities.Recepcion();
        Xpinn.Caja.Services.RecepcionService recepcionServicio = new Xpinn.Caja.Services.RecepcionService();

        pRecepcion.fecha_recepcion = Convert.ToDateTime(txtFechaCierreCaja.Text);

        if (long.Parse(Session["CajaPpal"].ToString()) == long.Parse(Session["Caja"].ToString()) && long.Parse(Session["CajeroPpal"].ToString()) == long.Parse(Session["Cajero"].ToString()))
        {
            pRecepcion.cod_recepcion = 1;
            pRecepcion.cod_caja = Convert.ToInt64(Session["CajaPpal"]);
            pRecepcion.cod_cajero = Convert.ToInt64(Session["CajeroPpal"]);
        }
        else
        {
            pRecepcion.cod_recepcion = 2;
            pRecepcion.cod_caja = Convert.ToInt64(Session["Caja"]);
            pRecepcion.cod_cajero = Convert.ToInt64(Session["Cajero"]);
        }
        pTraslado = recepcionServicio.ConsultarTraslado(pRecepcion, (Usuario)Session["usuario"]);
        if(pTraslado.cod_traslado > 0 && pRecepcion.cod_recepcion == 1)
        {
            VerError("La caja principal tiene traslados pendientes por recibir");
            return false;
        }
        else if (pTraslado.cod_traslado > 0 && pRecepcion.cod_recepcion == 2)
        {
            VerError("La caja auxiliar tiene traslados que no han sido recibidos");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        // Validar proceso de caja
        /*if (long.Parse(Session["estadoOfi"].ToString()) == 1)
        {
            if (long.Parse(Session["estadoCaja"].ToString()) == 1)
            {
                if (long.Parse(Session["conteoOfiHorario"].ToString()) == 1)
                {
                    if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                    {
                        if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                        {
                            //se valida de que la grilla de Saldos(Arqueo) este populada -- no puede estar vacia
                            if (gvSaldos.Rows.Count > 0)
                            {
                                Xpinn.Caja.Entities.ArqueoCaja arqueoCaja = DatosArqueo();                                
                                string Error = "";
                                Boolean bValidar = false;
                                bValidar = arqueoCajaService.ValidarArqueo(arqueoCaja, (Usuario)Session["usuario"], ref Error);
                                if (bValidar == false)
                                {
                                    VerError(Error);
                                    return;
                                }
                            }
                            else
                            {
                                VerError("Para Realizar el Cierre se hace Necesario que se haga la Generacion de Arqueo");
                                return;
                            }
                        }
                        else
                        { 
                            VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                            return;
                        }
                    }
                    else
                    { 
                        VerError("La Oficina se encuentra por fuera del horario configurado");
                        return;
                    }
                }
                else
                { 
                    VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
                    return;
                }
            }
            else
            { 
                VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
                return;
            }
        }
        else
        { 
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
            return;
        }*/
        if (ValidarProcesoCaja())
        {
            // Determinar código de proceso contable para generar el comprobante
            if (DebeGenerarComprobante())
            {
                // Validar que exista la parametrización contable por procesos
                if (ValidarProcesoContable(Convert.ToDateTime(txtFechaCierreCaja.Text), tipoOpe) == false)
                {
                    VerError("No se encontró parametrización contable por procesos para el tipo de operación " + tipoOpe + " = Comprobante de Caja");
                    return;
                }
                // Si genera comprobante debe mostrar para seleccionar el proceso contable
                Int64? rpta = 0;
                if (!panelProceso.Visible && panelGeneral.Visible)
                {
                    rpta = ctlproceso.Inicializar(tipoOpe, Convert.ToDateTime(txtFechaCierreCaja.Text), (Usuario)Session["Usuario"]);
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
                        if (AplicarDatos())
                            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                        else
                            VerError("Se presentó error");
                    }
                }
            }
            else
            {
                // Si no genera comprobante graba los datos y muestra el reporte
                AplicarDatos();
            }
        }
    }
    
    protected void comprobante()
    {

        ReportParameter[] param = new ReportParameter[9];
        param[0] = new ReportParameter("total"," "+ Txttotal.Text);
        param[1] = new ReportParameter("efectivo", " " + Txtefecitivo.Text);
        param[2] = new ReportParameter("caja", " " + txtCaja.Text);
        param[3] = new ReportParameter("cajero", " " + txtCajero.Text);
        param[4] = new ReportParameter("oficina", " " + txtOficina.Text);
        param[5] = new ReportParameter("fecha", " " + txtFechaCierreCaja.Text);
        param[6] = new ReportParameter("cheque", " " + Txtcheque.Text);
        param[7] = new ReportParameter("tipo", " " + Txtmoneda.Text);
        param[8] = new ReportParameter("ImagenReport", ImagenReporte());
        mvReporte.Visible = true;
        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.LocalReport.SetParameters(param);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.Refresh();
        ReportViewer1.Visible = true;

        mvReporte.ActiveViewIndex = 0;
        mvReporte.Visible = true;

    }

    protected void ObtenerDatos()
    {
        //try
        //{
            arqueoCaja = arqueoCajaService.ConsultarCajero((Usuario)Session["usuario"]);//se consulta la informacion del cajero que se encuentra conectado

            if (!string.IsNullOrEmpty(arqueoCaja.nom_oficina.ToString()))
                txtOficina.Text = arqueoCaja.nom_oficina.ToString();
            if (!string.IsNullOrEmpty(arqueoCaja.nom_caja.ToString()))
                txtCaja.Text = arqueoCaja.nom_caja.ToString();
            if (!string.IsNullOrEmpty(arqueoCaja.nom_cajero.ToString()))
                txtCajero.Text = arqueoCaja.nom_cajero.ToString().Trim();
            if (!string.IsNullOrEmpty(arqueoCaja.fecha_cierre.ToString()))
                txtFechaCierreCaja.Text = arqueoCaja.fecha_cierre.ToShortDateString().Trim();
            if (!string.IsNullOrEmpty(arqueoCaja.tipo_horario.ToString()))
                txtHorario.Text = arqueoCaja.tipo_horario == 1 ? "Normal" : "Adicional";

            if (!string.IsNullOrEmpty(arqueoCaja.cod_oficina.ToString()))
                Session["Oficina"] = arqueoCaja.cod_oficina.ToString().Trim();
            if (!string.IsNullOrEmpty(arqueoCaja.cod_caja.ToString()))
                Session["Caja"] = arqueoCaja.cod_caja.ToString().Trim();// Caja origen
            if (!string.IsNullOrEmpty(arqueoCaja.cod_cajero.ToString()))// Cajero Origen
                Session["Cajero"] = arqueoCaja.cod_cajero.ToString().Trim();

            cajero = cajeroService.ConsultarCajeroPrincipalAsignadoAlCajero(int.Parse(Session["Oficina"].ToString()),int.Parse(Session["Cajero"].ToString()), (Usuario)Session["usuario"]);

            if (cajero.nom_cajero_ppal != null)
                if (!string.IsNullOrEmpty(cajero.nom_cajero_ppal.ToString()))
                    txtCajeroPricipal.Text = cajero.nom_cajero_ppal;
            if (cajero.cod_cajero_ppal != null)
                if (!string.IsNullOrEmpty(cajero.cod_cajero_ppal.ToString()))
                    Session["CajeroPpal"] = cajero.cod_cajero_ppal;// Cajero Destino
            if (!string.IsNullOrEmpty(cajero.cod_caja_ppal.ToString()))
                Session["CajaPpal"] = cajero.cod_caja_ppal;//Cajero Destino

        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(arqueoCajaService.GetType().Name + "A", "ObtenerDatos", ex);
        //}
    }

    public void ActualizarCheques()
    {
        try
        {
            lstCheques = movCajaServicio.ListarChequesPendientes(ObtenerValoresCheques(), (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);

            gvChquesXEntregar.DataSource = lstCheques;

            if (lstCheques.Count > 0)
            {
                gvChquesXEntregar.Visible = true;
                gvChquesXEntregar.DataBind();
                ValidarPermisosGrilla(gvChquesXEntregar);
            }
            else
            {
                gvChquesXEntregar.Visible = false;
            }

            Session.Add(movCajaServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Xpinn.Caja.Entities.MovimientoCaja ObtenerValoresCheques()
    {
        Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();

        movCaja.cod_caja = long.Parse(Session["Caja"].ToString());
        movCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
        movCaja.cod_tipo_pago = 2;
        movCaja.cod_moneda = 1;

        return movCaja;
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
                                ActualizarCheques();
                                btnGuardar.Visible = true;
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

    public void ActualizarSaldos()
    {
        try
        {
            arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
            arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaCierreCaja.Text);
            arqueoCaja = arqueoCajaService.ConsultarUltFechaArqueoCaja(arqueoCaja, (Usuario)Session["usuario"]);

            arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
            arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaCierreCaja.Text);
            //aqui va el codigo para borrar todos los datos de la tabla TempArqueoCaja
            movCajaServicio.EliminarTempArqueoCaja(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);

            arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
            arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaCierreCaja.Text);

            //aqui va el metodo para realizar la insercion
            movCajaServicio.CrearTempArqueoCaja(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);

            arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
            arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaCierreCaja.Text);

            //este es el metodo que sirve para consultar los datos que hay en ese momento en la tabla de TempArqueoCaja
            lstSaldos = movCajaServicio.ListarSaldos(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);
            lstcomprobante = movCajaServicio.Listarcomprobante(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);
            arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
            arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaCierreCaja.Text);
          
            //aqui va el codigo para borrar todos los datos de la tabla TempArqueoCaja
            movCajaServicio.EliminarTempArqueoCaja(ObtenerValoresSaldos(arqueoCaja), (Usuario)Session["usuario"]);
            gvSaldos.DataSource = lstSaldos;

            Session["lstSaldos"] = lstSaldos;
            Xpinn.Caja.Entities.MovimientoCaja varibel = new Xpinn.Caja.Entities.MovimientoCaja();

            for (int i = 0; i < lstcomprobante.Count; i++)
            {
                varibel = lstcomprobante[i];
                if (varibel.total == 0)
                {
                    Txttotal.Text = "0.00";
                }
                else
                {
                    Txttotal.Text = Convert.ToString(varibel.total);
                }
                if (Convert.ToString(varibel.concepto) == null)
                {
                    Txtconcepto.Text = "-";
                }
                else
                {
                    Txtconcepto.Text = Convert.ToString(varibel.concepto);
                }
                if (varibel.efectivo == 0)
                {
                    Txtefecitivo.Text = "0.00";
                }
                else
                {
                    Txtefecitivo.Text = Convert.ToString(varibel.efectivo);
                }
                if (varibel.cheque == 0)
                {
                    Txtcheque.Text = "0.00";
                }
                else
                {
                    Txtcheque.Text = Convert.ToString(varibel.cheque);
                }
                if (Convert.ToString(varibel.nom_moneda) == null)
                {
                    Txtmoneda.Text = "Pesos";
                }
                else
                {
                    Txtmoneda.Text = Convert.ToString(varibel.nom_moneda);
                }
            }

        

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
        Session["FechaArqueo"] = txtFechaCierreCaja.Text;
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
                                    Session["StateMov"] = 1;// Cierre de Caja
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
        // Validar datos nulos
        if (Session["estadoOfi"] == null)
            return;
        // Validar estado de caja
        if (long.Parse(Session["estadoOfi"].ToString()) == 1)
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

                        string printScript =
                        @"function PrintGridView()
                             {
     
                                div = document.getElementById('DivButtons');
                                div.style.display='none';
                                divP = document.getElementById('DivButtonsP');
                                divP.style.display='none';

                                var gridInsideDiv = document.getElementById('gvDiv');
                                var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=150,height=300,toolbar=1,scrollbars=1,status=1');
                                printWindow.document.write(gridInsideDiv.innerHTML);
                                printWindow.document.close();
                                printWindow.focus();
                                printWindow.print();
                                printWindow.close();}";

                        this.ClientScript.RegisterStartupScript(Page.GetType(), "PrintGridView", printScript.ToString(), true);

                        btnImprimirArqueo.Attributes.Add("onclick", "PrintGridView();");
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
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");

    }

    protected void btnImprimircomprobantes_Click(object sender, EventArgs e)
    {

    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar("~/General/Global/inicio.aspx");
    }

    protected void btnImprimirArqueo_Click(object sender, EventArgs e)
     {
         comprobante();
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
            AplicarDatos();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected bool AplicarDatos()
    {       
        Xpinn.Caja.Entities.ArqueoCaja arqueoCaja = DatosArqueo();        

        // Grabar datos del arqueo
        arqueoCaja = arqueoCajaService.CrearArqueoCaja(arqueoCaja, gvSaldos, gvChquesXEntregar, (Usuario)Session["usuario"]);
        if (arqueoCaja != null)
        {
            // Grabar el detalle del arqueo
            arqueodetalle = arqueoCajaService.ArqueoCajadetalle(arqueoCaja, gvSaldos, gvChquesXEntregar, (Usuario)Session["usuario"]);

            // Determinar parámetro general 18 si el valor es 1 entonces genera comprobante y no imprime en caso contrario imprime el reporte solamente
            if (DebeGenerarComprobante())
            {
                Int64 codproceso = Convert.ToInt64(ctlproceso.cod_proceso);
                Int64 numcomp = 0;
                Int64 tipocomp = 0;
                string error = "";

                if (comprobanteService.GenerarComprobanteCaja(0, 120, Convert.ToDateTime(txtFechaCierreCaja.Text), long.Parse(Session["Oficina"].ToString()), long.Parse(Session["Caja"].ToString()), 0, codproceso, ref numcomp, ref tipocomp, ref error, (Usuario)Session["usuario"]))
                {
                    // Asignar las variables para mostrar en el comprobante
                    Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new
                    Xpinn.Contabilidad.Services.ComprobanteService();
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = 0;
                    Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = tipoOpe;
                    Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(txtFechaCierreCaja.Text);
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = 0;
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = Session["Oficina"];
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_caja"] = Session["Caja"];
                    Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = numcomp;
                    Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = tipocomp;
                    Session[ComprobanteServicio.CodigoPrograma + ".modificar"] = "1";
                }
                else
                {
                    VerError(error);
                    return false;
                }

            }
            else
            {
                comprobante();
                btnGuardar.Visible = false;
                btnRegresar.Visible = true;
                btnCancelar.Visible = false;
            }
        }
        else
        {
            return false;
        }

        return true;
    }

    protected bool DebeGenerarComprobante()
    {
        Xpinn.Comun.Entities.General general = new Xpinn.Comun.Entities.General();
        general = generalService.ConsultarGeneral(18, (Usuario)Session["usuario"]);
        if (general.valor == "1")
            return true;
        return false;
    }

    protected Xpinn.Caja.Entities.ArqueoCaja DatosArqueo()
    {
        Xpinn.Caja.Entities.ArqueoCaja arqueoCaja = new Xpinn.Caja.Entities.ArqueoCaja();
        arqueoCaja = arqueoCajaService.Consultarparametrotraslados((Usuario)Session["Usuario"]);
        Int64 arqueoCajas = arqueoCaja.codigo_parametro;
        arqueoCaja.cod_oficina = long.Parse(Session["Oficina"].ToString());
        arqueoCaja.cod_caja = long.Parse(Session["Caja"].ToString());
        arqueoCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
        arqueoCaja.cod_caja_ppal = long.Parse(Session["CajaPpal"].ToString());
        arqueoCaja.cod_cajero_ppal = long.Parse(Session["CajeroPpal"].ToString());
        arqueoCaja.fecha_cierre = Convert.ToDateTime(txtFechaCierreCaja.Text);
        arqueoCaja.tipo_movimiento = "EGRESO";        
        arqueoCaja.tipo_ope = tipoOpe;

        return arqueoCaja;
    }


}