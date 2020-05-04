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

public partial class Lista : GlobalWeb
{
    Xpinn.Caja.Services.CajeroService cajeroServicio = new Xpinn.Caja.Services.CajeroService();
    Xpinn.Caja.Data.CajeroData cajeroData = new Xpinn.Caja.Data.CajeroData();
    Xpinn.Caja.Services.OficinaService oficinaService = new Xpinn.Caja.Services.OficinaService();
    Usuario user = new Usuario();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(cajeroServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajeroServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboOficinas(ddlOficinas);
                user = (Usuario)Session["usuario"];
                Actualizar((int)user.cod_oficina);

                if (Session[cajeroServicio.GetType().Name + ".consulta"] != null)
                    Actualizar(int.Parse(ddlOficinas.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajeroServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session[oficinaService.CodigoOficina + ".IdO"] = ddlOficinas.SelectedValue;
        Navegar(Pagina.Nuevo);
    }

    private Xpinn.Caja.Entities.Cajero ObtenerValores(int idOf)
    {
        Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();
        cajero.cod_oficina = idOf;
        return cajero;
    }

    public void Actualizar(int idOf)
    {
        try
        {
            List<Xpinn.Caja.Entities.Cajero> lstConsulta = new List<Xpinn.Caja.Entities.Cajero>();
            lstConsulta = cajeroServicio.ListarCajero(ObtenerValores(idOf), (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);

            gvCajeros.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvCajeros.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvCajeros.DataBind();
                ValidarPermisosGrilla(gvCajeros);
            }
            else
            {
                gvCajeros.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(cajeroServicio.GetType().Name + ".consulta", 1);
            ddlOficinas.SelectedValue = idOf.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajeroServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }


    protected void gvCajeros_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvCajeros.Rows[e.NewEditIndex].Cells[3].Text;
        Session[cajeroServicio.CodigoCajero + ".id"] = id;
        Session[oficinaService.CodigoOficina + ".IdO"] = ddlOficinas.SelectedValue;
        Session["Cajero.from"] = "l";
        Navegar(Pagina.Nuevo);
    }

    protected void gvCajeros_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvCajeros.SelectedRow.Cells[3].Text;
        Session[cajeroServicio.CodigoCajero + ".id"] = id;
        Session[oficinaService.CodigoOficina + ".IdO"] = ddlOficinas.SelectedValue;
        Navegar(Pagina.Detalle);
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

    protected void ddlOficinas_SelectedIndexChanged(object sender, EventArgs e)
    {
        int oficinaId = int.Parse(ddlOficinas.SelectedValue);
        Actualizar(oficinaId);
    }

    protected void gvCajeros_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        Usuario usuari = (Usuario)Session["usuario"];

        try
        {
            if (long.Parse(ddlOficinas.SelectedValue) == usuari.cod_oficina)
            {
                long idObjeto1 = Convert.ToInt64(gvCajeros.Rows[e.RowIndex].Cells[3].Text);
                cajeroServicio.EliminarCajero(idObjeto1, (Usuario)Session["usuario"]);
                Navegar(Pagina.Lista);
            }
            else
                VerError("El Usuario no pertenece a la oficina seleccionada");

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajeroServicio.GetType().Name + "L", "gvLista_RowDeleting", ex);
        }

    }

    protected void gvCajeros_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }
}