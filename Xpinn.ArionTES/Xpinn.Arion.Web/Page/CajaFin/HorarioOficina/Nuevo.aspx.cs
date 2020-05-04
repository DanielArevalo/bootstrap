using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

public partial class Nuevo : GlobalWeb
{
    Xpinn.Caja.Services.HorarioOficinaService horarioService = new Xpinn.Caja.Services.HorarioOficinaService();
    Xpinn.Caja.Services.OficinaService oficinaService = new Xpinn.Caja.Services.OficinaService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[horarioService.CodigoHorarioOficina + ".idh"] != null)
                VisualizarOpciones(horarioService.CodigoPrograma, "E");
            else
                VisualizarOpciones(horarioService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;

            if (Session[horarioService.CodigoHorarioOficina + ".idh"] != null)
                toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(horarioService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                LlenarComboDiasSemana(ddlDias);
                ddlDias.Enabled = true;
                ObtenerOficinaData();

                if (Session[horarioService.CodigoHorarioOficina + ".idh"] != null)
                {
                    ddlDias.Enabled = false;
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
            BOexcepcion.Throw(horarioService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../CajaFin/Oficina/Detalle.aspx");
    }

    protected void ObtenerOficinaData()
    {
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();

        if (Session[oficinaService.CodigoOficina + ".id"] == null)
        {
            Usuario pUsu = new Usuario();
            pUsu = (Usuario)Session["usuario"];
            Session[oficinaService.CodigoOficina + ".id"] = pUsu.cod_oficina;
            Xpinn.Caja.Services.HorarioOficinaService HorOfiSer = new Xpinn.Caja.Services.HorarioOficinaService();
            Xpinn.Caja.Entities.HorarioOficina vHorario = new Xpinn.Caja.Entities.HorarioOficina();
            vHorario = HorOfiSer.getHorarioOficina(pUsu.cod_oficina, pUsu);
            Session[horarioService.CodigoHorarioOficina + ".idh"] = vHorario.cod_horario;
        }
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

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToDateTime(txtHoraIni.Text) < Convert.ToDateTime(txtHoraFin.Text))
        {
            try
            {
                Xpinn.Caja.Entities.HorarioOficina horario = new Xpinn.Caja.Entities.HorarioOficina();
                if (idObjeto != "")
                    horario = horarioService.ConsultarHorarioOficina(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

                //se atrapan los datos del formulario
                horario.dia = Convert.ToInt64(ddlDias.SelectedValue);
                horario.hora_inicial = Convert.ToDateTime(txtHoraIni.Text);
                horario.hora_final = Convert.ToDateTime(txtHoraFin.Text);
                horario.cod_oficina = long.Parse(Session[oficinaService.CodigoOficina + ".id"].ToString());
                horario.tipo_horario = long.Parse(Session[horarioService.CodigoHorarioOficina + ".idth"].ToString());

                if (idObjeto != "")
                {
                    horario.cod_horario = Convert.ToInt64(idObjeto);
                    horarioService.ModificarHorarioOficina(horario, (Usuario)Session["usuario"]);
                    Session[horarioService.CodigoHorarioOficina + ".idh"] = idObjeto;
                    Navegar(Pagina.Detalle);
                }
                else
                {
                    horario = horarioService.ConsultarHorarioXOficina(horario, (Usuario)Session["usuario"]);
                    if (horario.conteo == 0)
                    {
                        horario.dia = Convert.ToInt64(ddlDias.SelectedValue);
                        horario.hora_inicial = Convert.ToDateTime(txtHoraIni.Text);
                        horario.hora_final = Convert.ToDateTime(txtHoraFin.Text);
                        horario.cod_oficina = long.Parse(Session[oficinaService.CodigoOficina + ".id"].ToString());
                        horario.tipo_horario = long.Parse(Session[horarioService.CodigoHorarioOficina + ".idth"].ToString());

                        // Se valida de que un dia no exista mas de una vez en una oficina especifica
                        horario = horarioService.CrearHorarioOficina(horario, (Usuario)Session["usuario"]);
                        idObjeto = horario.cod_horario.ToString();
                        Session[horarioService.CodigoHorarioOficina + ".idh"] = idObjeto;
                        Navegar(Pagina.Detalle);
                    }
                    else
                        VerError("El dia escogido ya existe en esta Oficina");

                }
            }
            catch (ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(horarioService.GetType().Name + "A", "btnGuardar_Click", ex);
            }

        }
        else
            VerError("la Fecha Final debe ser mayor que la fecha Inicial");
    }

    protected void LlenarComboDiasSemana(DropDownList ddlDias)
    {
        Xpinn.Caja.Services.DiaSemanaService diaSemanaServicio = new Xpinn.Caja.Services.DiaSemanaService();
        Xpinn.Caja.Entities.DiaSemana dia = new Xpinn.Caja.Entities.DiaSemana();
        Usuario usuario = new Usuario();

        ddlDias.DataSource = diaSemanaServicio.ListarDiaSemana(dia, (Usuario)Session["usuario"]);
        ddlDias.DataTextField = "nom_diasemana";
        ddlDias.DataValueField = "cod_diasemana";
        ddlDias.DataBind();
    }
}