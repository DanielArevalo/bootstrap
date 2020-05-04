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
            VisualizarOpciones(ComposicionPasivoServicio.CodigoProgramaobli, "L");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoAdelante += btnAdelante_Click;
            toolBar.eventoAtras += btnAtras_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();

            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            btnAdelante.ValidationGroup = "";
            btnAdelante.ImageUrl = "~/Images/btnReferencias.jpg";    
      
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComposicionPasivoServicio.CodigoProgramaobli, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, ComposicionPasivoServicio.CodigoProgramaobli);
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
      //  GuardarValoresConsulta(pConsulta, ComposicionPasivoServicio.CodigoPrograma);
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

            if (Session["Cod_InfFin"] != null)
            {
                lstConsulta = ComposicionPasivoServicio.Listarobligacion(Convert.ToInt64(Session["Cod_InfFin"]), (Usuario)Session["Usuario"]);
                gvLista.PageSize = 15;
                gvLista.EmptyDataText = "No se encontraron registros";
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                if (lstConsulta.Count > 0)
                {
                    gvLista.Visible = true;
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                }
                else
                {
                    gvLista.Visible = false;
                    lblTotalRegs.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComposicionPasivoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.ComposicionPasivo ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.ComposicionPasivo vComposicionPasivo = new Xpinn.FabricaCreditos.Entities.ComposicionPasivo();

        vComposicionPasivo.cod_persona = ConvertirToInt64(Session["Cod_persona"].ToString());
        vComposicionPasivo.cod_inffin = ConvertirToInt64(Session["Cod_InfFin"].ToString());
        if(txtentidad.Text.Trim() != "")
            try {
                vComposicionPasivo.acreedor = Convert.ToString(txtentidad.Text.Trim());
            } catch { }
        if(txtcupo.Text.Trim() != "")
            vComposicionPasivo.monto_otorgado = ConvertirToInt64(txtcupo.Text.Trim());
        if(txtsaldo.Text.Trim() != "")
            vComposicionPasivo.valor_cuota = ConvertirToInt64(txtsaldo.Text.Trim());
        if(txtcuota.Text.Trim() != "")
            try {
                vComposicionPasivo.periodicidad = Convert.ToString(txtcuota.Text.Trim());
            } catch { }

        return vComposicionPasivo;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.InformacionFinanciera vInformacionFinanciera = new Xpinn.FabricaCreditos.Entities.InformacionFinanciera();


            if (Session["Cod_InfFin"] == null)
            {
                //Crear registros en informacionfinanciera y en estadosfinancieros
                vInformacionFinanciera.cod_inffin = 0;
                vInformacionFinanciera.cod_persona = ConvertirToInt64(Session["Cod_persona"].ToString());
                vInformacionFinanciera.fecha = DateTime.Now;

                InformacionFinancieraServicio.CrearInformacionFinanciera(vInformacionFinanciera, (Usuario)Session["usuario"]);
                Session["Cod_InfFin"] = vInformacionFinanciera.cod_inffin;
            }

            Xpinn.FabricaCreditos.Entities.ComposicionPasivo vComposicionPasivo = new Xpinn.FabricaCreditos.Entities.ComposicionPasivo();

            if (txtIdpasivo.Text != "") vComposicionPasivo.idpasivo = ConvertirToInt64(txtIdpasivo.Text.Trim());

            vComposicionPasivo.cod_inffin = ConvertirToInt64(Session["Cod_InfFin"].ToString());

            vComposicionPasivo.entidad = (txtentidad.Text != "") ? ConvertirToInt64(txtentidad.Text.Trim()) : 0;

            if (txtcupo.Text != "") vComposicionPasivo.cupo = ConvertirToInt64(txtcupo.Text.Trim());

            if (txtsaldo.Text != "") vComposicionPasivo.saldo = ConvertirToInt64(txtsaldo.Text.Trim());

            vComposicionPasivo.cuota = (txtcuota.Text != "") ? ConvertirToInt64(txtcuota.Text.Trim()) : 0;

            vComposicionPasivo.cod_persona = ConvertirToInt64(Session["Cod_persona"].ToString());                       
    
            vComposicionPasivo = ComposicionPasivoServicio.creaobligacion(vComposicionPasivo, (Usuario)Session["usuario"]);

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

    private Int64 ConvertirToInt64(String svalor)
    {
        try
        {
            return Convert.ToInt64(svalor.Trim());
        }
        catch
        {
            return 0;
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
                txtentidad.Text = vProductosProceso.acreedor.ToString().Trim();
            if (vProductosProceso.monto_otorgado != Int64.MinValue)
                txtcupo.Text = vProductosProceso.monto_otorgado.ToString().Trim();
            if (vProductosProceso.valor_cuota != Int64.MinValue)
                txtsaldo.Text = vProductosProceso.valor_cuota.ToString().Trim();
            if (!string.IsNullOrEmpty(vProductosProceso.periodicidad))
                txtcuota.Text = vProductosProceso.periodicidad.ToString().Trim();
           
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
        txtentidad.Text = "";
        txtcupo.Text = "";
        txtsaldo.Text = "";
        txtcuota.Text = "";
        idObjeto = "";
    }

    protected void btnAtras_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/Patrimonio/Default.aspx");
    }

    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/Referencias/Lista.aspx");
    }
}