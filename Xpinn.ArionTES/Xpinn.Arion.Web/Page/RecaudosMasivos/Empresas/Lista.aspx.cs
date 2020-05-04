using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class Lista : GlobalWeb
{
    Xpinn.Tesoreria.Services.EmpresaRecaudoServices recaudoEmpresaService = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(recaudoEmpresaService.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(recaudoEmpresaService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                 Actualizar();
                //mvAplicar.ActiveViewIndex = 0;
                //if (Session[recaudoEmpresaService.CodigoPrograma + ".consulta"] != null)
                //if (Session[recaudoService.GetType().Name + ".consulta"] != null)
                   
            }
            msg.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(recaudoEmpresaService.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void Actualizar()
    {
        // Mostrar los datos
        try
        {
            List<EmpresaRecaudo> lstEmpresa = new List<EmpresaRecaudo>();
            EmpresaRecaudo pEmpresa = new EmpresaRecaudo();

            lstEmpresa = recaudoEmpresaService.ListarEmpresaRecaudo(pEmpresa, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;

            if (lstEmpresa == null)
            {
                Session["DTEMPRESA_RECA"] = null;
                gvLista.Visible = false;
                VerError("No se encontraron Datos");
                return;
            }
            if (lstEmpresa.Count > 0)
            {
                gvLista.DataSource = lstEmpresa;
                gvLista.Visible = true;
                gvLista.DataBind();               
                Session["DTEMPRESA_RECA"] = lstEmpresa;
            }
            else
            {
                Session["DTEMPRESA_RECA"] = null;
                gvLista.Visible = false;
                VerError("No se encontraron Datos");
            }

            // Mostrar el número de registros
            lblTotalRegs.Visible = true;
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstEmpresa.Count.ToString();

            Session.Add(recaudoEmpresaService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(recaudoEmpresaService.CodigoPrograma, "Actualizar", ex);
        }
    }
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Session.Remove(recaudoEmpresaService.CodigoPrograma + ".id");
        Navegar(Pagina.Nuevo);
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
            BOexcepcion.Throw(recaudoEmpresaService.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }
  
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[recaudoEmpresaService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }
    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[recaudoEmpresaService.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }
}