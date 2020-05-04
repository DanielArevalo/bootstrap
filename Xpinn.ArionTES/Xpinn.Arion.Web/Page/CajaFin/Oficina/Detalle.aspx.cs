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
    Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
    Xpinn.Caja.Services.CajaService cajaServicio = new Xpinn.Caja.Services.CajaService();
    Xpinn.Caja.Services.HorarioOficinaService horarioServicio = new Xpinn.Caja.Services.HorarioOficinaService();
    Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
    Usuario _usuario;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(oficinaServicio.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oficinaServicio.CodigoOficina + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {                
                LlenarComboCiudades(ddlCiudades);
                LlenarComboCentroCosto(ddlCentrosCosto);
                LlenarComboEncargados(ddlEncargados);

                AsignarEventoConfirmar();

                if (Session[oficinaServicio.CodigoOficina + ".id"] != null) 
                {
                    idObjeto = Session[oficinaServicio.CodigoOficina + ".id"].ToString();
                    Session.Remove(oficinaServicio.CodigoOficina + ".id");
                    ObtenerDatos(idObjeto);
                    ActualizarCaja();
                    ActualizarHorarios();
                }
                
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oficinaServicio.GetType().Name + "D", "Page_Load", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[oficinaServicio.CodigoOficina+ ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            oficina = oficinaServicio.ConsultarUsersXOficina(Convert.ToInt64(idObjeto), _usuario);

            if (oficina.conteo == 0)
            {
                oficinaServicio.EliminarOficina(Convert.ToInt64(idObjeto), _usuario);
                Navegar(Pagina.Lista);
            }
            else
                VerError("La Oficina que esta tratando de Eliminar tiene Usuarios Asociados, No Puede realizar esta operación ");
            
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oficinaServicio.CodigoOficina + "C", "btnEliminar_Click", ex);
        }
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
            oficina = oficinaServicio.ConsultarOficina(Convert.ToInt64(pIdObjeto), _usuario);

            if (!string.IsNullOrEmpty(oficina.cod_oficina.ToString()))
                lblCodigo.Text = oficina.cod_oficina.ToString();
            if (!string.IsNullOrEmpty(oficina.nombre))
                txtOficina.Text = oficina.nombre.Trim().ToString();
            if (!string.IsNullOrEmpty(oficina.fecha_creacion.ToString()))
                txtFechaCreacion.Text = oficina.fecha_creacion.ToShortDateString();
            if (!string.IsNullOrEmpty(oficina.cod_ciudad.ToString()))
                ddlCiudades.SelectedValue = oficina.cod_ciudad.ToString();
            if (!string.IsNullOrEmpty(oficina.direccion.Trim().ToString()))
                txtDireccion.Text  = oficina.direccion.Trim().ToString();
            if (!string.IsNullOrEmpty(oficina.telefono.Trim().ToString()))
                txtTelefono.Text = oficina.telefono.Trim().ToString();
            if (!string.IsNullOrEmpty(oficina.estado.ToString()))
            {
                if (oficina.estado == 1)
                    lblEstado.Text = "Activo";
                if (oficina.estado == 0)
                    lblEstado.Text = "Inactivo";
                if (oficina.estado == 2)
                    lblEstado.Text = "Cerrada";
            }
            if (!string.IsNullOrEmpty(oficina.cod_persona.ToString()))
                ddlEncargados.SelectedValue = oficina.cod_persona.ToString();

            if (!string.IsNullOrEmpty(oficina.centro_costo.ToString()))
                ddlCentrosCosto.SelectedValue = oficina.centro_costo.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(oficinaServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    protected void LlenarComboCiudades(DropDownList ddlCiudades)
    {
        Xpinn.Caja.Services.CiudadService ciudadService = new Xpinn.Caja.Services.CiudadService();
        Xpinn.Caja.Entities.Ciudad ciudad = new Xpinn.Caja.Entities.Ciudad();
        ddlCiudades.DataSource = ciudadService.ListarCiudad(ciudad, _usuario);
        ddlCiudades.DataTextField = "nom_ciudad";
        ddlCiudades.DataValueField = "cod_ciudad";
        ddlCiudades.DataBind();
    }

    protected void LlenarComboCentroCosto(DropDownList ddlCentrosCosto)
    {
        Xpinn.Caja.Services.CentroCostoService centroCostoService = new Xpinn.Caja.Services.CentroCostoService();
        Xpinn.Caja.Entities.CentroCosto centroCosto = new Xpinn.Caja.Entities.CentroCosto();
        ddlCentrosCosto.DataSource = centroCostoService.ListarCentroCosto(centroCosto, _usuario);
        ddlCentrosCosto.DataTextField = "nom_centro";
        ddlCentrosCosto.DataValueField = "centro_costo";
        ddlCentrosCosto.DataBind();
    }
    protected void LlenarComboEncargados(DropDownList ddlEncargados)
    {
        Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
        Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();
        ddlEncargados.DataSource = personaService.ListarPersona(persona, _usuario);
        ddlEncargados.DataTextField = "nom_persona";
        ddlEncargados.DataValueField = "cod_persona";
        ddlEncargados.DataBind();
    }


    //se busca actualizar el listado de cajas
    private void ActualizarCaja()
    {
        try
        {
            List<Xpinn.Caja.Entities.Caja> lstConsulta = new List<Xpinn.Caja.Entities.Caja>();

            Xpinn.Caja.Entities.Caja caja = new Xpinn.Caja.Entities.Caja();
            caja.cod_oficina = long.Parse(idObjeto);
            lstConsulta = cajaServicio.ListarCaja(caja, _usuario);//, (UserSession)Session["user"]);
            
            gvCajas.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvCajas.Visible = true;
                gvCajas.DataBind();
                ValidarPermisosGrilla(gvCajas);
            }
            else
            {
                gvCajas.Visible = false;
            }

            Session.Add(cajaServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        Session[oficinaServicio.CodigoOficina + ".id"] = idObjeto;
        Navegar("../../CajaFin/Caja/Nuevo.aspx");
    }
    protected void gvCajas_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvCajas.Rows[e.NewEditIndex].Cells[3].Text;
        Session[cajaServicio.CodigoCaja  + ".ids"] = id;
        Session[oficinaServicio.CodigoOficina + ".id"]=idObjeto;
        Session[cajaServicio.CodigoCaja + ".from"] = "l";
        Navegar("../../CajaFin/Caja/Nuevo.aspx");
    }

    protected void gvCajas_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvCajas.SelectedRow.Cells[3].Text;
        Session[cajaServicio.CodigoCaja + ".ids"] = id;
        Session[oficinaServicio.CodigoOficina + ".id"] = idObjeto;
        Navegar("../../CajaFin/Caja/Detalle.aspx");
    }

    protected void gvCajas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto1 = Convert.ToInt64(gvCajas.Rows[e.RowIndex].Cells[3].Text);
            Session[oficinaServicio.CodigoOficina + ".id"] = idObjeto;
            cajaServicio.EliminarCaja(idObjeto1, _usuario);
            Navegar(Pagina.Detalle);
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaServicio.GetType().Name + "L", "gvCajas_RowDeleting", ex);
        }
    }
    protected void gvCajas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void gvHorarioNormal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto1 = Convert.ToInt64(gvHorarioNormal.Rows[e.RowIndex].Cells[0].Text);
            Session[oficinaServicio.CodigoOficina + ".id"] = idObjeto;
            horarioServicio.EliminarHorarioOficina(idObjeto1, _usuario);
            Navegar(Pagina.Detalle);
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(horarioServicio.GetType().Name + "L", "gvHorarioNormal_RowDeleting", ex);
        }
    }
    protected void gvHorarioNormal_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvHorarioNormal.Rows[e.NewEditIndex].Cells[0].Text;
        Session[horarioServicio.CodigoHorarioOficina + ".idh"] = id;
        Session[horarioServicio.CodigoHorarioOficina + ".idth"] = 1;// 1: Horario Normal 
        Session[oficinaServicio.CodigoOficina + ".id"] = idObjeto;
        Session[horarioServicio.CodigoHorarioOficina + ".from"] = "l";
        Navegar("../../CajaFin/HorarioOficina/Nuevo.aspx");
    }

    protected void gvHorarioNormal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void gvHorarioAdicional_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto1 = Convert.ToInt64(gvHorarioAdicional.Rows[e.RowIndex].Cells[0].Text);
            Session[oficinaServicio.CodigoOficina + ".id"] = idObjeto;
            horarioServicio.EliminarHorarioOficina(idObjeto1, _usuario);
            Navegar(Pagina.Detalle);
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(horarioServicio.GetType().Name + "L", "gvHorarioAdicional_RowDeleting", ex);
        }
    }

    protected void gvHorarioAdicional_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvHorarioAdicional.Rows[e.NewEditIndex].Cells[0].Text;
        Session[horarioServicio.CodigoHorarioOficina + ".idh"] = id;
        Session[horarioServicio.CodigoHorarioOficina + ".idth"] = 2;// 1: Horario Adicional
        Session[oficinaServicio.CodigoOficina + ".id"] = idObjeto;
        Session[horarioServicio.CodigoHorarioOficina + ".from"] = "l";
        Navegar("../../CajaFin/HorarioOficina/Nuevo.aspx");
    }

    protected void gvHorarioAdicional_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void btnHorarioNormal_Click(object sender, EventArgs e)
    {
        Session[oficinaServicio.CodigoOficina + ".id"] = idObjeto;
        Session[horarioServicio.CodigoHorarioOficina + ".idth"] = 1;//se establece el tipo como 1:Horario Normal
        Navegar("../../CajaFin/HorarioOficina/Nuevo.aspx");
    }

    protected void btnHorarioAdi_Click(object sender, EventArgs e)
    {
        Session[oficinaServicio.CodigoOficina + ".id"] = idObjeto;
        Session[horarioServicio.CodigoHorarioOficina + ".idth"] = 2;//se establece el tipo como 2:Horario Adicional
        Navegar("../../CajaFin/HorarioOficina/Nuevo.aspx");
    }


    //se busca actualizar el listado de Horario
    private void ActualizarHorarios()
    {
        try
        {
            List<Xpinn.Caja.Entities.HorarioOficina> lstConsulta = new List<Xpinn.Caja.Entities.HorarioOficina>();

            Xpinn.Caja.Entities.HorarioOficina horario = new Xpinn.Caja.Entities.HorarioOficina();
            horario.cod_oficina = long.Parse(idObjeto);
            horario.tipo_horario = 1; //1:Horario Normal
            lstConsulta = horarioServicio.ListarHorarioOficina(horario, _usuario);//, (UserSession)Session["user"]);

            gvHorarioNormal.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvHorarioNormal.Visible = true;
                gvHorarioNormal.DataBind();
                ValidarPermisosGrilla(gvHorarioNormal);
            }
            else
            {
                gvHorarioNormal.Visible = false;
            }

            List<Xpinn.Caja.Entities.HorarioOficina> lstConsulta2= new List<Xpinn.Caja.Entities.HorarioOficina>();

            Xpinn.Caja.Entities.HorarioOficina horario2 = new Xpinn.Caja.Entities.HorarioOficina();
            horario2.cod_oficina = long.Parse(idObjeto);
            horario2.tipo_horario = 2; //2:Horario Adicional
            lstConsulta2 = horarioServicio.ListarHorarioOficina(horario2, _usuario);//, (UserSession)Session["user"]);

            gvHorarioAdicional.DataSource = lstConsulta2;

            if (lstConsulta2.Count > 0)
            {
                gvHorarioAdicional.Visible = true;
                gvHorarioAdicional.DataBind();
                ValidarPermisosGrilla(gvHorarioAdicional);
            }
            else
            {
                gvHorarioAdicional.Visible = false;
            }

            Session.Add(horarioServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(horarioServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }
}