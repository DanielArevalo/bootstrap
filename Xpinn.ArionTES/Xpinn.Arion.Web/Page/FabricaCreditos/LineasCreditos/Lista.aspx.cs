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
    private Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
    String operacion = "";
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(LineasCreditoServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            //toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, LineasCreditoServicio.CodigoPrograma);
                if (Session[LineasCreditoServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {        
        Session.Remove(LineasCreditoServicio.CodigoPrograma + ".id");
      //  GuardarValoresConsulta(pConsulta, LineasCreditoServicio.CodigoPrograma);
        operacion = "N";
        Navegar(Pagina.Nuevo);
        LimpiarFormulario();
        LimpiarValoresConsulta(pConsulta, LineasCreditoServicio.CodigoPrograma);
        
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, LineasCreditoServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, LineasCreditoServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[LineasCreditoServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[LineasCreditoServicio.CodigoPrograma + ".id"] = id;
        operacion = null;
        Navegar(Pagina.Editar);

    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = Convert.ToString(e.Keys[0]);
            LineasCreditoServicio.EliminarLineasCredito(Convert.ToString(id), (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.LineasCredito> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.LineasCredito>();
            lstConsulta = LineasCreditoServicio.ListarLineasCredito(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
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

            Session.Add(LineasCreditoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasCreditoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.LineasCredito ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.LineasCredito vLineasCredito = new Xpinn.FabricaCreditos.Entities.LineasCredito();

    if (txtCod_linea_credito1.Text.Trim() != "")
        vLineasCredito.cod_linea_credito = Convert.ToString(txtCod_linea_credito1.Text.Trim());
    if(txtNombre1.Text.Trim() != "")
        vLineasCredito.nombre = Convert.ToString(txtNombre1.Text.Trim());
    //if(txtTipo_linea.Text.Trim() != "")
    //    vLineasCredito.tipo_linea = Convert.ToInt64(txtTipo_linea.Text.Trim());
    //if(txtTipo_liquidacion.Text.Trim() != "")
    //    vLineasCredito.tipo_liquidacion = Convert.ToInt64(txtTipo_liquidacion.Text.Trim());
    //if(txtTipo_cupo.Text.Trim() != "")
    //    vLineasCredito.tipo_cupo = Convert.ToInt64(txtTipo_cupo.Text.Trim());
    //if(txtRecoge_saldos.Text.Trim() != "")
    //    vLineasCredito.recoge_saldos = Convert.ToInt64(txtRecoge_saldos.Text.Trim());
    //if(txtCobra_mora.Text.Trim() != "")
    //    vLineasCredito.cobra_mora = Convert.ToInt64(txtCobra_mora.Text.Trim());
    //if(txtTipo_refinancia.Text.Trim() != "")
    //    vLineasCredito.tipo_refinancia = Convert.ToInt64(txtTipo_refinancia.Text.Trim());
    //if(txtMinimo_refinancia.Text.Trim() != "")
    //    vLineasCredito.minimo_refinancia = Convert.ToInt64(txtMinimo_refinancia.Text.Trim());
    //if(txtMaximo_refinancia.Text.Trim() != "")
    //    vLineasCredito.maximo_refinancia = Convert.ToInt64(txtMaximo_refinancia.Text.Trim());
    //if(txtManeja_pergracia.Text.Trim() != "")
    //    vLineasCredito.maneja_pergracia = Convert.ToString(txtManeja_pergracia.Text.Trim());
    //if(txtPeriodo_gracia.Text.Trim() != "")
    //    vLineasCredito.periodo_gracia = Convert.ToInt64(txtPeriodo_gracia.Text.Trim());
    //if(txtTipo_periodic_gracia.Text.Trim() != "")
    //    vLineasCredito.tipo_periodic_gracia = Convert.ToString(txtTipo_periodic_gracia.Text.Trim());
    //if(txtModifica_datos.Text.Trim() != "")
    //    vLineasCredito.modifica_datos = Convert.ToString(txtModifica_datos.Text.Trim());
    //if(txtModifica_fecha_pago.Text.Trim() != "")
    //    vLineasCredito.modifica_fecha_pago = Convert.ToString(txtModifica_fecha_pago.Text.Trim());
    //if(txtGarantia_requerida.Text.Trim() != "")
    //    vLineasCredito.garantia_requerida = Convert.ToString(txtGarantia_requerida.Text.Trim());
    //if(txtTipo_capitalizacion.Text.Trim() != "")
    //    vLineasCredito.tipo_capitalizacion = Convert.ToInt64(txtTipo_capitalizacion.Text.Trim());
    //if(txtCuotas_extras.Text.Trim() != "")
    //    vLineasCredito.cuotas_extras = Convert.ToInt64(txtCuotas_extras.Text.Trim());
    //if(txtCod_clasifica.Text.Trim() != "")
    //    vLineasCredito.cod_clasifica = Convert.ToInt64(txtCod_clasifica.Text.Trim());
    //if(txtNumero_codeudores.Text.Trim() != "")
    //    vLineasCredito.numero_codeudores = Convert.ToInt64(txtNumero_codeudores.Text.Trim());
    //if(txtCod_moneda.Text.Trim() != "")
    //    vLineasCredito.cod_moneda = Convert.ToInt64(txtCod_moneda.Text.Trim());
    //if(txtPorc_corto.Text.Trim() != "")
    //    vLineasCredito.porc_corto = Convert.ToInt64(txtPorc_corto.Text.Trim());
    //if(txtTipo_amortiza.Text.Trim() != "")
    //    vLineasCredito.tipo_amortiza = Convert.ToInt64(txtTipo_amortiza.Text.Trim());
    //if(txtEstado.Text.Trim() != "")
    //    vLineasCredito.estado = Convert.ToInt64(txtEstado.Text.Trim());

        return vLineasCredito;
    }
}