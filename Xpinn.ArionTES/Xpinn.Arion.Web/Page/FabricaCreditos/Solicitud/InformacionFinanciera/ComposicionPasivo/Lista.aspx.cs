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
    private Xpinn.FabricaCreditos.Services.ComposicionPasivoService ComposicionPasivoServicio = new Xpinn.FabricaCreditos.Services.ComposicionPasivoService();
    private Xpinn.FabricaCreditos.Services.InformacionFinancieraService InformacionFinancieraServicio = new Xpinn.FabricaCreditos.Services.InformacionFinancieraService();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ComposicionPasivoServicio.CodigoPrograma, "L");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoAdelante += btnAdelante_Click;
            toolBar.eventoAtras += btnAtras_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();

            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            btnAdelante.ValidationGroup = "";

            if (Session["TipoCredito"].ToString() == "M")
                btnAdelante.ImageUrl = "~/Images/btnBalance.jpg";    
            else
                btnAdelante.ImageUrl = "~/Images/btnCrearSolicitud.jpg";    
      
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComposicionPasivoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, ComposicionPasivoServicio.CodigoPrograma);
                if (Session[ComposicionPasivoServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComposicionPasivoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ComposicionPasivoServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ComposicionPasivoServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ComposicionPasivoServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComposicionPasivoServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ComposicionPasivoServicio.CodigoPrograma + ".id"] = id;
        Edicion();
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;//gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ComposicionPasivoServicio.CodigoPrograma + ".id"] = id;
       
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long id = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            Int64 Cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            Int64 Cod_InfFin = Convert.ToInt64(Session["Cod_InfFin"].ToString());
            ComposicionPasivoServicio.EliminarComposicionPasivo(id, (Usuario)Session["usuario"], Cod_persona, Cod_InfFin);
            Actualizar();

        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComposicionPasivoServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(ComposicionPasivoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.ComposicionPasivo> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.ComposicionPasivo>();
            lstConsulta = ComposicionPasivoServicio.ListarComposicionPasivo(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            gvLista.EmptyDataText = "No se encontraron registros";
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                txtTotalMontoOtorgado.Text = lstConsulta.Sum(item => item.monto_otorgado).ToString();
                txtTotalValorCuota.Text = lstConsulta.Sum(item => item.valor_cuota).ToString();
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                txtTotalMontoOtorgado.Text = "0";
                txtTotalValorCuota.Text = "0";
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ComposicionPasivoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComposicionPasivoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.ComposicionPasivo ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.ComposicionPasivo vComposicionPasivo = new Xpinn.FabricaCreditos.Entities.ComposicionPasivo();

        vComposicionPasivo.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        vComposicionPasivo.cod_inffin = Convert.ToInt64(Session["Cod_InfFin"].ToString());
    if(txtAcreedor.Text.Trim() != "")
        vComposicionPasivo.acreedor = Convert.ToString(txtAcreedor.Text.Trim());
    if(txtMonto_otorgado.Text.Trim() != "")
        vComposicionPasivo.monto_otorgado = Convert.ToInt64(txtMonto_otorgado.Text.Trim());
    if(txtValor_cuota.Text.Trim() != "")
        vComposicionPasivo.valor_cuota = Convert.ToInt64(txtValor_cuota.Text.Trim());
    if(txtPeriodicidad.Text.Trim() != "")
        vComposicionPasivo.periodicidad = Convert.ToString(txtPeriodicidad.Text.Trim());
    if(txtCuota.Text.Trim() != "")
        vComposicionPasivo.cuota = Convert.ToInt64(txtCuota.Text.Trim());
    if(txtPlazo.Text.Trim() != "")
        vComposicionPasivo.plazo = Convert.ToInt64(txtPlazo.Text.Trim());

        return vComposicionPasivo;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.ComposicionPasivo vComposicionPasivo = new Xpinn.FabricaCreditos.Entities.ComposicionPasivo();

            if (idObjeto != "")
                vComposicionPasivo = ComposicionPasivoServicio.ConsultarComposicionPasivo(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            if (txtIdpasivo.Text != "") vComposicionPasivo.idpasivo = Convert.ToInt64(txtIdpasivo.Text.Trim());
            vComposicionPasivo.cod_inffin = Convert.ToInt64(Session["Cod_InfFin"].ToString());
            vComposicionPasivo.acreedor = (txtAcreedor.Text != "") ? Convert.ToString(txtAcreedor.Text.Trim()) : String.Empty;
            if (txtMonto_otorgado.Text != "") vComposicionPasivo.monto_otorgado = Convert.ToInt64(txtMonto_otorgado.Text.Trim());
            if (txtValor_cuota.Text != "") vComposicionPasivo.valor_cuota = Convert.ToInt64(txtValor_cuota.Text.Trim());
            vComposicionPasivo.periodicidad = (txtPeriodicidad.Text != "") ? Convert.ToString(txtPeriodicidad.Text.Trim()) : String.Empty;
            if (txtCuota.Text != "") vComposicionPasivo.cuota = Convert.ToInt64(txtCuota.Text.Trim());
            if (txtPlazo.Text != "") vComposicionPasivo.plazo = Convert.ToInt64(txtPlazo.Text.Trim());
            vComposicionPasivo.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
                       
            if (idObjeto != "")
            {
                vComposicionPasivo.idpasivo = Convert.ToInt64(idObjeto);
                ComposicionPasivoServicio.ModificarComposicionPasivo(vComposicionPasivo, (Usuario)Session["usuario"]);

            }
            else
            {
                vComposicionPasivo = ComposicionPasivoServicio.CrearComposicionPasivo(vComposicionPasivo, (Usuario)Session["usuario"]);
                idObjeto = vComposicionPasivo.idpasivo.ToString();
            }

            Session[ComposicionPasivoServicio.CodigoPrograma + ".id"] = idObjeto;
            Actualizar();
            Borrar();
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComposicionPasivoServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    private void Edicion()
    {

        try
        {
            if (Session[ComposicionPasivoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ComposicionPasivoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ComposicionPasivoServicio.CodigoPrograma, "A");

            //Page_Load
            if (Session[ComposicionPasivoServicio.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[ComposicionPasivoServicio.CodigoPrograma + ".id"].ToString();
                if (idObjeto != null)
                {
                    ObtenerDatos(idObjeto);
                    Session.Remove(ComposicionPasivoServicio.CodigoPrograma + ".id");
                }
            }
           
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComposicionPasivoServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.ComposicionPasivo vProductosProceso = new Xpinn.FabricaCreditos.Entities.ComposicionPasivo();
            vProductosProceso = ComposicionPasivoServicio.ConsultarComposicionPasivo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vProductosProceso.idpasivo != Int64.MinValue)
                txtIdpasivo.Text = vProductosProceso.idpasivo.ToString().Trim();
            if (vProductosProceso.cod_inffin != Int64.MinValue)
                txtCod_inffin.Text = vProductosProceso.cod_inffin.ToString().Trim();
            if (!string.IsNullOrEmpty(vProductosProceso.acreedor))
                txtAcreedor.Text = vProductosProceso.acreedor.ToString().Trim();
            if (vProductosProceso.monto_otorgado != Int64.MinValue)
                txtMonto_otorgado.Text = vProductosProceso.monto_otorgado.ToString().Trim();
            if (vProductosProceso.valor_cuota != Int64.MinValue)
                txtValor_cuota.Text = vProductosProceso.valor_cuota.ToString().Trim();
            if (!string.IsNullOrEmpty(vProductosProceso.periodicidad))
                txtPeriodicidad.Text = vProductosProceso.periodicidad.ToString().Trim();
            if (vProductosProceso.cuota != Int64.MinValue)
                txtCuota.Text = vProductosProceso.cuota.ToString().Trim();
            if (vProductosProceso.plazo != Int64.MinValue)
                txtPlazo.Text = vProductosProceso.plazo.ToString().Trim();      
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComposicionPasivoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }       
    }

    private void Borrar()
    {
        txtIdpasivo.Text = "";
        txtCod_inffin.Text = "";
        txtAcreedor.Text = "";
        txtMonto_otorgado.Text = "";
        txtValor_cuota.Text = "";
        txtPeriodicidad.Text = "";
        txtCuota.Text = "";
        txtPlazo.Text = "";
        idObjeto = "";
    }

    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/Productos/Default.aspx");
    }

    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Session["GarantiaReal"] = "0";
        if (Session["TipoCredito"].ToString() == "M")
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/InformacionFinancieraNegocio/Blance.aspx");
        else
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/DatosSolicitud/SolicitudCredito.aspx");
    }
}