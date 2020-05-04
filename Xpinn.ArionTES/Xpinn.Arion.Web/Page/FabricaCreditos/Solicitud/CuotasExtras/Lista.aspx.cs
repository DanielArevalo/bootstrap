using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.CuotasExtrasService CuotasExtrasServicio = new Xpinn.FabricaCreditos.Services.CuotasExtrasService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CuotasExtrasServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasExtrasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, CuotasExtrasServicio.CodigoPrograma);
                if (Session[CuotasExtrasServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasExtrasServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, CuotasExtrasServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, CuotasExtrasServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, CuotasExtrasServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasExtrasServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[CuotasExtrasServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[CuotasExtrasServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            CuotasExtrasServicio.EliminarCuotasExtras(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasExtrasServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
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
            BOexcepcion.Throw(CuotasExtrasServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.CuotasExtras> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.CuotasExtras>();
            lstConsulta = CuotasExtrasServicio.ListarCuotasExtras(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(CuotasExtrasServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuotasExtrasServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.CuotasExtras ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.CuotasExtras vCuotasExtras = new Xpinn.FabricaCreditos.Entities.CuotasExtras();

    if(txtNumero_radicacion.Text.Trim() != "")
        vCuotasExtras.numero_radicacion = Convert.ToInt64(txtNumero_radicacion.Text.Trim());
    if(txtFecha_pago.Text.Trim() != "")
        vCuotasExtras.fecha_pago = Convert.ToDateTime(txtFecha_pago.Text.Trim());
    if(txtValor.Text.Trim() != "")
        vCuotasExtras.valor = Convert.ToInt64(txtValor.Text.Trim());
    if(txtValor_capital.Text.Trim() != "")
        vCuotasExtras.valor_capital = Convert.ToInt64(txtValor_capital.Text.Trim());
    if(txtValor_interes.Text.Trim() != "")
        vCuotasExtras.valor_interes = Convert.ToInt64(txtValor_interes.Text.Trim());
    if(txtSaldo_capital.Text.Trim() != "")
        vCuotasExtras.saldo_capital = Convert.ToInt64(txtSaldo_capital.Text.Trim());
    if(txtSaldo_interes.Text.Trim() != "")
        vCuotasExtras.saldo_interes = Convert.ToInt64(txtSaldo_interes.Text.Trim());
    if(txtForma_pago.Text.Trim() != "")
        vCuotasExtras.forma_pago = Convert.ToString(txtForma_pago.Text.Trim());

        return vCuotasExtras;
    }
}