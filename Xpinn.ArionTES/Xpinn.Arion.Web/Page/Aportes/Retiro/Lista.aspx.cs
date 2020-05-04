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
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;

partial class Lista : GlobalWeb
{
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AporteServicio.ProgramaRetiro, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaRetiro, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboLineaAporte(DdlLineaAporte);               
                CargarValoresConsulta(pConsulta, AporteServicio.ProgramaRetiro);
                if (Session[AporteServicio.ProgramaRetiro + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.ProgramaRetiro, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, AporteServicio.ProgramaRetiro);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, AporteServicio.ProgramaRetiro);
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
            BOexcepcion.Throw(AporteServicio.ProgramaRetiro + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AporteServicio.ProgramaRetiro + ".id"] = id;
        Session["operacion"] = ""; 
        Navegar(Pagina.Nuevo);

    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[AporteServicio.ProgramaRetiro + ".id"] = id;
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(AporteServicio.ProgramaRetiro, "gvLista_PageIndexChanging", ex);
        }
    }

    private void ConsultarCliente(String pIdObjeto)
    {
        Xpinn.Aportes.Services.AporteServices AportesServicio = new Xpinn.Aportes.Services.AporteServices();
        Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();
        String IdObjeto=txtNumeIdentificacion.Text;
        aporte = AportesServicio.ConsultarClienteAporte(IdObjeto,0, (Usuario)Session["usuario"]);

        if (!string.IsNullOrEmpty(aporte.nombre))
            txtNombre.Text = HttpUtility.HtmlDecode(aporte.nombre);
       
    }

    private void Actualizar()
    {
      
        try
        {
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            lstConsulta = AporteServicio.ListarAperturaAporte(ObtenerValores(), (Usuario)Session["usuario"], "Numero_Aporte");

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
        if (DdlLineaAporte.SelectedValue.Trim() != "")
            vAporte.cod_linea_aporte = Convert.ToInt32(DdlLineaAporte.SelectedValue);
      

            return vAporte;
    }

    protected void LlenarComboLineaAporte(DropDownList ddlOficina)
    {
          Xpinn.Aportes.Services.AporteServices aporteService = new  Xpinn.Aportes.Services.AporteServices();       
          Usuario usuap = (Usuario)Session["usuario"];
          Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();
          DdlLineaAporte.DataSource = aporteService.ListarLineaAporte(aporte, (Usuario)Session["usuario"]);
          DdlLineaAporte.DataTextField = "nom_linea_aporte";
          DdlLineaAporte.DataValueField = "cod_linea_aporte";       
          DdlLineaAporte.DataBind();
          DdlLineaAporte.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));       

    }

    protected void btnInfo_Click(object sender, ImageClickEventArgs e)
    {

    }



    protected void DdlOrdenadorpor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }
    protected void DdlLineaAporte_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }
}