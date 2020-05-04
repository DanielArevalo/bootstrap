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
    private Xpinn.Contabilidad.Services.ParametroCtasCreditosService ParCueServicio = new Xpinn.Contabilidad.Services.ParametroCtasCreditosService();    

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ParCueServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoCopiar += btnCopiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParCueServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                CargarValoresConsulta(pConsulta, ParCueServicio.CodigoPrograma);
                if (Session[ParCueServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParCueServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService lineaServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        List<Xpinn.FabricaCreditos.Entities.LineasCredito> lstLineasCredito = new List<Xpinn.FabricaCreditos.Entities.LineasCredito>();
        Xpinn.FabricaCreditos.Entities.LineasCredito pLineasCredito = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        List<Xpinn.FabricaCreditos.Entities.Atributos> lstAtributos = new List<Xpinn.FabricaCreditos.Entities.Atributos>();
        Xpinn.FabricaCreditos.Entities.Atributos pAtributos = new Xpinn.FabricaCreditos.Entities.Atributos();
        List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred> lstCategorias = new List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred>();
        try
        {
            lstLineasCredito = lineaServicio.ListarLineasCredito(pLineasCredito, (Usuario)Session["Usuario"]);
            ddlLineaCredito.DataSource = lstLineasCredito;
            ddlLineaCredito.DataTextField = "nom_linea_credito";
            ddlLineaCredito.DataValueField = "Codigo";
            ddlLineaCredito.DataBind();

            lstAtributos = lineaServicio.ListarAtributos(pAtributos, (Usuario)Session["Usuario"]);
            ddlAtributo.DataSource = lstAtributos;
            ddlAtributo.DataTextField = "nom_atr";
            ddlAtributo.DataValueField = "cod_atr";
            ddlAtributo.DataBind();

            lstCategorias = ParCueServicio.ListarClasificacion((Usuario)Session["Usuario"]);
            ddlCategoria.DataSource = lstCategorias;
            ddlCategoria.DataTextField = "cod_categoria";
            ddlCategoria.DataValueField = "cod_categoria";
            ddlCategoria.DataBind();

            if (lstLineasCredito.Count > 0)
            {
                ddlLineaDesde.DataSource = lstLineasCredito;
                ddlLineaDesde.DataTextField = "nom_linea_credito";
                ddlLineaDesde.DataValueField = "Codigo";
                ddlLineaDesde.DataBind();

                ddlLineaHasta.DataSource = lstLineasCredito;
                ddlLineaHasta.DataTextField = "nom_linea_credito";
                ddlLineaHasta.DataValueField = "Codigo";
                ddlLineaHasta.DataBind();
                
                lstAtributos = lineaServicio.ListarAtributos(pAtributos, (Usuario)Session["Usuario"]);
                ddlAtributoDesde.DataSource = lstAtributos;
                ddlAtributoDesde.DataTextField = "nom_atr";
                ddlAtributoDesde.DataValueField = "cod_atr";
                ddlAtributoDesde.DataBind();
            }


        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(ParCueServicio.CodigoPrograma + ".id");
        GuardarValoresConsulta(pConsulta, ParCueServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ParCueServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ParCueServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[13].Text.Trim() == "2")
                {
                    switch (e.Row.Cells[6].Text.Trim())
                    {
                        case "0":
                            e.Row.Cells[6].Text = "Disminución Anterior";
                            break;
                        case "1":
                            e.Row.Cells[6].Text = "Disminución Actual";
                            break;
                        case "2":
                            e.Row.Cells[6].Text = "Aumento Provisión";
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (e.Row.Cells[6].Text.Trim())
                    {
                        case "1":
                            e.Row.Cells[6].Text = "Normal";
                            break;
                        case "2":
                            e.Row.Cells[6].Text = "Causado";
                            break;
                        case "3":
                            e.Row.Cells[6].Text = "Orden";
                            break;
                        default:
                            break;
                    }
                }
                switch (e.Row.Cells[12].Text.Trim())
                {
                    case "1":
                        e.Row.Cells[12].Text = "Débito";
                        break;
                    case "2":
                        e.Row.Cells[12].Text = "Crédito";
                        break;
                    default:
                        break;
                }
                switch (e.Row.Cells[13].Text.Trim())
                {
                    case "&nbsp;":
                        e.Row.Cells[13].Text = "Operaciones";
                        break;
                    case "0":
                        e.Row.Cells[13].Text = "Operaciones";
                        break;
                    case "1":
                        e.Row.Cells[13].Text = "Clasificación";
                        break;
                    case "2":
                        e.Row.Cells[13].Text = "Provisión";
                        break;
                    case "3":
                        e.Row.Cells[13].Text = "Causación";
                        break;
                    case "4":
                        e.Row.Cells[13].Text = "Provisión General";
                        break;
                    default:
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParCueServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ParCueServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[ParCueServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            ParCueServicio.EliminarPar_Cue_LinCred(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParCueServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(ParCueServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred> lstConsulta = new List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred>();
            lstConsulta = ParCueServicio.ListarPar_Cue_LinCred(ObtenerValores(), (Usuario)Session["usuario"]);

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

            Session.Add(ParCueServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParCueServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.Contabilidad.Entities.Par_Cue_LinCred ObtenerValores()
    {
        Xpinn.Contabilidad.Entities.Par_Cue_LinCred vParCue = new Xpinn.Contabilidad.Entities.Par_Cue_LinCred();

        if (ddlLineaCredito.SelectedValue != "")
            vParCue.cod_linea_credito = Convert.ToString(ddlLineaCredito.SelectedValue);
        if (ddlAtributo.SelectedValue != "")
            vParCue.cod_atr = Convert.ToInt32(ddlAtributo.SelectedValue);
        if (ddlTipoCuenta.SelectedValue != "")
            vParCue.tipo_cuenta = Convert.ToInt32(ddlTipoCuenta.SelectedValue);
        if (ddlTipo.SelectedValue != "")
            vParCue.tipo = Convert.ToInt32(ddlTipo.SelectedValue);
        if (ddlCategoria.SelectedValue != "")
            vParCue.cod_categoria = Convert.ToString(ddlCategoria.SelectedValue);

        return vParCue;
    }



    protected void btnCopiar_Click(object sender, EventArgs e)
    {
        lblMsj.Text = "";
        mpeCopiar.Show();
    }


    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        mpeCopiar.Hide();    
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
       lblMsj.Text = "";
       if (ddlLineaDesde.SelectedValue == ddlLineaHasta.SelectedValue)
       {
           lblMsj.Text = "No se puede copiar los registros a la misma linea";
       }
       else
       {
           string lineaDesde = "",LineaHasta = "";

           lineaDesde = ddlLineaDesde.SelectedValue;
           LineaHasta = ddlLineaHasta.SelectedValue;

           //Consultar los datos por copiar
           List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred> lstConsulta = new List<Xpinn.Contabilidad.Entities.Par_Cue_LinCred>();
           Xpinn.Contabilidad.Entities.Par_Cue_LinCred vParCue = new Xpinn.Contabilidad.Entities.Par_Cue_LinCred();
           vParCue.cod_linea_credito = ddlLineaDesde.SelectedValue;
           if (ddlAtributoDesde.SelectedValue != "")
               vParCue.cod_atr = Convert.ToInt32(ddlAtributoDesde.SelectedValue);
           if (ddlTipoDesde.SelectedValue != "")
               vParCue.tipo = Convert.ToInt32(ddlTipoDesde.SelectedValue);
           lstConsulta = ParCueServicio.ListarPar_Cue_LinCred(vParCue, (Usuario)Session["usuario"]);
           if (lstConsulta.Count > 0)
           {
                //Insertar los datos
                int? cod_atr = null;
                if (ddlAtributoDesde.SelectedValue != "")
                    cod_atr = Convert.ToInt32(ddlAtributoDesde.SelectedValue);
                int? tipo = null;
                if (ddlTipoDesde.SelectedValue != "")
                    tipo = Convert.ToInt32(ddlTipoDesde.SelectedValue);

               ParCueServicio.CopiarPar_Cue_LinCred(ddlLineaDesde.SelectedValue, ddlLineaHasta.SelectedValue, cod_atr, tipo, (Usuario)Session["usuario"]);
               ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
               mpeCopiar.Hide();
               Actualizar();
           }
       }
    }


}