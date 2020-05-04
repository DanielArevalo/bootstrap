using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

public partial class Detalle : GlobalWeb
{
    Xpinn.Caja.Services.HorarioOficinaService horarioService = new Xpinn.Caja.Services.HorarioOficinaService();
    Xpinn.Caja.Services.OficinaService oficinaService = new Xpinn.Caja.Services.OficinaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(horarioService.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(horarioService.CodigoHorarioOficina + "A", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                AsignarEventoConfirmar();
                LlenarComboDiasSemana(ddlDias);
                ObtenerOficinaData();

                if (Session[horarioService.CodigoHorarioOficina + ".idh"] != null)
                {
                    idObjeto = Session[horarioService.CodigoHorarioOficina + ".idh"].ToString();
                    Session.Remove(horarioService.CodigoHorarioOficina + ".idh");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(horarioService.GetType().Name + "D", "Page_Load", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[horarioService.CodigoHorarioOficina + ".idh"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../CajaFin/Oficina/Detalle.aspx");
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            horarioService.EliminarHorarioOficina(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            Navegar("../../CajaFin/Oficina/Detalle.aspx");
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(horarioService.CodigoHorarioOficina + "C", "btnEliminar_Click", ex);
        }
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void ObtenerOficinaData()
    {
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();

        oficina = oficinaService.ConsultarOficina(long.Parse(Session[oficinaService.CodigoOficina + ".id"].ToString()), (Usuario)Session["usuario"]);

        if (!string.IsNullOrEmpty(oficina.cod_oficina.ToString()))
            lblCodOficina.Text = oficina.cod_oficina.ToString();
        if (!string.IsNullOrEmpty(oficina.nombre.ToString()))
            lblOficina.Text = oficina.nombre.ToString();

    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Caja.Entities.HorarioOficina horario = new Xpinn.Caja.Entities.HorarioOficina();

            if (pIdObjeto != null)
                horario = horarioService.ConsultarHorarioOficina(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(horario.cod_horario.ToString()))
                lblCodigo.Text = horario.cod_horario.ToString();
            if (!string.IsNullOrEmpty(horario.dia.ToString()))
                ddlDias.SelectedValue = horario.dia.ToString();
            if (!string.IsNullOrEmpty(horario.hora_inicial.ToString()))
                txtHoraIni.Text = horario.hora_inicial.ToLongTimeString();
            if (!string.IsNullOrEmpty(horario.hora_final.ToString()))
                txtHoraFin.Text = horario.hora_final.ToLongTimeString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(horarioService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }


    protected void LlenarComboDiasSemana(DropDownList ddlDias)
    {
        Xpinn.Caja.Services.DiaSemanaService diaSemanaServicio = new Xpinn.Caja.Services.DiaSemanaService();
        Xpinn.Caja.Entities.DiaSemana dia = new Xpinn.Caja.Entities.DiaSemana();
        Usuario usuario = new Usuario();

        ddlDias.DataSource = diaSemanaServicio.ListarDiaSemana(dia, usuario);
        ddlDias.DataTextField = "nom_diasemana";
        ddlDias.DataValueField = "cod_diasemana";
        ddlDias.DataBind();
    }
}