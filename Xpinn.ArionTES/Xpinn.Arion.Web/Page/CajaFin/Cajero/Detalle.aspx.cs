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
    Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Data.CajeroData cajaroData = new Xpinn.Caja.Data.CajeroData();
    Xpinn.Caja.Services.OficinaService oficinaService = new Xpinn.Caja.Services.OficinaService();
    string idObjeto2;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(cajeroService.CodigoPrograma, "D");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoEliminar += btnEliminar_Click;
            toolBar.eventoEditar += btnEditar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;

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
                AsignarEventoConfirmar();
                //se inicializa el combo de Usuarios, Cajas
                LlenarComboUsuarios(ddlUsuarios);
                LlenarComboCajas(ddlCajas);

                if (Session[cajeroService.CodigoCajero + ".id"] != null)
                {
                    idObjeto = Session[cajeroService.CodigoCajero + ".id"].ToString();
                    Session.Remove(cajeroService.CodigoCajero + ".id");
                    ObtenerDatos(idObjeto);
                }
            }

            idObjeto2 = Session[oficinaService.CodigoOficina + ".IdO"].ToString();
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajeroService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    protected void LlenarComboUsuarios(DropDownList ddlUsuarios)
    {
        Xpinn.Caja.Services.UsuariosService usuariosServicio = new Xpinn.Caja.Services.UsuariosService();
        Xpinn.Caja.Entities.Usuarios usuarioE = new Xpinn.Caja.Entities.Usuarios();
        usuarioE.cod_oficina = long.Parse(Session[oficinaService.CodigoOficina + ".IdO"].ToString());
        ddlUsuarios.DataSource = usuariosServicio.ListarUsuariosXOficina2(usuarioE, (Usuario)Session["usuario"]);
        ddlUsuarios.DataTextField = "nombre";
        ddlUsuarios.DataValueField = "codusuario";
        ddlUsuarios.DataBind();
    }

    protected void LlenarComboCajas(DropDownList ddlCajas)
    {
        Xpinn.Caja.Services.CajaService cajaServicio = new Xpinn.Caja.Services.CajaService();
        Xpinn.Caja.Entities.Caja caja = new Xpinn.Caja.Entities.Caja();
        ddlCajas.DataSource = cajaServicio.ListarComboCaja(caja, (Usuario)Session["usuario"]);
        ddlCajas.DataTextField = "nombre";
        ddlCajas.DataValueField = "cod_caja";
        ddlCajas.DataBind();
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session[oficinaService.CodigoOficina + ".IdO"] = idObjeto2;
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../CajaFin/Cajero/Lista.aspx");
    }

    protected void btnEliminar_Click(object sender, ImageClickEventArgs e)
    {
        Usuario usuari = (Usuario)Session["usuario"];

        try
        {
            if (long.Parse(Session[oficinaService.CodigoOficina + ".IdO"].ToString()) == usuari.cod_oficina)
            {
                cajeroService.EliminarCajero(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
                Navegar("../../CajaFin/Cajero/Lista.aspx");
            }
            else
                VerError("El Usuario no hace parte de esta oficina, no esta permitida esta operación");
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajeroService.CodigoCajero + "C", "btnEliminar_Click", ex);
        }
    }

    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        Session[oficinaService.CodigoOficina + ".IdO"] = idObjeto2;
        Session[cajeroService.CodigoCajero + ".id"] = idObjeto;
        Navegar(Pagina.Nuevo);
    }


    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();
            if (pIdObjeto != null)
                cajero = cajeroService.ConsultarCajero(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(cajero.cod_cajero.ToString()))
                lblCodigo.Text = cajero.cod_cajero.ToString().Trim();
            if (!string.IsNullOrEmpty(cajero.cod_persona.ToString()))
                ddlUsuarios.SelectedValue = cajero.cod_persona.ToString();
            if (!string.IsNullOrEmpty(cajero.cod_caja.ToString()))
                ddlCajas.SelectedValue = cajero.cod_caja.ToString();

            if (cajero.fecha_ingreso.ToShortDateString() != "01/01/0001")
                txtFechaIngreso.Text = cajero.fecha_ingreso.ToShortDateString();
            else
                txtFechaIngreso.Text = DateTime.Now.ToShortDateString();

            if (cajero.fecha_retiro.ToShortDateString() != "01/01/0001")
                txtFechaRetiro.Text = cajero.fecha_retiro.ToShortDateString();
            else
                txtFechaRetiro.Text = "";

            if (!string.IsNullOrEmpty(cajero.estado.ToString()))
            {
                if (cajero.estado == 1)
                    chkEstado.Checked = true;
                else
                    chkEstado.Checked = false;
            }
            if (cajero.identificacion != null)
                if (!string.IsNullOrEmpty(cajero.identificacion.ToString()))
                    txtIdentificacion.Text = HttpUtility.HtmlDecode(cajero.identificacion.ToString().Trim());

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajeroService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

}