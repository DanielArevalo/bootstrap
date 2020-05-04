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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
//using System.Globalization;

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.ActasService ActasServicio = new Xpinn.FabricaCreditos.Services.ActasService();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ActasServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
          
            if (!IsPostBack)
            {
             
                CargarValoresConsulta(pConsulta, ActasServicio.CodigoPrograma);
                if (Session[ActasServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActasServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/FabricaCreditos/Actas/Nuevo.aspx");
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        gvLista.Visible = true;

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, ActasServicio.CodigoPrograma);
            Actualizar();
        }       
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.DataSourceID = null;
        gvLista.DataBind();
        gvLista.Visible = false;        
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, ActasServicio.CodigoPrograma);
        txtacta.Text = "";
        txtFechaacta.Text = "";
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ActasServicio.CodigoPrograma + ".id"] = id;
        String fecha = gvLista.Rows[gvLista.SelectedRow.RowIndex].Cells[3].Text;
        Session[ActasServicio.CodigoPrograma + ".fecha"] = fecha;
        Navegar(Pagina.Detalle);


     
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
       // String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        //Session[ReporteServicio.CodigoPrograma + ".id"] = id;

      //  String fecha = gvLista.Rows[e.NewEditIndex].Cells[4].Text;
      //  Session[ReporteServicio.CodigoPrograma + ".fecha"] = fecha;
    
       // Navegar(Pagina.Editar);
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
            BOexcepcion.Throw(ActasServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Credito> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            String filtro = obtFiltroListarActas(ObtenerValores());
            lstConsulta = ActasServicio.ListarActas(ObtenerValores(), (Usuario)Session["usuario"], filtro);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                ValidarPermisosGrilla(gvLista);
           //     Session["ReporteActas"] = txtacta.Text;
          
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ActasServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActasServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.Credito ObtenerValores()
    {
        Configuracion conf = new Configuracion();
        Xpinn.FabricaCreditos.Entities.Credito vCredito = new Xpinn.FabricaCreditos.Entities.Credito();

        if (txtacta.Text.Trim() != "")
            vCredito.acta = Convert.ToInt64(txtacta.Text.Trim());
        if (txtFechaacta.Text.Trim() != "")
            vCredito.fechaacta = Convert.ToString(txtFechaacta.Text.Trim());         
            

        return vCredito;
    }

  

    /// <summary>
    /// Generar las condiciones de acuerdo a los filtros ingresados
    /// </summary>
    /// <param name="credito"></param>
    /// <returns></returns>
    private string obtFiltro(Credito credito)
    {
        String filtro = String.Empty;
       

        if (txtacta.Text.Trim() != "")
            filtro += " and a.codacta='" + credito.acta + "'";

        if(txtFechaacta.Text.Trim() != "")
            filtro += " and to_char(b.fecha,'MM/dd/yyyy')=  '" + credito.fechaacta + "'";

       
        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = " and " + filtro;
        }
        return filtro;
    }
    /// <summary>
    /// Generar las condiciones de acuerdo a los filtros ingresados
    /// </summary>
    /// <param name="credito"></param>
    /// <returns></returns>
    private string obtFiltroListarActas(Credito credito)
    {
        String filtro = String.Empty;


        if (txtacta.Text.Trim() != "")
            filtro += " and a.codacta='" + credito.acta + "'";

        if (txtFechaacta.Text.Trim() != "")
            filtro += " and to_char(b.fecha,'MM/dd/yyyy')=  '" + credito.fechaacta + "'";


        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = " where " + filtro;
        }
        return filtro;
    }


    protected void txtNombre_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtFechaaprobacion_TextChanged(object sender, EventArgs e)
    {

    }
    protected void gvLista_Load(object sender, EventArgs e)
    {
       // Actualizar();
    }
}