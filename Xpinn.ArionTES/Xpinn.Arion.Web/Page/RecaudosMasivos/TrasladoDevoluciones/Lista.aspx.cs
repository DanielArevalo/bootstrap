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
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;

partial class Lista : GlobalWeb
{
    TrasladoDevolucionServices TrasladoServicios = new TrasladoDevolucionServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(TrasladoServicios.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cargardropdows();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            Actualizar();
        }
    }


    protected void cargardropdows()
    {
        Xpinn.FabricaCreditos.Services.OficinaService oficinaService = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();

        Usuario usuap = (Usuario)Session["usuario"];

        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);
        if (consulta >= 1)
        {
            ddloficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);

            ddloficina.DataTextField = "nombre";
            ddloficina.DataValueField = "codigo";
            ddloficina.DataBind();
            ddloficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
            ddloficina.Enabled = true;
            ddloficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        }
        else
        {
            ddloficina.Items.Insert(0, new ListItem(Convert.ToString(usuap.nombre_oficina), Convert.ToString(usuap.cod_oficina)));
            ddloficina.DataBind();
            ddloficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
            ddloficina.Enabled = false;
            ddloficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
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
            BOexcepcion.Throw(TrasladoServicios.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
          Session[TrasladoServicios.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }


    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvLista.DataKeys[e.RowIndex].Values[0].ToString());
        Session["ID"] = id;
        ctlMensaje.MostrarMensaje("Desea eliminar la Devolución?");
    }
     
    
    
    private void Actualizar()
    {
        try
        {
            List<TrasladoDevolucion> lstConsulta = new List<TrasladoDevolucion>();
            String filtro = obtFiltro();
            String orden;
            orden = obtOrden();
            lstConsulta = TrasladoServicios.ListarTrasladoDevolucion(orden, filtro,(Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           

            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
            }
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString(); 

            Session.Add(TrasladoServicios.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TrasladoServicios.CodigoPrograma, "Actualizar", ex);
        }
    }
     
    private string  obtOrden()
    {
        String orden = " order by fecha_devolucion DESC";
            
        return orden;
    }

    private string obtFiltro()
    {        
        String filtro = String.Empty;
        if (txtIdentificacion.Text.Trim() != "")
            filtro += " and (D.IDENTIFICACION like '%" + txtIdentificacion.Text.Trim() + "%' OR V.IDENTIFICACION like '%" + txtIdentificacion.Text.Trim() + "%') ";       
        if (txtNombre.Text.Trim() != "")
            filtro += " and V.PRIMER_NOMBRE ||' '|| V.SEGUNDO_NOMBRE ||' '||V.PRIMER_APELLIDO ||' '||V.SEGUNDO_APELLIDO like '%" + txtNombre.Text.Trim() + "%'";
        if (ddloficina.SelectedIndex != 0)
            filtro += " and v.cod_oficina = " + ddloficina.SelectedValue + "";
        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina like '%" + txtCodigoNomina.Text + "%'";
        return filtro;
    }


   
}