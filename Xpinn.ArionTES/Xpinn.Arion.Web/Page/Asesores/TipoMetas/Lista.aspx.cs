using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;
using System.Configuration;

public partial class Lista : GlobalWeb
{
    Usuario usuario = new Usuario();
    EjecutivoMetaService serviceEjecutivo = new EjecutivoMetaService();
    EjecutivoMeta entityEjecutivoMeta = new EjecutivoMeta();
    String operacion = "";
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(serviceEjecutivo.CodigoProgMetas, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;

            ucImprimir.PrintCustomEvent += ucImprimir_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ObtenerDatosDropDownList();
                if (Session[serviceEjecutivo.GetType().Name + ".consulta"] != null)
                    Actualizar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.GetType().Name + "L", "Page_Load", ex);
        }
    }

    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = gvLista;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", Imprimir.JSCRIPT_PRINT);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, serviceEjecutivo.GetType().Name);
      //  Session["operacion"] = "N";
        Session["operacion"] = "N";
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, serviceEjecutivo.GetType().Name);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, serviceEjecutivo.GetType().Name);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ConfirmarEliminarFila(e, "btnBorrar");
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[0].Text;
        Session[serviceEjecutivo.CodigoProgMetas + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[serviceEjecutivo.CodigoProgMetas + ".id"] = id;
        Session[serviceEjecutivo.CodigoProgMetas + ".from"] = "l";
        Session["EditMetaEjecutivo"] = id;
        Session["operacion"] = "E";
        
        Navegar(Pagina.Editar);
    }
    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        if (evt.CommandName == "Editar")
        {
            String[] tmp = evt.CommandArgument.ToString().Split('|');
            EjecutivoMeta ejeMeta = new EjecutivoMeta();
            ejeMeta.IdMeta = Convert.ToInt64(tmp[0]);
            ejeMeta.IdMeta = Convert.ToInt64(tmp[1]);
            ejeMeta.IdMeta = Convert.ToInt64(tmp[2]);
            ejeMeta.IdMeta = Convert.ToInt64(tmp[3]);
            ejeMeta.NombreMeta = tmp[4];
            ejeMeta.formatoMeta = tmp[5];

            Session["EditMetaEjecutivo"] = ejeMeta;
            Session["Ejecutivo"] = ejeMeta.IdEjecutivo;
            Session[serviceEjecutivo.CodigoProgMetas + ".id"] = ejeMeta.IdMeta;
            Session[serviceEjecutivo.CodigoProgMetas + ".from"] = "l";
            Navegar(Pagina.Detalle);
        }
    }
    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            serviceEjecutivo.EliminarMeta(idObjeto, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.CodigoPrograma + "L", "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(serviceEjecutivo.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }
    private string obtFiltro(EjecutivoMeta meta)
    {
        Configuracion conf = new Configuracion();
        String filtro = String.Empty;
        if (this.IdMeta.Text.Trim() != "")
            filtro += " and icodmeta =" + meta.IdMeta;
        if (txtNombreMeta.Text.Trim() != "")
            filtro += " and snombremeta like '%" + meta.NombreMeta + "%'";
       
        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = "where " + filtro;
        }
        return filtro;
    }
    
    private void Actualizar()
    {
        try
        {
            String filtro = obtFiltro(ObtenerValores());
            
            List<EjecutivoMeta> lstConsulta = new List<EjecutivoMeta>();
            EjecutivoMeta meta = new EjecutivoMeta();
            lstConsulta = serviceEjecutivo.ListarMetasFiltro(filtro, (Usuario)Session["usuario"]);

            var lst = from i in lstConsulta


                      select new
                      {
                          i.IdMeta,
                          i.NombreMeta,
                          i.formatoMeta,
                          meta = (i.IdMeta.ToString() + "|" + i.NombreMeta.ToString() + "|" + i.formatoMeta.ToString())
                      };

            lst = lst.OrderBy(i => i.IdMeta).ToList();

            gvLista.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvLista.DataSource = lst;

            if (lst.Count() > 0)
            {
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lst.Count().ToString();
                gvLista.DataBind();
                //   ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
            }

            Session.Add(serviceEjecutivo.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivo.GetType().Name + "L", "Actualizar", ex);
        }
    }


    private EjecutivoMeta ObtenerValores()
    {
        EjecutivoMeta entitymeta= new EjecutivoMeta();

        if (!string.IsNullOrEmpty(txtNombreMeta.Text.Trim()))
            entitymeta.NombreMeta = txtNombreMeta.Text.Trim();
             if (!string.IsNullOrEmpty(IdMeta.Text.Trim()))
                 entitymeta.IdMeta = Convert.ToInt64(IdMeta.Text.Trim());
       

        return entitymeta;
    }

    private void ObtenerDatosDropDownList()
    {
        
    }
}