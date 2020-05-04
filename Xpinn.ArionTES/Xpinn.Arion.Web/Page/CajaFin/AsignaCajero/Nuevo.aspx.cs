using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Caja.Entities;

public partial class Nuevo : GlobalWeb
{
    Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Entities.Cajero cajero1 = new Xpinn.Caja.Entities.Cajero();
    Xpinn.Caja.Services.HorarioOficinaService HorarioService = new Xpinn.Caja.Services.HorarioOficinaService();
    Xpinn.Caja.Entities.HorarioOficina horario = new Xpinn.Caja.Entities.HorarioOficina();
    Usuario user = new Usuario();
    List<Caja> _lstCajasPrincipal;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[cajeroService.CodigoCajero + ".id"] != null)
                VisualizarOpciones(cajeroService.CodigoPrograma, "E");
            else
                VisualizarOpciones(cajeroService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajeroService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                user = (Usuario)Session["usuario"];
                cajero1 = cajeroService.ConsultarIfUserIsntCajeroPrincipal(user.codusuario, (Usuario)Session["usuario"]);
                Session["estadoCaj"] = cajero1.estado;//estado Cajero
                Session["estadoOfi"] = cajero1.estado_ofi;// estado Oficina

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

                //se inicializa el combo de ciudades, centro de costos
                LlenarComboOficinas(ddlOficinas);
                user = (Usuario)Session["usuario"];
                CargarCajasOficina(user.cod_oficina);
                //Actualizar(user.cod_oficina);
            }
            else
            {
                _lstCajasPrincipal = (List<Caja>)ViewState["lstCajasPrincipal"];
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void LlenarComboOficinas(DropDownList ddlOficinas)
    {
        Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
        ddlOficinas.DataSource = oficinaServicio.ListarOficina(oficina, (Usuario)Session["usuario"]);
        ddlOficinas.DataTextField = "nombre";
        ddlOficinas.DataValueField = "cod_oficina";
        ddlOficinas.DataBind();
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (long.Parse(Session["estadoOfi"].ToString()) == 1)
            {
                if (long.Parse(Session["conteoOfiHorario"].ToString()) == 1)
                {
                    if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                    {
                        if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                        {
                            cajero1.cod_caja = long.Parse(ddlCajas.SelectedValue);
                            cajeroService.CrearCajeroMass(cajero1, gvCajeros, (Usuario)Session["usuario"]);
                            Navegar("../../../General/Global/inicio.aspx");
                            //VerError("La Asignacion ha sido realizada con Exito");
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
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajeroService.GetType().Name + "A", "btnGuardar_Click", ex);
        }
    }

    public void CargarCajasOficina(Int64 idOficina)
    {
        Xpinn.Caja.Services.CajaService cajaServicio = new Xpinn.Caja.Services.CajaService();
        Xpinn.Caja.Entities.Caja caja = new Xpinn.Caja.Entities.Caja();
        caja.cod_oficina = idOficina;
        Session["oficin"] = idOficina;
        List<Caja> lstCaja = cajaServicio.ListarCaja(caja, (Usuario)Session["usuario"]);
        ddlCajas.DataSource = lstCaja;

        _lstCajasPrincipal = (from cajas in lstCaja
                              where cajas.esprincipal == 1
                              select cajas).ToList();

        ViewState.Add("lstCajasPrincipal", _lstCajasPrincipal);

        ddlCajas.DataTextField = "nombre";
        ddlCajas.DataValueField = "cod_caja";
        ddlCajas.DataBind();
        Actualizar(idOficina);

        CheckBox chkOperacionPermitida;
        long CajaId = int.Parse(ddlCajas.SelectedValue);

        long cajero = 0;

        foreach (GridViewRow fila in gvCajeros.Rows)
        {
            //se captura la opcion chequeda en el grid
            cajero = int.Parse(fila.Cells[0].Text);
            chkOperacionPermitida = (CheckBox)fila.FindControl("chkPermiso");
            cajero1 = cajeroService.ConsultarCajeroXCaja(cajero, CajaId, (Usuario)Session["usuario"]);


            if (cajero1.conteo == 1)
            {
                chkOperacionPermitida.Checked = true;
            }
        }

        ddlOficinas.SelectedValue = idOficina.ToString();
    }

    public void Actualizar(Int64 idOficina)
    {
        try
        {
            Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();
            List<Xpinn.Caja.Entities.Cajero> lstConsulta = new List<Xpinn.Caja.Entities.Cajero>();

            //se configura el id de la oficina del usuario que esta conectado
            cajero.cod_oficina = idOficina;
            lstConsulta = cajeroService.ListarTCajero(cajero, (Usuario)Session["usuario"]);

            // se llena la grilla de Cajeros

            gvCajeros.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvCajeros.Visible = true;
                gvCajeros.DataBind();
                ValidarPermisosGrilla(gvCajeros);
            }
            else
            {
                gvCajeros.Visible = false;
            }

            Session.Add(cajeroService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajeroService.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void ddlCajas_SelectedIndexChanged(object sender, EventArgs e)
    {
        //permite traer datos a la grilla de los Cajero relacionados con la caja
        //se inserta las opciones de la grilla en Cajero

        CheckBox chkOperacionPermitida;
        long CajaId = int.Parse(ddlCajas.SelectedValue);

        long cajero = 0;

        Actualizar(long.Parse(Session["oficin"].ToString()));
        foreach (GridViewRow fila in gvCajeros.Rows)
        {
            //se captura la opcion chequeda en el grid
            cajero = int.Parse(fila.Cells[0].Text);
            chkOperacionPermitida = (CheckBox)fila.FindControl("chkPermiso");
            cajero1 = cajeroService.ConsultarCajeroXCaja(cajero, CajaId, (Usuario)Session["usuario"]);

            if (cajero1.conteo == 1)
            {
                chkOperacionPermitida.Checked = true;
            }
        }
    }

    protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkSeleccionar = (CheckBoxGrid)sender;
        int nItem = Convert.ToInt32(chkSeleccionar.CommandArgument);
        string cajaSeleccionada = ddlCajas.SelectedValue;
        bool cajaPrincipalSeleccionada = _lstCajasPrincipal.Where(x => string.Compare(x.cod_caja, cajaSeleccionada) == 0).Any();

        if (cajaPrincipalSeleccionada)
        {
            DropDownList ddlCajaPrincipal = gvCajeros.Rows[nItem].FindControl("ddlCajaPrincipal") as DropDownList;
            ddlCajaPrincipal.SelectedValue = "0";
        }

        foreach (GridViewRow row in gvCajeros.Rows)
        {
            CheckBoxGrid chkBox = row.FindControl("chkPermiso") as CheckBoxGrid;

            if (chkBox.Checked && chkBox != chkSeleccionar)
            {
                chkBox.Checked = false;
            }
        }
    }

    protected void gvCajeros_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl = e.Row.FindControl("ddlCajaPrincipal") as DropDownList;
            Label lblPrincipal = e.Row.FindControl("lblEsPrincipal") as Label;
            string cod_cajero = e.Row.Cells[0].Text;

            if (ddl != null)
            {
                ddl.DataSource = _lstCajasPrincipal;
                ddl.DataBind();

                foreach (var caja in _lstCajasPrincipal)
                {
                    Cajero cajero = cajeroService.ConsultarCajeroXCaja(Convert.ToInt64(cod_cajero), Convert.ToInt64(caja.cod_caja), (Usuario)Session["usuario"]);
                    bool esPrincipal = _lstCajasPrincipal.Where(x => cajero.conteo > 0 && x.esprincipal == 1).Any();

                    if (esPrincipal)
                    {
                        lblPrincipal.Text = "1";
                        ddl.Enabled = false;
                        break;
                    }
                    else
                    {
                        lblPrincipal.Text = "0";
                        ddl.Enabled = true;
                        Cajero cajeroDes = cajeroService.ConsultarCajero(Convert.ToInt64(cod_cajero), (Usuario)Session["usuario"]);

                        ddl.SelectedValue = cajeroDes.cod_caja_des.ToString();
                    }
                }
            }
        }
    }
}