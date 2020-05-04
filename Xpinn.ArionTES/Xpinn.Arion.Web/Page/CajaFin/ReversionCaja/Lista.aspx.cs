using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Microsoft.Reporting.WebForms;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;
using System.Drawing.Printing;
using System.Web.Script.Services;
using Xpinn.Interfaces.Services;
using Xpinn.Interfaces.Entities;

public partial class Lista : GlobalWeb
{
    Int64 cod_ope = 0;
    private Xpinn.Caja.Services.TransaccionCajaService transaccionService = new Xpinn.Caja.Services.TransaccionCajaService();
    private Xpinn.Caja.Entities.TransaccionCaja transacCaja = new Xpinn.Caja.Entities.TransaccionCaja();

    private Xpinn.Caja.Services.ReintegroService reintegroService = new Xpinn.Caja.Services.ReintegroService();
    private Xpinn.Caja.Entities.Reintegro reintegro = new Xpinn.Caja.Entities.Reintegro();

    private Xpinn.Caja.Services.HorarioOficinaService HorarioService = new Xpinn.Caja.Services.HorarioOficinaService();
    private Xpinn.Caja.Entities.HorarioOficina horario = new Xpinn.Caja.Entities.HorarioOficina();

    private Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    private Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();

    Xpinn.Caja.Services.MovimientoCajaService movCajaServicio = new Xpinn.Caja.Services.MovimientoCajaService();
    Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();
    List<Xpinn.Caja.Entities.MovimientoCaja> lstConsultaCheque = new List<Xpinn.Caja.Entities.MovimientoCaja>();

    Usuario user = new Usuario();
    List<Xpinn.Caja.Entities.TransaccionCaja> lstConsulta = new List<Xpinn.Caja.Entities.TransaccionCaja>();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(transaccionService.CodigoPrograma2, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(transaccionService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                mvReversion.ActiveViewIndex = 0;
                user = (Usuario)Session["usuario"];
                cajero = cajeroService.ConsultarIfUserIsntCajeroPrincipal(user.codusuario, (Usuario)Session["usuario"]);
                Session["estadoCaj"] = cajero.estado;//estado Cajero
                Session["estadoOfi"] = cajero.estado_ofi;// estado Oficina
                Session["estadoCaja"] = cajero.estado_caja;// estado Caja

                if (transaccionService.UsuarioResponsableOficina(user.cod_oficina, (Usuario)Session["usuario"]) == user.codusuario)
                {
                    Session["estadoCaj"] = "1";
                    Session["estadoOfi"] = "1";
                    Session["estadoCaja"] = "1";
                }

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
                            VerError("La Oficina no cuenta con un horario establecido para el día de hoy (Día: " + HorarioService.getDiaHorarioOficina(user.cod_oficina, (Usuario)Session["usuario"]) + ")");
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

                LlenarComboMotivosAnu(ddlMotivoAnulacion);
                ObtenerDatos();
                Actualizar();
                LlenarComboMonedas(ddlMonedas);
                //Calcular valor cheque 
                ValorCheque(long.Parse(ddlMonedas.SelectedValue));
                setValueGrid();

            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(transaccionService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void LlenarComboMonedas(DropDownList ddlMonedas)
    {

        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, (Usuario)Session["Usuario"]);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();
    }
    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }

    protected void LlenarComboMotivosAnu(DropDownList ddlMotivoAnu)
    {

        Xpinn.Caja.Services.TipoMotivoAnuService motivoAnuService = new Xpinn.Caja.Services.TipoMotivoAnuService();
        Xpinn.Caja.Entities.TipoMotivoAnu motivoAnu = new Xpinn.Caja.Entities.TipoMotivoAnu();
        ddlMotivoAnu.DataSource = motivoAnuService.ListarTipoMotivoAnu(motivoAnu, (Usuario)Session["usuario"]);
        ddlMotivoAnu.DataTextField = "descripcion";
        ddlMotivoAnu.DataValueField = "tipo_motivo";
        ddlMotivoAnu.DataBind();
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        try
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

                                decimal acum = 0;
                                decimal acumulado = 0;
                                CheckBox chkAnula;
                                decimal valEgreso = 0;
                                decimal valIngreso = 0;
                                String tipo_ope = "";

                                foreach (GridViewRow fila in gvOperacion.Rows)
                                {
                                    tipo_ope = (fila.Cells[5].Text);
                                    chkAnula = (CheckBox)fila.FindControl("chkAnula");

                                    if (chkAnula.Checked == true)
                                    {
                                        valIngreso = decimal.Parse(fila.Cells[6].Text);
                                        valEgreso = decimal.Parse(fila.Cells[7].Text);

                                        if (valIngreso > 0)
                                            acum = acum - valIngreso;
                                        if (valEgreso > 0)
                                            acum = acum + valEgreso;
                                        acumulado = valEgreso;
                                        if (valIngreso != 0)
                                            acumulado = valIngreso;
                                        else
                                            acumulado = valEgreso;

                                    }
                                }

                                Xpinn.Caja.Entities.SaldoCaja saldo = new Xpinn.Caja.Entities.SaldoCaja();
                                Xpinn.Caja.Services.SaldoCajaService saldoService = new Xpinn.Caja.Services.SaldoCajaService();

                                saldo.cod_caja = long.Parse(Session["Caja"].ToString());
                                saldo.cod_cajero = long.Parse(Session["Cajero"].ToString());
                                saldo.tipo_moneda = 1;
                                saldo.fecha = Convert.ToDateTime(txtFecha.Text);

                                saldo = saldoService.ConsultarSaldoCaja(saldo, (Usuario)Session["usuario"]);

                                //if (saldo.valor > 0 && tipo_ope != "Consignaciones")
                                if (saldo.valor > 0 || transaccionService.UsuarioResponsableOficina(user.cod_oficina, (Usuario)Session["usuario"]) == user.codusuario)
                                {
                                    // if (acum <= saldo.valor)// si el total valor de la reversion es menor se hace si es mayor no
                                    //{
                                    try
                                    {
                                        transacCaja.cod_motivo_reversion = long.Parse(ddlMotivoAnulacion.SelectedValue);
                                    }
                                    catch (FormatException ex)
                                    {
                                        VerError("Debe ingresar motivo de reversión");
                                        return;
                                    }
                                    catch (Exception ex)
                                    {
                                        VerError(ex.Message);
                                        return;
                                    }

                                    //if (acumulado >= saldo.valor)// si el total valor de la reversion es mayor no se hace 
                                    //{
                                    // VerError("No se puede reverar supera el saldo de la caja");
                                    // return;
                                    //}
                                    //  else
                                    // {

                                    transacCaja = transaccionService.CrearTransaccionCajaReversion(transacCaja, gvOperacion, (Usuario)Session["usuario"]);

                                    //Resultado 2 es igual a mensaje LA OPERACIÓN NO SE PUEDE ANULAR , TIENE OPERACIONES MAS RECIENTES'''
                                    if (transacCaja.resultado == 2)
                                    {
                                        VerError("LA OPERACIÓN NO SE PUEDE ANULAR, TIENE OPERACIONES MAS RECIENTES");
                                        return;

                                    }
                                    else
                                    {
                                        if (transacCaja.resultado == 1)
                                        {
                                            VerError("LA OPERACIÓN  FUE ANULADA CORRECTAMENTE");
                                            Site toolbar1 = (Site)this.Master;
                                            toolbar1.MostrarGuardar(false);
                                           
                                        }
                                        else
                                        {
                                            VerError("LA OPERACIÓN  NO SE PUDO ANULAR");
                                            return;
                                        }
                                    }

                                    mvReversion.ActiveViewIndex = 1;
                                    foreach (GridViewRow fila in gvOperacion.Rows)
                                    {
                                        chkAnula = (CheckBox)fila.FindControl("chkAnula");
                                        cod_ope = int.Parse(fila.Cells[1].Text);
                                        if (chkAnula.Checked == true)
                                        {
                                            Session["Cod_ope"] = cod_ope;
                                        }
                                    }

                                    //Imprimir();
                                    // }
                                    // }
                                    //else
                                    //   VerError("El Valor Total de todas las operaciones escogidas no puede ser superior al Saldo de Caja. Saldo: " + saldo.valor + " Total Operaciones: " + acum);

                                }
                                else
                                    VerError("La Caja no tiene Dinero disponible para realizar esta Operación");
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
        catch (Exception ex)
        {
            BOexcepcion.Throw(reintegroService.GetType().Name + "A", "ObtenerDatos", ex);
        }

    }

    protected void ObtenerDatos()
    {
        try
        {
            reintegro = reintegroService.ConsultarCajero((Usuario)Session["usuario"]);
            if ((reintegro == null || reintegro.fechareintegro == null || reintegro.fechareintegro == DateTime.MinValue) && transaccionService.UsuarioResponsableOficina(user.cod_oficina, (Usuario)Session["usuario"]) == user.codusuario)
            {
                reintegro.fechareintegro = DateTime.Now;
                reintegro.nomoficina = ((Usuario)Session["usuario"]).nombre_oficina;
                reintegro.cod_oficina = ((Usuario)Session["usuario"]).cod_oficina;
            }

            if (!string.IsNullOrEmpty(reintegro.fechareintegro.ToString()))
                txtFecha.Text = reintegro.fechareintegro.ToShortDateString();
            if (!string.IsNullOrEmpty(reintegro.nomoficina))
                txtOficina.Text = reintegro.nomoficina.ToString();
            if (!string.IsNullOrEmpty(reintegro.nomcajero))
                txtCajero.Text = reintegro.nomcajero.ToString().Trim();
            if (!string.IsNullOrEmpty(reintegro.cod_oficina.ToString()))
                Session["Oficina"] = reintegro.cod_oficina.ToString().Trim();
            if (!string.IsNullOrEmpty(reintegro.cod_caja.ToString()))
                Session["Caja"] = reintegro.cod_caja.ToString().Trim();
            if (!string.IsNullOrEmpty(reintegro.cod_cajero.ToString()))
                Session["Cajero"] = reintegro.cod_cajero.ToString().Trim();

            // Determinar el saldo de la caja
            Xpinn.Caja.Entities.SaldoCaja saldo = new Xpinn.Caja.Entities.SaldoCaja();
            Xpinn.Caja.Services.SaldoCajaService saldoService = new Xpinn.Caja.Services.SaldoCajaService();
            saldo.cod_caja = long.Parse(Session["Caja"].ToString());
            saldo.cod_cajero = long.Parse(Session["Cajero"].ToString());
            saldo.tipo_moneda = 1;
            saldo.fecha = Convert.ToDateTime(txtFecha.Text);
            saldo = saldoService.ConsultarSaldoCaja(saldo, (Usuario)Session["usuario"]);
            txtSaldoCaja.Text = Convert.ToString(saldo.valor);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reintegroService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    public void Actualizar()
    {
        try
        {
            transacCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
            transacCaja.cod_oficina = long.Parse(Session["Oficina"].ToString());
            transacCaja.fecha_cierre = Convert.ToDateTime(txtFecha.Text);
            lstConsulta = transaccionService.ListarOperaciones(transacCaja, (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);

            gvOperacion.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvOperacion.Visible = true;
                gvOperacion.DataBind();
            }
            else
            {
                gvOperacion.Visible = false;
            }

            Session.Add(transaccionService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(transaccionService.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Xpinn.Caja.Entities.MovimientoCaja ObtenerValoresCheque(Int64 MonedaId)
    {
        Xpinn.Caja.Entities.MovimientoCaja movCaja = new Xpinn.Caja.Entities.MovimientoCaja();

        movCaja.cod_caja = long.Parse(Session["Caja"].ToString());
        movCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
        movCaja.cod_moneda = MonedaId;
        movCaja.tipo_mov = "I";
        movCaja.cod_tipo_pago = 2;
        //averiguuar por que el estado era 0 
        movCaja.estado = 0;

        return movCaja;
    }

    public void ValorCheque(Int64 MonedaId)
    {
        try
        {


            lstConsultaCheque = movCajaServicio.ListarMovimientoCaja(ObtenerValoresCheque(MonedaId), (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);

            gvConsignacion.DataSource = lstConsultaCheque;

            if (lstConsulta.Count > 0)
            {
                //gvConsignacion.Visible = true;
                gvConsignacion.DataBind();
                ValidarPermisosGrilla(gvConsignacion);
            }
            else
            {
                gvConsignacion.Visible = false;
            }

            Session.Add(movCajaServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(movCajaServicio.GetType().Name + "L", "ValorCheque", ex);
        }
    }


    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        // RpviewInfo.Visible = true;
        mvReversion.ActiveViewIndex = 3;
        Imprimir();
        //  Navegar(Pagina.Lista);
    }
    protected void ddlMonedas_SelectedIndexChanged(object sender, EventArgs e)
    {
        int MonedaId = int.Parse(ddlMonedas.SelectedValue);
        ValorCheque(MonedaId);
        setValueGrid();
    }
    protected void setValueGrid()
    {
        decimal valorCheque = 0;
        decimal acum = 0;
        if (lstConsulta.Count > 0)
        {
            foreach (GridViewRow fila in gvConsignacion.Rows)
            {
                valorCheque = Decimal.Parse(fila.Cells[5].Text);
                acum += valorCheque;
            }

            txtSaldoCajaCheque.Text = acum.ToString();
        }
        else
            txtSaldoCajaCheque.Text = "0";
    }


    protected void Imprimir()
    {

        Usuario pUsuario = (Usuario)Session["usuario"];
        String cajero = "";
        String caja = "";
        String oficina = "";
        String identificacion = "";
        String num_producto = "";
        String tipo_producto = "";
        String fechaoper = "";
        decimal valor_pago = 0;
        String nom_oficina = "";
        String cliente = "";
        try
        {

            transacCaja = transaccionService.ConsultarOperacionesAnuladas(Convert.ToInt64(Session["Cod_ope"]), (Usuario)Session["usuario"]);
            if (!string.IsNullOrEmpty(transacCaja.fecha_movimiento.ToString()))
                fechaoper = transacCaja.fecha_movimiento.ToShortDateString();

            if (!string.IsNullOrEmpty(transacCaja.nom_cajero))
                cajero = transacCaja.nom_cajero.ToString().Trim();


            if (!string.IsNullOrEmpty(transacCaja.nom_caja))
                caja = transacCaja.nom_caja.ToString().Trim();

            if (!string.IsNullOrEmpty(transacCaja.identificacion))
                identificacion = transacCaja.identificacion.ToString().Trim();


            if (!string.IsNullOrEmpty(transacCaja.nom_producto))
                num_producto = transacCaja.nom_producto.ToString().Trim();

            if (!string.IsNullOrEmpty(transacCaja.tipoproducto))
                tipo_producto = transacCaja.tipoproducto.ToString().Trim();

            if (transacCaja.valor_pago != 0)
                valor_pago = transacCaja.valor_pago;

            if (transacCaja.cod_movimiento != 0)
                cod_ope = transacCaja.cod_movimiento;

            if (!string.IsNullOrEmpty(transacCaja.nom_Cliente))
                cliente = transacCaja.nom_Cliente.ToString().Trim();
            DateTime fechahoy = DateTime.Now;

            if (!string.IsNullOrEmpty(transacCaja.nom_oficina))
                nom_oficina = transacCaja.nom_oficina.ToString().Trim();


            //Imprimir 

            ReportParameter[] param = new ReportParameter[16];
            param[0] = new ReportParameter("nit", pUsuario.nitempresa);
            //  param[1] = new ReportParameter("logo", ImagenReporte());
            param[1] = new ReportParameter("fechaoper", fechaoper.ToString());
            param[2] = new ReportParameter("cajero", cajero);
            param[3] = new ReportParameter("caja", caja);
            param[4] = new ReportParameter("identificacion", identificacion);
            param[5] = new ReportParameter("num_producto", num_producto);
            param[6] = new ReportParameter("tipo_producto", tipo_producto);
            param[7] = new ReportParameter("valor_pago", valor_pago.ToString("n0"));
            param[8] = new ReportParameter("cod_ope", cod_ope.ToString());
            param[9] = new ReportParameter("cliente", cliente);
            param[10] = new ReportParameter("fechahoy", fechahoy.ToString());
            param[11] = new ReportParameter("direccion", pUsuario.direccion);
            param[12] = new ReportParameter("telefono", pUsuario.telefono);
            param[13] = new ReportParameter("empresa", pUsuario.empresa);
            param[14] = new ReportParameter("logo", new Uri(Server.MapPath("~/Images/LogoEmpresa.jpg")).AbsoluteUri);
            param[15] = new ReportParameter("oficina", nom_oficina);
            RpviewInfo.LocalReport.EnableExternalImages = true;
            RpviewInfo.LocalReport.SetParameters(param);
            var sa = RpviewInfo.LocalReport.GetDefaultPageSettings();

            RpviewInfo.LocalReport.DataSources.Clear();
            RpviewInfo.LocalReport.Refresh();

            //Site toolBar = (Site)this.Master;
            //toolBar.MostrarCancelar(true);
            //toolBar.MostrarExportar(false);
            // mvReversion.ActiveViewIndex = 3;
            //mvOperacion.ActiveViewIndex = 1;                                            
            //   RpviewInfo.Visible = true;
            btnImprimiendose_Click();
            Session["Cod_ope"] = null;
            return;


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reintegroService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    protected void btnImprimirRep_Click(object sender, EventArgs e)
    {
        if (RpviewInfo.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Usuario pUsuario = (Usuario)Session["Usuario"];
            string cod_usuario = pUsuario.codusuario.ToString();

            byte[] bytes = RpviewInfo.LocalReport.Render("PDF");
            string ruta = HttpContext.Current.Server.MapPath("output" + cod_usuario + ".pdf");

            if (File.Exists(ruta))
            {
                File.Delete(ruta);
            }

            FileStream fs = new FileStream(ruta, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            //  frmPrint.Visible = true;
            Session["Archivo" + Usuario.codusuario] = Server.MapPath("output" + cod_usuario + ".pdf");
        }
    }

    protected void btnImprimiendose_Click()
    {
        if (RpviewInfo.Visible == true)
        {

            mvReversion.ActiveViewIndex = 3;
            //  View1.Visible = true;
            //MOSTRAR REPORTE EN PANTALLA
            var bytes = RpviewInfo.LocalReport.Render("PDF");
            MostrarArchivoEnLiteralL(bytes);
            //RpviewInfo.Visible = true;
        }
    }

    void MostrarArchivoEnLiteralL(byte[] bytes)
    {
        Usuario pUsuario = Usuario;

        string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_ReportReversiones" + pUsuario.nombre : "";
        // ELIMINANDO ARCHIVOS GENERADOS
        try
        {
            string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("..\\..\\..\\Archivos\\"));
            foreach (string ficheroActual in ficherosCarpeta)
                if (ficheroActual.Contains(pNomUsuario))
                    File.Delete(ficheroActual);
        }
        catch
        { }

        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("..\\..\\..\\Archivos/output" + pNomUsuario + ".pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        //MOSTRANDO REPORTE
        string adjuntar = "<object data=\"{0}\" type=\"application/pdf\" width=\"50%\" height=\"500px\">";
        adjuntar += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
        adjuntar += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
        adjuntar += "</object>";

        LiteralDcl.Text = string.Format(adjuntar, ResolveUrl("..\\..\\..\\Archivos/output" + pNomUsuario + ".pdf"));
        LiteralDcl.Visible = true;
    }



}