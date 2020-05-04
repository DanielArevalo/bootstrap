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

public partial class Nuevo : GlobalWeb
{
    Xpinn.Caja.Services.ReintegroService reintegroService = new Xpinn.Caja.Services.ReintegroService ();
    Xpinn.Caja.Entities.Reintegro reintegro = new Xpinn.Caja.Entities.Reintegro();

    Xpinn.Caja.Services.HorarioOficinaService HorarioService = new Xpinn.Caja.Services.HorarioOficinaService();
    Xpinn.Caja.Entities.HorarioOficina horario = new Xpinn.Caja.Entities.HorarioOficina();

    Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();
    Usuario user = new Usuario();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(reintegroService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            mvprincipal.ActiveViewIndex = 0;
            toolBar.MostrarImprimir(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reintegroService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                user = (Usuario)Session["usuario"];
                cajero = cajeroService.ConsultarIfUserIsntCajeroPrincipal(user.codusuario, (Usuario)Session["usuario"]);
                Session["estadoCaj"] = cajero.estado;//estado Cajero
                Session["estadoOfi"] = cajero.estado_ofi;// estado Oficina
                Session["estadoCaja"] = cajero.estado_caja;// estado Caja

                horario = HorarioService.VerificarHorarioOficina(user.cod_oficina, (Usuario)Session["usuario"]);
                Session["conteoOfiHorario"]= horario.conteo;

                horario = HorarioService.getHorarioOficina(user.cod_oficina, (Usuario)Session["usuario"]);

                Session["Resp1"]=0;
                Session["Resp2"] = 0;

                //si la hora actual es mayor que de la hora inicial
                if (TimeSpan.Compare(horario.fecha_hoy.TimeOfDay,horario.hora_inicial.TimeOfDay) > 0)
                    Session["Resp1"] = 1;
                //si la hora actual es menor que la hora final
                if (TimeSpan.Compare(horario.fecha_hoy.TimeOfDay,horario.hora_final.TimeOfDay) < 0)
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
                
                //se inicializa el combo de monedas y bancos
                LlenarComboMonedas(ddlMonedas);
                LlenarComboBancos(ddlBancos);
                ObtenerDatos();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reintegroService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void LlenarComboMonedas(DropDownList ddlMonedas)
    {

        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        Usuario usuario = new Usuario();
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, (Usuario)Session["usuario"]);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();
    }

    protected void LlenarComboBancos(DropDownList ddlBancos)
    {

        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        Usuario usuario = new Usuario();
        ddlBancos.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlBancos.DataTextField = "nombrebanco";
        ddlBancos.DataValueField = "cod_banco";
        ddlBancos.DataBind();
    }





    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        mvprincipal.ActiveViewIndex = 1;
        ReportParameter[] param = new ReportParameter[8];
        param[0] = new ReportParameter("fecha", txtFechaReintegro.Text);
        param[1] = new ReportParameter("oficina", txtOficina.Text);
        param[2] = new ReportParameter("caja", txtCaja.Text);
        param[3] = new ReportParameter("cajero", txtCajero.Text);
        param[4] = new ReportParameter("valor", txtValor.Text);
        param[5] = new ReportParameter("cajero_entrega", Convert.ToString(ddlBancos.SelectedItem));
        param[6] = new ReportParameter("moneda", Convert.ToString(ddlMonedas.SelectedItem));
        param[7] = new ReportParameter("ImagenReport", ImagenReporte());

        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.LocalReport.SetParameters(param);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.Refresh();
     
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (txtValor.Text.Trim() == "")
        {
            VerError("Debe ingresar el valor del reintegro");
            return;
        }
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
                            if (ConvertirStringToDecimal(txtValor.Text) > 0)
                            {
                                try
                                {
                                    txtfecha.ToDateTime = DateTime.Now;
                                    //se atrapan los datos del formulario
                                    reintegro.cod_banco = long.Parse(ddlBancos.SelectedValue);
                                    reintegro.cod_moneda = long.Parse(ddlMonedas.SelectedValue);
                                    reintegro.fechareintegro = Convert.ToDateTime(txtFechaReintegro.Text);
                                    reintegro.valor_reintegro = decimal.Parse(txtValor.Text);
                                    reintegro.cod_oficina = long.Parse(Session["Office"].ToString());
                                    reintegro.cod_cajero = long.Parse(Session["cajero"].ToString());
                                    reintegro.cod_caja = long.Parse(Session["caja"].ToString());
                                    reintegro.tipo_movimiento = "INGRESO";
                                    reintegro.tipo_ope = 30;
                                    reintegro.ip = Convert.ToString((Session["ipusuario"].ToString()));
                                    reintegro.fechaarqueo = Convert.ToDateTime(txtfecha.Text);

                                   
                                    reintegro = reintegroService.CrearReintegro(reintegro, (Usuario)Session["usuario"]);
                                    mvprincipal.ActiveViewIndex = 2;
                                    Site toolBar = (Site)this.Master;
                                    toolBar.MostrarGuardar(false);
                                    toolBar.MostrarImprimir(true);
                                }
                                catch (ExceptionBusiness ex)
                                {
                                    VerError(ex.Message);
                                }
                                catch (Exception ex)
                                {
                                    BOexcepcion.Throw(reintegroService.GetType().Name + "A", "btnGuardar_Click", ex);
                                }
                            }
                            else
                            {
                                VerError("El Valor debe ser mayor que Cero");
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
            }else
                VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
        }else
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
    }

    protected void ObtenerDatos()
    {
        try
        {
            reintegro = reintegroService.ConsultarCajero((Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(reintegro.fechareintegro.ToString()))
                txtFechaReintegro.Text = reintegro.fechareintegro.ToShortDateString();

            if (!string.IsNullOrEmpty(reintegro.nomoficina))
                txtOficina.Text = reintegro.nomoficina.ToString();

            if (!string.IsNullOrEmpty(reintegro.nomcaja))
                txtCaja.Text  = reintegro.nomcaja.ToString();
            if (!string.IsNullOrEmpty(reintegro.nomcajero))
                txtCajero.Text = reintegro.nomcajero.ToString().Trim();

            if (!string.IsNullOrEmpty(Convert.ToString(reintegro.cod_caja)))
                Session["Caja"] = reintegro.cod_caja.ToString().Trim();

            if (!string.IsNullOrEmpty(reintegro.cod_oficina.ToString()))
                Session["Office"] = reintegro.cod_oficina.ToString().Trim();

            if (!string.IsNullOrEmpty(reintegro.cod_cajero.ToString()))
                Session["Cajero"] = reintegro.cod_cajero.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(reintegroService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }
}