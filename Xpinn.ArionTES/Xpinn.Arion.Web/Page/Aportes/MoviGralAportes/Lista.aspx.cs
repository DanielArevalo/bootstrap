﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;

partial class Lista : GlobalWeb
{
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AporteServicio.ProgramaAperturaAporte, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {         
                CargarValoresConsulta(pConsulta, AporteServicio.ProgramaAperturaAporte);
                if (Session[AporteServicio.ProgramaAperturaAporte + ".consulta"] != null)
                {
                    Actualizar();
                    Session.Remove(AporteServicio.ProgramaAperturaAporte + ".consulta"); 
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AporteServicio.ProgramaAperturaAporte);
        Navegar(Pagina.Nuevo);
      Session["operacion"] = "N";
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AporteServicio.ProgramaAperturaAporte);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, AporteServicio.ProgramaAperturaAporte);
        txtNombre.Text = "";
        txtNumAporte.Text = "";
        txtNumeIdentificacion.Text="";
        gvLista.DataBind();
   
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AporteServicio.ProgramaAperturaAporte + ".id"] = id;
      //  Navegar(Pagina.Detalle);
        Response.Redirect("~/Page/Aportes/CuentasAportes/Detalle.aspx");
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[AporteServicio.ProgramaAperturaAporte + ".id"] = id;
        Response.Redirect("~/Page/Aportes/CuentasAportes/Nuevo.aspx");
        //Navegar(Pagina.Editar);
        Session["operacion"] = "";
     
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
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
            BOexcepcion.Throw(AporteServicio.ProgramaAperturaAporte, "gvLista_PageIndexChanging", ex);
        }
    }

    private void ConsultarCliente(String pIdObjeto)
    {
        Xpinn.Aportes.Services.AporteServices AportesServicio = new Xpinn.Aportes.Services.AporteServices();
        Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();
        String IdObjeto=txtNumeIdentificacion.Text;
        aporte = AportesServicio.ConsultarClienteAporte(IdObjeto,0, (Usuario)Session["usuario"]);

        if (!string.IsNullOrEmpty(aporte.nombre.ToString()))
            txtNombre.Text = HttpUtility.HtmlDecode(aporte.nombre);
        

    }

    private void Actualizar()
    {
      
        try
        {
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            lstConsulta = AporteServicio.ListarAperturaAporte(ObtenerValores(), (Usuario)Session["usuario"],DdlOrdenadorpor.SelectedValue);

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
            if (txtNumeIdentificacion.Text != "")
            {
                ConsultarCliente(txtNumeIdentificacion.Text);
            }
            else
            {
                txtNombre.Text = "";
            }
            Session.Add(AporteServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoPrograma, "Actualizar", ex);
        }
        
    }

    private Xpinn.Aportes.Entities.Aporte ObtenerValores()
    {
        Xpinn.Aportes.Entities.Aporte vAporte = new Xpinn.Aportes.Entities.Aporte();
        if (txtNumAporte.Text.Trim() != "")
            vAporte.numero_aporte = Convert.ToInt64(txtNumAporte.Text.Trim());
        if  (txtNumeIdentificacion.Text.Trim() != "")
            vAporte.identificacion = Convert.ToString(txtNumeIdentificacion.Text.Trim());
        //if (DdlLineaAporte.SelectedValue.Trim() != "")
           // vAporte.cod_linea_aporte = Convert.ToInt32(DdlLineaAporte.SelectedValue);
        if (DdlEstado.SelectedValue.Trim() != "")
            vAporte.estado = Convert.ToInt32(DdlEstado.SelectedValue);
        if (txtFecha_apertura.Text.Trim() != "")
            vAporte.fecha_apertura = Convert.ToDateTime(txtFecha_apertura.Text.Trim());
        if (txtFecha_vencimiento.Text.Trim() != "")
            vAporte.fecha_proximo_pago = Convert.ToDateTime(txtFecha_vencimiento.Text.Trim());


            return vAporte;
    }

    //protected void LlenarComboLineaAporte(DropDownList ddlOficina)
    //{
    //      Xpinn.Aportes.Services.AporteServices aporteService = new  Xpinn.Aportes.Services.AporteServices();       
    //      Usuario usuap = (Usuario)Session["usuario"];
    //      Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();
    //      DdlLineaAporte.DataSource = aporteService.ListarLineaAporte(aporte, (Usuario)Session["usuario"]);
    //      DdlLineaAporte.DataTextField = "nom_linea_aporte";
    //      DdlLineaAporte.DataValueField = "cod_linea_aporte";       
    //      DdlLineaAporte.DataBind();
    //      DdlLineaAporte.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));       

    //}

    protected void btnInfo_Click(object sender, ImageClickEventArgs e)
    {

    }



    protected void DdlOrdenadorpor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }
}