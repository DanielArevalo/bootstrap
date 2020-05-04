﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    LiquidacionNominaService _liquidacionServices = new LiquidacionNominaService();
    bool _esNuevaLiquidacion;
    long? _consecutivoLiquidacion;
    #region Eventos Iniciales

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_liquidacionServices.CodigoProgramaAntInd, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensajeBorrar.eventoClick += CtlMensajeBorrar_eventoClick;
            toolBar.eventoNuevo += (s, evt) => {
                Session.Remove(_liquidacionServices.CodigoProgramaAntInd + ".id");
                Navegar(Pagina.Nuevo);
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionServices.CodigoProgramaAntInd, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            InicializarPagina();
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.NominaEmpleado, ddlTipoNomina);
        LlenarListasDesplegables(TipoLista.CentroCostos, ddlCentroCosto);
    }


    #endregion


    #region Eventos Intermedios GridView - Botonera


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLista.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvLista.SelectedRow.Cells[2].Text);

        Session[_liquidacionServices.CodigoProgramaAntInd + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        long idBorrar = Convert.ToInt64(gvLista.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

        ViewState.Add("idBorrar", idBorrar);

        ctlMensajeBorrar.MostrarMensaje("Seguro que deseas eliminar esta registro?");
    }

    void CtlMensajeBorrar_eventoClick(object sender, EventArgs e)
    {
        if (ViewState["idBorrar"] != null)
        {
            long idBorrar = Convert.ToInt64(ViewState["idBorrar"]);

            try
            {
               // _liquidacionServices.EliminarLiquidacionPrima(idBorrar, Usuario);
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtCodigoLiquidacion.Text = string.Empty;
       
        ddlTipoNomina.SelectedIndex = 0;
        ddlCentroCosto.SelectedIndex = 0;
    }

    void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }


    #endregion


    #region Métodos Ayuda


    void Actualizar()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();

            List<LiquidacionNomina> lstLiquidacion = _liquidacionServices.ListarAnticiposNominaInd(filtro, Usuario);
             
            if (lstLiquidacion.Count > 0)
            {
                lblTotalRegs.Text = "Se encontraron " + lstLiquidacion.Count + " registros!.";
            }
            else
            {
                lblTotalRegs.Text = "Su consulta no obtuvo ningún resultado!.";
            }

            gvLista.DataSource = lstLiquidacion;
            gvLista.DataBind();
        }
        catch (Exception ex)
        {
            VerError("Error al actualizar la grilla, " + ex.Message);
        }
    }


    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtCodigoLiquidacion.Text))
        {
            filtro += " and liq.consecutivo = " + txtCodigoLiquidacion.Text.Trim();
        }

      
        if (!string.IsNullOrWhiteSpace(ddlCentroCosto.SelectedValue))
        {
            filtro += " and liq.CODIGOCENTROCOSTO = " + ddlCentroCosto.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue))
        {
            filtro += " and liq.CODIGONOMINA = " + ddlTipoNomina.SelectedValue;
        }

        filtro += " and an.tipo =  1";

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            StringHelper stringHelper = new StringHelper();
            filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);
        }

        return filtro;
    }


    #endregion


}