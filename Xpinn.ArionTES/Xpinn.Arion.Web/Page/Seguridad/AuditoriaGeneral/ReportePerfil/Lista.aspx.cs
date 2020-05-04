using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Seguridad.Services;
using Xpinn.Seguridad.Entities;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using System.Configuration;
using Xpinn.Util;

public partial class Page_Seguridad_AuditoriaGeneral_ReportePerfil_Lista :GlobalWeb
{
    private Xpinn.Seguridad.Services.Rep_AuditoriaService ReporteaudServicio = new Xpinn.Seguridad.Services.Rep_AuditoriaService();

    Usuario usuario = new Usuario();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            //VisualizarOpciones(ReporteaudServicio.CodigoPrograma, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(ReporteaudServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, ReporteaudServicio.CodigoPrograma);
            Actualizar();

        }
    }

    /// <summary>
    /// Méotodo para limpiar datos de la conuslta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ReporteaudServicio.CodigoPrograma);
        gvLista.DataSource = null;
       

    }

    private void Actualizar()
    {
        try
        {
            List<ReporteAuditoria> lstConsulta = new List<ReporteAuditoria>();

            string filtro = obtFiltro();
            lstConsulta = ReporteaudServicio.ConsultarReportePerfil(filtro,usuario);
            if (lstConsulta.Count>0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
            }
        }

        catch (Exception ex)
        {
            VerError(ex.ToString());
        }
    }
    private string obtFiltro()
    {

        String filtro = String.Empty;
        
        if (txtCodigo.Text.Trim() != "")
           
            filtro += " and U.CODUSUARIO = " + txtCodigo.Text.Trim();
        if (txtUsuario.Text != "")
            filtro += " and U.IDENTIFICACION Like '%" + txtUsuario.Text + "%'";
        //filtro += " and estado ='G'";

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
       
    }

}