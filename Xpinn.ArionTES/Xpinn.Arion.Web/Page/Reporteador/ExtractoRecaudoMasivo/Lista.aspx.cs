﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;

partial class Lista : GlobalWeb
{
    private Xpinn.Tesoreria.Services.RecaudosMasivosService RecaudosMasivosServicio = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
 

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(RecaudosMasivosServicio.CodigoProgramaConsulta, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarEmpresa();
                CargarValoresConsulta(pConsulta, RecaudosMasivosServicio.CodigoProgramaConsulta);
                if (Session[RecaudosMasivosServicio.CodigoProgramaConsulta + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if(txtNumIdentifi.Text == "")
        {
            GuardarValoresConsulta(pConsulta, RecaudosMasivosServicio.CodigoProgramaConsulta);
            Actualizar();
        }
        else
        {

            RecaudosMasivos vRecaudosMasivos = new RecaudosMasivos();
            if (txtNumIdentifi.Text.Trim() != "")
                vRecaudosMasivos.identificacion = Convert.ToString(txtNumIdentifi.Text.Trim());
            if (txtNumeroRecaudo.Text.Trim() != "")
                vRecaudosMasivos.numero_recaudo = Convert.ToInt64(txtNumeroRecaudo.Text.Trim());
            if (txtFechaPeriodo.TieneDatos == true)
                vRecaudosMasivos.periodo_corte = txtFechaPeriodo.ToDateTime;
            if (ddlEmpresa.SelectedValue != "")
                vRecaudosMasivos.cod_empresa = Convert.ToInt64(ddlEmpresa.SelectedValue);                        
            //Aca determinamos el estado del recaudo
            if (NomGenerada.Checked == true)
            {
                vRecaudosMasivos.estado = "1";
            }
            else
            {
                vRecaudosMasivos.estado = "2";
            }
            Session["RvRecaudosMasivos"] = vRecaudosMasivos;
            Session["Estadomasi"] = vRecaudosMasivos;
            Navegar(Pagina.Detalle);

            
        }

      
        
        
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, RecaudosMasivosServicio.CodigoProgramaConsulta);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {     
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[RecaudosMasivosServicio.CodigoProgramaConsulta + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RecaudosMasivos vRecaudosMasivos = new RecaudosMasivos();
        if (txtNumIdentifi.Text.Trim() != "")
            vRecaudosMasivos.identificacion = Convert.ToString(txtNumIdentifi.Text.Trim());
        if (txtNumeroRecaudo.Text.Trim() != "")
            vRecaudosMasivos.numero_recaudo = Convert.ToInt64(txtNumeroRecaudo.Text.Trim());
        if (txtFechaPeriodo.TieneDatos == true)
            vRecaudosMasivos.periodo_corte = txtFechaPeriodo.ToDateTime;
        if (ddlEmpresa.SelectedValue != "")
            vRecaudosMasivos.cod_empresa = Convert.ToInt64(ddlEmpresa.SelectedValue);
        //Aca determinamos el estado del recaudo
        if (NomGenerada.Checked == true)
        {
            vRecaudosMasivos.estado = "1";
        }
        else
        {
            vRecaudosMasivos.estado = "2";
        }

        //falta pasar el objeto 
        // Session.Add("RvRecaudosMasivos", vRecaudosMasivos);
        Session["Estadomasi"] = vRecaudosMasivos;

        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[RecaudosMasivosServicio.CodigoProgramaConsulta + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            try
            {
                RecaudosMasivosServicio.EliminarRecaudosMasivos(id, (Usuario)Session["usuario"]);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta, "gvLista_PageIndexChanging", ex);
        }
    }


    /// <summary>
    /// EXTRACTO
    /// </summary>
    private void Actualizar()
    {
        try { 
            List<RecaudosMasivos> lstConsulta = new List<RecaudosMasivos>();           
            lstConsulta = RecaudosMasivosServicio.ListarRecaudoExtracto(ObtenerValores(), (Usuario)Session["usuario"]); 
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

            Session.Add(RecaudosMasivosServicio.CodigoProgramaConsulta + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta, "Actualizar", ex);
        }
    }

    private RecaudosMasivos ObtenerValores()
    {
        RecaudosMasivos vRecaudosMasivos = new RecaudosMasivos();
        if (txtNumIdentifi.Text.Trim() != "")
            vRecaudosMasivos.identificacion = Convert.ToString(txtNumIdentifi.Text.Trim());
        if (txtNumeroRecaudo.Text.Trim() != "")
            vRecaudosMasivos.numero_recaudo = Convert.ToInt64(txtNumeroRecaudo.Text.Trim());
        if (txtFechaPeriodo.TieneDatos == true)
            vRecaudosMasivos.periodo_corte = txtFechaPeriodo.ToDateTime;
        if (ddlEmpresa.SelectedValue != "")
            vRecaudosMasivos.cod_empresa = Convert.ToInt64(ddlEmpresa.SelectedValue);  
        //Aca determinamos el estado del recaudo
        if(NomGenerada.Checked == true)
        {
            vRecaudosMasivos.estado = "1";
        }
        else
        {
            vRecaudosMasivos.estado = "2";
        }  
        return vRecaudosMasivos;
    }

    private void CargarEmpresa()
    {
        try
        {
            Xpinn.Tesoreria.Services.RecaudosMasivosService recaudoServicio = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstModulo = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();

            lstModulo = recaudoServicio.ListarEmpresaRecaudo(null, (Usuario)Session["usuario"]);

            ddlEmpresa.DataSource = lstModulo;
            ddlEmpresa.DataTextField = "nom_empresa";
            ddlEmpresa.DataValueField = "cod_empresa";
            ddlEmpresa.DataBind();

            ddlEmpresa.Items.Insert(0, "");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(RecaudosMasivosServicio.CodigoProgramaConsulta, "CargarEmpresa", ex);
        }
    }
}