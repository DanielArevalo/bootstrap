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
using System.IO;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.Linq;
using System.Diagnostics;

public partial class Lista : GlobalWeb
{
    Usuario usuario = new Usuario();
    EjecutivoMetaService serviceEjecutivoMeta = new EjecutivoMetaService();
    EjecutivoMeta entityEjecutivoMeta = new EjecutivoMeta();
    Int64 fechactual;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[serviceEjecutivoMeta.GetType().Name + ".consulta"] != null)

                VisualizarOpciones(serviceEjecutivoMeta.CodigoProgMetas, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;

            ucImprimir.PrintCustomEvent += ucImprimir_Click; 
          
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivoMeta.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
        try
        {
            if (!IsPostBack)
            {
                DateTime fecha = DateTime.Now;
                fechactual = fecha.Year;
                DdlYear.SelectedValue = Convert.ToString(fechactual);
                CargarValoresConsulta(pConsulta, serviceEjecutivoMeta.GetType().Name);
                if (Session[serviceEjecutivoMeta.GetType().Name + ".consulta"] != null)
                    fechactual = fecha.Year;
                DdlYear.SelectedValue = Convert.ToString(fechactual);
                Actualizar();

            }
            msg.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivoMeta.GetType().Name + "L", "Page_Load", ex);
        }
        
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        //GuardarValoresConsulta(pConsulta, serviceEjecutivoMeta.GetType().Name);
        Navegar(Pagina.Editar);
    }
    

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, serviceEjecutivoMeta.GetType().Name);
        //if (txtMes.Text == "")
        //    txtMes.Text = Convert.ToString(0);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, serviceEjecutivoMeta.GetType().Name);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            serviceEjecutivoMeta.EliminarEjecutivoMeta(idObjeto, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivoMeta.CodigoPrograma + "L", "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        if (evt.CommandName == "Editar")
        {
            String[] tmp = evt.CommandArgument.ToString().Split('|');
            EjecutivoMeta ejeMeta = new EjecutivoMeta();
            ejeMeta.IdEjecutivo = Convert.ToInt64(tmp[0]);
            ejeMeta.IdEjecutivoMeta = Convert.ToInt64(tmp[1]);
            ejeMeta.IdMeta = Convert.ToInt64(tmp[2]);
            ejeMeta.PrimerNombre = tmp[3];
            ejeMeta.SegundoNombre = tmp[4];
            ejeMeta.NombreOficina = tmp[5];
            ejeMeta.Mes = tmp[6];           
            ejeMeta.NombreMeta = tmp[7];
            ejeMeta.VlrMeta =  Convert.ToInt64(tmp[8]);
            ejeMeta.Year = Convert.ToString(tmp[9]);

           

           Session["EditMetaEjecutivo"] = ejeMeta;
            Session["Ejecutivo"] = ejeMeta.IdEjecutivo;
            Session[serviceEjecutivoMeta.CodigoProgMetas + ".id"] = ejeMeta.IdMeta;
            Session[serviceEjecutivoMeta.CodigoProgMetas + ".from"] = "l";
            Navegar(Pagina.Detalle);
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
            BOexcepcion.Throw(serviceEjecutivoMeta.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<EjecutivoMeta> lstConsulta = new List<EjecutivoMeta>();
            EjecutivoMeta meta = new EjecutivoMeta();
            lstConsulta = serviceEjecutivoMeta.ListarEjecutivoMeta(ObtenerValores(), (Usuario)Session["usuario"]);

            var lst = from i in lstConsulta
                      select new
                      {
                          i.IdEjecutivo,
                          i.IdEjecutivoMeta,
                          i.IdMeta,
                          i.PrimerNombre,
                          i.SegundoNombre,
                          i.NombreOficina,
                          i.Mes,                         
                          i.NombreMeta,
                          i.VlrMeta,
                          i.Year,
                          meta = (i.IdEjecutivo.ToString() + "|" + i.IdEjecutivoMeta.ToString() + "|" + i.IdMeta.ToString() + "|" + i.PrimerNombre + "|" + i.SegundoNombre + "|" + i.NombreOficina + "|" + i.Mes + "|" + i.NombreMeta + "|" + i.VlrMeta.ToString()+ "|" + i.Year.ToString())
                      };

            lst = lst.OrderBy(i => i.IdEjecutivo).ToList();

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

            Session.Add(serviceEjecutivoMeta.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivoMeta.GetType().Name + "L", "Actualizar", ex);
        }
    }


    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = gvLista;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", Imprimir.JSCRIPT_PRINT);
    }

    private EjecutivoMeta ObtenerValores()
    {
        entityEjecutivoMeta = new EjecutivoMeta();

        if (!string.IsNullOrEmpty(DdlMes.SelectedValue))
            entityEjecutivoMeta.Mes = DdlMes.SelectedValue;

        if (!string.IsNullOrEmpty(DdlYear.SelectedValue))
            entityEjecutivoMeta.Year = DdlYear.SelectedValue;


        return entityEjecutivoMeta;
    }

       protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
       protected void btnDescargarMetas_Click(object sender, EventArgs e)
       {

       }
       protected void DdlYear_SelectedIndexChanged(object sender, EventArgs e)
       {

       }
}