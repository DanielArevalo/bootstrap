using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Caja.Services;

public partial class Nuevo : GlobalWeb 
{
    Xpinn.Caja.Services.RecepcionService recepcionServicio = new Xpinn.Caja.Services.RecepcionService();
    Xpinn.Caja.Entities.Recepcion recepcion = new Xpinn.Caja.Entities.Recepcion();
    Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();
    Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Services.HorarioOficinaService HorarioService = new Xpinn.Caja.Services.HorarioOficinaService();
    Xpinn.Caja.Entities.HorarioOficina horario = new Xpinn.Caja.Entities.HorarioOficina();
    Usuario user = new Usuario();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(recepcionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(recepcionServicio.GetType().Name + "A", "Page_PreInit", ex);
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
            BOexcepcion.Throw(recepcionServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }

    public void Actualizar()
    {
        try
        {
            List<Xpinn.Caja.Entities.Traslado> lstConsulta = new List<Xpinn.Caja.Entities.Traslado>();
            lstConsulta = recepcionServicio.ListarTraslado(ObtenerValores(), (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);

            gvTraslados.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvTraslados.Visible = true;
                gvTraslados.DataBind();
                ValidarPermisosGrilla(gvTraslados);
            }
            else
            {
                gvTraslados.Visible = false;
            }

            Session.Add(recepcionServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(recepcionServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Xpinn.Caja.Entities.Recepcion ObtenerValores()
    {
        Xpinn.Caja.Entities.Recepcion recepcion = new Xpinn.Caja.Entities.Recepcion();
        recepcion.cod_caja = long.Parse(Session["Caja"].ToString());
        recepcion.cod_cajero = long.Parse(Session["Cajero"].ToString());
        return recepcion;
    }

    protected void ObtenerDatos()
    {
        try
        {
            recepcion = recepcionServicio.ConsultarCajero((Usuario)Session["usuario"]);
            if (!string.IsNullOrEmpty(recepcion.fecha_recepcion.ToString()))
                txtFechaRecepcion.Text = recepcion.fecha_recepcion.ToShortDateString();
            if (!string.IsNullOrEmpty(recepcion.nomoficina.ToString()))
                txtOficina.Text = recepcion.nomoficina.ToString();
            if (!string.IsNullOrEmpty(recepcion.nomcaja.ToString()))
                txtCaja.Text = recepcion.nomcaja.ToString();
            if (!string.IsNullOrEmpty(recepcion.nomcajero.ToString()))
                txtCajero.Text = recepcion.nomcajero.ToString().Trim();

            if (!string.IsNullOrEmpty(recepcion.cod_oficina.ToString()))
                Session["Office"] = recepcion.cod_oficina.ToString().Trim();
            if (!string.IsNullOrEmpty(recepcion.cod_caja.ToString()))
                Session["Caja"] = recepcion.cod_caja.ToString().Trim();
            if (!string.IsNullOrEmpty(recepcion.cod_cajero.ToString()))
                Session["Cajero"] = recepcion.cod_cajero.ToString().Trim();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(recepcionServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }


    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
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

                            if (gvTraslados.Rows.Count > 0)
                            {
                                try
                                {
                                    //se atrapan los datos del formulario
                                    recepcion.cod_oficina = long.Parse(Session["Office"].ToString());
                                    recepcion.cod_cajero = long.Parse(Session["Cajero"].ToString());
                                    recepcion.fecha_recepcion = Convert.ToDateTime(txtFechaRecepcion.Text);
                                    recepcion.cod_caja = long.Parse(Session["Caja"].ToString());
                                    recepcion.tipo_traslado = 1;
                                    recepcion.tipo_movimiento = "INGRESO";
                                    recepcion.estado = 1;
                                    recepcion.tipo_ope = 33;
                                    recepcion = recepcionServicio.CrearRecepcion(recepcion, gvTraslados, (Usuario)Session["usuario"]);
                                    Navegar("../../../General/Global/inicio.aspx");
                                }
                                catch (ExceptionBusiness ex)
                                {
                                    VerError(ex.Message);
                                }
                                catch (Exception ex)
                                {
                                    BOexcepcion.Throw(recepcionServicio.GetType().Name + "A", "btnGuardar_Click", ex);
                                }
                            }
                            else
                                VerError("El Cajero no tiene Traslados que Recibir.");
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

}