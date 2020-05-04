using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    Xpinn.Caja.Services.OficinaService cajaServicio = new Xpinn.Caja.Services.OficinaService();
    Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(cajaServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaServicio.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, cajaServicio.GetType().Name);
                if (Session[cajaServicio.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaServicio.GetType().Name + "L", "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, cajaServicio.GetType().Name);
            Navegar(Pagina.Nuevo);
            //Response.Redirect("Nuevo.aspx");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaServicio.GetType().Name + "L", "btnNuevo_Click", ex);
        }

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GuardarValoresConsulta(pConsulta, cajaServicio.GetType().Name);
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaServicio.GetType().Name + "L", "btnConsultar_Click", ex);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, cajaServicio.GetType().Name);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnEliminar");
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[3].Text;
        Session[cajaServicio.CodigoOficina + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaServicio.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Caja.Entities.Oficina> lstConsulta = new List<Xpinn.Caja.Entities.Oficina>();
            lstConsulta = cajaServicio.ListarOficina(ObtenerValores(), (Usuario) Session["usuario"]);

            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(cajaServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    private Xpinn.Caja.Entities.Oficina ObtenerValores()
    {
        Xpinn.Caja.Entities.Oficina Oficina = new Xpinn.Caja.Entities.Oficina();

        Oficina.cod_oficina = txtCodigo.Text.Trim();
        Oficina.nombre = txtOficina.Text.Trim();

        return Oficina;
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        Session[cajaServicio.CodigoOficina + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto1 = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[3].Text);
            oficina = cajaServicio.ConsultarUsersXOficina(idObjeto1, (Usuario)Session["usuario"]);

            if (oficina.conteo == 0)
            {
                cajaServicio.EliminarOficina(idObjeto1, (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);
                Navegar(Pagina.Lista);
            }
            else
                VerError("La Oficina que esta tratando de Eliminar tiene Usuarios Asociados, No Puede realizar esta operación ");
            
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cajaServicio.GetType().Name + "L", "gvLista_RowDeleting", ex);
        }
    }

}