using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using System.Text;
using System.IO;

public partial class Lista : GlobalWeb
{
    Xpinn.Caja.Services.MovimientoCajaService movCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();
    Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();

    Xpinn.Caja.Services.TransaccionCajaService tranCajaServicio = new Xpinn.Caja.Services.TransaccionCajaService();
    Xpinn.Caja.Services.ReintegroService reintegroService = new Xpinn.Caja.Services.ReintegroService();
    Xpinn.Caja.Entities.Reintegro reintegro = new Xpinn.Caja.Entities.Reintegro();

    Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();

    Usuario user = new Usuario();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(movCajaServicio.CodigoPrograma5, "L");

            Site toolBar = (Site)this.Master;
            // toolBar.eventoRegresar += btnRegresar_Click;
            //toolBar.eventoGuardar += btnGuardar_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //ImprimirGrilla();
            if (!IsPostBack)
            {
                cajero = cajeroService.ConsultarIfUserIsntCajeroPrincipal(user.codusuario, (Usuario)Session["usuario"]);

                if (gControlarCompCaja == "1")
                {
                    Session["estadoCaj"] = cajero.estado;
                    Session["estadoOfi"] = cajero.estado_ofi; 
                }
                else
                {
                    Session["estadoCaj"] = "1";
                    Session["estadoOfi"] = "1";
                }

                if (long.Parse(Session["estadoOfi"].ToString()) == 2)
                    VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
                else
                {
                    if (long.Parse(Session["estadoCaj"].ToString()) == 0 && gControlarCompCaja == "1")
                        VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                }

                ObtenerDatos();
                Actualizar();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }


    public void Actualizar()
    {
        try
        {
            List<Xpinn.Caja.Entities.TransaccionCaja> lstConsulta = new List<Xpinn.Caja.Entities.TransaccionCaja>();

            lstConsulta = tranCajaServicio.ListarTransaccionesComprobante(ObtenerValores(), (Usuario)Session["usuario"]);

            gvMovimiento.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvMovimiento.Visible = true;
                gvMovimiento.DataBind();
                btnGuardar.Visible = true;
            }
            else
            {
                btnGuardar.Visible = false;
                gvMovimiento.Visible = false;                
            }

            Session["NumRegistros"] = lstConsulta.Count.ToString();

            List<Xpinn.Caja.Entities.TransaccionCaja> lstConsultaTot = new List<Xpinn.Caja.Entities.TransaccionCaja>();
            lstConsultaTot = tranCajaServicio.ListarTransaccionesComprobanteTot(ObtenerValores(), (Usuario)Session["usuario"]);
            gvTotales.DataSource = lstConsultaTot;
            gvTotales.DataBind();

            Session.Add(movCajaServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Xpinn.Caja.Entities.TransaccionCaja ObtenerValores()
    {
        Xpinn.Caja.Entities.TransaccionCaja transaccion = new Xpinn.Caja.Entities.TransaccionCaja();

        transaccion.fecha_cierre = Convert.ToDateTime(Session["FechaArqueo"].ToString());
        transaccion.cod_oficina = long.Parse(Session["Oficina"].ToString());
        return transaccion;
    }

    protected void ObtenerDatos()
    {
        try
        {
            if (gControlarCompCaja == "1")
                reintegro = reintegroService.ConsultarCajero((Usuario)Session["usuario"]);
            else
                reintegro = cajeroService.ConsultarFecha((Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(reintegro.fechareintegro.ToString()))
                txtFecha.Text = reintegro.fechareintegro.ToShortDateString();
            if (!string.IsNullOrEmpty(reintegro.nomoficina.ToString()))
                txtOficina.Text = reintegro.nomoficina.ToString();

            if (!string.IsNullOrEmpty(reintegro.cod_oficina.ToString()))
                Session["Oficina"] = reintegro.cod_oficina.ToString().Trim();
            if (!string.IsNullOrEmpty(reintegro.fechareintegro.ToString()))
                Session["FechaArqueo"] = reintegro.fechareintegro.ToString().Trim();
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reintegroService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    } 

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (long.Parse(Session["estadoOfi"].ToString()) == 1)
        {
            if (long.Parse(Session["estadoCaj"].ToString()) == 1)
            {
                Session["FechaArqueo"] = Convert.ToDateTime(txtFecha.Text);
                Actualizar();
            }
            else
                VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
        }
        else
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
    }

    private bool ValidarTraslados()
    {
        Xpinn.Caja.Entities.Traslado pTraslado = new Xpinn.Caja.Entities.Traslado();
        Xpinn.Caja.Entities.Recepcion pRecepcion = new Xpinn.Caja.Entities.Recepcion();
        Xpinn.Caja.Services.RecepcionService recepcionServicio = new Xpinn.Caja.Services.RecepcionService();

        pRecepcion.fecha_recepcion = Convert.ToDateTime(txtFecha.Text);
        cajero = cajeroService.ConsultarIfUserIsntCajeroPrincipal(user.codusuario, (Usuario)Session["usuario"]);

        pRecepcion.cod_recepcion = 1;
        pRecepcion.cod_caja = cajero.cod_caja;
        pRecepcion.cod_cajero = Convert.ToInt64(cajero.cod_cajero);
        pTraslado = recepcionServicio.ConsultarTraslado(pRecepcion, (Usuario)Session["usuario"]);
        if (pTraslado.cod_traslado > 0 && pRecepcion.cod_recepcion == 1)
        {
            VerError("La caja principal tiene traslados pendientes por recibir");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Método para generar el comprobante de caja
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Session["FechaComprobante"] = txtFecha.Text;

        if (long.Parse(Session["estadoOfi"].ToString()) == 1)
        {
            if (long.Parse(Session["estadoCaj"].ToString()) == 1)
            {
                try
                {
                    Int64 NumReg = 0;
                    if (Session["NumRegistros"] != null)
                        NumReg = Convert.ToInt64(Session["NumRegistros"].ToString());
                    if (NumReg > 0)
                    {
                        //Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = 0;
                        //Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 120;
                        //Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(txtFecha.Text);
                        //Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = -1;
                        //Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = Session["Oficina"];
                        //Session[ComprobanteServicio.CodigoPrograma + ".cod_caja"] = "";

                        // Validar que exista la parametrización contable por procesos
                        if (ValidarProcesoContable(Convert.ToDateTime(txtFecha.Text), 120) == false)
                        {
                            VerError("No se encontró parametrización contable por procesos para el tipo de operación XX = XXXXXXX");
                            return;
                        }
                        if (!ValidarTraslados())
                            return;
                        Int64? rpta = 0;
                        if (!panelProceso.Visible && panelGeneral.Visible)
                        {
                            rpta = ctlproceso.Inicializar(120, Convert.ToDateTime(txtFecha.Text), (Usuario)Session["Usuario"]);
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
                                if (GenerarComprobante())
                                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                                else
                                    VerError("Se presentó error");
                            }
                        }
                    }
                    else
                    {
                        VerError("No hay datos de transacciones para generar el comprobante");
                    }
                    btnGuardar.Visible = false;
                }
                catch
                {
                }
            }
            else
                VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
        }
        else
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
    }

    protected void gvMovimiento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMovimiento.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.CodigoPrograma, "gvMovimiento_PageIndexChanging", ex);
        }
    }

    protected void btnExportarExcel_Click(object sender, EventArgs e)
    {
        if (gvMovimiento.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvMovimiento.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvMovimiento);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
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
            if (GenerarComprobante())
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            else
                VerError("Se presentó error");
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected bool GenerarComprobante()
    {
        VerError("");
        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        Int64 pcod_proceso = Convert.ToInt64(ctlproceso.cod_proceso); 
        Int64 pnum_comp = 0;
        Int64 ptipo_comp = 0;
        string pError = "";
        if (ComprobanteServicio.GenerarComprobanteCaja(0, 120, Convert.ToDateTime(txtFecha.Text), Convert.ToInt64(Session["Oficina"].ToString()), 0, -1, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref pError, (Usuario)Session["Usuario"]))
        {
            if (pError.Trim() != "")
                VerError(pError);
            // Se cargan las variables requeridas para generar el comprobante
            Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = pnum_comp;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = ptipo_comp;
            btnGuardar.Visible = false;
            return true;
        }
        else
        {
            VerError("Error al generar el comprobante. " + pError);
            return false;
        }
    }

}


